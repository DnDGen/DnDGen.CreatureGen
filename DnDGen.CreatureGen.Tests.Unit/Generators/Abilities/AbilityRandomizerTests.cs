using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Generators.Abilities;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class AbilityRandomizerTests
    {
        private Mock<Dice> mockDice;
        private AbilityRandomizer abilityRandomizer;

        [SetUp]
        public void Setup()
        {
            mockDice = new();
            abilityRandomizer = new("my roll");
        }

        [Test]
        public void AbilityRandomizer_IsInitialized_Defaults()
        {
            abilityRandomizer = new();
            Assert.That(abilityRandomizer.Roll, Is.EqualTo(AbilityConstants.RandomizerRolls.Default));
            Assert.That(abilityRandomizer.PriorityAbility, Is.Null);
            Assert.That(abilityRandomizer.AbilityAdvancements, Is.Not.Null.And.Empty);
            Assert.That(abilityRandomizer.SetRolls, Is.Not.Null.And.Empty);
        }

        [Test]
        public void AbilityRandomizer_IsInitialized_Roll()
        {
            abilityRandomizer = new("my initialized roll");
            Assert.That(abilityRandomizer.Roll, Is.EqualTo("my initialized roll"));
            Assert.That(abilityRandomizer.PriorityAbility, Is.Null);
            Assert.That(abilityRandomizer.AbilityAdvancements, Is.Not.Null.And.Empty);
            Assert.That(abilityRandomizer.SetRolls, Is.Not.Null.And.Empty);
        }

        [Test]
        public void Randomize_ReturnsRoll()
        {
            abilityRandomizer.SetRolls["other ability"] = 666;

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsSum<int>()).Returns(9266);

            var roll = abilityRandomizer.Randomize("my ability", mockDice.Object);
            Assert.That(roll, Is.EqualTo(9266));
        }

        [Test]
        public void Randomize_ReturnsSetValue()
        {
            abilityRandomizer.SetRolls["other ability"] = 666;
            abilityRandomizer.SetRolls["my ability"] = 90210;

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsSum<int>()).Returns(9266);

            var roll = abilityRandomizer.Randomize("my ability", mockDice.Object);
            Assert.That(roll, Is.EqualTo(90210));

            mockDice.Verify(d => d.Roll(It.IsAny<string>()), Times.Never);
        }

        [TestCase(9266, 9266)]
        [TestCase(9266 + 1, 9266)]
        [TestCase(9266 + 2, 9266)]
        [TestCase(90210, 9266)]
        public void Validate_ReturnsValid_FromRoll(int maxRoll, int minimum)
        {
            abilityRandomizer.SetRolls["other ability"] = 666;

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(maxRoll);

            var valid = abilityRandomizer.Validate("my ability", mockDice.Object, minimum);
            Assert.That(valid, Is.True);
        }

        [TestCase(42, 600)]
        [TestCase(42, -600)]
        [TestCase(-42, 600)]
        [TestCase(-42, -600)]
        public void Validate_ReturnsValid_FromRoll_WithAdjustments(int adjust1, int adjust2)
        {
            abilityRandomizer.SetRolls["other ability"] = 666;

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(9266 - (adjust1 + adjust2));

            var valid = abilityRandomizer.Validate("my ability", mockDice.Object, 9266, adjust1, adjust2);
            Assert.That(valid, Is.True);
        }

        [TestCase(9266, 9266)]
        [TestCase(9266 + 1, 9266)]
        [TestCase(9266 + 2, 9266)]
        [TestCase(90210, 9266)]
        public void Validate_ReturnsValid_FromSet(int setValue, int minimum)
        {
            abilityRandomizer.SetRolls["other ability"] = 666;
            abilityRandomizer.SetRolls["my ability"] = setValue;

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(int.MinValue);

            var valid = abilityRandomizer.Validate("my ability", mockDice.Object, minimum);
            Assert.That(valid, Is.True);

            mockDice.Verify(d => d.Roll(It.IsAny<string>()), Times.Never);
        }

        [TestCase(42, 600)]
        [TestCase(42, -600)]
        [TestCase(-42, 600)]
        [TestCase(-42, -600)]
        public void Validate_ReturnsValid_FromSet_WithAdjustments(int adjust1, int adjust2)
        {
            abilityRandomizer.SetRolls["other ability"] = 666;
            abilityRandomizer.SetRolls["my ability"] = 9266 - (adjust1 + adjust2);

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(int.MinValue);

            var valid = abilityRandomizer.Validate("my ability", mockDice.Object, 9266, adjust1, adjust2);
            Assert.That(valid, Is.True);

            mockDice.Verify(d => d.Roll(It.IsAny<string>()), Times.Never);
        }

        [TestCase(1337, 9266)]
        [TestCase(9266 - 2, 9266)]
        [TestCase(9266 - 1, 9266)]
        public void Validate_ReturnsInvalid_FromRoll(int maxRoll, int minimum)
        {
            abilityRandomizer.SetRolls["other ability"] = 666;

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(maxRoll);

            var valid = abilityRandomizer.Validate("my ability", mockDice.Object, minimum);
            Assert.That(valid, Is.False);
        }

        [TestCase(42, 600)]
        [TestCase(42, -600)]
        [TestCase(-42, 600)]
        [TestCase(-42, -600)]
        public void Validate_ReturnsInvalid_FromRoll_WithAdjustments(int adjust1, int adjust2)
        {
            abilityRandomizer.SetRolls["other ability"] = 666;

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(9266 - 1 - (adjust1 + adjust2));

            var valid = abilityRandomizer.Validate("my ability", mockDice.Object, 9266, adjust1, adjust2);
            Assert.That(valid, Is.False);
        }

        [TestCase(1337, 9266)]
        [TestCase(9266 - 2, 9266)]
        [TestCase(9266 - 1, 9266)]
        public void Validate_ReturnsInvalid_FromSet(int setValue, int minimum)
        {
            abilityRandomizer.SetRolls["other ability"] = 666;
            abilityRandomizer.SetRolls["my ability"] = setValue;

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(int.MaxValue);

            var valid = abilityRandomizer.Validate("my ability", mockDice.Object, minimum);
            Assert.That(valid, Is.False);

            mockDice.Verify(d => d.Roll(It.IsAny<string>()), Times.Never);
        }

        [TestCase(42, 600)]
        [TestCase(42, -600)]
        [TestCase(-42, 600)]
        [TestCase(-42, -600)]
        public void Validate_ReturnsInvalid_FromSet_WithAdjustments(int adjust1, int adjust2)
        {
            abilityRandomizer.SetRolls["other ability"] = 666;
            abilityRandomizer.SetRolls["my ability"] = 9266 - 1 - (adjust1 + adjust2);

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(int.MaxValue);

            var valid = abilityRandomizer.Validate("my ability", mockDice.Object, 9266, adjust1, adjust2);
            Assert.That(valid, Is.False);

            mockDice.Verify(d => d.Roll(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void Validate_ReturnsValidity_FromRoll_CachesComputedMax()
        {
            abilityRandomizer.SetRolls["other ability"] = 666;

            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(90210);

            var valid = abilityRandomizer.Validate("my ability", mockDice.Object, 9266, 42);
            Assert.That(valid, Is.True);

            valid = abilityRandomizer.Validate("my ability", mockDice.Object, 9266, -90210, 600);
            Assert.That(valid, Is.False);

            valid = abilityRandomizer.Validate("my ability", mockDice.Object, 9266, 1337, -1336);
            Assert.That(valid, Is.True);

            mockDice.Verify(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true), Times.Once);
        }

        [TestCase(9266, 42, -10, 9266 - 32)]
        [TestCase(9266, 42, -2, 9266 - 40)]
        [TestCase(9266, 42, -1, 9266 - 41)]
        [TestCase(9266, 42, 0, 9266 - 42)]
        [TestCase(9266, 42, 1, 9266 - 43)]
        [TestCase(9266, 42, 2, 9266 - 44)]
        [TestCase(9266, 42, 10, 9266 - 52)]
        [TestCase(9266, 9256, -10, 20)]
        [TestCase(9266, 9256, -2, 12)]
        [TestCase(9266, 9256, -1, 11)]
        [TestCase(9266, 9256, 0, 10)]
        [TestCase(9266, 9256, 1, 9)]
        [TestCase(9266, 9256, 2, 8)]
        [TestCase(9266, 9256, 10, 0)]
        [TestCase(9266, 9264, -10, 12)]
        [TestCase(9266, 9264, -2, 4)]
        [TestCase(9266, 9264, -1, 3)]
        [TestCase(9266, 9264, 0, 2)]
        [TestCase(9266, 9264, 1, 1)]
        [TestCase(9266, 9264, 2, 0)]
        [TestCase(9266, 9264, 10, 0)]
        [TestCase(9266, 9265, -10, 11)]
        [TestCase(9266, 9265, -2, 3)]
        [TestCase(9266, 9265, -1, 2)]
        [TestCase(9266, 9265, 0, 1)]
        [TestCase(9266, 9265, 1, 0)]
        [TestCase(9266, 9265, 2, 0)]
        [TestCase(9266, 9265, 10, 0)]
        [TestCase(9266, 9266, -10, 10)]
        [TestCase(9266, 9266, -2, 2)]
        [TestCase(9266, 9266, -1, 1)]
        [TestCase(9266, 9266, 0, 0)]
        [TestCase(9266, 9266, 1, 0)]
        [TestCase(9266, 9266, 2, 0)]
        [TestCase(9266, 9266, 10, 0)]
        [TestCase(9266, 9267, -10, 9)]
        [TestCase(9266, 9267, -2, 1)]
        [TestCase(9266, 9267, -1, 0)]
        [TestCase(9266, 9267, 0, 0)]
        [TestCase(9266, 9267, 1, 0)]
        [TestCase(9266, 9267, 2, 0)]
        [TestCase(9266, 9267, 10, 0)]
        [TestCase(9266, 9268, -10, 8)]
        [TestCase(9266, 9268, -2, 0)]
        [TestCase(9266, 9268, -1, 0)]
        [TestCase(9266, 9268, 0, 0)]
        [TestCase(9266, 9268, 1, 0)]
        [TestCase(9266, 9268, 2, 0)]
        [TestCase(9266, 9268, 10, 0)]
        [TestCase(9266, 9276, -10, 0)]
        [TestCase(9266, 9276, -2, 0)]
        [TestCase(9266, 9276, -1, 0)]
        [TestCase(9266, 9276, 0, 0)]
        [TestCase(9266, 9276, 1, 0)]
        [TestCase(9266, 9276, 2, 0)]
        [TestCase(9266, 9276, 10, 0)]
        [TestCase(9266, 90210, -10, 0)]
        [TestCase(9266, 90210, -2, 0)]
        [TestCase(9266, 90210, -1, 0)]
        [TestCase(9266, 90210, 0, 0)]
        [TestCase(9266, 90210, 1, 0)]
        [TestCase(9266, 90210, 2, 0)]
        [TestCase(9266, 90210, 10, 0)]
        public void GetAdjustment_ReturnsAdjustment_FromRoll(int minimum, int baseScore, int racial, int expected)
        {
            var ability = new Ability("my ability") { BaseScore = baseScore, RacialAdjustment = racial };
            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(int.MaxValue);

            var adjustment = abilityRandomizer.GetAdjustment(mockDice.Object, ability, minimum);
            Assert.That(adjustment, Is.EqualTo(expected));
        }

        [Test]
        public void BUG_GetAdjustment_ReturnsAdjustment_FromRoll_VeryLow()
        {
            var ability = new Ability("my ability") { BaseScore = 5, RacialAdjustment = -6 };
            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(18);

            var adjustment = abilityRandomizer.GetAdjustment(mockDice.Object, ability, 4);
            Assert.That(adjustment, Is.EqualTo(5));
        }

        [Test]
        public void GetAdjustment_ReturnsAdjustment_FromSet()
        {
            abilityRandomizer.SetRolls["other ability"] = 666;
            abilityRandomizer.SetRolls["my ability"] = 9266;

            var ability = new Ability("my ability") { BaseScore = 9266, RacialAdjustment = 42 };
            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(int.MaxValue);

            var adjustment = abilityRandomizer.GetAdjustment(mockDice.Object, ability, 90210);
            Assert.That(adjustment, Is.Zero);

            mockDice.Verify(d => d.Roll(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void GetAdjustment_ThrowsException_WhenAdjustmentExceedsMaximumAllowed()
        {
            var ability = new Ability("my ability") { BaseScore = 600, RacialAdjustment = 42 };
            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(9266);

            var action = () => abilityRandomizer.GetAdjustment(mockDice.Object, ability, 90210);
            Assert.That(action, Throws.InvalidOperationException.With.Message.EqualTo($"Cannot increase ability my ability by {90210 - 642}, max allowed is {9266 - 600}"));
        }

        [Test]
        public void GetAdjustment_ThrowsException_WhenAdjustmentExceedsMaximumAllowed_VeryLow()
        {
            var ability = new Ability("my ability") { BaseScore = 3, RacialAdjustment = -6 };
            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(9);

            var action = () => abilityRandomizer.GetAdjustment(mockDice.Object, ability, 4);
            Assert.That(action, Throws.InvalidOperationException.With.Message.EqualTo("Cannot increase ability my ability by 7, max allowed is 6"));
        }

        [TestCase(-9266, false)]
        [TestCase(-2, false)]
        [TestCase(-1, false)]
        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(9, true)]
        [TestCase(10, true)]
        [TestCase(11, true)]
        [TestCase(18, true)]
        [TestCase(9266, true)]
        public void Validate_BasedOnMaxRoll(int maxRoll, bool expected)
        {
            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(maxRoll);

            var valid = abilityRandomizer.Validate(mockDice.Object);
            Assert.That(valid, Is.EqualTo(expected));
        }

        [TestCase(-9266, false)]
        [TestCase(-2, false)]
        [TestCase(-1, false)]
        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(9, true)]
        [TestCase(10, true)]
        [TestCase(11, true)]
        [TestCase(18, true)]
        [TestCase(9266, true)]
        public void Validate_BasedOnSetValue(int setValue, bool expected)
        {
            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(11);

            abilityRandomizer.SetRolls[AbilityConstants.Strength] = setValue;
            abilityRandomizer.SetRolls[AbilityConstants.Constitution] = 90210;
            abilityRandomizer.SetRolls[AbilityConstants.Dexterity] = 42;
            abilityRandomizer.SetRolls[AbilityConstants.Intelligence] = 600;
            abilityRandomizer.SetRolls[AbilityConstants.Wisdom] = 1337;
            abilityRandomizer.SetRolls[AbilityConstants.Charisma] = 1336;

            var valid = abilityRandomizer.Validate(mockDice.Object);
            Assert.That(valid, Is.EqualTo(expected));
        }

        [TestCase(AbilityConstants.Strength, true)]
        [TestCase(AbilityConstants.Strength, false)]
        [TestCase(AbilityConstants.Constitution, true)]
        [TestCase(AbilityConstants.Constitution, false)]
        [TestCase(AbilityConstants.Dexterity, true)]
        [TestCase(AbilityConstants.Dexterity, false)]
        [TestCase(AbilityConstants.Intelligence, true)]
        [TestCase(AbilityConstants.Intelligence, false)]
        [TestCase(AbilityConstants.Wisdom, true)]
        [TestCase(AbilityConstants.Wisdom, false)]
        [TestCase(AbilityConstants.Charisma, true)]
        [TestCase(AbilityConstants.Charisma, false)]
        public void Validate_BasedOnAnySetValue(string ability, bool expected)
        {
            mockDice.Setup(d => d.Roll(abilityRandomizer.Roll).AsPotentialMaximum<int>(true)).Returns(11);

            abilityRandomizer.SetRolls[AbilityConstants.Strength] = 9266;
            abilityRandomizer.SetRolls[AbilityConstants.Constitution] = 90210;
            abilityRandomizer.SetRolls[AbilityConstants.Dexterity] = 42;
            abilityRandomizer.SetRolls[AbilityConstants.Intelligence] = 600;
            abilityRandomizer.SetRolls[AbilityConstants.Wisdom] = 1337;
            abilityRandomizer.SetRolls[AbilityConstants.Charisma] = 1336;

            if (!expected)
                abilityRandomizer.SetRolls[ability] = -666;

            var valid = abilityRandomizer.Validate(mockDice.Object);
            Assert.That(valid, Is.EqualTo(expected));
        }
    }
}
