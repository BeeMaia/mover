param name string
param location string = resourceGroup().location

param containerAppsEnvironmentName string
param containerRegistryName string
param containerRegistryUserName string
@secure()
param containerRegistryPassword string
param imageName string 
param serviceName string 
param managedIdentityName string

module app './reusable/container-app.bicep' = {
  name: '${serviceName}-container-app-module'
  params: {
    name: name
    location: location
    tags: {}
    containerAppsEnvironmentName: containerAppsEnvironmentName
    containerRegistryName: containerRegistryName
    containerRegistryUserName: containerRegistryUserName
    containerRegistryPassword: containerRegistryPassword
    containerCpuCoreCount: '1.0'
    containerMemory: '2.0Gi'
    imageName: !empty(imageName) ? imageName : 'nginx:latest'
    daprEnabled: true
    containerName: serviceName
    targetPort: 7001
    managedIdentityEnabled: true
    managedIdentityName: managedIdentityName
  }
}

output SERVICE_API_IDENTITY_PRINCIPAL_ID string = app.outputs.identityPrincipalId
output SERVICE_API_NAME string = app.outputs.name
output SERVICE_API_URI string = app.outputs.uri
output SERVICE_API_IMAGE_NAME string = app.outputs.imageName


