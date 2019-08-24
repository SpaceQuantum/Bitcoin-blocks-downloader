using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessingContracts;

namespace BitcoinProcessing
{
    public class BlockDetailsProcessing
    {
        private readonly ILogger _logger;
        private readonly IRepository _repository;
        private readonly HttpHelper httpHelper = new HttpHelper();
        public BlockDetailsProcessing(ILogger logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<int> GetBlocksDetails(ConcurrentBag<string> blocksHashList, Action<string> resultCallbackAction)
        {
            List<string> urls = new List<string>();
            foreach (var blockHash in blocksHashList)
            {
                string url = "https://blockexplorer.com/api/block/" + blockHash;
                urls.Add(url);
            }
            
            IEnumerable<Task<string>> downloadBlockDetailsTasks = from blocDetailsUrl in urls select httpHelper.ProcessURL(blocDetailsUrl, resultCallbackAction);

            Task<string>[] downloadTasks = downloadBlockDetailsTasks.ToArray();

            // Await the completion of all the running tasks.  
            string[] lengths = await Task.WhenAll(downloadTasks);
            return lengths.Length;
        }
    }
}
