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
                .SelectMany(kvp => kvp.Value.Select(v => SpecialQualityHelper.BuildRequirementKey(kvp.Key, v)));

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
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aasimar, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aasimar, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aasimar, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aasimar, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aasimar, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aasimar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Daylight)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.MucusCloud, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HypnoticPattern)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.IllusoryWall)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirageArcana)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ProgrammedImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aboleth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ProjectImage)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Achaierai, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Allip, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.WeaponProficiency_Simple, WeaponConstants.HeavyMace)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.ChangeShape, "Small or Medium Humanoid")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Aid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BladeBarrier)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DiscernLies)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HolySmite)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + " (self only)")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveCurse)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveDisease)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveFear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_AstralDeva, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.ChangeShape, "Small or Medium Humanoid")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.Regeneration, "Does not regenerate evil damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BladeBarrier)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster_Mass)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectSnaresAndPits)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DiscernLies)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Earthquake)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FlameStrike)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HolySmite)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + " (self only)")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordStun)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RaiseDead)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveCurse)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveDisease)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveFear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Restoration_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Restoration_Lesser)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithDead)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WavesOfExhaustion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Planetar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WavesOfFatigue)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.ChangeShape, "Small or Medium Humanoid")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to epic evil")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.Regeneration, "Does not regenerate evil damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Aid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimateObjects)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BladeBarrier)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster_Mass)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Commune)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectSnaresAndPits)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionalAnchor)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DiscernLies)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Earthquake)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HealHarm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HolySmite)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Imprisonment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + " (self only)")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Permanency)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordBlind)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordKill)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordStun)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PrismaticSpray)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveCurse)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveDisease)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveFear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ResistEnergy)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Restoration_Lesser)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Restoration_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Resurrection)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithDead)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SummonMonsterVII)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WavesOfExhaustion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WavesOfFatigue)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Angel_Solar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Wish)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ankheg, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Annis, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to bludgeoning weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Annis, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Annis, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Annis, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ape, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ape_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aranea, FeatConstants.SpecialQualities.ChangeShape, "Small or Medium humanoid; or Medium spider-human hybrid (like a Lycanthrope)")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Elder, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Juvenile, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.Camouflage, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.AssassinVine, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil or silver weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.LayOnHands, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Aid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur + ": self only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Command)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldPerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Light)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LightningBolt)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment + ": self only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicMissile)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Avoral, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Azer, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.ProtectiveSlime, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Babau, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Baboon, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Badger, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Badger, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Badger, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Badger_Dire, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Badger_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.Whip)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good, cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.FlamingBody, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blasphemy)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DominateMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FireStorm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Implosion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Insanity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordStun)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Telekinesis)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyAura)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Balor, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.BarbedDefense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldPerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.OrdersWrath)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ScorchingRay + ": 2 rays only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BarbedDevil_Hamatula, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest, FeatConstants.SpecialQualities.ChangeShape, "Goblin or wolf")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blink)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CrushingDespair)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Misdirection)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PassWithoutTrace + ": in wolf form")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Rage)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.ChangeShape, "Goblin or wolf")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blink)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CrushingDespair)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Misdirection)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PassWithoutTrace + ": in wolf form")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Rage)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bat, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bat_Swarm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Basilisk_AbyssalGreater, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Basilisk_AbyssalGreater, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Basilisk_AbyssalGreater, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Basilisk_AbyssalGreater, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bear_Black, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bear_Brown, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bear_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bear_Polar, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Glaive)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or silver weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BeardedDevil_Barbazu, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bebilith, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bebilith, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bebilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift + ": self only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bebilith, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Behir, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Behir, FeatConstants.SpecialQualities.Immunity, "Tripping")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Behir, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Beholder, FeatConstants.Alertness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Beholder, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Beholder, FeatConstants.SpecialQualities.AntimagicCone, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Beholder, FeatConstants.SpecialQualities.Flight, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Beholder_Gauth, FeatConstants.Alertness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Beholder_Gauth, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Beholder_Gauth, FeatConstants.SpecialQualities.Flight, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Belker, FeatConstants.SpecialQualities.SmokeForm, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bison, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BlackPudding, FeatConstants.SpecialQualities.Split, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BlackPudding_Elder, FeatConstants.SpecialQualities.Split, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BlinkDog, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BlinkDog, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BlinkDog, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blink)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BlinkDog, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Boar, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Boar_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bodak, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bodak, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bodak, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bodak, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bodak, FeatConstants.SpecialQualities.Vulnerability, "Sunlight")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionalAnchor)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fly)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfIce)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.BoneDevil_Osyluth, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Scimitar)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.AlternateForm, "Humanoid or whirlwind form")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron or evil weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmPerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictSeriousWounds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LightningBolt)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirrorImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Tongues)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bralani, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WindWall)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bugbear, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bugbear, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bugbear, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Adult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.InsectPlague)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Ancient, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.CharmReptiles, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.InsectPlague)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Juvenile, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Old, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryOld, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.CorruptWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.InsectPlague)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_Young, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Black_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HallucinatoryTerrain)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Veil)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HallucinatoryTerrain)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirageArcana)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Veil)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Juvenile, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HallucinatoryTerrain)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HallucinatoryTerrain)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HallucinatoryTerrain)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Veil)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.CreateDestroyWater, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.SoundImitation, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Blue_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 60 ft.")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 100 ft.")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 120 ft.")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SummonMonsterVII + ": one Djinni")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 40 ft.")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 70 ft.")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 80 ft.")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 90 ft.")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_VeryYoung, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 110 ft.")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Wyrmling, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_Young, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EndureElements + ": radius 50 ft.")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Brass_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Adult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Ancient, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Juvenile, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Old, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryOld, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_Young, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Bronze_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Juvenile, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Young, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.InsectPlague)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0]; ;
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Air_Elder, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Air_Greater, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Air_Huge, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Air_Large, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Elder, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Greater, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Huge, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Large, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Elder, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Greater, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Huge, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Large, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Water_Elder, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Water_Greater, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Water_Huge, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Water_Large, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.Net)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Trident)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Longspear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Shortspear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Spear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.SpecialQualities.Gills, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.SpecialQualities.LowLightVision_Superior, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.FastHealing, "Exposed to moving air")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Average, FeatConstants.Monster.Multiattack, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Average, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Flamebrother, FeatConstants.Monster.Multiattack, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Noble, FeatConstants.Monster.Multiattack, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Red, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Adult, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Elder, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tojanida_Juvenile, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Whale_Baleen, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Whale_Baleen, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Whale_Cachalot, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Whale_Cachalot, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Whale_Orca, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Whale_Orca, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Average, FeatConstants.Cleave, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to bludgeoning weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Average, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Elder, FeatConstants.Cleave, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to bludgeoning weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Elder, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.AllAroundVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to bludgeoning weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xorn_Minor, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    //Creature Types
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Aberration, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Aberration, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Aberration, FeatConstants.ShieldProficiency, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Animal, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    //Creature Subtypes
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.ProtectiveAura, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Angel, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Tongues)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.AuraOfMenace, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Archon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Tongues)] = new string[0];

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }
        }
    }
}
