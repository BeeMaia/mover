# Deploy via GitHub Actions

The entire solution is configured with [GitHub Actions](https://github.com/features/actions) and [Bicep](https://docs.microsoft.com/azure/azure-resource-manager/bicep/overview) for CI/CD

## Pre

Create a custom role for allow to assign roles during deployments

```
az role definition create --role-definition '{
"Name": "Contributor Assign Roles",
"Id": "1a200ac6-5a49-4198-9403-0af86342bd35",
"IsCustom": true,
"Description": "Grants full access to manage all resources, allow you to assign roles in Azure RBAC but not delete roles in Azure RBAC manage assignments in Azure Blueprints, or share image galleries.",
"Actions": [
""
],
"NotActions": [
"Microsoft.Authorization/*/Delete",
"Microsoft.Authorization/elevateAccess/Action",
"Microsoft.Blueprint/blueprintAssignments/write",
"Microsoft.Blueprint/blueprintAssignments/delete",
"Microsoft.Compute/galleries/share/action"
],
"DataActions": [],
"NotDataActions": [],
"AssignableScopes": [
"/subscriptions/TYPE_YOUR_SUBSCRIPTION_ID_HERE"
]
}'
```

1. Create the following required [encrypted secrets](https://docs.github.com/en/actions/security-guides/encrypted-secrets#creating-encrypted-secrets-for-a-repository) for the sample

| Name              | Value                                                                                                                                                                                                                                                                                                   |
| ----------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| AZURE_CREDENTIALS | The JSON credentials for an Azure subscription. Make sure the Service Principal has permissions at the subscription level scope [Learn more](https://docs.microsoft.com/azure/developer/github/connect-from-azure?tabs=azure-portal%2Cwindows#create-a-service-principal-and-add-it-as-a-github-secret) |
| RESOURCE_GROUP    | The name of the resource group to create                                                                                                                                                                                                                                                                |
| REGISTRY_LOGINURL | The registry url                                                                                                                                                                                                                                                                                        |
| REGISTRY_USERNAME | The client id of AZURE_CREDENTIALS as registry username                                                                                                                                                                                                                                                 |
| REGISTRY_PASSWORD | The client secret of AZURE_CREDENTIALS as registry password                                                                                                                                                                                                                                             |

2. Open GitHub Actions, select the **1. Setup Container registry** action and choose to run the workflow.

   The GitHub action performs the following actions:

   - Create an Azure Resource Group using the `RESOURCE_GROUP` secret
   - Create an Azure Container Registry

3. Open GitHub Actions, select the **2. Build and Publish to Registry** action and choose to run the workflow.

   The GitHub action performs the following actions:

   - Build the code and container image for webapi
   - Push the images to the Azure Container Registry using the `REGISTRY_USERNAME` and `REGISTRY_PASSWORD`

4. Open GitHub Actions, select the **3. Setup environment** action and choose to run the workflow.

   The GitHub action performs the following actions:

   - Create an Azure Container Apps environment with an associated Log Analytics workspace and App Insights instance for Dapr distributed tracing
   - Deploy Container Apps for webapi

5. Once the GitHub Actions have completed successfully, navigate to the [Azure Portal](https://portal.azure.com) and select the resource group you created.
