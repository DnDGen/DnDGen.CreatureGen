using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.TreasureGen.Items;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Feats
{
    [TestFixture]
    public class FeatFocusGeneratorTests
    {
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private IFeatFocusGenerator featFocusGenerator;
        private List<RequiredFeatSelection> requiredFeats;
        private List<Feat> otherFeats;
        private List<Skill> skills;
        private Dictionary<string, IEnumerable<string>> focusTypes;
        private List<string> featSkillFoci;
        private Dictionary<string, Ability> abilities;
        private List<Attack> attacks;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            featFocusGenerator = new FeatFocusGenerator(mockCollectionsSelector.Object, mockTypeAndAmountSelector.Object);

            requiredFeats = new List<RequiredFeatSelection>();
            otherFeats = new List<Feat>();
            skills = new List<Skill>();
            focusTypes = new Dictionary<string, IEnumerable<string>>();
            featSkillFoci = new List<string>();
            abilities = new Dictionary<string, Ability>();
            attacks = new List<Attack>();

            focusTypes[GroupConstants.Skills] = featSkillFoci;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatFoci, It.IsAny<string>())).Returns((string table, string name) => focusTypes[name]);
            mockCollectionsSelector.Setup(s => s.IsCollection(TableNameConstants.Collection.FeatFoci, It.IsAny<string>())).Returns((string table, string name) => focusTypes.ContainsKey(name));
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.First());
            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, It.IsAny<string>())).Returns(Enumerable.Empty<TypeAndAmountSelection>());
        }

        [Test]
        public void FeatsWithoutFociDoNotFill()
        {
            focusTypes[""] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", string.Empty, skills, requiredFeats, otherFeats, 1, abilities, attacks);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Collection.FeatFoci, It.IsAny<string>()), Times.Never);
            Assert.That(focus, Is.Empty);
        }

        [Test]
        public void FocusGenerated()
        {
            focusTypes["focus type"] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("school 1"));
        }

        [Test]
        public void FocusRandomlyGenerated()
        {
            focusTypes["focus type"] = new[] { "school 1", "school 2" };
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(1));

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void DoNotGetDuplicateFocus()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "featToFill";
            otherFeats[0].Foci = new[] { "school 1" };

            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void DoNotGetDuplicateFoci()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "featToFill";
            otherFeats[0].Foci = new[] { "school 1", "school 2" };

            focusTypes["focus type"] = new[] { "school 1", "school 2", "school 3" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("school 3"));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(42)]
        [TestCase(600)]
        [TestCase(1337)]
        [TestCase(9266)]
        [TestCase(90210)]
        public void SpellcastersCanSelectRayForWeaponFoci(int casterLevel)
        {
            //INFO: You won't have this focus type without a proficiency requirement
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay] = new[] { FeatConstants.Foci.Weapons.Ray, "weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay, skills, requiredFeats, otherFeats, casterLevel, abilities, attacks);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.Weapons.Ray));
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void NonSpellcastersCannotSelectRayForWeaponFoci(int casterLevel)
        {
            //INFO: You won't have this focus type without a proficiency requirement
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay] = new[] { FeatConstants.Foci.Weapons.Ray, "weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay, skills, requiredFeats, otherFeats, casterLevel, abilities, attacks);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.NoValidFociAvailable));
        }

        [Test]
        public void FeatsWithoutFociButWithRequirementsThatHaveFociDoNotUseSameFocus()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Foci = new[] { "focus" };

            focusTypes[""] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", string.Empty, skills, requiredFeats, otherFeats, 1, abilities, attacks);
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

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void IfFeatRequirementHasMultipleFoci_PickRandomlyAmongThem()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Foci = new[] { "focus", "other focus" };

            focusTypes["focus type"] = new[] { "focus", "other focus" };
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(1));

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("other focus"));
        }

        [Test]
        public void IfFeatRequirementHasAllAsFoci_ExplodeIt()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = "feat1" });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Foci = new[] { GroupConstants.All };

            focusTypes["focus type"] = new[] { "school 2", "school 3" };
            focusTypes["feat1"] = new[] { "school 1", "school 2" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfWeaponFamiliarityAndAllMartialOnRequirement_AddInFamiliarityTypes()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());

            otherFeats[0].Name = FeatConstants.WeaponProficiency_Martial;
            otherFeats[0].Foci = new[] { GroupConstants.All };
            otherFeats[1].Name = FeatConstants.SpecialQualities.WeaponFamiliarity;
            otherFeats[1].Foci = new[] { "weird weapon" };

            focusTypes["focus type"] = new[] { "school 2", "school 3", "weird weapon" };
            focusTypes[FeatConstants.WeaponProficiency_Martial] = new[] { "school 1", "school 2" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "school 2", "school 3", "school 1", "weird weapon", "school 5" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2", FeatConstants.WeaponProficiency_Martial };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(1));

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("weird weapon"));
        }

        [Test]
        public void ProficiencyFulfillsProficiencyRequirement()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Foci = new[] { "school 2" };

            focusTypes["focus type"] = new[] { "school 1", "school 2" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "school 2", "school 3", "school 1", "school 4", "school 5" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void ProficiencyWithAllFulfillsProficiencyRequirement()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Foci = new[] { GroupConstants.All };

            focusTypes["focus type"] = new[] { "school 1", "school 2" };
            focusTypes["proficiency2"] = new[] { "school 2", "school 3" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "school 2", "school 3", "school 1", "school 4", "school 5" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void ProficiencyFulfillsSpecificProficiencyRequirement()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency, Foci = new[] { "specific weapon" } });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Foci = new[] { "wrong weapon", "specific weapon" };

            focusTypes["focus type"] = new[] { "wrong weapon", "other weapon", "specific weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "specific weapon", "other weapon", "wrong weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("specific weapon"));
        }

        [Test]
        public void ProficiencyFulfillsAnySpecificProficiencyRequirement()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency, Foci = new[] { "other specific weapon", "specific weapon" } });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Foci = new[] { "wrong weapon", "specific weapon" };

            focusTypes["focus type"] = new[] { "wrong weapon", "other weapon", "specific weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "specific weapon", "other weapon", "wrong weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("specific weapon"));
        }

        [Test]
        public void ProficiencyWithAllFulfillsSpecificProficiencyRequirement()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency, Foci = new[] { "specific weapon" } });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Foci = new[] { GroupConstants.All };

            focusTypes["focus type"] = new[] { "wrong weapon", "other weapon", "specific weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "specific weapon", "other weapon", "wrong weapon" };
            focusTypes["proficiency2"] = new[] { "other weapon", "specific weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("specific weapon"));
        }

        [Test]
        public void SpecificProficiencyRequirementUnfulfilled()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency, Foci = new[] { "specific weapon" } });
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Foci = new[] { "wrong weapon" };

            focusTypes["focus type"] = new[] { "wrong weapon", "other weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "specific weapon", "other weapon", "wrong weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.NoValidFociAvailable));
        }

        [Test]
        public void IfWeaponFamiliarityAndMartialWeaponFocusType_AddInFamiliarityTypes()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = FeatConstants.SpecialQualities.WeaponFamiliarity;
            otherFeats[0].Foci = new[] { "weird weapon" };

            focusTypes[FeatConstants.WeaponProficiency_Martial] = new[] { "school 2", "school 3" };

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(2));

            var focus = featFocusGenerator.GenerateFrom(FeatConstants.WeaponProficiency_Martial, GroupConstants.All, skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("weird weapon"));
        }

        [Test]
        public void AvailableFociAreIntersectionOfAllDependentFeats()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = "feat" });
            requiredFeats.Add(new RequiredFeatSelection { Feat = "improved feat" });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());

            otherFeats[0].Name = "feat";
            otherFeats[0].Foci = new[] { "school 1", "school 4" };
            otherFeats[1].Name = "improved feat";
            otherFeats[1].Foci = new[] { "school 4" };

            focusTypes["focus type"] = new[] { "school 1", "school 2", "school 3", "school 4", "school 5", "school 6" };

            var focus = featFocusGenerator.GenerateFrom("greater feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("school 4"));
        }

        [Test]
        public void AvailableFociAreIntersectionOfAllDependentFeatsWithProficiencies()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });
            requiredFeats.Add(new RequiredFeatSelection { Feat = "feat" });
            requiredFeats.Add(new RequiredFeatSelection { Feat = "improved feat" });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());

            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Foci = new[] { GroupConstants.All };
            otherFeats[1].Name = "proficiency1";
            otherFeats[1].Foci = new[] { "school 4" };
            otherFeats[2].Name = "feat";
            otherFeats[2].Foci = new[] { "school 1", "school 4", "school 2" };
            otherFeats[3].Name = "improved feat";
            otherFeats[3].Foci = new[] { "school 3", "school 2" };

            focusTypes["focus type"] = new[] { "school 1", "school 2", "school 3", "school 4", "school 5", "school 6" };
            focusTypes["proficiency1"] = new[] { "school 5", "school 4" };
            focusTypes["proficiency2"] = new[] { "school 2", "school 3", "school 1" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "school 2", "school 3", "school 1", "school 4", "school 5" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("greater feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void GeneratingFromSkillsReturnsEmptyFocusForEmptyFocusType()
        {
            focusTypes[""] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", string.Empty, skills, abilities);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Collection.FeatFoci, It.IsAny<string>()), Times.Never);
            Assert.That(focus, Is.Empty);
        }

        [Test]
        public void GeneratingFromSkillsReturnsFocus()
        {
            focusTypes["focus type"] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, abilities);
            Assert.That(focus, Is.EqualTo("school 1"));
        }

        [Test]
        public void GeneratingFromSkillsReturnsRandomFocus()
        {
            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(1));

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, abilities);
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

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, abilities);
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

            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus type", skills, abilities);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.NoValidFociAvailable));
        }

        [Test]
        public void ReturnPresetFocusWhenGeneratingWithSkills()
        {
            focusTypes["focus type"] = new[] { "school 1" };
            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus", skills, abilities);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void ReturnPresetFocus()
        {
            focusTypes["focus type"] = new[] { "school 1" };
            var focus = featFocusGenerator.GenerateFrom("featToFill", "focus", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void IfWeaponFamiliarityAndExoticWeaponProficiency_DoNotPickFamiliarityFocus()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = FeatConstants.SpecialQualities.WeaponFamiliarity;
            otherFeats[0].Foci = new[] { "weird weapon" };

            focusTypes[FeatConstants.WeaponProficiency_Exotic] = new[] { "weird weapon", "school 2" };

            var focus = featFocusGenerator.GenerateFrom(FeatConstants.WeaponProficiency_Exotic, GroupConstants.All, skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfNoWeaponFamiliarity_UseOnlyMartialWeapons()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "other feat";
            otherFeats[0].Foci = new[] { "weird weapon" };

            focusTypes[FeatConstants.WeaponProficiency_Martial] = new[] { "school 2" };

            var focus = featFocusGenerator.GenerateFrom(FeatConstants.WeaponProficiency_Martial, GroupConstants.All, skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("school 2"));
        }

        [Test]
        public void IfNoWeaponFamiliarity_UseAllExoticWeapons()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "other feat";
            otherFeats[0].Foci = new[] { "weird weapon" };

            focusTypes[FeatConstants.WeaponProficiency_Exotic] = new[] { "weird weapon", "school 2" };

            var focus = featFocusGenerator.GenerateFrom(FeatConstants.WeaponProficiency_Exotic, GroupConstants.All, skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("weird weapon"));
        }

        [Test]
        public void CanBeProficientInAllFromSkills()
        {
            focusTypes["feat"] = new[] { "focus" };
            var focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom("feat", GroupConstants.All, skills, abilities);
            Assert.That(focus, Is.EqualTo(GroupConstants.All));
        }

        [Test]
        public void CannotBeProficientInAllFromSkills()
        {
            focusTypes["feat"] = new[] { "focus" };
            var focus = featFocusGenerator.GenerateFrom("feat", GroupConstants.All, skills, abilities);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void CannotBeProficientInAll()
        {
            focusTypes["feat"] = new[] { "focus" };
            var focus = featFocusGenerator.GenerateFrom("feat", GroupConstants.All, skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [TestCase(FeatConstants.Foci.WeaponWithUnarmed)]
        [TestCase(FeatConstants.Foci.WeaponWithUnarmedAndGrapple)]
        [TestCase(FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay)]
        public void CanFocusInUnarmedStrikeWhenProficiencyIsRequirement(string focusType)
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmed] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrapple] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike, FeatConstants.Foci.Weapons.Grapple };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike, FeatConstants.Foci.Weapons.Grapple, FeatConstants.Foci.Weapons.Ray };

            var focus = featFocusGenerator.GenerateFrom("featToFill", focusType, skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.Weapons.UnarmedStrike));
        }

        [TestCase(FeatConstants.Foci.Weapon)]
        public void CannotFocusInUnarmedStrikeWhenProficiencyIsRequirement(string focusType)
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmed] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrapple] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike, FeatConstants.Foci.Weapons.Grapple };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike, FeatConstants.Foci.Weapons.Grapple, FeatConstants.Foci.Weapons.Ray };

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateFrom("featToFill", focusType, skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.NoValidFociAvailable));
        }

        [TestCase(FeatConstants.Foci.WeaponWithUnarmedAndGrapple)]
        [TestCase(FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay)]
        public void CanFocusInGrappleWhenProficiencyIsRequirement(string focusType)
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmed] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrapple] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike, FeatConstants.Foci.Weapons.Grapple };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike, FeatConstants.Foci.Weapons.Grapple, FeatConstants.Foci.Weapons.Ray };

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateFrom("featToFill", focusType, skills, requiredFeats, otherFeats, 0, abilities, attacks);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.Weapons.Grapple));
        }

        [TestCase(FeatConstants.Foci.Weapon)]
        [TestCase(FeatConstants.Foci.WeaponWithUnarmed)]
        public void CannotFocusInGrappleWhenProficiencyIsRequirement(string focusType)
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmed] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrapple] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike, FeatConstants.Foci.Weapons.Grapple };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike, FeatConstants.Foci.Weapons.Grapple, FeatConstants.Foci.Weapons.Ray };

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateFrom("featToFill", focusType, skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.Not.EqualTo(FeatConstants.Foci.Weapons.Grapple));
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.NoValidFociAvailable).Or.EqualTo(FeatConstants.Foci.Weapons.UnarmedStrike));
        }

        [TestCase(FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay, 1)]
        public void CanFocusInRayWhenProficiencyIsRequirement(string focusType, int casterLevel)
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmed] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrapple] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike, FeatConstants.Foci.Weapons.Grapple };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike, FeatConstants.Foci.Weapons.Grapple, FeatConstants.Foci.Weapons.Ray };

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateFrom("featToFill", focusType, skills, requiredFeats, otherFeats, casterLevel, abilities, attacks);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.Weapons.Ray));
        }

        [TestCase(FeatConstants.Foci.Weapon, 0)]
        [TestCase(FeatConstants.Foci.Weapon, 1)]
        [TestCase(FeatConstants.Foci.WeaponWithUnarmed, 0)]
        [TestCase(FeatConstants.Foci.WeaponWithUnarmed, 1)]
        [TestCase(FeatConstants.Foci.WeaponWithUnarmedAndGrapple, 0)]
        [TestCase(FeatConstants.Foci.WeaponWithUnarmedAndGrapple, 1)]
        [TestCase(FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay, 0)]
        public void CannotFocusInRayWhenProficiencyIsRequirement(string focusType, int casterLevel)
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmed] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrapple] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike, FeatConstants.Foci.Weapons.Grapple };
            focusTypes[FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay] = new[] { "weapon", "other weapon", FeatConstants.Foci.Weapons.UnarmedStrike, FeatConstants.Foci.Weapons.Grapple, FeatConstants.Foci.Weapons.Ray };

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateFrom("featToFill", focusType, skills, requiredFeats, otherFeats, casterLevel, abilities, attacks);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.NoValidFociAvailable).Or.EqualTo(FeatConstants.Foci.Weapons.Grapple).Or.EqualTo(FeatConstants.Foci.Weapons.UnarmedStrike));
        }

        [TestCase(FeatConstants.Foci.Weapons.Grapple)]
        [TestCase(FeatConstants.Foci.Weapons.Ray)]
        [TestCase(FeatConstants.Foci.Weapons.UnarmedStrike)]
        public void CanFocusInAutomaticProficiencyEvenWhenProficientWithWeapons(string automaticProficiency)
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Foci = new[] { "weapon" };

            focusTypes["focus type"] = new[] { "weapon", "other weapon", automaticProficiency };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo(automaticProficiency));
        }

        [TestCase(FeatConstants.Foci.Weapons.Grapple)]
        [TestCase(FeatConstants.Foci.Weapons.Ray)]
        [TestCase(FeatConstants.Foci.Weapons.UnarmedStrike)]
        public void CannotFocusInAutomaticProficiencyEvenWhenProficientWithWeapons(string automaticProficiency)
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });
            requiredFeats.Add(new RequiredFeatSelection { Feat = "required feat" });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "proficiency2";
            otherFeats[0].Foci = new[] { "weapon" };
            otherFeats[1].Name = "required feat";
            otherFeats[1].Foci = new[] { "weapon" };

            focusTypes["focus type"] = new[] { "weapon", "other weapon", automaticProficiency };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };

            var proficiencyFeats = new[] { "proficiency1", "proficiency2" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("weapon"));
        }

        [Test]
        public void CannotChooseFocusWhenFocusedInAll()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat";
            otherFeats[0].Foci = new[] { GroupConstants.All };

            focusTypes["focus type"] = new[] { "school 1" };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.NoValidFociAvailable));
        }

        [Test]
        public void CannotChooseFocusWhenAllAlreadyTaken()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat";
            otherFeats[0].Foci = new[] { "school 1", "school 2" };

            focusTypes["focus type"] = new[] { "school 1", "school 2" };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.NoValidFociAvailable));
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

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
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

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.NoValidFociAvailable));
        }

        [TestCase(FeatConstants.Foci.Weapon)]
        [TestCase(FeatConstants.Foci.WeaponWithUnarmed)]
        [TestCase(FeatConstants.Foci.WeaponWithUnarmedAndGrapple)]
        [TestCase(FeatConstants.Foci.WeaponWithUnarmedAndGrappleAndRay)]
        public void FeatWithRequiredFeatThatHasWeaponFocusHonorsOnlyThatWeaponFocus(string focusType)
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = "feat1" });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "feat1";
            otherFeats[0].Foci = new[] { "weapon" };
            otherFeats[1].Name = "all proficiency";
            otherFeats[1].Foci = new[] { GroupConstants.All };
            otherFeats[2].Name = "specific proficiency";
            otherFeats[2].Foci = new[] { "specific weapon" };

            focusTypes[focusType] = new[] {
                "specific weapon",
                "other weapon",
                "weapon",
                FeatConstants.Foci.Weapons.UnarmedStrike,
                FeatConstants.Foci.Weapons.Grapple,
                FeatConstants.Foci.Weapons.Ray
            };

            focusTypes["all proficiency"] = new[] { "proficiency weapon", "other weapon", "weapon" };

            var proficiencyFeats = new[] { "all proficiency", "specific proficiency" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(c => c.Single() == "weapon"))).Returns("only weapon");

            var focus = featFocusGenerator.GenerateFrom("featToFill", focusType, skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("only weapon"));
        }

        [TestCase("proficiency feat", "proficiency focus")]
        [TestCase(FeatConstants.WeaponProficiency_Simple, WeaponConstants.Club)]
        [TestCase(FeatConstants.WeaponProficiency_Martial, WeaponConstants.Longsword)]
        [TestCase(FeatConstants.WeaponProficiency_Exotic, WeaponConstants.GnomeHookedHammer)]
        public void FocusCanBeFromProficiency(string featName, string proficiencyFocus)
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = featName;
            otherFeats[0].Foci = new[] { proficiencyFocus };

            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            focusTypes["focus type"] = new[] { "weapon", proficiencyFocus };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon", proficiencyFocus };

            var proficiencyFeats = new[] { "other proficiency feat", featName };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo(proficiencyFocus));
        }

        [Test]
        public void FocusCanBeFromProficiencyWithNaturalAttack()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "all proficiency";
            otherFeats[0].Foci = new[] { GroupConstants.All };
            otherFeats[1].Name = "specific proficiency";
            otherFeats[1].Foci = new[] { "specific weapon" };

            focusTypes["focus type"] = new[] { "weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };
            focusTypes["all proficiency"] = new[] { "proficiency weapon", "other weapon", "weapon" };

            var proficiencyFeats = new[] { "all proficiency", "specific proficiency" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            attacks.Add(new Attack { Name = "claw", IsNatural = true });
            attacks.Add(new Attack { Name = "bite", IsNatural = true });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("bite"));
        }

        [Test]
        public void FocusCannotBeFromProficiencyWithUnnaturalAttack()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "all proficiency";
            otherFeats[0].Foci = new[] { GroupConstants.All };
            otherFeats[1].Name = "specific proficiency";
            otherFeats[1].Foci = new[] { "specific weapon" };

            focusTypes["focus type"] = new[] { "weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "other weapon", "weapon" };
            focusTypes["all proficiency"] = new[] { "proficiency weapon", "other weapon", "weapon" };

            var proficiencyFeats = new[] { "all proficiency", "specific proficiency" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            attacks.Add(new Attack { Name = "claw", IsNatural = false });
            attacks.Add(new Attack { Name = "bite", IsNatural = false });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("weapon"));
        }

        [Test]
        public void FocusCannotDuplicateProficiencyWithNaturalAttack()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "all proficiency";
            otherFeats[0].Foci = new[] { GroupConstants.All };
            otherFeats[1].Name = "specific proficiency";
            otherFeats[1].Foci = new[] { "specific weapon" };
            otherFeats[2].Name = "feat";
            otherFeats[2].Foci = new[] { "claw" };

            focusTypes["focus type"] = new[] { "weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };
            focusTypes["all proficiency"] = new[] { "proficiency weapon", "other weapon", "weapon" };

            var proficiencyFeats = new[] { "all proficiency", "specific proficiency" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            attacks.Add(new Attack { Name = "bite", IsNatural = true });
            attacks.Add(new Attack { Name = "claw", IsNatural = true });
            attacks.Add(new Attack { Name = "claw", IsNatural = true });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("bite"));
        }

        [Test]
        public void FocusCannotDuplicateProficiencyWithAllNaturalAttacks()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "all proficiency";
            otherFeats[0].Foci = new[] { GroupConstants.All };
            otherFeats[1].Name = "specific proficiency";
            otherFeats[1].Foci = new[] { "specific weapon" };
            otherFeats[2].Name = "feat";
            otherFeats[2].Foci = new[] { "claw", "bite" };

            focusTypes["focus type"] = new[] { "weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "other weapon", "weapon" };
            focusTypes["all proficiency"] = new[] { "proficiency weapon", "other weapon", "weapon" };

            var proficiencyFeats = new[] { "all proficiency", "specific proficiency" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            attacks.Add(new Attack { Name = "bite", IsNatural = true });
            attacks.Add(new Attack { Name = "claw", IsNatural = true });
            attacks.Add(new Attack { Name = "claw", IsNatural = true });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("weapon"));
        }

        [Test]
        public void AttacksNotFocusForNonProficiencyFeats()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "all proficiency";
            otherFeats[0].Foci = new[] { GroupConstants.All };
            otherFeats[1].Name = "specific proficiency";
            otherFeats[1].Foci = new[] { "specific weapon" };

            focusTypes["focus type"] = new[] { "focus" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };
            focusTypes["all proficiency"] = new[] { "proficiency weapon", "other weapon", "weapon" };

            var proficiencyFeats = new[] { "all proficiency", "specific proficiency" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            attacks.Add(new Attack { Name = "bite", IsNatural = true });
            attacks.Add(new Attack { Name = "claw", IsNatural = true });
            attacks.Add(new Attack { Name = "claw", IsNatural = true });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void FocusCannotBeFulfilledBecauseCannotUseWeaponsAndHasNoNaturalWeapons()
        {
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "other feat";
            otherFeats[0].Foci = new[] { "other focus" };

            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            var proficiencyFeats = new[] { "other proficiency feat", "proficiency feat" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            focusTypes["focus type"] = new[] { "weapon", "other weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo(FeatConstants.Foci.NoValidFociAvailable));
        }

        //INFO: Example is Bastard Sword for exotic weapon proficiency requiring Strength of 13
        [Test]
        public void FocusForFeatCanHaveAbilityRequirementUnmet()
        {
            var abilityRequirements = new List<TypeAndAmountSelection>();
            abilityRequirements.Add(new TypeAndAmountSelection
            {
                Type = "ability",
                Amount = 13
            });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "feat/focus")).Returns(abilityRequirements);

            focusTypes["focus type"] = new[] { "focus", "other focus" };
            abilities["other ability"] = new Ability("other ability") { BaseScore = 13 };
            abilities["ability"] = new Ability("ability") { BaseScore = 12 };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("other focus"));
        }

        [TestCase(13)]
        [TestCase(14)]
        public void FocusForFeatCanHaveAbilityRequirementMet(int abilityScore)
        {
            var abilityRequirements = new List<TypeAndAmountSelection>();
            abilityRequirements.Add(new TypeAndAmountSelection
            {
                Type = "ability",
                Amount = 13
            });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "feat/focus")).Returns(abilityRequirements);

            focusTypes["focus type"] = new[] { "focus", "other focus" };
            abilities["other ability"] = new Ability("other ability") { BaseScore = 13 };
            abilities["ability"] = new Ability("ability") { BaseScore = abilityScore };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void FocusForFeatCanHaveAnyAbilityRequirementMet()
        {
            var abilityRequirements = new List<TypeAndAmountSelection>();
            abilityRequirements.Add(new TypeAndAmountSelection
            {
                Type = "ability",
                Amount = 13
            });
            abilityRequirements.Add(new TypeAndAmountSelection
            {
                Type = "other ability",
                Amount = 10
            });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "feat/focus")).Returns(abilityRequirements);

            focusTypes["focus type"] = new[] { "focus", "other focus" };
            abilities["other ability"] = new Ability("other ability") { BaseScore = 13 };
            abilities["ability"] = new Ability("ability") { BaseScore = 10 };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void FocusForFeatCanHaveNoAbilityRequirementForFocus()
        {
            var abilityRequirements = new List<TypeAndAmountSelection>();
            abilityRequirements.Add(new TypeAndAmountSelection
            {
                Type = "ability",
                Amount = 13
            });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "feat/focus")).Returns(abilityRequirements);

            focusTypes["focus type"] = new[] { "focus", "other focus" };
            abilities["other ability"] = new Ability("other ability") { BaseScore = 13 };
            abilities["ability"] = new Ability("ability") { BaseScore = 12 };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("other focus"));
        }

        [Test]
        public void FocusForFeatCanHaveNoAbilityRequirementForFeat()
        {
            var abilityRequirements = new List<TypeAndAmountSelection>();
            abilityRequirements.Add(new TypeAndAmountSelection
            {
                Type = "ability",
                Amount = 13
            });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "feat/focus")).Returns(abilityRequirements);

            focusTypes["focus type"] = new[] { "focus", "other focus" };
            abilities["other ability"] = new Ability("other ability") { BaseScore = 13 };
            abilities["ability"] = new Ability("ability") { BaseScore = 12 };

            var focus = featFocusGenerator.GenerateFrom("other feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        //INFO: Example is Bastard Sword for exotic weapon proficiency requiring Strength of 13
        [Test]
        public void FocusForFeatCanHaveAbilityRequirementUnmetFromSkills()
        {
            var abilityRequirements = new List<TypeAndAmountSelection>();
            abilityRequirements.Add(new TypeAndAmountSelection
            {
                Type = "ability",
                Amount = 13
            });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "feat/focus")).Returns(abilityRequirements);

            focusTypes["focus type"] = new[] { "focus", "other focus" };
            abilities["other ability"] = new Ability("other ability") { BaseScore = 13 };
            abilities["ability"] = new Ability("ability") { BaseScore = 12 };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, abilities);
            Assert.That(focus, Is.EqualTo("other focus"));
        }

        [TestCase(13)]
        [TestCase(14)]
        public void FocusForFeatCanHaveAbilityRequirementMetFromSkills(int abilityScore)
        {
            var abilityRequirements = new List<TypeAndAmountSelection>();
            abilityRequirements.Add(new TypeAndAmountSelection
            {
                Type = "ability",
                Amount = 13
            });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "feat/focus")).Returns(abilityRequirements);

            focusTypes["focus type"] = new[] { "focus", "other focus" };
            abilities["other ability"] = new Ability("other ability") { BaseScore = 13 };
            abilities["ability"] = new Ability("ability") { BaseScore = abilityScore };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, abilities);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void FocusForFeatCanHaveAnyAbilityRequirementMetFromSkills()
        {
            var abilityRequirements = new List<TypeAndAmountSelection>();
            abilityRequirements.Add(new TypeAndAmountSelection
            {
                Type = "ability",
                Amount = 13
            });
            abilityRequirements.Add(new TypeAndAmountSelection
            {
                Type = "other ability",
                Amount = 10
            });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "feat/focus")).Returns(abilityRequirements);

            focusTypes["focus type"] = new[] { "focus", "other focus" };
            abilities["other ability"] = new Ability("other ability") { BaseScore = 13 };
            abilities["ability"] = new Ability("ability") { BaseScore = 10 };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, abilities);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void FocusForFeatCanHaveNoAbilityRequirementForFocusFromSkills()
        {
            var abilityRequirements = new List<TypeAndAmountSelection>();
            abilityRequirements.Add(new TypeAndAmountSelection
            {
                Type = "ability",
                Amount = 13
            });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "feat/focus")).Returns(abilityRequirements);

            focusTypes["focus type"] = new[] { "focus", "other focus" };
            abilities["other ability"] = new Ability("other ability") { BaseScore = 13 };
            abilities["ability"] = new Ability("ability") { BaseScore = 12 };

            var focus = featFocusGenerator.GenerateFrom("feat", "focus type", skills, abilities);
            Assert.That(focus, Is.EqualTo("other focus"));
        }

        [Test]
        public void FocusForFeatCanHaveNoAbilityRequirementForFeatFromSkills()
        {
            var abilityRequirements = new List<TypeAndAmountSelection>();
            abilityRequirements.Add(new TypeAndAmountSelection
            {
                Type = "ability",
                Amount = 13
            });

            mockTypeAndAmountSelector.Setup(s => s.Select(TableNameConstants.TypeAndAmount.FeatAbilityRequirements, "feat/focus")).Returns(abilityRequirements);

            focusTypes["focus type"] = new[] { "focus", "other focus" };
            abilities["other ability"] = new Ability("other ability") { BaseScore = 13 };
            abilities["ability"] = new Ability("ability") { BaseScore = 12 };

            var focus = featFocusGenerator.GenerateFrom("other feat", "focus type", skills, abilities);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void FocusAllowingAllCanBeFromProficiencyWithNaturalAttack()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "all proficiency";
            otherFeats[0].Foci = new[] { GroupConstants.All };
            otherFeats[1].Name = "specific proficiency";
            otherFeats[1].Foci = new[] { "specific weapon" };

            focusTypes["focus type"] = new[] { "weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };
            focusTypes["all proficiency"] = new[] { "proficiency weapon", "other weapon", "weapon" };

            var proficiencyFeats = new[] { "all proficiency", "specific proficiency" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            attacks.Add(new Attack { Name = "claw", IsNatural = true });
            attacks.Add(new Attack { Name = "bite", IsNatural = true });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("bite"));
        }

        [Test]
        public void FocusAllowingAllCannotBeFromProficiencyWithUnnaturalAttack()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "all proficiency";
            otherFeats[0].Foci = new[] { GroupConstants.All };
            otherFeats[1].Name = "specific proficiency";
            otherFeats[1].Foci = new[] { "specific weapon" };

            focusTypes["focus type"] = new[] { "weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "other weapon", "weapon" };
            focusTypes["all proficiency"] = new[] { "proficiency weapon", "other weapon", "weapon" };

            var proficiencyFeats = new[] { "all proficiency", "specific proficiency" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            attacks.Add(new Attack { Name = "claw", IsNatural = false });
            attacks.Add(new Attack { Name = "bite", IsNatural = false });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("weapon"));
        }

        [Test]
        public void FocusAllowingAllCannotDuplicateProficiencyWithNaturalAttack()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "all proficiency";
            otherFeats[0].Foci = new[] { GroupConstants.All };
            otherFeats[1].Name = "specific proficiency";
            otherFeats[1].Foci = new[] { "specific weapon" };
            otherFeats[2].Name = "feat";
            otherFeats[2].Foci = new[] { "claw" };

            focusTypes["focus type"] = new[] { "weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };
            focusTypes["all proficiency"] = new[] { "proficiency weapon", "other weapon", "weapon" };

            var proficiencyFeats = new[] { "all proficiency", "specific proficiency" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            attacks.Add(new Attack { Name = "bite", IsNatural = true });
            attacks.Add(new Attack { Name = "claw", IsNatural = true });
            attacks.Add(new Attack { Name = "claw", IsNatural = true });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("bite"));
        }

        [Test]
        public void FocusAllowingAllCannotDuplicateProficiencyWithAllNaturalAttacks()
        {
            requiredFeats.Add(new RequiredFeatSelection { Feat = GroupConstants.WeaponProficiency });

            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "all proficiency";
            otherFeats[0].Foci = new[] { GroupConstants.All };
            otherFeats[1].Name = "specific proficiency";
            otherFeats[1].Foci = new[] { "specific weapon" };
            otherFeats[2].Name = "feat";
            otherFeats[2].Foci = new[] { "claw", "bite" };

            focusTypes["focus type"] = new[] { "weapon" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "other weapon", "weapon" };
            focusTypes["all proficiency"] = new[] { "proficiency weapon", "other weapon", "weapon" };

            var proficiencyFeats = new[] { "all proficiency", "specific proficiency" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            attacks.Add(new Attack { Name = "bite", IsNatural = true });
            attacks.Add(new Attack { Name = "claw", IsNatural = true });
            attacks.Add(new Attack { Name = "claw", IsNatural = true });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("weapon"));
        }

        [Test]
        public void AttacksNotFocusAllowingAllForNonProficiencyFeats()
        {
            otherFeats.Add(new Feat());
            otherFeats.Add(new Feat());
            otherFeats[0].Name = "all proficiency";
            otherFeats[0].Foci = new[] { GroupConstants.All };
            otherFeats[1].Name = "specific proficiency";
            otherFeats[1].Foci = new[] { "specific weapon" };

            focusTypes["focus type"] = new[] { "focus" };
            focusTypes[FeatConstants.Foci.Weapon] = new[] { "weapon", "other weapon" };
            focusTypes["all proficiency"] = new[] { "proficiency weapon", "other weapon", "weapon" };

            var proficiencyFeats = new[] { "all proficiency", "specific proficiency" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.WeaponProficiency)).Returns(proficiencyFeats);

            attacks.Add(new Attack { Name = "bite", IsNatural = true });
            attacks.Add(new Attack { Name = "claw", IsNatural = true });
            attacks.Add(new Attack { Name = "claw", IsNatural = true });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            var focus = featFocusGenerator.GenerateAllowingFocusOfAllFrom("feat", "focus type", skills, requiredFeats, otherFeats, 1, abilities, attacks);
            Assert.That(focus, Is.EqualTo("focus"));
        }

        [Test]
        public void FocusIsNotPresetIfEmpty()
        {
            var preset = featFocusGenerator.FocusTypeIsPreset(string.Empty);
            Assert.That(preset, Is.False);
        }

        [Test]
        public void FocusIsNotPresetIfAll()
        {
            var preset = featFocusGenerator.FocusTypeIsPreset(GroupConstants.All);
            Assert.That(preset, Is.False);
        }

        [Test]
        public void FocusIsNotPresetIfACollection()
        {
            focusTypes["my focus type"] = new[] { "focus", "other focus" };

            var preset = featFocusGenerator.FocusTypeIsPreset("my focus type");
            Assert.That(preset, Is.False);
        }

        [Test]
        public void FocusIsPresetIfNotACollection()
        {
            focusTypes["wrong focus type"] = new[] { "focus", "other focus" };

            var preset = featFocusGenerator.FocusTypeIsPreset("my focus type");
            Assert.That(preset, Is.True);
        }
    }
}