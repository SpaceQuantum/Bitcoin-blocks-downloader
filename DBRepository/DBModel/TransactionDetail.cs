namespace DBRepository.DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TransactionDetail
    {
        [Key]
        [StringLength(200)]
        public string txid { get; set; }

        public decimal? valueOut { get; set; }

        public decimal? valueIn { get; set; }

        [Required]
        [StringLength(200)]
        public string blockhash { get; set; }

        public int blockheight { get; set; }
    }
}
