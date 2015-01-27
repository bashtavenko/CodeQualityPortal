using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimDateConfiguration : EntityTypeConfiguration<DimDate>
    {
        public DimDateConfiguration()
        {
            HasKey(k => k.DateId);
        }
    }
}