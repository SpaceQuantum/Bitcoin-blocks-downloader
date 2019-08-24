using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessingContracts;

namespace BitcoinProcessing
{
    public class TransactionDetailsProcesing
    {
        private readonly IRepository _repository;
        private readonly HttpHelper httpHelper = new HttpHelper();

        public TransactionDetailsProcesing(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> GetTransactionsDetails(ConcurrentDictionary<string, List<string>> blockTransactionsDictionary, Action<string> resultCallbackAction)
        {
            int i = 0;
            foreach (var blockDetails in blockTransactionsDictionary)
            {
                List<string> urls = new List<string>();

                var txIdsFromDb = await _repository.GetTransactionsIds(blockDetails.Key);

                foreach (var txId in blockDetails.Value)
                {
                    if (!txIdsFromDb.Contains(txId))
                    {
                        string url = "https://blockexplorer.com/api/tx/" + txId;
                        urls.Add(url);
                        i++;
                    }
                }

                IEnumerable<Task<string>> downloadTransactionDetailsTasks = from blocDetailsUrl in urls
                    select httpHelper.ProcessURL(blocDetailsUrl, resultCallbackAction);

                Task<string>[] downloadTasks = downloadTransactionDetailsTasks.ToArray();

                // Await the completion of all the running tasks.  
                string[] lengths = await Task.WhenAll(downloadTasks);

                await _repository.SetBlockToFinishProcessing(blockDetails.Key);
            }

            return i;
        }
    }
}
