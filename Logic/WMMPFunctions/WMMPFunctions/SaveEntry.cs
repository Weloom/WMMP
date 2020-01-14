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
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;

namespace WMMPFunctions
{
    public static class SaveEntry
    {
        [FunctionName("SaveEntry")]
        public async static Task<string> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            try
            {
                string requestBody = StringUtil.GetBody(req.Body);
                JObject o = JObject.Parse(requestBody);
                string accountId = o.SelectToken("$.AccountId").Value<string>();

                var settings = new Settings();
                CloudTable table = await StorageAccess.GetTable("Entry", log);
                CloudBlobContainer blob = await StorageAccess.GetBlobContainer($"entry-{accountId?.ToLower()}", log);
                await new Core.SaveEntry().Run(o, table, blob, settings, log, DateTime.Now);
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
    public class SaveEntry
    {
        public async Task Run(JObject changes, CloudTable table, CloudBlobContainer blob, Settings settings, ILogger log, DateTime now)
        {
            ValidateJson(changes, settings, "SaveEntry");
            log.LogInformation($"BLOB step 1-1");
            string accountId = changes.SelectToken("$.AccountId").Value<string>();
            log.LogInformation($"BLOB step 1-2");
            string OperationTitle = changes.SelectToken("$.OperationTitle")?.Value<string>();
            log.LogInformation($"BLOB step 1-3");
            string workId = changes.SelectToken("$.WorkId").Value<string>();
            log.LogInformation($"BLOB step 1-4");
            string action = changes.SelectToken("$.Action").Value<string>().ToLower();
            log.LogInformation($"BLOB step 1-5");

            string myLock = GetAccessLockString(now);
            log.LogInformation($"BLOB step 1-6");
            await AddAccessLock(table, accountId, workId, myLock);

            try
            {
                log.LogInformation($"BLOB step 1-7");
                string data = await ReadMetadata(table, accountId, workId);
                log.LogInformation($"BLOB step 1-8");
                JArray existings = string.IsNullOrEmpty(data) ? new JArray() : JArray.Parse(data);
                log.LogInformation($"BLOB step 1-9");

                int rowId = 0;
                foreach (JToken change in changes.SelectTokens("$.Entries").Values())
                {
                    rowId++;
                    var changeObj = (JObject)JsonConvert.DeserializeObject(change.ToString());
                    log.LogInformation($"BLOB step 1-10");
                    EnsureId(changeObj, OperationTitle, rowId);
                    log.LogInformation($"BLOB step 1-11");
                    string changeId = changeObj.SelectToken("$.Id").Value<string>();
                    log.LogInformation($"BLOB step 1-12");

                    await WriteEntryToBlob(blob, workId, changeId, changeObj.Property("Entry").Value.ToString(), log);
                    log.LogInformation($"BLOB step 1-13");
                    EnsureExtract(changeObj);
                    changeObj.Property("Entry").Remove();

                    if (action == "add")
                    {
                        //append
                        existings.Add(changeObj);
                    }
                    else
                    {
                        var existing = existings.SelectToken($"$[?(@.Id == '{changeId}')]");
                        if (existing != null)
                        {
                            //update
                            existing.Replace(changeObj);
                        }
                        else
                        {
                            //append
                            existings.Add(changeObj);
                        }
                    }
                }
                log.LogInformation($"BLOB step x-1");
                await WriteMetaDataToTable(table, accountId, workId, existings.ToString(), log);
                log.LogInformation($"BLOB step x-2");
            }
            finally
            {
                log.LogInformation($"BLOB step x-3");
                await RemoveAccessLock(table, accountId, workId, myLock);
                log.LogInformation($"BLOB step x-4");
            }
        }

        public async Task AddAccessLock(CloudTable table, string partitionKey, string rowKey, string lockString)
        {
            TableOperation read = TableOperation.Retrieve(partitionKey, rowKey, new List<string>() { "Lock" });
            var result = await table.ExecuteAsync(read);
            if (result != null)
            {
                DynamicTableEntity row = (DynamicTableEntity)result.Result;
                string lockValue = row?.Properties["Lock"]?.StringValue;
                if (!string.IsNullOrEmpty(lockValue))
                {
                    if (lockValue == lockString)
                    {
                        //locked by this process
                        return;
                    }
                    else
                    {
                        //locked by other process
                        throw new Exception($"Lock with '{lockString}' failed for table '{table.Name}' with partion/row '{partitionKey}/{rowKey}', because it is already locked with '{lockValue}'.");
                    }
                }
                else
                {
                    //not locked. lock it (ANd maybe create it too)
                    var record = new TableEntity.TextEntry() { PartitionKey = partitionKey, RowKey = rowKey, Lock = lockString };
                    TableOperation insert = TableOperation.InsertOrMerge(record);
                    await table.ExecuteAsync(insert);
                }
            }
        }

        public async Task<string> ReadMetadata(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation read = TableOperation.Retrieve(partitionKey, rowKey, new List<string>() { "Data" });
            var result = await table.ExecuteAsync(read);
            if (result != null)
            {
                DynamicTableEntity row = (DynamicTableEntity)result.Result;
                return row.Properties["Data"]?.StringValue;
            }
            return null;
        }

        //        public async Task<IDictionary<string, string>> OrderJsonTokeChildren(CloudTable table, string partitionKey, string rowKey)
        //        {
        //            TableOperation read = TableOperation.Retrieve(partitionKey, rowKey, new List<string>() { "Data" });
        //            var result = await table.ExecuteAsync(read);
        //            var list = new Dictionary<string, string>();
        //            if (result != null)
        //            {
        //                DynamicTableEntity row = (DynamicTableEntity)result.Result;
        //                var entries = JToken.Parse(row.Properties["Data"]?.StringValue);
        //                foreach (var entry in entries.Children())
        //                {
        //                    list.Add(entry.SelectToken("$.Id").Value<string>(), entry.SelectToken("$.WorkId").Value<string>();
        //)
        //                    string workId = o.SelectToken("$.WorkId").Value<string>();
        //                }
        //            }
        //            return list;
        //        }


        public async Task RemoveAccessLock(CloudTable table, string partitionKey, string rowKey, string lockString)
        {
            TableOperation read = TableOperation.Retrieve(partitionKey, rowKey, new List<string>() { "Lock" });
            var result = await table.ExecuteAsync(read);
            if (result != null)
            {
                DynamicTableEntity row = (DynamicTableEntity)result.Result;
                string lockValue = row.Properties["Lock"]?.StringValue;
                if (!string.IsNullOrEmpty(lockValue))
                {
                    if (lockValue == lockString)
                    {
                        //locked by this process. clear it
                        var record = new TableEntity.TextEntry() { PartitionKey = partitionKey, RowKey = rowKey, Lock = "" , ETag="*"};
                        TableOperation insert = TableOperation.Merge(record);
                        await table.ExecuteAsync(insert);
                    }
                    else
                    {
                        //locked by other process
                        throw new Exception($"Unlock with '{lockString}' failed for table '{table.Name}' with partion/row '{partitionKey}/{rowKey}', because it has been locekd by another process with '{lockValue}'.");
                    }
                }
                else
                {
                    //not locked. lock it
                    throw new Exception($"Unlock with '{lockString}' failed for table '{table.Name}' with partion/row '{partitionKey}/{rowKey}', because it wan't locked.");
                }
            }
        }


        public async Task WriteMetaDataToTable(CloudTable table, string partitionKey, string rowKey, string entries, ILogger log)
        {
            var record = new TableEntity.TextEntry() { PartitionKey = partitionKey, RowKey = rowKey, Data = entries };
            TableOperation insert = TableOperation.InsertOrMerge(record);
            await table.ExecuteAsync(insert);
        }

        public async Task WriteEntryToBlob(CloudBlobContainer container, string folder, string id, string entry, ILogger log)
        {
            CloudBlockBlob blob = container.GetBlockBlobReference($"{"1"}/{id}.txt");
            await blob.UploadTextAsync(entry);
        }

        public void EnsureExtract(JObject obj)
        {
            if (!obj.ContainsKey("Extract"))
            {
                string extractValue = obj.Property("Entry").Value.ToString().Substring(0, 10) + "...";
                obj.Property("Entry").AddBeforeSelf(new JProperty("Extract", extractValue));
            }
        }

        public void EnsureId(JObject obj, string operationTitle, int rowNo)
        {
            if (!obj.ContainsKey("Id") || obj.ContainsKey("Id").ToString() == "")
            {
                string id = $"{operationTitle}{rowNo}";
                if (obj.ContainsKey("Id"))
                {
                    obj.Property("Id").Value = id;
                }
                else {
                    obj.Add("Id", id);
                }
            }
        }

        public void ValidateJson(JObject o, Settings settings, string name)
        {
            var isValidateInputSchema = settings.BoolValue("validateInputSchema", true, true);
            if (isValidateInputSchema)
            {
                string schemaString = settings.Value($"{name}-InputSchema", false);
                if (schemaString != null)
                {
                    JSchema schema = JSchema.Parse(schemaString);
                    if (!o.IsValid(schema))
                    {
                        throw new Exception($"Passed data did not match schema {name}");
                    }
                }
            }
        }

        public string GetAccessLockString(DateTime now)
        {
            return $"SaveEntry-{now.ToString("yyyy-MM-dd-HH-mm-ss-ffff")}";
        }
    }


}

/*
 * Data example:
 {
  "OperationTitle": "DiigoBlaBlaBla",
  "AccountId": "1",
  "WorkId": "1",
  "Action": "Update", //or "Add"
  "Entries": [
    {
      "Id": 1,
      "Subject": "...",
      "OrderNo": 1,
      "Extract": "Ellen er sent på den. Hun haster igennem den lyse atriumgård på Ny Odenses IT universitet.",
      "Link": "...",
      "Entry": "..."
    }
  ]
}

 
     
    {
  "$schema": "http://json-schema.org/draft-04/schema#",
  "type": "object",
  "properties": {
    "OperationTitle": {
      "type": "string"
    },
    "AccountId": {
      "type": "string"
    },
    "WorkId": {
      "type": "string"
    },
    "Action": {
      "type": "string",
      "enum": ["add","update"]
    },
    "Entries": {
      "type": "array",
      "items": [
        {
          "type": "object",
          "properties": {
            "Subject": {
              "type": "string"
            },
            "OrderNo": {
              "type": "integer"
            },
            "Extract": {
              "type": "string"
            },
            "Link": {
              "type": "string"
            },
            "Entry": {
              "type": "string"
            }
          },
          "required": [
            "Subject",
            "Entry"
          ]
        }
      ]
    }
  },
  "required": [
    "AccountId",
    "WorkId",
    "Action",
    "Entries"
  ]
} 

     */
