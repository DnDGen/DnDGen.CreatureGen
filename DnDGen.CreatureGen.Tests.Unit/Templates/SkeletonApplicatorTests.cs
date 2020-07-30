using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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
        [TestCase(10, true)]
        [TestCase(19, true)]
        [TestCase(20, true)]
        [TestCase(21, false)]
        [TestCase(22, false)]
        [TestCase(42, false)]
        public void IsCompatible_FewerThan20HitDice(double hitDice, bool compatible)
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
            baseCreature.HitPoints.HitDie = hitDie;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 9266, 90210 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(42);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [Test]
        public void ApplyTo_LoseFlySpeed_Wings()
        {
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Value = 9266;
            baseCreature.Speeds[SpeedConstants.Fly].Description = "Superb (Wings)";

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Speeds, Is.Not.Empty
                .And.Not.ContainKey(SpeedConstants.Fly));
        }

        [Test]
        public void ApplyTo_KeepFlySpeed_Magic()
        {
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Value = 9266;
            baseCreature.Speeds[SpeedConstants.Fly].Description = "Superb (Magic)";

            var creature = applicator.ApplyTo(baseCreature);
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
        [TestCase(SizeConstants.Large, 2)]
        [TestCase(SizeConstants.Huge, 3)]
        [TestCase(SizeConstants.Gargantuan, 6)]
        [TestCase(SizeConstants.Colossal, 10)]
        public void ApplyTo_ReplacesNaturalArmorBasedOnSize(string size, int bonus)
        {
            baseCreature.Size = size;
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 666);

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
            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints))
                .Returns(9266);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.BaseAttackBonus, Is.EqualTo(9266));
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
                    DamageRoll = "damage roll",
                    Frequency = new Frequency
                    {
                        Quantity = 2,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            });

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints))
                .Returns(9266);

            var skeletonAttacks = new[]
            {
                new Attack
                {
                    Name = "Claw",
                    DamageRoll = damage,
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
                    size,
                    9266,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(skeletonAttacks);

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(42);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(90210);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Not.Null.And.Not.EqualTo(skeletonAttacks[0]));
            Assert.That(claw.DamageRoll, Is.EqualTo(damage));
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

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints))
                .Returns(9266);

            var skeletonAttacks = new[]
            {
                new Attack
                {
                    Name = "Claw",
                    DamageRoll = "skeleton damage",
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
                    9266,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(skeletonAttacks);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Not.Null.And.Not.EqualTo(skeletonAttacks[0]));
            Assert.That(claw.DamageRoll, Is.EqualTo("skeleton damage"));
            Assert.That(claw.Frequency.Quantity, Is.EqualTo(numberOfHands));
        }

        [Test]
        public void ApplyTo_GainClawAttacks_WithAttackBonuses()
        {
            baseCreature.NumberOfHands = 2;

            var skeletonQualities = new[]
            {
                new Feat { Name = "skeleton quality 1" },
                new Feat { Name = "skeleton quality 2" },
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

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints))
                .Returns(9266);

            var skeletonAttacks = new[]
            {
                new Attack
                {
                    Name = "Claw",
                    DamageRoll = "skeleton damage",
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
                    9266,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(skeletonAttacks);

            var attacksWithBonuses = new[]
            {
                new Attack { Name = "Claw", DamageRoll = "skeleton claw roll", IsSpecial = false, IsMelee = true, AttackBonuses = new List<int> { 92 } },
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Not.Null.And.Not.EqualTo(skeletonAttacks[0]));
            Assert.That(claw.DamageRoll, Is.EqualTo("skeleton claw roll"));
            Assert.That(claw.AttackBonuses, Has.Count.EqualTo(1).And.Contains(92));
            Assert.That(claw.Frequency.Quantity, Is.EqualTo(2));
        }

        [Test]
        public void ApplyTo_DoesNotGainClawAttacks_NoHands()
        {
            baseCreature.NumberOfHands = 0;

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints))
                .Returns(9266);

            var skeletonAttacks = new[]
            {
                new Attack
                {
                    Name = "Claw",
                    DamageRoll = "skeleton damage",
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
                    9266,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(skeletonAttacks);

            var creature = applicator.ApplyTo(baseCreature);
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
                    DamageRoll = "damage roll",
                    Frequency = new Frequency
                    {
                        Quantity = 2,
                        TimePeriod = FeatConstants.Frequencies.Round,
                    }
                }
            });

            mockAttacksGenerator
                .Setup(g => g.GenerateBaseAttackBonus(
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints))
                .Returns(9266);

            var skeletonAttacks = new[]
            {
                new Attack
                {
                    Name = "Claw",
                    DamageRoll = damage,
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
                    size,
                    9266,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(skeletonAttacks);

            mockDice
                .Setup(d => d.Roll("damage roll").AsPotentialMaximum<int>(true))
                .Returns(90210);

            mockDice
                .Setup(d => d.Roll(damage).AsPotentialMaximum<int>(true))
                .Returns(42);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));

            var claw = creature.Attacks.FirstOrDefault(a => a.Name == "Claw");
            Assert.That(claw, Is.Not.Null.And.Not.EqualTo(skeletonAttacks[0]));
            Assert.That(claw.DamageRoll, Is.EqualTo("damage roll"));
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
                new Attack { Name = "my nroaml attack", IsSpecial = false },
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
            var skeletonQualities = new[]
            {
                new Feat { Name = "skeleton quality 1" },
                new Feat { Name = "skeleton quality 2" },
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.EqualTo(skeletonQualities));
        }

        [Test]
        public void ApplyTo_ReplaceSpecialQualities_KeepAttackBonuses()
        {
            var attackBonus = new Feat { Name = FeatConstants.SpecialQualities.AttackBonus, Power = 9266, Foci = new[] { "losers" } };
            baseCreature.SpecialQualities = baseCreature.SpecialQualities.Union(new[]
            {
                attackBonus
            });

            var skeletonQualities = new[]
            {
                new Feat { Name = "skeleton quality 1" },
                new Feat { Name = "skeleton quality 2" },
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(skeletonQualities)
                .And.Contain(attackBonus));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(3));
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

            var skeletonQualities = new[]
            {
                new Feat { Name = "skeleton quality 1" },
                new Feat { Name = "skeleton quality 2" },
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(skeletonQualities)
                .And.Contain(proficiency));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(3));
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

            var skeletonQualities = new[]
            {
                new Feat { Name = "skeleton quality 1" },
                new Feat { Name = "skeleton quality 2" },
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(skeletonQualities)
                .And.Contain(proficiency));
            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(3));
        }

        [Test]
        public void ApplyTo_SetSavingThrows()
        {
            var skeletonSaves = new Dictionary<string, Save>();
            skeletonSaves[SaveConstants.Fortitude] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Constitution],
                BaseValue = 9266,
            };
            skeletonSaves[SaveConstants.Reflex] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Dexterity],
                BaseValue = 90210,
            };
            skeletonSaves[SaveConstants.Will] = new Save
            {
                BaseAbility = baseCreature.Abilities[AbilityConstants.Wisdom],
                BaseValue = 42,
            };

            mockSavesGenerator
                .Setup(g => g.GenerateWith(
                    CreatureConstants.Templates.Skeleton,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    Enumerable.Empty<Feat>(),
                    baseCreature.Abilities))
                .Returns(skeletonSaves);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Saves, Is.EqualTo(skeletonSaves));
        }

        [Test]
        public void ApplyTo_SetAbilities()
        {
            var wisdomOffset = baseCreature.Abilities[AbilityConstants.Wisdom].FullScore;
            var charismaOffset = baseCreature.Abilities[AbilityConstants.Charisma].FullScore;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(int.MinValue));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(int.MinValue));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.EqualTo(10 - wisdomOffset));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(1 - charismaOffset));

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

        [TestCase(.1, ChallengeRatingConstants.OneSixth)]
        [TestCase(.25, ChallengeRatingConstants.OneSixth)]
        [TestCase(.5, ChallengeRatingConstants.OneSixth)]
        [TestCase(1, ChallengeRatingConstants.OneThird)]
        [TestCase(2, ChallengeRatingConstants.One)]
        [TestCase(3, ChallengeRatingConstants.One)]
        [TestCase(4, ChallengeRatingConstants.Two)]
        [TestCase(5, ChallengeRatingConstants.Two)]
        [TestCase(6, ChallengeRatingConstants.Three)]
        [TestCase(7, ChallengeRatingConstants.Three)]
        [TestCase(8, ChallengeRatingConstants.Four)]
        [TestCase(9, ChallengeRatingConstants.Four)]
        [TestCase(10, ChallengeRatingConstants.Five)]
        [TestCase(11, ChallengeRatingConstants.Five)]
        [TestCase(12, ChallengeRatingConstants.Six)]
        [TestCase(13, ChallengeRatingConstants.Six)]
        [TestCase(14, ChallengeRatingConstants.Six)]
        [TestCase(15, ChallengeRatingConstants.Seven)]
        [TestCase(16, ChallengeRatingConstants.Seven)]
        [TestCase(17, ChallengeRatingConstants.Seven)]
        [TestCase(18, ChallengeRatingConstants.Eight)]
        [TestCase(19, ChallengeRatingConstants.Eight)]
        [TestCase(20, ChallengeRatingConstants.Eight)]
        public void ApplyTo_AdjustChallengeRating(double hitDice, string challengeRating)
        {
            baseCreature.HitPoints.HitDiceQuantity = hitDice;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
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
            baseCreature.LevelAdjustment = 9266;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.Null);
        }

        [Test]
        public async Task ApplyToAsync_()
        {
            Assert.Fail("need to copy");
        }
    }
}
