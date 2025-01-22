using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        private Dice dice;
        private int generationTimeLimitInSeconds;
        private AbilityRandomizerFactory abilityRandomizerFactory;

        [SetUp]
        public void Setup()
        {
            creatureAsserter = GetNewInstanceOf<CreatureAsserter>();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();
            dice = GetNewInstanceOf<Dice>();
            stopwatch = new Stopwatch();
            abilityRandomizerFactory = GetNewInstanceOf<AbilityRandomizerFactory>();

            //INFO: This accounts for how tests and generation may run more slowly when running locally versus in Azure
#if STRESS
            generationTimeLimitInSeconds = 1;
#else
            generationTimeLimitInSeconds = 5;
#endif

        }

        [TestCase(true, null)] //INFO: Pre-random template
        [TestCase(true, CreatureConstants.Templates.None)]
        [TestCase(false, null)] //INFO: Pre-random template
        [TestCase(false, CreatureConstants.Templates.None)]
        public void StressCreature(bool asCharacter, string template)
        {
            stressor.Stress(() => GenerateAndAssertCreature(asCharacter, template));
        }

        private void GenerateAndAssertCreature(bool asCharacter, string template)
        {
            var randomCreature = GetCreatureAndTemplate(asCharacter, template);
            GenerateAndAssertCreature(randomCreature.Creature, asCharacter, false, randomCreature.Template);
        }

        private (string Creature, string Template) GetCreatureAndTemplate(bool asCharacter, string template)
        {
            if (template == null)
            {
                var validTemplates = allTemplates.Where(t => creatureVerifier.VerifyCompatibility(
                    asCharacter,
                    null,
                    new Filters { Templates = [t] }));

                template = collectionSelector.SelectRandomFrom(validTemplates);
            }

            var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(
                asCharacter,
                c,
                new Filters { Templates = [template] }));
            var randomCreatureName = collectionSelector.SelectRandomFrom(validCreatures);

            return (randomCreatureName, template);
        }

        private Creature GenerateAndAssertCreature(string creatureName, bool asCharacter, bool useDefaultAbilities = false, params string[] templates)
        {
            var randomizer = abilityRandomizerFactory.GetAbilityRandomizer(templates);
            if (useDefaultAbilities)
                randomizer.Roll = AbilityConstants.RandomizerRolls.Default;

            stopwatch.Restart();
            var creature = creatureGenerator.Generate(asCharacter, creatureName, randomizer, templates);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(generationTimeLimitInSeconds).Or.LessThan(creature.HitPoints.HitDiceQuantity * 0.1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(creatureName), creature.Summary);
            Assert.That(creature.Templates, Is.EqualTo(templates.Where(t => t != CreatureConstants.Templates.None)), creature.Summary);

            if (asCharacter)
                creatureAsserter.AssertCreatureAsCharacter(creature);
            else
                creatureAsserter.AssertCreature(creature);

            return creature;
        }

        [TestCase(true, null)] //INFO: Pre-random template
        [TestCase(true, CreatureConstants.Templates.None)]
        [TestCase(false, null)] //INFO: Pre-random template
        [TestCase(false, CreatureConstants.Templates.None)]
        public async Task StressCreatureAsync(bool asCharacter, string template)
        {
            await stressor.StressAsync(async () => await GenerateAndAssertCreatureAsync(asCharacter, template));
        }

        private async Task GenerateAndAssertCreatureAsync(bool asCharacter, string template)
        {
            var randomCreature = GetCreatureAndTemplate(asCharacter, template);
            await GenerateAndAssertCreatureAsync(randomCreature.Creature, randomCreature.Template, asCharacter);
        }

        private async Task<Creature> GenerateAndAssertCreatureAsync(string creatureName, string template, bool asCharacter)
        {
            var randomizer = abilityRandomizerFactory.GetAbilityRandomizer([template]);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateAsync(asCharacter, creatureName, randomizer, template);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(generationTimeLimitInSeconds).Or.LessThan(creature.HitPoints.HitDiceQuantity * 0.1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(creatureName), creature.Summary);

            if (template != CreatureConstants.Templates.None)
                Assert.That(creature.Templates, Is.EqualTo(new[] { template }), creature.Summary);

            if (asCharacter)
                creatureAsserter.AssertCreatureAsCharacter(creature);
            else
                creatureAsserter.AssertCreature(creature);

            return creature;
        }

        [Test]
        public void StressRandomCreature()
        {
            stressor.Stress(GenerateAndAssertRandomCreature);
        }

        private void GenerateAndAssertRandomCreature()
        {
            var filters = GetRandomFilters();
            GenerateAndAssertRandomCreature(filters.AsCharacter, filters.Type, filters.ChallengeRating, filters.Alignment, false, filters.Template);
        }

        private (bool AsCharacter, string Template, string Type, string ChallengeRating, string Alignment) GetRandomFilters()
        {
            var asCharacter = dice.Roll().d2().AsTrueOrFalse();
            var setTemplate = dice.Roll().d2().AsTrueOrFalse();
            var setType = dice.Roll().d2().AsTrueOrFalse();
            var setCr = dice.Roll().d2().AsTrueOrFalse();
            var setAlignment = dice.Roll().d2().AsTrueOrFalse();

            var filters = GetRandomFilters(asCharacter, setTemplate, setType, setCr, setAlignment);
            return (asCharacter, filters.Template, filters.Type, filters.ChallengeRating, filters.Alignment);
        }

        private (string Template, string Type, string ChallengeRating, string Alignment) GetRandomFilters(
            bool asCharacter,
            bool setTemplate,
            bool setType,
            bool setCr,
            bool setAlignment)
        {
            string template = null;
            string type = null;
            string cr = null;
            string alignment = null;

            if (setTemplate)
            {
                var validTemplates = allTemplates.Where(t => creatureVerifier.VerifyCompatibility(
                    asCharacter,
                    null,
                    new Filters { Templates = new List<string> { t } }));

                template = collectionSelector.SelectRandomFrom(validTemplates);
            }

            if (setType)
            {
                var types = CreatureConstants.Types.GetAll();
                var subtypes = CreatureConstants.Types.Subtypes.GetAll();
                var allTypes = types.Union(subtypes);
                var validTypes = allTypes.Where(t => creatureVerifier.VerifyCompatibility(
                    asCharacter,
                    null,
                    new Filters { Templates = new List<string> { template }, Type = t }));

                type = collectionSelector.SelectRandomFrom(validTypes);
            }

            if (setCr)
            {
                var challengeRatings = ChallengeRatingConstants.GetOrdered();
                var validChallengeRatings = challengeRatings
                    .Where(c => creatureVerifier.VerifyCompatibility(
                        asCharacter,
                        null,
                        new Filters { Templates = new List<string> { template }, Type = type, ChallengeRating = c }));

                cr = collectionSelector.SelectRandomFrom(validChallengeRatings);
            }

            if (setAlignment)
            {
                var alignments = new[]
                {
                    AlignmentConstants.LawfulGood,
                    AlignmentConstants.NeutralGood,
                    AlignmentConstants.ChaoticGood,
                    AlignmentConstants.LawfulNeutral,
                    AlignmentConstants.TrueNeutral,
                    AlignmentConstants.ChaoticNeutral,
                    AlignmentConstants.LawfulEvil,
                    AlignmentConstants.NeutralEvil,
                    AlignmentConstants.ChaoticEvil,
                };
                var validAlignments = alignments
                    .Where(a => creatureVerifier.VerifyCompatibility(
                        asCharacter,
                        null,
                        new Filters { Templates = [template], Type = type, ChallengeRating = cr, Alignment = a }));

                alignment = collectionSelector.SelectRandomFrom(validAlignments);
            }

            return (template, type, cr, alignment);
        }

        private Creature GenerateAndAssertRandomCreature(
            bool asCharacter,
            string type,
            string challengeRating,
            string alignment,
            bool useDefaultAbilities = false,
            params string[] templates)
        {
            var filters = new Filters();
            filters.Templates.AddRange(templates);
            filters.Type = type;
            filters.ChallengeRating = challengeRating;
            filters.Alignment = alignment;

            var randomizer = abilityRandomizerFactory.GetAbilityRandomizer(templates);
            if (useDefaultAbilities)
                randomizer.Roll = AbilityConstants.RandomizerRolls.Default;

            stopwatch.Restart();
            var creature = creatureGenerator.GenerateRandom(asCharacter, randomizer, filters);
            stopwatch.Stop();

            AssertRandomCreature(creature, asCharacter, type, challengeRating, alignment, templates);

            return creature;
        }

        private void AssertRandomCreature(Creature creature, bool asCharacter, string type, string challengeRating, string alignment, params string[] templates)
        {
            var message = new StringBuilder();
            var joinedTemplates = string.Join(", ", templates);
            var messageTemplate = templates.Any() ? (!string.IsNullOrEmpty(joinedTemplates) ? joinedTemplates : "(None)") : "Null";

            message.AppendLine($"Creature: {creature.Summary}");
            message.AppendLine($"As Character: {asCharacter}");
            message.AppendLine($"Template: {messageTemplate}");
            message.AppendLine($"Type: {type ?? "Null"}");
            message.AppendLine($"CR: {challengeRating ?? "Null"}");
            message.AppendLine($"Alignment: {alignment ?? "Null"}");

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(generationTimeLimitInSeconds).Or.LessThan(creature.HitPoints.HitDiceQuantity * 0.1), message.ToString());

            if (templates.Any(t => !string.IsNullOrEmpty(t)))
                Assert.That(creature.Templates, Is.EqualTo(templates.Where(t => t != CreatureConstants.Templates.None)), message.ToString());

            if (type != null)
                creatureAsserter.AssertCreatureIsType(creature, type, message.ToString());

            if (challengeRating != null)
                Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), message.ToString());

            if (alignment != null)
                Assert.That(creature.Alignment.Full, Is.EqualTo(alignment), message.ToString());

            if (asCharacter)
                creatureAsserter.AssertCreatureAsCharacter(creature, message.ToString());
            else
                creatureAsserter.AssertCreature(creature, message.ToString());
        }

        [Test]
        public async Task StressRandomCreatureAsync()
        {
            await stressor.StressAsync(GenerateAndAssertRandomCreatureAsync);
        }

        private async Task GenerateAndAssertRandomCreatureAsync()
        {
            var filters = GetRandomFilters();
            await GenerateAndAssertRandomCreatureAsync(filters.AsCharacter, filters.Template, filters.Type, filters.ChallengeRating, filters.Alignment);
        }

        private async Task<Creature> GenerateAndAssertRandomCreatureAsync(bool asCharacter, string template, string type, string challengeRating, string alignment)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment
            };

            if (template != null)
                filters.Templates.Add(template);

            var randomizer = abilityRandomizerFactory.GetAbilityRandomizer([template]);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, randomizer, filters);
            stopwatch.Stop();

            AssertRandomCreature(creature, asCharacter, type, challengeRating, alignment, template);

            return creature;
        }

        [Test]
        public void BUG_StressProblematicCreature()
        {
            stressor.Stress(GenerateAndAssertProblematicCreature);
        }

        private void GenerateAndAssertProblematicCreature()
        {
            var randomCreature = collectionSelector.SelectRandomFrom(CreatureTestData.ProblematicCreatures);
            GenerateAndAssertCreature(randomCreature.Creature, randomCreature.AsCharacter, true, randomCreature.Templates);
        }

        [Test]
        public void BUG_StressProblematicFilters()
        {
            stressor.Stress(GenerateAndAssertProblematicFilters);
        }

        private void GenerateAndAssertProblematicFilters()
        {
            var randomFilters = collectionSelector.SelectRandomFrom(CreatureTestData.ProblematicFilters);
            GenerateAndAssertRandomCreature(
                randomFilters.AsCharacter,
                randomFilters.Filters.Type,
                randomFilters.Filters.ChallengeRating,
                randomFilters.Filters.Alignment,
                true,
                randomFilters.Filters.Templates.ToArray());
        }
    }
}