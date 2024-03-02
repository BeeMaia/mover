param managedIdentityName string
param containerAppsEnvironmentName string 
param storageAccountName string
param serviceBusName string
param containerAppName string

resource fitBlobDaprComponent 'Microsoft.App/managedEnvironments/daprComponents@2022-03-01' = {
  name: 'mover-fitblob'
  parent: containerAppsEnvironment
  properties: {
    componentType: 'bindings.azure.blobstorage'
    version: 'v1'
    ignoreErrors: false
    initTimeout: '5s'
    metadata: [
      {
        name: 'accountName'
        value: storageAccountName
      }
      {
        name: 'containerName'
        value: 'fit-files'
      }
      {
        name: 'accountKey'
        value: storageAccount.listKeys().keys[0].value
      }
    ]
    scopes: [
      containerAppName
    ]
  }
  dependsOn: [
    storageAccount
  ]
}

resource gpxBlobDaprComponent 'Microsoft.App/managedEnvironments/daprComponents@2022-03-01' = {
  name: 'mover-gpxblob'
  parent: containerAppsEnvironment
  properties: {
    componentType: 'bindings.azure.blobstorage'
    version: 'v1'
    ignoreErrors: false
    initTimeout: '5s'
    metadata: [
      {
        name: 'accountName'
        value: storageAccountName
      }
      {
        name: 'containerName'
        value: 'gpx-files'
      }
      {
        name: 'accountKey'
        value: storageAccount.listKeys().keys[0].value
      }
      {
        name: 'decodeBase64'
        value: 'true'
      }
    ]
    scopes: [
      containerAppName
    ]
  }
  dependsOn: [
    storageAccount
  ]
}

resource rawBlobDaprComponent 'Microsoft.App/managedEnvironments/daprComponents@2022-03-01' = {
  name: 'mover-rawblob'
  parent: containerAppsEnvironment
  properties: {
    componentType: 'bindings.azure.blobstorage'
    version: 'v1'
    ignoreErrors: false
    initTimeout: '5s'
    metadata: [
      {
        name: 'accountName'
        value: storageAccountName
      }
      {
        name: 'containerName'
        value: 'raw-files'
      }
      {
        name: 'accountKey'
        value: storageAccount.listKeys().keys[0].value
      }
    ]
    scopes: [
      containerAppName
    ]
  }
  dependsOn: [
    storageAccount
  ]
}

resource pubsubDaprComponent 'Microsoft.App/managedEnvironments/daprComponents@2022-03-01' = {
  name: 'mover-pubsub'
  parent: containerAppsEnvironment
  properties: {
    componentType: 'pubsub.azure.servicebus.topics'
    version: 'v1'
    ignoreErrors: false
    initTimeout: '5s'
    metadata: [
      {
        name: 'namespaceName'
        value: '${serviceBusName}.servicebus.windows.net'
      }
      {
        name: 'azureClientId'
        value: managedIdentity.properties.clientId
      }
    ]
    scopes: [
      containerAppName
    ]
  }
  dependsOn: [
    serviceBusNamespace
  ]
}

resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2022-01-31-preview' existing = {
  name: managedIdentityName
}

resource containerAppsEnvironment 'Microsoft.App/managedEnvironments@2022-03-01' existing = {
  name: containerAppsEnvironmentName
}

resource storageAccount 'Microsoft.Storage/storageAccounts@2022-05-01' existing = {
  name: storageAccountName
}

resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2021-11-01' existing = {
  name: serviceBusName
}
