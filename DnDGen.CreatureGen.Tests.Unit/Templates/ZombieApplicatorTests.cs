using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class ZombieApplicatorTests
    {
        private TemplateApplicator applicator;
        private Creature baseCreature;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<Dice> mockDice;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ISavesGenerator> mockSavesGenerator;
        private Attack[] zombieAttacks;
        private IEnumerable<Feat> zombieQualities;
        private int zombieBaseAttack;
        private Mock<IHitPointsGenerator> mockHitPointsGenerator;
        private Mock<IDemographicsGenerator> mockDemographicsGenerator;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockDice = new Mock<Dice>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockSavesGenerator = new Mock<ISavesGenerator>();
            mockHitPointsGenerator = new Mock<IHitPointsGenerator>();
            mockDemographicsGenerator = new Mock<IDemographicsGenerator>();

            applicator = new ZombieApplicator(
                mockCollectionSelector.Object,
                mockDice.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSavesGenerator.Object,
                mockHitPointsGenerator.Object,
                mockDemographicsGenerator.Object);

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .WithHitDiceQuantityNoMoreThan(10)
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .Build();

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity * 2)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity * 2)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            zombieQualities =
            [
                new Feat { Name = "zombie quality 1" },
                new Feat { Name = "zombie quality 2" },
                new Feat { Name = FeatConstants.Toughness, Power = 600 },
            ];

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(zombieQualities);

            zombieBaseAttack = 42;

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    BaseAttackQuality.Poor,
                    baseCreature.HitPoints))
                .Returns(zombieBaseAttack);

            zombieAttacks =
            [
                new Attack
                {
                    Name = "Slam",
                    Damages =
                    [
                        new() { Roll = "zombie damage roll", Type = "zombie damage type" }
                    ],
                    Frequency = new Frequency
                    {
                        Quantity = 1,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            ];

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    zombieBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2,
                    baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    zombieAttacks,
                    It.Is<IEnumerable<Feat>>(f =>
                        f.IsEquivalentTo(baseCreature.SpecialQualities
                            .Union(zombieQualities))),
                    baseCreature.Abilities))
                .Returns(zombieAttacks);

            mockHitPointsGenerator
                .Setup(g => g.RegenerateWith(
                    baseCreature.HitPoints,
                    It.Is<IEnumerable<Feat>>(f =>
                        f.IsEquivalentTo(baseCreature.SpecialQualities
                            .Union(zombieQualities)))))
                .Returns(baseCreature.HitPoints);

            baseCreature.HasSkeleton = true;

            mockDemographicsGenerator
                .Setup(s => s.UpdateByTemplate(baseCreature.Demographics, baseCreature.Name, CreatureConstants.Templates.Zombie, false, false))
                .Returns(baseCreature.Demographics);
        }

        [Test]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Outsider;

            var expected = new InvalidCreatureException("Type 'Outsider' is not valid", false, baseCreature.Name, templates: [CreatureConstants.Templates.Zombie]);
            var function = () => applicator.ApplyTo(baseCreature, false);
            Assert.That(function, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(expected.Message));
        }

        [TestCase("subtype 1", ChallengeRatingConstants.CR1_3rd, AlignmentConstants.TrueNeutral)]
        [TestCase("subtype 1", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralEvil)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1_3rd, AlignmentConstants.NeutralEvil)]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible_WithFilters(string type, string challengeRating, string alignment)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");

            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [challengeRating],
                Alignments = [alignment]
            };
            var (Compatible, Reason) = filters.AreCompatible(
                [AlignmentConstants.NeutralEvil],
                [ChallengeRatingConstants.CR1_2nd],
                [CreatureConstants.Types.Undead, "subtype 1", "subtype 2"]);

            var expected = new InvalidCreatureException(
                Reason,
                false,
                baseCreature.Name,
                filters,
                null,
                CreatureConstants.Templates.Zombie);

            var func = () => applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(func, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(expected.Message));
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithOtherTemplate()
        {
            baseCreature.Templates.Add("my other template");

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.Zombie));
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity * 2)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity * 2)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    zombieBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2,
                    baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var filters = new Filters
            {
                Types = ["subtype 1"],
                ChallengeRatings = [ChallengeRatingConstants.CR1_2nd],
                Alignments = [AlignmentConstants.NeutralEvil]
            };

            var creature = applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Zombie));
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void ApplyTo_ChangeCreatureType(string original)
        {
            baseCreature.Type.Name = original;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void ApplyTo_KeepSubtype(string subtype)
        {
            var subtypes = new[]
            {
                "subtype 1",
                subtype,
                "subtype 2",
            };
            baseCreature.Type.SubTypes = subtypes;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.SubTypes.ToArray(), Is.EqualTo(subtypes)
                .And.Contains(subtype)
                .And.Length.EqualTo(3));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Angel)]
        [TestCase(CreatureConstants.Types.Subtypes.Archon)]
        [TestCase(CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Types.Subtypes.Dwarf)]
        [TestCase(CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Types.Subtypes.Evil)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnoll)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnome)]
        [TestCase(CreatureConstants.Types.Subtypes.Goblinoid)]
        [TestCase(CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Types.Subtypes.Halfling)]
        [TestCase(CreatureConstants.Types.Subtypes.Human)]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Types.Subtypes.Orc)]
        [TestCase(CreatureConstants.Types.Subtypes.Reptilian)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger)]
        public void ApplyTo_LoseSubtype(string subtype)
        {
            var subtypes = new[]
            {
                "subtype 1",
                subtype,
                "subtype 2",
            };
            baseCreature.Type.SubTypes = subtypes;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.SubTypes.ToArray(), Is.EqualTo(subtypes.Except([subtype]))
                .And.Not.Contains(subtype)
                .And.Length.EqualTo(2));
        }

        [Test]
        public void ApplyTo_DemographicsAdjusted()
        {
            var zombieDemographics = new Demographics
            {
                Skin = "rotting",
                Other = "hungry for brains",
                Gender = "decaying gender",
            };
            mockDemographicsGenerator
                .Setup(s => s.UpdateByTemplate(baseCreature.Demographics, baseCreature.Name, CreatureConstants.Templates.Zombie, false, false))
                .Returns(zombieDemographics);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    zombieBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2,
                    zombieDemographics.Gender))
                .Returns(zombieAttacks);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(zombieDemographics));
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        public void ApplyTo_HitDiceBecomeD12(int hitDie)
        {
            baseCreature.HitPoints.HitDice[0].HitDie = hitDie;
            var newQuantity = baseCreature.HitPoints.RoundedHitDiceQuantity * 2;

            mockDice
                .Setup(d => d
                    .Roll(newQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([600, 1337]);
            mockDice
                .Setup(d => d
                    .Roll(newQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(newQuantity));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(600 + 1337));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(1336));
        }

        [TestCase(0.5)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public void ApplyTo_HitDiceQuantityDoubles(double quantity)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = quantity;
            var newRounded = Convert.ToInt32(Math.Max(1, quantity * 2));

            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([96, 1337]);
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    newRounded, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(quantity * 2));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(quantity * 2));
            Assert.That(creature.HitPoints.HitDice[0].RoundedQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(96 + 1337));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(1336));
        }

        [TestCase(0.1)]
        [TestCase(0.25)]
        public void ApplyTo_HitDiceQuantityDoubles_Fractional(double quantity)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = quantity;
            var newRounded = Convert.ToInt32(Math.Max(1, quantity * 2));

            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([96, 1337]);
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    newRounded, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(quantity * 2));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(quantity * 2));
            Assert.That(creature.HitPoints.HitDice[0].RoundedQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(Math.Floor((96 + 1337) * quantity * 2)));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(Math.Floor(1336 * quantity * 2)));
        }

        [TestCase(0.5)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public async Task ApplyToAsync_HitDiceQuantityDoubles(double quantity)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = quantity;
            var newRounded = Convert.ToInt32(Math.Max(1, quantity * 2));

            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([96, 1337]);
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    newRounded, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(quantity * 2));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(quantity * 2));
            Assert.That(creature.HitPoints.HitDice[0].RoundedQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(96 + 1337));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(1336));
        }

        [TestCase(0.1)]
        [TestCase(0.25)]
        public async Task ApplyToAsync_HitDiceQuantityDoubles_Fractional(double quantity)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = quantity;
            var newRounded = Convert.ToInt32(Math.Max(1, quantity * 2));

            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([96, 1337]);
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    newRounded, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(quantity * 2));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(quantity * 2));
            Assert.That(creature.HitPoints.HitDice[0].RoundedQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(Math.Floor((96 + 1337) * quantity * 2)));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(Math.Floor(1336 * quantity * 2)));
        }

        [Test]
        public void ApplyTo_AerialManeuverability_BecomesClumsy()
        {
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs")
            {
                Value = 1337,
                Description = "Superb Maneuverability (hoverboard)"
            };

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Is.Not.Empty
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(1337));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("Clumsy Maneuverability (hoverboard)"));
        }

        [TestCase(SizeConstants.Fine, 0)]
        [TestCase(SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Small, 1)]
        [TestCase(SizeConstants.Medium, 2)]
        [TestCase(SizeConstants.Large, 3)]
        [TestCase(SizeConstants.Huge, 4)]
        [TestCase(SizeConstants.Gargantuan, 7)]
        [TestCase(SizeConstants.Colossal, 11)]
        public void ApplyTo_GainsNaturalArmorBasedOnSize(string size, int bonus)
        {
            baseCreature.Size = size;

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(zombieQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(bonus));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

            var naturalArmor = creature.ArmorClass.NaturalArmorBonuses.Single();
            Assert.That(naturalArmor.Condition, Is.Empty);
            Assert.That(naturalArmor.IsConditional, Is.False);
            Assert.That(naturalArmor.Value, Is.EqualTo(bonus));
        }

        [TestCase(SizeConstants.Fine, 0)]
        [TestCase(SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Small, 1)]
        [TestCase(SizeConstants.Medium, 2)]
        [TestCase(SizeConstants.Large, 3)]
        [TestCase(SizeConstants.Huge, 4)]
        [TestCase(SizeConstants.Gargantuan, 7)]
        [TestCase(SizeConstants.Colossal, 11)]
        public void ApplyTo_ReplacesNaturalArmorBasedOnSize(string size, int bonus)
        {
            baseCreature.Size = size;
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 666);

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(zombieQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(bonus));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

            var naturalArmor = creature.ArmorClass.NaturalArmorBonuses.Single();
            Assert.That(naturalArmor.Condition, Is.Empty);
            Assert.That(naturalArmor.IsConditional, Is.False);
            Assert.That(naturalArmor.Value, Is.EqualTo(bonus));
        }

        [Test]
        public void ApplyTo_BaseAttackBonus()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(zombieBaseAttack));
        }

        [TestCase(SizeConstants.Fine, "1")]
        [TestCase(SizeConstants.Diminutive, "1")]
        [TestCase(SizeConstants.Tiny, "1d2")]
        [TestCase(SizeConstants.Small, "1d3")]
        [TestCase(SizeConstants.Medium, "1d4")]
        [TestCase(SizeConstants.Large, "1d6")]
        [TestCase(SizeConstants.Huge, "1d8")]
        [TestCase(SizeConstants.Gargantuan, "2d6")]
        [TestCase(SizeConstants.Colossal, "2d8")]
        public void ApplyTo_ReplacesSlamAttack_DamageBasedOnSize(string size, string damage)
        {
            baseCreature.Size = size;
            baseCreature.Attacks = baseCreature.Attacks.Union(
            [
                new Attack
                {
                    Name = "Slam",
                    Damages =
                    [
                        new() { Roll = "damage roll", Type = "damage type" }
                    ],
                    Frequency = new Frequency
                    {
                        Quantity = 2,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            ]);

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(zombieQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            zombieAttacks[0].Damages[0].Roll = damage;

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(1336);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(1337);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.Not.EqualTo(zombieAttacks[0]));
            Assert.That(slam.DamageSummary, Is.EqualTo($"{damage} zombie damage type"));
            Assert.That(slam.Frequency.Quantity, Is.EqualTo(2));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public void ApplyTo_GainSlamAttacks_OnlyOne(int numberOfHands)
        {
            baseCreature.NumberOfHands = numberOfHands;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.EqualTo(zombieAttacks[0]));
            Assert.That(slam.Frequency.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void ApplyTo_GainSlamAttacks_WithAttackBonuses()
        {
            baseCreature.NumberOfHands = 2;

            var attacksWithBonuses = new[]
            {
                new Attack
                {
                    Name = "Slam",
                    Damages =
                    [
                        new() { Roll = "zombie slam roll", Type = "zombie slam type" }
                    ],
                    Frequency = new Frequency { Quantity = 1, TimePeriod = FeatConstants.Frequencies.Round },
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = [92]
                },
            };

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    zombieAttacks,
                    It.Is<IEnumerable<Feat>>(f =>
                        f.IsEquivalentTo(baseCreature.Feats
                            .Union(baseCreature.SpecialQualities)
                            .Union(zombieQualities))),
                    baseCreature.Abilities))
                .Returns(attacksWithBonuses);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.Not.EqualTo(zombieAttacks[0]));
            Assert.That(slam.DamageSummary, Is.EqualTo("zombie slam roll zombie slam type"));
            Assert.That(slam.AttackBonuses, Has.Count.EqualTo(1).And.Contains(92));
            Assert.That(slam.Frequency.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void ApplyTo_GainSlamAttacks_NoHands()
        {
            baseCreature.NumberOfHands = 0;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.EqualTo(zombieAttacks[0]));
            Assert.That(slam.Frequency.Quantity, Is.EqualTo(1));
        }

        [TestCase(SizeConstants.Fine, "1")]
        [TestCase(SizeConstants.Diminutive, "1")]
        [TestCase(SizeConstants.Tiny, "1d2")]
        [TestCase(SizeConstants.Small, "1d3")]
        [TestCase(SizeConstants.Medium, "1d4")]
        [TestCase(SizeConstants.Large, "1d6")]
        [TestCase(SizeConstants.Huge, "1d8")]
        [TestCase(SizeConstants.Gargantuan, "2d6")]
        [TestCase(SizeConstants.Colossal, "2d8")]
        public void ApplyTo_ReplacesSlamAttack_KeepOriginalSlamDamage(string size, string damage)
        {
            baseCreature.Size = size;
            baseCreature.Attacks = baseCreature.Attacks.Union(
            [
                new Attack
                {
                    Name = "Slam",
                    Damages =
                    [
                        new() { Roll = "damage roll", Type = "damage type" }
                    ],
                    Frequency = new Frequency
                    {
                        Quantity = 2,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            ]);

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(zombieQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            zombieAttacks[0].Damages[0].Roll = damage;

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(1337);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(1336);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.Not.EqualTo(zombieAttacks[0]));
            Assert.That(slam.DamageSummary, Is.EqualTo("damage roll damage type"));
            Assert.That(slam.Frequency.Quantity, Is.EqualTo(2));
        }

        [Test]
        public void ApplyTo_LoseSpecialAttacks()
        {
            var specialAttack = new Attack
            {
                Name = "my special attack",
                IsSpecial = true,
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(
            [
                specialAttack,
                new Attack { Name = "my normal attack", IsSpecial = false },
            ]);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialAttacks, Is.Empty);
            Assert.That(creature.Attacks, Does.Not.Contain(specialAttack)
                .And.Not.Empty);
        }

        [Test]
        public void ApplyTo_ReplaceSpecialQualities()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.EqualTo(zombieQualities));
        }

        [Test]
        public void ApplyTo_ReplaceSpecialQualities_KeepAttackBonuses()
        {
            var attackBonus = new Feat { Name = FeatConstants.SpecialQualities.AttackBonus, Power = 1337, Foci = ["losers"] };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(
            [
                attackBonus
            ]);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(zombieQualities)
                .And.Contain(attackBonus));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public void ApplyTo_ReplaceSpecialQualities_KeepWeaponProficiencies()
        {
            var proficiency = new Feat { Name = "weapon proficiency", Foci = ["guns"] };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(
            [
                proficiency
            ]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency))
                .Returns(["weapon proficiency", "other weapon proficiency"]);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(zombieQualities)
                .And.Contain(proficiency));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public void ApplyTo_ReplaceSpecialQualities_KeepArmorProficiencies()
        {
            var proficiency = new Feat { Name = "armor proficiency" };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(
            [
                proficiency
            ]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, GroupConstants.ArmorProficiency))
                .Returns(["armor proficiency", "other armor proficiency"]);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(zombieQualities)
                .And.Contain(proficiency));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public void ApplyTo_SetSavingThrows()
        {
            var zombieSaves = new Dictionary<string, Save>
            {
                [SaveConstants.Fortitude] = new Save
                {
                    BaseAbility = baseCreature.Abilities[AbilityConstants.Constitution],
                    BaseValue = 96,
                },
                [SaveConstants.Reflex] = new Save
                {
                    BaseAbility = baseCreature.Abilities[AbilityConstants.Dexterity],
                    BaseValue = 1337,
                },
                [SaveConstants.Will] = new Save
                {
                    BaseAbility = baseCreature.Abilities[AbilityConstants.Wisdom],
                    BaseValue = 1336,
                }
            };

            mockSavesGenerator
                .Setup(g => g.GenerateWith(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    It.Is<IEnumerable<Feat>>(ff => ff.IsEquivalentTo(baseCreature.SpecialQualities.Union(zombieQualities))),
                    baseCreature.Abilities))
                .Returns(zombieSaves);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Saves, Is.EqualTo(zombieSaves));
        }

        [Test]
        public void ApplyTo_SetAbilities()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.EqualTo(-2));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.Zero);

            Assert.That(creature.Abilities[AbilityConstants.Constitution].HasScore, Is.False);
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].HasScore, Is.False);
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
        }

        [Test]
        public void BUG_ApplyTo_SetAbilities_DoNotIncreaseStrengthIfNoStrength()
        {
            baseCreature.Abilities[AbilityConstants.Strength].BaseScore = 0;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(0));
            Assert.That(creature.Abilities[AbilityConstants.Strength].HasScore, Is.False);
            Assert.That(creature.Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
        }

        [Test]
        public void ApplyTo_LoseAllSkills()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.Empty);
        }

        [Test]
        public void ApplyTo_LoseAllFeats()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Is.Empty);
        }

        [TestCase(.1, ChallengeRatingConstants.CR1_8th)]
        [TestCase(.25, ChallengeRatingConstants.CR1_8th)]
        [TestCase(.5, ChallengeRatingConstants.CR1_4th)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(2, ChallengeRatingConstants.CR1)]
        [TestCase(3, ChallengeRatingConstants.CR2)]
        [TestCase(4, ChallengeRatingConstants.CR3)]
        [TestCase(5, ChallengeRatingConstants.CR3)]
        [TestCase(6, ChallengeRatingConstants.CR4)]
        [TestCase(7, ChallengeRatingConstants.CR4)]
        [TestCase(8, ChallengeRatingConstants.CR5)]
        [TestCase(9, ChallengeRatingConstants.CR6)]
        [TestCase(10, ChallengeRatingConstants.CR6)]
        public void ApplyTo_AdjustChallengeRating(double hitDice, string challengeRating)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDice;
            var newRounded = Convert.ToInt32(Math.Max(1, hitDice * 2));

            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    newRounded, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(hitDice * 2));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating));
        }

        [TestCase(11)]
        [TestCase(12)]
        [TestCase(20)]
        [TestCase(96)]
        public void ApplyTo_ThrowsException_IfHitDiceTooHigh(double hitDice)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDice;
            var newRounded = Convert.ToInt32(Math.Max(1, hitDice * 2));

            mockDice
                .Setup(d => d
                    .Roll(20)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(20)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    20, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var expected = new InvalidCreatureException(
                $"Creature has too many hit dice ({hitDice} > 10)",
                false,
                baseCreature.Name,
                templates: [CreatureConstants.Templates.Zombie]);

            var function = () => applicator.ApplyTo(baseCreature, false);
            Assert.That(function, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(expected.Message));
        }

        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Evil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Evil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Evil)]
        public void ApplyTo_ChangeAlignment(string lawfulness, string goodness)
        {
            baseCreature.Alignment.Lawfulness = lawfulness;
            baseCreature.Alignment.Goodness = goodness;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo(AlignmentConstants.NeutralEvil));
        }

        [Test]
        public void ApplyTo_SetNoLevelAdjustment()
        {
            baseCreature.LevelAdjustment = 96;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Outsider;

            var expected = new InvalidCreatureException(
                "Type 'Outsider' is not valid",
                false,
                baseCreature.Name,
                templates: [CreatureConstants.Templates.Zombie]);
            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(expected.Message));
        }

        [TestCase("subtype 1", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.TrueNeutral)]
        [TestCase("subtype 1", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralEvil)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.NeutralEvil)]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible_WithFilters(string type, string challengeRating, string alignment)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");

            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [challengeRating],
                Alignments = [alignment]
            };
            var (Compatible, Reason) = filters.AreCompatible(
                [AlignmentConstants.NeutralEvil],
                [ChallengeRatingConstants.CR1_2nd],
                [CreatureConstants.Types.Undead, "subtype 1", "subtype 2"]);

            var expected = new InvalidCreatureException(
                Reason,
                false,
                baseCreature.Name,
                filters,
                null,
                CreatureConstants.Templates.Zombie);

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, false, filters),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(expected.Message));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithOtherTemplate()
        {
            baseCreature.Templates.Add("my other template");

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.Zombie));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.NeutralEvil);

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity * 2)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity * 2)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    zombieBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var filters = new Filters
            {
                Types = ["subtype 1"],
                ChallengeRatings = [ChallengeRatingConstants.CR1_2nd],
                Alignments = [AlignmentConstants.NeutralEvil]
            };

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Zombie));
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public async Task ApplyToAsync_ChangeCreatureType(string original)
        {
            baseCreature.Type.Name = original;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public async Task ApplyToAsync_KeepSubtype(string subtype)
        {
            var subtypes = new[]
            {
                "subtype 1",
                subtype,
                "subtype 2",
            };
            baseCreature.Type.SubTypes = subtypes;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.SubTypes.ToArray(), Is.EqualTo(subtypes)
                .And.Contains(subtype)
                .And.Length.EqualTo(3));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Angel)]
        [TestCase(CreatureConstants.Types.Subtypes.Archon)]
        [TestCase(CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Types.Subtypes.Dwarf)]
        [TestCase(CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Types.Subtypes.Evil)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnoll)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnome)]
        [TestCase(CreatureConstants.Types.Subtypes.Goblinoid)]
        [TestCase(CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Types.Subtypes.Halfling)]
        [TestCase(CreatureConstants.Types.Subtypes.Human)]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Types.Subtypes.Orc)]
        [TestCase(CreatureConstants.Types.Subtypes.Reptilian)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger)]
        public async Task ApplyToAsync_LoseSubtype(string subtype)
        {
            var subtypes = new[]
            {
                "subtype 1",
                subtype,
                "subtype 2",
            };
            baseCreature.Type.SubTypes = subtypes;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.SubTypes.ToArray(), Is.EqualTo(subtypes.Except([subtype]))
                .And.Not.Contains(subtype)
                .And.Length.EqualTo(2));
        }

        [Test]
        public async Task ApplyToAsync_DemographicsAdjusted()
        {
            var zombieDemographics = new Demographics
            {
                Skin = "rotting",
                Other = "hungry for brains",
                Gender = "decaying gender",
            };
            mockDemographicsGenerator
                .Setup(s => s.UpdateByTemplate(baseCreature.Demographics, baseCreature.Name, CreatureConstants.Templates.Zombie, false, false))
                .Returns(zombieDemographics);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    zombieBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2,
                    zombieDemographics.Gender))
                .Returns(zombieAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(zombieDemographics));
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        public async Task ApplyToAsync_HitDiceBecomeD12(int hitDie)
        {
            baseCreature.HitPoints.HitDice[0].HitDie = hitDie;
            var newQuantity = baseCreature.HitPoints.HitDiceQuantity * 2;
            var newRounded = baseCreature.HitPoints.RoundedHitDiceQuantity * 2;

            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([96, 1337]);
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice, Has.Count.EqualTo(1));
            Assert.That(creature.HitPoints.HitDice[0].Quantity, Is.EqualTo(newQuantity));
            Assert.That(creature.HitPoints.HitDice[0].RoundedQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(96 + 1337));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(1336));
        }

        [Test]
        public async Task ApplyToAsync_AerialManeuverability_BecomesClumsy()
        {
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs")
            {
                Value = 1337,
                Description = "Superb Maneuverability (hoverboard)"
            };

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Is.Not.Empty
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(1337));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("Clumsy Maneuverability (hoverboard)"));
        }

        [TestCase(SizeConstants.Fine, 0)]
        [TestCase(SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Small, 1)]
        [TestCase(SizeConstants.Medium, 2)]
        [TestCase(SizeConstants.Large, 3)]
        [TestCase(SizeConstants.Huge, 4)]
        [TestCase(SizeConstants.Gargantuan, 7)]
        [TestCase(SizeConstants.Colossal, 11)]
        public async Task ApplyToAsync_GainsNaturalArmorBasedOnSize(string size, int bonus)
        {
            baseCreature.Size = size;

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(zombieQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(bonus));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

            var naturalArmor = creature.ArmorClass.NaturalArmorBonuses.Single();
            Assert.That(naturalArmor.Condition, Is.Empty);
            Assert.That(naturalArmor.IsConditional, Is.False);
            Assert.That(naturalArmor.Value, Is.EqualTo(bonus));
        }

        [TestCase(SizeConstants.Fine, 0)]
        [TestCase(SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Small, 1)]
        [TestCase(SizeConstants.Medium, 2)]
        [TestCase(SizeConstants.Large, 3)]
        [TestCase(SizeConstants.Huge, 4)]
        [TestCase(SizeConstants.Gargantuan, 7)]
        [TestCase(SizeConstants.Colossal, 11)]
        public async Task ApplyToAsync_ReplacesNaturalArmorBasedOnSize(string size, int bonus)
        {
            baseCreature.Size = size;
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 666);

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(zombieQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(bonus));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

            var naturalArmor = creature.ArmorClass.NaturalArmorBonuses.Single();
            Assert.That(naturalArmor.Condition, Is.Empty);
            Assert.That(naturalArmor.IsConditional, Is.False);
            Assert.That(naturalArmor.Value, Is.EqualTo(bonus));
        }

        [Test]
        public async Task ApplyToAsync_BaseAttackBonus()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(zombieBaseAttack));
        }

        [TestCase(SizeConstants.Fine, "1")]
        [TestCase(SizeConstants.Diminutive, "1")]
        [TestCase(SizeConstants.Tiny, "1d2")]
        [TestCase(SizeConstants.Small, "1d3")]
        [TestCase(SizeConstants.Medium, "1d4")]
        [TestCase(SizeConstants.Large, "1d6")]
        [TestCase(SizeConstants.Huge, "1d8")]
        [TestCase(SizeConstants.Gargantuan, "2d6")]
        [TestCase(SizeConstants.Colossal, "2d8")]
        public async Task ApplyToAsync_ReplacesSlamAttack_DamageBasedOnSize(string size, string damage)
        {
            baseCreature.Size = size;
            baseCreature.Attacks = baseCreature.Attacks.Union(
            [
                new Attack
                {
                    Name = "Slam",
                    Damages =
                    [
                        new() { Roll = "damage roll", Type = "damage type" }
                    ],
                    Frequency = new Frequency
                    {
                        Quantity = 2,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            ]);

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(zombieQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            zombieAttacks[0].Damages[0].Roll = damage;

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(1336);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(1337);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.Not.EqualTo(zombieAttacks[0]));
            Assert.That(slam.DamageSummary, Is.EqualTo($"{damage} zombie damage type"));
            Assert.That(slam.Frequency.Quantity, Is.EqualTo(2));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        public async Task ApplyToAsync_GainSlamAttacks_OnlyOne(int numberOfHands)
        {
            baseCreature.NumberOfHands = numberOfHands;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.EqualTo(zombieAttacks[0]));
            Assert.That(slam.Frequency.Quantity, Is.EqualTo(1));
        }

        [Test]
        public async Task ApplyToAsync_GainSlamAttacks_WithAttackBonuses()
        {
            baseCreature.NumberOfHands = 2;

            var attacksWithBonuses = new[]
            {
                new Attack
                {
                    Name = "Slam",
                    Damages =
                    [
                        new() { Roll = "zombie slam roll", Type = "zombie slam type" }
                    ],
                    Frequency = new Frequency { Quantity = 1, TimePeriod = FeatConstants.Frequencies.Round },
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = [92]
                },
            };

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    zombieAttacks,
                    It.Is<IEnumerable<Feat>>(f =>
                        f.IsEquivalentTo(baseCreature.Feats
                            .Union(baseCreature.SpecialQualities)
                            .Union(zombieQualities))),
                    baseCreature.Abilities))
                .Returns(attacksWithBonuses);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.Not.EqualTo(zombieAttacks[0]));
            Assert.That(slam.DamageSummary, Is.EqualTo("zombie slam roll zombie slam type"));
            Assert.That(slam.AttackBonuses, Has.Count.EqualTo(1).And.Contains(92));
            Assert.That(slam.Frequency.Quantity, Is.EqualTo(1));
        }

        [Test]
        public async Task ApplyToAsync_GainSlamAttacks_NoHands()
        {
            baseCreature.NumberOfHands = 0;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.EqualTo(zombieAttacks[0]));
            Assert.That(slam.Frequency.Quantity, Is.EqualTo(1));
        }

        [TestCase(SizeConstants.Fine, "1")]
        [TestCase(SizeConstants.Diminutive, "1")]
        [TestCase(SizeConstants.Tiny, "1d2")]
        [TestCase(SizeConstants.Small, "1d3")]
        [TestCase(SizeConstants.Medium, "1d4")]
        [TestCase(SizeConstants.Large, "1d6")]
        [TestCase(SizeConstants.Huge, "1d8")]
        [TestCase(SizeConstants.Gargantuan, "2d6")]
        [TestCase(SizeConstants.Colossal, "2d8")]
        public async Task ApplyToAsync_ReplacesSlamAttack_KeepOriginalSlamDamage(string size, string damage)
        {
            baseCreature.Size = size;
            baseCreature.Attacks = baseCreature.Attacks.Union(
            [
                new Attack
                {
                    Name = "Slam",
                    Damages =
                    [
                        new() { Roll = "damage roll", Type = "damage type" }
                    ],
                    Frequency = new Frequency
                    {
                        Quantity = 2,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            ]);

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(zombieQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            zombieAttacks[0].Damages[0].Roll = damage;

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(1337);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(1336);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.Not.EqualTo(zombieAttacks[0]));
            Assert.That(slam.DamageSummary, Is.EqualTo("damage roll damage type"));
            Assert.That(slam.Frequency.Quantity, Is.EqualTo(2));
        }

        [Test]
        public async Task ApplyToAsync_LoseSpecialAttacks()
        {
            var specialAttack = new Attack
            {
                Name = "my special attack",
                IsSpecial = true,
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(
            [
                specialAttack,
                new Attack { Name = "my normal attack", IsSpecial = false },
            ]);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialAttacks, Is.Empty);
            Assert.That(creature.Attacks, Does.Not.Contain(specialAttack)
                .And.Not.Empty);
        }

        [Test]
        public async Task ApplyToAsync_ReplaceSpecialQualities()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.EqualTo(zombieQualities));
        }

        [Test]
        public async Task ApplyToAsync_ReplaceSpecialQualities_KeepAttackBonuses()
        {
            var attackBonus = new Feat { Name = FeatConstants.SpecialQualities.AttackBonus, Power = 1337, Foci = ["losers"] };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(
            [
                attackBonus
            ]);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(zombieQualities)
                .And.Contain(attackBonus));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public async Task ApplyToAsync_ReplaceSpecialQualities_KeepWeaponProficiencies()
        {
            var proficiency = new Feat { Name = "weapon proficiency", Foci = ["guns"] };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(
            [
                proficiency
            ]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency))
                .Returns(["weapon proficiency", "other weapon proficiency"]);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(zombieQualities)
                .And.Contain(proficiency));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public async Task ApplyToAsync_ReplaceSpecialQualities_KeepArmorProficiencies()
        {
            var proficiency = new Feat { Name = "armor proficiency" };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(
            [
                proficiency
            ]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, GroupConstants.ArmorProficiency))
                .Returns(["armor proficiency", "other armor proficiency"]);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(zombieQualities)
                .And.Contain(proficiency));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public async Task ApplyToAsync_SetSavingThrows()
        {
            var zombieSaves = new Dictionary<string, Save>
            {
                [SaveConstants.Fortitude] = new Save
                {
                    BaseAbility = baseCreature.Abilities[AbilityConstants.Constitution],
                    BaseValue = 96,
                },
                [SaveConstants.Reflex] = new Save
                {
                    BaseAbility = baseCreature.Abilities[AbilityConstants.Dexterity],
                    BaseValue = 1337,
                },
                [SaveConstants.Will] = new Save
                {
                    BaseAbility = baseCreature.Abilities[AbilityConstants.Wisdom],
                    BaseValue = 1336,
                }
            };

            mockSavesGenerator
                .Setup(g => g.GenerateWith(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    It.Is<IEnumerable<Feat>>(ff => ff.IsEquivalentTo(baseCreature.SpecialQualities.Union(zombieQualities))),
                    baseCreature.Abilities))
                .Returns(zombieSaves);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Saves, Is.EqualTo(zombieSaves));
        }

        [Test]
        public async Task ApplyToAsync_SetAbilities()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.EqualTo(-2));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.Zero);

            Assert.That(creature.Abilities[AbilityConstants.Constitution].HasScore, Is.False);
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].HasScore, Is.False);
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
        }

        [Test]
        public async Task BUG_ApplyToAsync_SetAbilities_DoNotIncreaseStrengthIfNoStrength()
        {
            baseCreature.Abilities[AbilityConstants.Strength].BaseScore = 0;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(0));
            Assert.That(creature.Abilities[AbilityConstants.Strength].HasScore, Is.False);
            Assert.That(creature.Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
        }

        [Test]
        public async Task ApplyToAsync_LoseAllSkills()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.Empty);
        }

        [Test]
        public async Task ApplyToAsync_LoseAllFeats()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Is.Empty);
        }

        [TestCase(.1, ChallengeRatingConstants.CR1_8th)]
        [TestCase(.25, ChallengeRatingConstants.CR1_8th)]
        [TestCase(.5, ChallengeRatingConstants.CR1_4th)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(2, ChallengeRatingConstants.CR1)]
        [TestCase(3, ChallengeRatingConstants.CR2)]
        [TestCase(4, ChallengeRatingConstants.CR3)]
        [TestCase(5, ChallengeRatingConstants.CR3)]
        [TestCase(6, ChallengeRatingConstants.CR4)]
        [TestCase(7, ChallengeRatingConstants.CR4)]
        [TestCase(8, ChallengeRatingConstants.CR5)]
        [TestCase(9, ChallengeRatingConstants.CR6)]
        [TestCase(10, ChallengeRatingConstants.CR6)]
        public async Task ApplyToAsync_AdjustChallengeRating(double hitDice, string challengeRating)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDice;
            var newRounded = Convert.ToInt32(Math.Max(1, hitDice * 2));

            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    newRounded, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(hitDice * 2));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating));
        }

        //INFO: This only occurs when a creature is advanced
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(20)]
        [TestCase(96)]
        public async Task ApplyToAsync_ThrowsException_IfHitDiceTooHigh(double hitDice)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDice;
            var newRounded = Convert.ToInt32(Math.Max(1, hitDice * 2));

            mockDice
                .Setup(d => d
                    .Roll(20)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(20)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    20, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var expected = new InvalidCreatureException(
                $"Creature has too many hit dice ({hitDice} > 10)",
                false,
                baseCreature.Name,
                templates: [CreatureConstants.Templates.Zombie]);

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(expected.Message));
        }

        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Evil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Evil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Evil)]
        public async Task ApplyToAsync_ChangeAlignment(string lawfulness, string goodness)
        {
            baseCreature.Alignment.Lawfulness = lawfulness;
            baseCreature.Alignment.Goodness = goodness;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo(AlignmentConstants.NeutralEvil));
        }

        [Test]
        public async Task ApplyToAsync_SetNoLevelAdjustment()
        {
            baseCreature.LevelAdjustment = 1337;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public void ApplyTo_RemoveMagic()
        {
            baseCreature.Magic.ArcaneSpellFailure = 9266;
            baseCreature.Magic.Caster = "my caster";
            baseCreature.Magic.CasterLevel = 90210;
            baseCreature.Magic.CastingAbility = baseCreature.Abilities[AbilityConstants.Wisdom];
            baseCreature.Magic.Domains = ["domain 1", "domain 2"];
            baseCreature.Magic.KnownSpells = [new Spell { Level = 42, Name = "my spell", Source = "my source" }];
            baseCreature.Magic.PreparedSpells = [new Spell { Level = 783, Name = "my prepared spell", Source = "my prepared source" }];
            baseCreature.Magic.SpellsPerDay = [new SpellQuantity { BonusSpells = 1337, Level = 1336, Quantity = 96, Source = "my per day source" }];
            baseCreature.CasterLevel = 783;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Magic.ArcaneSpellFailure, Is.Zero);
            Assert.That(creature.Magic.Caster, Is.Empty);
            Assert.That(creature.Magic.CasterLevel, Is.Zero);
            Assert.That(creature.Magic.CastingAbility, Is.Null);
            Assert.That(creature.Magic.Domains, Is.Empty);
            Assert.That(creature.Magic.KnownSpells, Is.Empty);
            Assert.That(creature.Magic.PreparedSpells, Is.Empty);
            Assert.That(creature.Magic.SpellsPerDay, Is.Empty);
            Assert.That(creature.CasterLevel, Is.Zero);
        }

        [Test]
        public async Task ApplyToAsync_RemoveMagic()
        {
            baseCreature.Magic.ArcaneSpellFailure = 9266;
            baseCreature.Magic.Caster = "my caster";
            baseCreature.Magic.CasterLevel = 90210;
            baseCreature.Magic.CastingAbility = baseCreature.Abilities[AbilityConstants.Wisdom];
            baseCreature.Magic.Domains = ["domain 1", "domain 2"];
            baseCreature.Magic.KnownSpells = [new Spell { Level = 42, Name = "my spell", Source = "my source" }];
            baseCreature.Magic.PreparedSpells = [new Spell { Level = 783, Name = "my prepared spell", Source = "my prepared source" }];
            baseCreature.Magic.SpellsPerDay = [new SpellQuantity { BonusSpells = 1337, Level = 1336, Quantity = 96, Source = "my per day source" }];
            baseCreature.CasterLevel = 783;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Magic.ArcaneSpellFailure, Is.Zero);
            Assert.That(creature.Magic.Caster, Is.Empty);
            Assert.That(creature.Magic.CasterLevel, Is.Zero);
            Assert.That(creature.Magic.CastingAbility, Is.Null);
            Assert.That(creature.Magic.Domains, Is.Empty);
            Assert.That(creature.Magic.KnownSpells, Is.Empty);
            Assert.That(creature.Magic.PreparedSpells, Is.Empty);
            Assert.That(creature.Magic.SpellsPerDay, Is.Empty);
            Assert.That(creature.CasterLevel, Is.Zero);
        }

        //INFO: Since Zombies get Toughness as a bonus feat
        [Test]
        public void ApplyTo_HitDiceQuantity_RerollWithQualities()
        {
            var updatedHitPoints = new HitPoints
            {
                Bonus = 600,
                Constitution = baseCreature.HitPoints.Constitution
            };
            updatedHitPoints.HitDice.AddRange(baseCreature.HitPoints.HitDice);

            mockHitPointsGenerator
                .Setup(g => g.RegenerateWith(baseCreature.HitPoints, zombieQualities))
                .Returns(updatedHitPoints);

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    BaseAttackQuality.Poor,
                    updatedHitPoints))
                .Returns(zombieBaseAttack);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        //INFO: Since Zombies get Toughness as a bonus feat
        [Test]
        public async Task ApplyToAsync_HitDiceQuantity_RerollWithQualities()
        {
            var updatedHitPoints = new HitPoints
            {
                Bonus = 600,
                Constitution = baseCreature.HitPoints.Constitution
            };
            updatedHitPoints.HitDice.AddRange(baseCreature.HitPoints.HitDice);

            mockHitPointsGenerator
                .Setup(g => g.RegenerateWith(baseCreature.HitPoints, zombieQualities))
                .Returns(updatedHitPoints);

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    BaseAttackQuality.Poor,
                    updatedHitPoints))
                .Returns(zombieBaseAttack);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public void ApplyTo_SetsTemplate()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Zombie));
        }

        [Test]
        public async Task ApplyToAsync_SetsTemplate()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Zombie));
        }

        [Test]
        public void IsCompatible_ReturnsTrue()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var compatible = applicator.IsCompatible(prototype);
            Assert.That(compatible, Is.True);
        }

        [Test]
        public void IsCompatible_AsCharacter_ReturnsFalse()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .WithAsCharacter(true)
                .Build();

            var compatible = applicator.IsCompatible(prototype);
            Assert.That(compatible, Is.False);
        }

        [TestCase(CreatureConstants.Types.Aberration, true)]
        [TestCase(CreatureConstants.Types.Animal, true)]
        [TestCase(CreatureConstants.Types.Construct, false)]
        [TestCase(CreatureConstants.Types.Dragon, true)]
        [TestCase(CreatureConstants.Types.Elemental, true)]
        [TestCase(CreatureConstants.Types.Fey, true)]
        [TestCase(CreatureConstants.Types.Giant, true)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, true)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, true)]
        [TestCase(CreatureConstants.Types.Ooze, false)]
        [TestCase(CreatureConstants.Types.Outsider, false)]
        [TestCase(CreatureConstants.Types.Plant, false)]
        [TestCase(CreatureConstants.Types.Undead, false)]
        [TestCase(CreatureConstants.Types.Vermin, true)]
        public void IsCompatible_ReturnsCompatibility_BasedOnCreatureType(string creatureType, bool expected)
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(creatureType, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var compatible = applicator.IsCompatible(prototype);
            Assert.That(compatible, Is.EqualTo(expected));
        }

        [Test]
        public void IsCompatible_ReturnsFalse_IfIncorporeal()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", CreatureConstants.Types.Subtypes.Incorporeal, "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var compatible = applicator.IsCompatible(prototype);
            Assert.That(compatible, Is.False);
        }

        [Test]
        public void IsCompatible_ReturnsFalse_IfCreatureDoesNotHaveSkeleton()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .WithSkeleton(false)
                .Build();

            var compatible = applicator.IsCompatible(prototype);
            Assert.That(compatible, Is.False);
        }

        [TestCase(0.1, true)]
        [TestCase(0.25, true)]
        [TestCase(0.5, true)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(9, true)]
        [TestCase(10, true)]
        [TestCase(11, false)]
        [TestCase(19, false)]
        [TestCase(20, false)]
        [TestCase(21, false)]
        [TestCase(22, false)]
        [TestCase(96, false)]
        public void IsCompatible_ReturnsCompatibility_BasedOnHitDiceQuantity(double hitDiceQuantity, bool expected)
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(hitDiceQuantity)
                .Build();

            var compatible = applicator.IsCompatible(prototype);
            Assert.That(compatible, Is.EqualTo(expected));
        }

        [TestCase(AlignmentConstants.ChaoticEvil, false)]
        [TestCase(AlignmentConstants.ChaoticNeutral, false)]
        [TestCase(AlignmentConstants.ChaoticGood, false)]
        [TestCase(AlignmentConstants.LawfulEvil, false)]
        [TestCase(AlignmentConstants.LawfulGood, false)]
        [TestCase(AlignmentConstants.LawfulNeutral, false)]
        [TestCase(AlignmentConstants.NeutralEvil, true)]
        [TestCase(AlignmentConstants.NeutralGood, false)]
        [TestCase(AlignmentConstants.TrueNeutral, false)]
        [TestCase("wrong alignment", false)]
        public void IsCompatible_WithAlignment_ReturnsCompatibility_BasedOnAlignmentFilter(string alignment, bool expected)
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var filters = new Filters { Alignments = ["wrong alignment", alignment] };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.EqualTo(expected));
        }

        [TestCase(ChallengeRatingConstants.CR1_10th, 0, false)]
        [TestCase(ChallengeRatingConstants.CR1_10th, 0.1, false)]
        [TestCase(ChallengeRatingConstants.CR1_10th, 0.24, false)]
        [TestCase(ChallengeRatingConstants.CR1_10th, 0.25, false)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0, true)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.1, true)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.24, true)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.25, true)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.26, false)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.33, false)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 1, false)]
        [TestCase(ChallengeRatingConstants.CR1_6th, 0, false)]
        [TestCase(ChallengeRatingConstants.CR1_6th, 0.1, false)]
        [TestCase(ChallengeRatingConstants.CR1_6th, 0.24, false)]
        [TestCase(ChallengeRatingConstants.CR1_6th, 0.25, false)]
        [TestCase(ChallengeRatingConstants.CR1_6th, 0.26, false)]
        [TestCase(ChallengeRatingConstants.CR1_6th, 0.4, false)]
        [TestCase(ChallengeRatingConstants.CR1_6th, 0.5, false)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0, false)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.24, false)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.25, false)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.26, true)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.4, true)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.5, true)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.6, false)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 1, false)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, 0.26, false)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, 0.4, false)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, 0.5, false)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, 0.6, false)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, 0.9, false)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, 1, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 0.4, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 0.5, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 0.6, true)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 0.9, true)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 1, true)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 2, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 3, false)]
        [TestCase(ChallengeRatingConstants.CR1, 0.9, false)]
        [TestCase(ChallengeRatingConstants.CR1, 1, false)]
        [TestCase(ChallengeRatingConstants.CR1, 2, true)]
        [TestCase(ChallengeRatingConstants.CR1, 3, false)]
        [TestCase(ChallengeRatingConstants.CR1, 4, false)]
        [TestCase(ChallengeRatingConstants.CR2, 1, false)]
        [TestCase(ChallengeRatingConstants.CR2, 2, false)]
        [TestCase(ChallengeRatingConstants.CR2, 3, true)]
        [TestCase(ChallengeRatingConstants.CR2, 4, false)]
        [TestCase(ChallengeRatingConstants.CR2, 5, false)]
        [TestCase(ChallengeRatingConstants.CR3, 2, false)]
        [TestCase(ChallengeRatingConstants.CR3, 3, false)]
        [TestCase(ChallengeRatingConstants.CR3, 4, true)]
        [TestCase(ChallengeRatingConstants.CR3, 5, true)]
        [TestCase(ChallengeRatingConstants.CR3, 6, false)]
        [TestCase(ChallengeRatingConstants.CR3, 7, false)]
        [TestCase(ChallengeRatingConstants.CR4, 4, false)]
        [TestCase(ChallengeRatingConstants.CR4, 5, false)]
        [TestCase(ChallengeRatingConstants.CR4, 6, true)]
        [TestCase(ChallengeRatingConstants.CR4, 7, true)]
        [TestCase(ChallengeRatingConstants.CR4, 8, false)]
        [TestCase(ChallengeRatingConstants.CR4, 9, false)]
        [TestCase(ChallengeRatingConstants.CR5, 6, false)]
        [TestCase(ChallengeRatingConstants.CR5, 7, false)]
        [TestCase(ChallengeRatingConstants.CR5, 8, true)]
        [TestCase(ChallengeRatingConstants.CR5, 9, false)]
        [TestCase(ChallengeRatingConstants.CR5, 10, false)]
        [TestCase(ChallengeRatingConstants.CR6, 7, false)]
        [TestCase(ChallengeRatingConstants.CR6, 8, false)]
        [TestCase(ChallengeRatingConstants.CR6, 9, true)]
        [TestCase(ChallengeRatingConstants.CR6, 10, true)]
        [TestCase(ChallengeRatingConstants.CR7, 10, false)]
        [TestCase(ChallengeRatingConstants.CR8, 10, false)]
        [TestCase(ChallengeRatingConstants.CR9, 10, false)]
        [TestCase(ChallengeRatingConstants.CR10, 10, false)]
        [TestCase(ChallengeRatingConstants.CR11, 10, false)]
        [TestCase(ChallengeRatingConstants.CR12, 10, false)]
        [TestCase(ChallengeRatingConstants.CR13, 10, false)]
        [TestCase(ChallengeRatingConstants.CR14, 10, false)]
        [TestCase(ChallengeRatingConstants.CR15, 10, false)]
        [TestCase(ChallengeRatingConstants.CR16, 10, false)]
        [TestCase(ChallengeRatingConstants.CR17, 10, false)]
        [TestCase(ChallengeRatingConstants.CR18, 10, false)]
        [TestCase(ChallengeRatingConstants.CR19, 10, false)]
        [TestCase(ChallengeRatingConstants.CR20, 10, false)]
        [TestCase(ChallengeRatingConstants.CR21, 10, false)]
        [TestCase(ChallengeRatingConstants.CR22, 10, false)]
        [TestCase(ChallengeRatingConstants.CR23, 10, false)]
        [TestCase(ChallengeRatingConstants.CR24, 10, false)]
        [TestCase(ChallengeRatingConstants.CR25, 10, false)]
        [TestCase(ChallengeRatingConstants.CR26, 10, false)]
        [TestCase(ChallengeRatingConstants.CR27, 10, false)]
        [TestCase("9266", 10, false)]
        public void IsCompatible_WithChallengeRating_ReturnsCompatibility_BasedOnUpdatedChallengeRating(string challengeRatingFilter, double hitDiceQuantity, bool expected)
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(hitDiceQuantity)
                .Build();

            var filters = new Filters { ChallengeRatings = ["wrong cr", challengeRatingFilter] };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.EqualTo(expected));
        }

        [Test]
        public void IsCompatible_WithType_ReturnsTrue()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var filters = new Filters { Types = ["wrong type", "subtype 1"] };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.True);
        }

        [Test]
        public void IsCompatible_WithType_ReturnsTrue_WhenFilterIsUndead()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var filters = new Filters { Types = ["wrong type", CreatureConstants.Types.Undead] };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.True);
        }

        [TestCase(CreatureConstants.Types.Subtypes.Angel)]
        [TestCase(CreatureConstants.Types.Subtypes.Archon)]
        [TestCase(CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Types.Subtypes.Dwarf)]
        [TestCase(CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Types.Subtypes.Evil)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnoll)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnome)]
        [TestCase(CreatureConstants.Types.Subtypes.Goblinoid)]
        [TestCase(CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Types.Subtypes.Halfling)]
        [TestCase(CreatureConstants.Types.Subtypes.Human)]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Types.Subtypes.Orc)]
        [TestCase(CreatureConstants.Types.Subtypes.Reptilian)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger)]
        public void IsCompatible_WithType_ReturnsFalse_WhenFilterIsInvalid(string typeFilter)
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", typeFilter, "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var filters = new Filters { Types = ["wrong type", typeFilter] };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.False);
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void IsCompatible_WithType_ReturnsFalse_WhenOriginalType(string typeFilter)
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(typeFilter, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var filters = new Filters { Types = ["wrong type", typeFilter] };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.False);
        }

        [TestCase(CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Types.Subtypes.Water)]
        public void IsCompatible_WithType_ReturnsTrue_WhenFilterMatchesSubtype(string typeFilter)
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", typeFilter, "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var filters = new Filters { Types = ["wrong type", typeFilter] };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.True);
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Types.Subtypes.Water)]
        public void IsCompatible_WithType_ReturnsFalse_WhenFilterDoesNotMatch(string typeFilter)
        {
            var creatureType = CreatureConstants.Types.Humanoid;
            if (creatureType == typeFilter)
                creatureType = CreatureConstants.Types.MagicalBeast;

            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(creatureType, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var filters = new Filters { Types = ["wrong type", typeFilter] };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.False);
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsTrue()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var filters = new Filters
            {
                Alignments = ["wrong alignment", AlignmentConstants.NeutralEvil],
                ChallengeRatings = ["wrong cr", ChallengeRatingConstants.CR1_2nd],
                Types = ["wrong type", "subtype 2"]
            };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.True);
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecausePrototype()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .WithSkeleton(false)
                .Build();

            var filters = new Filters
            {
                Alignments = ["wrong alignment", AlignmentConstants.NeutralEvil],
                ChallengeRatings = ["wrong cr", ChallengeRatingConstants.CR1_2nd],
                Types = ["wrong type", "subtype 2"]
            };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.False);
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseAlignment()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var filters = new Filters
            {
                Alignments = ["wrong alignment", AlignmentConstants.ChaoticEvil],
                ChallengeRatings = ["wrong cr", ChallengeRatingConstants.CR1_2nd],
                Types = ["wrong type", "subtype 2"]
            };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.False);
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseChallengeRating()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var filters = new Filters
            {
                Alignments = ["wrong alignment", AlignmentConstants.NeutralEvil],
                ChallengeRatings = ["wrong cr", ChallengeRatingConstants.CR1_3rd],
                Types = ["wrong type", "subtype 2"]
            };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.False);
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseType()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var filters = new Filters
            {
                Alignments = ["wrong alignment", AlignmentConstants.NeutralEvil],
                ChallengeRatings = ["wrong cr", ChallengeRatingConstants.CR1_2nd],
                Types = ["wrong type", "subtype 3"]
            };

            var compatible = applicator.IsCompatible(prototype, filters);
            Assert.That(compatible, Is.False);
        }

        [Test]
        public void ApplyTo_Prototype_ThrowsException_WhenCreatureNotCompatible()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithAlignments("original alignment")
                .Build();

            var expected = new InvalidCreatureException(
                "Type 'Outsider' is not valid",
                false,
                prototype.Name,
                templates: [CreatureConstants.Templates.Zombie]);

            var function = () => applicator.ApplyTo(prototype);
            Assert.That(function, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(expected.Message));
        }

        [TestCase("subtype 1", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.TrueNeutral)]
        [TestCase("subtype 1", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralEvil)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.NeutralEvil)]
        public void ApplyTo_Prototype_ThrowsException_WhenCreatureNotCompatible_WithFilters(string type, string challengeRating, string alignment)
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithAlignments("original alignment")
                .Build();

            var filters = new Filters
            {
                Types = [type],
                ChallengeRatings = [challengeRating],
                Alignments = [alignment]
            };
            var (Compatible, Reason) = filters.AreCompatible(
                [AlignmentConstants.NeutralEvil],
                [ChallengeRatingConstants.CR1_2nd],
                [CreatureConstants.Types.Undead, "subtype 1", "subtype 2"]);

            var expected = new InvalidCreatureException(
                Reason,
                false,
                prototype.Name,
                filters,
                null,
                CreatureConstants.Templates.Zombie);

            var func = () => applicator.ApplyTo(prototype, filters);
            Assert.That(func, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(expected.Message));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAbility(AbilityConstants.Constitution, 9266)
                .WithAbility(AbilityConstants.Wisdom, 90210)
                .WithAbility(AbilityConstants.Intelligence, 42)
                .WithAbility(AbilityConstants.Charisma, 600)
                .WithAbility(AbilityConstants.Strength, 1337)
                .WithAbility(AbilityConstants.Dexterity, 1336)
                .WithHitDiceQuantity(1)
                .WithChallengeRating("my cr")
                .WithLevelAdjustment(96)
                .WithAlignments("my alignment", "my other alignment")
                .WithCasterLevel(783)
                .Build();

            var updatedPrototype = applicator.ApplyTo(prototype);
            Assert.That(updatedPrototype, Is.SameAs(prototype));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].TemplateScore, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].FullScore, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].HasScore, Is.False);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].HasScore, Is.True);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].FullScore, Is.Zero);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].HasScore, Is.False);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].HasScore, Is.True);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(1337 + Ability.DefaultScore + 2));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].HasScore, Is.True);
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.EqualTo(-2));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(1336 + Ability.DefaultScore - 2));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].HasScore, Is.True);
            Assert.That(updatedPrototype.HitDiceQuantity, Is.EqualTo(2));
            Assert.That(updatedPrototype.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1_2nd));
            Assert.That(updatedPrototype.LevelAdjustment, Is.Null);
            Assert.That(updatedPrototype.Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(updatedPrototype.Type.SubTypes, Is.EquivalentTo(["subtype 1", "subtype 2"]));
            Assert.That(updatedPrototype.Alignments, Is.EquivalentTo([new Alignment(AlignmentConstants.NeutralEvil)]));
            Assert.That(updatedPrototype.CasterLevel, Is.Zero);
            Assert.That(updatedPrototype.Templates, Is.EqualTo([CreatureConstants.Templates.Zombie]));
        }

        [TestCase(0, 0)]
        [TestCase(1 / 10d, 2 / 10d)]
        [TestCase(1 / 8d, 2 / 8d)]
        [TestCase(1 / 6d, 2 / 6d)]
        [TestCase(1 / 4d, 2 / 4d)]
        [TestCase(1 / 3d, 2 / 3d)]
        [TestCase(1 / 2d, 2 / 2d)]
        [TestCase(1, 2)]
        [TestCase(2, 4)]
        [TestCase(3, 6)]
        [TestCase(4, 8)]
        [TestCase(5, 10)]
        [TestCase(6, 12)]
        [TestCase(7, 14)]
        [TestCase(8, 16)]
        [TestCase(9, 18)]
        [TestCase(10, 20)]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithUpdatedHitDiceQuantity(double hitDiceQuantity, double expected)
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(hitDiceQuantity)
                .Build();

            var updatedPrototype = applicator.ApplyTo(prototype);
            Assert.That(updatedPrototype, Is.SameAs(prototype));
            Assert.That(updatedPrototype.HitDiceQuantity, Is.EqualTo(expected));
        }

        [TestCase(ChallengeRatingConstants.CR1_8th, 0)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.01)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 1 / 10d)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 1 / 8d)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 1 / 6d)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.5 / 2d - .01)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.5 / 2d)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.5 / 2d + 0.01)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 1 / 3d)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 1 / 2d - 0.01)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 1 / 2d)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 1 / 2d + 0.01)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 2 / 2d - 0.01)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 2 / 2d)]
        [TestCase(ChallengeRatingConstants.CR1, 3 / 2d)]
        [TestCase(ChallengeRatingConstants.CR1, 4 / 2d)]
        [TestCase(ChallengeRatingConstants.CR2, 5 / 2d)]
        [TestCase(ChallengeRatingConstants.CR2, 6 / 2d)]
        [TestCase(ChallengeRatingConstants.CR3, 7 / 2d)]
        [TestCase(ChallengeRatingConstants.CR3, 8 / 2d)]
        [TestCase(ChallengeRatingConstants.CR3, 9 / 2d)]
        [TestCase(ChallengeRatingConstants.CR3, 10 / 2d)]
        [TestCase(ChallengeRatingConstants.CR4, 11 / 2d)]
        [TestCase(ChallengeRatingConstants.CR4, 12 / 2d)]
        [TestCase(ChallengeRatingConstants.CR4, 13 / 2d)]
        [TestCase(ChallengeRatingConstants.CR4, 14 / 2d)]
        [TestCase(ChallengeRatingConstants.CR5, 15 / 2d)]
        [TestCase(ChallengeRatingConstants.CR5, 16 / 2d)]
        [TestCase(ChallengeRatingConstants.CR6, 17 / 2d)]
        [TestCase(ChallengeRatingConstants.CR6, 18 / 2d)]
        [TestCase(ChallengeRatingConstants.CR6, 19 / 2d)]
        [TestCase(ChallengeRatingConstants.CR6, 20 / 2d)]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithUpdatedChallengeRating(string expected, double hitDiceQuantity)
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(hitDiceQuantity)
                .WithChallengeRating("my cr")
                .Build();

            var updatedPrototype = applicator.ApplyTo(prototype);
            Assert.That(updatedPrototype, Is.SameAs(prototype));
            Assert.That(updatedPrototype.ChallengeRating, Is.EqualTo(expected));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Angel)]
        [TestCase(CreatureConstants.Types.Subtypes.Archon)]
        [TestCase(CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Types.Subtypes.Dwarf)]
        [TestCase(CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Types.Subtypes.Evil)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnoll)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnome)]
        [TestCase(CreatureConstants.Types.Subtypes.Goblinoid)]
        [TestCase(CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Types.Subtypes.Halfling)]
        [TestCase(CreatureConstants.Types.Subtypes.Human)]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Types.Subtypes.Orc)]
        [TestCase(CreatureConstants.Types.Subtypes.Reptilian)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger)]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithUpdatedCreatureTypes_RemovesInvalidType(string subtype)
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", subtype, "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();

            var updatedPrototype = applicator.ApplyTo(prototype);
            Assert.That(updatedPrototype, Is.SameAs(prototype));
            Assert.That(updatedPrototype.Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(updatedPrototype.Type.SubTypes, Is.EquivalentTo(["subtype 1", "subtype 2"]));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithAdditionalTemplates()
        {
            var prototype = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithHitDiceQuantity(1)
                .Build();
            prototype.Templates.Add("my template");

            var updatedPrototype = applicator.ApplyTo(prototype);
            Assert.That(updatedPrototype, Is.SameAs(prototype));
            Assert.That(updatedPrototype.Templates, Is.EqualTo(["my template", CreatureConstants.Templates.Zombie]));
        }
    }
}
