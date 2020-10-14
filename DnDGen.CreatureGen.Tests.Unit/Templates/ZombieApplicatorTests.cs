using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
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
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;
        private Mock<Dice> mockDice;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ISavesGenerator> mockSavesGenerator;
        private Attack[] zombieAttacks;
        private IEnumerable<Feat> zombieQualities;
        private int zombieBaseAttack;
        private Mock<IHitPointsGenerator> mockHitPointsGenerator;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();
            mockDice = new Mock<Dice>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockSavesGenerator = new Mock<ISavesGenerator>();
            mockHitPointsGenerator = new Mock<IHitPointsGenerator>();

            applicator = new ZombieApplicator(
                mockCollectionSelector.Object,
                mockAdjustmentSelector.Object,
                mockDice.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSavesGenerator.Object,
                mockHitPointsGenerator.Object);

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .Build();

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity * 2)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 9266 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity * 2)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            zombieQualities = new[]
            {
                new Feat { Name = "zombie quality 1" },
                new Feat { Name = "zombie quality 2" },
                new Feat { Name = FeatConstants.Toughness, Power = 600 },
            };

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
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints))
                .Returns(zombieBaseAttack);

            zombieAttacks = new[]
            {
                new Attack
                {
                    Name = "Slam",
                    DamageRoll = "zombie damage roll",
                    Frequency = new Frequency
                    {
                        Quantity = 1,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    zombieBaseAttack,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2))
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
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { creatureType, "subtype 1", "subtype 2" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(1);

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [Test]
        public void IsCompatible_CannotBeIncorporeal()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", CreatureConstants.Types.Subtypes.Incorporeal, "subtype 2" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(1);

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.False);
        }

        [Test]
        public void IsCompatible_MustHaveSkeleton()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my other creature" });

            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(1);

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.False);
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
        public void IsCompatible_FewerThan10HitDice(double hitDice, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, CreatureConstants.Groups.HasSkeleton))
                .Returns(new[] { "my wrong creature", "my creature", "my other creature" });

            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, "my creature"))
                .Returns(hitDice);

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(compatible));
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

            var creature = applicator.ApplyTo(baseCreature);
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

            var creature = applicator.ApplyTo(baseCreature);
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

            var creature = applicator.ApplyTo(baseCreature);
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
            var newQuantity = baseCreature.HitPoints.RoundedHitDiceQuantity * 2;

            mockDice
                .Setup(d => d
                    .Roll(newQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 600, 1337 });
            mockDice
                .Setup(d => d
                    .Roll(newQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            var creature = applicator.ApplyTo(baseCreature);
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
                .Returns(new[] { 96, 1337 });
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    newRounded))
                .Returns(zombieAttacks);

            var creature = applicator.ApplyTo(baseCreature);
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
                .Returns(new[] { 96, 1337 });
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    newRounded))
                .Returns(zombieAttacks);

            var creature = applicator.ApplyTo(baseCreature);
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
                .Returns(new[] { 96, 1337 });
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    newRounded))
                .Returns(zombieAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature);
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
                .Returns(new[] { 96, 1337 });
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    newRounded))
                .Returns(zombieAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature);
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
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Value = 1337;
            baseCreature.Speeds[SpeedConstants.Fly].Description = "Superb Maneuverability (hoverboard)";

            var creature = applicator.ApplyTo(baseCreature);
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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2))
                .Returns(zombieAttacks);

            var creature = applicator.ApplyTo(baseCreature);
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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2))
                .Returns(zombieAttacks);

            var creature = applicator.ApplyTo(baseCreature);
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
            var creature = applicator.ApplyTo(baseCreature);
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
            baseCreature.Attacks = baseCreature.Attacks.Union(new[]
            {
                new Attack
                {
                    Name = "Slam",
                    DamageRoll = "damage roll",
                    Frequency = new Frequency
                    {
                        Quantity = 2,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            });

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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2))
                .Returns(zombieAttacks);

            zombieAttacks[0].DamageRoll = damage;

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(1336);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(1337);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.Not.EqualTo(zombieAttacks[0]));
            Assert.That(slam.DamageRoll, Is.EqualTo(damage));
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

            var creature = applicator.ApplyTo(baseCreature);
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
                    DamageRoll = "zombie slam roll",
                    Frequency = new Frequency { Quantity = 1, TimePeriod = FeatConstants.Frequencies.Round },
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = new List<int> { 92 }
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.Not.EqualTo(zombieAttacks[0]));
            Assert.That(slam.DamageRoll, Is.EqualTo("zombie slam roll"));
            Assert.That(slam.AttackBonuses, Has.Count.EqualTo(1).And.Contains(92));
            Assert.That(slam.Frequency.Quantity, Is.EqualTo(1));
        }

        [Test]
        public void ApplyTo_GainSlamAttacks_NoHands()
        {
            baseCreature.NumberOfHands = 0;

            var creature = applicator.ApplyTo(baseCreature);
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
            baseCreature.Attacks = baseCreature.Attacks.Union(new[]
            {
                new Attack
                {
                    Name = "Slam",
                    DamageRoll = "damage roll",
                    Frequency = new Frequency
                    {
                        Quantity = 2,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            });

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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2))
                .Returns(zombieAttacks);

            zombieAttacks[0].DamageRoll = damage;

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(1337);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(1336);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.Not.EqualTo(zombieAttacks[0]));
            Assert.That(slam.DamageRoll, Is.EqualTo("damage roll"));
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
            baseCreature.Attacks = baseCreature.Attacks.Union(new[]
            {
                specialAttack,
                new Attack { Name = "my normal attack", IsSpecial = false },
            });

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialAttacks, Is.Empty);
            Assert.That(creature.Attacks, Does.Not.Contain(specialAttack)
                .And.Not.Empty);
        }

        [Test]
        public void ApplyTo_ReplaceSpecialQualities()
        {
            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.EqualTo(zombieQualities));
        }

        [Test]
        public void ApplyTo_ReplaceSpecialQualities_KeepAttackBonuses()
        {
            var attackBonus = new Feat { Name = FeatConstants.SpecialQualities.AttackBonus, Power = 1337, Foci = new[] { "losers" } };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(new[]
            {
                attackBonus
            });

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(zombieQualities)
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(zombieQualities)
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(zombieQualities)
                .And.Contain(proficiency));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public void ApplyTo_SetSavingThrows()
        {
            var zombieSaves = new Dictionary<string, Save>();
            zombieSaves[SaveConstants.Fortitude] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Constitution],
                BaseValue = 96,
            };
            zombieSaves[SaveConstants.Reflex] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Dexterity],
                BaseValue = 1337,
            };
            zombieSaves[SaveConstants.Will] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Wisdom],
                BaseValue = 1336,
            };

            mockSavesGenerator
                .Setup(g => g.GenerateWith(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    It.Is<IEnumerable<Feat>>(ff => ff.IsEquivalentTo(baseCreature.SpecialQualities.Union(zombieQualities))),
                    baseCreature.Abilities))
                .Returns(zombieSaves);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Saves, Is.EqualTo(zombieSaves));
        }

        [Test]
        public void ApplyTo_SetAbilities()
        {
            var creature = applicator.ApplyTo(baseCreature);
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
        public void ApplyTo_LoseAllSkills()
        {
            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.Empty);
        }

        [Test]
        public void ApplyTo_LoseAllFeats()
        {
            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Is.Empty);
        }

        [TestCase(.1, ChallengeRatingConstants.OneEighth)]
        [TestCase(.25, ChallengeRatingConstants.OneEighth)]
        [TestCase(.5, ChallengeRatingConstants.OneFourth)]
        [TestCase(1, ChallengeRatingConstants.OneHalf)]
        [TestCase(2, ChallengeRatingConstants.One)]
        [TestCase(3, ChallengeRatingConstants.Two)]
        [TestCase(4, ChallengeRatingConstants.Three)]
        [TestCase(5, ChallengeRatingConstants.Three)]
        [TestCase(6, ChallengeRatingConstants.Four)]
        [TestCase(7, ChallengeRatingConstants.Four)]
        [TestCase(8, ChallengeRatingConstants.Five)]
        [TestCase(9, ChallengeRatingConstants.Six)]
        [TestCase(10, ChallengeRatingConstants.Six)]
        public void ApplyTo_AdjustChallengeRating(double hitDice, string challengeRating)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDice;
            var newRounded = Convert.ToInt32(Math.Max(1, hitDice * 2));

            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 9266 });
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    newRounded))
                .Returns(zombieAttacks);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(hitDice * 2));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating));
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo(AlignmentConstants.NeutralEvil));
        }

        [Test]
        public void ApplyTo_SetNoLevelAdjustment()
        {
            baseCreature.LevelAdjustment = 96;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.Null);
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

            var creature = await applicator.ApplyToAsync(baseCreature);
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

            var creature = await applicator.ApplyToAsync(baseCreature);
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

            var creature = await applicator.ApplyToAsync(baseCreature);
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
            var newQuantity = baseCreature.HitPoints.HitDiceQuantity * 2;
            var newRounded = baseCreature.HitPoints.RoundedHitDiceQuantity * 2;

            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 96, 1337 });
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(1336);

            var creature = await applicator.ApplyToAsync(baseCreature);
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
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Value = 1337;
            baseCreature.Speeds[SpeedConstants.Fly].Description = "Superb Maneuverability (hoverboard)";

            var creature = await applicator.ApplyToAsync(baseCreature);
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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2))
                .Returns(zombieAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature);
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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2))
                .Returns(zombieAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature);
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
            var creature = await applicator.ApplyToAsync(baseCreature);
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
            baseCreature.Attacks = baseCreature.Attacks.Union(new[]
            {
                new Attack
                {
                    Name = "Slam",
                    DamageRoll = "damage roll",
                    Frequency = new Frequency
                    {
                        Quantity = 2,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            });

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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2))
                .Returns(zombieAttacks);

            zombieAttacks[0].DamageRoll = damage;

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(1336);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(1337);

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.Not.EqualTo(zombieAttacks[0]));
            Assert.That(slam.DamageRoll, Is.EqualTo(damage));
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

            var creature = await applicator.ApplyToAsync(baseCreature);
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
                    DamageRoll = "zombie slam roll",
                    Frequency = new Frequency { Quantity = 1, TimePeriod = FeatConstants.Frequencies.Round },
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = new List<int> { 92 }
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

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.Not.EqualTo(zombieAttacks[0]));
            Assert.That(slam.DamageRoll, Is.EqualTo("zombie slam roll"));
            Assert.That(slam.AttackBonuses, Has.Count.EqualTo(1).And.Contains(92));
            Assert.That(slam.Frequency.Quantity, Is.EqualTo(1));
        }

        [Test]
        public async Task ApplyToAsync_GainSlamAttacks_NoHands()
        {
            baseCreature.NumberOfHands = 0;

            var creature = await applicator.ApplyToAsync(baseCreature);
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
            baseCreature.Attacks = baseCreature.Attacks.Union(new[]
            {
                new Attack
                {
                    Name = "Slam",
                    DamageRoll = "damage roll",
                    Frequency = new Frequency
                    {
                        Quantity = 2,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            });

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
                    SizeConstants.Medium,
                    size,
                    42,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity * 2))
                .Returns(zombieAttacks);

            zombieAttacks[0].DamageRoll = damage;

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(1337);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(1336);

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var slam = creature.Attacks.FirstOrDefault(a => a.Name == "Slam");
            Assert.That(slam, Is.Not.Null.And.Not.EqualTo(zombieAttacks[0]));
            Assert.That(slam.DamageRoll, Is.EqualTo("damage roll"));
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
            baseCreature.Attacks = baseCreature.Attacks.Union(new[]
            {
                specialAttack,
                new Attack { Name = "my normal attack", IsSpecial = false },
            });

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialAttacks, Is.Empty);
            Assert.That(creature.Attacks, Does.Not.Contain(specialAttack)
                .And.Not.Empty);
        }

        [Test]
        public async Task ApplyToAsync_ReplaceSpecialQualities()
        {
            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.EqualTo(zombieQualities));
        }

        [Test]
        public async Task ApplyToAsync_ReplaceSpecialQualities_KeepAttackBonuses()
        {
            var attackBonus = new Feat { Name = FeatConstants.SpecialQualities.AttackBonus, Power = 1337, Foci = new[] { "losers" } };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(new[]
            {
                attackBonus
            });

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(zombieQualities)
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

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(zombieQualities)
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

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(zombieQualities)
                .And.Contain(proficiency));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(4));
        }

        [Test]
        public async Task ApplyToAsync_SetSavingThrows()
        {
            var zombieSaves = new Dictionary<string, Save>();
            zombieSaves[SaveConstants.Fortitude] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Constitution],
                BaseValue = 96,
            };
            zombieSaves[SaveConstants.Reflex] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Dexterity],
                BaseValue = 1337,
            };
            zombieSaves[SaveConstants.Will] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Wisdom],
                BaseValue = 1336,
            };

            mockSavesGenerator
                .Setup(g => g.GenerateWith(
                    CreatureConstants.Templates.Zombie,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    It.Is<IEnumerable<Feat>>(ff => ff.IsEquivalentTo(baseCreature.SpecialQualities.Union(zombieQualities))),
                    baseCreature.Abilities))
                .Returns(zombieSaves);

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Saves, Is.EqualTo(zombieSaves));
        }

        [Test]
        public async Task ApplyToAsync_SetAbilities()
        {
            var creature = await applicator.ApplyToAsync(baseCreature);
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
        public async Task ApplyToAsync_LoseAllSkills()
        {
            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Skills, Is.Empty);
        }

        [Test]
        public async Task ApplyToAsync_LoseAllFeats()
        {
            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Feats, Is.Empty);
        }

        [TestCase(.1, ChallengeRatingConstants.OneEighth)]
        [TestCase(.25, ChallengeRatingConstants.OneEighth)]
        [TestCase(.5, ChallengeRatingConstants.OneFourth)]
        [TestCase(1, ChallengeRatingConstants.OneHalf)]
        [TestCase(2, ChallengeRatingConstants.One)]
        [TestCase(3, ChallengeRatingConstants.Two)]
        [TestCase(4, ChallengeRatingConstants.Three)]
        [TestCase(5, ChallengeRatingConstants.Three)]
        [TestCase(6, ChallengeRatingConstants.Four)]
        [TestCase(7, ChallengeRatingConstants.Four)]
        [TestCase(8, ChallengeRatingConstants.Five)]
        [TestCase(9, ChallengeRatingConstants.Six)]
        [TestCase(10, ChallengeRatingConstants.Six)]
        public async Task ApplyToAsync_AdjustChallengeRating(double hitDice, string challengeRating)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDice;
            var newRounded = Convert.ToInt32(Math.Max(1, hitDice * 2));

            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 9266 });
            mockDice
                .Setup(d => d
                    .Roll(newRounded)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Zombie,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    42,
                    baseCreature.Abilities,
                    newRounded))
                .Returns(zombieAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(hitDice * 2));
            Assert.That(creature.HitPoints.RoundedHitDiceQuantity, Is.EqualTo(newRounded));
            Assert.That(creature.ChallengeRating, Is.EqualTo(challengeRating));
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

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo(AlignmentConstants.NeutralEvil));
        }

        [Test]
        public async Task ApplyToAsync_SetNoLevelAdjustment()
        {
            baseCreature.LevelAdjustment = 1337;

            var creature = await applicator.ApplyToAsync(baseCreature);
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
            baseCreature.Magic.PreparedSpells = new[] { new Spell { Level = 783, Name = "my prepared spell", Source = "my prepared source" } };
            baseCreature.Magic.SpellsPerDay = new[] { new SpellQuantity { BonusSpells = 1337, Level = 1336, Quantity = 96, Source = "my per day source" } };

            var creature = applicator.ApplyTo(baseCreature);
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
            baseCreature.Magic.PreparedSpells = new[] { new Spell { Level = 783, Name = "my prepared spell", Source = "my prepared source" } };
            baseCreature.Magic.SpellsPerDay = new[] { new SpellQuantity { BonusSpells = 1337, Level = 1336, Quantity = 96, Source = "my per day source" } };

            var creature = await applicator.ApplyToAsync(baseCreature);
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

        //INFO: Since Zombies get Toughness as a bonus feat
        [Test]
        public void ApplyTo_HitDiceQuantity_RerollWithQualities()
        {
            var updatedHitPoints = new HitPoints();
            updatedHitPoints.Bonus = 600;
            updatedHitPoints.Constitution = baseCreature.HitPoints.Constitution;
            updatedHitPoints.HitDice.AddRange(baseCreature.HitPoints.HitDice);

            mockHitPointsGenerator
                .Setup(g => g.RegenerateWith(baseCreature.HitPoints, zombieQualities))
                .Returns(updatedHitPoints);

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    updatedHitPoints))
                .Returns(zombieBaseAttack);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        //INFO: Since Zombies get Toughness as a bonus feat
        [Test]
        public async Task ApplyToAsync_HitDiceQuantity_RerollWithQualities()
        {
            var updatedHitPoints = new HitPoints();
            updatedHitPoints.Bonus = 600;
            updatedHitPoints.Constitution = baseCreature.HitPoints.Constitution;
            updatedHitPoints.HitDice.AddRange(baseCreature.HitPoints.HitDice);

            mockHitPointsGenerator
                .Setup(g => g.RegenerateWith(baseCreature.HitPoints, zombieQualities))
                .Returns(updatedHitPoints);

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    updatedHitPoints))
                .Returns(zombieBaseAttack);

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints, Is.EqualTo(updatedHitPoints));
        }

        [Test]
        public void ApplyTo_SetsTemplate()
        {
            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.Zombie));
        }

        [Test]
        public async Task ApplyToAsync_SetsTemplate()
        {
            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.Zombie));
        }
    }
}
