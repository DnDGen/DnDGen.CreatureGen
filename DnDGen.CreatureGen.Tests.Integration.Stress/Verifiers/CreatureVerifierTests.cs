using DnDGen.CreatureGen.Creatures;
using DnDGen.Infrastructure.Selectors.Collections;
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
        private Random random;

        [SetUp]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            random = new Random();

            timeLimit = TimeSpan.FromSeconds(1);
        }

        [Test]
        public void StressCreatureVerification()
        {
            stressor.Stress(ValidateRandomCreatureAndTemplate);
        }

        private void ValidateRandomCreatureAndTemplate()
        {
            var randomCreatureName = collectionSelector.SelectRandomFrom(allCreatures);
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates);
            var asCharacter = Convert.ToBoolean(random.Next(2));

            stopwatch.Restart();
            var verified = creatureVerifier.VerifyCompatibility(asCharacter, randomCreatureName, randomTemplate);
            stopwatch.Stop();

            var message = $"As Character: {asCharacter}; Creature: {randomCreatureName}; Template: {randomTemplate}; Verified: {verified}";
            Assert.That(stopwatch.Elapsed, Is.LessThan(timeLimit), message);
        }

        [Test]
        public void StressTemplateVerification()
        {
            stressor.Stress(ValidateRandomTemplateAndChallengeRating);
        }

        private void ValidateRandomTemplateAndChallengeRating()
        {
            var randomTemplate = collectionSelector.SelectRandomFrom(allTemplates);
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var asCharacter = Convert.ToBoolean(random.Next(2));
            var withCr = Convert.ToBoolean(random.Next(2));
            string cr = null;

            if (withCr)
                cr = collectionSelector.SelectRandomFrom(challengeRatings);

            stopwatch.Restart();
            var verified = creatureVerifier.VerifyCompatibility(asCharacter, template: randomTemplate, challengeRating: cr);
            stopwatch.Stop();

            var message = $"As Character: {asCharacter}; Template: {randomTemplate}; CR: {cr ?? "None"}; Verified: {verified}";
            Assert.That(stopwatch.Elapsed, Is.LessThan(timeLimit), message);
        }

        [Test]
        public void StressTypeVerification()
        {
            stressor.Stress(ValidateRandomTypeAndChallengeRating);
        }

        private void ValidateRandomTypeAndChallengeRating()
        {
            var types = CreatureConstants.Types.GetAll().Union(CreatureConstants.Types.Subtypes.GetAll());
            var randomType = collectionSelector.SelectRandomFrom(types);
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var asCharacter = Convert.ToBoolean(random.Next(2));
            var withCr = Convert.ToBoolean(random.Next(2));
            string cr = null;

            if (withCr)
                cr = collectionSelector.SelectRandomFrom(challengeRatings);

            stopwatch.Restart();
            var verified = creatureVerifier.VerifyCompatibility(asCharacter, type: randomType, challengeRating: cr);
            stopwatch.Stop();

            var message = $"As Character: {asCharacter}; Type: {randomType}; CR: {cr ?? "None"}; Verified: {verified}";
            Assert.That(stopwatch.Elapsed, Is.LessThan(timeLimit), message);
        }

        [Test]
        public void StressChallengeRatingVerification()
        {
            stressor.Stress(ValidateRandomChallengeRating);
        }

        private void ValidateRandomChallengeRating()
        {
            var challengeRatings = ChallengeRatingConstants.GetOrdered();
            var cr = collectionSelector.SelectRandomFrom(challengeRatings);
            var asCharacter = Convert.ToBoolean(random.Next(2));

            stopwatch.Restart();
            var verified = creatureVerifier.VerifyCompatibility(asCharacter, challengeRating: cr);
            stopwatch.Stop();

            var message = $"As Character: {asCharacter}; CR: {cr}; Verified: {verified}";
            Assert.That(stopwatch.Elapsed, Is.LessThan(timeLimit), message);
        }
    }
}
