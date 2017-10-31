using CreatureGen.Tables;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using NUnit.Framework;
using System;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.Racial.BaseRaces
{
    [TestFixture]
    public class LizardfolkFeatDataTests : RacialFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, CreatureConstants.Lizardfolk); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.NaturalArmor,
                FeatConstants.HoldBreath,
                FeatConstants.NaturalWeapon + "Claw",
                FeatConstants.NaturalWeapon + "Bite"
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SimpleWeaponProficiency,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.Foci.All,
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.ShieldProficiency,
            FeatConstants.ShieldProficiency,
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
            5,
            0, 0)]
        [TestCase(FeatConstants.HoldBreath,
            FeatConstants.HoldBreath,
            "",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon + "Claw",
            FeatConstants.NaturalWeapon,
            "Claw (x2)",
            0,
            "",
            0,
            "",
            0,
            0, 0)]
        [TestCase(FeatConstants.NaturalWeapon + "Bite",
            FeatConstants.NaturalWeapon,
            "Bite",
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
