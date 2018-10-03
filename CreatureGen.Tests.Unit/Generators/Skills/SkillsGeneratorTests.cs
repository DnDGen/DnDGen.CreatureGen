using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
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
        private List<string> untrainedSkills;
        private Mock<ISkillSelector> mockSkillSelector;
        private int creatureTypeSkillPoints;
        private List<string> allSkills;
        private HitPoints hitPoints;
        private CreatureType creatureType;
        private List<string> unnaturalSkills;
        private string size;

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
            untrainedSkills = new List<string>();
            unnaturalSkills = new List<string>();
            hitPoints = new HitPoints();
            creatureType = new CreatureType();

            allSkills.Add("skill 1");
            allSkills.Add("skill 2");
            allSkills.Add("skill 3");
            allSkills.Add("skill 4");
            allSkills.Add("skill 5");

            hitPoints.HitDiceQuantity = 1;
            creatureType.Name = "creature type";
            size = SizeConstants.Medium;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, GroupConstants.All)).Returns(allSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "creature")).Returns(creatureSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, GroupConstants.Unnatural)).Returns(unnaturalSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, GroupConstants.Untrained)).Returns(untrainedSkills);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SkillPoints, creatureType.Name)).Returns(() => creatureTypeSkillPoints);

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> s) => s.ElementAt(index++ % s.Count()));
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<Skill>>())).Returns((IEnumerable<Skill> s) => s.ElementAt(index++ % s.Count()));
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => sk.ClassSkill)), It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => !sk.ClassSkill)), null, null))
                .Returns((IEnumerable<Skill> c, IEnumerable<Skill> u, IEnumerable<Skill> r, IEnumerable<Skill> v) => c.Union(u).ElementAt(index++ % c.Union(u).Count()));

            mockSkillSelector.Setup(s => s.SelectFor(It.IsAny<string>())).Returns((string skill) => new SkillSelection { SkillName = skill, BaseAbilityName = AbilityConstants.Intelligence });
        }

        [Test]
        public void GetSkills()
        {
            creatureTypeSkillPoints = 2;
            AddCreatureSkills(3);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
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
        public void GetNoSkills()
        {
            hitPoints.HitDiceQuantity = 0;
            creatureTypeSkillPoints = 2;
            AddCreatureSkills(3);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills, Is.Empty);
        }

        [Test]
        public void SkillHasArmorCheckPenalty()
        {
            creatureTypeSkillPoints = 2;
            AddCreatureSkills(3);

            var armorCheckSkills = new[] { "other skill", creatureSkills[0] };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, GroupConstants.ArmorCheckPenalty)).Returns(armorCheckSkills);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skill = skills.First(s => s.Name == creatureSkills[0]);
            Assert.That(skill.HasArmorCheckPenalty, Is.True);
        }

        [Test]
        public void SkillDoesNotHaveArmorCheckPenalty()
        {
            creatureTypeSkillPoints = 2;
            AddCreatureSkills(3);

            var armorCheckSkills = new[] { "other skill", "different skill" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, GroupConstants.ArmorCheckPenalty)).Returns(armorCheckSkills);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
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

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("class skill name"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities["ability 1"]));
        }

        [Test]
        public void SetClassSkill()
        {
            AddCreatureSkills(2);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);

            Assert.That(skills.Single(s => s.Name == "creature skill 1").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "creature skill 2").ClassSkill, Is.True);
        }

        [Test]
        public void GetUntrainedSkills()
        {
            AddUntrainedSkills(2);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);

            Assert.That(skills.Single(s => s.Name == "untrained skill 1").ClassSkill, Is.False);
            Assert.That(skills.Single(s => s.Name == "untrained skill 2").ClassSkill, Is.False);
        }

        private void AddUntrainedSkills(int quantity)
        {
            while (quantity > 0)
            {
                untrainedSkills.Add($"untrained skill {quantity--}");
            }
        }

        [Test]
        public void UntrainedSkillsBecomeClassSkills()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            creatureSkills.Add(untrainedSkills[1]);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);

            Assert.That(skills.Single(s => s.Name == "untrained skill 1").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "untrained skill 2").ClassSkill, Is.False);
            Assert.That(skills.Single(s => s.Name == "creature skill 2").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "creature skill 1").ClassSkill, Is.True);
        }

        [Test]
        public void UntrainedSkillsWithNameChangeBecomeClassSkills()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            creatureSkills.Add(untrainedSkills[0]);

            var skillSelection = new SkillSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "changed name";
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(untrainedSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);

            Assert.That(skills.Single(s => s.Name == "untrained skill 1").ClassSkill, Is.False);
            Assert.That(skills.Single(s => s.Name == "creature skill 2").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "creature skill 1").ClassSkill, Is.True);

            var convertedClassSkill = skills.Single(s => s.Name == "changed name");
            Assert.That(convertedClassSkill.ClassSkill, Is.True);
            Assert.That(convertedClassSkill.Name, Is.EqualTo("changed name"));
            Assert.That(convertedClassSkill.Focus, Is.EqualTo("random"));
            Assert.That(convertedClassSkill.ClassSkill, Is.True);
        }

        [Test]
        public void UntrainedSkillsWithRandomFocusBecomeClassSkills()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            creatureSkills.Add(untrainedSkills[0]);

            var skillSelection = new SkillSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "untrained skill 2";
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(untrainedSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);

            Assert.That(skills.Single(s => s.Name == "untrained skill 1").ClassSkill, Is.False);
            Assert.That(skills.Single(s => s.Name == "creature skill 2").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "creature skill 1").ClassSkill, Is.True);

            var convertedClassSkill = skills.Single(s => s.Name == "untrained skill 2");
            Assert.That(convertedClassSkill.ClassSkill, Is.True);
            Assert.That(convertedClassSkill.Name, Is.EqualTo("untrained skill 2"));
            Assert.That(convertedClassSkill.Focus, Is.EqualTo("random"));
            Assert.That(convertedClassSkill.ClassSkill, Is.True);
        }

        [Test]
        public void UntrainedSkillsRepeatingClassSkillsWithRandomFocusBecomeClassSkills()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);

            var skillSelection = new SkillSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = creatureSkills[0];
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(untrainedSkills[0])).Returns(skillSelection);
            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(skillSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);

            Assert.That(skills.Single(s => s.Name == "untrained skill 1").ClassSkill, Is.False);
            Assert.That(skills.Single(s => s.Name == "creature skill 1").ClassSkill, Is.True);

            var convertedClassSkills = skills.Where(s => s.Name == creatureSkills[0]).ToArray();
            Assert.That(convertedClassSkills[0].ClassSkill, Is.True);
            Assert.That(convertedClassSkills[0].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(convertedClassSkills[0].Focus, Is.EqualTo("random"));
            Assert.That(convertedClassSkills[0].ClassSkill, Is.True);

            Assert.That(convertedClassSkills[1].ClassSkill, Is.True);
            Assert.That(convertedClassSkills[1].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(convertedClassSkills[1].Focus, Is.EqualTo("other random"));
            Assert.That(convertedClassSkills[1].ClassSkill, Is.True);

            Assert.That(skills.Any(s => s.Name == untrainedSkills[0]), Is.False);
        }

        [Test]
        public void UntrainedSkillsWithSetFocusBecomeClassSkills()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            creatureSkills.Add(untrainedSkills[0]);

            var skillSelection = new SkillSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "untrained skill 2";
            skillSelection.Focus = "set focus";

            mockSkillSelector.Setup(s => s.SelectFor(untrainedSkills[0])).Returns(skillSelection);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);

            Assert.That(skills.Single(s => s.Name == "untrained skill 1").ClassSkill, Is.False);
            Assert.That(skills.Single(s => s.Name == "creature skill 2").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "creature skill 1").ClassSkill, Is.True);

            var convertedClassSkill = skills.Single(s => s.Name == "untrained skill 2");
            Assert.That(convertedClassSkill.ClassSkill, Is.True);
            Assert.That(convertedClassSkill.Name, Is.EqualTo("untrained skill 2"));
            Assert.That(convertedClassSkill.Focus, Is.EqualTo("set focus"));
            Assert.That(convertedClassSkill.ClassSkill, Is.True);
        }

        [Test]
        public void UntrainedSkillsWithMultipleRandomFociBecomeClassSkills()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            creatureSkills.Add(untrainedSkills[0]);

            var skillSelection = new SkillSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "untrained skill 2";
            skillSelection.RandomFociQuantity = 2;

            mockSkillSelector.Setup(s => s.SelectFor(untrainedSkills[0])).Returns(skillSelection);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "another random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);

            Assert.That(skills.Single(s => s.Name == "untrained skill 1").ClassSkill, Is.False);
            Assert.That(skills.Single(s => s.Name == "creature skill 2").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "creature skill 1").ClassSkill, Is.True);

            var convertedClassSkills = skills.Where(s => s.Name == "untrained skill 2").ToArray();
            Assert.That(convertedClassSkills[0].ClassSkill, Is.True);
            Assert.That(convertedClassSkills[0].Name, Is.EqualTo("untrained skill 2"));
            Assert.That(convertedClassSkills[0].Focus, Is.EqualTo("random"));
            Assert.That(convertedClassSkills[0].ClassSkill, Is.True);

            Assert.That(convertedClassSkills[1].ClassSkill, Is.True);
            Assert.That(convertedClassSkills[1].Name, Is.EqualTo("untrained skill 2"));
            Assert.That(convertedClassSkills[1].Focus, Is.EqualTo("another random"));
            Assert.That(convertedClassSkills[1].ClassSkill, Is.True);

            Assert.That(convertedClassSkills.Length, Is.EqualTo(2));
        }

        [Test]
        public void AssignRankToClassSkill()
        {
            AddCreatureSkills(3);
            AddUntrainedSkills(3);

            var index = 0;
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => sk.ClassSkill)), It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => !sk.ClassSkill)), null, null))
                .Returns((IEnumerable<Skill> c, IEnumerable<Skill> u, IEnumerable<Skill> r, IEnumerable<Skill> v) => c.ElementAt(index++ % c.Count()));

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("untrained skill 3"));
            Assert.That(skills[0].ClassSkill, Is.False);
            Assert.That(skills[0].Ranks, Is.Zero);
            Assert.That(skills[0].EffectiveRanks, Is.Zero);
            Assert.That(skills[1].Name, Is.EqualTo("untrained skill 2"));
            Assert.That(skills[1].ClassSkill, Is.False);
            Assert.That(skills[1].Ranks, Is.Zero);
            Assert.That(skills[1].EffectiveRanks, Is.Zero);
            Assert.That(skills[2].Name, Is.EqualTo("untrained skill 1"));
            Assert.That(skills[2].ClassSkill, Is.False);
            Assert.That(skills[2].Ranks, Is.Zero);
            Assert.That(skills[2].EffectiveRanks, Is.Zero);
            Assert.That(skills[3].Name, Is.EqualTo("creature skill 3"));
            Assert.That(skills[3].ClassSkill, Is.True);
            Assert.That(skills[3].Ranks, Is.EqualTo(3));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[4].Name, Is.EqualTo("creature skill 2"));
            Assert.That(skills[4].ClassSkill, Is.True);
            Assert.That(skills[4].Ranks, Is.EqualTo(3));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[5].Name, Is.EqualTo("creature skill 1"));
            Assert.That(skills[5].ClassSkill, Is.True);
            Assert.That(skills[5].Ranks, Is.EqualTo(2));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills.Length, Is.EqualTo(6));
        }

        [Test]
        public void AssignRankToUntrainedSkill()
        {
            AddCreatureSkills(3);
            AddUntrainedSkills(3);

            var index = 0;
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => sk.ClassSkill)), It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => !sk.ClassSkill)), null, null))
                .Returns((IEnumerable<Skill> c, IEnumerable<Skill> u, IEnumerable<Skill> r, IEnumerable<Skill> v) => u.ElementAt(index++ % u.Count()));

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("untrained skill 3"));
            Assert.That(skills[0].ClassSkill, Is.False);
            Assert.That(skills[0].Ranks, Is.EqualTo(3));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(1.5));
            Assert.That(skills[1].Name, Is.EqualTo("untrained skill 2"));
            Assert.That(skills[1].ClassSkill, Is.False);
            Assert.That(skills[1].Ranks, Is.EqualTo(3));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(1.5));
            Assert.That(skills[2].Name, Is.EqualTo("untrained skill 1"));
            Assert.That(skills[2].ClassSkill, Is.False);
            Assert.That(skills[2].Ranks, Is.EqualTo(2));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(1));
            Assert.That(skills[3].Name, Is.EqualTo("creature skill 3"));
            Assert.That(skills[3].ClassSkill, Is.True);
            Assert.That(skills[3].Ranks, Is.Zero);
            Assert.That(skills[3].EffectiveRanks, Is.Zero);
            Assert.That(skills[4].Name, Is.EqualTo("creature skill 2"));
            Assert.That(skills[4].ClassSkill, Is.True);
            Assert.That(skills[4].Ranks, Is.Zero);
            Assert.That(skills[4].EffectiveRanks, Is.Zero);
            Assert.That(skills[5].Name, Is.EqualTo("creature skill 1"));
            Assert.That(skills[5].ClassSkill, Is.True);
            Assert.That(skills[5].Ranks, Is.Zero);
            Assert.That(skills[5].EffectiveRanks, Is.Zero);
            Assert.That(skills.Length, Is.EqualTo(6));
        }

        [Test]
        public void AssignRankToClassAndUntrainedSkill()
        {
            AddCreatureSkills(3);
            AddUntrainedSkills(3);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("untrained skill 3"));
            Assert.That(skills[0].ClassSkill, Is.False);
            Assert.That(skills[0].Ranks, Is.EqualTo(1));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(.5));
            Assert.That(skills[1].Name, Is.EqualTo("untrained skill 2"));
            Assert.That(skills[1].ClassSkill, Is.False);
            Assert.That(skills[1].Ranks, Is.EqualTo(1));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(.5));
            Assert.That(skills[2].Name, Is.EqualTo("untrained skill 1"));
            Assert.That(skills[2].ClassSkill, Is.False);
            Assert.That(skills[2].Ranks, Is.EqualTo(1));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(.5));
            Assert.That(skills[3].Name, Is.EqualTo("creature skill 3"));
            Assert.That(skills[3].ClassSkill, Is.True);
            Assert.That(skills[3].Ranks, Is.EqualTo(2));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[4].Name, Is.EqualTo("creature skill 2"));
            Assert.That(skills[4].ClassSkill, Is.True);
            Assert.That(skills[4].Ranks, Is.EqualTo(2));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[5].Name, Is.EqualTo("creature skill 1"));
            Assert.That(skills[5].ClassSkill, Is.True);
            Assert.That(skills[5].Ranks, Is.EqualTo(1));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(1));
            Assert.That(skills.Length, Is.EqualTo(6));
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

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
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
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
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
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

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
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt((index++ / 2) % ss.Count()));

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

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
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, skillSelection.SkillName)).Returns(new[] { "random", "other random", "third random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

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
            creatureTypeSkillPoints = 3;
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);

            Assert.That(skills.Single(s => s.Name == "skill 1").Ranks, Is.EqualTo(hitPoints.HitDiceQuantity + 3));
            Assert.That(skills.Single(s => s.Name == "skill 2").Ranks, Is.EqualTo(hitPoints.HitDiceQuantity + 3));
        }

        [Test]
        public void IfCannotUseEquipment_DoNotGetUntrainedUnnaturalSkill()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            unnaturalSkills.Add(untrainedSkills[0]);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, false, size);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Is.All.Not.EqualTo(untrainedSkills[0]));
        }

        [Test]
        public void IfCannotUseEquipment_GetTrainedUnnaturalSkill()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            unnaturalSkills.Add(creatureSkills[0]);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, false, size);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Contains.Item(creatureSkills[0]));
        }

        [Test]
        public void IfCanUseEquipment_GetUntrainedUnnaturalSkill()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            unnaturalSkills.Add(untrainedSkills[0]);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Contains.Item(untrainedSkills[0]));
        }

        [Test]
        public void IfCanUseEquipment_GetTrainedUnnaturalSkill()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            unnaturalSkills.Add(creatureSkills[0]);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Contains.Item(creatureSkills[0]));
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
        [TestCase(1, 8, 32)]
        [TestCase(2, 8, 40)]
        [TestCase(3, 8, 48)]
        [TestCase(4, 8, 56)]
        [TestCase(5, 8, 64)]
        [TestCase(6, 8, 72)]
        [TestCase(7, 8, 80)]
        [TestCase(8, 8, 88)]
        [TestCase(9, 8, 96)]
        [TestCase(10, 8, 104)]
        [TestCase(11, 8, 112)]
        [TestCase(12, 8, 120)]
        [TestCase(13, 8, 128)]
        [TestCase(14, 8, 136)]
        [TestCase(15, 8, 144)]
        [TestCase(16, 8, 152)]
        [TestCase(17, 8, 160)]
        [TestCase(18, 8, 168)]
        [TestCase(19, 8, 176)]
        [TestCase(20, 8, 184)]
        public void SkillPointsDeterminedByHitDice(int hitDiceQuantity, int skillPointsPerDie, int skillPoints)
        {
            hitPoints.HitDiceQuantity = hitDiceQuantity;
            creatureTypeSkillPoints = skillPointsPerDie;
            AddCreatureSkills(hitDiceQuantity + skillPointsPerDie);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var totalRanks = skills.Sum(s => s.Ranks);

            Assert.That(totalRanks, Is.EqualTo(skillPoints));
        }

        [TestCase(0, 2, 0)]
        [TestCase(1, 2, 4)]
        [TestCase(2, 2, 5)]
        [TestCase(3, 2, 6)]
        [TestCase(4, 2, 7)]
        [TestCase(5, 2, 8)]
        [TestCase(6, 2, 9)]
        [TestCase(7, 2, 10)]
        [TestCase(8, 2, 11)]
        [TestCase(9, 2, 12)]
        [TestCase(10, 2, 13)]
        [TestCase(11, 2, 14)]
        [TestCase(12, 2, 15)]
        [TestCase(13, 2, 16)]
        [TestCase(14, 2, 17)]
        [TestCase(15, 2, 18)]
        [TestCase(16, 2, 19)]
        [TestCase(17, 2, 20)]
        [TestCase(18, 2, 21)]
        [TestCase(19, 2, 22)]
        [TestCase(20, 2, 23)]
        [TestCase(0, 8, 0)]
        [TestCase(1, 8, 4)]
        [TestCase(2, 8, 5)]
        [TestCase(3, 8, 6)]
        [TestCase(4, 8, 7)]
        [TestCase(5, 8, 8)]
        [TestCase(6, 8, 9)]
        [TestCase(7, 8, 10)]
        [TestCase(8, 8, 11)]
        [TestCase(9, 8, 12)]
        [TestCase(10, 8, 13)]
        [TestCase(11, 8, 14)]
        [TestCase(12, 8, 15)]
        [TestCase(13, 8, 16)]
        [TestCase(14, 8, 17)]
        [TestCase(15, 8, 18)]
        [TestCase(16, 8, 19)]
        [TestCase(17, 8, 20)]
        [TestCase(18, 8, 21)]
        [TestCase(19, 8, 22)]
        [TestCase(20, 8, 23)]
        [TestCase(0, 10, 0)]
        [TestCase(1, 10, 8)]
        [TestCase(2, 10, 10)]
        [TestCase(3, 10, 12)]
        [TestCase(4, 10, 14)]
        [TestCase(5, 10, 16)]
        [TestCase(6, 10, 18)]
        [TestCase(7, 10, 20)]
        [TestCase(8, 10, 22)]
        [TestCase(9, 10, 24)]
        [TestCase(10, 10, 26)]
        [TestCase(11, 10, 28)]
        [TestCase(12, 10, 30)]
        [TestCase(13, 10, 32)]
        [TestCase(14, 10, 34)]
        [TestCase(15, 10, 36)]
        [TestCase(16, 10, 38)]
        [TestCase(17, 10, 40)]
        [TestCase(18, 10, 42)]
        [TestCase(19, 10, 44)]
        [TestCase(20, 10, 46)]
        [TestCase(0, 12, 0)]
        [TestCase(1, 12, 12)]
        [TestCase(2, 12, 15)]
        [TestCase(3, 12, 18)]
        [TestCase(4, 12, 21)]
        [TestCase(5, 12, 24)]
        [TestCase(6, 12, 27)]
        [TestCase(7, 12, 30)]
        [TestCase(8, 12, 33)]
        [TestCase(9, 12, 36)]
        [TestCase(10, 12, 39)]
        [TestCase(11, 12, 42)]
        [TestCase(12, 12, 45)]
        [TestCase(13, 12, 48)]
        [TestCase(14, 12, 51)]
        [TestCase(15, 12, 54)]
        [TestCase(16, 12, 57)]
        [TestCase(17, 12, 60)]
        [TestCase(18, 12, 63)]
        [TestCase(19, 12, 66)]
        [TestCase(20, 12, 69)]
        [TestCase(0, 18, 0)]
        [TestCase(1, 18, 24)]
        [TestCase(2, 18, 30)]
        [TestCase(3, 18, 36)]
        [TestCase(4, 18, 42)]
        [TestCase(5, 18, 48)]
        [TestCase(6, 18, 54)]
        [TestCase(7, 18, 60)]
        [TestCase(8, 18, 66)]
        [TestCase(9, 18, 72)]
        [TestCase(10, 18, 78)]
        [TestCase(11, 18, 84)]
        [TestCase(12, 18, 90)]
        [TestCase(13, 18, 96)]
        [TestCase(14, 18, 102)]
        [TestCase(15, 18, 108)]
        [TestCase(16, 18, 114)]
        [TestCase(17, 18, 120)]
        [TestCase(18, 18, 126)]
        [TestCase(19, 18, 132)]
        [TestCase(20, 18, 138)]
        public void ApplyIntelligenceBonusToSkillPoints(int hitDie, int intelligence, int skillPoints)
        {
            hitPoints.HitDiceQuantity = hitDie;
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseScore = intelligence;
            AddCreatureSkills(hitDie + intelligence);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var totalRanks = skills.Sum(s => s.Ranks);

            Assert.That(totalRanks, Is.EqualTo(skillPoints));
        }

        [Test]
        public void IfCreatureHasBaseAbility_GetSkill()
        {
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);

            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(() => new SkillSelection { BaseAbilityName = AbilityConstants.Constitution, SkillName = "skill 2" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
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

            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution) { BaseScore = 0 };

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Contains.Item("skill 1"));
            Assert.That(skillNames, Is.All.Not.EqualTo("skill 2"));
            Assert.That(skills.Count, Is.EqualTo(1));
        }

        [Test]
        public void DoNotAssignSkillPointsToSkillsThatTheCreatureDoesNotHaveDueToNotHavingTheBaseAbility()
        {
            creatureTypeSkillPoints = 1;
            hitPoints.HitDiceQuantity = 2;

            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var constitutionSelection = new SkillSelection { BaseAbilityName = AbilityConstants.Constitution, SkillName = "skill 1" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(constitutionSelection);

            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution) { BaseScore = 0 };

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Ranks, Is.EqualTo(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(5));
        }

        [TestCase("skill 1Intelligence")]
        [TestCase("skill 1All")]
        [TestCase("skill 19266")]
        public void CreatureSkillWithDifferentNameIsClassSkill(string creatureSkill)
        {
            creatureTypeSkillPoints = 2;
            creatureSkills.Add(creatureSkill);

            var selection = new SkillSelection { BaseAbilityName = AbilityConstants.Intelligence, SkillName = "skill 1" };
            mockSkillSelector.Setup(s => s.SelectFor("creature skill")).Returns(selection);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(1));

            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo("skill 1"));
            Assert.That(skill.Focus, Is.Empty);
            Assert.That(skill.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void SelectPresetFocusForSkill()
        {
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var selection = new SkillSelection { BaseAbilityName = AbilityConstants.Charisma, Focus = "focus", SkillName = "skill with focus" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(selection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("skill with focus"));
            Assert.That(skills[1].Focus, Is.EqualTo("focus"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills.Count, Is.EqualTo(2));
        }

        [Test]
        public void SelectRandomFocusForSkill()
        {
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var randomSelection = new SkillSelection { BaseAbilityName = AbilityConstants.Charisma, RandomFociQuantity = 1, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[1].Focus, Is.EqualTo("random"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills.Count, Is.EqualTo(2));
        }

        [Test]
        public void SelectMultipleRandomFociForSkill()
        {
            hitPoints.HitDiceQuantity = 20;
            creatureTypeSkillPoints = 0;
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseScore = 10;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var randomSelection = new SkillSelection { BaseAbilityName = AbilityConstants.Charisma, RandomFociQuantity = 2, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            var count = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[1].Focus, Is.EqualTo("random"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[2].Focus, Is.EqualTo("other random"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void DoNotSelectRepeatedRandomFociForSkill()
        {
            hitPoints.HitDiceQuantity = 20;
            creatureTypeSkillPoints = 0;
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseScore = 10;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var randomSelection = new SkillSelection { BaseAbilityName = AbilityConstants.Charisma, RandomFociQuantity = 2, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            var count = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(count++ / 2 % ss.Count()));

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[1].Focus, Is.EqualTo("random"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[2].Focus, Is.EqualTo("other random"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void SelectAllRandomFociForSkill()
        {
            hitPoints.HitDiceQuantity = 20;
            creatureTypeSkillPoints = 0;
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseScore = 10;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var randomSelection = new SkillSelection { BaseAbilityName = AbilityConstants.Charisma, RandomFociQuantity = 3, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectFor("skill 2")).Returns(randomSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "skill with random foci")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[1].Focus, Is.EqualTo("random"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("skill with random foci"));
            Assert.That(skills[2].Focus, Is.EqualTo("other random"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills.Count, Is.EqualTo(3));
        }

        [Test]
        public void ProfessionSkillGrantsAdditionalSkills()
        {
            AddCreatureSkills(1);
            creatureSkills.Add("professional skill 1");
            creatureSkills.Add("professional skill 2");
            creatureSkills.Add("professional skill 3");

            abilities["ability 1"] = new Ability("ability 1");
            abilities["ability 2"] = new Ability("ability 2");
            abilities["ability 3"] = new Ability("ability 3");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            var professionBonusSkillSelection = new SkillSelection();
            professionBonusSkillSelection.BaseAbilityName = "ability 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillSelection();
            professionBonusWithSetFocusSkillSelection.BaseAbilityName = "ability 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillSelection();
            professionBonusWithRandomFocusSkillSelection.BaseAbilityName = "ability 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "professional skill 3")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("professional skill 1"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["ability 1"]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("professional skill 2"));
            Assert.That(skills[2].Focus, Is.EqualTo("set focus"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities["ability 2"]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills[3].Name, Is.EqualTo("professional skill 3"));
            Assert.That(skills[3].Focus, Is.EqualTo("random"));
            Assert.That(skills[3].BaseAbility, Is.EqualTo(abilities["ability 3"]));
            Assert.That(skills[3].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(4));
        }

        [Test]
        public void RandomProfessionSkillGrantsAdditionalSkills()
        {
            AddCreatureSkills(1);
            creatureSkills.Add("professional skill 1");
            creatureSkills.Add("professional skill 2");
            creatureSkills.Add("professional skill 3");

            abilities["ability 1"] = new Ability("ability 1");
            abilities["ability 2"] = new Ability("ability 2");
            abilities["ability 3"] = new Ability("ability 3");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.RandomFociQuantity = 1;

            var professionBonusSkillSelection = new SkillSelection();
            professionBonusSkillSelection.BaseAbilityName = "ability 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillSelection();
            professionBonusWithSetFocusSkillSelection.BaseAbilityName = "ability 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillSelection();
            professionBonusWithRandomFocusSkillSelection.BaseAbilityName = "ability 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "professional skill 3")).Returns(new[] { "random", "other random" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, SkillConstants.Profession)).Returns(new[] { "random job", "other random job" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("random job"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("professional skill 1"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["ability 1"]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("professional skill 2"));
            Assert.That(skills[2].Focus, Is.EqualTo("set focus"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities["ability 2"]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills[3].Name, Is.EqualTo("professional skill 3"));
            Assert.That(skills[3].Focus, Is.EqualTo("other random"));
            Assert.That(skills[3].BaseAbility, Is.EqualTo(abilities["ability 3"]));
            Assert.That(skills[3].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(4));
        }

        [Test]
        public void ProfessionSkillGrantsNoDuplicateCreatureSkills()
        {
            AddCreatureSkills(1);
            creatureSkills.Add("professional skill 1");
            creatureSkills.Add("professional skill 2");
            creatureSkills.Add("professional skill 3");

            abilities["ability 1"] = new Ability("ability 1");
            abilities["ability 2"] = new Ability("ability 2");
            abilities["ability 3"] = new Ability("ability 3");

            var professionSkillSelection = new SkillSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            var professionBonusSkillSelection = new SkillSelection();
            professionBonusSkillSelection.BaseAbilityName = "ability 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillSelection();
            professionBonusWithSetFocusSkillSelection.BaseAbilityName = "ability 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillSelection();
            professionBonusWithRandomFocusSkillSelection.BaseAbilityName = "ability 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor("professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "professional skill 3")).Returns(new[] { "random", "other random" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills[1].Name, Is.EqualTo("professional skill 1"));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["ability 1"]));
            Assert.That(skills[1].ClassSkill, Is.True);

            Assert.That(skills[2].Name, Is.EqualTo("professional skill 2"));
            Assert.That(skills[2].Focus, Is.EqualTo("set focus"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities["ability 2"]));
            Assert.That(skills[2].ClassSkill, Is.True);

            Assert.That(skills[3].Name, Is.EqualTo("professional skill 3"));
            Assert.That(skills[3].Focus, Is.EqualTo("random"));
            Assert.That(skills[3].BaseAbility, Is.EqualTo(abilities["ability 3"]));
            Assert.That(skills[3].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(4));
        }

        [Test]
        public void ProfessionSkillGrantsNoAdditionalCreatureSkills()
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

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(1));
        }

        [Test]
        public void NoProfessionSkillGrantsNoAdditionalCreatureSkills()
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
            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("other skill"));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(1));
        }

        [Test]
        public void CreaturesWithoutIntelligenceReceiveNoRanksForSkills()
        {
            abilities[AbilityConstants.Intelligence].BaseScore = 0;
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);

            AddUntrainedSkills(4);

            creatureTypeSkillPoints = 10;
            hitPoints.HitDiceQuantity = 10;

            mockSkillSelector.Setup(s => s.SelectFor(It.IsAny<string>())).Returns((string skill) => new SkillSelection { SkillName = skill, BaseAbilityName = AbilityConstants.Strength });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(4));

            var ranks = skills.Select(s => s.Ranks);
            Assert.That(ranks, Is.All.Zero);
        }

        [TestCase(SizeConstants.Colossal, -16)]
        [TestCase(SizeConstants.Gargantuan, -12)]
        [TestCase(SizeConstants.Huge, -8)]
        [TestCase(SizeConstants.Large, -4)]
        [TestCase(SizeConstants.Medium, 0)]
        [TestCase(SizeConstants.Small, 4)]
        [TestCase(SizeConstants.Tiny, 8)]
        [TestCase(SizeConstants.Diminutive, 12)]
        [TestCase(SizeConstants.Fine, 16)]
        public void ApplySizeModiferToHideSkill(string creatureSize, int bonus)
        {
            AddCreatureSkills(1);
            AddUntrainedSkills(1);
            untrainedSkills.Add(SkillConstants.Hide);
            size = creatureSize;

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var hideSkill = skills.Single(s => s.Name == SkillConstants.Hide);
            Assert.That(hideSkill.Bonus, Is.EqualTo(bonus), size);

            var otherSkills = skills.Except(new[] { hideSkill });
            var otherBonuses = otherSkills.Select(s => s.Bonus);
            Assert.That(otherBonuses, Is.All.Zero);
        }

        [Test]
        public void ApplyNoSkillBonus()
        {
            AddCreatureSkills(1);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(1));

            var skill = skills.Single();
            Assert.That(skill.Bonus, Is.Zero);
            Assert.That(skill.Bonuses, Is.Empty);
        }

        [Test]
        public void ApplySkillBonus()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusSelection { Source = creatureSkills[0], Bonus = 9266 }
            };
            mockSkillSelector.Setup(s => s.SelectBonusesFor("creature")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.Bonus, Is.EqualTo(9266));
            Assert.That(skill.Bonuses, Is.Not.Empty);

            var bonus = skill.Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            var wrongSkill = skills.Last();
            Assert.That(wrongSkill.Bonus, Is.Zero);
            Assert.That(wrongSkill.Bonuses, Is.Empty);
        }

        [Test]
        public void ApplySkillBonuses()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusSelection { Source = creatureSkills[0], Bonus = 9266 },
                new BonusSelection { Source = creatureSkills[1], Bonus = 90210 },
            };
            mockSkillSelector.Setup(s => s.SelectBonusesFor("creature")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.Bonus, Is.EqualTo(9266));
            Assert.That(skill.Bonuses, Is.Not.Empty);

            var bonus = skill.Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            var wrongSkill = skills.Last();
            Assert.That(wrongSkill.Bonus, Is.EqualTo(90210));
            Assert.That(wrongSkill.Bonuses, Is.Not.Empty);

            bonus = wrongSkill.Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(90210));
        }

        [Test]
        public void ApplySkillBonusesToSameSkill()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusSelection { Source = creatureSkills[0], Bonus = 9266 },
                new BonusSelection { Source = creatureSkills[0], Bonus = 90210 }
            };
            mockSkillSelector.Setup(s => s.SelectBonusesFor("creature")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.Bonus, Is.EqualTo(9266 + 90210));
            Assert.That(skill.Bonuses, Is.Not.Empty);
            Assert.That(skill.Bonuses.Count, Is.EqualTo(2));

            var bonus = skill.Bonuses.First();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            bonus = skill.Bonuses.Last();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(90210));

            var wrongSkill = skills.Last();
            Assert.That(wrongSkill.Bonus, Is.Zero);
            Assert.That(wrongSkill.Bonuses, Is.Empty);
        }

        [Test]
        public void ApplySkillBonusWithCondition()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusSelection { Source = creatureSkills[0], Bonus = 9266, Condition = "condition" }
            };
            mockSkillSelector.Setup(s => s.SelectBonusesFor("creature")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.Bonus, Is.Zero);
            Assert.That(skill.Bonuses, Is.Not.Empty);

            var bonus = skill.Bonuses.Single();
            Assert.That(bonus.Condition, Is.EqualTo("condition"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            var wrongSkill = skills.Last();
            Assert.That(wrongSkill.Bonus, Is.Zero);
            Assert.That(wrongSkill.Bonuses, Is.Empty);
        }

        [Test]
        public void ApplySkillBonusesWithCondition()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusSelection { Source = creatureSkills[0], Bonus = 9266, Condition = "condition" },
                new BonusSelection { Source = creatureSkills[0], Bonus = 90210, Condition = "other condition" }
            };
            mockSkillSelector.Setup(s => s.SelectBonusesFor("creature")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.Bonus, Is.Zero);
            Assert.That(skill.Bonuses, Is.Not.Empty);
            Assert.That(skill.Bonuses.Count, Is.EqualTo(2));

            var bonus = skill.Bonuses.First();
            Assert.That(bonus.Condition, Is.EqualTo("condition"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            bonus = skill.Bonuses.Last();
            Assert.That(bonus.Condition, Is.EqualTo("other condition"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(90210));

            var wrongSkill = skills.Last();
            Assert.That(wrongSkill.Bonus, Is.Zero);
            Assert.That(wrongSkill.Bonuses, Is.Empty);
        }

        [Test]
        public void ApplySkillBonusesWithAndWithoutCondition()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusSelection { Source = creatureSkills[0], Bonus = 9266, Condition = "condition" },
                new BonusSelection { Source = creatureSkills[0], Bonus = 90210 }
            };
            mockSkillSelector.Setup(s => s.SelectBonusesFor("creature")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.Bonus, Is.EqualTo(90210));
            Assert.That(skill.Bonuses, Is.Not.Empty);
            Assert.That(skill.Bonuses.Count, Is.EqualTo(2));

            var bonus = skill.Bonuses.First();
            Assert.That(bonus.Condition, Is.EqualTo("condition"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            bonus = skill.Bonuses.Last();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(90210));

            var wrongSkill = skills.Last();
            Assert.That(wrongSkill.Bonus, Is.Zero);
            Assert.That(wrongSkill.Bonuses, Is.Empty);
        }

        [Test]
        public void ApplySkillBonusWithFocus()
        {
            AddCreatureSkills(2);

            var skillSelection = new SkillSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "skill";
            skillSelection.Focus = "focus";

            var wrongSkillSelection = new SkillSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "skill";
            wrongSkillSelection.Focus = "other focus";

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(skillSelection);
            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[1])).Returns(wrongSkillSelection);

            var bonuses = new[]
            {
                new BonusSelection { Source = SkillConstants.Build("skill", "focus"), Bonus = 9266 }
            };
            mockSkillSelector.Setup(s => s.SelectBonusesFor("creature")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.Bonus, Is.EqualTo(9266));
            Assert.That(skill.Bonuses, Is.Not.Empty);

            var bonus = skill.Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            var wrongSkill = skills.Last();
            Assert.That(wrongSkill.Bonus, Is.Zero);
            Assert.That(wrongSkill.Bonuses, Is.Empty);
        }

        [Test]
        public void ApplySkillBonusWithFocusAndCondition()
        {
            AddCreatureSkills(2);

            var skillSelection = new SkillSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "skill";
            skillSelection.Focus = "focus";

            var wrongSkillSelection = new SkillSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "skill";
            wrongSkillSelection.Focus = "other focus";

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(skillSelection);
            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[1])).Returns(wrongSkillSelection);

            var bonuses = new[]
            {
                new BonusSelection { Source = SkillConstants.Build("skill", "focus"), Bonus = 9266, Condition = "condition" }
            };
            mockSkillSelector.Setup(s => s.SelectBonusesFor("creature")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.Bonus, Is.Zero);
            Assert.That(skill.Bonuses, Is.Not.Empty);

            var bonus = skill.Bonuses.Single();
            Assert.That(bonus.Condition, Is.EqualTo("condition"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            var wrongSkill = skills.Last();
            Assert.That(wrongSkill.Bonus, Is.Zero);
            Assert.That(wrongSkill.Bonuses, Is.Empty);
        }

        [Test]
        public void ApplySkillBonusToMultipleSkills()
        {
            AddCreatureSkills(3);

            var skillSelection1 = new SkillSelection();
            skillSelection1.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection1.SkillName = "skill";
            skillSelection1.Focus = "focus";

            var skillSelection2 = new SkillSelection();
            skillSelection2.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection2.SkillName = "skill";
            skillSelection2.Focus = "other focus";

            var wrongSkillSelection = new SkillSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "other skill";

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(skillSelection1);
            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[1])).Returns(wrongSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[2])).Returns(skillSelection2);

            var bonuses = new[]
            {
                new BonusSelection { Source = "skill", Bonus = 9266 }
            };
            mockSkillSelector.Setup(s => s.SelectBonusesFor("creature")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills.Count, Is.EqualTo(3));

            Assert.That(skills[0].Bonus, Is.EqualTo(9266));
            Assert.That(skills[0].Bonuses, Is.Not.Empty);

            var bonus = skills[0].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            Assert.That(skills[1].Bonus, Is.Zero);
            Assert.That(skills[1].Bonuses, Is.Empty);

            Assert.That(skills[2].Bonus, Is.EqualTo(9266));
            Assert.That(skills[2].Bonuses, Is.Not.Empty);

            bonus = skills[2].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [Test]
        public void ApplySkillBonusWithConditionToMultipleSkills()
        {
            AddCreatureSkills(3);

            var skillSelection1 = new SkillSelection();
            skillSelection1.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection1.SkillName = "skill";
            skillSelection1.Focus = "focus";

            var skillSelection2 = new SkillSelection();
            skillSelection2.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection2.SkillName = "skill";
            skillSelection2.Focus = "other focus";

            var wrongSkillSelection = new SkillSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "other skill";

            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[0])).Returns(skillSelection1);
            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[1])).Returns(wrongSkillSelection);
            mockSkillSelector.Setup(s => s.SelectFor(creatureSkills[2])).Returns(skillSelection2);

            var bonuses = new[]
            {
                new BonusSelection { Source = "skill", Bonus = 9266, Condition = "condition" }
            };
            mockSkillSelector.Setup(s => s.SelectBonusesFor("creature")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills.Count, Is.EqualTo(3));

            Assert.That(skills[0].Bonus, Is.Zero);
            Assert.That(skills[0].Bonuses, Is.Not.Empty);

            var bonus = skills[0].Bonuses.Single();
            Assert.That(bonus.Condition, Is.EqualTo("condition"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            Assert.That(skills[1].Bonus, Is.Zero);
            Assert.That(skills[1].Bonuses, Is.Empty);

            Assert.That(skills[2].Bonus, Is.Zero);
            Assert.That(skills[2].Bonuses, Is.Not.Empty);

            bonus = skills[2].Bonuses.Single();
            Assert.That(bonus.Condition, Is.EqualTo("condition"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [Test]
        public void ApplyDuplicateSkillBonuses()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusSelection { Source = creatureSkills[0], Bonus = 9266 },
                new BonusSelection { Source = creatureSkills[0], Bonus = 9266 }
            };
            mockSkillSelector.Setup(s => s.SelectBonusesFor("creature")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.Bonus, Is.EqualTo(9266 + 9266));
            Assert.That(skill.Bonuses, Is.Not.Empty);
            Assert.That(skill.Bonuses.Count, Is.EqualTo(2));

            var bonus = skill.Bonuses.First();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            bonus = skill.Bonuses.Last();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            var wrongSkill = skills.Last();
            Assert.That(wrongSkill.Bonus, Is.Zero);
            Assert.That(wrongSkill.Bonuses, Is.Empty);
        }

        //INFO: Example is how a centipede swarm uses Dexterity for Climb instead of Strength
        [Test]
        public void UseAlternateBaseAbilityForSkill()
        {
            abilities["base ability"] = new Ability("base ability");
            abilities["other base ability"] = new Ability("other base ability");

            var skills = new List<Skill>();
            skills.Add(new Skill("skill 1", abilities["base ability"], 1));
            skills.Add(new Skill("skill 2", abilities["base ability"], 1));

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.SpecialQualities.SwapSkillBaseAbility;
            feats[0].Foci = new[] { $"{skills[1].Name}:{abilities["other base ability"].Name}" };

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities["base ability"]), skills[0].BaseAbility.Name);
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["other base ability"]), skills[1].BaseAbility.Name);
        }

        //INFO: Example is how a centipede swarm uses Dexterity for Climb instead of Strength
        [Test]
        public void UseAlternateBaseAbilityForSkills()
        {
            abilities["base ability"] = new Ability("base ability");
            abilities["other base ability"] = new Ability("other base ability");

            var skills = new List<Skill>();
            skills.Add(new Skill("skill 1", abilities["base ability"], 1));
            skills.Add(new Skill("skill 2", abilities["base ability"], 1));
            skills.Add(new Skill("skill 3", abilities["base ability"], 1));

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats[0].Name = FeatConstants.SpecialQualities.SwapSkillBaseAbility;
            feats[0].Foci = new[] { $"{skills[0].Name}:{abilities["other base ability"].Name}", $"{skills[2].Name}:{abilities["other base ability"].Name}" };

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities["other base ability"]), skills[0].BaseAbility.Name);
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].BaseAbility, Is.EqualTo(abilities["base ability"]), skills[1].BaseAbility.Name);
            Assert.That(skills[2].Name, Is.EqualTo("skill 3"));
            Assert.That(skills[2].BaseAbility, Is.EqualTo(abilities["other base ability"]), skills[2].BaseAbility.Name);
        }

        [Test]
        public void ApplyFeatThatGrantSkillBonusesToSkills()
        {
            var baseAbility = new Ability("base ability");

            var skills = new List<Skill>();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills.Add(new Skill("skill 3", baseAbility, 1));
            skills.Add(new Skill("skill 4", baseAbility, 1));
            skills[0].AddBonus(1);
            skills[1].AddBonus(2);
            skills[2].AddBonus(3);
            skills[3].AddBonus(4);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Power = 1;
            feats[1].Name = "feat2";
            feats[1].Power = 2;
            feats[2].Name = "feat3";
            feats[2].Power = 3;

            var featGrantingSkillBonuses = new[] { "feat3", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "feat1")).Returns(new[] { "skill 1" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "feat3")).Returns(new[] { "skill 2", "skill 4" });

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].Bonus, Is.EqualTo(2));
            Assert.That(skills[1].Bonus, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(3));
            Assert.That(skills[3].Bonus, Is.EqualTo(7));
        }

        [Test]
        public void ApplyFeatThatGrantSkillBonusesToSkillsWithFocus()
        {
            var baseAbility = new Ability("base ability");

            var skills = new List<Skill>();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills.Add(new Skill("skill 3", baseAbility, 1, "other focus"));
            skills.Add(new Skill("skill 3", baseAbility, 1, "focus"));
            skills[0].AddBonus(1);
            skills[1].AddBonus(2);
            skills[2].AddBonus(3);
            skills[3].AddBonus(4);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Power = 1;
            feats[1].Name = "feat2";
            feats[1].Power = 2;
            feats[2].Name = "feat3";
            feats[2].Power = 3;

            var featGrantingSkillBonuses = new[] { "feat3", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "feat1")).Returns(new[] { "skill 1" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "feat3")).Returns(new[] { "skill 2", "skill 3/focus" });

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].Bonus, Is.EqualTo(2));
            Assert.That(skills[1].Bonus, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(3));
            Assert.That(skills[3].Bonus, Is.EqualTo(7));
        }

        [Test]
        public void IfFocusIsSkill_ApplyBonusToThatSkill()
        {
            var baseAbility = new Ability("base ability");

            var skills = new List<Skill>();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills.Add(new Skill("skill 3", baseAbility, 1));
            skills[0].AddBonus(1);
            skills[1].AddBonus(2);
            skills[2].AddBonus(3);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 2", "skill 3", "non-skill focus" };
            feats[0].Power = 4;
            feats[1].Name = "feat2";
            feats[1].Foci = new[] { "skill 3", "non-skill focus" };
            feats[1].Power = 1;
            feats[2].Name = "feat1";
            feats[2].Foci = new[] { "skill 2", "non-skill focus" };
            feats[2].Power = 3;

            var featGrantingSkillBonuses = new[] { "feat2", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].Bonus, Is.EqualTo(1));
            Assert.That(skills[1].Bonus, Is.EqualTo(9));
            Assert.That(skills[2].Bonus, Is.EqualTo(8));
        }

        [Test]
        public void IfFocusIsSkillWithFocus_ApplyBonusToThatSkill()
        {
            var baseAbility = new Ability("base ability");

            var skills = new List<Skill>();
            skills.Add(new Skill("skill 1", baseAbility, 1, "focus 1"));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills.Add(new Skill("skill 1", baseAbility, 1, "focus 2"));
            skills[0].AddBonus(1);
            skills[1].AddBonus(2);
            skills[2].AddBonus(3);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 2", "skill 1/focus 2", "non-skill focus" };
            feats[0].Power = 4;
            feats[1].Name = "feat2";
            feats[1].Foci = new[] { "skill 1/focus 2", "non-skill focus" };
            feats[1].Power = 1;
            feats[2].Name = "feat1";
            feats[2].Foci = new[] { "skill 2", "non-skill focus" };
            feats[2].Power = 3;

            var featGrantingSkillBonuses = new[] { "feat2", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].Bonus, Is.EqualTo(1));
            Assert.That(skills[1].Bonus, Is.EqualTo(9));
            Assert.That(skills[2].Bonus, Is.EqualTo(8));
        }

        [Test]
        public void OnlyApplySkillFeatToSkillsIfSkillFocusIsPurelySkill()
        {
            var baseAbility = new Ability("base ability");

            var skills = new List<Skill>();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills[0].AddBonus(1);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1: with qualifiers", "non-skill focus" };
            feats[0].Power = 666;

            var featGrantingSkillBonuses = new[] { "feat2", "feat1" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));

            var skill = updatedSkills.First(s => s.Name == "skill 1");
            Assert.That(skill.Bonus, Is.EqualTo(1));
            Assert.That(skill.Bonuses.Count, Is.EqualTo(2));

            var bonus = skill.Bonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(666));
            Assert.That(bonus.Condition, Is.EqualTo("with qualifiers"));
        }

        [Test]
        public void NoCircumstantialBonusIfBonusApplied()
        {
            var baseAbility = new Ability("base ability");

            var skills = new List<Skill>();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills[0].AddBonus(1);
            skills[1].AddBonus(2);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Power = 3;
            feats[1].Name = "feat2";
            feats[1].Foci = new[] { "skill 2", "non-skill focus" };
            feats[1].Power = 4;

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.SkillGroups, "feat1")).Returns(new[] { "skill 1" });

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].Bonus, Is.EqualTo(4));
            Assert.That(skills[0].CircumstantialBonus, Is.False);
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(2));
            Assert.That(skills[1].Bonus, Is.EqualTo(6));
            Assert.That(skills[1].CircumstantialBonus, Is.False);
            Assert.That(skills[1].Bonuses.Count, Is.EqualTo(2));

            var bonus = skills[0].Bonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(3));
            Assert.That(bonus.Condition, Is.Empty);

            bonus = skills[1].Bonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(4));
            Assert.That(bonus.Condition, Is.Empty);
        }

        [Test]
        public void IfSkillBonusFocusIsNotPurelySkill_MarkSkillAsHavingCircumstantialBonus()
        {
            var baseAbility = new Ability("base ability");

            var skills = new List<Skill>();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills[0].AddBonus(1);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1: with qualifiers", "non-skill focus" };
            feats[0].Power = 666;

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].CircumstantialBonus, Is.True);
            Assert.That(skills[0].Bonus, Is.EqualTo(1));
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(2));

            var bonus = skills[0].Bonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(666));
            Assert.That(bonus.Condition, Is.EqualTo("with qualifiers"));
        }

        [Test]
        public void MarkSkillWithCircumstantialBonusWhenOtherFociDoNotHaveCircumstantialBonus()
        {
            var baseAbility = new Ability("base ability");

            var skills = new List<Skill>();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills[0].AddBonus(1);
            skills[1].AddBonus(2);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1: with qualifiers", "skill 2" };
            feats[0].Power = 3;

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].CircumstantialBonus, Is.True);
            Assert.That(skills[0].Bonus, Is.EqualTo(1));
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(2));

            var bonus = skills[0].Bonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(3));
            Assert.That(bonus.Condition, Is.EqualTo("with qualifiers"));

            Assert.That(skills[1].CircumstantialBonus, Is.False);
            Assert.That(skills[1].Bonus, Is.EqualTo(5));
            Assert.That(skills[1].Bonuses.Count, Is.EqualTo(2));

            bonus = skills[1].Bonuses.Last();
            Assert.That(bonus.Value, Is.EqualTo(3));
            Assert.That(bonus.Condition, Is.Empty);
        }

        [Test]
        public void CircumstantialBonusIsNotOverwritten()
        {
            var baseAbility = new Ability("base ability");

            var skills = new List<Skill>();
            skills.Add(new Skill("skill 1", baseAbility, 1));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills[0].AddBonus(1);
            skills[1].AddBonus(2);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1: with qualifiers", "skill 2" };
            feats[0].Power = 1;
            feats[1].Name = "feat2";
            feats[1].Foci = new[] { "skill 1" };
            feats[1].Power = 1;

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.SkillBonus))
                .Returns(featGrantingSkillBonuses);

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].CircumstantialBonus, Is.True);
            Assert.That(skills[1].CircumstantialBonus, Is.False);
        }

        [Test]
        public void SwapBaseSkillAbility()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void DoNotSwapBaseSkillAbility()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void SwapBaseSkillAbilityByCreature()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void SwapBaseSkillAbilityByCreatureType()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void SwapBaseSkillAbilityByCreatureSubtype()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetSkillBonusFromCreature()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetSkillBonusFromCreatureType()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GetSkillBonusFromCreatureSubtype()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void ApplySkillSynergyBonusToMultipleSkills()
        {
            var baseAbility = new Ability("base ability");

            var skills = new List<Skill>();
            skills.Add(new Skill("skill 1", baseAbility, 1, "focus 1"));
            skills.Add(new Skill("skill 1", baseAbility, 1, "focus 2"));
            skills.Add(new Skill("skill 2", baseAbility, 1));
            skills.Add(new Skill("skill 3", baseAbility, 1, "focus 1"));
            skills.Add(new Skill("skill 3", baseAbility, 1, "focus 2"));
            skills[0].AddBonus(1);
            skills[1].AddBonus(2);
            skills[2].AddBonus(3);
            skills[3].AddBonus(4);
            skills[4].AddBonus(5);

            var feats = new List<Feat>();
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat1";
            feats[0].Foci = new[] { "skill 1", "non-skill focus" };
            feats[0].Power = 6;
            feats[1].Name = "feat2";
            feats[1].Foci = new[] { "skill 3/focus 2", "non-skill focus" };
            feats[1].Power = 7;

            var featGrantingSkillBonuses = new[] { "feat1", "feat2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.SkillBonus)).Returns(featGrantingSkillBonuses);

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].Bonus, Is.EqualTo(7));
            Assert.That(skills[1].Bonus, Is.EqualTo(8));
            Assert.That(skills[2].Bonus, Is.EqualTo(3));
            Assert.That(skills[3].Bonus, Is.EqualTo(4));
            Assert.That(skills[4].Bonus, Is.EqualTo(12));
        }
    }
}