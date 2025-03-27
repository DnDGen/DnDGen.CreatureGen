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
        private Dictionary<string, List<string>> creatureAttackData;
        private Dictionary<string, List<string>> templateAttackData;
        private Dictionary<string, List<string>> creatureAttackDamageData;
        private Dictionary<string, List<string>> templateAttackDamageData;
        private Dictionary<string, IEnumerable<AdvancementDataSelection>> advancementData;
        private Dictionary<string, CreatureDataSelection> creatureData;

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
        }

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            featsSelector = GetNewInstanceOf<IFeatsSelector>();
        }

        [Test]
        public void DamageDataNames()
        {
            var creatureKeys = AttackTestData.GetCreatureDamageKeys();
            var templateKeys = AttackTestData.GetTemplateDamageKeys();
            var names = creatureKeys.Union(templateKeys);

            var testKeys = creatureAttackDamageData.Keys.Union(templateAttackDamageData.Keys);
            Assert.That(testKeys, Is.EquivalentTo(names));

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureAttackDamageData(string creature)
        {
            var attacks = creatureAttackData[creature].Select(DataHelper.Parse<AttackDataSelection>);
            var sizes = advancementData[creature]
                .Select(a => a.Size)
                .Union([creatureData[creature].Size]);

            foreach (var attack in attacks)
            {
                foreach (var size in sizes)
                {
                    var key = attack.BuildDamageKey(creature, size);
                    Assert.That(creatureAttackDamageData, Contains.Key(key));
                    AssertCreatureAttackDamages(key, creature);
                }
            }
        }

        private void AssertCreatureAttackDamages(string key, string creature)
        {
            AssertCreatureHasCorrectImprovedGrab(creatureAttackDamageData[key]);
            AssertCreatureHasCorrectSpellLikeAbility(creatureAttackDamageData[key]);
            AssertCreatureHasCorrectSpells(creatureAttackDamageData[key]);
            AssertCreatureEffectDoesNotHaveDamage(creatureAttackDamageData[key]);
            AssertNaturalAttacksHaveCorrectDamageTypes(creatureAttackDamageData[key]);
            AssertPoisonAttacksHaveCorrectDamageTypes(creatureAttackDamageData[key]);
            AssertDiseaseAttacksHaveCorrectDamageTypes(creatureAttackDamageData[key]);

            AssertCollection(key, creatureAttackDamageData[key]);

            CreatureWithSpellLikeAbilityAttack_HasSpellLikeAbilitySpecialQuality(creature);
            CreatureWithPsionicAttack_HasPsionicSpecialQuality(creature);
            CreatureWithSpellsAttack_HasMagicSpells(creature);
            CreatureWithUnnaturalAttack_CanUseEquipment(creature);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateAttackDamageData(string template)
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

        private void AssertNaturalAttacksHaveCorrectDamageTypes(string key, List<string> entries)
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

            foreach (var kvp in damageTypes)
            {
                if (!key.Contains(kvp.Key, StringComparison.CurrentCultureIgnoreCase))
                    continue;

                Assert.That(entries, Is.Not.Empty, key);

                var damageType = kvp.Value;
                var firstDamage = DataHelper.Parse<DamageDataSelection>(entries[0]);
                Assert.That(firstDamage.Type, Is.EqualTo(damageType), key);
            }
        }

        private void AssertPoisonAttacksHaveCorrectDamageTypes(string creature, string key, List<string> entries)
        {
            if (!key.Contains("Poison"))
                return;

            var damageSelections = entries.Select(DataHelper.Parse<DamageDataSelection>).ToArray();
            Assert.That(damageSelections, Has.Length.AtMost(2).Or.Length.EqualTo(4), key);

            if (damageSelections.Length == 4)
            {
                Assert.That(damageSelections[0].Condition, Is.EqualTo("Initial"));
                Assert.That(damageSelections[1].Condition, Is.EqualTo("Initial"));
                Assert.That(damageSelections[2].Condition, Is.EqualTo("Secondary"));
                Assert.That(damageSelections[3].Condition, Is.EqualTo("Secondary"));
            }
            else if (damageSelections.Length == 2)
            {
                Assert.That(damageSelections[0].Condition, Is.EqualTo("Initial"));
                Assert.That(damageSelections[1].Condition, Is.EqualTo("Secondary"));
            }
            else if (damageSelections.Length == 1)
            {
                Assert.That(damageSelections[0].Condition, Is.EqualTo("Initial"));
            }
            else
            {
                var poisonAttack = creatureAttackData[creature]
                    .Select(DataHelper.Parse<AttackDataSelection>)
                    .FirstOrDefault(a => a.Name.Equals("poison", StringComparison.CurrentCultureIgnoreCase));

                Assert.That(poisonAttack.DamageEffect, Is.Not.Empty);
            }
        }

        private void AssertDiseaseAttacksHaveCorrectDamageTypes(string creature, string key, List<string> entries)
        {
            var attackSelections = creatureAttackData[creature].Select(DataHelper.Parse<AttackDataSelection>);

            var disease = attackSelections.FirstOrDefault(s => s.Name.Equals("disease", StringComparison.CurrentCultureIgnoreCase));
            if (disease == null)
                return;

            var specificDisease = attackSelections.FirstOrDefault(s => s.Name == disease.DamageEffect);
            if (specificDisease == null)
                Assert.Fail($"Could not find disease '{disease.DamageEffect}'");

            if (!key.Contains(specificDisease.Name))
                return;

            var damageSelections = entries.Select(DataHelper.Parse<DamageDataSelection>);
            foreach (var damageSelection in damageSelections)
            {
                Assert.That(damageSelection.Roll, Is.Not.Empty, key);
                Assert.That(damageSelection.Type, Is.Not.Empty, key);
                Assert.That(damageSelection.Condition, Does.StartWith("Incubation period"), key);
            }
        }

        private void CreatureWithUnnaturalAttack_CanUseEquipment(string creature, string key)
        {
            //No Damage for Equipment attacks
            if (key.Contains(AttributeConstants.Melee) || key.Contains(AttributeConstants.Ranged))
            {
                Assert.That(creatureAttackDamageData[key], Is.Empty, key);
            }

            //Has Natural Attack with Damage
            var naturalAttack = creatureAttackData[creature]
                .Select(DataHelper.Parse<AttackDataSelection>)
                .FirstOrDefault(a => a.IsNatural && !a.IsSpecial);

            Assert.That(naturalAttack, Is.Not.Null);
            Assert.That(naturalAttack.Name, Is.Not.Empty);
            Assert.That(naturalAttack.IsNatural, Is.True, naturalAttack.Name);
            Assert.That(naturalAttack.IsSpecial, Is.False, naturalAttack.Name);

            if (key.Contains(naturalAttack.Name))
                Assert.That(creatureAttackDamageData[key], Is.Not.Empty, key);
        }

        private void AssertCreatureHasCorrectImprovedGrab(string key, List<string> entries) => AssertEmpty(key, entries, "Improved Grab");
        private void AssertCreatureHasCorrectSpells(string key, List<string> entries) => AssertEmpty(key, entries, "Spells");
        private void AssertCreatureHasCorrectSpellLikeAbility(string key, List<string> entries) => AssertEmpty(key, entries, FeatConstants.SpecialQualities.SpellLikeAbility);

        private void AssertEmpty(string key, List<string> entries, string attackName)
        {
            if (key.Contains(attackName))
            {
                Assert.That(entries, Is.Empty, key);
            }
        }
    }
}
