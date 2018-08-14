using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Feats;
using CreatureGen.Generators.Defenses;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Defenses
{
    [TestFixture]
    public class HitPointsGeneratorTests
    {
        private Mock<Dice> mockDice;
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;
        private IHitPointsGenerator hitPointsGenerator;

        private Ability constitution;
        private List<Feat> feats;
        private Dictionary<int, Mock<PartialRoll>> mockPartialRolls;
        private CreatureType creatureType;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();
            hitPointsGenerator = new HitPointsGenerator(mockDice.Object, mockAdjustmentSelector.Object);

            mockPartialRolls = new Dictionary<int, Mock<PartialRoll>>();
            feats = new List<Feat>();
            constitution = new Ability(AbilityConstants.Constitution);
            creatureType = new CreatureType();

            creatureType.Name = "creature type";

            SetUpRoll(9266, 90210, 42);
            SetUpAverageRoll("9266d90210", 600.5);

            mockAdjustmentSelector.Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "creature")).Returns(9266);
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, creatureType.Name)).Returns(90210);

            mockDice.Setup(d => d.Roll(It.IsAny<int>())).Returns((int q) => mockPartialRolls[q].Object);
        }

        private void SetUpRoll(int quantity, int die, params int[] rolls)
        {
            if (!mockPartialRolls.ContainsKey(quantity))
                mockPartialRolls[quantity] = new Mock<PartialRoll>();

            var endRoll = new Mock<PartialRoll>();
            endRoll.Setup(r => r.AsIndividualRolls()).Returns(rolls);

            mockPartialRolls[quantity].Setup(r => r.d(die)).Returns(endRoll.Object);
        }

        private void SetUpAverageRoll(string roll, double value)
        {
            var endRoll = new Mock<PartialRoll>();
            endRoll.Setup(r => r.AsPotentialAverage()).Returns(value);

            mockDice.Setup(d => d.Roll(roll)).Returns(endRoll.Object);
        }

        [Test]
        public void GetHitDie()
        {
            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public void ConstitutionBonusAppliedPerHitDie()
        {
            constitution.BaseScore = 20;
            SetUpRoll(9266, 90210, 42, 600);
            SetUpAverageRoll("9266d90210+46330", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210+46330"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(600 + 42 + 2 * 5));
        }

        [Test]
        public void ConstitutionPenaltyAppliedPerHitDie()
        {
            constitution.BaseScore = 2;
            SetUpRoll(9266, 90210, 42, 600);
            SetUpAverageRoll("9266d90210-37064", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210-37064"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(600 + 42 + 2 * -4));
        }

        [Test]
        public void NoConstitutionAppliedPerHitDie()
        {
            SetUpRoll(9266, 90210, 42, 600);
            constitution.BaseScore = 0;

            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.False);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(600 + 42));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerHitDie()
        {
            constitution.BaseScore = 1;
            SetUpRoll(9266, 90210, 1, 3, 5);
            SetUpAverageRoll("9266d90210-46330", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210-46330"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(3));
        }

        [Test]
        public void MinimumCheckAppliedPerHitDieOnRoll()
        {
            constitution.BaseScore = 6;
            SetUpRoll(9266, 90210, 1, 2, 4);
            SetUpAverageRoll("9266d90210-18532", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210-18532"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.Not.EqualTo(1)); //1 + 2 + 4 + 3 * -2 = 7 - 6
            Assert.That(hitPoints.Total, Is.EqualTo(4)); //[1-2,1]+[2-2,1]+[4-2,1] = 1 + 1 + 2
        }

        [Test]
        public void AddAdditionalHitDice()
        {
            SetUpRoll(9266 + 1337, 90210, 1336);
            SetUpAverageRoll($"{9266 + 1337}d90210", 9.6);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, "size", 1337);
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo($"{9266 + 1337}d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(9));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266 + 1337));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(1336));
        }

        [Test]
        public void ToughnessIncreassHitPoints()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            SetUpAverageRoll("9266d90210+3", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, "size");
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.EqualTo(3));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210+3"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(45));
        }

        [Test]
        public void NotToughnessDoesNotIncreassHitPoints()
        {
            feats.Add(new Feat { Name = "Not " + FeatConstants.Toughness, Power = 3 });

            SetUpAverageRoll("9266d90210+3", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, "size");
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(42));
        }

        [Test]
        public void ToughnessIncreasesHitPointsMultipleTimes()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            SetUpAverageRoll("9266d90210+6", 13.37);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, "size");
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.EqualTo(6));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210+6"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(48));
        }

        [TestCase(SizeConstants.Fine, 0)]
        [TestCase(SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Small, 10)]
        [TestCase(SizeConstants.Medium, 20)]
        [TestCase(SizeConstants.Large, 30)]
        [TestCase(SizeConstants.Huge, 40)]
        [TestCase(SizeConstants.Gargantuan, 60)]
        [TestCase(SizeConstants.Colossal, 80)]
        public void ConstructsGetBonusHitPointsBasedOnSize(string size, int bonusHitPoints)
        {
            creatureType.Name = CreatureConstants.Types.Construct;
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, CreatureConstants.Types.Construct)).Returns(1336);

            var roll = bonusHitPoints > 0 ? $"9266d1336+{bonusHitPoints}" : "9266d1336";
            SetUpAverageRoll(roll, 13.37);
            SetUpRoll(9266, 1336, 96);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, size);
            Assert.That(hitPoints.Bonus, Is.EqualTo(bonusHitPoints));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo(roll));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(1336));
            Assert.That(hitPoints.Total, Is.EqualTo(96 + bonusHitPoints));
        }

        [TestCase(SizeConstants.Fine, 0)]
        [TestCase(SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Small, 10)]
        [TestCase(SizeConstants.Medium, 20)]
        [TestCase(SizeConstants.Large, 30)]
        [TestCase(SizeConstants.Huge, 40)]
        [TestCase(SizeConstants.Gargantuan, 60)]
        [TestCase(SizeConstants.Colossal, 80)]
        public void ConstructsGetBonusHitPointsBasedOnSizeWithBonusFeats(string size, int bonusHitPoints)
        {
            creatureType.Name = CreatureConstants.Types.Construct;
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, CreatureConstants.Types.Construct)).Returns(1336);

            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            var roll = bonusHitPoints > 0 ? $"9266d1336+{bonusHitPoints}" : "9266d1336";
            var regeneratedRoll = $"9266d1336+{bonusHitPoints + 3}";
            SetUpAverageRoll(roll, 13.37);
            SetUpAverageRoll(regeneratedRoll, 78.3);
            SetUpRoll(9266, 1336, 96);

            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, size);
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.EqualTo(bonusHitPoints + 3));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo(regeneratedRoll));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(78));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(1336));
            Assert.That(hitPoints.Total, Is.EqualTo(96 + bonusHitPoints + 3));
        }

        [TestCase(SizeConstants.Fine)]
        [TestCase(SizeConstants.Diminutive)]
        [TestCase(SizeConstants.Tiny)]
        [TestCase(SizeConstants.Small)]
        [TestCase(SizeConstants.Medium)]
        [TestCase(SizeConstants.Large)]
        [TestCase(SizeConstants.Huge)]
        [TestCase(SizeConstants.Gargantuan)]
        [TestCase(SizeConstants.Colossal)]
        public void NonConstructsDoNotGetBonusHitPointsBasedOnSize(string size)
        {
            var hitPoints = hitPointsGenerator.GenerateFor("creature", creatureType, constitution, size);
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9266d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9266));
            Assert.That(hitPoints.HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(42));
        }
    }
}