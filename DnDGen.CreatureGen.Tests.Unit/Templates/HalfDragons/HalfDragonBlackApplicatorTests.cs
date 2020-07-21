using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Languages;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Templates.HalfDragons;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates.HalfDragons
{
    [TestFixture]
    public class HalfDragonBlackApplicatorTests
    {
        private TemplateApplicator applicator;
        private Creature baseCreature;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<Dice> mockDice;
        private Mock<ISpeedsGenerator> mockSpeedsGenerator;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ISkillsGenerator> mockSkillsGenerator;
        private Mock<IAlignmentGenerator> mockAlignmentGenerator;

        private const string template = CreatureConstants.Templates.HalfDragon_Black;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockDice = new Mock<Dice>();
            mockSpeedsGenerator = new Mock<ISpeedsGenerator>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockSkillsGenerator = new Mock<ISkillsGenerator>();
            mockAlignmentGenerator = new Mock<IAlignmentGenerator>();

            applicator = new HalfDragonBlackApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object);

            baseCreature = new CreatureBuilder().WithTestValues().Build();
            baseCreature.HitPoints.HitDie = 8;

            var speeds = new Dictionary<string, Measurement>();
            speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            speeds[SpeedConstants.Fly].Description = "the goodest";
            speeds[SpeedConstants.Fly].Value = 666;

            mockSpeedsGenerator.Setup(g => g.Generate(template)).Returns(speeds);

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(10)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 9266 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(10)
                    .AsPotentialAverage())
                .Returns(90210);

            var dragonAlignment = new Alignment { Lawfulness = "dragony", Goodness = "scaley" };
            mockAlignmentGenerator
                .Setup(g => g.Generate(template))
                .Returns(dragonAlignment);
        }

        [TestCase(CreatureConstants.Types.Aberration, true)]
        [TestCase(CreatureConstants.Types.Animal, true)]
        [TestCase(CreatureConstants.Types.Construct, false)]
        [TestCase(CreatureConstants.Types.Dragon, false)]
        [TestCase(CreatureConstants.Types.Elemental, true)]
        [TestCase(CreatureConstants.Types.Fey, true)]
        [TestCase(CreatureConstants.Types.Giant, true)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, true)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, true)]
        [TestCase(CreatureConstants.Types.Ooze, true)]
        [TestCase(CreatureConstants.Types.Outsider, true)]
        [TestCase(CreatureConstants.Types.Plant, true)]
        [TestCase(CreatureConstants.Types.Undead, false)]
        [TestCase(CreatureConstants.Types.Vermin, true)]
        public void IsCompatible_BasedOnCreatureType(string creatureType, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { creatureType, "subtype 1", "subtype 2" });

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [Test]
        public void IsCompatible_CannotBeIncorporeal()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", CreatureConstants.Types.Subtypes.Incorporeal, "subtype 2" });

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
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
        public void ApplyTo_CreatureTypeIsAdjusted(string original)
        {
            baseCreature.Type.Name = original;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(2));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2"));
        }

        [TestCase(4, 6)]
        [TestCase(6, 8)]
        [TestCase(8, 10)]
        [TestCase(10, 12)]
        [TestCase(12, 12)]
        public void ApplyTo_HitDieIncreases(int original, int increased)
        {
            baseCreature.HitPoints.HitDie = original;
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = -4;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 2;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(increased)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 9266, 90210 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(increased)
                    .AsPotentialAverage())
                .Returns(42);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(increased));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [TestCase(4, 6)]
        [TestCase(6, 8)]
        [TestCase(8, 10)]
        [TestCase(10, 12)]
        [TestCase(12, 12)]
        public void ApplyTo_HitDieIncreases_WithBoostedConstitution(int original, int increased)
        {
            baseCreature.HitPoints.HitDie = original;
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 42;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(increased)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 9266, 90210 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(increased)
                    .AsPotentialAverage())
                .Returns(96);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(increased));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210 + 2 * 17));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(96 + baseCreature.HitPoints.RoundedHitDiceQuantity * 17));
        }

        [TestCase(SizeConstants.Large)]
        [TestCase(SizeConstants.Huge)]
        [TestCase(SizeConstants.Gargantuan)]
        [TestCase(SizeConstants.Colossal)]
        public void ApplyTo_GainFlySpeed(string size)
        {
            baseCreature.Size = size;
            baseCreature.Speeds[SpeedConstants.Land].Value = 42;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(42 * 2));
        }

        [TestCase(SizeConstants.Fine)]
        [TestCase(SizeConstants.Diminutive)]
        [TestCase(SizeConstants.Tiny)]
        [TestCase(SizeConstants.Small)]
        [TestCase(SizeConstants.Medium)]
        public void ApplyTo_DoNotGainFlySpeed(string size)
        {
            baseCreature.Size = size;
            baseCreature.Speeds[SpeedConstants.Land].Value = 42;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Speeds, Has.Count.EqualTo(1)
                .And.Not.ContainKey(SpeedConstants.Fly));
        }

        [TestCase(5, 10)]
        [TestCase(10, 20)]
        [TestCase(20, 40)]
        [TestCase(30, 60)]
        [TestCase(40, 80)]
        [TestCase(50, 100)]
        [TestCase(60, 120)]
        [TestCase(70, 120)]
        [TestCase(80, 120)]
        [TestCase(90, 120)]
        [TestCase(100, 120)]
        public void ApplyTo_GainFlySpeed_TwiceLandSpeed(int landSpeed, int flySpeed)
        {
            baseCreature.Size = SizeConstants.Large;
            baseCreature.Speeds[SpeedConstants.Land].Value = landSpeed;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(flySpeed));
        }

        [Test]
        public void ApplyTo_AdjustAbilities()
        {
            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(8));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(2));
        }

        [TestCase(AbilityConstants.Charisma)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Wisdom)]
        public void ApplyTo_AbilitiesImprove_DoNotImproveMissingAbility(string ability)
        {
            baseCreature.Abilities[ability].BaseScore = 0;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Abilities[ability].HasScore, Is.False);
            Assert.That(creature.Abilities[ability].TemplateAdjustment, Is.Zero);
        }

        [Test]
        public void ApplyTo_GainNaturalArmor()
        {
            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(4));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(4));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public void ApplyTo_ImproveNaturalArmor()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(9270));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public void ApplyTo_ImprovesBestNaturalArmorBonus()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 90210);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(90214));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.False);

            bonus = creature.ArmorClass.NaturalArmorBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(90214));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public void ApplyTo_ImproveNaturalArmor_PreserveConditions()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266, "only sometimes");

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Condition, Is.EqualTo("only sometimes"));
        }

        [Test]
        public void ApplyTo_GainAttacks()
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true },
                new Attack { Name = "Bite", DamageRoll = "dragon bite roll", IsSpecial = false, IsMelee = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(newAttacks));
        }

        [Test]
        public void ApplyTo_GainAttacks_WithAttackBonuses()
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true },
                new Attack { Name = "Bite", DamageRoll = "dragon bite roll", IsSpecial = false, IsMelee = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            var newQualities = new[]
            {
                new Feat { Name = "half-dragon quality 1" },
                new Feat { Name = "half-dragon quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    template,
                    baseCreature.Type,
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = "dragony", Goodness = "scaley" }))
                .Returns(newQualities);

            var attacksWithBonuses = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true, AttackBonuses = new List<int> { 92 } },
                new Attack { Name = "Bite", DamageRoll = "dragon bite roll", IsSpecial = false, IsMelee = true, AttackBonuses = new List<int> { -66 } },
            };

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.Is<IEnumerable<Feat>>(f =>
                        f.IsEquivalentTo(baseCreature.Feats
                            .Union(baseCreature.SpecialQualities)
                            .Union(newQualities))),
                    baseCreature.Abilities))
                .Returns(attacksWithBonuses);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + attacksWithBonuses.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(attacksWithBonuses));
        }

        [TestCase(9266, 90210, "dragon claw roll")]
        [TestCase(9266, 42, "base claw roll")]
        public void ApplyTo_GainAttacks_DuplicateClawAttacks(int baseClawMax, int dragonClawMax, string expectedClawDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true },
                new Attack { Name = "Bite", DamageRoll = "dragon bite roll", IsSpecial = false, IsMelee = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var claw = new Attack { Name = "Claw", DamageRoll = "base claw roll", IsSpecial = false, IsMelee = true };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { claw });

            mockDice
                .Setup(d => d.Roll("base claw roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("dragon claw roll").AsPotentialMaximum<int>(true))
                .Returns(dragonClawMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageRoll, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(newAttacks[3]));
            Assert.That(bites[0].DamageRoll, Is.EqualTo("dragon bite roll"));
        }

        [TestCase(9266, 90210, "dragon bite roll")]
        [TestCase(9266, 42, "base bite roll")]
        public void ApplyTo_GainAttacks_DuplicateBiteAttack(int baseBiteMax, int dragonBiteMax, string expectedBiteDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true },
                new Attack { Name = "Bite", DamageRoll = "dragon bite roll", IsSpecial = false, IsMelee = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var bite = new Attack { Name = "Bite", DamageRoll = "base bite roll", IsSpecial = false, IsMelee = true };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { bite });

            mockDice
                .Setup(d => d.Roll("base bite roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("dragon bite roll").AsPotentialMaximum<int>(true))
                .Returns(dragonBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(newAttacks[2]));
            Assert.That(claws[0].DamageRoll, Is.EqualTo("dragon claw roll"));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageRoll, Is.EqualTo(expectedBiteDamage));
        }

        [TestCase(9266, 90210, "dragon claw roll", 1336, 1337, "dragon bite roll")]
        [TestCase(9266, 90210, "dragon claw roll", 1336, 600, "base bite roll")]
        [TestCase(9266, 42, "base claw roll", 1336, 1337, "dragon bite roll")]
        [TestCase(9266, 42, "base claw roll", 1336, 600, "base bite roll")]
        public void ApplyTo_GainAttacks_DuplicateClawAndBiteAttack(int baseClawMax, int dragonClawMax, string expectedClawDamage, int baseBiteMax, int dragonBiteMax, string expectedBiteDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true },
                new Attack { Name = "Bite", DamageRoll = "dragon bite roll", IsSpecial = false, IsMelee = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var claw = new Attack { Name = "Claw", DamageRoll = "base claw roll", IsSpecial = false, IsMelee = true };
            var bite = new Attack { Name = "Bite", DamageRoll = "base bite roll", IsSpecial = false, IsMelee = true };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { claw, bite });

            mockDice
                .Setup(d => d.Roll("base claw roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("dragon claw roll").AsPotentialMaximum<int>(true))
                .Returns(dragonClawMax);
            mockDice
                .Setup(d => d.Roll("base bite roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("dragon bite roll").AsPotentialMaximum<int>(true))
                .Returns(dragonBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature);
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 2));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageRoll, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageRoll, Is.EqualTo(expectedBiteDamage));
        }

        [Test]
        public void ApplyTo_GainSpecialQualities()
        {
            var newQualities = new[]
            {
                new Feat { Name = "half-dragon quality 1" },
                new Feat { Name = "half-dragon quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    template,
                    baseCreature.Type,
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = "dragony", Goodness = "scaley" }))
                .Returns(newQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = applicator.ApplyTo(baseCreature);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + newQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(newQualities));
        }

        [Test]
        public void ApplyTo_GainsSkillPoints()
        {
            var ranks = 1;
            foreach (var skill in baseCreature.Skills)
            {
                skill.Ranks = ranks++ % skill.RankCap;
            }

            var newSkills = new[]
            {
                new Skill("my skill", baseCreature.Abilities[AbilityConstants.Strength], 9266) { Ranks = 42 },
                new Skill("my other skill", baseCreature.Abilities[AbilityConstants.Intelligence], 90210) { Ranks = 600 },
            };
            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.Is<IEnumerable<Skill>>(ss =>
                        ss == baseCreature.Skills
                        && ss.All(s => s.Ranks == 0)),
                    baseCreature.HitPoints,
                    baseCreature.Type,
                    baseCreature.Abilities))
                .Returns(newSkills);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Skills, Is.EqualTo(newSkills));
        }

        [Test]
        public void ApplyTo_GainsSkillPoints_NoSkills()
        {
            baseCreature.Skills = Enumerable.Empty<Skill>();

            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.Is<IEnumerable<Skill>>(ss =>
                        ss == baseCreature.Skills
                        && !ss.Any()),
                    baseCreature.HitPoints,
                    baseCreature.Type,
                    baseCreature.Abilities))
                .Returns(Enumerable.Empty<Skill>());

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Skills, Is.Empty);
        }

        [TestCaseSource("ChallengeRatingAdjustments")]
        public void ApplyTo_ChallengeRatingAdjusted(string original, string adjusted)
        {
            baseCreature.ChallengeRating = original;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        private static IEnumerable ChallengeRatingAdjustments
        {
            get
            {
                var challengeRatings = ChallengeRatingConstants.GetOrdered();
                var minIndex = Array.IndexOf(challengeRatings, ChallengeRatingConstants.Three);

                for (var i = 0; i < challengeRatings.Length - 2; i++)
                {
                    if (i + 2 < minIndex)
                        yield return new TestCaseData(challengeRatings[i], ChallengeRatingConstants.Three);
                    else
                        yield return new TestCaseData(challengeRatings[i], challengeRatings[i + 2]);
                }
            }
        }

        [Test]
        public void ApplyTo_GetNewAlignment()
        {
            var dragonAlignment = new Alignment { Lawfulness = "dragony", Goodness = "scaley" };
            mockAlignmentGenerator
                .Setup(g => g.Generate(template))
                .Returns(dragonAlignment);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Alignment, Is.EqualTo(dragonAlignment));
        }

        [TestCase(null, null)]
        [TestCase(0, 3)]
        [TestCase(1, 4)]
        [TestCase(2, 5)]
        [TestCase(10, 13)]
        [TestCase(42, 45)]
        public void ApplyTo_LevelAdjustmentIncreased(int? adjustment, int? adjusted)
        {
            baseCreature.LevelAdjustment = adjustment;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
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
        public async Task ApplyToAsync_CreatureTypeIsAdjusted(string original)
        {
            baseCreature.Type.Name = original;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(2));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2"));
        }

        [TestCase(4, 6)]
        [TestCase(6, 8)]
        [TestCase(8, 10)]
        [TestCase(10, 12)]
        [TestCase(12, 12)]
        public async Task ApplyToAsync_HitDieIncreases(int original, int increased)
        {
            baseCreature.HitPoints.HitDie = original;
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = -4;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 2;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(increased)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 9266, 90210 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(increased)
                    .AsPotentialAverage())
                .Returns(42);

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(increased));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [TestCase(4, 6)]
        [TestCase(6, 8)]
        [TestCase(8, 10)]
        [TestCase(10, 12)]
        [TestCase(12, 12)]
        public async Task ApplyToAsync_HitDieIncreases_WithBoostedConstitution(int original, int increased)
        {
            baseCreature.HitPoints.HitDie = original;
            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 42;
            baseCreature.Abilities[AbilityConstants.Constitution].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Constitution].AdvancementAdjustment = 0;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(increased)
                    .AsIndividualRolls<int>())
                .Returns(new[] { 9266, 90210 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(increased)
                    .AsPotentialAverage())
                .Returns(96);

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(increased));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210 + 2 * 17));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(96 + baseCreature.HitPoints.RoundedHitDiceQuantity * 17));
        }

        [TestCase(SizeConstants.Large)]
        [TestCase(SizeConstants.Huge)]
        [TestCase(SizeConstants.Gargantuan)]
        [TestCase(SizeConstants.Colossal)]
        public async Task ApplyToAsync_GainFlySpeed(string size)
        {
            baseCreature.Size = size;
            baseCreature.Speeds[SpeedConstants.Land].Value = 42;

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(42 * 2));
        }

        [TestCase(SizeConstants.Fine)]
        [TestCase(SizeConstants.Diminutive)]
        [TestCase(SizeConstants.Tiny)]
        [TestCase(SizeConstants.Small)]
        [TestCase(SizeConstants.Medium)]
        public async Task ApplyToAsync_DoNotGainFlySpeed(string size)
        {
            baseCreature.Size = size;
            baseCreature.Speeds[SpeedConstants.Land].Value = 42;

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.Speeds, Has.Count.EqualTo(1)
                .And.Not.ContainKey(SpeedConstants.Fly));
        }

        [TestCase(5, 10)]
        [TestCase(10, 20)]
        [TestCase(20, 40)]
        [TestCase(30, 60)]
        [TestCase(40, 80)]
        [TestCase(50, 100)]
        [TestCase(60, 120)]
        [TestCase(70, 120)]
        [TestCase(80, 120)]
        [TestCase(90, 120)]
        [TestCase(100, 120)]
        public async Task ApplyToAsync_GainFlySpeed_TwiceLandSpeed(int landSpeed, int flySpeed)
        {
            baseCreature.Size = SizeConstants.Large;
            baseCreature.Speeds[SpeedConstants.Land].Value = landSpeed;

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(flySpeed));
        }

        [Test]
        public async Task ApplyToAsync_AdjustAbilities()
        {
            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(8));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(2));
        }

        [TestCase(AbilityConstants.Charisma)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Wisdom)]
        public async Task ApplyToAsync_AbilitiesImprove_DoNotImproveMissingAbility(string ability)
        {
            baseCreature.Abilities[ability].BaseScore = 0;

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.Abilities[ability].HasScore, Is.False);
            Assert.That(creature.Abilities[ability].TemplateAdjustment, Is.Zero);
        }

        [Test]
        public async Task ApplyToAsync_GainNaturalArmor()
        {
            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(4));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(4));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public async Task ApplyToAsync_ImproveNaturalArmor()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(9270));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public async Task ApplyToAsync_ImprovesBestNaturalArmorBonus()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 90210);

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(90214));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.False);

            bonus = creature.ArmorClass.NaturalArmorBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(90214));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public async Task ApplyToAsync_ImproveNaturalArmor_PreserveConditions()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266, "only sometimes");

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Condition, Is.EqualTo("only sometimes"));
        }

        [Test]
        public async Task ApplyToAsync_GainAttacks()
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true },
                new Attack { Name = "Bite", DamageRoll = "dragon bite roll", IsSpecial = false, IsMelee = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicator.ApplyToAsync(baseCreature);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(newAttacks));
        }

        [Test]
        public async Task ApplyToAsync_GainAttacks_WithAttackBonuses()
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true },
                new Attack { Name = "Bite", DamageRoll = "dragon bite roll", IsSpecial = false, IsMelee = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            var newQualities = new[]
            {
                new Feat { Name = "half-dragon quality 1" },
                new Feat { Name = "half-dragon quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    template,
                    baseCreature.Type,
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = "dragony", Goodness = "scaley" }))
                .Returns(newQualities);

            var attacksWithBonuses = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true, AttackBonuses = new List<int> { 92 } },
                new Attack { Name = "Bite", DamageRoll = "dragon bite roll", IsSpecial = false, IsMelee = true, AttackBonuses = new List<int> { -66 } },
            };

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.Is<IEnumerable<Feat>>(f =>
                        f.IsEquivalentTo(baseCreature.Feats
                            .Union(baseCreature.SpecialQualities)
                            .Union(newQualities))),
                    baseCreature.Abilities))
                .Returns(attacksWithBonuses);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicator.ApplyToAsync(baseCreature);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + attacksWithBonuses.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(attacksWithBonuses));
        }

        [TestCase(9266, 90210, "dragon claw roll")]
        [TestCase(9266, 42, "base claw roll")]
        public async Task ApplyToAsync_GainAttacks_DuplicateClawAttacks(int baseClawMax, int dragonClawMax, string expectedClawDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true },
                new Attack { Name = "Bite", DamageRoll = "dragon bite roll", IsSpecial = false, IsMelee = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var claw = new Attack { Name = "Claw", DamageRoll = "base claw roll", IsSpecial = false, IsMelee = true };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { claw });

            mockDice
                .Setup(d => d.Roll("base claw roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("dragon claw roll").AsPotentialMaximum<int>(true))
                .Returns(dragonClawMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicator.ApplyToAsync(baseCreature);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageRoll, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(newAttacks[3]));
            Assert.That(bites[0].DamageRoll, Is.EqualTo("dragon bite roll"));
        }

        [TestCase(9266, 90210, "dragon bite roll")]
        [TestCase(9266, 42, "base bite roll")]
        public async Task ApplyToAsync_GainAttacks_DuplicateBiteAttack(int baseBiteMax, int dragonBiteMax, string expectedBiteDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true },
                new Attack { Name = "Bite", DamageRoll = "dragon bite roll", IsSpecial = false, IsMelee = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var bite = new Attack { Name = "Bite", DamageRoll = "base bite roll", IsSpecial = false, IsMelee = true };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { bite });

            mockDice
                .Setup(d => d.Roll("base bite roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("dragon bite roll").AsPotentialMaximum<int>(true))
                .Returns(dragonBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicator.ApplyToAsync(baseCreature);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(newAttacks[2]));
            Assert.That(claws[0].DamageRoll, Is.EqualTo("dragon claw roll"));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageRoll, Is.EqualTo(expectedBiteDamage));
        }

        [TestCase(9266, 90210, "dragon claw roll", 1336, 1337, "dragon bite roll")]
        [TestCase(9266, 90210, "dragon claw roll", 1336, 600, "base bite roll")]
        [TestCase(9266, 42, "base claw roll", 1336, 1337, "dragon bite roll")]
        [TestCase(9266, 42, "base claw roll", 1336, 600, "base bite roll")]
        public async Task ApplyToAsync_GainAttacks_DuplicateClawAndBiteAttack(int baseClawMax, int dragonClawMax, string expectedClawDamage, int baseBiteMax, int dragonBiteMax, string expectedBiteDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true },
                new Attack { Name = "Bite", DamageRoll = "dragon bite roll", IsSpecial = false, IsMelee = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    template,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var claw = new Attack { Name = "Claw", DamageRoll = "base claw roll", IsSpecial = false, IsMelee = true };
            var bite = new Attack { Name = "Bite", DamageRoll = "base bite roll", IsSpecial = false, IsMelee = true };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { claw, bite });

            mockDice
                .Setup(d => d.Roll("base claw roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("dragon claw roll").AsPotentialMaximum<int>(true))
                .Returns(dragonClawMax);
            mockDice
                .Setup(d => d.Roll("base bite roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("dragon bite roll").AsPotentialMaximum<int>(true))
                .Returns(dragonBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicator.ApplyToAsync(baseCreature);
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 2));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageRoll, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageRoll, Is.EqualTo(expectedBiteDamage));
        }

        [Test]
        public async Task ApplyToAsync_GainSpecialQualities()
        {
            var newQualities = new[]
            {
                new Feat { Name = "half-dragon quality 1" },
                new Feat { Name = "half-dragon quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    template,
                    baseCreature.Type,
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = "dragony", Goodness = "scaley" }))
                .Returns(newQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = await applicator.ApplyToAsync(baseCreature);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + newQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(newQualities));
        }

        [Test]
        public async Task ApplyToAsync_GainsSkillPoints()
        {
            var ranks = 1;
            foreach (var skill in baseCreature.Skills)
            {
                skill.Ranks = ranks++ % skill.RankCap;
            }

            var newSkills = new[]
            {
                new Skill("my skill", baseCreature.Abilities[AbilityConstants.Strength], 9266) { Ranks = 42 },
                new Skill("my other skill", baseCreature.Abilities[AbilityConstants.Intelligence], 90210) { Ranks = 600 },
            };
            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.Is<IEnumerable<Skill>>(ss =>
                        ss == baseCreature.Skills
                        && ss.All(s => s.Ranks == 0)),
                    baseCreature.HitPoints,
                    baseCreature.Type,
                    baseCreature.Abilities))
                .Returns(newSkills);

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.Skills, Is.EqualTo(newSkills));
        }

        [Test]
        public async Task ApplyToAsync_GainsSkillPoints_NoSkills()
        {
            baseCreature.Skills = Enumerable.Empty<Skill>();

            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.Is<IEnumerable<Skill>>(ss =>
                        ss == baseCreature.Skills
                        && !ss.Any()),
                    baseCreature.HitPoints,
                    baseCreature.Type,
                    baseCreature.Abilities))
                .Returns(Enumerable.Empty<Skill>());

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.Skills, Is.Empty);
        }

        [TestCaseSource("ChallengeRatingAdjustments")]
        public async Task ApplyToAsync_ChallengeRatingAdjusted(string original, string adjusted)
        {
            baseCreature.ChallengeRating = original;

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        [Test]
        public async Task ApplyToAsync_GetNewAlignment()
        {
            var dragonAlignment = new Alignment { Lawfulness = "dragony", Goodness = "scaley" };
            mockAlignmentGenerator
                .Setup(g => g.Generate(template))
                .Returns(dragonAlignment);

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.Alignment, Is.EqualTo(dragonAlignment));
        }

        [TestCase(null, null)]
        [TestCase(0, 3)]
        [TestCase(1, 4)]
        [TestCase(2, 5)]
        [TestCase(10, 13)]
        [TestCase(42, 45)]
        public async Task ApplyToAsync_LevelAdjustmentIncreased(int? adjustment, int? adjusted)
        {
            baseCreature.LevelAdjustment = adjustment;

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [Test]
        public void ApplyTo_GainDraconicAsLanguage()
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Dragonish"));
        }

        [Test]
        public void ApplyTo_GainDraconicAsLanguage_NoLanguages()
        {
            baseCreature.Languages = Enumerable.Empty<string>();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages, Is.Empty);
        }

        [Test]
        public void ApplyTo_GainDraconicAsLanguage_AlreadyHasDraconic()
        {
            baseCreature.Languages = baseCreature.Languages.Union(new[] { "Dragonish" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Dragonish"));
        }

        [Test]
        public void ApplyTo_GainNewBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 12;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", "Dragonish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Dragonish")
                .And.Contains("Drachensprach"));
        }

        [Test]
        public void ApplyTo_GainBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", "Dragonish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Dragonish")
                .And.Contains("Drachensprach"));
        }

        [Test]
        public void ApplyTo_GainNoBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 8;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", "Dragonish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Dragonish"));
        }

        [Test]
        public void ApplyTo_GainAllBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            baseCreature.Languages = baseCreature.Languages.Union(new[] { "Dragonish", "Lizard", "Latin" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", "Dragonish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Dragonish")
                .And.Contains("Drachensprach")
                .And.Contains("Latin"));
        }

        [Test]
        public async Task ApplyToAsync_GainDraconicAsLanguage()
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Dragonish"));
        }

        [Test]
        public async Task ApplyToAsync_GainDraconicAsLanguage_NoLanguages()
        {
            baseCreature.Languages = Enumerable.Empty<string>();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages, Is.Empty);
        }

        [Test]
        public async Task ApplyToAsync_GainDraconicAsLanguage_AlreadyHasDraconic()
        {
            baseCreature.Languages = baseCreature.Languages.Union(new[] { "Dragonish" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Dragonish"));
        }

        [Test]
        public async Task ApplyToAsync_GainNewBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 12;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", "Dragonish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Dragonish")
                .And.Contains("Drachensprach"));
        }

        [Test]
        public async Task ApplyToAsync_GainBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", "Dragonish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Dragonish")
                .And.Contains("Drachensprach"));
        }

        [Test]
        public async Task ApplyToAsync_GainNoBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 8;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", "Dragonish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Dragonish"));
        }

        [Test]
        public async Task ApplyToAsync_GainAllBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            baseCreature.Languages = baseCreature.Languages.Union(new[] { "Dragonish", "Lizard", "Latin" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns("Dragonish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", "Dragonish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Dragonish")
                .And.Contains("Drachensprach")
                .And.Contains("Latin"));
        }
    }
}
