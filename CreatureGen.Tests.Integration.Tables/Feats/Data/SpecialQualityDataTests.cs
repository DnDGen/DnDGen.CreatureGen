using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Helpers;
using CreatureGen.Tables;
using CreatureGen.Tests.Integration.TestData;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Feats.Data
{
    [TestFixture]
    public class SpecialQualityDataTests : DataTests
    {
        //INFO: Need this for the feats selector
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }
        [Inject]
        internal IFeatsSelector FeatsSelector { get; set; }
        [Inject]
        internal ICreatureDataSelector CreatureDataSelector { get; set; }

        protected override string tableName => TableNameConstants.Collection.SpecialQualityData;

        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.SpecialQualityData.FeatNameIndex] = "Feat Name";
            indices[DataIndexConstants.SpecialQualityData.FocusIndex] = "Focus";
            indices[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex] = "Frequency Quantity";
            indices[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex] = "Frequency Time Period";
            indices[DataIndexConstants.SpecialQualityData.PowerIndex] = "Power";
            indices[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex] = "Random Foci Quantity";
            indices[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex] = "Requires Equipment";
            indices[DataIndexConstants.SpecialQualityData.SaveAbilityIndex] = "Save Ability";
            indices[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex] = "Save Base Value";
            indices[DataIndexConstants.SpecialQualityData.SaveIndex] = "Save";
        }

        [SetUp]
        public void Setup()
        {
            var clientID = Guid.NewGuid();
            ClientIdManager.SetClientID(clientID);
        }

        [Test]
        public void SpecialQualityDataNames()
        {
            var creatures = CreatureConstants.All();
            var types = CreatureConstants.Types.All();
            var subtypes = CreatureConstants.Types.Subtypes.All();

            var names = creatures.Union(types).Union(subtypes);

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(SpecialQualityTestData), "Creatures")]
        [TestCaseSource(typeof(SpecialQualityTestData), "Types")]
        [TestCaseSource(typeof(SpecialQualityTestData), "Subtypes")]
        public void SpecialQualityData(string creature, List<string[]> entries)
        {
            if (!entries.Any())
                Assert.Fail("Test case did not specify special qualities or NONE");

            if (entries[0][DataIndexConstants.SpecialQualityData.FeatNameIndex] == SpecialQualityTestData.None)
                entries.Clear();

            AssertData(creature, entries, e => SpecialQualityHelper.BuildData(e));
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void BonusFeatsHaveCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var feats = FeatsSelector.SelectFeats();
            var datas = table[creature]
                .Select(e => SpecialQualityHelper.ParseData(e))
                .Where(d => feats.Any(f => f.Feat == d[DataIndexConstants.SpecialQualityData.FeatNameIndex]));

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var data in datas)
            {
                var matchingFeat = feats.First(f => f.Feat == data[DataIndexConstants.SpecialQualityData.FeatNameIndex]);

                Assert.That(testCaseSpecialQualityDatas.Any(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == matchingFeat.Feat), Is.True, $"TEST CASE: {matchingFeat.Feat}");
                var testCaseData = testCaseSpecialQualityDatas.First(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == matchingFeat.Feat);

                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(matchingFeat.Frequency.Quantity.ToString()), $"TEST CASE: {matchingFeat.Feat} - Frequency Quantity");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(matchingFeat.Frequency.TimePeriod), $"TEST CASE: {matchingFeat.Feat} - Frequency Time Period");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(matchingFeat.Power.ToString()), $"TEST CASE: {matchingFeat.Feat} - Power");

                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(matchingFeat.Frequency.Quantity.ToString()), $"XML: {matchingFeat.Feat} - Frequency Quantit");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(matchingFeat.Frequency.TimePeriod), $"XML: {matchingFeat.Feat} - Frequency Time Period");
                Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(matchingFeat.Power.ToString()), $"XML: {matchingFeat.Feat} - Power");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void ProficiencyFeatsHaveCorrectFoci(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var proficiencyFeats = new[]
            {
                FeatConstants.WeaponProficiency_Exotic,
                FeatConstants.WeaponProficiency_Martial,
                FeatConstants.WeaponProficiency_Simple,
            };

            var datas = table[creature]
                .Select(e => SpecialQualityHelper.ParseData(e))
                .Where(d => proficiencyFeats.Contains(d[DataIndexConstants.SpecialQualityData.FeatNameIndex]))
                .Where(d => d[DataIndexConstants.SpecialQualityData.FocusIndex] != GroupConstants.All);

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            var weaponFamiliarityData = testCaseSpecialQualityDatas.Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.WeaponFamiliarity);
            var weaponFamiliarityFoci = weaponFamiliarityData.Select(d => d[DataIndexConstants.SpecialQualityData.FocusIndex]);

            foreach (var data in datas)
            {
                var featName = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];
                var focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];

                var featFoci = CollectionMapper.Map(TableNameConstants.Collection.FeatFoci);
                var proficiencyFoci = featFoci[featName];

                var testCaseData = testCaseSpecialQualityDatas.First(d =>
                    d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == featName
                    && d[DataIndexConstants.SpecialQualityData.FocusIndex] == focus);

                if (weaponFamiliarityFoci.Contains(focus) && featName == FeatConstants.WeaponProficiency_Martial)
                {
                    Assert.That(featFoci[FeatConstants.WeaponProficiency_Exotic], Contains.Item(focus), $"WEAPON FAMILIARITY: {focus}");
                    proficiencyFoci = proficiencyFoci.Union(new[] { focus });
                }

                Assert.That(proficiencyFoci, Contains.Item(testCaseData[DataIndexConstants.SpecialQualityData.FocusIndex]), $"TEST CASE: {featName}");
                Assert.That(proficiencyFoci, Contains.Item(focus), $"XML: {featName}");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void FeatsFocusingOnWeaponsOrArmorRequireEquipment(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var weaponAndArmorFeats = new[]
            {
                FeatConstants.ArmorProficiency_Heavy,
                FeatConstants.ArmorProficiency_Light,
                FeatConstants.ArmorProficiency_Medium,
                FeatConstants.ShieldBash_Improved,
                FeatConstants.ShieldProficiency,
                FeatConstants.ShieldProficiency_Tower,
                FeatConstants.TwoWeaponDefense,
                FeatConstants.TwoWeaponFighting,
                FeatConstants.TwoWeaponFighting_Greater,
                FeatConstants.TwoWeaponFighting_Improved,
                FeatConstants.WeaponProficiency_Exotic,
                FeatConstants.WeaponProficiency_Martial,
                FeatConstants.WeaponProficiency_Simple,
                FeatConstants.Monster.MultiweaponFighting,
                FeatConstants.Monster.MultiweaponFighting_Greater,
                FeatConstants.Monster.MultiweaponFighting_Improved,
                FeatConstants.SpecialQualities.OversizedWeapon,
                FeatConstants.SpecialQualities.TwoWeaponFighting_Superior,
                FeatConstants.SpecialQualities.WeaponFamiliarity,
            };

            var datas = table[creature]
                .Select(e => SpecialQualityHelper.ParseData(e))
                .Where(d => weaponAndArmorFeats.Contains(d[DataIndexConstants.SpecialQualityData.FeatNameIndex]));

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var data in datas)
            {
                var featName = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];

                var testCaseData = testCaseSpecialQualityDatas.First(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == featName);

                var requiresEquipment = Convert.ToBoolean(testCaseData[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex]);
                Assert.That(requiresEquipment, Is.True, $"TEST CASE: {featName}");

                requiresEquipment = Convert.ToBoolean(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex]);
                Assert.That(requiresEquipment, Is.True, $"XML: {featName}");
            }
        }

        private IEnumerable<string[]> GetTestCaseData(string creature)
        {
            var testCases = SpecialQualityTestData.Creatures.Cast<TestCaseData>()
                .Union(SpecialQualityTestData.Types.Cast<TestCaseData>())
                .Union(SpecialQualityTestData.Subtypes.Cast<TestCaseData>());

            var creatureTestCase = testCases.First(c => c.Arguments[0].ToString() == creature);

            var testCaseSpecialQualityDatas = creatureTestCase.Arguments[1] as List<string[]>;

            return testCaseSpecialQualityDatas.Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] != SpecialQualityTestData.None);
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void FastHealingHasCorrectFrequency(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];
            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] != FeatConstants.SpecialQualities.FastHealing)
                    continue;

                var testCaseData = testCaseSpecialQualityDatas.First(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.FastHealing);

                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), "TEST CASE: Frequency Quantity");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round), "TEST CASE: Frequency Time Period");

                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), "XML: Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round), "XML: Frequency Time Period");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void RegenerationHasCorrectFrequency(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];
            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] != FeatConstants.SpecialQualities.Regeneration)
                    continue;

                var testCaseData = testCaseSpecialQualityDatas.First(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.Regeneration);

                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), "TEST CASE: Frequency Quantity");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round), "TEST CASE: Frequency Time Period");

                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), "XML: Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round), "XML: Frequency Time Period");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void DamageReductionHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var datas = table[creature]
                .Select(e => SpecialQualityHelper.ParseData(e))
                .Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.DamageReduction);

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var data in datas)
            {
                Assert.That(testCaseSpecialQualityDatas.Any(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.DamageReduction), Is.True, $"TEST CASE: No Damage Reduction in test case");
                var testCaseData = testCaseSpecialQualityDatas.First(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.DamageReduction);

                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), "TEST CASE: Frequency Quantity");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Hit), "TEST CASE: Frequency Time Period");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty, "TEST CASE: Focus");
                Assert.That(Convert.ToInt32(testCaseData[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Positive, "TEST CASE: Power");

                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), "XML: Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Hit), "XML: Frequency Time Period");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty, "XML: Focus");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Positive, "XML: Power");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void ImmunityHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var datas = table[creature]
                .Select(e => SpecialQualityHelper.ParseData(e))
                .Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.Immunity);

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var data in datas)
            {
                Assert.That(testCaseSpecialQualityDatas.Any(d =>
                    d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.Immunity
                    && d[DataIndexConstants.SpecialQualityData.FocusIndex] == data[DataIndexConstants.SpecialQualityData.FocusIndex]
                ), Is.True, $"TEST CASE: No Immunity to {data[DataIndexConstants.SpecialQualityData.FocusIndex]} in test case");

                var testCaseData = testCaseSpecialQualityDatas.First(d =>
                    d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.Immunity
                    && d[DataIndexConstants.SpecialQualityData.FocusIndex] == data[DataIndexConstants.SpecialQualityData.FocusIndex]);

                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(0.ToString()), "TEST CASE: Frequency Quantity");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Empty, "TEST CASE: Frequency Time Period");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty, "TEST CASE: Focus");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()), "TEST CASE: Power");

                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(0.ToString()), "XML: Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Empty, "XML: Frequency Time Period");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty, "XML: Focus");
                Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()), "XML: Power");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void ChangeShapeHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var datas = table[creature]
                .Select(e => SpecialQualityHelper.ParseData(e))
                .Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.ChangeShape);

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var data in datas)
            {
                var testCaseData = testCaseSpecialQualityDatas.First(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.ChangeShape);

                Assert.That(Convert.ToInt32(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]), Is.Not.Negative, "TEST CASE: Frequency Quantity");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Not.Empty, "TEST CASE: Frequency Time Period");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty, "TEST CASE: Focus");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()), "TEST CASE: Power");

                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]), Is.Not.Negative, "XML: Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Not.Empty, "XML: Frequency Time Period");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty, "XML: Focus");
                Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()), "XML: Power");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void SpellLikeAbilityHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var datas = table[creature]
                .Select(e => SpecialQualityHelper.ParseData(e))
                .Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.SpellLikeAbility);

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var data in datas)
            {
                Assert.That(testCaseSpecialQualityDatas.Any(d =>
                    d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.SpellLikeAbility
                    && d[DataIndexConstants.SpecialQualityData.FocusIndex] == data[DataIndexConstants.SpecialQualityData.FocusIndex]), Is.True, $"TEST CASE: Spell-Like Ability - {data[DataIndexConstants.SpecialQualityData.FocusIndex]}");

                var testCaseData = testCaseSpecialQualityDatas.First(d =>
                    d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.SpellLikeAbility
                    && d[DataIndexConstants.SpecialQualityData.FocusIndex] == data[DataIndexConstants.SpecialQualityData.FocusIndex]);

                var focus = testCaseData[DataIndexConstants.SpecialQualityData.FocusIndex];

                Assert.That(focus, Is.Not.Empty, "TEST CASE: Focus");
                Assert.That(Convert.ToInt32(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]), Is.Not.Negative, $"TEST CASE: {focus} - Frequency Quantity");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Not.Empty, focus, $"TEST CASE: {focus} - Frequency Time Period");
                Assert.That(Convert.ToInt32(testCaseData[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Zero, focus, $"TEST CASE: {focus} - Power");

                focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];

                Assert.That(focus, Is.Not.Empty, "XML: Focus");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]), Is.Not.Negative, $"XML: {focus} - Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Not.Empty, focus, $"XML: {focus} - Frequency Time Period");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Zero, focus, $"XML: {focus} - Power");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void PsionicHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var datas = table[creature]
                .Select(e => SpecialQualityHelper.ParseData(e))
                .Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.Psionic);

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var data in datas)
            {
                Assert.That(testCaseSpecialQualityDatas.Any(d =>
                    d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.Psionic
                    && d[DataIndexConstants.SpecialQualityData.FocusIndex] == data[DataIndexConstants.SpecialQualityData.FocusIndex]), Is.True, $"TEST CASE: Psionic - {data[DataIndexConstants.SpecialQualityData.FocusIndex]}");

                var testCaseData = testCaseSpecialQualityDatas.First(d =>
                    d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.Psionic
                    && d[DataIndexConstants.SpecialQualityData.FocusIndex] == data[DataIndexConstants.SpecialQualityData.FocusIndex]);

                var focus = testCaseData[DataIndexConstants.SpecialQualityData.FocusIndex];

                Assert.That(focus, Is.Not.Empty, "TEST CASE: Focus");
                Assert.That(Convert.ToInt32(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]), Is.Not.Negative, $"TEST CASE: {focus} - Frequency Quantity");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Not.Empty, focus, $"TEST CASE: {focus} - Frequency Time Period");
                Assert.That(Convert.ToInt32(testCaseData[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Zero, focus, $"TEST CASE: {focus} - Power");

                focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];

                Assert.That(focus, Is.Not.Empty, "XML: Focus");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]), Is.Not.Negative, $"XML: {focus} - Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Not.Empty, focus, $"XML: {focus} - Frequency Time Period");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Zero, focus, $"XML: {focus} - Power");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void EnergyResistanceHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var energies = new[]
            {
                FeatConstants.Foci.Elements.Acid,
                FeatConstants.Foci.Elements.Cold,
                FeatConstants.Foci.Elements.Electricity,
                FeatConstants.Foci.Elements.Fire,
                FeatConstants.Foci.Elements.Sonic,
            };

            var datas = table[creature]
                .Select(e => SpecialQualityHelper.ParseData(e))
                .Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.EnergyResistance);

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var data in datas)
            {
                var testCaseData = testCaseSpecialQualityDatas.First(d =>
                    d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.EnergyResistance
                    && d[DataIndexConstants.SpecialQualityData.FocusIndex] == data[DataIndexConstants.SpecialQualityData.FocusIndex]);

                var focus = testCaseData[DataIndexConstants.SpecialQualityData.FocusIndex];
                Assert.That(focus, Is.Not.Empty, "TEST CASE: Focus");
                Assert.That(energies, Contains.Item(focus), $"TEST CASE: Focus");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), $"TEST CASE: {focus} - Frequency Quantity");
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round), $"TEST CASE: {focus} - Frequency Time Period");
                Assert.That(Convert.ToInt32(testCaseData[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Positive, $"TEST CASE: {focus} - Power");
                Assert.That(Convert.ToInt32(testCaseData[DataIndexConstants.SpecialQualityData.PowerIndex]) % 5, Is.Zero, $"TEST CASE: {focus} - Power");

                focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];
                Assert.That(focus, Is.Not.Empty, "XML: Focus");
                Assert.That(energies, Contains.Item(focus), $"XML: Focus");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), $"XML: {focus} - Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round), $"XML: {focus} - Frequency Time Period");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Positive, $"XML: {focus} - Power");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]) % 5, Is.Zero, $"XML: {focus} - Power");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void NoOverlapBetweenCreatureAndCreatureTypes(string creature)
        {
            var types = CollectionMapper.Map(TableNameConstants.Collection.CreatureTypes);
            var creatureTypes = types[creature].Except(new[] { creature }); //INFO: In case creature name duplicates as type, such as Gnoll

            Assert.That(table.Keys, Is.SupersetOf(creatureTypes));

            var creatureTestCaseSpecialQualityDatas = GetTestCaseData(creature);
            var creatureTestCaseSpecialQualities = creatureTestCaseSpecialQualityDatas.Select(d => SpecialQualityHelper.BuildData(d));

            foreach (var creatureType in creatureTypes)
            {
                var creatureTypeTestCaseSpecialQualityDatas = GetTestCaseData(creatureType);
                var creatureTypeTestCaseSpecialQualities = creatureTypeTestCaseSpecialQualityDatas.Select(d => SpecialQualityHelper.BuildData(d));

                var overlap = creatureTypeTestCaseSpecialQualities.Intersect(creatureTestCaseSpecialQualities);
                Assert.That(overlap, Is.Empty, $"TEST CASE v TEST CASE: {creature} - {creatureType}");

                overlap = table[creatureType].Intersect(creatureTestCaseSpecialQualities);
                Assert.That(overlap, Is.Empty, $"TEST CASE v XML: {creature} - {creatureType}");

                overlap = creatureTypeTestCaseSpecialQualities.Intersect(table[creature]);
                Assert.That(overlap, Is.Empty, $"XML v TEST CASE: {creature} - {creatureType}");

                overlap = table[creatureType].Intersect(table[creature]);
                Assert.That(overlap, Is.Empty, $"XML v XML: {creature} - {creatureType}");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreaturesThatCanChangeShapeIntoHumanoidCanUseEquipment(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var changeShapeFeats = new[]
            {
                FeatConstants.SpecialQualities.ChangeShape,
                FeatConstants.SpecialQualities.AlternateForm,
            };

            var datas = table[creature]
                .Select(e => SpecialQualityHelper.ParseData(e))
                .Where(d => changeShapeFeats.Contains(d[DataIndexConstants.SpecialQualityData.FeatNameIndex]));

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            var humanoids = new[]
            {
                CreatureConstants.Goblin, //For Barghest
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MonstrousHumanoid,
            };

            var creatureData = CreatureDataSelector.SelectFor(creature);

            foreach (var data in datas)
            {
                var featName = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];

                var testCaseData = testCaseSpecialQualityDatas.First(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == featName);

                var focus = testCaseData[DataIndexConstants.SpecialQualityData.FocusIndex];
                var changesIntoHumanoid = humanoids.Any(h => focus.ToLower().Contains(h.ToLower()));

                if (changesIntoHumanoid)
                {
                    Assert.That(creatureData.CanUseEquipment, Is.True, $"TEST CASE: {focus}");
                }

                focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];
                changesIntoHumanoid = humanoids.Any(h => focus.ToLower().Contains(h.ToLower()));

                if (changesIntoHumanoid)
                {
                    Assert.That(creatureData.CanUseEquipment, Is.True, $"XML: {focus}");
                }
            }
        }
    }
}
