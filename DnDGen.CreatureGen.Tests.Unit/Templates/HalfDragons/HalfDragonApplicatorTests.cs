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
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Languages;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Templates.HalfDragons;
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

namespace DnDGen.CreatureGen.Tests.Unit.Templates.HalfDragons
{
    [TestFixture]
    public class HalfDragonApplicatorTests
    {
        private Dictionary<string, TemplateApplicator> applicators;
        private Creature baseCreature;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<Dice> mockDice;
        private Mock<ISpeedsGenerator> mockSpeedsGenerator;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ISkillsGenerator> mockSkillsGenerator;
        private Mock<IAlignmentGenerator> mockAlignmentGenerator;
        private Mock<IMagicGenerator> mockMagicGenerator;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;

        private static IEnumerable<string> templates = new[]
        {
            CreatureConstants.Templates.HalfDragon_Black,
            CreatureConstants.Templates.HalfDragon_Blue,
            CreatureConstants.Templates.HalfDragon_Green,
            CreatureConstants.Templates.HalfDragon_Red,
            CreatureConstants.Templates.HalfDragon_White,
            CreatureConstants.Templates.HalfDragon_Brass,
            CreatureConstants.Templates.HalfDragon_Bronze,
            CreatureConstants.Templates.HalfDragon_Copper,
            CreatureConstants.Templates.HalfDragon_Gold,
            CreatureConstants.Templates.HalfDragon_Silver,
        };

        private static IEnumerable AllHalfDragonTemplates => templates.Select(t => new TestCaseData(t));

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
            mockMagicGenerator = new Mock<IMagicGenerator>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();

