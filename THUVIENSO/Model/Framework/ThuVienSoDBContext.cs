namespace Model.Framework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ThuVienSoDBContext : DbContext
    {
        public ThuVienSoDBContext()
            : base("name=ThuVienSoDBContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<book> books { get; set; }
        public virtual DbSet<Information> Information { get; set; }
        public virtual DbSet<Monney> Monneys { get; set; }
        public virtual DbSet<themebook> themebooks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.accountname)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.passwords)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasOptional(e => e.Information)
                .WithRequired(e => e.Account);

            modelBuilder.Entity<Account>()
                .HasOptional(e => e.Monney)
                .WithRequired(e => e.Account);

            modelBuilder.Entity<themebook>()
                .HasOptional(e => e.book)
                .WithRequired(e => e.themebook);
        }
    }
}
