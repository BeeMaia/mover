param containerAppsEnvName string
param location string
param logAnalyticsWorkspaceName string
param applicationInsightsName string
param daprEnabled bool = false

// Container apps host (including container registry)
module containerApps 'container-apps.bicep' = {
  name: 'container-apps'
  params: {
    name: 'mover'
    containerAppsEnvironmentName: containerAppsEnvName
    location: location
    logAnalyticsWorkspaceName: logAnalyticsWorkspaceName
    applicationInsightsName: applicationInsightsName
    daprEnabled: daprEnabled
  }
}

output environmentName string = containerApps.outputs.environmentName
