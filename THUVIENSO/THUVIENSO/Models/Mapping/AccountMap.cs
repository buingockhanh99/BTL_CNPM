using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace THUVIENSO.Models.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Account");
            this.Property(t => t.accountname).HasColumnName("accountname");
            this.Property(t => t.passwords).HasColumnName("passwords");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.levels).HasColumnName("levels");
        }
    }
}
