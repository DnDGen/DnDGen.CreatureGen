using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using NUnit.Framework;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.CharacterClasses.Classes
{
    [TestFixture]
    public class WizardFeatDataTests : CharacterClassFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.ScribeScroll,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Club,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Dagger,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.HeavyCrossbow,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.LightCrossbow,
                FeatConstants.SimpleWeaponProficiency + WeaponConstants.Quarterstaff
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.ScribeScroll,
            FeatConstants.ScribeScroll,
            "",
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.SimpleWeaponProficiency + WeaponConstants.Club,
            FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.Club,
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.SimpleWeaponProficiency + WeaponConstants.Dagger,
            FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.Dagger,
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.SimpleWeaponProficiency + WeaponConstants.HeavyCrossbow,
            FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.HeavyCrossbow,
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.SimpleWeaponProficiency + WeaponConstants.LightCrossbow,
            FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.LightCrossbow,
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.SimpleWeaponProficiency + WeaponConstants.Quarterstaff,
            FeatConstants.SimpleWeaponProficiency,
            WeaponConstants.Quarterstaff,
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        public override void ClassFeatData(string name, string feat, string focusType, int frequencyQuantity, string frequencyQuantityStat, string frequencyTimePeriod, int minimumLevel, int maximumLevel, int strength, string sizeRequirement, bool allowFocusOfAll)
        {
            base.ClassFeatData(name, feat, focusType, frequencyQuantity, frequencyQuantityStat, frequencyTimePeriod, minimumLevel, maximumLevel, strength, sizeRequirement, allowFocusOfAll);
        }
    }
}
