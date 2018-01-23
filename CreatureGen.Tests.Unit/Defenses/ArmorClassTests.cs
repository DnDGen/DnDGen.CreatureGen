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
            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(ArmorClass.BaseArmorClass));
            Assert.That(armorClass.TotalBonus, Is.EqualTo(ArmorClass.BaseArmorClass));
            Assert.That(armorClass.TouchBonus, Is.EqualTo(ArmorClass.BaseArmorClass));
            Assert.That(armorClass.CircumstantialBonus, Is.False);
            Assert.That(armorClass.Dexterity, Is.Null);
            Assert.That(armorClass.ArmorBonus, Is.Zero);
            Assert.That(armorClass.DeflectionBonus, Is.Zero);
            Assert.That(armorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(armorClass.ShieldBonus, Is.Zero);
            Assert.That(armorClass.SizeModifier, Is.Zero);
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
            armorClass.Dexterity.BaseScore = 9266;
            armorClass.ArmorBonus = 90210;
            armorClass.DeflectionBonus = 42;
            armorClass.NaturalArmorBonus = 1337;
            armorClass.ShieldBonus = 1234;
            armorClass.SizeModifier = 600;

            var total = ArmorClass.BaseArmorClass;
            total += armorClass.Dexterity.Modifier;
            total += armorClass.ArmorBonus;
            total += armorClass.DeflectionBonus;
            total += armorClass.NaturalArmorBonus;
            total += armorClass.ShieldBonus;
            total += armorClass.SizeModifier;

            Assert.That(armorClass.TotalBonus, Is.EqualTo(total));
        }

        [Test]
        public void FullArmorClassMustBePositive()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = -9266;
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
            armorClass.Dexterity.BaseScore = 9266;
            armorClass.ArmorBonus = 90210;
            armorClass.DeflectionBonus = 42;
            armorClass.NaturalArmorBonus = 1337;
            armorClass.ShieldBonus = 1234;
            armorClass.SizeModifier = 600;

            var total = ArmorClass.BaseArmorClass;
            total += armorClass.ArmorBonus;
            total += armorClass.DeflectionBonus;
            total += armorClass.NaturalArmorBonus;
            total += armorClass.ShieldBonus;
            total += armorClass.SizeModifier;

            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(total));
        }

        [Test]
        public void FlatFootedArmorClassMustBePositive()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = -9266;
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
            armorClass.Dexterity.BaseScore = 9266;
            armorClass.ArmorBonus = 90210;
            armorClass.DeflectionBonus = 42;
            armorClass.NaturalArmorBonus = 1337;
            armorClass.ShieldBonus = 1234;
            armorClass.SizeModifier = 600;

            var total = ArmorClass.BaseArmorClass;
            total += armorClass.Dexterity.Modifier;
            total += armorClass.DeflectionBonus;
            total += armorClass.SizeModifier;

            Assert.That(armorClass.TouchBonus, Is.EqualTo(total));
        }

        [Test]
        public void TouchArmorClassMustBePositive()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = -9266;
            armorClass.ArmorBonus = -90210;
            armorClass.DeflectionBonus = -42;
            armorClass.NaturalArmorBonus = -1337;
            armorClass.ShieldBonus = -1234;
            armorClass.SizeModifier = -600;

            Assert.That(armorClass.TouchBonus, Is.EqualTo(1));
        }

        [Test]
        public void FullArmorClassIsEverythingWhenDexterityHasNoScore()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            armorClass.ArmorBonus = 90210;
            armorClass.DeflectionBonus = 42;
            armorClass.NaturalArmorBonus = 1337;
            armorClass.ShieldBonus = 1234;
            armorClass.SizeModifier = 600;

            var total = ArmorClass.BaseArmorClass;
            total += armorClass.ArmorBonus;
            total += armorClass.DeflectionBonus;
            total += armorClass.NaturalArmorBonus;
            total += armorClass.ShieldBonus;
            total += armorClass.SizeModifier;

            Assert.That(armorClass.TotalBonus, Is.EqualTo(total));
        }

        [Test]
        public void FullArmorClassMustBePositiveWhenDexterityHasNoScore()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            armorClass.ArmorBonus = -90210;
            armorClass.DeflectionBonus = -42;
            armorClass.NaturalArmorBonus = -1337;
            armorClass.ShieldBonus = -1234;
            armorClass.SizeModifier = -600;

            Assert.That(armorClass.TotalBonus, Is.EqualTo(1));
        }

        [Test]
        public void FlatFootedArmorClassDoesNotIncludeDodgeOrDexterityWhenDexterityHasNoScore()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            armorClass.ArmorBonus = 90210;
            armorClass.DeflectionBonus = 42;
            armorClass.NaturalArmorBonus = 1337;
            armorClass.ShieldBonus = 1234;
            armorClass.SizeModifier = 600;

            var total = ArmorClass.BaseArmorClass;
            total += armorClass.ArmorBonus;
            total += armorClass.DeflectionBonus;
            total += armorClass.NaturalArmorBonus;
            total += armorClass.ShieldBonus;
            total += armorClass.SizeModifier;

            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(total));
        }

        [Test]
        public void FlatFootedArmorClassMustBePositiveWhenDexterityHasNoScore()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            armorClass.ArmorBonus = -90210;
            armorClass.DeflectionBonus = -42;
            armorClass.NaturalArmorBonus = -1337;
            armorClass.ShieldBonus = -1234;
            armorClass.SizeModifier = -600;

            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(1));
        }

        [Test]
        public void TouchArmorClassDoesNotIncludeArmorOrShieldOrNaturalWhenDexterityHasNoScore()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            armorClass.ArmorBonus = 90210;
            armorClass.DeflectionBonus = 42;
            armorClass.NaturalArmorBonus = 1337;
            armorClass.ShieldBonus = 1234;
            armorClass.SizeModifier = 600;

            var total = ArmorClass.BaseArmorClass;
            total += armorClass.DeflectionBonus;
            total += armorClass.SizeModifier;

            Assert.That(armorClass.TouchBonus, Is.EqualTo(total));
        }

        [Test]
        public void TouchArmorClassMustBePositiveWhenDexterityHasNoScore()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            armorClass.ArmorBonus = -90210;
            armorClass.DeflectionBonus = -42;
            armorClass.NaturalArmorBonus = -1337;
            armorClass.ShieldBonus = -1234;
            armorClass.SizeModifier = -600;

            Assert.That(armorClass.TouchBonus, Is.EqualTo(1));
        }
    }
}