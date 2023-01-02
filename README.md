Learn Portal link is: [Microsoft Learn Portal](https://docs.microsoft.com/en-us/learn/)

Additional Information from Udemy: [Udemy Learn and Questions Pack](https://www.udemy.com/course/az-204-mock-test/learn/quiz/5134706#overview)  
**Pay attention, this questions come from legal sources, this is NOT HORSES**

Additional Resources you could find here: [thomasmaurer blog, Azure Developer](https://www.thomasmaurer.ch/2020/03/az-204-study-guide-developing-solutions-for-microsoft-azure/)

# AZ-204-Microsoft-Azure-Developer
This repo consists of materials gathering from different sources which help you to successfully pass  official Microsoft Azure Developer exam.

# Functions
## Function with write to Queue Storage and to Blob Storage
My function v3 (most modern) example: [Example in C#](https://github.com/Glareone/AZ-204-Microsoft-Azure-Developer/blob/main/EShopFunctionApp/FunctionApp1/OrderItemsReserver.cs)   
My example: [Prepared example in c#](https://github.com/Glareone/AZ-204-Microsoft-Azure-Developer/tree/main/Functions%20and%20Write%20to%20QueueStorage%20and%20to%20Blob/FunctionWithStorage)

## Functions With Docker and Azure Container Registry
My example: [Prepared example in c#](https://github.com/Glareone/AZ-204-Microsoft-Azure-Developer/tree/main/Function%20with%20Docker%20and%20Azure%20Container%20Registry)

* Build Azure Function with Docker:
`docker build --tag alekseikolesnikov/azurefunctionsimage:v.1.0.0 .`
* Run Azure Function Docker:
`docker run -p 8080:80 -it alekseikolesnikov/azurefunctionsimage:v.1.0.0`
* To Publish Azure Function into Container Registry you must add Docker Support to your project (you cant do that initially)

# Durable Functions
## Durable Functions. "Orchestrator"  
link: [Durable functions example](https://docs.microsoft.com/en-us/learn/modules/create-long-running-serverless-workflow-with-durable-functions/4-exercise-create-a-workflow-using-durable-functions)

* Useful in situations when you need to organize your work with several Functions and with People (not high-available elements)
* Useful for chain of responsibility pattern
Consisft of Several Parts:   
1) Durable HTTP Start. Runs your queue. Runs your Durable Orchestrator (place it in "in" connection). It's your entry point and this fucntion can be used via browser.
2) Durable Orchestrator Function. Runs your little Activity Functions (which can make some work).
3) Durable Activity Function. Does the job.

Example workflow:  

* A project design is submitted.
* An approval task is allocated to a manager, so they can review the project design proposal.
* The project design proposal is rejected or approved.
* An escalation task is allocated if the approval task isn't completed within a pre-defined time limit.

![image](https://user-images.githubusercontent.com/4239376/111462948-b714cc80-8727-11eb-8bd9-8192c8b9359c.png)
![image](https://user-images.githubusercontent.com/4239376/111463227-065afd00-8728-11eb-8ded-2f8e34705724.png)

## Durable Orchestrator Functions with timer.
link: [Orchestrator Function with Timer](https://docs.microsoft.com/en-us/learn/modules/create-long-running-serverless-workflow-with-durable-functions/5-control-long-running-tasks-using-timers)

* You should use durable timers in orchestrator functions instead of the setTimeout() and setInterval() functions.

Example: [Watch Function Example Using Visual Studio 2019](https://github.com/Glareone/AZ-204-Microsoft-Azure-Developer/tree/main/WatchPortalFunction/WatchPortalFunction/WatchPortalFunction)

# Azure Event Hub (for High Load of Events);

1. First you need to create Namespace:  
  a. Select and push to bash your selected name: `NS_NAME=event-hub-namespace`  
  b. Create Event Hub Namespace: `az eventhubs namespace create --name event-hub-namespace`  
  c. Get JSON Configuration (with connections) to your created namespace: `az eventhubs namespace authorization-rule keys list     --name RootManageSharedAccessKey     --namespace-name $NS_NAME`  
2. Create Event Hub:  
  a. New Hub Name: `HUB_NAME=hubname-$RANDOM`  
  b. Create Event Hub: `az eventhubs eventhub create --name $HUB_NAME --namespace-name $NS_NAME`  
  c. Check that Event Hub created properly: `az eventhubs eventhub show --namespace-name $NS_NAME --name $HUB_NAME`  
  
## Event Hub vs Event Grid:
The noticeable difference between them is that Event Hubs are accepting only endpoints for the ingestion of data and they don't provide a mechanism for sending data back to publishers. On the other hand, Event Grid sends HTTP requests to notify events that happen in publishers.  
[Good Article about the difference](https://www.serverless360.com/blog/azure-event-grid-vs-event-hub#:~:text=The%20noticeable%20difference%20between%20them,events%20that%20happen%20in%20publishers.)
  
### Azure Event Hub Resilience: 
  `Azure Event Hubs keeps received messages from your sender application, even when the hub is unavailable. Messages received after the hub becomes unavailable are successfully transmitted to our application as soon as the hub becomes available.`
  **Event Hub, not Event Hub Namespace. You can Disable(turn off) the Event Hub to test that. All messages will appear when you turn on you Event Hub again.**

# Blob Storage and live example how to work with it (but library is deprecated, not a big deal anyway)
* Application works in Azure Environment. Otherwise, you have to create and define this configuration file on your own (`services.Configure<AzureStorageConfig>(Configuration.GetSection("AzureStorageConfig"));`)
* Better to store this code in AppService.
    1) Create a plan `az appservice plan create \  
   --name blob-exercise-plan \  
   --resource-group learn-21e4e8a8-bc24-473a-ab32-9db698dcb993 \  
   --sku FREE --location centralus`  
    2) Create WebApp `az webapp create \  
       --name <your-unique-app-name> \  
       --plan blob-exercise-plan \  
       --resource-group learn-21e4e8a8-bc24-473a-ab32-9db698dcb993`  
    3) get Connection string `CONNECTIONSTRING=$(az storage account show-connection-string \  
       --name <your-unique-storage-account-name> \  
       --output tsv)`  
       
    4) Create AppSettings config webapp `CONNECTIONSTRING=$(az storage account show-connection-string \  
       --name <your-unique-storage-account-name> \  
       --output tsv)`  
  Link to the BlobStorage + .Net MVC Project example: [Blob Storage + MVC](https://github.com/Glareone/AZ-204-Microsoft-Azure-Developer/tree/main/BlobStorage%2BMVC/WorkingWithBlobStorage)
  
# Azure CLI Tips:
## Azure CLI with bash syntax
**To use az module you have to install it first! It has all commands which Azure PowerShell provides**

* Azure bot can help you to find information about anything from Azure World:  
`azure find KEY_WORD` - to find anything tied with your keyword, i.e. `azure find "blob storage create"` or `azure find blob-storage-create` (first example with quotes usually works better)
* To find information about proper command (like to get the list of all parameters): `az YOUR_COMMAND --help`  
* To find information in bash you may also use `az find "YOUR_QUESTION"`

## On-premise gateway & Isolated Service Environment & Direct Link | Peer-2-Site Site-2-Site. Logic App, Power BI, Power Apps connection to on-premise data and network. 
### App Gateway
#### Connect On-Premise Resources to LogicApp
[Logic App Gateway Install overview](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-gateway-install)  
[logic-apps-gateway](https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-gateway-connection)  
### ISE - Integration Service Environment. Connect to On-premise resources. Isolation.
[isolated-environment-overview](https://docs.microsoft.com/en-us/azure/logic-apps/connect-virtual-network-vnet-isolated-environment-overview)  
### On-premise gateway
[P2P & S2S & Express Route](https://docs.microsoft.com/en-us/azure/virtual-network/virtual-networks-overview#communicate-with-on-premises-resources)  
**Tips**  
`Point-to-site - Established between a virtual network and a single computer in your network. Each computer that wants to establish connectivity with a virtual network must configure its connection. This connection type is great if you're just getting started with Azure, or for developers, because it requires little or no changes to your existing network.`

`Site-to-site VPN: Established between your on-premises VPN device and an Azure VPN Gateway that is deployed in a virtual network. This connection type enables any on-premises resource that you authorize to access a virtual network.`

## Azure PowerShell
All cmd-lets support interactiveness.

* get all resources by Resource Group Name: `Get-AzResource -ResourceGroupName $vm.ResourceGroupName | ft`
* Set Complex Variale: `$vm = (Get-AzVM -Name "testvm-eus-01" -ResourceGroupName learn-34fad52d-63cc-4ea1-a121-107041f719b7)`
* Create VM: `New-AzVm -ResourceGroupName learn-34fad52d-63cc-4ea1-a121-107041f719b7 -Name "testvm-eus-01" -Credential (Get-Credential) -Location "East US" -Image UbuntuLTS -OpenPorts 22`
* Get Resources by type: `Get-AzResource -ResourceType Microsoft.Compute/virtualMachines`

PowerShell script example: [Link](https://github.com/Glareone/AZ-204-Microsoft-Azure-Developer/blob/main/PowerShell%20script%20example%20(Create%20VM)/ConferenceDailyReset.ps1)

# Azure App Service
## Deploy Zip File
* Create WebApp (App Service):
  1)  + Create new Resource, WebApp in Search, Select name "web-app-learn1", Select .net stack, Review and Create
* Create .net project and publish it locally:
  1)  Create .net project
  2)  Publish .net project into folder "pub" and name it "site.zip" (`dotnet build` + `cd yourProjectToPublish` + `dotnet publish -o pub`)
  3)  zip all files from publishing folder with 7zip 
*  Publish into Azure:
  1)  open Visual Studio Terminal
  2)  cd pub
  3)  az login (potentially with --tenant "yourLearnDirectory.docs.microsoft.com")
  4)  az webapp deployment source config-zip --src site.zip --resource-group learn-cef0f929-db52-4acc-acc1-981a6ceb1852 --name web-app-learn1
  6)  check your application

