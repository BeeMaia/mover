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
        objectId: managedIdentityObjectId
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
  parent: keyVault
  name: 'moverDbConnString'
  properties: {
    value: moverDbConnString
  }
}

resource jwtKeySecret 'Microsoft.KeyVault/vaults/secrets@2022-07-01' = {
  parent: keyVault
  name: 'jwtKey'
  properties: {
    value: 'EB5122690A16C347B5FDC19CC7F43E910373E5BD512957EE2887F52C14B57C3D'
  }
}

output vaultName string = keyVault.name
