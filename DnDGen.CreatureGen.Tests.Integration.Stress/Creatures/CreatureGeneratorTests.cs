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

        private Creature GenerateAndAssertCreature(string creatureName) => GenerateAndAssertCreature(creatureName, CreatureConstants.Templates.None);

        private Creature GenerateAndAssertCreature(string creatureName, string template)
        {
            var creature = creatureGenerator.Generate(creatureName, template);
            creatureAsserter.AssertCreature(creature);

            return creature;
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

            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);
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

            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);
            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public void StressCreatureAsCharacter()
        {
            stressor.Stress(GenerateAndAssertCreatureAsCharacter);
        }

        private void GenerateAndAssertCreatureAsCharacter()
        {
            var characters = CreatureConstants.GetAllCharacters();
            var randomCreatureName = collectionSelector.SelectRandomFrom(characters);

            var creature = creatureGenerator.GenerateAsCharacter(randomCreatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [Test]
        public async Task StressCreatureAsCharacterAsync()
        {
            await stressor.StressAsync(GenerateAndAssertCreatureAsCharacterAsync);
        }

        private async Task GenerateAndAssertCreatureAsCharacterAsync()
        {
            var characters = CreatureConstants.GetAllCharacters();
            var randomCreatureName = collectionSelector.SelectRandomFrom(characters);

            var creature = await creatureGenerator.GenerateAsCharacterAsync(randomCreatureName, CreatureConstants.Templates.None);
            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [Test]
        public void StressCreatureAsCharacterWithTemplate()
        {
            stressor.Stress(GenerateAndAssertCreatureAsCharacterWithTemplate);
        }

        private void GenerateAndAssertCreatureAsCharacterWithTemplate()
        {
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates);
            if (!compatibleCreatures.ContainsKey(randomTemplate))
            {
                var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(c, randomTemplate)).ToArray();
                compatibleCreatures.TryAdd(randomTemplate, validCreatures);
            }

            var characters = CreatureConstants.GetAllCharacters().Intersect(compatibleCreatures[randomTemplate]);
            var randomCreatureName = collectionSelector.SelectRandomFrom(characters);

            var creature = creatureGenerator.GenerateAsCharacter(randomCreatureName, randomTemplate);
            creatureAsserter.AssertCreatureAsCharacter(creature);

            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);
        }

        [Test]
        public async Task StressCreatureAsCharacterWithTemplateAsync()
        {
            await stressor.StressAsync(GenerateAndAssertCreatureAsCharacterWithTemplateAsync);
        }

        private async Task GenerateAndAssertCreatureAsCharacterWithTemplateAsync()
        {
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates);
            if (!compatibleCreatures.ContainsKey(randomTemplate))
            {
                var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(c, randomTemplate)).ToArray();
                compatibleCreatures.TryAdd(randomTemplate, validCreatures);
            }

            var characters = CreatureConstants.GetAllCharacters().Intersect(compatibleCreatures[randomTemplate]);
            var randomCreatureName = collectionSelector.SelectRandomFrom(characters);

            var creature = await creatureGenerator.GenerateAsCharacterAsync(randomCreatureName, randomTemplate);

            Assert.That(creature.Template, Is.EqualTo(randomTemplate));
            creatureAsserter.AssertCreatureAsCharacter(creature);

            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);
        }

        [Test]
        public void StressRandomCreatureOfTemplate()
        {
            stressor.Stress(GenerateAndAssertRandomCreatureOfTemplate);
        }

        private void GenerateAndAssertRandomCreatureOfTemplate()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var randomTemplate = collectionSelector.SelectRandomFrom(templates);

            var creature = creatureGenerator.GenerateRandomOfTemplate(randomTemplate);

            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);
            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public void StressRandomCreatureOfTemplateAsCharacter()
        {
            stressor.Stress(GenerateAndAssertRandomCreatureOfTemplateAsCharacter);
        }

        private void GenerateAndAssertRandomCreatureOfTemplateAsCharacter()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var randomTemplate = collectionSelector.SelectRandomFrom(templates);

            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter(randomTemplate);

            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);
            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [Test]
        public async Task StressRandomCreatureOfTemplateAsync()
        {
            await stressor.StressAsync(GenerateAndAssertRandomCreatureOfTemplateAsync);
        }

        private async Task GenerateAndAssertRandomCreatureOfTemplateAsync()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var randomTemplate = collectionSelector.SelectRandomFrom(templates);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync(randomTemplate);

            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);
            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public async Task StressRandomCreatureOfTemplateAsCharacterAsync()
        {
            await stressor.StressAsync(GenerateAndAssertRandomCreatureOfTemplateAsCharacterAsync);
        }

        private async Task GenerateAndAssertRandomCreatureOfTemplateAsCharacterAsync()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var randomTemplate = collectionSelector.SelectRandomFrom(templates);

            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync(randomTemplate);

            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);
            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [Test]
        public void StressRandomCreatureOfType()
        {
            stressor.Stress(GenerateAndAssertRandomCreatureOfType);
        }

        private void GenerateAndAssertRandomCreatureOfType()
        {
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var randomType = collectionSelector.SelectRandomFrom(types.Union(subtypes));

            GenerateAndAssertRandomCreatureOfType(randomType);
        }

        private void GenerateAndAssertRandomCreatureOfType(string type)
        {
            var creature = creatureGenerator.GenerateRandomOfType(type);

            var types = CreatureConstants.Types.GetAll();
            if (types.Contains(type))
                Assert.That(creature.Type.Name, Is.EqualTo(type), creature.Summary);
            else
                Assert.That(creature.Type.SubTypes, Contains.Item(type), creature.Summary);

            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public void StressRandomCreatureOfTypeAsCharacter()
        {
            stressor.Stress(GenerateAndAssertRandomCreatureOfTypeAsCharacter);
        }

        private void GenerateAndAssertRandomCreatureOfTypeAsCharacter()
        {
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var nonCharacterTypes = new[]
            {
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Ooze,
                CreatureConstants.Types.Vermin,
                CreatureConstants.Types.Subtypes.Swarm,
            };
            var randomType = collectionSelector.SelectRandomFrom(types.Union(subtypes).Except(nonCharacterTypes));

            var creature = creatureGenerator.GenerateRandomOfTypeAsCharacter(randomType);

            if (types.Contains(randomType))
                Assert.That(creature.Type.Name, Is.EqualTo(randomType), creature.Summary);
            else
                Assert.That(creature.Type.SubTypes, Contains.Item(randomType), creature.Summary);

            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [Test]
        public async Task StressRandomCreatureOfTypeAsync()
        {
            await stressor.StressAsync(GenerateAndAssertRandomCreatureOfTypeAsync);
        }

        private async Task GenerateAndAssertRandomCreatureOfTypeAsync()
        {
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var randomType = collectionSelector.SelectRandomFrom(types.Union(subtypes));

            var creature = await creatureGenerator.GenerateRandomOfTypeAsync(randomType);

            if (types.Contains(randomType))
                Assert.That(creature.Type.Name, Is.EqualTo(randomType), creature.Summary);
            else
                Assert.That(creature.Type.SubTypes, Contains.Item(randomType), creature.Summary);

            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public async Task StressRandomCreatureOfTypeAsCharacterAsync()
        {
            await stressor.StressAsync(GenerateAndAssertRandomCreatureOfTypeAsCharacterAsync);
        }

        private async Task GenerateAndAssertRandomCreatureOfTypeAsCharacterAsync()
        {
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var nonCharacterTypes = new[]
            {
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Ooze,
                CreatureConstants.Types.Vermin,
                CreatureConstants.Types.Subtypes.Swarm,
            };
            var randomType = collectionSelector.SelectRandomFrom(types.Union(subtypes).Except(nonCharacterTypes));

            var creature = await creatureGenerator.GenerateRandomOfTypeAsCharacterAsync(randomType);

            if (types.Contains(randomType))
                Assert.That(creature.Type.Name, Is.EqualTo(randomType), creature.Summary);
            else
                Assert.That(creature.Type.SubTypes, Contains.Item(randomType), creature.Summary);

            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [TestCase(CreatureConstants.Dragon_Brass_Young, CreatureConstants.Templates.Ghost)]
        [TestCase(CreatureConstants.Dragon_White_Old, CreatureConstants.Templates.HalfFiend)]
        [TestCase(CreatureConstants.Dragon_Brass_Young, CreatureConstants.Templates.HalfCelestial)]
        [Repeat(100)]
        [Ignore("Only use this for debugging")]
        public void BUG_StressSpecificCreature(string creatureName, string template)
        {
            stressor.Stress(() => GenerateAndAssertCreature(creatureName, template));
        }

        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [Repeat(100)]
        [Ignore("Only use this for debugging")]
        public void BUG_StressSpecificType(string type)
        {
            stressor.Stress(() => GenerateAndAssertRandomCreatureOfType(type));
        }
    }
}