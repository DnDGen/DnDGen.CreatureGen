using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Tests.Unit.TestCaseSources;
using NUnit.Framework;
using System;
using System.Collections;
using System.Linq;

namespace CreatureGen.Tests.Unit.Defenses
{
    [TestFixture]
    public class ArmorClassTests
    {
        private ArmorClass armorClass;

        [SetUp]
        public void Setup()
        {
            armorClass = new ArmorClass();
        }

        [Test]
        public void ArmorClassInitialized()
        {
            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(ArmorClass.BaseArmorClass));
            Assert.That(armorClass.TotalBonus, Is.EqualTo(ArmorClass.BaseArmorClass));
            Assert.That(armorClass.TouchBonus, Is.EqualTo(ArmorClass.BaseArmorClass));
            Assert.That(armorClass.IsConditional, Is.False);
            Assert.That(armorClass.Dexterity, Is.Null);
            Assert.That(armorClass.ArmorBonus, Is.Zero);
            Assert.That(armorClass.ArmorBonuses, Is.Empty);
            Assert.That(armorClass.DeflectionBonus, Is.Zero);
            Assert.That(armorClass.DeflectionBonuses, Is.Zero);
            Assert.That(armorClass.DodgeBonus, Is.Zero);
            Assert.That(armorClass.DodgeBonuses, Is.Zero);
            Assert.That(armorClass.MaxDexterityBonus, Is.EqualTo(int.MaxValue));
            Assert.That(armorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(armorClass.NaturalArmorBonuses, Is.Zero);
            Assert.That(armorClass.ShieldBonus, Is.Zero);
            Assert.That(armorClass.ShieldBonuses, Is.Zero);
            Assert.That(armorClass.SizeModifier, Is.Zero);
        }

        [Test]
        public void BaseArmorClassIs10()
        {
            Assert.That(ArmorClass.BaseArmorClass, Is.EqualTo(10));
        }

        [Test]
        public void DexterityBonusIsZeroIfNull()
        {
            armorClass.Dexterity = null;
            Assert.That(armorClass.DexterityBonus, Is.Zero);
        }

        [Test]
        public void DexterityBonusIsZeroIfNoValue()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            Assert.That(armorClass.DexterityBonus, Is.Zero);
        }

        [TestCaseSource(typeof(NumericTestData), "BaseAbilityTestNumbers")]
        [TestCaseSource(typeof(NumericTestData), "PositiveValues")]
        public void DexterityBonusIsModifier(int value)
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = value;

