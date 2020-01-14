using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using WMMPFunctions.Entity;
using WMMPFunctions.TableEntity;

namespace WMMPFunctions
{
    public static class TextJobStarter
    {
        [FunctionName("TextJobStarter")]
        public static async Task Run(
            [ServiceBusTrigger("textjobs", Connection = "ServiceBusConnection")]string myQueueItem,
            [Table("Text", Connection = "StorageAccountConnection")] CloudTable textTable,
            ILogger log)
        {
            await Core.TextJobStarter.Run(myQueueItem, textTable, log);
        }
    }
}

namespace WMMPFunctions.Core
{
    public static class TextJobStarter
    {
        public static async Task Run(
            string myQueueItem,
             CloudTable textTable,
            ILogger log)
        {
            TableOperation opr = TableOperation.Retrieve("Global", "as1");
            var result = await textTable.ExecuteAsync(opr);


            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            //TODO: Should use Newton serializer. It's faster
            byte[] existingData = System.Text.Encoding.UTF8.GetBytes(myQueueItem);
            var str = new MemoryStream();
            str.Write(existingData, 0, existingData.Length);
            var ser = new DataContractJsonSerializer(typeof(TextJob));
            str.Position = 0;
            var job = ser.ReadObject(str) as TextJob;

            ////TODO: use id to find the right text (And only ONE entry)
            //TableQuery<TextEntry> query = new TableQuery<TextEntry>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Global"));
            //TableContinuationToken token = null;
            //do
            //{
            //    TableQuerySegment<TextEntry> resultSegment = await textTable.ExecuteQuerySegmentedAsync(query, token);
            //    token = resultSegment.ContinuationToken;

            //    foreach (TextEntry entity in resultSegment.Results)
            //    {
            //        Console.WriteLine("{0}, {1}\t{2}", entity.PartitionKey, entity.RowKey);
            //    }
            //} while (token != null);
        }
    }
}
