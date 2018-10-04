using CreatureGen.Abilities;
using CreatureGen.Defenses;
using CreatureGen.Tests.Unit.TestCaseSources;
using NUnit.Framework;
using System.Collections;
using System.Linq;

namespace CreatureGen.Tests.Unit.Defenses
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
            Assert.That(save.CircumstantialBonus, Is.False);
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
            //TODO: Should assert added to Bonuses
            //TODO: Should assert counted in Bonus
            //TODO: Should check IsCircumstantial to be false
            Assert.Fail("not yet written");
        }

        [TestCaseSource(typeof(NumericTestData), "AllValues")]
        public void OneBonus(int value)
        {
            //TODO: Should assert added to Bonuses
            //TODO: Should assert counted in Bonus
            //TODO: Should check IsCircumstantial to be false
            Assert.Fail("not yet written");
        }

        [TestCaseSource(typeof(SaveTestData), "Bonuses")]
        public void TwoBonuses(int value1, int value2)
        {
            //TODO: Should assert added to Bonuses
            //TODO: Should assert counted in Bonus
            //TODO: Should check IsCircumstantial to be false
            Assert.Fail("not yet written");
        }

        [Test]
        public void CircumstantialBonus()
        {
            //TODO: Should assert added to Bonuses
            //TODO: Should assert not counted in Bonus
            //TODO: Should check IsCircumstantial to be true
            Assert.Fail("not yet written");
        }

        [Test]
        public void BonusAndCircumstantialBonus()
        {
            //TODO: Should assert added to Bonuses
            //TODO: Should assert not counted in Bonus
            //TODO: Should check IsCircumstantial to be true
            Assert.Fail("not yet written");
        }

        [TestCaseSource(typeof(SaveTestData), "TotalBonus")]
        public void TotalBonus(int abilityScore, int baseValue, params int[] bonuses)
        {
            save.BaseAbility = new Ability(AbilityConstants.Charisma);
            save.BaseAbility.BaseScore = abilityScore;
            save.BaseValue = baseValue;

            foreach (var bonus in bonuses)
                save.AddBonus(bonus);

            var expectedTotal = abilityScore + baseValue + bonuses.Sum();
            Assert.That(save.TotalBonus, Is.EqualTo(expectedTotal));
        }

        public class SaveTestData
        {
            public static IEnumerable TotalBonus
            {
                get
                {
                    foreach (var abilityScore in NumericTestData.PositiveValues)
                    {
                        foreach (var baseValue in NumericTestData.PositiveValues)
                        {
                            yield return new TestCaseData(abilityScore, baseValue);

                            foreach (var bonus1 in NumericTestData.TestValues)
                            {
                                yield return new TestCaseData(abilityScore, baseValue, bonus1);

                                foreach (var bonus2 in NumericTestData.TestValues)
                                {
                                    yield return new TestCaseData(abilityScore, baseValue, bonus1, bonus2);
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
                    foreach (var value1 in NumericTestData.TestValues)
                    {
                        foreach (var value2 in NumericTestData.TestValues)
                        {
                            yield return new TestCaseData(value1, value2);
                        }
                    }
                }
            }
        }

        [TestCaseSource(typeof(SaveTestData), "TotalBonus")]
        public void TotalBonusWithOneCircumstantial(int abilityScore, int baseValue, params int[] bonuses)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource(typeof(SaveTestData), "TotalBonus")]
        public void TotalBonusWithAllCircumstantial(int abilityScore, int baseValue, params int[] bonuses)
        {
            Assert.Fail("not yet written");
        }
    }
}