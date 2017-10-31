using CreatureGen.Abilities;
using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.Metaraces
{
    [TestFixture]
    public class VampireFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Templates.Vampire);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.ArmorBonus,
                FeatConstants.Slam + SizeConstants.Sizes.Large,
                FeatConstants.Slam + SizeConstants.Sizes.Medium,
                FeatConstants.Slam + SizeConstants.Sizes.Small,
                FeatConstants.BloodDrain,
                FeatConstants.ChildrenOfTheNight,
                FeatConstants.Dominate,
                FeatConstants.CreateSpawn,
                FeatConstants.EnergyDrain,
                FeatConstants.AlternateForm,
                FeatConstants.DamageReduction,
                FeatConstants.FastHealing,
                FeatConstants.GaseousForm,
                FeatConstants.Resistance + FeatConstants.Foci.Cold,
                FeatConstants.Resistance + FeatConstants.Foci.Electricity,
                FeatConstants.SpellLikeAbility + SpellConstants.SpiderClimb,
                FeatConstants.TurnResistance,
                FeatConstants.SkillBonus + SkillConstants.Bluff,
                FeatConstants.SkillBonus + SkillConstants.Hide,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.MoveSilently,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.SenseMotive,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.Alertness,
                FeatConstants.CombatReflexes,
                FeatConstants.Dodge,
                FeatConstants.ImprovedInitiative,
                FeatConstants.LightningReflexes
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
            6, 0, 0)]
        [TestCase(FeatConstants.Slam + SizeConstants.Sizes.Large,
            FeatConstants.Slam,
            "1d8",
            0,
            "",
            0,
            SizeConstants.Sizes.Large,
            0, 0, 0)]
        [TestCase(FeatConstants.Slam + SizeConstants.Sizes.Medium,
            FeatConstants.Slam,
            "1d6",
            0,
            "",
            0,
            SizeConstants.Sizes.Medium,
            0, 0, 0)]
        [TestCase(FeatConstants.Slam + SizeConstants.Sizes.Small,
            FeatConstants.Slam,
            "1d4",
            0,
            "",
            0,
            SizeConstants.Sizes.Small,
            0, 0, 0)]
        [TestCase(FeatConstants.BloodDrain,
            FeatConstants.BloodDrain,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.ChildrenOfTheNight,
            FeatConstants.ChildrenOfTheNight,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.Dominate,
            FeatConstants.Dominate,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.CreateSpawn,
            FeatConstants.CreateSpawn,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.EnergyDrain,
            FeatConstants.EnergyDrain,
            "",
            1,
            FeatConstants.Frequencies.Round,
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.AlternateForm,
            FeatConstants.AlternateForm,
            "Bat, dire bat, wolf, or dire wolf",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.DamageReduction,
            FeatConstants.DamageReduction,
            "Must be magical and silver to overcome",
            1,
            FeatConstants.Frequencies.Hit,
            0,
            "",
            10, 0, 0)]
        [TestCase(FeatConstants.FastHealing,
            FeatConstants.FastHealing,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.GaseousForm,
            FeatConstants.GaseousForm,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Cold,
            FeatConstants.Resistance,
            FeatConstants.Foci.Cold,
            0,
            "",
            0,
            "",
            10, 0, 0)]
        [TestCase(FeatConstants.Resistance + FeatConstants.Foci.Electricity,
            FeatConstants.Resistance,
            FeatConstants.Foci.Electricity,
            0,
            "",
            0,
            "",
            10, 0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.SpiderClimb,
            FeatConstants.SpellLikeAbility,
            SpellConstants.SpiderClimb,
            0,
            FeatConstants.Frequencies.Constant,
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
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Bluff,
            FeatConstants.SkillBonus,
            SkillConstants.Bluff,
            0,
            "",
            0,
            "",
            8, 0, 0)]
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
        [TestCase(FeatConstants.Alertness,
            FeatConstants.Alertness,
            "",
            0,
            "",
            0,
            "",
            2, 0, 0)]
        [TestCase(FeatConstants.CombatReflexes,
            FeatConstants.CombatReflexes,
            "",
            0,
            "",
            0,
            "",
            0, 0, 0)]
        [TestCase(FeatConstants.Dodge,
            FeatConstants.Dodge,
            "",
            0,
            "",
            0,
            "",
            1, 0, 13, AbilityConstants.Dexterity)]
        [TestCase(FeatConstants.ImprovedInitiative,
            FeatConstants.ImprovedInitiative,
            "",
            0,
            "",
            0,
            "",
            4, 0, 0)]
        [TestCase(FeatConstants.LightningReflexes,
            FeatConstants.LightningReflexes,
            "",
            0,
            "",
            0,
            "",
            2, 0, 0)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, power, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
