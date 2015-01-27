using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimNamespaceConfiguration : EntityTypeConfiguration<DimNamespace>
    {
        public DimNamespaceConfiguration()
        {
            HasKey(k => k.NamespaceId);
            Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);
        }
    }
}