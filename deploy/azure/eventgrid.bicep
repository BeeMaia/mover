param storageAccountName string
param serviceBusTopicName string
param serviceBusName string
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
      endpointType: 'ServiceBusTopic'
      properties: {
        resourceId: topic.id
      }
    }
    eventDeliverySchema: 'CloudEventSchemaV1_0'
    filter: {
      subjectBeginsWith: '/blobServices/default/containers/fit-files/'
      includedEventTypes: [
        'Microsoft.Storage.BlobCreated'
      ]
    }
  }
}

resource topic 'Microsoft.ServiceBus/namespaces/topics@2021-11-01' existing = {
  parent: serviceBusNamespace
  name: serviceBusTopicName
}

resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2021-11-01' existing = {
  name: serviceBusName
}

resource storageAccount 'Microsoft.Storage/storageAccounts@2022-09-01' existing = {
  name: storageAccountName
}

resource managedIdentity 'Microsoft.ManagedIdentity/userAssignedIdentities@2022-01-31-preview' existing = {
  name: managedIdentityName
}
