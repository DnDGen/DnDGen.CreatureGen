using CreatureGen.Abilities;
using CreatureGen.Defenses;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Defenses
{
    [TestFixture]
    public class ArmorClassTests
    {
        private ArmorClass armorClass;

        [SetUp]
        public void Setup()
        {
            armorClass = new ArmorClass();
        }

        [Test]
        public void ArmorClassInitialized()
        {
            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(10));
            Assert.That(armorClass.TotalBonus, Is.EqualTo(10));
            Assert.That(armorClass.TouchBonus, Is.EqualTo(10));
            Assert.That(armorClass.CircumstantialBonus, Is.False);
            Assert.That(armorClass.Dexterity, Is.Null);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(0));
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(0));
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(0));
            Assert.That(armorClass.ShieldBonus, Is.EqualTo(0));
            Assert.That(armorClass.SizeModifier, Is.EqualTo(0));
        }

        [Test]
        public void BaseArmorClassIs10()
        {
            Assert.That(ArmorClass.BaseArmorClass, Is.EqualTo(10));
        }

        [Test]
        public void FullArmorClassIsEverything()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseValue = 9266;
            armorClass.ArmorBonus = 90210;
            armorClass.DeflectionBonus = 42;
            armorClass.NaturalArmorBonus = 1337;
            armorClass.ShieldBonus = 1234;
            armorClass.SizeModifier = 600;

            Assert.That(armorClass.TotalBonus, Is.EqualTo(98061));
        }

        [Test]
        public void FullArmorClassMustBePositive()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseValue = -9266;
            armorClass.ArmorBonus = -90210;
            armorClass.DeflectionBonus = -42;
            armorClass.NaturalArmorBonus = -1337;
            armorClass.ShieldBonus = -1234;
            armorClass.SizeModifier = -600;

            Assert.That(armorClass.TotalBonus, Is.EqualTo(1));
        }

        [Test]
        public void FlatFootedArmorClassDoesNotIncludeDodgeOrDexterity()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseValue = 9266;
            armorClass.ArmorBonus = 90210;
            armorClass.DeflectionBonus = 42;
            armorClass.NaturalArmorBonus = 1337;
            armorClass.ShieldBonus = 1234;
            armorClass.SizeModifier = 600;

            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(93433));
        }

        [Test]
        public void FlatFootedArmorClassMustBePositive()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseValue = -9266;
            armorClass.ArmorBonus = -90210;
            armorClass.DeflectionBonus = -42;
            armorClass.NaturalArmorBonus = -1337;
            armorClass.ShieldBonus = -1234;
            armorClass.SizeModifier = -600;

            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(1));
        }

        [Test]
        public void TouchArmorClassDoesNotIncludeArmorOrShieldOrNatural()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseValue = 9266;
            armorClass.ArmorBonus = 90210;
            armorClass.DeflectionBonus = 42;
            armorClass.NaturalArmorBonus = 1337;
            armorClass.ShieldBonus = 1234;
            armorClass.SizeModifier = 600;

            Assert.That(armorClass.TouchBonus, Is.EqualTo(5280));
        }

        [Test]
        public void TouchArmorClassMustBePositive()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseValue = -9266;
            armorClass.ArmorBonus = -90210;
            armorClass.DeflectionBonus = -42;
            armorClass.NaturalArmorBonus = -1337;
            armorClass.ShieldBonus = -1234;
            armorClass.SizeModifier = -600;

            Assert.That(armorClass.TouchBonus, Is.EqualTo(1));
        }
    }
}