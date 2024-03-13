param location string

param managedIdentityEnabled bool = false
param managedIdentityName string = ''
param containerAppsEnvironmentName string = ''

param serviceName string 

resource envoyApp 'Microsoft.App/containerApps@2022-03-01' = {
  name: serviceName
  location: location
  identity: managedIdentityEnabled ? {
    type: 'SystemAssigned,UserAssigned'
    userAssignedIdentities: {
      '${managedIdentity.id}' : {}
    }
  } : { type: 'None' }
  dependsOn: [managedIdentity]
  properties: {
    managedEnvironmentId: containerAppsEnvironment.id
    template: {
      containers: [
        {
          name: serviceName
          image: 'envoyproxy/envoy-alpine:v1.21-latest'
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 1
      }
    }
    configuration: {
      activeRevisionsMode: 'single'
      dapr: {
        enabled: true
        appId: 'envoygw'
        appPort: 10000
      }
      ingress: {
        external: true
        targetPort: 10000
        allowInsecure: true
      }
    }
  }
}

resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' existing = {
  name: managedIdentityName
}

resource containerAppsEnvironment 'Microsoft.App/managedEnvironments@2022-10-01' existing = {
  name: containerAppsEnvironmentName
}
