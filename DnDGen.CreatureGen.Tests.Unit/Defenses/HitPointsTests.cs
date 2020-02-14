using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Defenses;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Defenses
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
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.Null);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("0"));
            Assert.That(hitPoints.DefaultTotal, Is.Zero);
            Assert.That(hitPoints.HitDiceQuantity, Is.Zero);
            Assert.That(hitPoints.HitDie, Is.Zero);
            Assert.That(hitPoints.Total, Is.Zero);
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
        [TestCase(.25, 0, 0, 0, "0")]
        [TestCase(.25, 0, 0, 3, "3")]
        [TestCase(.25, 0, 8, 0, "0")]
        [TestCase(.25, 0, 8, 3, "3")]
        [TestCase(.25, 0, 10, 0, "0")]
        [TestCase(.25, 0, 10, 3, "3")]
        [TestCase(.25, 0, 18, 0, "0")]
        [TestCase(.25, 0, 18, 3, "3")]
        [TestCase(.25, 8, 0, 0, "1d8/4")]
        [TestCase(.25, 8, 0, 3, "1d8/4+3")]
        [TestCase(.25, 8, 8, 0, "1d8/4-1")]
        [TestCase(.25, 8, 8, 3, "1d8/4-1+3")]
        [TestCase(.25, 8, 10, 0, "1d8/4")]
        [TestCase(.25, 8, 10, 3, "1d8/4+3")]
        [TestCase(.25, 8, 18, 0, "1d8/4+4")]
        [TestCase(.25, 8, 18, 3, "1d8/4+4+3")]
        [TestCase(.25, 10, 0, 0, "1d10/4")]
        [TestCase(.25, 10, 0, 3, "1d10/4+3")]
        [TestCase(.25, 10, 8, 0, "1d10/4-1")]
        [TestCase(.25, 10, 8, 3, "1d10/4-1+3")]
        [TestCase(.25, 10, 10, 0, "1d10/4")]
        [TestCase(.25, 10, 10, 3, "1d10/4+3")]
        [TestCase(.25, 10, 18, 0, "1d10/4+4")]
        [TestCase(.25, 10, 18, 3, "1d10/4+4+3")]
        [TestCase(.25, 12, 0, 0, "1d12/4")]
        [TestCase(.25, 12, 0, 3, "1d12/4+3")]
        [TestCase(.25, 12, 8, 0, "1d12/4-1")]
        [TestCase(.25, 12, 8, 3, "1d12/4-1+3")]
        [TestCase(.25, 12, 10, 0, "1d12/4")]
        [TestCase(.25, 12, 10, 3, "1d12/4+3")]
        [TestCase(.25, 12, 18, 0, "1d12/4+4")]
        [TestCase(.25, 12, 18, 3, "1d12/4+4+3")]
        [TestCase(.5, 0, 0, 0, "0")]
        [TestCase(.5, 0, 0, 3, "3")]
        [TestCase(.5, 0, 8, 0, "0")]
        [TestCase(.5, 0, 8, 3, "3")]
        [TestCase(.5, 0, 10, 0, "0")]
        [TestCase(.5, 0, 10, 3, "3")]
        [TestCase(.5, 0, 18, 0, "0")]
        [TestCase(.5, 0, 18, 3, "3")]
        [TestCase(.5, 8, 0, 0, "1d8/2")]
        [TestCase(.5, 8, 0, 3, "1d8/2+3")]
        [TestCase(.5, 8, 8, 0, "1d8/2-1")]
        [TestCase(.5, 8, 8, 3, "1d8/2-1+3")]
        [TestCase(.5, 8, 10, 0, "1d8/2")]
        [TestCase(.5, 8, 10, 3, "1d8/2+3")]
        [TestCase(.5, 8, 18, 0, "1d8/2+4")]
        [TestCase(.5, 8, 18, 3, "1d8/2+4+3")]
        [TestCase(.5, 10, 0, 0, "1d10/2")]
        [TestCase(.5, 10, 0, 3, "1d10/2+3")]
        [TestCase(.5, 10, 8, 0, "1d10/2-1")]
        [TestCase(.5, 10, 8, 3, "1d10/2-1+3")]
        [TestCase(.5, 10, 10, 0, "1d10/2")]
        [TestCase(.5, 10, 10, 3, "1d10/2+3")]
        [TestCase(.5, 10, 18, 0, "1d10/2+4")]
        [TestCase(.5, 10, 18, 3, "1d10/2+4+3")]
        [TestCase(.5, 12, 0, 0, "1d12/2")]
        [TestCase(.5, 12, 0, 3, "1d12/2+3")]
        [TestCase(.5, 12, 8, 0, "1d12/2-1")]
        [TestCase(.5, 12, 8, 3, "1d12/2-1+3")]
        [TestCase(.5, 12, 10, 0, "1d12/2")]
        [TestCase(.5, 12, 10, 3, "1d12/2+3")]
        [TestCase(.5, 12, 18, 0, "1d12/2+4")]
        [TestCase(.5, 12, 18, 3, "1d12/2+4+3")]
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
        [TestCase(1.5, 0, 0, 0, "0")]
        [TestCase(1.5, 0, 0, 3, "3")]
        [TestCase(1.5, 0, 8, 0, "0")]
        [TestCase(1.5, 0, 8, 3, "3")]
        [TestCase(1.5, 0, 10, 0, "0")]
        [TestCase(1.5, 0, 10, 3, "3")]
        [TestCase(1.5, 0, 18, 0, "0")]
        [TestCase(1.5, 0, 18, 3, "3")]
        [TestCase(1.5, 8, 0, 0, "1d8")]
        [TestCase(1.5, 8, 0, 3, "1d8+3")]
        [TestCase(1.5, 8, 8, 0, "1d8-1")]
        [TestCase(1.5, 8, 8, 3, "1d8-1+3")]
        [TestCase(1.5, 8, 10, 0, "1d8")]
        [TestCase(1.5, 8, 10, 3, "1d8+3")]
        [TestCase(1.5, 8, 18, 0, "1d8+4")]
        [TestCase(1.5, 8, 18, 3, "1d8+4+3")]
        [TestCase(1.5, 10, 0, 0, "1d10")]
        [TestCase(1.5, 10, 0, 3, "1d10+3")]
        [TestCase(1.5, 10, 8, 0, "1d10-1")]
        [TestCase(1.5, 10, 8, 3, "1d10-1+3")]
        [TestCase(1.5, 10, 10, 0, "1d10")]
        [TestCase(1.5, 10, 10, 3, "1d10+3")]
        [TestCase(1.5, 10, 18, 0, "1d10+4")]
        [TestCase(1.5, 10, 18, 3, "1d10+4+3")]
        [TestCase(1.5, 12, 0, 0, "1d12")]
        [TestCase(1.5, 12, 0, 3, "1d12+3")]
        [TestCase(1.5, 12, 8, 0, "1d12-1")]
        [TestCase(1.5, 12, 8, 3, "1d12-1+3")]
        [TestCase(1.5, 12, 10, 0, "1d12")]
        [TestCase(1.5, 12, 10, 3, "1d12+3")]
        [TestCase(1.5, 12, 18, 0, "1d12+4")]
        [TestCase(1.5, 12, 18, 3, "1d12+4+3")]
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
        [TestCase(2.9999, 0, 0, 0, "0")]
        [TestCase(2.9999, 0, 0, 3, "3")]
        [TestCase(2.9999, 0, 8, 0, "0")]
        [TestCase(2.9999, 0, 8, 3, "3")]
        [TestCase(2.9999, 0, 10, 0, "0")]
        [TestCase(2.9999, 0, 10, 3, "3")]
        [TestCase(2.9999, 0, 18, 0, "0")]
        [TestCase(2.9999, 0, 18, 3, "3")]
        [TestCase(2.9999, 8, 0, 0, "2d8")]
        [TestCase(2.9999, 8, 0, 3, "2d8+3")]
        [TestCase(2.9999, 8, 8, 0, "2d8-2")]
        [TestCase(2.9999, 8, 8, 3, "2d8-2+3")]
        [TestCase(2.9999, 8, 10, 0, "2d8")]
        [TestCase(2.9999, 8, 10, 3, "2d8+3")]
        [TestCase(2.9999, 8, 18, 0, "2d8+8")]
        [TestCase(2.9999, 8, 18, 3, "2d8+8+3")]
        [TestCase(2.9999, 10, 0, 0, "2d10")]
        [TestCase(2.9999, 10, 0, 3, "2d10+3")]
        [TestCase(2.9999, 10, 8, 0, "2d10-2")]
        [TestCase(2.9999, 10, 8, 3, "2d10-2+3")]
        [TestCase(2.9999, 10, 10, 0, "2d10")]
        [TestCase(2.9999, 10, 10, 3, "2d10+3")]
        [TestCase(2.9999, 10, 18, 0, "2d10+8")]
        [TestCase(2.9999, 10, 18, 3, "2d10+8+3")]
        [TestCase(2.9999, 12, 0, 0, "2d12")]
        [TestCase(2.9999, 12, 0, 3, "2d12+3")]
        [TestCase(2.9999, 12, 8, 0, "2d12-2")]
        [TestCase(2.9999, 12, 8, 3, "2d12-2+3")]
        [TestCase(2.9999, 12, 10, 0, "2d12")]
        [TestCase(2.9999, 12, 10, 3, "2d12+3")]
        [TestCase(2.9999, 12, 18, 0, "2d12+8")]
        [TestCase(2.9999, 12, 18, 3, "2d12+8+3")]
        public void DefaultRoll(double quantity, int die, int constitution, int bonus, string roll)
        {
            hitPoints.HitDiceQuantity = quantity;
            hitPoints.HitDie = die;
            hitPoints.Bonus = bonus;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = constitution };

            Assert.That(hitPoints.DefaultRoll, Is.EqualTo(roll));
        }

        [TestCase(0, 0)]
        [TestCase(.01, 1)]
        [TestCase(.1, 1)]
        [TestCase(.25, 1)]
        [TestCase(.5, 1)]
        [TestCase(.6, 1)]
        [TestCase(.9266, 1)]
        [TestCase(1, 1)]
        [TestCase(1.5, 1)]
        [TestCase(2, 2)]
        [TestCase(2.5, 2)]
        [TestCase(2.999999999, 2)]
        [TestCase(9.266, 9)]
        [TestCase(92.66, 92)]
        [TestCase(926.6, 926)]
        [TestCase(9266, 9266)]
        public void RoundedHitDiceQuantity(double quantity, int roundedValue)
        {
            hitPoints.HitDiceQuantity = quantity;
            Assert.That(hitPoints.RoundedHitDiceQuantity, Is.EqualTo(roundedValue));
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

        [Test]
        public void RollTotalWithoutConstitutionScore_FractionalDice()
        {
            hitPoints.HitDiceQuantity = .5;
            hitPoints.HitDie = 8;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 0 };

            SetUpRoll(1, 8, 5);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(2));
        }

        [Test]
        public void RollTotalWithoutConstitutionScore_LowFractionalDice()
        {
            hitPoints.HitDiceQuantity = .25;
            hitPoints.HitDie = 8;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 0 };

            SetUpRoll(1, 8, 3);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(1));
        }

        private void SetUpRoll(int quantity, int die, params int[] rolls)
        {
            if (!mockPartialRolls.ContainsKey(quantity))
                mockPartialRolls[quantity] = new Mock<PartialRoll>();

            var endRoll = new Mock<PartialRoll>();
            endRoll.Setup(r => r.AsIndividualRolls<int>()).Returns(rolls);

            mockPartialRolls[quantity].Setup(r => r.d(die)).Returns(endRoll.Object);
        }

        [Test]
        public void ConstitutionBonusAppliedPerHitDie()
        {
            hitPoints.HitDiceQuantity = 2;
            hitPoints.HitDie = 90210;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 20 };

            SetUpRoll(2, 90210, 42, 600);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(652));
        }

        [Test]
        public void ConstitutionBonusAppliedPerHitDie_FractionalDice()
        {
            hitPoints.HitDiceQuantity = .5;
            hitPoints.HitDie = 8;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 20 };

            SetUpRoll(1, 8, 4);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(7));
        }

        [Test]
        public void ConstitutionBonusAppliedPerHitDie_LowFractionalDice()
        {
            hitPoints.HitDiceQuantity = .25;
            hitPoints.HitDie = 8;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 20 };

            SetUpRoll(1, 8, 2);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(6));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerHitDie()
        {
            hitPoints.HitDiceQuantity = 3;
            hitPoints.HitDie = 90210;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 1 };

            SetUpRoll(3, 90210, 1, 3, 5);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(3));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerHitDie_FractionalDice()
        {
            hitPoints.HitDiceQuantity = .5;
            hitPoints.HitDie = 8;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 1 };

            SetUpRoll(1, 8, 4);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(1));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerHitDie_LowFractionalDice()
        {
            hitPoints.HitDiceQuantity = .25;
            hitPoints.HitDie = 8;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 1 };

            SetUpRoll(1, 8, 3);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(1));
        }

        [Test]
        public void MinimumCheckAppliedPerHitDieOnRoll()
        {
            hitPoints.HitDiceQuantity = 3;
            hitPoints.HitDie = 90210;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 6 };

            SetUpRoll(3, 90210, 1, 2, 4);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.Not.EqualTo(1) //(1 + 2 + 4) + 3 * -2 = 7 -  = 1
                .And.Not.EqualTo(3) //[(1 + 2 + 4) + 3 * -2, 3] = [7 - 6, 3] = 3
                .And.EqualTo(4)); //[1-2,1]+[2-2,1]+[4-2,1] = 1 + 1 + 2 = 4
        }

        [Test]
        public void MinimumCheckAppliedPerHitDieOnRoll_FractionalDice()
        {
            hitPoints.HitDiceQuantity = .5;
            hitPoints.HitDie = 8;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 6 };

            SetUpRoll(1, 8, 2);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(1));
        }

        [Test]
        public void MinimumCheckAppliedPerHitDieOnRoll_LowFractionalDice()
        {
            hitPoints.HitDiceQuantity = .25;
            hitPoints.HitDie = 8;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = 6 };

            SetUpRoll(1, 8, 1);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(1));
        }

        [TestCase(0, 6, 0, 0, 1)]
        [TestCase(0, 6, 0, 3, 4)]
        [TestCase(0, 6, 4, 0, 1)]
        [TestCase(0, 6, 4, 3, 4)]
        [TestCase(0, 6, 8, 0, 1)]
        [TestCase(0, 6, 8, 3, 4)]
        [TestCase(0, 6, 10, 0, 1)]
        [TestCase(0, 6, 10, 3, 4)]
        [TestCase(0, 6, 12, 0, 1)]
        [TestCase(0, 6, 12, 3, 4)]
        [TestCase(0, 6, 18, 0, 1)]
        [TestCase(0, 6, 18, 3, 4)]
        [TestCase(0, 8, 0, 0, 1)]
        [TestCase(0, 8, 0, 3, 4)]
        [TestCase(0, 8, 4, 0, 1)]
        [TestCase(0, 8, 4, 3, 4)]
        [TestCase(0, 8, 8, 0, 1)]
        [TestCase(0, 8, 8, 3, 4)]
        [TestCase(0, 8, 10, 0, 1)]
        [TestCase(0, 8, 10, 3, 4)]
        [TestCase(0, 8, 12, 0, 1)]
        [TestCase(0, 8, 12, 3, 4)]
        [TestCase(0, 8, 18, 0, 1)]
        [TestCase(0, 8, 18, 3, 4)]
        [TestCase(0, 10, 0, 0, 1)]
        [TestCase(0, 10, 0, 3, 4)]
        [TestCase(0, 10, 4, 0, 1)]
        [TestCase(0, 10, 4, 3, 4)]
        [TestCase(0, 10, 8, 0, 1)]
        [TestCase(0, 10, 8, 3, 4)]
        [TestCase(0, 10, 10, 0, 1)]
        [TestCase(0, 10, 10, 3, 4)]
        [TestCase(0, 10, 12, 0, 1)]
        [TestCase(0, 10, 12, 3, 4)]
        [TestCase(0, 10, 18, 0, 1)]
        [TestCase(0, 10, 18, 3, 4)]
        [TestCase(0, 12, 0, 0, 1)]
        [TestCase(0, 12, 0, 3, 4)]
        [TestCase(0, 12, 4, 0, 1)]
        [TestCase(0, 12, 4, 3, 4)]
        [TestCase(0, 12, 8, 0, 1)]
        [TestCase(0, 12, 8, 3, 4)]
        [TestCase(0, 12, 10, 0, 1)]
        [TestCase(0, 12, 10, 3, 4)]
        [TestCase(0, 12, 12, 0, 1)]
        [TestCase(0, 12, 12, 3, 4)]
        [TestCase(0, 12, 18, 0, 1)]
        [TestCase(0, 12, 18, 3, 4)]
        [TestCase(0.1, 6, 0, 0, 1)]
        [TestCase(0.1, 6, 0, 3, 4)]
        [TestCase(0.1, 6, 4, 0, 1)]
        [TestCase(0.1, 6, 4, 3, 4)]
        [TestCase(0.1, 6, 8, 0, 1)]
        [TestCase(0.1, 6, 8, 3, 4)]
        [TestCase(0.1, 6, 10, 0, 1)]
        [TestCase(0.1, 6, 10, 3, 4)]
        [TestCase(0.1, 6, 12, 0, 2)]
        [TestCase(0.1, 6, 12, 3, 5)]
        [TestCase(0.1, 6, 18, 0, 5)]
        [TestCase(0.1, 6, 18, 3, 8)]
        [TestCase(0.1, 8, 0, 0, 1)]
        [TestCase(0.1, 8, 0, 3, 4)]
        [TestCase(0.1, 8, 4, 0, 1)]
        [TestCase(0.1, 8, 4, 3, 4)]
        [TestCase(0.1, 8, 8, 0, 1)]
        [TestCase(0.1, 8, 8, 3, 4)]
        [TestCase(0.1, 8, 10, 0, 1)]
        [TestCase(0.1, 8, 10, 3, 4)]
        [TestCase(0.1, 8, 12, 0, 2)]
        [TestCase(0.1, 8, 12, 3, 5)]
        [TestCase(0.1, 8, 18, 0, 5)]
        [TestCase(0.1, 8, 18, 3, 8)]
        [TestCase(0.1, 10, 0, 0, 1)]
        [TestCase(0.1, 10, 0, 3, 4)]
        [TestCase(0.1, 10, 4, 0, 1)]
        [TestCase(0.1, 10, 4, 3, 4)]
        [TestCase(0.1, 10, 8, 0, 1)]
        [TestCase(0.1, 10, 8, 3, 4)]
        [TestCase(0.1, 10, 10, 0, 1)]
        [TestCase(0.1, 10, 10, 3, 4)]
        [TestCase(0.1, 10, 12, 0, 2)]
        [TestCase(0.1, 10, 12, 3, 5)]
        [TestCase(0.1, 10, 18, 0, 5)]
        [TestCase(0.1, 10, 18, 3, 8)]
        [TestCase(0.1, 12, 0, 0, 1)]
        [TestCase(0.1, 12, 0, 3, 4)]
        [TestCase(0.1, 12, 4, 0, 1)]
        [TestCase(0.1, 12, 4, 3, 4)]
        [TestCase(0.1, 12, 8, 0, 1)]
        [TestCase(0.1, 12, 8, 3, 4)]
        [TestCase(0.1, 12, 10, 0, 1)]
        [TestCase(0.1, 12, 10, 3, 4)]
        [TestCase(0.1, 12, 12, 0, 2)]
        [TestCase(0.1, 12, 12, 3, 5)]
        [TestCase(0.1, 12, 18, 0, 5)]
        [TestCase(0.1, 12, 18, 3, 8)]
        [TestCase(0.25, 6, 0, 0, 1)]
        [TestCase(0.25, 6, 0, 3, 4)]
        [TestCase(0.25, 6, 4, 0, 1)]
        [TestCase(0.25, 6, 4, 3, 4)]
        [TestCase(0.25, 6, 8, 0, 1)]
        [TestCase(0.25, 6, 8, 3, 4)]
        [TestCase(0.25, 6, 10, 0, 1)]
        [TestCase(0.25, 6, 10, 3, 4)]
        [TestCase(0.25, 6, 12, 0, 2)]
        [TestCase(0.25, 6, 12, 3, 5)]
        [TestCase(0.25, 6, 18, 0, 5)]
        [TestCase(0.25, 6, 18, 3, 8)]
        [TestCase(0.25, 8, 0, 0, 1)]
        [TestCase(0.25, 8, 0, 3, 4)]
        [TestCase(0.25, 8, 4, 0, 1)]
        [TestCase(0.25, 8, 4, 3, 4)]
        [TestCase(0.25, 8, 8, 0, 1)]
        [TestCase(0.25, 8, 8, 3, 4)]
        [TestCase(0.25, 8, 10, 0, 1)]
        [TestCase(0.25, 8, 10, 3, 4)]
        [TestCase(0.25, 8, 12, 0, 2)]
        [TestCase(0.25, 8, 12, 3, 5)]
        [TestCase(0.25, 8, 18, 0, 5)]
        [TestCase(0.25, 8, 18, 3, 8)]
        [TestCase(0.25, 10, 0, 0, 1)]
        [TestCase(0.25, 10, 0, 3, 4)]
        [TestCase(0.25, 10, 4, 0, 1)]
        [TestCase(0.25, 10, 4, 3, 4)]
        [TestCase(0.25, 10, 8, 0, 1)]
        [TestCase(0.25, 10, 8, 3, 4)]
        [TestCase(0.25, 10, 10, 0, 1)]
        [TestCase(0.25, 10, 10, 3, 4)]
        [TestCase(0.25, 10, 12, 0, 2)]
        [TestCase(0.25, 10, 12, 3, 5)]
        [TestCase(0.25, 10, 18, 0, 5)]
        [TestCase(0.25, 10, 18, 3, 8)]
        [TestCase(0.25, 12, 0, 0, 1)]
        [TestCase(0.25, 12, 0, 3, 4)]
        [TestCase(0.25, 12, 4, 0, 1)]
        [TestCase(0.25, 12, 4, 3, 4)]
        [TestCase(0.25, 12, 8, 0, 1)]
        [TestCase(0.25, 12, 8, 3, 4)]
        [TestCase(0.25, 12, 10, 0, 1)]
        [TestCase(0.25, 12, 10, 3, 4)]
        [TestCase(0.25, 12, 12, 0, 2)]
        [TestCase(0.25, 12, 12, 3, 5)]
        [TestCase(0.25, 12, 18, 0, 5)]
        [TestCase(0.25, 12, 18, 3, 8)]
        [TestCase(0.5, 6, 0, 0, 1)]
        [TestCase(0.5, 6, 0, 3, 4)]
        [TestCase(0.5, 6, 4, 0, 1)]
        [TestCase(0.5, 6, 4, 3, 4)]
        [TestCase(0.5, 6, 8, 0, 1)]
        [TestCase(0.5, 6, 8, 3, 4)]
        [TestCase(0.5, 6, 10, 0, 1)]
        [TestCase(0.5, 6, 10, 3, 4)]
        [TestCase(0.5, 6, 12, 0, 2)]
        [TestCase(0.5, 6, 12, 3, 5)]
        [TestCase(0.5, 6, 18, 0, 5)]
        [TestCase(0.5, 6, 18, 3, 8)]
        [TestCase(0.5, 8, 0, 0, 2)]
        [TestCase(0.5, 8, 0, 3, 5)]
        [TestCase(0.5, 8, 4, 0, 1)]
        [TestCase(0.5, 8, 4, 3, 4)]
        [TestCase(0.5, 8, 8, 0, 1)]
        [TestCase(0.5, 8, 8, 3, 4)]
        [TestCase(0.5, 8, 10, 0, 2)]
        [TestCase(0.5, 8, 10, 3, 5)]
        [TestCase(0.5, 8, 12, 0, 3)]
        [TestCase(0.5, 8, 12, 3, 6)]
        [TestCase(0.5, 8, 18, 0, 6)]
        [TestCase(0.5, 8, 18, 3, 9)]
        [TestCase(0.5, 10, 0, 0, 2)]
        [TestCase(0.5, 10, 0, 3, 5)]
        [TestCase(0.5, 10, 4, 0, 1)]
        [TestCase(0.5, 10, 4, 3, 4)]
        [TestCase(0.5, 10, 8, 0, 1)]
        [TestCase(0.5, 10, 8, 3, 4)]
        [TestCase(0.5, 10, 10, 0, 2)]
        [TestCase(0.5, 10, 10, 3, 5)]
        [TestCase(0.5, 10, 12, 0, 3)]
        [TestCase(0.5, 10, 12, 3, 6)]
        [TestCase(0.5, 10, 18, 0, 6)]
        [TestCase(0.5, 10, 18, 3, 9)]
        [TestCase(0.5, 12, 0, 0, 3)]
        [TestCase(0.5, 12, 0, 3, 6)]
        [TestCase(0.5, 12, 4, 0, 1)]
        [TestCase(0.5, 12, 4, 3, 4)]
        [TestCase(0.5, 12, 8, 0, 2)]
        [TestCase(0.5, 12, 8, 3, 5)]
        [TestCase(0.5, 12, 10, 0, 3)]
        [TestCase(0.5, 12, 10, 3, 6)]
        [TestCase(0.5, 12, 12, 0, 4)]
        [TestCase(0.5, 12, 12, 3, 7)]
        [TestCase(0.5, 12, 18, 0, 7)]
        [TestCase(0.5, 12, 18, 3, 10)]
        [TestCase(1, 6, 0, 0, 3)]
        [TestCase(1, 6, 0, 3, 6)]
        [TestCase(1, 6, 4, 0, 1)]
        [TestCase(1, 6, 4, 3, 4)]
        [TestCase(1, 6, 8, 0, 2)]
        [TestCase(1, 6, 8, 3, 5)]
        [TestCase(1, 6, 10, 0, 3)]
        [TestCase(1, 6, 10, 3, 6)]
        [TestCase(1, 6, 12, 0, 4)]
        [TestCase(1, 6, 12, 3, 7)]
        [TestCase(1, 6, 18, 0, 7)]
        [TestCase(1, 6, 18, 3, 10)]
        [TestCase(1, 8, 0, 0, 4)]
        [TestCase(1, 8, 0, 3, 7)]
        [TestCase(1, 8, 4, 0, 1)]
        [TestCase(1, 8, 4, 3, 4)]
        [TestCase(1, 8, 8, 0, 3)]
        [TestCase(1, 8, 8, 3, 6)]
        [TestCase(1, 8, 10, 0, 4)]
        [TestCase(1, 8, 10, 3, 7)]
        [TestCase(1, 8, 12, 0, 5)]
        [TestCase(1, 8, 12, 3, 8)]
        [TestCase(1, 8, 18, 0, 8)]
        [TestCase(1, 8, 18, 3, 11)]
        [TestCase(1, 10, 0, 0, 5)]
        [TestCase(1, 10, 0, 3, 8)]
        [TestCase(1, 10, 4, 0, 2)]
        [TestCase(1, 10, 4, 3, 5)]
        [TestCase(1, 10, 8, 0, 4)]
        [TestCase(1, 10, 8, 3, 7)]
        [TestCase(1, 10, 10, 0, 5)]
        [TestCase(1, 10, 10, 3, 8)]
        [TestCase(1, 10, 12, 0, 6)]
        [TestCase(1, 10, 12, 3, 9)]
        [TestCase(1, 10, 18, 0, 9)]
        [TestCase(1, 10, 18, 3, 12)]
        [TestCase(1, 12, 0, 0, 6)]
        [TestCase(1, 12, 0, 3, 9)]
        [TestCase(1, 12, 4, 0, 3)]
        [TestCase(1, 12, 4, 3, 6)]
        [TestCase(1, 12, 8, 0, 5)]
        [TestCase(1, 12, 8, 3, 8)]
        [TestCase(1, 12, 10, 0, 6)]
        [TestCase(1, 12, 10, 3, 9)]
        [TestCase(1, 12, 12, 0, 7)]
        [TestCase(1, 12, 12, 3, 10)]
        [TestCase(1, 12, 18, 0, 10)]
        [TestCase(1, 12, 18, 3, 13)]
        [TestCase(2, 6, 0, 0, 7)]
        [TestCase(2, 6, 0, 3, 7 + 3)]
        [TestCase(2, 6, 4, 0, 2)]
        [TestCase(2, 6, 4, 3, 2 + 3)]
        [TestCase(2, 6, 8, 0, 7 - 2)]
        [TestCase(2, 6, 8, 3, 7 - 2 + 3)]
        [TestCase(2, 6, 10, 0, 7)]
        [TestCase(2, 6, 10, 3, 7 + 3)]
        [TestCase(2, 6, 12, 0, 7 + 2)]
        [TestCase(2, 6, 12, 3, 7 + 2 + 3)]
        [TestCase(2, 6, 18, 0, 7 + 8)]
        [TestCase(2, 6, 18, 3, 7 + 8 + 3)]
        [TestCase(2, 8, 0, 0, 9)]
        [TestCase(2, 8, 0, 3, 9 + 3)]
        [TestCase(2, 8, 4, 0, 9 - 6)]
        [TestCase(2, 8, 4, 3, 9 - 6 + 3)]
        [TestCase(2, 8, 8, 0, 9 - 2)]
        [TestCase(2, 8, 8, 3, 9 - 2 + 3)]
        [TestCase(2, 8, 10, 0, 9)]
        [TestCase(2, 8, 10, 3, 9 + 3)]
        [TestCase(2, 8, 12, 0, 9 + 2)]
        [TestCase(2, 8, 12, 3, 9 + 2 + 3)]
        [TestCase(2, 8, 18, 0, 9 + 8)]
        [TestCase(2, 8, 18, 3, 9 + 8 + 3)]
        [TestCase(2, 10, 0, 0, 11)]
        [TestCase(2, 10, 0, 3, 11 + 3)]
        [TestCase(2, 10, 4, 0, 11 - 6)]
        [TestCase(2, 10, 4, 3, 11 - 6 + 3)]
        [TestCase(2, 10, 8, 0, 11 - 2)]
        [TestCase(2, 10, 8, 3, 11 - 2 + 3)]
        [TestCase(2, 10, 10, 0, 11)]
        [TestCase(2, 10, 10, 3, 11 + 3)]
        [TestCase(2, 10, 12, 0, 11 + 2)]
        [TestCase(2, 10, 12, 3, 11 + 2 + 3)]
        [TestCase(2, 10, 18, 0, 11 + 8)]
        [TestCase(2, 10, 18, 3, 11 + 8 + 3)]
        [TestCase(2, 12, 0, 0, 13)]
        [TestCase(2, 12, 0, 3, 13 + 3)]
        [TestCase(2, 12, 4, 0, 13 - 6)]
        [TestCase(2, 12, 4, 3, 13 - 6 + 3)]
        [TestCase(2, 12, 8, 0, 13 - 2)]
        [TestCase(2, 12, 8, 3, 13 - 2 + 3)]
        [TestCase(2, 12, 10, 0, 13)]
        [TestCase(2, 12, 10, 3, 13 + 3)]
        [TestCase(2, 12, 12, 0, 13 + 2)]
        [TestCase(2, 12, 12, 3, 13 + 2 + 3)]
        [TestCase(2, 12, 18, 0, 13 + 8)]
        [TestCase(2, 12, 18, 3, 13 + 8 + 3)]
        [TestCase(10, 6, 0, 0, 35)]
        [TestCase(10, 6, 0, 3, 35 + 3)]
        [TestCase(10, 6, 4, 0, 10)]
        [TestCase(10, 6, 4, 3, 13)]
        [TestCase(10, 6, 8, 0, 35 - 10)]
        [TestCase(10, 6, 8, 3, 35 - 10 + 3)]
        [TestCase(10, 6, 10, 0, 35)]
        [TestCase(10, 6, 10, 3, 35 + 3)]
        [TestCase(10, 6, 12, 0, 35 + 10)]
        [TestCase(10, 6, 12, 3, 35 + 10 + 3)]
        [TestCase(10, 6, 18, 0, 35 + 40)]
        [TestCase(10, 6, 18, 3, 35 + 40 + 3)]
        [TestCase(10, 8, 0, 0, 45)]
        [TestCase(10, 8, 0, 3, 45 + 3)]
        [TestCase(10, 8, 4, 0, 45 - 30)]
        [TestCase(10, 8, 4, 3, 45 - 30 + 3)]
        [TestCase(10, 8, 8, 0, 45 - 10)]
        [TestCase(10, 8, 8, 3, 45 - 10 + 3)]
        [TestCase(10, 8, 10, 0, 45)]
        [TestCase(10, 8, 10, 3, 45 + 3)]
        [TestCase(10, 8, 12, 0, 45 + 10)]
        [TestCase(10, 8, 12, 3, 45 + 10 + 3)]
        [TestCase(10, 8, 18, 0, 45 + 40)]
        [TestCase(10, 8, 18, 3, 45 + 40 + 3)]
        [TestCase(10, 10, 0, 0, 55)]
        [TestCase(10, 10, 0, 3, 55 + 3)]
        [TestCase(10, 10, 4, 0, 55 - 30)]
        [TestCase(10, 10, 4, 3, 55 - 30 + 3)]
        [TestCase(10, 10, 8, 0, 55 - 10)]
        [TestCase(10, 10, 8, 3, 55 - 10 + 3)]
        [TestCase(10, 10, 10, 0, 55)]
        [TestCase(10, 10, 10, 3, 55 + 3)]
        [TestCase(10, 10, 12, 0, 55 + 10)]
        [TestCase(10, 10, 12, 3, 55 + 10 + 3)]
        [TestCase(10, 10, 18, 0, 55 + 40)]
        [TestCase(10, 10, 18, 3, 55 + 40 + 3)]
        [TestCase(10, 12, 0, 0, 65)]
        [TestCase(10, 12, 0, 3, 65 + 3)]
        [TestCase(10, 12, 4, 0, 65 - 30)]
        [TestCase(10, 12, 4, 3, 65 - 30 + 3)]
        [TestCase(10, 12, 8, 0, 65 - 10)]
        [TestCase(10, 12, 8, 3, 65 - 10 + 3)]
        [TestCase(10, 12, 10, 0, 65)]
        [TestCase(10, 12, 10, 3, 65 + 3)]
        [TestCase(10, 12, 12, 0, 65 + 10)]
        [TestCase(10, 12, 12, 3, 65 + 10 + 3)]
        [TestCase(10, 12, 18, 0, 65 + 40)]
        [TestCase(10, 12, 18, 3, 65 + 40 + 3)]
        [TestCase(20, 6, 0, 0, 70)]
        [TestCase(20, 6, 0, 3, 70 + 3)]
        [TestCase(20, 6, 4, 0, 20)]
        [TestCase(20, 6, 4, 3, 20 + 3)]
        [TestCase(20, 6, 8, 0, 70 - 20)]
        [TestCase(20, 6, 8, 3, 70 - 20 + 3)]
        [TestCase(20, 6, 10, 0, 70)]
        [TestCase(20, 6, 10, 3, 70 + 3)]
        [TestCase(20, 6, 12, 0, 70 + 20)]
        [TestCase(20, 6, 12, 3, 70 + 20 + 3)]
        [TestCase(20, 6, 18, 0, 70 + 80)]
        [TestCase(20, 6, 18, 3, 70 + 80 + 3)]
        [TestCase(20, 8, 0, 0, 90)]
        [TestCase(20, 8, 0, 3, 90 + 3)]
        [TestCase(20, 8, 4, 0, 90 - 60)]
        [TestCase(20, 8, 4, 3, 90 - 60 + 3)]
        [TestCase(20, 8, 8, 0, 90 - 20)]
        [TestCase(20, 8, 8, 3, 90 - 20 + 3)]
        [TestCase(20, 8, 10, 0, 90)]
        [TestCase(20, 8, 10, 3, 90 + 3)]
        [TestCase(20, 8, 12, 0, 90 + 20)]
        [TestCase(20, 8, 12, 3, 90 + 20 + 3)]
        [TestCase(20, 8, 18, 0, 90 + 80)]
        [TestCase(20, 8, 18, 3, 90 + 80 + 3)]
        [TestCase(20, 10, 0, 0, 110)]
        [TestCase(20, 10, 0, 3, 110 + 3)]
        [TestCase(20, 10, 4, 0, 110 - 60)]
        [TestCase(20, 10, 4, 3, 110 - 60 + 3)]
        [TestCase(20, 10, 8, 0, 110 - 20)]
        [TestCase(20, 10, 8, 3, 110 - 20 + 3)]
        [TestCase(20, 10, 10, 0, 110)]
        [TestCase(20, 10, 10, 3, 110 + 3)]
        [TestCase(20, 10, 12, 0, 110 + 20)]
        [TestCase(20, 10, 12, 3, 110 + 20 + 3)]
        [TestCase(20, 10, 18, 0, 110 + 80)]
        [TestCase(20, 10, 18, 3, 110 + 80 + 3)]
        [TestCase(20, 12, 0, 0, 130)]
        [TestCase(20, 12, 0, 3, 130 + 3)]
        [TestCase(20, 12, 4, 0, 130 - 60)]
        [TestCase(20, 12, 4, 3, 130 - 60 + 3)]
        [TestCase(20, 12, 8, 0, 130 - 20)]
        [TestCase(20, 12, 8, 3, 130 - 20 + 3)]
        [TestCase(20, 12, 10, 0, 130)]
        [TestCase(20, 12, 10, 3, 130 + 3)]
        [TestCase(20, 12, 12, 0, 130 + 20)]
        [TestCase(20, 12, 12, 3, 130 + 20 + 3)]
        [TestCase(20, 12, 18, 0, 130 + 80)]
        [TestCase(20, 12, 18, 3, 130 + 80 + 3)]
        [TestCase(9266, 42, 90210, 600, (43 * 9266 / 2) + (90210 - 10) * 9266 / 2 + 600)]
        public void RollDefault(double quantity, int die, int constitution, int bonus, int defaultTotal)
        {
            hitPoints.HitDiceQuantity = quantity;
            hitPoints.HitDie = die;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution) { BaseScore = constitution };
            hitPoints.Bonus = bonus;

            var mockPartialRoll = new Mock<PartialRoll>();
            var average = (1 + die) / 2d * hitPoints.RoundedHitDiceQuantity;
            mockPartialRoll.Setup(r => r.AsPotentialAverage()).Returns(average);
            mockDice.Setup(d => d.Roll(hitPoints.RoundedHitDiceQuantity).d(die)).Returns(mockPartialRoll.Object);

            hitPoints.RollDefault(mockDice.Object);
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(defaultTotal));
        }

        [TestCase(.01, 600, 6)]
        [TestCase(.1, 600, 60)]
        [TestCase(.25, 1, 1)]
        [TestCase(.25, 2, 1)]
        [TestCase(.25, 3, 1)]
        [TestCase(.25, 4, 1)]
        [TestCase(.25, 5, 1)]
        [TestCase(.25, 6, 1)]
        [TestCase(.25, 7, 1)]
        [TestCase(.25, 8, 2)]
        [TestCase(.25, 9, 2)]
        [TestCase(.25, 10, 2)]
        [TestCase(.25, 11, 2)]
        [TestCase(.25, 12, 3)]
        [TestCase(.25, 600, 150)]
        [TestCase(.5, 1, 1)]
        [TestCase(.5, 2, 1)]
        [TestCase(.5, 3, 1)]
        [TestCase(.5, 4, 2)]
        [TestCase(.5, 5, 2)]
        [TestCase(.5, 6, 3)]
        [TestCase(.5, 7, 3)]
        [TestCase(.5, 8, 4)]
        [TestCase(.5, 9, 4)]
        [TestCase(.5, 10, 5)]
        [TestCase(.5, 11, 5)]
        [TestCase(.5, 12, 6)]
        [TestCase(.5, 600, 300)]
        [TestCase(1.5, 1, 1)]
        [TestCase(1.5, 2, 2)]
        [TestCase(1.5, 3, 3)]
        [TestCase(1.5, 4, 4)]
        [TestCase(1.5, 5, 5)]
        [TestCase(1.5, 6, 6)]
        [TestCase(1.5, 7, 7)]
        [TestCase(1.5, 8, 8)]
        [TestCase(1.5, 9, 9)]
        [TestCase(1.5, 10, 10)]
        [TestCase(1.5, 11, 11)]
        [TestCase(1.5, 12, 12)]
        [TestCase(1.5, 600, 600)]
        [TestCase(2.5, 1, 1)]
        [TestCase(2.5, 2, 2)]
        [TestCase(2.5, 3, 3)]
        [TestCase(2.5, 4, 4)]
        [TestCase(2.5, 5, 5)]
        [TestCase(2.5, 6, 6)]
        [TestCase(2.5, 7, 7)]
        [TestCase(2.5, 8, 8)]
        [TestCase(2.5, 9, 9)]
        [TestCase(2.5, 10, 10)]
        [TestCase(2.5, 11, 11)]
        [TestCase(2.5, 12, 12)]
        [TestCase(2.5, 600, 600)]
        public void RollFractionalHitDice(double amount, int roll, int total)
        {
            hitPoints.HitDiceQuantity = amount;
            hitPoints.HitDie = 90210;
            hitPoints.Constitution = new Ability(AbilityConstants.Constitution);

            SetUpRoll(hitPoints.RoundedHitDiceQuantity, hitPoints.HitDie, roll);

            hitPoints.Roll(mockDice.Object);
            Assert.That(hitPoints.Total, Is.EqualTo(total));
        }
    }
}
