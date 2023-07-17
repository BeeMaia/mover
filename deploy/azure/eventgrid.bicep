param storageAccountName string
param webhookEndpointUrl string
param systemTopicName string
param eventSubName string
param location string = resourceGroup().location
param managedIdentityName string

resource systemTopic 'Microsoft.EventGrid/systemTopics@2022-06-15' = {
  name: systemTopicName
  location: location
  identity:{
    type: 'SystemAssigned, UserAssigned'
    userAssignedIdentities: {
      '${managedIdentity.id}' : {}
    }
  }
  properties: {
    source: storageAccount.id
    topicType: 'Microsoft.Storage.StorageAccounts'
  }
}

resource eventSubscription 'Microsoft.EventGrid/systemTopics/eventSubscriptions@2022-06-15' = {
  parent: systemTopic
  name: eventSubName
  properties: {
    destination: {
      endpointType: 'WebHook'
      properties: {
        endpointUrl: webhookEndpointUrl
        deliveryAttributeMappings: [
          {
	          name: 'Access-Control-Request-Method'
	          type: 'Static'
	          properties: {
	            value: '*'
	            isSecret: false
	          }
          }
	        {
	          name: 'Origin'
	          type: 'Static'
	          properties: {
	            value: '${systemTopic.name}.azure.com'
	            isSecret: false
	          }
	        }
	      ]
      }
    }
    eventDeliverySchema: 'CloudEventSchemaV1_0'
    filter: {
      includedEventTypes: [
        'Microsoft.Storage.BlobCreated'
      ]
    }
  }
}

resource storageAccount 'Microsoft.Storage/storageAccounts@2022-09-01' existing = {
  name: storageAccountName
}

resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2022-01-31-preview' existing = {
  name: managedIdentityName
}
