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
    public class DoppelgangerFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, SizeConstants.BaseRaces.Doppelganger); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SkillBonus + SkillConstants.Bluff,
                FeatConstants.SkillBonus + SkillConstants.Disguise,
                FeatConstants.SkillBonus + SkillConstants.Disguise + FeatConstants.ChangeShape,
                FeatConstants.SkillBonus + SkillConstants.Bluff + SpellConstants.DetectThoughts,
                FeatConstants.SkillBonus + SkillConstants.Disguise + SpellConstants.DetectThoughts,
                FeatConstants.Darkvision,
                FeatConstants.NaturalArmor,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectThoughts,
                FeatConstants.ChangeShape,
                FeatConstants.ImmuneToEffect + "Sleep",
                FeatConstants.ImmuneToEffect + "Charm"
            };

            AssertCollectionNames(names);
        }

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
            SkillConstants.Bluff + " (when reading minds)",
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Disguise + SpellConstants.DetectThoughts,
            FeatConstants.SkillBonus,
            SkillConstants.Disguise + " (when reading minds)",
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
        [TestCase(FeatConstants.NaturalArmor,
            FeatConstants.NaturalArmor,
            "",
            0,
            "",
            0,
            "",
            4,
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
        [TestCase(FeatConstants.ImmuneToEffect + "Charm",
            FeatConstants.ImmuneToEffect,
            "Charm",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect + "Sleep",
            FeatConstants.ImmuneToEffect,
            "Sleep",
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
