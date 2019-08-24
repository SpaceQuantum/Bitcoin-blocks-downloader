using System;

namespace BitcoinProcessing.DTO
{
    public class Vin
    {
        public string coinbase { get; set; }
        public long sequence { get; set; }
        public int n { get; set; }
        public string txid { get; set; }
        public int vout { get; set; }
        public ScriptSig scriptSig { get; set; }
        public string addr { get; set; }
        public Int64 valueSat { get; set; }
        public double value { get; set; }
        public object doubleSpentTxID { get; set; }
    }
}
