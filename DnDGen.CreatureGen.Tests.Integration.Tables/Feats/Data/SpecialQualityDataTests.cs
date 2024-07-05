using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Feats.Data
{
    [TestFixture]
    public class SpecialQualityDataTests : DataTests
    {
        private IFeatsSelector featsSelector;
        private ICreatureDataSelector creatureDataSelector;

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
            indices[DataIndexConstants.SpecialQualityData.MinHitDiceIndex] = "Minimum Hit Dice";
            indices[DataIndexConstants.SpecialQualityData.MaxHitDiceIndex] = "Maximum Hit Dice";
        }

        [SetUp]
        public void Setup()
        {
            helper = new SpecialQualityHelper();

            featsSelector = GetNewInstanceOf<IFeatsSelector>();
            creatureDataSelector = GetNewInstanceOf<ICreatureDataSelector>();
        }

        [Test]
        public void SpecialQualityDataNames()
        {
            var creatures = CreatureConstants.GetAll();
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var templates = CreatureConstants.Templates.GetAll();

            var names = creatures.Union(types).Union(subtypes).Union(templates);

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(SpecialQualityTestData), nameof(SpecialQualityTestData.Creatures))]
        [TestCaseSource(typeof(SpecialQualityTestData), nameof(SpecialQualityTestData.Types))]
        [TestCaseSource(typeof(SpecialQualityTestData), nameof(SpecialQualityTestData.Subtypes))]
        [TestCaseSource(typeof(SpecialQualityTestData), nameof(SpecialQualityTestData.Templates))]
        public void SpecialQualityData(string creature, List<string[]> entries)
        {
            if (!entries.Any())
                Assert.Fail("Test case did not specify special qualities or NONE");

            if (entries[0][DataIndexConstants.SpecialQualityData.FeatNameIndex] == SpecialQualityTestData.None)
                entries.Clear();

            AssertData(creature, entries);

            //Bonus Feats Have Correct Data
            var feats = featsSelector.SelectFeats();
            var bonusFeatsData = entries.Where(d => feats.Any(f => f.Feat == d[DataIndexConstants.SpecialQualityData.FeatNameIndex]));

            foreach (var data in bonusFeatsData)
            {
                var matchingFeat = feats.First(f => f.Feat == data[DataIndexConstants.SpecialQualityData.FeatNameIndex]);

                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(matchingFeat.Frequency.Quantity.ToString()), $"XML: {matchingFeat.Feat} - Frequency Quantit");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(matchingFeat.Frequency.TimePeriod), $"XML: {matchingFeat.Feat} - Frequency Time Period");
                Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(matchingFeat.Power.ToString()), $"XML: {matchingFeat.Feat} - Power");
            }

            //Proficiency Feats Have Correct Foci
            var proficiencyFeats = new[]
            {
                FeatConstants.WeaponProficiency_Exotic,
                FeatConstants.WeaponProficiency_Martial,
                FeatConstants.WeaponProficiency_Simple,
            };

            var proficiencyFeatsData = entries
                .Where(d => proficiencyFeats.Contains(d[DataIndexConstants.SpecialQualityData.FeatNameIndex]))
                .Where(d => d[DataIndexConstants.SpecialQualityData.FocusIndex] != GroupConstants.All);

            var weaponFamiliarityData = entries
                .Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.WeaponFamiliarity);
            var weaponFamiliarityFoci = weaponFamiliarityData.Select(d => d[DataIndexConstants.SpecialQualityData.FocusIndex]);

            foreach (var data in proficiencyFeatsData)
            {
                var featName = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];
                var focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];

                var featFoci = collectionMapper.Map(Config.Name, TableNameConstants.Collection.FeatFoci);
                var proficiencyFoci = featFoci[featName];

                if (weaponFamiliarityFoci.Contains(focus) && featName == FeatConstants.WeaponProficiency_Martial)
                {
                    Assert.That(featFoci[FeatConstants.WeaponProficiency_Exotic], Contains.Item(focus), $"WEAPON FAMILIARITY: {focus}");
                    proficiencyFoci = proficiencyFoci.Union(new[] { focus });
                }

                Assert.That(proficiencyFoci, Contains.Item(focus), $"XML: {featName}");
            }

            //Feats Focusing On Weapons Or Armor Require Equipment
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

            var weaponArmorFeatsData = entries.Where(d => weaponAndArmorFeats.Contains(d[DataIndexConstants.SpecialQualityData.FeatNameIndex]));

            foreach (var data in weaponArmorFeatsData)
            {
                var featName = data[DataIndexConstants.SpecialQualityData.FeatNameIndex];

                var requiresEquipment = Convert.ToBoolean(data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex]);
                Assert.That(requiresEquipment, Is.True, $"XML: {featName}");
            }

            //Fast Healing Has Correct Frequency
            var fastHealingData = entries.FirstOrDefault(e => e[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.FastHealing);
            if (fastHealingData != null)
            {
                Assert.That(fastHealingData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), "XML: Frequency Quantity");
                Assert.That(fastHealingData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round), "XML: Frequency Time Period");
            }

            //Regeneration Has Correct Frequency
            var regenerationData = entries.FirstOrDefault(e => e[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.Regeneration);
            if (regenerationData != null)
            {
                Assert.That(regenerationData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), "XML: Frequency Quantity");
                Assert.That(regenerationData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round), "XML: Frequency Time Period");
            }

            //Damage Reduction Has Correct Data
            var damageReductiondatas = entries.Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.DamageReduction);

            foreach (var data in damageReductiondatas)
            {
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), "XML: Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Hit), "XML: Frequency Time Period");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty, "XML: Focus");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Positive, "XML: Power");
            }

            //Immunity Has Correct Data
            var immunityDatas = entries.Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.Immunity);

            foreach (var data in immunityDatas)
            {
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(0.ToString()), "XML: Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Empty, "XML: Frequency Time Period");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty, "XML: Focus");
                Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()), "XML: Power");
            }

            //Change Shape Has Correct Data
            var changeShapeDatas = entries.Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.ChangeShape);

            foreach (var data in changeShapeDatas)
            {
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]), Is.Not.Negative, "XML: Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Not.Empty, "XML: Frequency Time Period");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FocusIndex], Is.Not.Empty, "XML: Focus");
                Assert.That(data[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo(0.ToString()), "XML: Power");
            }

            //Spell-Like Ability Has Correct Data
            var spellLikeAbilityDatas = entries.Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.SpellLikeAbility);

            foreach (var data in spellLikeAbilityDatas)
            {
                var focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];

                Assert.That(focus, Is.Not.Empty, "XML: Focus");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]), Is.Not.Negative, $"XML: {focus} - Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Not.Empty, focus, $"XML: {focus} - Frequency Time Period");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Zero, focus, $"XML: {focus} - Power");
            }

            //Psionic Has Correct Data
            var psionicDatas = entries.Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.Psionic);

            foreach (var data in psionicDatas)
            {
                var focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];

                Assert.That(focus, Is.Not.Empty, "XML: Focus");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex]), Is.Not.Negative, $"XML: {focus} - Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.Not.Empty, focus, $"XML: {focus} - Frequency Time Period");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Zero, focus, $"XML: {focus} - Power");
            }

            //Energy Resistance Has Correct Data
            var energies = new[]
            {
                FeatConstants.Foci.Elements.Acid,
                FeatConstants.Foci.Elements.Cold,
                FeatConstants.Foci.Elements.Electricity,
                FeatConstants.Foci.Elements.Fire,
                FeatConstants.Foci.Elements.Sonic,
            };

            var energyResistanceDatas = entries.Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] == FeatConstants.SpecialQualities.EnergyResistance);

            foreach (var data in energyResistanceDatas)
            {
                var focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];
                Assert.That(focus, Is.Not.Empty, "XML: Focus");
                Assert.That(energies, Contains.Item(focus), $"XML: Focus");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()), $"XML: {focus} - Frequency Quantity");
                Assert.That(data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round), $"XML: {focus} - Frequency Time Period");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]), Is.Positive, $"XML: {focus} - Power");
                Assert.That(Convert.ToInt32(data[DataIndexConstants.SpecialQualityData.PowerIndex]) % 5, Is.Zero, $"XML: {focus} - Power");
            }

            //Creatures That Can Change Shape Into Humanoid Can Use Equipment
            var allCreatures = CreatureConstants.GetAll();
            if (allCreatures.Contains(creature))
            {
                var changeShapeFeats = new[]
                {
                    FeatConstants.SpecialQualities.ChangeShape,
                    FeatConstants.SpecialQualities.AlternateForm,
                };

                changeShapeDatas = entries.Where(d => changeShapeFeats.Contains(d[DataIndexConstants.SpecialQualityData.FeatNameIndex]));

                var humanoids = new[]
                {
                    CreatureConstants.Goblin, //For Barghest
                    CreatureConstants.Types.Giant,
                    CreatureConstants.Types.Humanoid,
                    CreatureConstants.Types.MonstrousHumanoid,
                };

                var creatureData = creatureDataSelector.SelectFor(creature);

                foreach (var data in changeShapeDatas)
                {
                    var focus = data[DataIndexConstants.SpecialQualityData.FocusIndex];
                    var changesIntoHumanoid = humanoids.Any(h => focus.ToLower().Contains(h.ToLower()));

                    if (changesIntoHumanoid)
                    {
                        Assert.That(creatureData.CanUseEquipment, Is.True, $"XML: {focus}");
                    }
                }
            }
        }

        [Test]
        public void AllSpecialQualityKeysUnique()
        {
            var keys = new List<string>();

            foreach (var kvp in table)
            {
                foreach (var value in kvp.Value)
                {
                    var isValid = helper.ValidateEntry(value);
                    Assert.That(isValid, Is.True, kvp.Key);

                    var key = helper.BuildKey(kvp.Key, value);
                    keys.Add(key);
                }
            }

            Assert.That(keys, Is.Unique);
        }

        private IEnumerable<string[]> GetTestCaseData(string creature)
        {
            var testCases = SpecialQualityTestData.Creatures.Cast<TestCaseData>()
                .Union(SpecialQualityTestData.Types.Cast<TestCaseData>())
                .Union(SpecialQualityTestData.Subtypes.Cast<TestCaseData>())
                .Union(SpecialQualityTestData.Templates.Cast<TestCaseData>());

            var creatureTestCase = testCases.First(c => c.Arguments[0].ToString() == creature);

            var testCaseSpecialQualityDatas = creatureTestCase.Arguments[1] as List<string[]>;

            return testCaseSpecialQualityDatas.Where(d => d[DataIndexConstants.SpecialQualityData.FeatNameIndex] != SpecialQualityTestData.None);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void NoOverlapBetweenCreatureAndCreatureTypes(string creature)
        {
            var types = collectionMapper.Map(Config.Name, TableNameConstants.Collection.CreatureTypes);
            var creatureTypes = types[creature].Except(new[] { creature }); //INFO: In case creature name duplicates as type, such as Gnoll

            Assert.That(table.Keys, Is.SupersetOf(creatureTypes));

            var creatureTestCaseSpecialQualityDatas = GetTestCaseData(creature);
            var creatureTestCaseSpecialQualities = creatureTestCaseSpecialQualityDatas.Select(helper.BuildEntry);

            foreach (var creatureType in creatureTypes)
            {
                var creatureTypeTestCaseSpecialQualityDatas = GetTestCaseData(creatureType);
                var creatureTypeTestCaseSpecialQualities = creatureTypeTestCaseSpecialQualityDatas.Select(helper.BuildEntry);

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
    }
}
