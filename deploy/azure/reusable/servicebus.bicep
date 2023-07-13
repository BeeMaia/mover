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

@description('This is the built-in Azure Service Bus Data Sender role. See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#azure-service-bus-data-sender')
resource senderRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-04-01' existing = {
  scope: serviceBusNamespace
  name: '69a216fc-b8fb-44d8-bc22-1f3c2cd27a39'
}

@description('This is the built-in Azure Service Bus Data Receiver role. See https://learn.microsoft.com/en-us/azure/role-based-access-control/built-in-roles#azure-service-bus-data-receiver')
resource receiverRoleDefinition 'Microsoft.Authorization/roleDefinitions@2022-04-01' existing = {
  scope: serviceBusNamespace
  name: '4f6d3b9b-027b-4f4c-9142-0e5a2a2247e0'
}

resource roleAssignmentReceiver 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(resourceGroup().id, receiverRoleDefinition.id)
  scope: serviceBusNamespace
  properties: {
    roleDefinitionId: receiverRoleDefinition.id
    principalId: managedIdentity.properties.principalId
    principalType: 'ServicePrincipal' // managed identity is a form of service principal
  }
}

// Grant permissions to the managedIdentity to specific role to servicebus
resource roleAssignmentSender 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(resourceGroup().id, senderRoleDefinition.id)
  scope: serviceBusNamespace
  properties: {
    roleDefinitionId: senderRoleDefinition.id
    principalId: managedIdentity.properties.principalId
    principalType: 'ServicePrincipal' // managed identity is a form of service principal
  }
}

resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2023-01-31' existing = {
  name: managedIdentityName
}

output SERVICEBUS_ENDPOINT string = serviceBusNamespace.properties.serviceBusEndpoint
output serviceBusName string = serviceBusNamespace.name
