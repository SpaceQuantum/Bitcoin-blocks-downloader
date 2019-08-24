using System;
using System.Collections.Generic;

namespace BitcoinProcessing.DTO
{
    public class BlockDetails
    {
        public string hash { get; set; }
        public int size { get; set; }
        public int height { get; set; }
        public int version { get; set; }
        public string merkleroot { get; set; }
        public List<string> tx { get; set; }
        public int time { get; set; }
        public Int64 nonce { get; set; }
        public string bits { get; set; }
        public double difficulty { get; set; }
        public string chainwork { get; set; }
        public int confirmations { get; set; }
        public string previousblockhash { get; set; }
        public string nextblockhash { get; set; }
        public double reward { get; set; }
        public bool isMainChain { get; set; }
        public PoolInfo poolInfo { get; set; }
    }
}
