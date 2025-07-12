using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Defenses
{
    [TestFixture]
    public class HitPointsGeneratorTests
    {
        private Mock<Dice> mockDice;
        private IHitPointsGenerator hitPointsGenerator;

        private Ability constitution;
        private List<Feat> feats;
        private CreatureType creatureType;

        private const int Quantity = 9;
        private const int Die = 90210;

        [SetUp]
        public void Setup()
        {
            mockDice = new Mock<Dice>();
            hitPointsGenerator = new HitPointsGenerator(mockDice.Object);

            feats = [];
            constitution = new Ability(AbilityConstants.Constitution);
            creatureType = new CreatureType
            {
                Name = "creature type"
            };

            SetUpRolls(600.5, [9266, 42]);
        }

        private void SetUpRolls(double average, int[] rolls, int quantity = Quantity, int die = Die)
        {
            mockDice.Setup(d => d.Roll(quantity).d(die).AsIndividualRolls<int>()).Returns(rolls);
            mockDice.Setup(d => d.Roll(quantity).d(die).AsPotentialAverage()).Returns(average);
        }

        [Test]
        public void GetHitDie()
        {
            var hitPoints = hitPointsGenerator.GenerateFor(Quantity, Die, creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42));
        }

        [Test]
        public void ConstitutionBonusAppliedPerHitDie()
        {
            constitution.BaseScore = 20;
            SetUpRolls(2025, [9266, 42, 22, 2022, 227], 5);

            var hitPoints = hitPointsGenerator.GenerateFor(5, Die, creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo($"5d90210+25"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(2025 + 25));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(5));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(5));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42 + 22 + 2022 + 227 + 25));
        }

        [Test]
        public void ConstitutionPenaltyAppliedPerHitDie()
        {
            constitution.BaseScore = 2;
            SetUpRolls(2025, [9266, 42, 22, 2022, 227], 5);

            var hitPoints = hitPointsGenerator.GenerateFor(5, Die, creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo($"5d90210-20"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(2025 - 20));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(5));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(5));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42 + 22 + 2022 + 227 - 20));
        }

        [Test]
        public void NoConstitutionAppliedPerHitDie()
        {
            constitution.BaseScore = 0;

            var hitPoints = hitPointsGenerator.GenerateFor(Quantity, Die, creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.False);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerHitDie()
        {
            constitution.BaseScore = 1;
            SetUpRolls(13.37, [1, 3, 5], 3);

            var hitPoints = hitPointsGenerator.GenerateFor(3, Die, creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("3d90210-15"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(3));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(3));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(3));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(3));
        }

        [Test]
        public void MinimumCheckAppliedPerHitDieOnRoll()
        {
            constitution.BaseScore = 6;
            SetUpRolls(13.37, [1, 2, 4], 3);

            var hitPoints = hitPointsGenerator.GenerateFor(3, Die, creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("3d90210-6"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13 - 6));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(3));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(3));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.Not.EqualTo(1) //1 + 2 + 4 + 3 * -2 = 7 - 6
                .And.EqualTo(4)); //[1-2,1]+[2-2,1]+[4-2,1] = 1 + 1 + 2
        }

        [Test]
        public void AddAdditionalHitDice()
        {
            SetUpRolls(13.37, [42, 600, 1337, 1336], Quantity + 3);

            var hitPoints = hitPointsGenerator.GenerateFor(Quantity, Die, creatureType, constitution, "size", 3);
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("12d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(Quantity + 3));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(Quantity + 3));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(42 + 600 + 1337 + 1336));
        }

        [Test]
        public void ToughnessIncreassHitPoints()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            var hitPoints = hitPointsGenerator.GenerateFor(Quantity, Die, creatureType, constitution, "size");
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.EqualTo(3));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9d90210+3"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600 + 3));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42 + 3));
        }

        [Test]
        public void NotToughnessDoesNotIncreassHitPoints()
        {
            feats.Add(new Feat { Name = "Not " + FeatConstants.Toughness, Power = 3 });

            var hitPoints = hitPointsGenerator.GenerateFor(Quantity, Die, creatureType, constitution, "size");
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42));
        }

        [Test]
        public void ToughnessIncreasesHitPointsMultipleTimes()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            var hitPoints = hitPointsGenerator.GenerateFor(Quantity, Die, creatureType, constitution, "size");
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.EqualTo(6));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9d90210+6"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600 + 6));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42 + 6));
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

            SetUpRolls(13.37, [96, 783], die: 1336);

            var hitPoints = hitPointsGenerator.GenerateFor(Quantity, 1336, creatureType, constitution, size);
            Assert.That(hitPoints.Bonus, Is.EqualTo(bonusHitPoints));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);

            var roll = bonusHitPoints > 0 ? $"9d1336+{bonusHitPoints}" : "9d1336";
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo(roll));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13 + bonusHitPoints));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(1336));
            Assert.That(hitPoints.Total, Is.EqualTo(96 + 783 + bonusHitPoints));
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

            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            SetUpRolls(13.37, [96, 783], die: 1336);

            var hitPoints = hitPointsGenerator.GenerateFor(Quantity, 1336, creatureType, constitution, size);
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.EqualTo(bonusHitPoints + 3));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);

            var regeneratedRoll = $"9d1336+{bonusHitPoints + 3}";
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo(regeneratedRoll));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13 + bonusHitPoints + 3));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(1336));
            Assert.That(hitPoints.Total, Is.EqualTo(96 + 783 + bonusHitPoints + 3));
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
            var hitPoints = hitPointsGenerator.GenerateFor(Quantity, Die, creatureType, constitution, size);
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("9d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(9));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(42 + 9266));
        }
    }
}