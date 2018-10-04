using CreatureGen.Creatures;
using CreatureGen.Generators.Creatures;
using DnDGen.Core.Selectors.Collections;
using Ninject;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Stress.Creatures
{
    [TestFixture]
    public class CreatureGeneratorTests : StressTests
    {
        [Inject]
        public CreatureAsserter CreatureAsserter { get; set; }
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        public ICreatureGenerator CreatureGenerator { get; set; }

        [Test]
        public void StressCreature()
        {
            stressor.Stress(GenerateAndAssertCreature);
        }

        private void GenerateAndAssertCreature()
        {
            var randomCreatureName = CollectionSelector.SelectRandomFrom(allCreatures);

            var creature = CreatureGenerator.Generate(randomCreatureName, CreatureConstants.Templates.None);

            CreatureAsserter.AssertCreature(creature);
        }

        [Test]
        public void StressCreatureWithTemplate()
        {
            stressor.Stress(GenerateAndAssertCreatureWithTemplate);
        }

        private void GenerateAndAssertCreatureWithTemplate()
        {
            var randomCreatureName = CollectionSelector.SelectRandomFrom(allCreatures);
            var randomTemplate = CollectionSelector.SelectRandomFrom(allTemplates);

            while (!CreatureVerifier.VerifyCompatibility(randomCreatureName, randomTemplate))
                randomCreatureName = CollectionSelector.SelectRandomFrom(allCreatures);

            var creature = CreatureGenerator.Generate(randomCreatureName, randomTemplate);

            CreatureAsserter.AssertCreature(creature);
        }
    }
}