using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class PixieFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Pixie); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.LowLightVision,
                FeatConstants.Dodge,
                FeatConstants.WeaponFinesse,
                FeatConstants.NaturalArmor,
                FeatConstants.DamageReduction,
                FeatConstants.SpellResistance,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.SpellLikeAbility + SpellConstants.Confusion_Lesser,
                FeatConstants.SpellLikeAbility + SpellConstants.DancingLights,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectAlignment,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectThoughts,
                FeatConstants.SpellLikeAbility + SpellConstants.DispelMagic,
                FeatConstants.SpellLikeAbility + SpellConstants.Entangle,
                FeatConstants.SpellLikeAbility + SpellConstants.PermanentImage,
                FeatConstants.SpellLikeAbility + SpellConstants.Invisibility_Greater,
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.LowLightVision,
            FeatConstants.LowLightVision,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Dodge,
            FeatConstants.Dodge,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.WeaponFinesse,
            FeatConstants.WeaponFinesse,
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
        [TestCase(FeatConstants.DamageReduction,
            FeatConstants.DamageReduction,
            "Must be cold iron to overcome",
            1,
            FeatConstants.Frequencies.Hit,
            0,
            "",
            10,
            0, 0)]
        [TestCase(FeatConstants.SpellResistance,
            FeatConstants.SpellResistance,
            "Add class levels to power",
            0,
            "",
            0,
            "",
            15,
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
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus,
            SkillConstants.Search,
            0,
            "",
            0,
            "",
            2,
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
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Confusion_Lesser,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Confusion_Lesser,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DancingLights,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DancingLights,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DetectAlignment,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DetectAlignment,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DetectThoughts,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DetectThoughts,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DispelMagic,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DispelMagic,
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
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.PermanentImage,
            FeatConstants.SpellLikeAbility,
            SpellConstants.PermanentImage,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Invisibility_Greater,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Invisibility_Greater,
            0,
            FeatConstants.Frequencies.Constant,
            0,
            "",
            0,
            0, 0)]
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int strength, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, strength, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
