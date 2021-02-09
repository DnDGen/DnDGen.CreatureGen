using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Integration.Stress.Creatures
{
    [TestFixture]
    public class CreatureGeneratorTests : StressTests
    {
        private CreatureAsserter creatureAsserter;
        private ICollectionSelector collectionSelector;
        private ICreatureGenerator creatureGenerator;
        private ConcurrentDictionary<string, IEnumerable<string>> compatibleCreatures;

        [SetUp]
        public void Setup()
        {
            creatureAsserter = new CreatureAsserter();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();
            compatibleCreatures = new ConcurrentDictionary<string, IEnumerable<string>>();
        }

        [Test]
        public void StressCreature()
        {
            stressor.Stress(GenerateAndAssertCreature);
        }

        private void GenerateAndAssertCreature()
        {
            var randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);
            GenerateAndAssertCreature(randomCreatureName);
        }

        private void GenerateAndAssertCreature(string creatureName)
        {
            var creature = creatureGenerator.Generate(creatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public async Task StressCreatureAsync()
        {
            await stressor.StressAsync(GenerateAndAssertCreatureAsync);
        }

        private async Task GenerateAndAssertCreatureAsync()
        {
            var randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);

            var creature = await creatureGenerator.GenerateAsync(randomCreatureName, CreatureConstants.Templates.None);

            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public void StressCreatureWithTemplate()
        {
            stressor.Stress(GenerateAndAssertCreatureWithTemplate);
        }

        private void GenerateAndAssertCreatureWithTemplate()
        {
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates);
            if (!compatibleCreatures.ContainsKey(randomTemplate))
            {
                var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(c, randomTemplate)).ToArray();
                compatibleCreatures.TryAdd(randomTemplate, validCreatures);
            }

            var randomCreatureName = collectionSelector.SelectRandomFrom(compatibleCreatures[randomTemplate]);

            var creature = creatureGenerator.Generate(randomCreatureName, randomTemplate);

            Assert.That(creature.Template, Is.EqualTo(randomTemplate));
            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public async Task StressCreatureWithTemplateAsync()
        {
            await stressor.StressAsync(GenerateAndAssertCreatureWithTemplateAsync);
        }

        private async Task GenerateAndAssertCreatureWithTemplateAsync()
        {
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates);
            if (!compatibleCreatures.ContainsKey(randomTemplate))
            {
                var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(c, randomTemplate)).ToArray();
                compatibleCreatures.TryAdd(randomTemplate, validCreatures);
            }

            var randomCreatureName = collectionSelector.SelectRandomFrom(compatibleCreatures[randomTemplate]);

            var creature = await creatureGenerator.GenerateAsync(randomCreatureName, randomTemplate);

            Assert.That(creature.Template, Is.EqualTo(randomTemplate));
            creatureAsserter.AssertCreature(creature);
        }

        [TestCase(CreatureConstants.Aranea)]
        [TestCase(CreatureConstants.Athach)]
        [TestCase(CreatureConstants.Erinyes)]
        [TestCase(CreatureConstants.Xill)]
        public void BUG_StressSpecificCreature(string creatureName)
        {
            stressor.Stress(() => GenerateAndAssertCreature(creatureName));
        }
    }
}