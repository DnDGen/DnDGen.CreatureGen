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
using DnDGen.CreatureGen.Selectors.Selections;
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
    public class HalfDragonApplicatorTests
    {
        private HalfDragonApplicator applicator;
        private Creature baseCreature;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<Dice> mockDice;
        private Mock<ISpeedsGenerator> mockSpeedsGenerator;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ISkillsGenerator> mockSkillsGenerator;
        private Mock<IAlignmentGenerator> mockAlignmentGenerator;
        private Mock<IMagicGenerator> mockMagicGenerator;
        private Mock<ICollectionDataSelector<CreatureDataSelection>> mockCreatureDataSelector;
        private Mock<ICollectionTypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<ICreaturePrototypeFactory> mockPrototypeFactory;
        private Mock<IDemographicsGenerator> mockDemographicsGenerator;

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
            mockCreatureDataSelector = new Mock<ICollectionDataSelector<CreatureDataSelection>>();
            mockPrototypeFactory = new Mock<ICreaturePrototypeFactory>();
            mockTypeAndAmountSelector = new Mock<ICollectionTypeAndAmountSelector>();
            mockDemographicsGenerator = new Mock<IDemographicsGenerator>();

            applicator = new HalfDragonApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockCreatureDataSelector.Object,
                mockPrototypeFactory.Object,
                mockTypeAndAmountSelector.Object,
                mockDemographicsGenerator.Object);
            applicator.DragonSpecies = "my dragon species";

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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => [$"other alignment", $"{c}y scaley", "preset alignment"]);

            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, false, false))
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
            message.AppendLine("\tTemplate: my dragon species");

            Assert.That(() => applicator.ApplyTo(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCaseSource(nameof(IncompatibleFilters))]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible_WithFilters(
            bool asCharacter,
            string type,
            string challengeRating,
            string alignment,
            string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment($"original alignment");

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: my dragon species");
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

        private static IEnumerable IncompatibleFilters
        {
            get
            {
                yield return new TestCaseData(false, "subtype 1", ChallengeRatingConstants.CR3, "wrong alignment", "Alignment filter 'wrong alignment' is not valid");
                yield return new TestCaseData(false, "subtype 1", ChallengeRatingConstants.CR2, "my dragon speciesy scaley", "CR filter 2 does not match updated creature CR 3 (from CR 1)");
                yield return new TestCaseData(false, "wrong subtype", ChallengeRatingConstants.CR3, "my dragon speciesy scaley", "Type filter 'wrong subtype' is not valid");
                //INFO: This test case isn't valid, since As Character doesn't affect already-generated creature compatibility
                //yield return new TestCaseData(true, "subtype 1", ChallengeRatingConstants.CR3, $"my dragon speciesy scaley");
            }
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithFilters()
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

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR3;
            filters.Alignment = $"my dragon speciesy scaley";

            var creature = applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo("my dragon species"));
        }

        [Test]
        public void ApplyTo_SetsTemplate()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo("my dragon species"));
        }

        [Test]
        public async Task ApplyToAsync_SetsTemplate()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo("my dragon species"));
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithOtherTemplate()
        {
            baseCreature.Templates.Add("my other template");

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo("my dragon species"));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithOtherTemplate()
        {
            baseCreature.Templates.Add("my other template");

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo("my dragon species"));
        }

        [TestCaseSource(nameof(CreatureTypeAdjusted))]
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

                foreach (var creatureType in creatureTypes)
                {
                    yield return new TestCaseData(creatureType);
                }
            }
        }

        [TestCase(SizeConstants.Large)]
        [TestCase(SizeConstants.Huge)]
        [TestCase(SizeConstants.Gargantuan)]
        [TestCase(SizeConstants.Colossal)]
        public void ApplyTo_ReturnsCreature_WithAdjustedDemographics_WithWings(string size)
        {
            baseCreature.Size = size;

            var templateDemographics = new Demographics
            {
                Skin = "scaley",
                Gender = "dragon gender",
                Other = "my base creature other appearance"
            };
            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, true, false))
                .Returns(templateDemographics);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
            Assert.That(creature.Demographics.Other, Is.EqualTo("my base creature other appearance. Has dragon wings."));
        }

        [TestCase("my base creature other appearance.", "my base creature other appearance. Has dragon wings.")]
        [TestCase("my base creature other appearance", "my base creature other appearance. Has dragon wings.")]
        [TestCase("", "Has dragon wings.")]
        public void ApplyTo_ReturnsCreature_WithAdjustedDemographics_AddingWings(string other, string expected)
        {
            baseCreature.Size = SizeConstants.Large;

            var templateDemographics = new Demographics
            {
                Skin = "scaley",
                Gender = "dragon gender",
                Other = other
            };
            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, true, false))
                .Returns(templateDemographics);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
            Assert.That(creature.Demographics.Other, Is.EqualTo(expected));
        }

        [TestCase("my base creature other winged appearance")]
        [TestCase("my base creature other WINGED appearance")]
        [TestCase("my base with wings appearance")]
        [TestCase("my base has a wing appearance")]
        public void ApplyTo_ReturnsCreature_WithAdjustedDemographics_WithExistingWings(string other)
        {
            baseCreature.Size = SizeConstants.Large;

            var templateDemographics = new Demographics
            {
                Skin = "scaley",
                Gender = "dragon gender",
                Other = other
            };
            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, true, false))
                .Returns(templateDemographics);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
            Assert.That(creature.Demographics.Other, Is.EqualTo(other));
        }

        [TestCase(SizeConstants.Tiny)]
        [TestCase(SizeConstants.Small)]
        [TestCase(SizeConstants.Medium)]
        public void ApplyTo_ReturnsCreature_WithAdjustedDemographics_WithoutWings(string size)
        {
            baseCreature.Size = size;

            var templateDemographics = new Demographics
            {
                Skin = "scaley",
                Gender = "dragon gender",
                Other = "my base creature other appearance",
            };
            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, false, false))
                .Returns(templateDemographics);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
            Assert.That(creature.Demographics.Other, Is.EqualTo("my base creature other appearance"));
        }

        [TestCase(SizeConstants.Large)]
        [TestCase(SizeConstants.Huge)]
        [TestCase(SizeConstants.Gargantuan)]
        [TestCase(SizeConstants.Colossal)]
        public async Task ApplyToAsync_ReturnsCreature_WithAdjustedDemographics_WithWings(string size)
        {
            baseCreature.Size = size;

            var templateDemographics = new Demographics
            {
                Skin = "scaley",
                Gender = "dragon gender",
                Other = "my base creature other appearance",
            };
            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, true, false))
                .Returns(templateDemographics);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
            Assert.That(creature.Demographics.Other, Is.EqualTo("my base creature other appearance. Has dragon wings."));
        }

        [TestCase("my base creature other appearance.", "my base creature other appearance. Has dragon wings.")]
        [TestCase("my base creature other appearance", "my base creature other appearance. Has dragon wings.")]
        [TestCase("", "Has dragon wings.")]
        public async Task ApplyToAsync_ReturnsCreature_WithAdjustedDemographics_AddingWings(string other, string expected)
        {
            baseCreature.Size = SizeConstants.Large;

            var templateDemographics = new Demographics
            {
                Skin = "scaley",
                Gender = "dragon gender",
                Other = other
            };
            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, true, false))
                .Returns(templateDemographics);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
            Assert.That(creature.Demographics.Other, Is.EqualTo(expected));
        }

        [TestCase("my base creature other winged appearance")]
        [TestCase("my base creature other WINGED appearance")]
        [TestCase("my base with wings appearance")]
        [TestCase("my base has a wing appearance")]
        public async Task ApplyToAsync_ReturnsCreature_WithAdjustedDemographics_WithExistingWings(string other)
        {
            baseCreature.Size = SizeConstants.Large;

            var templateDemographics = new Demographics
            {
                Skin = "scaley",
                Gender = "dragon gender",
                Other = other
            };
            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, true, false))
                .Returns(templateDemographics);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
            Assert.That(creature.Demographics.Other, Is.EqualTo(other));
        }

        [TestCase(SizeConstants.Tiny)]
        [TestCase(SizeConstants.Small)]
        [TestCase(SizeConstants.Medium)]
        public async Task ApplyToAsync_ReturnsCreature_WithAdjustedDemographics_WithoutWings(string size)
        {
            baseCreature.Size = size;

            var templateDemographics = new Demographics
            {
                Skin = "scaley",
                Gender = "dragon gender",
                Other = "my base creature other appearance",
            };
            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, false, false))
                .Returns(templateDemographics);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
            Assert.That(creature.Demographics.Other, Is.EqualTo("my base creature other appearance"));
        }

        [TestCaseSource(nameof(HitDieIncreased))]
        public void ApplyTo_HitDieIncreases(int original, int increased)
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

            var creature = applicator.ApplyTo(baseCreature, false);
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

                foreach (var hitDieIncrease in hitDieIncreases)
                {
                    yield return new TestCaseData(hitDieIncrease.Item1, hitDieIncrease.Item2);
                }
            }
        }

        [TestCaseSource(nameof(HitDieIncreased))]
        public void ApplyTo_HitDieIncreases_WithBoostedConstitution(int original, int increased)
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

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(increased));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210 + 2 * 17));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(96 + baseCreature.HitPoints.RoundedHitDiceQuantity * 17));
        }

        [TestCaseSource(nameof(GainFlySpeed))]
        public void ApplyTo_GainFlySpeed(string size, int landSpeed, int flySpeed)
        {
            baseCreature.Size = size;
            baseCreature.Speeds[SpeedConstants.Land].Value = landSpeed;
            baseCreature.Demographics.Other = "I look like a potato other.";

            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, true, false))
                .Returns(baseCreature.Demographics);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Land));
            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(landSpeed));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo($"my dragon species furlongs"));
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

                foreach (var size in sizes)
                {
                    foreach (var speed in speeds)
                    {
                        yield return new TestCaseData(size, speed.Item1, speed.Item2);
                    }
                }
            }
        }

        [TestCaseSource(nameof(DoNotGainFlySpeed))]
        public void ApplyTo_DoNotGainFlySpeed_TooSmall(string size)
        {
            baseCreature.Size = size;
            baseCreature.Speeds[SpeedConstants.Land].Value = 42;

            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, false, false))
                .Returns(baseCreature.Demographics);

            var creature = applicator.ApplyTo(baseCreature, false);
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

                foreach (var size in sizes)
                {
                    yield return new TestCaseData(size);
                }
            }
        }

        [Test]
        public void ApplyTo_DoNotGainFlySpeed_HasNoLandSpeed()
        {
            baseCreature.Size = SizeConstants.Large;
            baseCreature.Speeds.Clear();
            baseCreature.Speeds[SpeedConstants.Swim] = new Measurement("feet per round") { Value = 42 };

            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, false, false))
                .Returns(baseCreature.Demographics);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(1)
                .And.Not.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Swim));
            Assert.That(creature.Speeds[SpeedConstants.Swim].Value, Is.EqualTo(42));
            Assert.That(creature.Speeds[SpeedConstants.Swim].Unit, Is.EqualTo("feet per round"));
        }

        [Test]
        public void ApplyTo_DoNotGainFlySpeed_AlreadyHasFlySpeed()
        {
            baseCreature.Size = SizeConstants.Large;
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 42, Description = "so-so maneuverability" };

            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, true, false))
                .Returns(baseCreature.Demographics);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Land));
            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(600));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(42));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("feet per round"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("so-so maneuverability"));
        }

        [Test]
        public async Task ApplyToAsync_DoNotGainFlySpeed_HasNoLandSpeed()
        {
            baseCreature.Size = SizeConstants.Large;
            baseCreature.Speeds.Clear();
            baseCreature.Speeds[SpeedConstants.Swim] = new Measurement("feet per round") { Value = 42 };

            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, false, false))
                .Returns(baseCreature.Demographics);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(1)
                .And.Not.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Swim));
            Assert.That(creature.Speeds[SpeedConstants.Swim].Value, Is.EqualTo(42));
            Assert.That(creature.Speeds[SpeedConstants.Swim].Unit, Is.EqualTo("feet per round"));
        }

        [Test]
        public async Task ApplyToAsync_DoNotGainFlySpeed_AlreadyHasFlySpeed()
        {
            baseCreature.Size = SizeConstants.Large;
            baseCreature.Speeds[SpeedConstants.Land].Value = 600;
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("feet per round") { Value = 42, Description = "so-so maneuverability" };

            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, true, false))
                .Returns(baseCreature.Demographics);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly)
                .And.ContainKey(SpeedConstants.Land));
            Assert.That(creature.Speeds[SpeedConstants.Land].Value, Is.EqualTo(600));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(42));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("feet per round"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("so-so maneuverability"));
        }

        [Test]
        public void ApplyTo_AdjustAbilities()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
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
        public void ApplyTo_AbilitiesImprove_DoNotImproveMissingAbility(string ability)
        {
            baseCreature.Abilities[ability].BaseScore = 0;

            var creature = applicator.ApplyTo(baseCreature, false);
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

                foreach (var ability in abilities)
                {
                    yield return new TestCaseData(ability);
                }
            }
        }

        [Test]
        public void ApplyTo_GainNaturalArmor()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
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

            var creature = applicator.ApplyTo(baseCreature, false);
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

            var creature = applicator.ApplyTo(baseCreature, false);
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

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9270));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Condition, Is.EqualTo("only sometimes"));
        }

        private Attack[] SetUpAttacks(string gender = null)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages =
                    [
                        new Damage { Roll = "dragon claw roll", Type = "dragon claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "dragon bite roll", Type = "dragon bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks, gender);

            return newAttacks;
        }

        private void SetUpAttacks(Attack[] attacks, string gender = null)
        {
            gender ??= baseCreature.Demographics.Gender;

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    "my dragon species",
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity,
                    gender))
                .Returns(attacks);
        }

        [Test]
        public void ApplyTo_GainAttacks()
        {
            var newAttacks = SetUpAttacks();

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
            var newAttacks = SetUpAttacks();

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
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Dragon
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
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
                    "my dragon species",
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Dragon
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    newSkills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = $"my dragon speciesy", Goodness = "scaley" }))
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
            var creature = applicator.ApplyTo(baseCreature, false);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + attacksWithBonuses.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(attacksWithBonuses));
        }

        [TestCaseSource(nameof(GainAttacks_DuplicateAttack))]
        public void ApplyTo_GainAttacks_DuplicateClawAttacks(int baseClawMax, int dragonClawMax, string expectedClawDamage)
        {
            var newAttacks = SetUpAttacks();

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
                .Setup(d => d.Roll("dragon claw roll").AsPotentialMaximum<int>(true))
                .Returns(dragonClawMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature, false);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageSummary, Is.EqualTo(expectedClawDamage.Replace("XXX", "claw")));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(newAttacks[3]));
            Assert.That(bites[0].DamageSummary, Is.EqualTo("dragon bite roll dragon bite type"));
        }

        private static IEnumerable GainAttacks_DuplicateAttack
        {
            get
            {
                var damages = new[]
                {
                    (9266, 90210, "dragon XXX roll dragon XXX type"),
                    (9266, 42, "base roll base type"),
                };

                foreach (var damage in damages)
                {
                    yield return new TestCaseData(damage.Item1, damage.Item2, damage.Item3);
                }
            }
        }

        [TestCaseSource(nameof(GainAttacks_DuplicateAttack))]
        public void ApplyTo_GainAttacks_DuplicateBiteAttack(int baseBiteMax, int dragonBiteMax, string expectedBiteDamage)
        {
            var newAttacks = SetUpAttacks();

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var bite = new Attack
            {
                Name = "Bite",
                Damages =
                    [
                        new() { Roll = "base roll", Type = "base type" }
                    ],
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union([bite]);

            mockDice
                .Setup(d => d.Roll("base roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("dragon bite roll").AsPotentialMaximum<int>(true))
                .Returns(dragonBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature, false);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(newAttacks[2]));
            Assert.That(claws[0].DamageSummary, Is.EqualTo("dragon claw roll dragon claw type"));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageSummary, Is.EqualTo(expectedBiteDamage.Replace("XXX", "bite")));
        }

        [TestCaseSource(nameof(GainAttacks_DuplicateAttacks))]
        public void ApplyTo_GainAttacks_DuplicateClawAndBiteAttack(
            int baseClawMax, int dragonClawMax, string expectedClawDescription,
            int baseBiteMax, int dragonBiteMax, string expectedBiteDescription)
        {
            var newAttacks = SetUpAttacks();

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
            var creature = applicator.ApplyTo(baseCreature, false);
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 2));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageSummary, Is.EqualTo(expectedClawDescription));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageSummary, Is.EqualTo(expectedBiteDescription));
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

                foreach (var clawDamage in clawDamages)
                {
                    foreach (var biteDamage in biteDamages)
                    {
                        yield return new TestCaseData(
                            clawDamage.Item1, clawDamage.Item2, clawDamage.Item3,
                            biteDamage.Item1, biteDamage.Item2, biteDamage.Item3);
                    }
                }
            }
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
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Dragon
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
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
                    "my dragon species",
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Dragon
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    newSkills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = $"my dragon speciesy", Goodness = "scaley" }))
                .Returns(newQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = applicator.ApplyTo(baseCreature, false);

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
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Dragon
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
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
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
            baseCreature.Skills = Enumerable.Empty<Skill>();

            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.Is<IEnumerable<Skill>>(ss =>
                        ss == baseCreature.Skills
                        && !ss.Any()),
                    baseCreature.HitPoints,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Dragon
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
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
        public void ApplyTo_ChallengeRatingAdjusted(string original, string adjusted)
        {
            baseCreature.ChallengeRating = original;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        private static IEnumerable ChallengeRatingAdjustments
        {
            get
            {
                //INFO: We will only deal with the first 12 (up to CR 5), as anything after that is basic Increase functionality
                var challengeRatings = ChallengeRatingConstants.GetOrdered().Take(12);

                foreach (var challengeRating in challengeRatings)
                {
                    var increased = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2);

                    if (ChallengeRatingConstants.IsGreaterThan(ChallengeRatingConstants.CR3, increased))
                        yield return new TestCaseData(challengeRating, ChallengeRatingConstants.CR3);
                    else
                        yield return new TestCaseData(challengeRating, increased);
                }
            }
        }

        [Test]
        public void ApplyTo_GetNewAlignment()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Alignment.Lawfulness, Is.EqualTo($"my dragon speciesy"));
            Assert.That(creature.Alignment.Goodness, Is.EqualTo($"scaley"));
        }

        [Test]
        public void ApplyTo_GetPresetAlignment()
        {
            mockAlignmentGenerator
                .Setup(g => g.Generate("my dragon species", null, "preset alignment"))
                .Returns((string c, string t, string p) => new Alignment(p));

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my dragon species"))
                .Returns((string a, string t, string c) => [$"other alignment", $"preset alignment"]);

            var filters = new Filters { Alignment = "preset alignment" };

            var creature = applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(creature.Alignment.Lawfulness, Is.EqualTo("preset"));
            Assert.That(creature.Alignment.Goodness, Is.EqualTo("alignment"));
        }

        [TestCaseSource(nameof(LevelAdjustmentIncreased))]
        public void ApplyTo_LevelAdjustmentIncreased(int? adjustment, int? adjusted)
        {
            baseCreature.LevelAdjustment = adjustment;

            var creature = applicator.ApplyTo(baseCreature, false);
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

                foreach (var level in levels)
                {
                    yield return new TestCaseData(level.Item1, level.Item2);
                }
            }
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
            message.AppendLine($"\tTemplate: my dragon species");

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCaseSource(nameof(IncompatibleFilters))]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible_WithFilters(
            bool asCharacter,
            string type,
            string challengeRating,
            string alignment,
            string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[] { "subtype 1", "subtype 2" };
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment($"original alignment");

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: my dragon species");
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
        public async Task ApplyToAsync_ReturnsCreature_WithFilters()
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

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR3;
            filters.Alignment = $"my dragon speciesy scaley";

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo("my dragon species"));
        }

        [TestCaseSource(nameof(CreatureTypeAdjusted))]
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
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(4));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(original)
                .And.Contains(CreatureConstants.Types.Subtypes.Augmented));
        }

        [TestCaseSource(nameof(HitDieIncreased))]
        public async Task ApplyToAsync_HitDieIncreases(int original, int increased)
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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(increased));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [TestCaseSource(nameof(HitDieIncreased))]
        public async Task ApplyToAsync_HitDieIncreases_WithBoostedConstitution(int original, int increased)
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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(increased));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210 + 2 * 17));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(96 + baseCreature.HitPoints.RoundedHitDiceQuantity * 17));
        }

        [TestCaseSource(nameof(GainFlySpeed))]
        public async Task ApplyToAsync_GainFlySpeed(string size, int landSpeed, int flySpeed)
        {
            baseCreature.Size = size;
            baseCreature.Speeds[SpeedConstants.Land].Value = landSpeed;

            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, true, false))
                .Returns(baseCreature.Demographics);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo($"my dragon species furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(flySpeed));
        }

        [TestCaseSource(nameof(DoNotGainFlySpeed))]
        public async Task ApplyToAsync_DoNotGainFlySpeed(string size)
        {
            baseCreature.Size = size;
            baseCreature.Speeds[SpeedConstants.Land].Value = 42;

            mockDemographicsGenerator
                .Setup(s => s.Update(baseCreature.Demographics, baseCreature.Name, applicator.DragonSpecies, false, false))
                .Returns(baseCreature.Demographics);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(1)
                .And.Not.ContainKey(SpeedConstants.Fly));
        }

        [Test]
        public async Task ApplyToAsync_AdjustAbilities()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
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
        public async Task ApplyToAsync_AbilitiesImprove_DoNotImproveMissingAbility(string ability)
        {
            baseCreature.Abilities[ability].BaseScore = 0;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Abilities[ability].HasScore, Is.False);
            Assert.That(creature.Abilities[ability].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[ability].TemplateAdjustment, Is.Zero);
        }

        [Test]
        public async Task ApplyToAsync_GainNaturalArmor()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
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
            var newAttacks = SetUpAttacks();

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

        [Test]
        public async Task ApplyToAsync_GainAttacks_WithAttackBonuses()
        {
            var newAttacks = SetUpAttacks();

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
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Dragon
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
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
                    "my dragon species",
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Dragon
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    newSkills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = $"my dragon speciesy", Goodness = "scaley" }))
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
            var creature = await applicator.ApplyToAsync(baseCreature, false);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + attacksWithBonuses.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(attacksWithBonuses));
        }

        [TestCaseSource(nameof(GainAttacks_DuplicateAttack))]
        public async Task ApplyToAsync_GainAttacks_DuplicateClawAttacks(int baseClawMax, int dragonClawMax, string expectedClawDamage)
        {
            var newAttacks = SetUpAttacks();

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var claw = new Attack
            {
                Name = "Claw",
                Damages =
                    [
                        new Damage { Roll = "base roll", Type = "base type" }
                    ],
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union([claw]);

            mockDice
                .Setup(d => d.Roll("base roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("dragon claw roll").AsPotentialMaximum<int>(true))
                .Returns(dragonClawMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageSummary, Is.EqualTo(expectedClawDamage.Replace("XXX", "claw")));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(newAttacks[3]));
            Assert.That(bites[0].DamageSummary, Is.EqualTo("dragon bite roll dragon bite type"));
        }

        [TestCaseSource(nameof(GainAttacks_DuplicateAttack))]
        public async Task ApplyToAsync_GainAttacks_DuplicateBiteAttack(int baseBiteMax, int dragonBiteMax, string expectedBiteDamage)
        {
            var newAttacks = SetUpAttacks();

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
                .Setup(d => d.Roll("dragon bite roll").AsPotentialMaximum<int>(true))
                .Returns(dragonBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 1));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(newAttacks[2]));
            Assert.That(claws[0].DamageSummary, Is.EqualTo("dragon claw roll dragon claw type"));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageSummary, Is.EqualTo(expectedBiteDamage.Replace("XXX", "bite")));
        }

        [TestCaseSource(nameof(GainAttacks_DuplicateAttacks))]
        public async Task ApplyToAsync_GainAttacks_DuplicateClawAndBiteAttack(
            int baseClawMax, int dragonClawMax, string expectedClawDescription,
            int baseBiteMax, int dragonBiteMax, string expectedBiteDescription)
        {
            var newAttacks = SetUpAttacks();

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
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            var bites = creature.Attacks.Where(a => a.Name == "Bite").ToArray();
            var claws = creature.Attacks.Where(a => a.Name == "Claw").ToArray();

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + newAttacks.Length - 2));
            Assert.That(claws, Has.Length.EqualTo(1));
            Assert.That(claws[0], Is.EqualTo(claw));
            Assert.That(claws[0].DamageSummary, Is.EqualTo(expectedClawDescription));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageSummary, Is.EqualTo(expectedBiteDescription));
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
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Dragon
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
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
                    "my dragon species",
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Dragon
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    newSkills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = $"my dragon speciesy", Goodness = "scaley" }))
                .Returns(newQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, false);

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
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Dragon
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
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
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
            baseCreature.Skills = Enumerable.Empty<Skill>();

            mockSkillsGenerator
                .Setup(g => g.ApplySkillPointsAsRanks(
                    It.Is<IEnumerable<Skill>>(ss =>
                        ss == baseCreature.Skills
                        && !ss.Any()),
                    baseCreature.HitPoints,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Dragon
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
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
        public async Task ApplyToAsync_ChallengeRatingAdjusted(string original, string adjusted)
        {
            baseCreature.ChallengeRating = original;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        [Test]
        public async Task ApplyToAsync_GetNewAlignment()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Lawfulness, Is.EqualTo($"my dragon speciesy"));
            Assert.That(creature.Alignment.Goodness, Is.EqualTo("scaley"));
        }

        [Test]
        public async Task ApplyToAsync_GetPresetAlignment()
        {
            mockAlignmentGenerator
                .Setup(g => g.Generate("my dragon species", null, "preset alignment"))
                .Returns((string c, string t, string p) => new Alignment(p));

            var filters = new Filters { Alignment = "preset alignment" };

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Lawfulness, Is.EqualTo("preset"));
            Assert.That(creature.Alignment.Goodness, Is.EqualTo("alignment"));
        }

        [TestCaseSource(nameof(LevelAdjustmentIncreased))]
        public async Task ApplyToAsync_LevelAdjustmentIncreased(int? adjustment, int? adjusted)
        {
            baseCreature.LevelAdjustment = adjustment;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [Test]
        public void ApplyTo_GainDraconicAsLanguage()
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"my dragon speciesish"));
        }

        [Test]
        public void ApplyTo_GainDraconicAsLanguage_NoLanguages()
        {
            baseCreature.Languages = Enumerable.Empty<string>();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages, Is.Empty);
        }

        [Test]
        public void ApplyTo_GainDraconicAsLanguage_AlreadyHasDraconic()
        {
            baseCreature.Languages = baseCreature.Languages.Union(new[] { $"my dragon speciesish" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"my dragon speciesish"));
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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"my dragon speciesish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"my dragon speciesish")
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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"my dragon speciesish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"my dragon speciesish")
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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"my dragon speciesish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"my dragon speciesish"));
        }

        [Test]
        public void ApplyTo_GainAllBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            baseCreature.Languages = baseCreature.Languages.Union(new[] { $"my dragon speciesish", "Lizard", "Latin" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"my dragon speciesish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"my dragon speciesish")
                .And.Contains("Drachensprach")
                .And.Contains("Latin"));
        }

        [Test]
        public async Task ApplyToAsync_GainDraconicAsLanguage()
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"my dragon speciesish"));
        }

        [Test]
        public async Task ApplyToAsync_GainDraconicAsLanguage_NoLanguages()
        {
            baseCreature.Languages = Enumerable.Empty<string>();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages, Is.Empty);
        }

        [Test]
        public async Task ApplyToAsync_GainDraconicAsLanguage_AlreadyHasDraconic()
        {
            baseCreature.Languages = baseCreature.Languages.Union(new[] { $"my dragon speciesish" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"my dragon speciesish"));
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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"my dragon speciesish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"my dragon speciesish")
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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"my dragon speciesish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 2));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"my dragon speciesish")
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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns($"my dragon speciesish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", $"my dragon speciesish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"my dragon speciesish"));
        }

        [Test]
        public async Task ApplyToAsync_GainAllBonusLanguages()
        {
            baseCreature.Abilities[AbilityConstants.Intelligence].BaseScore = 10;
            baseCreature.Abilities[AbilityConstants.Intelligence].RacialAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Intelligence].AdvancementAdjustment = 0;

            baseCreature.Languages = baseCreature.Languages.Union(new[] { $"my dragon speciesish", "Lizard", "Latin" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Automatic))
                .Returns("my dragon speciesish");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    "my dragon species" + LanguageConstants.Groups.Bonus))
                .Returns(new[] { "Drachensprach", "my dragon speciesish", "Lizard", "Latin" });

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains($"my dragon speciesish")
                .And.Contains("Drachensprach")
                .And.Contains("Latin"));
        }

        [Test]
        public void ApplyTo_RegenerateMagic()
        {
            var dragonMagic = new Magic();
            mockMagicGenerator
                .Setup(g => g.GenerateWith(
                    baseCreature.Name,
                    new Alignment { Lawfulness = $"my dragon speciesy", Goodness = "scaley" },
                    baseCreature.Abilities,
                    baseCreature.Equipment))
                .Returns(dragonMagic);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Magic, Is.EqualTo(dragonMagic));
        }

        [Test]
        public async Task ApplyToAsync_RegenerateMagic()
        {
            var dragonMagic = new Magic();
            mockMagicGenerator
                .Setup(g => g.GenerateWith(
                    baseCreature.Name,
                    new Alignment { Lawfulness = $"my dragon speciesy", Goodness = "scaley" },
                    baseCreature.Abilities,
                    baseCreature.Equipment))
                .Returns(dragonMagic);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Magic, Is.EqualTo(dragonMagic));
        }

        [Test]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures_NoFilters()
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures_NoneMatch()
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var compatibleCreatures = applicator.GetCompatibleCreatures(["undead creature", "other invalid creature"], false);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        private Dictionary<string, IEnumerable<CreatureDataSelection>> SetUpCreatureData(string cr = ChallengeRatingConstants.CR1)
        {
            var data = new Dictionary<string, IEnumerable<CreatureDataSelection>>
            {
                ["my creature"] = [new CreatureDataSelection { ChallengeRating = cr }],
                ["my other creature"] = [new CreatureDataSelection { ChallengeRating = cr }],
                ["wrong creature 1"] = [new CreatureDataSelection { ChallengeRating = cr }],
                ["wrong creature 2"] = [new CreatureDataSelection { ChallengeRating = cr }],
                ["wrong creature 3"] = [new CreatureDataSelection { ChallengeRating = cr }],
                ["wrong creature 4"] = [new CreatureDataSelection { ChallengeRating = cr }],
                ["wrong creature 5"] = [new CreatureDataSelection { ChallengeRating = cr }],
                ["wrong creature 6"] = [new CreatureDataSelection { ChallengeRating = cr }],
            };

            mockCreatureDataSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData))
                .Returns(data);

            return data;
        }

        private Dictionary<string, IEnumerable<TypeAndAmountDataSelection>> SetUpHitDice(double amount = 1)
        {
            var hitDice = new Dictionary<string, IEnumerable<TypeAndAmountDataSelection>>
            {
                ["my creature"] = [new() { AmountAsDouble = amount }],
                ["my other creature"] = [new() { AmountAsDouble = amount }],
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

            return hitDice;
        }

        [Test]
        public void GetCompatibleCreatures_ReturnEmpty_IfAlignmentFilterNotValid()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature" };

            var dragonCreatures = creatures;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            SetUpCreatureData();
            SetUpHitDice();

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my dragon species"))
                .Returns(["my alignment", "other alignment", "other wrong alignment"]);

            var filters = new Filters { Alignment = "wrong alignment" };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures_IfAlignmentFilterValid()
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            SetUpCreatureData();
            SetUpHitDice();

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my dragon species"))
                .Returns(["my alignment", "other alignment", "preset alignment", "wrong alignment"]);

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void GetCompatibleCreatures_WithChallengeRating_ReturnCompatibleCreatures(string original, string challengeRating)
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature", "wrong creature 2" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData(original);
            data["wrong creature 2"] = [new() { ChallengeRating = challengeRating }];

            SetUpHitDice();

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EquivalentTo(["my creature", "my other creature"]));
        }

        [TestCaseSource(nameof(CreatureTypeFilter_FromTemplate))]
        public void GetCompatibleCreatures_WithType_ReturnCompatibleCreatures(string type)
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            SetUpCreatureData();
            SetUpHitDice();

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EquivalentTo(["my creature", "my other creature"]));
        }

        private static IEnumerable CreatureTypeFilter_FromTemplate
        {
            get
            {
                yield return new TestCaseData(CreatureConstants.Types.Dragon);
                yield return new TestCaseData(CreatureConstants.Types.Subtypes.Augmented);
            }
        }

        [Test]
        public void GetCompatibleCreatures_WithType_ReturnCompatibleCreatures_FilterOutInvalidTypes()
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature", "wrong creature 2" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 3", "subtype 1" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            SetUpCreatureData();
            SetUpHitDice();

            var filters = new Filters { Type = "subtype 2" };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature", "wrong creature 2", "wrong creature 3" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 3", "subtype 1" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2", "subtype 1" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData(ChallengeRatingConstants.CR2);
            data["wrong creature 3"] = [new() { ChallengeRating = ChallengeRatingConstants.CR3 }];

            SetUpHitDice();

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR4 };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [TestCaseSource(nameof(CreatureTypeCompatible))]
        public void GetCompatibleCreatures_BasedOnCreatureType(string creatureType, bool compatible)
        {
            //INFO: Creature type compatibility will be handled by the creature group
            var dragonType = compatible ? creatureType : "wrong";
            var dragonCreatures = new[] { $"my {applicator.DragonSpecies} creature", "my creature", $"my {dragonType} creature", "my other creature" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var compatibleCreatures = applicator.GetCompatibleCreatures([$"my {creatureType} creature"], false);
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

                foreach (var compatibility in compatibilities)
                {
                    yield return new TestCaseData(compatibility.Item1, compatibility.Item2);
                }
            }
        }

        [Test]
        public void GetCompatibleCreatures_CannotBeIncorporeal()
        {
            var dragonCreatures = new[] { $"my {applicator.DragonSpecies} creature", "my creature", "my corporeal creature", "my other creature" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var compatibleCreatures = applicator.GetCompatibleCreatures(["my incorporeal creature"], false);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCaseSource(nameof(CreatureTypeCompatible_Filtered))]
        public void GetCompatibleCreatures_TypeMustMatch(string type, bool compatible)
        {
            var dragonCreatures = new[] { $"my {applicator.DragonSpecies} creature", "my creature", "my other creature" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            SetUpCreatureData();
            SetUpHitDice();

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable CreatureTypeCompatible_Filtered
        {
            get
            {
                yield return new TestCaseData(null, true);
                yield return new TestCaseData(CreatureConstants.Types.Humanoid, true);
                yield return new TestCaseData(CreatureConstants.Types.Dragon, true);
                yield return new TestCaseData("subtype 1", true);
                yield return new TestCaseData("subtype 2", true);
                yield return new TestCaseData(CreatureConstants.Types.Subtypes.Augmented, true);
                yield return new TestCaseData("wrong type", false);
            }
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_Filtered))]
        public void GetCompatibleCreatures_ChallengeRatingMustMatch(string original, string challengeRating, bool compatible)
        {
            var dragonCreatures = new[] { $"my {applicator.DragonSpecies} creature", "my creature", "my other creature" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            SetUpCreatureData(original);
            SetUpHitDice();

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_Filtered_HumanoidCharacter))]
        public void GetCompatibleCreatures_ChallengeRatingMustMatch_HumanoidAsCharacter(double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            var dragonCreatures = new[] { $"my {applicator.DragonSpecies} creature", "my creature", "my other creature" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            SetUpCreatureData(original);
            SetUpHitDice(hitDiceQuantity);

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, true, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable ChallengeRatingAdjustments_Filtered_HumanoidCharacter
        {
            get
            {
                //INFO: We will only deal with the first 12 (up to CR 5), as anything after that is basic Increase functionality
                var challengeRatings = ChallengeRatingConstants.GetOrdered().Take(12);
                var hitDice = new[] { 0.5, 1, 2 };

                foreach (var quantity in hitDice)
                {
                    foreach (var cr in challengeRatings)
                    {
                        var increase = ChallengeRatingConstants.IncreaseChallengeRating(cr, 2);

                        if (quantity <= 1)
                        {
                            yield return new TestCaseData(quantity, cr, ChallengeRatingConstants.CR2, false);
                            yield return new TestCaseData(quantity, cr, ChallengeRatingConstants.CR3, true);
                            yield return new TestCaseData(quantity, cr, ChallengeRatingConstants.CR4, false);
                        }
                        else if (ChallengeRatingConstants.IsGreaterThan(ChallengeRatingConstants.CR3, increase))
                        {
                            yield return new TestCaseData(quantity, cr, cr, false);
                            yield return new TestCaseData(quantity, cr, ChallengeRatingConstants.CR2, false);
                            yield return new TestCaseData(quantity, cr, ChallengeRatingConstants.CR3, true);
                            yield return new TestCaseData(quantity, cr, ChallengeRatingConstants.CR4, false);
                        }
                        else
                        {
                            var low1 = ChallengeRatingConstants.IncreaseChallengeRating(cr, 1);
                            var high1 = ChallengeRatingConstants.IncreaseChallengeRating(increase, 1);

                            yield return new TestCaseData(quantity, cr, cr, false);
                            yield return new TestCaseData(quantity, cr, low1, false);
                            yield return new TestCaseData(quantity, cr, increase, true);
                            yield return new TestCaseData(quantity, cr, high1, false);
                        }
                    }
                }
            }
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments_Filtered))]
        public void GetCompatibleCreatures_ChallengeRatingMustMatch_NonHumanoidAsCharacter(string original, string challengeRating, bool compatible)
        {
            var dragonCreatures = new[] { $"my {applicator.DragonSpecies} creature", "my creature", "my other creature" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            SetUpCreatureData(original);
            SetUpHitDice();

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, true, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable ChallengeRatingAdjustments_Filtered
        {
            get
            {
                //INFO: We will only deal with the first 12 (up to CR 5), as anything after that is basic Increase functionality
                var challengeRatings = ChallengeRatingConstants.GetOrdered().Take(12);

                foreach (var cr in challengeRatings)
                {
                    var increase = ChallengeRatingConstants.IncreaseChallengeRating(cr, 2);

                    if (ChallengeRatingConstants.IsGreaterThan(ChallengeRatingConstants.CR3, increase))
                    {
                        yield return new TestCaseData(cr, cr, false);
                        yield return new TestCaseData(cr, ChallengeRatingConstants.CR2, false);
                        yield return new TestCaseData(cr, ChallengeRatingConstants.CR3, true);
                        yield return new TestCaseData(cr, ChallengeRatingConstants.CR4, false);
                    }
                    else
                    {
                        var low1 = ChallengeRatingConstants.IncreaseChallengeRating(cr, 1);
                        var high1 = ChallengeRatingConstants.IncreaseChallengeRating(increase, 1);

                        yield return new TestCaseData(cr, cr, false);
                        yield return new TestCaseData(cr, low1, false);
                        yield return new TestCaseData(cr, increase, true);
                        yield return new TestCaseData(cr, high1, false);
                    }
                }
            }
        }

        [TestCaseSource(nameof(Alignments_Filtered))]
        public void GetCompatibleCreatures_AlignmentMustMatch(string dragonAlignment, string alignment, bool compatible)
        {
            var dragonCreatures = new[] { $"my {applicator.DragonSpecies} creature", "my creature", "my other creature" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData();
            data["my creature"] = [new() { ChallengeRating = ChallengeRatingConstants.CR1, Size = SizeConstants.Medium }];

            SetUpHitDice();

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, applicator.DragonSpecies))
                .Returns(["other alignment", dragonAlignment, "original alignment"]);

            var filters = new Filters { Alignment = alignment };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable Alignments_Filtered
        {
            get
            {
                yield return new TestCaseData("preset alignment", "preset alignment", true);
                yield return new TestCaseData("preset alignment", "wrong alignment", false);
                yield return new TestCaseData("other alignment", "preset alignment", false);
            }
        }

        [TestCaseSource(nameof(AllFilters))]
        public void GetCompatibleCreatures_AllFiltersMustMatch(string type, string challengeRating, string alignment, bool compatible)
        {
            var dragonCreatures = new[] { $"my {applicator.DragonSpecies} creature", "my creature", "my other creature" };
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            SetUpCreatureData(ChallengeRatingConstants.CR2);
            SetUpHitDice();

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, applicator.DragonSpecies))
                .Returns(["other alignment", "preset alignment", "original alignment"]);

            var filters = new Filters { Alignment = alignment, Type = type, ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        private static IEnumerable AllFilters
        {
            get
            {
                yield return new TestCaseData(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR2, "preset alignment", false);
                yield return new TestCaseData(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR2, "wrong alignment", false);
                yield return new TestCaseData(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR4, "preset alignment", true);
                yield return new TestCaseData(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR4, "wrong alignment", false);
                yield return new TestCaseData("wrong subtype", ChallengeRatingConstants.CR2, "preset alignment", false);
                yield return new TestCaseData("wrong subtype", ChallengeRatingConstants.CR2, "wrong alignment", false);
                yield return new TestCaseData("wrong subtype", ChallengeRatingConstants.CR4, "preset alignment", false);
                yield return new TestCaseData("wrong subtype", ChallengeRatingConstants.CR4, "wrong alignment", false);
            }
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData();
            var hitDice = SetUpHitDice();

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithChallengeRating(data["my creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"].Single().AmountAsDouble)
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
                    .WithChallengeRating(data["my other creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my other creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"].Single().AmountAsDouble)
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
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures_AsCharacter()
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData();
            var hitDice = SetUpHitDice();

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithChallengeRating(data["my creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"].Single().AmountAsDouble)
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
                    .WithChallengeRating(data["my other creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my other creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"].Single().AmountAsDouble)
                    .WithAbility(AbilityConstants.Strength, 96)
                    .WithAbility(AbilityConstants.Constitution, 783)
                    .WithAbility(AbilityConstants.Dexterity, 8245)
                    .WithAbility(AbilityConstants.Intelligence, -8)
                    .WithAbility(AbilityConstants.Wisdom, 0)
                    .WithAbility(AbilityConstants.Charisma, 1)
                    .Build(),
            };
            mockPrototypeFactory
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), true))
                .Returns(prototypes);

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, true).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnEmpty_IfAlignmentFilterNotValid()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature" };

            var dragonCreatures = creatures;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData();
            var hitDice = SetUpHitDice();

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my dragon species"))
                .Returns(["my alignment", "other alignment", "other wrong alignment"]);

            var filters = new Filters { Alignment = "wrong alignment" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);

            mockPrototypeFactory.Verify(f => f.Build(It.IsAny<IEnumerable<string>>(), It.IsAny<bool>()), Times.Never);
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures_IfAlignmentFilterValid()
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData();
            var hitDice = SetUpHitDice();

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my dragon species"))
                .Returns(["my alignment", "other alignment", "preset alignment", "wrong alignment"]);

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithChallengeRating(data["my creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"].Single().AmountAsDouble)
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
                    .WithChallengeRating(data["my other creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my other creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"].Single().AmountAsDouble)
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

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void GetCompatiblePrototypes_FromNames_WithChallengeRating_ReturnCompatibleCreatures(string original, string challengeRating)
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature", "wrong creature 2" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>
            {
                ["my creature"] = [CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2"],
                ["my other creature"] = [CreatureConstants.Types.Giant, "subtype 3"],
                ["wrong creature 2"] = [CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2"]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData(original);
            data["wrong creature 2"] = [new() { ChallengeRating = challengeRating }];

            var hitDice = SetUpHitDice();

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithChallengeRating(data["my creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"].Single().AmountAsDouble)
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
                    .WithChallengeRating(data["my other creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my other creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"].Single().AmountAsDouble)
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
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [TestCaseSource(nameof(CreatureTypeFilter_FromTemplate))]
        public void GetCompatiblePrototypes_FromNames_WithType_ReturnCompatibleCreatures(string type)
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>
            {
                ["my creature"] = [CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2"],
                ["my other creature"] = [CreatureConstants.Types.Giant, "subtype 3"]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData();
            var hitDice = SetUpHitDice();

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithChallengeRating(data["my creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"].Single().AmountAsDouble)
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
                    .WithChallengeRating(data["my other creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my other creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"].Single().AmountAsDouble)
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
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_WithType_ReturnCompatibleCreatures_FilterOutInvalidTypes()
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature", "wrong creature 2" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 3", "subtype 1" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData();
            var hitDice = SetUpHitDice();

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithChallengeRating(data["my creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"].Single().AmountAsDouble)
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
                    .WithChallengeRating(data["my other creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my other creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"].Single().AmountAsDouble)
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

            var filters = new Filters { Type = "subtype 2" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "undead creature", "my other creature", "wrong creature 2", "wrong creature 3" };

            var dragonCreatures = creatures.Except(["undead creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>
            {
                ["my creature"] = [CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2"],
                ["my other creature"] = [CreatureConstants.Types.Giant, "subtype 2"],
                ["wrong creature 2"] = [CreatureConstants.Types.Humanoid, "subtype 3", "subtype 1"],
                ["wrong creature 3"] = [CreatureConstants.Types.Humanoid, "subtype 2", "subtype 1"]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData(ChallengeRatingConstants.CR2);
            data["wrong creature 3"] = [new() { ChallengeRating = ChallengeRatingConstants.CR3 }];

            var hitDice = SetUpHitDice();

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithChallengeRating(data["my creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"].Single().AmountAsDouble)
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
                    .WithChallengeRating(data["my other creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my other creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"].Single().AmountAsDouble)
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

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR4 };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures_WithLevelAdjustments()
        {
            var creatures = new[] { "my creature", "my other creature", };

            var dragonCreatures = creatures;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 3", "subtype 1" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2", "subtype 1" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData(ChallengeRatingConstants.CR2);
            data["my creature"] = [new() { ChallengeRating = ChallengeRatingConstants.CR2, LevelAdjustment = 0, CasterLevel = 3 }];
            data["my other creature"] = [new() { ChallengeRating = ChallengeRatingConstants.CR2, LevelAdjustment = 2, CasterLevel = 4 }];
            data["wrong creature 3"] = [new() { ChallengeRating = ChallengeRatingConstants.CR3 }];

            var hitDice = SetUpHitDice();

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(types["my creature"].ToArray())
                    .WithChallengeRating(data["my creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"].Single().AmountAsDouble)
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
                    .WithChallengeRating(data["my other creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my other creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"].Single().AmountAsDouble)
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
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.EqualTo(3));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(3));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.EqualTo(4));
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.EqualTo(5));
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures_WithValidAlignments()
        {
            var creatures = new[] { "my creature", "my other creature" };

            var dragonCreatures = creatures;
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, applicator.DragonSpecies))
                .Returns(dragonCreatures);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 3", "subtype 1" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 2", "subtype 1" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);

            var data = SetUpCreatureData(ChallengeRatingConstants.CR2);
            data["wrong creature 3"] = [new() { ChallengeRating = ChallengeRatingConstants.CR3 }];

            var hitDice = SetUpHitDice();

            var prototypes = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType([.. types["my creature"]])
                    .WithChallengeRating(data["my creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my creature"].Single().AmountAsDouble)
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
                    .WithCreatureType([.. types["my other creature"]])
                    .WithChallengeRating(data["my other creature"].Single().ChallengeRating)
                    .WithCasterLevel(data["my other creature"].Single().CasterLevel)
                    .WithLevelAdjustment(data["my other creature"].Single().LevelAdjustment)
                    .WithHitDiceQuantity(hitDice["my other creature"].Single().AmountAsDouble)
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

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR4 };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 8245));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 783 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 96 + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
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
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 8));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [TestCaseSource(nameof(CreatureTypeCompatible))]
        public void GetCompatiblePrototypes_FromPrototypes_BasedOnCreatureType(string creatureType, bool compatible)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(creatureType, "subtype 1", "subtype 2")
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_CannotBeIncorporeal()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", CreatureConstants.Types.Subtypes.Incorporeal, "subtype 2")
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_AsCharacter()
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
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, true).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 8));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnEmpty_IfAlignmentFilterNotValid()
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
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my dragon species"))
                .Returns(["my alignment", "other alignment", "other wrong alignment"]);

            var filters = new Filters { Alignment = "wrong alignment" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_IfAlignmentFilterValid()
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
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .Build(),
            };

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "my dragon species"))
                .Returns(new[] { "my alignment", "other alignment", "preset alignment", "wrong alignment" });

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 8));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void GetCompatiblePrototypes_FromPrototypes_WithChallengeRating_ReturnCompatibleCreatures(string original, string challengeRating)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithChallengeRating(original)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .WithChallengeRating(original)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .WithChallengeRating(original)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithChallengeRating(challengeRating)
                    .Build(),
            };

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 8));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(challengeRating));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }

        [TestCaseSource(nameof(CreatureTypeFilter_FromTemplate))]
        public void GetCompatiblePrototypes_FromPrototypes_WithType_ReturnCompatibleCreatures(string type)
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
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .Build(),
            };

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 8));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
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
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 2")
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 3", "subtype 1")
                    .Build(),
            };

            var filters = new Filters { Type = "subtype 2" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 8));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
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
                    .WithChallengeRating(ChallengeRatingConstants.CR2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .WithChallengeRating(ChallengeRatingConstants.CR2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 2")
                    .WithChallengeRating(ChallengeRatingConstants.CR2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 3", "subtype 1")
                    .WithChallengeRating(ChallengeRatingConstants.CR2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 3")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithChallengeRating(ChallengeRatingConstants.CR3)
                    .Build(),
            };

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR4 };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 8));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
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
                    .WithChallengeRating(ChallengeRatingConstants.CR2)
                    .WithLevelAdjustment(0)
                    .WithCasterLevel(3)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 2")
                    .WithChallengeRating(ChallengeRatingConstants.CR2)
                    .WithLevelAdjustment(2)
                    .WithCasterLevel(4)
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 8));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.EqualTo(3));
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(3));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.EqualTo(4));
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.EqualTo(5));
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
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
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 2")
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 8));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
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
                    .WithAbility(AbilityConstants.Strength, 9266, 96)
                    .WithAbility(AbilityConstants.Constitution, 90210, 783)
                    .WithAbility(AbilityConstants.Dexterity, 42, 8245)
                    .WithAbility(AbilityConstants.Intelligence, 600, 69)
                    .WithAbility(AbilityConstants.Wisdom, 1337, 420)
                    .WithAbility(AbilityConstants.Charisma, 1336, 1)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .WithAbility(AbilityConstants.Strength, 2, -3)
                    .WithAbility(AbilityConstants.Constitution, -4, 5)
                    .WithAbility(AbilityConstants.Dexterity, 6, 7)
                    .WithAbility(AbilityConstants.Intelligence, -8, 9)
                    .WithAbility(AbilityConstants.Wisdom, 10, 11)
                    .WithAbility(AbilityConstants.Charisma, 12, 13)
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(10 + 9266 + 96 + 8));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(10 + 1336 + 1 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(10 + 600 + 69 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(10 + 1337 + 420));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(10 + 42 + 8245));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(10 + 90210 + 783 + 2));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(1));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Dragon));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 12 + 13 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore - 8 + 9 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 10 + 11));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 6 + 7));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(Ability.DefaultScore - 4 + 5 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 2 - 3 + 8));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment($"my dragon speciesy scaley"),
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(1));
        }
    }
}
