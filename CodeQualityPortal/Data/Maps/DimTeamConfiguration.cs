using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimTeamConfiguration : EntityTypeConfiguration<DimTeam>
    {
        public DimTeamConfiguration()
        {
            HasKey(s => s.TeamId);
            Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);
            Property(k => k.Name).IsRequired();
        }
    }
}