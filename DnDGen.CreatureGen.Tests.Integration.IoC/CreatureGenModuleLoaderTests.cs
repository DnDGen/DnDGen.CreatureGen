using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.IoC;
using DnDGen.Infrastructure.Generators;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Generators;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System;

namespace DnDGen.CreatureGen.Tests.Integration.IoC
{
    [TestFixture]
    public class CreatureGenModuleLoaderTests : IoCTests
    {
        [Test]
        public void ModuleLoaderCanBeRunTwice()
        {
            //INFO: First time was in the IntegrationTest one-time setup
            var creatureGenLoader = new CreatureGenModuleLoader();
            creatureGenLoader.LoadModules(kernel);

            var treasureGenerator = GetNewInstanceOf<ICreatureGenerator>();
            Assert.That(treasureGenerator, Is.Not.Null);
        }

        [Test]
        public void ModuleLoaderLoadsRollGenDependency()
        {
            AssertNotSingleton<Dice>();
            AssertSingleton<Random>();
        }

        [Test]
        public void ModuleLoaderLoadsInfrastructureDependency()
        {
            AssertNotSingleton<JustInTimeFactory>();
            AssertNotSingleton<IPercentileSelector>();
            AssertNotSingleton<ICollectionSelector>();
        }

        [Test]
        public void ModuleLoaderLoadsTreasureGenDependency()
        {
            AssertNotSingleton<ITreasureGenerator>();
            AssertNotSingleton<IItemsGenerator>();
        }
    }
}
