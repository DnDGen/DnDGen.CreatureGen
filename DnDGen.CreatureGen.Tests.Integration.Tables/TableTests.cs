using DnDGen.Infrastructure.IoC;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class TableTests : IntegrationTests
    {
        protected abstract string tableName { get; }

        [OneTimeSetUp]
        public void TableOneTimeSetup()
        {
            var infrastructureLoader = new InfrastructureModuleLoader();
            infrastructureLoader.ReplaceAssemblyLoaderWith<CreatureGenAssemblyLoader>(kernel);
        }
    }
}