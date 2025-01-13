using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
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
    public class GhostApplicatorTests
    {
        private TemplateApplicator applicator;
        private Creature baseCreature;
        private Mock<Dice> mockDice;
        private Mock<ISpeedsGenerator> mockSpeedsGenerator;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<IItemsGenerator> mockItemsGenerator;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentSelector;
        private Mock<ICreaturePrototypeFactory> mockPrototypeFactory;

        [SetUp]
        public void SetUp()
        {
            mockDice = new Mock<Dice>();
            mockSpeedsGenerator = new Mock<ISpeedsGenerator>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockItemsGenerator = new Mock<IItemsGenerator>();
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockAdjustmentSelector = new Mock<IAdjustmentsSelector>();
            mockPrototypeFactory = new Mock<ICreaturePrototypeFactory>();

            applicator = new GhostApplicator(
                mockDice.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockCollectionSelector.Object,
                mockFeatsGenerator.Object,
                mockItemsGenerator.Object,
                mockTypeAndAmountSelector.Object,
                mockCreatureDataSelector.Object,
                mockAdjustmentSelector.Object,
                mockPrototypeFactory.Object);

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .CanUseEquipment(false)
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .WithMinimumAbility(AbilityConstants.Charisma, 6)
                .Build();

            mockDice
                .Setup(d => d.Roll(1).d(3).AsSum<int>())
                .Returns(1);

            mockDice
                .Setup(d => d.Roll(2).d(4).AsSum<int>())
                .Returns(2);

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

            var ghostSpeeds = new Dictionary<string, Measurement>();
            mockSpeedsGenerator
                .Setup(g => g.Generate(CreatureConstants.Templates.Ghost))
                .Returns(ghostSpeeds);

            var ghostAttacks = new[]
            {
                new Attack { Name = "spoopy", IsSpecial = true },
                new Attack { Name = "Manifestation", IsSpecial = true },
                new Attack { Name = "spooky", IsSpecial = true },
                new Attack { Name = "scary", IsSpecial = true },
                new Attack { Name = "skeletons", IsSpecial = true },
                new Attack { Name = "shout", IsSpecial = true },
                new Attack { Name = "starkly", IsSpecial = true },
                new Attack { Name = "thrilling", IsSpecial = true },
                new Attack { Name = "screams", IsSpecial = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Ghost,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(ghostAttacks);

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<Attack>>()))
                .Returns((IEnumerable<Attack> aa) => aa.ElementAt(count++ % aa.Count()));

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Undead, Amount = 1000, RawAmount = "raw 1000" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, CreatureConstants.Templates.Ghost))
                .Returns(ageRolls);
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
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Ghost}");

            Assert.That(() => applicator.ApplyTo(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR3, "wrong alignment", "Alignment filter 'wrong alignment' is not valid")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR2, "original alignment", "CR filter 2 does not match updated creature CR 3 (from CR 1)")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR3, "original alignment", "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR3, "original alignment", "",
            Ignore = "As Character doesn't affect already-generated creature compatiblity")]
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
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Ghost}");
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
            baseCreature.Alignment = new Alignment("original alignment");
            baseCreature.Templates.Add("my other template");

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

            var ghostAttacks = new[]
            {
                new Attack { Name = "spoopy", IsSpecial = true },
                new Attack { Name = "Manifestation", IsSpecial = true },
                new Attack { Name = "spooky", IsSpecial = true },
                new Attack { Name = "scary", IsSpecial = true },
                new Attack { Name = "skeletons", IsSpecial = true },
                new Attack { Name = "shout", IsSpecial = true },
                new Attack { Name = "starkly", IsSpecial = true },
                new Attack { Name = "thrilling", IsSpecial = true },
                new Attack { Name = "screams", IsSpecial = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Ghost,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(ghostAttacks);

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR3;
            filters.Alignment = "original alignment";

            var creature = applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.Ghost));
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

            var ghostAttacks = new[]
            {
                new Attack { Name = "spoopy", IsSpecial = true },
                new Attack { Name = "Manifestation", IsSpecial = true },
                new Attack { Name = "spooky", IsSpecial = true },
                new Attack { Name = "scary", IsSpecial = true },
                new Attack { Name = "skeletons", IsSpecial = true },
                new Attack { Name = "shout", IsSpecial = true },
                new Attack { Name = "starkly", IsSpecial = true },
                new Attack { Name = "thrilling", IsSpecial = true },
                new Attack { Name = "screams", IsSpecial = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Ghost,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(ghostAttacks);

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR3;
            filters.Alignment = "original alignment";

            var creature = applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Ghost));
        }

        [TestCase(CreatureConstants.Types.Aberration, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Dragon, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Giant, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Plant, CreatureConstants.Types.Undead)]
        public void ApplyTo_CreatureTypeIsAdjusted(string original, string adjusted)
        {
            baseCreature.Type.Name = original;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(adjusted));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(5));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(original)
                .And.Contains(CreatureConstants.Types.Subtypes.Augmented)
                .And.Contains(CreatureConstants.Types.Subtypes.Incorporeal));
        }

        [Test]
        public void ApplyTo_DemographicsAdjusted()
        {
            baseCreature.Demographics.Skin = "I look like a potato skin.";
            baseCreature.Demographics.Hair = "I look like a potato hair.";
            baseCreature.Demographics.Eyes = "I look like a potato eyes.";
            baseCreature.Demographics.Other = "I look like a potato other.";
            baseCreature.Demographics.Age.Value = 42;
            baseCreature.Demographics.MaximumAge.Value = 600;

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Appearances("Skin"), CreatureConstants.Templates.Ghost))
                .Returns("I am the meanest boi skin.");
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Appearances("Hair"), CreatureConstants.Templates.Ghost))
                .Returns("I am the meanest boi hair.");
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Appearances("Eyes"), CreatureConstants.Templates.Ghost))
                .Returns("I am the meanest boi eyes.");
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Appearances("Other"), CreatureConstants.Templates.Ghost))
                .Returns("I am the meanest boi other.");

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = AgeConstants.Categories.Undead, Amount = 9266, RawAmount = "raw 9266" },
                new TypeAndAmountSelection { Type = AgeConstants.Categories.Maximum, Amount = AgeConstants.Ageless, RawAmount = AgeConstants.Ageless.ToString() }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, CreatureConstants.Templates.Ghost))
                .Returns(ageRolls);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics.Age.Value, Is.EqualTo(42 + 9266));
            Assert.That(creature.Demographics.MaximumAge.Value, Is.EqualTo(AgeConstants.Ageless));
            Assert.That(creature.Demographics.Skin, Is.EqualTo("I look like a potato skin. I am the meanest boi skin."));
            Assert.That(creature.Demographics.Hair, Is.EqualTo("I look like a potato hair. I am the meanest boi hair."));
            Assert.That(creature.Demographics.Eyes, Is.EqualTo("I look like a potato eyes. I am the meanest boi eyes."));
            Assert.That(creature.Demographics.Other, Is.EqualTo("I look like a potato other. I am the meanest boi other."));
        }

        [Test]
        public void ApplyTo_CharismaIncreasesBy4()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(4));
        }

        [Test]
        public void ApplyTo_ConstitutionGoesAway()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].HasScore, Is.False);
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        public void ApplyTo_HitDiceChangeToD12_AndRerolled(int die)
        {
            baseCreature.HitPoints.HitDice[0].HitDie = die;

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

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [Test]
        public void ApplyTo_HitDiceChangeToD12_AndRerolled_WithoutConstitution()
        {
            baseCreature.HitPoints.HitDice[0].HitDie = 4;

            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 600;

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

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [Test]
        public void ApplyTo_CreatureGainsFlySpeed()
        {
            var speeds = new Dictionary<string, Measurement>();
            speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            speeds[SpeedConstants.Fly].Description = "the goodest";
            speeds[SpeedConstants.Fly].Value = 96;

            mockSpeedsGenerator
                .Setup(g => g.Generate(CreatureConstants.Templates.Ghost))
                .Returns(speeds);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(96));
        }

        [Test]
        public void ApplyTo_CreatureGainsFlySpeed_BetterThanOriginalFlySpeed()
        {
            var speeds = new Dictionary<string, Measurement>();
            speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            speeds[SpeedConstants.Fly].Description = "the goodest";
            speeds[SpeedConstants.Fly].Value = 96;

            mockSpeedsGenerator
                .Setup(g => g.Generate(CreatureConstants.Templates.Ghost))
                .Returns(speeds);

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
        public void ApplyTo_CreatureGainsFlySpeed_SlowerThanOriginalFlySpeed()
        {
            var speeds = new Dictionary<string, Measurement>();
            speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            speeds[SpeedConstants.Fly].Description = "the goodest";
            speeds[SpeedConstants.Fly].Value = 96;

            mockSpeedsGenerator
                .Setup(g => g.Generate(CreatureConstants.Templates.Ghost))
                .Returns(speeds);

            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Description = "so-so";
            baseCreature.Speeds[SpeedConstants.Fly].Value = 600;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(600));
        }

        [Test]
        public void ApplyTo_CreatureGainsFlySpeed_OriginalFlySpeedLessManeuverable()
        {
            var speeds = new Dictionary<string, Measurement>();
            speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            speeds[SpeedConstants.Fly].Description = "the goodest";
            speeds[SpeedConstants.Fly].Value = 96;

            mockSpeedsGenerator
                .Setup(g => g.Generate(CreatureConstants.Templates.Ghost))
                .Returns(speeds);

            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Description = "so-so";
            baseCreature.Speeds[SpeedConstants.Fly].Value = 96;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(96));
        }

        [Test]
        public void ApplyTo_CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 42);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);

            var naturalArmorBonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(naturalArmorBonuses, Has.Length.EqualTo(1));
            Assert.That(naturalArmorBonuses[0].Value, Is.EqualTo(42));
            Assert.That(naturalArmorBonuses[0].IsConditional, Is.True);
            Assert.That(naturalArmorBonuses[0].Condition, Is.EqualTo("Only applies for ethereal creatures"));
        }

        [Test]
        public void ApplyTo_CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal_MultipleBonuses()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 42);
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 600);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);

            var naturalArmorBonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(naturalArmorBonuses, Has.Length.EqualTo(2));
            Assert.That(naturalArmorBonuses[0].Value, Is.EqualTo(42));
            Assert.That(naturalArmorBonuses[0].IsConditional, Is.True);
            Assert.That(naturalArmorBonuses[0].Condition, Is.EqualTo("Only applies for ethereal creatures"));
            Assert.That(naturalArmorBonuses[1].Value, Is.EqualTo(600));
            Assert.That(naturalArmorBonuses[1].IsConditional, Is.True);
            Assert.That(naturalArmorBonuses[1].Condition, Is.EqualTo("Only applies for ethereal creatures"));
        }

        [Test]
        public void ApplyTo_CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal_NoNaturalArmor()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(creature.ArmorClass.NaturalArmorBonuses, Is.Empty);
        }

        [Test]
        public void ApplyTo_CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal_AlreadyConditional()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 42, "only sometimes");

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);

            var naturalArmorBonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(naturalArmorBonuses, Has.Length.EqualTo(1));
            Assert.That(naturalArmorBonuses[0].Value, Is.EqualTo(42));
            Assert.That(naturalArmorBonuses[0].IsConditional, Is.True);
            Assert.That(naturalArmorBonuses[0].Condition, Is.EqualTo("only sometimes AND Only applies for ethereal creatures"));
        }

        [Test]
        public void ApplyTo_CreatureArmorClass_DeflectionBonusOfCharisma()
        {
            baseCreature.Abilities[AbilityConstants.Charisma].BaseScore = 42;
            baseCreature.Abilities[AbilityConstants.Charisma].AdvancementAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Charisma].RacialAdjustment = 0;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(46));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(42));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(creature.ArmorClass.DeflectionBonus, Is.EqualTo(18).And.EqualTo(baseCreature.Abilities[AbilityConstants.Charisma].Modifier));

            var deflectionBonuses = creature.ArmorClass.DeflectionBonuses.ToArray();
            Assert.That(deflectionBonuses, Has.Length.EqualTo(1));
            Assert.That(deflectionBonuses[0].Value, Is.EqualTo(18).And.EqualTo(baseCreature.Abilities[AbilityConstants.Charisma].Modifier));
            Assert.That(deflectionBonuses[0].IsConditional, Is.False);
            Assert.That(deflectionBonuses[0].Condition, Is.Empty);
        }

        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        public void ApplyTo_CreatureArmorClass_DeflectionBonusOfCharisma_AtLeast1(int charisma)
        {
            baseCreature.Abilities[AbilityConstants.Charisma].BaseScore = charisma;
            baseCreature.Abilities[AbilityConstants.Charisma].AdvancementAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Charisma].RacialAdjustment = 0;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(charisma + 4));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(charisma));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.AtLeast(-1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.AtLeast(4));
            Assert.That(creature.ArmorClass.DeflectionBonus, Is.Positive.And.EqualTo(Math.Max(1, creature.Abilities[AbilityConstants.Charisma].Modifier)));

            var deflectionBonuses = creature.ArmorClass.DeflectionBonuses.ToArray();
            Assert.That(deflectionBonuses, Has.Length.EqualTo(1));
            Assert.That(deflectionBonuses[0].Value, Is.EqualTo(Math.Max(1, creature.Abilities[AbilityConstants.Charisma].Modifier)));
            Assert.That(deflectionBonuses[0].IsConditional, Is.False);
            Assert.That(deflectionBonuses[0].Condition, Is.Empty);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ApplyTo_CreatureGainsSpecialAttacks_RandomAmount(int amount)
        {
            //one to three
            //use attack generator to get all
            var manifestation = new Attack { Name = "Manifestation", IsSpecial = true };
            var ghostAttacks = new[]
            {
                new Attack { Name = "spoopy", IsSpecial = true },
                manifestation,
                new Attack { Name = "spooky", IsSpecial = true },
                new Attack { Name = "scary", IsSpecial = true },
                new Attack { Name = "skeletons", IsSpecial = true },
                new Attack { Name = "shout", IsSpecial = true },
                new Attack { Name = "starkly", IsSpecial = true },
                new Attack { Name = "thrilling", IsSpecial = true },
                new Attack { Name = "screams", IsSpecial = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Ghost,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(ghostAttacks);

            mockDice
                .Setup(d => d.Roll(1).d(3).AsSum<int>())
                .Returns(amount);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature, false);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + 1 + amount));
            Assert.That(creature.Attacks, Contains.Item(manifestation).And.Contains(ghostAttacks[0]));

            if (amount > 1)
            {
                Assert.That(creature.Attacks, Contains.Item(ghostAttacks[3]));
            }

            if (amount > 2)
            {
                Assert.That(creature.Attacks, Contains.Item(ghostAttacks[5]));
            }
        }

        [Test]
        public void ApplyTo_CreatureGainsSpecialQualities()
        {
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
            var ghostQualities = new[]
            {
                new Feat { Name = "ghost quality 1" },
                new Feat { Name = "ghost quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Ghost,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Undead
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Incorporeal,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(ghostQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = applicator.ApplyTo(baseCreature, false);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + ghostQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(ghostQualities));
        }

        [Test]
        public void ApplyTo_CreatureGainsSpecialQualities_Undead()
        {
            var ghostQualities = new[]
            {
                new Feat { Name = "ghost quality 1" },
                new Feat { Name = "ghost quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Ghost,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(ghostQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = applicator.ApplyTo(baseCreature, false);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + ghostQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(ghostQualities));
        }

        [Test]
        public async Task ApplyToAsync_CreatureGainsSpecialQualities_Undead()
        {
            var ghostQualities = new[]
            {
                new Feat { Name = "ghost quality 1" },
                new Feat { Name = "ghost quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Ghost,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(ghostQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, false);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + ghostQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(ghostQualities));
        }

        [Test]
        public void ApplyTo_CreatureSkills_GainRacialBonuses()
        {
            var skills = new[]
            {
                new Skill("other skill 1", baseCreature.Abilities[AbilityConstants.Constitution], 42),
                new Skill(SkillConstants.Hide, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 2", baseCreature.Abilities[AbilityConstants.Strength], 42),
                new Skill(SkillConstants.Listen, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 3", baseCreature.Abilities[AbilityConstants.Dexterity], 42),
                new Skill(SkillConstants.Search, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 4", baseCreature.Abilities[AbilityConstants.Intelligence], 42),
                new Skill(SkillConstants.Spot, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 5", baseCreature.Abilities[AbilityConstants.Charisma], 42),
            };

            foreach (var skill in skills)
            {
                skill.AddBonus(600);
                skill.AddBonus(666, "conditional");
            }

            baseCreature.Skills = baseCreature.Skills.Union(skills);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Skills, Is.SupersetOf(skills));
            Assert.That(skills[0].Name, Is.EqualTo("other skill 1"));
            Assert.That(skills[0].Bonus, Is.EqualTo(600));
            Assert.That(skills[0].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[1].Name, Is.EqualTo(SkillConstants.Hide));
            Assert.That(skills[1].Bonus, Is.EqualTo(608));
            Assert.That(skills[1].Bonuses.Count(), Is.EqualTo(3));
            Assert.That(skills[2].Name, Is.EqualTo("other skill 2"));
            Assert.That(skills[2].Bonus, Is.EqualTo(600));
            Assert.That(skills[2].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[3].Name, Is.EqualTo(SkillConstants.Listen));
            Assert.That(skills[3].Bonus, Is.EqualTo(608));
            Assert.That(skills[3].Bonuses.Count(), Is.EqualTo(3));
            Assert.That(skills[4].Name, Is.EqualTo("other skill 3"));
            Assert.That(skills[4].Bonus, Is.EqualTo(600));
            Assert.That(skills[4].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[5].Name, Is.EqualTo(SkillConstants.Search));
            Assert.That(skills[5].Bonus, Is.EqualTo(608));
            Assert.That(skills[5].Bonuses.Count(), Is.EqualTo(3));
            Assert.That(skills[6].Name, Is.EqualTo("other skill 4"));
            Assert.That(skills[6].Bonus, Is.EqualTo(600));
            Assert.That(skills[6].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[7].Name, Is.EqualTo(SkillConstants.Spot));
            Assert.That(skills[7].Bonus, Is.EqualTo(608));
            Assert.That(skills[7].Bonuses.Count(), Is.EqualTo(3));
            Assert.That(skills[8].Name, Is.EqualTo("other skill 5"));
            Assert.That(skills[8].Bonus, Is.EqualTo(600));
            Assert.That(skills[8].Bonuses.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ApplyTo_CreatureSkills_GainRacialBonuses_NoSkills()
        {
            baseCreature.Skills = new List<Skill>();

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Skills, Is.Empty);
        }

        [Test]
        public void ApplyTo_CreatureSkills_GainRacialBonuses_MissingRelevantSkills()
        {
            var skills = new[]
            {
                new Skill("other skill 1", baseCreature.Abilities[AbilityConstants.Constitution], 42),
                new Skill("other skill 2", baseCreature.Abilities[AbilityConstants.Strength], 42),
                new Skill("other skill 3", baseCreature.Abilities[AbilityConstants.Dexterity], 42),
                new Skill("other skill 4", baseCreature.Abilities[AbilityConstants.Intelligence], 42),
                new Skill("other skill 5", baseCreature.Abilities[AbilityConstants.Charisma], 42),
            };

            foreach (var skill in skills)
            {
                skill.AddBonus(600);
                skill.AddBonus(666, "conditional");
            }

            baseCreature.Skills = baseCreature.Skills.Union(skills);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Skills, Is.SupersetOf(skills));
            Assert.That(skills[0].Name, Is.EqualTo("other skill 1"));
            Assert.That(skills[0].Bonus, Is.EqualTo(600));
            Assert.That(skills[0].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[1].Name, Is.EqualTo("other skill 2"));
            Assert.That(skills[1].Bonus, Is.EqualTo(600));
            Assert.That(skills[1].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[2].Name, Is.EqualTo("other skill 3"));
            Assert.That(skills[2].Bonus, Is.EqualTo(600));
            Assert.That(skills[2].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[3].Name, Is.EqualTo("other skill 4"));
            Assert.That(skills[3].Bonus, Is.EqualTo(600));
            Assert.That(skills[3].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[4].Name, Is.EqualTo("other skill 5"));
            Assert.That(skills[4].Bonus, Is.EqualTo(600));
            Assert.That(skills[4].Bonuses.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ApplyTo_CreatureSkills_SwapsAbilityForConcentrationToCharisma()
        {
            var concentration = new Skill(SkillConstants.Concentration, baseCreature.Abilities[AbilityConstants.Constitution], 42);
            baseCreature.Skills = baseCreature.Skills.Union(new[] { concentration });

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Skills, Contains.Item(concentration));
            Assert.That(concentration.BaseAbility, Is.EqualTo(creature.Abilities[AbilityConstants.Charisma]));
        }

        [Test]
        public void ApplyTo_CreatureSkills_DoNotHaveFortitudeSave()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Saves[SaveConstants.Fortitude].HasSave, Is.False);
            Assert.That(creature.Saves[SaveConstants.Reflex].HasSave, Is.True);
            Assert.That(creature.Saves[SaveConstants.Will].HasSave, Is.True);
        }

        [Test]
        public async Task ApplyToAsync_CreatureSkills_SwapsAbilityForConcentrationToCharisma()
        {
            var concentration = new Skill(SkillConstants.Concentration, baseCreature.Abilities[AbilityConstants.Constitution], 42);
            baseCreature.Skills = baseCreature.Skills.Union(new[] { concentration });

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Skills, Contains.Item(concentration));
            Assert.That(concentration.BaseAbility, Is.EqualTo(creature.Abilities[AbilityConstants.Charisma]));
        }

        [Test]
        public async Task ApplyToAsync_CreatureSkills_DoNotHaveFortitudeSave()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Saves[SaveConstants.Fortitude].HasSave, Is.False);
            Assert.That(creature.Saves[SaveConstants.Reflex].HasSave, Is.True);
            Assert.That(creature.Saves[SaveConstants.Will].HasSave, Is.True);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void ApplyTo_CreatureChallengeRating_IncreasesBy2(string original, string adjusted)
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
                //INFO: Don't need to test every CR, since it is the basic Increase functionality, which is tested separately
                //So, we only need to test the amount it is increased, not every CR permutation
                var challengeRating = ChallengeRatingConstants.CR1;

                var newCr = ChallengeRatingConstants.IncreaseChallengeRating(challengeRating, 2);
                yield return new TestCaseData(challengeRating, newCr);
            }
        }

        [TestCase(null, null)]
        [TestCase(0, 5)]
        [TestCase(1, 6)]
        [TestCase(2, 7)]
        [TestCase(10, 15)]
        [TestCase(20, 25)]
        [TestCase(42, 47)]
        public void ApplyTo_CreatureLevelAdjustment_Increases(int? original, int? adjusted)
        {
            baseCreature.LevelAdjustment = original;

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [Test]
        public void ApplyTo_CreatureEquipment_CannotUseEquipment_HasNoEquipment_GainsNone()
        {
            baseCreature.CanUseEquipment = false;
            baseCreature.Equipment.Armor = null;
            baseCreature.Equipment.Items = new List<Item>();
            baseCreature.Equipment.Shield = null;
            baseCreature.Equipment.Weapons = new List<Weapon>();

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.False);
            Assert.That(creature.Equipment.Armor, Is.Null);
            Assert.That(creature.Equipment.Items, Is.Empty);
            Assert.That(creature.Equipment.Shield, Is.Null);
            Assert.That(creature.Equipment.Weapons, Is.Empty);
        }

        //INFO: Example is Nessian Warhound that has barding
        [Test]
        public void ApplyTo_CreatureEquipment_CannotUseEquipment_HasEquipment_GainsNone()
        {
            baseCreature.CanUseEquipment = false;
            baseCreature.Equipment.Armor = new Armor { Name = "my armor" };
            baseCreature.Equipment.Items = new List<Item>();
            baseCreature.Equipment.Shield = null;
            baseCreature.Equipment.Weapons = new List<Weapon>();

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.False);
            Assert.That(creature.Equipment.Armor, Is.Not.Null);
            Assert.That(creature.Equipment.Armor.Name, Is.EqualTo("my armor"));
            Assert.That(creature.Equipment.Items, Is.Empty);
            Assert.That(creature.Equipment.Shield, Is.Null);
            Assert.That(creature.Equipment.Weapons, Is.Empty);
        }

        [Test]
        public void ApplyTo_CreatureEquipment_HasNone_GainsNone()
        {
            baseCreature.CanUseEquipment = true;
            baseCreature.Equipment.Armor = null;
            baseCreature.Equipment.Items = new List<Item>();
            baseCreature.Equipment.Shield = null;
            baseCreature.Equipment.Weapons = new List<Weapon>();

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.True);
            Assert.That(creature.Equipment.Armor, Is.Null);
            Assert.That(creature.Equipment.Items, Is.Empty);
            Assert.That(creature.Equipment.Shield, Is.Null);
            Assert.That(creature.Equipment.Weapons, Is.Empty);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void ApplyTo_CreatureEquipment_HasWeapon_GainsGhostlyEquipment(int quantity)
        {
            var weapons = new[] { new Weapon { Name = "my weapon" } };
            baseCreature.CanUseEquipment = true;
            baseCreature.Equipment.Armor = null;
            baseCreature.Equipment.Items = new List<Item>();
            baseCreature.Equipment.Shield = null;
            baseCreature.Equipment.Weapons = weapons;

            mockDice
                .Setup(d => d.Roll(2).d(4).AsSum<int>())
                .Returns(quantity);

            var count = 1;
            mockItemsGenerator
                .Setup(g => g.GenerateRandomAtLevel(baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(() => Enumerable.Range(1, count++)
                    .Select(i => new Item { Name = $"Item {count - 1}.{i}" }));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.True);
            Assert.That(creature.Equipment.Armor, Is.Null);
            Assert.That(creature.Equipment.Shield, Is.Null);
            Assert.That(creature.Equipment.Weapons, Is.EqualTo(weapons));

            var items = creature.Equipment.Items.ToArray();
            Assert.That(items, Has.Length.EqualTo(quantity));
            Assert.That(items[0].Name, Is.EqualTo("Item 1.1"));
            Assert.That(items[1].Name, Is.EqualTo("Item 2.1"));

            if (quantity > 2)
                Assert.That(items[2].Name, Is.EqualTo("Item 2.2"));

            if (quantity > 3)
                Assert.That(items[3].Name, Is.EqualTo("Item 3.1"));

            if (quantity > 4)
                Assert.That(items[4].Name, Is.EqualTo("Item 3.2"));

            if (quantity > 5)
                Assert.That(items[5].Name, Is.EqualTo("Item 3.3"));

            if (quantity > 6)
                Assert.That(items[6].Name, Is.EqualTo("Item 4.1"));

            if (quantity > 7)
                Assert.That(items[7].Name, Is.EqualTo("Item 4.2"));
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void ApplyTo_CreatureEquipment_HasArmor_GainsGhostlyEquipment(int quantity)
        {
            baseCreature.CanUseEquipment = true;
            baseCreature.Equipment.Armor = new Armor { Name = "my armor" };
            baseCreature.Equipment.Items = new List<Item>();
            baseCreature.Equipment.Shield = null;
            baseCreature.Equipment.Weapons = new List<Weapon>();

            mockDice
                .Setup(d => d.Roll(2).d(4).AsSum<int>())
                .Returns(quantity);

            var count = 1;
            mockItemsGenerator
                .Setup(g => g.GenerateRandomAtLevel(baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(() => Enumerable.Range(1, count++)
                    .Select(i => new Item { Name = $"Item {count - 1}.{i}" }));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.True);
            Assert.That(creature.Equipment.Armor, Is.Not.Null);
            Assert.That(creature.Equipment.Armor.Name, Is.EqualTo("my armor"));
            Assert.That(creature.Equipment.Shield, Is.Null);
            Assert.That(creature.Equipment.Weapons, Is.Empty);

            var items = creature.Equipment.Items.ToArray();
            Assert.That(items, Has.Length.EqualTo(quantity));
            Assert.That(items[0].Name, Is.EqualTo("Item 1.1"));
            Assert.That(items[1].Name, Is.EqualTo("Item 2.1"));

            if (quantity > 2)
                Assert.That(items[2].Name, Is.EqualTo("Item 2.2"));

            if (quantity > 3)
                Assert.That(items[3].Name, Is.EqualTo("Item 3.1"));

            if (quantity > 4)
                Assert.That(items[4].Name, Is.EqualTo("Item 3.2"));

            if (quantity > 5)
                Assert.That(items[5].Name, Is.EqualTo("Item 3.3"));

            if (quantity > 6)
                Assert.That(items[6].Name, Is.EqualTo("Item 4.1"));

            if (quantity > 7)
                Assert.That(items[7].Name, Is.EqualTo("Item 4.2"));
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void ApplyTo_CreatureEquipment_HasShield_GainsGhostlyEquipment(int quantity)
        {
            baseCreature.CanUseEquipment = true;
            baseCreature.Equipment.Armor = null;
            baseCreature.Equipment.Items = new List<Item>();
            baseCreature.Equipment.Shield = new Armor { Name = "my shield" };
            baseCreature.Equipment.Weapons = new List<Weapon>();

            mockDice
                .Setup(d => d.Roll(2).d(4).AsSum<int>())
                .Returns(quantity);

            var count = 1;
            mockItemsGenerator
                .Setup(g => g.GenerateRandomAtLevel(baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(() => Enumerable.Range(1, count++)
                    .Select(i => new Item { Name = $"Item {count - 1}.{i}" }));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.True);
            Assert.That(creature.Equipment.Armor, Is.Null);
            Assert.That(creature.Equipment.Shield, Is.Not.Null);
            Assert.That(creature.Equipment.Shield.Name, Is.EqualTo("my shield"));
            Assert.That(creature.Equipment.Weapons, Is.Empty);

            var items = creature.Equipment.Items.ToArray();
            Assert.That(items, Has.Length.EqualTo(quantity));
            Assert.That(items[0].Name, Is.EqualTo("Item 1.1"));
            Assert.That(items[1].Name, Is.EqualTo("Item 2.1"));

            if (quantity > 2)
                Assert.That(items[2].Name, Is.EqualTo("Item 2.2"));

            if (quantity > 3)
                Assert.That(items[3].Name, Is.EqualTo("Item 3.1"));

            if (quantity > 4)
                Assert.That(items[4].Name, Is.EqualTo("Item 3.2"));

            if (quantity > 5)
                Assert.That(items[5].Name, Is.EqualTo("Item 3.3"));

            if (quantity > 6)
                Assert.That(items[6].Name, Is.EqualTo("Item 4.1"));

            if (quantity > 7)
                Assert.That(items[7].Name, Is.EqualTo("Item 4.2"));
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void ApplyTo_CreatureEquipment_HasItem_GainsGhostlyEquipment(int quantity)
        {
            var items = new[] { new Item { Name = "my item" } };
            baseCreature.CanUseEquipment = true;
            baseCreature.Equipment.Armor = null;
            baseCreature.Equipment.Items = items;
            baseCreature.Equipment.Shield = new Armor { Name = "my shield" };
            baseCreature.Equipment.Weapons = new List<Weapon>();

            mockDice
                .Setup(d => d.Roll(2).d(4).AsSum<int>())
                .Returns(quantity);

            var count = 1;
            mockItemsGenerator
                .Setup(g => g.GenerateRandomAtLevel(baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(() => Enumerable.Range(1, count++)
                    .Select(i => new Item { Name = $"Item {count - 1}.{i}" }));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.True);
            Assert.That(creature.Equipment.Armor, Is.Null);
            Assert.That(creature.Equipment.Shield, Is.Not.Null);
            Assert.That(creature.Equipment.Shield.Name, Is.EqualTo("my shield"));
            Assert.That(creature.Equipment.Weapons, Is.Empty);

            var newItems = creature.Equipment.Items.ToArray();
            Assert.That(newItems, Has.Length.EqualTo(quantity + 1));
            Assert.That(newItems[0].Name, Is.EqualTo("my item"));
            Assert.That(newItems[1].Name, Is.EqualTo("Item 1.1"));
            Assert.That(newItems[2].Name, Is.EqualTo("Item 2.1"));

            if (quantity > 2)
                Assert.That(newItems[3].Name, Is.EqualTo("Item 2.2"));

            if (quantity > 3)
                Assert.That(newItems[4].Name, Is.EqualTo("Item 3.1"));

            if (quantity > 4)
                Assert.That(newItems[5].Name, Is.EqualTo("Item 3.2"));

            if (quantity > 5)
                Assert.That(newItems[6].Name, Is.EqualTo("Item 3.3"));

            if (quantity > 6)
                Assert.That(newItems[7].Name, Is.EqualTo("Item 4.1"));

            if (quantity > 7)
                Assert.That(newItems[8].Name, Is.EqualTo("Item 4.2"));
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public void ApplyTo_CreatureEquipment_HasItems_GainsGhostlyEquipment(int quantity)
        {
            var items = new[] { new Item { Name = "my item" }, new Item { Name = "my other item" } };
            baseCreature.CanUseEquipment = true;
            baseCreature.Equipment.Armor = null;
            baseCreature.Equipment.Items = items;
            baseCreature.Equipment.Shield = new Armor { Name = "my shield" };
            baseCreature.Equipment.Weapons = new List<Weapon>();

            mockDice
                .Setup(d => d.Roll(2).d(4).AsSum<int>())
                .Returns(quantity);

            var count = 1;
            mockItemsGenerator
                .Setup(g => g.GenerateRandomAtLevel(baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(() => Enumerable.Range(1, count++)
                    .Select(i => new Item { Name = $"Item {count - 1}.{i}" }));

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.True);
            Assert.That(creature.Equipment.Armor, Is.Null);
            Assert.That(creature.Equipment.Shield, Is.Not.Null);
            Assert.That(creature.Equipment.Shield.Name, Is.EqualTo("my shield"));
            Assert.That(creature.Equipment.Weapons, Is.Empty);

            var newItems = creature.Equipment.Items.ToArray();
            Assert.That(newItems, Has.Length.EqualTo(quantity + 2));
            Assert.That(newItems[0].Name, Is.EqualTo("my item"));
            Assert.That(newItems[1].Name, Is.EqualTo("my other item"));
            Assert.That(newItems[2].Name, Is.EqualTo("Item 1.1"));
            Assert.That(newItems[3].Name, Is.EqualTo("Item 2.1"));

            if (quantity > 2)
                Assert.That(newItems[4].Name, Is.EqualTo("Item 2.2"));

            if (quantity > 3)
                Assert.That(newItems[5].Name, Is.EqualTo("Item 3.1"));

            if (quantity > 4)
                Assert.That(newItems[6].Name, Is.EqualTo("Item 3.2"));

            if (quantity > 5)
                Assert.That(newItems[7].Name, Is.EqualTo("Item 3.3"));

            if (quantity > 6)
                Assert.That(newItems[8].Name, Is.EqualTo("Item 4.1"));

            if (quantity > 7)
                Assert.That(newItems[9].Name, Is.EqualTo("Item 4.2"));
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
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Ghost}");

            Assert.That(async () => await applicator.ApplyToAsync(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR3, "wrong alignment", "Alignment filter 'wrong alignment' is not valid")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR2, "original alignment", "CR filter 2 does not match updated creature CR 3 (from CR 1)")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR3, "original alignment", "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR3, "original alignment", "",
            Ignore = "As Character doesn't affect already-generated creature compatiblity")]
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
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Ghost}");
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
            baseCreature.Alignment = new Alignment("original alignment");
            baseCreature.Templates.Add("my other template");

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

            var ghostAttacks = new[]
            {
                new Attack { Name = "spoopy", IsSpecial = true },
                new Attack { Name = "Manifestation", IsSpecial = true },
                new Attack { Name = "spooky", IsSpecial = true },
                new Attack { Name = "scary", IsSpecial = true },
                new Attack { Name = "skeletons", IsSpecial = true },
                new Attack { Name = "shout", IsSpecial = true },
                new Attack { Name = "starkly", IsSpecial = true },
                new Attack { Name = "thrilling", IsSpecial = true },
                new Attack { Name = "screams", IsSpecial = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Ghost,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(ghostAttacks);

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR3;
            filters.Alignment = "original alignment";

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.Ghost));
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

            var ghostAttacks = new[]
            {
                new Attack { Name = "spoopy", IsSpecial = true },
                new Attack { Name = "Manifestation", IsSpecial = true },
                new Attack { Name = "spooky", IsSpecial = true },
                new Attack { Name = "scary", IsSpecial = true },
                new Attack { Name = "skeletons", IsSpecial = true },
                new Attack { Name = "shout", IsSpecial = true },
                new Attack { Name = "starkly", IsSpecial = true },
                new Attack { Name = "thrilling", IsSpecial = true },
                new Attack { Name = "screams", IsSpecial = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Ghost,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(ghostAttacks);

            var filters = new Filters();
            filters.Type = "subtype 1";
            filters.ChallengeRating = ChallengeRatingConstants.CR3;
            filters.Alignment = "original alignment";

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Ghost));
        }

        [TestCase(CreatureConstants.Types.Aberration, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Dragon, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Giant, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Plant, CreatureConstants.Types.Undead)]
        public async Task ApplyToAsync_CreatureTypeIsAdjusted(string original, string adjusted)
        {
            baseCreature.Type.Name = original;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(adjusted));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(5));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(original)
                .And.Contains(CreatureConstants.Types.Subtypes.Incorporeal)
                .And.Contains(CreatureConstants.Types.Subtypes.Augmented));
        }

        [Test]
        public async Task ApplyToAsync_DemographicsAdjusted()
        {
            baseCreature.Demographics.Skin = "I look like a potato skin.";
            baseCreature.Demographics.Hair = "I look like a potato hair.";
            baseCreature.Demographics.Eyes = "I look like a potato eyes.";
            baseCreature.Demographics.Other = "I look like a potato other.";
            baseCreature.Demographics.Age.Value = 42;
            baseCreature.Demographics.MaximumAge.Value = 600;

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Appearances("Skin"), CreatureConstants.Templates.Ghost))
                .Returns("I am the meanest boi skin.");
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Appearances("Hair"), CreatureConstants.Templates.Ghost))
                .Returns("I am the meanest boi hair.");
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Appearances("Eyes"), CreatureConstants.Templates.Ghost))
                .Returns("I am the meanest boi eyes.");
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Appearances("Other"), CreatureConstants.Templates.Ghost))
                .Returns("I am the meanest boi other.");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Undead, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Maximum, Amount = AgeConstants.Ageless, RawAmount = AgeConstants.Ageless.ToString() });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, CreatureConstants.Templates.Ghost))
                .Returns(ageRolls);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics.Age.Value, Is.EqualTo(42 + 9266));
            Assert.That(creature.Demographics.MaximumAge.Value, Is.EqualTo(AgeConstants.Ageless));
            Assert.That(creature.Demographics.Skin, Is.EqualTo("I look like a potato skin. I am the meanest boi skin."));
            Assert.That(creature.Demographics.Hair, Is.EqualTo("I look like a potato hair. I am the meanest boi hair."));
            Assert.That(creature.Demographics.Eyes, Is.EqualTo("I look like a potato eyes. I am the meanest boi eyes."));
            Assert.That(creature.Demographics.Other, Is.EqualTo("I look like a potato other. I am the meanest boi other."));
        }

        [Test]
        public async Task ApplyToAsync_CharismaIncreasesBy4()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(4));
        }

        [Test]
        public async Task ApplyToAsync_ConstitutionGoesAway()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].HasScore, Is.False);
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        public async Task ApplyToAsync_HitDiceChangeToD12_AndRerolled(int die)
        {
            baseCreature.HitPoints.HitDice[0].HitDie = die;

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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [Test]
        public async Task ApplyToAsync_HitDiceChangeToD12_AndRerolled_WithoutConstitution()
        {
            baseCreature.HitPoints.HitDice[0].HitDie = 4;

            baseCreature.Abilities[AbilityConstants.Constitution].BaseScore = 600;

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

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [Test]
        public async Task ApplyToAsync_CreatureGainsFlySpeed()
        {
            var speeds = new Dictionary<string, Measurement>();
            speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            speeds[SpeedConstants.Fly].Description = "the goodest";
            speeds[SpeedConstants.Fly].Value = 96;

            mockSpeedsGenerator
                .Setup(g => g.Generate(CreatureConstants.Templates.Ghost))
                .Returns(speeds);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(96));
        }

        [Test]
        public async Task ApplyToAsync_CreatureGainsFlySpeed_BetterThanOriginalFlySpeed()
        {
            var speeds = new Dictionary<string, Measurement>();
            speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            speeds[SpeedConstants.Fly].Description = "the goodest";
            speeds[SpeedConstants.Fly].Value = 96;

            mockSpeedsGenerator
                .Setup(g => g.Generate(CreatureConstants.Templates.Ghost))
                .Returns(speeds);

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
        public async Task ApplyToAsync_CreatureGainsFlySpeed_SlowerThanOriginalFlySpeed()
        {
            var speeds = new Dictionary<string, Measurement>();
            speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            speeds[SpeedConstants.Fly].Description = "the goodest";
            speeds[SpeedConstants.Fly].Value = 96;

            mockSpeedsGenerator
                .Setup(g => g.Generate(CreatureConstants.Templates.Ghost))
                .Returns(speeds);

            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Description = "so-so";
            baseCreature.Speeds[SpeedConstants.Fly].Value = 600;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(600));
        }

        [Test]
        public async Task ApplyToAsync_CreatureGainsFlySpeed_OriginalFlySpeedLessManeuverable()
        {
            var speeds = new Dictionary<string, Measurement>();
            speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            speeds[SpeedConstants.Fly].Description = "the goodest";
            speeds[SpeedConstants.Fly].Value = 96;

            mockSpeedsGenerator
                .Setup(g => g.Generate(CreatureConstants.Templates.Ghost))
                .Returns(speeds);

            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            baseCreature.Speeds[SpeedConstants.Fly].Description = "so-so";
            baseCreature.Speeds[SpeedConstants.Fly].Value = 96;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(96));
        }

        [Test]
        public async Task ApplyToAsync_CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 42);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);

            var naturalArmorBonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(naturalArmorBonuses, Has.Length.EqualTo(1));
            Assert.That(naturalArmorBonuses[0].Value, Is.EqualTo(42));
            Assert.That(naturalArmorBonuses[0].IsConditional, Is.True);
            Assert.That(naturalArmorBonuses[0].Condition, Is.EqualTo("Only applies for ethereal creatures"));
        }

        [Test]
        public async Task ApplyToAsync_CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal_MultipleBonuses()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 42);
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 600);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);

            var naturalArmorBonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(naturalArmorBonuses, Has.Length.EqualTo(2));
            Assert.That(naturalArmorBonuses[0].Value, Is.EqualTo(42));
            Assert.That(naturalArmorBonuses[0].IsConditional, Is.True);
            Assert.That(naturalArmorBonuses[0].Condition, Is.EqualTo("Only applies for ethereal creatures"));
            Assert.That(naturalArmorBonuses[1].Value, Is.EqualTo(600));
            Assert.That(naturalArmorBonuses[1].IsConditional, Is.True);
            Assert.That(naturalArmorBonuses[1].Condition, Is.EqualTo("Only applies for ethereal creatures"));
        }

        [Test]
        public async Task ApplyToAsync_CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal_NoNaturalArmor()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(creature.ArmorClass.NaturalArmorBonuses, Is.Empty);
        }

        [Test]
        public async Task ApplyToAsync_CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal_AlreadyConditional()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 42, "only sometimes");

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);

            var naturalArmorBonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(naturalArmorBonuses, Has.Length.EqualTo(1));
            Assert.That(naturalArmorBonuses[0].Value, Is.EqualTo(42));
            Assert.That(naturalArmorBonuses[0].IsConditional, Is.True);
            Assert.That(naturalArmorBonuses[0].Condition, Is.EqualTo("only sometimes AND Only applies for ethereal creatures"));
        }

        [Test]
        public async Task ApplyToAsync_CreatureArmorClass_DeflectionBonusOfCharisma()
        {
            baseCreature.Abilities[AbilityConstants.Charisma].BaseScore = 42;
            baseCreature.Abilities[AbilityConstants.Charisma].AdvancementAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Charisma].RacialAdjustment = 0;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(46));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(42));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(creature.ArmorClass.DeflectionBonus, Is.EqualTo(18).And.EqualTo(baseCreature.Abilities[AbilityConstants.Charisma].Modifier));

            var deflectionBonuses = creature.ArmorClass.DeflectionBonuses.ToArray();
            Assert.That(deflectionBonuses, Has.Length.EqualTo(1));
            Assert.That(deflectionBonuses[0].Value, Is.EqualTo(18).And.EqualTo(baseCreature.Abilities[AbilityConstants.Charisma].Modifier));
            Assert.That(deflectionBonuses[0].IsConditional, Is.False);
            Assert.That(deflectionBonuses[0].Condition, Is.Empty);
        }

        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        public async Task ApplyToAsync_CreatureArmorClass_DeflectionBonusOfCharisma_AtLeast1(int charisma)
        {
            baseCreature.Abilities[AbilityConstants.Charisma].BaseScore = charisma;
            baseCreature.Abilities[AbilityConstants.Charisma].AdvancementAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Charisma].RacialAdjustment = 0;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(charisma + 4));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(charisma));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.AtLeast(-1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.AtLeast(4));
            Assert.That(creature.ArmorClass.DeflectionBonus, Is.Positive.And.EqualTo(Math.Max(1, creature.Abilities[AbilityConstants.Charisma].Modifier)));

            var deflectionBonuses = creature.ArmorClass.DeflectionBonuses.ToArray();
            Assert.That(deflectionBonuses, Has.Length.EqualTo(1));
            Assert.That(deflectionBonuses[0].Value, Is.EqualTo(Math.Max(1, creature.Abilities[AbilityConstants.Charisma].Modifier)));
            Assert.That(deflectionBonuses[0].IsConditional, Is.False);
            Assert.That(deflectionBonuses[0].Condition, Is.Empty);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task ApplyToAsync_CreatureGainsSpecialAttacks_RandomAmount(int amount)
        {
            //one to three
            //use attack generator to get all
            var manifestation = new Attack { Name = "Manifestation", IsSpecial = true };
            var ghostAttacks = new[]
            {
                new Attack { Name = "spoopy", IsSpecial = true },
                manifestation,
                new Attack { Name = "spooky", IsSpecial = true },
                new Attack { Name = "scary", IsSpecial = true },
                new Attack { Name = "skeletons", IsSpecial = true },
                new Attack { Name = "shout", IsSpecial = true },
                new Attack { Name = "starkly", IsSpecial = true },
                new Attack { Name = "thrilling", IsSpecial = true },
                new Attack { Name = "screams", IsSpecial = true },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Ghost,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(ghostAttacks);

            mockDice
                .Setup(d => d.Roll(1).d(3).AsSum<int>())
                .Returns(amount);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, false);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + 1 + amount));
            Assert.That(creature.Attacks, Contains.Item(manifestation).And.Contains(ghostAttacks[0]));

            if (amount > 1)
            {
                Assert.That(creature.Attacks, Contains.Item(ghostAttacks[3]));
            }

            if (amount > 2)
            {
                Assert.That(creature.Attacks, Contains.Item(ghostAttacks[5]));
            }
        }

        [Test]
        public async Task ApplyToAsync_CreatureGainsSpecialQualities()
        {
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
            var ghostQualities = new[]
            {
                new Feat { Name = "ghost quality 1" },
                new Feat { Name = "ghost quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Ghost,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Undead
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Incorporeal,
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(ghostQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, false);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + ghostQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(ghostQualities));
        }

        [Test]
        public async Task ApplyToAsync_CreatureSkills_GainRacialBonuses()
        {
            var skills = new[]
            {
                new Skill("other skill 1", baseCreature.Abilities[AbilityConstants.Constitution], 42),
                new Skill(SkillConstants.Hide, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 2", baseCreature.Abilities[AbilityConstants.Strength], 42),
                new Skill(SkillConstants.Listen, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 3", baseCreature.Abilities[AbilityConstants.Dexterity], 42),
                new Skill(SkillConstants.Search, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 4", baseCreature.Abilities[AbilityConstants.Intelligence], 42),
                new Skill(SkillConstants.Spot, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 5", baseCreature.Abilities[AbilityConstants.Charisma], 42),
            };

            foreach (var skill in skills)
            {
                skill.AddBonus(600);
                skill.AddBonus(666, "conditional");
            }

            baseCreature.Skills = baseCreature.Skills.Union(skills);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Skills, Is.SupersetOf(skills));
            Assert.That(skills[0].Name, Is.EqualTo("other skill 1"));
            Assert.That(skills[0].Bonus, Is.EqualTo(600));
            Assert.That(skills[0].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[1].Name, Is.EqualTo(SkillConstants.Hide));
            Assert.That(skills[1].Bonus, Is.EqualTo(608));
            Assert.That(skills[1].Bonuses.Count(), Is.EqualTo(3));
            Assert.That(skills[2].Name, Is.EqualTo("other skill 2"));
            Assert.That(skills[2].Bonus, Is.EqualTo(600));
            Assert.That(skills[2].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[3].Name, Is.EqualTo(SkillConstants.Listen));
            Assert.That(skills[3].Bonus, Is.EqualTo(608));
            Assert.That(skills[3].Bonuses.Count(), Is.EqualTo(3));
            Assert.That(skills[4].Name, Is.EqualTo("other skill 3"));
            Assert.That(skills[4].Bonus, Is.EqualTo(600));
            Assert.That(skills[4].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[5].Name, Is.EqualTo(SkillConstants.Search));
            Assert.That(skills[5].Bonus, Is.EqualTo(608));
            Assert.That(skills[5].Bonuses.Count(), Is.EqualTo(3));
            Assert.That(skills[6].Name, Is.EqualTo("other skill 4"));
            Assert.That(skills[6].Bonus, Is.EqualTo(600));
            Assert.That(skills[6].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[7].Name, Is.EqualTo(SkillConstants.Spot));
            Assert.That(skills[7].Bonus, Is.EqualTo(608));
            Assert.That(skills[7].Bonuses.Count(), Is.EqualTo(3));
            Assert.That(skills[8].Name, Is.EqualTo("other skill 5"));
            Assert.That(skills[8].Bonus, Is.EqualTo(600));
            Assert.That(skills[8].Bonuses.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task ApplyToAsync_CreatureSkills_GainRacialBonuses_NoSkills()
        {
            baseCreature.Skills = new List<Skill>();

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Skills, Is.Empty);
        }

        [Test]
        public async Task ApplyToAsync_CreatureSkills_GainRacialBonuses_MissingRelevantSkills()
        {
            var skills = new[]
            {
                new Skill("other skill 1", baseCreature.Abilities[AbilityConstants.Constitution], 42),
                new Skill("other skill 2", baseCreature.Abilities[AbilityConstants.Strength], 42),
                new Skill("other skill 3", baseCreature.Abilities[AbilityConstants.Dexterity], 42),
                new Skill("other skill 4", baseCreature.Abilities[AbilityConstants.Intelligence], 42),
                new Skill("other skill 5", baseCreature.Abilities[AbilityConstants.Charisma], 42),
            };

            foreach (var skill in skills)
            {
                skill.AddBonus(600);
                skill.AddBonus(666, "conditional");
            }

            baseCreature.Skills = baseCreature.Skills.Union(skills);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Skills, Is.SupersetOf(skills));
            Assert.That(skills[0].Name, Is.EqualTo("other skill 1"));
            Assert.That(skills[0].Bonus, Is.EqualTo(600));
            Assert.That(skills[0].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[1].Name, Is.EqualTo("other skill 2"));
            Assert.That(skills[1].Bonus, Is.EqualTo(600));
            Assert.That(skills[1].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[2].Name, Is.EqualTo("other skill 3"));
            Assert.That(skills[2].Bonus, Is.EqualTo(600));
            Assert.That(skills[2].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[3].Name, Is.EqualTo("other skill 4"));
            Assert.That(skills[3].Bonus, Is.EqualTo(600));
            Assert.That(skills[3].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[4].Name, Is.EqualTo("other skill 5"));
            Assert.That(skills[4].Bonus, Is.EqualTo(600));
            Assert.That(skills[4].Bonuses.Count(), Is.EqualTo(2));
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public async Task ApplyToAsync_CreatureChallengeRating_IncreasesBy2(string original, string adjusted)
        {
            baseCreature.ChallengeRating = original;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        [TestCase(null, null)]
        [TestCase(0, 5)]
        [TestCase(1, 6)]
        [TestCase(2, 7)]
        [TestCase(10, 15)]
        [TestCase(20, 25)]
        [TestCase(42, 47)]
        public async Task ApplyToAsync_CreatureLevelAdjustment_Increases(int? original, int? adjusted)
        {
            baseCreature.LevelAdjustment = original;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [Test]
        public async Task ApplyToAsync_CreatureEquipment_CannotUseEquipment_HasNoEquipment_GainsNone()
        {
            baseCreature.CanUseEquipment = false;
            baseCreature.Equipment.Armor = null;
            baseCreature.Equipment.Items = new List<Item>();
            baseCreature.Equipment.Shield = null;
            baseCreature.Equipment.Weapons = new List<Weapon>();

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.False);
            Assert.That(creature.Equipment.Armor, Is.Null);
            Assert.That(creature.Equipment.Items, Is.Empty);
            Assert.That(creature.Equipment.Shield, Is.Null);
            Assert.That(creature.Equipment.Weapons, Is.Empty);
        }

        //INFO: Example is Nessian Warhound that has barding
        [Test]
        public async Task ApplyToAsync_CreatureEquipment_CannotUseEquipment_HasEquipment_GainsNone()
        {
            baseCreature.CanUseEquipment = false;
            baseCreature.Equipment.Armor = new Armor { Name = "my armor" };
            baseCreature.Equipment.Items = new List<Item>();
            baseCreature.Equipment.Shield = null;
            baseCreature.Equipment.Weapons = new List<Weapon>();

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.False);
            Assert.That(creature.Equipment.Armor, Is.Not.Null);
            Assert.That(creature.Equipment.Armor.Name, Is.EqualTo("my armor"));
            Assert.That(creature.Equipment.Items, Is.Empty);
            Assert.That(creature.Equipment.Shield, Is.Null);
            Assert.That(creature.Equipment.Weapons, Is.Empty);
        }

        [Test]
        public async Task ApplyToAsync_CreatureEquipment_HasNone_GainsNone()
        {
            baseCreature.CanUseEquipment = true;
            baseCreature.Equipment.Armor = null;
            baseCreature.Equipment.Items = new List<Item>();
            baseCreature.Equipment.Shield = null;
            baseCreature.Equipment.Weapons = new List<Weapon>();

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.True);
            Assert.That(creature.Equipment.Armor, Is.Null);
            Assert.That(creature.Equipment.Items, Is.Empty);
            Assert.That(creature.Equipment.Shield, Is.Null);
            Assert.That(creature.Equipment.Weapons, Is.Empty);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public async Task ApplyToAsync_CreatureEquipment_HasWeapon_GainsGhostlyEquipment(int quantity)
        {
            var weapons = new[] { new Weapon { Name = "my weapon" } };
            baseCreature.CanUseEquipment = true;
            baseCreature.Equipment.Armor = null;
            baseCreature.Equipment.Items = new List<Item>();
            baseCreature.Equipment.Shield = null;
            baseCreature.Equipment.Weapons = weapons;

            mockDice
                .Setup(d => d.Roll(2).d(4).AsSum<int>())
                .Returns(quantity);

            var count = 1;
            mockItemsGenerator
                .Setup(g => g.GenerateRandomAtLevelAsync(baseCreature.HitPoints.RoundedHitDiceQuantity))
                .ReturnsAsync(() => Enumerable.Range(1, count++)
                    .Select(i => new Item { Name = $"Item {count - 1}.{i}" }));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.True);
            Assert.That(creature.Equipment.Armor, Is.Null);
            Assert.That(creature.Equipment.Shield, Is.Null);
            Assert.That(creature.Equipment.Weapons, Is.EqualTo(weapons));

            var items = creature.Equipment.Items.ToArray();
            Assert.That(items, Has.Length.EqualTo(quantity));
            Assert.That(items[0].Name, Is.EqualTo("Item 1.1"));
            Assert.That(items[1].Name, Is.EqualTo("Item 2.1"));

            if (quantity > 2)
                Assert.That(items[2].Name, Is.EqualTo("Item 2.2"));

            if (quantity > 3)
                Assert.That(items[3].Name, Is.EqualTo("Item 3.1"));

            if (quantity > 4)
                Assert.That(items[4].Name, Is.EqualTo("Item 3.2"));

            if (quantity > 5)
                Assert.That(items[5].Name, Is.EqualTo("Item 3.3"));

            if (quantity > 6)
                Assert.That(items[6].Name, Is.EqualTo("Item 4.1"));

            if (quantity > 7)
                Assert.That(items[7].Name, Is.EqualTo("Item 4.2"));
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public async Task ApplyToAsync_CreatureEquipment_HasArmor_GainsGhostlyEquipment(int quantity)
        {
            baseCreature.CanUseEquipment = true;
            baseCreature.Equipment.Armor = new Armor { Name = "my armor" };
            baseCreature.Equipment.Items = new List<Item>();
            baseCreature.Equipment.Shield = null;
            baseCreature.Equipment.Weapons = new List<Weapon>();

            mockDice
                .Setup(d => d.Roll(2).d(4).AsSum<int>())
                .Returns(quantity);

            var count = 1;
            mockItemsGenerator
                .Setup(g => g.GenerateRandomAtLevelAsync(baseCreature.HitPoints.RoundedHitDiceQuantity))
                .ReturnsAsync(() => Enumerable.Range(1, count++)
                    .Select(i => new Item { Name = $"Item {count - 1}.{i}" }));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.True);
            Assert.That(creature.Equipment.Armor, Is.Not.Null);
            Assert.That(creature.Equipment.Armor.Name, Is.EqualTo("my armor"));
            Assert.That(creature.Equipment.Shield, Is.Null);
            Assert.That(creature.Equipment.Weapons, Is.Empty);

            var items = creature.Equipment.Items.ToArray();
            Assert.That(items, Has.Length.EqualTo(quantity));
            Assert.That(items[0].Name, Is.EqualTo("Item 1.1"));
            Assert.That(items[1].Name, Is.EqualTo("Item 2.1"));

            if (quantity > 2)
                Assert.That(items[2].Name, Is.EqualTo("Item 2.2"));

            if (quantity > 3)
                Assert.That(items[3].Name, Is.EqualTo("Item 3.1"));

            if (quantity > 4)
                Assert.That(items[4].Name, Is.EqualTo("Item 3.2"));

            if (quantity > 5)
                Assert.That(items[5].Name, Is.EqualTo("Item 3.3"));

            if (quantity > 6)
                Assert.That(items[6].Name, Is.EqualTo("Item 4.1"));

            if (quantity > 7)
                Assert.That(items[7].Name, Is.EqualTo("Item 4.2"));
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public async Task ApplyToAsync_CreatureEquipment_HasShield_GainsGhostlyEquipment(int quantity)
        {
            baseCreature.CanUseEquipment = true;
            baseCreature.Equipment.Armor = null;
            baseCreature.Equipment.Items = new List<Item>();
            baseCreature.Equipment.Shield = new Armor { Name = "my shield" };
            baseCreature.Equipment.Weapons = new List<Weapon>();

            mockDice
                .Setup(d => d.Roll(2).d(4).AsSum<int>())
                .Returns(quantity);

            var count = 1;
            mockItemsGenerator
                .Setup(g => g.GenerateRandomAtLevelAsync(baseCreature.HitPoints.RoundedHitDiceQuantity))
                .ReturnsAsync(() => Enumerable.Range(1, count++)
                    .Select(i => new Item { Name = $"Item {count - 1}.{i}" }));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.True);
            Assert.That(creature.Equipment.Armor, Is.Null);
            Assert.That(creature.Equipment.Shield, Is.Not.Null);
            Assert.That(creature.Equipment.Shield.Name, Is.EqualTo("my shield"));
            Assert.That(creature.Equipment.Weapons, Is.Empty);

            var items = creature.Equipment.Items.ToArray();
            Assert.That(items, Has.Length.EqualTo(quantity));
            Assert.That(items[0].Name, Is.EqualTo("Item 1.1"));
            Assert.That(items[1].Name, Is.EqualTo("Item 2.1"));

            if (quantity > 2)
                Assert.That(items[2].Name, Is.EqualTo("Item 2.2"));

            if (quantity > 3)
                Assert.That(items[3].Name, Is.EqualTo("Item 3.1"));

            if (quantity > 4)
                Assert.That(items[4].Name, Is.EqualTo("Item 3.2"));

            if (quantity > 5)
                Assert.That(items[5].Name, Is.EqualTo("Item 3.3"));

            if (quantity > 6)
                Assert.That(items[6].Name, Is.EqualTo("Item 4.1"));

            if (quantity > 7)
                Assert.That(items[7].Name, Is.EqualTo("Item 4.2"));
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public async Task ApplyToAsync_CreatureEquipment_HasItem_GainsGhostlyEquipment(int quantity)
        {
            var items = new[] { new Item { Name = "my item" } };
            baseCreature.CanUseEquipment = true;
            baseCreature.Equipment.Armor = null;
            baseCreature.Equipment.Items = items;
            baseCreature.Equipment.Shield = new Armor { Name = "my shield" };
            baseCreature.Equipment.Weapons = new List<Weapon>();

            mockDice
                .Setup(d => d.Roll(2).d(4).AsSum<int>())
                .Returns(quantity);

            var count = 1;
            mockItemsGenerator
                .Setup(g => g.GenerateRandomAtLevelAsync(baseCreature.HitPoints.RoundedHitDiceQuantity))
                .ReturnsAsync(() => Enumerable.Range(1, count++)
                    .Select(i => new Item { Name = $"Item {count - 1}.{i}" }));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.True);
            Assert.That(creature.Equipment.Armor, Is.Null);
            Assert.That(creature.Equipment.Shield, Is.Not.Null);
            Assert.That(creature.Equipment.Shield.Name, Is.EqualTo("my shield"));
            Assert.That(creature.Equipment.Weapons, Is.Empty);

            var newItems = creature.Equipment.Items.ToArray();
            Assert.That(newItems, Has.Length.EqualTo(quantity + 1));
            Assert.That(newItems[0].Name, Is.EqualTo("my item"));
            Assert.That(newItems[1].Name, Is.EqualTo("Item 1.1"));
            Assert.That(newItems[2].Name, Is.EqualTo("Item 2.1"));

            if (quantity > 2)
                Assert.That(newItems[3].Name, Is.EqualTo("Item 2.2"));

            if (quantity > 3)
                Assert.That(newItems[4].Name, Is.EqualTo("Item 3.1"));

            if (quantity > 4)
                Assert.That(newItems[5].Name, Is.EqualTo("Item 3.2"));

            if (quantity > 5)
                Assert.That(newItems[6].Name, Is.EqualTo("Item 3.3"));

            if (quantity > 6)
                Assert.That(newItems[7].Name, Is.EqualTo("Item 4.1"));

            if (quantity > 7)
                Assert.That(newItems[8].Name, Is.EqualTo("Item 4.2"));
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        public async Task ApplyToAsync_CreatureEquipment_HasItems_GainsGhostlyEquipment(int quantity)
        {
            var items = new[] { new Item { Name = "my item" }, new Item { Name = "my other item" } };
            baseCreature.CanUseEquipment = true;
            baseCreature.Equipment.Armor = null;
            baseCreature.Equipment.Items = items;
            baseCreature.Equipment.Shield = new Armor { Name = "my shield" };
            baseCreature.Equipment.Weapons = new List<Weapon>();

            mockDice
                .Setup(d => d.Roll(2).d(4).AsSum<int>())
                .Returns(quantity);

            var count = 1;
            mockItemsGenerator
                .Setup(g => g.GenerateRandomAtLevelAsync(baseCreature.HitPoints.RoundedHitDiceQuantity))
                .ReturnsAsync(() => Enumerable.Range(1, count++)
                    .Select(i => new Item { Name = $"Item {count - 1}.{i}" }));

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.CanUseEquipment, Is.True);
            Assert.That(creature.Equipment.Armor, Is.Null);
            Assert.That(creature.Equipment.Shield, Is.Not.Null);
            Assert.That(creature.Equipment.Shield.Name, Is.EqualTo("my shield"));
            Assert.That(creature.Equipment.Weapons, Is.Empty);

            var newItems = creature.Equipment.Items.ToArray();
            Assert.That(newItems, Has.Length.EqualTo(quantity + 2));
            Assert.That(newItems[0].Name, Is.EqualTo("my item"));
            Assert.That(newItems[1].Name, Is.EqualTo("my other item"));
            Assert.That(newItems[2].Name, Is.EqualTo("Item 1.1"));
            Assert.That(newItems[3].Name, Is.EqualTo("Item 2.1"));

            if (quantity > 2)
                Assert.That(newItems[4].Name, Is.EqualTo("Item 2.2"));

            if (quantity > 3)
                Assert.That(newItems[5].Name, Is.EqualTo("Item 3.1"));

            if (quantity > 4)
                Assert.That(newItems[6].Name, Is.EqualTo("Item 3.2"));

            if (quantity > 5)
                Assert.That(newItems[7].Name, Is.EqualTo("Item 3.3"));

            if (quantity > 6)
                Assert.That(newItems[8].Name, Is.EqualTo("Item 4.1"));

            if (quantity > 7)
                Assert.That(newItems[9].Name, Is.EqualTo("Item 4.2"));
        }

        [Test]
        public void ApplyTo_SetsTemplate()
        {
            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Ghost));
        }

        [Test]
        public async Task ApplyToAsync_SetsTemplate()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Ghost));
        }

        [Test]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

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
                .Returns(new[]
                {
                    new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -6 },
                });
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

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
            hitDice["my creature"] = 2;
            hitDice["my other creature"] = 2;
            hitDice["wrong creature 1"] = 2;
            hitDice["wrong creature 2"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_ReturnCompatibleCreatures_WithPresetAlignment()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2", "wrong creature 3" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "preset alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "preset alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "wrong alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "wrong alignment", "original alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "preset alignment"))
                .Returns(new[] { "wrong creature 4", "my other creature", "wrong creature 5", "my creature" });

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
                .Returns(new[]
                {
                    new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -6 },
                });
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

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

            var hitDice = new Dictionary<string, double>
            {
                ["my creature"] = 2,
                ["my other creature"] = 2,
                ["wrong creature 1"] = 2,
                ["wrong creature 2"] = 2,
                ["wrong creature 3"] = 2
            };

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "preset alignment"))
                .Returns(["my creature", "wrong creature 2", "my other creature", "wrong creature 1"]);

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EquivalentTo(new[] { "my creature", "my other creature" }));
        }

        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        public void GetCompatibleCreatures_WithChallengeRating_ReturnCompatibleCreatures(string original, string filter)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

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
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, It.IsAny<string>()))
                .Returns(adjustments);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(original, -1) };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(original, 1) };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 2;
            hitDice["my other creature"] = 2;
            hitDice["wrong creature 1"] = 2;
            hitDice["wrong creature 2"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { ChallengeRating = filter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));

            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 1"), Times.Never);
            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 2"), Times.Never);
        }

        [TestCase(CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal)]
        public void GetCompatibleCreatures_WithType_ReturnCompatibleCreatures(string filter)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

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
                .Returns(new[]
                {
                    new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -6 },
                });
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);

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
            hitDice["my creature"] = 2;
            hitDice["my other creature"] = 2;
            hitDice["wrong creature 1"] = 2;
            hitDice["wrong creature 2"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Type = filter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_WithType_ReturnCompatibleCreatures_FilterOutInvalidTypes()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

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
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, It.IsAny<string>()))
                .Returns(adjustments);

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
            hitDice["my creature"] = 2;
            hitDice["my other creature"] = 2;
            hitDice["wrong creature 1"] = 2;
            hitDice["wrong creature 2"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Type = "subtype 2" };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));
        }

        [Test]
        public void GetCompatibleCreatures_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2", "wrong creature 3" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

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
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, It.IsAny<string>()))
                .Returns(adjustments);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1_2nd };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 2;
            hitDice["my other creature"] = 2;
            hitDice["wrong creature 1"] = 2;
            hitDice["wrong creature 2"] = 2;
            hitDice["wrong creature 3"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR3 };

            var compatibleCreatures = applicator.GetCompatibleCreatures(creatures, false, filters);
            Assert.That(compatibleCreatures, Is.EqualTo(new[] { "my creature", "my other creature" }));

            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 1"), Times.Never);
            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 2"), Times.Never);
            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 3"), Times.Never);
        }

        [TestCase(CreatureConstants.Types.Aberration, true)]
        [TestCase(CreatureConstants.Types.Animal, true)]
        [TestCase(CreatureConstants.Types.Construct, false)]
        [TestCase(CreatureConstants.Types.Dragon, true)]
        [TestCase(CreatureConstants.Types.Elemental, false)]
        [TestCase(CreatureConstants.Types.Fey, false)]
        [TestCase(CreatureConstants.Types.Giant, true)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, true)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, true)]
        [TestCase(CreatureConstants.Types.Ooze, false)]
        [TestCase(CreatureConstants.Types.Outsider, false)]
        [TestCase(CreatureConstants.Types.Plant, true)]
        [TestCase(CreatureConstants.Types.Undead, false)]
        [TestCase(CreatureConstants.Types.Vermin, false)]
        public void IsCompatible_BasedOnCreatureType(string creatureType, bool compatible)
        {
            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { creatureType, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 2;

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

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [Test]
        public void IsCompatible_MustHaveCharisma()
        {
            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 2;

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
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures, Is.Empty);
        }

        [TestCase(-10, false)]
        [TestCase(-8, false)]
        [TestCase(-6, false)]
        [TestCase(-4, true)]
        [TestCase(-2, true)]
        [TestCase(0, true)]
        [TestCase(2, true)]
        [TestCase(4, true)]
        [TestCase(6, true)]
        [TestCase(8, true)]
        [TestCase(10, true)]
        [TestCase(42, true)]
        public void IsCompatible_MustHaveCharismaOfAtLeast6(int charismaAdjustment, bool compatible)
        {
            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 2;

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
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = charismaAdjustment },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(null, true)]
        [TestCase(CreatureConstants.Types.Undead, true)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase("subtype 1", true)]
        [TestCase("subtype 2", true)]
        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal, true)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, true)]
        [TestCase("wrong type", false)]
        public void IsCompatible_TypeMustMatch(string type, bool compatible)
        {
            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

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
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 0 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            var filters = new Filters { Type = type };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1, true)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR2, false)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR3, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        public void IsCompatible_ChallengeRatingMustMatch(string original, string challengeRating, bool compatible)
        {
            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 0 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

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

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR2, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(0.5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR2, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(1, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR0, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, true)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR0, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR1, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, true)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, false)]
        [TestCase(1, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1, true)]
        [TestCase(2, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR2, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(2, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(2, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(2, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        public void IsCompatible_ChallengeRatingMustMatch_HumanoidCharacter(double hitDiceQuantity, string original, string challengeRating, bool compatible)
        {
            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 0 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

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

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, true, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_3rd, false)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1, true)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR2, false)]
        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR3, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
        public void IsCompatible_ChallengeRatingMustMatch_NonHumanoidCharacter(string original, string challengeRating, bool compatible)
        {
            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 0 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Giant, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

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

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, true, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase("original alignment", "original alignment", true)]
        [TestCase("original alignment", "wrong alignment", false)]
        public void IsCompatible_AlignmentMustMatch(string alignmentFilter, string creatureAlignment, bool compatible)
        {
            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 0 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var types = new Dictionary<string, IEnumerable<string>>
            {
                ["my creature"] = [CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2"],
                [CreatureConstants.Human] = [CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

            var alignments = new Dictionary<string, IEnumerable<string>>
            {
                ["my creature" + GroupConstants.Exploded] = ["other alignment", creatureAlignment]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, alignmentFilter))
                .Returns((string a, string t, string f) => alignments.Where(kvp => kvp.Value.Contains(f)).Select(kvp => kvp.Key));

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "original alignment"))
                .Returns(["other creature", "my creature", "wrong creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "wrong alignment"))
                .Returns(["other creature", "wrong creature"]);

            var data = new Dictionary<string, CreatureDataSelection>
            {
                ["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 }
            };

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

            var filters = new Filters { Alignment = alignmentFilter };

            var compatibleCreatures = applicator.GetCompatibleCreatures(["my creature"], false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR3, "original alignment", true)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR3, "wrong alignment", false)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR1, "original alignment", false)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, ChallengeRatingConstants.CR1, "wrong alignment", false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR3, "original alignment", false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR3, "wrong alignment", false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1, "original alignment", false)]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR1, "wrong alignment", false)]
        public void IsCompatible_AllFiltersMustMatch(string type, string challengeRating, string alignment, bool compatible)
        {
            var adjustments = new[]
            {
                new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = 0 },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "my creature"))
                .Returns(adjustments);

            var alignments = new Dictionary<string, IEnumerable<string>>
            {
                ["my creature" + GroupConstants.Exploded] = ["other alignment", "original alignment"]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, alignment))
                .Returns((string a, string t, string f) => alignments.Where(kvp => kvp.Value.Contains(f)).Select(kvp => kvp.Key));

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "original alignment"))
                .Returns(["other creature", "my creature", "wrong creature"]);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "wrong alignment"))
                .Returns(["other creature", "wrong creature"]);

            var types = new Dictionary<string, IEnumerable<string>>
            {
                ["my creature"] = [CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2"],
                [CreatureConstants.Human] = [CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human]
            };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

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

            var filters = new Filters { Type = type, ChallengeRating = challengeRating, Alignment = alignment };

            var compatibleCreatures = applicator.GetCompatibleCreatures(new[] { "my creature" }, false, filters);
            Assert.That(compatibleCreatures.Any(), Is.EqualTo(compatible));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures(bool asCharacter)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

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
                .Returns(new[]
                {
                    new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -6 },
                });
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

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
            hitDice["my creature"] = 2;
            hitDice["my other creature"] = 2;
            hitDice["wrong creature 1"] = 2;
            hitDice["wrong creature 2"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

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
                .Setup(f => f.Build(It.Is<IEnumerable<string>>(cc => cc.IsEquivalentTo(new[] { "my creature", "my other creature" })), asCharacter))
                .Returns(prototypes);

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, asCharacter).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures_WithPresetAlignment()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2", "wrong creature 3" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "preset alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "preset alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "wrong alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "wrong alignment", "original alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureGroups, "preset alignment"))
                .Returns(new[] { "wrong creature 4", "my other creature", "wrong creature 5", "my creature" });

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
                .Returns(new[]
                {
                    new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -6 },
                });
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 3"))
                .Returns(adjustments);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

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

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 2;
            hitDice["my other creature"] = 2;
            hitDice["wrong creature 1"] = 2;
            hitDice["wrong creature 2"] = 2;
            hitDice["wrong creature 3"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, "preset alignment"))
                .Returns(new[] { "my creature", "wrong creature 2", "my other creature", "wrong creature 1" });

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

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        public void GetCompatiblePrototypes_FromNames_WithChallengeRating_ReturnCompatibleCreatures(string original, string filter)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

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
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, It.IsAny<string>()))
                .Returns(adjustments);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = original };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(original, -1) };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.IncreaseChallengeRating(original, 1) };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 2;
            hitDice["my other creature"] = 2;
            hitDice["wrong creature 1"] = 2;
            hitDice["wrong creature 2"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

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

            var filters = new Filters { ChallengeRating = filter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(filter));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(filter));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));

            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 1"), Times.Never);
            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 2"), Times.Never);
        }

        [TestCase(CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal)]
        public void GetCompatiblePrototypes_FromNames_WithType_ReturnCompatibleCreatures(string filter)
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 3" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);

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
                .Returns(new[]
                {
                    new TypeAndAmountSelection { Type = AbilityConstants.Strength, Amount = 9266 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Constitution, Amount = 90210 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Dexterity, Amount = 42 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Intelligence, Amount = 600 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Wisdom, Amount = 1337 },
                    new TypeAndAmountSelection { Type = AbilityConstants.Charisma, Amount = -6 },
                });
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, "wrong creature 2"))
                .Returns(adjustments);

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
            hitDice["my creature"] = 2;
            hitDice["my other creature"] = 2;
            hitDice["wrong creature 1"] = 2;
            hitDice["wrong creature 2"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

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

            var filters = new Filters { Type = filter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_WithType_ReturnCompatibleCreatures_FilterOutInvalidTypes()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

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
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, It.IsAny<string>()))
                .Returns(adjustments);

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
            hitDice["my creature"] = 2;
            hitDice["my other creature"] = 2;
            hitDice["wrong creature 1"] = 2;
            hitDice["wrong creature 2"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

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

            var filters = new Filters { Type = "subtype 2" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[] { "my creature", "wrong creature 1", "my other creature", "wrong creature 2", "wrong creature 3" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

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
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, It.IsAny<string>()))
                .Returns(adjustments);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1_2nd };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 2;
            hitDice["my other creature"] = 2;
            hitDice["wrong creature 1"] = 2;
            hitDice["wrong creature 2"] = 2;
            hitDice["wrong creature 3"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

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

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR3 };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));

            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 1"), Times.Never);
            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 2"), Times.Never);
            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 3"), Times.Never);
        }

        [Test]
        public void GetCompatiblePrototypes_FromNames_ReturnCompatibleCreatures_WithLevelAdjustment()
        {
            var creatures = new[] { "my creature", "my other creature" };

            var alignments = new Dictionary<string, IEnumerable<string>>();
            alignments["my creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["my other creature" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 1" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 2" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };
            alignments["wrong creature 3" + GroupConstants.Exploded] = new[] { "other alignment", "original alignment" };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups))
                .Returns(alignments);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => alignments[c]);

            var types = new Dictionary<string, IEnumerable<string>>();
            types["my creature"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" };
            types["my other creature"] = new[] { CreatureConstants.Types.Giant, "subtype 2" };
            types["wrong creature 1"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };
            types["wrong creature 2"] = new[] { CreatureConstants.Types.Undead, "subtype 1", "subtype 2" };
            types["wrong creature 3"] = new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3" };
            types[CreatureConstants.Human] = new[] { CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Human };

            mockCollectionSelector
                .Setup(s => s.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureTypes))
                .Returns(types);
            mockCollectionSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CreatureTypes, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types[c]);
            mockCollectionSelector
                .Setup(s => s.Explode(Config.Name, TableNameConstants.Collection.CreatureGroups, It.IsAny<string>()))
                .Returns((string a, string t, string c) => types.Where(kvp => kvp.Value.Contains(c)).Select(kvp => kvp.Key));

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
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AbilityAdjustments, It.IsAny<string>()))
                .Returns(adjustments);

            var data = new Dictionary<string, CreatureDataSelection>();
            data["my creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1, LevelAdjustment = 0 };
            data["my other creature"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2, LevelAdjustment = 2 };
            data["wrong creature 1"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1_2nd };
            data["wrong creature 2"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR2 };
            data["wrong creature 3"] = new CreatureDataSelection { ChallengeRating = ChallengeRatingConstants.CR1 };

            mockCreatureDataSelector
                .Setup(s => s.SelectAll())
                .Returns(data);
            mockCreatureDataSelector
                .Setup(s => s.SelectFor(It.IsAny<string>()))
                .Returns((string c) => data[c]);

            var hitDice = new Dictionary<string, double>();
            hitDice["my creature"] = 2;
            hitDice["my other creature"] = 3;
            hitDice["wrong creature 1"] = 2;
            hitDice["wrong creature 2"] = 2;
            hitDice["wrong creature 3"] = 2;

            mockAdjustmentSelector
                .Setup(s => s.SelectAllFrom<double>(TableNameConstants.Adjustments.HitDice))
                .Returns(hitDice);
            mockAdjustmentSelector
                .Setup(s => s.SelectFrom<double>(TableNameConstants.Adjustments.HitDice, It.IsAny<string>()))
                .Returns((string t, string c) => hitDice[c]);

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
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.Zero);
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(5));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(8255));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(106));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.EqualTo(7));
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(3));

            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 1"), Times.Never);
            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 2"), Times.Never);
            mockCreatureDataSelector.Verify(s => s.SelectFor("wrong creature 3"), Times.Never);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures(bool asCharacter)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, -6)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(2)
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, asCharacter).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_WithPresetAlignment()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithAlignments("other alignment", "preset alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithAlignments("other alignment", "wrong alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, -6)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithAlignments("preset alignment", "original alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithAlignments("wrong alignment", "original alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 3")
                    .WithAlignments("other alignment", "original alignment")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithHitDiceQuantity(2)
                    .Build(),
            };

            var filters = new Filters { Alignment = "preset alignment" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("preset alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        public void GetCompatiblePrototypes_FromPrototypes_WithChallengeRating_ReturnCompatibleCreatures(string original, string filter)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithChallengeRating(original)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithChallengeRating(ChallengeRatingConstants.IncreaseChallengeRating(original, -1))
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithChallengeRating(original)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithChallengeRating(ChallengeRatingConstants.IncreaseChallengeRating(original, 1))
                    .WithHitDiceQuantity(2)
                    .Build(),
            };

            var filters = new Filters { ChallengeRating = filter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(filter));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(filter));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [TestCase(CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal)]
        public void GetCompatiblePrototypes_FromPrototypes_WithType_ReturnCompatibleCreatures(string filter)
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, -6)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 3")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(2)
                    .Build(),
            };

            var filters = new Filters { Type = filter };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 3",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_WithType_ReturnCompatibleCreatures_FilterOutInvalidTypes()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithHitDiceQuantity(2)
                    .Build(),
            };

            var filters = new Filters { Type = "subtype 2" };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_WithTypeAndChallengeRating_ReturnCompatibleCreatures()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 1")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithChallengeRating(ChallengeRatingConstants.CR1_2nd)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 2")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Undead, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithChallengeRating(ChallengeRatingConstants.CR2)
                    .WithHitDiceQuantity(2)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("wrong creature 3")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 3")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(2)
                    .Build(),
            };

            var filters = new Filters { Type = "subtype 2", ChallengeRating = ChallengeRatingConstants.CR3 };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false, filters).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.Null);
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(2));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_WithLevelAdjustment()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(2)
                    .WithLevelAdjustment(0)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266)
                    .WithAbility(AbilityConstants.Constitution, 90210)
                    .WithAbility(AbilityConstants.Dexterity, 42)
                    .WithAbility(AbilityConstants.Intelligence, 600)
                    .WithAbility(AbilityConstants.Wisdom, 1337)
                    .WithAbility(AbilityConstants.Charisma, 1336)
                    .WithChallengeRating(ChallengeRatingConstants.CR2)
                    .WithHitDiceQuantity(3)
                    .WithLevelAdjustment(2)
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(5));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.EqualTo(7));
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(3));
        }

        [Test]
        public void GetCompatiblePrototypes_FromPrototypes_ReturnCompatibleCreatures_WithImprovedTemplateAdjustment()
        {
            var creatures = new[]
            {
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                    .WithAbility(AbilityConstants.Strength, 9266, 96)
                    .WithAbility(AbilityConstants.Constitution, 90210, 783)
                    .WithAbility(AbilityConstants.Dexterity, 42, 8245)
                    .WithAbility(AbilityConstants.Intelligence, 600, 69)
                    .WithAbility(AbilityConstants.Wisdom, 1337, 420)
                    .WithAbility(AbilityConstants.Charisma, 1336, 1)
                    .WithChallengeRating(ChallengeRatingConstants.CR1)
                    .WithHitDiceQuantity(2)
                    .WithLevelAdjustment(0)
                    .Build(),
                new CreaturePrototypeBuilder()
                    .WithTestValues()
                    .WithName("my other creature")
                    .WithAlignments("other alignment", "original alignment")
                    .WithCreatureType(CreatureConstants.Types.Giant, "subtype 2")
                    .WithAbility(AbilityConstants.Strength, -1, 2)
                    .WithAbility(AbilityConstants.Constitution, -2, -3)
                    .WithAbility(AbilityConstants.Dexterity, 3, -4)
                    .WithAbility(AbilityConstants.Intelligence, 4, 5)
                    .WithAbility(AbilityConstants.Wisdom, -5, 6)
                    .WithAbility(AbilityConstants.Charisma, 6, 7)
                    .WithChallengeRating(ChallengeRatingConstants.CR2)
                    .WithHitDiceQuantity(3)
                    .WithLevelAdjustment(2)
                    .Build(),
            };

            var compatibleCreatures = applicator.GetCompatiblePrototypes(creatures, false).ToArray();
            Assert.That(compatibleCreatures, Has.Length.EqualTo(2));

            Assert.That(compatibleCreatures[0].Name, Is.EqualTo("my creature"));
            Assert.That(compatibleCreatures[0].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[0].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[0].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Humanoid,
            }));
            Assert.That(compatibleCreatures[0].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore + 9266 + 96));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 1336 + 1 + 4));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 600 + 69));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore + 1337 + 420));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 42 + 8245));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[0].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[0].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[0].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(compatibleCreatures[0].LevelAdjustment, Is.EqualTo(5));
            Assert.That(compatibleCreatures[0].HitDiceQuantity, Is.EqualTo(2));

            Assert.That(compatibleCreatures[1].Name, Is.EqualTo("my other creature"));
            Assert.That(compatibleCreatures[1].Type, Is.Not.Null);
            Assert.That(compatibleCreatures[1].Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(compatibleCreatures[1].Type.SubTypes, Is.EqualTo(new[]
            {
                "subtype 2",
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Giant,
            }));
            Assert.That(compatibleCreatures[1].Abilities, Has.Count.EqualTo(6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(Ability.DefaultScore - 1 + 2));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Ability.DefaultScore + 6 + 7 + 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(Ability.DefaultScore + 4 + 5));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(Ability.DefaultScore - 5 + 6));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(Ability.DefaultScore + 3 - 4));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(0));
            Assert.That(compatibleCreatures[1].Alignments, Is.EqualTo(new[]
            {
                new Alignment("other alignment"),
                new Alignment("original alignment"),
            }));
            Assert.That(compatibleCreatures[1].CasterLevel, Is.Zero);
            Assert.That(compatibleCreatures[1].ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR4));
            Assert.That(compatibleCreatures[1].LevelAdjustment, Is.EqualTo(7));
            Assert.That(compatibleCreatures[1].HitDiceQuantity, Is.EqualTo(3));
        }
    }
}
