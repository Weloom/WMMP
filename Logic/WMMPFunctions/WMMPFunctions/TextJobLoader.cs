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

namespace WMMPFunctions
{
    public static class TextJobLoader
    {
        [FunctionName("TextJobLoader")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        ILogger log,
        [ServiceBus("textjobs", Connection = "ServiceBusConnection", EntityType = EntityType.Queue)] ICollector<string> outputSbQueue)
        {
            return await TextJobLoaderRun.Run(log, outputSbQueue);
        }
    }

    public static class TextJobLoaderRun
    {
        public static async Task<IActionResult> Run(ILogger log, ICollector<string> outputSbQueue)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];
            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);

            int count = 0; //TODO: read form setting
            if (count == 0)
            {
                count = 1;
            }

            for (int i = 1; i <= count; i++)
            {
                var job = new TextJob
                {
                    TextId = i.ToString(),
                    OwnerId = i,
                    TextfileId = i,
                    TextJobType = TextJobType.WordCount
                };


                //TODO: Should use Newton serializer. It's faster
                var ser = new DataContractJsonSerializer(typeof(TextJob));
                var str = new System.IO.MemoryStream();
                ser.WriteObject(str, job);
                str.Position = 0;
                var msg = new StreamReader(str).ReadToEnd();
                outputSbQueue.Add(msg);
            }

            return (ActionResult)new OkObjectResult($"Working on it");
        }
    }
}
