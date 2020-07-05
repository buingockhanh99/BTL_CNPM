using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace THUVIENSO.Models.Mapping
{
    public class bookMap : EntityTypeConfiguration<book>
    {
        public bookMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.namebook)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("book");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.namebook).HasColumnName("namebook");
            this.Property(t => t.content).HasColumnName("content");
            this.Property(t => t.price).HasColumnName("price");

            // Relationships
            this.HasRequired(t => t.themebook)
                .WithOptional(t => t.book);

        }
    }
}
