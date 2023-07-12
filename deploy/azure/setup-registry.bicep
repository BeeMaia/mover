@minLength(1)
@description('Primary location for all resources')
param location string = resourceGroup().location

@minLength(1)
@description('Environment for all resources')
param environment string = 'dev'


var appName = 'mover'
var resourceToken = toLower('${appName}${environment}')
var abbrs = loadJsonContent('./abbreviations.json')
var tags = {}

module containerRegistry './reusable/container-registry.bicep' = {
  name: 'cr-${appName}'
  params: {
    name: '${abbrs.containerRegistryRegistries}${resourceToken}'
    location: location
    tags: tags
  }
}

output registryLoginServer string = containerRegistry.outputs.loginServer
output registryName string = containerRegistry.outputs.name
