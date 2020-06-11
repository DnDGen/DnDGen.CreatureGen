using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Verifiers;
using Ninject;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace DnDGen.CreatureGen.Tests.Integration.Verifiers
{
    [TestFixture]
    public class CreatureVerifierTests : IntegrationTests
    {
        private ICreatureVerifier creatureVerifier;
        private Stopwatch stopwatch;

        private TimeSpan timeLimit;

        [SetUp]
        public void Setup()
        {
            stopwatch = new Stopwatch();
            stopwatch.Reset();

            creatureVerifier = GetNewInstanceOf<ICreatureVerifier>();

            timeLimit = new TimeSpan(TimeSpan.TicksPerSecond);
        }

        [TestCase(CreatureConstants.Bison, CreatureConstants.Templates.Ghost, true)]
        [TestCase(CreatureConstants.Djinni, CreatureConstants.Templates.Ghost, false)]
        [TestCase(CreatureConstants.Djinni, CreatureConstants.Templates.None, true)]
        [TestCase(CreatureConstants.Djinni_Noble, CreatureConstants.Templates.Ghost, false)]
        [TestCase(CreatureConstants.Djinni_Noble, CreatureConstants.Templates.None, true)]
        [TestCase(CreatureConstants.Efreeti, CreatureConstants.Templates.Ghost, false)]
        [TestCase(CreatureConstants.Efreeti, CreatureConstants.Templates.None, true)]
        [TestCase(CreatureConstants.Janni, CreatureConstants.Templates.Ghost, false)]
        [TestCase(CreatureConstants.Janni, CreatureConstants.Templates.None, true)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Ghost, true)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.None, true)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Vampire, true)]
        public void CreatureVerificationIsFast(string creatureName, string templateName, bool isValid)
        {
            stopwatch.Restart();
            var verified = creatureVerifier.VerifyCompatibility(creatureName, templateName);
            stopwatch.Stop();

            Assert.That(verified, Is.EqualTo(isValid));
            Assert.That(stopwatch.Elapsed, Is.LessThan(timeLimit));
        }
    }
}
