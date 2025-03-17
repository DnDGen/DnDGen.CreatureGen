using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Attacks
{
    [TestFixture]
    public class AttackDataTests : DataTests
    {
        private ICollectionSelector collectionSelector;
        private IFeatsSelector featsSelector;
        private ICreatureDataSelector creatureDataSelector;
        private Dictionary<string, List<string>> creatureAttackData;
        private Dictionary<string, List<string>> templateAttackData;
        private Dictionary<string, IEnumerable<AdvancementDataSelection>> advancementData;
        private Dictionary<string, IEnumerable<CreatureDataSelection>> creatureData;

        protected override string tableName => TableNameConstants.Collection.AttackData;

        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.AttackData.IsMeleeIndex] = "Is Melee";
            indices[DataIndexConstants.AttackData.IsNaturalIndex] = "Is Natural";
            indices[DataIndexConstants.AttackData.IsPrimaryIndex] = "Is Primary";
            indices[DataIndexConstants.AttackData.IsSpecialIndex] = "Is Special";
            indices[DataIndexConstants.AttackData.NameIndex] = "Name";
            indices[DataIndexConstants.AttackData.AttackTypeIndex] = "Attack Type";
            indices[DataIndexConstants.AttackData.DamageEffectIndex] = "Damage Effect";
            indices[DataIndexConstants.AttackData.FrequencyQuantityIndex] = "Frequency Quantity";
            indices[DataIndexConstants.AttackData.FrequencyTimePeriodIndex] = "Frequency Time Period";
            indices[DataIndexConstants.AttackData.SaveAbilityIndex] = "Save Ability";
            indices[DataIndexConstants.AttackData.SaveIndex] = "Save";
            indices[DataIndexConstants.AttackData.DamageBonusMultiplierIndex] = "Damage Bonus Multiplier";
            indices[DataIndexConstants.AttackData.SaveDcBonusIndex] = "Save DC Bonus Multiplier";
            indices[DataIndexConstants.AttackData.RequiredGenderIndex] = "Required Gender";
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            creatureAttackData = AttackTestData.GetCreatureAttackData();
            templateAttackData = AttackTestData.GetTemplateAttackData();

            var advancementDataSelector = GetNewInstanceOf<ICollectionDataSelector<AdvancementDataSelection>>();
            advancementData = advancementDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.Advancements);

            var creatureDataSelector = GetNewInstanceOf<ICollectionDataSelector<CreatureDataSelection>>();
            creatureData = creatureDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData);
        }

        [SetUp]
        public void Setup()
        {
            helper = new AttackHelper();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            featsSelector = GetNewInstanceOf<IFeatsSelector>();
            creatureDataSelector = GetNewInstanceOf<ICreatureDataSelector>();
        }

        [Test]
        public void AttackDataNames()
        {
            var creatures = CreatureConstants.GetAll();
            var templates = CreatureConstants.Templates.GetAll();

            Assert.That(creatureAttackData.Keys, Is.EquivalentTo(creatures));
            Assert.That(templateAttackData.Keys, Is.EquivalentTo(templates));

            var names = creatureAttackData.Keys.Union(templateAttackData.Keys);

            AssertCollectionNames(names);
        }

        [Test]
        public void AttackDamageKeysAreUnique()
        {
            var attackDamageKeys = new List<string>();

            foreach (var kvp in creatureAttackData)
            {
                var creature = kvp.Key;
                var sizes = advancementData[creature]
                    .Select(a => a.Size)
                    .Union([creatureData[creature].Single().Size]);

                foreach (var size in sizes)
                {
                    var keys = kvp.Value
                        .Select(Infrastructure.Helpers.DataHelper.Parse<AttackDataSelection>)
                        .Select(s => s.BuildDamageKey(creature, size));
                    attackDamageKeys.AddRange(keys);
                }
            }

            foreach (var kvp in templateAttackData)
            {
                var keys = kvp.Value
                    .Select(Infrastructure.Helpers.DataHelper.Parse<AttackDataSelection>)
                    .Select(s => s.BuildDamageKey(kvp.Key, string.Empty));
                attackDamageKeys.AddRange(keys);
            }

            Assert.That(attackDamageKeys, Is.Unique);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureAttackData(string creature)
        {
            if (!creatureAttackData[creature].Any())
                Assert.Fail("Test case did not specify attacks or NONE");

            if (creatureAttackData[creature][0] == AttackTestData.None)
                creatureAttackData[creature].Clear();

            AssertCreatureHasCorrectImprovedGrab(creatureAttackData[creature]);
            AssertCreatureHasCorrectSpellLikeAbility(creatureAttackData[creature]);
            AssertCreatureHasCorrectSpells(creatureAttackData[creature]);
            AssertCreatureEffectDoesNotHaveDamage(creatureAttackData[creature]);
            AssertNaturalAttacksHaveCorrectDamageTypes(creatureAttackData[creature]);
            AssertPoisonAttacksHaveCorrectDamageTypes(creatureAttackData[creature]);
            AssertDiseaseAttacksHaveCorrectDamageTypes(creatureAttackData[creature]);

            AssertData(creature, creatureAttackData[creature]);

            CreatureWithSpellLikeAbilityAttack_HasSpellLikeAbilitySpecialQuality(creature);
            CreatureWithPsionicAttack_HasPsionicSpecialQuality(creature);
            CreatureWithSpellsAttack_HasMagicSpells(creature);
            CreatureWithUnnaturalAttack_CanUseEquipment(creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateAttackData(string creature)
        {
            if (!templateAttackData[creature].Any())
                Assert.Fail("Test case did not specify attacks or NONE");

            if (templateAttackData[creature][0] == AttackTestData.None)
                templateAttackData[creature].Clear();

            foreach (var entry in templateAttackData[creature])
            {
                var stringEntry = helper.BuildEntry(entry);
                var attackValid = helper.ValidateEntry(stringEntry);
                Assert.That(attackValid, Is.True, $"{creature}: {entry[DataIndexConstants.AttackData.NameIndex]} is not valid attack data");

                var damageValid = damageHelper.ValidateEntries(entry[DataIndexConstants.AttackData.DamageDataIndex]);
                Assert.That(damageValid, Is.True, $"{creature}: {entry[DataIndexConstants.AttackData.NameIndex]}: {entry[DataIndexConstants.AttackData.DamageDataIndex]} is not valid damage data");
            }

            AssertCreatureHasCorrectImprovedGrab(templateAttackData[creature]);
            AssertCreatureHasCorrectSpellLikeAbility(templateAttackData[creature]);
            AssertCreatureHasCorrectSpells(templateAttackData[creature]);
            AssertCreatureEffectDoesNotHaveDamage(templateAttackData[creature]);
            AssertNaturalAttacksHaveCorrectDamageTypes(templateAttackData[creature]);
            AssertPoisonAttacksHaveCorrectDamageTypes(templateAttackData[creature]);
            AssertDiseaseAttacksHaveCorrectDamageTypes(templateAttackData[creature]);

            AssertData(creature, templateAttackData[creature]);
        }

        private void AssertCreatureEffectDoesNotHaveDamage(List<string[]> entries)
        {
            var damageTypes = new[]
            {
                AttributeConstants.DamageTypes.Bludgeoning.ToLower(),
                AttributeConstants.DamageTypes.Piercing.ToLower(),
                AttributeConstants.DamageTypes.Slashing.ToLower(),

                FeatConstants.Foci.Elements.Acid.ToLower(),
                FeatConstants.Foci.Elements.Cold.ToLower(),
                FeatConstants.Foci.Elements.Electricity.ToLower(),
                FeatConstants.Foci.Elements.Fire.ToLower(),
                FeatConstants.Foci.Elements.Sonic.ToLower(),

                AbilityConstants.Charisma.ToLower(),
                AbilityConstants.Constitution.ToLower(),
                AbilityConstants.Dexterity.ToLower(),
                AbilityConstants.Intelligence.ToLower(),
                AbilityConstants.Strength.ToLower(),
                AbilityConstants.Wisdom.ToLower(),
                "str",
                "con",
                "dex",
                "wis",
                "int",
                "cha",

                "level",
                "negative"
            };

            var attackNames = entries.Select(e => e[DataIndexConstants.AttackData.NameIndex]);

            foreach (var entry in entries)
            {
                if (attackNames.Contains(entry[DataIndexConstants.AttackData.DamageEffectIndex]))
                {
                    continue;
                }

                foreach (var damageType in damageTypes)
                {
                    var words = entry[DataIndexConstants.AttackData.DamageEffectIndex].ToLower().Split(' ');
                    Assert.That(words, Does.Not.Contain(damageType), entry[DataIndexConstants.AttackData.NameIndex]);
                }
            }
        }

        private void AssertNaturalAttacksHaveCorrectDamageTypes(List<string[]> entries)
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

            foreach (var entry in entries)
            {
                if (!damageTypes.ContainsKey(entry[DataIndexConstants.AttackData.NameIndex].ToLower()))
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

        private void AssertCreatureHasCorrectImprovedGrab(List<string> entries)
        {
            var selections = entries.Select(Infrastructure.Helpers.DataHelper.Parse<AttackDataSelection>);

            var improvedGrab = selections.FirstOrDefault(s => s.Name == "Improved Grab");
            if (improvedGrab == null)
            {
                return;
            }

            Assert.That(improvedGrab.AttackType, Is.EqualTo("extraordinary ability"));
            Assert.That(improvedGrab.DamageBonusMultiplier, Is.EqualTo(0));
            Assert.That(improvedGrab.DamageEffect, Is.Empty);
            Assert.That(improvedGrab.FrequencyQuantity, Is.EqualTo(1));
            Assert.That(improvedGrab.FrequencyTimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(improvedGrab.IsMelee, Is.True);
            Assert.That(improvedGrab.IsNatural, Is.True);
            Assert.That(improvedGrab.IsPrimary, Is.False);
            Assert.That(improvedGrab.IsSpecial, Is.True);
            Assert.That(improvedGrab.Name, Is.EqualTo("Improved Grab"));
            Assert.That(improvedGrab.SaveAbility, Is.Empty);
            Assert.That(improvedGrab.SaveDcBonus, Is.EqualTo(0));
            Assert.That(improvedGrab.Save, Is.Empty);
        }

        private void AssertCreatureHasCorrectSpells(List<string> entries)
        {
            var selections = entries.Select(Infrastructure.Helpers.DataHelper.Parse<AttackDataSelection>);

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

        private void AssertCreatureHasCorrectSpellLikeAbility(List<string> entries)
        {
            var selections = entries.Select(Infrastructure.Helpers.DataHelper.Parse<AttackDataSelection>);

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
    }
}
