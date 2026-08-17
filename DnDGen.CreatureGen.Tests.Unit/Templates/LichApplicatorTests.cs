using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Languages;
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
    public class LichApplicatorTests
    {
        private TemplateApplicator applicator;
        private Creature baseCreature;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<Dice> mockDice;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
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
            mockDice = new Mock<Dice>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockDemographicsGenerator = new Mock<IDemographicsGenerator>();

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .WithLevelAdjustment(0)
                .Build();

            applicator = new LichApplicator(
                mockCollectionSelector.Object,
                mockDice.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockDemographicsGenerator.Object);

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            mockDemographicsGenerator
                .Setup(s => s.UpdateByTemplate(baseCreature.Demographics, baseCreature.Name, CreatureConstants.Templates.Lich, false, false))
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
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Lich}");

            Assert.That((Func<object>)(() => applicator.ApplyTo(baseCreature, false)),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase("subtype 1", ChallengeRatingConstants.CR3, "original Neutral", "Alignment filter 'original Neutral' is not valid")]
        [TestCase("subtype 1", ChallengeRatingConstants.CR2, "original Evil", "CR filter 2 does not match updated creature CR 3 (from CR 1)")]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR3, "original Evil", "Type filter 'wrong subtype' is not valid")]
        public void ApplyTo_ThrowsException_WhenCreatureNotCompatible_WithFilters(string type, string challengeRating, string alignment, string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");
            baseCreature.CasterLevel = 11;

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Lich}");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment
            };

            var func = () => applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(func, Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithOtherTemplate()
        {
            baseCreature.Templates.Add("my other template");

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.Lich));
        }

        [Test]
        public void ApplyTo_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");
            baseCreature.CasterLevel = 11;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            var filters = new Filters
            {
                Type = "subtype 1",
                ChallengeRating = ChallengeRatingConstants.CR3,
                Alignment = "original Evil"
            };

            var creature = applicator.ApplyTo(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Lich));
        }

        [Test]
        public void ApplyTo_GainsCommon()
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.Lich + LanguageConstants.Groups.Automatic))
                .Returns("Spoopy");

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Spoopy"));
        }

        [Test]
        public void ApplyTo_GainsCommon_AlreadyKnows()
        {
            baseCreature.Languages = baseCreature.Languages.Union(["Spoopy"]);
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.Lich + LanguageConstants.Groups.Automatic))
                .Returns("Spoopy");

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Spoopy"));
        }

        [Test]
        public void ApplyTo_CreatureTypeChangesToUndead()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes =
            [
                "subtype 1",
                "subtype 2",
            ];

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(4));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains(CreatureConstants.Types.Subtypes.Augmented)
                .And.Contains(CreatureConstants.Types.Humanoid)
                .And.Contains("subtype 2"));
        }

        [Test]
        public void ApplyTo_DemographicsAdjusted()
        {
            var templateDemographics = new Demographics
            {
                Skin = "withered skin",
                Gender = "mean gender",
            };
            mockDemographicsGenerator
                .Setup(s => s.UpdateByTemplate(baseCreature.Demographics, baseCreature.Name, CreatureConstants.Templates.Lich, false, false))
                .Returns(templateDemographics);

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        public void ApplyTo_HitDiceChangeToD12AndReroll(int die)
        {
            baseCreature.HitPoints.HitDice[0].HitDie = die;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266, 90210]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(42);

            var creature = applicator.ApplyTo(baseCreature, true);
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
                .Returns([9266, 90210]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(42);

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [Test]
        public void ApplyTo_GainsNaturalArmor()
        {
            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(5));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(5));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void ApplyTo_ImprovesNaturalArmor(int oldValue)
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, oldValue);

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(5));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(2));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(oldValue));
            Assert.That(bonus.IsConditional, Is.False);

            bonus = creature.ArmorClass.NaturalArmorBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(5));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public void ApplyTo_UsesExistingNaturalArmor()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(9266));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(2));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9266));
            Assert.That(bonus.IsConditional, Is.False);

            bonus = creature.ArmorClass.NaturalArmorBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(5));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public void ApplyTo_GainAttacks()
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Touch",
                    Damages =
                    [
                        new Damage { Roll = "lich touch roll", Type = "lich touch type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Lich,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(newAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature, true);

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
                new Attack
                {
                    Name = "Touch",
                    Damages =
                    [
                        new Damage { Roll = "lich touch roll", Type = "lich touch type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Lich,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(newAttacks);

            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
            var newQualities = new[]
            {
                new Feat { Name = "lich quality 1" },
                new Feat { Name = "lich quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Lich,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Undead
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = baseCreature.Alignment.Lawfulness, Goodness = AlignmentConstants.Evil }))
                .Returns(newQualities);

            var attacksWithBonuses = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Touch",
                    Damages =
                    [
                        new Damage { Roll = "lich touch roll", Type = "lich touch type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = [92],
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
            var creature = applicator.ApplyTo(baseCreature, true);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + attacksWithBonuses.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(attacksWithBonuses));
        }

        [Test]
        public void ApplyTo_GainSpecialQualities()
        {
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
            var lichQualities = new[]
            {
                new Feat { Name = "lich quality 1" },
                new Feat { Name = "lich quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Lich,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Undead
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Evil && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(lichQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = applicator.ApplyTo(baseCreature, true);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + lichQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(lichQualities));
        }

        [Test]
        public void ApplyTo_CreatureGainsSpecialQualities_Undead()
        {
            var lichQualities = new[]
            {
                new Feat { Name = "lich quality 1" },
                new Feat { Name = "lich quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Lich,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Evil && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(lichQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = applicator.ApplyTo(baseCreature, true);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + lichQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(lichQualities));
        }

        [Test]
        public void ApplyTo_ModifyAbilities()
        {
            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(2));

            Assert.That(creature.Abilities[AbilityConstants.Constitution].HasScore, Is.False);
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

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature.Abilities[ability].HasScore, Is.False);
            Assert.That(creature.Abilities[ability].TemplateAdjustment, Is.Zero);
        }

        [Test]
        public void ApplyTo_GainsSkillBonuses()
        {
            var skills = new[]
            {
                new Skill("other skill 1", baseCreature.Abilities[AbilityConstants.Constitution], 42),
                new Skill(SkillConstants.Hide, baseCreature.Abilities[AbilityConstants.Dexterity], 42),
                new Skill("other skill 2", baseCreature.Abilities[AbilityConstants.Strength], 42),
                new Skill(SkillConstants.Listen, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 3", baseCreature.Abilities[AbilityConstants.Dexterity], 42),
                new Skill(SkillConstants.Search, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 4", baseCreature.Abilities[AbilityConstants.Intelligence], 42),
                new Skill(SkillConstants.Spot, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 5", baseCreature.Abilities[AbilityConstants.Intelligence], 42),
                new Skill(SkillConstants.MoveSilently, baseCreature.Abilities[AbilityConstants.Dexterity], 42),
                new Skill("other skill 6", baseCreature.Abilities[AbilityConstants.Intelligence], 42),
                new Skill(SkillConstants.SenseMotive, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 7", baseCreature.Abilities[AbilityConstants.Charisma], 42),
            };

            foreach (var skill in skills)
            {
                skill.AddBonus(600);
                skill.AddBonus(666, "conditional");
            }

            baseCreature.Skills = baseCreature.Skills.Union(skills);

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature.Skills, Is.SupersetOf(skills));
            Assert.That(skills[0].Name, Is.EqualTo("other skill 1"));
            Assert.That(skills[0].Bonus, Is.EqualTo(600));
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[1].Name, Is.EqualTo(SkillConstants.Hide));
            Assert.That(skills[1].Bonus, Is.EqualTo(608));
            Assert.That(skills[1].Bonuses.Count, Is.EqualTo(3));
            Assert.That(skills[2].Name, Is.EqualTo("other skill 2"));
            Assert.That(skills[2].Bonus, Is.EqualTo(600));
            Assert.That(skills[2].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[3].Name, Is.EqualTo(SkillConstants.Listen));
            Assert.That(skills[3].Bonus, Is.EqualTo(608));
            Assert.That(skills[3].Bonuses.Count, Is.EqualTo(3));
            Assert.That(skills[4].Name, Is.EqualTo("other skill 3"));
            Assert.That(skills[4].Bonus, Is.EqualTo(600));
            Assert.That(skills[4].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[5].Name, Is.EqualTo(SkillConstants.Search));
            Assert.That(skills[5].Bonus, Is.EqualTo(608));
            Assert.That(skills[5].Bonuses.Count, Is.EqualTo(3));
            Assert.That(skills[6].Name, Is.EqualTo("other skill 4"));
            Assert.That(skills[6].Bonus, Is.EqualTo(600));
            Assert.That(skills[6].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[7].Name, Is.EqualTo(SkillConstants.Spot));
            Assert.That(skills[7].Bonus, Is.EqualTo(608));
            Assert.That(skills[7].Bonuses.Count, Is.EqualTo(3));
            Assert.That(skills[8].Name, Is.EqualTo("other skill 5"));
            Assert.That(skills[8].Bonus, Is.EqualTo(600));
            Assert.That(skills[8].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[9].Name, Is.EqualTo(SkillConstants.MoveSilently));
            Assert.That(skills[9].Bonus, Is.EqualTo(608));
            Assert.That(skills[9].Bonuses.Count, Is.EqualTo(3));
            Assert.That(skills[10].Name, Is.EqualTo("other skill 6"));
            Assert.That(skills[10].Bonus, Is.EqualTo(600));
            Assert.That(skills[10].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[11].Name, Is.EqualTo(SkillConstants.SenseMotive));
            Assert.That(skills[11].Bonus, Is.EqualTo(608));
            Assert.That(skills[11].Bonuses.Count, Is.EqualTo(3));
            Assert.That(skills[12].Name, Is.EqualTo("other skill 7"));
            Assert.That(skills[12].Bonus, Is.EqualTo(600));
            Assert.That(skills[12].Bonuses.Count, Is.EqualTo(2));
        }

        [Test]
        public void ApplyTo_CreatureSkills_GainRacialBonuses_NoSkills()
        {
            baseCreature.Skills = [];

            var creature = applicator.ApplyTo(baseCreature, true);
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

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature.Skills, Is.SupersetOf(skills));
            Assert.That(skills[0].Name, Is.EqualTo("other skill 1"));
            Assert.That(skills[0].Bonus, Is.EqualTo(600));
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[1].Name, Is.EqualTo("other skill 2"));
            Assert.That(skills[1].Bonus, Is.EqualTo(600));
            Assert.That(skills[1].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[2].Name, Is.EqualTo("other skill 3"));
            Assert.That(skills[2].Bonus, Is.EqualTo(600));
            Assert.That(skills[2].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[3].Name, Is.EqualTo("other skill 4"));
            Assert.That(skills[3].Bonus, Is.EqualTo(600));
            Assert.That(skills[3].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[4].Name, Is.EqualTo("other skill 5"));
            Assert.That(skills[4].Bonus, Is.EqualTo(600));
            Assert.That(skills[4].Bonuses.Count, Is.EqualTo(2));
        }

        [Test]
        public void ApplyTo_SwapConsitutionForCharismaForConcentration()
        {
            var concentration = new Skill(SkillConstants.Concentration, baseCreature.Abilities[AbilityConstants.Constitution], 42);
            baseCreature.Skills = baseCreature.Skills.Union([concentration]);

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature.Skills, Contains.Item(concentration));
            Assert.That(concentration.BaseAbility, Is.EqualTo(creature.Abilities[AbilityConstants.Charisma]));
        }

        [Test]
        public void ApplyTo_CreatureSkills_DoNotHaveFortitudeSave()
        {
            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature.Saves[SaveConstants.Fortitude].HasSave, Is.False);
            Assert.That(creature.Saves[SaveConstants.Reflex].HasSave, Is.True);
            Assert.That(creature.Saves[SaveConstants.Will].HasSave, Is.True);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void ApplyTo_ImproveChallengeRating(string original, string adjusted)
        {
            baseCreature.ChallengeRating = original;

            var creature = applicator.ApplyTo(baseCreature, true);
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

        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Good, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Neutral, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Evil, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Good, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Neutral, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Evil, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Good, AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Neutral, AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Evil, AlignmentConstants.LawfulEvil)]
        public void ApplyTo_AlignmentAdjusted(string lawfulness, string goodness, string adjusted)
        {
            baseCreature.Alignment.Lawfulness = lawfulness;
            baseCreature.Alignment.Goodness = goodness;

            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo(adjusted));
        }

        [Test]
        public void ApplyTo_GetPresetAlignment()
        {
            baseCreature.Alignment.Lawfulness = "preset";
            baseCreature.Alignment.Goodness = "alignment";

            var filters = new Filters { Alignment = "preset Evil" };

            var creature = applicator.ApplyTo(baseCreature, true, filters);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo("preset Evil"));
        }

        [TestCase(null, null)]
        [TestCase(0, 4)]
        [TestCase(1, 5)]
        [TestCase(2, 6)]
        [TestCase(10, 14)]
        [TestCase(20, 24)]
        [TestCase(42, 46)]
        public void ApplyTo_ImproveLevelAdjustment(int? original, int? adjusted)
        {
            baseCreature.LevelAdjustment = original;
            baseCreature.CasterLevel = 11;

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
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Lich}");

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, false),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [TestCase("subtype 1", ChallengeRatingConstants.CR3, "original Neutral", "Alignment filter 'original Neutral' is not valid")]
        [TestCase("subtype 1", ChallengeRatingConstants.CR2, "original Evil", "CR filter 2 does not match updated creature CR 3 (from CR 1)")]
        [TestCase("wrong subtype", ChallengeRatingConstants.CR3, "original Evil", "Type filter 'wrong subtype' is not valid")]
        public async Task ApplyToAsync_ThrowsException_WhenCreatureNotCompatible_WithFilters(
            string type,
            string challengeRating,
            string alignment,
            string reason)
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");
            baseCreature.CasterLevel = 11;

            var message = new StringBuilder();
            message.AppendLine("Invalid creature:");
            message.AppendLine($"\tReason: {reason}");
            message.AppendLine($"\tAs Character: {false}");
            message.AppendLine($"\tCreature: {baseCreature.Name}");
            message.AppendLine($"\tTemplate: {CreatureConstants.Templates.Lich}");
            message.AppendLine($"\tType: {type}");
            message.AppendLine($"\tCR: {challengeRating}");
            message.AppendLine($"\tAlignment: {alignment}");

            var filters = new Filters
            {
                Type = type,
                ChallengeRating = challengeRating,
                Alignment = alignment
            };

            await Assert.ThatAsync(async () => await applicator.ApplyToAsync(baseCreature, false, filters),
                Throws.InstanceOf<InvalidCreatureException>().With.Message.EqualTo(message.ToString()));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithOtherTemplate()
        {
            baseCreature.Templates.Add("my other template");

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature.Templates, Has.Count.EqualTo(2));
            Assert.That(creature.Templates[0], Is.EqualTo("my other template"));
            Assert.That(creature.Templates[1], Is.EqualTo(CreatureConstants.Templates.Lich));
        }

        [Test]
        public async Task ApplyToAsync_ReturnsCreature_WithFilters()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = ["subtype 1", "subtype 2"];
            baseCreature.HitPoints.HitDice[0].Quantity = 1;
            baseCreature.ChallengeRating = ChallengeRatingConstants.CR1;
            baseCreature.Alignment = new Alignment("original alignment");
            baseCreature.CasterLevel = 11;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(90210);

            var filters = new Filters
            {
                Type = "subtype 1",
                ChallengeRating = ChallengeRatingConstants.CR3,
                Alignment = "original Evil"
            };

            var creature = await applicator.ApplyToAsync(baseCreature, false, filters);
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Lich));
        }

        [Test]
        public async Task ApplyToAsync_GainsCommon()
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.Lich + LanguageConstants.Groups.Automatic))
                .Returns("Spoopy");

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Spoopy"));
        }

        [Test]
        public async Task ApplyToAsync_GainsCommon_AlreadyKnows()
        {
            baseCreature.Languages = baseCreature.Languages.Union(["Spoopy"]);
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    Config.Name,
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.Lich + LanguageConstants.Groups.Automatic))
                .Returns("Spoopy");

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Spoopy"));
        }

        [Test]
        public async Task ApplyToAsync_CreatureTypeChangesToUndead()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes =
            [
                "subtype 1",
                "subtype 2",
            ];

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(4));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(CreatureConstants.Types.Humanoid)
                .And.Contains(CreatureConstants.Types.Subtypes.Augmented));
        }

        [Test]
        public async Task ApplyToAsync_DemographicsAdjusted()
        {
            var templateDemographics = new Demographics
            {
                Skin = "withered skin",
                Gender = "mean gender",
            };
            mockDemographicsGenerator
                .Setup(s => s.UpdateByTemplate(baseCreature.Demographics, baseCreature.Name, CreatureConstants.Templates.Lich, false, false))
                .Returns(templateDemographics);

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Demographics, Is.EqualTo(templateDemographics));
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        [TestCase(12)]
        public async Task ApplyToAsync_HitDiceChangeToD12AndReroll(int die)
        {
            baseCreature.HitPoints.HitDice[0].HitDie = die;

            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsIndividualRolls<int>())
                .Returns([9266, 90210]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(42);

            var creature = await applicator.ApplyToAsync(baseCreature, true);
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
                .Returns([9266, 90210]);
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(42);

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [Test]
        public async Task ApplyToAsync_GainsNaturalArmor()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(5));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(1));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(5));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public async Task ApplyToAsync_ImprovesNaturalArmor(int oldValue)
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, oldValue);

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(5));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(2));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(oldValue));
            Assert.That(bonus.IsConditional, Is.False);

            bonus = creature.ArmorClass.NaturalArmorBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(5));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public async Task ApplyToAsync_UsesExistingNaturalArmor()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 9266);

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(9266));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count, Is.EqualTo(2));

            var bonus = creature.ArmorClass.NaturalArmorBonuses.First();
            Assert.That(bonus.Value, Is.EqualTo(9266));
            Assert.That(bonus.IsConditional, Is.False);

            bonus = creature.ArmorClass.NaturalArmorBonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(5));
            Assert.That(bonus.IsConditional, Is.False);
        }

        [Test]
        public async Task ApplyToAsync_GainAttacks()
        {
            var newAttacks = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Touch",
                    Damages =
                    [
                        new Damage { Roll = "lich touch roll", Type = "lich touch type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Lich,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(newAttacks);

            mockAttacksGenerator
                .Setup(g => g.ApplyAttackBonuses(
                    newAttacks,
                    It.IsAny<IEnumerable<Feat>>(),
                    baseCreature.Abilities))
                .Returns((IEnumerable<Attack> a, IEnumerable<Feat> f, Dictionary<string, Ability> ab) => a);

            var originalCount = baseCreature.Attacks.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, true);

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
                new Attack
                {
                    Name = "Touch",
                    Damages =
                    [
                        new Damage { Roll = "lich touch roll", Type = "lich touch type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Lich,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity, baseCreature.Demographics.Gender))
                .Returns(newAttacks);

            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
            var newQualities = new[]
            {
                new Feat { Name = "lich quality 1" },
                new Feat { Name = "lich quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Lich,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Undead
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    new Alignment { Lawfulness = baseCreature.Alignment.Lawfulness, Goodness = AlignmentConstants.Evil }))
                .Returns(newQualities);

            var attacksWithBonuses = new[]
            {
                new Attack { Name = "special attack 1", IsSpecial = true },
                new Attack { Name = "special attack 2", IsSpecial = true },
                new Attack
                {
                    Name = "Touch",
                    Damages =
                    [
                        new Damage { Roll = "lich touch roll", Type = "lich touch type" }
                    ],
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = [92],
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
            var creature = await applicator.ApplyToAsync(baseCreature, true);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + attacksWithBonuses.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(attacksWithBonuses));
        }

        [Test]
        public async Task ApplyToAsync_GainSpecialQualities()
        {
            var originalSubtypes = baseCreature.Type.SubTypes.ToArray();
            var lichQualities = new[]
            {
                new Feat { Name = "lich quality 1" },
                new Feat { Name = "lich quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Lich,
                    It.Is<CreatureType>(ct => ct.Name == CreatureConstants.Types.Undead
                        && ct.SubTypes.IsEquivalentTo(originalSubtypes.Union(new[]
                        {
                            CreatureConstants.Types.Subtypes.Augmented,
                            CreatureConstants.Types.Humanoid,
                        }))),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Evil && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(lichQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, true);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + lichQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(lichQualities));
        }

        [Test]
        public async Task ApplyToAsync_CreatureGainsSpecialQualities_Undead()
        {
            var lichQualities = new[]
            {
                new Feat { Name = "lich quality 1" },
                new Feat { Name = "lich quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Lich,
                    It.Is<CreatureType>(t => t.Name == CreatureConstants.Types.Undead),
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    It.Is<Alignment>(a => a.Goodness == AlignmentConstants.Evil && a.Lawfulness == baseCreature.Alignment.Lawfulness)))
                .Returns(lichQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = await applicator.ApplyToAsync(baseCreature, true);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + lichQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(lichQualities));
        }

        [Test]
        public async Task ApplyToAsync_ModifyAbilities()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Strength].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Dexterity].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateScore, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].TemplateAdjustment, Is.Zero);
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Intelligence].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Wisdom].TemplateAdjustment, Is.EqualTo(2));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateScore, Is.EqualTo(-1));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(2));

            Assert.That(creature.Abilities[AbilityConstants.Constitution].HasScore, Is.False);
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

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature.Abilities[ability].HasScore, Is.False);
            Assert.That(creature.Abilities[ability].TemplateAdjustment, Is.Zero);
        }

        [Test]
        public async Task ApplyToAsync_GainsSkillBonuses()
        {
            var skills = new[]
            {
                new Skill("other skill 1", baseCreature.Abilities[AbilityConstants.Constitution], 42),
                new Skill(SkillConstants.Hide, baseCreature.Abilities[AbilityConstants.Dexterity], 42),
                new Skill("other skill 2", baseCreature.Abilities[AbilityConstants.Strength], 42),
                new Skill(SkillConstants.Listen, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 3", baseCreature.Abilities[AbilityConstants.Dexterity], 42),
                new Skill(SkillConstants.Search, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 4", baseCreature.Abilities[AbilityConstants.Intelligence], 42),
                new Skill(SkillConstants.Spot, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 5", baseCreature.Abilities[AbilityConstants.Intelligence], 42),
                new Skill(SkillConstants.MoveSilently, baseCreature.Abilities[AbilityConstants.Dexterity], 42),
                new Skill("other skill 6", baseCreature.Abilities[AbilityConstants.Intelligence], 42),
                new Skill(SkillConstants.SenseMotive, baseCreature.Abilities[AbilityConstants.Wisdom], 42),
                new Skill("other skill 7", baseCreature.Abilities[AbilityConstants.Charisma], 42),
            };

            foreach (var skill in skills)
            {
                skill.AddBonus(600);
                skill.AddBonus(666, "conditional");
            }

            baseCreature.Skills = baseCreature.Skills.Union(skills);

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature.Skills, Is.SupersetOf(skills));
            Assert.That(skills[0].Name, Is.EqualTo("other skill 1"));
            Assert.That(skills[0].Bonus, Is.EqualTo(600));
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[1].Name, Is.EqualTo(SkillConstants.Hide));
            Assert.That(skills[1].Bonus, Is.EqualTo(608));
            Assert.That(skills[1].Bonuses.Count, Is.EqualTo(3));
            Assert.That(skills[2].Name, Is.EqualTo("other skill 2"));
            Assert.That(skills[2].Bonus, Is.EqualTo(600));
            Assert.That(skills[2].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[3].Name, Is.EqualTo(SkillConstants.Listen));
            Assert.That(skills[3].Bonus, Is.EqualTo(608));
            Assert.That(skills[3].Bonuses.Count, Is.EqualTo(3));
            Assert.That(skills[4].Name, Is.EqualTo("other skill 3"));
            Assert.That(skills[4].Bonus, Is.EqualTo(600));
            Assert.That(skills[4].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[5].Name, Is.EqualTo(SkillConstants.Search));
            Assert.That(skills[5].Bonus, Is.EqualTo(608));
            Assert.That(skills[5].Bonuses.Count, Is.EqualTo(3));
            Assert.That(skills[6].Name, Is.EqualTo("other skill 4"));
            Assert.That(skills[6].Bonus, Is.EqualTo(600));
            Assert.That(skills[6].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[7].Name, Is.EqualTo(SkillConstants.Spot));
            Assert.That(skills[7].Bonus, Is.EqualTo(608));
            Assert.That(skills[7].Bonuses.Count, Is.EqualTo(3));
            Assert.That(skills[8].Name, Is.EqualTo("other skill 5"));
            Assert.That(skills[8].Bonus, Is.EqualTo(600));
            Assert.That(skills[8].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[9].Name, Is.EqualTo(SkillConstants.MoveSilently));
            Assert.That(skills[9].Bonus, Is.EqualTo(608));
            Assert.That(skills[9].Bonuses.Count, Is.EqualTo(3));
            Assert.That(skills[10].Name, Is.EqualTo("other skill 6"));
            Assert.That(skills[10].Bonus, Is.EqualTo(600));
            Assert.That(skills[10].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[11].Name, Is.EqualTo(SkillConstants.SenseMotive));
            Assert.That(skills[11].Bonus, Is.EqualTo(608));
            Assert.That(skills[11].Bonuses.Count, Is.EqualTo(3));
            Assert.That(skills[12].Name, Is.EqualTo("other skill 7"));
            Assert.That(skills[12].Bonus, Is.EqualTo(600));
            Assert.That(skills[12].Bonuses.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task ApplyToAsync_CreatureSkills_GainRacialBonuses_NoSkills()
        {
            baseCreature.Skills = [];

            var creature = await applicator.ApplyToAsync(baseCreature, true);
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

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature.Skills, Is.SupersetOf(skills));
            Assert.That(skills[0].Name, Is.EqualTo("other skill 1"));
            Assert.That(skills[0].Bonus, Is.EqualTo(600));
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[1].Name, Is.EqualTo("other skill 2"));
            Assert.That(skills[1].Bonus, Is.EqualTo(600));
            Assert.That(skills[1].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[2].Name, Is.EqualTo("other skill 3"));
            Assert.That(skills[2].Bonus, Is.EqualTo(600));
            Assert.That(skills[2].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[3].Name, Is.EqualTo("other skill 4"));
            Assert.That(skills[3].Bonus, Is.EqualTo(600));
            Assert.That(skills[3].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[4].Name, Is.EqualTo("other skill 5"));
            Assert.That(skills[4].Bonus, Is.EqualTo(600));
            Assert.That(skills[4].Bonuses.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task ApplyToAsync_SwapConsitutionForCharismaForConcentration()
        {
            var concentration = new Skill(SkillConstants.Concentration, baseCreature.Abilities[AbilityConstants.Constitution], 42);
            baseCreature.Skills = baseCreature.Skills.Union([concentration]);

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature.Skills, Contains.Item(concentration));
            Assert.That(concentration.BaseAbility, Is.EqualTo(creature.Abilities[AbilityConstants.Charisma]));
        }

        [Test]
        public async Task ApplyToAsync_CreatureSkills_DoNotHaveFortitudeSave()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature.Saves[SaveConstants.Fortitude].HasSave, Is.False);
            Assert.That(creature.Saves[SaveConstants.Reflex].HasSave, Is.True);
            Assert.That(creature.Saves[SaveConstants.Will].HasSave, Is.True);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public async Task ApplyToAsync_ImproveChallengeRating(string original, string adjusted)
        {
            baseCreature.ChallengeRating = original;

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.ChallengeRating, Is.EqualTo(adjusted));
        }

        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Good, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Neutral, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Chaotic, AlignmentConstants.Evil, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Good, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Neutral, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Neutral, AlignmentConstants.Evil, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Good, AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Neutral, AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.Lawful, AlignmentConstants.Evil, AlignmentConstants.LawfulEvil)]
        public async Task ApplyToAsync_AlignmentAdjusted(string lawfulness, string goodness, string adjusted)
        {
            baseCreature.Alignment.Lawfulness = lawfulness;
            baseCreature.Alignment.Goodness = goodness;

            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo(adjusted));
        }

        [Test]
        public async Task ApplyToAsync_GetPresetAlignment()
        {
            baseCreature.Alignment.Lawfulness = "preset";
            baseCreature.Alignment.Goodness = "alignment";

            var filters = new Filters { Alignment = "preset Evil" };

            var creature = await applicator.ApplyToAsync(baseCreature, true, filters);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo("preset Evil"));
        }

        [TestCase(null, null)]
        [TestCase(0, 4)]
        [TestCase(1, 5)]
        [TestCase(2, 6)]
        [TestCase(10, 14)]
        [TestCase(20, 24)]
        [TestCase(42, 46)]
        public async Task ApplyToAsync_ImproveLevelAdjustment(int? original, int? adjusted)
        {
            baseCreature.LevelAdjustment = original;
            baseCreature.CasterLevel = 11;

            var creature = await applicator.ApplyToAsync(baseCreature, false);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [Test]
        public void ApplyTo_SetsTemplate()
        {
            var creature = applicator.ApplyTo(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Lich));
        }

        [Test]
        public async Task ApplyToAsync_SetsTemplate()
        {
            var creature = await applicator.ApplyToAsync(baseCreature, true);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Templates.Single(), Is.EqualTo(CreatureConstants.Templates.Lich));
        }

        [Test]
        public void IsCompatible_ReturnsTrue()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithCasterLevel(11)
                .Build();

            var compatible = applicator.IsCompatible(creature, false);
            Assert.That(compatible, Is.True);
        }

        [Test]
        public void IsCompatible_AsCharacter_ReturnsTrue()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithLevelAdjustment(0)
                .Build();

            var compatible = applicator.IsCompatible(creature, true);
            Assert.That(compatible, Is.True);
        }

        [TestCase(CreatureConstants.Types.Aberration, false)]
        [TestCase(CreatureConstants.Types.Animal, false)]
        [TestCase(CreatureConstants.Types.Construct, false)]
        [TestCase(CreatureConstants.Types.Dragon, false)]
        [TestCase(CreatureConstants.Types.Elemental, false)]
        [TestCase(CreatureConstants.Types.Fey, false)]
        [TestCase(CreatureConstants.Types.Giant, false)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, false)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, false)]
        [TestCase(CreatureConstants.Types.Ooze, false)]
        [TestCase(CreatureConstants.Types.Outsider, false)]
        [TestCase(CreatureConstants.Types.Plant, false)]
        [TestCase(CreatureConstants.Types.Undead, false)]
        [TestCase(CreatureConstants.Types.Vermin, false)]
        public void IsCompatible_ReturnsCompatibility_BasedOnCreatureType(string creatureType, bool expected)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(creatureType, "subtype 1", "subtype 2")
                .WithLevelAdjustment(0)
                .Build();

            var compatible = applicator.IsCompatible(creature, true);
            Assert.That(compatible, Is.EqualTo(expected));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void IsCompatible_ReturnsFalse_WhenCasterLevelInsufficient(int casterLevel)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithCasterLevel(casterLevel)
                .Build();

            var compatible = applicator.IsCompatible(creature, false);
            Assert.That(compatible, Is.False);
        }

        [TestCase(11)]
        [TestCase(12)]
        [TestCase(20)]
        [TestCase(100)]
        public void IsCompatible_ReturnsTrue_WhenCasterLevelSufficient(int casterLevel)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithCasterLevel(casterLevel)
                .Build();

            var compatible = applicator.IsCompatible(creature, false);
            Assert.That(compatible, Is.True);
        }

        [Test]
        public void IsCompatible_AsCharacter_ReturnsFalse_WhenNoLevelAdjustment()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithLevelAdjustment(null)
                .Build();

            var compatible = applicator.IsCompatible(creature, true);
            Assert.That(compatible, Is.False);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(20)]
        [TestCase(100)]
        public void IsCompatible_AsCharacter_ReturnsTrue_WhenLevelAdjustmentSufficient(int levelAdjustment)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithLevelAdjustment(levelAdjustment)
                .Build();

            var compatible = applicator.IsCompatible(creature, true);
            Assert.That(compatible, Is.True);
        }

        [Test]
        public void IsCompatible_AsCharacter_ReturnsFalse_WhenNoLevelAdjustment_EvenIfCasterLevelSufficient()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithLevelAdjustment(null)
                .WithCasterLevel(11)
                .Build();

            var compatible = applicator.IsCompatible(creature, true);
            Assert.That(compatible, Is.False);
        }

        [Test]
        public void IsCompatible_AsCharacter_ReturnsTrue_EvenIfCasterLevelInsufficient()
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithLevelAdjustment(0)
                .WithCasterLevel(0)
                .Build();

            var compatible = applicator.IsCompatible(creature, true);
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
                .WithLevelAdjustment(0)
                .Build();

            var filters = new Filters { Alignment = alignment };

            var compatible = applicator.IsCompatible(creature, true, filters);
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
                .WithLevelAdjustment(0)
                .Build();

            var filters = new Filters { Alignment = alignment };

            var compatible = applicator.IsCompatible(creature, true, filters);
            Assert.That(compatible, Is.True);
        }

        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulGood, true)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.NeutralGood, false)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.ChaoticGood, false)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulNeutral, true)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.TrueNeutral, false)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.ChaoticNeutral, false)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulEvil, true)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.NeutralEvil, false)]
        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.ChaoticEvil, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.LawfulGood, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.NeutralGood, true)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.ChaoticGood, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.LawfulNeutral, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.TrueNeutral, true)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.ChaoticNeutral, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.LawfulEvil, false)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.NeutralEvil, true)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.ChaoticEvil, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.LawfulGood, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.NeutralGood, false)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.ChaoticGood, true)]
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
                .WithAlignments("other Good", creatureAlignment)
                .Build();

            var filters = new Filters { Alignment = alignmentFilter };

            var compatible = applicator.IsCompatible(creature, false, filters);
            Assert.That(compatible, Is.EqualTo(expected));
        }

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
        public void IsCompatible_WithChallengeRating_ReturnsCompatibility(string original, string filter, bool expected)
        {
            Assert.Fail("not yet written");
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
        public void IsCompatible_WithChallengeRating_ReturnsCompatibility_HumanoidCharacter(double hitDiceQuantity, string original, string challengeRating, bool expected)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        [Ignore("Liches must be humanoid, so testing for non-humanoid characters isn't needed")]
        public void IsCompatible_WithChallengeRating_ReturnsCompatibility_NonHumanoidCharacter(string original, string challengeRating, bool expected)
        {
            throw new NotImplementedException();
        }

        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.Undead, true)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, true)]
        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal, false)]
        [TestCase(CreatureConstants.Types.Subtypes.Elf, true)]
        [TestCase(CreatureConstants.Types.Subtypes.Dwarf, false)]
        [TestCase("subtype 1", true)]
        [TestCase("subtype 2", true)]
        [TestCase("subtype 3", false)]
        public void IsCompatible_WithType_ReturnsCompatibility(string filter, bool expected)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsTrue()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseAlignment()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseChallengeRating()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_WithAllFilters_ReturnsFalse_BecauseType()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype()
        {
            Assert.Fail("not yet written");
            Assert.Fail("Assert Con is 0");
            Assert.Fail("Assert Wis template adj is +2, full score 2 better");
            Assert.Fail("Assert Int template adj is +2, full score 2 better");
            Assert.Fail("Assert Cha template adj is +2, full score 2 better");
            Assert.Fail("Assert CR +2");
            Assert.Fail("Assert level adjustment +4");
            Assert.Fail("Assert updated creature types");
            Assert.Fail("Assert alignment is evil");
        }

        [TestCase(AbilityConstants.Intelligence)]
        [TestCase(AbilityConstants.Wisdom)]
        [TestCase(AbilityConstants.Charisma)]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_MissingAbilityIsUnaltered(string ability)
        {
            var creature = new CreaturePrototypeBuilder()
                .WithTestValues()
                .WithName("my creature")
                .WithCreatureType(CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2")
                .WithChallengeRating(ChallengeRatingConstants.CR1)
                .WithCasterLevel(9266)
                .WithLevelAdjustment(90210)
                .WithHitDiceQuantity(9)
                .WithoutAbility(ability)
                .Build();

            var updatedPrototype = applicator.ApplyTo(creature, true);
            Assert.That(updatedPrototype.Name, Is.EqualTo("my creature"));
            Assert.That(updatedPrototype.Abilities, Has.Count.EqualTo(6));
            Assert.That(updatedPrototype.Abilities[ability].FullScore, Is.Zero);
            Assert.That(updatedPrototype.Abilities[ability].TemplateScore, Is.EqualTo(-1));
            Assert.That(updatedPrototype.Abilities[ability].HasScore, Is.False);
        }

        [TestCase(ChallengeRatingConstants.CR1_3rd, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1_2nd, ChallengeRatingConstants.CR2)]
        [TestCase(ChallengeRatingConstants.CR1, ChallengeRatingConstants.CR3)]
        [TestCase(ChallengeRatingConstants.CR2, ChallengeRatingConstants.CR4)]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithUpdatedChallengeRating(string original, string updated)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.LawfulGood, AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.LawfulNeutral, AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.ChaoticEvil, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.ChaoticNeutral, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.NeutralEvil, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.NeutralGood, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.TrueNeutral, AlignmentConstants.NeutralEvil)]
        public void ApplyTo_Prototype_ReturnsUpdatedPrototype_WithUpdatedAlignment(string original, string updated)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_PrototypeWithoutLevelAdjustment_ReturnsUpdatedPrototype_WithUnchangedLevelAdjustment()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_PrototypeWithAlignment_ReturnsUpdatedPrototype_WithFilteredAlignment()
        {
            Assert.Fail("not yet written");
        }
    }
}
