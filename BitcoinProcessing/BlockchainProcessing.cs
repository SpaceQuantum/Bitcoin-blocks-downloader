using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BitcoinProcessing.DTO;
using DBRepository.DBModel;
using Newtonsoft.Json;
using ProcessingContracts;

namespace BitcoinProcessing
{
    public class BlockchainProcessing : IProcessing
    {
        private readonly ILogger _logger;
        private readonly IRepository _repository;
        //HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(60) };
        private readonly HttpHelper _httpHelper = new HttpHelper();

        private readonly object _lockObject = new object();

        private int _tasksProcessed;

        private int _blockCount;

        private readonly ConcurrentBag<string> _blocksHashList = new ConcurrentBag<string>();

        private readonly ConcurrentDictionary<string, List<string>> _blockTransactionsDictionary = new ConcurrentDictionary<string, List<string>>();
        
        public BlockchainProcessing(ILogger logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
            ServicePointManager.DefaultConnectionLimit = 100;
        }

        public async Task Process(int totalLastProcessingCount)
        {
            //int minWorker, minIOC;
            //// Get the current settings.
            //ThreadPool.GetMinThreads(out minWorker, out minIOC);
            //// Change the minimum number of worker threads to four, but
            //// keep the old setting for minimum asynchronous I/O 
            //// completion threads.
            //if (ThreadPool.SetMinThreads(minWorker, 100))
            //{
            //    // The minimum number of threads was set successfully.
            //    _logger.LogMessage($"Minimum worker threads: {minWorker}");
            //    _logger.LogMessage($"Minimum IOC: {minIOC}");
            //}
            //else
            //{
            //    // The minimum number of threads was not changed.
            //    _logger.LogMessage($"Minimum worker threads: {minWorker}");
            //    _logger.LogMessage($"Minimum IOC: {minIOC}");
            //}

            _blockCount = totalLastProcessingCount;

            var totalBlocks = await GetTotalBlocksNumber();
            if (totalBlocks != 0)
            {
                //---------- Get block hashes
                BlockHashProcessing blockHashProcessing = new BlockHashProcessing(_logger, _repository);
                int lenth = await blockHashProcessing.GetBlockHashes(totalBlocks, _blockCount, ProecssBlockHashResult);

                //---------- Get block details
                BlockDetailsProcessing blockDetailsProcessing = new BlockDetailsProcessing(_logger, _repository);
                await blockDetailsProcessing.GetBlocksDetails(_blocksHashList, ProcessBlockDetailsResult);

                //---------- Get transaction details ---------------
                TransactionDetailsProcesing transactionDetailsProcesing = new TransactionDetailsProcesing(_repository);
                await transactionDetailsProcesing.GetTransactionsDetails(_blockTransactionsDictionary, ProcessTransactionDetailsResult);
               
                _logger.LogMessage("Total: " + lenth);
                _logger.LogMessage($"Failed requests: {lenth - _tasksProcessed}");
            }
        }
        
        private async Task<int> GetTotalBlocksNumber()
        {
            string url = "https://blockexplorer.com/api/status?q=getblockcount";
            
            var result = await _httpHelper.ProcessURL(url, ProcessBlockCountResult);
            if (result != string.Empty)
            {
                var info = JsonConvert.DeserializeObject<InfoRoot>(result).info;
                var lastBlock = info.blocks;
                return lastBlock;
            }
            return 0;
        }

        private async void ProcessTransactionDetailsResult(string result)
        {
            try
            {
                var info = JsonConvert.DeserializeObject<TransactionDetails>(result);

                await _repository.SaveTransaction(new TransactionDetail
                {
                    txid = info.txid,
                    blockhash = info.blockhash,
                    blockheight = info.blockheight,
                    valueOut = (decimal) info.valueOut,
                    valueIn = (decimal) info.valueIn
                });

                _logger.LogMessage("Total transactions blockhash: " + info.blockhash);
                _logger.LogMessage("Total transactions txid: " + info.txid);
                _logger.LogMessage("Total transactions in: " + info.valueIn);
                _logger.LogMessage("Total transactions out: " + info.valueOut);
            }
            catch (Exception e)
            {
                _logger.LogMessage(e.ToString());
            }
        }

        private async void ProcessBlockDetailsResult(string result)
        {
            try
            {
                var info = JsonConvert.DeserializeObject<BlockDetails>(result);               
                _blockTransactionsDictionary.TryAdd(info.hash, info.tx);

                await _repository.AddBlock(new Block
                {
                    hash = info.hash,
                    blockNumber = info.height,
                    tx = string.Join(",", info.tx)
                });

                _logger.LogMessage("Total transactions in block: " + info.tx.Count);
            }
            catch (Exception e)
            {
                _logger.LogMessage(e.ToString());
            }
        }

        private void ProcessBlockCountResult(string result)
        {
            try
            {
                var info = JsonConvert.DeserializeObject<InfoRoot>(result).info;
                var lastBlock = info.blocks;
                _logger.LogMessage($"Block count request processing: {lastBlock}");
            }
            catch (Exception e)
            {
                _logger.LogMessage(e.ToString());
            }
        }

        private void ProecssBlockHashResult(string result)
        {
            try
            {
                int i;
                lock (_lockObject)
                {
                    i = _tasksProcessed++;
                }

                var blockHash = JsonConvert.DeserializeObject<BlockHash>(result).blockHash;
                _blocksHashList.Add(blockHash);
                
                _logger.LogMessage($"Block hash request processing: {i}");
            }
            catch (Exception e)
            {
                _logger.LogMessage(e.ToString());
            }
        }
    }
}
