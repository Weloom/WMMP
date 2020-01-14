using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using WMMPFunctions;

namespace WWWMPFunctions.test
{
    [TestClass]
    public class SaveEntryTests
    {
        /* Json eval
         *   missing entry
         *   missing extract
         *   missing id w/wo OperationTite
         *   invalid json
         * Enum
         *   single entry
         *   multiple entries
         * Actions
         *   action=add
         *   action=update
         *   action=other/invalid   
         * Locking
         *   locked by other
         *   locked by this
         *   not locked
         * existing data
         *  table already exits
         *  table not
         *  blob exists
         *  blob not
         */


        [TestMethod]
        public void Run_SaveEntryTests_Success()
        {
            SetupEnvironment();

            string requestBody = @"{
  ""AccountId"": ""Test1"",
  ""WorkId"": ""Test1"",
  ""Action"": ""update"",
  ""Entries"": [
    {
      ""Id"": ""1"",
      ""Subject"": ""..."",
      ""Link"": ""..."",
      ""Entry"": ""Ellen er sent på den. Hun haster igennem den lyse atriumgård på Ny Odenses IT universitet.""
    },
    {
      ""Id"": ""2"",
      ""Subject"": ""..."",
      ""Link"": ""..."",
      ""Entry"": ""dfks dfældlæj lkgjfl gkjfkfjgzkjd læ""
    }
  ]
}
";
            JObject o = JObject.Parse(requestBody);
            string accountId = o.SelectToken("$.AccountId").Value<string>();
            var now = DateTime.Now;

            var settings = new Settings();
            var log = new ListLogger();
            CloudTable table = StorageAccess.GetTable("Entry", log).GetAwaiter().GetResult();
            CloudBlobContainer blob = StorageAccess.GetBlobContainer($"entry-{accountId.ToLower()}", log).GetAwaiter().GetResult();
            new WMMPFunctions.Core.SaveEntry().Run(o, table, blob, settings, log, now).GetAwaiter().GetResult();

            //Assert.AreEqual("myId", result.RowKey);
            //Assert.AreEqual("1", result.PartitionKey);
            //Assert.AreEqual(data, result.Data);
        }

        [TestMethod]
        public void Run_SaveEntryTests_NoId_Success()
        {
            SetupEnvironment();

            string requestBody = @"{
  ""OperationTitle"": ""Test1-"",
  ""AccountId"": ""Test1"",
  ""WorkId"": ""Test1"",
  ""Action"": ""update"",
  ""Entries"": [
    {
      ""Subject"": ""..."",
      ""Link"": ""..."",
      ""Entry"": ""Ellen er sent på den. Hun haster igennem den lyse atriumgård på Ny Odenses IT universitet.""
    },
    {
      ""Subject"": ""..."",
      ""Link"": ""..."",
      ""Entry"": ""dfks dfældlæj lkgjfl gkjfkfjgzkjd læ""
    }
  ]
}
";
            JObject o = JObject.Parse(requestBody);
            string accountId = o.SelectToken("$.AccountId").Value<string>();
            var now = DateTime.Now;

            var settings = new Settings();
            var log = new ListLogger();
            CloudTable table = StorageAccess.GetTable("Entry", log).GetAwaiter().GetResult();
            CloudBlobContainer blob = StorageAccess.GetBlobContainer($"entry-{accountId.ToLower()}", log).GetAwaiter().GetResult();
            new WMMPFunctions.Core.SaveEntry().Run(o, table, blob, settings, log, now).GetAwaiter().GetResult();

            //Assert.AreEqual("myId", result.RowKey);
            //Assert.AreEqual("1", result.PartitionKey);
            //Assert.AreEqual(data, result.Data);
        }


        [TestMethod]
        public void Run_SaveEntryTests_NoId_NoOperationTitle_Success()
        {
            SetupEnvironment();

            string requestBody = @"{
  ""AccountId"": ""Test1"",
  ""WorkId"": ""Test1"",
  ""Action"": ""update"",
  ""Entries"": [
    {
      ""Subject"": ""..."",
      ""Link"": ""..."",
      ""Entry"": ""Ellen er sent på den. Hun haster igennem den lyse atriumgård på Ny Odenses IT universitet.""
    },
    {
      ""Subject"": ""..."",
      ""Link"": ""..."",
      ""Entry"": ""dfks dfældlæj lkgjfl gkjfkfjgzkjd læ""
    }
  ]
}
";
            JObject o = JObject.Parse(requestBody);
            string accountId = o.SelectToken("$.AccountId").Value<string>();
            var now = DateTime.Now;

            var settings = new Settings();
            var log = new ListLogger();
            CloudTable table = StorageAccess.GetTable("Entry", log).GetAwaiter().GetResult();
            CloudBlobContainer blob = StorageAccess.GetBlobContainer($"entry-{accountId.ToLower()}", log).GetAwaiter().GetResult();
            new WMMPFunctions.Core.SaveEntry().Run(o, table, blob, settings, log, now).GetAwaiter().GetResult();

            //Assert.AreEqual("myId", result.RowKey);
            //Assert.AreEqual("1", result.PartitionKey);
            //Assert.AreEqual(data, result.Data);
        }




        public static void SetupEnvironment()
        {
            string basePath = Path.GetFullPath(@".");
            var settings = JsonConvert.DeserializeObject<LocalSettings>(
                File.ReadAllText(basePath + "\\local.settings.json"));

            foreach (var setting in settings.Values)
            {
                Environment.SetEnvironmentVariable(setting.Key, setting.Value);
            }
        }
    }

    class LocalSettings
    {
        public bool IsEncrypted { get; set; }
        public Dictionary<string, string> Values { get; set; }
    }
}
