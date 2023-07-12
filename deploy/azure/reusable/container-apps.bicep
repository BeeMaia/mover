param name string
param location string = resourceGroup().location
param tags object = {}

param containerAppsEnvironmentName string = ''

param logAnalyticsWorkspaceName string = ''
param applicationInsightsName string = ''
param daprEnabled bool = false

module containerAppsEnvironment 'container-apps-environment.bicep' = {
  name: 'env-${name}'
  params: {
    name: containerAppsEnvironmentName
    location: location
    tags: tags
    logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
    applicationInsightsName: applicationInsightsName
    daprEnabled: daprEnabled
  }
}

output environmentName string = containerAppsEnvironment.outputs.name
