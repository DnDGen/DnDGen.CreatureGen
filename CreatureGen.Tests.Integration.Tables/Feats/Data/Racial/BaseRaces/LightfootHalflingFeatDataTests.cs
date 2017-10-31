using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class LightfootHalflingFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Halfling_Lightfoot); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SaveBonus + FeatConstants.Foci.All,
                FeatConstants.SaveBonus + "Fear",
                FeatConstants.AttackBonus + "ThrowOrSling",
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.Climb,
                FeatConstants.SkillBonus + SkillConstants.Jump,
                FeatConstants.SkillBonus + SkillConstants.MoveSilently
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SaveBonus + "Fear",
            FeatConstants.SaveBonus,
            "Fear",
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + FeatConstants.Foci.All,
            FeatConstants.SaveBonus,
            FeatConstants.Foci.All,
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.AttackBonus + "ThrowOrSling",
            FeatConstants.AttackBonus,
            "Thrown weapons and slings",
            0,
            "",
            0,
            "",
            1,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Climb,
            FeatConstants.SkillBonus,
            SkillConstants.Climb,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Jump,
            FeatConstants.SkillBonus,
            SkillConstants.Jump,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SkillBonus,
            SkillConstants.MoveSilently,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        public override void RacialFeatData(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}