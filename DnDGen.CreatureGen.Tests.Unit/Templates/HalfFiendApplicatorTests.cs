using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Languages;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
using Moq;
using Newtonsoft.Json;
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
    public class HalfFiendApplicatorTests
    {
        private TemplateApplicator applicator;
        private Creature baseCreature;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<ISpeedsGenerator> mockSpeedsGenerator;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ISkillsGenerator> mockSkillsGenerator;
        private Mock<Dice> mockDice;
        private Mock<IMagicGenerator> mockMagicGenerator;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;
        private Mock<ICreaturePrototypeFactory> mockPrototypeFactory;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            mockSpeedsGenerator = new Mock<ISpeedsGenerator>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockSkillsGenerator = new Mock<ISkillsGenerator>();
            mockDice = new Mock<Dice>();
            mockMagicGenerator = new Mock<IMagicGenerator>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();
            mockPrototypeFactory = new Mock<ICreaturePrototypeFactory>();

            applicator = new HalfFiendApplicator(
                mockCollectionSelector.Object,
                mockTypeAndAmountSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockAdjustmentSelector.Object,
                mockCreatureDataSelector.Object,
                mockPrototypeFactory.Object);

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .WithMinimumAbility(AbilityConstants.Intelligence, 6)
                .Build();

            var speeds = new Dictionary<string, Measurement>();
            speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            speeds[SpeedConstants.Fly].Description = "the goodest";
            speeds[SpeedConstants.Fly].Value = 666;

            mockSpeedsGenerator
                .Setup(g => g.Generate(CreatureConstants.Templates.HalfFiend))
                .Returns(speeds);

            var smiteEvil = new Attack
            {
                Name = "Smite Good",
                IsSpecial = true
            };
            var attacks = new[]
            {
                smiteEvil,
                new Attack { Name = "other attack" },
                new Attack { Name = "Claw" },
                new Attack { Name = "Bite" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(attacks);
            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    attacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);
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
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.HalfFiend}");

            Assert.That(() => applicator.ApplyTo(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR2, AlignmentConstants.NeutralEvil, "Alignment filter 'Neutral Evil' is not valid for creature alignments")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR3, AlignmentConstants.LawfulEvil, "CR filter 3 does not match updated creature CR 2 (from CR 1)")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulEvil, "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulEvil, "",
            Ignore = "As Character doesn't affect already-generated creature compatiblity")]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.HalfFiend}");
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
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);
            baseCreature.Templates.Add("my other template");

            var smiteEvil = new Attack
            {
                Name = "Smite Good",
                IsSpecial = true
            };
            var attacks = new[]
            {
                smiteEvil,
                new Attack { Name = "other attack" },
                new Attack { Name = "Claw" },
                new Attack { Name = "Bite" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(attacks);
            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    attacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.HalfFiend));
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);

            var smiteEvil = new Attack
            {
                Name = "Smite Good",
                IsSpecial = true
            };
            var attacks = new[]
            {
                smiteEvil,
                new Attack { Name = "other attack" },
                new Attack { Name = "Claw" },
                new Attack { Name = "Bite" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(attacks);
            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    attacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR2;
            filters.Alignment = AlignmentConstants.LawfulEvil;

            var creature = applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.HalfFiend));
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
        [TestCase(CreatureConstants.Types.Ooze)]
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

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(5));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(original)
                .And.Contains(CreatureConstants.Types.Subtypes.Augmented)
                .And.Contains(CreatureConstants.Types.Subtypes.Native));
        }

        [Test]
        public void ApplyTo_GainsFlySpeed()
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 96;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(96));
        }

        [Test]
        public void ApplyTo_GainsBetterFlySpeed()
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 96;
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Description = "so-so";
            baseCreature.Speeds[SpeedConstants.Fly].Value = 42;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(96));
        }

        [Test]
        public void ApplyTo_UsesExistingFlySpeed()
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 96;
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Description = "so-so";
            baseCreature.Speeds[SpeedConstants.Fly].Value = 600;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("so-so"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(600));
        }

        [Test]
        public void ApplyTo_DoNotGainFlySpeed_NoLandSpeed()
        {
            baseCreature.Speeds.Clear();
            baseCreature.Speeds[SpeedConstants.Swim] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Swim].Value = 600;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(1)
                .And.ContainKey(SpeedConstants.Swim));
            Assert.That(creature.Speeds[SpeedConstants.Swim].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Swim].Value, Is.EqualTo(600));
        }

        [Test]
        public async Task ApplyToAsync_DoNotGainFlySpeed_NoLandSpeed()
        {
            baseCreature.Speeds.Clear();
            baseCreature.Speeds[SpeedConstants.Swim] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Swim].Value = 600;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(1)
                .And.ContainKey(SpeedConstants.Swim));
            Assert.That(creature.Speeds[SpeedConstants.Swim].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Swim].Value, Is.EqualTo(600));
        }

        [Test]
        public void ApplyTo_GainsNaturalArmorBonus()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(1));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(1));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public void ApplyTo_ImprovesNaturalArmorBonus()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(9267));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9267));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public void ApplyTo_ImprovesBestNaturalArmorBonus()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 90210);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(90211));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9267));
            Assert.That(bonus.IsConditional, Is.False);

            bonus = creature.ArmorClass.NaturalArmorBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(90211));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public void ApplyTo_ImprovesNaturalArmorBonus_PreserveCondition()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266, "only sometimes");

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9267));
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
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
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
            var creature = applicator.ApplyTo(baseCreature, false);

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
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
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
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Outsider
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Native,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.Abilities,
                    true))
                .Returns(newSkills);

            var newQualities = new[]
            {
                new Feat { Name = "half-Fiend quality 1" },
                new Feat { Name = "half-Fiend quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.HalfFiend,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Outsider
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Native,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    newSkills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Evil && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(newQualities);

            var attacksWithBonuses = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = new List<int> { 92 }
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = new List<int> { -66 }
                },
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
            var creature = applicator.ApplyTo(baseCreature, false);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + attacksWithBonuses.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(attacksWithBonuses));
        }

        [Test]
        public async Task ApplyToAsync_GainAttacks_WithAttackBonuses()
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
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
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Outsider
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Native,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.Abilities,
                    true))
                .Returns(newSkills);

            var newQualities = new[]
            {
                new Feat { Name = "half-Fiend quality 1" },
                new Feat { Name = "half-Fiend quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.HalfFiend,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Outsider
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Native,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    newSkills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Evil && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(newQualities);

            var attacksWithBonuses = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = new List<int> { 92 },
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = new List<int> { -66 }
                },
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
            var creature = await applicator.ApplyToAsync(baseCreature, false);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + attacksWithBonuses.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(attacksWithBonuses));
        }

        [TestCase(9266, 90210, "fiend claw roll fiend claw type")]
        [TestCase(9266, 42, "base claw roll base claw type")]
        public void ApplyTo_GainAttacks_DuplicateClawAttacks(int baseClawMax, int fiendClawMax, string expectedClawDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
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

            var claw = new Attack
            {
                Name = "Claw",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "base claw roll", Type = "base claw type" }
                },
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { claw });

            mockDice
                .Setup(d => d.Roll("base claw roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("fiend claw roll").AsPotentialMaximum<int>(true))
                .Returns(fiendClawMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature, false);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageDescription, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(newAttacks[4]));
            Assert.That(bites[0].DamageDescription, Is.EqualTo("fiend bite roll fiend bite type"));
        }

        [TestCase(9266, 90210, "fiend bite roll fiend bite type")]
        [TestCase(9266, 42, "base bite roll base bite type")]
        public void ApplyTo_GainAttacks_DuplicateBiteAttack(int baseBiteMax, int fiendBiteMax, string expectedBiteDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
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

            var bite = new Attack
            {
                Name = "Bite",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "base bite roll", Type = "base bite type" }
                },
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { bite });

            mockDice
                .Setup(d => d.Roll("base bite roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("fiend bite roll").AsPotentialMaximum<int>(true))
                .Returns(fiendBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature, false);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(newAttacks[3]));
            Assert.That(claws[0].DamageDescription, Is.EqualTo("fiend claw roll fiend claw type"));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageDescription, Is.EqualTo(expectedBiteDamage));
        }

        [TestCase(9266, 90210, "fiend claw roll fiend claw type", 1336, 1337, "fiend bite roll fiend bite type")]
        [TestCase(9266, 90210, "fiend claw roll fiend claw type", 1336, 600, "base bite roll base bite type")]
        [TestCase(9266, 42, "base claw roll base claw type", 1336, 1337, "fiend bite roll fiend bite type")]
        [TestCase(9266, 42, "base claw roll base claw type", 1336, 600, "base bite roll base bite type")]
        public void ApplyTo_GainAttacks_DuplicateClawAndBiteAttack(int baseClawMax, int fiendClawMax, string expectedClawDamage, int baseBiteMax, int fiendBiteMax, string expectedBiteDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
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

            var claw = new Attack
            {
                Name = "Claw",
                Damages = new List<Damage>
                    {
                        new Damage { Roll = "base claw roll", Type = "base claw type" }
                    },
                IsSpecial = false,
                IsMelee = true
            };
            var bite = new Attack
            {
                Name = "Bite",
                Damages = new List<Damage>
                    {
                        new Damage { Roll = "base bite roll", Type = "base bite type" }
                    },
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { claw, bite });

            mockDice
                .Setup(d => d.Roll("base claw roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("fiend claw roll").AsPotentialMaximum<int>(true))
                .Returns(fiendClawMax);
            mockDice
                .Setup(d => d.Roll("base bite roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("fiend bite roll").AsPotentialMaximum<int>(true))
                .Returns(fiendBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature, false);
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 2));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageDescription, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageDescription, Is.EqualTo(expectedBiteDamage));
        }

        [TestCase(9266, 90210, "fiend claw roll fiend claw type")]
        [TestCase(9266, 42, "base claw roll base claw type")]
        public async Task ApplyToAsync_GainAttacks_DuplicateClawAttacks(int baseClawMax, int fiendClawMax, string expectedClawDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
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

            var claw = new Attack
            {
                Name = "Claw",
                Damages = new List<Damage>
                {
                    new Damage { Roll = "base claw roll", Type = "base claw type" }
                },
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { claw });

            mockDice
                .Setup(d => d.Roll("base claw roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("fiend claw roll").AsPotentialMaximum<int>(true))
                .Returns(fiendClawMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageDescription, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(newAttacks[4]));
            Assert.That(bites[0].DamageDescription, Is.EqualTo("fiend bite roll fiend bite type"));
        }

        [TestCase(9266, 90210, "fiend bite roll fiend bite type")]
        [TestCase(9266, 42, "base bite roll base bite type")]
        public async Task ApplyToAsync_GainAttacks_DuplicateBiteAttack(int baseBiteMax, int fiendBiteMax, string expectedBiteDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
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

            var bite = new Attack
            {
                Name = "Bite",
                Damages = new List<Damage>
                    {
                        new Damage { Roll = "base bite roll", Type = "base bite type" }
                    },
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { bite });

            mockDice
                .Setup(d => d.Roll("base bite roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("fiend bite roll").AsPotentialMaximum<int>(true))
                .Returns(fiendBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(newAttacks[3]));
            Assert.That(claws[0].DamageDescription, Is.EqualTo("fiend claw roll fiend claw type"));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageDescription, Is.EqualTo(expectedBiteDamage));
        }

        [TestCase(9266, 90210, "fiend claw roll fiend claw type", 1336, 1337, "fiend bite roll fiend bite type")]
        [TestCase(9266, 90210, "fiend claw roll fiend claw type", 1336, 600, "base bite roll base bite type")]
        [TestCase(9266, 42, "base claw roll base claw type", 1336, 1337, "fiend bite roll fiend bite type")]
        [TestCase(9266, 42, "base claw roll base claw type", 1336, 600, "base bite roll base bite type")]
        public async Task ApplyToAsync_GainAttacks_DuplicateClawAndBiteAttack(int baseClawMax, int fiendClawMax, string expectedClawDamage, int baseBiteMax, int fiendBiteMax, string expectedBiteDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
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

            var claw = new Attack
            {
                Name = "Claw",
                Damages = new List<Damage>
                    {
                        new Damage { Roll = "base claw roll", Type = "base claw type" }
                    },
                IsSpecial = false,
                IsMelee = true
            };
            var bite = new Attack
            {
                Name = "Bite",
                Damages = new List<Damage>
                    {
                        new Damage { Roll = "base bite roll", Type = "base bite type" }
                    },
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { claw, bite });

            mockDice
                .Setup(d => d.Roll("base claw roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("fiend claw roll").AsPotentialMaximum<int>(true))
                .Returns(fiendClawMax);
            mockDice
                .Setup(d => d.Roll("base bite roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("fiend bite roll").AsPotentialMaximum<int>(true))
                .Returns(fiendBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 2));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageDescription, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageDescription, Is.EqualTo(expectedBiteDamage));
        }

        [TestCase(.1, 1)]
        [TestCase(.25, 1)]
        [TestCase(.5, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, 6)]
        [TestCase(7, 7)]
        [TestCase(8, 8)]
        [TestCase(9, 9)]
        [TestCase(10, 10)]
        [TestCase(11, 11)]
        [TestCase(12, 12)]
        [TestCase(13, 13)]
        [TestCase(14, 14)]
        [TestCase(15, 15)]
        [TestCase(16, 16)]
        [TestCase(17, 17)]
        [TestCase(18, 18)]
        [TestCase(19, 19)]
        [TestCase(20, 20)]
        [TestCase(21, 20)]
        [TestCase(22, 20)]
        [TestCase(42, 20)]
        public void ApplyTo_CreatureGainsSmiteGoodSpecialAttack(double hitDiceQuantity, int smiteDamage)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDiceQuantity;

            var originalAttacks = baseCreature.Attacks
                .Select(a => JsonConvert.SerializeObject(a))
                .Select(a => JsonConvert.DeserializeObject<Attack>(a))
                .ToArray();
            var originalSpecialAttacks = baseCreature.SpecialAttacks
                .Select(a => JsonConvert.SerializeObject(a))
                .Select(a => JsonConvert.DeserializeObject<Attack>(a))
                .ToArray();

            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
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

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalAttacks.Length + 5));
            Assert.That(creature.Attacks.Select(a => a.Name), Is.SupersetOf(originalAttacks.Select(a => a.Name)));
            Assert.That(creature.Attacks, Contains.Item(newAttacks[2]));
            Assert.That(creature.SpecialAttacks.Count(), Is.EqualTo(originalSpecialAttacks.Length + 3));
            Assert.That(creature.SpecialAttacks, Contains.Item(newAttacks[2]));

            Assert.That(newAttacks[2].DamageDescription, Is.EqualTo(smiteDamage.ToString()));
        }

        [Test]
        public void ApplyTo_GainSpecialQualities()
        {
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
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
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Outsider
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Native,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.Abilities,
                    true))
                .Returns(newSkills);

            var newQualities = new[]
            {
                new Feat { Name = "half-Fiend quality 1" },
                new Feat { Name = "half-Fiend quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.HalfFiend,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Outsider
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Native,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    newSkills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Evil)))
                .Returns(newQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = applicator.ApplyTo(baseCreature, false);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + newQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(newQualities));
        }

        [Test]
        public void ApplyTo_AbilitiesImprove()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(2));
        }

        [TestCase(AbilityConstants.Charisma)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence, Ignore = "Intelligence is required")]
        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Wisdom)]
        public void ApplyTo_AbilitiesImprove_DoNotImproveMissingAbility(string ability)
        {
            baseCreature.Abilities[ability].BaseScore = 0;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Abilities[ability].HasScore, Is.False);
            Assert.That(creature.Abilities[ability].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[ability].TemplateAdjustment, Is.Zero);
        }

        [Test]
        public void ApplyTo_GainsSkillPoints()
        {
            var ranks = 1;
            foreach (var skill in baseCreature.Skills)
            {
                skill.Ranks = ranks++ % skill.RankCap;
            }

            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
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
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Outsider
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Native,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.Abilities,
                    true))
                .Returns(newSkills);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Skills, Is.EqualTo(newSkills));
        }

        [Test]
        public void ApplyTo_GainsSkillPoints_NoSkills()
        {
            baseCreature.Skills = Enumerable.Empty<Skill>();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.Is<IEnumerable<Skill>>(ss =>
                        ss == baseCreature.Skills
                        && !ss.Any()),
                    baseCreature.HitPoints,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Outsider
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Native,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.Abilities,
                    true))
                .Returns(Enumerable.Empty<Skill>());

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Skills, Is.Empty);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void ApplyTo_ChallengeRatingAdjusted(double hitDiceQuantity, string original, string adjusted)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDiceQuantity;
            baseCreature.ChallengeRating = original;

            var smiteEvil = new Attack
            {
                Name = "Smite Good",
                IsSpecial = true
            };
            var newAttacks = new[] { smiteEvil, new Attack { Name = "other attack" } };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
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

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(hitDiceQuantity));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        private static IEnumerable ChallengeRatingAdjustments
        {
            get
            {
                var hitDice = new List<double>(Enumerable.Range(1, 20)
                    .Select(i => Convert.ToDouble(i)));

                hitDice.AddRange(new[]
                {
                    .1, .2, .3, .4, .5, .6, .7, .8, .9,
                });

                //INFO: Don't need to test every CR, since it is the basic Increase functionality, which is tested separately
                //So, we only need to test the amount it is increased, not every CR permutation
                var challengeRating = ChallengeRatingConstants.CR1;

                foreach (var hitDie in hitDice)
                {
                    var increase = 1;

                    if (hitDie > 10)
                    {
                        increase = 3;
                    }
                    else if (hitDie > 4)
                    {
                        increase = 2;
                    }

                    var newCr = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, increase);
                    yield return new TestCaseData(hitDie, challengeRating, newCr);
                }
            }
        }

        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Neutral, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Evil, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Neutral, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Evil, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Neutral, AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Evil, AlignmentConstants.LawfulEvil)]
        public void ApplyTo_AlignmentAdjusted(string lawfulness, string goodness, string adjusted)
        {
            baseCreature.Alignment.Lawfulness = lawfulness;
            baseCreature.Alignment.Goodness = goodness;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo(adjusted));
        }

        [Test]
        public void ApplyTo_GetPresetAlignment()
        {
            baseCreature.Alignment.Lawfulness = "preset";
            baseCreature.Alignment.Goodness = "alignment";

            var filters = new Filters { Alignment = "preset Evil" };

            var creature = applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo("preset Evil"));
        }

        [TestCase(null, null)]
        [TestCase(0, 4)]
        [TestCase(1, 5)]
        [TestCase(2, 6)]
        [TestCase(10, 14)]
        [TestCase(42, 46)]
        public void ApplyTo_LevelAdjustmentIncreased(int? adjustment, int? adjusted)
        {
            baseCreature.LevelAdjustment = adjustment;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
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
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.HalfFiend}");

            Assert.That(async () => await applicator.ApplyToAsync(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR2, AlignmentConstants.NeutralEvil, "Alignment filter 'Neutral Evil' is not valid for creature alignments")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR3, AlignmentConstants.LawfulEvil, "CR filter 3 does not match updated creature CR 2 (from CR 1)")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulEvil, "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulEvil, "",
            Ignore = "As Character doesn't affect already-generated creature compatiblity")]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.HalfFiend}");
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
        public async Task ApplyToAsync_ReturnsCreature_WithOtherTemplate()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);
            baseCreature.Templates.Add("my other template");

            var smiteEvil = new Attack
            {
                Name = "Smite Good",
                IsSpecial = true
            };
            var attacks = new[]
            {
                smiteEvil,
                new Attack { Name = "other attack" },
                new Attack { Name = "Claw" },
                new Attack { Name = "Bite" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(attacks);
            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    attacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.HalfFiend));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);

            var smiteEvil = new Attack
            {
                Name = "Smite Good",
                IsSpecial = true
            };
            var attacks = new[]
            {
                smiteEvil,
                new Attack { Name = "other attack" },
                new Attack { Name = "Claw" },
                new Attack { Name = "Bite" },
            };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
                    SizeConstants.Medium,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(attacks);
            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    attacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR2;
            filters.Alignment = AlignmentConstants.LawfulEvil;

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.HalfFiend));
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
        [TestCase(CreatureConstants.Types.Ooze)]
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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(5));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(original)
                .And.Contains(CreatureConstants.Types.Subtypes.Augmented)
                .And.Contains(CreatureConstants.Types.Subtypes.Native));
        }

        [Test]
        public async Task ApplyToAsync_GainsFlySpeed()
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 96;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(96));
        }

        [Test]
        public async Task ApplyToAsync_GainsBetterFlySpeed()
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 96;
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Description = "so-so";
            baseCreature.Speeds[SpeedConstants.Fly].Value = 42;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(96));
        }

        [Test]
        public async Task ApplyToAsync_UsesExistingFlySpeed()
        {
            baseCreature.Speeds[SpeedConstants.Land].Value = 96;
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Description = "so-so";
            baseCreature.Speeds[SpeedConstants.Fly].Value = 600;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("so-so"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(600));
        }

        [Test]
        public async Task ApplyToAsync_GainsNaturalArmorBonus()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(1));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(1));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public async Task ApplyToAsync_ImprovesNaturalArmorBonus()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(9267));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9267));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public async Task ApplyToAsync_ImprovesBestNaturalArmorBonus()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 90210);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(90211));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9267));
            Assert.That(bonus.IsConditional, Is.False);

            bonus = creature.ArmorClass.NaturalArmorBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(90211));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public async Task ApplyToAsync_ImprovesNaturalArmorBonus_PreserveCondition()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266, "only sometimes");

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9267));
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
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
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
            var creature = await applicator.ApplyToAsync(baseCreature, false);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(newAttacks));
        }

        [TestCase(.1, 1)]
        [TestCase(.25, 1)]
        [TestCase(.5, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, 6)]
        [TestCase(7, 7)]
        [TestCase(8, 8)]
        [TestCase(9, 9)]
        [TestCase(10, 10)]
        [TestCase(11, 11)]
        [TestCase(12, 12)]
        [TestCase(13, 13)]
        [TestCase(14, 14)]
        [TestCase(15, 15)]
        [TestCase(16, 16)]
        [TestCase(17, 17)]
        [TestCase(18, 18)]
        [TestCase(19, 19)]
        [TestCase(20, 20)]
        [TestCase(21, 20)]
        [TestCase(22, 20)]
        [TestCase(42, 20)]
        public async Task ApplyToAsync_CreatureGainsSmiteEvilSpecialAttack(double hitDiceQuantity, int smiteDamage)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDiceQuantity;

            var originalAttacks = baseCreature.Attacks
                .Select(a => JsonConvert.SerializeObject(a))
                .Select(a => JsonConvert.DeserializeObject<Attack>(a))
                .ToArray();
            var originalSpecialAttacks = baseCreature.SpecialAttacks
                .Select(a => JsonConvert.SerializeObject(a))
                .Select(a => JsonConvert.DeserializeObject<Attack>(a))
                .ToArray();

            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalAttacks.Length + 5));
            Assert.That(creature.Attacks.Select(a => a.Name), Is.SupersetOf(originalAttacks.Select(a => a.Name)));
            Assert.That(creature.Attacks, Contains.Item(newAttacks[2]));
            Assert.That(creature.SpecialAttacks.Count(), Is.EqualTo(originalSpecialAttacks.Length + 3));
            Assert.That(creature.SpecialAttacks, Contains.Item(newAttacks[2]));

            Assert.That(newAttacks[2].DamageDescription, Is.EqualTo(smiteDamage.ToString()));
        }

        [Test]
        public async Task ApplyToAsync_GainSpecialQualities()
        {
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
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
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Outsider
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Native,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.Abilities,
                    true))
                .Returns(newSkills);

            var newQualities = new[]
            {
                new Feat { Name = "half-Fiend quality 1" },
                new Feat { Name = "half-Fiend quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.HalfFiend,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Outsider
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Native,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    newSkills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Evil && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(newQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, false);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + newQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(newQualities));
        }

        [Test]
        public async Task ApplyToAsync_AbilitiesImprove()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(2));
        }

        [TestCase(AbilityConstants.Charisma)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence, Ignore = "Intelligence is required")]
        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Wisdom)]
        public async Task ApplyToAsync_AbilitiesImprove_DoNotImproveMissingAbility(string ability)
        {
            baseCreature.Abilities[ability].BaseScore = 0;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Abilities[ability].HasScore, Is.False);
            Assert.That(creature.Abilities[ability].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[ability].TemplateAdjustment, Is.Zero);
        }

        [Test]
        public async Task ApplyToAsync_GainsSkillPoints()
        {
            var ranks = 1;
            foreach (var skill in baseCreature.Skills)
            {
                skill.Ranks = ranks++ % skill.RankCap;
            }

            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
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
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Outsider
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Native,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.Abilities,
                    true))
                .Returns(newSkills);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Skills, Is.EqualTo(newSkills));
        }

        [Test]
        public async Task ApplyToAsync_GainsSkillPoints_NoSkills()
        {
            baseCreature.Skills = Enumerable.Empty<Skill>();
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();

            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.Is<IEnumerable<Skill>>(ss =>
                        ss == baseCreature.Skills
                        && !ss.Any()),
                    baseCreature.HitPoints,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Outsider
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Native,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.Abilities,
                    true))
                .Returns(Enumerable.Empty<Skill>());

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Skills, Is.Empty);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public async Task ApplyToAsync_ChallengeRatingAdjusted(double hitDiceQuantity, string original, string adjusted)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDiceQuantity;
            baseCreature.ChallengeRating = original;

            var smiteEvil = new Attack
            {
                Name = "Smite Good",
                IsSpecial = true
            };
            var newAttacks = new[] { smiteEvil, new Attack { Name = "other attack" } };
            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(hitDiceQuantity));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Neutral, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Evil, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Neutral, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Evil, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Neutral, AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Evil, AlignmentConstants.LawfulEvil)]
        public async Task ApplyToAsync_AlignmentAdjusted(string lawfulness, string goodness, string adjusted)
        {
            baseCreature.Alignment.Lawfulness = lawfulness;
            baseCreature.Alignment.Goodness = goodness;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo(adjusted));
        }

        [Test]
        public async Task ApplyToAsync_GetPresetAlignment()
        {
            baseCreature.Alignment.Lawfulness = "preset";
            baseCreature.Alignment.Goodness = "alignment";

            var filters = new Filters { Alignment = "preset Evil" };

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo("preset Evil"));
        }

        [TestCase(null, null)]
        [TestCase(0, 4)]
        [TestCase(1, 5)]
        [TestCase(2, 6)]
        [TestCase(10, 14)]
        [TestCase(42, 46)]
        public async Task ApplyToAsync_LevelAdjustmentIncreased(int? adjustment, int? adjusted)
        {
            baseCreature.LevelAdjustment = adjustment;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [Test]
        public void ApplyTo_GainARandomAutomaticLanguage()
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor"));
        }

        [Test]
        public void ApplyTo_GainALanguage_NoLanguages()
        {
            baseCreature.Languages = Enumerable.Empty<string>();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages, Is.Empty);
        }

        [Test]
        public void ApplyTo_GainAnAutomaticLanguage_AlreadyHas()
        {
            baseCreature.Languages = baseCreature.Languages.Union(new[] { "Mordor" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor"));
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
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Hellsprach", "Mordor", "Profanity", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 3));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor")
                .And.Contains("Hellsprach")
                .And.Contains("Latin"));
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
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Hellsprach", "Mordor", "Profanity", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 3));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor")
                .And.Contains("Hellsprach")
                .And.Contains("Latin"));
        }

        [Test]
        public void ApplyTo_GainSomeBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 8;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Hellsprach", "Mordor", "Profanity", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor")
                .And.Contains("Hellsprach"));
        }

        [Test]
        public void ApplyTo_GainNoBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 6;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Hellsprach", "Mordor", "Profanity", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor"));
        }

        [Test]
        public void ApplyTo_GainAllBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            baseCreature.Languages = baseCreature.Languages.Union(new[] { "Mordor", "Profanity", "Latin" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Hellsprach", "Mordor", "Profanity", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor")
                .And.Contains("Hellsprach")
                .And.Contains("Latin"));
        }

        [Test]
        public async Task ApplyToAsync_GainARandomAutomaticLanguage()
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor"));
        }

        [Test]
        public async Task ApplyToAsync_GainALanguage_NoLanguages()
        {
            baseCreature.Languages = Enumerable.Empty<string>();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages, Is.Empty);
        }

        [Test]
        public async Task ApplyToAsync_GainAnAutomaticLanguage_AlreadyHas()
        {
            baseCreature.Languages = baseCreature.Languages.Union(new[] { "Mordor" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor"));
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
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Hellsprach", "Mordor", "Profanity", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 3));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor")
                .And.Contains("Hellsprach")
                .And.Contains("Latin"));
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
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Hellsprach", "Mordor", "Profanity", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 3));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor")
                .And.Contains("Hellsprach")
                .And.Contains("Latin"));
        }

        [Test]
        public async Task ApplyToAsync_GainSomeBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 8;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Hellsprach", "Mordor", "Profanity", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor")
                .And.Contains("Hellsprach"));
        }

        [Test]
        public async Task ApplyToAsync_GainNoBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 6;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Hellsprach", "Mordor", "Profanity", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor"));
        }

        [Test]
        public async Task ApplyToAsync_GainAllBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            baseCreature.Languages = baseCreature.Languages.Union(new[] { "Mordor", "Profanity", "Latin" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Hellsprach", "Mordor", "Profanity", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Mordor")
                .And.Contains("Hellsprach")
                .And.Contains("Latin"));
        }

        [Test]
        public void ApplyTo_RegenerateMagic()
        {
            var newMagic = new Magic();
            mockMagicGenerator
                .Setup(g => g.GenerateWith(
                    baseCreature.Name,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Evil),
                    baseCreature.Abilities,
                    baseCreature.Equipment))
                .Returns(newMagic);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Magic, Is.EqualTo(newMagic));
        }

        [Test]
        public async Task ApplyToAsync_RegenerateMagic()
        {
            var newMagic = new Magic();
            mockMagicGenerator
                .Setup(g => g.GenerateWith(
                    baseCreature.Name,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Evil),
                    baseCreature.Abilities,
                    baseCreature.Equipment))
                .Returns(newMagic);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Magic, Is.EqualTo(newMagic));
        }

        [Test]
        public void ApplyTo_SetsTemplate()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.HalfFiend));
        }

        [Test]
        public async Task ApplyToAsync_SetsTemplate()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.HalfFiend));
        }

        [Test]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2", "wrong creature 3" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments.Except(new[] { adjustments[3] }));
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticNeutral, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;
            hitDice["wrong creature 3"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_ReturnEmpty_WhenAlignmentFilterInvalid()
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

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);

            var filters = new Filters { Alignment = "preset alignment" };

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

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "preset Evil", "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "preset Neutral", "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);

            var filters = new Filters { Alignment = "preset Evil" };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5)]
        public void GetCompatibleCreatures_WithChallengeRating_ReturnCompatibleCreatures(double hitDiceQuantity, string original, string challengeRating)
        {
            var creatures = new[]
            {
                "my creature",
                "wrong creature 1",
                "my other creature",
                "wrong creature 2",
                "wrong creature 3",
                "wrong creature 4",
                "wrong creature 5",
            };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 4"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 5"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments.Except(new[] { adjustments[3] }));
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 4"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 5"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticNeutral, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { AlignmentConstants.TrueNeutral, "other alignment" };
            alignments["wrong creature 5" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulNeutral, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;
            hitDice["my other creature"] = hitDiceQuantity;
            hitDice["wrong creature 1"] = hitDiceQuantity;
            hitDice["wrong creature 2"] = hitDiceQuantity;
            hitDice["wrong creature 3"] = hitDiceQuantity;
            hitDice["wrong creature 4"] = hitDiceQuantity >= 11 ? 1 : 666;
            hitDice["wrong creature 5"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 4"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 5"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(original, 1) };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.Subtypes.Native)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        public void GetCompatibleCreatures_WithType_ReturnCompatibleCreatures(string type)
        {
            var creatures = new[]
            {
                "my creature",
                "wrong creature 1",
                "my other creature",
                "wrong creature 2",
                "wrong creature 3",
            };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments.Except(new[] { adjustments[3] }));
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticNeutral, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_WithType_ReturnCompatibleCreatures_FilterOutInvalidTypes()
        {
            var creatures = new[]
            {
                "my creature",
                "wrong creature 1",
                "my other creature",
                "wrong creature 2",
                "wrong creature 3",
                "wrong creature 4",
            };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3", "subtype 1" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 4"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2", "subtype 3" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments.Except(new[] { adjustments[3] }));
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 4"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticNeutral, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { AlignmentConstants.TrueNeutral, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;
            hitDice["wrong creature 4"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 4"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var filters = new Filters { Type = "subtype 1" };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[]
            {
                "my creature",
                "wrong creature 1",
                "my other creature",
                "wrong creature 2",
                "wrong creature 3",
                "wrong creature 4",
                "wrong creature 5",
                "wrong creature 6",
            };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3", "subtype 1" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 3", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 4"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2", "subtype 3" };
            types["wrong creature 5"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2", "subtype 1" };
            types["wrong creature 6"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 1"))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments.Except(new[] { adjustments[3] }));
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 4"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 5"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 6"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticNeutral, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { AlignmentConstants.TrueNeutral, "other alignment" };
            alignments["wrong creature 5" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulNeutral, "other alignment" };
            alignments["wrong creature 6" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;
            hitDice["wrong creature 4"] = 4;
            hitDice["wrong creature 5"] = 666;
            hitDice["wrong creature 6"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 4"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 5"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 6"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var filters = new Filters { Type = "subtype 1", ChallengeRating = ChallengeRatingConstants.CR2 };

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
        [TestCase(CreatureConstants.Types.Ooze, true)]
        [TestCase(CreatureConstants.Types.Outsider, false)]
        [TestCase(CreatureConstants.Types.Plant, true)]
        [TestCase(CreatureConstants.Types.Undead, false)]
        [TestCase(CreatureConstants.Types.Vermin, true)]
        public void IsCompatible_BasedOnCreatureType(string creatureType, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { creatureType, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [Test]
        public void IsCompatible_CannotBeIncorporeal()
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", CreatureConstants.Types.Subtypes.Incorporeal, "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void IsCompatible_MustHaveIntelligence()
        {
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
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(-10, false)]
        [TestCase(-8, false)]
        [TestCase(-6, true)]
        [TestCase(-4, true)]
        [TestCase(-2, true)]
        [TestCase(0, true)]
        [TestCase(2, true)]
        [TestCase(4, true)]
        [TestCase(6, true)]
        [TestCase(8, true)]
        [TestCase(10, true)]
        [TestCase(42, true)]
        public void IsCompatible_IntelligenceMustBeAtLeast4(int intelligenceAdjustment, bool compatible)
        {
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
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = intelligenceAdjustment },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 600 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(AlignmentConstants.LawfulGood, false)]
        [TestCase(AlignmentConstants.NeutralGood, false)]
        [TestCase(AlignmentConstants.ChaoticGood, false)]
        [TestCase(AlignmentConstants.LawfulNeutral, true)]
        [TestCase(AlignmentConstants.TrueNeutral, true)]
        [TestCase(AlignmentConstants.ChaoticNeutral, true)]
        [TestCase(AlignmentConstants.LawfulEvil, true)]
        [TestCase(AlignmentConstants.NeutralEvil, true)]
        [TestCase(AlignmentConstants.ChaoticEvil, true)]
        public void IsCompatible_MustHaveNonEvilAlignment(string alignment, bool compatible)
        {
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
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { alignment, "other Good" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(AlignmentConstants.LawfulGood, false)]
        [TestCase(AlignmentConstants.NeutralGood, false)]
        [TestCase(AlignmentConstants.ChaoticGood, false)]
        [TestCase(AlignmentConstants.LawfulNeutral, true)]
        [TestCase(AlignmentConstants.TrueNeutral, true)]
        [TestCase(AlignmentConstants.ChaoticNeutral, true)]
        [TestCase(AlignmentConstants.LawfulEvil, true)]
        [TestCase(AlignmentConstants.NeutralEvil, true)]
        [TestCase(AlignmentConstants.ChaoticEvil, true)]
        public void IsCompatible_MustHaveAnyNonEvilAlignment(string alignment, bool compatible)
        {
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
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other Good", alignment };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(null, true)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.Outsider, true)]
        [TestCase("subtype 1", true)]
        [TestCase("subtype 2", true)]
        [TestCase(CreatureConstants.Types.Subtypes.Native, true)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, true)]
        [TestCase("wrong type", false)]
        public void IsCompatible_TypeMustMatch(string type, bool compatible)
        {
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
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, true)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, true)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, true)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR4, false)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, true)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR5, false)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, true)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR6, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, true)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR4, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, true)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR5, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, true)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR6, false)]
        public void IsCompatible_ChallengeRatingMustMatch(double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, true)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, true)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, true)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR4, false)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, true)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR5, false)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, true)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR6, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, true)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR4, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, true)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR5, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, true)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR6, false)]
        public void IsCompatible_ChallengeRatingMustMatch_HumanoidCharacter(double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, true, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_2nd, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, true)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, true)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, true)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR4, false)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, true)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR5, false)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, true)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR6, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, true)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR4, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, true)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR5, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, true)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR6, false)]
        public void IsCompatible_ChallengeRatingMustMatch_NonHumanoidCharacter(double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, true, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulGood, false)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.NeutralGood, false)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.ChaoticGood, false)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulNeutral, true)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.TrueNeutral, false)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.ChaoticNeutral, false)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulEvil, true)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.NeutralEvil, false)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.ChaoticEvil, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.LawfulGood, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.NeutralGood, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.ChaoticGood, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.LawfulNeutral, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.TrueNeutral, true)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.ChaoticNeutral, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.LawfulEvil, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.NeutralEvil, true)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.ChaoticEvil, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.LawfulGood, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.NeutralGood, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.ChaoticGood, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.LawfulNeutral, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.TrueNeutral, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.ChaoticNeutral, true)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.LawfulEvil, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.NeutralEvil, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.ChaoticEvil, true)]
        public void IsCompatible_AlignmentMustMatch(string alignmentFilter, string creatureAlignment, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };
            types[CreatureConstants.Rat] = new[] { CreatureConstants.Types.Vermin };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other Good", creatureAlignment };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var filters = new Filters { Alignment = alignmentFilter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR2, AlignmentConstants.LawfulEvil, true)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR2, AlignmentConstants.NeutralEvil, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR1, AlignmentConstants.LawfulEvil, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR1, AlignmentConstants.NeutralEvil, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulEvil, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR2, AlignmentConstants.NeutralEvil, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1, AlignmentConstants.LawfulEvil, false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1, AlignmentConstants.NeutralEvil, false)]
        public void IsCompatible_AllFiltersMustMatch(string type, string challengeRating, string alignment, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var filters = new Filters { Type = type, ChallengeRating = challengeRating, Alignment = alignment };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2", "wrong creature 3" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments.Except(new[] { adjustments[3] }));
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticNeutral, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;
            hitDice["wrong creature 3"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
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
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
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
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnEmpty_WhenAlignmentFilterInvalid()
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

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);

            var filters = new Filters { Alignment = "preset alignment" };

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
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "preset Evil", "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "preset Neutral", "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;
            hitDice["my other creature"] = 1;
            hitDice["wrong creature 1"] = 1;
            hitDice["wrong creature 2"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
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
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
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

            var filters = new Filters { Alignment = "preset Evil" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5)]
        public void GetCompatiblePrototypes_FromNames_WithChallengeRating_ReturnCompatibleCreatures(double hitDiceQuantity, string original, string challengeRating)
        {
            var creatures = new[]
            {
                "my creature",
                "wrong creature 1",
                "my other creature",
                "wrong creature 2",
                "wrong creature 3",
                "wrong creature 4",
                "wrong creature 5",
            };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 4"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 5"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments.Except(new[] { adjustments[3] }));
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 4"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 5"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticNeutral, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { AlignmentConstants.TrueNeutral, "other alignment" };
            alignments["wrong creature 5" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulNeutral, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;
            hitDice["my other creature"] = hitDiceQuantity;
            hitDice["wrong creature 1"] = hitDiceQuantity;
            hitDice["wrong creature 2"] = hitDiceQuantity;
            hitDice["wrong creature 3"] = hitDiceQuantity;
            hitDice["wrong creature 4"] = hitDiceQuantity >= 11 ? 1 : 666;
            hitDice["wrong creature 5"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 4"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 5"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(original, 1) };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
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
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
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

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(hitDiceQuantity));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(hitDiceQuantity));
        }

        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.Subtypes.Native)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        public void GetCompatiblePrototypes_FromNames_WithType_ReturnCompatibleCreatures(string type)
        {
            var creatures = new[]
            {
                "my creature",
                "wrong creature 1",
                "my other creature",
                "wrong creature 2",
                "wrong creature 3",
            };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments.Except(new[] { adjustments[3] }));
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticNeutral, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
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
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
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

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_WithType_ReturnCompatibleCreatures_FilterOutInvalidTypes()
        {
            var creatures = new[]
            {
                "my creature",
                "wrong creature 1",
                "my other creature",
                "wrong creature 2",
                "wrong creature 3",
                "wrong creature 4",
            };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3", "subtype 1" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 4"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2", "subtype 3" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments.Except(new[] { adjustments[3] }));
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 4"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticNeutral, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { AlignmentConstants.TrueNeutral, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;
            hitDice["wrong creature 4"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 4"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
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
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
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

            var filters = new Filters { Type = "subtype 1" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                "subtype 1",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[]
            {
                "my creature",
                "wrong creature 1",
                "my other creature",
                "wrong creature 2",
                "wrong creature 3",
                "wrong creature 4",
                "wrong creature 5",
                "wrong creature 6",
            };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3", "subtype 1" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 3", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 4"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2", "subtype 3" };
            types["wrong creature 5"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2", "subtype 1" };
            types["wrong creature 6"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 1"))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments.Except(new[] { adjustments[3] }));
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 4"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 5"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 6"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticNeutral, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { AlignmentConstants.TrueNeutral, "other alignment" };
            alignments["wrong creature 5" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulNeutral, "other alignment" };
            alignments["wrong creature 6" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;
            hitDice["wrong creature 4"] = 4;
            hitDice["wrong creature 5"] = 666;
            hitDice["wrong creature 6"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 4"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 5"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 6"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
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
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
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

            var filters = new Filters { Type = "subtype 1", ChallengeRating = ChallengeRatingConstants.CR2 };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                "subtype 1",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures_WithLevelAdjustments()
        {
            var creatures = new[]
            {
                "my creature",
                "my other creature",
            };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3", "subtype 1" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 3", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 4"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2", "subtype 3" };
            types["wrong creature 5"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2", "subtype 1" };
            types["wrong creature 6"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 1"))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 1"))
                .Returns(adjustments.Except(new[] { adjustments[3] }));
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 4"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 5"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 6"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralEvil, "other alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticEvil, "other alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticNeutral, "other alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { AlignmentConstants.NeutralGood };
            alignments["wrong creature 4" + GroupConstants.Exploded] = new[] { AlignmentConstants.TrueNeutral, "other alignment" };
            alignments["wrong creature 5" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulNeutral, "other alignment" };
            alignments["wrong creature 6" + GroupConstants.Exploded] = new[] { AlignmentConstants.LawfulEvil, "other alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;
            hitDice["wrong creature 1"] = 4;
            hitDice["wrong creature 2"] = 4;
            hitDice["wrong creature 3"] = 4;
            hitDice["wrong creature 4"] = 4;
            hitDice["wrong creature 5"] = 666;
            hitDice["wrong creature 6"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, LevelAdjustment = 0, CasterLevel = 3 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, LevelAdjustment = 2, CasterLevel = 4 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 4"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 5"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 6"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
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
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
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
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.EqualTo(3));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(4));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                "subtype 1",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.EqualTo(4));
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.EqualTo(6));
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures_WithValidAlignments()
        {
            var creatures = new[]
            {
                "my creature",
                "my other creature",
            };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3", "subtype 1" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 1"))
                .Returns((string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 1336 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my other creature"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.ChaoticNeutral, "other alignment", "other Good" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { AlignmentConstants.TrueNeutral, "wrong Good" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 4;
            hitDice["my other creature"] = 4;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithAlignments(alignments["my creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my creature"].ChallengeRating)
                    .WithCasterLevel(data["my creature"].CasterLevel)
                    .WithLevelAdjustment(data["my creature"].LevelAdjustment)
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
                    .WithAlignments(alignments["my other creature" + GroupConstants.Exploded].ToArray())
                    .WithChallengeRating(data["my other creature"].ChallengeRating)
                    .WithCasterLevel(data["my other creature"].CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].LevelAdjustment)
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
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.ChaoticEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                "subtype 1",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticEvil, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithoutAbility(AbilityConstants.Intelligence)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .WithAlignments(AlignmentConstants.NeutralEvil, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticNeutral, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 3")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.NeutralGood)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnEmpty_WhenAlignmentFilterInvalid()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticEvil, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .WithAlignments(AlignmentConstants.NeutralEvil, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.NeutralGood)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
            };

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, filters);
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
                    .WithAlignments("preset Evil", "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticEvil, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .WithAlignments("preset Neutral", "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.NeutralGood)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
            };

            var filters = new Filters { Alignment = "preset Evil" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_2nd)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3)]
        [TestCase(6, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(6, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(6, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        [TestCase(11, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3)]
        [TestCase(11, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4)]
        [TestCase(11, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5)]
        [TestCase(20, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3)]
        [TestCase(20, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4)]
        [TestCase(20, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5)]
        public void GetCompatiblePrototypes_FromPrototypes_WithChallengeRating_ReturnCompatibleCreatures(double hitDiceQuantity, string original, string challengeRating)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(hitDiceQuantity)
                    .WithChallengeRating(original)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticEvil, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithoutAbility(AbilityConstants.Intelligence)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(hitDiceQuantity)
                    .WithChallengeRating(original)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .WithAlignments(AlignmentConstants.NeutralEvil, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(hitDiceQuantity)
                    .WithChallengeRating(original)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticNeutral, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(hitDiceQuantity)
                    .WithChallengeRating(original)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 3")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.NeutralGood)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(hitDiceQuantity)
                    .WithChallengeRating(original)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 4")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.TrueNeutral, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(hitDiceQuantity >= 11 ? 1 : 666)
                    .WithChallengeRating(original)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 5")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulNeutral, "other alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(hitDiceQuantity)
                    .WithChallengeRating(ChallengeRatingConstants.IncreaseChallengeRating(original, 1))
                    .Build(),
            };

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(hitDiceQuantity));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(hitDiceQuantity));
        }

        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.Subtypes.Native)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        public void GetCompatiblePrototypes_FromPrototypes_WithType_ReturnCompatibleCreatures(string type)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithoutAbility(AbilityConstants.Intelligence)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithAlignments(AlignmentConstants.ChaoticEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithAlignments(AlignmentConstants.NeutralEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithAlignments(AlignmentConstants.ChaoticNeutral, "other alignment")
                    .WithHitDiceQuantity(4)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 3")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithAlignments(AlignmentConstants.NeutralGood)
                    .WithHitDiceQuantity(4)
                    .Build(),
            };

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_WithType_ReturnCompatibleCreatures_FilterOutInvalidTypes()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithoutAbility(AbilityConstants.Intelligence)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithAlignments(AlignmentConstants.ChaoticEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3", "subtype 1")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithAlignments(AlignmentConstants.NeutralEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithAlignments(AlignmentConstants.ChaoticNeutral, "other alignment")
                    .WithHitDiceQuantity(4)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 3")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithAlignments(AlignmentConstants.NeutralGood)
                    .WithHitDiceQuantity(4)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 4")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 3", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithAlignments(AlignmentConstants.TrueNeutral, "other alignment")
                    .WithHitDiceQuantity(4)
                    .Build(),
            };

            var filters = new Filters { Type = "subtype 1" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                "subtype 1",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithoutAbility(AbilityConstants.Intelligence)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3", "subtype 1")
                    .WithAlignments(AlignmentConstants.NeutralEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticNeutral, "other alignment")
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 3")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.NeutralGood)
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 4")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 2", "subtype 3")
                    .WithAlignments(AlignmentConstants.TrueNeutral, "other alignment")
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 5")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 2", "subtype 1")
                    .WithAlignments(AlignmentConstants.LawfulNeutral, "other alignment")
                    .WithHitDiceQuantity(666)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 6")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1")
                    .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .WithChallengeRating(ChallengeRatingConstants.CR2)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
            };

            var filters = new Filters { Type = "subtype 1", ChallengeRating = ChallengeRatingConstants.CR2 };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                "subtype 1",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_WithLevelAdjustments()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 9266)
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
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3", "subtype 1")
                    .WithAlignments(AlignmentConstants.NeutralEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithLevelAdjustment(2)
                    .WithCasterLevel(4)
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.EqualTo(3));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(4));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                "subtype 1",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.EqualTo(4));
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.EqualTo(6));
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_WithValidAlignments()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.ChaoticNeutral, "other alignment", "other Good")
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3", "subtype 1")
                    .WithAlignments(AlignmentConstants.TrueNeutral, "wrong Good")
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.ChaoticEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                "subtype 1",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_WithImprovedTemplateAdjustments()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 9266, 96)
                    .WithAbility(AbilityConstants.Constitution, 90210, 783)
                    .WithAbility(AbilityConstants.Dexterity, 42, 8245)
                    .WithAbility(AbilityConstants.Intelligence, 600, 69)
                    .WithAbility(AbilityConstants.Wisdom, 1337, 420)
                    .WithAbility(AbilityConstants.Charisma, 1336, -1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3", "subtype 1")
                    .WithAlignments(AlignmentConstants.NeutralEvil, "other alignment")
                    .WithHitDiceQuantity(4)
                    .WithAbility(AbilityConstants.Strength, 2, -3)
                    .WithAbility(AbilityConstants.Constitution, -4, 5)
                    .WithAbility(AbilityConstants.Dexterity, 6, 7)
                    .WithAbility(AbilityConstants.Intelligence, 8, -9)
                    .WithAbility(AbilityConstants.Wisdom, -10, 11)
                    .WithAbility(AbilityConstants.Charisma, 12, 13)
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 96 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 - 1 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 69 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337 + 420));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 8245 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 783 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(4));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                "subtype 1",
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 12 + 13 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 8 - 9 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore - 10 + 11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 6 + 7 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore - 4 + 5 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 2 - 3 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment(AlignmentConstants.NeutralEvil),
                new Alignment("other Evil"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR2));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(4));
        }
    }
}
