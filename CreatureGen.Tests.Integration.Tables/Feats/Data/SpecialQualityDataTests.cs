﻿using CreatureGen.Creatures;
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
    public class SpecialQualityDataTests : CollectionTests
    {
        //INFO: Need this for the feats selector
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }
        [Inject]
        internal IFeatsSelector FeatsSelector { get; set; }
        [Inject]
        internal ICreatureDataSelector CreatureDataSelector { get; set; }

        protected override string tableName => TableNameConstants.Collection.SpecialQualityData;

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

            var data = entries.Select(e => SpecialQualityHelper.BuildData(e)).ToArray();

            AssertDistinctCollection(creature, data);
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void BonusFeatsHaveCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var feats = FeatsSelector.SelectFeats();
            var collection = table[creature];

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);
                var matchingFeat = feats.FirstOrDefault(f => f.Feat == data[DataIndexConstants.SpecialQualityData.FeatNameIndex]);

                if (matchingFeat == null)
                    continue;

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

            var collection = table[creature];

            var proficiencyFeats = new[]
            {
                FeatConstants.WeaponProficiency_Exotic,
                FeatConstants.WeaponProficiency_Martial,
                FeatConstants.WeaponProficiency_Simple,
            };

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);
                var featName = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];

                if (!proficiencyFeats.Contains(featName))
                    continue;

                var focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];

                if (focus == GroupConstants.All)
                    continue;

                var featFoci = CollectionMapper.Map(TableNameConstants.Collection.FeatFoci);
                var proficiencyFoci = featFoci[featName];

                var testCaseData = testCaseSpecialQualityDatas.First(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == featName);

                Assert.That(proficiencyFoci, Contains.Item(testCaseData[DataIndexConstants.SpecialQualityData.FocusIndex]), featName, $"TEST CASE: {featName}");
                Assert.That(proficiencyFoci, Contains.Item(focus), featName, $"XML: {featName}");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void ProficiencyFeatsRequireEquipment(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];

            var proficiencyFeats = new[]
            {
                FeatConstants.ArmorProficiency_Heavy,
                FeatConstants.ArmorProficiency_Light,
                FeatConstants.ArmorProficiency_Medium,
                FeatConstants.ShieldProficiency,
                FeatConstants.ShieldProficiency_Tower,
                FeatConstants.WeaponProficiency_Exotic,
                FeatConstants.WeaponProficiency_Martial,
                FeatConstants.WeaponProficiency_Simple,
            };

            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);
                var featName = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];

                if (!proficiencyFeats.Contains(featName))
                    continue;

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

            return testCaseSpecialQualityDatas;
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
                Assert.That(testCaseData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Turn), "TEST CASE: Frequency Time Period");

                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), "XML: Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Turn), "XML: Frequency Time Period");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void DamageReductionHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];
            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] != FeatConstants.SpecialQualities.DamageReduction)
                    continue;

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

            var collection = table[creature];
            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] != FeatConstants.SpecialQualities.Immunity)
                    continue;

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

            var collection = table[creature];
            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] != FeatConstants.SpecialQualities.ChangeShape)
                    continue;

                var testCaseData = testCaseSpecialQualityDatas.First(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.ChangeShape);

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
        public void SpellLikeAbilityHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];
            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] != FeatConstants.SpecialQualities.SpellLikeAbility)
                    continue;

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
        public void EnergyResistanceHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];
            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            var energies = new[]
            {
                FeatConstants.Foci.Elements.Acid,
                FeatConstants.Foci.Elements.Cold,
                FeatConstants.Foci.Elements.Electricity,
                FeatConstants.Foci.Elements.Fire,
                FeatConstants.Foci.Elements.Sonic,
            };

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] != FeatConstants.SpecialQualities.EnergyResistance)
                    continue;

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
            var creatureTypes = types[creature];

            AssertCollection(table.Keys.Intersect(creatureTypes), creatureTypes);

            var testCases = SpecialQualityTestData.Creatures.Cast<TestCaseData>();
            var creatureTestCase = testCases.First(c => c.Arguments[0].ToString() == creature);

            var testCaseSpecialQualityDatas = creatureTestCase.Arguments[1] as List<string[]>;
            var testCaseSpecialQualities = testCaseSpecialQualityDatas.Select(d => SpecialQualityHelper.BuildData(d));

            foreach (var creatureType in creatureTypes)
            {
                var overlap = table[creatureType].Intersect(testCaseSpecialQualities);
                Assert.That(overlap, Is.Empty, $"TEST CASE: {creature} - {creatureType}");

                overlap = table[creatureType].Intersect(table[creature]);
                Assert.That(overlap, Is.Empty, $"XML: {creature} - {creatureType}");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreaturesThatCanChangeShapeIntoHumanoidCanUseEquipment(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];
            var testCaseSpecialQualityDatas = GetTestCaseData(creature);

            var humanoids = new[]
            {
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Goblin, //For Barghest
            };

            var changeShapeFeats = new[]
            {
                FeatConstants.SpecialQualities.ChangeShape,
                FeatConstants.SpecialQualities.AlternateForm,
            };

            var creatureData = CreatureDataSelector.SelectFor(creature);

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);
                var featName = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];

                if (!changeShapeFeats.Contains(featName))
                    continue;

                var testCaseData = testCaseSpecialQualityDatas.First(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == featName);

                var focus = testCaseData[DataIndexConstants.SpecialQualityData.FocusIndex];
                var changesIntoHumanoid = humanoids.Any(h => focus.ToLower().Contains(h.ToLower()));

                Assert.That(changesIntoHumanoid, Is.EqualTo(creatureData.CanUseEquipment), $"TEST CASE: {focus}");

                focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];
                changesIntoHumanoid = humanoids.Any(h => focus.ToLower().Contains(h.ToLower()));

                Assert.That(changesIntoHumanoid, Is.EqualTo(creatureData.CanUseEquipment), $"XML: {focus}");
            }
        }
    }
}
