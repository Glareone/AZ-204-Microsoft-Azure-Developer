using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EshopFunctionApp
{
    public static class OrderItemsReserver
    {
        [FunctionName("OrderItemsReserver")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Blob("eshopcontainer/{rand-guid}.txt", FileAccess.Write, Connection = "AzureWebJobsStorage")] Stream myBlob,
            ILogger log)
        {

            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!myBlob.CanWrite)
            {
                log.LogInformation("No Write Permissions");
                throw new UnauthorizedAccessException("No Write Permissions");
            }

            var contentType = req.ContentType;

            if (contentType != "application/json")
            {
                log.LogInformation("input content type is not application/json type");
                throw new Exception("input content type is not application/json type");
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            if (data != null)
            {
                var information = $"OrderId: {data.orderId}\nItemId: {data.itemId}\nQuantity: {data.quantity}\n";
                log.LogInformation($"{information}");

                try
                {
                    await myBlob.WriteAsync(Encoding.UTF8.GetBytes(information));
                }
                catch (Exception e)
                {
                    log.LogInformation($"Write operation failed, {e}");
                    throw e;
                }
            }

            return new OkObjectResult("File created");
        }
    }
}

