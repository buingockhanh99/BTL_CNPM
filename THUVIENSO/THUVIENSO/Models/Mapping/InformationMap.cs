using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace THUVIENSO.Models.Mapping
{
    public class InformationMap : EntityTypeConfiguration<Information>
    {
        public InformationMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.username)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Information");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.username).HasColumnName("username");

            // Relationships
            this.HasRequired(t => t.Account)
                .WithOptional(t => t.Information);

        }
    }
}
