using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using DnDGen.TreasureGen.Items;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Skills
{
    [TestFixture]
    public class SkillsGeneratorTests
    {
        private ISkillsGenerator skillsGenerator;
        private Mock<ICollectionTypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<Dice> mockDice;
        private Dictionary<string, Ability> abilities;
        private List<string> creatureSkills;
        private List<string> creatureTypeSkills;
        private List<string> untrainedSkills;
        private Mock<ICollectionDataSelector<SkillDataSelection>> mockSkillSelector;
        private Mock<ICollectionDataSelector<BonusDataSelection>> mockBonusSelector;
        private int creatureTypeSkillPoints;
        private List<string> allSkills;
        private HitPoints hitPoints;
        private CreatureType creatureType;
        private List<string> unnaturalSkills;
        private string size;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ICollectionTypeAndAmountSelector>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockSkillSelector = new Mock<ICollectionDataSelector<SkillDataSelection>>();
            mockBonusSelector = new Mock<ICollectionDataSelector<BonusDataSelection>>();
            mockDice = new Mock<Dice>();
            skillsGenerator = new SkillsGenerator(
                mockSkillSelector.Object,
                mockBonusSelector.Object,
                mockCollectionsSelector.Object,
                mockTypeAndAmountSelector.Object,
                mockDice.Object);

            abilities = new Dictionary<string, Ability>
            {
                [AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence)
            };
            allSkills = [];
            creatureSkills = [];
            untrainedSkills = [];
            unnaturalSkills = [];
            hitPoints = new HitPoints();
            creatureType = new CreatureType();
            creatureTypeSkills = [];

            allSkills.Add("skill 1");
            allSkills.Add("skill 2");
            allSkills.Add("skill 3");
            allSkills.Add("skill 4");
            allSkills.Add("skill 5");

            hitPoints.HitDice.Add(new HitDice { Quantity = 1 });
            creatureType.Name = "creature type";
            size = SizeConstants.Medium;
            creatureTypeSkillPoints = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, It.IsAny<string>())).Returns([]);
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, GroupConstants.All)).Returns(allSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "creature")).Returns(creatureSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, creatureType.Name)).Returns(creatureTypeSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, GroupConstants.Unnatural)).Returns(unnaturalSkills);
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, GroupConstants.Untrained)).Returns(untrainedSkills);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.SkillPoints, creatureType.Name))
                .Returns(() => new TypeAndAmountDataSelection { AmountAsDouble = creatureTypeSkillPoints });

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> s) => s.ElementAt(index++ % s.Count()));
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<Skill>>())).Returns((IEnumerable<Skill> s) => s.ElementAt(index++ % s.Count()));
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => sk.ClassSkill)),
                    It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => !sk.ClassSkill)),
                    null,
                    null))
                .Returns((IEnumerable<Skill> c, IEnumerable<Skill> u, IEnumerable<Skill> r, IEnumerable<Skill> v) => c.Union(u).ElementAt(index++ % c.Union(u).Count()));

            mockSkillSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, It.IsAny<string>()))
                .Returns((string a, string t, string skill) => new SkillDataSelection { SkillName = skill, BaseAbilityName = AbilityConstants.Intelligence });

            mockDice
                .Setup(d => d.Roll(It.IsAny<string>()).AsSum<int>())
                .Returns(1);
        }

        [Test]
        public void GenerateFor_GetSkills()
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
        public void GenerateFor_GetNoSkills()
        {
            hitPoints.HitDice[0].Quantity = 0;
            creatureTypeSkillPoints = 2;
            AddCreatureSkills(3);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills, Is.Empty);
        }

        [Test]
        public void GenerateFor_SkillHasArmorCheckPenalty()
        {
            creatureTypeSkillPoints = 2;
            AddCreatureSkills(3);

            var armorCheckSkills = new[] { "other skill", creatureSkills[0] };
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, GroupConstants.ArmorCheckPenalty)).Returns(armorCheckSkills);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skill = skills.First(s => s.Name == creatureSkills[0]);
            Assert.That(skill.HasArmorCheckPenalty, Is.True);
        }

        [Test]
        public void GenerateFor_SkillDoesNotHaveArmorCheckPenalty()
        {
            creatureTypeSkillPoints = 2;
            AddCreatureSkills(3);

            var armorCheckSkills = new[] { "other skill", "different skill" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, GroupConstants.ArmorCheckPenalty)).Returns(armorCheckSkills);

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
        public void GenerateFor_AssignAbilitiesToSkills()
        {
            AddCreatureSkills(1);

            var creatureSkillSelection = new SkillDataSelection
            {
                BaseAbilityName = "ability 1",
                SkillName = "class skill name"
            };

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(creatureSkillSelection);

            abilities["ability 1"] = new Ability("ability 1");

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("class skill name"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities["ability 1"]));
        }

        [Test]
        public void GenerateFor_SetRankCap_WithFirstLevelBonus()
        {
            hitPoints.HitDice[0].Quantity = 42;

            AddCreatureSkills(2);
            AddUntrainedSkills(2);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size, true);

            var skillsArray = skills.ToArray();
            Assert.That(skillsArray[0].Name, Is.EqualTo(untrainedSkills[0]));
            Assert.That(skillsArray[0].ClassSkill, Is.False);
            Assert.That(skillsArray[0].RankCap, Is.EqualTo(42 + 3));
            Assert.That(skillsArray[1].Name, Is.EqualTo(untrainedSkills[1]));
            Assert.That(skillsArray[1].ClassSkill, Is.False);
            Assert.That(skillsArray[1].RankCap, Is.EqualTo(42 + 3));
            Assert.That(skillsArray[2].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skillsArray[2].ClassSkill, Is.True);
            Assert.That(skillsArray[2].RankCap, Is.EqualTo(42 + 3));
            Assert.That(skillsArray[3].Name, Is.EqualTo(creatureSkills[1]));
            Assert.That(skillsArray[3].ClassSkill, Is.True);
            Assert.That(skillsArray[3].RankCap, Is.EqualTo(42 + 3));
        }

        [Test]
        public void GenerateFor_SetRankCap_WithoutFirstLevelBonus()
        {
            hitPoints.HitDice[0].Quantity = 42;

            AddCreatureSkills(2);
            AddUntrainedSkills(2);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size, false);

            var skillsArray = skills.ToArray();
            Assert.That(skillsArray[0].Name, Is.EqualTo(untrainedSkills[0]));
            Assert.That(skillsArray[0].ClassSkill, Is.False);
            Assert.That(skillsArray[0].RankCap, Is.EqualTo(42));
            Assert.That(skillsArray[1].Name, Is.EqualTo(untrainedSkills[1]));
            Assert.That(skillsArray[1].ClassSkill, Is.False);
            Assert.That(skillsArray[1].RankCap, Is.EqualTo(42));
            Assert.That(skillsArray[2].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skillsArray[2].ClassSkill, Is.True);
            Assert.That(skillsArray[2].RankCap, Is.EqualTo(42));
            Assert.That(skillsArray[3].Name, Is.EqualTo(creatureSkills[1]));
            Assert.That(skillsArray[3].ClassSkill, Is.True);
            Assert.That(skillsArray[3].RankCap, Is.EqualTo(42));
        }

        [Test]
        public void GenerateFor_SetClassSkill()
        {
            AddCreatureSkills(2);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);

            Assert.That(skills.Single(s => s.Name == "creature skill 1").ClassSkill, Is.True);
            Assert.That(skills.Single(s => s.Name == "creature skill 2").ClassSkill, Is.True);
        }

        [Test]
        public void GenerateFor_GetUntrainedSkills()
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
        public void GenerateFor_UntrainedSkillsBecomeClassSkills()
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
        public void GenerateFor_UntrainedSkillsWithNameChangeBecomeClassSkills()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            creatureSkills.Add(untrainedSkills[0]);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "changed name";
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, untrainedSkills[0])).Returns(skillSelection);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, skillSelection.SkillName))
                .Returns(["random", "other random"]);

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
        public void GenerateFor_UntrainedSkillsWithRandomFocusBecomeClassSkills()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            creatureSkills.Add(untrainedSkills[0]);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "untrained skill 2";
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, untrainedSkills[0])).Returns(skillSelection);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, skillSelection.SkillName))
                .Returns(["random", "other random"]);

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
        public void GenerateFor_UntrainedSkillsRepeatingClassSkillsWithRandomFocusBecomeClassSkills()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = creatureSkills[0];
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, untrainedSkills[0])).Returns(skillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, skillSelection.SkillName))
                .Returns(["random", "other random"]);

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
        public void GenerateFor_UntrainedSkillsWithSetFocusBecomeClassSkills()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            creatureSkills.Add(untrainedSkills[0]);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "untrained skill 2";
            skillSelection.Focus = "set focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, untrainedSkills[0])).Returns(skillSelection);

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
        public void GenerateFor_UntrainedSkillsWithMultipleRandomFociBecomeClassSkills()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            creatureSkills.Add(untrainedSkills[0]);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "untrained skill 2";
            skillSelection.RandomFociQuantity = 2;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, untrainedSkills[0])).Returns(skillSelection);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, skillSelection.SkillName))
                .Returns(["random", "other random", "another random"]);

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
        public void GenerateFor_AssignRankToClassSkill()
        {
            creatureTypeSkillPoints = 2;
            AddCreatureSkills(3);
            AddUntrainedSkills(3);

            var index = 0;
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => sk.ClassSkill)), It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => !sk.ClassSkill)), null, null))
                .Returns((IEnumerable<Skill> c, IEnumerable<Skill> u, IEnumerable<Skill> r, IEnumerable<Skill> v) => c.ElementAt(index++ % c.Count()));

            var count = 0;
            mockDice
                .Setup(d => d.Roll("1d4").AsSum<int>())
                .Returns(() => count++ % 4 + 1);

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
            Assert.That(skills[3].Ranks, Is.EqualTo(2));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[4].Name, Is.EqualTo("creature skill 2"));
            Assert.That(skills[4].ClassSkill, Is.True);
            Assert.That(skills[4].Ranks, Is.EqualTo(3));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[5].Name, Is.EqualTo("creature skill 1"));
            Assert.That(skills[5].ClassSkill, Is.True);
            Assert.That(skills[5].Ranks, Is.EqualTo(3));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills.Length, Is.EqualTo(6));
        }

        [Test]
        public void GenerateFor_AssignRankToUntrainedSkill()
        {
            creatureTypeSkillPoints = 2;
            AddCreatureSkills(3);
            AddUntrainedSkills(3);

            var index = 0;
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => sk.ClassSkill)), It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => !sk.ClassSkill)), null, null))
                .Returns((IEnumerable<Skill> c, IEnumerable<Skill> u, IEnumerable<Skill> r, IEnumerable<Skill> v) => u.ElementAt(index++ % u.Count()));

            var count = 0;
            mockDice
                .Setup(d => d.Roll("1d4").AsSum<int>())
                .Returns(() => count++ % 4 + 1);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("untrained skill 3"));
            Assert.That(skills[0].ClassSkill, Is.False);
            Assert.That(skills[0].Ranks, Is.EqualTo(2));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(1));
            Assert.That(skills[1].Name, Is.EqualTo("untrained skill 2"));
            Assert.That(skills[1].ClassSkill, Is.False);
            Assert.That(skills[1].Ranks, Is.EqualTo(3));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(1.5));
            Assert.That(skills[2].Name, Is.EqualTo("untrained skill 1"));
            Assert.That(skills[2].ClassSkill, Is.False);
            Assert.That(skills[2].Ranks, Is.EqualTo(3));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(1.5));
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
        public void GenerateFor_AssignRankToClassAndUntrainedSkill()
        {
            creatureTypeSkillPoints = 2;
            AddCreatureSkills(3);
            AddUntrainedSkills(3);

            var count = 0;
            mockDice
                .Setup(d => d.Roll("1d4").AsSum<int>())
                .Returns(() => count++ % 4 + 1);

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
            Assert.That(skills[2].Ranks, Is.Zero);
            Assert.That(skills[2].EffectiveRanks, Is.Zero);
            Assert.That(skills[3].Name, Is.EqualTo("creature skill 3"));
            Assert.That(skills[3].ClassSkill, Is.True);
            Assert.That(skills[3].Ranks, Is.EqualTo(1));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(1));
            Assert.That(skills[4].Name, Is.EqualTo("creature skill 2"));
            Assert.That(skills[4].ClassSkill, Is.True);
            Assert.That(skills[4].Ranks, Is.EqualTo(2));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[5].Name, Is.EqualTo("creature skill 1"));
            Assert.That(skills[5].ClassSkill, Is.True);
            Assert.That(skills[5].Ranks, Is.EqualTo(3));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills.Length, Is.EqualTo(6));
        }

        [Test]
        public void GenerateFor_GetSkillWithSetFocus()
        {
            AddCreatureSkills(1);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.Focus = Guid.NewGuid().ToString();

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skill.Focus, Is.EqualTo(skillSelection.Focus));
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GenerateFor_GetSkillWithRandomFocus()
        {
            AddCreatureSkills(1);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, skillSelection.SkillName))
                .Returns(["random", "other random"]);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(skillSelection.SkillName));
            Assert.That(skill.Focus, Is.EqualTo("random"));
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GenerateFor_GetSkillWithMultipleRandomFoci()
        {
            AddCreatureSkills(1);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 2;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, skillSelection.SkillName))
                .Returns(["random", "other random", "third random"]);

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
        public void GenerateFor_DoNotRepeatFociForSkill()
        {
            AddCreatureSkills(1);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = 2;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, skillSelection.SkillName))
                .Returns(["random", "other random", "third random"]);

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
        public void GenerateFor_GetSkillWithAllFoci()
        {
            AddCreatureSkills(1);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = Guid.NewGuid().ToString();
            skillSelection.RandomFociQuantity = SkillConstants.Foci.QuantityOfAll;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, skillSelection.SkillName))
                .Returns(["random", "other random", "third random"]);

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
        public void GenerateFor_AllSkillsMaxedOut()
        {
            creatureTypeSkillPoints = 3;
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);

            Assert.That(skills.Single(s => s.Name == "skill 1").Ranks, Is.EqualTo(hitPoints.HitDiceQuantity + 3));
            Assert.That(skills.Single(s => s.Name == "skill 2").Ranks, Is.EqualTo(hitPoints.HitDiceQuantity + 3));
        }

        [Test]
        public void GenerateFor_IfCannotUseEquipment_DoNotGetUntrainedUnnaturalSkill()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            unnaturalSkills.Add(untrainedSkills[0]);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, false, size);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Is.All.Not.EqualTo(untrainedSkills[0]));
        }

        [Test]
        public void GenerateFor_IfCannotUseEquipment_GetTrainedUnnaturalSkill()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            unnaturalSkills.Add(creatureSkills[0]);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, false, size);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Contains.Item(creatureSkills[0]));
        }

        [Test]
        public void GenerateFor_IfCanUseEquipment_GetUntrainedUnnaturalSkill()
        {
            AddUntrainedSkills(2);
            AddCreatureSkills(2);
            unnaturalSkills.Add(untrainedSkills[0]);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Contains.Item(untrainedSkills[0]));
        }

        [Test]
        public void GenerateFor_IfCanUseEquipment_GetTrainedUnnaturalSkill()
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
        public void GenerateFor_SkillPointsDeterminedByHitDice(int hitDiceQuantity, int skillPointsPerDie, int skillPoints)
        {
            hitPoints.HitDice[0].Quantity = hitDiceQuantity;
            creatureTypeSkillPoints = skillPointsPerDie;
            AddCreatureSkills(hitDiceQuantity + skillPointsPerDie);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var totalRanks = skills.Sum(s => s.Ranks);

            Assert.That(totalRanks, Is.EqualTo(skillPoints));
        }

        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 2)]
        [TestCase(3, 1, 3)]
        [TestCase(4, 1, 4)]
        [TestCase(5, 1, 5)]
        [TestCase(6, 1, 6)]
        [TestCase(7, 1, 7)]
        [TestCase(8, 1, 8)]
        [TestCase(9, 1, 9)]
        [TestCase(10, 1, 10)]
        [TestCase(11, 1, 11)]
        [TestCase(12, 1, 12)]
        [TestCase(13, 1, 13)]
        [TestCase(14, 1, 14)]
        [TestCase(15, 1, 15)]
        [TestCase(16, 1, 16)]
        [TestCase(17, 1, 17)]
        [TestCase(18, 1, 18)]
        [TestCase(19, 1, 19)]
        [TestCase(20, 1, 20)]
        [TestCase(1, 2, 2)]
        [TestCase(2, 2, 4)]
        [TestCase(3, 2, 6)]
        [TestCase(4, 2, 8)]
        [TestCase(5, 2, 10)]
        [TestCase(6, 2, 12)]
        [TestCase(7, 2, 14)]
        [TestCase(8, 2, 16)]
        [TestCase(9, 2, 18)]
        [TestCase(10, 2, 20)]
        [TestCase(11, 2, 22)]
        [TestCase(12, 2, 24)]
        [TestCase(13, 2, 26)]
        [TestCase(14, 2, 28)]
        [TestCase(15, 2, 30)]
        [TestCase(16, 2, 32)]
        [TestCase(17, 2, 34)]
        [TestCase(18, 2, 36)]
        [TestCase(19, 2, 38)]
        [TestCase(20, 2, 40)]
        [TestCase(1, 8, 8)]
        [TestCase(2, 8, 16)]
        [TestCase(3, 8, 24)]
        [TestCase(4, 8, 32)]
        [TestCase(5, 8, 40)]
        [TestCase(6, 8, 48)]
        [TestCase(7, 8, 56)]
        [TestCase(8, 8, 64)]
        [TestCase(9, 8, 72)]
        [TestCase(10, 8, 80)]
        [TestCase(11, 8, 88)]
        [TestCase(12, 8, 96)]
        [TestCase(13, 8, 104)]
        [TestCase(14, 8, 112)]
        [TestCase(15, 8, 120)]
        [TestCase(16, 8, 128)]
        [TestCase(17, 8, 136)]
        [TestCase(18, 8, 144)]
        [TestCase(19, 8, 152)]
        [TestCase(20, 8, 160)]
        public void GenerateFor_SkillPointsDeterminedByHitDice_NotFirstHitDie(int hitDiceQuantity, int skillPointsPerDie, int skillPoints)
        {
            hitPoints.HitDice[0].Quantity = hitDiceQuantity;
            creatureTypeSkillPoints = skillPointsPerDie;
            AddCreatureSkills(hitDiceQuantity + skillPointsPerDie);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size, false);
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
        public void GenerateFor_ApplyIntelligenceBonusToSkillPoints(int hitDie, int intelligence, int skillPoints)
        {
            hitPoints.HitDice[0].Quantity = hitDie;
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseScore = intelligence;
            AddCreatureSkills(hitDie + intelligence);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var totalRanks = skills.Sum(s => s.Ranks);

            Assert.That(totalRanks, Is.EqualTo(skillPoints));
        }

        [Test]
        public void GenerateFor_ApplyLotsOfSkillPoints()
        {
            hitPoints.HitDice[0].Quantity = 100;
            creatureTypeSkillPoints = 8;
            abilities[AbilityConstants.Intelligence].BaseScore = 18;

            AddCreatureSkills(20);

            //[1,103]
            mockDice
                .Setup(d => d.Roll("1d103").AsSum<int>())
                .Returns(51);

            //[1,52]
            mockDice
                .Setup(d => d.Roll("1d52").AsSum<int>())
                .Returns(52);

            var start = DateTime.Now;
            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var duration = DateTime.Now - start;

            var totalRanks = skills.Sum(s => s.Ranks);
            Assert.That(totalRanks, Is.EqualTo(103 * 12));
            Assert.That(duration, Is.LessThan(TimeSpan.FromMilliseconds(200)));
        }

        [Test]
        public void GenerateFor_IfCreatureHasBaseAbility_GetSkill()
        {
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "skill 2")).Returns(() => new SkillDataSelection { BaseAbilityName = AbilityConstants.Constitution, SkillName = "skill 2" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Contains.Item("skill 1"));
            Assert.That(skillNames, Contains.Item("skill 2"));
            Assert.That(skills.Count, Is.EqualTo(2));
        }

        [Test]
        public void GenerateFor_IfCreatureDoesNotHaveBaseAbility_CannotGetSkill()
        {
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "skill 2")).Returns(() => new SkillDataSelection { BaseAbilityName = AbilityConstants.Constitution, SkillName = "skill 2" });

            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution) { BaseScore = 0 };

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skillNames = skills.Select(s => s.Name);

            Assert.That(skillNames, Contains.Item("skill 1"));
            Assert.That(skillNames, Is.All.Not.EqualTo("skill 2"));
            Assert.That(skills.Count, Is.EqualTo(1));
        }

        [Test]
        public void GenerateFor_DoNotAssignSkillPointsToSkillsThatTheCreatureDoesNotHaveDueToNotHavingTheBaseAbility()
        {
            creatureTypeSkillPoints = 1;
            hitPoints.HitDice[0].Quantity = 2;

            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var constitutionSelection = new SkillDataSelection { BaseAbilityName = AbilityConstants.Constitution, SkillName = "skill 2" };
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "skill 2")).Returns(constitutionSelection);

            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution) { BaseScore = 0 };

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills, Is.Not.Empty);

            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].Ranks, Is.EqualTo(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(5));
        }

        [TestCase("skill 1Intelligence")]
        [TestCase("skill 1All")]
        [TestCase("skill 19266")]
        public void GenerateFor_CreatureSkillWithDifferentNameIsClassSkill(string creatureSkill)
        {
            creatureTypeSkillPoints = 2;
            creatureSkills.Add(creatureSkill);

            var selection = new SkillDataSelection { BaseAbilityName = AbilityConstants.Intelligence, SkillName = "skill 1" };
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkill)).Returns(selection);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(1));

            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo("skill 1"));
            Assert.That(skill.Focus, Is.Empty);
            Assert.That(skill.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GenerateFor_SelectPresetFocusForSkill()
        {
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var selection = new SkillDataSelection { BaseAbilityName = AbilityConstants.Charisma, Focus = "focus", SkillName = "skill with focus" };
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "skill 2")).Returns(selection);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "skill with random foci"))
                .Returns(["random", "other random"]);

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
        public void GenerateFor_SelectRandomFocusForSkill()
        {
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var randomSelection = new SkillDataSelection { BaseAbilityName = AbilityConstants.Charisma, RandomFociQuantity = 1, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "skill 2")).Returns(randomSelection);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "skill with random foci"))
                .Returns(["random", "other random"]);

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
        public void GenerateFor_SelectMultipleRandomFociForSkill()
        {
            hitPoints.HitDice[0].Quantity = 20;
            creatureTypeSkillPoints = 0;
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseScore = 10;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var randomSelection = new SkillDataSelection { BaseAbilityName = AbilityConstants.Charisma, RandomFociQuantity = 2, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "skill 2")).Returns(randomSelection);

            var count = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(count++ % ss.Count()));

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "skill with random foci"))
                .Returns(["random", "other random"]);

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
        public void GenerateFor_DoNotSelectRepeatedRandomFociForSkill()
        {
            hitPoints.HitDice[0].Quantity = 20;
            creatureTypeSkillPoints = 0;
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseScore = 10;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var randomSelection = new SkillDataSelection { BaseAbilityName = AbilityConstants.Charisma, RandomFociQuantity = 2, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "skill 2")).Returns(randomSelection);

            var count = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> ss) => ss.ElementAt(count++ / 2 % ss.Count()));

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "skill with random foci"))
                .Returns(["random", "other random"]);

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
        public void GenerateFor_SelectAllRandomFociForSkill()
        {
            hitPoints.HitDice[0].Quantity = 20;
            creatureTypeSkillPoints = 0;
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseScore = 10;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            creatureSkills.Add("skill 1");
            creatureSkills.Add("skill 2");

            var randomSelection = new SkillDataSelection { BaseAbilityName = AbilityConstants.Charisma, RandomFociQuantity = 3, SkillName = "skill with random foci" };
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "skill 2")).Returns(randomSelection);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "skill with random foci"))
                .Returns(["random", "other random"]);

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
        public void GenerateFor_ProfessionSkillGrantsAdditionalSkills()
        {
            AddCreatureSkills(1);
            creatureSkills.Add("professional skill 1");
            creatureSkills.Add("professional skill 2");
            creatureSkills.Add("professional skill 3");

            abilities["ability 1"] = new Ability("ability 1");
            abilities["ability 2"] = new Ability("ability 2");
            abilities["ability 3"] = new Ability("ability 3");

            var professionSkillSelection = new SkillDataSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            var professionBonusSkillSelection = new SkillDataSelection();
            professionBonusSkillSelection.BaseAbilityName = "ability 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillDataSelection();
            professionBonusWithSetFocusSkillSelection.BaseAbilityName = "ability 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillDataSelection();
            professionBonusWithRandomFocusSkillSelection.BaseAbilityName = "ability 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "professional skill 3"))
                .Returns(["random", "other random"]);

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
        public void GenerateFor_RandomProfessionSkillGrantsAdditionalSkills()
        {
            AddCreatureSkills(1);
            creatureSkills.Add("professional skill 1");
            creatureSkills.Add("professional skill 2");
            creatureSkills.Add("professional skill 3");

            abilities["ability 1"] = new Ability("ability 1");
            abilities["ability 2"] = new Ability("ability 2");
            abilities["ability 3"] = new Ability("ability 3");

            var professionSkillSelection = new SkillDataSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.RandomFociQuantity = 1;

            var professionBonusSkillSelection = new SkillDataSelection();
            professionBonusSkillSelection.BaseAbilityName = "ability 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillDataSelection();
            professionBonusWithSetFocusSkillSelection.BaseAbilityName = "ability 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillDataSelection();
            professionBonusWithRandomFocusSkillSelection.BaseAbilityName = "ability 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "professional skill 3"))
                .Returns(["random", "other random"]);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, SkillConstants.Profession))
                .Returns(["random job", "other random job"]);

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
        public void GenerateFor_ProfessionSkillGrantsNoDuplicateCreatureSkills()
        {
            AddCreatureSkills(1);
            creatureSkills.Add("professional skill 1");
            creatureSkills.Add("professional skill 2");
            creatureSkills.Add("professional skill 3");

            abilities["ability 1"] = new Ability("ability 1");
            abilities["ability 2"] = new Ability("ability 2");
            abilities["ability 3"] = new Ability("ability 3");

            var professionSkillSelection = new SkillDataSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            var professionBonusSkillSelection = new SkillDataSelection();
            professionBonusSkillSelection.BaseAbilityName = "ability 1";
            professionBonusSkillSelection.SkillName = "professional skill 1";

            var professionBonusWithSetFocusSkillSelection = new SkillDataSelection();
            professionBonusWithSetFocusSkillSelection.BaseAbilityName = "ability 2";
            professionBonusWithSetFocusSkillSelection.SkillName = "professional skill 2";
            professionBonusWithSetFocusSkillSelection.Focus = "set focus";

            var professionBonusWithRandomFocusSkillSelection = new SkillDataSelection();
            professionBonusWithRandomFocusSkillSelection.BaseAbilityName = "ability 3";
            professionBonusWithRandomFocusSkillSelection.SkillName = "professional skill 3";
            professionBonusWithRandomFocusSkillSelection.RandomFociQuantity = 1;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(professionSkillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "professional skill 1")).Returns(professionBonusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "professional skill 2")).Returns(professionBonusWithSetFocusSkillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, "professional skill 3")).Returns(professionBonusWithRandomFocusSkillSelection);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "professional skill 3"))
                .Returns(["random", "other random"]);

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
        public void GenerateFor_ProfessionSkillGrantsNoAdditionalCreatureSkills()
        {
            AddCreatureSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            var professionSkillSelection = new SkillDataSelection();
            professionSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            professionSkillSelection.SkillName = SkillConstants.Profession;
            professionSkillSelection.Focus = "software developer";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(professionSkillSelection);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo(SkillConstants.Profession));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(1));
        }

        [Test]
        public void GenerateFor_NoProfessionSkillGrantsNoAdditionalCreatureSkills()
        {
            AddCreatureSkills(1);
            abilities["stat 1"] = new Ability("stat 1");
            abilities["stat 2"] = new Ability("stat 2");
            abilities["stat 3"] = new Ability("stat 3");

            var otherSkillSelection = new SkillDataSelection
            {
                BaseAbilityName = AbilityConstants.Intelligence,
                SkillName = "other skill",
                Focus = "software developer"
            };

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(otherSkillSelection);

            var professionSkills = new[] { "profession skill 1", "profession skill 2", "professional skill 3" };
            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("other skill"));
            Assert.That(skills[0].Focus, Is.EqualTo("software developer"));
            Assert.That(skills[0].BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skills[0].ClassSkill, Is.True);

            Assert.That(skills.Length, Is.EqualTo(1));
        }

        [Test]
        public void GenerateFor_CreaturesWithoutIntelligenceReceiveNoRanksForSkills()
        {
            abilities[AbilityConstants.Intelligence].BaseScore = 0;
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);

            AddUntrainedSkills(4);

            creatureTypeSkillPoints = 10;
            hitPoints.HitDice[0].Quantity = 10;

            mockSkillSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, It.IsAny<string>()))
                .Returns((string a, string t, string skill) => new SkillDataSelection { SkillName = skill, BaseAbilityName = AbilityConstants.Strength });

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
        public void GenerateFor_ApplySizeModiferToHideSkill(string creatureSize, int bonus)
        {
            AddCreatureSkills(1);
            AddUntrainedSkills(1);
            untrainedSkills.Add(SkillConstants.Hide);
            size = creatureSize;

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var hideSkill = skills.Single(s => s.Name == SkillConstants.Hide);
            Assert.That(hideSkill.Bonus, Is.EqualTo(bonus), size);

            var otherSkills = skills.Except([hideSkill]);
            var otherBonuses = otherSkills.Select(s => s.Bonus);
            Assert.That(otherBonuses, Is.All.Zero);
        }

        [Test]
        public void GenerateFor_ApplyNoSkillBonus()
        {
            AddCreatureSkills(1);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(1));

            var skill = skills.Single();
            Assert.That(skill.Bonus, Is.Zero);
            Assert.That(skill.Bonuses, Is.Empty);
        }

        [Test]
        public void GenerateFor_ApplySkillBonus()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 9266 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature")).Returns(bonuses);

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
        public void GenerateFor_ApplySkillBonuses()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 9266 },
                new BonusDataSelection { Target = creatureSkills[1], Bonus = 90210 },
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature")).Returns(bonuses);

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
        public void GenerateFor_ApplySkillBonusesToSameSkill()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 9266 },
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 90210 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature")).Returns(bonuses);

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
        public void GenerateFor_ApplySkillBonusWithCondition()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 9266, Condition = "condition" }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature")).Returns(bonuses);

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
        public void GenerateFor_ApplySkillBonusesWithCondition()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 9266, Condition = "condition" },
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 90210, Condition = "other condition" }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature")).Returns(bonuses);

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
        public void GenerateFor_ApplySkillBonusesWithAndWithoutCondition()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 9266, Condition = "condition" },
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 90210 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature")).Returns(bonuses);

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
        public void GenerateFor_ApplySkillBonusWithFocus()
        {
            AddCreatureSkills(2);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "skill";
            skillSelection.Focus = "focus";

            var wrongSkillSelection = new SkillDataSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "skill";
            wrongSkillSelection.Focus = "other focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[1])).Returns(wrongSkillSelection);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = SkillConstants.Build("skill", "focus"), Bonus = 9266 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature")).Returns(bonuses);

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
        public void GenerateFor_ApplySkillBonusWithFocusAndCondition()
        {
            AddCreatureSkills(2);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "skill";
            skillSelection.Focus = "focus";

            var wrongSkillSelection = new SkillDataSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "skill";
            wrongSkillSelection.Focus = "other focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[1])).Returns(wrongSkillSelection);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = SkillConstants.Build("skill", "focus"), Bonus = 9266, Condition = "condition" }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature")).Returns(bonuses);

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
        public void GenerateFor_ApplySkillBonusToMultipleSkills()
        {
            AddCreatureSkills(3);

            var skillSelection1 = new SkillDataSelection();
            skillSelection1.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection1.SkillName = "skill";
            skillSelection1.Focus = "focus";

            var skillSelection2 = new SkillDataSelection();
            skillSelection2.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection2.SkillName = "skill";
            skillSelection2.Focus = "other focus";

            var wrongSkillSelection = new SkillDataSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "other skill";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection1);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[1])).Returns(wrongSkillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[2])).Returns(skillSelection2);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = "skill", Bonus = 9266 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature")).Returns(bonuses);

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
        public void GenerateFor_ApplySkillBonusWithConditionToMultipleSkills()
        {
            AddCreatureSkills(3);

            var skillSelection1 = new SkillDataSelection();
            skillSelection1.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection1.SkillName = "skill";
            skillSelection1.Focus = "focus";

            var skillSelection2 = new SkillDataSelection();
            skillSelection2.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection2.SkillName = "skill";
            skillSelection2.Focus = "other focus";

            var wrongSkillSelection = new SkillDataSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "other skill";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection1);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[1])).Returns(wrongSkillSelection);
            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[2])).Returns(skillSelection2);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = "skill", Bonus = 9266, Condition = "condition" }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature")).Returns(bonuses);

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
        public void GenerateFor_ApplyDuplicateSkillBonuses()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 9266 },
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 9266 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature")).Returns(bonuses);

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
        public void GenerateFor_UseAlternateBaseAbilityForSkill()
        {
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);

            AddCreatureSkills(1);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Dexterity;
            skillSelection.SkillName = SkillConstants.Climb;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, skillSelection.SkillName))
                .Returns(["random", "other random"]);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(SkillConstants.Climb));
            Assert.That(skill.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
            Assert.That(skill.Focus, Is.Empty);
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GenerateFor_ApplyFeatThatGrantSkillBonusesToSkills()
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "feat1")).Returns(new[] { "skill 1" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "feat3")).Returns(new[] { "skill 2", "skill 4" });

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].Bonus, Is.EqualTo(2));
            Assert.That(skills[1].Bonus, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(3));
            Assert.That(skills[3].Bonus, Is.EqualTo(7));
        }

        [Test]
        public void GenerateFor_ApplyFeatThatGrantSkillBonusesToSkillsWithFocus()
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "feat1")).Returns(new[] { "skill 1" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "feat3")).Returns(new[] { "skill 2", "skill 3/focus" });

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].Bonus, Is.EqualTo(2));
            Assert.That(skills[1].Bonus, Is.EqualTo(5));
            Assert.That(skills[2].Bonus, Is.EqualTo(3));
            Assert.That(skills[3].Bonus, Is.EqualTo(7));
        }

        [Test]
        public void GenerateFor_IfFocusIsSkill_ApplyBonusToThatSkill()
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

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].Bonus, Is.EqualTo(1));
            Assert.That(skills[1].Bonus, Is.EqualTo(9));
            Assert.That(skills[2].Bonus, Is.EqualTo(8));
        }

        [Test]
        public void GenerateFor_IfFocusIsSkillWithFocus_ApplyBonusToThatSkill()
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

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].Bonus, Is.EqualTo(1));
            Assert.That(skills[1].Bonus, Is.EqualTo(9));
            Assert.That(skills[2].Bonus, Is.EqualTo(8));
        }

        [Test]
        public void GenerateFor_OnlyApplySkillFeatToSkillsIfSkillFocusIsPurelySkill()
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
        public void GenerateFor_NoCircumstantialBonusIfBonusApplied()
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

            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "feat1")).Returns(new[] { "skill 1" });

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
        public void GenerateFor_IfSkillBonusFocusIsNotPurelySkill_MarkSkillAsHavingCircumstantialBonus()
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
        public void GenerateFor_MarkSkillWithCircumstantialBonusWhenOtherFociDoNotHaveCircumstantialBonus()
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
        public void GenerateFor_CircumstantialBonusIsNotOverwritten()
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

            var updatedSkills = skillsGenerator.ApplyBonusesFromFeats(skills, feats, abilities);
            Assert.That(updatedSkills, Is.EqualTo(skills));
            Assert.That(updatedSkills, Is.EquivalentTo(skills));
            Assert.That(skills[0].CircumstantialBonus, Is.True);
            Assert.That(skills[1].CircumstantialBonus, Is.False);
        }

        [Test]
        public void GenerateFor_SwapBaseSkillAbilityByCreature()
        {
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);

            AddCreatureSkills(1);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Dexterity;
            skillSelection.SkillName = SkillConstants.Climb;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(SkillConstants.Climb));
            Assert.That(skill.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Dexterity]));
            Assert.That(skill.Focus, Is.Empty);
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GenerateFor_GetSkillFromCreatureType()
        {
            creatureTypeSkills.Add("creature type skill");

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo("creature type skill"));
            Assert.That(skill.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skill.Focus, Is.Empty);
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GenerateFor_GetSkillFromCreatureSubtype()
        {
            creatureType.SubTypes = new[] { "subtype" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "subtype")).Returns(new[] { "subtype skill" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo("subtype skill"));
            Assert.That(skill.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skill.Focus, Is.Empty);
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GenerateFor_GetSkillsFromCreatureSubtypes()
        {
            creatureType.SubTypes = new[] { "subtype", "other subtype" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "subtype")).Returns(new[] { "subtype skill" });
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillGroups, "other subtype")).Returns(new[] { "other subtype skill" });

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.Name, Is.EqualTo("subtype skill"));
            Assert.That(skill.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skill.Focus, Is.Empty);
            Assert.That(skill.ClassSkill, Is.True);

            skill = skills.Last();
            Assert.That(skill.Name, Is.EqualTo("other subtype skill"));
            Assert.That(skill.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Intelligence]));
            Assert.That(skill.Focus, Is.Empty);
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GenerateFor_SwapBaseSkillAbilityByCreatureType()
        {
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            AddCreatureSkills(1);
            creatureTypeSkills.Add("creature type skill");

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Constitution;
            skillSelection.SkillName = SkillConstants.Concentration;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);

            var typeSkillSelection = new SkillDataSelection();
            typeSkillSelection.BaseAbilityName = AbilityConstants.Charisma;
            typeSkillSelection.SkillName = SkillConstants.Concentration;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureTypeSkills[0])).Returns(typeSkillSelection);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(SkillConstants.Concentration));
            Assert.That(skill.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]));
            Assert.That(skill.Focus, Is.Empty);
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test]
        public void GenerateFor_SwapBaseSkillAbilityByCreatureTypeIfCreatureDoesNotHaveSkillButSkillIsUntrained()
        {
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            AddCreatureSkills(1);
            creatureTypeSkills.Add("creature type skill");
            untrainedSkills.Add(SkillConstants.Concentration);

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Constitution;
            skillSelection.SkillName = SkillConstants.Concentration;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);

            var typeSkillSelection = new SkillDataSelection();
            typeSkillSelection.BaseAbilityName = AbilityConstants.Charisma;
            typeSkillSelection.SkillName = SkillConstants.Concentration;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureTypeSkills[0])).Returns(typeSkillSelection);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            var skill = skills.Single();
            Assert.That(skill.Name, Is.EqualTo(SkillConstants.Concentration));
            Assert.That(skill.BaseAbility, Is.EqualTo(abilities[AbilityConstants.Charisma]), skill.BaseAbility.Name);
            Assert.That(skill.Focus, Is.Empty);
            Assert.That(skill.ClassSkill, Is.True);
        }

        [Test, Ignore("Only creature type ability swap is Undead Charisma for concentration, which is a natural skill, so this is not a valid usecase")]
        public void GenerateFor_DoNotSwapBaseSkillAbilityByCreatureTypeIfCreatureDoesNotHaveSkill()
        {
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            abilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            abilities[AbilityConstants.Constitution].BaseScore = 0;

            AddCreatureSkills(1);
            creatureTypeSkills.Add("creature type skill");

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Constitution;
            skillSelection.SkillName = SkillConstants.Concentration;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);

            var typeSkillSelection = new SkillDataSelection();
            typeSkillSelection.BaseAbilityName = AbilityConstants.Charisma;
            typeSkillSelection.SkillName = SkillConstants.Concentration;

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureTypeSkills[0])).Returns(typeSkillSelection);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills, Is.Empty);
        }

        [Test, Ignore("No subtypes require a base ability swap")]
        public void GenerateFor_SwapBaseSkillAbilityByCreatureSubtype()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void GenerateFor_GetSkillBonusFromCreature()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 9266 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature")).Returns(bonuses);

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
        public void GenerateFor_GetSkillBonusFromCreatureType()
        {
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 9266 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureType.Name)).Returns(bonuses);

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
        public void GenerateFor_GetSkillBonusFromCreatureSubtype()
        {
            AddCreatureSkills(2);

            creatureType.SubTypes = new[] { "subtype" };

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[0], Bonus = 9266 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "subtype")).Returns(bonuses);

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
        public void GenerateFor_GetNoSkillSynergiesBecauseSkillHasNoSynergies()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(2);

            hitPoints.HitDice[0].Quantity = 2;

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.EffectiveRanks, Is.AtLeast(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
            Assert.That(skill.Bonuses, Is.Empty);

            skill = skills.Last();
            Assert.That(skill.EffectiveRanks, Is.AtLeast(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
            Assert.That(skill.Bonuses, Is.Empty);
        }

        [Test]
        public void GenerateFor_GetNoSkillSynergiesBecauseSkillHasInsufficientRanks()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(2);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[1], Bonus = 9266 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[0])).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.EffectiveRanks, Is.LessThan(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(4));
            Assert.That(skill.Bonuses, Is.Empty);

            skill = skills.Last();
            Assert.That(skill.EffectiveRanks, Is.LessThan(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(4));
            Assert.That(skill.Bonuses, Is.Empty);
        }

        [Test]
        public void GenerateFor_GetNoSkillSynergiesBecauseMissingSourceSkill()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(2);

            hitPoints.HitDice[0].Quantity = 2;

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[1], Bonus = 9266 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "creature skill 3")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.EffectiveRanks, Is.AtLeast(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
            Assert.That(skill.Bonuses, Is.Empty);

            skill = skills.Last();
            Assert.That(skill.EffectiveRanks, Is.AtLeast(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
            Assert.That(skill.Bonuses, Is.Empty);
        }

        [Test]
        public void GenerateFor_GetNoSkillSynergiesBecauseMissingSourceSkillAndFocus()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(2);

            hitPoints.HitDice[0].Quantity = 2;

            var skillSelection1 = new SkillDataSelection();
            skillSelection1.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection1.SkillName = "creature skill";
            skillSelection1.Focus = "focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection1);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = creatureSkills[1], Bonus = 9266 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, SkillConstants.Build("creature skill", "wrong focus"))).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.EffectiveRanks, Is.AtLeast(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
            Assert.That(skill.Bonuses, Is.Empty);

            skill = skills.Last();
            Assert.That(skill.EffectiveRanks, Is.AtLeast(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
            Assert.That(skill.Bonuses, Is.Empty);
        }

        [Test]
        public void GenerateFor_GetNoSkillSynergiesBecauseMissingTargetSkill()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(2);

            hitPoints.HitDice[0].Quantity = 2;

            var bonuses = new[]
            {
                new BonusDataSelection { Target = "creature skill 3", Bonus = 9266 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[0])).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.EffectiveRanks, Is.AtLeast(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
            Assert.That(skill.Bonuses, Is.Empty);

            skill = skills.Last();
            Assert.That(skill.EffectiveRanks, Is.AtLeast(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
            Assert.That(skill.Bonuses, Is.Empty);
        }

        [Test]
        public void GenerateFor_GetNoSkillSynergiesBecauseMissingTargetSkillAndFocus()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(2);

            hitPoints.HitDice[0].Quantity = 2;

            var skillSelection1 = new SkillDataSelection();
            skillSelection1.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection1.SkillName = "creature skill";
            skillSelection1.Focus = "focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[1])).Returns(skillSelection1);

            var bonuses = new[]
            {
                new BonusDataSelection { Target = SkillConstants.Build("creature skill", "wrong focus"), Bonus = 9266 }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[0])).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.First();
            Assert.That(skill.EffectiveRanks, Is.AtLeast(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
            Assert.That(skill.Bonuses, Is.Empty);

            skill = skills.Last();
            Assert.That(skill.EffectiveRanks, Is.AtLeast(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
            Assert.That(skill.Bonuses, Is.Empty);
        }

        [Test]
        public void GenerateFor_GetSkillSynergy()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(1);
            AddUntrainedSkills(1);

            hitPoints.HitDice[0].Quantity = 2;

            var bonuses = new[] { new BonusDataSelection { Bonus = 9266, Target = untrainedSkills[0] } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[0])).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.Last();
            Assert.That(skill.Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skill.EffectiveRanks, Is.AtLeast(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
            Assert.That(skill.Bonuses, Is.Empty);

            skill = skills.First();
            Assert.That(skill.Name, Is.EqualTo(untrainedSkills[0]));
            Assert.That(skill.EffectiveRanks, Is.LessThan(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(2.5));
            Assert.That(skill.Bonuses, Is.Not.Empty);
            Assert.That(skill.Bonuses.Count, Is.EqualTo(1));

            var bonus = skill.Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateFor_GetSkillSynergyWithCondition()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(1);
            AddUntrainedSkills(1);

            hitPoints.HitDice[0].Quantity = 2;

            var bonuses = new[] { new BonusDataSelection { Bonus = 9266, Target = untrainedSkills[0], Condition = "condition" } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[0])).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size);
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            var skill = skills.Last();
            Assert.That(skill.Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skill.EffectiveRanks, Is.AtLeast(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(5));
            Assert.That(skill.Bonuses, Is.Empty);

            skill = skills.First();
            Assert.That(skill.Name, Is.EqualTo(untrainedSkills[0]));
            Assert.That(skill.EffectiveRanks, Is.LessThan(5));
            Assert.That(skill.EffectiveRanks, Is.EqualTo(2.5));
            Assert.That(skill.Bonuses, Is.Not.Empty);
            Assert.That(skill.Bonuses.Count, Is.EqualTo(1));

            var bonus = skill.Bonuses.Single();
            Assert.That(bonus.Condition, Is.EqualTo("condition"));
            Assert.That(bonus.IsConditional, Is.True);
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateFor_GetSkillSynergies()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(2);
            AddUntrainedSkills(2);

            hitPoints.HitDice[0].Quantity = 3;

            var bonuses1 = new[] { new BonusDataSelection { Bonus = 9266, Target = untrainedSkills[0] } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[0])).Returns(bonuses1);

            var bonuses2 = new[] { new BonusDataSelection { Bonus = 90210, Target = untrainedSkills[1] } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[1])).Returns(bonuses2);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(4));

            Assert.That(skills[2].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skills[2].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonuses, Is.Empty);

            Assert.That(skills[3].Name, Is.EqualTo(creatureSkills[1]));
            Assert.That(skills[3].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonuses, Is.Empty);

            Assert.That(skills[0].Name, Is.EqualTo(untrainedSkills[0]));
            Assert.That(skills[0].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[0].Bonuses, Is.Not.Empty);
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(1));

            var bonus = skills[0].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            Assert.That(skills[1].Name, Is.EqualTo(untrainedSkills[1]));
            Assert.That(skills[1].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[1].Bonuses, Is.Not.Empty);
            Assert.That(skills[1].Bonuses.Count, Is.EqualTo(1));

            bonus = skills[1].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(90210));
        }

        [Test]
        public void GenerateFor_GetSkillSynergiesFromSameSkill()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(2);
            AddUntrainedSkills(2);

            hitPoints.HitDice[0].Quantity = 3;

            var bonuses1 = new[]
            {
                new BonusDataSelection { Bonus = 9266, Target = untrainedSkills[0] },
                new BonusDataSelection { Bonus = 90210, Target = untrainedSkills[1] }
            };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[0])).Returns(bonuses1);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(4));

            Assert.That(skills[2].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skills[2].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonuses, Is.Empty);

            Assert.That(skills[3].Name, Is.EqualTo(creatureSkills[1]));
            Assert.That(skills[3].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonuses, Is.Empty);

            Assert.That(skills[0].Name, Is.EqualTo(untrainedSkills[0]));
            Assert.That(skills[0].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[0].Bonuses, Is.Not.Empty);
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(1));

            var bonus = skills[0].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            Assert.That(skills[1].Name, Is.EqualTo(untrainedSkills[1]));
            Assert.That(skills[1].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[1].Bonuses, Is.Not.Empty);
            Assert.That(skills[1].Bonuses.Count, Is.EqualTo(1));

            bonus = skills[1].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(90210));
        }

        [Test]
        public void GenerateFor_GetSkillSynergiesForSameSkill()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(2);
            AddUntrainedSkills(2);

            hitPoints.HitDice[0].Quantity = 3;

            var bonuses1 = new[] { new BonusDataSelection { Bonus = 9266, Target = untrainedSkills[1] } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[0])).Returns(bonuses1);

            var bonuses2 = new[] { new BonusDataSelection { Bonus = 90210, Target = untrainedSkills[1] } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[1])).Returns(bonuses2);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(4));

            Assert.That(skills[2].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skills[2].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonuses, Is.Empty);

            Assert.That(skills[3].Name, Is.EqualTo(creatureSkills[1]));
            Assert.That(skills[3].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonuses, Is.Empty);

            Assert.That(skills[0].Name, Is.EqualTo(untrainedSkills[0]));
            Assert.That(skills[0].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[0].Bonuses, Is.Empty);

            Assert.That(skills[1].Name, Is.EqualTo(untrainedSkills[1]));
            Assert.That(skills[1].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[1].Bonuses, Is.Not.Empty);
            Assert.That(skills[1].Bonuses.Count, Is.EqualTo(2));

            var bonus = skills[1].Bonuses.First();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            bonus = skills[1].Bonuses.Last();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(90210));
        }

        [Test]
        public void GenerateFor_GetSkillSynergyFromSkillWithFocus()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(2);
            AddUntrainedSkills(1);

            hitPoints.HitDice[0].Quantity = 2;

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "skill name";
            skillSelection.Focus = "focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);

            var wrongSkillSelection = new SkillDataSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "skill name";
            wrongSkillSelection.Focus = "wrong focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[1])).Returns(wrongSkillSelection);

            var bonuses = new[] { new BonusDataSelection { Bonus = 9266, Target = untrainedSkills[0] } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, SkillConstants.Build("skill name", "focus"))).Returns(bonuses);

            var wrongBonuses = new[] { new BonusDataSelection { Bonus = 666, Target = untrainedSkills[0] } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, SkillConstants.Build("skill name", "other focus"))).Returns(wrongBonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(3));

            Assert.That(skills[1].Name, Is.EqualTo("skill name"));
            Assert.That(skills[1].Focus, Is.EqualTo("focus"));
            Assert.That(skills[1].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[1].Bonuses, Is.Empty);

            Assert.That(skills[2].Name, Is.EqualTo("skill name"));
            Assert.That(skills[2].Focus, Is.EqualTo("wrong focus"));
            Assert.That(skills[2].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonuses, Is.Empty);

            Assert.That(skills[0].Name, Is.EqualTo(untrainedSkills[0]));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(2.5));
            Assert.That(skills[0].Bonuses, Is.Not.Empty);
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(1));

            var bonus = skills[0].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateFor_GetSkillSynergyForSkillWithFocus()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(2);
            AddUntrainedSkills(2);

            hitPoints.HitDice[0].Quantity = 3;

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "skill name";
            skillSelection.Focus = "focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, untrainedSkills[0])).Returns(skillSelection);

            var wrongSkillSelection = new SkillDataSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "skill name";
            wrongSkillSelection.Focus = "other focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, untrainedSkills[1])).Returns(wrongSkillSelection);

            var bonuses = new[] { new BonusDataSelection { Bonus = 9266, Target = SkillConstants.Build("skill name", "focus") } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[0])).Returns(bonuses);

            var wrongBonuses = new[] { new BonusDataSelection { Bonus = 90210, Target = SkillConstants.Build("skill name", "other focus") } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[1])).Returns(wrongBonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(4));

            Assert.That(skills[2].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skills[2].Focus, Is.Empty);
            Assert.That(skills[2].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[2].Bonuses, Is.Empty);

            Assert.That(skills[3].Name, Is.EqualTo(creatureSkills[1]));
            Assert.That(skills[3].Focus, Is.Empty);
            Assert.That(skills[3].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[3].Bonuses, Is.Empty);

            Assert.That(skills[0].Name, Is.EqualTo("skill name"));
            Assert.That(skills[0].Focus, Is.EqualTo("focus"));
            Assert.That(skills[0].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[0].Bonuses, Is.Not.Empty);
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(1));

            var bonus = skills[0].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));

            Assert.That(skills[1].Name, Is.EqualTo("skill name"));
            Assert.That(skills[1].Focus, Is.EqualTo("other focus"));
            Assert.That(skills[1].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[1].Bonuses, Is.Not.Empty);
            Assert.That(skills[1].Bonuses.Count, Is.EqualTo(1));

            bonus = skills[1].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(90210));
        }

        [Test]
        public void GenerateFor_GetSkillSynergyFromSkillWithFocusForSkillWithFocus()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(1);
            AddUntrainedSkills(1);

            hitPoints.HitDice[0].Quantity = 2;

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "skill name";
            skillSelection.Focus = "focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);

            var wrongSkillSelection = new SkillDataSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "other skill name";
            wrongSkillSelection.Focus = "other focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, untrainedSkills[0])).Returns(wrongSkillSelection);

            var bonuses = new[] { new BonusDataSelection { Bonus = 9266, Target = SkillConstants.Build("other skill name", "other focus") } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, SkillConstants.Build("skill name", "focus"))).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            Assert.That(skills[1].Name, Is.EqualTo("skill name"));
            Assert.That(skills[1].Focus, Is.EqualTo("focus"));
            Assert.That(skills[1].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[1].Bonuses, Is.Empty);

            Assert.That(skills[0].Name, Is.EqualTo("other skill name"));
            Assert.That(skills[0].Focus, Is.EqualTo("other focus"));
            Assert.That(skills[0].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(2.5));
            Assert.That(skills[0].Bonuses, Is.Not.Empty);
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(1));

            var bonus = skills[0].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateFor_GetSkillSynergyFromSkillWithoutFocusForSkillWithFocus()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(1);
            AddUntrainedSkills(1);

            hitPoints.HitDice[0].Quantity = 2;

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "skill name";
            skillSelection.Focus = "focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);

            var wrongSkillSelection = new SkillDataSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "other skill name";
            wrongSkillSelection.Focus = "other focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, untrainedSkills[0])).Returns(wrongSkillSelection);

            var bonuses = new[] { new BonusDataSelection { Bonus = 9266, Target = SkillConstants.Build("other skill name", "other focus") } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "skill name")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            Assert.That(skills[1].Name, Is.EqualTo("skill name"));
            Assert.That(skills[1].Focus, Is.EqualTo("focus"));
            Assert.That(skills[1].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[1].Bonuses, Is.Empty);

            Assert.That(skills[0].Name, Is.EqualTo("other skill name"));
            Assert.That(skills[0].Focus, Is.EqualTo("other focus"));
            Assert.That(skills[0].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(2.5));
            Assert.That(skills[0].Bonuses, Is.Not.Empty);
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(1));

            var bonus = skills[0].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateFor_GetSkillSynergyFromSkillWithFocusForSkillWithoutFocus()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(1);
            AddUntrainedSkills(1);

            hitPoints.HitDice[0].Quantity = 2;

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "skill name";
            skillSelection.Focus = "focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);

            var wrongSkillSelection = new SkillDataSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "other skill name";
            wrongSkillSelection.Focus = "other focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, untrainedSkills[0])).Returns(wrongSkillSelection);

            var bonuses = new[] { new BonusDataSelection { Bonus = 9266, Target = "other skill name" } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, SkillConstants.Build("skill name", "focus"))).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            Assert.That(skills[1].Name, Is.EqualTo("skill name"));
            Assert.That(skills[1].Focus, Is.EqualTo("focus"));
            Assert.That(skills[1].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[1].Bonuses, Is.Empty);

            Assert.That(skills[0].Name, Is.EqualTo("other skill name"));
            Assert.That(skills[0].Focus, Is.EqualTo("other focus"));
            Assert.That(skills[0].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(2.5));
            Assert.That(skills[0].Bonuses, Is.Not.Empty);
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(1));

            var bonus = skills[0].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateFor_GetSkillSynergyFromSkillWithoutFocusForSkillWithoutFocus()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(1);
            AddUntrainedSkills(1);

            hitPoints.HitDice[0].Quantity = 2;

            var skillSelection = new SkillDataSelection();
            skillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            skillSelection.SkillName = "skill name";
            skillSelection.Focus = "focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, creatureSkills[0])).Returns(skillSelection);

            var wrongSkillSelection = new SkillDataSelection();
            wrongSkillSelection.BaseAbilityName = AbilityConstants.Intelligence;
            wrongSkillSelection.SkillName = "other skill name";
            wrongSkillSelection.Focus = "other focus";

            mockSkillSelector.Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.Collection.SkillData, untrainedSkills[0])).Returns(wrongSkillSelection);

            var bonuses = new[] { new BonusDataSelection { Bonus = 9266, Target = "other skill name" } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, "skill name")).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            Assert.That(skills[1].Name, Is.EqualTo("skill name"));
            Assert.That(skills[1].Focus, Is.EqualTo("focus"));
            Assert.That(skills[1].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[1].Bonuses, Is.Empty);

            Assert.That(skills[0].Name, Is.EqualTo("other skill name"));
            Assert.That(skills[0].Focus, Is.EqualTo("other focus"));
            Assert.That(skills[0].EffectiveRanks, Is.LessThan(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(2.5));
            Assert.That(skills[0].Bonuses, Is.Not.Empty);
            Assert.That(skills[0].Bonuses.Count, Is.EqualTo(1));

            var bonus = skills[0].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateFor_GetSkillSynergyFromUntrainedSkill()
        {
            creatureTypeSkillPoints = 3;
            AddUntrainedSkills(2);

            hitPoints.HitDice[0].Quantity = 7;

            var bonuses = new[] { new BonusDataSelection { Bonus = 9266, Target = untrainedSkills[1] } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, untrainedSkills[0])).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            Assert.That(skills[0].Name, Is.EqualTo(untrainedSkills[0]));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[0].Bonuses, Is.Empty);

            Assert.That(skills[1].Name, Is.EqualTo(untrainedSkills[1]));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[1].Bonuses, Is.Not.Empty);
            Assert.That(skills[1].Bonuses.Count, Is.EqualTo(1));

            var bonus = skills[1].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateFor_GetSkillSynergyForCreatureSkill()
        {
            creatureTypeSkillPoints = 3;
            AddCreatureSkills(2);

            hitPoints.HitDice[0].Quantity = 2;

            var bonuses = new[] { new BonusDataSelection { Bonus = 9266, Target = creatureSkills[1] } };
            mockBonusSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SkillBonuses, creatureSkills[0])).Returns(bonuses);

            var skills = skillsGenerator.GenerateFor(hitPoints, "creature", creatureType, abilities, true, size).ToArray();
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(2));

            Assert.That(skills[0].Name, Is.EqualTo(creatureSkills[0]));
            Assert.That(skills[0].Focus, Is.Empty);
            Assert.That(skills[0].EffectiveRanks, Is.AtLeast(5));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[0].Bonuses, Is.Empty);

            Assert.That(skills[1].Name, Is.EqualTo(creatureSkills[1]));
            Assert.That(skills[1].Focus, Is.Empty);
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(5));
            Assert.That(skills[1].Bonuses, Is.Not.Empty);
            Assert.That(skills[1].Bonuses.Count, Is.EqualTo(1));

            var bonus = skills[1].Bonuses.Single();
            Assert.That(bonus.Condition, Is.Empty);
            Assert.That(bonus.IsConditional, Is.False);
            Assert.That(bonus.Value, Is.EqualTo(9266));
        }

        [Test]
        public void SetArmorCheckPenalties_NoSkills()
        {
            var skills = new List<Skill>();

            var equipment = new Equipment();
            equipment.Armor = new Armor { Name = "my armor", ArmorCheckPenalty = -9266 };
            equipment.Shield = new Armor { Name = "my shield", ArmorCheckPenalty = -90210 };

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties("creature", skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills).And.Empty);
        }

        [Test]
        public void SetArmorCheckPenalties_NoSkillsWithArmorCheckPenalties()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = false,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = new Armor { Name = "my armor", ArmorCheckPenalty = -42 };
            equipment.Shield = new Armor { Name = "my shield", ArmorCheckPenalty = -600 };

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties("creature", skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(2));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[0].ArmorCheckPenalty, Is.Zero);
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
        }

        [Test]
        public void SetArmorCheckPenalties_NoArmorOrShield()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = true,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = null;
            equipment.Shield = null;

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties("creature", skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(2));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[0].ArmorCheckPenalty, Is.Zero);
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
        }

        [Test]
        public void SetArmorCheckPenalties_NoArmorOrShield_Swim()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = true,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
                new Skill(SkillConstants.Swim, new Ability("ability 1"), 42)
                {
                    HasArmorCheckPenalty = true,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = null;
            equipment.Shield = null;

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties("creature", skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(3));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[0].ArmorCheckPenalty, Is.Zero);
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
            Assert.That(skills[2].Name, Is.EqualTo(SkillConstants.Swim));
            Assert.That(skills[2].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[2].ArmorCheckPenalty, Is.Zero);
        }

        [Test]
        public void SetArmorCheckPenalties_NoArmorOrShield_Swim_StormGiant()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = true,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
                new Skill(SkillConstants.Swim, new Ability("ability 1"), 42)
                {
                    HasArmorCheckPenalty = true,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = null;
            equipment.Shield = null;

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties(CreatureConstants.Giant_Storm, skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(3));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[0].ArmorCheckPenalty, Is.Zero);
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
            Assert.That(skills[2].Name, Is.EqualTo(SkillConstants.Swim));
            Assert.That(skills[2].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[2].ArmorCheckPenalty, Is.Zero);
        }

        [Test]
        public void SetArmorCheckPenalties_Armor()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = true,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = new Armor { Name = "my armor", ArmorCheckPenalty = -42 };
            equipment.Shield = null;

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties("creature", skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(2));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[0].ArmorCheckPenalty, Is.EqualTo(-42));
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
        }

        [Test]
        public void SetArmorCheckPenalties_Armor_Swim()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = true,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
                new Skill(SkillConstants.Swim, new Ability("ability 1"), 42)
                {
                    HasArmorCheckPenalty = true,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = new Armor { Name = "my armor", ArmorCheckPenalty = -600 };
            equipment.Shield = null;

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties("creature", skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(3));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[0].ArmorCheckPenalty, Is.EqualTo(-600));
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
            Assert.That(skills[2].Name, Is.EqualTo(SkillConstants.Swim));
            Assert.That(skills[2].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[2].ArmorCheckPenalty, Is.EqualTo(-1200));
        }

        [Test]
        public void SetArmorCheckPenalties_Armor_Swim_StormGiant()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = true,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
                new Skill(SkillConstants.Swim, new Ability("ability 1"), 42)
                {
                    HasArmorCheckPenalty = true,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = new Armor { Name = "my armor", ArmorCheckPenalty = -600 };
            equipment.Shield = null;

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties(CreatureConstants.Giant_Storm, skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(3));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[0].ArmorCheckPenalty, Is.EqualTo(-600));
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
            Assert.That(skills[2].Name, Is.EqualTo(SkillConstants.Swim));
            Assert.That(skills[2].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[2].ArmorCheckPenalty, Is.EqualTo(-600));
        }

        [Test]
        public void SetArmorCheckPenalties_Shield()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = true,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = null;
            equipment.Shield = new Armor { Name = "my shield", ArmorCheckPenalty = -600 };

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties("creature", skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(2));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[0].ArmorCheckPenalty, Is.EqualTo(-600));
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
        }

        [Test]
        public void SetArmorCheckPenalties_Shield_Swim()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = true,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
                new Skill(SkillConstants.Swim, new Ability("ability 1"), 42)
                {
                    HasArmorCheckPenalty = true,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = null;
            equipment.Shield = new Armor { Name = "my shield", ArmorCheckPenalty = -1337 };

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties("creature", skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(3));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[0].ArmorCheckPenalty, Is.EqualTo(-1337));
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
            Assert.That(skills[2].Name, Is.EqualTo(SkillConstants.Swim));
            Assert.That(skills[2].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[2].ArmorCheckPenalty, Is.EqualTo(-1337 * 2));
        }

        [Test]
        public void SetArmorCheckPenalties_Shield_Swim_StormGiant()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = true,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
                new Skill(SkillConstants.Swim, new Ability("ability 1"), 42)
                {
                    HasArmorCheckPenalty = true,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = null;
            equipment.Shield = new Armor { Name = "my shield", ArmorCheckPenalty = -1337 };

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties(CreatureConstants.Giant_Storm, skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(3));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[0].ArmorCheckPenalty, Is.EqualTo(-1337));
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
            Assert.That(skills[2].Name, Is.EqualTo(SkillConstants.Swim));
            Assert.That(skills[2].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[2].ArmorCheckPenalty, Is.EqualTo(-1337));
        }

        [Test]
        public void SetArmorCheckPenalties_ArmorAndShield()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = true,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = new Armor { Name = "my armor", ArmorCheckPenalty = -42 };
            equipment.Shield = new Armor { Name = "my shield", ArmorCheckPenalty = -600 };

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties("creature", skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(2));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[0].ArmorCheckPenalty, Is.EqualTo(-642));
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
        }

        [Test]
        public void SetArmorCheckPenalties_ArmorAndShield_Swim()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = true,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
                new Skill(SkillConstants.Swim, new Ability("ability 1"), 42)
                {
                    HasArmorCheckPenalty = true,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = new Armor { Name = "my armor", ArmorCheckPenalty = -600 };
            equipment.Shield = new Armor { Name = "my shield", ArmorCheckPenalty = -1337 };

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties("creature", skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(3));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[0].ArmorCheckPenalty, Is.EqualTo(-1937));
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
            Assert.That(skills[2].Name, Is.EqualTo(SkillConstants.Swim));
            Assert.That(skills[2].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[2].ArmorCheckPenalty, Is.EqualTo(-1937 * 2));
        }

        [Test]
        public void SetArmorCheckPenalties_ArmorAndShield_Swim_StormGiant()
        {
            var skills = new List<Skill>
            {
                new Skill("skill 1", new Ability("ability 1"), 9266)
                {
                    HasArmorCheckPenalty = true,
                },
                new Skill("skill 2", new Ability("ability 2"), 90210)
                {
                    HasArmorCheckPenalty = false,
                },
                new Skill(SkillConstants.Swim, new Ability("ability 1"), 42)
                {
                    HasArmorCheckPenalty = true,
                },
            };

            var equipment = new Equipment();
            equipment.Armor = new Armor { Name = "my armor", ArmorCheckPenalty = -600 };
            equipment.Shield = new Armor { Name = "my shield", ArmorCheckPenalty = -1337 };

            var modifiedSkills = skillsGenerator.SetArmorCheckPenalties(CreatureConstants.Giant_Storm, skills, equipment);

            Assert.That(modifiedSkills, Is.EqualTo(skills));
            Assert.That(skills.Count, Is.EqualTo(3));
            Assert.That(skills[0].Name, Is.EqualTo("skill 1"));
            Assert.That(skills[0].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[0].ArmorCheckPenalty, Is.EqualTo(-1937));
            Assert.That(skills[1].Name, Is.EqualTo("skill 2"));
            Assert.That(skills[1].HasArmorCheckPenalty, Is.False);
            Assert.That(skills[1].ArmorCheckPenalty, Is.Zero);
            Assert.That(skills[2].Name, Is.EqualTo(SkillConstants.Swim));
            Assert.That(skills[2].HasArmorCheckPenalty, Is.True);
            Assert.That(skills[2].ArmorCheckPenalty, Is.EqualTo(-1937));
        }

        [Test]
        public void ApplySkillPointsAsRanks_AssignRankToClassSkill()
        {
            creatureTypeSkillPoints = 2;
            var unrankedSkills = new[]
            {
                new Skill("creature skill 1", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true },
                new Skill("untrained skill 1", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
                new Skill("creature skill 2", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true },
                new Skill("untrained skill 2", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
                new Skill("creature skill 3", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true },
                new Skill("untrained skill 3", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
            };

            var index = 0;
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => sk.ClassSkill)),
                    It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => !sk.ClassSkill)),
                    null,
                    null))
                .Returns((IEnumerable<Skill> c, IEnumerable<Skill> u, IEnumerable<Skill> r, IEnumerable<Skill> v) => c.ElementAt(index++ % c.Count()));

            var count = 0;
            mockDice
                .Setup(d => d.Roll("1d4").AsSum<int>())
                .Returns(() => count++ % 4 + 1);

            var skills = skillsGenerator.ApplySkillPointsAsRanks(unrankedSkills, hitPoints, creatureType, abilities, true).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("creature skill 1"));
            Assert.That(skills[0].ClassSkill, Is.True);
            Assert.That(skills[0].Ranks, Is.EqualTo(2));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[1].Name, Is.EqualTo("untrained skill 1"));
            Assert.That(skills[1].ClassSkill, Is.False);
            Assert.That(skills[1].Ranks, Is.Zero);
            Assert.That(skills[1].EffectiveRanks, Is.Zero);
            Assert.That(skills[2].Name, Is.EqualTo("creature skill 2"));
            Assert.That(skills[2].ClassSkill, Is.True);
            Assert.That(skills[2].Ranks, Is.EqualTo(3));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[3].Name, Is.EqualTo("untrained skill 2"));
            Assert.That(skills[3].ClassSkill, Is.False);
            Assert.That(skills[3].Ranks, Is.Zero);
            Assert.That(skills[3].EffectiveRanks, Is.Zero);
            Assert.That(skills[4].Name, Is.EqualTo("creature skill 3"));
            Assert.That(skills[4].ClassSkill, Is.True);
            Assert.That(skills[4].Ranks, Is.EqualTo(3));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[5].Name, Is.EqualTo("untrained skill 3"));
            Assert.That(skills[5].ClassSkill, Is.False);
            Assert.That(skills[5].Ranks, Is.Zero);
            Assert.That(skills[5].EffectiveRanks, Is.Zero);
            Assert.That(skills.Length, Is.EqualTo(6));
        }

        [Test]
        public void ApplySkillPointsAsRanks_AssignRankToUntrainedSkill()
        {
            creatureTypeSkillPoints = 2;
            var unrankedSkills = new[]
            {
                new Skill("creature skill 1", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true },
                new Skill("untrained skill 1", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
                new Skill("creature skill 2", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true },
                new Skill("untrained skill 2", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
                new Skill("creature skill 3", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true },
                new Skill("untrained skill 3", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
            };

            var index = 0;
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => sk.ClassSkill)),
                    It.Is<IEnumerable<Skill>>(c => !c.Any() || c.All(sk => !sk.ClassSkill)),
                    null,
                    null))
                .Returns((IEnumerable<Skill> c, IEnumerable<Skill> u, IEnumerable<Skill> r, IEnumerable<Skill> v) => u.ElementAt(index++ % u.Count()));

            var count = 0;
            mockDice
                .Setup(d => d.Roll("1d4").AsSum<int>())
                .Returns(() => count++ % 4 + 1);

            var skills = skillsGenerator.ApplySkillPointsAsRanks(unrankedSkills, hitPoints, creatureType, abilities, true).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("creature skill 1"));
            Assert.That(skills[0].ClassSkill, Is.True);
            Assert.That(skills[0].Ranks, Is.Zero);
            Assert.That(skills[0].EffectiveRanks, Is.Zero);
            Assert.That(skills[1].Name, Is.EqualTo("untrained skill 1"));
            Assert.That(skills[1].ClassSkill, Is.False);
            Assert.That(skills[1].Ranks, Is.EqualTo(2));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(1));
            Assert.That(skills[2].Name, Is.EqualTo("creature skill 2"));
            Assert.That(skills[2].ClassSkill, Is.True);
            Assert.That(skills[2].Ranks, Is.Zero);
            Assert.That(skills[2].EffectiveRanks, Is.Zero);
            Assert.That(skills[3].Name, Is.EqualTo("untrained skill 2"));
            Assert.That(skills[3].ClassSkill, Is.False);
            Assert.That(skills[3].Ranks, Is.EqualTo(3));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(1.5));
            Assert.That(skills[4].Name, Is.EqualTo("creature skill 3"));
            Assert.That(skills[4].ClassSkill, Is.True);
            Assert.That(skills[4].Ranks, Is.Zero);
            Assert.That(skills[4].EffectiveRanks, Is.Zero);
            Assert.That(skills[5].Name, Is.EqualTo("untrained skill 3"));
            Assert.That(skills[5].ClassSkill, Is.False);
            Assert.That(skills[5].Ranks, Is.EqualTo(3));
            Assert.That(skills[5].EffectiveRanks, Is.EqualTo(1.5));
            Assert.That(skills.Length, Is.EqualTo(6));
        }

        [Test]
        public void ApplySkillPointsAsRanks_AssignRankToClassAndUntrainedSkill()
        {
            creatureTypeSkillPoints = 2;
            var unrankedSkills = new[]
            {
                new Skill("creature skill 1", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true },
                new Skill("untrained skill 1", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
                new Skill("creature skill 2", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true },
                new Skill("untrained skill 2", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
                new Skill("creature skill 3", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true },
                new Skill("untrained skill 3", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
            };

            var count = 0;
            mockDice
                .Setup(d => d.Roll("1d4").AsSum<int>())
                .Returns(() => count++ % 4 + 1);

            var skills = skillsGenerator.ApplySkillPointsAsRanks(unrankedSkills, hitPoints, creatureType, abilities, true).ToArray();

            Assert.That(skills[0].Name, Is.EqualTo("creature skill 1"));
            Assert.That(skills[0].ClassSkill, Is.True);
            Assert.That(skills[0].Ranks, Is.EqualTo(1));
            Assert.That(skills[0].EffectiveRanks, Is.EqualTo(1));
            Assert.That(skills[1].Name, Is.EqualTo("untrained skill 1"));
            Assert.That(skills[1].ClassSkill, Is.False);
            Assert.That(skills[1].Ranks, Is.EqualTo(1));
            Assert.That(skills[1].EffectiveRanks, Is.EqualTo(.5));
            Assert.That(skills[2].Name, Is.EqualTo("creature skill 2"));
            Assert.That(skills[2].ClassSkill, Is.True);
            Assert.That(skills[2].Ranks, Is.EqualTo(2));
            Assert.That(skills[2].EffectiveRanks, Is.EqualTo(2));
            Assert.That(skills[3].Name, Is.EqualTo("untrained skill 2"));
            Assert.That(skills[3].ClassSkill, Is.False);
            Assert.That(skills[3].Ranks, Is.EqualTo(1));
            Assert.That(skills[3].EffectiveRanks, Is.EqualTo(.5));
            Assert.That(skills[4].Name, Is.EqualTo("creature skill 3"));
            Assert.That(skills[4].ClassSkill, Is.True);
            Assert.That(skills[4].Ranks, Is.EqualTo(3));
            Assert.That(skills[4].EffectiveRanks, Is.EqualTo(3));
            Assert.That(skills[5].Name, Is.EqualTo("untrained skill 3"));
            Assert.That(skills[5].ClassSkill, Is.False);
            Assert.That(skills[5].Ranks, Is.Zero);
            Assert.That(skills[5].EffectiveRanks, Is.Zero);
            Assert.That(skills.Length, Is.EqualTo(6));
        }

        [Test]
        public void ApplySkillPointsAsRanks_AllSkillsMaxedOut()
        {
            creatureTypeSkillPoints = 3;
            var unrankedSkills = new[]
            {
                new Skill("skill 1", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true },
                new Skill("skill 2", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true },
            };

            var skills = skillsGenerator.ApplySkillPointsAsRanks(unrankedSkills, hitPoints, creatureType, abilities, true);

            Assert.That(skills.Single(s => s.Name == "skill 1").Ranks, Is.EqualTo(hitPoints.HitDiceQuantity + 3));
            Assert.That(skills.Single(s => s.Name == "skill 2").Ranks, Is.EqualTo(hitPoints.HitDiceQuantity + 3));
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
        public void ApplySkillPointsAsRanks_SkillPointsDeterminedByHitDice(int hitDiceQuantity, int skillPointsPerDie, int skillPoints)
        {
            hitPoints.HitDice[0].Quantity = hitDiceQuantity;
            creatureTypeSkillPoints = skillPointsPerDie;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            var unrankedSkills = new List<Skill>();
            while (unrankedSkills.Count < hitDiceQuantity + skillPointsPerDie)
            {
                var skill = new Skill($"skill {unrankedSkills.Count + 1}", abilities[AbilityConstants.Charisma], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true };
                unrankedSkills.Add((skill));
            }
            ;

            var skills = skillsGenerator.ApplySkillPointsAsRanks(unrankedSkills, hitPoints, creatureType, abilities, true);
            var totalRanks = skills.Sum(s => s.Ranks);

            Assert.That(totalRanks, Is.EqualTo(skillPoints));
        }

        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 2)]
        [TestCase(3, 1, 3)]
        [TestCase(4, 1, 4)]
        [TestCase(5, 1, 5)]
        [TestCase(6, 1, 6)]
        [TestCase(7, 1, 7)]
        [TestCase(8, 1, 8)]
        [TestCase(9, 1, 9)]
        [TestCase(10, 1, 10)]
        [TestCase(11, 1, 11)]
        [TestCase(12, 1, 12)]
        [TestCase(13, 1, 13)]
        [TestCase(14, 1, 14)]
        [TestCase(15, 1, 15)]
        [TestCase(16, 1, 16)]
        [TestCase(17, 1, 17)]
        [TestCase(18, 1, 18)]
        [TestCase(19, 1, 19)]
        [TestCase(20, 1, 20)]
        [TestCase(1, 2, 2)]
        [TestCase(2, 2, 4)]
        [TestCase(3, 2, 6)]
        [TestCase(4, 2, 8)]
        [TestCase(5, 2, 10)]
        [TestCase(6, 2, 12)]
        [TestCase(7, 2, 14)]
        [TestCase(8, 2, 16)]
        [TestCase(9, 2, 18)]
        [TestCase(10, 2, 20)]
        [TestCase(11, 2, 22)]
        [TestCase(12, 2, 24)]
        [TestCase(13, 2, 26)]
        [TestCase(14, 2, 28)]
        [TestCase(15, 2, 30)]
        [TestCase(16, 2, 32)]
        [TestCase(17, 2, 34)]
        [TestCase(18, 2, 36)]
        [TestCase(19, 2, 38)]
        [TestCase(20, 2, 40)]
        [TestCase(1, 8, 8)]
        [TestCase(2, 8, 16)]
        [TestCase(3, 8, 24)]
        [TestCase(4, 8, 32)]
        [TestCase(5, 8, 40)]
        [TestCase(6, 8, 48)]
        [TestCase(7, 8, 56)]
        [TestCase(8, 8, 64)]
        [TestCase(9, 8, 72)]
        [TestCase(10, 8, 80)]
        [TestCase(11, 8, 88)]
        [TestCase(12, 8, 96)]
        [TestCase(13, 8, 104)]
        [TestCase(14, 8, 112)]
        [TestCase(15, 8, 120)]
        [TestCase(16, 8, 128)]
        [TestCase(17, 8, 136)]
        [TestCase(18, 8, 144)]
        [TestCase(19, 8, 152)]
        [TestCase(20, 8, 160)]
        public void ApplySkillPointsAsRanks_SkillPointsDeterminedByHitDice_NotFirstHitDie(int hitDiceQuantity, int skillPointsPerDie, int skillPoints)
        {
            hitPoints.HitDice[0].Quantity = hitDiceQuantity;
            creatureTypeSkillPoints = skillPointsPerDie;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            var unrankedSkills = new List<Skill>();
            while (unrankedSkills.Count < hitDiceQuantity + skillPointsPerDie)
            {
                var skill = new Skill($"skill {unrankedSkills.Count + 1}", abilities[AbilityConstants.Charisma], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true };
                unrankedSkills.Add((skill));
            }
            ;

            var skills = skillsGenerator.ApplySkillPointsAsRanks(unrankedSkills, hitPoints, creatureType, abilities, false);
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
        public void ApplySkillPointsAsRanks_ApplyIntelligenceBonusToSkillPoints(int hitDie, int intelligence, int skillPoints)
        {
            hitPoints.HitDice[0].Quantity = hitDie;
            creatureTypeSkillPoints = 2;
            abilities[AbilityConstants.Intelligence].BaseScore = intelligence;
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            var unrankedSkills = new List<Skill>();
            while (unrankedSkills.Count < hitDie + intelligence)
            {
                var skill = new Skill($"skill {unrankedSkills.Count + 1}", abilities[AbilityConstants.Charisma], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true };
                unrankedSkills.Add((skill));
            }
            ;

            var skills = skillsGenerator.ApplySkillPointsAsRanks(unrankedSkills, hitPoints, creatureType, abilities, true);
            var totalRanks = skills.Sum(s => s.Ranks);

            Assert.That(totalRanks, Is.EqualTo(skillPoints));
        }

        [Test]
        public void ApplySkillPointsAsRanks_ApplyLotsOfSkillPoints()
        {
            hitPoints.HitDice[0].Quantity = 100;
            creatureTypeSkillPoints = 8;
            abilities[AbilityConstants.Intelligence].BaseScore = 18;

            var unrankedSkills = new List<Skill>();
            while (unrankedSkills.Count < 20)
            {
                var skill = new Skill($"skill {unrankedSkills.Count + 1}", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = true };
                unrankedSkills.Add(skill);
            }
            ;

            //[1,103]
            mockDice
                .Setup(d => d.Roll("1d103").AsSum<int>())
                .Returns(51);

            //[1,52]
            mockDice
                .Setup(d => d.Roll("1d52").AsSum<int>())
                .Returns(52);

            var start = DateTime.Now;
            var skills = skillsGenerator.ApplySkillPointsAsRanks(unrankedSkills, hitPoints, creatureType, abilities, true);
            var duration = DateTime.Now - start;

            var totalRanks = skills.Sum(s => s.Ranks);
            Assert.That(totalRanks, Is.EqualTo(103 * 12));
            Assert.That(duration, Is.LessThan(TimeSpan.FromMilliseconds(200)));
        }

        [Test]
        public void ApplySkillPointsAsRanks_CreaturesWithoutIntelligenceReceiveNoRanksForSkills()
        {
            abilities[AbilityConstants.Intelligence].BaseScore = 0;
            abilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);

            var unrankedSkills = new[]
            {
                new Skill("skill 1", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
                new Skill("skill 2", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
                new Skill("skill 3", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
                new Skill("skill 4", abilities[AbilityConstants.Intelligence], hitPoints.RoundedHitDiceQuantity + 3) { ClassSkill = false },
            };

            creatureTypeSkillPoints = 10;
            hitPoints.HitDice[0].Quantity = 10;

            var skills = skillsGenerator.ApplySkillPointsAsRanks(unrankedSkills, hitPoints, creatureType, abilities, true);
            Assert.That(skills, Is.Not.Empty);
            Assert.That(skills.Count, Is.EqualTo(4));

            var ranks = skills.Select(s => s.Ranks);
            Assert.That(ranks, Is.All.Zero);
        }
    }
}