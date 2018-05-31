using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Abilities
{
    [TestFixture]
    public class AbilityAdjustmentsTests : TypesAndAmountsTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        internal ITypeAndAmountSelector TypesAndAmountsSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.TypeAndAmount.AbilityAdjustments;
            }
        }

        [Test]
        public void CollectionNames()
        {
            var creatures = CreatureConstants.All();
            var names = creatures.Union(new[] { GroupConstants.All });

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(AbilityAdjustmentsTestData), "TestCases")]
        public void AbilityAdjustment(string name, Dictionary<string, int> typesAndAmounts)
        {
            Assert.That(typesAndAmounts, Is.Not.Empty, name);
            AssertTypesAndAmounts(name, typesAndAmounts);
        }

        public class AbilityAdjustmentsTestData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    testCases[GroupConstants.All] = new Dictionary<string, int>();
                    testCases[GroupConstants.All][AbilityConstants.Charisma] = 0;
                    testCases[GroupConstants.All][AbilityConstants.Constitution] = 0;
                    testCases[GroupConstants.All][AbilityConstants.Dexterity] = 0;
                    testCases[GroupConstants.All][AbilityConstants.Intelligence] = 0;
                    testCases[GroupConstants.All][AbilityConstants.Strength] = 0;
                    testCases[GroupConstants.All][AbilityConstants.Wisdom] = 0;

                    var creatures = CreatureConstants.All();

                    foreach (var creature in creatures)
                    {
                        testCases[creature] = new Dictionary<string, int>();
                    }

                    testCases[CreatureConstants.Aasimar][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Aasimar][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Aasimar][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Aasimar][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Aasimar][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Aasimar][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Aboleth][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Aboleth][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Aboleth][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Aboleth][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Aboleth][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Aboleth][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Achaierai][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Achaierai][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Achaierai][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Achaierai][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Achaierai][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Achaierai][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Allip][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Allip][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Allip][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Allip][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Angel_AstralDeva][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Angel_AstralDeva][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Angel_AstralDeva][AbilityConstants.Dexterity] = 8;
                    testCases[CreatureConstants.Angel_AstralDeva][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Angel_AstralDeva][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Angel_AstralDeva][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Angel_Planetar][AbilityConstants.Charisma] = 12;
                    testCases[CreatureConstants.Angel_Planetar][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Angel_Planetar][AbilityConstants.Dexterity] = 8;
                    testCases[CreatureConstants.Angel_Planetar][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Angel_Planetar][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.Angel_Planetar][AbilityConstants.Wisdom] = 12;
                    testCases[CreatureConstants.Angel_Solar][AbilityConstants.Charisma] = 14;
                    testCases[CreatureConstants.Angel_Solar][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Angel_Solar][AbilityConstants.Dexterity] = 10;
                    testCases[CreatureConstants.Angel_Solar][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Angel_Solar][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Angel_Solar][AbilityConstants.Wisdom] = 14;
                    testCases[CreatureConstants.AnimatedObject_Colossal][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Colossal][AbilityConstants.Dexterity] = -6;
                    testCases[CreatureConstants.AnimatedObject_Colossal][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.AnimatedObject_Colossal][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.AnimatedObject_Gargantuan][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Gargantuan][AbilityConstants.Dexterity] = -4;
                    testCases[CreatureConstants.AnimatedObject_Gargantuan][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.AnimatedObject_Gargantuan][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.AnimatedObject_Huge][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Huge][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.AnimatedObject_Huge][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.AnimatedObject_Huge][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.AnimatedObject_Large][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Large][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.AnimatedObject_Large][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.AnimatedObject_Large][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.AnimatedObject_Medium][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Medium][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.AnimatedObject_Medium][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.AnimatedObject_Medium][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.AnimatedObject_Small][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Small][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.AnimatedObject_Small][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.AnimatedObject_Small][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.AnimatedObject_Tiny][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Tiny][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.AnimatedObject_Tiny][AbilityConstants.Strength] = -2;
                    testCases[CreatureConstants.AnimatedObject_Tiny][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.Human][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Human][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Human][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Human][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Human][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Human][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Tiefling][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Tiefling][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Tiefling][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Tiefling][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Tiefling][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Tiefling][AbilityConstants.Wisdom] = -2;

                    foreach (var testCase in testCases)
                    {
                        var speeds = testCase.Value.Select(kvp => $"{kvp.Key}:{kvp.Value}");
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"AbilityAdjustments({testCase.Key}, [{string.Join("], [", speeds)}])");
                    }
                }
            }
        }

        [Test]
        public void AllCreaturesHaveAtLeast1Ability()
        {
            var creatures = CreatureConstants.All();

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                Assert.That(table.Keys, Contains.Item(creature));

                var abilities = GetCollection(creature);
                Assert.That(abilities, Is.Not.Empty, creature);
            }
        }

        [Test]
        public void AllAbilityAdjustmentsAreMultiplesOf2()
        {
            var creatures = CreatureConstants.All();

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                Assert.That(table.Keys, Contains.Item(creature));

                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                Assert.That(abilities, Is.Not.Empty, creature);

                foreach (var ability in abilities)
                {
                    Assert.That(ability.Amount % 2, Is.EqualTo(0), $"{creature} {ability.Type}");
                }
            }
        }

        [Test]
        public void AllAbilityAdjustmentsAreRealAbilities()
        {
            var allAbilities = new[]
            {
                AbilityConstants.Charisma,
                AbilityConstants.Constitution,
                AbilityConstants.Dexterity,
                AbilityConstants.Intelligence,
                AbilityConstants.Strength,
                AbilityConstants.Wisdom,
            };

            var creatures = CreatureConstants.All();

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                Assert.That(table.Keys, Contains.Item(creature));

                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                Assert.That(abilities, Is.Not.Empty, creature);

                var abilityNames = abilities.Select(a => a.Type);
                Assert.That(abilityNames, Is.SubsetOf(allAbilities), creature);
            }
        }

        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Undead)]
        public void DoNotHaveConstitution(string creatureType)
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                var abilityNames = abilities.Select(a => a.Type);
                Assert.That(abilityNames, Does.Not.Contains(AbilityConstants.Constitution));
            }
        }

        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal)]
        public void DoNotHaveStrength(string creatureType)
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                var abilityNames = abilities.Select(a => a.Type);
                Assert.That(abilityNames, Does.Not.Contains(AbilityConstants.Strength));
            }
        }

        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Ooze)]
        public void DoNotHaveIntelligence(string creatureType)
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                var abilityNames = abilities.Select(a => a.Type);
                Assert.That(abilityNames, Does.Not.Contains(AbilityConstants.Intelligence));
            }
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void HaveAllAbilities(string creatureType)
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            var allAbilities = TypesAndAmountsSelector.Select(tableName, GroupConstants.All);
            var allAbilitiesNames = allAbilities.Select(a => a.Type);

            foreach (var creature in creatures)
            {
                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                var abilityNames = abilities.Select(a => a.Type);
                Assert.That(allAbilitiesNames, Is.EquivalentTo(abilityNames));
            }
        }
    }
}
