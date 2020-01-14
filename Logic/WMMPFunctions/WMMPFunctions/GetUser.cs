using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.ServiceBus;
using System.Runtime.Serialization.Json;
using Microsoft.WindowsAzure.Storage.Table;
using WMMPFunctions.Entity;

namespace WMMPFunctions
{
    public static class GetUser
    {
        [FunctionName("GetUser")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [Table("Account", Connection = "StorageAccountConnection")] CloudTable cloudTable,
        ILogger log)
        {
            return await Core.GetUser.Run(cloudTable, log);
        }
    }
}

namespace WMMPFunctions.Core
{
    public static class GetUser
    {
        public static async Task<IActionResult> Run(CloudTable cloudTable, ILogger log)
        {
            ////TODO: Should use Newton serializer. It's faster
            //var ser = new DataContractJsonSerializer(typeof(TextJob));
            //var str = new System.IO.MemoryStream();
            //ser.WriteObject(str, job);
            //str.Position = 0;
            //var msg = new StreamReader(str).ReadToEnd();
            //outputSbQueue.Add(msg);

            return (ActionResult)new OkObjectResult($"Working on it");
        }
    }
}
