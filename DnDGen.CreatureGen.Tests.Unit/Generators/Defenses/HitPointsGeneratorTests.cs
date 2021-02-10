using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tables;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Defenses
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

        private const int Die = 90210;
        private const string CreatureName = "creature";

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

            SetUpRolls(600.5, new[] { 9266, 42 });

            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, creatureType.Name)).Returns(Die);

            mockDice.Setup(d => d.Roll(It.IsAny<int>())).Returns((int q) => mockPartialRolls[q].Object);
        }

        private void SetUpRolls(double average, int[] rolls, int die = Die)
        {
            mockAdjustmentSelector.Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, CreatureName)).Returns(rolls.Length);

            if (!mockPartialRolls.ContainsKey(rolls.Length))
                mockPartialRolls[rolls.Length] = new Mock<PartialRoll>();

            var endRoll = new Mock<PartialRoll>();
            endRoll.Setup(r => r.AsIndividualRolls<int>()).Returns(rolls);
            endRoll.Setup(r => r.AsPotentialAverage()).Returns(average);

            mockPartialRolls[rolls.Length].Setup(r => r.d(die)).Returns(endRoll.Object);
        }

        [Test]
        public void GetHitDie()
        {
            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("2d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42));
        }

        [TestCase(CreatureConstants.Types.Aberration, 2)]
        [TestCase(CreatureConstants.Types.Animal, 2)]
        [TestCase(CreatureConstants.Types.Construct, 2)]
        [TestCase(CreatureConstants.Types.Dragon, 2)]
        [TestCase(CreatureConstants.Types.Elemental, 2)]
        [TestCase(CreatureConstants.Types.Fey, 2)]
        [TestCase(CreatureConstants.Types.Giant, 2)]
        [TestCase(CreatureConstants.Types.Humanoid, 1)]
        [TestCase(CreatureConstants.Types.MagicalBeast, 2)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, 2)]
        [TestCase(CreatureConstants.Types.Ooze, 2)]
        [TestCase(CreatureConstants.Types.Outsider, 2)]
        [TestCase(CreatureConstants.Types.Plant, 2)]
        [TestCase(CreatureConstants.Types.Undead, 2)]
        [TestCase(CreatureConstants.Types.Vermin, 2)]
        public void GetHitDieAsCharacter(string type, int quantity)
        {
            creatureType.Name = type;
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, type)).Returns(Die);

            var rolls = Enumerable.Range(9266, quantity).ToArray();
            SetUpRolls(600.5, rolls);
            mockAdjustmentSelector.Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, CreatureName)).Returns(2);

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, "size", asCharacter: true);
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo($"{quantity}d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(quantity));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(quantity));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + (quantity > 1 ? 9267 : 0)));
        }

        [TestCase(CreatureConstants.Types.Aberration, 1)]
        [TestCase(CreatureConstants.Types.Animal, 1)]
        [TestCase(CreatureConstants.Types.Construct, 1)]
        [TestCase(CreatureConstants.Types.Dragon, 1)]
        [TestCase(CreatureConstants.Types.Elemental, 1)]
        [TestCase(CreatureConstants.Types.Fey, 1)]
        [TestCase(CreatureConstants.Types.Giant, 1)]
        [TestCase(CreatureConstants.Types.Humanoid, 0)]
        [TestCase(CreatureConstants.Types.MagicalBeast, 1)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, 1)]
        [TestCase(CreatureConstants.Types.Ooze, 1)]
        [TestCase(CreatureConstants.Types.Outsider, 1)]
        [TestCase(CreatureConstants.Types.Plant, 1)]
        [TestCase(CreatureConstants.Types.Undead, 1)]
        [TestCase(CreatureConstants.Types.Vermin, 1)]
        public void GetHitDieAsCharacter_OnlyHas1HitDie(string type, int quantity)
        {
            creatureType.Name = type;
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, type)).Returns(Die);
            mockAdjustmentSelector.Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, CreatureName)).Returns(1);

            if (quantity > 0)
            {
                var rolls = Enumerable.Range(9266, quantity).ToArray();
                SetUpRolls(600.5, rolls);
            }

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, "size", asCharacter: true);

            if (quantity > 0)
            {
                Assert.That(hitPoints.Bonus, Is.Zero);
                Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
                Assert.That(hitPoints.Constitution.HasScore, Is.True);
                Assert.That(hitPoints.DefaultRoll, Is.EqualTo("1d90210"));
                Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
                Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(quantity));
                Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
                Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(quantity));
                Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
                Assert.That(hitPoints.Total, Is.EqualTo(9266));
            }
            else
            {
                Assert.That(hitPoints.Bonus, Is.Zero);
                Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
                Assert.That(hitPoints.Constitution.HasScore, Is.True);
                Assert.That(hitPoints.DefaultRoll, Is.EqualTo("0"));
                Assert.That(hitPoints.DefaultTotal, Is.Zero);
                Assert.That(hitPoints.HitDiceQuantity, Is.Zero);
                Assert.That(hitPoints.HitDice, Is.Empty);
                Assert.That(hitPoints.Total, Is.Zero);
            }
        }

        [Test]
        public void ConstitutionBonusAppliedPerHitDie()
        {
            constitution.BaseScore = 20;

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo($"2d90210+10"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600 + 10));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42 + 10));
        }

        [Test]
        public void ConstitutionPenaltyAppliedPerHitDie()
        {
            constitution.BaseScore = 2;

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo($"2d90210-8"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600 - 8));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42 - 8));
        }

        [Test]
        public void NoConstitutionAppliedPerHitDie()
        {
            constitution.BaseScore = 0;

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, "size");
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.False);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("2d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42));
        }

        [Test]
        public void CannotGainFewerThan1HitPointPerHitDie()
        {
            constitution.BaseScore = 1;
            SetUpRolls(13.37, new[] { 1, 3, 5 });

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, "size");
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
            SetUpRolls(13.37, new[] { 1, 2, 4 });

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, "size");
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
            SetUpRolls(13.37, new[] { 42, 600, 1337, 1336 });
            mockAdjustmentSelector.Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, CreatureName)).Returns(1);

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, "size", 3);
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo($"4d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(4));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(4));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(42 + 600 + 1337 + 1336));
        }

        [Test]
        public void ToughnessIncreassHitPoints()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, "size");
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.EqualTo(3));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("2d90210+3"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600 + 3));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42 + 3));
        }

        [Test]
        public void NotToughnessDoesNotIncreassHitPoints()
        {
            feats.Add(new Feat { Name = "Not " + FeatConstants.Toughness, Power = 3 });

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, "size");
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("2d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(9266 + 42));
        }

        [Test]
        public void ToughnessIncreasesHitPointsMultipleTimes()
        {
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });
            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, "size");
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.EqualTo(6));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("2d90210+6"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600 + 6));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(2));
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
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, CreatureConstants.Types.Construct)).Returns(1336);

            SetUpRolls(13.37, new[] { 96, 783 }, 1336);

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, size);
            Assert.That(hitPoints.Bonus, Is.EqualTo(bonusHitPoints));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);

            var roll = bonusHitPoints > 0 ? $"2d1336+{bonusHitPoints}" : "2d1336";
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo(roll));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13 + bonusHitPoints));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(2));
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
            mockAdjustmentSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.HitDice, CreatureConstants.Types.Construct)).Returns(1336);

            feats.Add(new Feat { Name = FeatConstants.Toughness, Power = 3 });

            SetUpRolls(13.37, new[] { 96, 783 }, 1336);

            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, size);
            hitPoints = hitPointsGenerator.RegenerateWith(hitPoints, feats);

            Assert.That(hitPoints.Bonus, Is.EqualTo(bonusHitPoints + 3));
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);

            var regeneratedRoll = $"2d1336+{bonusHitPoints + 3}";
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo(regeneratedRoll));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(13 + bonusHitPoints + 3));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(2));
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
            var hitPoints = hitPointsGenerator.GenerateFor(CreatureName, creatureType, constitution, size);
            Assert.That(hitPoints.Bonus, Is.Zero);
            Assert.That(hitPoints.Constitution, Is.EqualTo(constitution));
            Assert.That(hitPoints.Constitution.HasScore, Is.True);
            Assert.That(hitPoints.DefaultRoll, Is.EqualTo("2d90210"));
            Assert.That(hitPoints.DefaultTotal, Is.EqualTo(600));
            Assert.That(hitPoints.HitDiceQuantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(hitPoints.HitDice[0].Quantity, Is.EqualTo(2));
            Assert.That(hitPoints.HitDice[0].HitDie, Is.EqualTo(90210));
            Assert.That(hitPoints.Total, Is.EqualTo(42 + 9266));
        }
    }
}