param name string
param managedIdentityName string
param location string
param skuName string = 'Standard'
param tags object = {}

resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2021-11-01' = {
  name: name
  location: location
  tags: tags
  sku: {
    name: skuName
    tier: skuName
  }
}

// See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#azure-service-bus-data-sender
var roleIdS = '69a216fc-b8fb-44d8-bc22-1f3c2cd27a39' // Azure Service Bus Data Sender
// See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#azure-service-bus-data-receiver
var roleIdR = '4f6d3b9b-027b-4f4c-9142-0e5a2a2247e0' // Azure Service Bus Data Receiver

resource roleAssignmentReceiver 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(serviceBusNamespace.id, roleIdR, managedIdentityName)
  scope: serviceBusNamespace
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', roleIdR)
    principalId: managedIdentity.properties.principalId
    principalType: 'ServicePrincipal' // managed identity is a form of service principal
  }
}

// Grant permissions to the managedIdentity to specific role to servicebus
resource roleAssignmentSender 'Microsoft.Authorization/roleAssignments@2020-04-01-preview' = {
  name: guid(serviceBusNamespace.id, roleIdS, managedIdentityName)
  scope: serviceBusNamespace
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', roleIdS)
    principalId: managedIdentity.properties.principalId
    principalType: 'ServicePrincipal' // managed identity is a form of service principal
  }
}

resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2022-01-31-preview' existing = {
  name: managedIdentityName
}

output SERVICEBUS_ENDPOINT string = serviceBusNamespace.properties.serviceBusEndpoint
output serviceBusName string = serviceBusNamespace.name
