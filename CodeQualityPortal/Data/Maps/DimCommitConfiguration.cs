using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimCommitConfiguration : EntityTypeConfiguration<DimCommit>
    {
        public DimCommitConfiguration()
        {
            HasKey(s => s.CommitId);
            Property(k => k.Sha).HasColumnType("varchar").HasMaxLength(255);
            Property(k => k.Sha).IsRequired();
            Property(k => k.Url).IsRequired();

            HasMany(c => c.Files)
                .WithMany(f => f.Commits)
                .Map(c =>
                {
                    c.ToTable("DimCommitFile");
                    c.MapLeftKey("CommitId");
                    c.MapRightKey("FileId");
                });
        }
    }
}