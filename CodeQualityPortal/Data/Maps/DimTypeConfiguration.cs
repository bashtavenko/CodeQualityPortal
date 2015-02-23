using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimTypeConfiguration : EntityTypeConfiguration<DimType>
    {
        public DimTypeConfiguration()
        {
            HasKey(k => k.TypeId);
            Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);

            HasMany(c => c.Members)
                .WithMany(m => m.Types)
                .Map(c =>
                {
                    c.ToTable("DimTypeMember");
                    c.MapLeftKey("TypeId");
                    c.MapRightKey("MemberId");
                });
        }
    }
}