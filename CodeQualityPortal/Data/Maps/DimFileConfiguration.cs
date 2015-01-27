using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimFileConfiguration : EntityTypeConfiguration<DimFile>
    {
        public DimFileConfiguration()
        {
            HasKey(k => k.FileId);
            Property(k => k.FileName).HasColumnType("varchar").HasMaxLength(255);
            Property(k => k.FileName).IsRequired();
            Property(k => k.FileExtension).HasColumnType("varchar").HasMaxLength(255);
            Property(k => k.FileExtension).IsRequired();
            Property(k => k.Url).IsOptional(); // Bitbucket doesn't provide file urls
        }
    }
}