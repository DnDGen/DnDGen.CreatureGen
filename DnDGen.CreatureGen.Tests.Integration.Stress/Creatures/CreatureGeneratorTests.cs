using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using NUnit.Framework;
using System;
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
        private Dictionary<string, (string Ability, int Minimum)> templateAbilityMinimums;

        [SetUp]
        public void Setup()
        {
            creatureAsserter = GetNewInstanceOf<CreatureAsserter>();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();
            dice = GetNewInstanceOf<Dice>();
            stopwatch = new Stopwatch();

            templateAbilityMinimums = new Dictionary<string, (string Ability, int Minimum)>();
            templateAbilityMinimums[CreatureConstants.Templates.Ghost] = (AbilityConstants.Charisma, 6);
            templateAbilityMinimums[CreatureConstants.Templates.HalfCelestial] = (AbilityConstants.Intelligence, 4);
            templateAbilityMinimums[CreatureConstants.Templates.HalfFiend] = (AbilityConstants.Intelligence, 4);
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
                    new Filters { Templates = new List<string> { t } }));

                template = collectionSelector.SelectRandomFrom(validTemplates);
            }

            var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(
                asCharacter,
                c,
                new Filters { Templates = new List<string> { template } }));
            var randomCreatureName = collectionSelector.SelectRandomFrom(validCreatures);

            return (randomCreatureName, template);
        }

        private AbilityRandomizer GetAbilityRandomizer(params string[] templates)
        {
            var rolls = new[]
            {
                AbilityConstants.RandomizerRolls.Best,
                AbilityConstants.RandomizerRolls.BestOfFour,
                AbilityConstants.RandomizerRolls.Default,
                AbilityConstants.RandomizerRolls.Good,
                AbilityConstants.RandomizerRolls.OnesAsSixes,
                AbilityConstants.RandomizerRolls.Poor,
                AbilityConstants.RandomizerRolls.Raw,
                AbilityConstants.RandomizerRolls.Wild,
            };

            var randomizer = new AbilityRandomizer();
            randomizer.Roll = collectionSelector.SelectRandomFrom(rolls);

            //HACK: This is just to avoid the issue when a randomly-rolled ability
            //(especially with "Poor" or "Wild") ends up much lower than normally would be with the "Default" roll,
            //and the template requires an ability to be a minimum value
            if (templates.Any(t => t != null && templateAbilityMinimums.ContainsKey(t)))
            {
                foreach (var template in templates.Where(t => templateAbilityMinimums.ContainsKey(t)))
                {
                    var newMin = Math.Max(randomizer.AbilityAdvancements[templateAbilityMinimums[template].Ability], templateAbilityMinimums[template].Minimum);
                    randomizer.AbilityAdvancements[templateAbilityMinimums[template].Ability] = newMin;
                    randomizer.PriorityAbility = templateAbilityMinimums[template].Ability;
                }
            }
            else if (templates.Contains(null))
            {
                //HACK: Here, the template might be randomly selected, so we have to guard against it for the sake of stress testing
                foreach (var kvp in templateAbilityMinimums)
                {
                    if (dice.Roll(randomizer.Roll).AsPotentialMinimum() < kvp.Value.Minimum)
                    {
                        randomizer.AbilityAdvancements[kvp.Value.Ability] = kvp.Value.Minimum;
                    }
                }
            }

            return randomizer;
        }

        private Creature GenerateAndAssertCreature(string creatureName, bool asCharacter, bool useDefaultAbilities = false, params string[] templates)
        {
            var randomizer = GetAbilityRandomizer(templates);
            if (useDefaultAbilities)
                randomizer.Roll = AbilityConstants.RandomizerRolls.Default;

            stopwatch.Restart();
            var creature = creatureGenerator.Generate(asCharacter, creatureName, randomizer, templates);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1).Or.LessThan(creature.HitPoints.HitDiceQuantity * 0.1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(creatureName), creature.Summary);
            Assert.That(creature.Templates, Is.EqualTo(templates), creature.Summary);

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
            var randomizer = GetAbilityRandomizer(template);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateAsync(asCharacter, creatureName, randomizer, template);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1).Or.LessThan(creature.HitPoints.HitDiceQuantity * 0.1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(creatureName), creature.Summary);
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
                        new Filters { Templates = new List<string> { template }, Type = type, ChallengeRating = cr, Alignment = a }));

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

            var randomizer = GetAbilityRandomizer(templates.FirstOrDefault());
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

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1).Or.LessThan(creature.HitPoints.HitDiceQuantity * 0.1), message.ToString());

            Assert.That(creature.Templates, Is.EqualTo(templates), message.ToString());

            if (type != null)
                AssertCreatureIsType(creature, type, message.ToString());

            if (challengeRating != null)
                Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating), message.ToString());

            if (alignment != null)
                Assert.That(creature.Alignment.Full, Is.EqualTo(alignment), message.ToString());

            if (asCharacter)
                creatureAsserter.AssertCreatureAsCharacter(creature, message.ToString());
            else
                creatureAsserter.AssertCreature(creature, message.ToString());
        }

        private void AssertCreatureIsType(Creature creature, string type, string message = null)
        {
            message ??= creature.Summary;

            var types = CreatureConstants.Types.GetAll();
            if (!types.Contains(type))
            {
                Assert.That(creature.Type.SubTypes, Contains.Item(type), message);
                return;
            }

            if (!creature.Templates.Any())
            {
                Assert.That(creature.Type.Name, Is.EqualTo(type), message);
                return;
            }

            var allTypes = creature.Type.SubTypes.Union(new[] { creature.Type.Name });
            Assert.That(new[] { type }, Is.SubsetOf(allTypes), message);
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
            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = challengeRating;
            filters.Alignment = alignment;

            if (template != null)
                filters.Templates.Add(template);

            var randomizer = GetAbilityRandomizer(template);

            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, randomizer, filters);
            stopwatch.Stop();

            AssertRandomCreature(creature, asCharacter, type, challengeRating, alignment, template);

            return creature;
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.ProblematicCreaturesTestCases))]
        [Repeat(100)]
        [Ignore("Only use this for debugging")]
        public void BUG_StressSpecificCreature(bool asCharacter, string creatureName, params string[] templates)
        {
            stressor.Stress(() => GenerateAndAssertCreature(creatureName, asCharacter, false, templates));
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.ProblematicFiltersTestCases))]
        [Repeat(100)]
        [Ignore("Only use this for debugging")]
        public void BUG_StressSpecificFilters(string type, bool asCharacter, string template, string challengeRating, string alignment)
        {
            stressor.Stress(() => GenerateAndAssertRandomCreature(asCharacter, type, challengeRating, alignment, false, template));
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