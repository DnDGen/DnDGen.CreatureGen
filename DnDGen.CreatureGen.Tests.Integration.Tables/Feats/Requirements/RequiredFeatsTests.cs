using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Data;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Requirements
{
    [TestFixture]
    public class RequiredFeatsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Collection.RequiredFeats; }
        }

        private SpecialQualityHelper helper;

        [SetUp]
        public void Setup()
        {
            helper = new SpecialQualityHelper();
        }

        [Test]
        public void RequiredFeatsNames()
        {
            var feats = FeatConstants.All();
            var metamagic = FeatConstants.Metamagic.All();
            var monster = FeatConstants.Monster.All();
            var craft = FeatConstants.MagicItemCreation.All();
            var specialQualities = SpecialQualityTestData.GetRequirementKeys();

            var names = new List<string>();
            names.AddRange(feats);
            names.AddRange(metamagic);
            names.AddRange(monster);
            names.AddRange(craft);
            names.AddRange(specialQualities);

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(RequiredFeatsTestData), "Feats")]
        [TestCaseSource(typeof(RequiredFeatsTestData), "Metamagic")]
        [TestCaseSource(typeof(RequiredFeatsTestData), "Monster")]
        [TestCaseSource(typeof(RequiredFeatsTestData), "Craft")]
        [TestCaseSource(typeof(RequiredFeatsTestData), "SpecialQualities")]
        public void RequiredFeats(string name, params string[] requiredFeats)
        {
            AssertDistinctCollection(name, requiredFeats);
        }

        public class RequiredFeatsTestData
        {
            public static IEnumerable Feats
            {
                get
                {
                    var testCases = new Dictionary<string, string[]>();
                    var feats = FeatConstants.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new string[0];
                    }

                    testCases[FeatConstants.ArmorProficiency_Heavy] = new string[2] { FeatConstants.ArmorProficiency_Light, FeatConstants.ArmorProficiency_Medium };
                    testCases[FeatConstants.ArmorProficiency_Medium] = new string[1] { FeatConstants.ArmorProficiency_Light };
                    testCases[FeatConstants.AugmentSummoning] = new string[1] { $"{FeatConstants.SpellFocus}/{FeatConstants.Foci.Schools.Conjuration}" };
                    testCases[FeatConstants.BullRush_Improved] = new string[1] { FeatConstants.PowerAttack };
                    testCases[FeatConstants.Cleave] = new string[1] { FeatConstants.PowerAttack };
                    testCases[FeatConstants.Cleave_Great] = new string[2] { FeatConstants.Cleave, FeatConstants.PowerAttack };
                    testCases[FeatConstants.Critical_Improved] = new string[1] { GroupConstants.WeaponProficiency };
                    testCases[FeatConstants.DeflectArrows] = new string[1] { FeatConstants.UnarmedStrike_Improved };
                    testCases[FeatConstants.Diehard] = new string[1] { FeatConstants.Endurance };
                    testCases[FeatConstants.Disarm_Improved] = new string[1] { FeatConstants.CombatExpertise };
                    testCases[FeatConstants.FarShot] = new string[1] { FeatConstants.PointBlankShot };
                    testCases[FeatConstants.Feint_Improved] = new string[1] { FeatConstants.CombatExpertise };
                    testCases[FeatConstants.Grapple_Improved] = new string[1] { FeatConstants.UnarmedStrike_Improved };
                    testCases[FeatConstants.Manyshot] = new string[2] { FeatConstants.PointBlankShot, FeatConstants.RapidShot };
                    testCases[FeatConstants.Mobility] = new string[1] { FeatConstants.Dodge };
                    testCases[FeatConstants.MountedArchery] = new string[1] { FeatConstants.MountedCombat };
                    //INFO: Wild Shape is only had by Druid classes
                    //testCases[FeatConstants.NaturalSpell] = new string[1] { FeatConstants.WildShape };
                    testCases[FeatConstants.Overrun_Improved] = new string[1] { FeatConstants.PowerAttack };
                    testCases[FeatConstants.PreciseShot] = new string[1] { FeatConstants.PointBlankShot };
                    testCases[FeatConstants.PreciseShot_Improved] = new string[2] { FeatConstants.PointBlankShot, FeatConstants.PreciseShot };
                    testCases[FeatConstants.RapidReload] = new string[1] { GroupConstants.WeaponProficiency };
                    testCases[FeatConstants.RapidShot] = new string[1] { FeatConstants.PointBlankShot };
                    testCases[FeatConstants.RideByAttack] = new string[1] { FeatConstants.MountedCombat };
                    testCases[FeatConstants.ShieldBash_Improved] = new string[1] { FeatConstants.ShieldProficiency };
                    testCases[FeatConstants.ShieldProficiency_Tower] = new string[1] { FeatConstants.ShieldProficiency };
                    testCases[FeatConstants.ShotOnTheRun] = new string[3] { FeatConstants.Dodge, FeatConstants.Mobility, FeatConstants.PointBlankShot };
                    testCases[FeatConstants.SnatchArrows] = new string[2] { FeatConstants.DeflectArrows, FeatConstants.UnarmedStrike_Improved };
                    testCases[FeatConstants.SpellFocus_Greater] = new string[1] { FeatConstants.SpellFocus };
                    testCases[FeatConstants.SpellPenetration_Greater] = new string[1] { FeatConstants.SpellPenetration };
                    testCases[FeatConstants.SpiritedCharge] = new string[2] { FeatConstants.MountedCombat, FeatConstants.RideByAttack };
                    testCases[FeatConstants.SpringAttack] = new string[2] { FeatConstants.Dodge, FeatConstants.Mobility };
                    testCases[FeatConstants.StunningFist] = new string[1] { FeatConstants.UnarmedStrike_Improved };
                    testCases[FeatConstants.Sunder_Improved] = new string[1] { FeatConstants.PowerAttack };
                    testCases[FeatConstants.Trample] = new string[1] { FeatConstants.MountedCombat };
                    testCases[FeatConstants.Trip_Improved] = new string[1] { FeatConstants.CombatExpertise };
                    //INFO: No monsters can natively turn or rebuke
                    //testCases[FeatConstants.Turning_Extra] = new string[1] { FeatConstants.Turn };
                    //testCases[FeatConstants.Turning_Improved] = new string[1] { FeatConstants.Turn };
                    testCases[FeatConstants.TwoWeaponDefense] = new string[1] { FeatConstants.TwoWeaponFighting };
                    testCases[FeatConstants.TwoWeaponFighting_Greater] = new string[2] { FeatConstants.TwoWeaponFighting_Improved, FeatConstants.TwoWeaponFighting };
                    testCases[FeatConstants.TwoWeaponFighting_Improved] = new string[1] { FeatConstants.TwoWeaponFighting };
                    //INFO: Being a Fighter is a requirement for these feats
                    testCases[FeatConstants.WeaponFocus] = new string[1] { GroupConstants.WeaponProficiency };
                    //testCases[FeatConstants.WeaponFocus_Greater] = new string[1] { FeatConstants.WeaponFocus };
                    //INFO: Being a Fighter is a requirement for these feats
                    //testCases[FeatConstants.WeaponSpecialization_Greater] = new string[1] { FeatConstants.WeaponSpecialization };
                    testCases[FeatConstants.WhirlwindAttack] = new string[4] { FeatConstants.CombatExpertise, FeatConstants.Dodge, FeatConstants.Mobility, FeatConstants.SpringAttack };


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
                    var testCases = new Dictionary<string, string[]>();
                    var feats = FeatConstants.Metamagic.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new string[0];
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
                    var testCases = new Dictionary<string, string[]>();
                    var feats = FeatConstants.Monster.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new string[0];
                    }

                    testCases[FeatConstants.Monster.AwesomeBlow] = new string[2] { FeatConstants.PowerAttack, FeatConstants.BullRush_Improved };
                    testCases[FeatConstants.Monster.CraftConstruct] = new string[2] { FeatConstants.MagicItemCreation.CraftMagicArmsAndArmor, FeatConstants.MagicItemCreation.CraftWondrousItem };
                    testCases[FeatConstants.Monster.FlybyAttack_Improved] = new string[3] { FeatConstants.Dodge, FeatConstants.Mobility, FeatConstants.Monster.FlybyAttack };
                    testCases[FeatConstants.Monster.Multiattack_Improved] = new string[1] { FeatConstants.Monster.Multiattack };
                    testCases[FeatConstants.Monster.MultiweaponFighting_Greater] = new string[2] { FeatConstants.Monster.MultiweaponFighting, FeatConstants.Monster.MultiweaponFighting_Improved };
                    testCases[FeatConstants.Monster.MultiweaponFighting_Improved] = new string[1] { FeatConstants.Monster.MultiweaponFighting };

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
                    var testCases = new Dictionary<string, string[]>();
                    var feats = FeatConstants.MagicItemCreation.All();

                    foreach (var feat in feats)
                    {
                        testCases[feat] = new string[0];
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
                    var testCases = new Dictionary<string, string[]>();
                    var helper = new SpecialQualityHelper();
                    var keys = SpecialQualityTestData.GetRequirementKeys();

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Aberration, FeatConstants.ShieldProficiency, string.Empty, 0.ToString())] = new string[1] { FeatConstants.ArmorProficiency_Light };

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Elemental, FeatConstants.ShieldProficiency, string.Empty, 0.ToString())] = new string[1] { FeatConstants.ArmorProficiency_Light };

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Fey, FeatConstants.ShieldProficiency, string.Empty, 0.ToString())] = new string[1] { FeatConstants.ArmorProficiency_Light };

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Giant, FeatConstants.ShieldProficiency, string.Empty, 0.ToString())] = new string[1] { FeatConstants.ArmorProficiency_Light };

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Humanoid, FeatConstants.ShieldProficiency, string.Empty, 0.ToString())] = new string[1] { FeatConstants.ArmorProficiency_Light };

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.MonstrousHumanoid, FeatConstants.ShieldProficiency, string.Empty, 0.ToString())] = new string[1] { FeatConstants.ArmorProficiency_Light };

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Outsider, FeatConstants.ShieldProficiency, string.Empty, 0.ToString())] = new string[1] { FeatConstants.ArmorProficiency_Light };

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Undead, FeatConstants.ShieldProficiency, string.Empty, 0.ToString())] = new string[1] { FeatConstants.ArmorProficiency_Light };

                    testCases[helper.BuildKeyFromSections(CreatureConstants.Types.Subtypes.Shapechanger, FeatConstants.ShieldProficiency, string.Empty, 0.ToString())] = new string[1] { FeatConstants.ArmorProficiency_Light };

                    foreach (var key in keys)
                    {
                        testCases[key] = new string[0];
                    }

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }
        }
    }
}
