using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(WMMPFunctions.Startup))]

namespace WMMPFunctions
{
    /// <summary>
    /// updates an account in table storage. Creates it if the account doesn't already exists.
    /// </summary>
    public static class SaveAccount
    {
        [FunctionName("SaveAccount")]
        public async static Task<string> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log)
        {
            try
            {
                string requestBody = StringUtil.GetBody(req.Body);
                CloudTable cloudTable = await StorageAccess.GetTable("Account", log);
                await Core.SaveAccount.Run(requestBody, cloudTable, log);
                return "ok";
            }
            catch (Exception ex)
            {
                log.LogError($"ERROR: {ex.Message}");
                return ex.Message;
            }
        }
    }
}

namespace WMMPFunctions.Core
{
    public static class SaveAccount
    {
        public async static Task Run(string data, CloudTable table, ILogger log)
        {
            JObject o = JObject.Parse(data);
            JToken id = o.SelectToken("$.Id");
            var record = new TableEntity.Account() { PartitionKey = "1", RowKey = id.Value<string>(), Data = data };
            TableOperation insert = TableOperation.InsertOrMerge(record);
            await table.ExecuteAsync(insert);
        }
    }
}

