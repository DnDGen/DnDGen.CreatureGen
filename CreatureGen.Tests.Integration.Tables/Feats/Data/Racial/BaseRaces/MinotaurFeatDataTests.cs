using CreatureGen.Combats;
using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;
using System;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class MinotaurFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Minotaur); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.Darkvision,
                FeatConstants.MartialWeaponProficiency + WeaponConstants.Greataxe,
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.NaturalArmor,
                FeatConstants.NaturalWeapon,
                FeatConstants.PowerfulCharge,
                FeatConstants.NaturalCunning,
                FeatConstants.Scent,
                FeatConstants.SkillBonus + SkillConstants.Search,
                FeatConstants.SkillBonus + SkillConstants.Spot,
                FeatConstants.SkillBonus + SkillConstants.Listen,
                FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
                FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
                FeatConstants.SaveBonus + SavingThrowConstants.Will
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.Darkvision,
            FeatConstants.Darkvision,
            "",
            0,
            "",
            0,
            "",
            60,
            0, 0)]
        [TestCase(FeatConstants.MartialWeaponProficiency + WeaponConstants.Greataxe,
            FeatConstants.MartialWeaponProficiency,
            WeaponConstants.Greataxe,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.SimpleWeaponProficiency,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.Foci.All,
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
            5,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon,
            FeatConstants.NaturalWeapon,
            "Gore",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.PowerfulCharge,
            FeatConstants.PowerfulCharge,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.NaturalCunning,
            FeatConstants.NaturalCunning,
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
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Search,
            FeatConstants.SkillBonus,
            SkillConstants.Search,
            0,
            "",
            0,
            "",
            4,
            0, 0)]
        [TestCase(FeatConstants.SkillBonus + SkillConstants.Spot,
            FeatConstants.SkillBonus,
            SkillConstants.Spot,
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
            4,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Fortitude,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Fortitude,
            0,
            "",
            0,
            "",
            2,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Reflex,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Reflex,
            0,
            "",
            0,
            "",
            5,
            0, 0)]
        [TestCase(FeatConstants.SaveBonus + SavingThrowConstants.Will,
            FeatConstants.SaveBonus,
            SavingThrowConstants.Will,
            0,
            "",
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
