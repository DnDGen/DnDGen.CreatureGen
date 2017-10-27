using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public abstract class BaseRaceRandomizerTests : StressTests
    {
        protected abstract IEnumerable<string> allowedBaseRaces { get; }

        protected void AssertBaseRace()
        {
            var prototype = GetCharacterPrototype();

            var baseRace = BaseRaceRandomizer.Randomize(prototype.Alignment, prototype.CharacterClass);
            Assert.That(allowedBaseRaces, Contains.Item(baseRace));
        }
    }
}