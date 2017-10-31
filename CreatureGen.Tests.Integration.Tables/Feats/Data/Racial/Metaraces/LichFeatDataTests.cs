using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.Metaraces
{
    [TestFixture]
    public class LichFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Templates.Lich);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.ArmorBonus,
                FeatConstants.FearAura,
                FeatConstants.ParalyzingTouch,
                FeatConstants.TurnResistance,
                FeatConstants.DamageReduction,
                FeatConstants.ImmuneToEffect + FeatConstants.Foci.Cold,
                FeatConstants.ImmuneToEffect + FeatConstants.Foci.Electricity,
                FeatConstants.ImmuneToEffect + SpellConstants.Polymorph,
                FeatConstants.ImmuneToEffect + "Mind",
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.MoveSilently,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.SenseMotive,
                FeatConstants.SkillBonus + SkillConstants.Spot
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.ArmorBonus,
            FeatConstants.ArmorBonus,
            "",
            0,
            "",
            0,
            "",
            5, 0, 0)]
        [TestCase(FeatConstants.FearAura,
            FeatConstants.FearAura,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.ParalyzingTouch,
            FeatConstants.ParalyzingTouch,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.TurnResistance,
            FeatConstants.TurnResistance,
            "",
            0,
            "",
            0,
            "",
            4, 0, 0)]
        [TestCase(FeatConstants.DamageReduction,
            FeatConstants.DamageReduction,
            "Must be magical and bludgeoning to overcome",
            1,
            FeatConstants.Frequencies.Hit,
            0,
            "",
            15, 0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect + FeatConstants.Foci.Cold,
            FeatConstants.ImmuneToEffect,
            FeatConstants.Foci.Cold,
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect + FeatConstants.Foci.Electricity,
            FeatConstants.ImmuneToEffect,
            FeatConstants.Foci.Electricity,
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect + SpellConstants.Polymorph,
            FeatConstants.ImmuneToEffect,
            SpellConstants.Polymorph,
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect + "Mind",
            FeatConstants.ImmuneToEffect,
            "Mind-affecting attacks",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Hide,
            FeatConstants.SkillBonus,
            SkillConstants.Hide,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Listen,
            FeatConstants.SkillBonus,
            SkillConstants.Listen,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.MoveSilently,
            FeatConstants.SkillBonus,
            SkillConstants.MoveSilently,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus,
            SkillConstants.Search,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.SenseMotive,
            FeatConstants.SkillBonus,
            SkillConstants.SenseMotive,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus,
            SkillConstants.Spot,
            0,
            "",
            0,
            "",
            8, 0, 0)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, power, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
