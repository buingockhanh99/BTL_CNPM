using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace THUVIENSO.Models.Mapping
{
    public class MonneyMap : EntityTypeConfiguration<Monney>
    {
        public MonneyMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Monney");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.monney1).HasColumnName("monney");

            // Relationships
            this.HasRequired(t => t.Account)
                .WithOptional(t => t.Monney);

        }
    }
}
