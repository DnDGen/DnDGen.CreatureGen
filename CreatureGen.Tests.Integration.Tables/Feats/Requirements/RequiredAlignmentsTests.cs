using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Magic;
using CreatureGen.Selectors.Helpers;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Feats.Requirements
{
    [TestFixture]
    public class RequiredAlignmentsTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.RequiredAlignments;

        [Test]
        public void RequiredAlignmentsNames()
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

            var specialQualityData = CollectionMapper.Map(TableNameConstants.Collection.SpecialQualityData);
            var specialQualities = specialQualityData
                .Where(kvp => kvp.Value.Any())
                .SelectMany(kvp => kvp.Value.Select(k => SpecialQualityHelper.ParseData(k).Union(new[] { kvp.Key }).ToArray()))
                .Select(d => d.Last() + d[DataIndexConstants.SpecialQualityData.FeatNameIndex] + d[DataIndexConstants.SpecialQualityData.FocusIndex]);

            var names = feats.Union(metamagic).Union(monster).Union(craft).Union(specialQualities);

            return names;
        }

        [TestCaseSource(typeof(RequiredAlignmentsTestData), "Feats")]
        [TestCaseSource(typeof(RequiredAlignmentsTestData), "Metamagic")]
        [TestCaseSource(typeof(RequiredAlignmentsTestData), "Monster")]
        [TestCaseSource(typeof(RequiredAlignmentsTestData), "Craft")]
        [TestCaseSource(typeof(RequiredAlignmentsTestData), "SpecialQualities")]
        public void RequiredAlignments(string name, params string[] alignments)
        {
            AssertCollection(name, alignments);
        }

        public class RequiredAlignmentsTestData
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

                    //Creatures
                    testCases[CreatureConstants.Aasimar + FeatConstants.ArmorProficiency_Light] = new string[0];
                    testCases[CreatureConstants.Aasimar + FeatConstants.ArmorProficiency_Medium] = new string[0];
                    testCases[CreatureConstants.Aasimar + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Acid] = new string[0];
                    testCases[CreatureConstants.Aasimar + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Cold] = new string[0];
                    testCases[CreatureConstants.Aasimar + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Electricity] = new string[0];
                    testCases[CreatureConstants.Aasimar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Daylight] = new string[0];

                    testCases[CreatureConstants.Aboleth + FeatConstants.SpecialQualities.MucusCloud] = new string[0];
                    testCases[CreatureConstants.Aboleth + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HypnoticPattern] = new string[0];
                    testCases[CreatureConstants.Aboleth + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.IllusoryWall] = new string[0];
                    testCases[CreatureConstants.Aboleth + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.MirageArcana] = new string[0];
                    testCases[CreatureConstants.Aboleth + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.PersistentImage] = new string[0];
                    testCases[CreatureConstants.Aboleth + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.ProgrammedImage] = new string[0];
                    testCases[CreatureConstants.Aboleth + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.ProjectImage] = new string[0];

                    testCases[CreatureConstants.Basilisk_AbyssalGreater + FeatConstants.SpecialQualities.DamageReduction + "Vulnerable to magic weapons"] = new string[0];
                    testCases[CreatureConstants.Basilisk_AbyssalGreater + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Cold] = new string[0];
                    testCases[CreatureConstants.Basilisk_AbyssalGreater + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Fire] = new string[0];
                    testCases[CreatureConstants.Basilisk_AbyssalGreater + FeatConstants.SpecialQualities.SpellResistance] = new string[0];

                    //Creature Types
                    testCases[CreatureConstants.Types.Aberration + FeatConstants.SpecialQualities.Darkvision] = new string[0];
                    testCases[CreatureConstants.Types.Aberration + FeatConstants.WeaponProficiency_Simple + GroupConstants.All] = new string[0];
                    testCases[CreatureConstants.Types.Aberration + FeatConstants.ShieldProficiency] = new string[0];

                    //Creature Subtypes

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }
        }
    }
}
