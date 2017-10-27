using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data.CharacterClasses.Classes
{
    [TestFixture]
    public class PaladinFeatDataTests : CharacterClassFeatDataTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSFeatData, CharacterClassConstants.Paladin); }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                FeatConstants.SimpleWeaponProficiency,
                FeatConstants.MartialWeaponProficiency,
                FeatConstants.LightArmorProficiency,
                FeatConstants.MediumArmorProficiency,
                FeatConstants.HeavyArmorProficiency,
                FeatConstants.ShieldProficiency,
                FeatConstants.AuraOfGood,
                FeatConstants.SpellLikeAbility + SpellConstants.DetectAlignment,
                FeatConstants.SmiteEvil + "1",
                FeatConstants.SmiteEvil + "2",
                FeatConstants.SmiteEvil + "3",
                FeatConstants.SmiteEvil + "4",
                FeatConstants.SmiteEvil + "5",
                FeatConstants.DivineGrace,
                FeatConstants.LayOnHands,
                FeatConstants.AuraOfCourage,
                FeatConstants.DivineHealth,
                FeatConstants.Turn,
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "1",
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "2",
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "3",
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "4",
                FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "5"
            };

            AssertCollectionNames(names);
        }

        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "1",
            FeatConstants.SpellLikeAbility,
            SpellConstants.RemoveDisease,
            1,
            "",
            FeatConstants.Frequencies.Week,
            6,
            8,
            0,
            "", true)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "2",
            FeatConstants.SpellLikeAbility,
            SpellConstants.RemoveDisease,
            2,
            "",
            FeatConstants.Frequencies.Week,
            9,
            11,
            0,
            "", true)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "3",
            FeatConstants.SpellLikeAbility,
            SpellConstants.RemoveDisease,
            3,
            "",
            FeatConstants.Frequencies.Week,
            12,
            14,
            0,
            "", true)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "4",
            FeatConstants.SpellLikeAbility,
            SpellConstants.RemoveDisease,
            4,
            "",
            FeatConstants.Frequencies.Week,
            15,
            17,
            0,
            "", true)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.RemoveDisease + "5",
            FeatConstants.SpellLikeAbility,
            SpellConstants.RemoveDisease,
            5,
            "",
            FeatConstants.Frequencies.Week,
            18,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.Turn,
            FeatConstants.Turn,
            "Undead (as cleric of level - 3)",
            3,
            AbilityConstants.Charisma,
            FeatConstants.Frequencies.Day,
            4,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.DivineHealth,
            FeatConstants.DivineHealth,
            "",
            0,
            "",
            "",
            3,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.AuraOfCourage,
            FeatConstants.AuraOfCourage,
            "",
            0,
            "",
            "",
            3,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.LayOnHands,
            FeatConstants.LayOnHands,
            "",
            0,
            "",
            "",
            2,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.DivineGrace,
            FeatConstants.DivineGrace,
            "",
            0,
            "",
            "",
            2,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.SmiteEvil + "1",
            FeatConstants.SmiteEvil,
            "",
            1,
            "",
            FeatConstants.Frequencies.Day,
            1,
            4,
            0,
            "", true)]
        [TestCase(FeatConstants.SmiteEvil + "2",
            FeatConstants.SmiteEvil,
            "",
            2,
            "",
            FeatConstants.Frequencies.Day,
            5,
            9,
            0,
            "", true)]
        [TestCase(FeatConstants.SmiteEvil + "3",
            FeatConstants.SmiteEvil,
            "",
            3,
            "",
            FeatConstants.Frequencies.Day,
            10,
            14,
            0,
            "", true)]
        [TestCase(FeatConstants.SmiteEvil + "4",
            FeatConstants.SmiteEvil,
            "",
            4,
            "",
            FeatConstants.Frequencies.Day,
            15,
            19,
            0,
            "", true)]
        [TestCase(FeatConstants.SmiteEvil + "5",
            FeatConstants.SmiteEvil,
            "",
            5,
            "",
            FeatConstants.Frequencies.Day,
            20,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.SpellLikeAbility + SpellConstants.DetectAlignment,
            FeatConstants.SpellLikeAbility,
            SpellConstants.DetectAlignment,
            0,
            "",
            FeatConstants.Frequencies.AtWill,
            1,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.AuraOfGood,
            FeatConstants.AuraOfGood,
            "",
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.SimpleWeaponProficiency,
            FeatConstants.SimpleWeaponProficiency,
            FeatConstants.Foci.All,
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.MartialWeaponProficiency,
            FeatConstants.MartialWeaponProficiency,
            FeatConstants.Foci.All,
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.LightArmorProficiency,
            FeatConstants.LightArmorProficiency,
            "",
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.MediumArmorProficiency,
            FeatConstants.MediumArmorProficiency,
            "",
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.HeavyArmorProficiency,
            FeatConstants.HeavyArmorProficiency,
            "",
            0,
            "",
            "",
            1,
            0,
            0,
            "", true)]
        [TestCase(FeatConstants.ShieldProficiency,
            FeatConstants.ShieldProficiency,
            "",
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
