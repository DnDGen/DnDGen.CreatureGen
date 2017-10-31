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
    public class RakshasaFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Rakshasa); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Darkvision,
                FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
                FeatConstants.SaveBonus + SavingThrowConstants.Will,
                FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
                FeatConstants.SkillBonus + SkillConstants.Bluff,
                FeatConstants.SkillBonus + SkillConstants.Disguise,
                FeatConstants.SkillBonus + SkillConstants.Disguise + FeatConstants.ChangeShape,
                FeatConstants.SkillBonus + SkillConstants.Bluff + SpellConstants.DetectThoughts,
                FeatConstants.SkillBonus + SkillConstants.Disguise + SpellConstants.DetectThoughts,
                FeatConstants.NaturalArmor,
                FeatConstants.NaturalWeapon + "Bite",
                FeatConstants.NaturalWeapon + "Claw",
                FeatConstants.SpellLikeAbility + SpellConstants.DetectThoughts,
                FeatConstants.ChangeShape,
                FeatConstants.DamageReduction,
                FeatConstants.SpellResistance,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.Darkvision,
            FeatConstants.Darkvision,
            "",
            0,
            "",
            0,
            "",
            60,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Fortitude,
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Will,
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Reflex,
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Bluff,
            FeatConstants.SkillBonus,
            SkillConstants.Bluff,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Disguise,
            FeatConstants.SkillBonus,
            SkillConstants.Disguise,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Disguise + FeatConstants.ChangeShape,
            FeatConstants.SkillBonus,
            SkillConstants.Disguise + " (when using Change Shape)",
            0,
            "",
            0,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Bluff + SpellConstants.DetectThoughts,
            FeatConstants.SkillBonus,
            SkillConstants.Bluff + " (when using Detect Thoughts)",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Disguise + SpellConstants.DetectThoughts,
            FeatConstants.SkillBonus,
            SkillConstants.Disguise + " (when using Detect Thoughts)",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            9,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon + "Bite",
            FeatConstants.NaturalWeapon,
            "Bite (1d6)",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon + "Claw",
            FeatConstants.NaturalWeapon,
            "Claw (1d4) x2",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DetectThoughts,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DetectThoughts,
            0,
            FeatConstants.Frequencies.Constant,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ChangeShape,
            FeatConstants.ChangeShape,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.DamageReduction,
            FeatConstants.DamageReduction,
            "Must be both good and piercing to overcome",
            1,
            FeatConstants.Frequencies.Hit,
            0,
            "",
            15,
            0, 0)]
        [TestCase(FeatConstants.SpellResistance,
            FeatConstants.SpellResistance,
            "Add class levels to power",
            0,
            "",
            0,
            "",
            27,
            0, 0)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, power, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
