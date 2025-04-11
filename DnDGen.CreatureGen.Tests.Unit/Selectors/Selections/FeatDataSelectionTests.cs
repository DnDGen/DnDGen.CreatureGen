using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Unit.TestCaseSources;
using DnDGen.Infrastructure.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class FeatDataSelectionTests
    {
        private FeatDataSelection selection;
        private List<Feat> feats;
        private Dictionary<string, Ability> abilities;
        private Dictionary<string, Measurement> speeds;
        private List<Skill> skills;
        private List<Attack> attacks;

        [SetUp]
        public void Setup()
        {
            selection = new FeatDataSelection();
            feats = [];
            abilities = [];
            skills = [];
            attacks = [];
            speeds = [];

            abilities["ability"] = new Ability("ability");
            speeds["speed"] = new Measurement("mph");
        }

        [Test]
        public void FeatSelectionInitialized()
        {
            Assert.That(selection.Feat, Is.Empty);
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.FrequencyQuantity, Is.Zero);
            Assert.That(selection.FrequencyTimePeriod, Is.Empty);
            Assert.That(selection.Power, Is.Zero);
            Assert.That(selection.RequiredAbilities, Is.Empty);
            Assert.That(selection.RequiredBaseAttack, Is.Zero);
            Assert.That(selection.RequiredFeats, Is.Empty);
            Assert.That(selection.RequiredHands, Is.Zero);
            Assert.That(selection.RequiredNaturalWeapons, Is.Zero);
            Assert.That(selection.RequiredSizes, Is.Empty);
            Assert.That(selection.RequiredSkills, Is.Empty);
            Assert.That(selection.RequiredSpeeds, Is.Empty);
            Assert.That(selection.RequiresNaturalArmor, Is.False);
            Assert.That(selection.RequiresSpecialAttack, Is.False);
            Assert.That(selection.RequiresSpellLikeAbility, Is.False);
            Assert.That(selection.RequiresEquipment, Is.False);
        }

        [Test]
        public void SectionCountIs24()
        {
            Assert.That(selection.SectionCount, Is.EqualTo(24));
        }

        [Test]
        public void SectionCountIs2_RequiredFeat()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection();
            Assert.That(requiredFeat.SectionCount, Is.EqualTo(2));
        }

        [Test]
        public void SectionCountIs3_RequiredSkill()
        {
            var requiredSkill = new FeatDataSelection.RequiredSkillDataSelection();
            Assert.That(requiredSkill.SectionCount, Is.EqualTo(3));
        }

        [Test]
        public void SeparatorsDoNotCollide()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection();
            var requiredSkill = new FeatDataSelection.RequiredSkillDataSelection();

            Assert.That(selection.Separator, Is.EqualTo('@')
                .And.Not.EqualTo(requiredFeat.Separator)
                .And.Not.EqualTo(requiredSkill.Separator)
                .And.Not.EqualTo(FeatDataSelection.Delimiter)
                .And.Not.EqualTo(FeatDataSelection.RequiredFeatDataSelection.Delimiter));
            Assert.That(FeatDataSelection.Delimiter, Is.EqualTo('|')
                .And.Not.EqualTo(selection.Separator)
                .And.Not.EqualTo(requiredFeat.Separator)
                .And.Not.EqualTo(requiredSkill.Separator)
                .And.Not.EqualTo(FeatDataSelection.RequiredFeatDataSelection.Delimiter));

            Assert.That(requiredFeat.Separator, Is.EqualTo('#')
                .And.Not.EqualTo(selection.Separator)
                .And.Not.EqualTo(FeatDataSelection.Delimiter)
                .And.Not.EqualTo(FeatDataSelection.RequiredFeatDataSelection.Delimiter));
            Assert.That(FeatDataSelection.RequiredFeatDataSelection.Delimiter, Is.EqualTo(',')
                .And.Not.EqualTo(selection.Separator)
                .And.Not.EqualTo(requiredFeat.Separator)
                .And.Not.EqualTo(FeatDataSelection.Delimiter));

            Assert.That(requiredSkill.Separator, Is.EqualTo('#')
                .And.Not.EqualTo(selection.Separator)
                .And.Not.EqualTo(FeatDataSelection.Delimiter));
        }

        private string[] GetTestDataArray()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.FeatData.BaseAttackRequirementIndex] = "9266";
            data[DataIndexConstants.FeatData.FocusTypeIndex] = "my focus type";
            data[DataIndexConstants.FeatData.FrequencyQuantityIndex] = "90210";
            data[DataIndexConstants.FeatData.FrequencyTimePeriodIndex] = "my frequency time period";
            data[DataIndexConstants.FeatData.MinimumCasterLevelIndex] = "42";
            data[DataIndexConstants.FeatData.NameIndex] = "my feat";
            data[DataIndexConstants.FeatData.PowerIndex] = "600";
            data[DataIndexConstants.FeatData.RequiredCharismaIndex] = "1337";
            data[DataIndexConstants.FeatData.RequiredConstitutionIndex] = "1336";
            data[DataIndexConstants.FeatData.RequiredDexterityIndex] = "96";
            data[DataIndexConstants.FeatData.RequiredFeatsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 1", Foci = ["required foci 1.1", "required foci 1.2"] }),
                DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" })]);
            data[DataIndexConstants.FeatData.RequiredFlySpeedIndex] = "783";
            data[DataIndexConstants.FeatData.RequiredHandQuantityIndex] = "8245";
            data[DataIndexConstants.FeatData.RequiredIntelligenceIndex] = "9";
            data[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex] = "22";
            data[DataIndexConstants.FeatData.RequiredSizesIndex] = string.Join(FeatDataSelection.Delimiter, ["my required size", "my other required size"]);
            data[DataIndexConstants.FeatData.RequiredSkillsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 1", Focus = "required focus", Ranks = 2022 }),
                DataHelper.Parse(new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 2", Ranks = 227 })]);
            data[DataIndexConstants.FeatData.RequiredStrengthIndex] = "2";
            data[DataIndexConstants.FeatData.RequiredWisdomIndex] = "12";
            data[DataIndexConstants.FeatData.RequiresEquipmentIndex] = bool.TrueString;
            data[DataIndexConstants.FeatData.RequiresNaturalArmorIndex] = bool.TrueString;
            data[DataIndexConstants.FeatData.RequiresSpecialAttackIndex] = bool.TrueString;
            data[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex] = bool.TrueString;
            data[DataIndexConstants.FeatData.TakenMultipleTimesIndex] = bool.TrueString;

            return data;
        }

        [Test]
        public void Map_FromString_ReturnsSelection()
        {
            var data = GetTestDataArray();

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredBaseAttack, Is.EqualTo(9266));
            Assert.That(newSelection.FocusType, Is.EqualTo("my focus type"));
            Assert.That(newSelection.FrequencyQuantity, Is.EqualTo(90210));
            Assert.That(newSelection.FrequencyTimePeriod, Is.EqualTo("my frequency time period"));
            Assert.That(newSelection.MinimumCasterLevel, Is.EqualTo(42));
            Assert.That(newSelection.Feat, Is.EqualTo("my feat"));
            Assert.That(newSelection.Power, Is.EqualTo(600));
            Assert.That(newSelection.RequiredAbilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Strength], Is.EqualTo(2));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Constitution], Is.EqualTo(1336));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Dexterity], Is.EqualTo(96));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Intelligence], Is.EqualTo(9));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Wisdom], Is.EqualTo(12));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Charisma], Is.EqualTo(1337));
            Assert.That(newSelection.RequiredSpeeds, Has.Count.EqualTo(1)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(newSelection.RequiredSpeeds[SpeedConstants.Fly], Is.EqualTo(783));
            Assert.That(newSelection.RequiredHands, Is.EqualTo(8245));
            Assert.That(newSelection.RequiredNaturalWeapons, Is.EqualTo(22));
            Assert.That(newSelection.RequiredSizes, Is.EqualTo(["my required size", "my other required size"]));
            Assert.That(newSelection.RequiresEquipment, Is.EqualTo(true));
            Assert.That(newSelection.RequiresNaturalArmor, Is.EqualTo(true));
            Assert.That(newSelection.RequiresSpecialAttack, Is.EqualTo(true));
            Assert.That(newSelection.RequiresSpellLikeAbility, Is.EqualTo(true));
            Assert.That(newSelection.CanBeTakenMultipleTimes, Is.EqualTo(true));

            var requiredFeats = newSelection.RequiredFeats.ToArray();
            Assert.That(requiredFeats, Has.Length.EqualTo(2).And.All.Not.Null);
            Assert.That(requiredFeats[0].Feat, Is.EqualTo("required feat 1"));
            Assert.That(requiredFeats[0].Foci, Is.EqualTo(["required foci 1.1", "required foci 1.2"]));
            Assert.That(requiredFeats[1].Feat, Is.EqualTo("required feat 2"));
            Assert.That(requiredFeats[1].Foci, Is.Empty);

            var requiredSkills = newSelection.RequiredSkills.ToArray();
            Assert.That(requiredSkills, Has.Length.EqualTo(2).And.All.Not.Null);
            Assert.That(requiredSkills[0].Skill, Is.EqualTo("required skill 1"));
            Assert.That(requiredSkills[0].Focus, Is.EqualTo("required focus"));
            Assert.That(requiredSkills[0].Ranks, Is.EqualTo(2022));
            Assert.That(requiredSkills[1].Skill, Is.EqualTo("required skill 2"));
            Assert.That(requiredSkills[1].Focus, Is.Empty);
            Assert.That(requiredSkills[1].Ranks, Is.EqualTo(227));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_NoRequirements()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.BaseAttackRequirementIndex] = "0";
            data[DataIndexConstants.FeatData.MinimumCasterLevelIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredCharismaIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredConstitutionIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredDexterityIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredFeatsIndex] = string.Empty;
            data[DataIndexConstants.FeatData.RequiredFlySpeedIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredHandQuantityIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredIntelligenceIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredSizesIndex] = string.Empty;
            data[DataIndexConstants.FeatData.RequiredSkillsIndex] = string.Empty;
            data[DataIndexConstants.FeatData.RequiredStrengthIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredWisdomIndex] = "0";
            data[DataIndexConstants.FeatData.RequiresEquipmentIndex] = bool.FalseString;
            data[DataIndexConstants.FeatData.RequiresNaturalArmorIndex] = bool.FalseString;
            data[DataIndexConstants.FeatData.RequiresSpecialAttackIndex] = bool.FalseString;
            data[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex] = bool.FalseString;

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(newSelection.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(newSelection.RequiredAbilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Strength], Is.EqualTo(0));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Constitution], Is.EqualTo(0));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Dexterity], Is.EqualTo(0));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Intelligence], Is.EqualTo(0));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Wisdom], Is.EqualTo(0));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Charisma], Is.EqualTo(0));
            Assert.That(newSelection.RequiredSpeeds, Has.Count.EqualTo(1)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(newSelection.RequiredSpeeds[SpeedConstants.Fly], Is.EqualTo(0));
            Assert.That(newSelection.RequiredHands, Is.EqualTo(0));
            Assert.That(newSelection.RequiredNaturalWeapons, Is.EqualTo(0));
            Assert.That(newSelection.RequiredSizes, Is.Empty);
            Assert.That(newSelection.RequiredFeats, Is.Empty);
            Assert.That(newSelection.RequiredSkills, Is.Empty);
            Assert.That(newSelection.RequiresEquipment, Is.EqualTo(false));
            Assert.That(newSelection.RequiresNaturalArmor, Is.EqualTo(false));
            Assert.That(newSelection.RequiresSpecialAttack, Is.EqualTo(false));
            Assert.That(newSelection.RequiresSpellLikeAbility, Is.EqualTo(false));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_NoRequiredFeats()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredFeatsIndex] = string.Empty;

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredFeats, Is.Empty);
        }

        [Test]
        public void Map_FromString_ReturnsSelection_OneRequiredFeat()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredFeatsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" })]);

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);

            var requiredFeats = newSelection.RequiredFeats.ToArray();
            Assert.That(requiredFeats, Has.Length.EqualTo(1).And.All.Not.Null);
            Assert.That(requiredFeats[0].Feat, Is.EqualTo("required feat 2"));
            Assert.That(requiredFeats[0].Foci, Is.Empty);
        }

        [Test]
        public void Map_FromString_ReturnsSelection_TwoRequiredFeats()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredFeatsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 1", Foci = ["required foci 1.1", "required foci 1.2"] }),
                DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" })]);

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);

            var requiredFeats = newSelection.RequiredFeats.ToArray();
            Assert.That(requiredFeats, Has.Length.EqualTo(2).And.All.Not.Null);
            Assert.That(requiredFeats[0].Feat, Is.EqualTo("required feat 1"));
            Assert.That(requiredFeats[0].Foci, Is.EqualTo(["required foci 1.1", "required foci 1.2"]));
            Assert.That(requiredFeats[1].Feat, Is.EqualTo("required feat 2"));
            Assert.That(requiredFeats[1].Foci, Is.Empty);
        }

        [Test]
        public void Map_FromString_ReturnsSelection_RequiredFeat()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection();
            var data = new string[requiredFeat.SectionCount];
            data[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex] = "my required feat";
            data[DataIndexConstants.FeatData.RequiredFeatData.FociIndex] = string.Join(FeatDataSelection.RequiredFeatDataSelection.Delimiter,
                ["my required focus", "my other required focus"]);

            var newSelection = FeatDataSelection.RequiredFeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Feat, Is.EqualTo("my required feat"));
            Assert.That(newSelection.Foci, Is.EqualTo(["my required focus", "my other required focus"]));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_RequiredFeat_NoFoci()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection();
            var data = new string[requiredFeat.SectionCount];
            data[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex] = "my required feat";
            data[DataIndexConstants.FeatData.RequiredFeatData.FociIndex] = string.Empty;

            var newSelection = FeatDataSelection.RequiredFeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Feat, Is.EqualTo("my required feat"));
            Assert.That(newSelection.Foci, Is.Empty);
        }

        [Test]
        public void Map_FromString_ReturnsSelection_RequiredFeat_OneFoci()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection();
            var data = new string[requiredFeat.SectionCount];
            data[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex] = "my required feat";
            data[DataIndexConstants.FeatData.RequiredFeatData.FociIndex] = string.Join(FeatDataSelection.RequiredFeatDataSelection.Delimiter, ["my required focus"]);

            var newSelection = FeatDataSelection.RequiredFeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Feat, Is.EqualTo("my required feat"));
            Assert.That(newSelection.Foci, Is.EqualTo(["my required focus"]));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_RequiredFeat_TwoFoci()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection();
            var data = new string[requiredFeat.SectionCount];
            data[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex] = "my required feat";
            data[DataIndexConstants.FeatData.RequiredFeatData.FociIndex] = string.Join(FeatDataSelection.RequiredFeatDataSelection.Delimiter,
                ["my required focus", "my other required focus"]);

            var newSelection = FeatDataSelection.RequiredFeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Feat, Is.EqualTo("my required feat"));
            Assert.That(newSelection.Foci, Is.EqualTo(["my required focus", "my other required focus"]));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_NoRequiredSizes()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredSizesIndex] = string.Empty;

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSizes, Is.Empty);
        }

        [Test]
        public void Map_FromString_ReturnsSelection_OneRequiredSize()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredSizesIndex] = "my size";

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSizes, Is.EqualTo(["my size"]));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_TwoRequiredSizes()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredSizesIndex] = string.Join(FeatDataSelection.Delimiter, ["my size", "my other size"]);

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSizes, Is.EqualTo(["my size", "my other size"]));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_NoRequiredSkills()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredSkillsIndex] = string.Empty;

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSkills, Is.Empty);
        }

        [Test]
        public void Map_FromString_ReturnsSelection_OneRequiredSkill()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredSkillsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 2", Ranks = 227 })]);

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);

            var requiredSkills = newSelection.RequiredSkills.ToArray();
            Assert.That(requiredSkills, Has.Length.EqualTo(1).And.All.Not.Null);
            Assert.That(requiredSkills[0].Skill, Is.EqualTo("required skill 2"));
            Assert.That(requiredSkills[0].Focus, Is.Empty);
            Assert.That(requiredSkills[0].Ranks, Is.EqualTo(227));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_TwoRequiredSkills()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredSkillsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 1", Focus = "required focus", Ranks = 2022 }),
                DataHelper.Parse(new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 2", Ranks = 227 })]);

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);

            var requiredSkills = newSelection.RequiredSkills.ToArray();
            Assert.That(requiredSkills, Has.Length.EqualTo(2).And.All.Not.Null);
            Assert.That(requiredSkills[0].Skill, Is.EqualTo("required skill 1"));
            Assert.That(requiredSkills[0].Focus, Is.EqualTo("required focus"));
            Assert.That(requiredSkills[0].Ranks, Is.EqualTo(2022));
            Assert.That(requiredSkills[1].Skill, Is.EqualTo("required skill 2"));
            Assert.That(requiredSkills[1].Focus, Is.Empty);
            Assert.That(requiredSkills[1].Ranks, Is.EqualTo(227));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_RequiredSkill()
        {
            var requiredSkill = new FeatDataSelection.RequiredSkillDataSelection();
            var data = new string[requiredSkill.SectionCount];
            data[DataIndexConstants.FeatData.RequiredSkillData.SkillIndex] = "my required skill";
            data[DataIndexConstants.FeatData.RequiredSkillData.FocusIndex] = "my required focus";
            data[DataIndexConstants.FeatData.RequiredSkillData.RanksIndex] = "9266";

            var newSelection = FeatDataSelection.RequiredSkillDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Skill, Is.EqualTo("my required skill"));
            Assert.That(newSelection.Focus, Is.EqualTo("my required focus"));
            Assert.That(newSelection.Ranks, Is.EqualTo(9266));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_RequiredSkill_NoFocus()
        {
            var requiredSkill = new FeatDataSelection.RequiredSkillDataSelection();
            var data = new string[requiredSkill.SectionCount];
            data[DataIndexConstants.FeatData.RequiredSkillData.SkillIndex] = "my required skill";
            data[DataIndexConstants.FeatData.RequiredSkillData.FocusIndex] = string.Empty;
            data[DataIndexConstants.FeatData.RequiredSkillData.RanksIndex] = "9266";

            var newSelection = FeatDataSelection.RequiredSkillDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Skill, Is.EqualTo("my required skill"));
            Assert.That(newSelection.Focus, Is.Empty);
            Assert.That(newSelection.Ranks, Is.EqualTo(9266));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromString_ReturnsSelection_RequiresEquipment(bool required)
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiresEquipmentIndex] = required.ToString();

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiresEquipment, Is.EqualTo(required));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromString_ReturnsSelection_RequiresNaturalArmor(bool required)
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiresNaturalArmorIndex] = required.ToString();

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiresNaturalArmor, Is.EqualTo(required));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromString_ReturnsSelection_RequiresSpecialAttack(bool required)
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiresSpecialAttackIndex] = required.ToString();

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiresSpecialAttack, Is.EqualTo(required));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromString_ReturnsSelection_RequiresSpellLikeAbility(bool required)
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex] = required.ToString();

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiresSpellLikeAbility, Is.EqualTo(required));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromString_ReturnsSelection_CanBeTakenMultipleTimes(bool multiple)
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.TakenMultipleTimesIndex] = multiple.ToString();

            var newSelection = FeatDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.CanBeTakenMultipleTimes, Is.EqualTo(multiple));
        }

        [Test]
        public void Map_FromSelection_ReturnsString()
        {
            var selection = GetTestDataSelection();

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.BaseAttackRequirementIndex], Is.EqualTo("9266"));
            Assert.That(rawData[DataIndexConstants.FeatData.FocusTypeIndex], Is.EqualTo("my focus type"));
            Assert.That(rawData[DataIndexConstants.FeatData.FrequencyQuantityIndex], Is.EqualTo("90210"));
            Assert.That(rawData[DataIndexConstants.FeatData.FrequencyTimePeriodIndex], Is.EqualTo("my frequency time period"));
            Assert.That(rawData[DataIndexConstants.FeatData.MinimumCasterLevelIndex], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.FeatData.NameIndex], Is.EqualTo("my feat"));
            Assert.That(rawData[DataIndexConstants.FeatData.PowerIndex], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredStrengthIndex], Is.EqualTo("2"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredConstitutionIndex], Is.EqualTo("1336"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredDexterityIndex], Is.EqualTo("96"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredIntelligenceIndex], Is.EqualTo("9"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredWisdomIndex], Is.EqualTo("12"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredCharismaIndex], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFlySpeedIndex], Is.EqualTo("783"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredHandQuantityIndex], Is.EqualTo("8245"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex], Is.EqualTo("22"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSizesIndex], Is.EqualTo("my required size|my other required size")
                .And.EqualTo(string.Join(FeatDataSelection.Delimiter, ["my required size", "my other required size"])));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresEquipmentIndex], Is.EqualTo(bool.TrueString));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresNaturalArmorIndex], Is.EqualTo(bool.TrueString));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresSpecialAttackIndex], Is.EqualTo(bool.TrueString));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex], Is.EqualTo(bool.TrueString));
            Assert.That(rawData[DataIndexConstants.FeatData.TakenMultipleTimesIndex], Is.EqualTo(bool.TrueString));

            var requiredFeatsData = string.Join(FeatDataSelection.Delimiter, selection.RequiredFeats.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatsIndex], Is.EqualTo(requiredFeatsData));

            var requiredSkillsData = string.Join(FeatDataSelection.Delimiter, selection.RequiredSkills.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillsIndex], Is.EqualTo(requiredSkillsData));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_NoRequirements()
        {
            var selection = GetTestDataSelection();
            selection.RequiredBaseAttack = 0;
            selection.MinimumCasterLevel = 0;
            selection.RequiredAbilities[AbilityConstants.Strength] = 0;
            selection.RequiredAbilities[AbilityConstants.Constitution] = 0;
            selection.RequiredAbilities[AbilityConstants.Dexterity] = 0;
            selection.RequiredAbilities[AbilityConstants.Intelligence] = 0;
            selection.RequiredAbilities[AbilityConstants.Wisdom] = 0;
            selection.RequiredAbilities[AbilityConstants.Charisma] = 0;
            selection.RequiredSpeeds[SpeedConstants.Fly] = 0;
            selection.RequiredHands = 0;
            selection.RequiredNaturalWeapons = 0;
            selection.RequiredSizes = [];
            selection.RequiredFeats = [];
            selection.RequiredSkills = [];
            selection.RequiresEquipment = false;
            selection.RequiresNaturalArmor = false;
            selection.RequiresSpecialAttack = false;
            selection.RequiresSpellLikeAbility = false;

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.BaseAttackRequirementIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.MinimumCasterLevelIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredStrengthIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredConstitutionIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredDexterityIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredIntelligenceIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredWisdomIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredCharismaIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFlySpeedIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredHandQuantityIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSizesIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatsIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillsIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresEquipmentIndex], Is.EqualTo(bool.FalseString));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresNaturalArmorIndex], Is.EqualTo(bool.FalseString));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresSpecialAttackIndex], Is.EqualTo(bool.FalseString));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex], Is.EqualTo(bool.FalseString));
        }

        [TestCase(AbilityConstants.Strength, DataIndexConstants.FeatData.RequiredStrengthIndex)]
        [TestCase(AbilityConstants.Constitution, DataIndexConstants.FeatData.RequiredConstitutionIndex)]
        [TestCase(AbilityConstants.Dexterity, DataIndexConstants.FeatData.RequiredDexterityIndex)]
        [TestCase(AbilityConstants.Intelligence, DataIndexConstants.FeatData.RequiredIntelligenceIndex)]
        [TestCase(AbilityConstants.Wisdom, DataIndexConstants.FeatData.RequiredWisdomIndex)]
        [TestCase(AbilityConstants.Charisma, DataIndexConstants.FeatData.RequiredCharismaIndex)]
        public void Map_FromSelection_ReturnsString_MissingAbility(string ability, int index)
        {
            var selection = GetTestDataSelection();
            selection.RequiredAbilities.Remove(ability);

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[index], Is.EqualTo("0"));

            var indices = new[]
            {
                DataIndexConstants.FeatData.RequiredStrengthIndex,
                DataIndexConstants.FeatData.RequiredConstitutionIndex,
                DataIndexConstants.FeatData.RequiredDexterityIndex,
                DataIndexConstants.FeatData.RequiredIntelligenceIndex,
                DataIndexConstants.FeatData.RequiredWisdomIndex,
                DataIndexConstants.FeatData.RequiredCharismaIndex
            };

            foreach (var validindex in indices.Except([index]))
            {
                Assert.That(rawData[validindex], Is.Not.Empty.And.Not.EqualTo("0"));
            }
        }

        [Test]
        public void Map_FromSelection_ReturnsString_MissingAbilities()
        {
            var selection = GetTestDataSelection();
            selection.RequiredAbilities.Clear();

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var indices = new[]
            {
                DataIndexConstants.FeatData.RequiredStrengthIndex,
                DataIndexConstants.FeatData.RequiredConstitutionIndex,
                DataIndexConstants.FeatData.RequiredDexterityIndex,
                DataIndexConstants.FeatData.RequiredIntelligenceIndex,
                DataIndexConstants.FeatData.RequiredWisdomIndex,
                DataIndexConstants.FeatData.RequiredCharismaIndex
            };

            foreach (var index in indices)
            {
                Assert.That(rawData[index], Is.EqualTo("0"));
            }
        }

        [Test]
        public void Map_FromSelection_ReturnsString_NoRequiredFeats()
        {
            var selection = GetTestDataSelection();
            selection.RequiredFeats = [];

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatsIndex], Is.Empty);
        }

        [Test]
        public void Map_FromSelection_ReturnsString_OneRequiredFeat()
        {
            var selection = GetTestDataSelection();
            selection.RequiredFeats = [new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" }];

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var requiredFeats = string.Join(FeatDataSelection.Delimiter, selection.RequiredFeats.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatsIndex], Is.EqualTo(requiredFeats));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_TwoRequiredFeats()
        {
            var selection = GetTestDataSelection();
            selection.RequiredFeats =
                [new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 1", Foci = ["required foci 1.1", "required foci 1.2"] },
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" }];

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var requiredFeats = string.Join(FeatDataSelection.Delimiter, selection.RequiredFeats.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatsIndex], Is.EqualTo(requiredFeats));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_RequiredFeat()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection
            {
                Feat = "my required feat",
                Foci = ["my required focus", "my other required focus"]
            };

            var rawData = FeatDataSelection.RequiredFeatDataSelection.Map(requiredFeat);
            Assert.That(rawData.Length, Is.EqualTo(requiredFeat.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex], Is.EqualTo("my required feat"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FociIndex], Is.EqualTo(["my required focus", "my other required focus"]));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_RequiredFeat_NoFoci()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection()
            {
                Feat = "my required feat",
                Foci = []
            };

            var rawData = FeatDataSelection.RequiredFeatDataSelection.Map(requiredFeat);
            Assert.That(rawData.Length, Is.EqualTo(requiredFeat.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex], Is.EqualTo("my required feat"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FociIndex], Is.Empty);
        }

        [Test]
        public void Map_FromSelection_ReturnsString_RequiredFeat_OneFoci()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection()
            {
                Feat = "my required feat",
                Foci = ["my required focus"]
            };

            var rawData = FeatDataSelection.RequiredFeatDataSelection.Map(requiredFeat);
            Assert.That(rawData.Length, Is.EqualTo(requiredFeat.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex], Is.EqualTo("my required feat"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FociIndex], Is.EqualTo("my required focus"));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_RequiredFeat_TwoFoci()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection()
            {
                Feat = "my required feat",
                Foci = ["my required focus", "my other required focus"]
            };

            var rawData = FeatDataSelection.RequiredFeatDataSelection.Map(requiredFeat);
            Assert.That(rawData.Length, Is.EqualTo(requiredFeat.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex], Is.EqualTo("my required feat"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FociIndex], Is.EqualTo("my required focus,my other required focus")
                .And.EqualTo(string.Join(FeatDataSelection.RequiredFeatDataSelection.Delimiter, ["my required focus", "my other required focus"])));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_NoRequiredSizes()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSizes = [];

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSizesIndex], Is.Empty);
        }

        [Test]
        public void Map_FromSelection_ReturnsString_OneRequiredSize()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSizes = ["my size"];

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSizesIndex], Is.EqualTo("my size"));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_TwoRequiredSizes()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSizes = ["my size", "my other size"];

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSizesIndex], Is.EqualTo("my size|my other size")
                .And.EqualTo(string.Join(FeatDataSelection.Delimiter, selection.RequiredSizes)));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_NoRequiredSkills()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSkills = [];

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillsIndex], Is.Empty);
        }

        [Test]
        public void Map_FromSelection_ReturnsString_OneRequiredSkill()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSkills = [new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 2", Ranks = 227 }];

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var requiredSkills = string.Join(FeatDataSelection.Delimiter, selection.RequiredSkills.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillsIndex], Is.EqualTo(requiredSkills));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_TwoRequiredSkills()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSkills =
                [new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 1", Focus = "required focus", Ranks = 2022 },
                new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 2", Ranks = 227 }];

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var requiredSkills = string.Join(FeatDataSelection.Delimiter, selection.RequiredSkills.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillsIndex], Is.EqualTo(requiredSkills));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_RequiredSkill()
        {
            var requiredSkill = new FeatDataSelection.RequiredSkillDataSelection
            {
                Skill = "my required skill",
                Focus = "my required focus",
                Ranks = 9266
            };

            var rawData = FeatDataSelection.RequiredSkillDataSelection.Map(requiredSkill);
            Assert.That(rawData.Length, Is.EqualTo(requiredSkill.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillData.SkillIndex], Is.EqualTo("my required skill"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillData.FocusIndex], Is.EqualTo("my required focus"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillData.RanksIndex], Is.EqualTo("9266"));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_RequiredSkill_NoFocus()
        {
            var requiredSkill = new FeatDataSelection.RequiredSkillDataSelection
            {
                Skill = "my required skill",
                Focus = string.Empty,
                Ranks = 9266
            };

            var rawData = FeatDataSelection.RequiredSkillDataSelection.Map(requiredSkill);
            Assert.That(rawData.Length, Is.EqualTo(requiredSkill.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillData.SkillIndex], Is.EqualTo("my required skill"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillData.FocusIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillData.RanksIndex], Is.EqualTo("9266"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromSelection_ReturnsString_RequiresEquipment(bool required)
        {
            var selection = GetTestDataSelection();
            selection.RequiresEquipment = required;

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresEquipmentIndex], Is.EqualTo(required.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromSelection_ReturnsString_RequiresNaturalArmor(bool required)
        {
            var selection = GetTestDataSelection();
            selection.RequiresNaturalArmor = required;

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresNaturalArmorIndex], Is.EqualTo(required.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromSelection_ReturnsString_RequiresSpecialAttack(bool required)
        {
            var selection = GetTestDataSelection();
            selection.RequiresSpecialAttack = required;

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresSpecialAttackIndex], Is.EqualTo(required.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromSelection_ReturnsString_RequiresSpellLikeAbility(bool required)
        {
            var selection = GetTestDataSelection();
            selection.RequiresSpellLikeAbility = required;

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex], Is.EqualTo(required.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromSelection_ReturnsString_CanBeTakenMultipleTimes(bool multiple)
        {
            var selection = GetTestDataSelection();
            selection.CanBeTakenMultipleTimes = multiple;

            var rawData = FeatDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.TakenMultipleTimesIndex], Is.EqualTo(multiple.ToString()));
        }

        private FeatDataSelection GetTestDataSelection() => new()
        {
            RequiredBaseAttack = 9266,
            FocusType = "my focus type",
            FrequencyQuantity = 90210,
            FrequencyTimePeriod = "my frequency time period",
            MinimumCasterLevel = 42,
            Feat = "my feat",
            Power = 600,
            RequiredAbilities = new()
            {
                [AbilityConstants.Strength] = 2,
                [AbilityConstants.Constitution] = 1336,
                [AbilityConstants.Dexterity] = 96,
                [AbilityConstants.Intelligence] = 9,
                [AbilityConstants.Wisdom] = 12,
                [AbilityConstants.Charisma] = 1337,
            },
            RequiredSpeeds = new()
            {
                [SpeedConstants.Fly] = 783,
            },
            RequiredHands = 8245,
            RequiredNaturalWeapons = 22,
            RequiredSizes = ["my required size", "my other required size"],
            RequiresEquipment = true,
            RequiresNaturalArmor = true,
            RequiresSpecialAttack = true,
            RequiresSpellLikeAbility = true,
            CanBeTakenMultipleTimes = true,
            RequiredFeats = [
                new() { Feat = "required feat 1", Foci = ["required foci 1.1", "required foci 1.2"] },
                new() { Feat = "required feat 2" },
            ],
            RequiredSkills = [
                new() { Skill = "required skill 1", Focus = "required focus", Ranks = 2022 },
                new() { Skill = "required skill 2", Ranks = 227 },
            ],
        };

        [Test]
        public void MapTo_ReturnsSelection()
        {
            var data = GetTestDataArray();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredBaseAttack, Is.EqualTo(9266));
            Assert.That(newSelection.FocusType, Is.EqualTo("my focus type"));
            Assert.That(newSelection.FrequencyQuantity, Is.EqualTo(90210));
            Assert.That(newSelection.FrequencyTimePeriod, Is.EqualTo("my frequency time period"));
            Assert.That(newSelection.MinimumCasterLevel, Is.EqualTo(42));
            Assert.That(newSelection.Feat, Is.EqualTo("my feat"));
            Assert.That(newSelection.Power, Is.EqualTo(600));
            Assert.That(newSelection.RequiredAbilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Strength], Is.EqualTo(2));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Constitution], Is.EqualTo(1336));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Dexterity], Is.EqualTo(96));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Intelligence], Is.EqualTo(9));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Wisdom], Is.EqualTo(12));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Charisma], Is.EqualTo(1337));
            Assert.That(newSelection.RequiredSpeeds, Has.Count.EqualTo(1)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(newSelection.RequiredSpeeds[SpeedConstants.Fly], Is.EqualTo(783));
            Assert.That(newSelection.RequiredHands, Is.EqualTo(8245));
            Assert.That(newSelection.RequiredNaturalWeapons, Is.EqualTo(22));
            Assert.That(newSelection.RequiredSizes, Is.EqualTo(["my required size", "my other required size"]));
            Assert.That(newSelection.RequiresEquipment, Is.EqualTo(true));
            Assert.That(newSelection.RequiresNaturalArmor, Is.EqualTo(true));
            Assert.That(newSelection.RequiresSpecialAttack, Is.EqualTo(true));
            Assert.That(newSelection.RequiresSpellLikeAbility, Is.EqualTo(true));
            Assert.That(newSelection.CanBeTakenMultipleTimes, Is.EqualTo(true));

            var requiredFeats = newSelection.RequiredFeats.ToArray();
            Assert.That(requiredFeats, Has.Length.EqualTo(2).And.All.Not.Null);
            Assert.That(requiredFeats[0].Feat, Is.EqualTo("required feat 1"));
            Assert.That(requiredFeats[0].Foci, Is.EqualTo(["required foci 1.1", "required foci 1.2"]));
            Assert.That(requiredFeats[1].Feat, Is.EqualTo("required feat 2"));
            Assert.That(requiredFeats[1].Foci, Is.Empty);

            var requiredSkills = newSelection.RequiredSkills.ToArray();
            Assert.That(requiredSkills, Has.Length.EqualTo(2).And.All.Not.Null);
            Assert.That(requiredSkills[0].Skill, Is.EqualTo("required skill 1"));
            Assert.That(requiredSkills[0].Focus, Is.EqualTo("required focus"));
            Assert.That(requiredSkills[0].Ranks, Is.EqualTo(2022));
            Assert.That(requiredSkills[1].Skill, Is.EqualTo("required skill 2"));
            Assert.That(requiredSkills[1].Focus, Is.Empty);
            Assert.That(requiredSkills[1].Ranks, Is.EqualTo(227));
        }

        [Test]
        public void MapTo_ReturnsSelection_NoRequirements()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.BaseAttackRequirementIndex] = "0";
            data[DataIndexConstants.FeatData.MinimumCasterLevelIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredCharismaIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredConstitutionIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredDexterityIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredFeatsIndex] = string.Empty;
            data[DataIndexConstants.FeatData.RequiredFlySpeedIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredHandQuantityIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredIntelligenceIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredSizesIndex] = string.Empty;
            data[DataIndexConstants.FeatData.RequiredSkillsIndex] = string.Empty;
            data[DataIndexConstants.FeatData.RequiredStrengthIndex] = "0";
            data[DataIndexConstants.FeatData.RequiredWisdomIndex] = "0";
            data[DataIndexConstants.FeatData.RequiresEquipmentIndex] = bool.FalseString;
            data[DataIndexConstants.FeatData.RequiresNaturalArmorIndex] = bool.FalseString;
            data[DataIndexConstants.FeatData.RequiresSpecialAttackIndex] = bool.FalseString;
            data[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex] = bool.FalseString;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredBaseAttack, Is.EqualTo(0));
            Assert.That(newSelection.MinimumCasterLevel, Is.EqualTo(0));
            Assert.That(newSelection.RequiredAbilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Strength], Is.EqualTo(0));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Constitution], Is.EqualTo(0));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Dexterity], Is.EqualTo(0));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Intelligence], Is.EqualTo(0));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Wisdom], Is.EqualTo(0));
            Assert.That(newSelection.RequiredAbilities[AbilityConstants.Charisma], Is.EqualTo(0));
            Assert.That(newSelection.RequiredSpeeds, Has.Count.EqualTo(1)
                .And.ContainKey(SpeedConstants.Fly));
            Assert.That(newSelection.RequiredSpeeds[SpeedConstants.Fly], Is.EqualTo(0));
            Assert.That(newSelection.RequiredHands, Is.EqualTo(0));
            Assert.That(newSelection.RequiredNaturalWeapons, Is.EqualTo(0));
            Assert.That(newSelection.RequiredSizes, Is.Empty);
            Assert.That(newSelection.RequiredFeats, Is.Empty);
            Assert.That(newSelection.RequiredSkills, Is.Empty);
            Assert.That(newSelection.RequiresEquipment, Is.EqualTo(false));
            Assert.That(newSelection.RequiresNaturalArmor, Is.EqualTo(false));
            Assert.That(newSelection.RequiresSpecialAttack, Is.EqualTo(false));
            Assert.That(newSelection.RequiresSpellLikeAbility, Is.EqualTo(false));
        }

        [Test]
        public void MapTo_ReturnsSelection_NoRequiredFeats()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredFeatsIndex] = string.Empty;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredFeats, Is.Empty);
        }

        [Test]
        public void MapTo_ReturnsSelection_OneRequiredFeat()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredFeatsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" })]);

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);

            var requiredFeats = newSelection.RequiredFeats.ToArray();
            Assert.That(requiredFeats, Has.Length.EqualTo(1).And.All.Not.Null);
            Assert.That(requiredFeats[0].Feat, Is.EqualTo("required feat 2"));
            Assert.That(requiredFeats[0].Foci, Is.Empty);
        }

        [Test]
        public void MapTo_ReturnsSelection_TwoRequiredFeats()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredFeatsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 1", Foci = ["required foci 1.1", "required foci 1.2"] }),
                DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" })]);

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);

            var requiredFeats = newSelection.RequiredFeats.ToArray();
            Assert.That(requiredFeats, Has.Length.EqualTo(2).And.All.Not.Null);
            Assert.That(requiredFeats[0].Feat, Is.EqualTo("required feat 1"));
            Assert.That(requiredFeats[0].Foci, Is.EqualTo(["required foci 1.1", "required foci 1.2"]));
            Assert.That(requiredFeats[1].Feat, Is.EqualTo("required feat 2"));
            Assert.That(requiredFeats[1].Foci, Is.Empty);
        }

        [Test]
        public void MapTo_ReturnsSelection_RequiredFeat()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection();
            var data = new string[requiredFeat.SectionCount];
            data[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex] = "my required feat";
            data[DataIndexConstants.FeatData.RequiredFeatData.FociIndex] = string.Join(FeatDataSelection.RequiredFeatDataSelection.Delimiter,
                ["my required focus", "my other required focus"]);

            var newSelection = requiredFeat.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Feat, Is.EqualTo("my required feat"));
            Assert.That(newSelection.Foci, Is.EqualTo(["my required focus", "my other required focus"]));
        }

        [Test]
        public void MapTo_ReturnsSelection_RequiredFeat_NoFoci()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection();
            var data = new string[requiredFeat.SectionCount];
            data[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex] = "my required feat";
            data[DataIndexConstants.FeatData.RequiredFeatData.FociIndex] = string.Empty;

            var newSelection = requiredFeat.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Feat, Is.EqualTo("my required feat"));
            Assert.That(newSelection.Foci, Is.Empty);
        }

        [Test]
        public void MapTo_ReturnsSelection_RequiredFeat_OneFoci()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection();
            var data = new string[requiredFeat.SectionCount];
            data[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex] = "my required feat";
            data[DataIndexConstants.FeatData.RequiredFeatData.FociIndex] = string.Join(FeatDataSelection.RequiredFeatDataSelection.Delimiter, ["my required focus"]);

            var newSelection = requiredFeat.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Feat, Is.EqualTo("my required feat"));
            Assert.That(newSelection.Foci, Is.EqualTo(["my required focus"]));
        }

        [Test]
        public void MapTo_ReturnsSelection_RequiredFeat_TwoFoci()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection();
            var data = new string[requiredFeat.SectionCount];
            data[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex] = "my required feat";
            data[DataIndexConstants.FeatData.RequiredFeatData.FociIndex] = string.Join(FeatDataSelection.RequiredFeatDataSelection.Delimiter,
                ["my required focus", "my other required focus"]);

            var newSelection = requiredFeat.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Feat, Is.EqualTo("my required feat"));
            Assert.That(newSelection.Foci, Is.EqualTo(["my required focus", "my other required focus"]));
        }

        [Test]
        public void MapTo_ReturnsSelection_NoRequiredSizes()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredSizesIndex] = string.Empty;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSizes, Is.Empty);
        }

        [Test]
        public void MapTo_ReturnsSelection_OneRequiredSize()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredSizesIndex] = "my size";

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSizes, Is.EqualTo(["my size"]));
        }

        [Test]
        public void MapTo_ReturnsSelection_TwoRequiredSizes()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredSizesIndex] = string.Join(FeatDataSelection.Delimiter, ["my size", "my other size"]);

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSizes, Is.EqualTo(["my size", "my other size"]));
        }

        [Test]
        public void MapTo_ReturnsSelection_NoRequiredSkills()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredSkillsIndex] = string.Empty;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSkills, Is.Empty);
        }

        [Test]
        public void MapTo_ReturnsSelection_OneRequiredSkill()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredSkillsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 2", Ranks = 227 })]);

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);

            var requiredSkills = newSelection.RequiredSkills.ToArray();
            Assert.That(requiredSkills, Has.Length.EqualTo(1).And.All.Not.Null);
            Assert.That(requiredSkills[0].Skill, Is.EqualTo("required skill 2"));
            Assert.That(requiredSkills[0].Focus, Is.Empty);
            Assert.That(requiredSkills[0].Ranks, Is.EqualTo(227));
        }

        [Test]
        public void MapTo_ReturnsSelection_TwoRequiredSkills()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiredSkillsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 1", Focus = "required focus", Ranks = 2022 }),
                DataHelper.Parse(new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 2", Ranks = 227 })]);

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);

            var requiredSkills = newSelection.RequiredSkills.ToArray();
            Assert.That(requiredSkills, Has.Length.EqualTo(2).And.All.Not.Null);
            Assert.That(requiredSkills[0].Skill, Is.EqualTo("required skill 1"));
            Assert.That(requiredSkills[0].Focus, Is.EqualTo("required focus"));
            Assert.That(requiredSkills[0].Ranks, Is.EqualTo(2022));
            Assert.That(requiredSkills[1].Skill, Is.EqualTo("required skill 2"));
            Assert.That(requiredSkills[1].Focus, Is.Empty);
            Assert.That(requiredSkills[1].Ranks, Is.EqualTo(227));
        }

        [Test]
        public void MapTo_ReturnsSelection_RequiredSkill()
        {
            var requiredSkill = new FeatDataSelection.RequiredSkillDataSelection();
            var data = new string[requiredSkill.SectionCount];
            data[DataIndexConstants.FeatData.RequiredSkillData.SkillIndex] = "my required skill";
            data[DataIndexConstants.FeatData.RequiredSkillData.FocusIndex] = "my required focus";
            data[DataIndexConstants.FeatData.RequiredSkillData.RanksIndex] = "9266";

            var newSelection = requiredSkill.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Skill, Is.EqualTo("my required skill"));
            Assert.That(newSelection.Focus, Is.EqualTo("my required focus"));
            Assert.That(newSelection.Ranks, Is.EqualTo(9266));
        }

        [Test]
        public void MapTo_ReturnsSelection_RequiredSkill_NoFocus()
        {
            var requiredSkill = new FeatDataSelection.RequiredSkillDataSelection();
            var data = new string[requiredSkill.SectionCount];
            data[DataIndexConstants.FeatData.RequiredSkillData.SkillIndex] = "my required skill";
            data[DataIndexConstants.FeatData.RequiredSkillData.FocusIndex] = string.Empty;
            data[DataIndexConstants.FeatData.RequiredSkillData.RanksIndex] = "9266";

            var newSelection = requiredSkill.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Skill, Is.EqualTo("my required skill"));
            Assert.That(newSelection.Focus, Is.Empty);
            Assert.That(newSelection.Ranks, Is.EqualTo(9266));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapTo_ReturnsSelection_RequiresEquipment(bool required)
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiresEquipmentIndex] = required.ToString();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiresEquipment, Is.EqualTo(required));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapTo_ReturnsSelection_RequiresNaturalArmor(bool required)
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiresNaturalArmorIndex] = required.ToString();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiresNaturalArmor, Is.EqualTo(required));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapTo_ReturnsSelection_RequiresSpecialAttack(bool required)
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiresSpecialAttackIndex] = required.ToString();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiresSpecialAttack, Is.EqualTo(required));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapTo_ReturnsSelection_RequiresSpellLikeAbility(bool required)
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex] = required.ToString();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiresSpellLikeAbility, Is.EqualTo(required));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapTo_ReturnsSelection_CanBeTakenMultipleTimes(bool multiple)
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.FeatData.TakenMultipleTimesIndex] = multiple.ToString();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.CanBeTakenMultipleTimes, Is.EqualTo(multiple));
        }

        [Test]
        public void MapFrom_ReturnsString()
        {
            var selection = GetTestDataSelection();

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.BaseAttackRequirementIndex], Is.EqualTo("9266"));
            Assert.That(rawData[DataIndexConstants.FeatData.FocusTypeIndex], Is.EqualTo("my focus type"));
            Assert.That(rawData[DataIndexConstants.FeatData.FrequencyQuantityIndex], Is.EqualTo("90210"));
            Assert.That(rawData[DataIndexConstants.FeatData.FrequencyTimePeriodIndex], Is.EqualTo("my frequency time period"));
            Assert.That(rawData[DataIndexConstants.FeatData.MinimumCasterLevelIndex], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.FeatData.NameIndex], Is.EqualTo("my feat"));
            Assert.That(rawData[DataIndexConstants.FeatData.PowerIndex], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredStrengthIndex], Is.EqualTo("2"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredConstitutionIndex], Is.EqualTo("1336"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredDexterityIndex], Is.EqualTo("96"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredIntelligenceIndex], Is.EqualTo("9"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredWisdomIndex], Is.EqualTo("12"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredCharismaIndex], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFlySpeedIndex], Is.EqualTo("783"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredHandQuantityIndex], Is.EqualTo("8245"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex], Is.EqualTo("22"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSizesIndex], Is.EqualTo("my required size|my other required size")
                .And.EqualTo(string.Join(FeatDataSelection.Delimiter, ["my required size", "my other required size"])));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresEquipmentIndex], Is.EqualTo(bool.TrueString));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresNaturalArmorIndex], Is.EqualTo(bool.TrueString));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresSpecialAttackIndex], Is.EqualTo(bool.TrueString));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex], Is.EqualTo(bool.TrueString));
            Assert.That(rawData[DataIndexConstants.FeatData.TakenMultipleTimesIndex], Is.EqualTo(bool.TrueString));

            var requiredFeatsData = string.Join(FeatDataSelection.Delimiter, selection.RequiredFeats.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatsIndex], Is.EqualTo(requiredFeatsData));

            var requiredSkillsData = string.Join(FeatDataSelection.Delimiter, selection.RequiredSkills.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillsIndex], Is.EqualTo(requiredSkillsData));
        }

        [Test]
        public void MapFrom_ReturnsString_NoRequirements()
        {
            var selection = GetTestDataSelection();
            selection.RequiredBaseAttack = 0;
            selection.MinimumCasterLevel = 0;
            selection.RequiredAbilities[AbilityConstants.Strength] = 0;
            selection.RequiredAbilities[AbilityConstants.Constitution] = 0;
            selection.RequiredAbilities[AbilityConstants.Dexterity] = 0;
            selection.RequiredAbilities[AbilityConstants.Intelligence] = 0;
            selection.RequiredAbilities[AbilityConstants.Wisdom] = 0;
            selection.RequiredAbilities[AbilityConstants.Charisma] = 0;
            selection.RequiredSpeeds[SpeedConstants.Fly] = 0;
            selection.RequiredHands = 0;
            selection.RequiredNaturalWeapons = 0;
            selection.RequiredSizes = [];
            selection.RequiredFeats = [];
            selection.RequiredSkills = [];
            selection.RequiresEquipment = false;
            selection.RequiresNaturalArmor = false;
            selection.RequiresSpecialAttack = false;
            selection.RequiresSpellLikeAbility = false;

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.BaseAttackRequirementIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.MinimumCasterLevelIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredStrengthIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredConstitutionIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredDexterityIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredIntelligenceIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredWisdomIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredCharismaIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFlySpeedIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredHandQuantityIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredNaturalWeaponQuantityIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSizesIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatsIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillsIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresEquipmentIndex], Is.EqualTo(bool.FalseString));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresNaturalArmorIndex], Is.EqualTo(bool.FalseString));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresSpecialAttackIndex], Is.EqualTo(bool.FalseString));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex], Is.EqualTo(bool.FalseString));
        }

        [TestCase(AbilityConstants.Strength, DataIndexConstants.FeatData.RequiredStrengthIndex)]
        [TestCase(AbilityConstants.Constitution, DataIndexConstants.FeatData.RequiredConstitutionIndex)]
        [TestCase(AbilityConstants.Dexterity, DataIndexConstants.FeatData.RequiredDexterityIndex)]
        [TestCase(AbilityConstants.Intelligence, DataIndexConstants.FeatData.RequiredIntelligenceIndex)]
        [TestCase(AbilityConstants.Wisdom, DataIndexConstants.FeatData.RequiredWisdomIndex)]
        [TestCase(AbilityConstants.Charisma, DataIndexConstants.FeatData.RequiredCharismaIndex)]
        public void MapFrom_ReturnsString_MissingAbility(string ability, int index)
        {
            var selection = GetTestDataSelection();
            selection.RequiredAbilities.Remove(ability);

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[index], Is.EqualTo("0"));

            var indices = new[]
            {
                DataIndexConstants.FeatData.RequiredStrengthIndex,
                DataIndexConstants.FeatData.RequiredConstitutionIndex,
                DataIndexConstants.FeatData.RequiredDexterityIndex,
                DataIndexConstants.FeatData.RequiredIntelligenceIndex,
                DataIndexConstants.FeatData.RequiredWisdomIndex,
                DataIndexConstants.FeatData.RequiredCharismaIndex
            };

            foreach (var validindex in indices.Except([index]))
            {
                Assert.That(rawData[validindex], Is.Not.Empty.And.Not.EqualTo("0"));
            }
        }

        [Test]
        public void MapFrom_ReturnsString_MissingAbilities()
        {
            var selection = GetTestDataSelection();
            selection.RequiredAbilities.Clear();

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var indices = new[]
            {
                DataIndexConstants.FeatData.RequiredStrengthIndex,
                DataIndexConstants.FeatData.RequiredConstitutionIndex,
                DataIndexConstants.FeatData.RequiredDexterityIndex,
                DataIndexConstants.FeatData.RequiredIntelligenceIndex,
                DataIndexConstants.FeatData.RequiredWisdomIndex,
                DataIndexConstants.FeatData.RequiredCharismaIndex
            };

            foreach (var index in indices)
            {
                Assert.That(rawData[index], Is.EqualTo("0"));
            }
        }

        [Test]
        public void MapFrom_ReturnsString_NoRequiredFeats()
        {
            var selection = GetTestDataSelection();
            selection.RequiredFeats = [];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatsIndex], Is.Empty);
        }

        [Test]
        public void MapFrom_ReturnsString_OneRequiredFeat()
        {
            var selection = GetTestDataSelection();
            selection.RequiredFeats = [new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" }];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var requiredFeats = string.Join(FeatDataSelection.Delimiter, selection.RequiredFeats.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatsIndex], Is.EqualTo(requiredFeats));
        }

        [Test]
        public void MapFrom_ReturnsString_TwoRequiredFeats()
        {
            var selection = GetTestDataSelection();
            selection.RequiredFeats =
                [new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 1", Foci = ["required foci 1.1", "required foci 1.2"] },
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" }];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var requiredFeats = string.Join(FeatDataSelection.Delimiter, selection.RequiredFeats.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatsIndex], Is.EqualTo(requiredFeats));
        }

        [Test]
        public void MapFrom_ReturnsString_RequiredFeat()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection
            {
                Feat = "my required feat",
                Foci = ["my required focus", "my other required focus"]
            };

            var rawData = requiredFeat.MapFrom(requiredFeat);
            Assert.That(rawData.Length, Is.EqualTo(requiredFeat.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex], Is.EqualTo("my required feat"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FociIndex], Is.EqualTo(["my required focus", "my other required focus"]));
        }

        [Test]
        public void MapFrom_ReturnsString_RequiredFeat_NoFoci()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection()
            {
                Feat = "my required feat",
                Foci = []
            };

            var rawData = requiredFeat.MapFrom(requiredFeat);
            Assert.That(rawData.Length, Is.EqualTo(requiredFeat.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex], Is.EqualTo("my required feat"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FociIndex], Is.Empty);
        }

        [Test]
        public void MapFrom_ReturnsString_RequiredFeat_OneFoci()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection()
            {
                Feat = "my required feat",
                Foci = ["my required focus"]
            };

            var rawData = requiredFeat.MapFrom(requiredFeat);
            Assert.That(rawData.Length, Is.EqualTo(requiredFeat.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex], Is.EqualTo("my required feat"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FociIndex], Is.EqualTo("my required focus"));
        }

        [Test]
        public void MapFrom_ReturnsString_RequiredFeat_TwoFoci()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection()
            {
                Feat = "my required feat",
                Foci = ["my required focus", "my other required focus"]
            };

            var rawData = requiredFeat.MapFrom(requiredFeat);
            Assert.That(rawData.Length, Is.EqualTo(requiredFeat.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FeatIndex], Is.EqualTo("my required feat"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredFeatData.FociIndex], Is.EqualTo("my required focus,my other required focus")
                .And.EqualTo(string.Join(FeatDataSelection.RequiredFeatDataSelection.Delimiter, ["my required focus", "my other required focus"])));
        }

        [Test]
        public void MapFrom_ReturnsString_NoRequiredSizes()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSizes = [];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSizesIndex], Is.Empty);
        }

        [Test]
        public void MapFrom_ReturnsString_OneRequiredSize()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSizes = ["my size"];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSizesIndex], Is.EqualTo("my size"));
        }

        [Test]
        public void MapFrom_ReturnsString_TwoRequiredSizes()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSizes = ["my size", "my other size"];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSizesIndex], Is.EqualTo("my size|my other size")
                .And.EqualTo(string.Join(FeatDataSelection.Delimiter, selection.RequiredSizes)));
        }

        [Test]
        public void MapFrom_ReturnsString_NoRequiredSkills()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSkills = [];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillsIndex], Is.Empty);
        }

        [Test]
        public void MapFrom_ReturnsString_OneRequiredSkill()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSkills = [new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 2", Ranks = 227 }];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var requiredSkills = string.Join(FeatDataSelection.Delimiter, selection.RequiredSkills.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillsIndex], Is.EqualTo(requiredSkills));
        }

        [Test]
        public void MapFrom_ReturnsString_TwoRequiredSkills()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSkills =
                [new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 1", Focus = "required focus", Ranks = 2022 },
                new FeatDataSelection.RequiredSkillDataSelection { Skill = "required skill 2", Ranks = 227 }];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var requiredSkills = string.Join(FeatDataSelection.Delimiter, selection.RequiredSkills.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillsIndex], Is.EqualTo(requiredSkills));
        }

        [Test]
        public void MapFrom_ReturnsString_RequiredSkill()
        {
            var requiredSkill = new FeatDataSelection.RequiredSkillDataSelection
            {
                Skill = "my required skill",
                Focus = "my required focus",
                Ranks = 9266
            };

            var rawData = requiredSkill.MapFrom(requiredSkill);
            Assert.That(rawData.Length, Is.EqualTo(requiredSkill.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillData.SkillIndex], Is.EqualTo("my required skill"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillData.FocusIndex], Is.EqualTo("my required focus"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillData.RanksIndex], Is.EqualTo("9266"));
        }

        [Test]
        public void MapFrom_ReturnsString_RequiredSkill_NoFocus()
        {
            var requiredSkill = new FeatDataSelection.RequiredSkillDataSelection
            {
                Skill = "my required skill",
                Focus = string.Empty,
                Ranks = 9266
            };

            var rawData = requiredSkill.MapFrom(requiredSkill);
            Assert.That(rawData.Length, Is.EqualTo(requiredSkill.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillData.SkillIndex], Is.EqualTo("my required skill"));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillData.FocusIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.FeatData.RequiredSkillData.RanksIndex], Is.EqualTo("9266"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapFrom_ReturnsString_RequiresEquipment(bool required)
        {
            var selection = GetTestDataSelection();
            selection.RequiresEquipment = required;

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresEquipmentIndex], Is.EqualTo(required.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapFrom_ReturnsString_RequiresNaturalArmor(bool required)
        {
            var selection = GetTestDataSelection();
            selection.RequiresNaturalArmor = required;

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresNaturalArmorIndex], Is.EqualTo(required.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapFrom_ReturnsString_RequiresSpecialAttack(bool required)
        {
            var selection = GetTestDataSelection();
            selection.RequiresSpecialAttack = required;

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresSpecialAttackIndex], Is.EqualTo(required.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapFrom_ReturnsString_RequiresSpellLikeAbility(bool required)
        {
            var selection = GetTestDataSelection();
            selection.RequiresSpellLikeAbility = required;

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.RequiresSpellLikeAbilityIndex], Is.EqualTo(required.ToString()));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapFrom_ReturnsString_CanBeTakenMultipleTimes(bool multiple)
        {
            var selection = GetTestDataSelection();
            selection.CanBeTakenMultipleTimes = multiple;

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.FeatData.TakenMultipleTimesIndex], Is.EqualTo(multiple.ToString()));
        }

        [Test]
        public void ImmutableRequirementsMetIfNoRequirements()
        {
            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MutableRequirementsMetIfFeatNotAlreadySelected()
        {
            feats.Add(new Feat { Name = "other feat" });
            selection.Feat = "feat";

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MutableRequirementsNotMetIfFeatAlreadySelected()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            selection.Feat = "feat";

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MutableRequirementsMetIfFeatAlreadySelectedWithFocus()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Foci = ["focus"];
            selection.Feat = "feat";
            selection.FocusType = "focus type";

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MutableRequirementsNotMetIfFeatAlreadySelectedWithFocusOfAll()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Foci = [GroupConstants.All];
            selection.Feat = "feat";
            selection.FocusType = "focus type";

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MutableRequirementsMetIfFeatAlreadySelectedAndCanBeTakenMultipleTimes()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].CanBeTakenMultipleTimes = true;
            selection.Feat = "feat";
            selection.CanBeTakenMultipleTimes = true;

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MutableRequirementsMetIfNoRequirements()
        {
            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void BaseAttackRequirementNotMet(int requiredBaseAttack, int baseAttack)
        {
            selection.RequiredBaseAttack = requiredBaseAttack;

            var met = selection.ImmutableRequirementsMet(baseAttack, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void BaseAttackRequirementMet(int requiredBaseAttack, int baseAttack)
        {
            selection.RequiredBaseAttack = requiredBaseAttack;

            var met = selection.ImmutableRequirementsMet(baseAttack, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void CasterLevelRequirementNotMet(int requiredCasterLevel, int casterLevel)
        {
            selection.MinimumCasterLevel = requiredCasterLevel;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, casterLevel, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void CasterLevelRequirementMet(int requiredCasterLevel, int casterLevel)
        {
            selection.MinimumCasterLevel = requiredCasterLevel;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, casterLevel, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.SumOfValuesLessThanPositiveRequirementWithMinimumOne))]
        public void AbilityRequirementsNotMet(int requiredScore, int baseScore, int racialAdjustment)
        {
            selection.RequiredAbilities["ability"] = requiredScore;

            abilities["ability"].BaseScore = baseScore;
            abilities["ability"].RacialAdjustment = racialAdjustment;
            abilities["other ability"] = new Ability("other ability")
            {
                BaseScore = int.MaxValue
            };

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllValuesAndAllPositiveRequirements))]
        public void AbilityRequirementsNotMetBecauseBaseScoreOfZero(int requiredScore, int racialAdjustment)
        {
            selection.RequiredAbilities["ability"] = requiredScore;

            abilities["ability"].BaseScore = 0;
            abilities["ability"].RacialAdjustment = racialAdjustment;
            abilities["other ability"] = new Ability("other ability")
            {
                BaseScore = int.MaxValue
            };

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.SumOfValuesGreaterThanOrEqualToPositiveRequirement))]
        public void AbilityRequirementsMet(int requiredScore, int baseScore, int racialAdjustment)
        {
            selection.RequiredAbilities["ability"] = requiredScore;

            abilities["ability"].BaseScore = baseScore;
            abilities["ability"].RacialAdjustment = racialAdjustment;
            abilities["other ability"] = new Ability("other ability")
            {
                BaseScore = int.MinValue
            };

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void AbilityRequirementsMetIfNotRequired()
        {
            abilities["ability"].BaseScore = 0;
            abilities["ability"].RacialAdjustment = 0;
            abilities["other ability"] = new Ability("other ability")
            {
                BaseScore = 0
            };

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void ClassSkillRequirementsNotMet(int requiredRanks, int ranks)
        {
            selection.RequiredSkills = [
                new FeatDataSelection.RequiredSkillDataSelection { Skill = "skill", Ranks = requiredRanks }
            ];

            skills.Add(new Skill("skill", abilities["ability"], int.MaxValue));
            skills.Add(new Skill("other skill", abilities["ability"], int.MaxValue));
            skills[0].Ranks = ranks;
            skills[0].ClassSkill = true;
            skills[1].Ranks = int.MaxValue;
            skills[1].ClassSkill = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void CrossClassSkillRequirementsNotMet(int requiredRanks, int ranks)
        {
            selection.RequiredSkills = [
                new FeatDataSelection.RequiredSkillDataSelection { Skill = "skill", Ranks = requiredRanks }
            ];

            skills.Add(new Skill("skill", abilities["ability"], int.MaxValue));
            skills.Add(new Skill("other skill", abilities["ability"], int.MaxValue));
            skills[0].Ranks = ranks * 2;
            skills[0].ClassSkill = false;
            skills[1].Ranks = int.MaxValue;
            skills[1].ClassSkill = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void ClassSkillRequirementsMet(int requiredRanks, int ranks)
        {
            selection.RequiredSkills = [
                new FeatDataSelection.RequiredSkillDataSelection { Skill = "skill", Ranks = requiredRanks }
            ];

            skills.Add(new Skill("skill", abilities["ability"], int.MaxValue));
            skills.Add(new Skill("other skill", abilities["ability"], int.MaxValue));
            skills[0].Ranks = ranks;
            skills[0].ClassSkill = true;
            skills[1].Ranks = int.MinValue;
            skills[1].ClassSkill = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void CrossClassSkillRequirementsMet(int requiredRanks, int ranks)
        {
            selection.RequiredSkills = [
                new FeatDataSelection.RequiredSkillDataSelection { Skill = "skill", Ranks = requiredRanks }
            ];

            skills.Add(new Skill("skill", abilities["ability"], int.MaxValue));
            skills.Add(new Skill("other skill", abilities["ability"], int.MaxValue));
            skills[0].Ranks = ranks * 2;
            skills[0].ClassSkill = false;
            skills[1].Ranks = int.MinValue;
            skills[1].ClassSkill = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCase(0, 0, 0, true, 0, true, true)]
        [TestCase(0, 0, 0, true, 0, false, true)]
        [TestCase(0, 0, 0, false, 0, true, true)]
        [TestCase(0, 0, 0, false, 0, false, true)]
        [TestCase(9266, 90210, 42, true, 600, true, false)]
        [TestCase(9266, 90210, 42, true, 600, false, false)]
        [TestCase(9266, 90210, 42, false, 600, true, false)]
        [TestCase(9266, 90210, 42, false, 600, false, false)]
        [TestCase(42, 600, 1337, true, 1336, true, true)]
        [TestCase(42, 600, 1337, true, 1336, false, true)]
        [TestCase(42, 600, 1337, false, 1336, true, true)]
        [TestCase(42, 600, 1337, false, 1336, false, true)]
        [TestCase(1337, 1336, 96, true, 783, true, false)]
        [TestCase(1337, 1336, 96, true, 783, false, false)]
        [TestCase(1337, 1336, 96, false, 783, true, false)]
        [TestCase(1337, 1336, 96, false, 783, false, false)]
        [TestCase(96, 783, 8245, true, 1234, true, true)]
        [TestCase(96, 783, 8245, true, 1234, false, false)]
        [TestCase(96, 783, 8245, false, 1234, true, true)]
        [TestCase(96, 783, 8245, false, 1234, false, false)]
        [TestCase(600, 1336, 783, true, 1337, true, true)]
        [TestCase(600, 1336, 783, true, 1337, false, false)]
        [TestCase(600, 1336, 783, false, 1337, true, false)]
        [TestCase(600, 1336, 783, false, 1337, false, false)]
        [TestCase(8245, 42, 9266, true, 1337, true, true)]
        [TestCase(8245, 42, 9266, true, 1337, false, true)]
        [TestCase(8245, 42, 9266, false, 1337, true, false)]
        [TestCase(8245, 42, 9266, false, 1337, false, false)]
        public void AllRequiredSkillWithSufficientRanksMeetRequirement(
            int requiredRanks1,
            int requiredRanks2,
            int ranks1,
            bool classSkill1,
            int ranks2,
            bool classSkill2,
            bool isMet)
        {
            selection.RequiredSkills =
            [
                new FeatDataSelection.RequiredSkillDataSelection { Skill = "skill", Ranks = requiredRanks1 },
                new FeatDataSelection.RequiredSkillDataSelection { Skill = "other skill", Ranks = requiredRanks2 }
            ];

            skills.Add(new Skill("skill", abilities["ability"], int.MaxValue));
            skills.Add(new Skill("other skill", abilities["ability"], int.MaxValue));
            skills[0].Ranks = ranks1;
            skills[0].ClassSkill = classSkill1;
            skills[1].Ranks = ranks2;
            skills[1].ClassSkill = classSkill2;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.EqualTo(isMet));
        }

        [Test]
        public void MeetSkillRequirementOfZeroRanks()
        {
            selection.RequiredSkills = [new FeatDataSelection.RequiredSkillDataSelection { Skill = "skill", Ranks = 0 }];
            skills.Add(new Skill("skill", abilities["ability"], 10));
            skills[0].ClassSkill = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void FeatRequirementsMet()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            selection.RequiredFeats =
            [
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat" },
            ];

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void FeatRequirementsWithFocusMet()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Foci = ["focus", "other focus"];
            selection.RequiredFeats =
            [
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat", Foci = ["focus"] },
            ];

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void FeatRequirementsWithFociMet()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Foci = ["focus"];
            selection.RequiredFeats =
            [
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat", Foci = ["focus", "other focus"] },
            ];

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void FeatRequirementsWithFocusNotMetByFeatName()
        {
            feats.Add(new Feat());
            feats[0].Name = "other feat";
            feats[0].Foci = ["focus", "other focus"];
            selection.RequiredFeats =
            [
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat", Foci = ["focus"] },
            ];

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void FeatRequirementsWithFocusNotMetByFocus()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Foci = ["wrong focus", "other focus"];
            selection.RequiredFeats =
            [
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat", Foci = ["focus"] },
            ];

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void FeatRequirementsNotMet()
        {
            feats.Add(new Feat());
            feats[0].Name = "feat";
            selection.RequiredFeats =
            [
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat" },
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "other required feat" }
            ];

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void AllMutableRequirementsMet()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[1].Name = "other required feat";
            selection.RequiredFeats =
            [
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat" },
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "other required feat" }
            ];

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void ExtraFeatDoNotMatter()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[1].Name = "other required feat";
            feats[2].Name = "yet another feat";

            selection.RequiredFeats =
            [
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat" },
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "other required feat" }
            ];

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void NotMetIfNoSpecialAttacks()
        {
            attacks.Add(new Attack { IsSpecial = false, Name = "attack" });
            attacks.Add(new Attack { IsSpecial = false, Name = "other attack" });

            selection.RequiresSpecialAttack = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MetIfSpecialAttacks()
        {
            attacks.Add(new Attack { IsSpecial = false, Name = "attack" });
            attacks.Add(new Attack { IsSpecial = true, Name = "other attack" });

            selection.RequiresSpecialAttack = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfSpecialAttacksNotRequired()
        {
            attacks.Add(new Attack { IsSpecial = false, Name = "attack" });
            attacks.Add(new Attack { IsSpecial = false, Name = "other attack" });

            selection.RequiresSpecialAttack = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void NotMetIfNoSpellLikeAbilities()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[1].Name = "other required feat";

            selection.RequiresSpellLikeAbility = true;

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MetIfSpellLikeAbilities()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[1].Name = FeatConstants.SpecialQualities.SpellLikeAbility;

            selection.RequiresSpellLikeAbility = true;

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfSpellLikeAbilitiesNotRequired()
        {
            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[1].Name = "other required feat";

            selection.RequiresSpellLikeAbility = false;

            var met = selection.MutableRequirementsMet(feats);
            Assert.That(met, Is.True);
        }

        [Test]
        public void NotMetIfDoesNotHaveRequiredSpeed()
        {
            selection.RequiredSpeeds["other speed"] = 0;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void NotMetIfDoesNotHaveRequiredSpeedValue(int requiredSpeed, int speed)
        {
            speeds["speed"].Value = speed;
            selection.RequiredSpeeds["speed"] = requiredSpeed;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void MetIfHasRequiredSpeed(int requiredSpeed, int speed)
        {
            speeds["speed"].Value = speed;
            selection.RequiredSpeeds["speed"] = requiredSpeed;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllNonPositiveValuesTestCases))]
        public void NotMetIfNoNaturalArmor(int naturalArmor)
        {
            selection.RequiresNaturalArmor = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, naturalArmor, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.AllPositiveValuesTestCases))]
        public void MetIfNaturalArmor(int naturalArmor)
        {
            selection.RequiresNaturalArmor = true;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, naturalArmor, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfNaturalArmorNotRequired()
        {
            selection.RequiresNaturalArmor = false;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void NotMetIfNaturalWeaponQuantityNotEnough(int requiredNaturalWeapons, int naturalWeapons)
        {
            selection.RequiredNaturalWeapons = requiredNaturalWeapons;

            attacks.Add(new Attack { IsNatural = false, Name = "not natural attack" });

            while (naturalWeapons-- > 0)
            {
                attacks.Add(new Attack { IsNatural = true, Name = $"natural attack {naturalWeapons}" });
            }

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void MetIfNaturalWeaponQuantityEnough(int requiredNaturalWeapons, int naturalWeapons)
        {
            selection.RequiredNaturalWeapons = requiredNaturalWeapons;

            attacks.Add(new Attack { IsNatural = false, Name = "not natural attack" });

            while (naturalWeapons-- > 0)
            {
                attacks.Add(new Attack { IsNatural = true, Name = $"natural attack {naturalWeapons}" });
            }

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfNaturalWeaponQuantityNotRequired()
        {
            selection.RequiredNaturalWeapons = 0;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueLessThanPositiveRequirement))]
        public void NotMetIfInsufficientHands(int requiredHands, int hands)
        {
            selection.RequiredHands = requiredHands;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, hands, string.Empty, false);
            Assert.That(met, Is.False);
        }

        [TestCaseSource(typeof(NumericTestData), nameof(NumericTestData.ValueGreaterThanOrEqualToPositiveRequirement))]
        public void MetIfSufficientHands(int requiredHands, int hands)
        {
            selection.RequiredHands = requiredHands;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, hands, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfSufficientHandsNotRequired()
        {
            selection.RequiredHands = 0;

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void NotMetIfDoesNotHaveSize()
        {
            selection.RequiredSizes = new[] { "size" };

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, "wrong size", false);
            Assert.That(met, Is.False);
        }

        [Test]
        public void NotMetIfDoesNotHaveAnySize()
        {
            selection.RequiredSizes = new[] { "size", "other size" };

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, "wrong size", false);
            Assert.That(met, Is.False);
        }

        [Test]
        public void MetIfHasSize()
        {
            selection.RequiredSizes = new[] { "size" };

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, "size", false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfHasAnySize()
        {
            selection.RequiredSizes = new[] { "size", "other size" };

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, "other size", false);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfSizeNotRequired()
        {
            Assert.That(selection.RequiredSizes, Is.Empty);

            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, "wrong size", false);
            Assert.That(met, Is.True);
        }

        [TestCase(true, true, true)]
        [TestCase(true, false, true)]
        [TestCase(false, true, false)]
        [TestCase(false, false, true)]
        public void HonorEquipmentRequirement(bool canUseEquipment, bool requiresEquipment, bool shouldBeMet)
        {
            selection.RequiresEquipment = requiresEquipment;
            var met = selection.ImmutableRequirementsMet(0, abilities, skills, attacks, 0, speeds, 0, 0, string.Empty, canUseEquipment);
            Assert.That(met, Is.EqualTo(shouldBeMet));
        }
    }
}