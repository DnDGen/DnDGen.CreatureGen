using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
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
    public class AttackDataTests : CollectionTests
    {
        private IFeatsSelector featsSelector;
        private Dictionary<string, List<string>> creatureAttackData;
        private Dictionary<string, List<string>> templateAttackData;
        private Dictionary<string, CreatureDataSelection> creatureData;

        protected override string tableName => TableNameConstants.Collection.AttackData;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            creatureAttackData = AttackTestData.GetCreatureAttackData();
            templateAttackData = AttackTestData.GetTemplateAttackData();

            var creatureDataSelector = GetNewInstanceOf<ICollectionDataSelector<CreatureDataSelection>>();
            creatureData = creatureDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Single());
        }

        [SetUp]
        public void Setup()
        {
            featsSelector = GetNewInstanceOf<IFeatsSelector>();
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

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureAttackData(string creature)
        {
            if (!creatureAttackData[creature].Any())
                Assert.Fail("Test case did not specify attacks or NONE");

            if (creatureAttackData[creature][0] == AttackTestData.None)
                creatureAttackData[creature].Clear();

            AssertImprovedGrabAttack(creatureAttackData[creature]);
            AssertConstrictAttack(creatureAttackData[creature]);
            AssertSpellLikeAbilityAttack(creatureAttackData[creature]);
            AssertSpellsAttack(creatureAttackData[creature]);
            AssertDamageEffectDoesNotHaveDamage(creatureAttackData[creature]);
            AssertDamageEffectAttackIsNotPrimary(creatureAttackData[creature]);
            AssertPoisonAttacks(creatureAttackData[creature]);
            AssertDiseaseAttacks(creatureAttackData[creature]);
            AssertSpecialAttacks(creatureAttackData[creature]);

            AssertCollection(creature, [.. creatureAttackData[creature]]);

            CreatureWithSpellLikeAbilityAttack_HasSpellLikeAbilitySpecialQuality(creature);
            CreatureWithPsionicAttack_HasPsionicSpecialQuality(creature);
            CreatureWithSpellsAttack_HasMagicSpells(creature);
            CreatureWithUnnaturalAttack_CanUseEquipment(creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateAttackData(string template)
        {
            if (!templateAttackData[template].Any())
                Assert.Fail("Test case did not specify attacks or NONE");

            if (templateAttackData[template][0] == AttackTestData.None)
                templateAttackData[template].Clear();

            AssertImprovedGrabAttack(templateAttackData[template]);
            AssertConstrictAttack(templateAttackData[template]);
            AssertSpellLikeAbilityAttack(templateAttackData[template]);
            AssertSpellsAttack(templateAttackData[template]);
            AssertDamageEffectDoesNotHaveDamage(templateAttackData[template]);
            AssertDamageEffectAttackIsNotPrimary(templateAttackData[template]);
            AssertPoisonAttacks(templateAttackData[template]);
            AssertDiseaseAttacks(templateAttackData[template]);
            AssertSpecialAttacks(templateAttackData[template]);

            AssertCollection(template, [.. templateAttackData[template]]);
        }

        private void AssertDamageEffectDoesNotHaveDamage(List<string> entries)
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

            var selections = entries.Select(DataHelper.Parse<AttackDataSelection>);
            var attackNames = selections.Select(s => s.Name);
            var nonDamageEffectAttacks = selections.Where(s => !attackNames.Contains(s.DamageEffect));

            foreach (var selection in nonDamageEffectAttacks)
            {
                var damageEffectWords = selection.DamageEffect.ToLower().Split(' ');
                Assert.That(damageEffectWords.Intersect(damageTypes), Is.Empty, selection.Name);
            }
        }

        private void AssertDamageEffectAttackIsNotPrimary(List<string> entries)
        {
            var selections = entries.Select(DataHelper.Parse<AttackDataSelection>);
            var damageEffects = selections.Select(s => s.DamageEffect);
            var damageEffectAttacks = selections.Where(s => damageEffects.Contains(s.Name));

            foreach (var selection in damageEffectAttacks)
            {
                Assert.That(selection.IsSpecial, Is.True, selection.Name);
                Assert.That(selection.IsPrimary, Is.False, selection.Name);
                Assert.That(selection.IsNatural, Is.True, selection.Name);
            }
        }

        private void AssertSpecialAttacks(List<string> entries)
        {
            var attackTypes = new[]
            {
                "spell-like",
                "supernatural",
                "extraordinary"
            };
            var specialAttacks = entries
                .Select(DataHelper.Parse<AttackDataSelection>)
                .Where(s => attackTypes.Any(a => s.AttackType.Contains(a)));

            foreach (var selection in specialAttacks)
            {
                Assert.That(selection.IsSpecial, Is.True, selection.Name);
            }
        }

        private void AssertPoisonAttacks(List<string> entries)
        {
            var poisons = entries
                .Select(DataHelper.Parse<AttackDataSelection>)
                .Where(s => s.Name.Equals("poison", StringComparison.CurrentCultureIgnoreCase));

            foreach (var selection in poisons)
            {
                Assert.That(selection.IsSpecial, Is.True, "Special");
                Assert.That(selection.IsMelee, Is.True, "Melee");
                Assert.That(selection.IsPrimary, Is.False, "Primary");
                Assert.That(selection.Save, Is.Not.Empty.And.EqualTo(SaveConstants.Fortitude));

                if (selection.IsNatural)
                    Assert.That(selection.SaveAbility, Is.Not.Empty);
                else
                    Assert.That(selection.SaveAbility, Is.Empty);
            }
        }

        private void AssertDiseaseAttacks(List<string> entries)
        {
            var selections = entries.Select(DataHelper.Parse<AttackDataSelection>);

            var disease = selections.FirstOrDefault(s => s.Name.Equals("disease", StringComparison.CurrentCultureIgnoreCase));
            if (disease == null)
                return;

            Assert.That(disease.IsSpecial, Is.True);
            Assert.That(disease.IsPrimary, Is.False);
            Assert.That(disease.SaveAbility, Is.Empty);
            Assert.That(disease.Save, Is.Empty);
            Assert.That(disease.DamageEffect, Is.Not.Empty);

            var specificDisease = selections.FirstOrDefault(s => s.Name == disease.DamageEffect);
            if (specificDisease == null)
                Assert.Fail($"Could not find disease '{disease.DamageEffect}'");

            Assert.That(specificDisease.IsSpecial, Is.True);
            Assert.That(specificDisease.IsPrimary, Is.False);
            Assert.That(specificDisease.SaveAbility, Is.Not.Empty);
            Assert.That(specificDisease.Save, Is.Not.Empty);
        }

        private void CreatureWithSpellLikeAbilityAttack_HasSpellLikeAbilitySpecialQuality(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            var creatureType = GetCreatureType(creature);
            var specialQualities = featsSelector.SelectSpecialQualities(creature, creatureType);
            var hasSpellLikeAbilityAttack = table[creature]
                .Select(DataHelper.Parse<AttackDataSelection>)
                .Any(s => s.Name == FeatConstants.SpecialQualities.SpellLikeAbility);

            //INFO: Want to ignore constant effects such as Doppelganger's Detect Thoughts and Copper Dragon's Spider Climb
            var hasSpellLikeAbilitySpecialQuality = specialQualities.Any(q =>
                q.Feat == FeatConstants.SpecialQualities.SpellLikeAbility
                && q.FrequencyTimePeriod != FeatConstants.Frequencies.Constant);
            Assert.That(hasSpellLikeAbilityAttack, Is.EqualTo(hasSpellLikeAbilitySpecialQuality));
        }

        private CreatureType GetCreatureType(string creatureName) => new(creatureData[creatureName].Types);

        private void CreatureWithPsionicAttack_HasPsionicSpecialQuality(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            var creatureType = GetCreatureType(creature);
            var specialQualities = featsSelector.SelectSpecialQualities(creature, creatureType);
            var hasPsionicAttack = table[creature]
                .Select(DataHelper.Parse<AttackDataSelection>)
                .Any(s => s.Name == FeatConstants.SpecialQualities.Psionic);

            var hasPsionicSpecialQuality = specialQualities.Any(q => q.Feat == FeatConstants.SpecialQualities.Psionic);
            Assert.That(hasPsionicAttack, Is.EqualTo(hasPsionicSpecialQuality));
        }

        private void CreatureWithSpellsAttack_HasMagicSpells(string creature)
        {
            Assert.That(table, Contains.Key(creature));

            var hasSpellsAttack = table[creature]
                .Select(DataHelper.Parse<AttackDataSelection>)
                .Any(s => s.Name == "Spells");

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

            var selections = table[creature].Select(DataHelper.Parse<AttackDataSelection>);
            var unnaturalAttacks = selections.Where(s => !s.IsNatural);

            if (!unnaturalAttacks.Any())
            {
                return;
            }

            Assert.That(unnaturalAttacks.Any(), Is.True.And.EqualTo(creatureData[creature].CanUseEquipment));

            //Has Natural Attack
            var naturalAttack = selections.FirstOrDefault(s => s.IsNatural && !s.IsSpecial);

            Assert.That(naturalAttack, Is.Not.Null);
            Assert.That(naturalAttack.Name, Is.Not.Empty);
            Assert.That(naturalAttack.IsNatural, Is.True, naturalAttack.Name);
            Assert.That(naturalAttack.IsSpecial, Is.False, naturalAttack.Name);
        }

        private void AssertImprovedGrabAttack(List<string> entries)
        {
            var selections = entries.Select(DataHelper.Parse<AttackDataSelection>);

            var improvedGrab = selections.FirstOrDefault(s => s.Name == "Improved Grab");
            if (improvedGrab == null)
            {
                return;
            }

            Assert.That(improvedGrab.AttackType, Is.EqualTo("extraordinary ability"));
            Assert.That(improvedGrab.DamageBonusMultiplier, Is.Zero);
            Assert.That(improvedGrab.FrequencyQuantity, Is.EqualTo(1));
            Assert.That(improvedGrab.FrequencyTimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(improvedGrab.IsMelee, Is.True);
            Assert.That(improvedGrab.IsNatural, Is.True);
            Assert.That(improvedGrab.IsPrimary, Is.False);
            Assert.That(improvedGrab.IsSpecial, Is.True);
            Assert.That(improvedGrab.Name, Is.EqualTo("Improved Grab"));
            Assert.That(improvedGrab.SaveAbility, Is.Empty);
            Assert.That(improvedGrab.SaveDcBonus, Is.Zero);
            Assert.That(improvedGrab.Save, Is.Empty);
        }

        private void AssertConstrictAttack(List<string> entries)
        {
            var selections = entries.Select(DataHelper.Parse<AttackDataSelection>);

            var constrict = selections.FirstOrDefault(s => s.Name == "Constrict");
            if (constrict == null)
            {
                return;
            }

            Assert.That(constrict.AttackType, Is.EqualTo("extraordinary ability"));
            Assert.That(constrict.FrequencyQuantity, Is.EqualTo(1));
            Assert.That(constrict.FrequencyTimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(constrict.IsMelee, Is.True);
            Assert.That(constrict.IsNatural, Is.True);
            Assert.That(constrict.IsPrimary, Is.False);
            Assert.That(constrict.IsSpecial, Is.True);
            Assert.That(constrict.Name, Is.EqualTo("Constrict"));
            Assert.That(constrict.SaveAbility, Is.Empty);
            Assert.That(constrict.SaveDcBonus, Is.Zero);
            Assert.That(constrict.Save, Is.Empty);
        }

        private void AssertSpellsAttack(List<string> entries)
        {
            var selections = entries.Select(DataHelper.Parse<AttackDataSelection>);

            var spells = selections.FirstOrDefault(s => s.Name == "Spells");
            if (spells == null)
            {
                return;
            }

            Assert.That(spells.AttackType, Is.EqualTo("spell-like ability"));
            Assert.That(spells.DamageBonusMultiplier, Is.EqualTo(0));
            Assert.That(spells.DamageEffect, Is.Empty);
            Assert.That(spells.FrequencyQuantity, Is.EqualTo(1));
            Assert.That(spells.FrequencyTimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(spells.IsMelee, Is.False);
            Assert.That(spells.IsNatural, Is.True);
            Assert.That(spells.IsPrimary, Is.True);
            Assert.That(spells.IsSpecial, Is.True);
            Assert.That(spells.Name, Is.EqualTo("Spells"));
            Assert.That(spells.SaveAbility, Is.Empty);
            Assert.That(spells.SaveDcBonus, Is.EqualTo(0));
            Assert.That(spells.Save, Is.Empty);
        }

        private void AssertSpellLikeAbilityAttack(List<string> entries)
        {
            var selections = entries.Select(DataHelper.Parse<AttackDataSelection>);

            var spellLikeAbility = selections.FirstOrDefault(s => s.Name == FeatConstants.SpecialQualities.SpellLikeAbility);
            if (spellLikeAbility == null)
            {
                return;
            }

            Assert.That(spellLikeAbility.AttackType, Is.EqualTo("spell-like ability"));
            Assert.That(spellLikeAbility.DamageBonusMultiplier, Is.EqualTo(0));
            Assert.That(spellLikeAbility.DamageEffect, Is.Empty);
            Assert.That(spellLikeAbility.FrequencyQuantity, Is.EqualTo(1));
            Assert.That(spellLikeAbility.FrequencyTimePeriod, Is.EqualTo(FeatConstants.Frequencies.Round));
            Assert.That(spellLikeAbility.IsMelee, Is.False);
            Assert.That(spellLikeAbility.IsNatural, Is.True);
            Assert.That(spellLikeAbility.IsPrimary, Is.True);
            Assert.That(spellLikeAbility.IsSpecial, Is.True);
            Assert.That(spellLikeAbility.Name, Is.EqualTo(FeatConstants.SpecialQualities.SpellLikeAbility));
            Assert.That(spellLikeAbility.SaveAbility, Is.Empty);
            Assert.That(spellLikeAbility.SaveDcBonus, Is.EqualTo(0));
            Assert.That(spellLikeAbility.Save, Is.Empty);
        }
    }
}
