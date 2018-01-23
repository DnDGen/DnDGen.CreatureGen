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
            Assert.That(saves.Constitution, Is.Null);
            Assert.That(saves.Dexterity, Is.Null);
            Assert.That(saves.Wisdom, Is.Null);
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
            saves.Constitution = new Ability(AbilityConstants.Constitution);
            saves.Constitution.BaseScore = 9266;
            saves.FeatFortitudeBonus = 90210;
            saves.RacialFortitudeBonus = 42;

            Assert.That(saves.Fortitude, Is.EqualTo(94880));
        }

        [Test]
        public void FortitudeTotalWithoutAbility()
        {
            saves.Constitution = new Ability(AbilityConstants.Constitution);
            saves.Constitution.BaseScore = 0;
            saves.FeatFortitudeBonus = 90210;
            saves.RacialFortitudeBonus = 42;

            Assert.That(saves.Fortitude, Is.EqualTo(90252));
        }

        [Test]
        public void ReflexTotal()
        {
            saves.Dexterity = new Ability(AbilityConstants.Dexterity);
            saves.Dexterity.BaseScore = 9266;
            saves.FeatReflexBonus = 90210;
            saves.RacialReflexBonus = 42;

            Assert.That(saves.Reflex, Is.EqualTo(94880));
        }

        [Test]
        public void ReflexTotalWithoutAbility()
        {
            saves.Dexterity = new Ability(AbilityConstants.Dexterity);
            saves.Dexterity.BaseScore = 0;
            saves.FeatReflexBonus = 90210;
            saves.RacialReflexBonus = 42;

            Assert.That(saves.Reflex, Is.EqualTo(90252));
        }

        [Test]
        public void WillTotal()
        {
            saves.Wisdom = new Ability(AbilityConstants.Wisdom);
            saves.Wisdom.BaseScore = 9266;
            saves.FeatWillBonus = 90210;
            saves.RacialWillBonus = 42;

            Assert.That(saves.Will, Is.EqualTo(94880));
        }

        [Test]
        public void WillTotalWithoutAbility()
        {
            saves.Wisdom = new Ability(AbilityConstants.Wisdom);
            saves.Wisdom.BaseScore = 0;
            saves.FeatWillBonus = 90210;
            saves.RacialWillBonus = 42;

            Assert.That(saves.Will, Is.EqualTo(90252));
        }
    }
}