using Microsoft.Azure.WebJobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using WMMPFunctions;

namespace WWWMPFunctions.test
{
    [TestClass]
    public class TextJobStarterTests
    {
        [TestMethod]
        public void Run_SpecifiesJobType1_Success()
        {
            var job = new TextJob
            {
                TextId = "test",
                TextJobType = TextJobType.WordCount
            };

            //TODO: Should use Newton serializer. It's faster
            var ser = new DataContractJsonSerializer(typeof(TextJob));
            var str = new System.IO.MemoryStream();
            ser.WriteObject(str, job);
            str.Position = 0;
            var msg = new StreamReader(str).ReadToEnd();

            //TODO: Mock CloudTable
            //TextJobStarter.Run(msg, new ListLogger());

            Assert.Inconclusive();
        }
    }
}
