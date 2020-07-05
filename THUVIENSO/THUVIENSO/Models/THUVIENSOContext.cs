using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using THUVIENSO.Models.Mapping;

namespace THUVIENSO.Models
{
    public partial class THUVIENSOContext : DbContext
    {
        static THUVIENSOContext()
        {
            Database.SetInitializer<THUVIENSOContext>(null);
        }

        public THUVIENSOContext()
            : base("Name=THUVIENSOContext")
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<book> books { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<Monney> Monneys { get; set; }
        public DbSet<themebook> themebooks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new bookMap());
            modelBuilder.Configurations.Add(new InformationMap());
            modelBuilder.Configurations.Add(new MonneyMap());
            modelBuilder.Configurations.Add(new themebookMap());
        }
    }
}
