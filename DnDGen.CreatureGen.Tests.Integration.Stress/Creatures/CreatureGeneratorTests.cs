using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Stopwatch stopwatch;
        private IEnumerable<string> nonCharacterTypes;
        private IEnumerable<string> nonCharacterTemplates;

        [SetUp]
        public void Setup()
        {
            creatureAsserter = new CreatureAsserter();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();
            stopwatch = new Stopwatch();

            nonCharacterTypes = new[]
            {
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Elemental,
                CreatureConstants.Types.Ooze,
                CreatureConstants.Types.Vermin,
                CreatureConstants.Types.Subtypes.Swarm,
            };

            nonCharacterTemplates = new[]
            {
                CreatureConstants.Templates.Skeleton,
                CreatureConstants.Templates.Zombie,
            };
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
            stopwatch.Restart();
            var creature = creatureGenerator.Generate(creatureName, template);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(creatureName), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(template), creature.Summary);

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

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateAsync(randomCreatureName, CreatureConstants.Templates.None);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(randomCreatureName), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None), creature.Summary);

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
            var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(false, creature: c, template: randomTemplate));
            var randomCreatureName = collectionSelector.SelectRandomFrom(validCreatures);

            stopwatch.Restart();
            var creature = creatureGenerator.Generate(randomCreatureName, randomTemplate);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(randomCreatureName), creature.Summary);
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
            var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(false, creature: c, template: randomTemplate));
            var randomCreatureName = collectionSelector.SelectRandomFrom(validCreatures);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateAsync(randomCreatureName, randomTemplate);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(randomCreatureName), creature.Summary);
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

            stopwatch.Restart();
            var creature = creatureGenerator.GenerateAsCharacter(randomCreatureName, CreatureConstants.Templates.None);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(randomCreatureName), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None), creature.Summary);

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

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateAsCharacterAsync(randomCreatureName, CreatureConstants.Templates.None);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(randomCreatureName), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.None), creature.Summary);

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
            var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(true, creature: c, template: randomTemplate));
            var randomCreatureName = collectionSelector.SelectRandomFrom(validCreatures);

            stopwatch.Restart();
            var creature = creatureGenerator.GenerateAsCharacter(randomCreatureName, randomTemplate);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(randomCreatureName), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);

            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [Test]
        public async Task StressCreatureAsCharacterWithTemplateAsync()
        {
            await stressor.StressAsync(GenerateAndAssertCreatureAsCharacterWithTemplateAsync);
        }

        private async Task GenerateAndAssertCreatureAsCharacterWithTemplateAsync()
        {
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates.Except(nonCharacterTemplates));
            var compatible = creatureVerifier.VerifyCompatibility(true, template: randomTemplate);
            if (!compatible)
            {
                Assert.Fail($"Invalid combination: Template {randomTemplate}; As Character True");
            }

            var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(true, creature: c, template: randomTemplate));
            var randomCreatureName = collectionSelector.SelectRandomFrom(validCreatures);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateAsCharacterAsync(randomCreatureName, randomTemplate);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(randomCreatureName), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);

            creatureAsserter.AssertCreatureAsCharacter(creature);
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

            stopwatch.Restart();
            var creature = creatureGenerator.GenerateRandomOfTemplate(randomTemplate);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);

            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public void StressRandomCreatureOfTemplateWithChallengeRating()
        {
            stressor.Stress(GenerateAndAssertRandomCreatureOfTemplateWithChallengeRating);
        }

        private void GenerateAndAssertRandomCreatureOfTemplateWithChallengeRating()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var randomTemplate = collectionSelector.SelectRandomFrom(templates);

            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var templateChallengeRatings = challengeRatings.Where(cr => creatureVerifier.VerifyCompatibility(false, template: randomTemplate, challengeRating: cr));
            var challengeRating = collectionSelector.SelectRandomFrom(templateChallengeRatings);

            stopwatch.Restart();
            var creature = creatureGenerator.GenerateRandomOfTemplate(randomTemplate, challengeRating);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), creature.Summary);

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

            stopwatch.Restart();
            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter(randomTemplate);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);

            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [Test]
        public void StressRandomCreatureOfTemplateAsCharacterWithChallengeRating()
        {
            stressor.Stress(GenerateAndAssertRandomCreatureOfTemplateAsCharacterWithChallengeRating);
        }

        private void GenerateAndAssertRandomCreatureOfTemplateAsCharacterWithChallengeRating()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var randomTemplate = collectionSelector.SelectRandomFrom(templates);

            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var templateChallengeRatings = challengeRatings.Where(cr => creatureVerifier.VerifyCompatibility(true, template: randomTemplate, challengeRating: cr));
            var challengeRating = collectionSelector.SelectRandomFrom(templateChallengeRatings);

            stopwatch.Restart();
            var creature = creatureGenerator.GenerateRandomOfTemplateAsCharacter(randomTemplate, challengeRating);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), creature.Summary);

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

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync(randomTemplate);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);

            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public async Task StressRandomCreatureOfTemplateWithChallengeRatingAsync()
        {
            await stressor.StressAsync(GenerateAndAssertRandomCreatureOfTemplateWithChallengeRatingAsync);
        }

        private async Task GenerateAndAssertRandomCreatureOfTemplateWithChallengeRatingAsync()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var randomTemplate = collectionSelector.SelectRandomFrom(templates);

            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var templateChallengeRatings = challengeRatings.Where(cr => creatureVerifier.VerifyCompatibility(false, template: randomTemplate, challengeRating: cr));
            var challengeRating = collectionSelector.SelectRandomFrom(templateChallengeRatings);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomOfTemplateAsync(randomTemplate, challengeRating);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), creature.Summary);

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

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync(randomTemplate);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);

            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [Test]
        public async Task StressRandomCreatureOfTemplateAsCharacterWithChallengeRatingAsync()
        {
            await stressor.StressAsync(GenerateAndAssertRandomCreatureOfTemplateAsCharacterWithChallengeRatingAsync);
        }

        private async Task GenerateAndAssertRandomCreatureOfTemplateAsCharacterWithChallengeRatingAsync()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var randomTemplate = collectionSelector.SelectRandomFrom(templates);

            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var templateChallengeRatings = challengeRatings.Where(cr => creatureVerifier.VerifyCompatibility(true, template: randomTemplate, challengeRating: cr));
            var challengeRating = collectionSelector.SelectRandomFrom(templateChallengeRatings);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomOfTemplateAsCharacterAsync(randomTemplate, challengeRating);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(randomTemplate), creature.Summary);
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), creature.Summary);

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
            var allTypes = types.Union(subtypes);
            var randomType = collectionSelector.SelectRandomFrom(allTypes);

            GenerateAndAssertRandomCreatureOfType(randomType);
        }

        private void GenerateAndAssertRandomCreatureOfType(string type, string challengeRating = null)
        {
            stopwatch.Restart();
            var creature = creatureGenerator.GenerateRandomOfType(type, challengeRating);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            AssertCreatureIsType(creature, type);

            if (challengeRating != null)
            {
                Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), creature.Summary);
            }

            creatureAsserter.AssertCreature(creature);
        }

        private void AssertCreatureIsType(Creature creature, string type)
        {
            var types = CreatureConstants.Types.GetAll();
            if (!types.Contains(type))
            {
                Assert.That(creature.Type.SubTypes, Contains.Item(type), creature.Summary);
                return;
            }

            if (creature.Template == CreatureConstants.Templates.None)
            {
                Assert.That(creature.Type.Name, Is.EqualTo(type), creature.Summary);
                return;
            }

            var allTypes = creature.Type.SubTypes.Union(new[] { creature.Type.Name });
            Assert.That(new[] { type }, Is.SubsetOf(allTypes), creature.Summary);
        }

        [Test]
        public void StressRandomCreatureOfTypeWithChallengeRating()
        {
            stressor.Stress(GenerateAndAssertRandomCreatureOfTypeWithChallengeRating);
        }

        private void GenerateAndAssertRandomCreatureOfTypeWithChallengeRating()
        {
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var allTypes = types.Union(subtypes);
            var randomType = collectionSelector.SelectRandomFrom(allTypes);

            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var typeChallengeRatings = challengeRatings.Where(cr => creatureVerifier.VerifyCompatibility(false, type: randomType, challengeRating: cr));
            var challengeRating = collectionSelector.SelectRandomFrom(typeChallengeRatings);

            GenerateAndAssertRandomCreatureOfType(randomType, challengeRating);
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
            var validTypes = types.Union(subtypes).Except(nonCharacterTypes);
            var randomType = collectionSelector.SelectRandomFrom(validTypes);

            stopwatch.Restart();
            var creature = creatureGenerator.GenerateRandomOfTypeAsCharacter(randomType);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            AssertCreatureIsType(creature, randomType);

            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [Test]
        public void StressRandomCreatureOfTypeAsCharacterWithChallengeRating()
        {
            stressor.Stress(GenerateAndAssertRandomCreatureOfTypeAsCharacterWithChallengeRating);
        }

        private void GenerateAndAssertRandomCreatureOfTypeAsCharacterWithChallengeRating()
        {
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var validTypes = types.Union(subtypes).Except(nonCharacterTypes);
            var randomType = collectionSelector.SelectRandomFrom(validTypes);

            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var typeChallengeRatings = challengeRatings.Where(cr => creatureVerifier.VerifyCompatibility(true, type: randomType, challengeRating: cr));
            var challengeRating = collectionSelector.SelectRandomFrom(typeChallengeRatings);

            stopwatch.Restart();
            var creature = creatureGenerator.GenerateRandomOfTypeAsCharacter(randomType, challengeRating);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), creature.Summary);
            AssertCreatureIsType(creature, randomType);

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
            var allTypes = types.Union(subtypes);
            var randomType = collectionSelector.SelectRandomFrom(allTypes);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomOfTypeAsync(randomType);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            AssertCreatureIsType(creature, randomType);

            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public async Task StressRandomCreatureOfTypeWithChallengeRatingAsync()
        {
            await stressor.StressAsync(GenerateAndAssertRandomCreatureOfTypeWithChallengeRatingAsync);
        }

        private async Task GenerateAndAssertRandomCreatureOfTypeWithChallengeRatingAsync()
        {
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var allTypes = types.Union(subtypes);
            var randomType = collectionSelector.SelectRandomFrom(allTypes);

            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var typeChallengeRatings = challengeRatings.Where(cr => creatureVerifier.VerifyCompatibility(false, type: randomType, challengeRating: cr));
            var challengeRating = collectionSelector.SelectRandomFrom(typeChallengeRatings);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomOfTypeAsync(randomType, challengeRating);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), creature.Summary);
            AssertCreatureIsType(creature, randomType);

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
            var validTypes = types.Union(subtypes).Except(nonCharacterTypes);
            var randomType = collectionSelector.SelectRandomFrom(validTypes);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomOfTypeAsCharacterAsync(randomType);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            AssertCreatureIsType(creature, randomType);

            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [Test]
        public async Task StressRandomCreatureOfTypeAsCharacterWithChallengeRatingAsync()
        {
            await stressor.StressAsync(GenerateAndAssertRandomCreatureOfTypeAsCharacterWithChallengeRatingAsync);
        }

        private async Task GenerateAndAssertRandomCreatureOfTypeAsCharacterWithChallengeRatingAsync()
        {
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var validTypes = types.Union(subtypes).Except(nonCharacterTypes);
            var randomType = collectionSelector.SelectRandomFrom(validTypes);

            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var typeChallengeRatings = challengeRatings.Where(cr => creatureVerifier.VerifyCompatibility(true, type: randomType, challengeRating: cr));
            var challengeRating = collectionSelector.SelectRandomFrom(typeChallengeRatings);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomOfTypeAsCharacterAsync(randomType, challengeRating);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), creature.Summary);
            AssertCreatureIsType(creature, randomType);

            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [Test]
        public void StressRandomCreatureOfChallengeRating()
        {
            stressor.Stress(GenerateAndAssertRandomCreatureOfChallengeRating);
        }

        private void GenerateAndAssertRandomCreatureOfChallengeRating()
        {
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var challengeRating = collectionSelector.SelectRandomFrom(challengeRatings);

            GenerateAndAssertRandomCreatureOfChallengeRating(challengeRating);
        }

        private void GenerateAndAssertRandomCreatureOfChallengeRating(string challengeRating)
        {
            stopwatch.Restart();
            var creature = creatureGenerator.GenerateRandomOfChallengeRating(challengeRating);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), creature.Summary);

            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public void StressRandomCreatureOfChallengeRatingAsCharacter()
        {
            stressor.Stress(GenerateAndAssertRandomCreatureOfChallengeRatingAsCharacter);
        }

        private void GenerateAndAssertRandomCreatureOfChallengeRatingAsCharacter()
        {
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var characterChallengeRatings = challengeRatings.Where(cr => creatureVerifier.VerifyCompatibility(true, challengeRating: cr));
            var challengeRating = collectionSelector.SelectRandomFrom(characterChallengeRatings);

            stopwatch.Restart();
            var creature = creatureGenerator.GenerateRandomOfChallengeRatingAsCharacter(challengeRating);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), creature.Summary);

            creatureAsserter.AssertCreatureAsCharacter(creature);
        }

        [Test]
        public async Task StressRandomCreatureOfChallengeRatingAsync()
        {
            await stressor.StressAsync(GenerateAndAssertRandomCreatureOfChallengeRatingAsync);
        }

        private async Task GenerateAndAssertRandomCreatureOfChallengeRatingAsync()
        {
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var challengeRating = collectionSelector.SelectRandomFrom(challengeRatings);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomOfChallengeRatingAsync(challengeRating);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), creature.Summary);

            creatureAsserter.AssertCreature(creature);
        }

        [Test]
        public async Task StressRandomCreatureOfChallengeRatingAsCharacterAsync()
        {
            await stressor.StressAsync(GenerateAndAssertRandomCreatureOfChallengeRatingAsCharacterAsync);
        }

        private async Task GenerateAndAssertRandomCreatureOfChallengeRatingAsCharacterAsync()
        {
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var characterChallengeRatings = challengeRatings.Where(cr => creatureVerifier.VerifyCompatibility(true, challengeRating: cr));
            var challengeRating = collectionSelector.SelectRandomFrom(characterChallengeRatings);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomOfChallengeRatingAsCharacterAsync(challengeRating);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1), creature.Summary);
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), creature.Summary);

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
        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [Repeat(100)]
        [Ignore("Only use this for debugging")]
        public void BUG_StressSpecificType(string type)
        {
            stressor.Stress(() => GenerateAndAssertRandomCreatureOfType(type));
        }
    }
}