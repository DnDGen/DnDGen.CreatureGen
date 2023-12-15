using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements.FeatSkillRankRequirementsTests;
using static DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements.FeatSpeedRequirementsTests;
using static DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements.RequiredAlignmentsTests;
using static DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements.RequiredFeatsTests;
using static DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements.RequiredSizesTests;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Feats
{
    [TestFixture]
    public class FeatGroupsTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.FeatGroups;

        [Test]
        public void FeatGroupsNames()
        {
            var names = new[]
            {
                GroupConstants.AttackBonus,
                GroupConstants.Initiative,
                GroupConstants.TakenMultipleTimes,
                GroupConstants.WeaponProficiency,
                GroupConstants.ArmorProficiency,
                SaveConstants.Fortitude,
                SaveConstants.Reflex,
                SaveConstants.Will,
                TableNameConstants.Collection.RequiredAlignments,
                TableNameConstants.Collection.RequiredSizes,
                TableNameConstants.Collection.RequiredFeats,
                TableNameConstants.TypeAndAmount.FeatAbilityRequirements,
                TableNameConstants.TypeAndAmount.FeatSpeedRequirements,
                TableNameConstants.TypeAndAmount.FeatSkillRankRequirements,
            };

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.AttackBonus,
            FeatConstants.SpecialQualities.AttackBonus)]
        [TestCase(GroupConstants.Initiative,
            FeatConstants.Initiative_Improved)]
        [TestCase(GroupConstants.TakenMultipleTimes,
            //FeatConstants.SpellMastery,
            FeatConstants.Toughness,
            //FeatConstants.Turning_Extra,
            FeatConstants.Monster.NaturalArmor_Improved,
            FeatConstants.SpecialQualities.AttackBonus,
            FeatConstants.SpecialQualities.DodgeBonus)]
        [TestCase(GroupConstants.WeaponProficiency,
            FeatConstants.WeaponProficiency_Exotic,
            FeatConstants.WeaponProficiency_Martial,
            FeatConstants.WeaponProficiency_Simple)]
        [TestCase(GroupConstants.ArmorProficiency,
            FeatConstants.ArmorProficiency_Heavy,
            FeatConstants.ArmorProficiency_Light,
            FeatConstants.ArmorProficiency_Medium,
            FeatConstants.ShieldProficiency,
            FeatConstants.ShieldProficiency_Tower)]
        [TestCase(SaveConstants.Fortitude,
            FeatConstants.GreatFortitude)]
        [TestCase(SaveConstants.Reflex,
            FeatConstants.LightningReflexes)]
        [TestCase(SaveConstants.Will,
            FeatConstants.IronWill)]
        public void FeatGroup(string name, params string[] collection)
        {
            AssertDistinctCollection(name, collection);
        }

        [TestCaseSource(typeof(FeatAbilityRequirementsTests), nameof(FeatAbilityRequirementsTests.Feats))]
        [TestCaseSource(typeof(FeatAbilityRequirementsTests), nameof(FeatAbilityRequirementsTests.Metamagic))]
        [TestCaseSource(typeof(FeatAbilityRequirementsTests), nameof(FeatAbilityRequirementsTests.Monster))]
        [TestCaseSource(typeof(FeatAbilityRequirementsTests), nameof(FeatAbilityRequirementsTests.Craft))]
        [TestCaseSource(typeof(FeatAbilityRequirementsTests), nameof(FeatAbilityRequirementsTests.SpecialQualities))]
        [TestCaseSource(typeof(FeatAbilityRequirementsTests), nameof(FeatAbilityRequirementsTests.FeatsWithFoci))]
        public void AbilityRequirementFeatGroup_ContainsFeatsWithRequirements(string name, Dictionary<string, int> typesAndAmounts)
        {
            var collection = table[TableNameConstants.TypeAndAmount.FeatAbilityRequirements];
            if (typesAndAmounts.Any())
                Assert.That(collection, Contains.Item(name));
            else
                Assert.That(collection, Does.Not.Contain(name));
        }

        [TestCaseSource(typeof(SkillRankRequirementsTestData), nameof(SkillRankRequirementsTestData.Feats))]
        [TestCaseSource(typeof(SkillRankRequirementsTestData), nameof(SkillRankRequirementsTestData.Metamagic))]
        [TestCaseSource(typeof(SkillRankRequirementsTestData), nameof(SkillRankRequirementsTestData.Monster))]
        [TestCaseSource(typeof(SkillRankRequirementsTestData), nameof(SkillRankRequirementsTestData.Craft))]
        public void SkillRankRequirementFeatGroup_ContainsFeatsWithRequirements(string name, Dictionary<string, int> typesAndAmounts)
        {
            var collection = table[TableNameConstants.TypeAndAmount.FeatSkillRankRequirements];
            if (typesAndAmounts.Any())
                Assert.That(collection, Contains.Item(name));
            else
                Assert.That(collection, Does.Not.Contain(name));
        }

        [TestCaseSource(typeof(SpeedRequirementsTestData), nameof(SpeedRequirementsTestData.Feats))]
        [TestCaseSource(typeof(SpeedRequirementsTestData), nameof(SpeedRequirementsTestData.Metamagic))]
        [TestCaseSource(typeof(SpeedRequirementsTestData), nameof(SpeedRequirementsTestData.Monster))]
        [TestCaseSource(typeof(SpeedRequirementsTestData), nameof(SpeedRequirementsTestData.Craft))]
        public void SpeedRequirementFeatGroup_ContainsFeatsWithRequirements(string name, Dictionary<string, int> typesAndAmounts)
        {
            var collection = table[TableNameConstants.TypeAndAmount.FeatSpeedRequirements];
            if (typesAndAmounts.Any())
                Assert.That(collection, Contains.Item(name));
            else
                Assert.That(collection, Does.Not.Contain(name));
        }

        [TestCaseSource(typeof(RequiredAlignmentsTestData), nameof(RequiredAlignmentsTestData.Feats))]
        [TestCaseSource(typeof(RequiredAlignmentsTestData), nameof(RequiredAlignmentsTestData.Metamagic))]
        [TestCaseSource(typeof(RequiredAlignmentsTestData), nameof(RequiredAlignmentsTestData.Monster))]
        [TestCaseSource(typeof(RequiredAlignmentsTestData), nameof(RequiredAlignmentsTestData.Craft))]
        [TestCaseSource(typeof(RequiredAlignmentsTestData), nameof(RequiredAlignmentsTestData.SpecialQualities))]
        public void AlignmentRequirementFeatGroup_ContainsFeatsWithRequirements(string name, params string[] alignments)
        {
            var collection = table[TableNameConstants.Collection.RequiredAlignments];
            if (alignments.Any())
                Assert.That(collection, Contains.Item(name));
            else
                Assert.That(collection, Does.Not.Contain(name));
        }

        [TestCaseSource(typeof(RequiredFeatsTestData), nameof(RequiredFeatsTestData.Feats))]
        [TestCaseSource(typeof(RequiredFeatsTestData), nameof(RequiredFeatsTestData.Metamagic))]
        [TestCaseSource(typeof(RequiredFeatsTestData), nameof(RequiredFeatsTestData.Monster))]
        [TestCaseSource(typeof(RequiredFeatsTestData), nameof(RequiredFeatsTestData.Craft))]
        [TestCaseSource(typeof(RequiredFeatsTestData), nameof(RequiredFeatsTestData.SpecialQualities))]
        public void FeatRequirementFeatGroup_ContainsFeatsWithRequirements(string name, params string[] requiredFeats)
        {
            var collection = table[TableNameConstants.Collection.RequiredFeats];
            if (requiredFeats.Any())
                Assert.That(collection, Contains.Item(name));
            else
                Assert.That(collection, Does.Not.Contain(name));
        }

        [TestCaseSource(typeof(RequiredSizesTestData), nameof(RequiredSizesTestData.Feats))]
        [TestCaseSource(typeof(RequiredSizesTestData), nameof(RequiredSizesTestData.Metamagic))]
        [TestCaseSource(typeof(RequiredSizesTestData), nameof(RequiredSizesTestData.Monster))]
        [TestCaseSource(typeof(RequiredSizesTestData), nameof(RequiredSizesTestData.Craft))]
        [TestCaseSource(typeof(RequiredSizesTestData), nameof(RequiredSizesTestData.SpecialQualities))]
        public void SizeRequirementFeatGroup_ContainsFeatsWithRequirements(string name, params string[] sizes)
        {
            var collection = table[TableNameConstants.Collection.RequiredSizes];
            if (sizes.Any())
                Assert.That(collection, Contains.Item(name));
            else
                Assert.That(collection, Does.Not.Contain(name));
        }
    }
}