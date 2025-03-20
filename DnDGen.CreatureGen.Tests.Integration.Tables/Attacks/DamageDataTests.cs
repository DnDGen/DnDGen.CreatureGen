using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.Tables.Creatures;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Helpers;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Attacks
{
    [TestFixture]
    public class DamageDataTests : CollectionTests
    {
        private ICollectionSelector collectionSelector;
        private IFeatsSelector featsSelector;
        private ICreatureDataSelector creatureDataSelector;
        private Dictionary<string, List<string>> creatureAttackData;
        private Dictionary<string, List<string>> templateAttackData;
        private Dictionary<string, List<string>> creatureAttackDamageData;
        private Dictionary<string, List<string>> templateAttackDamageData;
        private Dictionary<string, string> damageMaps;
        private Dictionary<string, IEnumerable<AdvancementDataSelection>> advancementData;
        private Dictionary<string, CreatureDataSelection> creatureData;
        private Dictionary<string, AttackDataSelection> attackData;

        protected override string tableName => TableNameConstants.Collection.DamageData;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            creatureAttackData = AttackTestData.GetCreatureAttackData();
            templateAttackData = AttackTestData.GetTemplateAttackData();
            creatureAttackDamageData = DamageTestData.GetCreatureAttackDamageData();
            templateAttackDamageData = DamageTestData.GetTemplateDamageData();
            creatureData = CreatureDataTests.GetCreatureTestData().ToDictionary(kvp => kvp.Key, kvp => DataHelper.Parse<CreatureDataSelection>(kvp.Value));
            advancementData = AdvancementsTests.GetAdvancementsTestData().ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(DataHelper.Parse<AdvancementDataSelection>));

            damageMaps = new Dictionary<string, string>
            {
                ["2d8"] = "3d8",
                ["2d6"] = "3d6",
                ["1d10"] = "2d8",
                ["1d8"] = "2d6",
                ["1d6"] = "1d8",
                ["1d4"] = "1d6",
                ["1d3"] = "1d4",
                ["1d2"] = "1d3"
            };

            attackData = [];
            foreach (var kvp in creatureAttackData)
            {
                var creature = kvp.Key;
                var sizes = advancementData[creature].Select(a => a.Size).Union([creatureData[creature].Size]);
                var selections = kvp.Value.Select(DataHelper.Parse<AttackDataSelection>);

                foreach (var selection in selections)
                {
                    foreach (var size in sizes)
                    {
                        attackData[selection.BuildDamageKey(creature, size)] = selection;
                    }
                }
            }
        }

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            featsSelector = GetNewInstanceOf<IFeatsSelector>();
            creatureDataSelector = GetNewInstanceOf<ICreatureDataSelector>();
        }

        [Test]
        public void DamageDataNames()
        {
            var names = AttackTestData.GetDamageKeys();
            var testKeys = creatureAttackDamageData.Keys.Union(templateAttackDamageData.Keys);
            Assert.That(testKeys, Is.EquivalentTo(names));

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureAttackDamageData(string creature)
        {
            var attacks = creatureAttackData[creature];
            var originalSize = creatureData[creature].Size;
            var advancedSizes = advancementData[creature].Select(a => a.Size).Except([originalSize]);

            foreach (var attack in attacks)
            {
                var key = attack.B
                AssertCreatureAttackDamages();
                //TODO: Assert creature damage for each attack, original size
                //TODO: Adjust damages for advancements, creature damage for each attack, advanced sizes
            }
        }

        private void AssertCreatureAttackDamages(string key, IEnumerable<string> entries)
        {
            AssertCreatureHasCorrectImprovedGrab(creatureAttackDamageData[creature]);
            AssertCreatureHasCorrectSpellLikeAbility(creatureAttackDamageData[creature]);
            AssertCreatureHasCorrectSpells(creatureAttackDamageData[creature]);
            AssertCreatureEffectDoesNotHaveDamage(creatureAttackDamageData[creature]);
            AssertNaturalAttacksHaveCorrectDamageTypes(creatureAttackDamageData[creature]);
            AssertPoisonAttacksHaveCorrectDamageTypes(creatureAttackDamageData[creature]);
            AssertDiseaseAttacksHaveCorrectDamageTypes(creatureAttackDamageData[creature]);

            AssertCollection(key, creatureAttackDamageData[creature]);

            CreatureWithSpellLikeAbilityAttack_HasSpellLikeAbilitySpecialQuality(creature);
            CreatureWithPsionicAttack_HasPsionicSpecialQuality(creature);
            CreatureWithSpellsAttack_HasMagicSpells(creature);
            CreatureWithUnnaturalAttack_CanUseEquipment(creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TempplateAttackDamageData(string template)
        {
            AssertCreatureHasCorrectImprovedGrab(templateAttackDamageData[template]);
            AssertCreatureHasCorrectSpellLikeAbility(templateAttackDamageData[template]);
            AssertCreatureHasCorrectSpells(templateAttackDamageData[template]);
            AssertCreatureEffectDoesNotHaveDamage(templateAttackDamageData[template]);
            AssertNaturalAttacksHaveCorrectDamageTypes(templateAttackDamageData[template]);
            AssertPoisonAttacksHaveCorrectDamageTypes(templateAttackDamageData[template]);
            AssertDiseaseAttacksHaveCorrectDamageTypes(templateAttackDamageData[template]);

            AssertCollection(template, templateAttackDamageData[template]);
        }

        private void AssertNaturalAttacksHaveCorrectDamageTypes(List<string> entries)
        {
            var damageTypes = new Dictionary<string, string>
            {
                ["bite"] = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}/{AttributeConstants.DamageTypes.Bludgeoning}",
                ["claw"] = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}",
                ["talon"] = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}",
                ["talons"] = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}",
                ["rake"] = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}",
                ["rend"] = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}",
                ["gore"] = $"{AttributeConstants.DamageTypes.Piercing}",
                ["slap"] = $"{AttributeConstants.DamageTypes.Bludgeoning}",
                ["tail slap"] = $"{AttributeConstants.DamageTypes.Bludgeoning}",
                ["slam"] = $"{AttributeConstants.DamageTypes.Bludgeoning}",
                ["sting"] = $"{AttributeConstants.DamageTypes.Piercing}",
                ["tentacle"] = $"{AttributeConstants.DamageTypes.Bludgeoning}",
                ["arm"] = $"{AttributeConstants.DamageTypes.Bludgeoning}",
                ["wing"] = $"{AttributeConstants.DamageTypes.Bludgeoning}",
                ["trample"] = $"{AttributeConstants.DamageTypes.Bludgeoning}",
                ["unarmed strike"] = $"{AttributeConstants.DamageTypes.Bludgeoning}"
            };

            var selections = entries.Select(DataHelper.Parse<DamageDataSelection>);

            foreach (var selection in selections)
            {
                if (!damageTypes.ContainsKey(selection.Name.ToLower()))
                {
                    continue;
                }

                var damageData = damageHelper.ParseEntries(entry[DataIndexConstants.AttackData.DamageDataIndex]);
                Assert.That(damageData, Is.Not.Empty, entry[DataIndexConstants.AttackData.NameIndex]);
                Assert.That(
                    damageData[0][DataIndexConstants.AttackData.DamageData.TypeIndex],
                    Is.EqualTo(damageTypes[entry[DataIndexConstants.AttackData.NameIndex].ToLower()]),
                    entry[DataIndexConstants.AttackData.NameIndex]);
            }
        }

        private void AssertPoisonAttacksHaveCorrectDamageTypes(List<string[]> entries)
        {
            foreach (var entry in entries)
            {
                if (entry[DataIndexConstants.AttackData.NameIndex].ToLower() != "poison")
                {
                    continue;
                }

                Assert.That(entry[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(bool.TrueString), "Special");
                Assert.That(entry[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(bool.TrueString), "Melee");
                Assert.That(entry[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(bool.FalseString), "Primary");
                Assert.That(entry[DataIndexConstants.AttackData.SaveIndex], Is.Not.Empty.And.EqualTo(SaveConstants.Fortitude));

                if (entry[DataIndexConstants.AttackData.IsNaturalIndex] == bool.TrueString)
                    Assert.That(entry[DataIndexConstants.AttackData.SaveAbilityIndex], Is.Not.Empty);
                else
                    Assert.That(entry[DataIndexConstants.AttackData.SaveAbilityIndex], Is.Empty);

                var damageData = damageHelper.ParseEntries(entry[DataIndexConstants.AttackData.DamageDataIndex]);
                Assert.That(damageData, Has.Length.AtMost(2).Or.Length.EqualTo(4), entry[DataIndexConstants.AttackData.DamageDataIndex]);

                if (damageData.Length > 2)
                {
                    Assert.That(damageData[0][DataIndexConstants.AttackData.DamageData.ConditionIndex], Is.EqualTo("Initial"));
                    Assert.That(damageData[1][DataIndexConstants.AttackData.DamageData.ConditionIndex], Is.EqualTo("Initial"));
                    Assert.That(damageData[2][DataIndexConstants.AttackData.DamageData.ConditionIndex], Is.EqualTo("Secondary"));
                    Assert.That(damageData[3][DataIndexConstants.AttackData.DamageData.ConditionIndex], Is.EqualTo("Secondary"));
                }
                else if (damageData.Length > 1)
                {
                    Assert.That(damageData[1][DataIndexConstants.AttackData.DamageData.ConditionIndex], Is.EqualTo("Secondary"));
                }

                if (damageData.Length > 0)
                {
                    Assert.That(damageData[0][DataIndexConstants.AttackData.DamageData.ConditionIndex], Is.EqualTo("Initial"));
                }
                else
                {
                    Assert.That(entry[DataIndexConstants.AttackData.DamageEffectIndex], Is.Not.Empty);
                }
            }
        }

        private void AssertDiseaseAttacksHaveCorrectDamageTypes(List<string[]> entries)
        {
            var attackNames = entries.Select(e => e[DataIndexConstants.AttackData.NameIndex]);
            var diseaseAttack = string.Empty;

            foreach (var entry in entries)
            {
                if (entry[DataIndexConstants.AttackData.NameIndex].ToLower() != "disease"
                    && entry[DataIndexConstants.AttackData.NameIndex] != diseaseAttack)
                {
                    continue;
                }

                if (attackNames.Contains(entry[DataIndexConstants.AttackData.DamageEffectIndex]))
                {
                    diseaseAttack = entry[DataIndexConstants.AttackData.DamageEffectIndex];
                    continue;
                }

                Assert.That(entry[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(bool.TrueString));
                Assert.That(entry[DataIndexConstants.AttackData.SaveAbilityIndex], Is.Not.Empty);
                Assert.That(entry[DataIndexConstants.AttackData.SaveIndex], Is.Not.Empty);

                var damageData = damageHelper.ParseEntries(entry[DataIndexConstants.AttackData.DamageDataIndex]);
                Assert.That(damageData, Is.Not.Empty, entry[DataIndexConstants.AttackData.NameIndex]);

                for (var i = 0; i < damageData.Length; i++)
                {
                    Assert.That(damageData[i][DataIndexConstants.AttackData.DamageData.RollIndex], Is.Not.Empty, entry[DataIndexConstants.AttackData.NameIndex]);
                    Assert.That(damageData[i][DataIndexConstants.AttackData.DamageData.TypeIndex], Is.Not.Empty, entry[DataIndexConstants.AttackData.NameIndex]);
                    Assert.That(damageData[i][DataIndexConstants.AttackData.DamageData.ConditionIndex], Does.StartWith("Incubation period"), entry[DataIndexConstants.AttackData.NameIndex]);
                }

                diseaseAttack = string.Empty;
            }

            if (!string.IsNullOrEmpty(diseaseAttack))
            {
                Assert.Fail($"Could not find disease '{diseaseAttack}'");
            }
        }

        private void CreatureWithSpellLikeAbilityAttack_HasSpellLikeAbilitySpecialQuality(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                var valid = helper.ValidateEntry(entry);
                Assert.That(valid, Is.True, $"Invalid entry: {entry}");
            }

            var creatureType = GetCreatureType(creature);
            var specialQualities = featsSelector.SelectSpecialQualities(creature, creatureType);
            var hasSpellLikeAbilityAttack = table[creature]
                .Select(helper.ParseEntry)
                .Any(d => d[DataIndexConstants.AttackData.NameIndex] == FeatConstants.SpecialQualities.SpellLikeAbility);

            //INFO: Want to ignore constant effects such as Doppelganger's Detect Thoughts and Copper Dragon's Spider Climb
            var hasSpellLikeAbilitySpecialQuality = specialQualities.Any(q =>
                q.Feat == FeatConstants.SpecialQualities.SpellLikeAbility
                && q.Frequency.TimePeriod != FeatConstants.Frequencies.Constant);
            Assert.That(hasSpellLikeAbilityAttack, Is.EqualTo(hasSpellLikeAbilitySpecialQuality));
        }

        private CreatureType GetCreatureType(string creatureName)
        {
            var types = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, creatureName);
            return new CreatureType(types);
        }

        [Test]
        public void AllAttackKeysUnique()
        {
            var keys = new List<string>();

            foreach (var kvp in table)
            {
                foreach (var value in kvp.Value)
                {
                    var key = helper.BuildKey(kvp.Key, value);
                    keys.Add(key);
                }
            }

            Assert.That(keys, Is.Unique);
        }

        private void CreatureWithPsionicAttack_HasPsionicSpecialQuality(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                var valid = helper.ValidateEntry(entry);
                Assert.That(valid, Is.True, $"Invalid entry: {entry}");
            }

            var creatureType = GetCreatureType(creature);
            var specialQualities = featsSelector.SelectSpecialQualities(creature, creatureType);
            var hasPsionicAttack = table[creature]
                .Select(helper.ParseEntry)
                .Any(d => d[DataIndexConstants.AttackData.NameIndex] == FeatConstants.SpecialQualities.Psionic);

            var hasPsionicSpecialQuality = specialQualities.Any(q => q.Feat == FeatConstants.SpecialQualities.Psionic);
            Assert.That(hasPsionicAttack, Is.EqualTo(hasPsionicSpecialQuality));
        }

        private void CreatureWithSpellsAttack_HasMagicSpells(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                var valid = helper.ValidateEntry(entry);
                Assert.That(valid, Is.True, $"Invalid entry: {entry}");
            }

            var hasSpellsAttack = table[creature]
                .Select(helper.ParseEntry)
                .Any(d => d[DataIndexConstants.AttackData.NameIndex] == "Spells");

            var caster = collectionSelector.SelectFrom(Config.Name, TableNameConstants.TypeAndAmount.Casters, creature);

            if (hasSpellsAttack)
            {
                Assert.That(caster, Is.Not.Empty);
            }
            else
            {
                Assert.That(caster, Is.Empty);
            }
        }

        private void CreatureWithUnnaturalAttack_CanUseEquipment(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                var valid = helper.ValidateEntry(entry);
                Assert.That(valid, Is.True, $"Invalid entry: {entry}");
            }

            var creatureData = creatureDataSelector.SelectFor(creature);
            var unnaturalAttacks = table[creature]
                .Select(helper.ParseEntry)
                .Where(d => !Convert.ToBoolean(d[DataIndexConstants.AttackData.IsNaturalIndex]));

            if (!unnaturalAttacks.Any())
            {
                Assert.Pass($"{creature} has all-natural, 100% USDA Organic attacks");
            }

            Assert.That(unnaturalAttacks.Any(), Is.True.And.EqualTo(creatureData.CanUseEquipment));

            //No Damage for Equipment attacks
            foreach (var attack in unnaturalAttacks)
            {
                if (attack[DataIndexConstants.AttackData.NameIndex] != AttributeConstants.Melee
                    && attack[DataIndexConstants.AttackData.NameIndex] != AttributeConstants.Ranged)
                {
                    continue;
                }

                Assert.That(attack[DataIndexConstants.AttackData.DamageDataIndex], Is.Empty, attack[DataIndexConstants.AttackData.NameIndex]);
            }

            //Has Natural Attack
            var naturalAttack = table[creature]
                .Select(helper.ParseEntry)
                .FirstOrDefault(d => d[DataIndexConstants.AttackData.IsNaturalIndex] == bool.TrueString
                    && d[DataIndexConstants.AttackData.IsSpecialIndex] == bool.FalseString);

            Assert.That(naturalAttack, Is.Not.Null);
            Assert.That(naturalAttack[DataIndexConstants.AttackData.NameIndex], Is.Not.Empty);
            Assert.That(naturalAttack[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(bool.TrueString), naturalAttack[DataIndexConstants.AttackData.NameIndex]);
            Assert.That(naturalAttack[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(bool.FalseString), naturalAttack[DataIndexConstants.AttackData.NameIndex]);
            Assert.That(naturalAttack[DataIndexConstants.AttackData.DamageDataIndex], Is.Not.Empty, naturalAttack[DataIndexConstants.AttackData.NameIndex]);
        }

        private void AssertCreatureHasCorrectImprovedGrab(List<string[]> entries)
        {
            var improvedGrab = entries.FirstOrDefault(d => d[DataIndexConstants.AttackData.NameIndex] == "Improved Grab");
            if (improvedGrab == null)
            {
                return;
            }

            Assert.That(improvedGrab[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("extraordinary ability"));
            Assert.That(improvedGrab[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo("0"));
            Assert.That(improvedGrab[DataIndexConstants.AttackData.DamageEffectIndex], Is.Empty);
            Assert.That(improvedGrab[DataIndexConstants.AttackData.DamageDataIndex], Is.Empty);
            Assert.That(improvedGrab[DataIndexConstants.AttackData.FrequencyQuantityIndex], Is.EqualTo("1"));
            Assert.That(improvedGrab[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(improvedGrab[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(bool.TrueString));
            Assert.That(improvedGrab[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(bool.TrueString));
            Assert.That(improvedGrab[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(bool.FalseString));
            Assert.That(improvedGrab[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(bool.TrueString));
            Assert.That(improvedGrab[DataIndexConstants.AttackData.NameIndex], Is.EqualTo("Improved Grab"));
            Assert.That(improvedGrab[DataIndexConstants.AttackData.SaveAbilityIndex], Is.Empty);
            Assert.That(improvedGrab[DataIndexConstants.AttackData.SaveDcBonusIndex], Is.EqualTo("0"));
            Assert.That(improvedGrab[DataIndexConstants.AttackData.SaveIndex], Is.Empty);
        }

        private void AssertCreatureHasCorrectSpells(List<string[]> entries)
        {
            var spells = entries.FirstOrDefault(d => d[DataIndexConstants.AttackData.NameIndex] == "Spells");
            if (spells == null)
            {
                return;
            }

            Assert.That(spells[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("spell-like ability"));
            Assert.That(spells[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo(0.ToString()));
            Assert.That(spells[DataIndexConstants.AttackData.DamageEffectIndex], Is.Empty);
            Assert.That(spells[DataIndexConstants.AttackData.DamageDataIndex], Is.Empty);
            Assert.That(spells[DataIndexConstants.AttackData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()));
            Assert.That(spells[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(spells[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(bool.FalseString));
            Assert.That(spells[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(bool.TrueString));
            Assert.That(spells[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(bool.TrueString));
            Assert.That(spells[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(bool.TrueString));
            Assert.That(spells[DataIndexConstants.AttackData.NameIndex], Is.EqualTo("Spells"));
            Assert.That(spells[DataIndexConstants.AttackData.SaveAbilityIndex], Is.Empty);
            Assert.That(spells[DataIndexConstants.AttackData.SaveDcBonusIndex], Is.EqualTo(0.ToString()));
            Assert.That(spells[DataIndexConstants.AttackData.SaveIndex], Is.Empty);
        }

        private void AssertCreatureHasCorrectSpellLikeAbility(List<string[]> entries)
        {
            var spellLikeAbility = entries.FirstOrDefault(d => d[DataIndexConstants.AttackData.NameIndex] == FeatConstants.SpecialQualities.SpellLikeAbility);
            if (spellLikeAbility == null)
            {
                return;
            }

            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("spell-like ability"));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo(0.ToString()));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.DamageEffectIndex], Is.Empty);
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.DamageDataIndex], Is.Empty);
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.FrequencyQuantityIndex], Is.EqualTo(1.ToString()));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(bool.FalseString));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(bool.TrueString));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(bool.TrueString));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(bool.TrueString));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.NameIndex], Is.EqualTo(FeatConstants.SpecialQualities.SpellLikeAbility));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.SaveAbilityIndex], Is.Empty);
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.SaveDcBonusIndex], Is.EqualTo(0.ToString()));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.SaveIndex], Is.Empty);
        }

        private IEnumerable<AttackDataSelection> Select(string creatureName, string originalSize, string advancedSize)
        {
            var attackSelections = attackDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AttackData, creatureName);

            foreach (var selection in attackSelections)
            {
                var key = selection.BuildDamageKey(creatureName, advancedSize);
                var damageSelections = damageDataSelector.SelectFrom(Config.Name, TableNameConstants.Collection.DamageData, key);
                selection.Damages.AddRange(damageSelections);

                if (selection.IsNatural && originalSize != advancedSize)
                {
                    foreach (var damage in selection.Damages)
                    {
                        damage.Roll = GetAdjustedDamage(damage.Roll, originalSize, advancedSize);
                    }
                }
            }

            return attackSelections;
        }

        private string GetAdjustedDamage(string originalDamage, string originalSize, string advancedSize)
        {
            var adjustedDamage = originalDamage;
            var sizeDifference = Array.IndexOf(orderedSizes, advancedSize) - Array.IndexOf(orderedSizes, originalSize);

            while (sizeDifference-- > 0 && damageMaps.ContainsKey(adjustedDamage))
            {
                adjustedDamage = damageMaps[adjustedDamage];
            }

            return adjustedDamage;
        }
    }
}
