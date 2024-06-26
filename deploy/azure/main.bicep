@minLength(1)
@description('Primary location for all resources')
param location string = resourceGroup().location

@minLength(1)
@description('Environment for all resources')
param environment string = 'dev'

param registryName string

var appName = 'mover'
var uploaderSN = 'uploader'
var statsSN = 'stats'
var authSN = 'auth'
var fitdecoderSN = 'fitdecoder'
var frontendSN = 'frontend'
var resourceToken = toLower('${appName}${environment}')
var abbrs = loadJsonContent('./abbreviations.json')
var tags = {}

// Monitor application with Azure Monitor
module monitoring './reusable/monitoring.bicep' = {
  name: 'monitoring'
  params: {
    location: location
    tags: tags
    logAnalyticsName: '${abbrs.operationalInsightsWorkspaces}${resourceToken}'
    applicationInsightsName: '${abbrs.insightsComponents}${resourceToken}'
    applicationInsightsDashboardName: '${abbrs.portalDashboards}${resourceToken}'
  }
}

// Shared App Env
module appEnv './reusable/app-env.bicep' = {
  name: '${deployment().name}-app-env'
  params: {
    containerAppsEnvName: '${abbrs.appManagedEnvironments}${resourceToken}'
    location: location
    logAnalyticsWorkspaceName: monitoring.outputs.logAnalyticsWorkspaceName
    applicationInsightsName: monitoring.outputs.applicationInsightsName
    daprEnabled: true
  }
}

// Setup managed identity
module security './reusable/security.bicep' = {
  name: 'security'
  params: {
    managedIdentityName: '${abbrs.managedIdentityUserAssignedIdentities}${resourceToken}'
    location: location
  }
}

module storage './reusable/storage-account.bicep' = {
  name: 'storage'
  params: {
    name: '${abbrs.storageStorageAccounts}${resourceToken}'
    location: location
    managedIdentityName: security.outputs.managedIdentityName
    containers: [
      {
        name: 'fit-files'
      }  
      {
        name: 'gpx-files'
      }
      {
        name: 'raw-files'
      }
    ]
  }
}

module serviceBus './reusable/servicebus.bicep' = {
  name: 'serviceBus'
  params: {
    name:'${abbrs.serviceBusNamespaces}${resourceToken}'
    location:location
    managedIdentityName: security.outputs.managedIdentityName
    topics: [
      'decodefit'
      'uploadfile'
      'uploadedfit'
      'uploadedgpx'
      'writestats'
      'wrotestats'
    ]
  }
}

module dapr 'dapr.bicep' = {
  name: 'dapr'
  params: {
    containerAppsEnvironmentName: appEnv.outputs.environmentName
    managedIdentityName: security.outputs.managedIdentityName
    storageAccountName: storage.outputs.name
    serviceBusName: serviceBus.outputs.serviceBusName 
    keyVaultName: keyvault.outputs.vaultName
    scopes: [
      uploaderSN
      fitdecoderSN
      statsSN
      authSN
    ]
  }
}

module uploader 'api.bicep' = {
  name: uploaderSN
  params: {
    name: '${abbrs.appContainerApps}${uploaderSN}-${resourceToken}'
    location: location
    imageName: 'mover-uploader:main'
    containerAppsEnvironmentName: appEnv.outputs.environmentName
    containerRegistryName: registryName
    serviceName: uploaderSN
    managedIdentityName: security.outputs.managedIdentityName
    applicationInsightsName: monitoring.outputs.applicationInsightsName
  }
  dependsOn:[dapr]
}

module fitdecoder 'api.bicep' = {
  name: fitdecoderSN
  params: {
    name: '${abbrs.appContainerApps}${fitdecoderSN}-${resourceToken}'
    location: location
    imageName: 'mover-fitdecoder:main'
    containerAppsEnvironmentName: appEnv.outputs.environmentName
    containerRegistryName: registryName
    serviceName: fitdecoderSN
    managedIdentityName: security.outputs.managedIdentityName
    applicationInsightsName: monitoring.outputs.applicationInsightsName
  }
  dependsOn:[dapr]
}

module stats 'api.bicep' = {
  name: statsSN
  params: {
    name: '${abbrs.appContainerApps}${statsSN}-${resourceToken}'
    location: location
    imageName: 'mover-stats:main'
    containerAppsEnvironmentName: appEnv.outputs.environmentName
    containerRegistryName: registryName
    serviceName: statsSN
    managedIdentityName: security.outputs.managedIdentityName
    applicationInsightsName: monitoring.outputs.applicationInsightsName
  }
  dependsOn:[dapr]
}

module auth 'api.bicep' = {
  name: authSN
  params: {
    name: '${abbrs.appContainerApps}${authSN}-${resourceToken}'
    location: location
    imageName: 'mover-auth:main'
    containerAppsEnvironmentName: appEnv.outputs.environmentName
    containerRegistryName: registryName
    serviceName: authSN
    managedIdentityName: security.outputs.managedIdentityName
    applicationInsightsName: monitoring.outputs.applicationInsightsName
  }
  dependsOn:[dapr]
}

module frontend 'frontend.bicep' = {
  name: frontendSN
  params: {
    name: '${abbrs.appContainerApps}${frontendSN}-${resourceToken}'
    location: location
    imageName: 'mover-fe:main'
    containerAppsEnvironmentName: appEnv.outputs.environmentName
    containerRegistryName: registryName
    serviceName: frontendSN
    managedIdentityName: security.outputs.managedIdentityName
    applicationInsightsName: monitoring.outputs.applicationInsightsName
  }
}

module envoy 'envoy.bicep' = {
  name: 'envoy'
  params: {
    name: '${abbrs.appContainerApps}envoy-${resourceToken}'
    location: location
    imageName: 'envoy:main'
    containerAppsEnvironmentName: appEnv.outputs.environmentName
    containerRegistryName: registryName
    serviceName: 'envoy'
    managedIdentityName: security.outputs.managedIdentityName
    applicationInsightsName: monitoring.outputs.applicationInsightsName
  }
}

module cosmosdb 'cosmos-db.bicep' = {
  name:'cosmosdb'
  params: {
    cosmosAccountName: '${abbrs.documentDBDatabaseAccounts}${resourceToken}'
    cosmosDbName: appName
    location: location
  }
}

module sqlServer 'sql-server.bicep' = {
  name: 'sqlServer'
  params: {
    location: location
    sqlServerName: '${abbrs.sqlServers}${resourceToken}'
  }
}

module keyvault 'keyvault.bicep' = {
  name:'keyvault'
  params: {
    vaultName: '${abbrs.keyVaultVaults}${resourceToken}'
    location: location
    managedIdentityObjectId: security.outputs.managedIdentityObjectId
    moverDbConnString: cosmosdb.outputs.connectionString
    moverSqlConnString: sqlServer.outputs.identityDbConnectionString
  }
}
