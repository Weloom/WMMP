using Microsoft.Azure.WebJobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WMMPFunctions;

namespace WWWMPFunctions.test
{
    public class ListCollector<T> : ICollector<T>
    {
        public readonly List<T> Items = new List<T>();

        public void Add(T item)
        {
            Items.Add(item);
        }
    }
}
