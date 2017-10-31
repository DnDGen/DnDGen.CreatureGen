using CreatureGen.Combats;
using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class YuanTiPurebloodFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.YuanTi_Pureblood); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
                FeatConstants.SaveBonus + SavingThrowConstants.Will,
                FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
                FeatConstants.Darkvision,
                FeatConstants.NaturalArmor,
                FeatConstants.Alertness,
                FeatConstants.BlindFight,
                FeatConstants.SpellLikeAbility + SpellConstants.AnimalTrance,
                FeatConstants.SpellLikeAbility + SpellConstants.CauseFear,
                FeatConstants.SpellLikeAbility + SpellConstants.CharmPerson,
                FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
                FeatConstants.SpellLikeAbility + SpellConstants.Entangle,
                FeatConstants.SpellResistance,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectPoison,
                FeatConstants.SkillBonus + SkillConstants.Disguise,
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
            1,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Will,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Reflex,
            0,
            "",
            0,
            "",
            4,
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
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.SpellResistance,
            FeatConstants.SpellResistance,
            "Add class levels to power",
            0,
            "",
            0,
            "",
            14,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Disguise,
            FeatConstants.SkillBonus,
            SkillConstants.Disguise + " (when impersonating a human)",
            0,
            "",
            0,
            "",
            5,
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
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Entangle,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Entangle,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Darkness,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.CharmPerson,
            FeatConstants.SpellLikeAbility,
            SpellConstants.CharmPerson,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.CauseFear,
            FeatConstants.SpellLikeAbility,
            SpellConstants.CauseFear,
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
