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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine("\tReason: Type 'Outsider' is not valid");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Zombie}");

            Assert.That((Func<object>)(() => applicator.ApplyTo(baseCreature, false)),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.TrueNeutral, "Alignment filter 'True Neutral' is not valid")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralEvil, "CR filter 1 does not match updated creature CR 1/2")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.NeutralEvil, "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.NeutralEvil, "Zombies cannot be characters")]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Zombie}");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment
            };

            var func = () => applicator.ApplyTo(baseCreature, asCharacter, filters);
            Assert.That(func, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
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
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2, baseCreature.Demographics.Gender))
                .Returns(zombieAttacks);

            var filters = new Filters
            {
                Type = "subtype 1",
                ChallengeRating = ChallengeRatingConstants.CR1_2nd,
                Alignment = AlignmentConstants.NeutralEvil
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

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: Creature has too many hit dice ({hitDice} > 10)");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Zombie}");

            Assert.That((Func<object>)(() => applicator.ApplyTo(baseCreature, false)),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
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

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine("\tReason: Type 'Outsider' is not valid");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Zombie}");

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.TrueNeutral, "Alignment filter 'True Neutral' is not valid")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralEvil, "CR filter 1 does not match updated creature CR 1/2")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.NeutralEvil, "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.NeutralEvil, "Zombies cannot be characters")]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Zombie}");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment
            };

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, asCharacter, filters),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
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
                Type = "subtype 1",
                ChallengeRating = ChallengeRatingConstants.CR1_2nd,
                Alignment = AlignmentConstants.NeutralEvil
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

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: Creature has too many hit dice ({hitDice} > 10)");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Zombie}");

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
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
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_AsCharacter_ReturnsFalse()
        {
            Assert.Fail("not yet written");
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
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_ReturnsFalse_IfIncorporeal()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_ReturnsFalse_IfCreatureDoesNotHaveSkeleton()
        {
            Assert.Fail("not yet written");
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
            Assert.Fail("not yet written");
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
            Assert.Fail("not yet written");
        }

        [TestCase(ChallengeRatingConstants.CR1_8th, 0, true)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.1, true)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.24, true)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.25, true)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.26, false)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.33, false)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 1, false)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0, false)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.1, false)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.24, false)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.25, true)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.26, true)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.4, true)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.5, true)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.6, false)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 1, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 0.1, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 0.4, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 0.5, true)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 0.6, true)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 0.9, true)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 1, true)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 2, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 3, false)]
        [TestCase(ChallengeRatingConstants.CR1, 0.5, false)]
        [TestCase(ChallengeRatingConstants.CR1, 0.9, false)]
        [TestCase(ChallengeRatingConstants.CR1, 1, true)]
        [TestCase(ChallengeRatingConstants.CR1, 2, true)]
        [TestCase(ChallengeRatingConstants.CR1, 3, false)]
        [TestCase(ChallengeRatingConstants.CR1, 4, false)]
        [TestCase(ChallengeRatingConstants.CR2, 0.5, false)]
        [TestCase(ChallengeRatingConstants.CR2, 1, false)]
        [TestCase(ChallengeRatingConstants.CR2, 2, true)]
        [TestCase(ChallengeRatingConstants.CR2, 3, true)]
        [TestCase(ChallengeRatingConstants.CR2, 4, false)]
        [TestCase(ChallengeRatingConstants.CR2, 5, false)]
        [TestCase(ChallengeRatingConstants.CR3, 1, false)]
        [TestCase(ChallengeRatingConstants.CR3, 2, false)]
        [TestCase(ChallengeRatingConstants.CR3, 3, true)]
        [TestCase(ChallengeRatingConstants.CR3, 4, true)]
        [TestCase(ChallengeRatingConstants.CR3, 5, true)]
        [TestCase(ChallengeRatingConstants.CR3, 6, false)]
        [TestCase(ChallengeRatingConstants.CR3, 7, false)]
        [TestCase(ChallengeRatingConstants.CR4, 3, false)]
        [TestCase(ChallengeRatingConstants.CR4, 4, false)]
        [TestCase(ChallengeRatingConstants.CR4, 5, true)]
        [TestCase(ChallengeRatingConstants.CR4, 6, true)]
        [TestCase(ChallengeRatingConstants.CR4, 7, true)]
        [TestCase(ChallengeRatingConstants.CR4, 8, false)]
        [TestCase(ChallengeRatingConstants.CR4, 9, false)]
        [TestCase(ChallengeRatingConstants.CR5, 5, false)]
        [TestCase(ChallengeRatingConstants.CR5, 6, false)]
        [TestCase(ChallengeRatingConstants.CR5, 7, true)]
        [TestCase(ChallengeRatingConstants.CR5, 8, true)]
        [TestCase(ChallengeRatingConstants.CR5, 9, false)]
        [TestCase(ChallengeRatingConstants.CR5, 10, false)]
        [TestCase(ChallengeRatingConstants.CR6, 6, false)]
        [TestCase(ChallengeRatingConstants.CR6, 7, false)]
        [TestCase(ChallengeRatingConstants.CR6, 8, true)]
        [TestCase(ChallengeRatingConstants.CR6, 9, true)]
        [TestCase(ChallengeRatingConstants.CR6, 10, true)]
        public void IsCompatible_WithChallengeRating_ReturnsCompatibility_BasedOnUpdatedChallengeRating(string challengeRatingFilter, double hitDiceQuantity, bool expected)
        {
            Assert.Fail("not yet written");
        }

        [TestCaseSource(nameof(InvalidChallengeRatings))]
        public void IsCompatible_WithChallengeRating_ReturnsFalse_WhenChallengeRatingFilterInvalid(string challengeRatingFilter)
        {
            Assert.Fail("not yet written");
        }

        private static IEnumerable InvalidChallengeRatings => ChallengeRatingConstants.GetOrdered()
            .Except(
            [
                ChallengeRatingConstants.CR1_8th,
                ChallengeRatingConstants.CR1_4th,
                ChallengeRatingConstants.CR1_2nd,
                ChallengeRatingConstants.CR1,
                ChallengeRatingConstants.CR2,
                ChallengeRatingConstants.CR3,
                ChallengeRatingConstants.CR4,
                ChallengeRatingConstants.CR5,
                ChallengeRatingConstants.CR6,
            ])
            .Union(["9266", "my challenge rating"])
            .Select(t => new TestCaseData(t));

        [Test]
        public void IsCompatible_WithType_ReturnsTrue()
        {
            Assert.Fail("not yet written");
        }

        [TestCase(CreatureConstants.Types.Undead)]
        public void IsCompatible_WithType_ReturnsTrue_WhenFilterMatchesSkeleton(string typeFilter)
        {
            Assert.Fail("not yet written");
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
            Assert.Fail("not yet written");
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
        public void IsCompatible_WithType_ReturnsTrue_WhenFilterMatches(string typeFilter)
        {
            Assert.Fail("not yet written");
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
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsTrue()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecausePrototype()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseAlignment()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseChallengeRating()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseType()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_Prototype_ThrowsException_WhenCreatureNotCompatible()
        {
            Assert.Fail("update for prototype");
            baseCreature.Type.Name = CreatureConstants.Types.Outsider;

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine("\tReason: Type 'Outsider' is not valid");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Zombie}");

            Assert.That((Func<object>)(() => applicator.ApplyTo(baseCreature, false)),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.TrueNeutral, "Alignment filter 'True Neutral' is not valid")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralEvil, "CR filter 1 does not match updated creature CR 1/2")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.NeutralEvil, "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR1_2nd, AlignmentConstants.NeutralEvil, "Zombies cannot be characters")]
        public void ApplyTo_Prototype_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            Assert.Fail("update for prototype");
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Zombie}");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment
            };

            var func = () => applicator.ApplyTo(baseCreature, asCharacter, filters);
            Assert.That(func, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype()
        {
            Assert.Fail("not yet written");
            Assert.Fail("Assert updated abilities");
            Assert.Fail("Assert updated hit points");
            Assert.Fail("Assert updated CR");
            Assert.Fail("Assert level adjustment is null");
            Assert.Fail("Assert updated creature type");
            Assert.Fail("Assert updated alignment");
            Assert.Fail("Assert caster level is 0");
            Assert.Fail("assert prototype template updated");
        }

        [TestCase(0, 0)]
        [TestCase(0.1, 0.2)]
        [TestCase(0.24, 0.48)]
        [TestCase(0.25, 0.50)]
        [TestCase(0.26, 0.52)]
        [TestCase(0.4, 0.8)]
        [TestCase(0.5, 1)]
        [TestCase(0.6, 1.2)]
        [TestCase(0.9, 1.8)]
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
            Assert.Fail("not yet written");
        }

        [TestCase(ChallengeRatingConstants.CR1_8th, 0)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.1)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.24)]
        [TestCase(ChallengeRatingConstants.CR1_8th, 0.25)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.25)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.26)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.4)]
        [TestCase(ChallengeRatingConstants.CR1_4th, 0.5)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 0.5)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 0.6)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 0.9)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, 1)]
        [TestCase(ChallengeRatingConstants.CR1, 1)]
        [TestCase(ChallengeRatingConstants.CR1, 2)]
        [TestCase(ChallengeRatingConstants.CR2, 2)]
        [TestCase(ChallengeRatingConstants.CR2, 3)]
        [TestCase(ChallengeRatingConstants.CR3, 3)]
        [TestCase(ChallengeRatingConstants.CR3, 4)]
        [TestCase(ChallengeRatingConstants.CR3, 5)]
        [TestCase(ChallengeRatingConstants.CR4, 5)]
        [TestCase(ChallengeRatingConstants.CR4, 6)]
        [TestCase(ChallengeRatingConstants.CR4, 7)]
        [TestCase(ChallengeRatingConstants.CR5, 7)]
        [TestCase(ChallengeRatingConstants.CR5, 8)]
        [TestCase(ChallengeRatingConstants.CR6, 8)]
        [TestCase(ChallengeRatingConstants.CR6, 9)]
        [TestCase(ChallengeRatingConstants.CR6, 10)]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithUpdatedChallengeRating(string expected, double hitDiceQuantity)
        {
            Assert.Fail("not yet written");
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
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithAdditionalTemplates()
        {
            Assert.Fail("not yet written");
        }
    }
}
