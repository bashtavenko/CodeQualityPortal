using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimNamespaceConfiguration : EntityTypeConfiguration<DimNamespace>
    {
        public DimNamespaceConfiguration()
        {
            HasKey(k => k.NamespaceId);
            Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);

            HasMany(c => c.Types)
                .WithMany(m => m.Namespaces)
                .Map(c =>
                {
                    c.ToTable("DimNamespaceType");
                    c.MapLeftKey("NamespaceId");
                    c.MapRightKey("TypeId");
                });
        }
    }
}