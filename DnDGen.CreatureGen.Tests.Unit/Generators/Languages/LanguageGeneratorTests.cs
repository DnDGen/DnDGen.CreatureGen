using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Generators.Languages;
using DnDGen.CreatureGen.Languages;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Languages
{
    [TestFixture]
    public class LanguageGeneratorTests
    {
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private ILanguageGenerator languageGenerator;
        private List<Skill> skills;
        private List<string> automaticLanguages;
        private List<string> bonusLanguages;
        private Dictionary<string, Ability> abilities;
        private string creature;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            languageGenerator = new LanguageGenerator(mockCollectionsSelector.Object);

            skills = new List<Skill>();
            automaticLanguages = new List<string>();
            bonusLanguages = new List<string>();
            abilities = new Dictionary<string, Ability>();

            creature = "my creature";

            AddSkill("skill 1");
            AddSkill("skill 2");
            automaticLanguages.Add("lang 1");
            automaticLanguages.Add("lang 2");
            bonusLanguages.Add("lang 1");
            bonusLanguages.Add("lang 2");
            bonusLanguages.Add("lang 3");

            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);

            var index = 0;
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> ss) => ss.ElementAt(index++ % ss.Count()));
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.LanguageGroups, creature + LanguageConstants.Groups.Automatic))
                .Returns(automaticLanguages);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.LanguageGroups, creature + LanguageConstants.Groups.Bonus))
                .Returns(bonusLanguages);
        }

        private void AddSkill(string skillName, string focus = "")
        {
            var stat = new Ability("base state");
            var skill = new Skill(skillName, stat, 0, focus);

            skills.Add(skill);
        }

        [Test]
        public void GetNoLanguages()
        {
            automaticLanguages.Clear();
            bonusLanguages.Clear();

            var languages = languageGenerator.GenerateWith(creature, abilities, skills);
            Assert.That(languages, Is.Empty);
        }

        [Test]
        public void GetAutomaticLanguagesFromSelector()
        {
            bonusLanguages.Clear();

            var languages = languageGenerator.GenerateWith(creature, abilities, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetNumberOfBonusLanguagesEqualToIntelligenceModifier()
        {
            automaticLanguages.Clear();
            abilities[AbilityConstants.Intelligence].BaseScore = 14;

            var languages = languageGenerator.GenerateWith(creature, abilities, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetAllBonusLanguagesIfIntelligenceBonusIsHigher()
        {
            automaticLanguages.Clear();
            abilities[AbilityConstants.Intelligence].BaseScore = 9266;

            var languages = languageGenerator.GenerateWith(creature, abilities, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages.Count(), Is.EqualTo(3));
        }

        [Test]
        public void LanguagesContainAutomaticLanguagesAndBonusLanguages()
        {
            automaticLanguages.Clear();
            bonusLanguages.Clear();

            automaticLanguages.Add("automatic language");
            bonusLanguages.Add("bonus language");

            abilities[AbilityConstants.Intelligence].BaseScore = 12;

            var languages = languageGenerator.GenerateWith(creature, abilities, skills);
            Assert.That(languages, Contains.Item("automatic language"));
            Assert.That(languages, Contains.Item("bonus language"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void LanguagesAreNotDuplicated()
        {
            abilities[AbilityConstants.Intelligence].BaseScore = 14;

            var languages = languageGenerator.GenerateWith(creature, abilities, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 2"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages.Count(), Is.EqualTo(3));
        }

        [Test]
        public void InterpreterGainsExtraLanguage()
        {
            automaticLanguages.Clear();

            AddSkill(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter);

            abilities[AbilityConstants.Intelligence].BaseScore = 12;

            var languages = languageGenerator.GenerateWith(creature, abilities, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages, Contains.Item("lang 3"));
            Assert.That(languages.Count(), Is.EqualTo(2));
        }

        [Test]
        public void InterpreterGainsExtraLanguageEvenIfIntelligenceBonusIs0()
        {
            automaticLanguages.Clear();

            AddSkill(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter);

            var languages = languageGenerator.GenerateWith(creature, abilities, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void InterpreterGainsExtraLanguageEvenIfIntelligenceBonusIsNegative()
        {
            automaticLanguages.Clear();

            AddSkill(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter);

            abilities[AbilityConstants.Intelligence].BaseScore = 9;

            var languages = languageGenerator.GenerateWith(creature, abilities, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void NonInterpreterGainsNoExtraLanguage()
        {
            automaticLanguages.Clear();

            AddSkill(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser);

            abilities[AbilityConstants.Intelligence].BaseScore = 12;

            var languages = languageGenerator.GenerateWith(creature, abilities, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }

        [Test]
        public void NoProfessionGainsNoExtraLanguage()
        {
            automaticLanguages.Clear();

            AddSkill("other skill", SkillConstants.Foci.Profession.Adviser);

            abilities[AbilityConstants.Intelligence].BaseScore = 12;

            var languages = languageGenerator.GenerateWith(creature, abilities, skills);
            Assert.That(languages, Contains.Item("lang 1"));
            Assert.That(languages.Count(), Is.EqualTo(1));
        }
    }
}