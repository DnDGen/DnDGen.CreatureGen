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
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
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
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;
        private Mock<Dice> mockDice;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ISavesGenerator> mockSavesGenerator;
        private Attack[] skeletonAttacks;
        private IEnumerable<Feat> skeletonQualities;
        private int skeletonBaseAttack;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();
            mockDice = new Mock<Dice>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockSavesGenerator = new Mock<ISavesGenerator>();

            applicator = new SkeletonApplicator(
                mockCollectionSelector.Object,
                mockAdjustmentSelector.Object,
                mockDice.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSavesGenerator.Object);

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

            skeletonAttacks = new[]
            {
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "skeleton damage roll", Type = "skeleton damage type" }
                    },
                    Frequency = new Frequency
                    {
                        Quantity = 1,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
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
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", baseCreature.Name, "my other creature" });
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
        public void ApplyTo_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
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
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(skeletonAttacks);

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR1_3rd;
            filters.Alignment = AlignmentConstants.NeutralEvil;

            var creature = applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.Skeleton));
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
            Assert.That(creature.Type.SubTypes.ToArray(), Is.EqualTo(subtypes.Except(new[] { subtype }))
                .And.Not.Contains(subtype)
                .And.Length.EqualTo(2));
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
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Value = 600;
            baseCreature.Speeds[SpeedConstants.Fly].Description = "Superb (Magic)";

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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
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
            Assert.That(claw.DamageDescription, Is.EqualTo($"{damage} skeleton damage type"));
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
            Assert.That(claw.DamageDescription, Is.EqualTo("skeleton claw roll skeleton claw type"));
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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
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
            Assert.That(claw.DamageDescription, Is.EqualTo("damage roll damage type"));
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
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency))
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
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.ArmorProficiency))
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
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
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
                .Returns(new[] { 9266 });
            mockDice
                .Setup(d => d
                    .Roll(20)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    20))
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

            Assert.That(async () => await applicator.ApplyToAsync(baseCreature, false),
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

            Assert.That(async () => await applicator.ApplyToAsync(baseCreature, asCharacter, filters),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
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
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(skeletonAttacks);

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR1_3rd;
            filters.Alignment = AlignmentConstants.NeutralEvil;

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.Skeleton));
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
            Assert.That(creature.Type.SubTypes.ToArray(), Is.EqualTo(subtypes.Except(new[] { subtype }))
                .And.Not.Contains(subtype)
                .And.Length.EqualTo(2));
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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
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
            Assert.That(claw.DamageDescription, Is.EqualTo($"{damage} skeleton damage type"));
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
            Assert.That(claw.DamageDescription, Is.EqualTo("skeleton claw roll skeleton claw type"));
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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
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
            Assert.That(claw.DamageDescription, Is.EqualTo("damage roll damage type"));
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
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency))
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
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.ArmorProficiency))
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
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
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
                .Returns(new[] { 9266 });
            mockDice
                .Setup(d => d
                    .Roll(20)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Skeleton,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    20))
                .Returns(skeletonAttacks);

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: Creature has too many hit dice ({hitDice} > 20)");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Skeleton}");

            Assert.That(async () => await applicator.ApplyToAsync(baseCreature, false),
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
        }

        [Test]
        public void ApplyTo_SetsTemplate()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.Skeleton));
        }

        [Test]
        public async Task ApplyToAsync_SetsTemplate()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.Skeleton));
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
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 3" };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 2;
            hitDice["another creature"] = 3;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 21;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 21;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = lower + 0.01;
            hitDice["my other creature"] = upper;
            hitDice["another creature"] = (lower + upper) / 2;
            hitDice["yet another creature"] = (lower + upper) / 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = lower + 0.01;
            hitDice["my other creature"] = upper + 0.01;
            hitDice["another creature"] = lower;
            hitDice["yet another creature"] = upper;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { ChallengeRating = challengeRatingFilter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "yet another creature" }));

            mockAdjustmentSelector.Verify(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my other creature"), Times.Never);
            mockAdjustmentSelector.Verify(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "another creature"), Times.Never);
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 2;
            hitDice["another creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Type = CreatureConstants.Types.Undead };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, typeFilter))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 2;
            hitDice["another creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Type = typeFilter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_WithChallengeRatingAndType_ReturnsCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "my other creature", "another creature", "yet another creature" };

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature", "yet another creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1.01;
            hitDice["my other creature"] = 2;
            hitDice["another creature"] = 1;
            hitDice["yet another creature"] = 1.5;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Animal, "subtype 2" };
            types["another creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2" };
            types["yet another creature"] = new[] { CreatureConstants.Types.Giant };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

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
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { creatureType, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [Test]
        public void IsCompatible_CannotBeIncorporeal()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", CreatureConstants.Types.Subtypes.Incorporeal, "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void IsCompatible_MustHaveSkeleton()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my other creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void IsCompatible_CannotBeCharacter()
        {
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

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
            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
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
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, type))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

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
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

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
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, alignmentFilter))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

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
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 5;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var filters = new Filters { Alignment = alignment, Type = type, ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }
    }
}
