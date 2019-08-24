namespace DBRepository.DBModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBModel : DbContext
    {
        public DBModel()
            : base("name=DBModel")
        {
        }

        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }
        public virtual DbSet<TransactionsIn> TransactionsIns { get; set; }
        public virtual DbSet<TransactionsOut> TransactionsOuts { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Block>()
                .Property(e => e.hash)
                .IsUnicode(false);

            modelBuilder.Entity<Block>()
                .Property(e => e.merkleroot)
                .IsUnicode(false);

            modelBuilder.Entity<Block>()
                .Property(e => e.tx)
                .IsUnicode(false);

            modelBuilder.Entity<Block>()
                .Property(e => e.bits)
                .IsUnicode(false);

            modelBuilder.Entity<Block>()
                .Property(e => e.chainwork)
                .IsUnicode(false);

            modelBuilder.Entity<Block>()
                .Property(e => e.previousblockhash)
                .IsUnicode(false);

            modelBuilder.Entity<Block>()
                .Property(e => e.nextblockhash)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.txid)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.valueOut)
                .HasPrecision(18, 8);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.valueIn)
                .HasPrecision(18, 8);

            modelBuilder.Entity<TransactionDetail>()
                .Property(e => e.blockhash)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionsIn>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionsIn>()
                .Property(e => e.Amount)
                .HasPrecision(16, 8);

            modelBuilder.Entity<TransactionsIn>()
                .Property(e => e.TxId)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionsOut>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<TransactionsOut>()
                .Property(e => e.Amount)
                .HasPrecision(16, 8);

            modelBuilder.Entity<TransactionsOut>()
                .Property(e => e.TxId)
                .IsUnicode(false);

            modelBuilder.Entity<Wallet>()
                .Property(e => e.Balance)
                .HasPrecision(16, 8);
        }
    }
}
