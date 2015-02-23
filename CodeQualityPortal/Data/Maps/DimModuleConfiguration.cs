using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class DimModuleConfiguration : EntityTypeConfiguration<DimModule>
    {
        public DimModuleConfiguration()
        {
            HasKey(k => k.ModuleId);
            Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);

            HasMany(c => c.Namespaces)
                .WithMany(m => m.Modules)
                .Map(c =>
                {
                    c.ToTable("DimModuleNamespace");
                    c.MapLeftKey("ModuleId");
                    c.MapRightKey("NamespaceId");
                });
        }
    }
}