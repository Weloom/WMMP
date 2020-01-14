using Microsoft.Azure.WebJobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using WMMPFunctions;
using WMMPFunctions.Entity;

namespace WWWMPFunctions.test
{
    [TestClass]
    public class SaveAccountTests
    {
        [TestMethod]
        public void Run_SaveAccountTests_Success()
        {
            var dataObject = new Account()
            {
                Id = "myId",
                AccountLevel = AccountLevel.Test
            };

            string data = JsonConvert.SerializeObject(dataObject);
            //{"Id":"myId","AccountLevel":99,"AccountState":0,"User":null}

            var log = new ListLogger();
            CloudTable table = StorageAccess.GetTable("Account", log).GetAwaiter().GetResult();

            WMMPFunctions.Core.SaveAccount.Run(data, table, new ListLogger());

            //Assert.AreEqual("myId", result.RowKey);
            //Assert.AreEqual("1", result.PartitionKey);
            //Assert.AreEqual(data, result.Data);
        }
    }
}

//var ser = new DataContractJsonSerializer(typeof(TextJob));
//var str = new System.IO.MemoryStream();
//ser.WriteObject(str, job);
//str.Position = 0;
//var msg = new StreamReader(str).ReadToEnd();
