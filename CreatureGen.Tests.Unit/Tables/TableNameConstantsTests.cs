using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Tables
{
    [TestFixture]
    public class TableNameConstantsTests
    {
        [TestCase(TableNameConstants.Set.Adjustments.ArmorDeflectionBonuses, "ArmorDeflectionBonuses")]
        [TestCase(TableNameConstants.Set.Adjustments.GrappleBonuses, "GrappleBonuses")]
        [TestCase(TableNameConstants.Set.Adjustments.HitDice, "HitDice")]
        [TestCase(TableNameConstants.Set.Adjustments.SizeModifiers, "SizeModifiers")]
        [TestCase(TableNameConstants.Set.Adjustments.SkillPoints, "SkillPoints")]
        [TestCase(TableNameConstants.Set.Collection.Advancements, "Advancements")]
        [TestCase(TableNameConstants.Set.Collection.AerialManeuverability, "AerialManeuverability")]
        [TestCase(TableNameConstants.Set.Collection.AlignmentGroups, "AlignmentGroups")]
        [TestCase(TableNameConstants.Set.Collection.ArmorClassModifiers, "ArmorClassModifiers")]
        [TestCase(TableNameConstants.Set.Collection.CreatureData, "CreatureData")]
        [TestCase(TableNameConstants.Set.Collection.CreatureGroups, "CreatureGroups")]
        [TestCase(TableNameConstants.Set.Collection.CreatureTypes, "CreatureTypes")]
        [TestCase(TableNameConstants.Set.Collection.DragonSpecies, "DragonSpecies")]
        [TestCase(TableNameConstants.Set.Collection.FeatData, "FeatData")]
        [TestCase(TableNameConstants.Set.Collection.FeatFoci, "FeatFoci")]
        [TestCase(TableNameConstants.Set.Collection.FeatGroups, "FeatGroups")]
        [TestCase(TableNameConstants.Set.Collection.RequiredFeats, "RequiredFeats")]
        [TestCase(TableNameConstants.Set.Collection.RequiredSizes, "RequiredSizes")]
        [TestCase(TableNameConstants.Set.Collection.SaveBonuses, "SaveBonuses")]
        [TestCase(TableNameConstants.Set.Collection.SkillData, "SkillData")]
        [TestCase(TableNameConstants.Set.Collection.SkillGroups, "SkillGroups")]
        [TestCase(TableNameConstants.Set.Collection.SkillSynergy, "SkillSynergy")]
        [TestCase(TableNameConstants.Set.Collection.SpecialQualityData, "SpecialQualityData")]
        [TestCase(TableNameConstants.Set.Collection.Speeds, "Speeds")]
        [TestCase(TableNameConstants.Set.Collection.SpellGroups, "SpellGroups")]
        [TestCase(TableNameConstants.Set.Collection.TemplateGroups, "TemplateGroups")]
        [TestCase(TableNameConstants.Set.Collection.WeightRolls, "WeightRolls")]
        [TestCase(TableNameConstants.Set.Percentile.AlignmentGoodness, "AlignmentGoodness")]
        [TestCase(TableNameConstants.Set.Percentile.AlignmentLawfulness, "AlignmentLawfulness")]
        [TestCase(TableNameConstants.Set.Percentile.Traits, "Traits")]
        [TestCase(TableNameConstants.Set.TrueOrFalse.Male, "Male")]
        [TestCase(TableNameConstants.Set.TypeAndAmount.AbilityAdjustments, "AbilityAdjustments")]
        [TestCase(TableNameConstants.Set.TypeAndAmount.FeatAbilityRequirements, "FeatAbilityRequirements")]
        [TestCase(TableNameConstants.Set.TypeAndAmount.FeatSkillRankRequirements, "FeatSkillRankRequirements")]
        [TestCase(TableNameConstants.Set.TypeAndAmount.FeatSpeedRequirements, "FeatSpeedRequirements")]
        public void TableNameConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}