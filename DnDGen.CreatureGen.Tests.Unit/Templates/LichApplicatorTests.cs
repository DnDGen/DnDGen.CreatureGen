using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Languages;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class LichApplicatorTests
    {
        private TemplateApplicator applicator;
        private Creature baseCreature;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<ICreatureDataSelector> mockCreatureDataSelector;
        private Mock<Dice> mockDice;
        private Mock<IAttacksGenerator> mockAttacksGenerator;
        private Mock<IFeatsGenerator> mockFeatsGenerator;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockCreatureDataSelector = new Mock<ICreatureDataSelector>();
            mockDice = new Mock<Dice>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();

            baseCreature = new CreatureBuilder()
                .WithTestValues()
                .WithCreatureType(CreatureConstants.Types.Humanoid)
                .Build();

            applicator = new LichApplicator(
                mockCollectionSelector.Object,
                mockCreatureDataSelector.Object,
                mockDice.Object,
                mockAttacksGenerator.Object,
                mockFeatsGenerator.Object,
                mockTypeAndAmountSelector.Object);

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
        public void IsCompatible_BasedOnCreatureType(string creatureType, bool compatible)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { creatureType, "subtype 1", "subtype 2" });

            var creatureData = new CreatureDataSelection { CasterLevel = 0, LevelAdjustment = 0 };
            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(creatureData);

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Casters, "my creature"))
                .Returns(Enumerable.Empty<TypeAndAmountSelection>());

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(compatible));
        }

        [TestCase(0)]
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
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void IsCompatible_HasCasterLevel_CreatureData(int casterLevel)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            var creatureData = new CreatureDataSelection { CasterLevel = casterLevel, LevelAdjustment = null };
            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(creatureData);

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Casters, "my creature"))
                .Returns(Enumerable.Empty<TypeAndAmountSelection>());

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(casterLevel >= 11));
        }

        [TestCase(0)]
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
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void IsCompatible_HasCasterLevel_Spells(int casterLevel)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            var creatureData = new CreatureDataSelection { CasterLevel = 0, LevelAdjustment = null };
            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(creatureData);

            var spellcaster = new TypeAndAmountSelection { Type = "spellcaster", Amount = casterLevel };
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Casters, "my creature"))
                .Returns(new[] { spellcaster });

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.EqualTo(casterLevel >= 11));
        }

        [Test]
        public void IsCompatible_ReturnsFalse_HasNoSpellcasterLevel()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            var creatureData = new CreatureDataSelection { CasterLevel = 0, LevelAdjustment = null };
            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(creatureData);

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Casters, "my creature"))
                .Returns(Enumerable.Empty<TypeAndAmountSelection>());

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.False);
        }

        [TestCase(0)]
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
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void IsCompatible_CanBeCharacter(int levelAdjustment)
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            var creatureData = new CreatureDataSelection { CasterLevel = 0, LevelAdjustment = levelAdjustment };
            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(creatureData);

            var isCompatible = applicator.IsCompatible("my creature");
            Assert.That(isCompatible, Is.True);
        }

        [Test]
        public void IsCompatible_TypeMustMatch()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_ChallengeRatingMustMatch()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_TypeAndChallengeRatingMustMatch()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplyTo_GainsCommon()
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.Lich + LanguageConstants.Groups.Automatic))
                .Returns("Spoopy");

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Spoopy"));
        }

        [Test]
        public void ApplyTo_GainsCommon_AlreadyKnows()
        {
            baseCreature.Languages = baseCreature.Languages.Union(new[] { "Spoopy" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.Lich + LanguageConstants.Groups.Automatic))
                .Returns("Spoopy");

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Spoopy"));
        }

        [Test]
        public void GetPotentialTypes_CreatureTypeChangesToUndead()
        {
            mockCollectionSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CreatureTypes, "my creature"))
                .Returns(new[] { CreatureConstants.Types.Humanoid, "subtype 1", "subtype 2" });

            var types = applicator.GetPotentialTypes("my creature");
            Assert.That(types.First(), Is.EqualTo(CreatureConstants.Types.Undead));

            var subtypes = types.Skip(1);
            Assert.That(subtypes.Count(), Is.EqualTo(4));
            Assert.That(subtypes, Contains.Item("subtype 1")
                .And.Contains(CreatureConstants.Types.Subtypes.Augmented)
                .And.Contains(CreatureConstants.Types.Humanoid)
                .And.Contains("subtype 2"));
        }

        [Test]
        public void ApplyTo_CreatureTypeChangesToUndead()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(4));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains(CreatureConstants.Types.Subtypes.Augmented)
                .And.Contains(CreatureConstants.Types.Humanoid)
                .And.Contains("subtype 2"));
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
                .Returns(new[] { 9266, 90210 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(42);

            var creature = applicator.ApplyTo(baseCreature);
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [Test]
        public void ApplyTo_GainsNaturalArmor()
        {
            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(5));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(5));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(9266));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

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
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "lich touch roll", Type = "lich touch type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Lich,
                    baseCreature.Size,
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
                new Attack
                {
                    Name = "Touch",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "lich touch roll", Type = "lich touch type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Lich,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            var newQualities = new[]
            {
                new Feat { Name = "lich quality 1" },
                new Feat { Name = "lich quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Lich,
                    baseCreature.Type,
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
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "lich touch roll", Type = "lich touch type" }
                    },
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = new List<int> { 92 },
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
            var creature = applicator.ApplyTo(baseCreature);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + attacksWithBonuses.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(attacksWithBonuses));
        }

        [Test]
        public void ApplyTo_GainSpecialQualities()
        {
            var lichQualities = new[]
            {
                new Feat { Name = "lich quality 1" },
                new Feat { Name = "lich quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Lich,
                    baseCreature.Type,
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(lichQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = applicator.ApplyTo(baseCreature);

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
                    baseCreature.Alignment))
                .Returns(lichQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = applicator.ApplyTo(baseCreature);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + lichQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(lichQualities));
        }

        [Test]
        public void ApplyTo_ModifyAbilities()
        {
            var creature = applicator.ApplyTo(baseCreature);
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

            var creature = applicator.ApplyTo(baseCreature);
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

            var creature = applicator.ApplyTo(baseCreature);
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
            Assert.That(skills[9].Name, Is.EqualTo(SkillConstants.MoveSilently));
            Assert.That(skills[9].Bonus, Is.EqualTo(608));
            Assert.That(skills[9].Bonuses.Count(), Is.EqualTo(3));
            Assert.That(skills[10].Name, Is.EqualTo("other skill 6"));
            Assert.That(skills[10].Bonus, Is.EqualTo(600));
            Assert.That(skills[10].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[11].Name, Is.EqualTo(SkillConstants.SenseMotive));
            Assert.That(skills[11].Bonus, Is.EqualTo(608));
            Assert.That(skills[11].Bonuses.Count(), Is.EqualTo(3));
            Assert.That(skills[12].Name, Is.EqualTo("other skill 7"));
            Assert.That(skills[12].Bonus, Is.EqualTo(600));
            Assert.That(skills[12].Bonuses.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ApplyTo_CreatureSkills_GainRacialBonuses_NoSkills()
        {
            baseCreature.Skills = new List<Skill>();

            var creature = applicator.ApplyTo(baseCreature);
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

            var creature = applicator.ApplyTo(baseCreature);
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
        public void ApplyTo_SwapConsitutionForCharismaForConcentration()
        {
            var concentration = new Skill(SkillConstants.Concentration, baseCreature.Abilities[AbilityConstants.Constitution], 42);
            baseCreature.Skills = baseCreature.Skills.Union(new[] { concentration });

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Skills, Contains.Item(concentration));
            Assert.That(concentration.BaseAbility, Is.EqualTo(creature.Abilities[AbilityConstants.Charisma]));
        }

        [Test]
        public void ApplyTo_CreatureSkills_DoNotHaveFortitudeSave()
        {
            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Saves[SaveConstants.Fortitude].HasSave, Is.False);
            Assert.That(creature.Saves[SaveConstants.Reflex].HasSave, Is.True);
            Assert.That(creature.Saves[SaveConstants.Will].HasSave, Is.True);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void GetPotentialChallengeRating_ImproveChallengeRating(string original, string adjusted)
        {
            mockCreatureDataSelector
                .Setup(s => s.SelectFor("my creature"))
                .Returns(new CreatureDataSelection { ChallengeRating = original });

            var challengeRating = applicator.GetPotentialChallengeRating("my creature");
            Assert.That(challengeRating, Is.EqualTo(adjusted));
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public void ApplyTo_ImproveChallengeRating(string original, string adjusted)
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

                for (var i = 0; i < challengeRatings.Length; i++)
                {
                    var increased = ChallengeRatingConstants.IncreaseChallengeRating(challengeRatings[i], 2);
                    yield return new TestCaseData(challengeRatings[i], increased);
                }
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo(adjusted));
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [Test]
        public async Task ApplyToAsync_GainsCommon()
        {
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.Lich + LanguageConstants.Groups.Automatic))
                .Returns("Spoopy");

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length + 1));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Spoopy"));
        }

        [Test]
        public async Task ApplyToAsync_GainsCommon_AlreadyKnows()
        {
            baseCreature.Languages = baseCreature.Languages.Union(new[] { "Spoopy" });
            var originalLanguages = baseCreature.Languages.ToArray();

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(
                    TableNameConstants.Collection.LanguageGroups,
                    CreatureConstants.Templates.Lich + LanguageConstants.Groups.Automatic))
                .Returns("Spoopy");

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Languages.Count(), Is.EqualTo(originalLanguages.Length));
            Assert.That(creature.Languages, Is.SupersetOf(originalLanguages)
                .And.Contains("Spoopy"));
        }

        [Test]
        public async Task ApplyToAsync_CreatureTypeChangesToUndead()
        {
            baseCreature.Type.Name = CreatureConstants.Types.Humanoid;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(CreatureConstants.Types.Undead));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(4));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(CreatureConstants.Types.Humanoid)
                .And.Contains(CreatureConstants.Types.Subtypes.Augmented));
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
                .Returns(new[] { 9266, 90210 });
            mockDice
                .Setup(d => d
                    .Roll(baseCreature.HitPoints.RoundedHitDiceQuantity)
                    .d(12)
                    .AsPotentialAverage())
                .Returns(42);

            var creature = await applicator.ApplyToAsync(baseCreature);
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

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.HitPoints.HitDice[0].HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [Test]
        public async Task ApplyToAsync_GainsNaturalArmor()
        {
            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(5));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(1));

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

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(5));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

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

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.EqualTo(9266));
            Assert.That(creature.ArmorClass.NaturalArmorBonuses.Count(), Is.EqualTo(2));

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
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "lich touch roll", Type = "lich touch type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Lich,
                    baseCreature.Size,
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
                new Attack
                {
                    Name = "Touch",
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "lich touch roll", Type = "lich touch type" }
                    },
                    IsSpecial = false,
                    IsMelee = true
                },
            };

            mockAttacksGenerator
                .Setup(g => g.GenerateAttacks(
                    CreatureConstants.Templates.Lich,
                    baseCreature.Size,
                    baseCreature.Size,
                    baseCreature.BaseAttackBonus,
                    baseCreature.Abilities,
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(newAttacks);

            var newQualities = new[]
            {
                new Feat { Name = "lich quality 1" },
                new Feat { Name = "lich quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Lich,
                    baseCreature.Type,
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
                    Damages = new List<Damage>
                    {
                        new Damage { Roll = "lich touch roll", Type = "lich touch type" }
                    },
                    IsSpecial = false,
                    IsMelee = true,
                    AttackBonuses = new List<int> { 92 },
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
            var creature = await applicator.ApplyToAsync(baseCreature);

            Assert.That(creature.Attacks.Count(), Is.EqualTo(originalCount + attacksWithBonuses.Length));
            Assert.That(creature.Attacks, Is.SupersetOf(attacksWithBonuses));
        }

        [Test]
        public async Task ApplyToAsync_GainSpecialQualities()
        {
            var lichQualities = new[]
            {
                new Feat { Name = "lich quality 1" },
                new Feat { Name = "lich quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Lich,
                    baseCreature.Type,
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(lichQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = await applicator.ApplyToAsync(baseCreature);

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
                    baseCreature.Alignment))
                .Returns(lichQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = await applicator.ApplyToAsync(baseCreature);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + lichQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(lichQualities));
        }

        [Test]
        public async Task ApplyToAsync_ModifyAbilities()
        {
            var creature = await applicator.ApplyToAsync(baseCreature);
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

            var creature = await applicator.ApplyToAsync(baseCreature);
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

            var creature = await applicator.ApplyToAsync(baseCreature);
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
            Assert.That(skills[9].Name, Is.EqualTo(SkillConstants.MoveSilently));
            Assert.That(skills[9].Bonus, Is.EqualTo(608));
            Assert.That(skills[9].Bonuses.Count(), Is.EqualTo(3));
            Assert.That(skills[10].Name, Is.EqualTo("other skill 6"));
            Assert.That(skills[10].Bonus, Is.EqualTo(600));
            Assert.That(skills[10].Bonuses.Count(), Is.EqualTo(2));
            Assert.That(skills[11].Name, Is.EqualTo(SkillConstants.SenseMotive));
            Assert.That(skills[11].Bonus, Is.EqualTo(608));
            Assert.That(skills[11].Bonuses.Count(), Is.EqualTo(3));
            Assert.That(skills[12].Name, Is.EqualTo("other skill 7"));
            Assert.That(skills[12].Bonus, Is.EqualTo(600));
            Assert.That(skills[12].Bonuses.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task ApplyToAsync_CreatureSkills_GainRacialBonuses_NoSkills()
        {
            baseCreature.Skills = new List<Skill>();

            var creature = await applicator.ApplyToAsync(baseCreature);
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

            var creature = await applicator.ApplyToAsync(baseCreature);
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
        public async Task ApplyToAsync_SwapConsitutionForCharismaForConcentration()
        {
            var concentration = new Skill(SkillConstants.Concentration, baseCreature.Abilities[AbilityConstants.Constitution], 42);
            baseCreature.Skills = baseCreature.Skills.Union(new[] { concentration });

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.Skills, Contains.Item(concentration));
            Assert.That(concentration.BaseAbility, Is.EqualTo(creature.Abilities[AbilityConstants.Charisma]));
        }

        [Test]
        public async Task ApplyToAsync_CreatureSkills_DoNotHaveFortitudeSave()
        {
            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature.Saves[SaveConstants.Fortitude].HasSave, Is.False);
            Assert.That(creature.Saves[SaveConstants.Reflex].HasSave, Is.True);
            Assert.That(creature.Saves[SaveConstants.Will].HasSave, Is.True);
        }

        [TestCaseSource(nameof(ChallengeRatingAdjustments))]
        public async Task ApplyToAsync_ImproveChallengeRating(string original, string adjusted)
        {
            baseCreature.ChallengeRating = original;

            var creature = await applicator.ApplyToAsync(baseCreature);
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

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Alignment.Full, Is.EqualTo(adjusted));
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

            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [Test]
        public void ApplyTo_SetsTemplate()
        {
            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.Lich));
        }

        [Test]
        public async Task ApplyToAsync_SetsTemplate()
        {
            var creature = await applicator.ApplyToAsync(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Template, Is.EqualTo(CreatureConstants.Templates.Lich));
        }
    }
}
