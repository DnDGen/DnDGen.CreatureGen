using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace DnDGen.CreatureGen.Tests.Integration.Stress.Verifiers
{
    [TestFixture]
    public class CreatureVerifierTests : StressTests
    {
        private ICollectionSelector collectionSelector;
        private Stopwatch stopwatch;
        private TimeSpan timeLimit;

        [SetUp]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();

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

            stopwatch.Restart();
            var verified = creatureVerifier.VerifyCompatibility(randomCreatureName, randomTemplate);
            stopwatch.Stop();

            var message = $"Creature: {randomCreatureName}; Template: {randomTemplate}; Verified: {verified}";
            Assert.That(stopwatch.Elapsed, Is.LessThan(timeLimit), message);
        }
    }
}
