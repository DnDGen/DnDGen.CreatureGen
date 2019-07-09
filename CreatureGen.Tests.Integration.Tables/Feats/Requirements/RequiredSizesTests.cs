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
    public class RequiredSizesTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.RequiredSizes;

        [Test]
        public void RequiredSizesNames()
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

        [TestCaseSource(typeof(RequiredSizesTestData), "Feats")]
        [TestCaseSource(typeof(RequiredSizesTestData), "Metamagic")]
        [TestCaseSource(typeof(RequiredSizesTestData), "Monster")]
        [TestCaseSource(typeof(RequiredSizesTestData), "Craft")]
        [TestCaseSource(typeof(RequiredSizesTestData), "CreatureSpecialQualities")]
        [TestCaseSource(typeof(RequiredSizesTestData), "CreatureTypeSpecialQualities")]
        [TestCaseSource(typeof(RequiredSizesTestData), "CreatureSubtypeSpecialQualities")]
        public void RequiredSizes(string name, params string[] sizes)
        {
            AssertCollection(name, sizes);
        }

        public class RequiredSizesTestData
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

                    testCases[FeatConstants.Monster.AwesomeBlow] = new[] { SizeConstants.Large, SizeConstants.Huge, SizeConstants.Gargantuan, SizeConstants.Colossal };
                    testCases[FeatConstants.Monster.Snatch] = new[] { SizeConstants.Huge, SizeConstants.Gargantuan, SizeConstants.Colossal };

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

                    testCases[CreatureConstants.Aasimar + FeatConstants.ArmorProficiency_Light] = new string[0];
                    testCases[CreatureConstants.Aasimar + FeatConstants.ArmorProficiency_Medium] = new string[0];
                    testCases[CreatureConstants.Aasimar + FeatConstants.SpecialQualities.EnergyResistance] = new string[0];
                    testCases[CreatureConstants.Aasimar + FeatConstants.SpecialQualities.SpellLikeAbility] = new string[0];

                    testCases[CreatureConstants.Aboleth + FeatConstants.SpecialQualities.MucusCloud] = new string[0];
                    testCases[CreatureConstants.Aboleth + FeatConstants.SpecialQualities.SpellLikeAbility] = new string[0];

                    testCases[CreatureConstants.Basilisk_AbyssalGreater + FeatConstants.SpecialQualities.DamageReduction] = new string[0];

                    testCases[CreatureConstants.Types.Aberration + FeatConstants.WeaponProficiency_Simple] = new string[0];
                    testCases[CreatureConstants.Types.Aberration + FeatConstants.ShieldProficiency] = new string[0];

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable CreatureSpecialQualities
            {
                get
                {
                    var testCases = new Dictionary<string, string[]>();

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

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ant_Giant_Queen, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ant_Giant_Queen, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ant_Giant_Soldier, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ant_Giant_Soldier, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ant_Giant_Worker, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ant_Giant_Worker, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ape, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ape_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Aranea, FeatConstants.SpecialQualities.ChangeShape, "Small or Medium humanoid; or Medium spider-human hybrid (like a Lycanthrope)")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Adult, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Arrowhawk_Elder, FeatConstants.WeaponFocus, "Bite")] = new string[0];
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
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BullsStrength_Mass)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CrushingDespair)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EnlargePerson_Mass)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.InvisibilitySphere)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Misdirection)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PassWithoutTrace + ": in wolf form")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Barghest_Greater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Rage)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bat, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bat_Dire, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];

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
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bugbear, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Morningstar)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bugbear, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bugbear, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bulette, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Bulette, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Camel_Bactrian, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Camel_Dromedary, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.CarrionCrawler, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cat, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Centaur, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Centaur, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Centaur, FeatConstants.MountedCombat, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Centipede_Monstrous_Large, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Centipede_Monstrous_Medium, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Centipede_Monstrous_Small, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Centipede_Monstrous_Tiny, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Centipede_Swarm, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Centipede_Swarm, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ChainDevil_Kyton, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.SpikedChain)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ChainDevil_Kyton, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or silver weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ChainDevil_Kyton, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ChainDevil_Kyton, FeatConstants.SpecialQualities.Regeneration, "Does not regenerate damage from silver weapons or good-aligned damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ChainDevil_Kyton, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ChaosBeast, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ChaosBeast, FeatConstants.SpecialQualities.Immunity, "Transformation")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ChaosBeast, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cheetah, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cheetah, FeatConstants.SpecialQualities.Sprint, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Chimera, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Choker, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Choker, FeatConstants.SpecialQualities.Quickness, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Chuul, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Chuul, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cloaker, FeatConstants.SpecialQualities.ShadowShift, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cockatrice, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Couatl, FeatConstants.SpecialQualities.ChangeShape, "Any Small or Medium humanoid")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Couatl, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EtherealJaunt)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Couatl, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Crocodile, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Crocodile_Giant, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_5Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_5Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_5Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_6Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_6Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_6Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_7Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_7Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_7Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_8Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_8Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_8Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_9Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_9Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_9Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_10Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_10Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_10Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_11Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_11Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_11Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_12Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_12Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Cryohydra_12Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Darkmantle, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Deinonychus, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Delver, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Delver, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Delver, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro, FeatConstants.SpecialQualities.Madness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Daze)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SoundBurst)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro, FeatConstants.SpecialQualities.Vulnerability, "Sunlight")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro_Sane, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro_Sane, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Daze)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SoundBurst)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Derro_Sane, FeatConstants.SpecialQualities.Vulnerability, "Sunlight")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Immunity, "Gaze attacks")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Immunity, "Visual effects")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Immunity, "Illusions")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Destrachan, FeatConstants.SpecialQualities.Immunity, "Attacks that rely on sight")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellDeflection, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlUndead)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhoulTouch)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlanarAlly_Lesser)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RayOfEnfeeblement)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpectralHand)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Devourer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Digester, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Digester, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.DisplacerBeast, FeatConstants.SpecialQualities.Displacement, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.DisplacerBeast_PackLord, FeatConstants.SpecialQualities.Displacement, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateWater + ": creates wine instead of water")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GaseousForm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorCreation + ": created vegetable matter is permanent")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WindWalk)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni_Noble, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateWater + ": creates wine instead of water")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GaseousForm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorCreation + ": created vegetable matter is permanent")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WindWalk)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Wish + ": 3 wishes to any non-genie who captures it")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Djinni_Noble, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dog, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dog, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dog_Riding, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dog_Riding, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Donkey, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Doppelganger, FeatConstants.SpecialQualities.ChangeShape, "Any Small or Medium Humanoid")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Doppelganger, FeatConstants.SpecialQualities.Immunity, "Charm effects")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Doppelganger, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Doppelganger, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];

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
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteMudToRock)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteRockToMud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfStone)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MoveEarth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteMudToRock)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteRockToMud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfStone)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteMudToRock)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteRockToMud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteMudToRock)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteRockToMud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_VeryYoung, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteMudToRock)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteRockToMud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfStone)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Wyrmling, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_Young, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Copper_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Adult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Sunburst)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Ancient, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Foresight)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Sunburst)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_GreatWyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Juvenile, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Old, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryOld, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_VeryYoung, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.LuckBonus, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Sunburst)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Wyrmling, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_Young, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Bless)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Gold_YoungAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Adult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DominatePerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Ancient, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CommandPlants)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DominatePerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
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
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_MatureAdult, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Old, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
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
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DominatePerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlantGrowth)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Green_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
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
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FindThePath)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DiscernLocation)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FindThePath)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FindThePath)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

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
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Red_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ReverseGravity)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.AlternateForm, "Animal or Humanoid form of Medium size or smaller")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Cloudwalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_Silver_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Adult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.FreezingFog, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Ancient, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfIce)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.FreezingFog, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_GreatWyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfIce)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Juvenile, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_MatureAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.FreezingFog, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Old, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.FreezingFog, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryOld, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_VeryYoung, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.FreezingFog, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfIce)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Wyrmling, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_Young, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0]; ;
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.Icewalking, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.KeenSenses, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragon_White_YoungAdult, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.DragonTurtle, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.DragonTurtle, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dragonne, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dretch, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dretch, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dretch, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dretch, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dretch, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dretch, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dretch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Scare)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dretch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StinkingCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dretch, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Drider, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Drider, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ClairaudienceClairvoyance)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FaerieFire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Drider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dryad, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dryad, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dryad, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmPerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeepSlumber)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithPlants)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TreeShape)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dryad, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TreeStride)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dryad, FeatConstants.SpecialQualities.TreeDependent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dryad, FeatConstants.SpecialQualities.WildEmpathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Deep, FeatConstants.WeaponProficiency_Martial, WeaponConstants.DwarvenWaraxe)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Deep, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Deep, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Deep, FeatConstants.SpecialQualities.LightSensitivity, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Deep, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.DwarvenUrgrosh)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Deep, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.DwarvenWaraxe)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Duergar, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Warhammer)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Duergar, FeatConstants.WeaponProficiency_Simple, WeaponConstants.LightCrossbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.LightSensitivity, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.Immunity, "Phantasms")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EnlargePerson + ": only self + carried items")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Duergar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": only self + carried items")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Hill, FeatConstants.WeaponProficiency_Martial, WeaponConstants.DwarvenWaraxe)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Hill, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Hill, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Hill, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.DwarvenUrgrosh)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Hill, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.DwarvenWaraxe)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Mountain, FeatConstants.WeaponProficiency_Martial, WeaponConstants.DwarvenWaraxe)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Mountain, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Mountain, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Mountain, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.DwarvenUrgrosh)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Dwarf_Mountain, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.DwarvenWaraxe)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Eagle, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Eagle_Giant, FeatConstants.SpecialQualities.Evasion, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.ChangeShape, "Any Small, Medium, or Large Humanoid or Giant")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GaseousForm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PermanentImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ProduceFlame)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Pyrotechnics)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ScorchingRay + ": 1 ray only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfFire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Wish + ": Grant up to 3 wishes to nongenies")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Efreeti, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elasmosaurus, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Air_Elder, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Air_Greater, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Air_Huge, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Air_Large, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Elder, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Elder, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Greater, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Greater, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Huge, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Huge, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Large, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Large, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Medium, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Earth_Small, FeatConstants.SpecialQualities.EarthGlide, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Elder, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Elder, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Elder, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Greater, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Greater, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Greater, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Huge, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Huge, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Huge, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Large, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Large, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Large, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Medium, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Medium, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Small, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Fire_Small, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Water_Elder, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Water_Greater, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Water_Huge, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elemental_Water_Large, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elephant, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.Net)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Trident)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Longspear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Shortspear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Spear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.SpecialQualities.Gills, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Aquatic, FeatConstants.SpecialQualities.LowLightVision_Superior, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Drow, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.HandCrossbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Drow, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Rapier)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Drow, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.LightBlindness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Drow, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FaerieFire)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeShortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Rapier)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Gray, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Gray, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Half, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Half, FeatConstants.SpecialQualities.ElvenBlood, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Half, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeShortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Rapier)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_High, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_High, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeShortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Rapier)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wild, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wild, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeShortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Rapier)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Elf_Wood, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MinorImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Erinyes, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.EtherealFilcher, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.EtherealFilcher, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EtherealJaunt)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.EtherealMarauder, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EtherealJaunt)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ettercap, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ettin, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ettin, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ettin, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ettin, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Morningstar)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ettin, FeatConstants.SpecialQualities.TwoWeaponFighting_Superior, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.HiveMind, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ClairaudienceClairvoyance)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Dictum)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianMyrmarch, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.OrdersWrath)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.EschewMaterials, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.HiveMind, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CalmEmotions)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ClairaudienceClairvoyance)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Dictum)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Divination)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.OrdersWrath)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ShieldOfLaw)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianQueen, FeatConstants.SpecialQualities.Telepathy, "Any intelligent creature within 50 miles whose presence she is aware of")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.HiveMind, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DominateMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianTaskmaster, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.HiveMind, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWarrior, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.HiveMind, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictSeriousWounds + ": 8 workers work together to cast the spell")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FormianWorker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MakeWhole + ": 3 workers work together to cast the spell")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.FrostWorm, FeatConstants.SpecialQualities.DeathThroes, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gargoyle, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gargoyle, FeatConstants.SpecialQualities.Freeze, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gargoyle_Kapoacinth, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gargoyle_Kapoacinth, FeatConstants.SpecialQualities.Freeze, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GelatinousCube, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GelatinousCube, FeatConstants.SpecialQualities.Transparent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.AlternateForm, "Humanoid and globe forms")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil, cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.ProtectiveAura, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Aid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChainLightning)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ColorSpray)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ComprehendLanguages)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictLightWounds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility_Greater + ": self only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PrismaticSpray)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Tongues)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghaele, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfForce)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghoul, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghoul_Ghast, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ghoul_Lacedon, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Cloud, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Cloud, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Morningstar)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.OversizedWeapon, SizeConstants.Gargantuan)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FogCloud)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate + ": self plus 2,000 pounds")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Cloud, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ObscuringMist)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Fire, FeatConstants.ArmorProficiency_Heavy, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Fire, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Fire, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Fire, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Fire, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Frost, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Frost, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greataxe)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Frost, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Hill, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Hill, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Hill, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatclub)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Hill, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatclub)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone_Elder, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone_Elder, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone_Elder, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatclub)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneShape)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StoneTell)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteMudToRock)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Stone_Elder, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TransmuteRockToMud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Storm, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Storm, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Storm, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Storm, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.RockCatching, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CallLightning)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChainLightning)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FreedomOfMovement)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Giant_Storm, FeatConstants.SpecialQualities.WaterBreathing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GibberingMouther, FeatConstants.SpecialQualities.Amorphous, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GibberingMouther, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to bludgeoning weapons")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Girallon, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githyanki, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githyanki, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githyanki, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githyanki, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githyanki, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githyanki, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githyanki, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Daze)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githyanki, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MageHand)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githzerai, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githzerai, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.InertialArmor, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Daze)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FeatherFall)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Githzerai, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Shatter)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChaosHammer)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirrorImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordStun)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ReverseGravity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Wish + ": for a mortal humanoid. The demon can use this ability to offer a mortal whatever he or she desires - but unless the wish is used to create pain and suffering in the world, the glabrezu demands either terrible evil acts or great sacrifice as compensation.")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Glabrezu, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnoll, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnoll, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Battleaxe)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnoll, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnoll, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Forest, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.AttackBonus, CreatureConstants.Types.Subtypes.Orc)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.AttackBonus, CreatureConstants.Types.Subtypes.Reptilian)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PassWithoutTrace + ": self only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Prestidigitation)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Forest, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals + ": on a very basic level with forest animals")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Rock, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Prestidigitation)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Rock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals + ": burrowing mammals only, duration 1 minute")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Svirfneblin, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Svirfneblin, FeatConstants.ArmorProficiency_Heavy, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Svirfneblin, FeatConstants.WeaponProficiency_Martial, WeaponConstants.HeavyPick)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BlindnessDeafness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gnome_Svirfneblin, FeatConstants.SpecialQualities.Stonecunning, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Goblin, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Goblin, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Goblin, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Morningstar)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Goblin, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Golem_Clay, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to adamantine, bludgeoning weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Golem_Clay, FeatConstants.SpecialQualities.Immunity, "Magic (see creature description)")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Golem_Clay, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Haste + ": after at least 1 round of combat, lasts 3 rounds")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Golem_Flesh, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to adamantine weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Golem_Flesh, FeatConstants.SpecialQualities.Immunity, "Magic (see creature description)")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Golem_Iron, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to adamantine weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Golem_Iron, FeatConstants.SpecialQualities.Immunity, "Magic (see creature description)")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Golem_Stone, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to adamantine weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Golem_Stone, FeatConstants.SpecialQualities.Immunity, "Magic (see creature description)")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Golem_Stone_Greater, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to adamantine weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Golem_Stone_Greater, FeatConstants.SpecialQualities.Immunity, "Magic (see creature description)")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gorgon, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GrayOoze, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GrayOoze, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GrayOoze, FeatConstants.SpecialQualities.Transparent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GrayRender, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GhostSound)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PassWithoutTrace)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Tongues)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.GreenHag, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WaterBreathing)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grick, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grick, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grick, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Griffon, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig, FeatConstants.Dodge, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Pyrotechnics)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig_WithFiddle, FeatConstants.Dodge, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig_WithFiddle, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig_WithFiddle, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig_WithFiddle, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Pyrotechnics)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grig_WithFiddle, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grimlock, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Immunity, "Attack forms that rely on sight")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Immunity, "Gaze attacks")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Immunity, "Illusions")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Immunity, "Visual effects")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Grimlock, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ClairaudienceClairvoyance)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ComprehendLanguages)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LegendLore)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateObject)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ReadMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveCurse)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfDeath)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfFear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfInsanity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfPain)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfPersuasion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfSleep)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Gynosphinx, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SymbolOfStunning)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Halfling_Deep, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Halfling_Deep, FeatConstants.SpecialQualities.Stonecunning, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Harpy, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Club)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hawk, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HellHound, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HellHound, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HellHound_NessianWarhound, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HellHound_NessianWarhound, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.InvisibleInLight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hellcat_Bezekira, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hellwasp_Swarm, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hellwasp_Swarm, FeatConstants.SpecialQualities.HiveMind, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blasphemy)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChaosHammer)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GaseousForm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hezrou, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hippogriff, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hobgoblin, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hobgoblin, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hobgoblin, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hobgoblin, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.SpikedChain)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good, silver weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.Regeneration, "Does not regenerate damage from good-aligned, silvered weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fireball)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LightningBolt)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HornedDevil_Cornugon, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Horse_Heavy, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Horse_Heavy_War, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Horse_Light, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Horse_Light_War, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HoundArchon, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.ChangeShape, "Any canine form of Small to Large size")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Aid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.HoundArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Message)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_5Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_5Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_5Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_6Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_6Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_6Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_7Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_7Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_7Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_8Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_8Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_8Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_9Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_9Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_9Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_10Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_10Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_10Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_11Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_11Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_11Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_12Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_12Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hydra_12Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Hyena, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Longspear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Spear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Shortspear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.Regeneration, "Does not regenerate good damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ConeOfCold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fly)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.IceStorm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyAura)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfIce)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.IceDevil_Gelugon, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Imp, FeatConstants.SpecialQualities.AlternateForm, "Imp Alternate Form")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Imp, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or silver weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Imp, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Imp, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Imp, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Commune + ": ask 6 questions")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Imp, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.InvisibleStalker, FeatConstants.SpecialQualities.Tracking_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.InvisibleStalker, FeatConstants.SpecialQualities.NaturalInvisibility, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Janni, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Janni, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Janni, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Janni, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Janni, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Scimitar)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Janni, FeatConstants.SpecialQualities.ElementalEndurance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateFoodAndWater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EtherealJaunt)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Janni, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Janni, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kobold, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Sling)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kobold, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Spear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kobold, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kobold, FeatConstants.SpecialQualities.LightSensitivity, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.ArmorProficiency_Heavy, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to chaotic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DiscernLies)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldPerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateCreature)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MarkOfJustice)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kolyarut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kraken, FeatConstants.SpecialQualities.InkCloud, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kraken, FeatConstants.SpecialQualities.Jet, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kraken, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWeather)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kraken, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ControlWinds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kraken, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DominateAnimal)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Kraken, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ResistEnergy)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Krenshar, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Krenshar, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.KuoToa, FeatConstants.Alertness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.KuoToa, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Shortspear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Adhesive, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.KeenSight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.LightBlindness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.KuoToa, FeatConstants.SpecialQualities.Slippery, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lamia, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeepSlumber)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DisguiseSelf)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirrorImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lamia, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Ventriloquism)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lammasu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lammasu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility_Greater + ": self only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lammasu, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic, evil weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Aid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.LanternArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lemure, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or silver weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lemure, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lemure, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lemure, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lemure, FeatConstants.SpecialQualities.Immunity, "Mind-Affecting Effects")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lemure, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lemure, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil, silver weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Sonic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.Immunity, "Petrification")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.LayOnHands, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.ProtectiveAura, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictCriticalWounds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fireball)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HealHarm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.NeutralizePoison)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveDisease)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals + ": does not require sound")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leonal, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfForce)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Leopard, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lillend, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lillend, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, "Bardic music ability as a 6th-level Bard")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmPerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HallucinatoryTerrain)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Knock)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Light)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithAnimals)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lillend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpeakWithPlants)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lion, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lion_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lizard, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lizardfolk, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Lizardfolk, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Locathah, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Longspear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Locathah, FeatConstants.WeaponProficiency_Simple, WeaponConstants.LightCrossbow)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Magmin, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Magmin, FeatConstants.SpecialQualities.MeltWeapons, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Manticore, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Manticore, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.Monster.MultiweaponFighting, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good, cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AlignWeapon)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BladeBarrier)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicWeapon)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ProjectImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Telekinesis)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyAura)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marilith, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.ArmorProficiency_Heavy, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to chaotic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AirWalk)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChainLightning)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CircleOfDeath)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Command_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictLightWounds_Mass)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Earthquake)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GeasQuest)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateCreature)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MarkOfJustice)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Marut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfForce)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Medusa, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Medusa, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Megaraptor, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.FastHealing, "Exposed to moving air")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Air, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GustOfWind)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Dust, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Dust, FeatConstants.SpecialQualities.FastHealing, "In arid, dusty environment")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Dust, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Dust, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WindWall)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Earth, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Earth, FeatConstants.SpecialQualities.FastHealing, "Underground or buried up to its waist in earth")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Earth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EnlargePerson + ": self only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Earth, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SoftenEarthAndStone)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Fire, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Fire, FeatConstants.SpecialQualities.FastHealing, "Touching a flame at least as large as a torch")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Fire, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HeatMetal)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Fire, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ScorchingRay)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Ice, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Ice, FeatConstants.SpecialQualities.FastHealing, "Touching a piece of ice at least Tiny in size, or ambient temperature is freezing or lower")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Ice, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChillMetal)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Ice, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicMissile)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Magma, FeatConstants.SpecialQualities.ChangeShape, "A pool of lava 3 feet in diameter and 6 inches deep")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Magma, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Magma, FeatConstants.SpecialQualities.FastHealing, "Touching magma, lava, or a flame at least as large as a torch")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Magma, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Pyrotechnics)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Ooze, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Ooze, FeatConstants.SpecialQualities.FastHealing, "In a wet or muddy environment")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Ooze, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AcidArrow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Ooze, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StinkingCloud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Salt, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Salt, FeatConstants.SpecialQualities.FastHealing, "In an arid environment")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Salt, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Glitterdust)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Salt, FeatConstants.SpecialQualities.SpellLikeAbility, "Draw moisture from the air")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Steam, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Steam, FeatConstants.SpecialQualities.FastHealing, "Touching boiling water or in a hot, humid area")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Steam, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blur)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Steam, FeatConstants.SpecialQualities.SpellLikeAbility, "Rainstorm of boiling water")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Water, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Water, FeatConstants.SpecialQualities.FastHealing, "Exposed to rain or submerged up to its waist in water")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Water, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AcidArrow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mephit_Water, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.StinkingCloud)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Merfolk, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Merfolk, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Trident)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Merfolk, FeatConstants.WeaponProficiency_Simple, WeaponConstants.HeavyCrossbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Merfolk, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Merfolk, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mimic, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mimic, FeatConstants.SpecialQualities.MimicShape, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.MindFlayer, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Minotaur, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greataxe)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Minotaur, FeatConstants.SpecialQualities.NaturalCunning, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Minotaur, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Monkey, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mule, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mummy, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Mummy, FeatConstants.SpecialQualities.Vulnerability, FeatConstants.Foci.Elements.Fire)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Naga_Dark, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Naga_Dark, FeatConstants.SpecialQualities.Immunity, "Any form of mind reading")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Naga_Dark, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Naga_Guardian, FeatConstants.EschewMaterials, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Naga_Spirit, FeatConstants.EschewMaterials, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Naga_Water, FeatConstants.EschewMaterials, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CallLightning)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Feeblemind)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Slow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyAura)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nalfeshnee, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.NightHag, FeatConstants.SpecialQualities.ChangeShape, "Any Small or Medium Humanoid")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.NightHag, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron, magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, "Charm")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.NightHag, FeatConstants.SpecialQualities.Immunity, "Fear")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.NightHag, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.AversionToDaylight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to silver, magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.DesecratingAura, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ConeOfCold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Contagion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FingerOfDeath)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Haste)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster_Mass)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightcrawler, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightmare, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AstralProjection)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightmare, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Etherealness)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightmare_Cauchemar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AstralProjection)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightmare_Cauchemar, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Etherealness)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.AversionToDaylight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to silver, magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.DesecratingAura, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ConeOfCold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Contagion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FingerOfDeath)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Haste)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwalker, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.AversionToDaylight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to silver, magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.DesecratingAura, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ConeOfCold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Contagion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FingerOfDeath)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Haste)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PlaneShift)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SeeInvisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nightwing, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nixie, FeatConstants.Dodge, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nixie, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nixie, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nixie, FeatConstants.WeaponProficiency_Simple, WeaponConstants.LightCrossbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nixie, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nixie, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nixie, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmPerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WaterBreathing)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nixie, FeatConstants.SpecialQualities.WildEmpathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nymph, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nymph, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nymph, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionDoor)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nymph, FeatConstants.SpecialQualities.UnearthlyGrace, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Nymph, FeatConstants.SpecialQualities.WildEmpathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OchreJelly, FeatConstants.SpecialQualities.Split, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Octopus, FeatConstants.SpecialQualities.InkCloud, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Octopus, FeatConstants.SpecialQualities.Jet, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Octopus_Giant, FeatConstants.SpecialQualities.InkCloud, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Octopus_Giant, FeatConstants.SpecialQualities.Jet, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ogre, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ogre, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ogre, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatclub)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ogre, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ogre, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ogre_Merrow, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ogre_Merrow, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Longspear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ogre_Merrow, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ogre_Merrow, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ogre_Merrow, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.ChangeShape, "Any Small, Medium, or Large Humanoid or Giant")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.Flight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.Regeneration, "Fire and acid deal normal damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmPerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ConeOfCold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GaseousForm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.OgreMage, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Sleep)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Orc, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Orc, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Falchion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Orc, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greataxe)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Orc, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Orc, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Orc, FeatConstants.SpecialQualities.LightSensitivity, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Orc_Half, FeatConstants.SpecialQualities.OrcBlood, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Otyugh, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Owl, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Owl_Giant, FeatConstants.SpecialQualities.LowLightVision_Superior, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Owlbear, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pegasus, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pegasus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment + ": within 60-foot radius")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PhantomFungus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility_Greater)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PhaseSpider, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EtherealJaunt)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Phasm, FeatConstants.SpecialQualities.AlternateForm, "Any form Large size or smaller, including Humanoid")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Phasm, FeatConstants.SpecialQualities.Amorphous, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Phasm, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Phasm, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Phasm, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good, silver weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.Regeneration, "Does not regenerate damage from good spells or effects, or from good-aligned silvered weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SeeInDarkness, "Can see perfectly in darkness of any kind, even that created by a Deeper Darkness spell")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Blasphemy)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CreateUndead)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fireball)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster_Mass)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MeteorSwarm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PowerWordStun)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyAura)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Wish)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PitFiend, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.Dodge, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion_Lesser)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PermanentImage + ": visual and auditory elements only")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.Dodge, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.WeaponProficiency_Martial, WeaponConstants.ShortSword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Confusion_Lesser)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DancingLights)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.IrresistibleDance)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pixie_WithIrresistibleDance, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PermanentImage + ": visual and auditory elements only")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pony, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pony_War, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Porpoise, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Porpoise, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pseudodragon, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pseudodragon, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pseudodragon, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pseudodragon, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.PurpleWorm, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_5Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_5Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_5Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_6Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_6Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_6Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_7Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_7Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_7Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_8Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_8Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_8Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_9Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_9Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_9Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_10Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_10Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_10Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_11Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_11Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_11Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_12Heads, FeatConstants.CombatReflexes, "Can use all of its heads for Attacks of Opportunity")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_12Heads, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Pyrohydra_12Heads, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Quasit, FeatConstants.SpecialQualities.AlternateForm, "Bat, Small or Medium monstrous centipede, toad, and wolf")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Quasit, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Quasit, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Quasit, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CauseFear + ": 30-foot radius area from the quasit")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Commune + ": can ask 6 questions")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Quasit, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility + ": self only")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Rakshasa, FeatConstants.SpecialQualities.ChangeShape, "Any Humanoid form")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Rakshasa, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good, piercing weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Rakshasa, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Rakshasa, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Rast, FeatConstants.SpecialQualities.Flight, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Rat, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Rat, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Rat_Dire, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Rat_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Rat_Swarm, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Rat_Swarm, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Raven, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ravid, FeatConstants.SpecialQualities.Flight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ravid, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Ravid, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimateObjects)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.RazorBoar, FeatConstants.SpecialQualities.DamageReduction, "No physical vulnerabilities")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.RazorBoar, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.RazorBoar, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.RazorBoar, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Remorhaz, FeatConstants.SpecialQualities.Heat, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Remorhaz, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Retriever, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Roper, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Roper, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Roper, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Roper, FeatConstants.SpecialQualities.Vulnerability, FeatConstants.Foci.Elements.Fire)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.RustMonster, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Trident)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin, FeatConstants.WeaponProficiency_Simple, WeaponConstants.HeavyCrossbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin, FeatConstants.Monster.Multiattack, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.FreshwaterSensitivity, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.LightBlindness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.SpeakWithSharks, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin, FeatConstants.SpecialQualities.WaterDependent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Malenti, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Trident)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Malenti, FeatConstants.WeaponProficiency_Simple, WeaponConstants.HeavyCrossbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Malenti, FeatConstants.Monster.Multiattack, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.FreshwaterSensitivity, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.LightSensitivity, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.SpeakWithSharks, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Malenti, FeatConstants.SpecialQualities.WaterDependent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Mutant, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Trident)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Mutant, FeatConstants.WeaponProficiency_Simple, WeaponConstants.HeavyCrossbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Mutant, FeatConstants.Monster.Multiattack, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.FreshwaterSensitivity, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.LightBlindness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.SpeakWithSharks, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Sahuagin_Mutant, FeatConstants.SpecialQualities.WaterDependent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Average, FeatConstants.Monster.Multiattack, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Average, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Flamebrother, FeatConstants.Monster.Multiattack, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Noble, FeatConstants.Monster.Multiattack, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to magic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BurningHands)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fireball)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FlamingSphere)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SummonMonsterVII + ": Huge fire elemental")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Salamander_Noble, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WallOfFire)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Satyr, FeatConstants.Alertness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Satyr, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Satyr, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Satyr, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Satyr_WithPipes, FeatConstants.Alertness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Satyr_WithPipes, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Shortbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Satyr_WithPipes, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Dagger)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Satyr_WithPipes, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to cold iron weapons")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpion_Monstrous_Colossal, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpion_Monstrous_Gargantuan, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpion_Monstrous_Huge, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpion_Monstrous_Large, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpion_Monstrous_Medium, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpion_Monstrous_Small, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpion_Monstrous_Small, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpion_Monstrous_Tiny, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpion_Monstrous_Tiny, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpionfolk, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Lance)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpionfolk, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpionfolk, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpionfolk, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MajorImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Scorpionfolk, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirrorImage)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.SeaCat, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.SeaCat, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.SeaHag, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.SeaHag, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Shadow, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Shadow_Greater, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShadowMastiff, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShadowMastiff, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShadowMastiff, FeatConstants.SpecialQualities.ShadowBlend, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShamblingMound, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShamblingMound, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShamblingMound, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Shark_Dire, FeatConstants.SpecialQualities.KeenScent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Shark_Huge, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Shark_Huge, FeatConstants.SpecialQualities.KeenScent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Shark_Large, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Shark_Large, FeatConstants.SpecialQualities.KeenScent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Shark_Medium, FeatConstants.SpecialQualities.Blindsense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Shark_Medium, FeatConstants.SpecialQualities.KeenScent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.FindMaster, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.Guard, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.SpellStoring, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShieldGuardian, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ShieldOther + ": within 100 feet of the amulet.  Does not provide spell's AC or save bonuses")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShockerLizard, FeatConstants.SpecialQualities.ElectricitySense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.ShockerLizard, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Skum, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Blue, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.ChangeShape, "Any humanoid form")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to lawful weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Death, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.ChangeShape, "Any humanoid form")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to lawful weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Gray, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Sonic)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Slaad_Green, FeatConstants.SpecialQualities.ChangeShape, "Medium or Large humanoid form")] = new string[0];
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

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Snake_Constrictor, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Snake_Constrictor_Giant, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Snake_Viper_Huge, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Snake_Viper_Large, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Snake_Viper_Medium, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Snake_Viper_Small, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Snake_Viper_Tiny, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spectre, FeatConstants.SpecialQualities.SunlightPowerlessness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spectre, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spectre, FeatConstants.SpecialQualities.UnnaturalAura, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.SpiderEater, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.SpiderEater, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FreedomOfMovement)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_Hunter_Colossal, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_Hunter_Gargantuan, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_Hunter_Huge, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_Hunter_Large, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_Hunter_Medium, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_Hunter_Medium, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_Hunter_Small, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_Hunter_Small, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_Hunter_Tiny, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_Hunter_Tiny, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_WebSpinner_Colossal, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_WebSpinner_Huge, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_WebSpinner_Large, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_WebSpinner_Small, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_WebSpinner_Small, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_WebSpinner_Tiny, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Monstrous_WebSpinner_Tiny, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Swarm, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Spider_Swarm, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Squid, FeatConstants.SpecialQualities.InkCloud, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Squid, FeatConstants.SpecialQualities.Jet, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Squid_Giant, FeatConstants.SpecialQualities.InkCloud, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Squid_Giant, FeatConstants.SpecialQualities.Jet, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.ChangeShape, "Any Small or Medium Humanoid")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good or cold iron weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectThoughts)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.EtherealJaunt + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Tongues)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Succubus, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Stirge, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Carapace, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to epic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, "Ability Damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, "Disease")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, "Energy Drain")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Regeneration, "No form of attack deals lethal damage to the tarrasque")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tarrasque, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tendriculos, FeatConstants.SpecialQualities.Regeneration, "Bludgeoning weapons and acid deal normal damage")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Thoqqua, FeatConstants.SpecialQualities.Heat, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Thoqqua, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tiefling, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tiefling, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tiefling, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tiefling, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tiefling, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tiger, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tiger_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.ArmorProficiency_Heavy, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Warhammer)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.ChangeShape, "Any Small or Medium Humanoid")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to lawful weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.OversizedWeapon, SizeConstants.Gargantuan)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BestowCurse)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ChainLightning)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CrushingHand)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictCriticalWounds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Daylight)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Etherealness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.FireStorm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Gate)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HolySmite)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Invisibility)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.InvisibilityPurge)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Levitate)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Maze)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MeteorSwarm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.PersistentImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.RemoveCurse)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Restoration_Greater)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SummonNaturesAllyIX)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.UnholyBlight)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Titan, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.WordOfChaos)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Toad, FeatConstants.SpecialQualities.Amphibious, string.Empty)] = new string[0];

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

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Treant, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to slashing weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Treant, FeatConstants.SpecialQualities.Vulnerability, FeatConstants.Foci.Elements.Fire)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Triceratops, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Triton, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SummonNaturesAllyIV)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Troglodyte, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Club)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Troglodyte, FeatConstants.WeaponProficiency_Simple, WeaponConstants.Javelin)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Troglodyte, FeatConstants.Monster.Multiattack, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Troglodyte, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Troll, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Troll, FeatConstants.SpecialQualities.Regeneration, "Fire and acid deal normal damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Troll, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Troll_Scrag, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Troll_Scrag, FeatConstants.SpecialQualities.Regeneration, "Fire and acid deal normal damage; only regenerates when immersed in water")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Troll_Scrag, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.TrumpetArchon, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Greatsword)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to evil weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ContinualFlame)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.TrumpetArchon, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Message)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Tyrannosaurus, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.UmberHulk, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.UmberHulk_TrulyHorrid, FeatConstants.SpecialQualities.Tremorsense, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.Immunity, "Charm")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.Immunity, "Compulsion")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictLightWounds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CureInflictModerateWounds)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": within its forest home")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MagicCircleAgainstAlignment)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.NeutralizePoison)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Unicorn, FeatConstants.SpecialQualities.WildEmpathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.VampireSpawn, FeatConstants.Alertness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.VampireSpawn, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.VampireSpawn, FeatConstants.LightningReflexes, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to silver weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.GaseousForm)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.SpiderClimb)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.VampireSpawn, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vargouille, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vrock, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to good weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vrock, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Acid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vrock, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vrock, FeatConstants.SpecialQualities.EnergyResistance, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vrock, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Electricity)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vrock, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Heroism)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MirrorImage)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Telekinesis)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vrock, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Vrock, FeatConstants.SpecialQualities.Telepathy, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Weasel, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Weasel, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Weasel_Dire, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Weasel_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Whale_Baleen, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Whale_Baleen, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Whale_Cachalot, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Whale_Cachalot, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Whale_Orca, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Whale_Orca, FeatConstants.SpecialQualities.HoldBreath, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.WillOWisp, FeatConstants.WeaponFinesse, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.WillOWisp, FeatConstants.SpecialQualities.Immunity, "Spells and spell-like effects that allow spell resistance, except Magic Missile and Maze")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.WillOWisp, FeatConstants.SpecialQualities.NaturalInvisibility, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.WinterWolf, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wolf, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wolf, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wolf_Dire, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wolf_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wolverine, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wolverine, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wolverine_Dire, FeatConstants.Track, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wolverine_Dire, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Worg, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wraith, FeatConstants.Alertness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wraith, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wraith, FeatConstants.SpecialQualities.DaylightPowerlessness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wraith, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wraith, FeatConstants.SpecialQualities.UnnaturalAura, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wraith_Dread, FeatConstants.Alertness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wraith_Dread, FeatConstants.Initiative_Improved, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wraith_Dread, FeatConstants.SpecialQualities.DaylightPowerlessness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wraith_Dread, FeatConstants.SpecialQualities.Lifesense, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wraith_Dread, FeatConstants.SpecialQualities.TurnResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wraith_Dread, FeatConstants.SpecialQualities.UnnaturalAura, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wyvern, FeatConstants.Monster.Multiattack, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Wyvern, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xill, FeatConstants.Monster.Multiattack, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xill, FeatConstants.SpecialQualities.Planewalk, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Xill, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];

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

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YethHound, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to silver weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YethHound, FeatConstants.SpecialQualities.Flight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YethHound, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Immunity, "Attacks that rely on sight")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Immunity, "Gaze attacks")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Immunity, "Illusions")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Immunity, "Visual effects")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Yrthak, FeatConstants.SpecialQualities.Vulnerability, FeatConstants.Foci.Elements.Sonic)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.Alertness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.BlindFight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Scimitar)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.AlternateForm, "a Tiny to Large viper")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.ChameleonPower, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimalTrance)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.BalefulPolymorph + ": into snake form only")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectPoison)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.NeutralizePoison)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Abomination, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.Alertness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.BlindFight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Scimitar)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.CompositeLongbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.SpecialQualities.AlternateForm, "a Tiny to Large viper")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.SpecialQualities.ChameleonPower, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.SpecialQualities.Scent, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimalTrance)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CauseFear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DeeperDarkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectPoison)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.NeutralizePoison)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Halfblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Suggestion)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Pureblood, FeatConstants.Alertness, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Pureblood, FeatConstants.BlindFight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Pureblood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Scimitar)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Pureblood, FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.AlternateForm, "a Tiny to Large viper")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.AnimalTrance)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CauseFear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.CharmPerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Darkness)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DetectPoison)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.YuanTi_Pureblood, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Entangle)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.ArmorProficiency_Heavy, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.MountedCombat, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.WeaponProficiency_Exotic, WeaponConstants.SpikedChain)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.DamageReduction, "Vulnerable to chaotic weapons")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.FastHealing, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellResistance, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.ClairaudienceClairvoyance)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DimensionalAnchor)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.DispelMagic)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Earthquake)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Fear)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.Geas_Lesser)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldMonster)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.HoldPerson)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.LocateCreature)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.MarkOfJustice)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Zelekhut, FeatConstants.SpecialQualities.SpellLikeAbility, SpellConstants.TrueSeeing)] = new string[0];

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable CreatureTypeSpecialQualities
            {
                get
                {
                    var testCases = new Dictionary<string, string[]>();

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Aberration, FeatConstants.ShieldProficiency, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Aberration, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Aberration, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Animal, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Ability Damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Ability Drain")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Being raised or resurrected")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Death")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Death from massive damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Disease")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Effect that requires a Fortitude save")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Energy Drain")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Exhaustion")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Fatigue")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Mind-Affecting Effects")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Necromancy")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Nonlethal damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.Immunity, "Stunning")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Construct, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Dragon, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Dragon, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Dragon, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Dragon, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Dragon, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Elemental, FeatConstants.ShieldProficiency, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Elemental, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, "Flanking")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Elemental, FeatConstants.SpecialQualities.Immunity, "Stunning")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Fey, FeatConstants.ShieldProficiency, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Fey, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Fey, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Giant, FeatConstants.ShieldProficiency, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Giant, FeatConstants.WeaponProficiency_Martial, GroupConstants.All)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Giant, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Giant, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Humanoid, FeatConstants.ShieldProficiency, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Humanoid, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.MagicalBeast, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.MagicalBeast, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.MonstrousHumanoid, FeatConstants.ShieldProficiency, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.MonstrousHumanoid, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.MonstrousHumanoid, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Blindsight, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Flanking")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Gaze attacks, visual effects, illusions, and other attack forms that rely on sight")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Mind-Affecting Effects")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Polymorph")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Ooze, FeatConstants.SpecialQualities.Immunity, "Stunning")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Outsider, FeatConstants.ShieldProficiency, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Outsider, FeatConstants.WeaponProficiency_Martial, GroupConstants.All)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Outsider, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Outsider, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Mind-Affecting Effects")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Polymorph")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.Immunity, "Stunning")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Plant, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.ShieldProficiency, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Ability Damage to Strength, Dexterity, or Constitution")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Ability Drain")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Death")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Death from massive damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Disease")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Effect that requires a Fortitude save")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Energy Drain")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Exhaustion")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Fatigue")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Mind-Affecting Effects")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Nonlethal damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Paralysis")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Poison")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Undead, FeatConstants.SpecialQualities.Immunity, "Stunning")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Vermin, FeatConstants.SpecialQualities.Darkvision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Vermin, FeatConstants.SpecialQualities.Immunity, "Mind-Affecting Effects")] = new string[0];

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }

            public static IEnumerable CreatureSubtypeSpecialQualities
            {
                get
                {
                    var testCases = new Dictionary<string, string[]>();

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

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Cold, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Cold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Cold, FeatConstants.SpecialQualities.Vulnerability, FeatConstants.Foci.Elements.Fire)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.ArmorProficiency_Medium, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.SpecialQualities.AttackBonus, CreatureConstants.Types.Subtypes.Goblinoid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.SpecialQualities.AttackBonus, CreatureConstants.Types.Subtypes.Orc)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.SpecialQualities.Stability, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Dwarf, FeatConstants.SpecialQualities.Stonecunning, string.Empty)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Elf, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Elf, FeatConstants.SpecialQualities.Immunity, "Sleep")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Fire, FeatConstants.SpecialQualities.Immunity, FeatConstants.Foci.Elements.Fire)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Fire, FeatConstants.SpecialQualities.Vulnerability, FeatConstants.Foci.Elements.Cold)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.ArmorProficiency_Light, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.WeaponProficiency_Simple, WeaponConstants.LightCrossbow)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.SpecialQualities.AttackBonus, CreatureConstants.Kobold)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.SpecialQualities.AttackBonus, CreatureConstants.Types.Subtypes.Goblinoid)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.SpecialQualities.LowLightVision, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Gnome, FeatConstants.SpecialQualities.WeaponFamiliarity, WeaponConstants.GnomeHookedHammer)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Halfling, FeatConstants.SpecialQualities.AttackBonus, "thrown weapons and slings")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, "50% chance to ignore any damage from a corporeal source (except for positive energy, negative energy, force effects such as magic missiles, or attacks made with ghost touch weapons)")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, FeatConstants.SpecialQualities.Blindsense)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, FeatConstants.SpecialQualities.Blindsight)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, "Falling or falling damage")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, "Grapple")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, "Nonmagical attacks")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, FeatConstants.SpecialQualities.Scent)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, FeatConstants.SpecialQualities.Tremorsense)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Incorporeal, FeatConstants.SpecialQualities.Immunity, "Trip")] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Shapechanger, FeatConstants.ShieldProficiency, string.Empty)] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Shapechanger, FeatConstants.WeaponProficiency_Simple, GroupConstants.All)] = new string[0];

                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.HalfDamage, AttributeConstants.DamageTypes.Piercing)] = new[] { SizeConstants.Tiny };
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.HalfDamage, AttributeConstants.DamageTypes.Slashing)] = new[] { SizeConstants.Tiny };
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Any spell that targets a specific number of creatures, including single-target spells")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Bull Rush")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Critical hits")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Dying state")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Flanking")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Grapple")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Staggering")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Trip")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Immunity, "Weapon damage")] = new[] { SizeConstants.Diminutive, SizeConstants.Fine };
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Vulnerability, "Area-of-effect spells")] = new string[0];
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Vulnerability, "High winds")] = new[] { SizeConstants.Diminutive, SizeConstants.Fine };
                    testCases[SpecialQualityHelper.BuildRequirementKey(CreatureConstants.Types.Subtypes.Swarm, FeatConstants.SpecialQualities.Vulnerability, "Splash damage")] = new string[0];

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value);
                    }
                }
            }
        }
    }
}
