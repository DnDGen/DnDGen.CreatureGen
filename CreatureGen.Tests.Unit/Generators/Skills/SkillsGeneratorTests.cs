using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Generators.Skills;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Skills
{
    [TestFixture]
    public class SkillsGeneratorTests
    {
        private ISkillsGenerator skillsGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private Dictionary<string, Ability> abilities;
        private List<string> creatureSkills;
        private Mock<ISkillSelector> mockSkillSelector;
        private int creatureSkillPoints;
        private List<string> allSkills;
        private Creature creature;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockSkillSelector = new Mock<ISkillSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            skillsGenerator = new SkillsGenerator(mockSkillSelector.Object, mockCollectionsSelector.Object, mockAdjustmentsSelector.Object, mockPercentileSelector.Object, mockTypeAndAmountSelector.Object);
            abilities = new Dictionary<string, Ability>();
            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            allSkills = new List<string>();
            creatureSkills = new List<string>();
            creature = new Creature();

            creature.Name = "creature name";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.All)).Returns(allSkills);

            var emptyAdjustments = new Dictionary<string, int>();
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(It.IsAny<string>())).Returns(emptyAdjustments);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SkillPoints, creature.Name)).Returns(() => creatureSkillPoints);

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<SkillSelection>>())).Returns((IEnumerable<SkillSelection> ss) => ss.ElementAt(index++ % ss.Count()));

            mockSkillSelector.Setup(s => s.SelectFor(It.IsAny<string>())).Returns((string skill) => new SkillSelection { SkillName = skill, BaseAbilityName = AbilityConstants.Intelligence });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, creature.Name)).Returns(creatureSkills);
        }

        [Test]
        public void GetSkills()
        {
            creatureSkillPoints = 2;
            AddCreatureSkills(3);

            var skills = skillsGenerator.GenerateFor(creature);
            Assert.That(skills, Is.Not.Empty);

            foreach (var skill in skills)
            {
                Assert.That(skill, Is.Not.Null);
                Assert.That(skill.Name, Is.Not.Empty);
                Assert.That(skill.RankCap, Is.Positive);
                Assert.That(skill.BaseAbility, Is.Not.Null);
                Assert.That(abilities.Values, Contains.Item(skill.BaseAbility));
            }
        }

        [Test]
        public void SkillHasArmorCheckPenalty()
        {
            creatureSkillPoints = 2;
            AddCreatureSkills(3);

            var armorCheckSkills = new[] { "other skill", creatureSkills[0] };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.ArmorCheckPenalty)).Returns(armorCheckSkills);

            var skills = skillsGenerator.GenerateFor(creature);
            var skill = skills.First(s => s.Name == creatureSkills[0]);
            Assert.That(skill.HasArmorCheckPenalty, Is.True);
        }

        [Test]
        public void SkillDoesNotHaveArmorCheckPenalty()
        {
            creatureSkillPoints = 2;
            AddCreatureSkills(3);

            var armorCheckSkills = new[] { "other skill", "different skill" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, GroupConstants.ArmorCheckPenalty)).Returns(armorCheckSkills);

            var skills = skillsGenerator.GenerateFor(creature);
            var skill = skills.First(s => s.Name == creatureSkills[0]);
            Assert.That(skill.HasArmorCheckPenalty, Is.False);
        }

        private void AddCreatureSkills(int quantity)
        {
            while (quantity > 0)
            {
                creatureSkills.Add($"creature skill {quantity--}");
            }
        }

        [Test]
        public void AssignAbilitiesToSkills()
        {
            AddCreatureSkills(1);

            var creatureSkillSelection = new SkillSelection();
            creatureSkillSelection.BaseAbilityName = "ability 1";
            creatureSkillSelection.SkillName = "class skill name";

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(creatureSkillSelection);

            abilities["ability 1"] = new Ability("ability 1");

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("class skill name"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities["stat 1"]));
        }

        [Test]
        public void SetClassSkill()
        {
            AddCreatureSkills(2);

            var skills = skillsGenerator.GenerateFor(creature);

            Assert.That(skills.Single(s => s.Name == "creature skill 1").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "creature skill 2").ClassSkill, Is.True);
        }

        [Test]
        public void GetSkillWithSetFocus()
        {
            AddCreatureSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.Focus = Guid.NewGuid().ToString();

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(skillSelection);

            var skills = skillsGenerator.GenerateFor(creature);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skill.Focus, Is.EqualTo(skillSelection.Focus));
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GetSkillWithRandomFocus()
        {
            AddCreatureSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(creature);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skill.Focus, Is.EqualTo("random"));
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GetSkillWithMultipleRandomFoci()
        {
            AddCreatureSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 2;

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[0].Focus, Is.EqualTo("random"));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[1].Focus, Is.EqualTo("third random"));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(2));
        }

        [Test]
        public void DoNotRepeatFociForSkill()
        {
            AddCreatureSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 2;

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt((index++ / 2) % ss.Count()));

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[0].Focus, Is.EqualTo("random"));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[1].Focus, Is.EqualTo("other random"));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(2));
        }

        [Test]
        public void GetSkillWithAllFoci()
        {
            AddCreatureSkills(1);

            var skillSelection = new SkillSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = SkillConstants.Foci.QuantityOfAll;

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[0].Focus, Is.EqualTo("random"));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[1].Focus, Is.EqualTo("other random"));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skills[2].Focus, Is.EqualTo("third random"));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(3));
        }

        [Test]
        public void AllSkillsMaxedOut()
        {
            creatureSkillPoints = 3;
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var skills = skillsGenerator.GenerateFor(creature);

            Assert.That(skills.Single(s => s.Name == "skill 1").Ranks, Is.EqualTo(creature.HitPoints.HitDiceQuantity + 3));
            Assert.That(skills.Single(s => s.Name == "skill 2").Ranks, Is.EqualTo(creature.HitPoints.HitDiceQuantity + 3));
        }

        [Test]
        public void ApplySkillSynergyIfSufficientRanks()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(new[] { "synergy 1", "synergy 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 3" });

            creatureSkillPoints = 2;
            creature.HitPoints.HitDiceQuantity = 13;

            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");
            creatureSkills.Add("synergy 1");
            creatureSkills.Add("synergy 2");
            creatureSkills.Add("synergy 3");

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Ranks, Is.EqualTo(4));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].Ranks, Is.EqualTo(0));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[2].Ranks, Is.EqualTo(4));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills[3].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[3].Ranks, Is.EqualTo(0));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills[3].Bonus, Is.EqualTo(0));

            Assert.That(skills[4].Name, Is.EqualTo("synergy 3"));
            Assert.That(skills[4].Ranks, Is.EqualTo(4));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills[4].Bonus, Is.EqualTo(0));
        }

        [Test]
        public void ApplySkillSynergyWithFociIfSufficientRanks()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(new[] { "synergy 1", "synergy 2/focus" });

            creatureSkillPoints = 2;
            creature.HitPoints.HitDiceQuantity = 13;

            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");
            creatureSkills.Add("skill 3");
            creatureSkills.Add("skill 4");
            creatureSkills.Add("synergy 1");
            creatureSkills.Add("synergy 3");

            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(new SkillSelection { SkillName = "synergy 2", BaseAbilityName = AbilityConstants.Intelligence, Focus = "focus" });
            mockSkillSelector.Setup(s => s.SelectFor("skill 3")).Returns(new SkillSelection { SkillName = "synergy 2", BaseAbilityName = AbilityConstants.Intelligence, Focus = "other focus" });
            mockSkillSelector.Setup(s => s.SelectFor("synergy 2/focus")).Returns(new SkillSelection { SkillName = "synergy 2", BaseAbilityName = AbilityConstants.Intelligence, Focus = "focus" });

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].Ranks, Is.EqualTo(6));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[1].Focus, Is.EqualTo("focus"));
            Assert.That(skills[1].Ranks, Is.EqualTo(6));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[1].Bonus, Is.EqualTo(2));

            Assert.That(skills[2].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[2].Focus, Is.EqualTo("other focus"));
            Assert.That(skills[2].Ranks, Is.EqualTo(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills[3].Name, Is.EqualTo("skill 4"));
            Assert.That(skills[3].Focus, Is.Empty);
            Assert.That(skills[3].Ranks, Is.EqualTo(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonus, Is.EqualTo(0));

            Assert.That(skills[4].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[4].Focus, Is.Empty);
            Assert.That(skills[4].Ranks, Is.EqualTo(5));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[4].Bonus, Is.EqualTo(2));

            Assert.That(skills[5].Name, Is.EqualTo("synergy 3"));
            Assert.That(skills[5].Focus, Is.Empty);
            Assert.That(skills[5].Ranks, Is.EqualTo(5));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[5].Bonus, Is.EqualTo(0));
        }

        [Test]
        public void ApplySkillSynergyIfSufficientRanksWithFoci()
        {
            creatureSkillPoints = 2;
            creature.HitPoints.HitDiceQuantity = 13;

            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");
            creatureSkills.Add("skill 3");
            creatureSkills.Add("skill 4");
            creatureSkills.Add("synergy 1");
            creatureSkills.Add("synergy 2");

            mockSkillSelector.Setup(s => s.SelectFor("skill 1")).Returns(new SkillSelection { SkillName = "skill 1", BaseAbilityName = AbilityConstants.Intelligence, Focus = "focus" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1/focus")).Returns(new[] { "synergy 1", "synergy 2" });

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.EqualTo("focus"));
            Assert.That(skills[0].Ranks, Is.EqualTo(6));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].Ranks, Is.EqualTo(6));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("skill 3"));
            Assert.That(skills[2].Focus, Is.Empty);
            Assert.That(skills[2].Ranks, Is.EqualTo(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills[3].Name, Is.EqualTo("skill 4"));
            Assert.That(skills[3].Focus, Is.Empty);
            Assert.That(skills[3].Ranks, Is.EqualTo(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonus, Is.EqualTo(0));

            Assert.That(skills[4].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[4].Focus, Is.Empty);
            Assert.That(skills[4].Ranks, Is.EqualTo(5));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[4].Bonus, Is.EqualTo(2));

            Assert.That(skills[5].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[5].Focus, Is.Empty);
            Assert.That(skills[5].Ranks, Is.EqualTo(5));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[5].Bonus, Is.EqualTo(2));
        }

        [Test]
        public void SkillSynergyStacks()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(new[] { "synergy 1", "synergy 2" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 1" });

            creatureSkillPoints = 4;
            creature.HitPoints.HitDiceQuantity = 2;
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");
            creatureSkills.Add("synergy 1");
            creatureSkills.Add("synergy 2");

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Ranks, Is.EqualTo(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].Ranks, Is.EqualTo(5));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[2].Ranks, Is.EqualTo(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(4));

            Assert.That(skills[3].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[3].Ranks, Is.EqualTo(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonus, Is.EqualTo(2));
        }

        [Test]
        public void DoNotApplySkillSynergyIfThereIsNoSynergisticSkill()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(Enumerable.Empty<string>());
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 1" });

            creatureSkillPoints = 2;
            creature.HitPoints.HitDiceQuantity = 2;

            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");
            creatureSkills.Add("synergy 2");

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Ranks, Is.EqualTo(4));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].Ranks, Is.EqualTo(3));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[2].Ranks, Is.EqualTo(3));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills.Length, Is.EqualTo(3));
        }

        [Test]
        public void DoNotApplySkillSynergyIfThereIsNoSynergisticSkillWithCorrectFocus()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(Enumerable.Empty<string>());
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 2")).Returns(new[] { "synergy 1/focus" });

            creatureSkillPoints = 2;
            creature.HitPoints.HitDiceQuantity = 2;

            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");
            creatureSkills.Add("synergy 1/other focus");

            mockSkillSelector.Setup(s => s.SelectFor("synergy 1/focus")).Returns(new SkillSelection { SkillName = "synergy 1", BaseAbilityName = AbilityConstants.Intelligence, Focus = "focus" });
            mockSkillSelector.Setup(s => s.SelectFor("synergy 1/other focus")).Returns(new SkillSelection { SkillName = "synergy 1", BaseAbilityName = AbilityConstants.Intelligence, Focus = "other focus" });

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].Ranks, Is.EqualTo(4));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(4));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].Ranks, Is.EqualTo(3));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[2].Focus, Is.EqualTo("other focus"));
            Assert.That(skills[2].Ranks, Is.EqualTo(3));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills.Length, Is.EqualTo(3));
        }

        [Test]
        public void DoNotApplySkillSynergyIfInsufficientRanks()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, "skill 1")).Returns(new[] { "synergy 1" });

            creatureSkillPoints = 3;
            creature.HitPoints.HitDiceQuantity = 6;

            creatureSkills.Add("skill 1");
            creatureSkills.Add("synergy 1");

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Ranks, Is.EqualTo(7));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(7));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[1].Ranks, Is.EqualTo(0));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(0));
            Assert.That(skills[1].Bonus, Is.EqualTo(2));
        }

        [TestCase(1, 1, 4)]
        [TestCase(2, 1, 5)]
        [TestCase(3, 1, 6)]
        [TestCase(4, 1, 7)]
        [TestCase(5, 1, 8)]
        [TestCase(6, 1, 9)]
        [TestCase(7, 1, 10)]
        [TestCase(8, 1, 11)]
        [TestCase(9, 1, 12)]
        [TestCase(10, 1, 13)]
        [TestCase(11, 1, 14)]
        [TestCase(12, 1, 15)]
        [TestCase(13, 1, 16)]
        [TestCase(14, 1, 17)]
        [TestCase(15, 1, 18)]
        [TestCase(16, 1, 19)]
        [TestCase(17, 1, 20)]
        [TestCase(18, 1, 21)]
        [TestCase(19, 1, 22)]
        [TestCase(20, 1, 23)]
        [TestCase(1, 2, 8)]
        [TestCase(2, 2, 10)]
        [TestCase(3, 2, 12)]
        [TestCase(4, 2, 14)]
        [TestCase(5, 2, 16)]
        [TestCase(6, 2, 18)]
        [TestCase(7, 2, 20)]
        [TestCase(8, 2, 22)]
        [TestCase(9, 2, 24)]
        [TestCase(10, 2, 26)]
        [TestCase(11, 2, 28)]
        [TestCase(12, 2, 30)]
        [TestCase(13, 2, 32)]
        [TestCase(14, 2, 34)]
        [TestCase(15, 2, 36)]
        [TestCase(16, 2, 38)]
        [TestCase(17, 2, 40)]
        [TestCase(18, 2, 42)]
        [TestCase(19, 2, 44)]
        [TestCase(20, 2, 46)]
        [TestCase(1, 8, 8)]
        [TestCase(2, 8, 10)]
        [TestCase(3, 8, 12)]
        [TestCase(4, 8, 14)]
        [TestCase(5, 8, 16)]
        [TestCase(6, 8, 18)]
        [TestCase(7, 8, 20)]
        [TestCase(8, 8, 22)]
        [TestCase(9, 8, 24)]
        [TestCase(10, 8, 26)]
        [TestCase(11, 8, 28)]
        [TestCase(12, 8, 30)]
        [TestCase(13, 8, 32)]
        [TestCase(14, 8, 34)]
        [TestCase(15, 8, 36)]
        [TestCase(16, 8, 38)]
        [TestCase(17, 8, 40)]
        [TestCase(18, 8, 42)]
        [TestCase(19, 8, 44)]
        [TestCase(20, 8, 46)]
        public void SkillPointsDeterminedByHitDice(int hitDiceQuantity, int skillPointsPerDie, int skillPoints)
        {
            creature.HitPoints.HitDiceQuantity = hitDiceQuantity;
            creatureSkillPoints = skillPointsPerDie;
            abilities[AbilityConstants.Intelligence].BaseValue = 10;
            AddCreatureSkills(hitDiceQuantity + 3);

            var skills = skillsGenerator.GenerateFor(creature).ToArray();
            var totalRanks = skills.Sum(s => s.Ranks);

            Assert.That(totalRanks, Is.EqualTo(skillPoints));

            Assert.That(skills[0].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skills[0].RankCap, Is.EqualTo(4));

            Assert.That(skills[1].Name, Is.EqualTo(creatureSkills[1]));
            Assert.That(skills[1].RankCap, Is.EqualTo(4));

            Assert.That(skills[2].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skills[2].RankCap, Is.EqualTo(4 + hitDiceQuantity));

            Assert.That(skills[3].Name, Is.EqualTo(creatureSkills[1]));
            Assert.That(skills[3].RankCap, Is.EqualTo(4 + hitDiceQuantity));

            for (var i = 4; i < skills.Length; i++)
                Assert.That(skills[i].RankCap, Is.EqualTo(hitDiceQuantity + 3));
        }

        [TestCase(1, 12)]
        [TestCase(2, 15)]
        [TestCase(3, 18)]
        [TestCase(4, 21)]
        [TestCase(5, 24)]
        [TestCase(6, 27)]
        [TestCase(7, 30)]
        [TestCase(8, 33)]
        [TestCase(9, 36)]
        [TestCase(10, 39)]
        [TestCase(11, 42)]
        [TestCase(12, 45)]
        [TestCase(13, 48)]
        [TestCase(14, 51)]
        [TestCase(15, 54)]
        [TestCase(16, 57)]
        [TestCase(17, 60)]
        [TestCase(18, 63)]
        [TestCase(19, 66)]
        [TestCase(20, 69)]
        public void ApplyIntelligenceBonusToSkillPoints(int hitDie, int skillPoints)
        {
            creature.HitPoints.HitDiceQuantity = hitDie;
            creatureSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = 12;
            AddCreatureSkills(hitDie + 3);

            var skills = skillsGenerator.GenerateFor(creature).ToArray();
            var totalRanks = skills.Sum(s => s.Ranks);

            Assert.That(totalRanks, Is.EqualTo(skillPoints));

            Assert.That(skills[0].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skills[0].RankCap, Is.EqualTo(4));

            Assert.That(skills[1].Name, Is.EqualTo(creatureSkills[1]));
            Assert.That(skills[1].RankCap, Is.EqualTo(4));

            Assert.That(skills[2].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skills[2].RankCap, Is.EqualTo(4 + hitDie));

            Assert.That(skills[3].Name, Is.EqualTo(creatureSkills[1]));
            Assert.That(skills[3].RankCap, Is.EqualTo(4 + hitDie));

            for (var i = 4; i < skills.Length; i++)
                Assert.That(skills[i].RankCap, Is.EqualTo(hitDie + 3));
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
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        [TestCase(19)]
        [TestCase(20)]
        public void CannotHaveFewerThan1SkillPointPerHitDie(int hitDie)
        {
            creature.HitPoints.HitDiceQuantity = hitDie;
            creatureSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = -600;
            AddCreatureSkills(hitDie + 2);

            var skills = skillsGenerator.GenerateFor(creature);
            var totalRanks = skills.Sum(s => s.Ranks);
            Assert.That(totalRanks, Is.EqualTo(hitDie));
        }

        [Test]
        public void IfCreatureHasBaseAbility_GetSkill()
        {
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);

            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(() => new SkillSelection { BaseAbilityName = AbilityConstants.Constitution, SkillName = "skill 2" });

            var skills = skillsGenerator.GenerateFor(creature);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Contains.Item("skill 1"));
            Assert.That(skillNames, Contains.Item("skill 2"));
            Assert.That(skills.Count, Is.EqualTo(2));
        }

        [Test]
        public void IfCreatureDoesNotHaveBaseAbility_CannotGetSkill()
        {
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(() => new SkillSelection { BaseAbilityName = AbilityConstants.Constitution, SkillName = "skill 2" });

            var skills = skillsGenerator.GenerateFor(creature);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Contains.Item("skill 1"));
            Assert.That(skillNames, Is.All.Not.EqualTo("skill 2"));
            Assert.That(skills.Count, Is.EqualTo(2));
        }

        [Test]
        public void DoNotAssignSkillPointsToSkillsThatTheCreatureDoesNotHaveDueToNotHavingTheBaseAbility()
        {
            creatureSkillPoints = 1;
            creature.HitPoints.HitDiceQuantity = 2;

            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var constitutionSelection = new SkillSelection { BaseAbilityName = AbilityConstants.Constitution, SkillName = "skill 1" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(constitutionSelection);

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Ranks, Is.EqualTo(4));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(4));
        }

        [Test]
        public void DoNotAssignSkillPointsToSkillsIfCreatureDoesNotHaveRequiredBaseAbility()
        {
            creature.HitPoints.HitDiceQuantity = 20;
            creatureSkillPoints = 0;
            creatureSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = 10;
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var constitutionSelection = new SkillSelection { BaseAbilityName = AbilityConstants.Constitution, SkillName = "skill 1" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 1")).Returns(constitutionSelection);

            var skills = skillsGenerator.GenerateFor(creature).ToArray();
            var skill = skills.Single();

            Assert.That(skill.Name, Is.EqualTo("skill 2"));
            Assert.That(skill.Ranks, Is.EqualTo(5));
            Assert.That(skill.RanksMaxedOut, Is.True);
        }

        [Test]
        public void SelectPresetFocusForSkill()
        {
            creatureSkillPoints = 2;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var selection = new SkillSelection { BaseAbilityName = AbilityConstants.Charisma, Focus = "focus", SkillName = "skill with focus" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(selection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));

            Assert.That(skills[1].Name, Is.EqualTo("skill with focus"));
            Assert.That(skills[1].Focus, Is.EqualTo("focus"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));

            Assert.That(skills.Count, Is.EqualTo(2));
        }

        [Test]
        public void SelectRandomFocusForSkill()
        {
            creatureSkillPoints = 2;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var randomSelection = new SkillSelection { BaseAbilityName = AbilityConstants.Charisma, RandomFociQuantity = 1, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[1].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[1].Focus, Is.EqualTo("random"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills.Count, Is.EqualTo(2));
        }

        [Test]
        public void SelectMultipleRandomFociForSkill()
        {
            creature.HitPoints.HitDiceQuantity = 20;
            creatureSkillPoints = 0;
            creatureSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = 10;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var randomSelection = new SkillSelection { BaseAbilityName = AbilityConstants.Charisma, RandomFociQuantity = 2, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            var count = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[1].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[1].Focus, Is.EqualTo("random"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[2].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[2].Focus, Is.EqualTo("other random"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void DoNotSelectRepeatedRandomFociForSkill()
        {
            creature.HitPoints.HitDiceQuantity = 20;
            creatureSkillPoints = 0;
            creatureSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = 10;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var randomSelection = new SkillSelection { BaseAbilityName = AbilityConstants.Charisma, RandomFociQuantity = 2, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            var count = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(count++ / 2 % ss.Count()));

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[1].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[1].Focus, Is.EqualTo("random"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[2].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[2].Focus, Is.EqualTo("other random"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void SelectAllRandomFociForSkill()
        {
            creature.HitPoints.HitDiceQuantity = 20;
            creatureSkillPoints = 0;
            creatureSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseValue = 10;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var randomSelection = new SkillSelection { BaseAbilityName = AbilityConstants.Charisma, RandomFociQuantity = 3, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[1].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[1].Focus, Is.EqualTo("random"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[2].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[2].Focus, Is.EqualTo("other random"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void ProfessionSkillGrantsAdditionalSkills()
        {
            AddCreatureSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            var professionBonusSkillSelection = new SkillSelection();
            professionBonusSkillSelection.BaseAbilityName = "stat 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillSelection();
            professionBonusWithSetFocusSkillSelection.BaseAbilityName = "stat 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillSelection();
            professionBonusWithRandomFocusSkillSelection.BaseAbilityName = "stat 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "professional skill 3")).Returns(new[] { "random", "other random" });

            var professionSkills = new[] { "professional skill 1", "professional skill 2", "professional skill 3" };
            //mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.creatureSkills, "software developer")).Returns(professionSkills);

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("professional skill 1"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["stat 1"]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("professional skill 2"));
            Assert.That(skills[2].Focus, Is.EqualTo("set focus"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities["stat 2"]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills[3].Name, Is.EqualTo("professional skill 3"));
            Assert.That(skills[3].Focus, Is.EqualTo("random"));
            Assert.That(skills[3].BaseAbility, Is.EqualTo(abilities["stat 3"]));
            Assert.That(skills[3].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(4));
        }

        [Test]
        public void RandomProfessionSkillGrantsAdditionalSkills()
        {
            AddCreatureSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.RandomFociQuantity = 1;

            var professionBonusSkillSelection = new SkillSelection();
            professionBonusSkillSelection.BaseAbilityName = "stat 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillSelection();
            professionBonusWithSetFocusSkillSelection.BaseAbilityName = "stat 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillSelection();
            professionBonusWithRandomFocusSkillSelection.BaseAbilityName = "stat 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "professional skill 3")).Returns(new[] { "random", "other random" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, SkillConstants.Profession)).Returns(new[] { "random job", "other random job" });

            var professionSkills = new[] { "professional skill 1", "professional skill 2", "professional skill 3" };
            //mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.creatureSkills, "random job")).Returns(professionSkills);

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("random job"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("professional skill 1"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["stat 1"]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("professional skill 2"));
            Assert.That(skills[2].Focus, Is.EqualTo("set focus"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities["stat 2"]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills[3].Name, Is.EqualTo("professional skill 3"));
            Assert.That(skills[3].Focus, Is.EqualTo("other random"));
            Assert.That(skills[3].BaseAbility, Is.EqualTo(abilities["stat 3"]));
            Assert.That(skills[3].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(4));
        }

        [Test]
        public void ProfessionSkillGrantsNoDuplicatecreatureSkills()
        {
            AddCreatureSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            creatureSkills.Add("professional skill 1");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            var professionBonusSkillSelection = new SkillSelection();
            professionBonusSkillSelection.BaseAbilityName = "stat 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillSelection();
            professionBonusWithSetFocusSkillSelection.BaseAbilityName = "stat 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillSelection();
            professionBonusWithRandomFocusSkillSelection.BaseAbilityName = "stat 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillGroups, "professional skill 3")).Returns(new[] { "random", "other random" });

            var professionSkills = new[] { "professional skill 1", "professional skill 2", "professional skill 3" };
            //mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.creatureSkills, "software developer")).Returns(professionSkills);

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("professional skill 1"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["stat 1"]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("professional skill 2"));
            Assert.That(skills[2].Focus, Is.EqualTo("set focus"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities["stat 2"]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills[3].Name, Is.EqualTo("professional skill 3"));
            Assert.That(skills[3].Focus, Is.EqualTo("random"));
            Assert.That(skills[3].BaseAbility, Is.EqualTo(abilities["stat 3"]));
            Assert.That(skills[3].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(4));
        }

        [Test]
        public void ProfessionSkillGrantsNoAdditionalcreatureSkills()
        {
            AddCreatureSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(professionSkillSelection);

            var professionSkills = Enumerable.Empty<string>();
            //mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.creatureSkills, "software developer")).Returns(professionSkills);

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(1));
        }

        [Test]
        public void NoProfessionSkillGrantsNoAdditionalcreatureSkills()
        {
            AddCreatureSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            var otherSkillSelection = new SkillSelection();
            otherSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            otherSkillSelection.SkillName = "other skill";
            otherSkillSelection.Focus = "software developer";

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(otherSkillSelection);

            var professionSkills = new[] { "profession skill 1", "profession skill 2", "professional skill 3" };
            //mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.creatureSkills, "software developer")).Returns(professionSkills);

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("other skill"));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(1));
        }

        [Test]
        public void GetSynergyFromProfessionSkill()
        {
            creatureSkillPoints = 2;
            creature.HitPoints.HitDiceQuantity = 13;

            AddCreatureSkills(4);
            creatureSkills.Add("synergy 1");
            creatureSkills.Add("synergy 2");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(professionSkillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, $"{SkillConstants.Profession}/software developer")).Returns(new[] { "synergy 1", "synergy 2" });

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].Ranks, Is.EqualTo(6));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[0].Bonus, Is.EqualTo(0));

            Assert.That(skills[1].Name, Is.EqualTo("class skill 3"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].Ranks, Is.EqualTo(6));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("class skill 2"));
            Assert.That(skills[2].Focus, Is.Empty);
            Assert.That(skills[2].Ranks, Is.EqualTo(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills[3].Name, Is.EqualTo("class skill 1"));
            Assert.That(skills[3].Focus, Is.Empty);
            Assert.That(skills[3].Ranks, Is.EqualTo(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonus, Is.EqualTo(0));

            Assert.That(skills[4].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[4].Focus, Is.Empty);
            Assert.That(skills[4].Ranks, Is.EqualTo(5));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[4].Bonus, Is.EqualTo(2));

            Assert.That(skills[5].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[5].Focus, Is.Empty);
            Assert.That(skills[5].Ranks, Is.EqualTo(5));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[5].Bonus, Is.EqualTo(2));
        }

        [Test]
        public void ApplySynergyToProfessionSkill()
        {
            creatureSkillPoints = 2;
            creature.HitPoints.HitDiceQuantity = 13;

            AddCreatureSkills(4);
            creatureSkills.Add("synergy 1");
            creatureSkills.Add("synergy 2");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(professionSkillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SkillSynergy, creatureSkills[1])).Returns(new[] { "synergy 1", $"{SkillConstants.Profession}/software developer" });

            var skills = skillsGenerator.GenerateFor(creature).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].Ranks, Is.EqualTo(6));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[0].Bonus, Is.EqualTo(2));

            Assert.That(skills[1].Name, Is.EqualTo("class skill 3"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].Ranks, Is.EqualTo(6));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(6));
            Assert.That(skills[1].Bonus, Is.EqualTo(0));

            Assert.That(skills[2].Name, Is.EqualTo("class skill 2"));
            Assert.That(skills[2].Focus, Is.Empty);
            Assert.That(skills[2].Ranks, Is.EqualTo(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(0));

            Assert.That(skills[3].Name, Is.EqualTo("class skill 1"));
            Assert.That(skills[3].Focus, Is.Empty);
            Assert.That(skills[3].Ranks, Is.EqualTo(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonus, Is.EqualTo(0));

            Assert.That(skills[4].Name, Is.EqualTo("synergy 1"));
            Assert.That(skills[4].Focus, Is.Empty);
            Assert.That(skills[4].Ranks, Is.EqualTo(5));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[4].Bonus, Is.EqualTo(2));

            Assert.That(skills[5].Name, Is.EqualTo("synergy 2"));
            Assert.That(skills[5].Focus, Is.Empty);
            Assert.That(skills[5].Ranks, Is.EqualTo(5));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[5].Bonus, Is.EqualTo(0));
        }
    }
}