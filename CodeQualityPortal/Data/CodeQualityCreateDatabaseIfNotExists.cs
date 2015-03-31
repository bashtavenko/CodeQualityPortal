using System.Data.Entity;

namespace CodeQualityPortal.Data
{
    public class CodeQualityCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<CodeQualityContext>
    {
        protected override void Seed(CodeQualityContext context)
        {
            new SampleDataSeeder(context).Seed();
        }
    }
}