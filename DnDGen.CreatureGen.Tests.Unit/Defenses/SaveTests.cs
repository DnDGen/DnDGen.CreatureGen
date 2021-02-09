using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
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

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllValues))]
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

        [TestCaseSource(typeof(SaveTestData), nameof(SaveTestData.Bonuses))]
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

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllValues))]
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

        [TestCaseSource(typeof(SaveTestData), nameof(SaveTestData.Bonuses))]
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

        [TestCaseSource(typeof(SaveTestData), nameof(SaveTestData.Bonuses))]
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

        [TestCaseSource(typeof(SaveTestData), nameof(SaveTestData.Bonuses))]
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

        [TestCaseSource(typeof(SaveTestData), nameof(SaveTestData.TotalBonus))]
        public void TotalBonus(int abilityScore, int baseValue, IEnumerable<int> bonuses)
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.BaseAbility.BaseScore = abilityScore;
            save.BaseValue = baseValue;

            foreach (var bonus in bonuses)
                save.AddBonus(bonus);

            var expectedTotal = save.BaseAbility.Modifier + baseValue + bonuses.Sum();
            Assert.That(save.TotalBonus, Is.EqualTo(expectedTotal));
        }

        public class SaveTestData
        {
            public static IEnumerable TotalBonus
            {
                get
                {
                    foreach (var abilityScore in NumericTestData.BaseAbilityTestNumbers)
                    {
                        foreach (var baseValue in NumericTestData.BaseTestNumbers)
                        {
                            yield return new TestCaseData(abilityScore, baseValue, Enumerable.Empty<int>());

                            foreach (var bonus1 in NumericTestData.AllBaseTestValues)
                            {
                                yield return new TestCaseData(abilityScore, baseValue, new[] { bonus1 });

                                foreach (var bonus2 in NumericTestData.AllBaseTestValues)
                                {
                                    yield return new TestCaseData(abilityScore, baseValue, new[] { bonus1, bonus2 });
                                }
                            }
                        }
                    }
                }
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

        [TestCaseSource(typeof(SaveTestData), nameof(SaveTestData.TotalBonus))]
        public void TotalBonusWithOneConditional(int abilityScore, int baseValue, IEnumerable<int> bonuses)
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.BaseAbility.BaseScore = abilityScore;
            save.BaseValue = baseValue;

            foreach (var bonus in bonuses)
                save.AddBonus(bonus);

            if (save.Bonuses.Any())
                save.Bonuses.First().Condition = "condition";

            var expectedTotal = save.BaseAbility.Modifier + baseValue + bonuses.Skip(1).Sum();
            Assert.That(save.TotalBonus, Is.EqualTo(expectedTotal));
        }

        [TestCaseSource(typeof(SaveTestData), nameof(SaveTestData.TotalBonus))]
        public void TotalBonusWithAllConditional(int abilityScore, int baseValue, IEnumerable<int> bonuses)
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.BaseAbility.BaseScore = abilityScore;
            save.BaseValue = baseValue;

            foreach (var bonus in bonuses)
                save.AddBonus(bonus, "condition");

            var expectedTotal = save.BaseAbility.Modifier + baseValue;
            Assert.That(save.TotalBonus, Is.EqualTo(expectedTotal));
        }
    }
}