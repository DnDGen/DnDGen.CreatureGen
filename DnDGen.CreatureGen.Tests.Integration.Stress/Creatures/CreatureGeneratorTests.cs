using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Integration.Stress.Creatures
{
    [TestFixture]
    public class CreatureGeneratorTests : StressTests
    {
        private CreatureAsserter creatureAsserter;
        private ICollectionSelector collectionSelector;
        private ICreatureGenerator creatureGenerator;

        [SetUp]
        public void Setup()
        {
            creatureAsserter = new CreatureAsserter();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();
        }

        [Test]
        public void StressCreature()
        {
            stressor.Stress(GenerateAndAssertCreature);
        }

        private void GenerateAndAssertCreature()
        {
            var randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);

            var creature = creatureGenerator.Generate(randomCreatureName, CreatureConstants.Templates.None);

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
            var randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates);

            var attempts = 100;
            while (!creatureVerifier.VerifyCompatibility(randomCreatureName, randomTemplate) && attempts-- > 0)
                randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);

            if (attempts == 0 && !creatureVerifier.VerifyCompatibility(randomCreatureName, randomTemplate))
                Assert.Fail($"After 100 attempts, could not find a creature that fits template {randomTemplate}");

            var creature = creatureGenerator.Generate(randomCreatureName, randomTemplate);

            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public async Task StressCreatureWithTemplateAsync()
        {
            await stressor.StressAsync(GenerateAndAssertCreatureWithTemplateAsync);
        }

        private async Task GenerateAndAssertCreatureWithTemplateAsync()
        {
            var randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates);

            var attempts = 100;
            while (!creatureVerifier.VerifyCompatibility(randomCreatureName, randomTemplate) && attempts-- > 0)
                randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);

            if (attempts == 0 && !creatureVerifier.VerifyCompatibility(randomCreatureName, randomTemplate))
                Assert.Fail($"After 100 attempts, could not find a creature that fits template {randomTemplate}");

            var creature = await creatureGenerator.GenerateAsync(randomCreatureName, randomTemplate);

            creatureAsserter.AssertCreature(creature);
        }
    }
}