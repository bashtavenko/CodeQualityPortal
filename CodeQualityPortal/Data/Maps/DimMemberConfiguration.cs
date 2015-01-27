using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimMemberConfiguration : EntityTypeConfiguration<DimMember>
    {
        public DimMemberConfiguration()
        {
            HasKey(k => k.MemberId);
            Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);            
        }
    }
}