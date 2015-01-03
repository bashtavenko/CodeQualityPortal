using NUnit.Framework;

namespace CodeQualityPortal.IntegrationTests
{
    [SetUpFixture]
    public class NinjectSetupFixture
    {
        [SetUp]
        public void Setup()
        {
            NinjectWebCommon.Start();
        }

        [TearDown]
        public void TearDown()
        {
            NinjectWebCommon.Stop();
        }
    }
}
