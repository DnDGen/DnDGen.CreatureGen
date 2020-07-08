using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Templates;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        [SetUp]
        public void SetUp()
        {
            mockDice = new Mock<Dice>();
            mockSpeedsGenerator = new Mock<ISpeedsGenerator>();
            mockAttacksGenerator = new Mock<IAttacksGenerator>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockFeatsGenerator = new Mock<IFeatsGenerator>();

            applicator = new GhostApplicator(
                mockDice.Object,
                mockSpeedsGenerator.Object,
                mockAttacksGenerator.Object,
                mockCollectionSelector.Object,
                mockFeatsGenerator.Object);

            baseCreature = new CreatureBuilder().WithTestValues().Build();

            mockDice
                .Setup(d => d.Roll(1).d(3).AsSum<int>())
                .Returns(1);

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
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(ghostAttacks);

            var count = 0;
            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<Attack>>()))
                .Returns((IEnumerable<Attack> aa) => aa.ElementAt(count++ % aa.Count()));
        }

        [TestCase(CreatureConstants.Types.Aberration, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Animal, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Dragon, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Giant, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Humanoid, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.MagicalBeast, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Plant, CreatureConstants.Types.Undead)]
        public void CreatureTypeIsAdjusted(string original, string adjusted)
        {
            baseCreature.Type.Name = original;
            baseCreature.Type.SubTypes = new[]
            {
                "subtype 1",
                "subtype 2",
            };

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Type.Name, Is.EqualTo(adjusted));
            Assert.That(creature.Type.SubTypes.Count(), Is.EqualTo(3));
            Assert.That(creature.Type.SubTypes, Contains.Item("subtype 1")
                .And.Contains("subtype 2")
                .And.Contains(CreatureConstants.Types.Subtypes.Incorporeal));
        }

        [TestCaseSource("AbilityAdjustments")]
        public void CharismaIncreasesBy4(int raceAdjust, int baseScore, int advanced, int adjusted)
        {
            baseCreature.Abilities[AbilityConstants.Charisma].BaseScore = baseScore;
            baseCreature.Abilities[AbilityConstants.Charisma].RacialAdjustment = raceAdjust;
            baseCreature.Abilities[AbilityConstants.Charisma].AdvancementAdjustment = advanced;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(adjusted).And.AtLeast(10));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.AtLeast(4));

            if (baseScore + raceAdjust + advanced < 6)
            {
                Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.GreaterThan(4)
                    .And.EqualTo(10 - (baseScore + raceAdjust + advanced)));
            }
            else
            {
                Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(4));
            }
        }

        private static IEnumerable AbilityAdjustments
        {
            get
            {
                var baseScores = Enumerable.Range(3, 18);
                var raceAdjustments = Enumerable.Range(-5, 5 + 1 + 4).Select(i => i * 2);
                var advanceds = Enumerable.Range(0, 4);

                foreach (var score in baseScores)
                {
                    foreach (var race in raceAdjustments)
                    {
                        foreach (var advanced in advanceds)
                        {
                            var total = score + race + advanced;
                            yield return new TestCaseData(race, score, advanced, Math.Max(total + 4, 10));
                        }
                    }
                }
            }
        }

        [Test]
        public void ConstitutionGoesAway()
        {
            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Abilities[AbilityConstants.Constitution].HasScore, Is.False);
        }

        [TestCase(4)]
        [TestCase(6)]
        [TestCase(8)]
        [TestCase(10)]
        public void HitDiceChangeToD12_AndRerolled(int die)
        {
            baseCreature.HitPoints.HitDie = die;

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
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [Test]
        public void HitDiceChangeToD12_AndRerolled_WithoutConstitution()
        {
            baseCreature.HitPoints.HitDie = 4;

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
            Assert.That(creature.HitPoints.HitDie, Is.EqualTo(12));
            Assert.That(creature.HitPoints.Total, Is.EqualTo(9266 + 90210));
            Assert.That(creature.HitPoints.DefaultTotal, Is.EqualTo(42));
        }

        [Test]
        public void CreatureGainsFlySpeed()
        {
            var speeds = new Dictionary<string, Measurement>();
            speeds[SpeedConstants.Fly] = new Measurement("furlongs");
            speeds[SpeedConstants.Fly].Description = "the goodest";
            speeds[SpeedConstants.Fly].Value = 96;

            mockSpeedsGenerator
                .Setup(g => g.Generate(CreatureConstants.Templates.Ghost))
                .Returns(speeds);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(96));
        }

        [Test]
        public void CreatureGainsFlySpeed_BetterThanOriginalFlySpeed()
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(96));
        }

        [Test]
        public void CreatureGainsFlySpeed_SlowerThanOriginalFlySpeed()
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(600));
        }

        [Test]
        public void CreatureGainsFlySpeed_OriginalFlySpeedLessManeuverable()
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

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Speeds, Has.Count.EqualTo(2)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Description, Is.EqualTo("the goodest"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Unit, Is.EqualTo("furlongs"));
            Assert.That(creature.Speeds[SpeedConstants.Fly].Value, Is.EqualTo(96));
        }

        [Test]
        public void CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 42);

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);

            var naturalArmorBonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(naturalArmorBonuses, Has.Length.EqualTo(1));
            Assert.That(naturalArmorBonuses[0].Value, Is.EqualTo(42));
            Assert.That(naturalArmorBonuses[0].IsConditional, Is.True);
            Assert.That(naturalArmorBonuses[0].Condition, Is.EqualTo("Only applies for ethereal creatures"));
        }

        [Test]
        public void CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal_MultipleBonuses()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 42);
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 600);

            var creature = applicator.ApplyTo(baseCreature);
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
        public void CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal_NoNaturalArmor()
        {
            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);
            Assert.That(creature.ArmorClass.NaturalArmorBonuses, Is.Empty);
        }

        [Test]
        public void CreatureArmorClass_NaturalArmorIsConditionalToBeOnlyEthereal_AlreadyConditional()
        {
            baseCreature.ArmorClass.AddBonus(ArmorClassConstants.Natural, 42, "only sometimes");

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.ArmorClass.NaturalArmorBonus, Is.Zero);

            var naturalArmorBonuses = creature.ArmorClass.NaturalArmorBonuses.ToArray();
            Assert.That(naturalArmorBonuses, Has.Length.EqualTo(1));
            Assert.That(naturalArmorBonuses[0].Value, Is.EqualTo(42));
            Assert.That(naturalArmorBonuses[0].IsConditional, Is.True);
            Assert.That(naturalArmorBonuses[0].Condition, Is.EqualTo("only sometimes AND Only applies for ethereal creatures"));
        }

        [Test]
        public void CreatureArmorClass_DeflectionBonusOfCharisma()
        {
            baseCreature.Abilities[AbilityConstants.Charisma].BaseScore = 42;
            baseCreature.Abilities[AbilityConstants.Charisma].AdvancementAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Charisma].RacialAdjustment = 0;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(46));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(42));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].TemplateAdjustment, Is.EqualTo(4));
            Assert.That(creature.ArmorClass.DeflectionBonus, Is.EqualTo(18).And.EqualTo(baseCreature.Abilities[AbilityConstants.Charisma].Modifier));

            var deflectionBonuses = creature.ArmorClass.DeflectionBonuses.ToArray();
            Assert.That(deflectionBonuses, Has.Length.EqualTo(1));
            Assert.That(deflectionBonuses[0].Value, Is.EqualTo(18).And.EqualTo(baseCreature.Abilities[AbilityConstants.Charisma].Modifier));
            Assert.That(deflectionBonuses[0].IsConditional, Is.False);
            Assert.That(deflectionBonuses[0].Condition, Is.Empty);
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
        [TestCase(11)]
        public void CreatureArmorClass_DeflectionBonusOfCharisma_AtLeast1(int charisma)
        {
            baseCreature.Abilities[AbilityConstants.Charisma].BaseScore = charisma;
            baseCreature.Abilities[AbilityConstants.Charisma].AdvancementAdjustment = 0;
            baseCreature.Abilities[AbilityConstants.Charisma].RacialAdjustment = 0;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Abilities[AbilityConstants.Charisma].FullScore, Is.EqualTo(Math.Max(charisma + 4, 10)));
            Assert.That(creature.Abilities[AbilityConstants.Charisma].BaseScore, Is.EqualTo(charisma));
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
        public void CreatureGainsSpecialAttacks_RandomAmount(int amount)
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
                    baseCreature.HitPoints.RoundedHitDiceQuantity))
                .Returns(ghostAttacks);

            mockDice
                .Setup(d => d.Roll(1).d(3).AsSum<int>())
                .Returns(amount);

            var originalCount = baseCreature.Attacks.Count();
            var creature = applicator.ApplyTo(baseCreature);

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
        public void CreatureGainsSpecialQualities()
        {
            var ghostQualities = new[]
            {
                new Feat { Name = "ghost quality 1" },
                new Feat { Name = "ghost quality 2" },
            };

            mockFeatsGenerator
                .Setup(g => g.GenerateSpecialQualities(
                    CreatureConstants.Templates.Ghost,
                    baseCreature.Type,
                    baseCreature.HitPoints,
                    baseCreature.Abilities,
                    baseCreature.Skills,
                    baseCreature.CanUseEquipment,
                    baseCreature.Size,
                    baseCreature.Alignment))
                .Returns(ghostQualities);

            var originalCount = baseCreature.SpecialQualities.Count();
            var creature = applicator.ApplyTo(baseCreature);

            Assert.That(creature.SpecialQualities.Count(), Is.EqualTo(originalCount + ghostQualities.Length));
            Assert.That(creature.SpecialQualities, Is.SupersetOf(ghostQualities));
        }

        [Test]
        public void CreatureSkills_GainRacialBonuses()
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
        }

        [Test]
        public void CreatureSkills_GainRacialBonuses_NoSkills()
        {
            baseCreature.Skills = new List<Skill>();

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature.Skills, Is.Empty);
        }

        [Test]
        public void CreatureSkills_GainRacialBonuses_MissingRelevantSkills()
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

        [TestCaseSource("ChallengeRatingAdjustments")]
        public void CreatureChallengeRating_IncreasesBy2(string original, string adjusted)
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

                //index 0 is CR 0
                for (var i = 1; i < challengeRatings.Length - 2; i++)
                {
                    yield return new TestCaseData(challengeRatings[i], challengeRatings[i + 2]);
                }
            }
        }

        [TestCase(null, null)]
        [TestCase(0, 5)]
        [TestCase(1, 6)]
        [TestCase(2, 7)]
        [TestCase(10, 15)]
        [TestCase(20, 25)]
        [TestCase(42, 47)]
        public void CreatureLevelAdjustment_Increases(int? original, int? adjusted)
        {
            baseCreature.LevelAdjustment = original;

            var creature = applicator.ApplyTo(baseCreature);
            Assert.That(creature, Is.EqualTo(baseCreature));
            Assert.That(creature.LevelAdjustment, Is.EqualTo(adjusted));
        }

        [Test]
        public void CreatureEquipment_None_GainsNone()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureEquipment_HasWeapon_GainsGhostlyEquipment()
        {
            //2d4 items - random type?  Use item generator?
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureEquipment_HasArmor_GainsGhostlyEquipment()
        {
            //2d4 items - random type?  Use item generator?
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureEquipment_HasShield_GainsGhostlyEquipment()
        {
            //2d4 items - random type?  Use item generator?
            Assert.Fail("not yet written");
        }

        [Test]
        public void CreatureEquipment_HasItem_GainsGhostlyEquipment()
        {
            //2d4 items - random type?  Use item generator?
            Assert.Fail("not yet written");
        }
    }
}
