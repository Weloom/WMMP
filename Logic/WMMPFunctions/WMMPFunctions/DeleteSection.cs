using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WMMPFunctions
{
    /// <summary>
    /// updates an account in table storage. Creates it if the account doesn't already exists.
    /// </summary>
    public static class DeleteSection
    {
        [FunctionName("DeleteSection")]
        public static TableEntity.Account Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            //var connectionString = process.env.StorageConnectionString;
            //var tableService = azure.createTableService(connectionString);


            var sr = new StreamReader(req.Body);
            string requestBody = sr.ReadToEnd();
            sr.Dispose();
            return Core.DeleteSection.Run(requestBody, log);
        }
    }
}

namespace WMMPFunctions.Core
{
    public static class DeleteSection
    {
        public static TableEntity.Account Run(string data, ILogger log)
        {
            JObject o = JObject.Parse(data);
            JToken id = o.SelectToken("$.Id");

            return new TableEntity.Account() { PartitionKey = "1", RowKey = id.Value<string>(), Data = data };
        }
    }
}
