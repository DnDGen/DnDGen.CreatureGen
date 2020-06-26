using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Data;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements
{
    [TestFixture]
    public class FeatAbilityRequirementsTests : TypesAndAmountsTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.TypeAndAmount.FeatAbilityRequirements;
            }
        }

        private SpecialQualityHelper helper;

        [SetUp]
        public void Setup()
        {
            helper = new SpecialQualityHelper();
        }

        [Test]
        public void FeatAbilityRequirementsNames()
        {
            var names = GetNames();

            AssertCollectionNames(names);
        }

        private IEnumerable<string> GetNames()
        {
            var feats = FeatConstants.All();
            var metamagic = FeatConstants.Metamagic.All();
            var monster = FeatConstants.Monster.All();
            var craft = FeatConstants.MagicItemCreation.All();
            var specialQualities = SpecialQualityTestData.GetRequirementKeys();
            var featsWithFoci = AbilityRequirementsTestData.GetFeatsWithFoci();

            var names = new List<string>();
            names.AddRange(feats);
            names.AddRange(metamagic);
            names.AddRange(monster);
            names.AddRange(craft);
            names.AddRange(specialQualities);
            names.AddRange(featsWithFoci);

            return names;
        }

        [TestCaseSource(typeof(AbilityRequirementsTestData), "Feats")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "Metamagic")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "Monster")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "Craft")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "SpecialQualities")]
        [TestCaseSource(typeof(AbilityRequirementsTestData), "FeatsWithFoci")]
        public void AbilityRequirements(string name, Dictionary<string, int> typesAndAmounts)
        {
            AssertTypesAndAmounts(name, typesAndAmounts);
        }

        public class AbilityRequirementsTestData
        {
            public static IEnumerable Feats
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = FeatConstants.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    testCases[FeatConstants.BullRush_Improved][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.Cleave][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.Cleave_Great][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.CombatExpertise][AbilityConstants.Intelligence] = 13;
                    testCases[FeatConstants.DeflectArrows][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.Disarm_Improved][AbilityConstants.Intelligence] = 13;
                    testCases[FeatConstants.Dodge][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.Feint_Improved][AbilityConstants.Intelligence] = 13;
                    testCases[FeatConstants.Grapple_Improved][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.Manyshot][AbilityConstants.Dexterity] = 17;
                    testCases[FeatConstants.Mobility][AbilityConstants.Dexterity] = 13;
                    //INFO: Natural Spell is only available to Druids
                    //testCases[FeatConstants.NaturalSpell][AbilityConstants.Wisdom] = 13;
                    testCases[FeatConstants.Overrun_Improved][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.PowerAttack][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.PreciseShot_Improved][AbilityConstants.Dexterity] = 19;
                    testCases[FeatConstants.RapidShot][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.ShotOnTheRun][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.SnatchArrows][AbilityConstants.Dexterity] = 15;
                    testCases[FeatConstants.SpringAttack][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.StunningFist][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.StunningFist][AbilityConstants.Wisdom] = 13;
                    testCases[FeatConstants.Sunder_Improved][AbilityConstants.Strength] = 13;
                    testCases[FeatConstants.Trip_Improved][AbilityConstants.Intelligence] = 13;
                    testCases[FeatConstants.TwoWeaponDefense][AbilityConstants.Dexterity] = 15;
                    testCases[FeatConstants.TwoWeaponFighting][AbilityConstants.Dexterity] = 15;
                    testCases[FeatConstants.TwoWeaponFighting_Greater][AbilityConstants.Dexterity] = 19;
                    testCases[FeatConstants.TwoWeaponFighting_Improved][AbilityConstants.Dexterity] = 17;
                    testCases[FeatConstants.WhirlwindAttack][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.WhirlwindAttack][AbilityConstants.Intelligence] = 13;

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable Metamagic
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = FeatConstants.Metamagic.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable Monster
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = FeatConstants.Monster.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    testCases[FeatConstants.Monster.AwesomeBlow][AbilityConstants.Strength] = 25;
                    testCases[FeatConstants.Monster.MultiweaponFighting][AbilityConstants.Dexterity] = 13;
                    testCases[FeatConstants.Monster.MultiweaponFighting_Greater][AbilityConstants.Dexterity] = 19;
                    testCases[FeatConstants.Monster.MultiweaponFighting_Improved][AbilityConstants.Dexterity] = 15;
                    testCases[FeatConstants.Monster.NaturalArmor_Improved][AbilityConstants.Constitution] = 13;

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable Craft
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var feats = FeatConstants.MagicItemCreation.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new Dictionary<string, int>();
                    }

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable SpecialQualities
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var helper = new SpecialQualityHelper();
                    var keys = SpecialQualityTestData.GetRequirementKeys();

                    foreach (var key in keys)
                    {
                        testCases[key] = new Dictionary<string, int>();
                    }

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights, 0.ToString())][AbilityConstants.Charisma] = 10;
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound, 0.ToString())][AbilityConstants.Charisma] = 10;
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Prestidigitation, 0.ToString())][AbilityConstants.Charisma] = 10;

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights, 0.ToString())][AbilityConstants.Charisma] = 10;
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound, 0.ToString())][AbilityConstants.Charisma] = 10;
                    testCases[helper.BuildKeyFromSections(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Prestidigitation, 0.ToString())][AbilityConstants.Charisma] = 10;

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable<string> GetFeatsWithFoci()
            {
                var featsWithFoci = new List<string>();

                var simpleWeapons = WeaponConstants.GetAllSimple(false, false);
                foreach (var weapon in simpleWeapons)
                {
                    featsWithFoci.Add($"{FeatConstants.WeaponProficiency_Simple}/{weapon}");
                }

                var martialWeapons = WeaponConstants.GetAllMartial(false, false);
                foreach (var weapon in martialWeapons)
                {
                    featsWithFoci.Add($"{FeatConstants.WeaponProficiency_Martial}/{weapon}");
                }

                var exoticWeapons = WeaponConstants.GetAllExotic(false, false);
                foreach (var weapon in exoticWeapons)
                {
                    featsWithFoci.Add($"{FeatConstants.WeaponProficiency_Exotic}/{weapon}");
                }

                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Appraise}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Balance}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Bluff}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Climb}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Concentration}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Craft}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Cloth)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Glass)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Leather)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Stonemasonry)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.DecipherScript}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Diplomacy}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.DisableDevice}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Disguise}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.EscapeArtist}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Forgery}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.GatherInformation}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.HandleAnimal}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Heal}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Hide}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Intimidate}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Jump}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Knowledge}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Religion)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Listen}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.MoveSilently}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.OpenLock}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Perform}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Act)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Comedy)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Dance)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.KeyboardInstruments)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Oratory)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.PercussionInstruments)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Sing)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.StringInstruments)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.WindInstruments)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Profession}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Clerk)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coiffeur)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cook)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Laborer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Launderer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Nursemaid)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Polisher)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Porter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Scribe)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Ride}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Search}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.SenseMotive}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.SleightOfHand}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Spellcraft}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Spot}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Survival}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Swim}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.Tumble}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.UseMagicDevice}");
                featsWithFoci.Add($"{FeatConstants.SkillFocus}/{SkillConstants.UseRope}");

                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Boar}");
                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Rat}");
                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Raven}");
                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Spider_Monstrous_Hunter_Small}");
                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Spider_Monstrous_Hunter_Medium}");
                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Spider_Monstrous_WebSpinner_Small}");
                featsWithFoci.Add($"{FeatConstants.SpecialQualities.AlternateForm}/{CreatureConstants.Spider_Monstrous_WebSpinner_Medium}");

                return featsWithFoci;
            }

            public static IEnumerable FeatsWithFoci
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();

                    //Prep feats with foci
                    var featsWithFoci = GetFeatsWithFoci();
                    foreach (var featWithFoci in featsWithFoci)
                    {
                        testCases[featWithFoci] = new Dictionary<string, int>();
                    }

                    //Set requirements
                    testCases[$"{FeatConstants.WeaponProficiency_Exotic}/{WeaponConstants.BastardSword}"][AbilityConstants.Strength] = 13;
                    testCases[$"{FeatConstants.WeaponProficiency_Exotic}/{WeaponConstants.DwarvenWaraxe}"][AbilityConstants.Strength] = 13;

                    //Return test cases
                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }
        }
    }
}
