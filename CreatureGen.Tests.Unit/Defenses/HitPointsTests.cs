using CreatureGen.Abilities;
using CreatureGen.Defenses;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Defenses
{
    [TestFixture]
    public class HitPointsTests
    {
        private HitPoints hitPoints;
        private Mock<Dice> mockDice;
        private Dictionary<int, Mock<PartialRoll>> mockPartialRolls;

        [SetUp]
        public void Setup()
        {
            hitPoints = new HitPoints();
            mockDice = new Mock<Dice>();
            mockPartialRolls = new Dictionary<int, Mock<PartialRoll>>();

            mockDice.Setup(d => d.Roll(It.IsAny<int>())).Returns((int q) => mockPartialRolls[q].Object);
        }

        [Test]
        public void HitPointsInitialized()
        {
            Assert.That(hitPoints.Bonus, Is.EqualTo(0));
            Assert.That(hitPoints.Constitution, Is.Null);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("0"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(0));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(0));
            Assert.That(hitPoints.HitDie, Is.EqualTo(0));
            Assert.That(hitPoints.Total, Is.EqualTo(0));
        }

        [TestCase(0, 0, 0, 0, "0")]
        [TestCase(0, 0, 0, 3, "3")]
        [TestCase(0, 0, 8, 0, "0")]
        [TestCase(0, 0, 8, 3, "3")]
        [TestCase(0, 0, 10, 0, "0")]
        [TestCase(0, 0, 10, 3, "3")]
        [TestCase(0, 0, 18, 0, "0")]
        [TestCase(0, 0, 18, 3, "3")]
        [TestCase(0, 8, 0, 0, "0")]
        [TestCase(0, 8, 0, 3, "3")]
        [TestCase(0, 8, 8, 0, "0")]
        [TestCase(0, 8, 8, 3, "3")]
        [TestCase(0, 8, 10, 0, "0")]
        [TestCase(0, 8, 10, 3, "3")]
        [TestCase(0, 8, 18, 0, "0")]
        [TestCase(0, 8, 18, 3, "3")]
        [TestCase(0, 10, 0, 0, "0")]
        [TestCase(0, 10, 0, 3, "3")]
        [TestCase(0, 10, 8, 0, "0")]
        [TestCase(0, 10, 8, 3, "3")]
        [TestCase(0, 10, 10, 0, "0")]
        [TestCase(0, 10, 10, 3, "3")]
        [TestCase(0, 10, 18, 0, "0")]
        [TestCase(0, 10, 18, 3, "3")]
        [TestCase(0, 12, 0, 0, "0")]
        [TestCase(0, 12, 0, 3, "3")]
        [TestCase(0, 12, 8, 0, "0")]
        [TestCase(0, 12, 8, 3, "3")]
        [TestCase(0, 12, 10, 0, "0")]
        [TestCase(0, 12, 10, 3, "3")]
        [TestCase(0, 12, 18, 0, "0")]
        [TestCase(0, 12, 18, 3, "3")]
        [TestCase(1, 0, 0, 0, "0")]
        [TestCase(1, 0, 0, 3, "3")]
        [TestCase(1, 0, 8, 0, "0")]
        [TestCase(1, 0, 8, 3, "3")]
        [TestCase(1, 0, 10, 0, "0")]
        [TestCase(1, 0, 10, 3, "3")]
        [TestCase(1, 0, 18, 0, "0")]
        [TestCase(1, 0, 18, 3, "3")]
        [TestCase(1, 8, 0, 0, "1d8")]
        [TestCase(1, 8, 0, 3, "1d8+3")]
        [TestCase(1, 8, 8, 0, "1d8-1")]
        [TestCase(1, 8, 8, 3, "1d8-1+3")]
        [TestCase(1, 8, 10, 0, "1d8")]
        [TestCase(1, 8, 10, 3, "1d8+3")]
        [TestCase(1, 8, 18, 0, "1d8+4")]
        [TestCase(1, 8, 18, 3, "1d8+4+3")]
        [TestCase(1, 10, 0, 0, "1d10")]
        [TestCase(1, 10, 0, 3, "1d10+3")]
        [TestCase(1, 10, 8, 0, "1d10-1")]
        [TestCase(1, 10, 8, 3, "1d10-1+3")]
        [TestCase(1, 10, 10, 0, "1d10")]
        [TestCase(1, 10, 10, 3, "1d10+3")]
        [TestCase(1, 10, 18, 0, "1d10+4")]
        [TestCase(1, 10, 18, 3, "1d10+4+3")]
        [TestCase(1, 12, 0, 0, "1d12")]
        [TestCase(1, 12, 0, 3, "1d12+3")]
        [TestCase(1, 12, 8, 0, "1d12-1")]
        [TestCase(1, 12, 8, 3, "1d12-1+3")]
        [TestCase(1, 12, 10, 0, "1d12")]
        [TestCase(1, 12, 10, 3, "1d12+3")]
        [TestCase(1, 12, 18, 0, "1d12+4")]
        [TestCase(1, 12, 18, 3, "1d12+4+3")]
        [TestCase(2, 0, 0, 0, "0")]
        [TestCase(2, 0, 0, 3, "3")]
        [TestCase(2, 0, 8, 0, "0")]
        [TestCase(2, 0, 8, 3, "3")]
        [TestCase(2, 0, 10, 0, "0")]
        [TestCase(2, 0, 10, 3, "3")]
        [TestCase(2, 0, 18, 0, "0")]
        [TestCase(2, 0, 18, 3, "3")]
        [TestCase(2, 8, 0, 0, "2d8")]
        [TestCase(2, 8, 0, 3, "2d8+3")]
        [TestCase(2, 8, 8, 0, "2d8-2")]
        [TestCase(2, 8, 8, 3, "2d8-2+3")]
        [TestCase(2, 8, 10, 0, "2d8")]
        [TestCase(2, 8, 10, 3, "2d8+3")]
        [TestCase(2, 8, 18, 0, "2d8+8")]
        [TestCase(2, 8, 18, 3, "2d8+8+3")]
        [TestCase(2, 10, 0, 0, "2d10")]
        [TestCase(2, 10, 0, 3, "2d10+3")]
        [TestCase(2, 10, 8, 0, "2d10-2")]
        [TestCase(2, 10, 8, 3, "2d10-2+3")]
        [TestCase(2, 10, 10, 0, "2d10")]
        [TestCase(2, 10, 10, 3, "2d10+3")]
        [TestCase(2, 10, 18, 0, "2d10+8")]
        [TestCase(2, 10, 18, 3, "2d10+8+3")]
        [TestCase(2, 12, 0, 0, "2d12")]
        [TestCase(2, 12, 0, 3, "2d12+3")]
        [TestCase(2, 12, 8, 0, "2d12-2")]
        [TestCase(2, 12, 8, 3, "2d12-2+3")]
        [TestCase(2, 12, 10, 0, "2d12")]
        [TestCase(2, 12, 10, 3, "2d12+3")]
        [TestCase(2, 12, 18, 0, "2d12+8")]
        [TestCase(2, 12, 18, 3, "2d12+8+3")]
        public void DefaultRoll(int quantity, int die, int constitution, int bonus, string roll)
        {
            hitPoints.HitDiceQuantity = quantity;
            hitPoints.HitDie = die;
            hitPoints.Bonus = bonus;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = constitution };

            Assert.That(hitPoints.DefaultRoll, Is.EqualTo(roll));
        }

        [Test]
        public void RollTotal()
        {
            hitPoints.HitDiceQuantity = 9266;
            hitPoints.HitDie = 90210;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 42 };

            SetUpRoll(9266, 90210, 600);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(616));
        }

        [Test]
        public void RerollTotal()
        {
            hitPoints.HitDiceQuantity = 9266;
            hitPoints.HitDie = 90210;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 42 };

            SetUpRoll(9266, 90210, 600);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(616));

            SetUpRoll(9266, 90210, 1337);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(1353));
        }

        [Test]
        public void RollTotalWithoutConstitutionScore()
        {
            hitPoints.HitDiceQuantity = 9266;
            hitPoints.HitDie = 90210;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 0 };

            SetUpRoll(9266, 90210, 600);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(600));
        }

        private void SetUpRoll(int quantity, int die, params int[] rolls)
        {
            if (!mockPartialRolls.ContainsKey(quantity))
                mockPartialRolls[quantity] = new Mock<PartialRoll>();

            var endRoll = new Mock<PartialRoll>();
            endRoll.Setup(r => r.AsIndividualRolls()).Returns(rolls);

            mockPartialRolls[quantity].Setup(r => r.d(die)).Returns(endRoll.Object);
        }

        [Test]
        public void ConstitutionBonusAppliedPerHitDie()
        {
            hitPoints.HitDiceQuantity = 9266;
            hitPoints.HitDie = 90210;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 20 };

            SetUpRoll(9266, 90210, 42, 600);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(652));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerHitDie()
        {
            hitPoints.HitDiceQuantity = 9266;
            hitPoints.HitDie = 90210;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 1 };

            SetUpRoll(9266, 90210, 1, 3, 5);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(3));
        }

        [Test]
        public void MinimumCheckAppliedPerHitDieOnRoll()
        {
            hitPoints.HitDiceQuantity = 9266;
            hitPoints.HitDie = 90210;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 6 };

            SetUpRoll(9266, 90210, 1, 2, 4);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.Not.EqualTo(1)); //1 + 2 + 4 + 3 * -2 = 7 - 6
            Assert.That(hitPoints.Total, Is.EqualTo(4)); //[1-2,1]+[2-2,1]+[4-2,1] = 1 + 1 + 2
        }

        [TestCase(0, 0)]
        [TestCase(0.5, 0)]
        [TestCase(1, 1)]
        [TestCase(1.5, 1)]
        [TestCase(2, 2)]
        [TestCase(2.5, 2)]
        [TestCase(3, 3)]
        public void RollDefault(double average, int defaultTotal)
        {
            mockDice.Setup(d => d.Roll(hitPoints.DefaultRoll).AsPotentialAverage()).Returns(defaultTotal);

            hitPoints.RollDefault(mockDice.Object);
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(defaultTotal));
        }
    }
}
