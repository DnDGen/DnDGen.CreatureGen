using DnDGen.CreatureGen.Defenses;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Defenses
{
    [TestFixture]
    public class HitDiceTests
    {
        private HitDice hitDice;
        private Mock<Dice> mockDice;
        private Dictionary<int, Mock<PartialRoll>> mockPartialRolls;

        [SetUp]
        public void Setup()
        {
            hitDice = new HitDice();
            mockDice = new Mock<Dice>();
            mockPartialRolls = new Dictionary<int, Mock<PartialRoll>>();

            mockDice.Setup(d => d.Roll(It.IsAny<int>())).Returns((int q) => mockPartialRolls[q].Object);
        }

        [Test]
        public void HitDiceInitialized()
        {
            Assert.That(hitDice.DefaultRoll, Is.EqualTo("0"));
            Assert.That(hitDice.Quantity, Is.Zero);
            Assert.That(hitDice.RoundedQuantity, Is.Zero);
            Assert.That(hitDice.HitDie, Is.Zero);
            Assert.That(hitDice.Divisor, Is.EqualTo(1));
        }

        [TestCase(0, 0, "0")]
        [TestCase(0, 8, "0d8")]
        [TestCase(0, 10, "0d10")]
        [TestCase(0, 12, "0d12")]
        [TestCase(.1, 0, "0")]
        [TestCase(.1, 8, "1d8/10")]
        [TestCase(.1, 10, "1d10/10")]
        [TestCase(.1, 12, "1d12/10")]
        [TestCase(.125, 0, "0")]
        [TestCase(.125, 8, "1d8/8")]
        [TestCase(.125, 10, "1d10/8")]
        [TestCase(.125, 12, "1d12/8")]
        [TestCase(.166, 0, "0")]
        [TestCase(.166, 8, "1d8/6")]
        [TestCase(.166, 10, "1d10/6")]
        [TestCase(.166, 12, "1d12/6")]
        [TestCase(.25, 0, "0")]
        [TestCase(.25, 8, "1d8/4")]
        [TestCase(.25, 10, "1d10/4")]
        [TestCase(.25, 12, "1d12/4")]
        [TestCase(.333, 0, "0")]
        [TestCase(.333, 8, "1d8/3")]
        [TestCase(.333, 10, "1d10/3")]
        [TestCase(.333, 12, "1d12/3")]
        [TestCase(.5, 0, "0")]
        [TestCase(.5, 8, "1d8/2")]
        [TestCase(.5, 10, "1d10/2")]
        [TestCase(.5, 12, "1d12/2")]
        [TestCase(1, 0, "0")]
        [TestCase(1, 8, "1d8")]
        [TestCase(1, 10, "1d10")]
        [TestCase(1, 12, "1d12")]
        [TestCase(1.5, 0, "0")]
        [TestCase(1.5, 8, "1d8")]
        [TestCase(1.5, 10, "1d10")]
        [TestCase(1.5, 12, "1d12")]
        [TestCase(2, 0, "0")]
        [TestCase(2, 8, "2d8")]
        [TestCase(2, 10, "2d10")]
        [TestCase(2, 12, "2d12")]
        [TestCase(2.9999, 0, "0")]
        [TestCase(2.9999, 8, "2d8")]
        [TestCase(2.9999, 10, "2d10")]
        [TestCase(10, 0, "0")]
        [TestCase(10, 8, "10d8")]
        [TestCase(10, 10, "10d10")]
        [TestCase(10, 12, "10d12")]
        [TestCase(100, 0, "0")]
        [TestCase(100, 8, "100d8")]
        [TestCase(100, 10, "100d10")]
        [TestCase(100, 12, "100d12")]
        [TestCase(9266, 90210, "9266d90210")]
        [TestCase(4.2, 600, "4d600")]
        [TestCase(.1337, 1336, "1d1336/7")]
        public void DefaultRoll(double quantity, int die, string roll)
        {
            hitDice.Quantity = quantity;
            hitDice.HitDie = die;

            Assert.That(hitDice.DefaultRoll, Is.EqualTo(roll));
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
        public void RoundedQuantity(double quantity, int roundedValue)
        {
            hitDice.Quantity = quantity;
            Assert.That(hitDice.RoundedQuantity, Is.EqualTo(roundedValue));
        }

        [TestCase(0, 1)]
        [TestCase(.01, 100)]
        [TestCase(.1, 10)]
        [TestCase(.111, 9)]
        [TestCase(.125, 8)]
        [TestCase(.166, 6)]
        [TestCase(.2, 5)]
        [TestCase(.25, 4)]
        [TestCase(.333, 3)]
        [TestCase(.5, 2)]
        [TestCase(1, 1)]
        [TestCase(1.5, 1)]
        [TestCase(2, 1)]
        [TestCase(2.9999, 1)]
        [TestCase(10, 1)]
        [TestCase(100, 1)]
        [TestCase(9266, 1)]
        [TestCase(4.2, 1)]
        [TestCase(.1337, 7)]
        public void Divisor(double quantity, int divisor)
        {
            hitDice.Quantity = quantity;
            Assert.That(hitDice.Divisor, Is.EqualTo(divisor));
        }
    }
}
