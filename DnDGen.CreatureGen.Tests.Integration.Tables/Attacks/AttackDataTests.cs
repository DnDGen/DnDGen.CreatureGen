using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
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
        private DamageHelper damageHelper;
        private Dice dice;

        protected override string tableName => TableNameConstants.Collection.AttackData;

        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.AttackData.DamageDataIndex] = "Damage Data";
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
        }

        [SetUp]
        public void Setup()
        {
            helper = new AttackHelper();
            damageHelper = new DamageHelper();
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            featsSelector = GetNewInstanceOf<IFeatsSelector>();
            creatureDataSelector = GetNewInstanceOf<ICreatureDataSelector>();
            dice = GetNewInstanceOf<Dice>();
        }

        [Test]
        public void AttackDataNames()
        {
            var creatures = CreatureConstants.GetAll();
            var templates = CreatureConstants.Templates.GetAll();

            var names = creatures.Union(templates);

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(AttackTestData), nameof(AttackTestData.Creatures))]
        [TestCaseSource(typeof(AttackTestData), nameof(AttackTestData.Templates))]
        public void AttackData(string creature, List<string[]> entries)
        {
            if (!entries.Any())
                Assert.Fail("Test case did not specify attacks or NONE");

            if (entries[0][DataIndexConstants.AttackData.NameIndex] == AttackTestData.None)
                entries.Clear();

            foreach (var entry in entries)
            {
                var valid = damageHelper.ValidateEntries(entry[DataIndexConstants.AttackData.DamageDataIndex]);
                if (!valid)
                {
                    Assert.Fail($"Attack {entry[DataIndexConstants.AttackData.NameIndex]} for creature {creature} has invalid damage data: {entry[DataIndexConstants.AttackData.DamageDataIndex]}");
                }
            }

            AssertData(creature, entries);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureWithSpellLikeAbilityAttack_HasSpellLikeAbilitySpecialQuality(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                Assert.That(helper.ValidateEntry(entry), Is.True, $"Invalid entry: {entry}");
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
            var creatureType = new CreatureType();
            var types = collectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creatureName);

            creatureType.Name = types.First();
            creatureType.SubTypes = types.Skip(1);

            return creatureType;
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

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureWithPsionicAttack_HasPsionicSpecialQuality(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                Assert.That(helper.ValidateEntry(entry), Is.True, $"Invalid entry: {entry}");
            }

            var creatureType = GetCreatureType(creature);
            var specialQualities = featsSelector.SelectSpecialQualities(creature, creatureType);
            var hasPsionicAttack = table[creature]
                .Select(helper.ParseEntry)
                .Any(d => d[DataIndexConstants.AttackData.NameIndex] == FeatConstants.SpecialQualities.Psionic);

            var hasPsionicSpecialQuality = specialQualities.Any(q => q.Feat == FeatConstants.SpecialQualities.Psionic);
            Assert.That(hasPsionicAttack, Is.EqualTo(hasPsionicSpecialQuality));
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureWithSpellsAttack_HasMagicSpells(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                Assert.That(helper.ValidateEntry(entry), Is.True, $"Invalid entry: {entry}");
            }

            var hasSpellsAttack = table[creature]
                .Select(helper.ParseEntry)
                .Any(d => d[DataIndexConstants.AttackData.NameIndex] == "Spells");

            var caster = collectionSelector.SelectFrom(TableNameConstants.TypeAndAmount.Casters, creature);

            if (hasSpellsAttack)
            {
                Assert.That(caster, Is.Not.Empty);
            }
            else
            {
                Assert.That(caster, Is.Empty);
            }
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureWithUnnaturalAttack_CanUseEquipment(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                Assert.That(helper.ValidateEntry(entry), Is.True, $"Invalid entry: {entry}");
            }

            var creatureData = creatureDataSelector.SelectFor(creature);
            var hasUnnaturalAttack = table[creature]
                .Select(helper.ParseEntry)
                .Any(d => !Convert.ToBoolean(d[DataIndexConstants.AttackData.IsNaturalIndex]));

            if (!hasUnnaturalAttack)
            {
                Assert.Pass($"{creature} has all-natural, 100% USDA Organic attacks");
            }

            Assert.That(hasUnnaturalAttack, Is.True.And.EqualTo(creatureData.CanUseEquipment));
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureWithUnnaturalAttack_HasNoDamageForEquipmentAttacks(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                Assert.That(helper.ValidateEntry(entry), Is.True, $"Invalid entry: {entry}");
            }

            var unnaturalAttacks = table[creature]
                .Select(helper.ParseEntry)
                .Where(d => !Convert.ToBoolean(d[DataIndexConstants.AttackData.IsNaturalIndex]));

            if (!unnaturalAttacks.Any())
            {
                Assert.Pass($"{creature} has all-natural, 100% USDA Organic attacks");
            }

            foreach (var attack in unnaturalAttacks)
            {
                if (attack[DataIndexConstants.AttackData.NameIndex] != AttributeConstants.Melee
                    && attack[DataIndexConstants.AttackData.NameIndex] != AttributeConstants.Ranged)
                {
                    continue;
                }

                Assert.That(attack[DataIndexConstants.AttackData.DamageDataIndex], Is.Empty, attack[DataIndexConstants.AttackData.NameIndex]);
            }
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureWithUnnaturalAttack_HasNaturalAttack(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                Assert.That(helper.ValidateEntry(entry), Is.True, $"Invalid entry: {entry}");
            }

            var hasUnnaturalAttack = table[creature]
                .Select(helper.ParseEntry)
                .Any(d => !Convert.ToBoolean(d[DataIndexConstants.AttackData.IsNaturalIndex]));

            if (!hasUnnaturalAttack)
            {
                Assert.Pass($"{creature} has all-natural, 100% USDA Organic attacks");
            }

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

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureHasCorrectImprovedGrab(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                Assert.That(helper.ValidateEntry(entry), Is.True, $"Invalid entry: {entry}");
            }

            var improvedGrab = table[creature]
                .Select(helper.ParseEntry)
                .FirstOrDefault(d => d[DataIndexConstants.AttackData.NameIndex] == "Improved Grab");

            if (improvedGrab == null)
            {
                Assert.Pass($"{creature} does not have attack 'Improved Grab'");
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

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureHasCorrectSpells(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                Assert.That(helper.ValidateEntry(entry), Is.True, $"Invalid entry: {entry}");
            }

            var spells = table[creature]
                .Select(helper.ParseEntry)
                .FirstOrDefault(d => d[DataIndexConstants.AttackData.NameIndex] == "Spells");

            if (spells == null)
            {
                Assert.Pass($"{creature} does not have attack 'Spells'");
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

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureHasCorrectSpellLikeAbility(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            foreach (var entry in table[creature])
            {
                Assert.That(helper.ValidateEntry(entry), Is.True, $"Invalid entry: {entry}");
            }

            var spellLikeAbility = table[creature]
                .Select(helper.ParseEntry)
                .FirstOrDefault(d => d[DataIndexConstants.AttackData.NameIndex] == FeatConstants.SpecialQualities.SpellLikeAbility);

            if (spellLikeAbility == null)
            {
                Assert.Pass($"{creature} does not have attack '{FeatConstants.SpecialQualities.SpellLikeAbility}'");
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
