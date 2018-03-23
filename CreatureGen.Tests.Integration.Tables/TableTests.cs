using DnDGen.Core.IoC;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class TableTests : IntegrationTests
    {
        protected abstract string tableName { get; }

        [OneTimeSetUp]
        public void TableOneTimeSetup()
        {
            var coreLoader = new CoreModuleLoader();
            coreLoader.ReplaceAssemblyLoaderWith<CreatureGenAssemblyLoader>(kernel);
        }
    }
}