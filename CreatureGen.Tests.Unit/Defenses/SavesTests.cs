using CreatureGen.Abilities;
using CreatureGen.Defenses;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Defenses
{
    [TestFixture]
    public class SavesTests
    {
        private Saves saves;

        [SetUp]
        public void Setup()
        {
            saves = new Saves();
        }

        [Test]
        public void SavesInitialized()
        {
            Assert.That(saves.Fortitude, Is.Zero);
            Assert.That(saves.Reflex, Is.Zero);
            Assert.That(saves.Will, Is.Zero);
            Assert.That(saves.FortitudeAbility, Is.Null);
            Assert.That(saves.ReflexAbility, Is.Null);
            Assert.That(saves.WillAbility, Is.Null);
            Assert.That(saves.FeatFortitudeBonus, Is.Zero);
            Assert.That(saves.FeatReflexBonus, Is.Zero);
            Assert.That(saves.FeatWillBonus, Is.Zero);
            Assert.That(saves.RacialFortitudeBonus, Is.Zero);
            Assert.That(saves.RacialReflexBonus, Is.Zero);
            Assert.That(saves.RacialWillBonus, Is.Zero);
            Assert.That(saves.CircumstantialBonus, Is.False);
        }

        [Test]
        public void FortitudeTotal()
        {
            saves.FortitudeAbility = new Ability(AbilityConstants.Constitution);
            saves.FortitudeAbility.BaseScore = 9266;
            saves.FeatFortitudeBonus = 90210;
            saves.RacialFortitudeBonus = 42;

            Assert.That(saves.Fortitude, Is.EqualTo(94880));
        }

        [Test]
        public void FortitudeTotalWithoutAbility()
        {
            saves.FortitudeAbility = new Ability(AbilityConstants.Constitution);
            saves.FortitudeAbility.BaseScore = 0;
            saves.FeatFortitudeBonus = 90210;
            saves.RacialFortitudeBonus = 42;

            Assert.That(saves.Fortitude, Is.EqualTo(90252));
        }

        [Test]
        public void ReflexTotal()
        {
            saves.ReflexAbility = new Ability(AbilityConstants.Dexterity);
            saves.ReflexAbility.BaseScore = 9266;
            saves.FeatReflexBonus = 90210;
            saves.RacialReflexBonus = 42;

            Assert.That(saves.Reflex, Is.EqualTo(94880));
        }

        [Test]
        public void ReflexTotalWithoutAbility()
        {
            saves.ReflexAbility = new Ability(AbilityConstants.Dexterity);
            saves.ReflexAbility.BaseScore = 0;
            saves.FeatReflexBonus = 90210;
            saves.RacialReflexBonus = 42;

            Assert.That(saves.Reflex, Is.EqualTo(90252));
        }

        [Test]
        public void WillTotal()
        {
            saves.WillAbility = new Ability(AbilityConstants.Wisdom);
            saves.WillAbility.BaseScore = 9266;
            saves.FeatWillBonus = 90210;
            saves.RacialWillBonus = 42;

            Assert.That(saves.Will, Is.EqualTo(94880));
        }

        [Test]
        public void WillTotalWithoutAbility()
        {
            saves.WillAbility = new Ability(AbilityConstants.Wisdom);
            saves.WillAbility.BaseScore = 0;
            saves.FeatWillBonus = 90210;
            saves.RacialWillBonus = 42;

            Assert.That(saves.Will, Is.EqualTo(90252));
        }
    }
}