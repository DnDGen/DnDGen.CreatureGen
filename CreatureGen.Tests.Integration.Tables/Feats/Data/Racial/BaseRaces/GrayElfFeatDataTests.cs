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
    public class GrayElfFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Elf_Gray); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.ImmuneToEffect,
                FeatConstants.SaveBonus,
                FeatConstants.LowLightVision,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Longsword,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Rapier,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Longbow,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
                FeatConstants.PassiveSecretDoorSearch,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.SkillBonus + SkillConstants.Listen
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Longsword,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Longsword,
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
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Longbow,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Longbow,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Shortbow,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Shortbow,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
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
        [TestCase(FeatConstants.LowLightVision,
            FeatConstants.LowLightVision,
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
        [TestCase(FeatConstants.PassiveSecretDoorSearch,
            FeatConstants.PassiveSecretDoorSearch,
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
