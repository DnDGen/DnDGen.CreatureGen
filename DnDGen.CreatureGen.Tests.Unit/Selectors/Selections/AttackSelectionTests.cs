using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Selectors.Selections;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class AttackSelectionTests
    {
        private AttackSelection selection;
        private AttackHelper attackHelper;
        private DamageHelper damageHelper;

        [SetUp]
        public void Setup()
        {
            selection = new AttackSelection();
            attackHelper = new AttackHelper();
            damageHelper = new DamageHelper();
        }

        [Test]
        public void AttackSelectionIsInitialized()
        {
            Assert.That(selection.Damages, Is.Empty);
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
        public void AttackSelectionDamageDivider()
        {
            Assert.That(AttackSelection.DamageDivider, Is.EqualTo('#'));
        }

        [Test]
        public void AttackSelectionDamageSplitDivider()
        {
            Assert.That(AttackSelection.DamageSplitDivider, Is.EqualTo(','));
        }

        [Test]
        public void FromData_ReturnsSelection_WithNoDamage()
        {
            var data = attackHelper.BuildData("name", string.Empty, "effect", 4.2, "attack type", 9266, "time period", true, true, true, true, string.Empty, string.Empty);
            var rawData = attackHelper.BuildEntry(data);

            var selection = AttackSelection.From(rawData);
            Assert.That(selection.AttackType, Is.EqualTo("attack type"));
            Assert.That(selection.Damages, Is.Empty);
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
            Assert.That(selection.SaveDcBonus, Is.Zero);
        }

        [Test]
        public void FromData_ReturnsSelection_WithDamage()
        {
            var damageData = damageHelper.BuildData("my roll", "my damage type");
            var damageEntry = damageHelper.BuildEntry(damageData);

            var data = attackHelper.BuildData("name", damageEntry, "effect", 4.2, "attack type", 9266, "time period", true, true, true, true, string.Empty, string.Empty);
            var rawData = attackHelper.BuildEntry(data);

            var selection = AttackSelection.From(rawData);
            Assert.That(selection.AttackType, Is.EqualTo("attack type"));
            Assert.That(selection.Damages, Has.Count.EqualTo(1));
            Assert.That(selection.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(selection.Damages[0].Type, Is.EqualTo("my damage type"));
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
            Assert.That(selection.SaveDcBonus, Is.Zero);
        }

        [Test]
        public void FromData_ReturnsSelection_WithMultipleDamages()
        {
            var damageData1 = damageHelper.BuildData("my roll", "my damage type");
            var damageEntry1 = damageHelper.BuildEntry(damageData1);

            var damageData2 = damageHelper.BuildData("my other roll", "my other damage type");
            var damageEntry2 = damageHelper.BuildEntry(damageData2);

            var damageEntry = string.Join(AttackSelection.DamageSplitDivider, damageEntry1, damageEntry2);

            var data = attackHelper.BuildData("name", damageEntry, "effect", 4.2, "attack type", 9266, "time period", true, true, true, true, string.Empty, string.Empty);
            var rawData = attackHelper.BuildEntry(data);

            var selection = AttackSelection.From(rawData);
            Assert.That(selection.AttackType, Is.EqualTo("attack type"));
            Assert.That(selection.Damages, Has.Count.EqualTo(2));
            Assert.That(selection.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(selection.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(selection.Damages[1].Roll, Is.EqualTo("my other roll"));
            Assert.That(selection.Damages[1].Type, Is.EqualTo("my other damage type"));
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
            Assert.That(selection.SaveDcBonus, Is.Zero);
        }

        [Test]
        public void FromData_ReturnsSelection_WithSave()
        {
            var damageData = damageHelper.BuildData("my roll", "my damage type");
            var damageEntry = damageHelper.BuildEntry(damageData);

            var data = attackHelper.BuildData("name", damageEntry, "effect", 4.2, "attack type", 9266, "time period", true, true, true, true, "save", "save ability", 90210);
            var rawData = attackHelper.BuildEntry(data);

            var selection = AttackSelection.From(rawData);
            Assert.That(selection.AttackType, Is.EqualTo("attack type"));
            Assert.That(selection.Damages, Has.Count.EqualTo(1));
            Assert.That(selection.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(selection.Damages[0].Type, Is.EqualTo("my damage type"));
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
            Assert.That(selection.SaveDcBonus, Is.EqualTo(90210));
        }

        [Test]
        public void FromData_ReturnsSelection_WithoutSave()
        {
            var damageData = damageHelper.BuildData("my roll", "my damage type");
            var damageEntry = damageHelper.BuildEntry(damageData);

            var data = attackHelper.BuildData("name", damageEntry, "effect", 4.2, "attack type", 9266, "time period", true, true, true, true, string.Empty, string.Empty);
            var rawData = attackHelper.BuildEntry(data);

            var selection = AttackSelection.From(rawData);
            Assert.That(selection.AttackType, Is.EqualTo("attack type"));
            Assert.That(selection.Damages, Has.Count.EqualTo(1));
            Assert.That(selection.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(selection.Damages[0].Type, Is.EqualTo("my damage type"));
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
            Assert.That(selection.SaveDcBonus, Is.Zero);
        }
    }
}
