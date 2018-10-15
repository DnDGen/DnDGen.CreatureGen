using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Helpers;
using CreatureGen.Tables;
using CreatureGen.Tests.Integration.Tables.TestData;
using DnDGen.Core.Selectors.Collections;
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
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }
        [Inject]
        internal IFeatsSelector FeatsSelector { get; set; }
        [Inject]
        internal ICreatureDataSelector CreatureDataSelector { get; set; }

        protected override string tableName
        {
            get { return TableNameConstants.Collection.SpecialQualityData; }
        }

        [SetUp]
        public void Setup()
        {
            var clientId = Guid.NewGuid();
            ClientIdManager.SetClientID(clientId);
        }

        [Test]
        public void CollectionNames()
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

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);
                var matchingFeat = feats.FirstOrDefault(f => f.Feat == data[DataIndexConstants.SpecialQualityData.FeatNameIndex]);

                if (matchingFeat != null)
                {
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(matchingFeat.Frequency.Quantity.ToString()), data[DataIndexConstants.SpecialQualityData.FeatNameIndex]);
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(matchingFeat.Frequency.TimePeriod), data[DataIndexConstants.SpecialQualityData.FeatNameIndex]);
                    Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(matchingFeat.Power.ToString()), data[DataIndexConstants.SpecialQualityData.FeatNameIndex]);
                }
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void FastHealingHasCorrectFrequency(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.FastHealing)
                {
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()));
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Turn));
                }
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void DamageReductionHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.DamageReduction)
                {
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()));
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Hit));
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty);
                    Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Positive);
                }
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void ImmunityHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.Immunity)
                {
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(0.ToString()));
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Empty);
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty);
                    Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()));
                }
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void ChangeShapeHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.ChangeShape)
                {
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(0.ToString()));
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Empty);
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty);
                    Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()));
                }
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void SpellLikeAbilityHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.SpellLikeAbility)
                {
                    var focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];

                    Assert.That(focus, Is.Not.Empty);
                    Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]), Is.Not.Negative, focus);
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Not.Empty, focus);
                    Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Zero, focus);
                }
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [TestCaseSource(typeof(CreatureTestData), "Types")]
        [TestCaseSource(typeof(CreatureTestData), "Subtypes")]
        public void EnergyResistanceHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];
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

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.EnergyResistance)
                {
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()));
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round));
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty);
                    Assert.That(new[] { data[DataIndexConstants.SpecialQualityData.FocusIndex] }, Is.SubsetOf(energies));
                    Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Positive);
                    Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]) % 5, Is.Zero);
                }
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void NoOverlapBetweenCreatureAndCreatureTypes(string creature)
        {
            var types = CollectionMapper.Map(TableNameConstants.Collection.CreatureTypes);
            var creatureTypes = types[creature];

            AssertCollection(table.Keys.Intersect(creatureTypes), creatureTypes);

            foreach (var creatureType in creatureTypes)
            {
                var overlap = table[creatureType].Intersect(table[creature]);

                Assert.That(overlap, Is.Empty, $"{creatureType}: {creature}");
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreaturesThatCanChangeShapeIntoHumanoidCanUseEquipment(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];
            var humanoids = new[]
            {
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Goblin, //For Barghest
            };

            var creatureData = CreatureDataSelector.SelectFor(creature);

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);
                var featName = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];

                if (featName == FeatConstants.SpecialQualities.ChangeShape || featName == FeatConstants.SpecialQualities.AlternateForm)
                {
                    var focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];
                    var changesIntoHumanoid = humanoids.Any(h => focus.ToLower().Contains(h.ToLower()));

                    Assert.That(changesIntoHumanoid, Is.EqualTo(creatureData.CanUseEquipment), focus);
                }
            }
        }
    }
}
