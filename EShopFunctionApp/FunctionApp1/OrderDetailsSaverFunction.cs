using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EshopFunctionApp
{
    public static class OrderDetailsSaverFunction
    {
        [FunctionName("OrderDetailsSaverFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "eshop-database",
                collectionName: "order-details",
                ConnectionStringSetting = "CosmosDbConnectionString")]IAsyncCollector<dynamic> documentsOut,
            ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic bodyData = JsonConvert.DeserializeObject(requestBody);

            if (bodyData == null)
            {
                log.LogInformation("Request body is empty");
                throw new DataException("Request body is empty");
            }

            var preparedData = new
            {
                orderId = bodyData.orderId,
                description = bodyData.description,
                items = bodyData.items,
                price = bodyData.price,
                shippingAddress = bodyData.shippingAddress,
            };

            // Add a JSON document to the output container.
            await documentsOut.AddAsync(preparedData);

            return new OkObjectResult("CosmosDB record created");
        }
    }
}
