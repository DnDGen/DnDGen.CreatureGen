using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System;
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

        [SetUp]
        public void Setup()
        {
            creatureAsserter = GetNewInstanceOf<CreatureAsserter>();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            creatureGenerator = GetNewInstanceOf<ICreatureGenerator>();
            stopwatch = new Stopwatch();
        }

        [TestCase(true, null)]
        [TestCase(true, CreatureConstants.Templates.None)]
        [TestCase(false, null)]
        [TestCase(false, CreatureConstants.Templates.None)]
        public void StressCreature(bool asCharacter, string template)
        {
            stressor.Stress(() => GenerateAndAssertCreature(asCharacter, template));
        }

        private void GenerateAndAssertCreature(bool asCharacter, string template)
        {
            var randomCreature = GetCreatureAndTemplate(asCharacter, template);
            GenerateAndAssertCreature(randomCreature.Creature, randomCreature.Template, asCharacter);
        }

        private (string Creature, string Template) GetCreatureAndTemplate(bool asCharacter, string template)
        {
            if (template == null)
            {
                var validTemplates = allTemplates.Where(t => creatureVerifier.VerifyCompatibility(asCharacter, template: t));

                template = collectionSelector.SelectRandomFrom(validTemplates);
            }

            var validCreatures = allCreatures.Where(c => creatureVerifier.VerifyCompatibility(asCharacter, creature: c, template: template));
            var randomCreatureName = collectionSelector.SelectRandomFrom(validCreatures);

            return (randomCreatureName, template);
        }

        private Creature GenerateAndAssertCreature(string creatureName, string template, bool asCharacter)
        {
            stopwatch.Restart();
            var creature = creatureGenerator.Generate(creatureName, template, asCharacter);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1).Or.LessThan(creature.HitPoints.HitDiceQuantity * 0.1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(creatureName), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(template), creature.Summary);

            if (asCharacter)
                creatureAsserter.AssertCreatureAsCharacter(creature);
            else
                creatureAsserter.AssertCreature(creature);

            return creature;
        }

        [TestCase(true, null)]
        [TestCase(true, CreatureConstants.Templates.None)]
        [TestCase(false, null)]
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
            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateAsync(creatureName, template, asCharacter);
            stopwatch.Stop();

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1).Or.LessThan(creature.HitPoints.HitDiceQuantity * 0.1), creature.Summary);
            Assert.That(creature.Name, Is.EqualTo(creatureName), creature.Summary);
            Assert.That(creature.Template, Is.EqualTo(template), creature.Summary);

            if (asCharacter)
                creatureAsserter.AssertCreatureAsCharacter(creature);
            else
                creatureAsserter.AssertCreature(creature);

            return creature;
        }

        [TestCase(true, true, true, true, true)]
        [TestCase(true, true, true, true, false)]
        [TestCase(true, true, true, false, true)]
        [TestCase(true, true, true, false, false)]
        [TestCase(true, true, false, true, true)]
        [TestCase(true, true, false, true, false)]
        [TestCase(true, true, false, false, true)]
        [TestCase(true, true, false, false, false)]
        [TestCase(true, false, true, true, true)]
        [TestCase(true, false, true, true, false)]
        [TestCase(true, false, true, false, true)]
        [TestCase(true, false, true, false, false)]
        [TestCase(true, false, false, true, true)]
        [TestCase(true, false, false, true, false)]
        [TestCase(true, false, false, false, true)]
        [TestCase(true, false, false, false, false)]
        [TestCase(false, true, true, true, true)]
        [TestCase(false, true, true, true, false)]
        [TestCase(false, true, true, false, true)]
        [TestCase(false, true, true, false, false)]
        [TestCase(false, true, false, true, true)]
        [TestCase(false, true, false, true, false)]
        [TestCase(false, true, false, false, true)]
        [TestCase(false, true, false, false, false)]
        [TestCase(false, false, true, true, true)]
        [TestCase(false, false, true, true, false)]
        [TestCase(false, false, true, false, true)]
        [TestCase(false, false, true, false, false)]
        [TestCase(false, false, false, true, true)]
        [TestCase(false, false, false, true, false)]
        [TestCase(false, false, false, false, true)]
        [TestCase(false, false, false, false, false)]
        public void StressRandomCreature(bool asCharacter, bool setTemplate, bool setType, bool setCr, bool setAlignment)
        {
            stressor.Stress(() => GenerateAndAssertRandomCreature(asCharacter, setTemplate, setType, setCr, setAlignment));
        }

        private void GenerateAndAssertRandomCreature(bool asCharacter, bool setTemplate, bool setType, bool setCr, bool setAlignment)
        {
            var filters = GetRandomFilters(asCharacter, setTemplate, setType, setCr, setAlignment);
            GenerateAndAssertRandomCreature(asCharacter, filters.Template, filters.Type, filters.ChallengeRating, filters.Alignment);
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
                var validTemplates = allTemplates.Where(t => creatureVerifier.VerifyCompatibility(asCharacter, template: t));

                template = collectionSelector.SelectRandomFrom(validTemplates);
            }

            if (setType)
            {
                var types = CreatureConstants.Types.GetAll();
                var subtypes = CreatureConstants.Types.Subtypes.GetAll();
                var allTypes = types.Union(subtypes);
                var validTypes = allTypes.Where(t => creatureVerifier.VerifyCompatibility(asCharacter, template: template, type: t));

                type = collectionSelector.SelectRandomFrom(validTypes);
            }

            if (setCr)
            {
                var challengeRatings = ChallengeRatingConstants.GetOrdered();
                var validChallengeRatings = challengeRatings.Where(c => creatureVerifier.VerifyCompatibility(asCharacter, template: template, type: type, challengeRating: c));

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
                var validAlignments = alignments.Where(a => creatureVerifier.VerifyCompatibility(asCharacter, template: template, type: type, challengeRating: cr, alignment: a));

                alignment = collectionSelector.SelectRandomFrom(validAlignments);
            }

            return (template, type, cr, alignment);
        }

        private Creature GenerateAndAssertRandomCreature(bool asCharacter, string template, string type, string challengeRating, string alignment)
        {
            stopwatch.Restart();
            var creature = creatureGenerator.GenerateRandom(asCharacter, template, type, challengeRating, alignment);
            stopwatch.Stop();

            var message = new StringBuilder();
            var messageTemplate = template == CreatureConstants.Templates.None ? "(None)" : template ?? "Null";

            message.AppendLine($"Creature: {creature.Summary}");
            message.AppendLine($"As Character: {asCharacter}");
            message.AppendLine($"Template: {messageTemplate}");
            message.AppendLine($"Type: {type ?? "Null"}");
            message.AppendLine($"CR: {challengeRating ?? "Null"}");
            message.AppendLine($"Alignment: {alignment ?? "Null"}");

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1).Or.LessThan(creature.HitPoints.HitDiceQuantity * 0.1), message.ToString());

            if (template != null)
                Assert.That(creature.Template, Is.EqualTo(template), message.ToString());

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

            return creature;
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

            if (creature.Template == CreatureConstants.Templates.None)
            {
                Assert.That(creature.Type.Name, Is.EqualTo(type), message);
                return;
            }

            var allTypes = creature.Type.SubTypes.Union(new[] { creature.Type.Name });
            Assert.That(new[] { type }, Is.SubsetOf(allTypes), message);
        }

        [TestCase(true, true, true, true, true)]
        [TestCase(true, true, true, true, false)]
        [TestCase(true, true, true, false, true)]
        [TestCase(true, true, true, false, false)]
        [TestCase(true, true, false, true, true)]
        [TestCase(true, true, false, true, false)]
        [TestCase(true, true, false, false, true)]
        [TestCase(true, true, false, false, false)]
        [TestCase(true, false, true, true, true)]
        [TestCase(true, false, true, true, false)]
        [TestCase(true, false, true, false, true)]
        [TestCase(true, false, true, false, false)]
        [TestCase(true, false, false, true, true)]
        [TestCase(true, false, false, true, false)]
        [TestCase(true, false, false, false, true)]
        [TestCase(true, false, false, false, false)]
        [TestCase(false, true, true, true, true)]
        [TestCase(false, true, true, true, false)]
        [TestCase(false, true, true, false, true)]
        [TestCase(false, true, true, false, false)]
        [TestCase(false, true, false, true, true)]
        [TestCase(false, true, false, true, false)]
        [TestCase(false, true, false, false, true)]
        [TestCase(false, true, false, false, false)]
        [TestCase(false, false, true, true, true)]
        [TestCase(false, false, true, true, false)]
        [TestCase(false, false, true, false, true)]
        [TestCase(false, false, true, false, false)]
        [TestCase(false, false, false, true, true)]
        [TestCase(false, false, false, true, false)]
        [TestCase(false, false, false, false, true)]
        [TestCase(false, false, false, false, false)]
        public async Task StressRandomCreatureAsync(bool asCharacter, bool setTemplate, bool setType, bool setCr, bool setAlignment)
        {
            await stressor.StressAsync(async () => await GenerateAndAssertRandomCreatureAsync(asCharacter, setTemplate, setType, setCr, setAlignment));
        }

        private async Task GenerateAndAssertRandomCreatureAsync(bool asCharacter, bool setTemplate, bool setType, bool setCr, bool setAlignment)
        {
            var filters = GetRandomFilters(asCharacter, setTemplate, setType, setCr, setAlignment);
            await GenerateAndAssertRandomCreatureAsync(asCharacter, filters.Template, filters.Type, filters.ChallengeRating, filters.Alignment);
        }

        private async Task<Creature> GenerateAndAssertRandomCreatureAsync(bool asCharacter, string template, string type, string challengeRating, string alignment)
        {
            stopwatch.Restart();
            var creature = await creatureGenerator.GenerateRandomAsync(asCharacter, template, type, challengeRating, alignment);
            stopwatch.Stop();

            var message = new StringBuilder();
            var messageTemplate = template == CreatureConstants.Templates.None ? "(None)" : template ?? "Null";

            message.AppendLine($"Creature: {creature.Summary}");
            message.AppendLine($"As Character: {asCharacter}");
            message.AppendLine($"Template: {messageTemplate}");
            message.AppendLine($"Type: {type ?? "Null"}");
            message.AppendLine($"CR: {challengeRating ?? "Null"}");
            message.AppendLine($"Alignment: {alignment ?? "Null"}");

            Assert.That(stopwatch.Elapsed.TotalSeconds, Is.LessThan(1).Or.LessThan(creature.HitPoints.HitDiceQuantity * 0.1), message.ToString());

            if (template != null)
                Assert.That(creature.Template, Is.EqualTo(template), message.ToString());

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

            return creature;
        }

        [TestCase(CreatureConstants.Chimera_White, CreatureConstants.Templates.Skeleton, false)]
        [TestCase(CreatureConstants.Dragon_Brass_Young, CreatureConstants.Templates.Ghost, false)]
        [TestCase(CreatureConstants.Dragon_Brass_Young, CreatureConstants.Templates.HalfCelestial, false)]
        [TestCase(CreatureConstants.Dragon_Bronze_GreatWyrm, CreatureConstants.Templates.HalfCelestial, false)]
        [TestCase(CreatureConstants.Dragon_Silver_Ancient, CreatureConstants.Templates.HalfCelestial, false)]
        [TestCase(CreatureConstants.Dragon_White_Old, CreatureConstants.Templates.HalfFiend, false)]
        [TestCase(CreatureConstants.GrayRender, CreatureConstants.Templates.None, true)]
        [TestCase(CreatureConstants.Hieracosphinx, CreatureConstants.Templates.Skeleton, false)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Ghost, false)]
        [TestCase(CreatureConstants.Otyugh, CreatureConstants.Templates.Zombie, false)]
        [TestCase(CreatureConstants.Xill, CreatureConstants.Templates.None, true)]
        [Repeat(100)]
        //[Ignore("Only use this for debugging")]
        public void BUG_StressSpecificCreature(string creatureName, string template, bool asCharacter)
        {
            stressor.Stress(() => GenerateAndAssertCreature(creatureName, template, asCharacter));
        }

        [TestCase(null, true, null, null, null)]
        [TestCase(null, false, CreatureConstants.Templates.Skeleton, null, null)]
        [TestCase(null, false, CreatureConstants.Templates.Zombie, null, null)]
        [TestCase(CreatureConstants.Types.Dragon, false, null, null, null)]
        [TestCase(CreatureConstants.Types.Giant, false, null, null, null)]
        [TestCase(CreatureConstants.Types.Humanoid, false, null, null, null)]
        [TestCase(CreatureConstants.Types.Outsider, false, null, null, null)]
        [TestCase(CreatureConstants.Types.MagicalBeast, false, null, null, null)]
        [TestCase(CreatureConstants.Types.Undead, false, null, null, null)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, false, null, null, null)]
        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal, false, null, null, null)]
        [TestCase(CreatureConstants.Types.Subtypes.Native, false, null, null, null)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger, false, null, null, null)]
        [Repeat(100)]
        //[Ignore("Only use this for debugging")]
        public void BUG_StressSpecificFilters(string type, bool asCharacter, string template, string challengeRating, string alignment)
        {
            stressor.Stress(() => GenerateAndAssertRandomCreature(asCharacter, template, type, challengeRating, alignment));
        }
    }
}