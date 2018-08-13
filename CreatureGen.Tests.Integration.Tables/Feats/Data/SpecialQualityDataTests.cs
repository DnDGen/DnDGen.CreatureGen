using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Helpers;
using CreatureGen.Skills;
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
        [TestCaseSource(typeof(SpecialQualityTestData), "CreatureTypes")]
        public void SpecialQualityData(string creature, List<string[]> entries)
        {
            if (!entries.Any())
                Assert.Fail("Test case did not specify special qualities or NONE");

            if (entries[0][DataIndexConstants.SpecialQualityData.FeatNameIndex] == SpecialQualityTestData.None)
                entries.Clear();

            var data = entries.Select(e => SpecialQualityHelper.BuildData(e)).ToArray();

            AssertDistinctCollection(creature, data);
        }

        [TestCase(CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Types.Subtypes.Water)]
        public void CreaturesOfSubtypeHaveSwimSkillBonus(string subtype)
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, subtype);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            var swimBonusData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": special action or avoid a hazard", power: 8);
            var swimTake10Data = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.SkillBonus, focus: SkillConstants.Swim + ": can always take 10", power: 10);

            var swimBonus = SpecialQualityHelper.BuildData(swimBonusData);
            var swimTake10 = SpecialQualityHelper.BuildData(swimTake10Data);

            foreach (var creature in creatures)
            {
                var specialQualities = table[creature];

                Assert.That(specialQualities, Is.Not.Empty, creature);
                Assert.That(specialQualities, Contains.Item(swimBonus), creature);
                Assert.That(specialQualities, Contains.Item(swimTake10), creature);
            }
        }

        [Test]
        public void ElementalsHaveElementalTraits()
        {
            var elementals = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Elemental);

            AssertCollection(elementals.Intersect(table.Keys), elementals);

            var immunityPoisonData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison");
            var immunitySleepData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep");
            var immunityParalysisData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis");
            var immunityStunningData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning");
            var immunityCriticalHitsData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits");
            var immunityFlankingData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Flanking");

            var immunityPoison = SpecialQualityHelper.BuildData(immunityPoisonData);
            var immunitySleep = SpecialQualityHelper.BuildData(immunitySleepData);
            var immunityParalysis = SpecialQualityHelper.BuildData(immunityParalysisData);
            var immunityStunning = SpecialQualityHelper.BuildData(immunityStunningData);
            var immunityCriticalHits = SpecialQualityHelper.BuildData(immunityCriticalHitsData);
            var immunityFlanking = SpecialQualityHelper.BuildData(immunityFlankingData);

            foreach (var elemental in elementals)
            {
                var specialQualities = table[elemental];

                Assert.That(specialQualities, Is.Not.Empty, elemental);
                Assert.That(specialQualities, Contains.Item(immunityPoison), elemental);
                Assert.That(specialQualities, Contains.Item(immunitySleep), elemental);
                Assert.That(specialQualities, Contains.Item(immunityParalysis), elemental);
                Assert.That(specialQualities, Contains.Item(immunityStunning), elemental);
                Assert.That(specialQualities, Contains.Item(immunityCriticalHits), elemental);
                Assert.That(specialQualities, Contains.Item(immunityFlanking), elemental);
            }
        }

        [Test]
        public void UndeadHaveUndeadTraits()
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Undead);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            var immunityMindData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects");
            var immunityPoisonData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison");
            var immunitySleepData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep");
            var immunityParalysisData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis");
            var immunityStunningData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning");
            var immunityDiseaseData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease");
            var immunityDeathData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death");
            var immunityCriticalHitsData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits");
            var immunityNonlethalData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage");
            var immunityAbilityDrainData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Abiltiy Drain");
            var immunityEnergyDrainData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain");
            var immunityFatigueData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue");
            var immunityExhaustionData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion");

            var immunityMind = SpecialQualityHelper.BuildData(immunityMindData);
            var immunityPoison = SpecialQualityHelper.BuildData(immunityPoisonData);
            var immunitySleep = SpecialQualityHelper.BuildData(immunitySleepData);
            var immunityParalysis = SpecialQualityHelper.BuildData(immunityParalysisData);
            var immunityStunning = SpecialQualityHelper.BuildData(immunityStunningData);
            var immunityDisease = SpecialQualityHelper.BuildData(immunityDiseaseData);
            var immunityDeath = SpecialQualityHelper.BuildData(immunityDeathData);
            var immunityCriticalHits = SpecialQualityHelper.BuildData(immunityCriticalHitsData);
            var immunityNonlethal = SpecialQualityHelper.BuildData(immunityNonlethalData);
            var immunityAbilityDrain = SpecialQualityHelper.BuildData(immunityAbilityDrainData);
            var immunityEnergyDrain = SpecialQualityHelper.BuildData(immunityEnergyDrainData);
            var immunityFatigue = SpecialQualityHelper.BuildData(immunityFatigueData);
            var immunityExhaustion = SpecialQualityHelper.BuildData(immunityExhaustionData);

            foreach (var creature in creatures)
            {
                var specialQualities = table[creature];

                Assert.That(specialQualities, Is.Not.Empty, creature);
                Assert.That(specialQualities, Contains.Item(immunityMind), creature);
                Assert.That(specialQualities, Contains.Item(immunityPoison), creature);
                Assert.That(specialQualities, Contains.Item(immunitySleep), creature);
                Assert.That(specialQualities, Contains.Item(immunityParalysis), creature);
                Assert.That(specialQualities, Contains.Item(immunityStunning), creature);
                Assert.That(specialQualities, Contains.Item(immunityDisease), creature);
                Assert.That(specialQualities, Contains.Item(immunityDeath), creature);
                Assert.That(specialQualities, Contains.Item(immunityCriticalHits), creature);
                Assert.That(specialQualities, Contains.Item(immunityNonlethal), creature);
                Assert.That(specialQualities, Contains.Item(immunityAbilityDrain), creature);
                Assert.That(specialQualities, Contains.Item(immunityEnergyDrain), creature);
                Assert.That(specialQualities, Contains.Item(immunityFatigue), creature);
                Assert.That(specialQualities, Contains.Item(immunityExhaustion), creature);
            }
        }

        [Test]
        public void IncorporealHaveIncorporealTraits()
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Incorporeal);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            var immunityNonmagicalAttacksData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonmagical attacks");

            var immunityNonmagicalAttacks = SpecialQualityHelper.BuildData(immunityNonmagicalAttacksData);

            foreach (var creature in creatures)
            {
                var specialQualities = table[creature];

                Assert.That(specialQualities, Is.Not.Empty, creature);
                Assert.That(specialQualities, Contains.Item(immunityNonmagicalAttacks), creature);
            }
        }

        [Test]
        public void ConstructsHaveConstructTraits()
        {
            var constructs = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Types.Construct);

            AssertCollection(constructs.Intersect(table.Keys), constructs);

            var immunityMindData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Mind-Affecting Effects");
            var immunityPoisonData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison");
            var immunitySleepData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep");
            var immunityParalysisData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis");
            var immunityStunningData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Stunning");
            var immunityDiseaseData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease");
            var immunityDeathData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Death");
            var immunityNecromancyData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Necromancy");
            var immunityCriticalHitsData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits");
            var immunityNonlethalData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Nonlethal damage");
            var immunityAbilityDamageData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage");
            var immunityAbilityDrainData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Drain");
            var immunityEnergyDrainData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain");
            var immunityFatigueData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fatigue");
            var immunityExhaustionData = SpecialQualityHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Exhaustion");

            var immunityMind = SpecialQualityHelper.BuildData(immunityMindData);
            var immunityPoison = SpecialQualityHelper.BuildData(immunityPoisonData);
            var immunitySleep = SpecialQualityHelper.BuildData(immunitySleepData);
            var immunityParalysis = SpecialQualityHelper.BuildData(immunityParalysisData);
            var immunityStunning = SpecialQualityHelper.BuildData(immunityStunningData);
            var immunityDisease = SpecialQualityHelper.BuildData(immunityDiseaseData);
            var immunityDeath = SpecialQualityHelper.BuildData(immunityDeathData);
            var immunityNecromancy = SpecialQualityHelper.BuildData(immunityNecromancyData);
            var immunityCriticalHits = SpecialQualityHelper.BuildData(immunityCriticalHitsData);
            var immunityNonlethal = SpecialQualityHelper.BuildData(immunityNonlethalData);
            var immunityAbilityDamage = SpecialQualityHelper.BuildData(immunityAbilityDamageData);
            var immunityAbilityDrain = SpecialQualityHelper.BuildData(immunityAbilityDrainData);
            var immunityEnergyDrain = SpecialQualityHelper.BuildData(immunityEnergyDrainData);
            var immunityFatigue = SpecialQualityHelper.BuildData(immunityFatigueData);
            var immunityExhaustion = SpecialQualityHelper.BuildData(immunityExhaustionData);

            foreach (var construct in constructs)
            {
                var specialQualities = table[construct];

                Assert.That(specialQualities, Is.Not.Empty, construct);
                Assert.That(specialQualities, Contains.Item(immunityMind), construct);
                Assert.That(specialQualities, Contains.Item(immunityPoison), construct);
                Assert.That(specialQualities, Contains.Item(immunitySleep), construct);
                Assert.That(specialQualities, Contains.Item(immunityParalysis), construct);
                Assert.That(specialQualities, Contains.Item(immunityStunning), construct);
                Assert.That(specialQualities, Contains.Item(immunityDisease), construct);
                Assert.That(specialQualities, Contains.Item(immunityDeath), construct);
                Assert.That(specialQualities, Contains.Item(immunityNecromancy), construct);
                Assert.That(specialQualities, Contains.Item(immunityCriticalHits), construct);
                Assert.That(specialQualities, Contains.Item(immunityNonlethal), construct);
                Assert.That(specialQualities, Contains.Item(immunityAbilityDamage), construct);
                Assert.That(specialQualities, Contains.Item(immunityAbilityDrain), construct);
                Assert.That(specialQualities, Contains.Item(immunityEnergyDrain), construct);
                Assert.That(specialQualities, Contains.Item(immunityFatigue), construct);
                Assert.That(specialQualities, Contains.Item(immunityExhaustion), construct);
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
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
                    Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]) % 5, Is.Zero);
                }
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
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
        public void SpellLikeAbilityHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.SpellLikeAbility)
                {
                    Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]), Is.Not.Negative);
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Not.Empty);
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty);
                    Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Zero);
                }
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void SkillBonusHasCorrectData(string creature)
        {
            Assert.That(table.Keys, Contains.Item(creature));

            var collection = table[creature];

            foreach (var entry in collection)
            {
                var data = SpecialQualityHelper.ParseData(entry);

                if (data[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.SkillBonus)
                {
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(0.ToString()));
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Empty);
                    Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty);
                    Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Positive);
                }
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
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
    }
}
