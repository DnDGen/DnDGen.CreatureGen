using CreatureGen.Creatures;
using CreatureGen.Verifiers;
using Ninject;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace CreatureGen.Tests.Integration.Generators.Verifiers
{
    [TestFixture]
    public class CreatureVerifierTests : IntegrationTests
    {
        [Inject]
        public ICreatureVerifier CreatureVerifier { get; set; }
        [Inject]
        public Stopwatch Stopwatch { get; set; }

        private TimeSpan timeLimit;

        [SetUp]
        public void Setup()
        {
            Stopwatch.Reset();
            timeLimit = new TimeSpan(TimeSpan.TicksPerSecond);
        }

        [TestCase(CreatureConstants.Bison, CreatureConstants.Templates.Ghost, true)]
        [TestCase(CreatureConstants.Genie, CreatureConstants.Templates.Ghost, false)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.None, true)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Vampire, true)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Templates.Ghost, true)]
        public void CreatureVerificationIsFast(string creatureName, string templateName, bool isValid)
        {
            Stopwatch.Restart();
            var verified = CreatureVerifier.VerifyCompatibility(creatureName, templateName);
            Stopwatch.Stop();

            Assert.That(verified, Is.EqualTo(isValid));
            Assert.That(Stopwatch.Elapsed, Is.LessThan(timeLimit));
        }
    }
}
