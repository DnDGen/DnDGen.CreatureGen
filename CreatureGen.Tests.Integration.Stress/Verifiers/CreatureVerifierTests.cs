using DnDGen.Core.Selectors.Collections;
using Ninject;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CreatureGen.Tests.Integration.Stress.Verifiers
{
    [TestFixture]
    public class CreatureVerifierTests : StressTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        public Stopwatch Stopwatch { get; set; }

        private TimeSpan timeLimit;

        [SetUp]
        public void Setup()
        {
            Stopwatch.Reset();
            timeLimit = new TimeSpan(TimeSpan.TicksPerSecond);
        }

        [Test]
        public void StressCreatureVerification()
        {
            stressor.Stress(ValidateRandomCreatureAndTemplate);
        }

        private void ValidateRandomCreatureAndTemplate()
        {
            var randomCreatureName = CollectionSelector.SelectRandomFrom(allCreatures);
            var randomTemplate = CollectionSelector.SelectRandomFrom(allTemplates);

            Stopwatch.Restart();
            var verified = CreatureVerifier.VerifyCompatibility(randomCreatureName, randomTemplate);
            Stopwatch.Stop();

            var message = $"Creature: {randomCreatureName}; Template: {randomTemplate}; Verified: {verified}";
            Assert.That(Stopwatch.Elapsed, Is.LessThan(timeLimit), message);
        }
    }
}
