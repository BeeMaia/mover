@minLength(1)
@description('Primary location for all resources')
param location string = resourceGroup().location

@minLength(1)
@description('Environment for all resources')
param environment string = 'dev'

@description('The image name for the api service')
param apiImageName string 

var appName = 'mover'
var apiServiceName = 'api'
var resourceToken = toLower('${appName}${environment}')
var abbrs = loadJsonContent('./abbreviations.json')
var tags = {}

// Monitor application with Azure Monitor
module monitoring 'monitoring.bicep' = {
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
module appEnv 'app-env.bicep' = {
  name: '${deployment().name}-app-env'
  params: {
    containerAppsEnvName: '${abbrs.appManagedEnvironments}${resourceToken}'
    containerRegistryName: '${abbrs.containerRegistryRegistries}${resourceToken}'
    location: location
    logAnalyticsWorkspaceName: monitoring.outputs.logAnalyticsWorkspaceName
    applicationInsightsName: monitoring.outputs.applicationInsightsName
    daprEnabled: true
  }
}

// Setup managed identity
module security 'security.bicep' = {
  name: 'security'
  params: {
    managedIdentityName: '${abbrs.managedIdentityUserAssignedIdentities}${resourceToken}'
    location: location
  }
}

module storage 'storage-account.bicep' = {
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
    ]
  }
}

module serviceBus 'servicebus.bicep' = {
  name: 'serviceBus'
  params:{
    name:'${abbrs.serviceBusNamespaces}${resourceToken}'
    location:location
    managedIdentityName: security.outputs.managedIdentityName
  }
}

module dapr 'dapr.bicep' = {
  name: 'daprComponents'
  params: {
    containerAppsEnvironmentName: appEnv.outputs.environmentName
    managedIdentityName: security.outputs.managedIdentityName
    storageAccountName: storage.outputs.name
    serviceBusName: serviceBus.outputs.serviceBusName 
    containerAppName: '${abbrs.appContainerApps}${apiServiceName}-${resourceToken}'
  }
}

module api 'api.bicep' = {
  name: apiServiceName
  params: {
    name: '${abbrs.appContainerApps}${apiServiceName}-${resourceToken}'
    location: location
    imageName: apiImageName
    containerAppsEnvironmentName: appEnv.outputs.environmentName
    containerRegistryName: appEnv.outputs.registryName
    serviceName: apiServiceName
    managedIdentityName: security.outputs.managedIdentityName
  }
  dependsOn:[dapr]
}
