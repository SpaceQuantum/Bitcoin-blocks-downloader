using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitcoinProcessing;
using DBRepository;
using ProcessingContracts;

namespace BitcoinBlocksDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            IProcessing blockchainProcessing = new BlockchainProcessing(new ConsoleLogger.ConsoleLogger(), new Repository());
            blockchainProcessing.Process(10).Wait();

            Console.Read();
        }
    }
}
