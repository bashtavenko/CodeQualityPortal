using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimBranchConfiguration : EntityTypeConfiguration<DimBranch>
    {
        public DimBranchConfiguration()
        {
            HasKey(k => k.BranchId);
            Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);
        }
    }
}