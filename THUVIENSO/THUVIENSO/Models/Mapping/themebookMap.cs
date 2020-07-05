using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace THUVIENSO.Models.Mapping
{
    public class themebookMap : EntityTypeConfiguration<themebook>
    {
        public themebookMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.nametheme)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("themebook");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.nametheme).HasColumnName("nametheme");
        }
    }
}
