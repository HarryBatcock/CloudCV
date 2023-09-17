using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Client;

namespace Company.Function
{
    public static class GetCVCounter
    {
        [FunctionName("GetCVCounter")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(databaseName: "AzureCVDB", collectionName: "Visit Counter", ConnectionStringSetting = "AzureCVConnectionString", Id = "1", PartitionKey = "1")] VisitCounter inputCounter,
            [CosmosDB(databaseName: "AzureCVDB", collectionName: "Visit Counter", ConnectionStringSetting = "AzureCVConnectionString", Id = "1", PartitionKey = "1")] DocumentClient client,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Check if inputCounter is null
            if (inputCounter == null)
            {
                // Create a new VisitCounter object with count set to 0
                inputCounter = new VisitCounter { id = "1", count = 0 };
            }

            // Increment the visit counter
            inputCounter.count++;

            // Update the visit counter in Cosmos DB
            Uri collectionUri = UriFactory.CreateDocumentCollectionUri("AzureCVDB", "Visit Counter");
            await client.UpsertDocumentAsync(collectionUri, inputCounter);

            // Return the visit counter value
            var jsonToReturn = JsonConvert.SerializeObject(inputCounter);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }

    public class VisitCounter
    {
        public string id { get; set; }
        public int count { get; set; }
    }
}
