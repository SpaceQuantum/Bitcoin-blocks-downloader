namespace DBRepository.DBModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Wallet
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        public decimal Balance { get; set; }
    }
}
