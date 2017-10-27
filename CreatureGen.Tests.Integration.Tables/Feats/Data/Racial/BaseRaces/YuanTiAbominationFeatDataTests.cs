using CreatureGen.Combats;
using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class YuanTiAbominationFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, SizeConstants.BaseRaces.YuanTiAbomination); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Darkvision,
                FeatConstants.NaturalArmor,
                FeatConstants.Alertness,
                FeatConstants.BlindFight,
                FeatConstants.NaturalWeapon,
                FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
                FeatConstants.SaveBonus + SavingThrowConstants.Will,
                FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
                FeatConstants.Aversion,
                FeatConstants.ImprovedGrab,
                FeatConstants.Poison,
                FeatConstants.ProduceAcid,
                FeatConstants.SpellLikeAbility + SpellConstants.AnimalTrance,
                FeatConstants.SpellLikeAbility + SpellConstants.Entangle,
                FeatConstants.SpellLikeAbility + SpellConstants.DeeperDarkness,
                FeatConstants.SpellLikeAbility + SpellConstants.NeutralizePoison,
                FeatConstants.SpellLikeAbility + SpellConstants.Suggestion,
                FeatConstants.SpellLikeAbility + SpellConstants.BalefulPolymorph,
                FeatConstants.SpellLikeAbility + SpellConstants.Fear,
                FeatConstants.Scent,
                FeatConstants.AlternateForm,
                FeatConstants.ChameleonPower,
                FeatConstants.SpellResistance,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectPoison,
                FeatConstants.SkillBonus + SkillConstants.Climb,
                FeatConstants.SkillBonus + SkillConstants.Swim,
                FeatConstants.SkillBonus + SkillConstants.Swim + "Take 10",
                FeatConstants.SkillBonus + SkillConstants.Hide,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Fortitude,
            0,
            "",
            0,
            "",
            3,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Will,
            0,
            "",
            0,
            "",
            6,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Reflex,
            0,
            "",
            0,
            "",
            6,
            0, 0)]
        [TestCase(FeatConstants.Darkvision,
            FeatConstants.Darkvision,
            "",
            0,
            "",
            0,
            "",
            60,
            0, 0)]
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.Alertness,
            FeatConstants.Alertness,
            "",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.BlindFight,
            FeatConstants.BlindFight,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon,
            FeatConstants.NaturalWeapon,
            "Constrict (1d6+6)",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Scent,
            FeatConstants.Scent,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Aversion,
            FeatConstants.Aversion,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ImprovedGrab,
            FeatConstants.ImprovedGrab,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Poison,
            FeatConstants.Poison,
            "Injury. Fortitude DC 17, initial damage 1d6 Con, secondary 1d6 Con",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ProduceAcid,
            FeatConstants.ProduceAcid,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.AlternateForm,
            FeatConstants.AlternateForm,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ChameleonPower,
            FeatConstants.ChameleonPower,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellResistance,
            FeatConstants.SpellResistance,
            "Add class levels to power",
            0,
            "",
            0,
            "",
            18,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Climb,
            FeatConstants.SkillBonus,
            SkillConstants.Climb + " (can always take 10)",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Swim,
            FeatConstants.SkillBonus,
            SkillConstants.Swim,
            0,
            "",
            0,
            "",
            8,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Swim + "Take 10",
            FeatConstants.SkillBonus,
            SkillConstants.Swim + " (can always take 10)",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus,
            SkillConstants.Hide + " (when using Chameleon Power)",
            0,
            "",
            0,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DetectPoison,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DetectPoison,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.AnimalTrance,
            FeatConstants.SpellLikeAbility,
            SpellConstants.AnimalTrance,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Entangle,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Entangle,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DeeperDarkness,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DeeperDarkness,
            3,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.NeutralizePoison,
            FeatConstants.SpellLikeAbility,
            SpellConstants.NeutralizePoison,
            3,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Suggestion,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Suggestion,
            3,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.BalefulPolymorph,
            FeatConstants.SpellLikeAbility,
            SpellConstants.BalefulPolymorph,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Fear,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Fear,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, power, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
