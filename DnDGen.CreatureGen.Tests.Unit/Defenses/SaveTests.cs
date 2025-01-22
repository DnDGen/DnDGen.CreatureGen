using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using NUnit.Framework;
using System.Collections;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Defenses
{
    [TestFixture]
    public class SaveTests
    {
        private Save save;

        [SetUp]
        public void Setup()
        {
            save = new Save();
        }

        [Test]
        public void SaveInitialized()
        {
            Assert.That(save.BaseAbility, Is.Null);
            Assert.That(save.BaseValue, Is.Zero);
            Assert.That(save.Bonus, Is.Zero);
            Assert.That(save.Bonuses, Is.Empty);
            Assert.That(save.IsConditional, Is.False);
            Assert.That(save.HasSave, Is.False);
            Assert.That(save.TotalBonus, Is.Zero);
        }

        [Test]
        public void HasSave()
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.BaseAbility.BaseScore = 1;

            Assert.That(save.HasSave, Is.True);
        }

        [Test]
        public void DoesNotHaveSaveBecauseNull()
        {
            save.BaseAbility = null;
            Assert.That(save.HasSave, Is.False);
        }

        [Test]
        public void DoesNotHaveSaveBecauseNoScore()
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.BaseAbility.BaseScore = 0;

            Assert.That(save.HasSave, Is.False);
        }

        [Test]
        public void NoBonuses()
        {
            Assert.That(save.Bonuses, Is.Empty);
            Assert.That(save.Bonus, Is.Zero);
            Assert.That(save.IsConditional, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllValuesTestCases))]
        public void OneBonus(int value)
        {
            save.AddBonus(value);

            Assert.That(save.Bonuses, Is.Not.Empty);
            Assert.That(save.Bonuses.Count(), Is.EqualTo(1));

            var bonus = save.Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(value));

            Assert.That(save.Bonus, Is.EqualTo(value));
            Assert.That(save.IsConditional, Is.False);
        }

        [TestCaseSource(nameof(Bonuses))]
        public void TwoBonuses(int value1, int value2)
        {
            save.AddBonus(value1);
            save.AddBonus(value2);

            Assert.That(save.Bonuses, Is.Not.Empty);
            Assert.That(save.Bonuses.Count(), Is.EqualTo(2));

            var bonus = save.Bonuses.First();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(value1));

            bonus = save.Bonuses.Last();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(value2));

            Assert.That(save.Bonus, Is.EqualTo(value1 + value2));
            Assert.That(save.IsConditional, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllValuesTestCases))]
        public void ConditionalBonus(int value)
        {
            save.AddBonus(value, "condition");

            Assert.That(save.Bonuses, Is.Not.Empty);
            Assert.That(save.Bonuses.Count(), Is.EqualTo(1));

            var bonus = save.Bonuses.Single();
            Assert.That(bonus.Condition, Is.EqualTo("condition"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(value));

            Assert.That(save.Bonus, Is.Zero);
            Assert.That(save.IsConditional, Is.True);
        }

        [TestCaseSource(nameof(Bonuses))]
        public void ConditionalBonuses(int value1, int value2)
        {
            save.AddBonus(value1, "condition 1");
            save.AddBonus(value2, "condition 2");

            Assert.That(save.Bonuses, Is.Not.Empty);
            Assert.That(save.Bonuses.Count(), Is.EqualTo(2));

            var bonus = save.Bonuses.First();
            Assert.That(bonus.Condition, Is.EqualTo("condition 1"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(value1));

            bonus = save.Bonuses.Last();
            Assert.That(bonus.Condition, Is.EqualTo("condition 2"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(value2));

            Assert.That(save.Bonus, Is.Zero);
            Assert.That(save.IsConditional, Is.True);
        }

        [TestCaseSource(nameof(Bonuses))]
        public void BonusAndConditionalBonus(int value1, int value2)
        {
            save.AddBonus(value1);
            save.AddBonus(value2, "condition 2");

            Assert.That(save.Bonuses, Is.Not.Empty);
            Assert.That(save.Bonuses.Count(), Is.EqualTo(2));
            Assert.That(save.Bonus, Is.EqualTo(value1));
            Assert.That(save.IsConditional, Is.True);

            var bonus = save.Bonuses.First();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(value1));

            bonus = save.Bonuses.Last();
            Assert.That(bonus.Condition, Is.EqualTo("condition 2"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(value2));
        }

        [TestCaseSource(nameof(Bonuses))]
        public void ConditionalBonusAndBonus(int value1, int value2)
        {
            save.AddBonus(value1, "condition 1");
            save.AddBonus(value2);

            Assert.That(save.Bonuses, Is.Not.Empty);
            Assert.That(save.Bonuses.Count(), Is.EqualTo(2));
            Assert.That(save.Bonus, Is.EqualTo(value2));
            Assert.That(save.IsConditional, Is.True);

            var bonus = save.Bonuses.First();
            Assert.That(bonus.Condition, Is.EqualTo("condition 1"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(value1));

            bonus = save.Bonuses.Last();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(value2));
        }

        [TestCase(1, -5)]
        [TestCase(2, -4)]
        [TestCase(3, -4)]
        [TestCase(4, -3)]
        [TestCase(5, -3)]
        [TestCase(6, -2)]
        [TestCase(7, -2)]
        [TestCase(8, -1)]
        [TestCase(9, -1)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 1)]
        [TestCase(13, 1)]
        [TestCase(14, 2)]
        [TestCase(15, 2)]
        [TestCase(16, 3)]
        [TestCase(17, 3)]
        [TestCase(18, 4)]
        [TestCase(19, 4)]
        [TestCase(20, 5)]
        [TestCase(42, 16)]
        public void TotalBonus_BasedOnAbilityBaseScore(int abilityBaseScore, int expectedBonus)
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.BaseAbility.BaseScore = abilityBaseScore;

            Assert.That(save.TotalBonus, Is.EqualTo(expectedBonus));
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllValuesTestCases))]
        public void TotalBonus_BasedOnBaseValue(int baseValue)
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.BaseValue = baseValue;

            Assert.That(save.TotalBonus, Is.EqualTo(baseValue));
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllValuesTestCases))]
        public void TotalBonus_BasedOnBonus(int bonus)
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.AddBonus(bonus);

            Assert.That(save.TotalBonus, Is.EqualTo(bonus));
            Assert.That(save.IsConditional, Is.False);
        }

        [Test]
        public void TotalBonus_BasedOnBonus_Conditional()
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.AddBonus(666, "my condition");

            Assert.That(save.TotalBonus, Is.Zero);
            Assert.That(save.IsConditional, Is.True);
        }

        [Test]
        public void TotalBonus_BasedOnBonus_MultipleConditional()
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.AddBonus(666, "my condition");
            save.AddBonus(6666, "my other condition");

            Assert.That(save.TotalBonus, Is.Zero);
            Assert.That(save.IsConditional, Is.True);
        }

        [TestCase(9266, 90210)]
        [TestCase(42, -600)]
        [TestCase(-1337, 1336)]
        [TestCase(-96, -783)]
        public void TotalBonus_BasedOnBonuses(int bonus1, int bonus2)
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.AddBonus(bonus1);
            save.AddBonus(bonus2);

            Assert.That(save.TotalBonus, Is.EqualTo(bonus1 + bonus2));
            Assert.That(save.IsConditional, Is.False);
        }

        [Test]
        public void TotalBonus_BasedOnBonuses_WithConditions()
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.AddBonus(9266);
            save.AddBonus(90210);
            save.AddBonus(666, "my condition");
            save.AddBonus(6666, "my other condition");

            Assert.That(save.TotalBonus, Is.EqualTo(9266 + 90210));
            Assert.That(save.IsConditional, Is.True);
        }

        [Test]
        public void TotalBonus_AllFactors()
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.BaseAbility.BaseScore = 9266;
            save.BaseValue = 90210;
            save.AddBonus(42);
            save.AddBonus(-600);

            var expectedTotal = save.BaseAbility.Modifier + 90210 + 42 - 600;
            Assert.That(save.TotalBonus, Is.EqualTo(expectedTotal));
            Assert.That(save.IsConditional, Is.False);
        }

        [Test]
        public void TotalBonus_AllFactors_WithConditions()
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.BaseAbility.BaseScore = 9266;
            save.BaseValue = 90210;
            save.AddBonus(42);
            save.AddBonus(1336, "my other condition");
            save.AddBonus(-600);
            save.AddBonus(-1337, "my condition");

            var expectedTotal = save.BaseAbility.Modifier + 90210 + 42 - 600;
            Assert.That(save.TotalBonus, Is.EqualTo(expectedTotal));
            Assert.That(save.IsConditional, Is.True);
        }

        public static IEnumerable Bonuses
        {
            get
            {
                foreach (var value1 in NumericTestData.AllTestValues)
                {
                    foreach (var value2 in NumericTestData.AllTestValues)
                    {
                        yield return new TestCaseData(value1, value2);
                    }
                }
            }
        }
    }
}