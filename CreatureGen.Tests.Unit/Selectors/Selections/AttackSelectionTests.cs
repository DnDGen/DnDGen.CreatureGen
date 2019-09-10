using CreatureGen.Selectors.Helpers;
using CreatureGen.Selectors.Selections;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class AttackSelectionTests
    {
        private AttackSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new AttackSelection();
        }

        [Test]
        public void AttackSelectionIsInitialized()
        {
            Assert.That(selection.DamageRoll, Is.Empty);
            Assert.That(selection.IsMelee, Is.False);
            Assert.That(selection.IsNatural, Is.False);
            Assert.That(selection.IsPrimary, Is.False);
            Assert.That(selection.IsSpecial, Is.False);
            Assert.That(selection.Name, Is.Empty);
        }

        [Test]
        public void AttackSelectionDivider()
        {
            Assert.That(AttackSelection.Divider, Is.EqualTo('@'));
        }

        [Test]
        public void FromData_ReturnsSelection_WithSave()
        {
            var data = AttackHelper.BuildData("name", "damage", "effect", 4.2, "attack type", 9266, "time period", true, true, true, true, "save", "save ability");
            var rawData = AttackHelper.BuildData(data);

            var selection = AttackSelection.From(rawData);
            Assert.That(selection.AttackType, Is.EqualTo("attack type"));
            Assert.That(selection.DamageRoll, Is.EqualTo("damage"));
            Assert.That(selection.DamageEffect, Is.EqualTo("effect"));
            Assert.That(selection.DamageBonusMultiplier, Is.EqualTo(4.2));
            Assert.That(selection.FrequencyQuantity, Is.EqualTo(9266));
            Assert.That(selection.FrequencyTimePeriod, Is.EqualTo("time period"));
            Assert.That(selection.IsMelee, Is.True);
            Assert.That(selection.IsNatural, Is.True);
            Assert.That(selection.IsPrimary, Is.True);
            Assert.That(selection.IsSpecial, Is.True);
            Assert.That(selection.Name, Is.EqualTo("name"));
            Assert.That(selection.Save, Is.EqualTo("save"));
            Assert.That(selection.SaveAbility, Is.EqualTo("save ability"));
        }

        [Test]
        public void FromData_ReturnsSelection_WithoutSave()
        {
            var data = AttackHelper.BuildData("name", "damage", "effect", 4.2, "attack type", 9266, "time period", true, true, true, true, string.Empty, string.Empty);
            var rawData = AttackHelper.BuildData(data);

            var selection = AttackSelection.From(rawData);
            Assert.That(selection.AttackType, Is.EqualTo("attack type"));
            Assert.That(selection.DamageRoll, Is.EqualTo("damage"));
            Assert.That(selection.DamageEffect, Is.EqualTo("effect"));
            Assert.That(selection.DamageBonusMultiplier, Is.EqualTo(4.2));
            Assert.That(selection.FrequencyQuantity, Is.EqualTo(9266));
            Assert.That(selection.FrequencyTimePeriod, Is.EqualTo("time period"));
            Assert.That(selection.IsMelee, Is.True);
            Assert.That(selection.IsNatural, Is.True);
            Assert.That(selection.IsPrimary, Is.True);
            Assert.That(selection.IsSpecial, Is.True);
            Assert.That(selection.Name, Is.EqualTo("name"));
            Assert.That(selection.Save, Is.Empty);
            Assert.That(selection.SaveAbility, Is.Empty);
        }
    }
}
