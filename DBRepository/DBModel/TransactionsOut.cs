namespace DBRepository.DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransactionsOut")]
    public partial class TransactionsOut
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        public decimal Amount { get; set; }

        public int Confirmations { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        public string TxId { get; set; }
    }
}
