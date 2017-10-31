using CreatureGen.CharacterClasses;
using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class WoodElfFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Elf_Wood); }
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
        public override void RacialFeatData(string name, string feat, string focus, int frequencyQuantity, string frequencyTimePeriod, int minimumHitDiceRequirement, string sizeRequirement, int power, int maximumHitDiceRequirement, int requiredStatMinimumValue, params string[] minimumAbilities)
        {
            base.RacialFeatData(name, feat, focus, frequencyQuantity, frequencyTimePeriod, minimumHitDiceRequirement, sizeRequirement, power, maximumHitDiceRequirement, requiredStatMinimumValue, minimumAbilities);
        }
    }
}
