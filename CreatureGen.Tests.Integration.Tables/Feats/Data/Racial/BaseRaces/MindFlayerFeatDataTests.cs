using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class MindFlayerFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, SizeConstants.BaseRaces.MindFlayer); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SaveBonus + "Will",
                FeatConstants.SaveBonus + "Fortitude",
                FeatConstants.SaveBonus + "Reflex",
                FeatConstants.NaturalArmor,
                FeatConstants.SkillBonus + SkillConstants.Bluff,
                FeatConstants.SkillBonus + SkillConstants.Concentration,
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.Intimidate,
                FeatConstants.SkillBonus + SkillConstants.MoveSilently,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.SenseMotive,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.Darkvision,
                FeatConstants.MindBlast,
                FeatConstants.NaturalWeapon,
                FeatConstants.SpellLikeAbility + SpellConstants.CharmMonster,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectThoughts,
                FeatConstants.SpellLikeAbility + SpellConstants.Levitate,
                FeatConstants.SpellLikeAbility + SpellConstants.PlaneShift,
                FeatConstants.SpellLikeAbility + SpellConstants.Suggestion,
                FeatConstants.ImprovedGrab,
                FeatConstants.Extract
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SaveBonus + "Fortitude",
            FeatConstants.SaveBonus,
            "Fortitude",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + "Reflex",
            FeatConstants.SaveBonus,
            "Reflex",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + "Will",
            FeatConstants.SaveBonus,
            "Will",
            0,
            "",
            0,
            "",
            6,
            0, 0)]
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            3,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Bluff,
            FeatConstants.SkillBonus,
            SkillConstants.Bluff,
            0,
            "",
            0,
            "",
            8,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Concentration,
            FeatConstants.SkillBonus,
            SkillConstants.Concentration,
            0,
            "",
            0,
            "",
            6,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus,
            SkillConstants.Hide,
            0,
            "",
            0,
            "",
            8,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Intimidate,
            FeatConstants.SkillBonus,
            SkillConstants.Intimidate,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SkillBonus,
            SkillConstants.MoveSilently,
            0,
            "",
            0,
            "",
            8,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            8,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.SenseMotive,
            FeatConstants.SkillBonus,
            SkillConstants.SenseMotive,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus,
            SkillConstants.Spot,
            0,
            "",
            0,
            "",
            8,
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
        [TestCase(FeatConstants.MindBlast,
            FeatConstants.MindBlast,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon,
            FeatConstants.NaturalWeapon,
            "Tentacles (x4)",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.CharmMonster,
            FeatConstants.SpellLikeAbility,
            SpellConstants.CharmMonster,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DetectThoughts,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DetectThoughts,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Levitate,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Levitate,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.PlaneShift,
            FeatConstants.SpellLikeAbility,
            SpellConstants.PlaneShift,
            0,
            FeatConstants.Frequencies.AtWill,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Suggestion,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Suggestion,
            0,
            FeatConstants.Frequencies.AtWill,
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
        [TestCase(FeatConstants.Extract,
            FeatConstants.Extract,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        public override void RacialFeatData(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
