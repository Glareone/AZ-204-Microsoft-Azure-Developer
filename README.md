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

1. First you need to create Namespace;
  a. Select and push to bash your selected name: `NS_NAME=event-hub-namespace`
  b. Create Event Hub Namespace: `az eventhubs namespace create --name event-hub-namespace`
  c. Get JSON Configuration (with connections) to your created namespace: `az eventhubs namespace authorization-rule keys list     --name RootManageSharedAccessKey     --namespace-name $NS_NAME`
2. Create Event Hub:
  a. New Hub Name: `HUB_NAME=hubname-$RANDOM`
  b. Create Event Hub: `az eventhubs eventhub create --name $HUB_NAME --namespace-name $NS_NAME`
  c. Check that Event Hub created properly: `az eventhubs eventhub show --namespace-name $NS_NAME --name $HUB_NAME`
