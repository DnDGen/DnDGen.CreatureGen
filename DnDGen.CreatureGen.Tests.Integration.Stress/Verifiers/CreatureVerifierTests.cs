using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Stress.Verifiers
{
    [TestFixture]
    public class CreatureVerifierTests : StressTests
    {
        private ICollectionSelector collectionSelector;
        private Stopwatch stopwatch;
        private TimeSpan timeLimit;
        private Dice dice;

        [SetUp]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            dice = GetNewInstanceOf<Dice>();

            timeLimit = TimeSpan.FromSeconds(1);
        }

        [Test]
        public void StressVerification()
        {
            stressor.Stress(ValidateRandomCreatureWithFilters);
        }

        private void ValidateRandomCreatureWithFilters()
        {
            var asCharacter = dice.Roll().d2().AsTrueOrFalse();
            var withCreature = dice.Roll().d2().AsTrueOrFalse();
            var withTemplate = dice.Roll().d2().AsTrueOrFalse();
            var withMultipleTemplates = dice.Roll().d2().AsTrueOrFalse();
            var withType = dice.Roll().d2().AsTrueOrFalse();
            var withCr = dice.Roll().d2().AsTrueOrFalse();
            var withAlignment = dice.Roll().d2().AsTrueOrFalse();

            string creature = null;
            string template = null;
            string type = null;
            string cr = null;
            string alignment = null;

            if (withCreature)
                creature = collectionSelector.SelectRandomFrom(allCreatures);

            if (withTemplate)
                template = collectionSelector.SelectRandomFrom(allTemplates);

            if (withType)
            {
                var types = CreatureConstants.Types.GetAll();
                var subtypes = CreatureConstants.Types.Subtypes.GetAll();
                var allTypes = types.Union(subtypes);
                type = collectionSelector.SelectRandomFrom(allTypes);
            }

            if (withCr)
            {
                var challengeRatings = ChallengeRatingConstants.GetOrdered();
                cr = collectionSelector.SelectRandomFrom(challengeRatings);
            }

            if (withAlignment)
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
                alignment = collectionSelector.SelectRandomFrom(alignments);
            }

            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = cr;
            filters.Alignment = alignment;

            if (template != null)
                filters.Templates.Add(template);

            if (withTemplate && withMultipleTemplates)
            {
                var quantity = dice.Roll().d2().AsSum();
                while (quantity-- > 0)
                {
                    var additionalTemplate = collectionSelector.SelectRandomFrom(allTemplates);
                    filters.Templates.Add(additionalTemplate);
                }
            }

            stopwatch.Restart();
            var verified = creatureVerifier.VerifyCompatibility(asCharacter, creature, filters);
            stopwatch.Stop();

            var failure = new InvalidCreatureException(null, asCharacter, creature, filters);
            Assert.That(stopwatch.Elapsed, Is.LessThan(timeLimit), $"Verified: {verified}\n{failure.Message}");
        }
    }
}
