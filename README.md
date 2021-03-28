# AZ-204-Microsoft-Azure-Developer
This repo consists of materials gathering from different sources which help you to successfully pass  official Microsoft Azure Developer exam.

## Durable Functions. Orchestrator.
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

# Azure Container Registry (let you save Docker images and use it for deployment in App Service)
## Create Registry and image
  1) Create Registry
  2) in your project `az acr build --registry <container_registry_name> --image <selected_image_name> .`

![image](https://user-images.githubusercontent.com/4239376/112735530-553d3980-8f55-11eb-8124-1bc83692f934.png)

## Use image in order to deploy new version in App Service
[Here is an instruction](https://docs.microsoft.com/en-us/learn/modules/deploy-run-container-app-service/5-exercise-deploy-web-app?pivots=csharp)

## Container Registry + App Service + Docker Images. Continuous Integration
[Here is an instruction](https://github.com/Glareone/AZ-204-Microsoft-Azure-Developer)

# Azure Key Vault Example
[Link](https://docs.microsoft.com/en-us/learn/modules/protect-against-security-threats-azure/5-manage-password-key-vault)

# Validation
## Validation techniques
[Input_Validation_Cheat_Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Input_Validation_Cheat_Sheet.html)
