using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimRepoConfiguration : EntityTypeConfiguration<DimRepo>
    {
        public DimRepoConfiguration()
        {
            HasKey(s => s.RepoId);
            Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);
            Property(k => k.Name).IsRequired();
        }
    }
}