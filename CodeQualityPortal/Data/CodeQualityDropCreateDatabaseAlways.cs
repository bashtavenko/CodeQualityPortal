using System.Data.Entity;

namespace CodeQualityPortal.Data
{
    public class CodeQualityDropCreateDatabaseAlways : DropCreateDatabaseAlways<CodeQualityContext>
    {
        protected override void Seed(CodeQualityContext context)
        {
            new SampleDataSeeder(context).Seed();
        }
    }
}