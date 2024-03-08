param location string
param vaultName string 
param managedIdentityObjectId string
param moverDbConnString string

resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' = {
  name: vaultName
  location: location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: subscription().tenantId
    accessPolicies: [
      {
        tenantId: subscription().tenantId
        managedIdentityObjectId: managedIdentityObjectId
        permissions: {
          secrets: [
            'get'
            'list'
            'set'
            'delete'
          ]
        }
      }
    ]
    enabledForDeployment: false
    enabledForDiskEncryption: false
    enabledForTemplateDeployment: false
    enableSoftDelete: false
  }
}

resource moverDbConnectionStringSecret 'Microsoft.KeyVault/vaults/secrets@2022-07-01' = {
  name: '${keyVault.name}/moverDbConnString'
  properties: {
    value: moverDbConnString
  }
}

output vaultName string = keyVault.name
