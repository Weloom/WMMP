using Microsoft.Azure.WebJobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WMMPFunctions;

namespace WWWMPFunctions.test
{
    [TestClass]
    public class TextJobLoaderTests
    {
        [TestMethod]
        public void Run_SpecifiesUserAndGroup_Success()
        {
            var outputSbQueue = new ListCollector<string>();
            TextJobLoaderRun.Run(new ListLogger(), outputSbQueue).GetAwaiter().GetResult();

            Assert.AreEqual(1000, outputSbQueue.Items.Count);
        }
    }
}