            Assert.That(armorClass.DexterityBonus, Is.EqualTo(armorClass.Dexterity.Modifier));
        }

        [TestCaseSource(typeof(NumericTestData), "TestCases")]
        public void DexterityBonusIsLimitedByMaxDexterityBonus(int value, int max)
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = value;
            armorClass.MaxDexterityBonus = max;

            var expected = Math.Min(max, armorClass.Dexterity.Modifier);

            Assert.That(armorClass.DexterityBonus, Is.EqualTo(expected));
        }

        public class DexterityBonusTestData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    foreach (var value in NumericTestData.BaseAbilityTestNumbers)
                        foreach (var max in NumericTestData.PositiveValues)
                            yield return new TestCaseData(value, max);
                }
            }
        }

        [Test]
        public void ArmorBonusesIsOnlyArmorBonuses()
        {
            armorClass.AddBonus(ArmorClassConstants.Armor, 9266);
            armorClass.AddBonus(ArmorClassConstants.Deflection, 90210);
            armorClass.AddBonus(ArmorClassConstants.Dodge, 42);
            armorClass.AddBonus(ArmorClassConstants.Natural, 600);
            armorClass.AddBonus(ArmorClassConstants.Shield, 1337);

            Assert.That(armorClass.ArmorBonuses.Count, Is.EqualTo(1));
            var bonus = armorClass.ArmorBonuses.Single();
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [TestCase("")]
        [TestCase("condition")]
        public void AddArmorBonus(string condition)
        {
            armorClass.AddBonus(ArmorClassConstants.Armor, 9266, condition);

            var isConditional = !string.IsNullOrEmpty(condition);

            if (isConditional)
                Assert.That(armorClass.ArmorBonus, Is.Zero);
            else
                Assert.That(armorClass.ArmorBonus, Is.EqualTo(9266));

            Assert.That(armorClass.IsConditional, Is.EqualTo(isConditional));

            Assert.That(armorClass.ArmorBonuses.Count, Is.EqualTo(1));

            var bonus = armorClass.ArmorBonuses.Single();
            Assert.That(bonus.Value, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.EqualTo(condition));
        }

        [TestCase("", "")]
        [TestCase("condition", "")]
        [TestCase("", "other condition")]
        [TestCase("condition", "other condition")]
        public void AddArmorBonuses(string condition1, string condition2)
        {
            armorClass.AddBonus(ArmorClassConstants.Armor, 9266, condition1);
            armorClass.AddBonus(ArmorClassConstants.Armor, 90210, condition2);

            var isConditional1 = !string.IsNullOrEmpty(condition1);
            var isConditional2 = !string.IsNullOrEmpty(condition2);

            if (isConditional1 && isConditional2)
                Assert.That(armorClass.ArmorBonus, Is.Zero);
            else if (isConditional1)
                Assert.That(armorClass.ArmorBonus, Is.EqualTo(90210));
            else if (isConditional2)
                Assert.That(armorClass.ArmorBonus, Is.EqualTo(9266));
            else
                Assert.That(armorClass.ArmorBonus, Is.EqualTo(90210));

            Assert.That(armorClass.IsConditional, Is.EqualTo(isConditional1 || isConditional2));

            Assert.That(armorClass.ArmorBonuses.Count, Is.EqualTo(2));

            var bonus = armorClass.ArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.EqualTo(condition1));

            bonus = armorClass.ArmorBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(90210));
            Assert.That(bonus.Condition, Is.EqualTo(condition2));
        }

        [TestCaseSource(typeof(MaxBonusTestData), "TestCases")]
        public void ArmorBonusIsMaxOfNonConditionalArmorBonuses(int value1, int value2)
        {
            armorClass.AddBonus(ArmorClassConstants.Armor, value1);
            armorClass.AddBonus(ArmorClassConstants.Armor, value2);
            armorClass.AddBonus(ArmorClassConstants.Armor, value1 + 1, "condition");
            armorClass.AddBonus(ArmorClassConstants.Armor, value2 + 1, "other condition");

            var expected = Math.Max(value1, value2);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(expected));
        }

        public class MaxBonusTestData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    foreach (var value1 in NumericTestData.AllTestValues)
                        foreach (var value2 in NumericTestData.AllTestValues)
                            yield return new TestCaseData(value1, value2);
                }
            }
        }

        [Test]
        public void DeflectionBonusesIsOnlyArmorBonuses()
        {
            armorClass.AddBonus(ArmorClassConstants.Armor, 9266);
            armorClass.AddBonus(ArmorClassConstants.Deflection, 90210);
            armorClass.AddBonus(ArmorClassConstants.Dodge, 42);
            armorClass.AddBonus(ArmorClassConstants.Natural, 600);
            armorClass.AddBonus(ArmorClassConstants.Shield, 1337);

            Assert.That(armorClass.DeflectionBonuses.Count, Is.EqualTo(1));
            var bonus = armorClass.DeflectionBonuses.Single();
            Assert.That(bonus.Value, Is.EqualTo(90210));
        }

        [TestCase("")]
        [TestCase("condition")]
        public void AddDeflectionBonus(string condition)
        {
            armorClass.AddBonus(ArmorClassConstants.Deflection, 9266, condition);

            var isConditional = !string.IsNullOrEmpty(condition);

            if (isConditional)
                Assert.That(armorClass.DeflectionBonus, Is.Zero);
            else
                Assert.That(armorClass.DeflectionBonus, Is.EqualTo(9266));

            Assert.That(armorClass.IsConditional, Is.EqualTo(isConditional));

            Assert.That(armorClass.DeflectionBonuses.Count, Is.EqualTo(1));

            var bonus = armorClass.DeflectionBonuses.Single();
            Assert.That(bonus.Value, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.EqualTo(condition));
        }

        [TestCase("", "")]
        [TestCase("condition", "")]
        [TestCase("", "other condition")]
        [TestCase("condition", "other condition")]
        public void AddDeflectionBonuses(string condition1, string condition2)
        {
            armorClass.AddBonus(ArmorClassConstants.Deflection, 9266, condition1);
            armorClass.AddBonus(ArmorClassConstants.Deflection, 90210, condition2);

            var isConditional1 = !string.IsNullOrEmpty(condition1);
            var isConditional2 = !string.IsNullOrEmpty(condition2);

            if (isConditional1 && isConditional2)
                Assert.That(armorClass.DeflectionBonus, Is.Zero);
            else if (isConditional1)
                Assert.That(armorClass.DeflectionBonus, Is.EqualTo(90210));
            else if (isConditional2)
                Assert.That(armorClass.DeflectionBonus, Is.EqualTo(9266));
            else
                Assert.That(armorClass.DeflectionBonus, Is.EqualTo(90210));

            Assert.That(armorClass.IsConditional, Is.EqualTo(isConditional1 || isConditional2));

            Assert.That(armorClass.DeflectionBonuses.Count, Is.EqualTo(2));

            var bonus = armorClass.DeflectionBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.EqualTo(condition1));

            bonus = armorClass.DeflectionBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(90210));
            Assert.That(bonus.Condition, Is.EqualTo(condition2));
        }

        [TestCaseSource(typeof(MaxBonusTestData), "TestCases")]
        public void DeflectionBonusIsMaxOfNonConditionalDeflectionBonuses(int value1, int value2)
        {
            armorClass.AddBonus(ArmorClassConstants.Deflection, value1);
            armorClass.AddBonus(ArmorClassConstants.Deflection, value2);
            armorClass.AddBonus(ArmorClassConstants.Deflection, value1 + 1, "condition");
            armorClass.AddBonus(ArmorClassConstants.Deflection, value2 + 1, "other condition");

            var expected = Math.Max(value1, value2);
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(expected));
        }

        [Test]
        public void DodgeBonusesIsOnlyArmorBonuses()
        {
            armorClass.AddBonus(ArmorClassConstants.Armor, 9266);
            armorClass.AddBonus(ArmorClassConstants.Deflection, 90210);
            armorClass.AddBonus(ArmorClassConstants.Dodge, 42);
            armorClass.AddBonus(ArmorClassConstants.Natural, 600);
            armorClass.AddBonus(ArmorClassConstants.Shield, 1337);

            Assert.That(armorClass.DodgeBonuses.Count, Is.EqualTo(1));
            var bonus = armorClass.DodgeBonuses.Single();
            Assert.That(bonus.Value, Is.EqualTo(42));
        }

        [TestCase("")]
        [TestCase("condition")]
        public void AddDodgeBonus(string condition)
        {
            armorClass.AddBonus(ArmorClassConstants.Dodge, 9266, condition);

            var isConditional = !string.IsNullOrEmpty(condition);

            if (isConditional)
                Assert.That(armorClass.DodgeBonus, Is.Zero);
            else
                Assert.That(armorClass.DodgeBonus, Is.EqualTo(9266));

            Assert.That(armorClass.IsConditional, Is.EqualTo(isConditional));

            Assert.That(armorClass.DodgeBonuses.Count, Is.EqualTo(1));

            var bonus = armorClass.DodgeBonuses.Single();
            Assert.That(bonus.Value, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.EqualTo(condition));
        }

        [TestCase("", "")]
        [TestCase("condition", "")]
        [TestCase("", "other condition")]
        [TestCase("condition", "other condition")]
        public void AddDodgeBonuses(string condition1, string condition2)
        {
            armorClass.AddBonus(ArmorClassConstants.Dodge, 9266, condition1);
            armorClass.AddBonus(ArmorClassConstants.Dodge, 90210, condition2);

            var isConditional1 = !string.IsNullOrEmpty(condition1);
            var isConditional2 = !string.IsNullOrEmpty(condition2);

            if (isConditional1 && isConditional2)
                Assert.That(armorClass.DodgeBonus, Is.Zero);
            else if (isConditional1)
                Assert.That(armorClass.DodgeBonus, Is.EqualTo(90210));
            else if (isConditional2)
                Assert.That(armorClass.DodgeBonus, Is.EqualTo(9266));
            else
                Assert.That(armorClass.DodgeBonus, Is.EqualTo(9266 + 90210));

            Assert.That(armorClass.IsConditional, Is.EqualTo(isConditional1 || isConditional2));

            Assert.That(armorClass.DodgeBonuses.Count, Is.EqualTo(2));

            var bonus = armorClass.DodgeBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.EqualTo(condition1));

            bonus = armorClass.DodgeBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(90210));
            Assert.That(bonus.Condition, Is.EqualTo(condition2));
        }

        [TestCaseSource(typeof(MaxBonusTestData), "TestCases")]
        public void DodgeBonusIsSumOfNonConditionalDodgeBonuses(int value1, int value2)
        {
            armorClass.AddBonus(ArmorClassConstants.Dodge, value1);
            armorClass.AddBonus(ArmorClassConstants.Dodge, value2);
            armorClass.AddBonus(ArmorClassConstants.Dodge, value1 + 1, "condition");
            armorClass.AddBonus(ArmorClassConstants.Dodge, value2 + 1, "other condition");

            Assert.That(armorClass.DodgeBonus, Is.EqualTo(value1 + value2));
        }

        [Test]
        public void NaturalArmorBonusesIsOnlyArmorBonuses()
        {
            armorClass.AddBonus(ArmorClassConstants.Armor, 9266);
            armorClass.AddBonus(ArmorClassConstants.Deflection, 90210);
            armorClass.AddBonus(ArmorClassConstants.Dodge, 42);
            armorClass.AddBonus(ArmorClassConstants.Natural, 600);
            armorClass.AddBonus(ArmorClassConstants.Shield, 1337);

            Assert.That(armorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));
            var bonus = armorClass.NaturalArmorBonuses.Single();
            Assert.That(bonus.Value, Is.EqualTo(600));
        }

        [TestCase("")]
        [TestCase("condition")]
        public void AddNaturalArmorBonus(string condition)
        {
            armorClass.AddBonus(ArmorClassConstants.Natural, 9266, condition);

            var isConditional = !string.IsNullOrEmpty(condition);

            if (isConditional)
                Assert.That(armorClass.NaturalArmorBonus, Is.Zero);
            else
                Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(9266));

            Assert.That(armorClass.IsConditional, Is.EqualTo(isConditional));

            Assert.That(armorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

            var bonus = armorClass.NaturalArmorBonuses.Single();
            Assert.That(bonus.Value, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.EqualTo(condition));
        }

        [TestCase("", "")]
        [TestCase("condition", "")]
        [TestCase("", "other condition")]
        [TestCase("condition", "other condition")]
        public void AddNaturalArmorBonuses(string condition1, string condition2)
        {
            armorClass.AddBonus(ArmorClassConstants.Natural, 9266, condition1);
            armorClass.AddBonus(ArmorClassConstants.Natural, 90210, condition2);

            var isConditional1 = !string.IsNullOrEmpty(condition1);
            var isConditional2 = !string.IsNullOrEmpty(condition2);

            if (isConditional1 && isConditional2)
                Assert.That(armorClass.NaturalArmorBonus, Is.Zero);
            else if (isConditional1)
                Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(90210));
            else if (isConditional2)
                Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(9266));
            else
                Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(90210));

            Assert.That(armorClass.IsConditional, Is.EqualTo(isConditional1 || isConditional2));

            Assert.That(armorClass.NaturalArmorBonuses.Count, Is.EqualTo(2));

            var bonus = armorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.EqualTo(condition1));

            bonus = armorClass.NaturalArmorBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(90210));
            Assert.That(bonus.Condition, Is.EqualTo(condition2));
        }

        [TestCaseSource(typeof(MaxBonusTestData), "TestCases")]
        public void NaturalArmorBonusIsMaxOfNonConditionalNaturalArmorBonuses(int value1, int value2)
        {
            armorClass.AddBonus(ArmorClassConstants.Natural, value1);
            armorClass.AddBonus(ArmorClassConstants.Natural, value2);
            armorClass.AddBonus(ArmorClassConstants.Natural, value1 + 1, "condition");
            armorClass.AddBonus(ArmorClassConstants.Natural, value2 + 1, "other condition");

            var expected = Math.Max(value1, value2);
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(expected));
        }

        [Test]
        public void ShieldBonusesIsOnlyArmorBonuses()
        {
            armorClass.AddBonus(ArmorClassConstants.Armor, 9266);
            armorClass.AddBonus(ArmorClassConstants.Deflection, 90210);
            armorClass.AddBonus(ArmorClassConstants.Dodge, 42);
            armorClass.AddBonus(ArmorClassConstants.Natural, 600);
            armorClass.AddBonus(ArmorClassConstants.Shield, 1337);

            Assert.That(armorClass.ShieldBonuses.Count, Is.EqualTo(1));
            var bonus = armorClass.ShieldBonuses.Single();
            Assert.That(bonus.Value, Is.EqualTo(1337));
        }

        [TestCase("")]
        [TestCase("condition")]
        public void AddShieldBonus(string condition)
        {
            armorClass.AddBonus(ArmorClassConstants.Shield, 9266, condition);

            var isConditional = !string.IsNullOrEmpty(condition);

            if (isConditional)
                Assert.That(armorClass.ShieldBonus, Is.Zero);
            else
                Assert.That(armorClass.ShieldBonus, Is.EqualTo(9266));

            Assert.That(armorClass.IsConditional, Is.EqualTo(isConditional));

            Assert.That(armorClass.ShieldBonuses.Count, Is.EqualTo(1));

            var bonus = armorClass.ShieldBonuses.Single();
            Assert.That(bonus.Value, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.EqualTo(condition));
        }

        [TestCase("", "")]
        [TestCase("condition", "")]
        [TestCase("", "other condition")]
        [TestCase("condition", "other condition")]
        public void AddShieldArmorBonuses(string condition1, string condition2)
        {
            armorClass.AddBonus(ArmorClassConstants.Shield, 9266, condition1);
            armorClass.AddBonus(ArmorClassConstants.Shield, 90210, condition2);

            var isConditional1 = !string.IsNullOrEmpty(condition1);
            var isConditional2 = !string.IsNullOrEmpty(condition2);

            if (isConditional1 && isConditional2)
                Assert.That(armorClass.ShieldBonus, Is.Zero);
            else if (isConditional1)
                Assert.That(armorClass.ShieldBonus, Is.EqualTo(90210));
            else if (isConditional2)
                Assert.That(armorClass.ShieldBonus, Is.EqualTo(9266));
            else
                Assert.That(armorClass.ShieldBonus, Is.EqualTo(90210));

            Assert.That(armorClass.IsConditional, Is.EqualTo(isConditional1 || isConditional2));

            Assert.That(armorClass.ShieldBonuses.Count, Is.EqualTo(2));

            var bonus = armorClass.ShieldBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9266));
            Assert.That(bonus.Condition, Is.EqualTo(condition1));

            bonus = armorClass.ShieldBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(90210));
            Assert.That(bonus.Condition, Is.EqualTo(condition2));
        }

        [TestCaseSource(typeof(MaxBonusTestData), "TestCases")]
        public void ShieldBonusIsMaxOfNonConditionalShieldBonuses(int value1, int value2)
        {
            armorClass.AddBonus(ArmorClassConstants.Shield, value1);
            armorClass.AddBonus(ArmorClassConstants.Shield, value2);
            armorClass.AddBonus(ArmorClassConstants.Shield, value1 + 1, "condition");
            armorClass.AddBonus(ArmorClassConstants.Shield, value2 + 1, "other condition");

            var expected = Math.Max(value1, value2);
            Assert.That(armorClass.ShieldBonus, Is.EqualTo(expected));
        }

        [Test]
        public void FullArmorClassIsEverything()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 9266;
            armorClass.SizeModifier = 600;

            armorClass.AddBonus(ArmorClassConstants.Armor, 90210);
            armorClass.AddBonus(ArmorClassConstants.Deflection, 42);
            armorClass.AddBonus(ArmorClassConstants.Natural, 1337);
            armorClass.AddBonus(ArmorClassConstants.Shield, 1234);
            armorClass.AddBonus(ArmorClassConstants.Dodge, 1336);

            var total = ArmorClass.BaseArmorClass;
            total += armorClass.DexterityBonus;
            total += armorClass.ArmorBonus;
            total += armorClass.DeflectionBonus;
            total += armorClass.NaturalArmorBonus;
            total += armorClass.ShieldBonus;
            total += armorClass.SizeModifier;
            total += armorClass.DodgeBonus;

            Assert.That(armorClass.TotalBonus, Is.EqualTo(total));
        }

        [Test]
        public void FullArmorClassMustBePositive()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = -9266;
            armorClass.SizeModifier = -600;

            armorClass.AddBonus(ArmorClassConstants.Armor, -90210);
            armorClass.AddBonus(ArmorClassConstants.Deflection, -42);
            armorClass.AddBonus(ArmorClassConstants.Natural, -1337);
            armorClass.AddBonus(ArmorClassConstants.Shield, -1234);
            armorClass.AddBonus(ArmorClassConstants.Dodge, -1336);

            Assert.That(armorClass.TotalBonus, Is.EqualTo(1));
        }

        [Test]
        public void FlatFootedArmorClassDoesNotIncludeDodgeOrDexterity()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 9266;
            armorClass.SizeModifier = 600;

            armorClass.AddBonus(ArmorClassConstants.Armor, 90210);
            armorClass.AddBonus(ArmorClassConstants.Deflection, 42);
            armorClass.AddBonus(ArmorClassConstants.Natural, 1337);
            armorClass.AddBonus(ArmorClassConstants.Shield, 1234);
            armorClass.AddBonus(ArmorClassConstants.Dodge, 1336);

            var total = ArmorClass.BaseArmorClass;
            total += armorClass.ArmorBonus;
            total += armorClass.DeflectionBonus;
            total += armorClass.NaturalArmorBonus;
            total += armorClass.ShieldBonus;
            total += armorClass.SizeModifier;

            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(total));
        }

        [Test]
        public void FlatFootedArmorClassMustBePositive()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = -9266;
            armorClass.SizeModifier = -600;

            armorClass.AddBonus(ArmorClassConstants.Armor, -90210);
            armorClass.AddBonus(ArmorClassConstants.Deflection, -42);
            armorClass.AddBonus(ArmorClassConstants.Natural, -1337);
            armorClass.AddBonus(ArmorClassConstants.Shield, -1234);
            armorClass.AddBonus(ArmorClassConstants.Dodge, -1336);

            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(1));
        }

        [Test]
        public void TouchArmorClassDoesNotIncludeArmorOrShieldOrNatural()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 9266;
            armorClass.SizeModifier = 600;

            armorClass.AddBonus(ArmorClassConstants.Armor, 90210);
            armorClass.AddBonus(ArmorClassConstants.Deflection, 42);
            armorClass.AddBonus(ArmorClassConstants.Natural, 1337);
            armorClass.AddBonus(ArmorClassConstants.Shield, 1234);
            armorClass.AddBonus(ArmorClassConstants.Dodge, 1336);

            var total = ArmorClass.BaseArmorClass;
            total += armorClass.DexterityBonus;
            total += armorClass.DeflectionBonus;
            total += armorClass.SizeModifier;

            Assert.That(armorClass.TouchBonus, Is.EqualTo(total));
        }

        [Test]
        public void TouchArmorClassMustBePositive()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = -9266;
            armorClass.SizeModifier = -600;

            armorClass.AddBonus(ArmorClassConstants.Armor, -90210);
            armorClass.AddBonus(ArmorClassConstants.Deflection, -42);
            armorClass.AddBonus(ArmorClassConstants.Natural, -1337);
            armorClass.AddBonus(ArmorClassConstants.Shield, -1234);
            armorClass.AddBonus(ArmorClassConstants.Dodge, -1336);

            Assert.That(armorClass.TouchBonus, Is.EqualTo(1));
        }

        [Test]
        public void FullArmorClassIsEverythingWhenDexterityHasNoScore()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            armorClass.SizeModifier = 600;

            armorClass.AddBonus(ArmorClassConstants.Armor, 90210);
            armorClass.AddBonus(ArmorClassConstants.Deflection, 42);
            armorClass.AddBonus(ArmorClassConstants.Natural, 1337);
            armorClass.AddBonus(ArmorClassConstants.Shield, 1234);
            armorClass.AddBonus(ArmorClassConstants.Dodge, 1336);

            var total = ArmorClass.BaseArmorClass;
            total += armorClass.ArmorBonus;
            total += armorClass.DeflectionBonus;
            total += armorClass.DodgeBonus;
            total += armorClass.NaturalArmorBonus;
            total += armorClass.ShieldBonus;
            total += armorClass.SizeModifier;

            Assert.That(armorClass.TotalBonus, Is.EqualTo(total));
        }

        [Test]
        public void FullArmorClassMustBePositiveWhenDexterityHasNoScore()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            armorClass.SizeModifier = -600;

            armorClass.AddBonus(ArmorClassConstants.Armor, -90210);
            armorClass.AddBonus(ArmorClassConstants.Deflection, -42);
            armorClass.AddBonus(ArmorClassConstants.Natural, -1337);
            armorClass.AddBonus(ArmorClassConstants.Shield, -1234);
            armorClass.AddBonus(ArmorClassConstants.Dodge, -1336);

            Assert.That(armorClass.TotalBonus, Is.EqualTo(1));
        }

        [Test]
        public void FlatFootedArmorClassDoesNotIncludeDodgeOrDexterityWhenDexterityHasNoScore()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            armorClass.SizeModifier = 600;

            armorClass.AddBonus(ArmorClassConstants.Armor, 90210);
            armorClass.AddBonus(ArmorClassConstants.Deflection, 42);
            armorClass.AddBonus(ArmorClassConstants.Natural, 1337);
            armorClass.AddBonus(ArmorClassConstants.Shield, 1234);
            armorClass.AddBonus(ArmorClassConstants.Dodge, 1336);

            var total = ArmorClass.BaseArmorClass;
            total += armorClass.ArmorBonus;
            total += armorClass.DeflectionBonus;
            total += armorClass.NaturalArmorBonus;
            total += armorClass.ShieldBonus;
            total += armorClass.SizeModifier;

            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(total));
        }

        [Test]
        public void FlatFootedArmorClassMustBePositiveWhenDexterityHasNoScore()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            armorClass.SizeModifier = -600;

            armorClass.AddBonus(ArmorClassConstants.Armor, -90210);
            armorClass.AddBonus(ArmorClassConstants.Deflection, -42);
            armorClass.AddBonus(ArmorClassConstants.Natural, -1337);
            armorClass.AddBonus(ArmorClassConstants.Shield, -1234);
            armorClass.AddBonus(ArmorClassConstants.Dodge, -1336);

            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(1));
        }

        [Test]
        public void TouchArmorClassDoesNotIncludeArmorOrShieldOrNaturalWhenDexterityHasNoScore()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            armorClass.SizeModifier = 600;

            armorClass.AddBonus(ArmorClassConstants.Armor, 90210);
            armorClass.AddBonus(ArmorClassConstants.Deflection, 42);
            armorClass.AddBonus(ArmorClassConstants.Natural, 1337);
            armorClass.AddBonus(ArmorClassConstants.Shield, 1234);
            armorClass.AddBonus(ArmorClassConstants.Dodge, 1336);

            var total = ArmorClass.BaseArmorClass;
            total += armorClass.DeflectionBonus;
            total += armorClass.DodgeBonus;
            total += armorClass.SizeModifier;

            Assert.That(armorClass.TouchBonus, Is.EqualTo(total));
        }

        [Test]
        public void TouchArmorClassMustBePositiveWhenDexterityHasNoScore()
        {
            armorClass.Dexterity = new Ability(AbilityConstants.Dexterity);
            armorClass.Dexterity.BaseScore = 0;
            armorClass.SizeModifier = -600;

            armorClass.AddBonus(ArmorClassConstants.Armor, -90210);
            armorClass.AddBonus(ArmorClassConstants.Deflection, -42);
            armorClass.AddBonus(ArmorClassConstants.Natural, -1337);
            armorClass.AddBonus(ArmorClassConstants.Shield, -1234);
            armorClass.AddBonus(ArmorClassConstants.Dodge, -1336);

            Assert.That(armorClass.TouchBonus, Is.EqualTo(1));
        }
    }
}