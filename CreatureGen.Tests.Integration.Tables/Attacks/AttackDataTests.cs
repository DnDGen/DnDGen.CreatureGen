using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Helpers;
using CreatureGen.Tables;
using CreatureGen.Tests.Integration.TestData;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Attacks
{
    [TestFixture]
    public class AttackDataTests : DataTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        //INFO: Need this for the feats selector
        [Inject]
        public ClientIDManager ClientIDManager { get; set; }
        [Inject]
        internal IFeatsSelector FeatsSelector { get; set; }
        [Inject]
        internal ICreatureDataSelector CreatureDataSelector { get; set; }

        protected override string tableName => TableNameConstants.Collection.AttackData;

        protected override void PopulateIndices(IEnumerable<string> collection)
        {
            indices[DataIndexConstants.AttackData.DamageRollIndex] = "Damage Roll";
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
            var clientId = Guid.NewGuid();
            ClientIDManager.SetClientID(clientId);

            helper = new AttackHelper();
        }

        [Test]
        public void AttackDataNames()
        {
            var names = CreatureConstants.All();
            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(AttackTestData), "Creatures")]
        public void AttackData(string creature, List<string[]> entries)
        {
            if (!entries.Any())
                Assert.Fail("Test case did not specify attacks or NONE");

            if (entries[0][DataIndexConstants.AttackData.NameIndex] == AttackTestData.None)
                entries.Clear();

            AssertData(creature, entries);
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreatureWithSpellLikeAbilityAttack_HasSpellLikeAbilitySpecialQuality(string creature)
        {
            Assert.That(table, Contains.Key(creature));
            Assert.That(table[creature].All(helper.ValidateEntry), Is.True);

            var creatureType = GetCreatureType(creature);
            var specialQualities = FeatsSelector.SelectSpecialQualities(creature, creatureType);
            var hasSpellLikeAbilityAttack = table[creature]
                .Select(helper.ParseEntry)
                .Any(d => d[DataIndexConstants.AttackData.NameIndex] == FeatConstants.SpecialQualities.SpellLikeAbility);

            var hasSpellLikeAbilitySpecialQuality = specialQualities.Any(q => q.Feat == FeatConstants.SpecialQualities.SpellLikeAbility);
            Assert.That(hasSpellLikeAbilityAttack, Is.EqualTo(hasSpellLikeAbilitySpecialQuality));
        }

        private CreatureType GetCreatureType(string creatureName)
        {
            var creatureType = new CreatureType();
            var types = CollectionSelector.SelectFrom(TableNameConstants.Collection.CreatureTypes, creatureName);

            creatureType.Name = types.First();
            creatureType.SubTypes = types.Skip(1);

            return creatureType;
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreatureWithPsionicAttack_HasPsionicSpecialQuality(string creature)
        {
            Assert.That(table, Contains.Key(creature));
            Assert.That(table[creature].All(helper.ValidateEntry), Is.True);

            var creatureType = GetCreatureType(creature);
            var specialQualities = FeatsSelector.SelectSpecialQualities(creature, creatureType);
            var hasPsionicAttack = table[creature]
                .Select(helper.ParseEntry)
                .Any(d => d[DataIndexConstants.AttackData.NameIndex] == FeatConstants.SpecialQualities.Psionic);

            var hasPsionicSpecialQuality = specialQualities.Any(q => q.Feat == FeatConstants.SpecialQualities.Psionic);
            Assert.That(hasPsionicAttack, Is.EqualTo(hasPsionicSpecialQuality));
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        [Ignore("Need to implement magic first")]
        public void CreatureWithSpellsAttack_HasMagicSpells(string creature)
        {
            Assert.That(table, Contains.Key(creature));
            Assert.That(table[creature].All(helper.ValidateEntry), Is.True);

            //TODO: Get creature magic
            var hasSpellsAttack = table[creature]
                .Select(helper.ParseEntry)
                .Any(d => d[DataIndexConstants.AttackData.NameIndex] == "Spells");

            Assert.Fail("not yet written");
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreatureWithUnnaturalAttack_CanUseEquipment(string creature)
        {
            Assert.That(table, Contains.Key(creature));
            Assert.That(table[creature].All(helper.ValidateEntry), Is.True);

            var creatureData = CreatureDataSelector.SelectFor(creature);
            var hasUnnaturalAttack = table[creature]
                .Select(helper.ParseEntry)
                .Any(d => !Convert.ToBoolean(d[DataIndexConstants.AttackData.IsNaturalIndex]));

            Assert.That(hasUnnaturalAttack, Is.EqualTo(creatureData.CanUseEquipment));
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreatureHasNaturalMeleeAttack(string creature)
        {
            Assert.That(table, Contains.Key(creature));
            Assert.That(table[creature].All(helper.ValidateEntry), Is.True);

            var naturalAttack = table[creature]
                .Select(helper.ParseEntry)
                .FirstOrDefault(d => d[DataIndexConstants.AttackData.IsNaturalIndex] == bool.TrueString
                    && d[DataIndexConstants.AttackData.IsMeleeIndex] == bool.TrueString
                    && d[DataIndexConstants.AttackData.IsSpecialIndex] == bool.FalseString);

            Assert.That(naturalAttack, Is.Not.Null);
            Assert.That(naturalAttack[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("melee"));
            Assert.That(naturalAttack[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(naturalAttack[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(bool.TrueString));
            Assert.That(naturalAttack[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(bool.TrueString));
            Assert.That(naturalAttack[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(bool.FalseString));
            Assert.That(naturalAttack[DataIndexConstants.AttackData.NameIndex], Is.Not.Empty);
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreatureHasCorrectImprovedGrab(string creature)
        {
            Assert.That(table, Contains.Key(creature));
            Assert.That(table[creature].All(helper.ValidateEntry), Is.True);

            var improvedGrab = table[creature]
                .Select(helper.ParseEntry)
                .FirstOrDefault(d => d[DataIndexConstants.AttackData.NameIndex] == "Improved Grab");

            if (improvedGrab == null)
            {
                Assert.Pass();
            }

            Assert.That(improvedGrab[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("extraordinary ability"));
            Assert.That(improvedGrab[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo("0"));
            Assert.That(improvedGrab[DataIndexConstants.AttackData.DamageEffectIndex], Is.Empty);
            Assert.That(improvedGrab[DataIndexConstants.AttackData.DamageRollIndex], Is.Empty);
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

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreatureHasCorrectSpells(string creature)
        {
            Assert.That(table, Contains.Key(creature));
            Assert.That(table[creature].All(helper.ValidateEntry), Is.True);

            var spells = table[creature]
                .Select(helper.ParseEntry)
                .FirstOrDefault(d => d[DataIndexConstants.AttackData.NameIndex] == "Spells");

            if (spells == null)
            {
                Assert.Pass();
            }

            Assert.That(spells[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("spell-like ability"));
            Assert.That(spells[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo("0"));
            Assert.That(spells[DataIndexConstants.AttackData.DamageEffectIndex], Is.Empty);
            Assert.That(spells[DataIndexConstants.AttackData.DamageRollIndex], Is.Empty);
            Assert.That(spells[DataIndexConstants.AttackData.FrequencyQuantityIndex], Is.EqualTo("1"));
            Assert.That(spells[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(spells[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(bool.FalseString));
            Assert.That(spells[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(bool.TrueString));
            Assert.That(spells[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(bool.FalseString));
            Assert.That(spells[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(bool.TrueString));
            Assert.That(spells[DataIndexConstants.AttackData.NameIndex], Is.EqualTo("Spells"));
            Assert.That(spells[DataIndexConstants.AttackData.SaveAbilityIndex], Is.Empty);
            Assert.That(spells[DataIndexConstants.AttackData.SaveDcBonusIndex], Is.EqualTo("0"));
            Assert.That(spells[DataIndexConstants.AttackData.SaveIndex], Is.Empty);
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreatureHasCorrectSpellLikeAbility(string creature)
        {
            Assert.That(table, Contains.Key(creature));
            Assert.That(table[creature].All(helper.ValidateEntry), Is.True);

            var spellLikeAbility = table[creature]
                .Select(helper.ParseEntry)
                .FirstOrDefault(d => d[DataIndexConstants.AttackData.NameIndex] == "Spells");

            if (spellLikeAbility == null)
            {
                Assert.Pass();
            }

            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.AttackTypeIndex], Is.EqualTo("spell-like ability"));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.DamageBonusMultiplierIndex], Is.EqualTo("0"));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.DamageEffectIndex], Is.Empty);
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.DamageRollIndex], Is.Empty);
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.FrequencyQuantityIndex], Is.EqualTo("1"));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.FrequencyTimePeriodIndex], Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.IsMeleeIndex], Is.EqualTo(bool.FalseString));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.IsNaturalIndex], Is.EqualTo(bool.TrueString));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.IsPrimaryIndex], Is.EqualTo(bool.FalseString));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.IsSpecialIndex], Is.EqualTo(bool.TrueString));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.NameIndex], Is.EqualTo("Spell-Like Ability"));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.SaveAbilityIndex], Is.Empty);
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.SaveDcBonusIndex], Is.EqualTo("0"));
            Assert.That(spellLikeAbility[DataIndexConstants.AttackData.SaveIndex], Is.Empty);
        }
    }
}
