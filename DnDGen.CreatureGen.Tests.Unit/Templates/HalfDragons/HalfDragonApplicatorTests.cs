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

            applicators = new Dictionary<string, TemplateApplicator>();
            applicators[CreatureConstants.Templates.HalfDragon_Black] = new HalfDragonBlackApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Blue] = new HalfDragonBlueApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Brass] = new HalfDragonBrassApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Bronze] = new HalfDragonBronzeApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Copper] = new HalfDragonCopperApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Gold] = new HalfDragonGoldApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Green] = new HalfDragonGreenApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Red] = new HalfDragonRedApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object);
            applicators[CreatureConstants.Templates.HalfDragon_Silver] = new HalfDragonSilverApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object);
            applicators[CreatureConstants.Templates.HalfDragon_White] = new HalfDragonWhiteApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockAlignmentGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object);

            baseCreature = new CreatureBuilder().WithTestValues().Build();
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
                .Setup(g => g.Generate(It.IsAny<string>()))
                .Returns((string t) => new Alignment { Lawfulness = $"{t}y", Goodness = "scaley" });
        }

        [TestCaseSource(nameof(CreatureTypeCompatible))]
        public void IsCompatible_BasedOnCreatureType(string template, string creatureType, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { creatureType, "subtype 1", "subtype 2" });

            var isCompatible = applicators[template].IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(compatible));
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
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", CreatureConstants.Types.Subtypes.Incorporeal, "subtype 2" });

            var isCompatible = applicators[template].IsCompatible("my creature");
            Assert.That(isCompatible, Is.False);
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_SetsTemplate(string template)
        {
            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Template, Is.EqualTo(template));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_SetsTemplate(string template)
        {
            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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
            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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
            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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
            var creature = applicators[template].ApplyTo(baseCreature);

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
            var creature = applicators[template].ApplyTo(baseCreature);

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
                new Attack { Name = "Claw", DamageRoll = "dragon roll", IsSpecial = false, IsMelee = true },
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

            var claw = new Attack { Name = "Claw", DamageRoll = "base roll", IsSpecial = false, IsMelee = true };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { claw });

            mockDice
                .Setup(d => d.Roll("base roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("dragon roll").AsPotentialMaximum<int>(true))
                .Returns(dragonClawMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicators[template].ApplyTo(baseCreature);
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

        private static IEnumerable GainAttacks_DuplicateAttack
        {
            get
            {
                var damages = new[]
                {
                    (9266, 90210, "dragon roll"),
                    (9266, 42, "base roll"),
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
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true },
                new Attack { Name = "Bite", DamageRoll = "dragon roll", IsSpecial = false, IsMelee = true },
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

            var bite = new Attack { Name = "Bite", DamageRoll = "base roll", IsSpecial = false, IsMelee = true };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { bite });

            mockDice
                .Setup(d => d.Roll("base roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("dragon roll").AsPotentialMaximum<int>(true))
                .Returns(dragonBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicators[template].ApplyTo(baseCreature);
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

        [TestCaseSource(nameof(GainAttacks_DuplicateAttacks))]
        public void ApplyTo_GainAttacks_DuplicateClawAndBiteAttack(string template,
            int baseClawMax, int dragonClawMax, string expectedClawDamage,
            int baseBiteMax, int dragonBiteMax, string expectedBiteDamage)
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
            var creature = applicators[template].ApplyTo(baseCreature);
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

        private static IEnumerable GainAttacks_DuplicateAttacks
        {
            get
            {
                var clawDamages = new[]
                {
                    (9266, 90210, "dragon claw roll"),
                    (9266, 42, "base claw roll"),
                };
                var biteDamages = new[]
                {
                    (1336, 1337, "dragon bite roll"),
                    (1336, 600, "base bite roll"),
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
            var creature = applicators[template].ApplyTo(baseCreature);

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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature.Skills, Is.Empty);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void ApplyTo_ChallengeRatingAdjusted(string template, string original, string adjusted)
        {
            baseCreature.ChallengeRating = original;

            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        private static IEnumerable ChallengeRatingAdjustments
        {
            get
            {
                var challengeRatings = ChallengeRatingConstants.GetOrdered();
                var minIndex = Array.IndexOf(challengeRatings, ChallengeRatingConstants.Three);

                foreach (var template in templates)
                {
                    for (var i = 0; i < challengeRatings.Length - 2; i++)
                    {
                        if (i + 2 < minIndex)
                            yield return new TestCaseData(template, challengeRatings[i], ChallengeRatingConstants.Three);
                        else
                            yield return new TestCaseData(template, challengeRatings[i], challengeRatings[i + 2]);
                    }
                }
            }
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public void ApplyTo_GetNewAlignment(string template)
        {
            var creature = applicators[template].ApplyTo(baseCreature);
            Assert.That(creature.Alignment.Lawfulness, Is.EqualTo($"{template}y"));
            Assert.That(creature.Alignment.Goodness, Is.EqualTo($"scaley"));
        }

        [TestCaseSource(nameof(LevelAdjustmentIncreased))]
        public void ApplyTo_LevelAdjustmentIncreased(string template, int? adjustment, int? adjusted)
        {
            baseCreature.LevelAdjustment = adjustment;

            var creature = applicators[template].ApplyTo(baseCreature);
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

        [TestCaseSource(nameof(CreatureTypeAdjusted))]
        public async Task ApplyToAsync_CreatureTypeIsAdjusted(string template, string original)
        {
            baseCreature.Type.Name = original;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature.Speeds, Has.Count.EqualTo(1)
                .And.Not.ContainKey(SpeedConstants.Fly));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_AdjustAbilities(string template)
        {
            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature.Abilities[ability].HasScore, Is.False);
            Assert.That(creature.Abilities[ability].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[ability].TemplateAdjustment, Is.Zero);
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GainNaturalArmor(string template)
        {
            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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
            var creature = await applicators[template].ApplyToAsync(baseCreature);

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
            var creature = await applicators[template].ApplyToAsync(baseCreature);

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
                new Attack { Name = "Claw", DamageRoll = "dragon roll", IsSpecial = false, IsMelee = true },
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

            var claw = new Attack { Name = "Claw", DamageRoll = "base roll", IsSpecial = false, IsMelee = true };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { claw });

            mockDice
                .Setup(d => d.Roll("base roll").AsPotentialMaximum<int>(true))
                .Returns(baseClawMax);
            mockDice
                .Setup(d => d.Roll("dragon roll").AsPotentialMaximum<int>(true))
                .Returns(dragonClawMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

        [TestCaseSource(nameof(GainAttacks_DuplicateAttack))]
        public async Task ApplyToAsync_GainAttacks_DuplicateBiteAttack(string template, int baseBiteMax, int dragonBiteMax, string expectedBiteDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Claw", DamageRoll = "dragon claw roll", IsSpecial = false, IsMelee = true },
                new Attack { Name = "Bite", DamageRoll = "dragon roll", IsSpecial = false, IsMelee = true },
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

            var bite = new Attack { Name = "Bite", DamageRoll = "base roll", IsSpecial = false, IsMelee = true };
            baseCreature.Attacks = baseCreature.Attacks.Union(new[] { bite });

            mockDice
                .Setup(d => d.Roll("base roll").AsPotentialMaximum<int>(true))
                .Returns(baseBiteMax);
            mockDice
                .Setup(d => d.Roll("dragon roll").AsPotentialMaximum<int>(true))
                .Returns(dragonBiteMax);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

        [TestCaseSource(nameof(GainAttacks_DuplicateAttacks))]
        public async Task ApplyToAsync_GainAttacks_DuplicateClawAndBiteAttack(string template,
            int baseClawMax, int dragonClawMax, string expectedClawDamage,
            int baseBiteMax, int dragonBiteMax, string expectedBiteDamage)
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
            var creature = await applicators[template].ApplyToAsync(baseCreature);
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
            var creature = await applicators[template].ApplyToAsync(baseCreature);

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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature.Skills, Is.Empty);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public async Task ApplyToAsync_ChallengeRatingAdjusted(string template, string original, string adjusted)
        {
            baseCreature.ChallengeRating = original;

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        [TestCaseSource(nameof(AllHalfDragonTemplates))]
        public async Task ApplyToAsync_GetNewAlignment(string template)
        {
            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Lawfulness, Is.EqualTo($"{template}y"));
            Assert.That(creature.Alignment.Goodness, Is.EqualTo($"scaley"));
        }

        [TestCaseSource(nameof(LevelAdjustmentIncreased))]
        public async Task ApplyToAsync_LevelAdjustmentIncreased(string template, int? adjustment, int? adjusted)
        {
            baseCreature.LevelAdjustment = adjustment;

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
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

            var creature = applicators[template].ApplyTo(baseCreature);
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

            var creature = await applicators[template].ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Magic, Is.EqualTo(dragonMagic));
        }
    }
}
