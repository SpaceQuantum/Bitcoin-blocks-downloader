namespace BitcoinProcessing.DTO
{
    public class Info
    {
        public int version { get; set; }
        public int protocolversion { get; set; }
        public int blocks { get; set; }
        public int timeoffset { get; set; }
        public int connections { get; set; }
        public string proxy { get; set; }
        public double difficulty { get; set; }
        public bool testnet { get; set; }
        public double relayfee { get; set; }
        public string errors { get; set; }
        public string network { get; set; }
    }

    public class InfoRoot
    {
        public Info info { get; set; }
    }
}