            applicators = new Dictionary<string, TemplateApplicator>();
            applicators[CreatureConstants.Templates.HalfDragon_Black] = new HalfDragonBlackApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockCreatureDataSelector.Object,
                mockAdjustmentSelector.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Blue] = new HalfDragonBlueApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockCreatureDataSelector.Object,
                mockAdjustmentSelector.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Brass] = new HalfDragonBrassApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockCreatureDataSelector.Object,
                mockAdjustmentSelector.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Bronze] = new HalfDragonBronzeApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockCreatureDataSelector.Object,
                mockAdjustmentSelector.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Copper] = new HalfDragonCopperApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockCreatureDataSelector.Object,
                mockAdjustmentSelector.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Gold] = new HalfDragonGoldApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockCreatureDataSelector.Object,
                mockAdjustmentSelector.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Green] = new HalfDragonGreenApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockCreatureDataSelector.Object,
                mockAdjustmentSelector.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Red] = new HalfDragonRedApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockCreatureDataSelector.Object,
                mockAdjustmentSelector.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Silver] = new HalfDragonSilverApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockCreatureDataSelector.Object,
                mockAdjustmentSelector.Object);
            applicators[CreatureConstants.Templates.HalfDragon_White] = new HalfDragonWhiteApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockCreatureDataSelector.Object,
                mockAdjustmentSelector.Object);

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .Build();
            baseCreature.HitPoints.HitDice[0].HitDie = 8;

            mockSpeedsGenerator
                .Setup(g => g.Generate(It.IsAny<string>()))
                .Returns((string t) =>
                {
                    var speeds = new Dictionary<string, Measurement>();
                    speeds[SpeedConstants.Fly] = new Measurement($"{t} furlongs");
                    speeds[SpeedConstants.Fly].Description = "the goodest";
                    speeds[SpeedConstants.Fly].Value = 666;

                    return speeds;
                });

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

            mockAlignmentGenerator
                .Setup(g => g.Generate(It.IsAny<string>(), null, null))
                .Returns((string c, string t, string p) => new Alignment { Lawfulness = $"{c}y", Goodness = "scaley" });

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => new[] { $"other alignment", $"{c.Replace(GroupConstants.Exploded, string.Empty)}y scaley", "preset alignment" });
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible(string template)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Outsider;

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {template}");

            Assert.That(() => applicators[template].ApplyTo(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCaseSource(nameof(IncompatibleFilters))]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible_WithFilters(string template, bool asCharacter, string type, string challengeRating, string alignment)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment($"original alignment");

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {template}");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            Assert.That(() => applicators[template].ApplyTo(baseCreature, asCharacter, type, challengeRating, alignment),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        private static IEnumerable IncompatibleFilters
        {
            get
            {
                foreach (var template in templates)
                {
                    yield return new TestCaseData(template, false, "subtype 1", ChallengeRatingConstants.CR3, $"wrong alignment");
                    yield return new TestCaseData(template, false, "subtype 1", ChallengeRatingConstants.CR2, $"{template}y scaley");
                    yield return new TestCaseData(template, false, "wrong subtype", ChallengeRatingConstants.CR3, $"{template}y scaley");
                    //INFO: This test case isn't valid, since As Character doesn't affect already-generated creature compatibility
                    //yield return new TestCaseData(template, true, "subtype 1", ChallengeRatingConstants.CR3, $"{template}y scaley");
                }
            }
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_ReturnsCreature_WithFilters(string template)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment($"original alignment");

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

            var creature = applicators[template].ApplyTo(baseCreature, false, "subtype 1", ChallengeRatingConstants.CR3, $"{template}y scaley");
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_SetsTemplate(string template)
        {
            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_SetsTemplate(string template)
        {
            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCaseSource(nameof(CreatureTypeAdjusted))]
        public void ApplyTo_CreatureTypeIsAdjusted(string template, string original)
        {
            baseCreature.Type.Name = original;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(4));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(original)
                .And.Contains(CreatureConstants.Types.Subtypes.Augmented));
        }

        private static IEnumerable CreatureTypeAdjusted
        {
            get
            {
                var creatureTypes = new[]
                {
                    CreatureConstants.Types.Aberration,
                    CreatureConstants.Types.Animal,
                    CreatureConstants.Types.Fey,
                    CreatureConstants.Types.Giant,
                    CreatureConstants.Types.Humanoid,
                    CreatureConstants.Types.MagicalBeast,
                    CreatureConstants.Types.MonstrousHumanoid,
                    CreatureConstants.Types.Ooze,
                    CreatureConstants.Types.Plant,
                    CreatureConstants.Types.Vermin,
                };

                foreach (var template in templates)
                {
                    foreach (var creatureType in creatureTypes)
                    {
                        yield return new TestCaseData(template, creatureType);
                    }
                }
            }
        }

        [TestCaseSource(nameof(HitDieIncreased))]
        public void ApplyTo_HitDieIncreases(string template, int original, int increased)
        {
            baseCreature.HitPoints.HitDice[0].HitDie = original;
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

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(increased));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        private static IEnumerable HitDieIncreased
        {
            get
            {
                var hitDieIncreases = new[]
                {
                    (4, 6),
                    (6, 8),
                    (8, 10),
                    (10, 12),
                    (12, 12),
                };

                foreach (var template in templates)
                {
                    foreach (var hitDieIncrease in hitDieIncreases)
                    {
                        yield return new TestCaseData(template, hitDieIncrease.Item1, hitDieIncrease.Item2);
                    }
                }
            }
        }

        [TestCaseSource(nameof(HitDieIncreased))]
        public void ApplyTo_HitDieIncreases_WithBoostedConstitution(string template, int original, int increased)
        {
            baseCreature.HitPoints.HitDice[0].HitDie = original;
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

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(increased));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210 + 2 * 17));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(96 + baseCreature.HitPoints.RoundedHitDiceQuantity * 17));
        }

        [TestCaseSource(nameof(GainFlySpeed))]
        public void ApplyTo_GainFlySpeed(string template, string size, int landSpeed, int flySpeed)
        {
            baseCreature.Size = size;
            baseCreature.Speeds[SpeedConstants.Land].Value = landSpeed;

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Land));
            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(landSpeed));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo($"{template} furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(flySpeed));
        }

        private static IEnumerable GainFlySpeed
        {
            get
            {
                var sizes = new[]
                {
                    SizeConstants.Large,
                    SizeConstants.Huge,
                    SizeConstants.Gargantuan,
                    SizeConstants.Colossal,
                };

                var speeds = new[]
                {
                    (1, 2),
                    (2, 4),
                    (5, 10),
                    (10, 20),
                    (20, 40),
                    (30, 60),
                    (40, 80),
                    (42, 84),
                    (50, 100),
                    (60, 120),
                    (70, 120),
                    (80, 120),
                    (90, 120),
                    (100, 120),
                };

                foreach (var template in templates)
                {
                    foreach (var size in sizes)
                    {
                        foreach (var speed in speeds)
                        {
                            yield return new TestCaseData(template, size, speed.Item1, speed.Item2);
                        }
                    }
                }
            }
        }

        [TestCaseSource(nameof(DoNotGainFlySpeed))]
        public void ApplyTo_DoNotGainFlySpeed_TooSmall(string template, string size)
        {
            baseCreature.Size = size;
            baseCreature.Speeds[SpeedConstants.Land].Value = 42;

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(1)
                .And.Not.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Land));
            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(42));
        }

        private static IEnumerable DoNotGainFlySpeed
        {
            get
            {
                var sizes = new[]
                {
                    SizeConstants.Fine,
                    SizeConstants.Diminutive,
                    SizeConstants.Tiny,
                    SizeConstants.Small,
                    SizeConstants.Medium,
                };

                foreach (var template in templates)
                {
                    foreach (var size in sizes)
                    {
                        yield return new TestCaseData(template, size);
                    }
                }
            }
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_DoNotGainFlySpeed_HasNoLandSpeed(string template)
        {
            baseCreature.Size = SizeConstants.Large;
            baseCreature.Speeds.Clear();
            baseCreature.Speeds[SpeedConstants.Swim] = new Measurement("feet per round") { Value = 42 };

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(1)
                .And.Not.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Swim));
            Assert.That(creature.Speeds[SpeedConstants.Swim].Value, Is.EqualTo(42));
            Assert.That(creature.Speeds[SpeedConstants.Swim].Unit, Is.EqualTo("feet per round"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_DoNotGainFlySpeed_AlreadyHasFlySpeed(string template)
        {
            baseCreature.Size = SizeConstants.Large;
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 42, Description = "so-so maneuverability" };

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Land));
            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(600));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(42));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("feet per round"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("so-so maneuverability"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_DoNotGainFlySpeed_HasNoLandSpeed(string template)
        {
            baseCreature.Size = SizeConstants.Large;
            baseCreature.Speeds.Clear();
            baseCreature.Speeds[SpeedConstants.Swim] = new Measurement("feet per round") { Value = 42 };

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(1)
                .And.Not.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Swim));
            Assert.That(creature.Speeds[SpeedConstants.Swim].Value, Is.EqualTo(42));
            Assert.That(creature.Speeds[SpeedConstants.Swim].Unit, Is.EqualTo("feet per round"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_DoNotGainFlySpeed_AlreadyHasFlySpeed(string template)
        {
            baseCreature.Size = SizeConstants.Large;
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 42, Description = "so-so maneuverability" };

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Land));
            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(600));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(42));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("feet per round"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("so-so maneuverability"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_AdjustAbilities(string template)
        {
            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(8));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(2));
        }

        [TestCaseSource(nameof(AbilitiesImprove_DoNotImproveMissingAbility))]
        public void ApplyTo_AbilitiesImprove_DoNotImproveMissingAbility(string template, string ability)
        {
            baseCreature.Abilities[ability].BaseScore = 0;

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.Abilities[ability].HasScore, Is.False);
            Assert.That(creature.Abilities[ability].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[ability].TemplateAdjustment, Is.Zero);
        }

        private static IEnumerable AbilitiesImprove_DoNotImproveMissingAbility
        {
            get
            {
                var abilities = new[]
                {
                    AbilityConstants.Charisma,
                    AbilityConstants.Constitution,
                    AbilityConstants.Dexterity,
                    AbilityConstants.Intelligence,
                    AbilityConstants.Strength,
                    AbilityConstants.Wisdom,
                };

                foreach (var template in templates)
                {
                    foreach (var ability in abilities)
                    {
                        yield return new TestCaseData(template, ability);
                    }
                }
            }
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainNaturalArmor(string template)
        {
            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(4));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(4));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_ImproveNaturalArmor(string template)
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(9270));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_ImprovesBestNaturalArmorBonus(string template)
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 90210);

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(90214));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.False);

            bonus = creature.ArmorClass.NaturalArmorBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(90214));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_ImproveNaturalArmor_PreserveConditions(string template)
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266, "only sometimes");

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Condition, Is.EqualTo("only sometimes"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainAttacks(string template)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon claw roll", Type = "dragon claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon bite roll", Type = "dragon bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
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
            var creature = applicators[template].ApplyTo(baseCreature, false);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(newAttacks));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainAttacks_WithAttackBonuses(string template)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon claw roll", Type = "dragon claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon bite roll", Type = "dragon bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
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
                    baseCreature.Abilities,
                    true))
                .Returns(newSkills);

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
                    newSkills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = $"{template}y", Goodness = "scaley" }))
                .Returns(newQualities);

            var attacksWithBonuses = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon claw roll", Type = "dragon claw type" }
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
                        new Damage { Roll = "dragon bite roll", Type = "dragon bite type" }
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
            var creature = applicators[template].ApplyTo(baseCreature, false);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + attacksWithBonuses.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(attacksWithBonuses));
        }

        [TestCaseSource(nameof(GainAttacks_DuplicateAttack))]
        public void ApplyTo_GainAttacks_DuplicateClawAttacks(string template, int baseClawMax, int dragonClawMax, string expectedClawDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon roll", Type = "dragon type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon bite roll", Type = "dragon bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
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

            var claw = new Attack
            {
                Name = "Claw",
                Damages = new List<Damage>
                    {
                        new Damage { Roll = "base roll", Type = "base type" }
                    },
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { claw });

            mockDice
                .Setup(d => d.Roll("base roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("dragon roll").AsPotentialMaximum<int>(true))
                .Returns(dragonClawMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicators[template].ApplyTo(baseCreature, false);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageDescription, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(newAttacks[3]));
            Assert.That(bites[0].DamageDescription, Is.EqualTo("dragon bite roll dragon bite type"));
        }

        private static IEnumerable GainAttacks_DuplicateAttack
        {
            get
            {
                var damages = new[]
                {
                    (9266, 90210, "dragon roll dragon type"),
                    (9266, 42, "base roll base type"),
                };

                foreach (var template in templates)
                {
                    foreach (var damage in damages)
                    {
                        yield return new TestCaseData(template, damage.Item1, damage.Item2, damage.Item3);
                    }
                }
            }
        }

        [TestCaseSource(nameof(GainAttacks_DuplicateAttack))]
        public void ApplyTo_GainAttacks_DuplicateBiteAttack(string template, int baseBiteMax, int dragonBiteMax, string expectedBiteDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon claw roll", Type = "dragon claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon roll", Type = "dragon type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
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

            var bite = new Attack
            {
                Name = "Bite",
                Damages = new List<Damage>
                    {
                        new Damage { Roll = "base roll", Type = "base type" }
                    },
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { bite });

            mockDice
                .Setup(d => d.Roll("base roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("dragon roll").AsPotentialMaximum<int>(true))
                .Returns(dragonBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicators[template].ApplyTo(baseCreature, false);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(newAttacks[2]));
            Assert.That(claws[0].DamageDescription, Is.EqualTo("dragon claw roll dragon claw type"));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageDescription, Is.EqualTo(expectedBiteDamage));
        }

        [TestCaseSource(nameof(GainAttacks_DuplicateAttacks))]
        public void ApplyTo_GainAttacks_DuplicateClawAndBiteAttack(string template,
            int baseClawMax, int dragonClawMax, string expectedClawDescription,
            int baseBiteMax, int dragonBiteMax, string expectedBiteDescription)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon claw roll", Type = "dragon claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon bite roll", Type = "dragon bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
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
                .Setup(d => d.Roll("dragon claw roll").AsPotentialMaximum<int>(true))
                .Returns(dragonClawMax);
            mockDice
                .Setup(d => d.Roll("base bite roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("dragon bite roll").AsPotentialMaximum<int>(true))
                .Returns(dragonBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicators[template].ApplyTo(baseCreature, false);
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 2));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageDescription, Is.EqualTo(expectedClawDescription));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageDescription, Is.EqualTo(expectedBiteDescription));
        }

        private static IEnumerable GainAttacks_DuplicateAttacks
        {
            get
            {
                var clawDamages = new[]
                {
                    (9266, 90210, "dragon claw roll dragon claw type"),
                    (9266, 42, "base claw roll base claw type"),
                };
                var biteDamages = new[]
                {
                    (1336, 1337, "dragon bite roll dragon bite type"),
                    (1336, 600, "base bite roll base bite type"),
                };

                foreach (var template in templates)
                {
                    foreach (var clawDamage in clawDamages)
                    {
                        foreach (var biteDamage in biteDamages)
                        {
                            yield return new TestCaseData(template,
                                clawDamage.Item1, clawDamage.Item2, clawDamage.Item3,
                                biteDamage.Item1, biteDamage.Item2, biteDamage.Item3);
                        }
                    }
                }
            }
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainSpecialQualities(string template)
        {
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
                    baseCreature.Abilities,
                    true))
                .Returns(newSkills);

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
                    newSkills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = $"{template}y", Goodness = "scaley" }))
                .Returns(newQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = applicators[template].ApplyTo(baseCreature, false);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + newQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(newQualities));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainsSkillPoints(string template)
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
                    baseCreature.Abilities,
                    true))
                .Returns(newSkills);

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.Skills, Is.EqualTo(newSkills));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainsSkillPoints_NoSkills(string template)
        {
            baseCreature.Skills = Enumerable.Empty<Skill>();

            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.Is<IEnumerable<Skill>>(ss =>
                        ss == baseCreature.Skills
                        && !ss.Any()),
                    baseCreature.HitPoints,
                    baseCreature.Type,
                    baseCreature.Abilities,
                    true))
                .Returns(Enumerable.Empty<Skill>());

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.Skills, Is.Empty);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void ApplyTo_ChallengeRatingAdjusted(string template, string original, string adjusted)
        {
            baseCreature.ChallengeRating = original;

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        private static IEnumerable ChallengeRatingAdjustments
        {
            get
            {
                var challengeRatings = ChallengeRatingConstants.GetOrdered();

                foreach (var template in templates)
                {
                    foreach (var challengeRating in challengeRatings)
                    {
                        var increased = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2);

                        if (ChallengeRatingConstants.IsGreaterThan(ChallengeRatingConstants.CR3, increased))
                            yield return new TestCaseData(template, challengeRating, ChallengeRatingConstants.CR3);
                        else
                            yield return new TestCaseData(template, challengeRating, increased);
                    }
                }
            }
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GetNewAlignment(string template)
        {
            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature.Alignment.Lawfulness, Is.EqualTo($"{template}y"));
            Assert.That(creature.Alignment.Goodness, Is.EqualTo($"scaley"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GetPresetAlignment(string template)
        {
            mockAlignmentGenerator
                .Setup(g => g.Generate(template, null, "preset alignment"))
                .Returns((string c, string t, string p) => new Alignment(p));

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, template + GroupConstants.Exploded))
                .Returns((string t, string c) => new[] { $"other alignment", $"preset alignment" });

            var creature = applicators[template].ApplyTo(baseCreature, false, alignment: "preset alignment");
            Assert.That(creature.Alignment.Lawfulness, Is.EqualTo("preset"));
            Assert.That(creature.Alignment.Goodness, Is.EqualTo("alignment"));
        }

        [TestCaseSource(nameof(LevelAdjustmentIncreased))]
        public void ApplyTo_LevelAdjustmentIncreased(string template, int? adjustment, int? adjusted)
        {
            baseCreature.LevelAdjustment = adjustment;

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        private static IEnumerable LevelAdjustmentIncreased
        {
            get
            {
                var levels = new (int?, int?)[]
                {
                    (null, null),
                    (0, 3),
                    (1, 4),
                    (2, 5),
                    (10, 13),
                    (42, 45),
                };

                foreach (var template in templates)
                {
                    foreach (var level in levels)
                    {
                        yield return new TestCaseData(template, level.Item1, level.Item2);
                    }
                }
            }
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible(string template)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Outsider;

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {template}");

            Assert.That(async () => await applicators[template].ApplyToAsync(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCaseSource(nameof(IncompatibleFilters))]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible_WithFilters(string template, bool asCharacter, string type, string challengeRating, string alignment)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment($"original alignment");

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {template}");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            Assert.That(async () => await applicators[template].ApplyToAsync(baseCreature, asCharacter, type, challengeRating, alignment),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_ReturnsCreature_WithFilters(string template)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment($"original alignment");

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

            var creature = await applicators[template].ApplyToAsync(baseCreature, false, "subtype 1", ChallengeRatingConstants.CR3, $"{template}y scaley");
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCaseSource(nameof(CreatureTypeAdjusted))]
        public async Task ApplyToAsync_CreatureTypeIsAdjusted(string template, string original)
        {
            baseCreature.Type.Name = original;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(4));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(original)
                .And.Contains(CreatureConstants.Types.Subtypes.Augmented));
        }

        [TestCaseSource(nameof(HitDieIncreased))]
        public async Task ApplyToAsync_HitDieIncreases(string template, int original, int increased)
        {
            baseCreature.HitPoints.HitDice[0].HitDie = original;
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

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(increased));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [TestCaseSource(nameof(HitDieIncreased))]
        public async Task ApplyToAsync_HitDieIncreases_WithBoostedConstitution(string template, int original, int increased)
        {
            baseCreature.HitPoints.HitDice[0].HitDie = original;
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

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(increased));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210 + 2 * 17));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(96 + baseCreature.HitPoints.RoundedHitDiceQuantity * 17));
        }

        [TestCaseSource(nameof(GainFlySpeed))]
        public async Task ApplyToAsync_GainFlySpeed(string template, string size, int landSpeed, int flySpeed)
        {
            baseCreature.Size = size;
            baseCreature.Speeds[SpeedConstants.Land].Value = landSpeed;

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo($"{template} furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(flySpeed));
        }

        [TestCaseSource(nameof(DoNotGainFlySpeed))]
        public async Task ApplyToAsync_DoNotGainFlySpeed(string template, string size)
        {
            baseCreature.Size = size;
            baseCreature.Speeds[SpeedConstants.Land].Value = 42;

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(1)
                .And.Not.ContainKey(SpeedConstants.Fly));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_AdjustAbilities(string template)
        {
            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(8));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(2));
        }

        [TestCaseSource(nameof(AbilitiesImprove_DoNotImproveMissingAbility))]
        public async Task ApplyToAsync_AbilitiesImprove_DoNotImproveMissingAbility(string template, string ability)
        {
            baseCreature.Abilities[ability].BaseScore = 0;

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature.Abilities[ability].HasScore, Is.False);
            Assert.That(creature.Abilities[ability].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[ability].TemplateAdjustment, Is.Zero);
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainNaturalArmor(string template)
        {
            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(4));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(4));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_ImproveNaturalArmor(string template)
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(9270));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_ImprovesBestNaturalArmorBonus(string template)
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 90210);

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(90214));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.False);

            bonus = creature.ArmorClass.NaturalArmorBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(90214));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_ImproveNaturalArmor_PreserveConditions(string template)
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266, "only sometimes");

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Condition, Is.EqualTo("only sometimes"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainAttacks(string template)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon claw roll", Type = "dragon claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon bite roll", Type = "dragon bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
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
            var creature = await applicators[template].ApplyToAsync(baseCreature, false);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(newAttacks));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainAttacks_WithAttackBonuses(string template)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon claw roll", Type = "dragon claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon bite roll", Type = "dragon bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
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
                    baseCreature.Abilities,
                    true))
                .Returns(newSkills);

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
                    newSkills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = $"{template}y", Goodness = "scaley" }))
                .Returns(newQualities);

            var attacksWithBonuses = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon claw roll", Type = "dragon claw type" }
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
                        new Damage { Roll = "dragon bite roll", Type = "dragon bite type" }
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
            var creature = await applicators[template].ApplyToAsync(baseCreature, false);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + attacksWithBonuses.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(attacksWithBonuses));
        }

        [TestCaseSource(nameof(GainAttacks_DuplicateAttack))]
        public async Task ApplyToAsync_GainAttacks_DuplicateClawAttacks(string template, int baseClawMax, int dragonClawMax, string expectedClawDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon roll", Type = "dragon type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon bite roll", Type = "dragon bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
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

            var claw = new Attack
            {
                Name = "Claw",
                Damages = new List<Damage>
                    {
                        new Damage { Roll = "base roll", Type = "base type" }
                    },
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { claw });

            mockDice
                .Setup(d => d.Roll("base roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("dragon roll").AsPotentialMaximum<int>(true))
                .Returns(dragonClawMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageDescription, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(newAttacks[3]));
            Assert.That(bites[0].DamageDescription, Is.EqualTo("dragon bite roll dragon bite type"));
        }

        [TestCaseSource(nameof(GainAttacks_DuplicateAttack))]
        public async Task ApplyToAsync_GainAttacks_DuplicateBiteAttack(string template, int baseBiteMax, int dragonBiteMax, string expectedBiteDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon claw roll", Type = "dragon claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon roll", Type = "dragon type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
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

            var bite = new Attack
            {
                Name = "Bite",
                Damages = new List<Damage>
                    {
                        new Damage { Roll = "base roll", Type = "base type" }
                    },
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { bite });

            mockDice
                .Setup(d => d.Roll("base roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("dragon roll").AsPotentialMaximum<int>(true))
                .Returns(dragonBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(newAttacks[2]));
            Assert.That(claws[0].DamageDescription, Is.EqualTo("dragon claw roll dragon claw type"));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageDescription, Is.EqualTo(expectedBiteDamage));
        }

        [TestCaseSource(nameof(GainAttacks_DuplicateAttacks))]
        public async Task ApplyToAsync_GainAttacks_DuplicateClawAndBiteAttack(string template,
            int baseClawMax, int dragonClawMax, string expectedClawDescription,
            int baseBiteMax, int dragonBiteMax, string expectedBiteDescription)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon claw roll", Type = "dragon claw type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "dragon bite roll", Type = "dragon bite type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
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
                .Setup(d => d.Roll("dragon claw roll").AsPotentialMaximum<int>(true))
                .Returns(dragonClawMax);
            mockDice
                .Setup(d => d.Roll("base bite roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("dragon bite roll").AsPotentialMaximum<int>(true))
                .Returns(dragonBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 2));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageDescription, Is.EqualTo(expectedClawDescription));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageDescription, Is.EqualTo(expectedBiteDescription));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainSpecialQualities(string template)
        {
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
                    baseCreature.Abilities,
                    true))
                .Returns(newSkills);

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
                    newSkills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = $"{template}y", Goodness = "scaley" }))
                .Returns(newQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = await applicators[template].ApplyToAsync(baseCreature, false);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + newQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(newQualities));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainsSkillPoints(string template)
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
                    baseCreature.Abilities,
                    true))
                .Returns(newSkills);

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature.Skills, Is.EqualTo(newSkills));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainsSkillPoints_NoSkills(string template)
        {
            baseCreature.Skills = Enumerable.Empty<Skill>();

            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.Is<IEnumerable<Skill>>(ss =>
                        ss == baseCreature.Skills
                        && !ss.Any()),
                    baseCreature.HitPoints,
                    baseCreature.Type,
                    baseCreature.Abilities,
                    true))
                .Returns(Enumerable.Empty<Skill>());

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature.Skills, Is.Empty);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public async Task ApplyToAsync_ChallengeRatingAdjusted(string template, string original, string adjusted)
        {
            baseCreature.ChallengeRating = original;

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GetNewAlignment(string template)
        {
            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Lawfulness, Is.EqualTo($"{template}y"));
            Assert.That(creature.Alignment.Goodness, Is.EqualTo($"scaley"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GetPresetAlignment(string template)
        {
            mockAlignmentGenerator
                .Setup(g => g.Generate(template, null, "preset alignment"))
                .Returns((string c, string t, string p) => new Alignment(p));

            var creature = await applicators[template].ApplyToAsync(baseCreature, false, alignment: "preset alignment");
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Lawfulness, Is.EqualTo($"preset"));
            Assert.That(creature.Alignment.Goodness, Is.EqualTo($"alignment"));
        }

        [TestCaseSource(nameof(LevelAdjustmentIncreased))]
        public async Task ApplyToAsync_LevelAdjustmentIncreased(string template, int? adjustment, int? adjusted)
        {
            baseCreature.LevelAdjustment = adjustment;

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainDraconicAsLanguage(string template)
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"{template}ish"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainDraconicAsLanguage_NoLanguages(string template)
        {
            baseCreature.Languages = Enumerable.Empty<string>();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages, Is.Empty);
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainDraconicAsLanguage_AlreadyHasDraconic(string template)
        {
            baseCreature.Languages = baseCreature.Languages.Union(new[] { $"{template}ish" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"{template}ish"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainNewBonusLanguages(string template)
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 12;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"{template}ish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"{template}ish")
                .And.Contains("Drachensprach"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainBonusLanguages(string template)
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"{template}ish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"{template}ish")
                .And.Contains("Drachensprach"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainNoBonusLanguages(string template)
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 8;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"{template}ish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"{template}ish"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GainAllBonusLanguages(string template)
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            baseCreature.Languages = baseCreature.Languages.Union(new[] { $"{template}ish", "Lizard", "Latin" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"{template}ish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"{template}ish")
                .And.Contains("Drachensprach")
                .And.Contains("Latin"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainDraconicAsLanguage(string template)
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"{template}ish"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainDraconicAsLanguage_NoLanguages(string template)
        {
            baseCreature.Languages = Enumerable.Empty<string>();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages, Is.Empty);
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainDraconicAsLanguage_AlreadyHasDraconic(string template)
        {
            baseCreature.Languages = baseCreature.Languages.Union(new[] { $"{template}ish" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"{template}ish"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainNewBonusLanguages(string template)
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 12;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"{template}ish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"{template}ish")
                .And.Contains("Drachensprach"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainBonusLanguages(string template)
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"{template}ish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"{template}ish")
                .And.Contains("Drachensprach"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainNoBonusLanguages(string template)
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 8;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"{template}ish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"{template}ish"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainAllBonusLanguages(string template)
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            baseCreature.Languages = baseCreature.Languages.Union(new[] { $"{template}ish", "Lizard", "Latin" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Automatic))
                .Returns($"{template}ish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    template + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"{template}ish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"{template}ish")
                .And.Contains("Drachensprach")
                .And.Contains("Latin"));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_RegenerateMagic(string template)
        {
            var dragonMagic = new Magic();
            mockMagicGenerator
                .Setup(g => g.GenerateWith(
                    baseCreature.Name,
                    new Alignment { Lawfulness = $"{template}y", Goodness = "scaley" },
                    baseCreature.Abilities,
                    baseCreature.Equipment))
                .Returns(dragonMagic);

            var creature = applicators[template].ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Magic, Is.EqualTo(dragonMagic));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_RegenerateMagic(string template)
        {
            var dragonMagic = new Magic();
            mockMagicGenerator
                .Setup(g => g.GenerateWith(
                    baseCreature.Name,
                    new Alignment { Lawfulness = $"{template}y", Goodness = "scaley" },
                    baseCreature.Abilities,
                    baseCreature.Equipment))
                .Returns(dragonMagic);

            var creature = await applicators[template].ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Magic, Is.EqualTo(dragonMagic));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures(string template)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

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

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(creatures, false);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void GetCompatibleCreatures_ReturnEmpty_IfAlignmentFilterNotValid(string template)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

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

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, template + GroupConstants.Exploded))
                .Returns(new[] { "my alignment", "other alignment", "other wrong alignment" });

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(creatures, false, alignment: "wrong alignment");
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures_IfAlignmentFilterValid(string template)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

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

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, template + GroupConstants.Exploded))
                .Returns(new[] { "my alignment", "other alignment", "preset alignment", "wrong alignment" });

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(creatures, false, alignment: "preset alignment");
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void GetCompatibleCreatures_WithChallengeRating_ReturnCompatibleCreatures(string template, string original, string challengeRating)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = challengeRating };

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

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(creatures, false, challengeRating: challengeRating);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCaseSource(nameof(CreatureTypeFilter_FromTemplate))]
        public void GetCompatibleCreatures_WithType_ReturnCompatibleCreatures(string template, string type)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

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

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(creatures, false, type: type);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        private static IEnumerable CreatureTypeFilter_FromTemplate
        {
            get
            {
                foreach (var template in templates)
                {
                    yield return new TestCaseData(template, CreatureConstants.Types.Dragon);
                    yield return new TestCaseData(template, CreatureConstants.Types.Subtypes.Augmented);
                }
            }
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void GetCompatibleCreatures_WithType_ReturnCompatibleCreatures_FilterOutInvalidTypes(string template)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 3", "subtype 1" };
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

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(creatures, false, type: "subtype 2");
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void GetCompatibleCreatures_WithTypeAndChallengeRating_ReturnCompatibleCreatures(string template)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2", "wrong creature 3" };

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 3", "subtype 1" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2", "subtype 1" };
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

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR3 };

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
            hitDice["wrong creature 3"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(creatures, false, type: "subtype 2", challengeRating: ChallengeRatingConstants.CR4);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCaseSource(nameof(CreatureTypeCompatible))]
        public void IsCompatible_BasedOnCreatureType(string template, string creatureType, bool compatible)
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

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable CreatureTypeCompatible
        {
            get
            {
                var compatibilities = new[]
                {
                    (CreatureConstants.Types.Aberration, true),
                    (CreatureConstants.Types.Animal, true),
                    (CreatureConstants.Types.Construct, false),
                    (CreatureConstants.Types.Dragon, false),
                    (CreatureConstants.Types.Elemental, false),
                    (CreatureConstants.Types.Fey, true),
                    (CreatureConstants.Types.Giant, true),
                    (CreatureConstants.Types.Humanoid, true),
                    (CreatureConstants.Types.MagicalBeast, true),
                    (CreatureConstants.Types.MonstrousHumanoid, true),
                    (CreatureConstants.Types.Ooze, true),
                    (CreatureConstants.Types.Outsider, false),
                    (CreatureConstants.Types.Plant, true),
                    (CreatureConstants.Types.Undead, false),
                    (CreatureConstants.Types.Vermin, true),
                };

                foreach (var template in templates)
                {
                    foreach (var compatibility in compatibilities)
                    {
                        yield return new TestCaseData(template, compatibility.Item1, compatibility.Item2);
                    }
                }
            }
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void IsCompatible_CannotBeIncorporeal(string template)
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

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCaseSource(nameof(CreatureTypeCompatible_Filtered))]
        public void IsCompatible_TypeMustMatch(string template, string type, bool compatible)
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

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(new[] { "my creature" }, false, type: type);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable CreatureTypeCompatible_Filtered
        {
            get
            {
                foreach (var template in templates)
                {
                    yield return new TestCaseData(template, null, true);
                    yield return new TestCaseData(template, CreatureConstants.Types.Humanoid, true);
                    yield return new TestCaseData(template, CreatureConstants.Types.Dragon, true);
                    yield return new TestCaseData(template, "subtype 1", true);
                    yield return new TestCaseData(template, "subtype 2", true);
                    yield return new TestCaseData(template, CreatureConstants.Types.Subtypes.Augmented, true);
                    yield return new TestCaseData(template, "wrong type", false);
                }
            }
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_Filtered))]
        public void IsCompatible_ChallengeRatingMustMatch(string template, string original, string challengeRating, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(new[] { "my creature" }, false, challengeRating: challengeRating);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_Filtered_HumanoidCharacter))]
        public void IsCompatible_ChallengeRatingMustMatch_HumanoidAsCharacter(string template, double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = hitDiceQuantity;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(new[] { "my creature" }, true, challengeRating: challengeRating);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable ChallengeRatingAdjustments_Filtered_HumanoidCharacter
        {
            get
            {
                var hitDice = new[] { 0.5, 1, 2 };
                var challengeRatings = ChallengeRatingConstants.GetOrdered();

                foreach (var template in templates)
                {
                    foreach (var quantity in hitDice)
                    {
                        foreach (var cr in challengeRatings)
                        {
                            var increase = ChallengeRatingConstants.IncreaseChallengeRating(cr, 2);

                            if (quantity <= 1)
                            {
                                yield return new TestCaseData(template, quantity, cr, ChallengeRatingConstants.CR2, false);
                                yield return new TestCaseData(template, quantity, cr, ChallengeRatingConstants.CR3, true);
                                yield return new TestCaseData(template, quantity, cr, ChallengeRatingConstants.CR4, false);
                            }
                            else if (ChallengeRatingConstants.IsGreaterThan(ChallengeRatingConstants.CR3, increase))
                            {
                                yield return new TestCaseData(template, quantity, cr, cr, false);
                                yield return new TestCaseData(template, quantity, cr, ChallengeRatingConstants.CR2, false);
                                yield return new TestCaseData(template, quantity, cr, ChallengeRatingConstants.CR3, true);
                                yield return new TestCaseData(template, quantity, cr, ChallengeRatingConstants.CR4, false);
                            }
                            else
                            {
                                var low1 = ChallengeRatingConstants.IncreaseChallengeRating(cr, 1);
                                var high1 = ChallengeRatingConstants.IncreaseChallengeRating(increase, 1);

                                yield return new TestCaseData(template, quantity, cr, cr, false);
                                yield return new TestCaseData(template, quantity, cr, low1, false);
                                yield return new TestCaseData(template, quantity, cr, increase, true);
                                yield return new TestCaseData(template, quantity, cr, high1, false);
                            }
                        }
                    }
                }
            }
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_Filtered))]
        public void IsCompatible_ChallengeRatingMustMatch_NonHumanoidAsCharacter(string template, string original, string challengeRating, bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(new[] { "my creature" }, true, challengeRating: challengeRating);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable ChallengeRatingAdjustments_Filtered
        {
            get
            {
                var challengeRatings = ChallengeRatingConstants.GetOrdered();

                foreach (var template in templates)
                {
                    foreach (var cr in challengeRatings)
                    {
                        var increase = ChallengeRatingConstants.IncreaseChallengeRating(cr, 2);

                        if (ChallengeRatingConstants.IsGreaterThan(ChallengeRatingConstants.CR3, increase))
                        {
                            yield return new TestCaseData(template, cr, cr, false);
                            yield return new TestCaseData(template, cr, ChallengeRatingConstants.CR2, false);
                            yield return new TestCaseData(template, cr, ChallengeRatingConstants.CR3, true);
                            yield return new TestCaseData(template, cr, ChallengeRatingConstants.CR4, false);
                        }
                        else
                        {
                            var low1 = ChallengeRatingConstants.IncreaseChallengeRating(cr, 1);
                            var high1 = ChallengeRatingConstants.IncreaseChallengeRating(increase, 1);

                            yield return new TestCaseData(template, cr, cr, false);
                            yield return new TestCaseData(template, cr, low1, false);
                            yield return new TestCaseData(template, cr, increase, true);
                            yield return new TestCaseData(template, cr, high1, false);
                        }
                    }
                }
            }
        }

        [TestCaseSource(nameof(Alignments_Filtered))]
        public void IsCompatible_AlignmentMustMatch(
            string template,
            string dragonAlignment,
            string alignment,
            bool compatible)
        {
            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(TableNameConstants.Collection.CreatureGroups, "subtype 2"))
                .Returns(new[] { "wrong creature", "my creature" });

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, Size = SizeConstants.Medium };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments[template + GroupConstants.Exploded] = new[] { "other alignment", dragonAlignment, "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(new[] { "my creature" }, false, alignment: alignment);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable Alignments_Filtered
        {
            get
            {
                foreach (var template in templates)
                {
                    yield return new TestCaseData(template, "preset alignment", "preset alignment", true);
                    yield return new TestCaseData(template, "preset alignment", "wrong alignment", false);
                    yield return new TestCaseData(template, "other alignment", "preset alignment", false);
                }
            }
        }

        [TestCaseSource(nameof(AllFilters))]
        public void IsCompatible_AllFiltersMustMatch(string template, string type, string challengeRating, string alignment, bool compatible)
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

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 1;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments[template + GroupConstants.Exploded] = new[] { "other alignment", "preset alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string t, string c) => alignments[c]);

            var compatibleCreatures = applicators[template].GetCompatibleCreatures(new[] { "my creature" }, false, type, challengeRating, alignment);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable AllFilters
        {
            get
            {
                foreach (var template in templates)
                {
                    yield return new TestCaseData(template, CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR2, "preset alignment", false);
                    yield return new TestCaseData(template, CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR2, "wrong alignment", false);
                    yield return new TestCaseData(template, CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR4, "preset alignment", true);
                    yield return new TestCaseData(template, CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR4, "wrong alignment", false);
                    yield return new TestCaseData(template, "wrong subtype", ChallengeRatingConstants.CR2, "preset alignment", false);
                    yield return new TestCaseData(template, "wrong subtype", ChallengeRatingConstants.CR2, "wrong alignment", false);
                    yield return new TestCaseData(template, "wrong subtype", ChallengeRatingConstants.CR4, "preset alignment", false);
                    yield return new TestCaseData(template, "wrong subtype", ChallengeRatingConstants.CR4, "wrong alignment", false);
                }
            }
        }
    }
}
