using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
        private AbilityRandomizerFactory abilityRandomizerFactory;

        [SetUp]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            dice = GetNewInstanceOf<Dice>();
            abilityRandomizerFactory = GetNewInstanceOf<AbilityRandomizerFactory>();

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
            var withMultipleTemplates = withTemplate && dice.Roll().d2().AsTrueOrFalse();
            var withType = dice.Roll().d2().AsTrueOrFalse();
            var withCr = dice.Roll().d2().AsTrueOrFalse();
            var withAlignment = dice.Roll().d2().AsTrueOrFalse();

            string creature = null;
            string template = null;
            string type = null;
            string cr = null;
            string alignment = null;
            List<string> templates = [];

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

            if (template != null)
                templates.Add(template);

            if (withMultipleTemplates)
            {
                var quantity = dice.Roll().d2().AsSum();
                while (quantity-- > 0)
                {
                    var additionalTemplate = collectionSelector.SelectRandomFrom(allTemplates);
                    templates.Add(additionalTemplate);
                }
            }

            var abilityRandomizer = abilityRandomizerFactory.GetAbilityRandomizer([.. templates]);

            ValidateRandomCreatureWithFilters(
                asCharacter,
                creature,
                type,
                cr,
                alignment,
                abilityRandomizer,
                [.. templates]);
        }

        [Test]
        public void BUG_StressProblematicFiltersValidation()
        {
            stressor.Stress(ValidateAndAssertProblematicFilters);
        }

        private void ValidateAndAssertProblematicFilters()
        {
            var abilityRandomizer = abilityRandomizerFactory.GetAbilityRandomizer([]);
            var randomFilters = collectionSelector.SelectRandomFrom(CreatureTestData.ProblematicFilters);
            ValidateRandomCreatureWithFilters(
                randomFilters.AsCharacter,
                null,
                randomFilters.Filters.Type,
                randomFilters.Filters.ChallengeRating,
                randomFilters.Filters.Alignment,
                abilityRandomizer,
                [.. randomFilters.Filters.Templates]);
        }

        private void ValidateRandomCreatureWithFilters(
            bool asCharacter,
            string creature,
            string type,
            string challengeRating,
            string alignment,
            AbilityRandomizer abilityRandomizer,
            params string[] templates)
        {
            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment,
                Templates = [.. templates],
            };

            stopwatch.Restart();
            var verified = creatureVerifier.VerifyCompatibility(asCharacter, creature, abilityRandomizer, filters);
            stopwatch.Stop();

            var failure = new InvalidCreatureException(null, asCharacter, creature, filters, abilityRandomizer);
            Assert.That(stopwatch.Elapsed, Is.LessThan(timeLimit), $"Verified: {verified}\n{failure.Message}");
        }

        [Test]
        public void BUG_StressGhostFiltersValidation()
        {
            stressor.Stress(ValidateAndAssertGhostFilters);
        }

        private void ValidateAndAssertGhostFilters()
        {
            var abilityRandomizer = abilityRandomizerFactory.GetAbilityRandomizer([CreatureConstants.Templates.Ghost]);
            var randomFilters = collectionSelector.SelectRandomFrom(CreatureTestData.ProblematicFilters);
            ValidateRandomCreatureWithFilters(
                randomFilters.AsCharacter,
                null,
                randomFilters.Filters.Type,
                randomFilters.Filters.ChallengeRating,
                randomFilters.Filters.Alignment,
                abilityRandomizer,
                CreatureConstants.Templates.Ghost);
        }
    }
}
