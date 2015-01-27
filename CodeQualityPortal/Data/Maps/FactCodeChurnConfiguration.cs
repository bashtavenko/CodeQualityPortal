using System.Data.Entity.ModelConfiguration;

namespace CodeQualityPortal.Data.Maps
{
    public class FactCodeChurnConfiguration : EntityTypeConfiguration<FactCodeChurn>
    {
        public FactCodeChurnConfiguration()
        {
            HasKey(k => k.CodeChurnId);
        }
    }
}