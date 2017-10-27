using CreatureGen.Randomizers.CharacterClasses;
using CreatureGen.Randomizers.Races;
using Ninject;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.Races.BaseRaces
{
    [TestFixture]
    public class SetBaseRaceRandomizerTests : StressTests
    {
        [Inject]
        public ISetBaseRaceRandomizer SetBaseRaceRandomizer { get; set; }

        [TearDown]
        public void TearDown()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
        }

        [Test]
        public void StressSetBaseRace()
        {
            stressor.Stress(AssertBaseRace);
        }

        protected void AssertBaseRace()
        {
            var prototype = GetCharacterPrototype();
            SetBaseRaceRandomizer.SetBaseRace = BaseRaceRandomizer.Randomize(prototype.Alignment, prototype.CharacterClass);

            var baseRace = SetBaseRaceRandomizer.Randomize(prototype.Alignment, prototype.CharacterClass);
            Assert.That(baseRace, Is.EqualTo(SetBaseRaceRandomizer.SetBaseRace));
        }

        [Test]
        public void StressNPCSetBaseRace()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyNPC);
            stressor.Stress(AssertBaseRace);
        }
    }
}