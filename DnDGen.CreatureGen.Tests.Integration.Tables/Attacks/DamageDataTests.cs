using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.Tables.Creatures;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Helpers;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Attacks
{
    [TestFixture]
    public class DamageDataTests : CollectionTests
    {
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
            creatureData = CreatureDataTests.GetCreatureDataSelections();
            advancementData = AdvancementsTests.GetAdvancementsTestData().ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(DataHelper.Parse<AdvancementDataSelection>));
        }

        [Test]
        public void DamageDataNames()
        {
            var creatureKeys = AttackTestData.GetCreatureDamageKeys();
            var templateKeys = AttackTestData.GetTemplateDamageKeys();
            var names = creatureKeys.Concat(templateKeys);

            var testKeys = creatureAttackDamageData.Keys.Union(templateAttackDamageData.Keys);
            Assert.That(testKeys, Is.Unique.And.EquivalentTo(names));

            var computedKeys = CreatureConstants.GetAll()
                .SelectMany(GetCreatureDamageKeys)
                .Concat(CreatureConstants.Templates.GetAll().SelectMany(GetTemplateDamageKeys));
            Assert.That(computedKeys, Is.Unique.And.EquivalentTo(names));

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureAttackDamageData(string creature)
        {
            var keys = GetCreatureDamageKeys(creature);
            foreach (var key in keys)
            {
                AssertCreatureAttackDamages(key, creature);
            }
        }

        private IEnumerable<string> GetCreatureDamageKeys(string creature)
        {
            var attacks = creatureAttackData[creature].Select(DataHelper.Parse<AttackDataSelection>);
            var sizes = advancementData[creature]
                .Select(a => a.Size)
                .Union([creatureData[creature].Size]);

            foreach (var attack in attacks)
            {
                foreach (var size in sizes)
                {
                    yield return attack.BuildDamageKey(creature, size);
                }
            }
        }

        private void AssertCreatureAttackDamages(string key, string creature)
        {
            Assert.That(creatureAttackDamageData, Contains.Key(key));

            AssertCorrectImprovedGrab(key, creatureAttackDamageData[key]);
            AssertCorrectSpellLikeAbility(key, creatureAttackDamageData[key]);
            AssertCorrectSpells(key, creatureAttackDamageData[key]);
            AssertNaturalAttacksHaveCorrectDamageTypes(key, creatureAttackDamageData[key]);
            AssertPoisonAttacksHaveCorrectDamageTypes(creature, key, creatureAttackDamageData[key]);
            AssertDiseaseAttacksHaveCorrectDamageTypes(creature, key, creatureAttackDamageData[key]);

            AssertCollection(key, [.. creatureAttackDamageData[key]]);

            CreatureWithUnnaturalAttack_CanUseEquipment(creature, key);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateAttackDamageData(string template)
        {
            var keys = GetTemplateDamageKeys(template);
            foreach (var key in keys)
            {
                AssertTemplateAttackDamages(key, template);
            }
        }

        private IEnumerable<string> GetTemplateDamageKeys(string template)
        {
            var attacks = templateAttackData[template].Select(DataHelper.Parse<AttackDataSelection>);
            var sizes = SizeConstants.GetOrdered();

            foreach (var attack in attacks)
            {
                foreach (var size in sizes)
                {
                    yield return attack.BuildDamageKey(template, size);
                }
            }
        }

        private void AssertTemplateAttackDamages(string key, string template)
        {
            Assert.That(templateAttackDamageData, Contains.Key(key));

            AssertCorrectImprovedGrab(key, templateAttackDamageData[key]);
            AssertCorrectSpellLikeAbility(key, templateAttackDamageData[key]);
            AssertCorrectSpells(key, templateAttackDamageData[key]);
            AssertNaturalAttacksHaveCorrectDamageTypes(key, templateAttackDamageData[key]);
            AssertPoisonAttacksHaveCorrectDamageTypes(template, key, templateAttackDamageData[key]);
            AssertDiseaseAttacksHaveCorrectDamageTypes(template, key, templateAttackDamageData[key]);

            AssertCollection(key, [.. templateAttackDamageData[key]]);
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

        private void AssertCorrectImprovedGrab(string key, List<string> entries) => AssertEmpty(key, entries, "Improved Grab");
        private void AssertCorrectSpells(string key, List<string> entries) => AssertEmpty(key, entries, "Spells");
        private void AssertCorrectSpellLikeAbility(string key, List<string> entries) => AssertEmpty(key, entries, FeatConstants.SpecialQualities.SpellLikeAbility);

        private void AssertEmpty(string key, List<string> entries, string attackName)
        {
            if (key.Contains(attackName))
            {
                Assert.That(entries, Is.Empty, key);
            }
        }

        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d2", "1d2")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d3", "1d3")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d4", "1d4")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d6", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d8", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d10", "1d10")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "2d6", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "2d8", "2d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d2", "1d3")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d3", "1d4")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d4", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d6", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d8", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d10", "2d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d2", "1d4")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d3", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d4", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d6", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d2", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d3", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d4", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d2", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d3", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d2", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d2", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d2", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d2", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d2", "1d2")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d3", "1d3")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d4", "1d4")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d6", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d8", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d10", "1d10")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "2d6", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "2d8", "2d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d2", "1d3")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d3", "1d4")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d4", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d6", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d8", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d10", "2d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d2", "1d4")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d3", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d4", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d6", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d2", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d3", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d4", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d2", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d3", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d2", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d3", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d2", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d3", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d2", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d2", "1d2")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d3", "1d3")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d4", "1d4")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d6", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d8", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d10", "1d10")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "2d6", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "2d8", "2d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d2", "1d3")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d3", "1d4")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d4", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d6", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d8", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d10", "2d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d2", "1d4")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d3", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d4", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d6", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d2", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d3", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d4", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d2", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d3", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d4", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d2", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d3", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d2", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d2", "1d2")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d3", "1d3")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d4", "1d4")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d6", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d8", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d10", "1d10")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "2d6", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "2d8", "2d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d2", "1d3")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d3", "1d4")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d4", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d6", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d8", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d10", "2d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d2", "1d4")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d3", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d4", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d6", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d2", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d3", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d4", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d2", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d3", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d2", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d2", "1d2")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d3", "1d3")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d4", "1d4")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d6", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d8", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d10", "1d10")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "2d6", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "2d8", "2d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d2", "1d3")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d3", "1d4")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d4", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d6", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d8", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d10", "2d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d2", "1d4")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d3", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d4", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d6", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d2", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d3", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d4", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d2", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d3", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d2", "1d2")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d3", "1d3")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d4", "1d4")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d6", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d8", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d10", "1d10")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "2d6", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "2d8", "2d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d2", "1d3")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d3", "1d4")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d4", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d6", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d8", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d10", "2d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d2", "1d4")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d3", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d4", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d6", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d2", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d3", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d4", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d2", "1d2")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d3", "1d3")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d4", "1d4")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d6", "1d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d8", "1d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d10", "1d10")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "2d6", "2d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "2d8", "2d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d2", "1d3")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d3", "1d4")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d4", "1d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d6", "1d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d8", "2d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d10", "2d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d2", "1d4")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d3", "1d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d4", "1d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d6", "2d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d2", "1d2")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d3", "1d3")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d4", "1d4")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d6", "1d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d8", "1d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d10", "1d10")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "2d6", "2d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "2d8", "2d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d2", "1d3")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d3", "1d4")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d4", "1d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d6", "1d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d8", "2d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d10", "2d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d2", "1d2")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d3", "1d3")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d4", "1d4")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d6", "1d6")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d8", "1d8")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d10", "1d10")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "2d6", "2d6")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "2d8", "2d8")]
        public void AdjustDamageUpForAdvancedSizeForNaturalAttack(string originalSize, string advancedSize, string originalDamage, string advancedDamage)
        {
            var attack = new AttackDataSelection { IsNatural = true };
            var adjustedDamage = DamageTestData.GetAdjustedDamage(attack, originalDamage, originalSize, advancedSize);
            Assert.That(adjustedDamage, Is.EqualTo(advancedDamage));
        }

        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d2", "1d2")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d3", "1d3")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d4", "1d4")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d6", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d8", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d10", "1d10")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "2d6", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "2d8", "2d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d2", "1d3")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d3", "1d4")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d4", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d6", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d8", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d10", "2d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d2", "1d4")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d3", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d4", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d6", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d2", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d3", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d4", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d2", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d3", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d2", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d2", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d2", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d2", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d2", "1d2")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d3", "1d3")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d4", "1d4")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d6", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d8", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d10", "1d10")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "2d6", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "2d8", "2d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d2", "1d3")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d3", "1d4")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d4", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d6", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d8", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d10", "2d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d2", "1d4")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d3", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d4", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d6", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d2", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d3", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d4", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d2", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d3", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d2", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d3", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d2", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d3", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d2", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d2", "1d2")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d3", "1d3")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d4", "1d4")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d6", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d8", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d10", "1d10")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "2d6", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "2d8", "2d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d2", "1d3")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d3", "1d4")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d4", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d6", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d8", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d10", "2d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d2", "1d4")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d3", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d4", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d6", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d2", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d3", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d4", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d2", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d3", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d4", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d2", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d3", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d2", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d2", "1d2")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d3", "1d3")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d4", "1d4")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d6", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d8", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d10", "1d10")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "2d6", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "2d8", "2d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d2", "1d3")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d3", "1d4")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d4", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d6", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d8", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d10", "2d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d2", "1d4")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d3", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d4", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d6", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d2", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d3", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d4", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d2", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d3", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d2", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d2", "1d2")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d3", "1d3")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d4", "1d4")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d6", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d8", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d10", "1d10")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "2d6", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "2d8", "2d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d2", "1d3")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d3", "1d4")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d4", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d6", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d8", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d10", "2d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d2", "1d4")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d3", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d4", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d6", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d2", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d3", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d4", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d2", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d3", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d2", "1d2")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d3", "1d3")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d4", "1d4")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d6", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d8", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d10", "1d10")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "2d6", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "2d8", "2d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d2", "1d3")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d3", "1d4")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d4", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d6", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d8", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d10", "2d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d2", "1d4")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d3", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d4", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d6", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d2", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d3", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d4", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d2", "1d2")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d3", "1d3")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d4", "1d4")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d6", "1d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d8", "1d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d10", "1d10")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "2d6", "2d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "2d8", "2d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d2", "1d3")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d3", "1d4")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d4", "1d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d6", "1d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d8", "2d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d10", "2d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d2", "1d4")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d3", "1d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d4", "1d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d6", "2d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d2", "1d2")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d3", "1d3")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d4", "1d4")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d6", "1d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d8", "1d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d10", "1d10")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "2d6", "2d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "2d8", "2d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d2", "1d3")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d3", "1d4")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d4", "1d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d6", "1d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d8", "2d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d10", "2d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d2", "1d2")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d3", "1d3")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d4", "1d4")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d6", "1d6")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d8", "1d8")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d10", "1d10")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "2d6", "2d6")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "2d8", "2d8")]
        public void AdjustDamageDownForAdvancedSizeForNaturalAttack(string adjustedSize, string originalSize, string adjustedDamage, string originalDamage)
        {
            var attack = new AttackDataSelection { IsNatural = true };
            var damage = DamageTestData.GetAdjustedDamage(attack, originalDamage, originalSize, adjustedSize);
            Assert.That(damage, Is.EqualTo(adjustedDamage));
        }

        [TestCaseSource(nameof(VerboseDamages))]
        public void AdjustDamageForAdvancedSizeForNaturalAttackWithVerboseRollDamage(string[] originalDamageData, string[] adjustedDamageData)
        {
            var attack = new AttackDataSelection { IsNatural = true };
            for (var i = 0; i < originalDamageData.Length; i++)
            {
                var originalSelection = DataHelper.Parse<DamageDataSelection>(originalDamageData[i]);
                var adjustedSelection = DataHelper.Parse<DamageDataSelection>(adjustedDamageData[i]);
                var adjustedDamage = DamageTestData.GetAdjustedDamage(attack, originalSelection.Roll, SizeConstants.Fine, SizeConstants.Colossal);

                Assert.That(adjustedDamage, Is.EqualTo(adjustedSelection.Roll));
                Assert.That(originalSelection.Type, Is.EqualTo(adjustedSelection.Type));
                Assert.That(originalSelection.Condition, Is.EqualTo(adjustedSelection.Condition));
            }
        }

        private static IEnumerable VerboseDamages
        {
            get
            {
                var originalDamagesDatas = new string[][]
                {
                    [DamageTestData.BuildData("1d6", "piercing")],
                    [DamageTestData.BuildData("1d6", "piercing", "sometimes")],
                    [DamageTestData.BuildData("1d6", "bludgeoning", string.Empty), DamageTestData.BuildData("1d4", "acid", "often")],
                    [DamageTestData.BuildData("1d6", "bludgeoning", "sometimes"), DamageTestData.BuildData("1d4", "acid", string.Empty)],
                    [DamageTestData.BuildData("1d6", "bludgeoning", "sometimes"), DamageTestData.BuildData("1d4", "acid", "often")],
                    [DamageTestData.BuildData("1d2", "bludgeoning", string.Empty), DamageTestData.BuildData("1d10", "acid", "often")],
                    [DamageTestData.BuildData("1d2", "bludgeoning", "sometimes"), DamageTestData.BuildData( "1d10", "acid", string.Empty)],
                    [DamageTestData.BuildData("1d2", "bludgeoning", "sometimes"), DamageTestData.BuildData("1d10", "acid", "often")],
                };

                var adjustedDamagesDatas = new string[][]
                {
                    [DamageTestData.BuildData("3d6", "piercing")],
                    [DamageTestData.BuildData("3d6", "piercing", "sometimes")],
                    [DamageTestData.BuildData("3d6", "bludgeoning", string.Empty), DamageTestData.BuildData("3d6", "acid", "often")],
                    [DamageTestData.BuildData("3d6", "bludgeoning", "sometimes"), DamageTestData.BuildData("3d6", "acid", string.Empty)],
                    [DamageTestData.BuildData("3d6", "bludgeoning", "sometimes"), DamageTestData.BuildData("3d6", "acid", "often")],
                    [DamageTestData.BuildData("3d6", "bludgeoning", string.Empty), DamageTestData.BuildData("3d8", "acid", "often")],
                    [DamageTestData.BuildData("3d6", "bludgeoning", "sometimes"), DamageTestData.BuildData("3d8", "acid", string.Empty)],
                    [DamageTestData.BuildData("3d6", "bludgeoning", "sometimes"), DamageTestData.BuildData("3d8", "acid", "often")],
                };

                for (var i = 0; i < originalDamagesDatas.Length; i++)
                {
                    yield return new TestCaseData(originalDamagesDatas[i], adjustedDamagesDatas[i]);
                }
            }
        }

        [TestCase("4d6")]
        [TestCase("4d4")]
        public void AdjustDamageForAdvancedSizeForNaturalAttackWithNonAdjustableRollDamage(string originalDamage)
        {
            var attack = new AttackDataSelection { IsNatural = true };
            var adjustedDamage = DamageTestData.GetAdjustedDamage(attack, originalDamage, SizeConstants.Fine, SizeConstants.Colossal);
            Assert.That(adjustedDamage, Is.EqualTo(originalDamage));
        }

        [Test]
        public void DoNotAdjustDamageForAdvancedSizeForUnnaturalAttack()
        {
            var attack = new AttackDataSelection { IsNatural = false };
            var adjustedDamage = DamageTestData.GetAdjustedDamage(attack, "1d2", SizeConstants.Fine, SizeConstants.Colossal);
            Assert.That(adjustedDamage, Is.EqualTo("1d2"));
        }

        [Test]
        public void DoNotAdjustEffectRolls()
        {
            var attack = new AttackDataSelection { IsNatural = true, DamageEffect = "1d2" };
            var adjustedDamage = DamageTestData.GetAdjustedDamage(attack, "1d2", SizeConstants.Fine, SizeConstants.Colossal);
            Assert.That(adjustedDamage, Is.EqualTo("3d6"));
            Assert.That(attack.DamageEffect, Is.EqualTo("1d2"));
        }
    }
}
