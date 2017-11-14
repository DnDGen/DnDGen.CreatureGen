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
            Assert.That(saves.Fortitude, Is.EqualTo(0));
            Assert.That(saves.Reflex, Is.EqualTo(0));
            Assert.That(saves.Will, Is.EqualTo(0));
            Assert.That(saves.Constitution, Is.Null);
            Assert.That(saves.Dexterity, Is.Null);
            Assert.That(saves.Wisdom, Is.Null);
            Assert.That(saves.FeatFortitudeBonus, Is.EqualTo(0));
            Assert.That(saves.FeatReflexBonus, Is.EqualTo(0));
            Assert.That(saves.FeatWillBonus, Is.EqualTo(0));
            Assert.That(saves.RacialFortitudeBonus, Is.EqualTo(0));
            Assert.That(saves.RacialReflexBonus, Is.EqualTo(0));
            Assert.That(saves.RacialWillBonus, Is.EqualTo(0));
            Assert.That(saves.CircumstantialBonus, Is.False);
        }

        [Test]
        public void FortitudeTotal()
        {
            saves.Constitution = new Ability(AbilityConstants.Constitution);
            saves.Constitution.BaseValue = 9266;
            saves.FeatFortitudeBonus = 90210;
            saves.RacialFortitudeBonus = 42;

            Assert.That(saves.Fortitude, Is.EqualTo(94880));
        }

        [Test]
        public void FortitudeTotalWithoutAbility()
        {
            saves.FeatFortitudeBonus = 90210;
            saves.RacialFortitudeBonus = 42;

            Assert.That(saves.Fortitude, Is.EqualTo(90252));
        }

        [Test]
        public void ReflexTotal()
        {
            saves.Dexterity = new Ability(AbilityConstants.Dexterity);
            saves.Dexterity.BaseValue = 9266;
            saves.FeatReflexBonus = 90210;
            saves.RacialReflexBonus = 42;

            Assert.That(saves.Reflex, Is.EqualTo(94880));
        }

        [Test]
        public void ReflexTotalWithoutAbility()
        {
            saves.FeatReflexBonus = 90210;
            saves.RacialReflexBonus = 42;

            Assert.That(saves.Reflex, Is.EqualTo(90252));
        }

        [Test]
        public void WillTotal()
        {
            saves.Wisdom = new Ability(AbilityConstants.Wisdom);
            saves.Wisdom.BaseValue = 9266;
            saves.FeatWillBonus = 90210;
            saves.RacialWillBonus = 42;

            Assert.That(saves.Will, Is.EqualTo(94880));
        }

        [Test]
        public void WillTotalWithoutAbility()
        {
            saves.FeatWillBonus = 90210;
            saves.RacialWillBonus = 42;

            Assert.That(saves.Will, Is.EqualTo(90252));
        }
    }
}