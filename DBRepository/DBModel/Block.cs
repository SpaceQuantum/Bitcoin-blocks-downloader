namespace DBRepository.DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Block
    {
        [Key]
        [StringLength(200)]
        public string hash { get; set; }

        public int blockNumber { get; set; }

        public int? size { get; set; }

        public int? height { get; set; }

        public int? version { get; set; }

        [StringLength(50)]
        public string merkleroot { get; set; }

        public string tx { get; set; }

        public int? time { get; set; }

        public int? nonce { get; set; }

        [StringLength(50)]
        public string bits { get; set; }

        public double? difficulty { get; set; }

        [StringLength(50)]
        public string chainwork { get; set; }

        public int? confirmations { get; set; }

        [StringLength(200)]
        public string previousblockhash { get; set; }

        [StringLength(200)]
        public string nextblockhash { get; set; }

        public double? reward { get; set; }

        public bool? isMainChain { get; set; }

        public bool isAllTransactionLoadedToDb { get; set; }
    }
}
