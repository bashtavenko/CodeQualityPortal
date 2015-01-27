using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimTargetConfiguration : EntityTypeConfiguration<DimTarget>
    {
        public DimTargetConfiguration()
        {
            HasKey(k => k.TargetId);
            Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);
            Property(k => k.Tag).HasColumnType("varchar").HasMaxLength(255);
            Property(k => k.FileName).HasColumnType("varchar").HasMaxLength(255);
        }
    }
}