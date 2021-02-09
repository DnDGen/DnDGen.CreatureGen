using DnDGen.CreatureGen.Tables;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Tables
{
    [TestFixture]
    public class TableNameConstantsTests
    {
        [TestCase(TableNameConstants.Adjustments.ArcaneSpellFailures, "ArcaneSpellFailures")]
        [TestCase(TableNameConstants.Adjustments.GrappleBonuses, "GrappleBonuses")]
        [TestCase(TableNameConstants.Adjustments.HitDice, "HitDice")]
        [TestCase(TableNameConstants.Adjustments.SizeModifiers, "SizeModifiers")]
        [TestCase(TableNameConstants.Adjustments.SkillPoints, "SkillPoints")]
        [TestCase(TableNameConstants.Collection.AbilityGroups, "AbilityGroups")]
        [TestCase(TableNameConstants.Collection.AerialManeuverability, "AerialManeuverability")]
        [TestCase(TableNameConstants.Collection.AlignmentGroups, "AlignmentGroups")]
        [TestCase(TableNameConstants.Collection.AttackData, "AttackData")]
        [TestCase(TableNameConstants.Collection.CasterGroups, "CasterGroups")]
        [TestCase(TableNameConstants.Collection.CreatureData, "CreatureData")]
        [TestCase(TableNameConstants.Collection.CreatureGroups, "CreatureGroups")]
        [TestCase(TableNameConstants.Collection.CreatureTypes, "CreatureTypes")]
        [TestCase(TableNameConstants.Collection.FeatData, "FeatData")]
        [TestCase(TableNameConstants.Collection.FeatFoci, "FeatFoci")]
        [TestCase(TableNameConstants.Collection.FeatGroups, "FeatGroups")]
        [TestCase(TableNameConstants.Collection.LanguageGroups, "LanguageGroups")]
        [TestCase(TableNameConstants.Collection.PredeterminedItems, "PredeterminedItems")]
        [TestCase(TableNameConstants.Collection.RequiredAlignments, "RequiredAlignments")]
        [TestCase(TableNameConstants.Collection.RequiredFeats, "RequiredFeats")]
        [TestCase(TableNameConstants.Collection.RequiredSizes, "RequiredSizes")]
        [TestCase(TableNameConstants.Collection.SkillData, "SkillData")]
        [TestCase(TableNameConstants.Collection.SkillGroups, "SkillGroups")]
        [TestCase(TableNameConstants.Collection.SpecialQualityData, "SpecialQualityData")]
        [TestCase(TableNameConstants.Collection.Speeds, "Speeds")]
        [TestCase(TableNameConstants.Collection.SpellGroups, "SpellGroups")]
        [TestCase(TableNameConstants.Collection.TemplateGroups, "TemplateGroups")]
        [TestCase(TableNameConstants.Collection.WeightRolls, "WeightRolls")]
        [TestCase(TableNameConstants.TypeAndAmount.AbilityAdjustments, "AbilityAdjustments")]
        [TestCase(TableNameConstants.TypeAndAmount.Advancements, "Advancements")]
        [TestCase(TableNameConstants.TypeAndAmount.ArmorClassBonuses, "ArmorClassBonuses")]
        [TestCase(TableNameConstants.TypeAndAmount.Casters, "Casters")]
        [TestCase(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "FeatAbilityRequirements")]
        [TestCase(TableNameConstants.TypeAndAmount.FeatSkillRankRequirements, "FeatSkillRankRequirements")]
        [TestCase(TableNameConstants.TypeAndAmount.FeatSpeedRequirements, "FeatSpeedRequirements")]
        [TestCase(TableNameConstants.TypeAndAmount.SaveBonuses, "SaveBonuses")]
        [TestCase(TableNameConstants.TypeAndAmount.SkillBonuses, "SkillBonuses")]
        [TestCase(TableNameConstants.TypeAndAmount.SpellDomains, "SpellDomains")]
        [TestCase(TableNameConstants.TypeAndAmount.SpellLevels, "SpellLevels")]
        [TestCase(TableNameConstants.TypeAndAmount.SpellsPerDay, "SpellsPerDay")]
        public void TableNameConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}