namespace BitcoinProcessing.DTO
{
    public class Vout
    {
        public string value { get; set; }
        public int n { get; set; }
        public ScriptPubKey scriptPubKey { get; set; }
        public object spentTxId { get; set; }
        public object spentIndex { get; set; }
        public object spentHeight { get; set; }
    }
}
