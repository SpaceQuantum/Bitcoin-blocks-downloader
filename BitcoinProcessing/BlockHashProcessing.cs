using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProcessingContracts;

namespace BitcoinProcessing
{
    public class BlockHashProcessing
    {
        private readonly ILogger _logger;
        private readonly IRepository _repository;
        private readonly HttpHelper httpHelper = new HttpHelper();

        public BlockHashProcessing(ILogger logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<int> GetBlockHashes(int totalBlocks, int blockCount, Action<string> resultCallbackAction)
        {
            List<string> urlBlockList = new List<string>();

            var blockNumbersFromDB = await _repository.GetBlockNumbers();

            for (int i = 0; i < blockCount; i++)
            {
                //Exclude blocks already loaded to database
                if (!blockNumbersFromDB.Contains(totalBlocks - i))
                {
                    string blockHashUrl = "https://blockexplorer.com/api/block-index/" + (totalBlocks - i);
                    urlBlockList.Add(blockHashUrl);
                }
            }

            IEnumerable<Task<string>> downloadBlocksTasks = from blockUrl in urlBlockList select httpHelper.ProcessURL(blockUrl, resultCallbackAction);

            // Use ToArray to execute the query and start the download tasks.  
            Task<string>[] downloadTasks = downloadBlocksTasks.ToArray();

            // Await the completion of all the running tasks.  
            string[] lengths = await Task.WhenAll(downloadTasks);
            return lengths.Length;
        }
    }
}
