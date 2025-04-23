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
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Models;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class SkeletonApplicatorTests
    {
        private TemplateApplicator applicator;
        private Creature baseCreature;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<ICollectionTypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<Dice> mockDice;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ISavesGenerator> mockSavesGenerator;
        private Attack[] skeletonAttacks;
        private IEnumerable<Feat> skeletonQualities;
        private int skeletonBaseAttack;
        private Mock<ICreaturePrototypeFactory> mockPrototypeFactory;
        private Mock<IDemographicsGenerator> mockDemographicsGenerator;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockTypeAndAmountSelector = new Mock<ICollectionTypeAndAmountSelector>();
            mockDice = new Mock<Dice>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockSavesGenerator = new Mock<ISavesGenerator>();
            mockPrototypeFactory = new Mock<ICreaturePrototypeFactory>();
            mockDemographicsGenerator = new Mock<IDemographicsGenerator>();

            applicator = new SkeletonApplicator(
                mockCollectionSelector.Object,
                mockTypeAndAmountSelector.Object,
                mockDice.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSavesGenerator.Object,
                mockPrototypeFactory.Object,
                mockDemographicsGenerator.Object);

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .WithHitDiceQuantityNoMoreThan(20)
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .Build();

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 9266 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            skeletonQualities = new[]
            {
                new Feat { Name = "skeleton quality 1" },
                new Feat { Name = "skeleton quality 2" },
                new Feat { Name = FeatConstants.Initiative_Improved, Power = 783 }
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Skeleton,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(skeletonQualities);

            skeletonBaseAttack = 42;

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints))
                .Returns(skeletonBaseAttack);

            skeletonAttacks =
            [
                new Attack
                {
                    Name = "Claw",
                    Damages =
                    [
                        new Damage { Roll = "skeleton damage roll", Type = "skeleton damage type" }
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
                    CreatureConstants.Templates.Skeleton,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity,
                    baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    skeletonAttacks,
                    It.Is<IEnumerable<Feat>>(f =>
                        f.IsEquivalentTo(baseCreature.SpecialQualities
                            .Union(skeletonQualities))),
                    baseCreature.Abilities))
                .Returns(skeletonAttacks);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(["my wrong creature", baseCreature.Name, "my other creature"]);

            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, CreatureConstants.Templates.Skeleton, false, true))
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
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Skeleton}");

            Assert.That(() => applicator.ApplyTo(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1_3rd, AlignmentConstants.TrueNeutral, "Alignment filter 'True Neutral' is not valid")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralEvil, "CR filter 1 does not match updated creature CR 1/3")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR1_3rd, AlignmentConstants.NeutralEvil, "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR1_3rd, AlignmentConstants.NeutralEvil, "Skeletons cannot be characters")]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Skeleton}");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = challengeRating;
            filters.Alignment = alignment;

            Assert.That(() => applicator.ApplyTo(baseCreature, asCharacter, filters),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithOtherTemplate()
        {
            baseCreature.Templates.Add("my other template");

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.Skeleton));
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
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 9266 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            var filters = new Filters
            {
                Type = "subtype 1",
                ChallengeRating = ChallengeRatingConstants.CR1_3rd,
                Alignment = AlignmentConstants.NeutralEvil
            };

            var creature = applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Skeleton));
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
            var skeletonDemographics = new Demographics
            {
                Hair = "skull",
                Other = "spooky scary",
                Gender = "boney gender",
            };
            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, CreatureConstants.Templates.Skeleton, false, true))
                .Returns(skeletonDemographics);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity,
                    skeletonDemographics.Gender))
                .Returns(skeletonAttacks);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(skeletonDemographics));
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        public void ApplyTo_HitDiceBecomeD12(int hitDie)
        {
            baseCreature.HitPoints.HitDice[0].HitDie = hitDie;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 600, 1337 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(600 + 1337));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(1336));
        }

        [Test]
        public void ApplyTo_LoseFlySpeed_Wings()
        {
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Value = 600;
            baseCreature.Speeds[SpeedConstants.Fly].Description = "Superb (Wings)";

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Is.Not.Empty
                .And.Not.ContainKey(SpeedConstants.Fly));
        }

        [Test]
        public void ApplyTo_KeepFlySpeed_Magic()
        {
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs")
            {
                Value = 600,
                Description = "Superb (Magic)"
            };

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Is.Not.Empty
                .And.ContainKey(SpeedConstants.Fly));
        }

        [TestCase(SizeConstants.Fine, 0)]
        [TestCase(SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Small, 1)]
        [TestCase(SizeConstants.Medium, 2)]
        [TestCase(SizeConstants.Large, 2)]
        [TestCase(SizeConstants.Huge, 3)]
        [TestCase(SizeConstants.Gargantuan, 6)]
        [TestCase(SizeConstants.Colossal, 10)]
        public void ApplyTo_GainsNaturalArmorBasedOnSize(string size, int bonus)
        {
            baseCreature.Size = size;

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Skeleton,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(skeletonQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity,
                    baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(bonus));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

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
        [TestCase(SizeConstants.Large, 2)]
        [TestCase(SizeConstants.Huge, 3)]
        [TestCase(SizeConstants.Gargantuan, 6)]
        [TestCase(SizeConstants.Colossal, 10)]
        public void ApplyTo_ReplacesNaturalArmorBasedOnSize(string size, int bonus)
        {
            baseCreature.Size = size;
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 666);

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Skeleton,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(skeletonQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(bonus));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

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
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(skeletonBaseAttack));
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
        public void ApplyTo_ReplacesClawAttack_DamageBasedOnSize(string size, string damage)
        {
            baseCreature.Size = size;
            baseCreature.Attacks = baseCreature.Attacks.Union(new[]
            {
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new() { Roll = "damage roll", Type = "damage type" }
                    },
                    Frequency = new Frequency
                    {
                        Quantity = 2,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            });

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Skeleton,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(skeletonQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            skeletonAttacks[0].Damages[0].Roll = damage;

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(1336);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(1337);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Not.Null.And.Not.EqualTo(skeletonAttacks[0]));
            Assert.That(claw.DamageSummary, Is.EqualTo($"{damage} skeleton damage type"));
            Assert.That(claw.Frequency.Quantity, Is.EqualTo(2));
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
        public void ApplyTo_GainClawAttacks_PerNumberOfHands(int numberOfHands)
        {
            baseCreature.NumberOfHands = numberOfHands;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Not.Null.And.EqualTo(skeletonAttacks[0]));
            Assert.That(claw.Frequency.Quantity, Is.EqualTo(numberOfHands));
        }

        [Test]
        public void ApplyTo_GainClawAttacks_WithAttackBonuses()
        {
            baseCreature.NumberOfHands = 2;

            var attacksWithBonuses = new[]
            {
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "skeleton claw roll", Type = "skeleton claw type" }
                    },
                    Frequency = new Frequency { Quantity = 1, TimePeriod = FeatConstants.Frequencies.Round },
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = new List<int> { 92 }
                },
            };

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    skeletonAttacks,
                    It.Is<IEnumerable<Feat>>(f =>
                        f.IsEquivalentTo(baseCreature.Feats
                            .Union(baseCreature.SpecialQualities)
                            .Union(skeletonQualities))),
                    baseCreature.Abilities))
                .Returns(attacksWithBonuses);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Not.Null.And.Not.EqualTo(skeletonAttacks[0]));
            Assert.That(claw.DamageSummary, Is.EqualTo("skeleton claw roll skeleton claw type"));
            Assert.That(claw.AttackBonuses, Has.Count.EqualTo(1).And.Contains(92));
            Assert.That(claw.Frequency.Quantity, Is.EqualTo(2));
        }

        [Test]
        public void ApplyTo_DoesNotGainClawAttacks_NoHands()
        {
            baseCreature.NumberOfHands = 0;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Null);
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
        public void ApplyTo_ReplacesClawAttack_KeepOriginalClawDamage(string size, string damage)
        {
            baseCreature.Size = size;
            baseCreature.Attacks = baseCreature.Attacks.Union(
            [
                new Attack
                {
                    Name = "Claw",
                    Damages =
                    [
                        new Damage { Roll = "damage roll", Type = "damage type" }
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
                    CreatureConstants.Templates.Skeleton,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(skeletonQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            skeletonAttacks[0].Damages[0].Roll = damage;

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(1337);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(1336);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Not.Null.And.Not.EqualTo(skeletonAttacks[0]));
            Assert.That(claw.DamageSummary, Is.EqualTo("damage roll damage type"));
            Assert.That(claw.Frequency.Quantity, Is.EqualTo(2));
        }

        [Test]
        public void ApplyTo_LoseSpecialAttacks()
        {
            var specialAttack = new Attack
            {
                Name = "my special attack",
                IsSpecial = true,
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[]
            {
                specialAttack,
                new Attack { Name = "my normal attack", IsSpecial = false },
            });

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
            Assert.That(creature.SpecialQualities, Is.EqualTo(skeletonQualities));
        }

        [Test]
        public void ApplyTo_ReplaceSpecialQualities_KeepAttackBonuses()
        {
            var attackBonus = new Feat { Name = FeatConstants.SpecialQualities.AttackBonus, Power = 600, Foci = new[] { "losers" } };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(new[]
            {
                attackBonus
            });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(skeletonQualities)
                .And.Contain(attackBonus));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public void ApplyTo_ReplaceSpecialQualities_KeepWeaponProficiencies()
        {
            var proficiency = new Feat { Name = "weapon proficiency", Foci = new[] { "guns" } };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(new[]
            {
                proficiency
            });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency))
                .Returns(new[] { "weapon proficiency", "other weapon proficiency" });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(skeletonQualities)
                .And.Contain(proficiency));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public void ApplyTo_ReplaceSpecialQualities_KeepArmorProficiencies()
        {
            var proficiency = new Feat { Name = "armor proficiency" };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(new[]
            {
                proficiency
            });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, GroupConstants.ArmorProficiency))
                .Returns(new[] { "armor proficiency", "other armor proficiency" });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(skeletonQualities)
                .And.Contain(proficiency));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        //INFO: Improve Initiative is one of the bonus feats for skeletons
        [Test]
        public void ApplyTo_RecomputeInitiativeBonus()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.InitiativeBonus, Is.EqualTo(783));
        }

        //INFO: Improve Initiative is one of the bonus feats for skeletons
        [Test]
        public async Task ApplyToAsync_RecomputeInitiativeBonus()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.InitiativeBonus, Is.EqualTo(783));
        }

        [Test]
        public void ApplyTo_SetSavingThrows()
        {
            var skeletonSaves = new Dictionary<string, Save>();
            skeletonSaves[SaveConstants.Fortitude] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Constitution],
                BaseValue = 600,
            };
            skeletonSaves[SaveConstants.Reflex] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Dexterity],
                BaseValue = 1337,
            };
            skeletonSaves[SaveConstants.Will] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Wisdom],
                BaseValue = 1336,
            };

            mockSavesGenerator
                .Setup(g => g.GenerateWith(
                    CreatureConstants.Templates.Skeleton,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    It.Is<IEnumerable<Feat>>(ff => ff.IsEquivalentTo(baseCreature.SpecialQualities.Union(skeletonQualities))),
                    baseCreature.Abilities))
                .Returns(skeletonSaves);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Saves, Is.EqualTo(skeletonSaves));
        }

        [Test]
        public void ApplyTo_SetAbilities()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.EqualTo(2));
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

        [TestCase(.1, ChallengeRatingConstants.CR1_6th)]
        [TestCase(.25, ChallengeRatingConstants.CR1_6th)]
        [TestCase(.5, ChallengeRatingConstants.CR1_6th)]
        [TestCase(1, ChallengeRatingConstants.CR1_3rd)]
        [TestCase(2, ChallengeRatingConstants.CR1)]
        [TestCase(3, ChallengeRatingConstants.CR1)]
        [TestCase(4, ChallengeRatingConstants.CR2)]
        [TestCase(5, ChallengeRatingConstants.CR2)]
        [TestCase(6, ChallengeRatingConstants.CR3)]
        [TestCase(7, ChallengeRatingConstants.CR3)]
        [TestCase(8, ChallengeRatingConstants.CR4)]
        [TestCase(9, ChallengeRatingConstants.CR4)]
        [TestCase(10, ChallengeRatingConstants.CR5)]
        [TestCase(11, ChallengeRatingConstants.CR5)]
        [TestCase(12, ChallengeRatingConstants.CR6)]
        [TestCase(13, ChallengeRatingConstants.CR6)]
        [TestCase(14, ChallengeRatingConstants.CR6)]
        [TestCase(15, ChallengeRatingConstants.CR7)]
        [TestCase(16, ChallengeRatingConstants.CR7)]
        [TestCase(17, ChallengeRatingConstants.CR7)]
        [TestCase(18, ChallengeRatingConstants.CR8)]
        [TestCase(19, ChallengeRatingConstants.CR8)]
        [TestCase(20, ChallengeRatingConstants.CR8)]
        public void ApplyTo_AdjustChallengeRating(double hitDice, string challengeRating)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDice;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating));
        }

        [TestCase(21)]
        [TestCase(22)]
        [TestCase(30)]
        [TestCase(96)]
        public void ApplyTo_ThrowException_IfHitDiceTooHigh(double hitDice)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDice;

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
                    CreatureConstants.Templates.Skeleton,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    20,
                    baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: Creature has too many hit dice ({hitDice} > 20)");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Skeleton}");

            Assert.That(() => applicator.ApplyTo(baseCreature, false),
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
            baseCreature.LevelAdjustment = 600;

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
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Skeleton}");

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1_3rd, AlignmentConstants.TrueNeutral, "Alignment filter 'True Neutral' is not valid")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralEvil, "CR filter 1 does not match updated creature CR 1/3")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR1_3rd, AlignmentConstants.NeutralEvil, "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR1_3rd, AlignmentConstants.NeutralEvil, "Skeletons cannot be characters")]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Skeleton}");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters();
            filters.Type = type;
            filters.ChallengeRating = challengeRating;
            filters.Alignment = alignment;

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
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.Skeleton));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            var filters = new Filters
            {
                Type = "subtype 1",
                ChallengeRating = ChallengeRatingConstants.CR1_3rd,
                Alignment = AlignmentConstants.NeutralEvil
            };

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Skeleton));
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
            var skeletonDemographics = new Demographics
            {
                Hair = "skull",
                Other = "spooky scary",
                Gender = "boney gender",
            };
            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, CreatureConstants.Templates.Skeleton, false, true))
                .Returns(skeletonDemographics);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity,
                    skeletonDemographics.Gender))
                .Returns(skeletonAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(skeletonDemographics));
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        public async Task ApplyToAsync_HitDiceBecomeD12(int hitDie)
        {
            baseCreature.HitPoints.HitDice[0].HitDie = hitDie;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 600, 1337 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(600 + 1337));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(1336));
        }

        [Test]
        public async Task ApplyToAsync_LoseFlySpeed_Wings()
        {
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Value = 600;
            baseCreature.Speeds[SpeedConstants.Fly].Description = "Superb (Wings)";

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Is.Not.Empty
                .And.Not.ContainKey(SpeedConstants.Fly));
        }

        [Test]
        public async Task ApplyToAsync_KeepFlySpeed_Magic()
        {
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Value = 600;
            baseCreature.Speeds[SpeedConstants.Fly].Description = "Superb (Magic)";

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Is.Not.Empty
                .And.ContainKey(SpeedConstants.Fly));
        }

        [TestCase(SizeConstants.Fine, 0)]
        [TestCase(SizeConstants.Diminutive, 0)]
        [TestCase(SizeConstants.Tiny, 0)]
        [TestCase(SizeConstants.Small, 1)]
        [TestCase(SizeConstants.Medium, 2)]
        [TestCase(SizeConstants.Large, 2)]
        [TestCase(SizeConstants.Huge, 3)]
        [TestCase(SizeConstants.Gargantuan, 6)]
        [TestCase(SizeConstants.Colossal, 10)]
        public async Task ApplyToAsync_GainsNaturalArmorBasedOnSize(string size, int bonus)
        {
            baseCreature.Size = size;

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Skeleton,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(skeletonQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(bonus));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

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
        [TestCase(SizeConstants.Large, 2)]
        [TestCase(SizeConstants.Huge, 3)]
        [TestCase(SizeConstants.Gargantuan, 6)]
        [TestCase(SizeConstants.Colossal, 10)]
        public async Task ApplyToAsync_ReplacesNaturalArmorBasedOnSize(string size, int bonus)
        {
            baseCreature.Size = size;
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 666);

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Skeleton,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(skeletonQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(bonus));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

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
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(skeletonBaseAttack));
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
        public async Task ApplyToAsync_ReplacesClawAttack_DamageBasedOnSize(string size, string damage)
        {
            baseCreature.Size = size;
            baseCreature.Attacks = baseCreature.Attacks.Union(
            [
                new Attack
                {
                    Name = "Claw",
                    Damages =
                    [
                        new Damage { Roll = "damage roll", Type = "damage type" }
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
                    CreatureConstants.Templates.Skeleton,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(skeletonQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            skeletonAttacks[0].Damages[0].Roll = damage;

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(1336);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(1337);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Not.Null.And.Not.EqualTo(skeletonAttacks[0]));
            Assert.That(claw.DamageSummary, Is.EqualTo($"{damage} skeleton damage type"));
            Assert.That(claw.Frequency.Quantity, Is.EqualTo(2));
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
        public async Task ApplyToAsync_GainClawAttacks_PerNumberOfHands(int numberOfHands)
        {
            baseCreature.NumberOfHands = numberOfHands;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Not.Null.And.EqualTo(skeletonAttacks[0]));
            Assert.That(claw.Frequency.Quantity, Is.EqualTo(numberOfHands));
        }

        [Test]
        public async Task ApplyToAsync_GainClawAttacks_WithAttackBonuses()
        {
            baseCreature.NumberOfHands = 2;

            var attacksWithBonuses = new[]
            {
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "skeleton claw roll", Type = "skeleton claw type" }
                    },
                    Frequency = new Frequency { Quantity = 1, TimePeriod = FeatConstants.Frequencies.Round },
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = new List<int> { 92 }
                },
            };

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    skeletonAttacks,
                    It.Is<IEnumerable<Feat>>(f =>
                        f.IsEquivalentTo(baseCreature.Feats
                            .Union(baseCreature.SpecialQualities)
                            .Union(skeletonQualities))),
                    baseCreature.Abilities))
                .Returns(attacksWithBonuses);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Not.Null.And.Not.EqualTo(skeletonAttacks[0]));
            Assert.That(claw.DamageSummary, Is.EqualTo("skeleton claw roll skeleton claw type"));
            Assert.That(claw.AttackBonuses, Has.Count.EqualTo(1).And.Contains(92));
            Assert.That(claw.Frequency.Quantity, Is.EqualTo(2));
        }

        [Test]
        public async Task ApplyToAsync_DoesNotGainClawAttacks_NoHands()
        {
            baseCreature.NumberOfHands = 0;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Null);
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
        public async Task ApplyToAsync_ReplacesClawAttack_KeepOriginalClawDamage(string size, string damage)
        {
            baseCreature.Size = size;
            baseCreature.Attacks = baseCreature.Attacks.Union(new[]
            {
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "damage roll", Type = "damage type" }
                    },
                    Frequency = new Frequency
                    {
                        Quantity = 2,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            });

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Skeleton,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    Enumerable.Empty<Skill>(),
                    baseCreature.CanUseEquipment,
                    size,
                    new Alignment { Lawfulness = AlignmentConstants.Neutral, Goodness = AlignmentConstants.Evil }))
                .Returns(skeletonQualities);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            skeletonAttacks[0].Damages[0].Roll = damage;

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(1337);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(1336);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Not.Null.And.Not.EqualTo(skeletonAttacks[0]));
            Assert.That(claw.DamageSummary, Is.EqualTo("damage roll damage type"));
            Assert.That(claw.Frequency.Quantity, Is.EqualTo(2));
        }

        [Test]
        public async Task ApplyToAsync_LoseSpecialAttacks()
        {
            var specialAttack = new Attack
            {
                Name = "my special attack",
                IsSpecial = true,
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[]
            {
                specialAttack,
                new Attack { Name = "my normal attack", IsSpecial = false },
            });

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
            Assert.That(creature.SpecialQualities, Is.EqualTo(skeletonQualities));
        }

        [Test]
        public async Task ApplyToAsync_ReplaceSpecialQualities_KeepAttackBonuses()
        {
            var attackBonus = new Feat { Name = FeatConstants.SpecialQualities.AttackBonus, Power = 600, Foci = new[] { "losers" } };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(new[]
            {
                attackBonus
            });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(skeletonQualities)
                .And.Contain(attackBonus));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public async Task ApplyToAsync_ReplaceSpecialQualities_KeepWeaponProficiencies()
        {
            var proficiency = new Feat { Name = "weapon proficiency", Foci = new[] { "guns" } };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(new[]
            {
                proficiency
            });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency))
                .Returns(new[] { "weapon proficiency", "other weapon proficiency" });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(skeletonQualities)
                .And.Contain(proficiency));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public async Task ApplyToAsync_ReplaceSpecialQualities_KeepArmorProficiencies()
        {
            var proficiency = new Feat { Name = "armor proficiency" };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(new[]
            {
                proficiency
            });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.FeatGroups, GroupConstants.ArmorProficiency))
                .Returns(new[] { "armor proficiency", "other armor proficiency" });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(skeletonQualities)
                .And.Contain(proficiency));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public async Task ApplyToAsync_SetSavingThrows()
        {
            var skeletonSaves = new Dictionary<string, Save>();
            skeletonSaves[SaveConstants.Fortitude] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Constitution],
                BaseValue = 600,
            };
            skeletonSaves[SaveConstants.Reflex] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Dexterity],
                BaseValue = 1337,
            };
            skeletonSaves[SaveConstants.Will] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Wisdom],
                BaseValue = 1336,
            };

            mockSavesGenerator
                .Setup(g => g.GenerateWith(
                    CreatureConstants.Templates.Skeleton,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    It.Is<IEnumerable<Feat>>(ff => ff.IsEquivalentTo(baseCreature.SpecialQualities.Union(skeletonQualities))),
                    baseCreature.Abilities))
                .Returns(skeletonSaves);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Saves, Is.EqualTo(skeletonSaves));
        }

        [Test]
        public async Task ApplyToAsync_SetAbilities()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.EqualTo(2));
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

        [TestCase(.1, ChallengeRatingConstants.CR1_6th)]
        [TestCase(.25, ChallengeRatingConstants.CR1_6th)]
        [TestCase(.5, ChallengeRatingConstants.CR1_6th)]
        [TestCase(1, ChallengeRatingConstants.CR1_3rd)]
        [TestCase(2, ChallengeRatingConstants.CR1)]
        [TestCase(3, ChallengeRatingConstants.CR1)]
        [TestCase(4, ChallengeRatingConstants.CR2)]
        [TestCase(5, ChallengeRatingConstants.CR2)]
        [TestCase(6, ChallengeRatingConstants.CR3)]
        [TestCase(7, ChallengeRatingConstants.CR3)]
        [TestCase(8, ChallengeRatingConstants.CR4)]
        [TestCase(9, ChallengeRatingConstants.CR4)]
        [TestCase(10, ChallengeRatingConstants.CR5)]
        [TestCase(11, ChallengeRatingConstants.CR5)]
        [TestCase(12, ChallengeRatingConstants.CR6)]
        [TestCase(13, ChallengeRatingConstants.CR6)]
        [TestCase(14, ChallengeRatingConstants.CR6)]
        [TestCase(15, ChallengeRatingConstants.CR7)]
        [TestCase(16, ChallengeRatingConstants.CR7)]
        [TestCase(17, ChallengeRatingConstants.CR7)]
        [TestCase(18, ChallengeRatingConstants.CR8)]
        [TestCase(19, ChallengeRatingConstants.CR8)]
        [TestCase(20, ChallengeRatingConstants.CR8)]
        public async Task ApplyToAsync_AdjustChallengeRating(double hitDice, string challengeRating)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDice;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating));
        }

        //INFO: This only occurs when a creature is advanced
        [TestCase(21)]
        [TestCase(22)]
        [TestCase(30)]
        [TestCase(96)]
        public async Task ApplyToAsync_ThrowsException_IfHitDiceTooHigh(double hitDice)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDice;

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
                    CreatureConstants.Templates.Skeleton,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    20,
                    baseCreature.Demographics.Gender))
                .Returns(skeletonAttacks);

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: Creature has too many hit dice ({hitDice} > 20)");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Skeleton}");

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
            baseCreature.LevelAdjustment = 600;

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
            baseCreature.Magic.Domains = new[] { "domain 1", "domain 2" };
            baseCreature.Magic.KnownSpells = new[] { new Spell { Level = 42, Name = "my spell", Source = "my source" } };
            baseCreature.Magic.PreparedSpells = new[] { new Spell { Level = 600, Name = "my prepared spell", Source = "my prepared source" } };
            baseCreature.Magic.SpellsPerDay = new[] { new SpellQuantity { BonusSpells = 1337, Level = 1336, Quantity = 96, Source = "my per day source" } };
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
            baseCreature.Magic.Domains = new[] { "domain 1", "domain 2" };
            baseCreature.Magic.KnownSpells = new[] { new Spell { Level = 42, Name = "my spell", Source = "my source" } };
            baseCreature.Magic.PreparedSpells = new[] { new Spell { Level = 600, Name = "my prepared spell", Source = "my prepared source" } };
            baseCreature.Magic.SpellsPerDay = new[] { new SpellQuantity { BonusSpells = 1337, Level = 1336, Quantity = 96, Source = "my per day source" } };
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

        [Test]
        public void ApplyTo_SetsTemplate()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Skeleton));
        }

        [Test]
        public async Task ApplyToAsync_SetsTemplate()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Skeleton));
        }

        [Test]
        public void GetCompatibleCreatures_ReturnsEmpty_IfAsCharacter()
        {
            var creatures = new[] { "my creature", "my other creature" };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, true);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void GetCompatibleCreatures_ReturnsCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "my other creature", "another creature" };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 3" };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var hitDice = SetUpHitDice();
            hitDice["my other creature"] = [new() { AmountAsDouble = 2 }];
            hitDice["another creature"] = [new() { AmountAsDouble = 3 }];

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        private Dictionary<string, IEnumerable<TypeAndAmountDataSelection>> SetUpHitDice(double amount = 1)
        {
            var hitDice = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["my creature"] = [new() { AmountAsDouble = amount }],
                ["my other creature"] = [new() { AmountAsDouble = amount }],
                ["another creature"] = [new() { AmountAsDouble = amount }],
                ["wrong creature 1"] = [new() { AmountAsDouble = amount }],
                ["wrong creature 2"] = [new() { AmountAsDouble = amount }],
                ["wrong creature 3"] = [new() { AmountAsDouble = amount }],
                ["wrong creature 4"] = [new() { AmountAsDouble = amount }],
                ["wrong creature 5"] = [new() { AmountAsDouble = amount }],
                ["wrong creature 6"] = [new() { AmountAsDouble = amount }],
            };

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice))
                .Returns(hitDice);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, It.IsAny<string>()))
                .Returns((string a, string t, string c) => hitDice[c]);

            return hitDice;
        }

        [TestCase(AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.TrueNeutral)]
        [TestCase(AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.ChaoticEvil)]
        [TestCase("wrong alignment")]
        public void GetCompatibleCreatures_ReturnEmpty_WhenAlignmentFilterInvalid(string alignmentFilter)
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var hitDice = SetUpHitDice();
            hitDice["wrong creature 2"] = [new() { AmountAsDouble = 21 }];

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(["my wrong creature", "my creature", "my other creature", "yet another creature"]);

            var filters = new Filters { Alignment = alignmentFilter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures_WhenAlignmentFilterValid()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var hitDice = SetUpHitDice();
            hitDice["wrong creature 2"] = [new() { AmountAsDouble = 21 }];

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var filters = new Filters { Alignment = AlignmentConstants.NeutralEvil };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EquivalentTo(new[] { "my creature", "my other creature" }));
        }

        [TestCase(ChallengeRatingConstants.CR1_6th, 0, 0.5)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, 0.5, 1)]
        [TestCase(ChallengeRatingConstants.CR1, 1, 3)]
        [TestCase(ChallengeRatingConstants.CR2, 3, 5)]
        [TestCase(ChallengeRatingConstants.CR3, 5, 7)]
        [TestCase(ChallengeRatingConstants.CR4, 7, 9)]
        [TestCase(ChallengeRatingConstants.CR5, 9, 11)]
        [TestCase(ChallengeRatingConstants.CR6, 11, 14)]
        [TestCase(ChallengeRatingConstants.CR7, 14, 17)]
        [TestCase(ChallengeRatingConstants.CR8, 17, 20)]
        public void GetCompatibleCreatures_WithChallengeRating_ReturnsCompatibleCreatures(string challengeRatingFilter, double lower, double upper)
        {
            var creatures = new[] { "my creature", "my other creature", "another creature", "yet another creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 3" };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["yet another creature"] = new[] { CreatureConstants.Types.Giant };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(["my wrong creature", "my creature", "my other creature", "yet another creature"]);

            var hitDice = SetUpHitDice();
            hitDice["my creature"] = [new() { AmountAsDouble = lower + 0.01 }];
            hitDice["my other creature"] = [new() { AmountAsDouble = upper }];
            hitDice["another creature"] = [new() { AmountAsDouble = (lower + upper) / 2 }];
            hitDice["yet another creature"] = [new() { AmountAsDouble = (lower + upper) / 2 }];

            var filters = new Filters { ChallengeRating = challengeRatingFilter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature", "yet another creature" }));
        }

        [TestCaseSource(nameof(InvalidChallengeRatings))]
        public void GetCompatibleCreatures_WithChallengeRating_ReturnsEmpty_IfChallengeRatingNotValidForTemplate(string challengeRatingFilter)
        {
            var creatures = new[] { "my creature", "my other creature", "another creature", "yet another creature" };

            var filters = new Filters { ChallengeRating = challengeRatingFilter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        private static IEnumerable InvalidChallengeRatings => ChallengeRatingConstants.GetOrdered()
            .Except(new[]
            {
                ChallengeRatingConstants.CR1_6th,
                ChallengeRatingConstants.CR1_3rd,
                ChallengeRatingConstants.CR1,
                ChallengeRatingConstants.CR2,
                ChallengeRatingConstants.CR3,
                ChallengeRatingConstants.CR4,
                ChallengeRatingConstants.CR5,
                ChallengeRatingConstants.CR6,
                ChallengeRatingConstants.CR7,
                ChallengeRatingConstants.CR8,
            })
            .Union(new[] { "9266", "my challenge rating" })
            .Select(t => new TestCaseData(t));

        [TestCase(ChallengeRatingConstants.CR1_6th, 0, 0.5)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, 0.5, 1)]
        [TestCase(ChallengeRatingConstants.CR1, 1, 3)]
        [TestCase(ChallengeRatingConstants.CR2, 3, 5)]
        [TestCase(ChallengeRatingConstants.CR3, 5, 7)]
        [TestCase(ChallengeRatingConstants.CR4, 7, 9)]
        [TestCase(ChallengeRatingConstants.CR5, 9, 11)]
        [TestCase(ChallengeRatingConstants.CR6, 11, 14)]
        [TestCase(ChallengeRatingConstants.CR7, 14, 17)]
        [TestCase(ChallengeRatingConstants.CR8, 17, 20)]
        public void GetCompatibleCreatures_WithChallengeRating_ReturnsCompatibleCreatures_FilterOutCreaturesOutOfHitDiceRange(
            string challengeRatingFilter,
            double lower,
            double upper)
        {
            var creatures = new[] { "my creature", "my other creature", "another creature", "yet another creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 3" };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["yet another creature"] = new[] { CreatureConstants.Types.Giant };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(["my wrong creature", "my creature", "my other creature", "yet another creature"]);

            var hitDice = SetUpHitDice();
            hitDice["my creature"] = [new() { AmountAsDouble = lower + 0.01 }];
            hitDice["my other creature"] = [new() { AmountAsDouble = upper + 0.01 }];
            hitDice["another creature"] = [new() { AmountAsDouble = lower }];
            hitDice["yet another creature"] = [new() { AmountAsDouble = upper }];

            var filters = new Filters { ChallengeRating = challengeRatingFilter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "yet another creature" }));

            mockTypeAndAmountSelector.Verify(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "my other creature"), Times.Never);
            mockTypeAndAmountSelector.Verify(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "another creature"), Times.Never);
        }

        [Test]
        public void GetCompatibleCreatures_WithType_ReturnsCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "my other creature", "another creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 3" };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(["my wrong creature", "my creature", "my other creature"]);

            var hitDice = SetUpHitDice();
            hitDice["my other creature"] = [new() { AmountAsDouble = 2 }];

            var filters = new Filters { Type = CreatureConstants.Types.Undead };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
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
        public void GetCompatibleCreatures_WithType_ReturnsEmpty_WhenTypeFilterNotValid(string typeFilter)
        {
            var creatures = new[] { "my creature", "my other creature", "another creature" };
            var filters = new Filters { Type = typeFilter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);
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
        public void GetCompatibleCreatures_WithType_ReturnsCompatibleCreatures_FilterOutCreaturesWithWrongSubtype(string typeFilter)
        {
            var creatures = new[] { "my creature", "my other creature", "another creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", typeFilter };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, typeFilter };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "wrong subtype" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, typeFilter))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(["my wrong creature", "my creature", "my other creature"]);

            var hitDice = SetUpHitDice();
            hitDice["my other creature"] = [new() { AmountAsDouble = 2 }];

            var filters = new Filters { Type = typeFilter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_WithChallengeRatingAndType_ReturnsCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "my other creature", "another creature", "yet another creature" };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(["my wrong creature", "my creature", "my other creature", "yet another creature"]);

            var hitDice = SetUpHitDice();
            hitDice["my creature"] = [new() { AmountAsDouble = 1.01 }];
            hitDice["my other creature"] = [new() { AmountAsDouble = 2 }];
            hitDice["another creature"] = [new() { AmountAsDouble = 1 }];
            hitDice["yet another creature"] = [new() { AmountAsDouble = 1.5 }];

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 2" };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2" };
            types["yet another creature"] = new[] { CreatureConstants.Types.Giant };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR1 };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
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
        public void IsCompatible_ByCreatureType(string creatureType, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            SetUpHitDice();

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { creatureType, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [Test]
        public void IsCompatible_CannotBeIncorporeal()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            SetUpHitDice();

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", CreatureConstants.Types.Subtypes.Incorporeal, "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void IsCompatible_MustHaveSkeleton()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my other creature" });

            SetUpHitDice();

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void IsCompatible_CannotBeCharacter()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            SetUpHitDice();

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, true);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(0.1, true)]
        [TestCase(0.25, true)]
        [TestCase(0.5, true)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(10, true)]
        [TestCase(19, true)]
        [TestCase(20, true)]
        [TestCase(21, false)]
        [TestCase(22, false)]
        [TestCase(96, false)]
        public void IsCompatible_FewerThan10HitDice(double hitDiceQuantity, bool compatible)
        {
            SetUpHitDice(hitDiceQuantity);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(null, true)]
        [TestCase(CreatureConstants.Types.Humanoid, false)]
        [TestCase(CreatureConstants.Types.Undead, true)]
        [TestCase("subtype 1", true)]
        [TestCase("subtype 2", true)]
        [TestCase(CreatureConstants.Types.Subtypes.Extraplanar, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, false)]
        [TestCase("wrong type", false)]
        public void IsCompatible_TypeMustMatch(string type, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            SetUpHitDice();

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, type))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(.1, ChallengeRatingConstants.CR1_8th, false)]
        [TestCase(.1, ChallengeRatingConstants.CR1_6th, true)]
        [TestCase(.1, ChallengeRatingConstants.CR1_4th, false)]
        [TestCase(.25, ChallengeRatingConstants.CR1_8th, false)]
        [TestCase(.25, ChallengeRatingConstants.CR1_6th, true)]
        [TestCase(.25, ChallengeRatingConstants.CR1_4th, false)]
        [TestCase(.5, ChallengeRatingConstants.CR1_8th, false)]
        [TestCase(.5, ChallengeRatingConstants.CR1_6th, true)]
        [TestCase(.5, ChallengeRatingConstants.CR1_4th, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_4th, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_3rd, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, true)]
        [TestCase(2, ChallengeRatingConstants.CR2, false)]
        [TestCase(3, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(3, ChallengeRatingConstants.CR1, true)]
        [TestCase(3, ChallengeRatingConstants.CR2, false)]
        [TestCase(4, ChallengeRatingConstants.CR1, false)]
        [TestCase(4, ChallengeRatingConstants.CR2, true)]
        [TestCase(4, ChallengeRatingConstants.CR3, false)]
        [TestCase(5, ChallengeRatingConstants.CR1, false)]
        [TestCase(5, ChallengeRatingConstants.CR2, true)]
        [TestCase(5, ChallengeRatingConstants.CR3, false)]
        [TestCase(6, ChallengeRatingConstants.CR2, false)]
        [TestCase(6, ChallengeRatingConstants.CR3, true)]
        [TestCase(6, ChallengeRatingConstants.CR4, false)]
        [TestCase(7, ChallengeRatingConstants.CR2, false)]
        [TestCase(7, ChallengeRatingConstants.CR3, true)]
        [TestCase(7, ChallengeRatingConstants.CR4, false)]
        [TestCase(8, ChallengeRatingConstants.CR3, false)]
        [TestCase(8, ChallengeRatingConstants.CR4, true)]
        [TestCase(8, ChallengeRatingConstants.CR5, false)]
        [TestCase(9, ChallengeRatingConstants.CR3, false)]
        [TestCase(9, ChallengeRatingConstants.CR4, true)]
        [TestCase(9, ChallengeRatingConstants.CR5, false)]
        [TestCase(10, ChallengeRatingConstants.CR4, false)]
        [TestCase(10, ChallengeRatingConstants.CR5, true)]
        [TestCase(10, ChallengeRatingConstants.CR6, false)]
        [TestCase(11, ChallengeRatingConstants.CR4, false)]
        [TestCase(11, ChallengeRatingConstants.CR5, true)]
        [TestCase(11, ChallengeRatingConstants.CR6, false)]
        [TestCase(12, ChallengeRatingConstants.CR5, false)]
        [TestCase(12, ChallengeRatingConstants.CR6, true)]
        [TestCase(12, ChallengeRatingConstants.CR7, false)]
        [TestCase(13, ChallengeRatingConstants.CR5, false)]
        [TestCase(13, ChallengeRatingConstants.CR6, true)]
        [TestCase(13, ChallengeRatingConstants.CR7, false)]
        [TestCase(14, ChallengeRatingConstants.CR5, false)]
        [TestCase(14, ChallengeRatingConstants.CR6, true)]
        [TestCase(14, ChallengeRatingConstants.CR7, false)]
        [TestCase(15, ChallengeRatingConstants.CR6, false)]
        [TestCase(15, ChallengeRatingConstants.CR7, true)]
        [TestCase(15, ChallengeRatingConstants.CR8, false)]
        [TestCase(16, ChallengeRatingConstants.CR6, false)]
        [TestCase(16, ChallengeRatingConstants.CR7, true)]
        [TestCase(16, ChallengeRatingConstants.CR8, false)]
        [TestCase(17, ChallengeRatingConstants.CR6, false)]
        [TestCase(17, ChallengeRatingConstants.CR7, true)]
        [TestCase(17, ChallengeRatingConstants.CR8, false)]
        [TestCase(18, ChallengeRatingConstants.CR7, false)]
        [TestCase(18, ChallengeRatingConstants.CR8, true)]
        [TestCase(18, ChallengeRatingConstants.CR9, false)]
        [TestCase(19, ChallengeRatingConstants.CR7, false)]
        [TestCase(19, ChallengeRatingConstants.CR8, true)]
        [TestCase(19, ChallengeRatingConstants.CR9, false)]
        [TestCase(20, ChallengeRatingConstants.CR7, false)]
        [TestCase(20, ChallengeRatingConstants.CR8, true)]
        [TestCase(20, ChallengeRatingConstants.CR9, false)]
        public void IsCompatible_ChallengeRatingMustMatch(double hitDiceQuantity, string challengeRating, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            SetUpHitDice(hitDiceQuantity);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(AlignmentConstants.LawfulGood, false)]
        [TestCase(AlignmentConstants.NeutralGood, false)]
        [TestCase(AlignmentConstants.ChaoticGood, false)]
        [TestCase(AlignmentConstants.LawfulNeutral, false)]
        [TestCase(AlignmentConstants.TrueNeutral, false)]
        [TestCase(AlignmentConstants.ChaoticNeutral, false)]
        [TestCase(AlignmentConstants.LawfulEvil, false)]
        [TestCase(AlignmentConstants.NeutralEvil, true)]
        [TestCase(AlignmentConstants.ChaoticEvil, false)]
        [TestCase("wrong alignment", false)]
        public void IsCompatible_AlignmentMustMatch(string alignmentFilter, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, alignmentFilter))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            SetUpHitDice(4);

            var filters = new Filters { Alignment = alignmentFilter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(CreatureConstants.Types.Undead, ChallengeRatingConstants.CR2, AlignmentConstants.LawfulEvil, false)]
        [TestCase(CreatureConstants.Types.Undead, ChallengeRatingConstants.CR2, AlignmentConstants.NeutralEvil, true)]
        [TestCase(CreatureConstants.Types.Undead, ChallengeRatingConstants.CR1, AlignmentConstants.LawfulEvil, false)]
        [TestCase(CreatureConstants.Types.Undead, ChallengeRatingConstants.CR1, AlignmentConstants.NeutralEvil, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulEvil, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR2, AlignmentConstants.NeutralEvil, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1, AlignmentConstants.LawfulEvil, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralEvil, false)]
        public void IsCompatible_AllFiltersMustMatch(string type, string challengeRating, string alignment, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            SetUpHitDice(5);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var filters = new Filters { Alignment = alignment, Type = type, ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnsEmpty_IfAsCharacter()
        {
            var creatures = new[] { "my creature", "my other creature" };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, true);
            Assert.That(compatibleCreatures, Is.Empty);

            mockPrototypeFactory.Verify(f => f.Build(It.IsAny<IEnumerable<string>>(), It.IsAny<bool>()), Times.Never);
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnsCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "my other creature", "another creature" };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 3" };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var hitDice = SetUpHitDice();
            hitDice["my other creature"] = [new() { AmountAsDouble = 2 }];
            hitDice["another creature"] = [new() { AmountAsDouble = 3 }];

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1_3rd));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 8245 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [TestCase(AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.TrueNeutral)]
        [TestCase(AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.ChaoticEvil)]
        [TestCase("wrong alignment")]
        public void GetCompatiblePrototypes_FromNames_ReturnEmpty_WhenAlignmentFilterInvalid(string alignmentFilter)
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var hitDice = SetUpHitDice();
            hitDice["wrong creature 2"] = [new() { AmountAsDouble = 21 }];

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { Alignment = alignmentFilter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);

            mockPrototypeFactory.Verify(f => f.Build(It.IsAny<IEnumerable<string>>(), It.IsAny<bool>()), Times.Never);
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures_WhenAlignmentFilterValid()
        {
            var creatures = new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Outsider, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 21;

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { Alignment = AlignmentConstants.NeutralEvil };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1_3rd));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 8245 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1_3rd));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [TestCase(ChallengeRatingConstants.CR1_6th, 0, 0.5)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, 0.5, 1)]
        [TestCase(ChallengeRatingConstants.CR1, 1, 3)]
        [TestCase(ChallengeRatingConstants.CR2, 3, 5)]
        [TestCase(ChallengeRatingConstants.CR3, 5, 7)]
        [TestCase(ChallengeRatingConstants.CR4, 7, 9)]
        [TestCase(ChallengeRatingConstants.CR5, 9, 11)]
        [TestCase(ChallengeRatingConstants.CR6, 11, 14)]
        [TestCase(ChallengeRatingConstants.CR7, 14, 17)]
        [TestCase(ChallengeRatingConstants.CR8, 17, 20)]
        public void GetCompatiblePrototypes_FromNames_WithChallengeRating_ReturnsCompatibleCreatures(string challengeRatingFilter, double lower, double upper)
        {
            var creatures = new[] { "my creature", "my other creature", "another creature", "yet another creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 3" };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["yet another creature"] = new[] { CreatureConstants.Types.Giant };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = lower + 0.01;
            hitDice["my other creature"] = upper;
            hitDice["another creature"] = (lower + upper) / 2;
            hitDice["yet another creature"] = (lower + upper) / 2;

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("yet another creature")
                    .WithCreatureType(types["yet another creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["yet another creature"])
                    .WithAbility(AbilityConstants.Strength, 69)
                    .WithAbility(AbilityConstants.Constitution, 420)
                    .WithAbility(AbilityConstants.Dexterity, -2)
                    .WithAbility(AbilityConstants.Intelligence, 3)
                    .WithAbility(AbilityConstants.Wisdom, -4)
                    .WithAbility(AbilityConstants.Charisma, 5)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature", "yet another creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { ChallengeRating = challengeRatingFilter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(3));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(challengeRatingFilter));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(lower + 0.01));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 8245 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(challengeRatingFilter));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(upper));

            Assert.That(compatibleCreatures[2].Name, Is.EqualTo("yet another creature"));
            Assert.That(compatibleCreatures[2].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[2].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[2].Type.SubTypes, Is.Empty);
            Assert.That(compatibleCreatures[2].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 - 2 + 2));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 69));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[2].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[2].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[2].ChallengeRating, Is.EqualTo(challengeRatingFilter));
            Assert.That(compatibleCreatures[2].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[2].HitDiceQuantity, Is.EqualTo((lower + upper) / 2));
        }

        [TestCaseSource(nameof(InvalidChallengeRatings))]
        public void GetCompatiblePrototypes_FromNames_WithChallengeRating_ReturnsEmpty_IfChallengeRatingNotValidForTemplate(string challengeRatingFilter)
        {
            var creatures = new[] { "my creature", "my other creature", "another creature", "yet another creature" };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var filters = new Filters { ChallengeRating = challengeRatingFilter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);

            mockPrototypeFactory.Verify(f => f.Build(It.IsAny<IEnumerable<string>>(), It.IsAny<bool>()), Times.Never);
        }

        [TestCase(ChallengeRatingConstants.CR1_6th, 0, 0.5)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, 0.5, 1)]
        [TestCase(ChallengeRatingConstants.CR1, 1, 3)]
        [TestCase(ChallengeRatingConstants.CR2, 3, 5)]
        [TestCase(ChallengeRatingConstants.CR3, 5, 7)]
        [TestCase(ChallengeRatingConstants.CR4, 7, 9)]
        [TestCase(ChallengeRatingConstants.CR5, 9, 11)]
        [TestCase(ChallengeRatingConstants.CR6, 11, 14)]
        [TestCase(ChallengeRatingConstants.CR7, 14, 17)]
        [TestCase(ChallengeRatingConstants.CR8, 17, 20)]
        public void GetCompatiblePrototypes_FromNames_WithChallengeRating_ReturnsCompatibleCreatures_FilterOutCreaturesOutOfHitDiceRange(
            string challengeRatingFilter,
            double lower,
            double upper)
        {
            var creatures = new[] { "my creature", "my other creature", "another creature", "yet another creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 3" };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["yet another creature"] = new[] { CreatureConstants.Types.Giant };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = lower + 0.01;
            hitDice["my other creature"] = upper + 0.01;
            hitDice["another creature"] = lower;
            hitDice["yet another creature"] = upper;

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("yet another creature")
                    .WithCreatureType(types["yet another creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["yet another creature"])
                    .WithAbility(AbilityConstants.Strength, 69)
                    .WithAbility(AbilityConstants.Constitution, 420)
                    .WithAbility(AbilityConstants.Dexterity, -2)
                    .WithAbility(AbilityConstants.Intelligence, 3)
                    .WithAbility(AbilityConstants.Wisdom, -4)
                    .WithAbility(AbilityConstants.Charisma, 5)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "yet another creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { ChallengeRating = challengeRatingFilter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(challengeRatingFilter));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(lower + 0.01));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("yet another creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.Empty);
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 - 2 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 69));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(challengeRatingFilter));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(upper));

            mockTypeAndAmountSelector.Verify(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my other creature"), Times.Never);
            mockTypeAndAmountSelector.Verify(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "another creature"), Times.Never);
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_WithType_ReturnsCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "my other creature", "another creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 3" };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 2;
            hitDice["another creature"] = 1;

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { Type = CreatureConstants.Types.Undead };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1_3rd));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 8245 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
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
        public void GetCompatiblePrototypes_FromNames_WithType_ReturnsEmpty_WhenTypeFilterNotValid(string typeFilter)
        {
            var creatures = new[] { "my creature", "my other creature", "another creature" };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var filters = new Filters { Type = typeFilter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);

            mockPrototypeFactory.Verify(f => f.Build(It.IsAny<IEnumerable<string>>(), It.IsAny<bool>()), Times.Never);
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
        public void GetCompatiblePrototypes_FromNames_WithType_ReturnsCompatibleCreatures_FilterOutCreaturesWithWrongSubtype(string typeFilter)
        {
            var creatures = new[] { "my creature", "my other creature", "another creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", typeFilter };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, typeFilter };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "wrong subtype" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, typeFilter))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "another creature", "my other creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 2;
            hitDice["another creature"] = 1;

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { Type = typeFilter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                typeFilter,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1_3rd));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                typeFilter,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 8245 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_WithChallengeRatingAndType_ReturnsCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "my other creature", "another creature", "yet another creature" };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1.01;
            hitDice["my other creature"] = 2;
            hitDice["another creature"] = 1;
            hitDice["yet another creature"] = 1.5;

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 2" };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2" };
            types["yet another creature"] = new[] { CreatureConstants.Types.Giant };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR1 };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1.01));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 8245 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
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
        public void GetCompatiblePrototypes_FromNames_ReturnsCompatibleCreatures_WithValidTypes(string invalidType)
        {
            var creatures = new[] { "my creature", "my other creature", "another creature" };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "another creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1.01;
            hitDice["my other creature"] = 2;
            hitDice["another creature"] = 1.5;

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2", invalidType };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 2", invalidType };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, invalidType, "subtype 3" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("another creature")
                    .WithCreatureType(types["another creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["another creature"])
                    .WithAbility(AbilityConstants.Strength, 69)
                    .WithAbility(AbilityConstants.Constitution, 420)
                    .WithAbility(AbilityConstants.Dexterity, -2)
                    .WithAbility(AbilityConstants.Intelligence, 3)
                    .WithAbility(AbilityConstants.Wisdom, -4)
                    .WithAbility(AbilityConstants.Charisma, 5)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature", "another creature" })), false))
                .Returns(prototypes);

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(3));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1.01));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 8245 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[2].Name, Is.EqualTo("another creature"));
            Assert.That(compatibleCreatures[2].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[2].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[2].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
            }));
            Assert.That(compatibleCreatures[2].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 - 2 + 2));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 69));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[2].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[2].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[2].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[2].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[2].HitDiceQuantity, Is.EqualTo(1.5));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnsCompatibleCreatures_WithLevelAdjustments()
        {
            var creatures = new[] { "my creature", "my other creature" };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1.01;
            hitDice["my other creature"] = 2;

            mockTypeAndAmountSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my creature"])
                    .WithoutAbility(AbilityConstants.Strength)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithLevelAdjustment(0)
                    .WithCasterLevel(3)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(types["my other creature"].ToArray())
                    .WithHitDiceQuantity(hitDice["my other creature"])
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .WithLevelAdjustment(2)
                    .WithCasterLevel(4)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), false))
                .Returns(prototypes);

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1.01));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 8245 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnsEmpty_IfAsCharacter()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, true);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnsCompatibleCreatures()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithHitDiceQuantity(1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 3")
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("another creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithHitDiceQuantity(3)
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1_3rd));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [TestCase(AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.TrueNeutral)]
        [TestCase(AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.ChaoticEvil)]
        [TestCase("wrong alignment")]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnEmpty_WhenAlignmentFilterInvalid(string alignmentFilter)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 2")
                    .WithHitDiceQuantity(21)
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var filters = new Filters { Alignment = alignmentFilter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_WhenAlignmentFilterValid()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 2")
                    .WithHitDiceQuantity(21)
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var filters = new Filters { Alignment = AlignmentConstants.NeutralEvil };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1_3rd));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1_3rd));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [TestCase(ChallengeRatingConstants.CR1_6th, 0, 0.5)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, 0.5, 1)]
        [TestCase(ChallengeRatingConstants.CR1, 1, 3)]
        [TestCase(ChallengeRatingConstants.CR2, 3, 5)]
        [TestCase(ChallengeRatingConstants.CR3, 5, 7)]
        [TestCase(ChallengeRatingConstants.CR4, 7, 9)]
        [TestCase(ChallengeRatingConstants.CR5, 9, 11)]
        [TestCase(ChallengeRatingConstants.CR6, 11, 14)]
        [TestCase(ChallengeRatingConstants.CR7, 14, 17)]
        [TestCase(ChallengeRatingConstants.CR8, 17, 20)]
        public void GetCompatiblePrototypes_FromPrototypes_WithChallengeRating_ReturnsCompatibleCreatures(string challengeRatingFilter, double lower, double upper)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithHitDiceQuantity(lower + 0.01)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 3")
                    .WithHitDiceQuantity(upper)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("another creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithHitDiceQuantity((lower + upper) / 2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("yet another creature")
                    .WithCreatureType(CreatureConstants.Types.Giant)
                    .WithHitDiceQuantity((lower + upper) / 2)
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var filters = new Filters { ChallengeRating = challengeRatingFilter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(3));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(challengeRatingFilter));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(lower + 0.01));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(challengeRatingFilter));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(upper));

            Assert.That(compatibleCreatures[2].Name, Is.EqualTo("yet another creature"));
            Assert.That(compatibleCreatures[2].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[2].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[2].Type.SubTypes, Is.Empty);
            Assert.That(compatibleCreatures[2].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[2].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[2].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[2].ChallengeRating, Is.EqualTo(challengeRatingFilter));
            Assert.That(compatibleCreatures[2].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[2].HitDiceQuantity, Is.EqualTo((lower + upper) / 2));
        }

        [TestCaseSource(nameof(InvalidChallengeRatings))]
        public void GetCompatiblePrototypes_FromPrototypes_WithChallengeRating_ReturnsEmpty_IfChallengeRatingNotValidForTemplate(string challengeRatingFilter)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("another creature")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("yet another creature")
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var filters = new Filters { ChallengeRating = challengeRatingFilter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(ChallengeRatingConstants.CR1_6th, 0, 0.5)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, 0.5, 1)]
        [TestCase(ChallengeRatingConstants.CR1, 1, 3)]
        [TestCase(ChallengeRatingConstants.CR2, 3, 5)]
        [TestCase(ChallengeRatingConstants.CR3, 5, 7)]
        [TestCase(ChallengeRatingConstants.CR4, 7, 9)]
        [TestCase(ChallengeRatingConstants.CR5, 9, 11)]
        [TestCase(ChallengeRatingConstants.CR6, 11, 14)]
        [TestCase(ChallengeRatingConstants.CR7, 14, 17)]
        [TestCase(ChallengeRatingConstants.CR8, 17, 20)]
        public void GetCompatiblePrototypes_FromPrototypes_WithChallengeRating_ReturnsCompatibleCreatures_FilterOutCreaturesOutOfHitDiceRange(
            string challengeRatingFilter,
            double lower,
            double upper)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithHitDiceQuantity(lower + 0.01)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 3")
                    .WithHitDiceQuantity(upper + 0.01)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("another creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithHitDiceQuantity(lower)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("yet another creature")
                    .WithCreatureType(CreatureConstants.Types.Giant)
                    .WithHitDiceQuantity(upper)
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var filters = new Filters { ChallengeRating = challengeRatingFilter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(challengeRatingFilter));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(lower + 0.01));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("yet another creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.Empty);
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(challengeRatingFilter));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(upper));

            mockTypeAndAmountSelector.Verify(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my other creature"), Times.Never);
            mockTypeAndAmountSelector.Verify(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "another creature"), Times.Never);
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_WithType_ReturnsCompatibleCreatures()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Animal, "subtype 3")
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("another creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var filters = new Filters { Type = CreatureConstants.Types.Undead };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1_3rd));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Types.Subtypes.Evil)]
        [TestCase(CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger)]
        [TestCase(CreatureConstants.Types.Subtypes.Dwarf)]
        [TestCase(CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnoll)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnome)]
        [TestCase(CreatureConstants.Types.Subtypes.Goblinoid)]
        [TestCase(CreatureConstants.Types.Subtypes.Halfling)]
        [TestCase(CreatureConstants.Types.Subtypes.Orc)]
        [TestCase(CreatureConstants.Types.Subtypes.Reptilian)]
        public void GetCompatiblePrototypes_FromPrototypes_WithType_ReturnsEmpty_WhenTypeFilterNotValid(string typeFilter)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(typeFilter)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(typeFilter)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("another creature")
                    .WithCreatureType(typeFilter)
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var filters = new Filters { Type = typeFilter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);
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
        public void GetCompatiblePrototypes_FromPrototypes_WithType_ReturnsCompatibleCreatures_FilterOutCreaturesWithWrongSubtype(string typeFilter)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(
                        typeFilter == CreatureConstants.Types.Humanoid ? CreatureConstants.Types.Animal : CreatureConstants.Types.Humanoid,
                        "subtype 1",
                        typeFilter)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(
                        typeFilter == CreatureConstants.Types.Animal ? CreatureConstants.Types.Humanoid : CreatureConstants.Types.Animal,
                        typeFilter)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("another creature")
                    .WithCreatureType(
                        typeFilter == CreatureConstants.Types.Humanoid ? CreatureConstants.Types.Animal : CreatureConstants.Types.Humanoid,
                        "wrong subtype")
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "another creature" });

            var filters = new Filters { Type = typeFilter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                typeFilter,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1_3rd));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                typeFilter,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_WithChallengeRatingAndType_ReturnsCompatibleCreatures()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithHitDiceQuantity(1.01)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithHitDiceQuantity(2)
                    .WithCreatureType(CreatureConstants.Types.Aberration, "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("another creature")
                    .WithHitDiceQuantity(1)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("yet another creature")
                    .WithHitDiceQuantity(1.5)
                    .WithCreatureType(CreatureConstants.Types.Giant)
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR1 };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1.01));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
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
        public void GetCompatiblePrototypes_FromPrototypes_ReturnsCompatibleCreatures_WithValidTypes(string invalidType)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithHitDiceQuantity(1.01)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2", invalidType)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithHitDiceQuantity(2)
                    .WithCreatureType(CreatureConstants.Types.Aberration, "subtype 2", invalidType)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("another creature")
                    .WithHitDiceQuantity(1.5)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, invalidType, "subtype 3")
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "another creature" });

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(3));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1.01));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[2].Name, Is.EqualTo("another creature"));
            Assert.That(compatibleCreatures[2].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[2].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[2].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
            }));
            Assert.That(compatibleCreatures[2].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[2].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[2].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[2].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[2].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[2].HitDiceQuantity, Is.EqualTo(1.5));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnsCompatibleCreatures_WithImprovedTemplateAdjustments()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithHitDiceQuantity(1.01)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Dexterity, 9266, 90210)
                    .WithAbility(AbilityConstants.Strength, 96, 783)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithHitDiceQuantity(2)
                    .WithCreatureType(CreatureConstants.Types.Aberration, "subtype 2")
                    .WithAbility(AbilityConstants.Dexterity, 42, 600)
                    .WithAbility(AbilityConstants.Strength, 8245, 69)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("another creature")
                    .WithHitDiceQuantity(1)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 2")
                    .WithAbility(AbilityConstants.Dexterity, 1337, 1336)
                    .WithAbility(AbilityConstants.Strength, 420, 1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("yet another creature")
                    .WithHitDiceQuantity(1.5)
                    .WithCreatureType(CreatureConstants.Types.Giant)
                    .WithAbility(AbilityConstants.Dexterity, -2, 3)
                    .WithAbility(AbilityConstants.Strength, -4, 5)
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "another creature", "yet another creature" });

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(4));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 96 + 783));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 9266 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1.01));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 600 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 8245 + 69));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[2].Name, Is.EqualTo("another creature"));
            Assert.That(compatibleCreatures[2].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[2].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[2].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[2].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 1337 + 1336 + 2));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 420 + 1));
            Assert.That(compatibleCreatures[2].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[2].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[2].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[2].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1_3rd));
            Assert.That(compatibleCreatures[2].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[2].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[3].Name, Is.EqualTo("yet another creature"));
            Assert.That(compatibleCreatures[3].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[3].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[3].Type.SubTypes, Is.Empty);
            Assert.That(compatibleCreatures[3].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[3].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[3].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[3].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[3].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[3].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[3].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[3].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 - 2 + 3 + 2));
            Assert.That(compatibleCreatures[3].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[3].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[3].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[3].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 - 4 + 5));
            Assert.That(compatibleCreatures[3].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[3].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[3].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[3].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[3].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[3].HitDiceQuantity, Is.EqualTo(1.5));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnsCompatibleCreatures_WithLevelAdjustments()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithHitDiceQuantity(1.01)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithLevelAdjustment(0)
                    .WithCasterLevel(3)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithHitDiceQuantity(2)
                    .WithCreatureType(CreatureConstants.Types.Aberration, "subtype 2")
                    .WithLevelAdjustment(2)
                    .WithCasterLevel(4)
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, GroupConstants.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1.01));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR1));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }
    }
}