## Deploy with Git system
* First option is to use Local Git.
  1) Create local Git under App Service > Deployment Center
  2) Create Local Git
  3) Copy Local Git URL and use it to create the repo locally on PC \ in Azure
  4) git init, git push...
  5) Create production branch. Push all changes to production branch to see these changes in App Serivce.

## Working with Slots (Production, Test slots within one App Service Plan (Standard, Premium and Isolated only, Free and Basic aren't supported))
Link: [MS-Learn Link](https://docs.microsoft.com/en-us/learn/modules/stage-deploy-app-service-deployment-slots/3-exercise-create-deployment-slots)  
Different connection links (and other dedicated configurations) for slots: [link](https://docs.microsoft.com/en-us/learn/modules/stage-deploy-app-service-deployment-slots/4-deploy-a-web-app-by-swapping-deployment-slots)  

## Application Service Environment. Isolation from 'noisy neighbours'
[Application Service Environment](https://docs.microsoft.com/en-us/azure/app-service/environment/intro)

# Azure Container Registry (let you save Docker images and use it for deployment in App Service)
## Create Registry and image
  1) Create Registry
  2) in your project `az acr build --registry <container_registry_name> --image <selected_image_name> .`

![image](https://user-images.githubusercontent.com/4239376/112735530-553d3980-8f55-11eb-8124-1bc83692f934.png)

## Upload docker image to Azure Container Registry
### Upload Sole Image (docker push)
  1) Create azure container registry
  2) Build your images locally using `docker build`
  3) add tag to your image `>docker tag <originalImageTag> <containerregistry.azurecr.io/newimagename>`
  4) login to azure container registry: `az login` and `az acr login --name myregistry`
  5) push your image `docker push <containerregistry.azurecr.io/newimagename>`
 
