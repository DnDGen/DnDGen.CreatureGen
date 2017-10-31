using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class AasimarFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Aasimar); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Darkvision,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SpellLikeAbility,
                FeatConstants.Resistance + FeatConstants.Foci.Acid,
                FeatConstants.Resistance + FeatConstants.Foci.Cold,
                FeatConstants.Resistance + FeatConstants.Foci.Electricity
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SpellLikeAbility,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Daylight,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus,
            SkillConstants.Spot,
            0,
            "",
            0,
            "",
            2,
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
        [TestCase(FeatConstants.Darkvision,
            FeatConstants.Darkvision,
            "",
            0,
            "",
            0,
            "",
            60,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Cold,
            FeatConstants.Resistance,
            FeatConstants.Foci.Cold,
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Electricity,
            FeatConstants.Resistance,
            FeatConstants.Foci.Electricity,
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Acid,
            FeatConstants.Resistance,
            FeatConstants.Foci.Acid,
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            5,
            0, 0)]
        public override void RacialFeatData(String name, String feat, String focus, Int32 frequencyQuantity, String frequencyTimePeriod, Int32 minimumHitDiceRequirement, String sizeRequirement, Int32 strength, Int32 maximumHitDiceRequirement, Int32 requiredStatMinimumValue, params String[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
