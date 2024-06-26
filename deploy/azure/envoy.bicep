param name string
param location string = resourceGroup().location

param containerAppsEnvironmentName string
param containerRegistryName string
param imageName string 
param serviceName string 
param managedIdentityName string
param applicationInsightsName string

module app './reusable/container-app.bicep' = {
  name: '${serviceName}-container-app-module'
  params: {
    name: name
    location: location
    tags: {}
    containerAppsEnvironmentName: containerAppsEnvironmentName
    containerRegistryName: containerRegistryName
    containerCpuCoreCount: '0.5'
    containerMemory: '1.0Gi'
    env:[
      {
        name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
        value: applicationInsights.properties.ConnectionString
      }
    ]
    imageName: !empty(imageName) ? imageName : 'nginx:latest'
    daprEnabled: true
    containerName: serviceName
    targetPort: 80
    managedIdentityEnabled: true
    managedIdentityName: managedIdentityName
  }
}

resource applicationInsights 'Microsoft.Insights/components@2020-02-02' existing = if (applicationInsightsName != '') {
  name: applicationInsightsName
}

output SERVICE_API_IDENTITY_PRINCIPAL_ID string = app.outputs.identityPrincipalId
output SERVICE_API_NAME string = app.outputs.name
output SERVICE_API_URI string = app.outputs.uri
output SERVICE_API_IMAGE_NAME string = app.outputs.imageName
