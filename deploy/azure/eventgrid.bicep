param storageAccountName string
param webhookEndpointUrl string
param systemTopicName string
param eventSubName string
param location string = resourceGroup().location

resource systemTopic 'Microsoft.EventGrid/systemTopics@2022-06-15' = {
  name: systemTopicName
  location: location
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