### Upload using docker-compose (docker-compose push)
  1) Create azure container registry
  2) Build your images locally using `docker-compose build`
  3) add tag to your image `>docker tag <originalImageTag> <containerregistry.azurecr.io/newimagename>`
  4) login to azure container registry: `az login` and `az acr login --name myregistry`
  5) update your docker-compose file with new image names `image: <containerregistry.azurecr.io/newimagename>`
  6) use `docker-compose push`

PS Additional information is under quick start tab in Azure Container Registry

## Use image in order to deploy new version in App Service
[Here is an instruction](https://docs.microsoft.com/en-us/learn/modules/deploy-run-container-app-service/5-exercise-deploy-web-app?pivots=csharp)

## Container Registry + App Service + Docker Images. Continuous Integration
[Here is an instruction](https://github.com/Glareone/AZ-204-Microsoft-Azure-Developer)

## Container Registry + Function
My example: [Prepared example in c#](https://github.com/Glareone/AZ-204-Microsoft-Azure-Developer/tree/main/Function%20with%20Docker%20and%20Azure%20Container%20Registry)

* Build Azure Function with Docker:
`docker build --tag alekseikolesnikov/azurefunctionsimage:v.1.0.0 .`
* Run Azure Function Docker:
`docker run -p 8080:80 -it alekseikolesnikov/azurefunctionsimage:v.1.0.0`
* To Publish Azure Function into Container Registry you must add Docker Support to your project (you cant do that initially)

## Enable Azure Active Directory (AD) Authentication for App Service
[Active Directory Authentication Example](https://docs.microsoft.com/en-us/archive/blogs/waws/azure-app-service-authentication-aad-groups)

## TLS Mutual (Certificate authentication, TLS mutual authentication)
[Configure tls mutual auth](https://docs.microsoft.com/en-us/azure/app-service/app-service-web-configure-tls-mutual-auth)

# Azure Key Vault Example
[Mapage Passwords in Key Vault Example](https://docs.microsoft.com/en-us/learn/modules/protect-against-security-threats-azure/5-manage-password-key-vault)
[Key Vault Management and Configuration](https://docs.microsoft.com/en-us/learn/modules/configure-and-manage-azure-key-vault/3-manage-access-and-permissions-to-secrets)
[Certificates KeyVault + WebApp](https://docs.microsoft.com/en-us/learn/modules/configure-and-manage-azure-key-vault/5-manage-certificates)

**Tips**
* For example, if you want to grant an application the rights to use keys in a key vault, you only need to grant data plane access permissions using key vault access policies. No management plane access is needed for this application. Conversely, if you want a user to be able to read vault properties and tags but not have any access to keys, secrets, or certificates, by using RBAC, you can grant read access to the management plane. No access to the data plane is required.

* Get secret from Key vault using Azure CLI `az keyvault secret show --vault-name <key-vault-name> --name <secret name>

# Threat Protection (Cross Site Scripting (CSS), CORS). Data Validation. Secure your Data.

## Validation techniques
[Input_Validation_Cheat_Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Input_Validation_Cheat_Sheet.html)
## Cross-Site Scripting (CSS)
[Cross_Site_Scripting_Prevention](https://cheatsheetseries.owasp.org/cheatsheets/Cross_Site_Scripting_Prevention_Cheat_Sheet.html)
## List of apps which help you to find vulnerabilities in your project. Safe Dependencies.
[tools to verify if our dependencies are vulnerable](https://docs.microsoft.com/en-us/learn/modules/top-5-security-items-to-consider/6-safe-dependencies)
## Data discovery
* Data discovery and classification (currently in preview) provides advanced capabilities built into Azure SQL Database for discovering, classifying, labeling and protecting sensitive data (such as business, personal data (PII), and financial information) in your databases. [Link with information](https://docs.microsoft.com/en-us/learn/modules/configure-security-policies-to-manage-data/2-configure-data-classification)

# API Management
## API Management Access Restriction Policies
[Access Restriction Policies](https://docs.microsoft.com/en-us/azure/api-management/api-management-access-restriction-policies)

# Azure Active Directory
## Active Directory App Manifect
[Reference app manifest](https://docs.microsoft.com/en-us/azure/active-directory/develop/reference-app-manifest)

# Azure RBAC
[RBAC Overview](https://docs.microsoft.com/en-us/learn/modules/secure-azure-resources-with-rbac/2-rbac-overview)
## Not Action
* Azure RBAC has something called `NotActions` permissions. Use NotActions to create a set of not allowed permissions. The access granted by a role, the effective permissions, is computed by subtracting the NotActions operations from the Actions operations. For example, the Contributor role has both Actions and NotActions. The wildcard (*) in Actions indicates that it can perform all operations on the control plane. Then you subtract the following operations in NotActions to compute the effective permissions

# Managed Identities: Access from one service to Another. IAM
**You can allow your VM to access to resource group using this identity**  
**Two types: System-Assigned and User-Assigned. System-assigned will be deleted automatically from Azure AD is resource is deleted(VM in my example). User-Assigned will not (but may be granted to several resources)**  
* Good example of how to apply user-defined Managed Identity to Azure Function and Azure Storage Account: [good explanation and example](https://learn.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/overview)  
* [Managed Identities - overview](https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/overview)  
* [Access from VM to selected resource group](https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/tutorial-windows-vm-access-arm) - also IAM example is here  
* [Invoke-Restmethod: let you delegate such permissions](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/invoke-restmethod?view=powershell-7.1)  
* [Invoke-WebRequest vs Invoke-Restmethod (both of them will allow you to delegate permissions to VM)](https://www.cloudsma.com/2018/05/invoke-restmethod-vs-invoke-webrequest/)  

# Azure Traffic Manager
![image](https://user-images.githubusercontent.com/4239376/120998334-ad33c000-c790-11eb-8769-7572c3260151.png)
![image](https://user-images.githubusercontent.com/4239376/120999216-888c1800-c791-11eb-9f98-43e14373e7e6.png)

