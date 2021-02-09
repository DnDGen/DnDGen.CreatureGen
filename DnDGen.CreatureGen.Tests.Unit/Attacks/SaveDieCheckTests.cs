using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Attacks
{
    [TestFixture]
    public class SaveDieCheckTests
    {
        private SaveDieCheck save;

        [SetUp]
        public void Setup()
        {
            save = new SaveDieCheck();
        }

        [TestCase(10, 1, 5)]
        [TestCase(10, 2, 6)]
        [TestCase(10, 3, 6)]
        [TestCase(10, 4, 7)]
        [TestCase(10, 5, 7)]
        [TestCase(10, 6, 8)]
        [TestCase(10, 7, 8)]
        [TestCase(10, 8, 9)]
        [TestCase(10, 9, 9)]
        [TestCase(10, 10, 10)]
        [TestCase(10, 11, 10)]
        [TestCase(10, 12, 11)]
        [TestCase(10, 13, 11)]
        [TestCase(10, 14, 12)]
        [TestCase(10, 15, 12)]
        [TestCase(10, 16, 13)]
        [TestCase(10, 17, 13)]
        [TestCase(10, 18, 14)]
        [TestCase(10, 19, 14)]
        [TestCase(10, 20, 15)]
        [TestCase(14, 1, 9)]
        [TestCase(14, 2, 10)]
        [TestCase(14, 3, 10)]
        [TestCase(14, 4, 11)]
        [TestCase(14, 5, 11)]
        [TestCase(14, 6, 12)]
        [TestCase(14, 7, 12)]
        [TestCase(14, 8, 13)]
        [TestCase(14, 9, 13)]
        [TestCase(14, 10, 14)]
        [TestCase(14, 11, 14)]
        [TestCase(14, 12, 15)]
        [TestCase(14, 13, 15)]
        [TestCase(14, 14, 16)]
        [TestCase(14, 15, 16)]
        [TestCase(14, 16, 17)]
        [TestCase(14, 17, 17)]
        [TestCase(14, 18, 18)]
        [TestCase(14, 19, 18)]
        [TestCase(14, 20, 19)]
        [TestCase(9266, 90210, 54366)]
        public void DC(int baseValue, int abilityValue, int dc)
        {
            save.BaseValue = baseValue;
            save.BaseAbility = new Ability("ability");
            save.BaseAbility.BaseScore = abilityValue;

            Assert.That(save.DC, Is.EqualTo(dc));
        }

        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(20)]
        [TestCase(9266)]
        public void DC_WithoutAbility(int baseValue)
        {
            save.BaseValue = baseValue;
            save.BaseAbility = null;

            Assert.That(save.DC, Is.EqualTo(baseValue));
        }
    }
}
