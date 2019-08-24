using System.Collections.Generic;

namespace BitcoinProcessing.DTO
{
    public class TransactionDetails
    {
        public string txid { get; set; }
        public int version { get; set; }
        public int locktime { get; set; }
        public List<Vin> vin { get; set; }
        public List<Vout> vout { get; set; }
        public string blockhash { get; set; }
        public int blockheight { get; set; }
        public int confirmations { get; set; }
        public int time { get; set; }
        public int blocktime { get; set; }
        public bool isCoinBase { get; set; }
        public double valueOut { get; set; }
        public int size { get; set; }
        public double valueIn { get; set; }
        public double fees { get; set; }
    }
}
