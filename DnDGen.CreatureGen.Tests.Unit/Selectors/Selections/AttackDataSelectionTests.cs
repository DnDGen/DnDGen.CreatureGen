using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class AttackDataSelectionTests
    {
        private AttackDataSelection selection;
        private AttackHelper attackHelper;
        private DamageHelper damageHelper;

        [SetUp]
        public void Setup()
        {
            selection = new AttackDataSelection();
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
        public void SectionCountIs10()
        {
            Assert.That(selection.SectionCount, Is.EqualTo(10));
        }

        [Test]
        public void Map_FromString_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.AdditionalHitDiceRoll] = "9266d90210";
            data[DataIndexConstants.AttackData.ChallengeRatingDivisor] = "42";
            data[DataIndexConstants.AttackData.ConstitutionAdjustment] = "600";
            data[DataIndexConstants.AttackData.DexterityAdjustment] = "1337";
            data[DataIndexConstants.AttackData.NaturalArmorAdjustment] = "1336";
            data[DataIndexConstants.AttackData.Reach] = "9.6";
            data[DataIndexConstants.AttackData.Size] = "enormous";
            data[DataIndexConstants.AttackData.Space] = "78.3";
            data[DataIndexConstants.AttackData.StrengthAdjustment] = "8245";
            data[DataIndexConstants.AttackData.AdjustedChallengeRating] = "adjusted cr";

            var newSelection = AdvancementDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.AdditionalHitDiceRoll, Is.EqualTo("9266d90210"));
            Assert.That(newSelection.ChallengeRatingDivisor, Is.EqualTo(42));
            Assert.That(newSelection.ConstitutionAdjustment, Is.EqualTo(600));
            Assert.That(newSelection.DexterityAdjustment, Is.EqualTo(1337));
            Assert.That(newSelection.NaturalArmorAdjustment, Is.EqualTo(1336));
            Assert.That(newSelection.Reach, Is.EqualTo(9.6));
            Assert.That(newSelection.Size, Is.EqualTo("enourmous"));
            Assert.That(newSelection.Space, Is.EqualTo(78.3));
            Assert.That(newSelection.StrengthAdjustment, Is.EqualTo(8245));
            Assert.That(newSelection.AdjustedChallengeRating, Is.EqualTo("adjusted cr"));
        }

        [Test]
        public void Map_FromSelection_ReturnsString()
        {
            var selection = new AdvancementDataSelection
            {
                AdditionalHitDiceRoll = "9266d90210",
                ChallengeRatingDivisor = 42,
                ConstitutionAdjustment = 600,
                DexterityAdjustment = 1337,
                NaturalArmorAdjustment = 1336,
                Reach = 9.6,
                Size = "enormous",
                Space = 78.3,
                StrengthAdjustment = 8245,
                AdjustedChallengeRating = "adjusted cr",
            };

            var rawData = AdvancementDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.AttackData.AdditionalHitDiceRoll], Is.EqualTo("9266d90210"));
            Assert.That(rawData[DataIndexConstants.AttackData.ChallengeRatingDivisor], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.AttackData.ConstitutionAdjustment], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.AttackData.DexterityAdjustment], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.AttackData.NaturalArmorAdjustment], Is.EqualTo("1336"));
            Assert.That(rawData[DataIndexConstants.AttackData.Reach], Is.EqualTo("9.6"));
            Assert.That(rawData[DataIndexConstants.AttackData.Size], Is.EqualTo("enormous"));
            Assert.That(rawData[DataIndexConstants.AttackData.Space], Is.EqualTo("78.3"));
            Assert.That(rawData[DataIndexConstants.AttackData.StrengthAdjustment], Is.EqualTo("8245"));
            Assert.That(rawData[DataIndexConstants.AttackData.AdjustedChallengeRating], Is.EqualTo("adjusted cr"));
        }

        [Test]
        public void MapTo_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AttackData.AdditionalHitDiceRoll] = "9266d90210";
            data[DataIndexConstants.AttackData.ChallengeRatingDivisor] = "42";
            data[DataIndexConstants.AttackData.ConstitutionAdjustment] = "600";
            data[DataIndexConstants.AttackData.DexterityAdjustment] = "1337";
            data[DataIndexConstants.AttackData.NaturalArmorAdjustment] = "1336";
            data[DataIndexConstants.AttackData.Reach] = "9.6";
            data[DataIndexConstants.AttackData.Size] = "enormous";
            data[DataIndexConstants.AttackData.Space] = "78.3";
            data[DataIndexConstants.AttackData.StrengthAdjustment] = "8245";
            data[DataIndexConstants.AttackData.AdjustedChallengeRating] = "adjusted cr";

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.AdditionalHitDiceRoll, Is.EqualTo("9266d90210"));
            Assert.That(newSelection.ChallengeRatingDivisor, Is.EqualTo(42));
            Assert.That(newSelection.ConstitutionAdjustment, Is.EqualTo(600));
            Assert.That(newSelection.DexterityAdjustment, Is.EqualTo(1337));
            Assert.That(newSelection.NaturalArmorAdjustment, Is.EqualTo(1336));
            Assert.That(newSelection.Reach, Is.EqualTo(9.6));
            Assert.That(newSelection.Size, Is.EqualTo("enourmous"));
            Assert.That(newSelection.Space, Is.EqualTo(78.3));
            Assert.That(newSelection.StrengthAdjustment, Is.EqualTo(8245));
            Assert.That(newSelection.AdjustedChallengeRating, Is.EqualTo("adjusted cr"));
        }

        [Test]
        public void MapFrom_ReturnsString()
        {
            var selection = new AdvancementDataSelection
            {
                AdditionalHitDiceRoll = "9266d90210",
                ChallengeRatingDivisor = 42,
                ConstitutionAdjustment = 600,
                DexterityAdjustment = 1337,
                NaturalArmorAdjustment = 1336,
                Reach = 9.6,
                Size = "enormous",
                Space = 78.3,
                StrengthAdjustment = 8245,
                AdjustedChallengeRating = "adjusted cr",
            };

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.AttackData.AdditionalHitDiceRoll], Is.EqualTo("9266d90210"));
            Assert.That(rawData[DataIndexConstants.AttackData.ChallengeRatingDivisor], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.AttackData.ConstitutionAdjustment], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.AttackData.DexterityAdjustment], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.AttackData.NaturalArmorAdjustment], Is.EqualTo("1336"));
            Assert.That(rawData[DataIndexConstants.AttackData.Reach], Is.EqualTo("9.6"));
            Assert.That(rawData[DataIndexConstants.AttackData.Size], Is.EqualTo("enormous"));
            Assert.That(rawData[DataIndexConstants.AttackData.Space], Is.EqualTo("78.3"));
            Assert.That(rawData[DataIndexConstants.AttackData.StrengthAdjustment], Is.EqualTo("8245"));
            Assert.That(rawData[DataIndexConstants.AttackData.AdjustedChallengeRating], Is.EqualTo("adjusted cr"));
        }

        [Test]
        public void FromData_ReturnsSelection_WithNoDamage()
        {
            var data = attackHelper.BuildData("name", string.Empty, "effect", 4.2, "attack type", 9266, "time period", true, true, true, true, string.Empty, string.Empty);
            var rawData = attackHelper.BuildEntry(data);

            var selection = AttackDataSelection.From(rawData);
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
            var damageData = damageHelper.BuildData("my roll", "my damage type", "my condition");
            var damageEntry = damageHelper.BuildEntry(damageData);

            var data = attackHelper.BuildData("name", damageEntry, "effect", 4.2, "attack type", 9266, "time period", true, true, true, true, string.Empty, string.Empty);
            var rawData = attackHelper.BuildEntry(data);

            var selection = AttackDataSelection.From(rawData);
            Assert.That(selection.AttackType, Is.EqualTo("attack type"));
            Assert.That(selection.Damages, Has.Count.EqualTo(1));
            Assert.That(selection.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(selection.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(selection.Damages[0].Condition, Is.EqualTo("my condition"));
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
            var damageData1 = damageHelper.BuildData("my roll", "my damage type", "my condition");
            var damageEntry1 = damageHelper.BuildEntry(damageData1);

            var damageData2 = damageHelper.BuildData("my other roll", "my other damage type", "my other condition");
            var damageEntry2 = damageHelper.BuildEntry(damageData2);

            var damageEntry = string.Join(AttackDataSelection.DamageSplitDivider, damageEntry1, damageEntry2);

            var data = attackHelper.BuildData("name", damageEntry, "effect", 4.2, "attack type", 9266, "time period", true, true, true, true, string.Empty, string.Empty);
            var rawData = attackHelper.BuildEntry(data);

            var selection = AttackDataSelection.From(rawData);
            Assert.That(selection.AttackType, Is.EqualTo("attack type"));
            Assert.That(selection.Damages, Has.Count.EqualTo(2));
            Assert.That(selection.Damages[0].Roll, Is.EqualTo("my roll"));
            Assert.That(selection.Damages[0].Type, Is.EqualTo("my damage type"));
            Assert.That(selection.Damages[0].Condition, Is.EqualTo("my condition"));
            Assert.That(selection.Damages[1].Roll, Is.EqualTo("my other roll"));
            Assert.That(selection.Damages[1].Type, Is.EqualTo("my other damage type"));
            Assert.That(selection.Damages[1].Condition, Is.EqualTo("my other condition"));
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

            var selection = AttackDataSelection.From(rawData);
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

            var selection = AttackDataSelection.From(rawData);
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

        [TestCase(null)]
        [TestCase("")]
        public void RequirementsMet_ReturnsTrue_WhenNoRequiredGender(string requiredGender)
        {
            selection.RequiredGender = requiredGender;
            var met = selection.RequirementsMet("whatever");
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMet_ReturnsTrue_WhenRequiredGenderMatches()
        {
            selection.RequiredGender = "my required gender";
            var met = selection.RequirementsMet("my required gender");
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMet_ReturnsFalse_WhenRequiredGenderDoesNotMatch()
        {
            selection.RequiredGender = "my required gender";
            var met = selection.RequirementsMet("wrong gender");
            Assert.That(met, Is.False);
        }
    }
}
