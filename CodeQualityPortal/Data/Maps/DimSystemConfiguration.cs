using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimSystemConfiguration : EntityTypeConfiguration<DimSystem>
    {
        public DimSystemConfiguration()
        {
            HasKey(k => k.SystemId);
            Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);

            HasMany(c => c.Modules)
                .WithMany(m => m.Systems)
                .Map(c =>
                {
                    c.ToTable("DimSystemModule");
                    c.MapLeftKey("SystemId");
                    c.MapRightKey("ModuleId");
                }
            );
        }
    }
}