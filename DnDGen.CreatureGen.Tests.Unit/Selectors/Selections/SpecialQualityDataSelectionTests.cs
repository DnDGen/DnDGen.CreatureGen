using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class SpecialQualityDataSelectionTests
    {
        private SpecialQualityDataSelection selection;
        private Dictionary<string, Ability> abilities;
        private List<Feat> feats;
        private Alignment alignment;
        private HitPoints hitPoints;

        [SetUp]
        public void Setup()
        {
            selection = new SpecialQualityDataSelection();
            abilities = [];
            feats = [];
            alignment = new Alignment();
            hitPoints = new HitPoints();

            abilities["ability"] = new Ability("ability")
            {
                BaseScore = 42
            };

            hitPoints.HitDice.Add(new HitDice { Quantity = 1 });
        }

        [Test]
        public void SpecialQualitySelectionInitialization()
        {
            Assert.That(selection.Feat, Is.Empty);
            Assert.That(selection.Power, Is.Zero);
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.FrequencyQuantity, Is.Zero);
            Assert.That(selection.FrequencyTimePeriod, Is.Empty);
            Assert.That(selection.MinimumAbilities, Is.Empty);
            Assert.That(selection.RandomFociQuantityRoll, Is.Empty);
            Assert.That(selection.RequiredFeats, Is.Empty);
            Assert.That(selection.RequiredSizes, Is.Empty);
            Assert.That(selection.RequiredAlignments, Is.Empty);
            Assert.That(selection.RequiresEquipment, Is.False);
            Assert.That(selection.MinHitDice, Is.Zero);
            Assert.That(selection.MaxHitDice, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void SectionCountIs21()
        {
            Assert.That(selection.SectionCount, Is.EqualTo(21));
        }

        [Test]
        public void SeparatorsDoNotCollide()
        {
            var requiredFeat = new FeatDataSelection.RequiredFeatDataSelection();

            Assert.That(selection.Separator, Is.EqualTo('@')
                .And.Not.EqualTo(requiredFeat.Separator)
                .And.Not.EqualTo(SpecialQualityDataSelection.Delimiter)
                .And.Not.EqualTo(FeatDataSelection.RequiredFeatDataSelection.Delimiter));
            Assert.That(SpecialQualityDataSelection.Delimiter, Is.EqualTo('|')
                .And.Not.EqualTo(selection.Separator)
                .And.Not.EqualTo(requiredFeat.Separator)
                .And.Not.EqualTo(FeatDataSelection.RequiredFeatDataSelection.Delimiter));

            Assert.That(requiredFeat.Separator, Is.EqualTo('#')
                .And.Not.EqualTo(selection.Separator)
                .And.Not.EqualTo(SpecialQualityDataSelection.Delimiter)
                .And.Not.EqualTo(FeatDataSelection.RequiredFeatDataSelection.Delimiter));
            Assert.That(FeatDataSelection.RequiredFeatDataSelection.Delimiter, Is.EqualTo(',')
                .And.Not.EqualTo(selection.Separator)
                .And.Not.EqualTo(requiredFeat.Separator)
                .And.Not.EqualTo(FeatDataSelection.Delimiter));
        }

        private string[] GetTestDataArray()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.SpecialQualityData.FeatNameIndex] = "my feat";
            data[DataIndexConstants.SpecialQualityData.FocusIndex] = "my focus type";
            data[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex] = "90210";
            data[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex] = "my frequency time period";
            data[DataIndexConstants.SpecialQualityData.PowerIndex] = "600";
            data[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex] = "42d9266";
            data[DataIndexConstants.SpecialQualityData.SaveIndex] = "my save";
            data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex] = "my save ability";
            data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex] = "783";
            data[DataIndexConstants.SpecialQualityData.RequiredCharismaIndex] = "1337";
            data[DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex] = "1336";
            data[DataIndexConstants.SpecialQualityData.RequiredDexterityIndex] = "96";
            data[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 1", Foci = ["required foci 1.1", "required foci 1.2"] }),
                DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" })]);
            data[DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex] = "9";
            data[DataIndexConstants.SpecialQualityData.RequiredSizesIndex] = string.Join(FeatDataSelection.Delimiter, ["my required size", "my other required size"]);
            data[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex] = string.Join(FeatDataSelection.Delimiter, ["my required alignment", "my other required alignment"]);
            data[DataIndexConstants.SpecialQualityData.RequiredStrengthIndex] = "2";
            data[DataIndexConstants.SpecialQualityData.RequiredWisdomIndex] = "12";
            data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex] = bool.TrueString;
            data[DataIndexConstants.SpecialQualityData.MinHitDiceIndex] = "22";
            data[DataIndexConstants.SpecialQualityData.MaxHitDiceIndex] = "8245";

            return data;
        }

        [Test]
        public void Map_FromString_ReturnsSelection()
        {
            var data = GetTestDataArray();

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Feat, Is.EqualTo("my feat"));
            Assert.That(newSelection.FocusType, Is.EqualTo("my focus type"));
            Assert.That(newSelection.FrequencyQuantity, Is.EqualTo(90210));
            Assert.That(newSelection.FrequencyTimePeriod, Is.EqualTo("my frequency time period"));
            Assert.That(newSelection.Power, Is.EqualTo(600));
            Assert.That(newSelection.RandomFociQuantityRoll, Is.EqualTo("42d9266"));
            Assert.That(newSelection.Save, Is.EqualTo("my save"));
            Assert.That(newSelection.SaveAbility, Is.EqualTo("my save ability"));
            Assert.That(newSelection.SaveBaseValue, Is.EqualTo(783));
            Assert.That(newSelection.MinimumAbilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Strength], Is.EqualTo(2));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Constitution], Is.EqualTo(1336));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Dexterity], Is.EqualTo(96));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Intelligence], Is.EqualTo(9));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Wisdom], Is.EqualTo(12));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Charisma], Is.EqualTo(1337));
            Assert.That(newSelection.RequiredSizes, Is.EqualTo(["my required size", "my other required size"]));
            Assert.That(newSelection.RequiredAlignments, Is.EqualTo(["my required alignment", "my other required alignment"]));
            Assert.That(newSelection.RequiresEquipment, Is.EqualTo(true));
            Assert.That(newSelection.MinHitDice, Is.EqualTo(22));
            Assert.That(newSelection.MaxHitDice, Is.EqualTo(8245));

            var requiredFeats = newSelection.RequiredFeats.ToArray();
            Assert.That(requiredFeats, Has.Length.EqualTo(2).And.All.Not.Null);
            Assert.That(requiredFeats[0].Feat, Is.EqualTo("required feat 1"));
            Assert.That(requiredFeats[0].Foci, Is.EqualTo(["required foci 1.1", "required foci 1.2"]));
            Assert.That(requiredFeats[1].Feat, Is.EqualTo("required feat 2"));
            Assert.That(requiredFeats[1].Foci, Is.Empty);
        }

        [Test]
        public void Map_FromString_ReturnsSelection_NoRequirements()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredCharismaIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.RequiredDexterityIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex] = string.Empty;
            data[DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.RequiredSizesIndex] = string.Empty;
            data[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex] = string.Empty;
            data[DataIndexConstants.SpecialQualityData.RequiredStrengthIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.RequiredWisdomIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex] = bool.FalseString;
            data[DataIndexConstants.SpecialQualityData.MinHitDiceIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.MaxHitDiceIndex] = int.MaxValue.ToString();

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.MinimumAbilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Strength], Is.EqualTo(0));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Constitution], Is.EqualTo(0));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Dexterity], Is.EqualTo(0));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Intelligence], Is.EqualTo(0));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Wisdom], Is.EqualTo(0));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Charisma], Is.EqualTo(0));
            Assert.That(newSelection.MinHitDice, Is.EqualTo(0));
            Assert.That(newSelection.MaxHitDice, Is.EqualTo(int.MaxValue));
            Assert.That(newSelection.RequiredSizes, Is.Empty);
            Assert.That(newSelection.RequiredAlignments, Is.Empty);
            Assert.That(newSelection.RequiredFeats, Is.Empty);
            Assert.That(newSelection.RequiresEquipment, Is.EqualTo(false));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_NoRequiredFeats()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex] = string.Empty;

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredFeats, Is.Empty);
        }

        [Test]
        public void Map_FromString_ReturnsSelection_OneRequiredFeat()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" })]);

            var newSelection = SpecialQualityDataSelection.Map(data);
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
            data[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex] = string.Join(FeatDataSelection.Delimiter,
                [DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 1", Foci = ["required foci 1.1", "required foci 1.2"] }),
                DataHelper.Parse(new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" })]);

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);

            var requiredFeats = newSelection.RequiredFeats.ToArray();
            Assert.That(requiredFeats, Has.Length.EqualTo(2).And.All.Not.Null);
            Assert.That(requiredFeats[0].Feat, Is.EqualTo("required feat 1"));
            Assert.That(requiredFeats[0].Foci, Is.EqualTo(["required foci 1.1", "required foci 1.2"]));
            Assert.That(requiredFeats[1].Feat, Is.EqualTo("required feat 2"));
            Assert.That(requiredFeats[1].Foci, Is.Empty);
        }

        [Test]
        public void Map_FromString_ReturnsSelection_NoRequiredSizes()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredSizesIndex] = string.Empty;

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSizes, Is.Empty);
        }

        [Test]
        public void Map_FromString_ReturnsSelection_OneRequiredSize()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredSizesIndex] = "my size";

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSizes, Is.EqualTo(["my size"]));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_TwoRequiredSizes()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredSizesIndex] = string.Join(FeatDataSelection.Delimiter, ["my size", "my other size"]);

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSizes, Is.EqualTo(["my size", "my other size"]));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromString_ReturnsSelection_RequiresEquipment(bool required)
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex] = required.ToString();

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiresEquipment, Is.EqualTo(required));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_NoRequiredAlignments()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex] = string.Empty;

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredAlignments, Is.Empty);
        }

        [Test]
        public void Map_FromString_ReturnsSelection_OneRequiredAlignment()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex] = "my alignment";

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredAlignments, Is.EqualTo(["my alignment"]));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_TwoRequiredAlignments()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex] = string.Join(FeatDataSelection.Delimiter, ["my alignment", "my other alignment"]);

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredAlignments, Is.EqualTo(["my alignment", "my other alignment"]));
        }

        [Test]
        public void Map_FromString_HasSave()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.SaveIndex] = "my save";
            data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex] = "my save ability";
            data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex] = "6629";

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Save, Is.EqualTo("my save"));
            Assert.That(newSelection.SaveAbility, Is.EqualTo("my save ability"));
            Assert.That(newSelection.SaveBaseValue, Is.EqualTo(6629));
        }

        [Test]
        public void Map_FromString_HasNoSave()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.SaveIndex] = string.Empty;
            data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex] = string.Empty;
            data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex] = "0";

            var newSelection = SpecialQualityDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Save, Is.Empty);
            Assert.That(newSelection.SaveAbility, Is.Empty);
            Assert.That(newSelection.SaveBaseValue, Is.Zero);
        }

        [Test]
        public void Map_FromSelection_ReturnsString()
        {
            var selection = GetTestDataSelection();

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo("my feat"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.FocusIndex], Is.EqualTo("my focus type"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo("90210"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo("my frequency time period"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo("42d9266"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredStrengthIndex], Is.EqualTo("2"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex], Is.EqualTo("1336"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredDexterityIndex], Is.EqualTo("96"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex], Is.EqualTo("9"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredWisdomIndex], Is.EqualTo("12"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredCharismaIndex], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveIndex], Is.EqualTo("my save"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.EqualTo("my save ability"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo("783"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.MaxHitDiceIndex], Is.EqualTo("8245"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.MinHitDiceIndex], Is.EqualTo("22"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredSizesIndex], Is.EqualTo("my required size|my other required size")
                .And.EqualTo(string.Join(FeatDataSelection.Delimiter, ["my required size", "my other required size"])));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex], Is.EqualTo("my required alignment|my other required alignment")
                .And.EqualTo(string.Join(FeatDataSelection.Delimiter, ["my required alignment", "my other required alignment"])));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(bool.TrueString));

            var requiredFeatsData = string.Join(FeatDataSelection.Delimiter, selection.RequiredFeats.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex], Is.EqualTo(requiredFeatsData));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_NoRequirements()
        {
            var selection = GetTestDataSelection();
            selection.MinimumAbilities[AbilityConstants.Strength] = 0;
            selection.MinimumAbilities[AbilityConstants.Constitution] = 0;
            selection.MinimumAbilities[AbilityConstants.Dexterity] = 0;
            selection.MinimumAbilities[AbilityConstants.Intelligence] = 0;
            selection.MinimumAbilities[AbilityConstants.Wisdom] = 0;
            selection.MinimumAbilities[AbilityConstants.Charisma] = 0;
            selection.RequiredSizes = [];
            selection.RequiredAlignments = [];
            selection.RequiredFeats = [];
            selection.RequiresEquipment = false;
            selection.MinHitDice = 0;
            selection.MaxHitDice = int.MaxValue;

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredStrengthIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredDexterityIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredWisdomIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredCharismaIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredSizesIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(bool.FalseString));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.MinHitDiceIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.MaxHitDiceIndex], Is.EqualTo(int.MaxValue.ToString()));
        }

        [TestCase(AbilityConstants.Strength, DataIndexConstants.SpecialQualityData.RequiredStrengthIndex)]
        [TestCase(AbilityConstants.Constitution, DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex)]
        [TestCase(AbilityConstants.Dexterity, DataIndexConstants.SpecialQualityData.RequiredDexterityIndex)]
        [TestCase(AbilityConstants.Intelligence, DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex)]
        [TestCase(AbilityConstants.Wisdom, DataIndexConstants.SpecialQualityData.RequiredWisdomIndex)]
        [TestCase(AbilityConstants.Charisma, DataIndexConstants.SpecialQualityData.RequiredCharismaIndex)]
        public void Map_FromSelection_ReturnsString_MissingAbility(string ability, int index)
        {
            var selection = GetTestDataSelection();
            selection.MinimumAbilities.Remove(ability);

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[index], Is.EqualTo("0"));

            var indices = new[]
            {
                DataIndexConstants.SpecialQualityData.RequiredStrengthIndex,
                DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex,
                DataIndexConstants.SpecialQualityData.RequiredDexterityIndex,
                DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex,
                DataIndexConstants.SpecialQualityData.RequiredWisdomIndex,
                DataIndexConstants.SpecialQualityData.RequiredCharismaIndex
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
            selection.MinimumAbilities.Clear();

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var indices = new[]
            {
                DataIndexConstants.SpecialQualityData.RequiredStrengthIndex,
                DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex,
                DataIndexConstants.SpecialQualityData.RequiredDexterityIndex,
                DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex,
                DataIndexConstants.SpecialQualityData.RequiredWisdomIndex,
                DataIndexConstants.SpecialQualityData.RequiredCharismaIndex
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

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex], Is.Empty);
        }

        [Test]
        public void Map_FromSelection_ReturnsString_OneRequiredFeat()
        {
            var selection = GetTestDataSelection();
            selection.RequiredFeats = [new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" }];

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var requiredFeats = string.Join(FeatDataSelection.Delimiter, selection.RequiredFeats.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex], Is.EqualTo(requiredFeats));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_TwoRequiredFeats()
        {
            var selection = GetTestDataSelection();
            selection.RequiredFeats =
                [new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 1", Foci = ["required foci 1.1", "required foci 1.2"] },
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" }];

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var requiredFeats = string.Join(FeatDataSelection.Delimiter, selection.RequiredFeats.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex], Is.EqualTo(requiredFeats));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_NoRequiredSizes()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSizes = [];

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredSizesIndex], Is.Empty);
        }

        [Test]
        public void Map_FromSelection_ReturnsString_OneRequiredSize()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSizes = ["my size"];

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredSizesIndex], Is.EqualTo("my size"));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_TwoRequiredSizes()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSizes = ["my size", "my other size"];

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredSizesIndex], Is.EqualTo("my size|my other size")
                .And.EqualTo(string.Join(FeatDataSelection.Delimiter, selection.RequiredSizes)));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_NoRequiredAlignments()
        {
            var selection = GetTestDataSelection();
            selection.RequiredAlignments = [];

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex], Is.Empty);
        }

        [Test]
        public void Map_FromSelection_ReturnsString_OneRequiredAlignment()
        {
            var selection = GetTestDataSelection();
            selection.RequiredAlignments = ["my alignment"];

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex], Is.EqualTo("my alignment"));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_TwoRequiredAlignments()
        {
            var selection = GetTestDataSelection();
            selection.RequiredAlignments = ["my alignment", "my other alignment"];

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex], Is.EqualTo("my alignment|my other alignment")
                .And.EqualTo(string.Join(FeatDataSelection.Delimiter, selection.RequiredAlignments)));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromSelection_ReturnsString_RequiresEquipment(bool required)
        {
            var selection = GetTestDataSelection();
            selection.RequiresEquipment = required;

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(required.ToString()));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_WithSave()
        {
            var selection = GetTestDataSelection();
            selection.Save = "my save";
            selection.SaveAbility = "my save ability";
            selection.SaveBaseValue = 783;

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveIndex], Is.EqualTo("my save"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.EqualTo("my save ability"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo("783"));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_WithoutSave()
        {
            var selection = GetTestDataSelection();
            selection.Save = string.Empty;
            selection.SaveAbility = string.Empty;
            selection.SaveBaseValue = 0;

            var rawData = SpecialQualityDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo("0"));
        }

        private SpecialQualityDataSelection GetTestDataSelection() => new()
        {
            Feat = "my feat",
            FocusType = "my focus type",
            FrequencyQuantity = 90210,
            FrequencyTimePeriod = "my frequency time period",
            RandomFociQuantityRoll = "42d9266",
            Power = 600,
            MinimumAbilities = new()
            {
                [AbilityConstants.Strength] = 2,
                [AbilityConstants.Constitution] = 1336,
                [AbilityConstants.Dexterity] = 96,
                [AbilityConstants.Intelligence] = 9,
                [AbilityConstants.Wisdom] = 12,
                [AbilityConstants.Charisma] = 1337,
            },
            MinHitDice = 22,
            MaxHitDice = 8245,
            RequiredSizes = ["my required size", "my other required size"],
            RequiredAlignments = ["my required alignment", "my other required alignment"],
            RequiresEquipment = true,
            RequiredFeats = [
                new() { Feat = "required feat 1", Foci = ["required foci 1.1", "required foci 1.2"] },
                new() { Feat = "required feat 2" },
            ],
            Save = "my save",
            SaveAbility = "my save ability",
            SaveBaseValue = 783,
        };

        [Test]
        public void MapTo_ReturnsSelection()
        {
            var data = GetTestDataArray();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Feat, Is.EqualTo("my feat"));
            Assert.That(newSelection.FocusType, Is.EqualTo("my focus type"));
            Assert.That(newSelection.FrequencyQuantity, Is.EqualTo(90210));
            Assert.That(newSelection.FrequencyTimePeriod, Is.EqualTo("my frequency time period"));
            Assert.That(newSelection.Power, Is.EqualTo(600));
            Assert.That(newSelection.RandomFociQuantityRoll, Is.EqualTo("42d9266"));
            Assert.That(newSelection.Save, Is.EqualTo("my save"));
            Assert.That(newSelection.SaveAbility, Is.EqualTo("my save ability"));
            Assert.That(newSelection.SaveBaseValue, Is.EqualTo(783));
            Assert.That(newSelection.MinimumAbilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Strength], Is.EqualTo(2));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Constitution], Is.EqualTo(1336));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Dexterity], Is.EqualTo(96));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Intelligence], Is.EqualTo(9));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Wisdom], Is.EqualTo(12));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Charisma], Is.EqualTo(1337));
            Assert.That(newSelection.RequiredSizes, Is.EqualTo(["my required size", "my other required size"]));
            Assert.That(newSelection.RequiredAlignments, Is.EqualTo(["my required alignment", "my other required alignment"]));
            Assert.That(newSelection.RequiresEquipment, Is.EqualTo(true));
            Assert.That(newSelection.MinHitDice, Is.EqualTo(22));
            Assert.That(newSelection.MaxHitDice, Is.EqualTo(8245));

            var requiredFeats = newSelection.RequiredFeats.ToArray();
            Assert.That(requiredFeats, Has.Length.EqualTo(2).And.All.Not.Null);
            Assert.That(requiredFeats[0].Feat, Is.EqualTo("required feat 1"));
            Assert.That(requiredFeats[0].Foci, Is.EqualTo(["required foci 1.1", "required foci 1.2"]));
            Assert.That(requiredFeats[1].Feat, Is.EqualTo("required feat 2"));
            Assert.That(requiredFeats[1].Foci, Is.Empty);
        }

        [Test]
        public void MapTo_ReturnsSelection_NoRequirements()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredCharismaIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.RequiredDexterityIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex] = string.Empty;
            data[DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.RequiredSizesIndex] = string.Empty;
            data[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex] = string.Empty;
            data[DataIndexConstants.SpecialQualityData.RequiredStrengthIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.RequiredWisdomIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex] = bool.FalseString;
            data[DataIndexConstants.SpecialQualityData.MinHitDiceIndex] = "0";
            data[DataIndexConstants.SpecialQualityData.MaxHitDiceIndex] = int.MaxValue.ToString();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.MinimumAbilities, Has.Count.EqualTo(6)
                .And.ContainKey(AbilityConstants.Strength)
                .And.ContainKey(AbilityConstants.Constitution)
                .And.ContainKey(AbilityConstants.Dexterity)
                .And.ContainKey(AbilityConstants.Intelligence)
                .And.ContainKey(AbilityConstants.Wisdom)
                .And.ContainKey(AbilityConstants.Charisma));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Strength], Is.EqualTo(0));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Constitution], Is.EqualTo(0));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Dexterity], Is.EqualTo(0));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Intelligence], Is.EqualTo(0));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Wisdom], Is.EqualTo(0));
            Assert.That(newSelection.MinimumAbilities[AbilityConstants.Charisma], Is.EqualTo(0));
            Assert.That(newSelection.MinHitDice, Is.EqualTo(0));
            Assert.That(newSelection.MaxHitDice, Is.EqualTo(int.MaxValue));
            Assert.That(newSelection.RequiredSizes, Is.Empty);
            Assert.That(newSelection.RequiredAlignments, Is.Empty);
            Assert.That(newSelection.RequiredFeats, Is.Empty);
            Assert.That(newSelection.RequiresEquipment, Is.EqualTo(false));
        }

        [Test]
        public void MapTo_ReturnsSelection_NoRequiredFeats()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex] = string.Empty;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredFeats, Is.Empty);
        }

        [Test]
        public void MapTo_ReturnsSelection_OneRequiredFeat()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex] = string.Join(FeatDataSelection.Delimiter,
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
            data[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex] = string.Join(FeatDataSelection.Delimiter,
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
        public void MapTo_ReturnsSelection_NoRequiredSizes()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredSizesIndex] = string.Empty;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSizes, Is.Empty);
        }

        [Test]
        public void MapTo_ReturnsSelection_OneRequiredSize()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredSizesIndex] = "my size";

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSizes, Is.EqualTo(["my size"]));
        }

        [Test]
        public void MapTo_ReturnsSelection_TwoRequiredSizes()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredSizesIndex] = string.Join(FeatDataSelection.Delimiter, ["my size", "my other size"]);

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredSizes, Is.EqualTo(["my size", "my other size"]));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapTo_ReturnsSelection_RequiresEquipment(bool required)
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex] = required.ToString();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiresEquipment, Is.EqualTo(required));
        }

        [Test]
        public void MapTo_ReturnsSelection_NoRequiredAlignments()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex] = string.Empty;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredAlignments, Is.Empty);
        }

        [Test]
        public void MapTo_ReturnsSelection_OneRequiredAlignment()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex] = "my alignment";

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredAlignments, Is.EqualTo(["my alignment"]));
        }

        [Test]
        public void MapTo_ReturnsSelection_TwoRequiredAlignments()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex] = string.Join(FeatDataSelection.Delimiter, ["my alignment", "my other alignment"]);

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.RequiredAlignments, Is.EqualTo(["my alignment", "my other alignment"]));
        }

        [Test]
        public void MapTo_HasSave()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.SaveIndex] = "my save";
            data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex] = "my save ability";
            data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex] = "6629";

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Save, Is.EqualTo("my save"));
            Assert.That(newSelection.SaveAbility, Is.EqualTo("my save ability"));
            Assert.That(newSelection.SaveBaseValue, Is.EqualTo(6629));
        }

        [Test]
        public void MapTo_HasNoSave()
        {
            var data = GetTestDataArray();
            data[DataIndexConstants.SpecialQualityData.SaveIndex] = string.Empty;
            data[DataIndexConstants.SpecialQualityData.SaveAbilityIndex] = string.Empty;
            data[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex] = "0";

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.Save, Is.Empty);
            Assert.That(newSelection.SaveAbility, Is.Empty);
            Assert.That(newSelection.SaveBaseValue, Is.Zero);
        }

        [Test]
        public void MapFrom_ReturnsString()
        {
            var selection = GetTestDataSelection();

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.FeatNameIndex], Is.EqualTo("my feat"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.FocusIndex], Is.EqualTo("my focus type"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.FrequencyQuantityIndex], Is.EqualTo("90210"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.FrequencyTimePeriodIndex], Is.EqualTo("my frequency time period"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RandomFociQuantityIndex], Is.EqualTo("42d9266"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.PowerIndex], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredStrengthIndex], Is.EqualTo("2"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex], Is.EqualTo("1336"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredDexterityIndex], Is.EqualTo("96"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex], Is.EqualTo("9"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredWisdomIndex], Is.EqualTo("12"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredCharismaIndex], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveIndex], Is.EqualTo("my save"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.EqualTo("my save ability"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo("783"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.MaxHitDiceIndex], Is.EqualTo("8245"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.MinHitDiceIndex], Is.EqualTo("22"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredSizesIndex], Is.EqualTo("my required size|my other required size")
                .And.EqualTo(string.Join(FeatDataSelection.Delimiter, ["my required size", "my other required size"])));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex], Is.EqualTo("my required alignment|my other required alignment")
                .And.EqualTo(string.Join(FeatDataSelection.Delimiter, ["my required alignment", "my other required alignment"])));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(bool.TrueString));

            var requiredFeatsData = string.Join(FeatDataSelection.Delimiter, selection.RequiredFeats.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex], Is.EqualTo(requiredFeatsData));
        }

        [Test]
        public void MapFrom_ReturnsString_NoRequirements()
        {
            var selection = GetTestDataSelection();
            selection.MinimumAbilities[AbilityConstants.Strength] = 0;
            selection.MinimumAbilities[AbilityConstants.Constitution] = 0;
            selection.MinimumAbilities[AbilityConstants.Dexterity] = 0;
            selection.MinimumAbilities[AbilityConstants.Intelligence] = 0;
            selection.MinimumAbilities[AbilityConstants.Wisdom] = 0;
            selection.MinimumAbilities[AbilityConstants.Charisma] = 0;
            selection.RequiredSizes = [];
            selection.RequiredAlignments = [];
            selection.RequiredFeats = [];
            selection.RequiresEquipment = false;
            selection.MinHitDice = 0;
            selection.MaxHitDice = int.MaxValue;

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredStrengthIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredDexterityIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredWisdomIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredCharismaIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredSizesIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(bool.FalseString));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.MinHitDiceIndex], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.MaxHitDiceIndex], Is.EqualTo(int.MaxValue.ToString()));
        }

        [TestCase(AbilityConstants.Strength, DataIndexConstants.SpecialQualityData.RequiredStrengthIndex)]
        [TestCase(AbilityConstants.Constitution, DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex)]
        [TestCase(AbilityConstants.Dexterity, DataIndexConstants.SpecialQualityData.RequiredDexterityIndex)]
        [TestCase(AbilityConstants.Intelligence, DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex)]
        [TestCase(AbilityConstants.Wisdom, DataIndexConstants.SpecialQualityData.RequiredWisdomIndex)]
        [TestCase(AbilityConstants.Charisma, DataIndexConstants.SpecialQualityData.RequiredCharismaIndex)]
        public void MapFrom_ReturnsString_MissingAbility(string ability, int index)
        {
            var selection = GetTestDataSelection();
            selection.MinimumAbilities.Remove(ability);

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[index], Is.EqualTo("0"));

            var indices = new[]
            {
                DataIndexConstants.SpecialQualityData.RequiredStrengthIndex,
                DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex,
                DataIndexConstants.SpecialQualityData.RequiredDexterityIndex,
                DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex,
                DataIndexConstants.SpecialQualityData.RequiredWisdomIndex,
                DataIndexConstants.SpecialQualityData.RequiredCharismaIndex
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
            selection.MinimumAbilities.Clear();

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var indices = new[]
            {
                DataIndexConstants.SpecialQualityData.RequiredStrengthIndex,
                DataIndexConstants.SpecialQualityData.RequiredConstitutionIndex,
                DataIndexConstants.SpecialQualityData.RequiredDexterityIndex,
                DataIndexConstants.SpecialQualityData.RequiredIntelligenceIndex,
                DataIndexConstants.SpecialQualityData.RequiredWisdomIndex,
                DataIndexConstants.SpecialQualityData.RequiredCharismaIndex
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
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex], Is.Empty);
        }

        [Test]
        public void MapFrom_ReturnsString_OneRequiredFeat()
        {
            var selection = GetTestDataSelection();
            selection.RequiredFeats = [new FeatDataSelection.RequiredFeatDataSelection { Feat = "required feat 2" }];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));

            var requiredFeats = string.Join(FeatDataSelection.Delimiter, selection.RequiredFeats.Select(DataHelper.Parse));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex], Is.EqualTo(requiredFeats));
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
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredFeatsIndex], Is.EqualTo(requiredFeats));
        }

        [Test]
        public void MapFrom_ReturnsString_NoRequiredSizes()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSizes = [];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredSizesIndex], Is.Empty);
        }

        [Test]
        public void MapFrom_ReturnsString_OneRequiredSize()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSizes = ["my size"];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredSizesIndex], Is.EqualTo("my size"));
        }

        [Test]
        public void MapFrom_ReturnsString_TwoRequiredSizes()
        {
            var selection = GetTestDataSelection();
            selection.RequiredSizes = ["my size", "my other size"];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredSizesIndex], Is.EqualTo("my size|my other size")
                .And.EqualTo(string.Join(FeatDataSelection.Delimiter, selection.RequiredSizes)));
        }

        [Test]
        public void MapFrom_ReturnsString_NoRequiredAlignments()
        {
            var selection = GetTestDataSelection();
            selection.RequiredAlignments = [];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex], Is.Empty);
        }

        [Test]
        public void MapFrom_ReturnsString_OneRequiredAlignment()
        {
            var selection = GetTestDataSelection();
            selection.RequiredAlignments = ["my alignment"];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex], Is.EqualTo("my alignment"));
        }

        [Test]
        public void MapFrom_ReturnsString_TwoRequiredAlignments()
        {
            var selection = GetTestDataSelection();
            selection.RequiredAlignments = ["my alignment", "my other alignment"];

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiredAlignmentsIndex], Is.EqualTo("my alignment|my other alignment")
                .And.EqualTo(string.Join(FeatDataSelection.Delimiter, selection.RequiredAlignments)));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapFrom_ReturnsString_RequiresEquipment(bool required)
        {
            var selection = GetTestDataSelection();
            selection.RequiresEquipment = required;

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.RequiresEquipmentIndex], Is.EqualTo(required.ToString()));
        }

        [Test]
        public void MapFrom_ReturnsString_WithSave()
        {
            var selection = GetTestDataSelection();
            selection.Save = "my save";
            selection.SaveAbility = "my save ability";
            selection.SaveBaseValue = 783;

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveIndex], Is.EqualTo("my save"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.EqualTo("my save ability"));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo("783"));
        }

        [Test]
        public void MapFrom_ReturnsString_WithoutSave()
        {
            var selection = GetTestDataSelection();
            selection.Save = string.Empty;
            selection.SaveAbility = string.Empty;
            selection.SaveBaseValue = 0;

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveAbilityIndex], Is.Empty);
            Assert.That(rawData[DataIndexConstants.SpecialQualityData.SaveBaseValueIndex], Is.EqualTo("0"));
        }

        [Test]
        public void RequirementsMetIfNoRequirements()
        {
            var met = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfDoesNotHaveMinimumStat()
        {
            selection.MinimumAbilities["ability"] = 9266;

            var met = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsMetIfMinimumStatIsMet()
        {
            selection.MinimumAbilities["ability"] = 9266;

            abilities["ability"] = new Ability("ability")
            {
                BaseScore = 9267
            };

            var met = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsMetIfMinimumStatIsMetWithRacialBonus()
        {
            selection.MinimumAbilities["ability"] = 9266;

            abilities["ability"] = new Ability("ability")
            {
                BaseScore = 10,
                RacialAdjustment = 9256
            };

            var met = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(met, Is.True);
        }

        [Test]
        public void RequirementsNotMetIfMinimumStatIsNotMetWithRacialBonus()
        {
            selection.MinimumAbilities["ability"] = 9266;

            abilities["ability"] = new Ability("ability")
            {
                BaseScore = 10,
                RacialAdjustment = 9255
            };

            var met = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(met, Is.False);
        }

        [Test]
        public void RequirementsMetIfAnyMinimumStatIsMet()
        {
            selection.MinimumAbilities["ability"] = 9266;
            selection.MinimumAbilities["ability 2"] = 600;

            abilities["ability 2"] = new Ability("ability 2")
            {
                BaseScore = 600
            };

            var met = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(met, Is.True);
        }

        [Test]
        public void MetIfNoRequiredFeats()
        {
            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetWithAllRequiredFeats()
        {
            selection.RequiredFeats = [new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 1" }, new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 2" }];
            feats.Add(new Feat { Name = "feat 1" });
            feats.Add(new Feat { Name = "feat 2" });

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void NotMetWithSomeRequiredFeats()
        {
            selection.RequiredFeats = [new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 1" }, new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 2" }];
            feats.Add(new Feat { Name = "feat 1" });

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void NotMetWithNoRequiredFeats()
        {
            selection.RequiredFeats = [new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 1" }, new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 2" }];

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void MetWithAllRequiredFeatsAndFoci()
        {
            selection.RequiredFeats =
            [
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 1", Foci = ["focus 1"] },
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 2", Foci = ["focus 2"] }
            ];

            feats.Add(new Feat { Name = "feat 1", Foci = ["focus 1", "focus 3"] });
            feats.Add(new Feat { Name = "feat 2", Foci = ["focus 4", "focus 2"] });

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetWithExtraRequiredFeatsAndFoci()
        {
            selection.RequiredFeats =
            [
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 1", Foci = ["focus 1"] },
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 2", Foci = ["focus 2"] }
            ];

            feats.Add(new Feat { Name = "feat 1", Foci = ["focus 1", "focus 2"] });
            feats.Add(new Feat { Name = "feat 2", Foci = ["focus 2", "focus 1", "focus 3"] });
            feats.Add(new Feat { Name = "feat 3" });
            feats.Add(new Feat { Name = "feat 4", Foci = ["focus 4"] });

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void NotMetWithSomeRequiredFeatsAndFoci()
        {
            selection.RequiredFeats =
            [
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 1", Foci = ["focus 1"] },
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 2", Foci = ["focus 2"] }
            ];

            feats.Add(new Feat { Name = "feat 1", Foci = ["focus 2"] });
            feats.Add(new Feat { Name = "feat 2", Foci = ["focus 2", "focus 1"] });
            feats.Add(new Feat { Name = "feat 3", Foci = ["focus 1"] });

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void NotMetWithNoRequiredFeatsAndFoci()
        {
            selection.RequiredFeats =
            [
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 1", Foci = ["focus 1"] },
                new FeatDataSelection.RequiredFeatDataSelection { Feat = "feat 2", Foci = ["focus 2"] }
            ];

            feats.Add(new Feat { Name = "feat 1", Foci = ["focus 2"] });
            feats.Add(new Feat { Name = "feat 2", Foci = ["focus 1"] });
            feats.Add(new Feat { Name = "feat 3", Foci = ["focus 1", "focus 2"] });

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void MetIfEquipmentNotRequiredAndCreatureCanUseEquipment()
        {
            selection.RequiresEquipment = false;

            var requirementsMet = selection.RequirementsMet(abilities, feats, true, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetIfEquipmentRequiredAndCreatureCanUseEquipment()
        {
            selection.RequiresEquipment = true;

            var requirementsMet = selection.RequirementsMet(abilities, feats, true, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void NotMetIfEquipmentRequiredAndCreatureCannotUseEquipment()
        {
            selection.RequiresEquipment = true;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void MetIfEquipmentNotRequiredAndCreatureCannotUseEquipment()
        {
            selection.RequiresEquipment = false;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetIfNoRequiredCreatureSize()
        {
            selection.RequiredSizes = [];

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "wrong size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetIfSizeIsRequiredCreatureSize()
        {
            selection.RequiredSizes = ["size"];

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetIfSizeIsAnyRequiredCreatureSize()
        {
            selection.RequiredSizes = ["other size", "size"];

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void NotMetIfSizeIsNotRequiredCreatureSize()
        {
            selection.RequiredSizes = ["size"];

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "wrong size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void NotMetIfSizeIsNotAnyRequiredCreatureSize()
        {
            selection.RequiredSizes = ["other size", "size"];

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "wrong size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.False);
        }

        [Test]
        public void MetIfNoRequiredAlignment()
        {
            selection.RequiredAlignments = [];

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "wrong size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [Test]
        public void MetIfSizeIsRequiredAlignment()
        {
            selection.RequiredAlignments = ["lawfulness goodness"];

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [TestCase("lawfulness", "goodness")]
        [TestCase("other lawfulness", "goodness")]
        [TestCase("lawfulness", "other goodness")]
        [TestCase("other lawfulness", "other goodness")]
        public void MetIfSizeIsAnyRequiredAlignment(string lawfulness, string goodness)
        {
            selection.RequiredAlignments = ["other lawfulness goodness", "lawfulness other goodness", "lawfulness goodness", "other lawfulness other goodness"];

            alignment.Goodness = goodness;
            alignment.Lawfulness = lawfulness;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }

        [TestCase("wrong lawfulness", "goodness")]
        [TestCase("lawfulness", "wrong goodness")]
        [TestCase("wrong lawfulness", "wrong goodness")]
        public void NotMetIfSizeIsNotRequiredAlignment(string lawfulness, string goodness)
        {
            selection.RequiredAlignments = ["lawfulness goodness"];

            alignment.Goodness = goodness;
            alignment.Lawfulness = lawfulness;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.False);
        }

        [TestCase("wrong lawfulness", "goodness")]
        [TestCase("lawfulness", "wrong goodness")]
        [TestCase("wrong lawfulness", "wrong goodness")]
        public void NotMetIfSizeIsNotAnyRequiredAlignment(string lawfulness, string goodness)
        {
            selection.RequiredAlignments = ["other lawfulness goodness", "lawfulness other goodness", "lawfulness goodness", "other lawfulness other goodness"];

            alignment.Goodness = goodness;
            alignment.Lawfulness = lawfulness;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.False);
        }

        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(2, 3)]
        [TestCase(1, 4)]
        [TestCase(2, 4)]
        [TestCase(3, 4)]
        [TestCase(42, 9266)]
        public void NotMetIfBelowMinimumHitDice(int hd, int min)
        {
            selection.MinHitDice = min;
            hitPoints.HitDice[0].Quantity = hd;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.False);
        }

        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 1)]
        [TestCase(4, 2)]
        [TestCase(90210, 9266)]
        public void NotMetIfAboveMaximumHitDice(int hd, int max)
        {
            selection.MaxHitDice = max;
            hitPoints.HitDice[0].Quantity = hd;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.False);
        }

        [TestCase(.5, 1, int.MaxValue)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 0, 2)]
        [TestCase(1, 0, int.MaxValue)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 1, 2)]
        [TestCase(1, 1, int.MaxValue)]
        [TestCase(2, 0, 2)]
        [TestCase(2, 0, int.MaxValue)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 1, int.MaxValue)]
        [TestCase(2, 2, 2)]
        [TestCase(2, 2, int.MaxValue)]
        [TestCase(2, 2, 4)]
        [TestCase(3, 2, 4)]
        [TestCase(4, 2, 4)]
        [TestCase(4, 4, 4)]
        [TestCase(42, 0, 9266)]
        [TestCase(600, 42, int.MaxValue)]
        [TestCase(600, 42, 1337)]
        public void MetIfHitDiceInRange(double hd, int min, int max)
        {
            selection.MinHitDice = min;
            selection.MaxHitDice = max;
            hitPoints.HitDice[0].Quantity = hd;

            var requirementsMet = selection.RequirementsMet(abilities, feats, false, "size", alignment, hitPoints);
            Assert.That(requirementsMet, Is.True);
        }
    }
}