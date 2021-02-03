using DnDGen.CreatureGen.IoC;
using Ninject;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration
{
    [TestFixture]
    public abstract class IntegrationTests
    {
        protected IKernel kernel;

        [OneTimeSetUp]
        public void IntegrationTestsFixtureSetup()
        {
            kernel = new StandardKernel();

            var creatureGenLoader = new CreatureGenModuleLoader();
            creatureGenLoader.LoadModules(kernel);
        }

        protected T GetNewInstanceOf<T>()
        {
            return kernel.Get<T>();
        }

        protected T GetNewInstanceOf<T>(string name)
        {
            return kernel.Get<T>(name);
        }
    }
}