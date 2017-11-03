using CreatureGen.Abilities;
using CreatureGen.Feats;
using CreatureGen.Generators.Feats;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Feats
{
    [TestFixture]
    public class FeatFocusGeneratorTests
    {
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private IFeatFocusGenerator featFocusGenerator;
        private List<RequiredFeatSelection> requiredFeats;
        private List<Feat> otherFeats;
        private List<Skill> skills;
        private Dictionary<string, IEnumerable<string>> focusTypes;
        private List<string> featSkillFoci;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            featFocusGenerator = new FeatFocusGenerator(mockCollectionsSelector.Object);
            requiredFeats = new List<RequiredFeatSelection>();
            otherFeats = new List<Feat>();
            skills = new List<Skill>();
            focusTypes = new Dictionary<string, IEnumerable<string>>();
            featSkillFoci = new List<string>();

            focusTypes[GroupConstants.Skills] = featSkillFoci;

            mockCollectionsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Collection.FeatFoci)).Returns(focusTypes);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.First());
        }

        [Test]
        public void FeatsWithoutFociDoNotFill()
        {
            focusTypes[""] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", string.Empty, skills, requiredFeats, otherFeats);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, It.IsAny<string>()), Times.Never);
            Assert.That(focus, Is.Empty);
        }

        [Test]
        public void FocusGenerated()
        {
            focusTypes["focus type"] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo("school 1"));
        }

        [Test]
        public void FocusRandomlyGenerated()
        {
            focusTypes["focus type"] = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(1));

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void DoNotGetDuplicateFocus()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "featToFill";
            otherFeats[0].Foci = new[] { "school 1" };

            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void DoNotGetDuplicateFoci()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "featToFill";
            otherFeats[0].Foci = new[] { "school 1", "school 2" };

            focusTypes["focus type"] = new[] { "school 1", "school 2", "school 3" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo("school 3"));
        }

        [Test]
        public void FeatsWithoutFociButWithRequirementsThatHaveFociDoNotUseSameFocus()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Foci = new[] { "focus" };

            focusTypes[""] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", string.Empty, skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.Empty);
        }

        [Test]
        public void FeatsWithFociAndRequirementsThatHaveFociUseFocus()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Foci = new[] { "focus" };

            focusTypes["focus type"] = new[] { "other focus", "focus" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void IfFeatRequirementHasMultipleFoci_PickRandomlyAmongThem()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Foci = new[] { "focus", "other focus" };

            focusTypes["focus type"] = new[] { "other focus", "focus" };
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(1));

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo("other focus"));
        }

        [Test]
        public void IfFeatRequirementHasAllAsFoci_ExplodeIt()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Foci = new[] { FeatConstants.Foci.All };

            focusTypes["focus type"] = new[] { "school 2", "school 3" };
            focusTypes["feat1"] = new[] { "school 1", "school 2" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void GeneratingFromSkillsReturnsEmptyFocusForEmptyFocusType()
        {
            focusTypes[""] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", string.Empty, skills);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.FeatFoci, It.IsAny<string>()), Times.Never);
            Assert.That(focus, Is.Empty);
        }

        [Test]
        public void GeneratingFromSkillsReturnsFocus()
        {
            focusTypes["focus type"] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills);
            Assert.That(focus, Is.EqualTo("school 1"));
        }

        [Test]
        public void GeneratingFromSkillsReturnsRandomFocus()
        {
            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(1));

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfAvailableFociFromSkillsContainsSkills_OnlyUseProvidedSkills()
        {
            focusTypes["focus type"] = new[] { "skill 1", "skill 2" };
            featSkillFoci.Add("skill 1");
            featSkillFoci.Add("skill 2");
            featSkillFoci.Add("skill 3");

            var stat = new Ability("stat");
            skills.Add(new Skill("skill 2", stat, 1));
            skills.Add(new Skill("skill 3", stat, 1));

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills);
            Assert.That(focus, Is.EqualTo("skill 2"));
        }

        [Test]
        public void IfNoWorkingSkillFociFromSkills_ReturnNoFocus()
        {
            focusTypes["focus type"] = new[] { "skill 1" };
            featSkillFoci.Add("skill 1");
            featSkillFoci.Add("skill 2");

            var stat = new Ability("stat");
            skills.Add(new Skill("skill 2", stat, 1));

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.All));
        }

        [Test]
        public void IfNotAFocusTypeWhenGeneratingWithSkills_ReturnWhatWasProvided()
        {
            focusTypes["focus type"] = new[] { "school 1" };
            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus", skills);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void IfNotAFocusType_ReturnWhatWasProvided()
        {
            focusTypes["focus type"] = new[] { "school 1" };
            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus", skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void CanBeProficientInAllFromSkills()
        {
            focusTypes["feat"] = new[] { "focus" };
            var focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom("feat", FeatConstants.Foci.All, skills);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.All));
        }

        [Test]
        public void CannotBeProficientInAllFromSkills()
        {
            focusTypes["feat"] = new[] { "focus" };
            var focus = featFocusGenerator.GenerateFrom("feat", FeatConstants.Foci.All, skills);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void CannotBeProficientInAll()
        {
            focusTypes["feat"] = new[] { "focus" };
            var focus = featFocusGenerator.GenerateFrom("feat", FeatConstants.Foci.All, skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void CannotChooseFocusWhenFocusedInAll()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat";
            otherFeats[0].Foci = new[] { FeatConstants.Foci.All };

            focusTypes["focus type"] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.All));
        }

        [Test]
        public void CannotChooseFocusWhenAllAlreadyTaken()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat";
            otherFeats[0].Foci = new[] { "school 1", "school 2" };

            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.All));
        }

        [Test]
        public void IfAvailableFociContainsSkills_OnlyUseProvidedSkills()
        {
            focusTypes["focus type"] = new[] { "skill 1", "skill 2" };
            featSkillFoci.Add("skill 1");
            featSkillFoci.Add("skill 2");
            featSkillFoci.Add("skill 3");

            var stat = new Ability("stat");
            skills.Add(new Skill("skill 2", stat, 1));
            skills.Add(new Skill("skill 3", stat, 1));

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo("skill 2"));
        }

        [Test]
        public void IfNoWorkingSkillFoci_ReturnNoFocus()
        {
            focusTypes["focus type"] = new[] { "skill 1" };
            featSkillFoci.Add("skill 1");
            featSkillFoci.Add("skill 2");

            var stat = new Ability("stat");
            skills.Add(new Skill("skill 2", stat, 1));

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.All));
        }
    }
}