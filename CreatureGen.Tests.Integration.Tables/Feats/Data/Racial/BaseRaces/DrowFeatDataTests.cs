using CreatureGen.CharacterClasses;
using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;
using System;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class DrowFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Drow);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.ImmuneToEffect,
                FeatConstants.SaveBonus,
                FeatConstants.SaveBonus + "Will",
                FeatConstants.Darkvision,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.HandCrossbow,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.ShortSword,
                FeatConstants.PassiveSecretDoorSearch,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SpellResistance,
                FeatConstants.SpellLikeAbility + SpellConstants.DancingLights,
                FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
                FeatConstants.SpellLikeAbility + SpellConstants.FaerieFire,
                FeatConstants.LightBlindness,
                FeatConstants.Poison
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SaveBonus,
            FeatConstants.SaveBonus,
            CharacterClassConstants.Schools.Enchantment,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.ImmuneToEffect,
            FeatConstants.ImmuneToEffect,
            SpellConstants.Sleep,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + "Will",
            FeatConstants.SaveBonus,
            "Will (spells and spell-like abilities)",
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
            120,
            0, 0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.HandCrossbow,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.HandCrossbow,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Rapier,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.ShortSword,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.ShortSword,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.PassiveSecretDoorSearch,
            FeatConstants.PassiveSecretDoorSearch,
            "",
            0,
            "",
            0,
            "",
            0,
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
        [TestCase(FeatConstants.SpellResistance,
            FeatConstants.SpellResistance,
            "Add class level to strength",
            0,
            "",
            0,
            "",
            11,
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
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.Darkness,
            FeatConstants.SpellLikeAbility,
            SpellConstants.Darkness,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.FaerieFire,
            FeatConstants.SpellLikeAbility,
            SpellConstants.FaerieFire,
            1,
            FeatConstants.Frequencies.Day,
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.LightBlindness,
            FeatConstants.LightBlindness,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.Poison,
            FeatConstants.Poison,
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
