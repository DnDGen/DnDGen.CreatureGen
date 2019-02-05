using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Magic;
using CreatureGen.Selectors.Helpers;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TreasureGen.Items;

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

                    testCases[CreatureConstants.Achaierai + FeatConstants.SpecialQualities.SpellResistance] = new string[0];

                    testCases[CreatureConstants.Allip + FeatConstants.SpecialQualities.TurnResistance] = new string[0];

                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.ChangeShape + "Small or Medium Humanoid"] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.DamageReduction + "Vulnerable to evil"] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.WeaponProficiency_Simple + WeaponConstants.HeavyMace] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellResistance] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Aid] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.BladeBarrier] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.ContinualFlame] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DetectAlignment] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DiscernLies] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DispelMagic] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HolySmite] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Invisibility + " (self only)"] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.RemoveCurse] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.RemoveDisease] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.RemoveFear] = new string[0];
                    testCases[CreatureConstants.Angel_AstralDeva + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.SeeInvisibility] = new string[0];

                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.ChangeShape + "Small or Medium Humanoid"] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.DamageReduction + "Vulnerable to evil"] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.WeaponProficiency_Martial + WeaponConstants.Greatsword] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.Regeneration + "Does not regenerate evil damage"] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellResistance] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.BladeBarrier] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.CharmMonster_Mass] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.ContinualFlame] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.CureInflictLightWounds] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DetectAlignment] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DetectSnaresAndPits] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DiscernLies] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DispelAlignment] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DispelMagic] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Earthquake] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.FlameStrike] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HealHarm] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HolyAura] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HolySmite] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HolyWord] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Invisibility + " (self only)"] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.PlaneShift] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.PowerWordStun] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.RaiseDead] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.RemoveCurse] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.RemoveDisease] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.RemoveFear] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Restoration_Greater] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Restoration_Lesser] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.SeeInvisibility] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.SpeakWithDead] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.TrueSeeing] = new string[0];
                    testCases[CreatureConstants.Angel_Planetar + FeatConstants.SpecialQualities.UncannyDodge] = new string[0];

                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.ChangeShape + "Small or Medium Humanoid"] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.WeaponProficiency_Martial + WeaponConstants.Greatsword] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.Regeneration + "Does not regenerate evil damage"] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellResistance] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Aid] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.BladeBarrier] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.ContinualFlame] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DetectAlignment] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DiscernLies] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.DispelMagic] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HealHarm] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HolyAura] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HolySmite] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.HolyWord] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Invisibility + " (self only)"] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.PlaneShift] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.RemoveCurse] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.RemoveDisease] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.RemoveFear] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.SeeInvisibility] = new string[0];
                    testCases[CreatureConstants.Angel_Solar + FeatConstants.SpecialQualities.UncannyDodge] = new string[0];

                    testCases[CreatureConstants.Arrowhawk_Adult + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Cold] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Adult + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Fire] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Adult + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Acid] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Adult + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Electricity] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Adult + FeatConstants.SpecialQualities.Immunity + "Poison"] = new string[0];

                    testCases[CreatureConstants.Arrowhawk_Elder + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Cold] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Elder + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Fire] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Elder + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Acid] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Elder + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Electricity] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Elder + FeatConstants.SpecialQualities.Immunity + "Poison"] = new string[0];

                    testCases[CreatureConstants.Arrowhawk_Juvenile + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Cold] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Juvenile + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Fire] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Juvenile + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Acid] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Juvenile + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Electricity] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Juvenile + FeatConstants.SpecialQualities.Immunity + "Poison"] = new string[0];

                    testCases[CreatureConstants.Basilisk_AbyssalGreater + FeatConstants.SpecialQualities.DamageReduction + "Vulnerable to magic weapons"] = new string[0];
                    testCases[CreatureConstants.Basilisk_AbyssalGreater + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Cold] = new string[0];
                    testCases[CreatureConstants.Basilisk_AbyssalGreater + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Fire] = new string[0];
                    testCases[CreatureConstants.Basilisk_AbyssalGreater + FeatConstants.SpecialQualities.SpellResistance] = new string[0];

                    testCases[CreatureConstants.Elemental_Air_Elder + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Air_Greater + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Air_Huge + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Air_Large + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Earth_Elder + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Earth_Greater + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Earth_Huge + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Earth_Large + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Fire_Elder + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Fire_Greater + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Fire_Huge + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Fire_Large + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Water_Elder + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Water_Greater + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Water_Huge + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Elemental_Water_Large + FeatConstants.SpecialQualities.DamageReduction + "No physical vulnerabilities"] = new string[0];

                    testCases[CreatureConstants.Mephit_Air + FeatConstants.SpecialQualities.DamageReduction + "Vulnerable to magic weapons"] = new string[0];
                    testCases[CreatureConstants.Mephit_Air + FeatConstants.SpecialQualities.FastHealing + "Exposed to moving air"] = new string[0];
                    testCases[CreatureConstants.Mephit_Air + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Blur] = new string[0];
                    testCases[CreatureConstants.Mephit_Air + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.GustOfWind] = new string[0];

                    testCases[CreatureConstants.Tojanida_Adult + FeatConstants.SpecialQualities.AllAroundVision] = new string[0];
                    testCases[CreatureConstants.Tojanida_Adult + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Electricity] = new string[0];
                    testCases[CreatureConstants.Tojanida_Adult + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Fire] = new string[0];
                    testCases[CreatureConstants.Tojanida_Adult + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Acid] = new string[0];
                    testCases[CreatureConstants.Tojanida_Adult + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Cold] = new string[0];
                    testCases[CreatureConstants.Tojanida_Adult + FeatConstants.SpecialQualities.Immunity + "Poison"] = new string[0];

                    testCases[CreatureConstants.Tojanida_Elder + FeatConstants.SpecialQualities.AllAroundVision] = new string[0];
                    testCases[CreatureConstants.Tojanida_Elder + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Electricity] = new string[0];
                    testCases[CreatureConstants.Tojanida_Elder + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Fire] = new string[0];
                    testCases[CreatureConstants.Tojanida_Elder + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Acid] = new string[0];
                    testCases[CreatureConstants.Tojanida_Elder + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Cold] = new string[0];
                    testCases[CreatureConstants.Tojanida_Elder + FeatConstants.SpecialQualities.Immunity + "Poison"] = new string[0];

                    testCases[CreatureConstants.Tojanida_Juvenile + FeatConstants.SpecialQualities.AllAroundVision] = new string[0];
                    testCases[CreatureConstants.Tojanida_Juvenile + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Electricity] = new string[0];
                    testCases[CreatureConstants.Tojanida_Juvenile + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Fire] = new string[0];
                    testCases[CreatureConstants.Tojanida_Juvenile + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Acid] = new string[0];
                    testCases[CreatureConstants.Tojanida_Juvenile + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Cold] = new string[0];
                    testCases[CreatureConstants.Tojanida_Juvenile + FeatConstants.SpecialQualities.Immunity + "Poison"] = new string[0];

                    //Creature Types
                    testCases[CreatureConstants.Types.Aberration + FeatConstants.SpecialQualities.Darkvision] = new string[0];
                    testCases[CreatureConstants.Types.Aberration + FeatConstants.WeaponProficiency_Simple + GroupConstants.All] = new string[0];
                    testCases[CreatureConstants.Types.Aberration + FeatConstants.ShieldProficiency] = new string[0];

                    //Creature Subtypes
                    testCases[CreatureConstants.Types.Subtypes.Angel + FeatConstants.SpecialQualities.Darkvision] = new string[0];
                    testCases[CreatureConstants.Types.Subtypes.Angel + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Electricity] = new string[0];
                    testCases[CreatureConstants.Types.Subtypes.Angel + FeatConstants.SpecialQualities.EnergyResistance + FeatConstants.Foci.Elements.Fire] = new string[0];
                    testCases[CreatureConstants.Types.Subtypes.Angel + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Acid] = new string[0];
                    testCases[CreatureConstants.Types.Subtypes.Angel + FeatConstants.SpecialQualities.Immunity + FeatConstants.Foci.Elements.Cold] = new string[0];
                    testCases[CreatureConstants.Types.Subtypes.Angel + FeatConstants.SpecialQualities.Immunity + "Petrification"] = new string[0];
                    testCases[CreatureConstants.Types.Subtypes.Angel + FeatConstants.SpecialQualities.LowLightVision] = new string[0];
                    testCases[CreatureConstants.Types.Subtypes.Angel + FeatConstants.SpecialQualities.ProtectiveAura] = new string[0];
                    testCases[CreatureConstants.Types.Subtypes.Angel + FeatConstants.SpecialQualities.SpellLikeAbility + SpellConstants.Tongues] = new string[0];

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }
        }
    }
}
