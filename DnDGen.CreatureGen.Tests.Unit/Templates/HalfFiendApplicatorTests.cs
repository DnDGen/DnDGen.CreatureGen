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
        private Mock<ISpeedsGenerator> mockSpeedsGenerator;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ISkillsGenerator> mockSkillsGenerator;
        private Mock<Dice> mockDice;
        private Mock<IMagicGenerator> mockMagicGenerator;
        private Mock<IDemographicsGenerator> mockDemographicsGenerator;

        private static readonly string[] AllAlignments =
            [
                AlignmentConstants.ChaoticEvil,
                AlignmentConstants.ChaoticGood,
                AlignmentConstants.ChaoticNeutral,
                AlignmentConstants.LawfulEvil,
                AlignmentConstants.LawfulGood,
                AlignmentConstants.LawfulNeutral,
                AlignmentConstants.NeutralEvil,
                AlignmentConstants.NeutralGood,
                AlignmentConstants.TrueNeutral,
            ];

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockSpeedsGenerator = new Mock<ISpeedsGenerator>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockSkillsGenerator = new Mock<ISkillsGenerator>();
            mockDice = new Mock<Dice>();
            mockMagicGenerator = new Mock<IMagicGenerator>();
            mockDemographicsGenerator = new Mock<IDemographicsGenerator>();

            applicator = new HalfFiendApplicator(
                mockCollectionSelector.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockSkillsGenerator.Object,
                mockDice.Object,
                mockMagicGenerator.Object,
                mockDemographicsGenerator.Object);

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .WithMinimumAbility(AbilityConstants.Intelligence, 6)
                .Build();

            var speeds = new Dictionary<string, Measurement>
            {
                [SpeedConstants.Fly] = new Measurement("furlongs")
            };
            speeds[SpeedConstants.Fly].Description = "the goodest";
            speeds[SpeedConstants.Fly].Value = 666;

            mockSpeedsGenerator
                .Setup(g => g.Generate(CreatureConstants.Templates.HalfFiend))
                .Returns(speeds);

            SetUpAttacks();

            mockDemographicsGenerator
                .Setup(s => s.UpdateByTemplate(baseCreature.Demographics, baseCreature.Name, CreatureConstants.Templates.HalfFiend, true, false))
                .Returns(baseCreature.Demographics);
        }

        private void SetUpAttacks(string gender = null) => SetUpAttacks(new Attack { Name = "Smite Good", IsSpecial = true }, gender);

        private void SetUpAttacks(Attack attack, string gender = null) => SetUpAttacks(
            [attack, new Attack { Name = "other attack" }, new Attack { Name = "Claw" }, new Attack { Name = "Bite" },],
            gender);

        private void SetUpAttacks(Attack[] attacks, string gender = null)
        {
            gender ??= baseCreature.Demographics.Gender;

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.HalfFiend,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity,
                    gender))
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
            message.AppendLine($"\tAbility Roll: {baseCreature.Abilities[AbilityConstants.Intelligence].FullScore}");

            var function = () => applicator.ApplyTo(baseCreature, false);
            Assert.That(function, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR2, AlignmentConstants.NeutralEvil, "Alignment filter 'Neutral Evil' is not valid for creature alignments")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR3, AlignmentConstants.LawfulEvil, "CR filter 3 does not match updated creature CR 2 (from CR 1)")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulEvil, "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulEvil, "",
            Ignore = "As Character doesn't affect already-generated creature compatiblity")]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
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
            message.AppendLine($"\tAbility Roll: {baseCreature.Abilities[AbilityConstants.Intelligence].FullScore}");

            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment
            };

            var function = () => applicator.ApplyTo(baseCreature, asCharacter, filters);
            Assert.That(function, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithOtherTemplate()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);
            baseCreature.Templates.Add("my other template");

            SetUpAttacks();

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.HalfFiend));
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);

            SetUpAttacks();

            var filters = new Filters
            {
                Type = "subtype 1",
                ChallengeRating = ChallengeRatingConstants.CR2,
                Alignment = AlignmentConstants.LawfulEvil
            };

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
            baseCreature.Type.SubTypes =
            [
                "subtype 1",
                "subtype 2",
            ];

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
        public void ApplyTo_DemographicsAdjusted()
        {
            var templateDemographics = new Demographics
            {
                Skin = "fiery",
                Gender = "hellish gender"
            };
            mockDemographicsGenerator
                .Setup(s => s.UpdateByTemplate(baseCreature.Demographics, baseCreature.Name, CreatureConstants.Templates.HalfFiend, true, false))
                .Returns(templateDemographics);

            SetUpAttacks(templateDemographics.Gender);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
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
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs")
            {
                Description = "so-so",
                Value = 42
            };

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
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs")
            {
                Description = "so-so",
                Value = 600
            };

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
            baseCreature.Speeds[SpeedConstants.Swim] = new Measurement("furlongs")
            {
                Value = 600
            };

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
            baseCreature.Speeds[SpeedConstants.Swim] = new Measurement("furlongs")
            {
                Value = 600
            };

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
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

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
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

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
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(2));

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
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks);

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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks);

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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = [92]
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = [-66]
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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks);

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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = [92],
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = [-66]
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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks);

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
                    new Damage { Roll = "base claw roll", Type = "base claw type" }
                ],
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union([claw]);

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
            Assert.That(claws[0].DamageSummary, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(newAttacks[4]));
            Assert.That(bites[0].DamageSummary, Is.EqualTo("fiend bite roll fiend bite type"));
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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks);

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
                    new Damage { Roll = "base bite roll", Type = "base bite type" }
                ],
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union([bite]);

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
            Assert.That(claws[0].DamageSummary, Is.EqualTo("fiend claw roll fiend claw type"));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageSummary, Is.EqualTo(expectedBiteDamage));
        }

        [TestCase(9266, 90210, "fiend claw roll fiend claw type", 1336, 1337, "fiend bite roll fiend bite type")]
        [TestCase(9266, 90210, "fiend claw roll fiend claw type", 1336, 600, "base bite roll base bite type")]
        [TestCase(9266, 42, "base claw roll base claw type", 1336, 1337, "fiend bite roll fiend bite type")]
        [TestCase(9266, 42, "base claw roll base claw type", 1336, 600, "base bite roll base bite type")]
        public void ApplyTo_GainAttacks_DuplicateClawAndBiteAttack(
            int baseClawMax,
            int fiendClawMax,
            string expectedClawDamage,
            int baseBiteMax,
            int fiendBiteMax,
            string expectedBiteDamage)
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack { Name = "Smite Good", IsSpecial = true },
                new Attack
                {
                    Name = "Claw",
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks);

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
                        new Damage { Roll = "base claw roll", Type = "base claw type" }
                    ],
                IsSpecial = false,
                IsMelee = true
            };
            var bite = new Attack
            {
                Name = "Bite",
                Damages =
                    [
                        new Damage { Roll = "base bite roll", Type = "base bite type" }
                    ],
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union([claw, bite]);

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
            Assert.That(claws[0].DamageSummary, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageSummary, Is.EqualTo(expectedBiteDamage));
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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks);

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
                    new Damage { Roll = "base claw roll", Type = "base claw type" }
                ],
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union([claw]);

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
            Assert.That(claws[0].DamageSummary, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(newAttacks[4]));
            Assert.That(bites[0].DamageSummary, Is.EqualTo("fiend bite roll fiend bite type"));
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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks);

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
                        new Damage { Roll = "base bite roll", Type = "base bite type" }
                    ],
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union([bite]);

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
            Assert.That(claws[0].DamageSummary, Is.EqualTo("fiend claw roll fiend claw type"));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageSummary, Is.EqualTo(expectedBiteDamage));
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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks);

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
                        new Damage { Roll = "base claw roll", Type = "base claw type" }
                    ],
                IsSpecial = false,
                IsMelee = true
            };
            var bite = new Attack
            {
                Name = "Bite",
                Damages =
                    [
                        new Damage { Roll = "base bite roll", Type = "base bite type" }
                    ],
                IsSpecial = false,
                IsMelee = true
            };
            baseCreature.Attacks = baseCreature.Attacks.Union([claw, bite]);

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
            Assert.That(claws[0].DamageSummary, Is.EqualTo(expectedClawDamage));
            Assert.That(bites, Has.Length.EqualTo(1));
            Assert.That(bites[0], Is.EqualTo(bite));
            Assert.That(bites[0].DamageSummary, Is.EqualTo(expectedBiteDamage));
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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks);

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

            Assert.That(newAttacks[2].DamageSummary, Is.EqualTo(smiteDamage.ToString()));
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
            baseCreature.Skills = [];
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
                .Returns([]);

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature.Skills, Is.Empty);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void ApplyTo_ChallengeRatingAdjusted(double hitDiceQuantity, string original, string adjusted)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDiceQuantity;
            baseCreature.ChallengeRating = original;

            SetUpAttacks();

            var creature = applicator.ApplyTo(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.HitPoints.HitDiceQuantity, Is.EqualTo(hitDiceQuantity));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        private static IEnumerable ChallengeRatingAdjustments
        {
            get
            {
                var hitDice = Enumerable.Range(1, 20)
                    .Select(Convert.ToDouble)
                    .Union([.1, .2, .3, .4, .5, .6, .7, .8, .9]);

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
            message.AppendLine($"\tAbility Roll: {baseCreature.Abilities[AbilityConstants.Intelligence].FullScore}");

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, false),
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
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
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
            message.AppendLine($"\tAbility Roll: {baseCreature.Abilities[AbilityConstants.Intelligence].FullScore}");

            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment
            };

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, asCharacter, filters),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithOtherTemplate()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);
            baseCreature.Templates.Add("my other template");

            SetUpAttacks();

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.HalfFiend));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment(AlignmentConstants.LawfulNeutral);

            SetUpAttacks();

            var filters = new Filters
            {
                Type = "subtype 1",
                ChallengeRating = ChallengeRatingConstants.CR2,
                Alignment = AlignmentConstants.LawfulEvil
            };

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
            baseCreature.Type.SubTypes =
            [
                "subtype 1",
                "subtype 2",
            ];

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
        public async Task ApplyToAsync_DemographicsAdjusted()
        {
            var templateDemographics = new Demographics
            {
                Skin = "fiery",
                Gender = "hellish gender"
            };
            mockDemographicsGenerator
                .Setup(s => s.UpdateByTemplate(baseCreature.Demographics, baseCreature.Name, CreatureConstants.Templates.HalfFiend, true, false))
                .Returns(templateDemographics);

            SetUpAttacks(templateDemographics.Gender);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
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
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs")
            {
                Description = "so-so",
                Value = 42
            };

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
            baseCreature.Speeds[SpeedConstants.Fly] = new Measurement("furlongs")
            {
                Description = "so-so",
                Value = 600
            };

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
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

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
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

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
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(2));

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
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks);

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
                    Damages =
                    [
                        new Damage { Roll = "fiend claw roll", Type = "fiend claw type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
                new Attack
                {
                    Name = "Bite",
                    Damages =
                    [
                        new Damage { Roll = "fiend bite roll", Type = "fiend bite type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            SetUpAttacks(newAttacks);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalAttacks.Length + 5));
            Assert.That(creature.Attacks.Select(a => a.Name), Is.SupersetOf(originalAttacks.Select(a => a.Name)));
            Assert.That(creature.Attacks, Contains.Item(newAttacks[2]));
            Assert.That(creature.SpecialAttacks.Count(), Is.EqualTo(originalSpecialAttacks.Length + 3));
            Assert.That(creature.SpecialAttacks, Contains.Item(newAttacks[2]));

            Assert.That(newAttacks[2].DamageSummary, Is.EqualTo(smiteDamage.ToString()));
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
            baseCreature.Skills = [];
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
                .Returns([]);

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature.Skills, Is.Empty);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public async Task ApplyToAsync_ChallengeRatingAdjusted(double hitDiceQuantity, string original, string adjusted)
        {
            baseCreature.HitPoints.HitDice[0].Quantity = hitDiceQuantity;
            baseCreature.ChallengeRating = original;

            SetUpAttacks();

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
                    Config.Name,
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
            baseCreature.Languages = [];

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
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
            baseCreature.Languages = baseCreature.Languages.Union(["Mordor"]);
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(["Hellsprach", "Mordor", "Profanity", "Latin"]);

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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(["Hellsprach", "Mordor", "Profanity", "Latin"]);

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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(["Hellsprach", "Mordor", "Profanity", "Latin"]);

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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(["Hellsprach", "Mordor", "Profanity", "Latin"]);

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

            baseCreature.Languages = baseCreature.Languages.Union(["Mordor", "Profanity", "Latin"]);
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(["Hellsprach", "Mordor", "Profanity", "Latin"]);

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
                    Config.Name,
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
            baseCreature.Languages = [];

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
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
            baseCreature.Languages = baseCreature.Languages.Union(["Mordor"]);
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(["Hellsprach", "Mordor", "Profanity", "Latin"]);

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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(["Hellsprach", "Mordor", "Profanity", "Latin"]);

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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(["Hellsprach", "Mordor", "Profanity", "Latin"]);

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
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(["Hellsprach", "Mordor", "Profanity", "Latin"]);

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

            baseCreature.Languages = baseCreature.Languages.Union(["Mordor", "Profanity", "Latin"]);
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Automatic))
                .Returns("Mordor");

            mockCollectionSelector
                .Setup(s => s.SelectFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.HalfFiend + LanguageConstants.Groups.Bonus))
                .Returns(["Hellsprach", "Mordor", "Profanity", "Latin"]);

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
        public void IsCompatible_ReturnsTrue()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .Build();

            var compatible = applicator.IsCompatible(creature);
            Assert.That(compatible, Is.True);
        }

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
        [TestCase(CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void IsCompatible_ReturnsFalse_IfIncorporeal(string creatureType)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(creatureType, "subtype 1", CreatureConstants.Types.Subtypes.Incorporeal, "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .Build();

            var compatible = applicator.IsCompatible(creature);
            Assert.That(compatible, Is.False);
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
        public void IsCompatible_ReturnsCompatibility_BasedOnCreatureType(string creatureType, bool expected)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(creatureType, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .Build();

            var compatible = applicator.IsCompatible(creature);
            Assert.That(compatible, Is.EqualTo(expected));
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
        public void IsCompatible_ReturnsCompatibility_MustHaveNonGoodAlignment(string alignment, bool expected)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(alignment, "other Good")
                .Build();

            var compatible = applicator.IsCompatible(creature);
            Assert.That(compatible, Is.EqualTo(expected));
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
        public void IsCompatible_ReturnsCompatibility_MustHaveAnyNonGoodAlignment(string alignment, bool expected)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments("other Good", alignment)
                .Build();

            var compatible = applicator.IsCompatible(creature);
            Assert.That(compatible, Is.EqualTo(expected));
        }

        [Test]
        public void IsCompatible_ReturnsFalse_WhenNoIntelligence()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithoutAbility(AbilityConstants.Intelligence)
                .Build();

            var compatible = applicator.IsCompatible(creature);
            Assert.That(compatible, Is.False);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void IsCompatible_ReturnsFalse_WhenIntelligenceTooLow(int baseScore)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAbility(AbilityConstants.Intelligence, baseScore - Ability.DefaultScore)
                .Build();

            var compatible = applicator.IsCompatible(creature);
            Assert.That(compatible, Is.False);
        }

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(18)]
        [TestCase(20)]
        [TestCase(100)]
        public void IsCompatible_ReturnsTrue_WhenIntelligenceMeetsMinimum(int baseScore)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAbility(AbilityConstants.Intelligence, baseScore - Ability.DefaultScore)
                .Build();

            var compatible = applicator.IsCompatible(creature);
            Assert.That(compatible, Is.True);
        }

        [TestCase(AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.TrueNeutral)]
        public void IsCompatible_WithAlignment_ReturnsFalse_WhenAlignmentFilterInvalid(string alignment)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AllAlignments)
                .Build();

            var filters = new Filters { Alignment = alignment };

            var compatible = applicator.IsCompatible(creature, filters);
            Assert.That(compatible, Is.False);
        }

        [TestCase(AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.NeutralEvil)]
        public void IsCompatible_WithAlignment_ReturnsTrue_WhenAlignmentFilterValid(string alignment)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AllAlignments)
                .Build();

            var filters = new Filters { Alignment = alignment };

            var compatible = applicator.IsCompatible(creature, filters);
            Assert.That(compatible, Is.True);
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
        public void IsCompatible_WithAlignment_ReturnsCompatibility_AdjustedAlignmentMustMatch(string alignmentFilter, string creatureAlignment, bool expected)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments("other Evil", creatureAlignment)
                .Build();

            var filters = new Filters { Alignment = alignmentFilter };

            var compatible = applicator.IsCompatible(creature, filters);
            Assert.That(compatible, Is.EqualTo(expected));
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
        [TestCase(5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1_2nd, false)]
        [TestCase(5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR1, false)]
        [TestCase(5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2, true)]
        [TestCase(5, ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR3, false)]
        [TestCase(5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR1, false)]
        [TestCase(5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR2, false)]
        [TestCase(5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3, true)]
        [TestCase(5, ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR4, false)]
        [TestCase(5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR2, false)]
        [TestCase(5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR3, false)]
        [TestCase(5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4, true)]
        [TestCase(5, ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR5, false)]
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
        public void IsCompatible_WithChallengeRating_ReturnsCompatibility_AdjustedChallengeRatingMustMatch(
            double hitDiceQuantity,
            string original,
            string challengeRating,
            bool expected)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .WithChallengeRating(original)
                .WithHitDiceQuantity(hitDiceQuantity)
                .Build();

            var filters = new Filters { ChallengeRating = challengeRating };

            var compatible = applicator.IsCompatible(creature, filters);
            Assert.That(compatible, Is.EqualTo(expected));
        }

        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Native)]
        public void IsCompatible_WithType_ReturnsTrue(string type)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .Build();

            var filters = new Filters { Type = type };

            var compatible = applicator.IsCompatible(creature, filters);
            Assert.That(compatible, Is.True);
        }

        [TestCase(CreatureConstants.Types.Humanoid, null, true)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Outsider, true)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Augmented, true)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Subtypes.Native, true)]
        [TestCase(CreatureConstants.Types.Humanoid, "subtype 1", true)]
        [TestCase(CreatureConstants.Types.Humanoid, "subtype 2", true)]
        [TestCase(CreatureConstants.Types.Humanoid, "wrong type", false)]
        [TestCase(CreatureConstants.Types.Animal, null, true)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.Animal, true)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.MagicalBeast, false)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.Outsider, true)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.Subtypes.Augmented, true)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.Subtypes.Native, true)]
        [TestCase(CreatureConstants.Types.Animal, "subtype 1", true)]
        [TestCase(CreatureConstants.Types.Animal, "subtype 2", true)]
        [TestCase(CreatureConstants.Types.Animal, "wrong type", false)]
        [TestCase(CreatureConstants.Types.Vermin, null, true)]
        [TestCase(CreatureConstants.Types.Vermin, CreatureConstants.Types.MagicalBeast, false)]
        [TestCase(CreatureConstants.Types.Vermin, CreatureConstants.Types.Outsider, true)]
        [TestCase(CreatureConstants.Types.Vermin, CreatureConstants.Types.Vermin, true)]
        [TestCase(CreatureConstants.Types.Vermin, CreatureConstants.Types.Subtypes.Augmented, true)]
        [TestCase(CreatureConstants.Types.Vermin, CreatureConstants.Types.Subtypes.Native, true)]
        [TestCase(CreatureConstants.Types.Vermin, "subtype 1", true)]
        [TestCase(CreatureConstants.Types.Vermin, "subtype 2", true)]
        [TestCase(CreatureConstants.Types.Vermin, "wrong type", false)]
        [TestCase(CreatureConstants.Types.MagicalBeast, null, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Animal, false)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.MagicalBeast, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Outsider, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Vermin, false)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Subtypes.Augmented, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Subtypes.Native, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, "subtype 1", true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, "subtype 2", true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, "wrong type", false)]
        public void IsCompatible_WithType_ReturnsCompatibility_AdjustedTypeMustMatch(string originalType, string filterType, bool expected)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(originalType, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .Build();

            var filters = new Filters { Type = filterType };

            var compatible = applicator.IsCompatible(creature, filters);
            Assert.That(compatible, Is.EqualTo(expected));
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsTrue()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Animal, "subtype 1", "subtype 2")
                .WithAlignments("wrong Good", AlignmentConstants.TrueNeutral, "other alignment")
                .WithChallengeRating(ChallengeRatingConstants.CR2)
                .WithHitDiceQuantity(8)
                .Build();

            var filters = new Filters
            {
                Alignment = AlignmentConstants.NeutralEvil,
                ChallengeRating = ChallengeRatingConstants.CR4,
                Type = CreatureConstants.Types.Animal,
            };

            var compatible = applicator.IsCompatible(creature, filters);
            Assert.That(compatible, Is.True);
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecausePrototype()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Animal, "subtype 1", CreatureConstants.Types.Subtypes.Incorporeal, "subtype 2")
                .WithAlignments("wrong Good", AlignmentConstants.TrueNeutral, "other alignment")
                .WithChallengeRating(ChallengeRatingConstants.CR2)
                .WithHitDiceQuantity(8)
                .Build();

            var filters = new Filters
            {
                Alignment = AlignmentConstants.NeutralEvil,
                ChallengeRating = ChallengeRatingConstants.CR4,
                Type = CreatureConstants.Types.Animal,
            };

            var compatible = applicator.IsCompatible(creature, filters);
            Assert.That(compatible, Is.False);
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseAlignment()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Animal, "subtype 1", "subtype 2")
                .WithAlignments("wrong Good", AlignmentConstants.TrueNeutral, "other alignment")
                .WithChallengeRating(ChallengeRatingConstants.CR2)
                .WithHitDiceQuantity(8)
                .Build();

            var filters = new Filters
            {
                Alignment = AlignmentConstants.ChaoticEvil,
                ChallengeRating = ChallengeRatingConstants.CR4,
                Type = CreatureConstants.Types.Animal,
            };

            var compatible = applicator.IsCompatible(creature, filters);
            Assert.That(compatible, Is.False);
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseChallengeRating()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Animal, "subtype 1", "subtype 2")
                .WithAlignments("wrong Good", AlignmentConstants.TrueNeutral, "other alignment")
                .WithChallengeRating(ChallengeRatingConstants.CR2)
                .WithHitDiceQuantity(8)
                .Build();

            var filters = new Filters
            {
                Alignment = AlignmentConstants.NeutralEvil,
                ChallengeRating = ChallengeRatingConstants.CR3,
                Type = CreatureConstants.Types.Animal,
            };

            var compatible = applicator.IsCompatible(creature, filters);
            Assert.That(compatible, Is.False);
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseType()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Animal, "subtype 1", "subtype 2")
                .WithAlignments("wrong Good", AlignmentConstants.TrueNeutral, "other alignment")
                .WithChallengeRating(ChallengeRatingConstants.CR2)
                .WithHitDiceQuantity(8)
                .Build();

            var filters = new Filters
            {
                Alignment = AlignmentConstants.NeutralEvil,
                ChallengeRating = ChallengeRatingConstants.CR4,
                Type = "wrong subtype",
            };

            var compatible = applicator.IsCompatible(creature, filters);
            Assert.That(compatible, Is.False);
        }

        [Test]
        public void ApplyTo_Prototype_ThrowsException_WhenCreatureNotCompatible()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Outsider, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .Build();

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine("\tReason: Type 'Outsider' is not valid");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {creature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.HalfFiend}");
            message.AppendLine($"\tAbility Roll: {creature.Abilities[AbilityConstants.Intelligence].FullScore}");

            var function = () => applicator.ApplyTo(creature);
            Assert.That(function, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR2, AlignmentConstants.NeutralEvil, "Alignment filter 'Neutral Evil' is not valid for creature alignments")]
        [TestCase(false, "subtype 1", ChallengeRatingConstants.CR3, AlignmentConstants.LawfulEvil, "CR filter 3 does not match updated creature CR 2 (from CR 1)")]
        [TestCase(false, "wrong subtype", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulEvil, "Type filter 'wrong subtype' is not valid")]
        [TestCase(true, "subtype 1", ChallengeRatingConstants.CR2, AlignmentConstants.LawfulEvil, "",
            Ignore = "As Character doesn't affect already-generated creature compatiblity")]
        public void ApplyTo_Prototype_ThrowsException_WhenCreatureNotCompatible_WithFilters(bool asCharacter, string type, string challengeRating, string alignment, string reason)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithHitDiceQuantity(1)
                .WithAlignments(AlignmentConstants.LawfulNeutral)
                .Build();

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {asCharacter}");
            message.AppendLine($"\tCreature: {creature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.HalfFiend}");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");
            message.AppendLine($"\tAbility Roll: {creature.Abilities[AbilityConstants.Intelligence].FullScore}");

            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment
            };

            var function = () => applicator.ApplyTo(creature, filters);
            Assert.That(function, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithCasterLevel(9266)
                .WithLevelAdjustment(90210)
                .WithHitDiceQuantity(9)
                .WithAbility(AbilityConstants.Strength, 96)
                .WithAbility(AbilityConstants.Constitution, 22)
                .WithAbility(AbilityConstants.Dexterity, 42)
                .WithAbility(AbilityConstants.Intelligence, 600)
                .WithAbility(AbilityConstants.Wisdom, 1337)
                .WithAbility(AbilityConstants.Charisma, 1336)
                .Build();

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Type, Is.Not.Null);
            Assert.That(updatedPrototype.Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(updatedPrototype.Type.SubTypes, Is.EquivalentTo(
            [
                "subtype 1",
                "subtype 2",
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Augmented
            ]));
            Assert.That(updatedPrototype.Abilities, Has.Count.EqualTo(6));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(96 + Ability.DefaultScore + 4));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1336 + Ability.DefaultScore + 2));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(600 + Ability.DefaultScore + 4));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1337 + Ability.DefaultScore + 0));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.EqualTo(0));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(42 + Ability.DefaultScore + 4));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(22 + Ability.DefaultScore + 2));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(updatedPrototype.Alignments, Is.EqualTo(
            [
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            ]));
            Assert.That(updatedPrototype.CasterLevel, Is.EqualTo(9266 + 0));
            Assert.That(updatedPrototype.ChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR3));
            Assert.That(updatedPrototype.LevelAdjustment, Is.EqualTo(90210 + 4));
            Assert.That(updatedPrototype.HitDiceQuantity, Is.EqualTo(9 + 0));
            Assert.That(updatedPrototype.Templates, Is.EqualTo([CreatureConstants.Templates.HalfFiend]));
        }

        [TestCase(AbilityConstants.Strength)]
        [TestCase(AbilityConstants.Constitution)]
        [TestCase(AbilityConstants.Dexterity)]
        [TestCase(AbilityConstants.Intelligence, IgnoreReason = "base creature is required to have Intelligence >= 4")]
        [TestCase(AbilityConstants.Wisdom)]
        [TestCase(AbilityConstants.Charisma)]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_MissingAbilityIsUnaltered(string ability)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithCasterLevel(9266)
                .WithLevelAdjustment(90210)
                .WithHitDiceQuantity(9)
                .WithoutAbility(ability)
                .Build();

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Abilities, Has.Count.EqualTo(6));
            Assert.That(updatedPrototype.Abilities[ability].FullScore, Is.Zero);
            Assert.That(updatedPrototype.Abilities[ability].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[ability].HasScore, Is.False);
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithImprovedTemplateAdjustments()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .WithAbility(AbilityConstants.Strength, 96, 783)
                .WithAbility(AbilityConstants.Constitution, 90210, 8245)
                .WithAbility(AbilityConstants.Dexterity, 42, 2022)
                .WithAbility(AbilityConstants.Intelligence, 600, 2015)
                .WithAbility(AbilityConstants.Wisdom, 1337, 9)
                .WithAbility(AbilityConstants.Charisma, 1336, 22)
                .Build();

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Abilities, Has.Count.EqualTo(6));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].FullScore, Is.EqualTo(96 + Ability.DefaultScore + 4 + 783));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.EqualTo(4 + 783));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(1336 + Ability.DefaultScore + 2 + 22));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(2 + 22));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].FullScore, Is.EqualTo(600 + Ability.DefaultScore + 4 + 2015));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(4 + 2015));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].FullScore, Is.EqualTo(1337 + Ability.DefaultScore + 0 + 9));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.EqualTo(0 + 9));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].FullScore, Is.EqualTo(42 + Ability.DefaultScore + 4 + 2022));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.EqualTo(4 + 2022));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].FullScore, Is.EqualTo(90210 + Ability.DefaultScore + 2 + 8245));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.EqualTo(2 + 8245));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_FilteringOutEvilAlignments()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other Good", "other alignment", AlignmentConstants.NeutralGood)
                .Build();

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Alignments, Is.EqualTo(
            [
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment("other Evil"),
            ]));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_PreserveAlignmentWeighting()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulNeutral, AlignmentConstants.NeutralEvil)
                .Build();

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Alignments, Is.EqualTo(
            [
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment(AlignmentConstants.LawfulEvil),
                new Alignment(AlignmentConstants.NeutralEvil),
            ]));
        }

        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.LawfulNeutral, AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.TrueNeutral, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.ChaoticNeutral, AlignmentConstants.ChaoticEvil)]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_AlignmentAdjusted(string creatureAlignment, string adjustedAlignment)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments("other Good", creatureAlignment)
                .Build();

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Alignments, Is.EqualTo([new Alignment(adjustedAlignment)]));
        }

        [Test]
        public void ApplyTo_PrototypeWithAlignment_ReturnsUpdatedPrototype()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "my alignment")
                .Build();

            var filters = new Filters { Alignment = "my Evil" };

            var updatedPrototype = applicator.ApplyTo(creature, filters);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Alignments, Is.EqualTo(
            [
                new Alignment("my Evil"),
                new Alignment("my Evil"),
            ]));
        }

        [Test]
        public void ApplyTo_PrototypeWithAlignment_ReturnsUpdatedPrototype_FilteringOutEvilAlignments()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other Good", "my alignment", AlignmentConstants.NeutralGood)
                .Build();

            var filters = new Filters { Alignment = "my Evil" };

            var updatedPrototype = applicator.ApplyTo(creature, filters);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Alignments, Is.EqualTo(
            [
                new Alignment("my Evil"),
                new Alignment("my Evil"),
            ]));
        }

        [Test]
        public void ApplyTo_PrototypeWithAlignment_ReturnsUpdatedPrototype_PreserveAlignmentWeighting()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "my alignment", "my alignment", AlignmentConstants.NeutralEvil)
                .Build();

            var filters = new Filters { Alignment = "my Evil" };

            var updatedPrototype = applicator.ApplyTo(creature, filters);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Alignments, Is.EqualTo(
            [
                new Alignment("my Evil"),
                new Alignment("my Evil"),
                new Alignment("my Evil"),
                new Alignment("my Evil"),
            ]));
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
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_ChallengeRatingAdjusted(double hitDiceQuantity, string original, string challengeRating)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .WithChallengeRating(original)
                .WithHitDiceQuantity(hitDiceQuantity)
                .Build();

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.ChallengeRating, Is.EqualTo(challengeRating));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_NoLevelAdjustment()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .WithLevelAdjustment(null)
                .Build();

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.LevelAdjustment, Is.Null);
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_TypeAdjusted(string originalType)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(originalType, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .Build();

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Type.Name, Is.EqualTo(CreatureConstants.Types.Outsider));
            Assert.That(updatedPrototype.Type.SubTypes,
                Is.EquivalentTo(["subtype 1", "subtype 2", CreatureConstants.Types.Subtypes.Augmented, CreatureConstants.Types.Subtypes.Native, originalType]));
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithAdditionalTemplates()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithAlignments(AlignmentConstants.LawfulEvil, "other alignment")
                .Build();
            creature.Templates.Add("my template");

            var updatedPrototype = applicator.ApplyTo(creature);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Templates, Is.EqualTo(["my template", CreatureConstants.Templates.HalfFiend]));
        }
    }
}
