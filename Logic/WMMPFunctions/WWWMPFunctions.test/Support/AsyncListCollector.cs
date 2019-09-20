using Microsoft.Azure.WebJobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WMMPFunctions;

namespace WWWMPFunctions.test
{
    public class AsyncListCollector<T> : IAsyncCollector<T>
    {
        public readonly List<T> Items = new List<T>();

        public Task AddAsync(T item, CancellationToken cancellationToken = default(CancellationToken))
        {

            Items.Add(item);

            return Task.FromResult(true);
        }

        public Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(true);
        }
    }
}
