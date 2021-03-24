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