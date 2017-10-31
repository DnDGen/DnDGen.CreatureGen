using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.Metaraces
{
    [TestFixture]
    public class WeretigerFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Templates.Weretiger); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.AlternateForm,
                FeatConstants.Empathy,
                FeatConstants.Lycanthropy,
                FeatConstants.ImprovedGrab,
                FeatConstants.Pounce,
                FeatConstants.Rake,
                FeatConstants.SkillBonus + SkillConstants.MoveSilently,
                FeatConstants.SkillBonus + SkillConstants.Balance,
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.Hide + "Grass",
                FeatConstants.SaveBonus + "Reflex",
                FeatConstants.SaveBonus + "Fortitude",
                FeatConstants.SaveBonus + "Will",
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.Alertness,
                FeatConstants.IronWill,
                FeatConstants.LowLightVision,
                FeatConstants.Scent,
                FeatConstants.NaturalArmor
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.AlternateForm,
            FeatConstants.AlternateForm,
            "Tiger",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Empathy,
            FeatConstants.Empathy,
            "Tiger",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.Lycanthropy,
            FeatConstants.Lycanthropy,
            "Weretiger",
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
        [TestCase(FeatConstants.Pounce,
            FeatConstants.Pounce,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Rake,
            FeatConstants.Rake,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.IronWill,
            FeatConstants.IronWill,
            "",
            0,
            "",
            0,
            "",
            2,
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
        [TestCase(FeatConstants.SaveBonus + "Fortitude",
            FeatConstants.SaveBonus,
            "Fortitude",
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + "Reflex",
            FeatConstants.SaveBonus,
            "Reflex",
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + "Will",
            FeatConstants.SaveBonus,
            "Will",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus,
            SkillConstants.Hide,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide + "Grass",
            FeatConstants.SkillBonus,
            SkillConstants.Hide + " (Tall grass or heavy undergrowth)",
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
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Balance,
            FeatConstants.SkillBonus,
            SkillConstants.Balance,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus,
            SkillConstants.Spot,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.LowLightVision,
            FeatConstants.LowLightVision,
            "",
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
        public override void RacialFeatData(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
