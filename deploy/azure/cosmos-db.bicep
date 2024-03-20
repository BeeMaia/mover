param location string
param cosmosAccountName string
param cosmosDbName string

resource cosmosAccount 'Microsoft.DocumentDB/databaseAccounts@2023-11-15' = {
  name: cosmosAccountName
  location: location
  kind: 'MongoDB'
  properties: {
    apiProperties:{
      serverVersion: '4.2'
    }
    databaseAccountOfferType: 'Standard'
    consistencyPolicy: {
      defaultConsistencyLevel: 'Session'
    }
    locations: [
      {
        locationName: location
      }
    ]
    capabilities: [
      {
        name: 'EnableServerless'
      }
    ]
  }
}

resource cosmosDb 'Microsoft.DocumentDB/databaseAccounts/mongodbDatabases@2023-11-15' = {
  parent: cosmosAccount
  name: cosmosDbName
  properties: {
    resource: {
      id: cosmosDbName
    }
  }
}

resource cosmosCollection 'Microsoft.DocumentDB/databaseAccounts/mongodbDatabases/collections@2023-11-15' = {
  parent: cosmosDb
  name: 'activities'
  properties: {
    resource: {
      id: 'activities'
      indexes:[
        {
          key:{
            keys:[
              'Timestamp'
            ]
          }
        }
      ]
    }
  }
}

output cosmosAccountName string = cosmosAccount.name
output cosmosDbName string = cosmosDbName
output cosmosUrl string = cosmosAccount.properties.documentEndpoint
output cosmosKey string = cosmosAccount.listKeys().primaryMasterKey
output connectionString string = cosmosAccount.listConnectionStrings().connectionStrings[0].connectionString
