using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Feats
{
    [TestFixture]
    public class FeatsGeneratorTests
    {
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IFeatsSelector> mockFeatsSelector;
        private Mock<IFeatFocusGenerator> mockFeatFocusGenerator;
        private IFeatsGenerator featsGenerator;
        private Dictionary<string, Ability> abilities;
        private Dictionary<string, Measurement> speeds;
        private List<Skill> skills;
        private List<FeatDataSelection> featSelections;
        private List<SpecialQualitySelection> specialQualitySelections;
        private Mock<Dice> mockDice;
        private HitPoints hitPoints;
        private List<Attack> attacks;
        private List<Feat> specialQualities;
        private CreatureType creatureType;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockFeatsSelector = new Mock<IFeatsSelector>();
            mockFeatFocusGenerator = new Mock<IFeatFocusGenerator>();
            mockDice = new Mock<Dice>();
            featsGenerator = new FeatsGenerator(mockCollectionsSelector.Object, mockFeatsSelector.Object, mockFeatFocusGenerator.Object, mockDice.Object);

            abilities = new Dictionary<string, Ability>();
            skills = new List<Skill>();
            featSelections = new List<FeatDataSelection>();
            specialQualitySelections = new List<SpecialQualitySelection>();
            hitPoints = new HitPoints();
            attacks = new List<Attack>();
            specialQualities = new List<Feat>();
            speeds = new Dictionary<string, Measurement>();
            creatureType = new CreatureType();
            alignment = new Alignment("creature alignment");

            hitPoints.HitDice.Add(new HitDice { Quantity = 1 });
            creatureType.Name = "creature type";

            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);

            mockFeatsSelector.Setup(s => s.SelectFeats()).Returns(featSelections);
            mockFeatsSelector.Setup(s => s.SelectSpecialQualities("creature", creatureType)).Returns(specialQualitySelections);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<FeatDataSelection>>())).Returns((IEnumerable<FeatDataSelection> fs) => fs.First());
        }

        [Test]
        public void CreaturesWithoutIntelligenceReceiveNoFeats()
        {
            abilities[AbilityConstants.Intelligence].BaseScore = 0;
            hitPoints.HitDice[0].Quantity = 20;
            AddFeatSelections(8);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            Assert.That(feats, Is.Empty);
        }

        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 2)]
        [TestCase(5, 2)]
        [TestCase(6, 3)]
        [TestCase(7, 3)]
        [TestCase(8, 3)]
        [TestCase(9, 4)]
        [TestCase(10, 4)]
        [TestCase(11, 4)]
        [TestCase(12, 5)]
        [TestCase(13, 5)]
        [TestCase(14, 5)]
        [TestCase(15, 6)]
        [TestCase(16, 6)]
        [TestCase(17, 6)]
        [TestCase(18, 7)]
        [TestCase(19, 7)]
        [TestCase(20, 7)]
        public void GetFeats(int hitDiceQuantity, int numberOfFeats)
        {
            hitPoints.HitDice[0].Quantity = hitDiceQuantity;
            AddFeatSelections(numberOfFeats + 2);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var featNames = feats.Select(f => f.Name);

            for (var i = 0; i < numberOfFeats; i++)
                Assert.That(featNames, Contains.Item(featSelections[i].Feat));

            Assert.That(featNames.Count(), Is.EqualTo(numberOfFeats));
        }

        private void AddFeatSelections(int quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                var selection = new FeatDataSelection();
                selection.Feat = $"feat{i + 1}";

                featSelections.Add(selection);
            }
        }

        [Test]
        public void DoNotGetFeatIfNoneAvailable()
        {
            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetMoreFeatsIfNoneAvailable()
        {
            AddFeatSelections(1);
            featSelections[0].RequiredBaseAttack = 90210;

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void FeatsPickedAtRandom()
        {
            hitPoints.HitDice[0].Quantity = 3;
            AddFeatSelections(3);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<FeatDataSelection>>(fs => fs.Count() == 3)))
                .Returns((IEnumerable<FeatDataSelection> fs) => fs.Last());
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<FeatDataSelection>>(fs => fs.Count() == 2)))
                .Returns((IEnumerable<FeatDataSelection> fs) => fs.First());

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].Feat));
            Assert.That(featNames, Contains.Item(featSelections[2].Feat));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DoNotGetFeatsWithUnmetPrerequisite()
        {
            AddFeatSelections(2);
            featSelections[0].RequiredBaseAttack = 9266;

            var feats = featsGenerator.GenerateFeats(hitPoints, 9265, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[1].Feat));
            Assert.That(featNames.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DoNotGetFeatsWithUnmetPrerequisite_NeedsEquipment()
        {
            AddFeatSelections(2);
            featSelections[0].RequiresEquipment = true;

            var feats = featsGenerator.GenerateFeats(hitPoints, 9265, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[1].Feat));
            Assert.That(featNames.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetFeatsWithMetPrerequisite()
        {
            AddFeatSelections(2);
            featSelections[0].RequiredBaseAttack = 9266;

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].Feat));
            Assert.That(featNames.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ReassessPrerequisitesEveryFeat()
        {
            hitPoints.HitDice[0].Quantity = 3;
            AddFeatSelections(3);
            featSelections[1].RequiredFeats = new[] { new RequiredFeatSelection { Feat = featSelections[0].Feat } };

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<FeatDataSelection>>())).Returns((IEnumerable<FeatDataSelection> fs) => fs.ElementAt(index++ % fs.Count()));

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].Feat));
            Assert.That(featNames, Contains.Item(featSelections[1].Feat));
            Assert.That(featNames.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CannotPickAFeatThatIsASpecialQuality()
        {
            var feat = new Feat();
            feat.Name = "feat1";
            specialQualities.Add(feat);

            AddFeatSelections(2);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var featNames = feats.Select(f => f.Name);
            Assert.That(featNames.Single(), Is.EqualTo("feat2"));
        }

        [Test]
        public void FeatFociAreFilled()
        {
            AddFeatSelections(1);
            featSelections[0].FocusType = "focus type";
            mockFeatFocusGenerator.Setup(g => g.GenerateFrom("feat1", "focus type", skills, featSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns("focus");

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(featSelections[0].Feat));
            Assert.That(onlyFeat.Foci.Single(), Is.EqualTo("focus"));
        }

        [Test]
        public void FeatFociAreFilledIndividually()
        {
            hitPoints.HitDice[0].Quantity = 3;
            AddFeatSelections(2);
            featSelections[0].FocusType = "focus type 1";
            featSelections[1].FocusType = "focus type 2";

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom("feat1", "focus type 1", skills, featSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns("focus 1");
            mockFeatFocusGenerator.Setup(g => g.GenerateFrom("feat2", "focus type 2", skills, featSelections[1].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns("focus 2");

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<FeatDataSelection>>()))
                .Returns(() => featSelections[index])
                .Callback(() => index++);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            Assert.That(feats.Count(), Is.EqualTo(2));

            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo(featSelections[0].Feat));
            Assert.That(first.Foci.Single(), Is.EqualTo("focus 1"));
            Assert.That(last.Name, Is.EqualTo(featSelections[1].Feat));
            Assert.That(last.Foci.Single(), Is.EqualTo("focus 2"));
        }

        [Test]
        public void FeatsWithFociCanBeFilledMoreThanOnce()
        {
            hitPoints.HitDice[0].Quantity = 3;
            AddFeatSelections(1);
            featSelections[0].FocusType = "focus type";

            mockFeatFocusGenerator.SetupSequence(g => g.GenerateFrom("feat1", "focus type", skills, featSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns("focus 1").Returns("focus 2");

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(featSelections[0].Feat));
            Assert.That(onlyFeat.Foci, Contains.Item("focus 1"));
            Assert.That(onlyFeat.Foci, Contains.Item("focus 2"));
            Assert.That(onlyFeat.Foci.Count(), Is.EqualTo(2));
        }

        [Test]
        public void FeatsThatCanBeTakenMultipleTimesWithoutFociAreAllowed()
        {
            AddFeatSelections(1);
            hitPoints.HitDice[0].Quantity = 3;
            featSelections[0].CanBeTakenMultipleTimes = true;

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var first = feats.First();
            var last = feats.Last();

            Assert.That(first.Name, Is.EqualTo("feat1"));
            Assert.That(last.Name, Is.EqualTo("feat1"));
            Assert.That(feats.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNoValidFocusGenerated_CannotSelectFeat()
        {
            AddFeatSelections(1);
            featSelections[0].FocusType = "focus type";

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(featSelections[0].Feat, "focus type", skills, featSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns(FeatConstants.Foci.NoValidFociAvailable);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void IfNoValidFocusGenerated_DoNotTryToSelectFeatAgain()
        {
            AddFeatSelections(2);
            featSelections[0].FocusType = "focus type";

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(featSelections[0].Feat, "focus type", skills, featSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns(FeatConstants.Foci.NoValidFociAvailable);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var onlyFeat = feats.Single();
            Assert.That(onlyFeat.Name, Is.EqualTo(featSelections[1].Feat));
        }

        [Test]
        public void CanHaveFeatWithoutFocus()
        {
            AddFeatSelections(1);
            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(featSelections[0].Feat, string.Empty, skills, featSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns(string.Empty);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo(featSelections[0].Feat));
            Assert.That(feat.Foci, Is.Empty);
        }

        [Test]
        public void AllDataFromFeatSelectionIsCopiedToFeat()
        {
            var selection = new FeatDataSelection();
            selection.Feat = "additional feat";
            selection.FocusType = "focus type";
            selection.Frequency.Quantity = 9266;
            selection.Frequency.TimePeriod = "frequency time period";
            selection.Power = 12345;

            featSelections.Add(selection);

            mockFeatFocusGenerator.SetupSequence(g => g.GenerateFrom("additional feat", "focus type", skills, featSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns("focus").Returns("wrong focus");

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size", false);
            var feat = feats.Single();
            Assert.That(feat.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(9266));
            Assert.That(feat.Frequency.TimePeriod, Is.EqualTo("frequency time period"));
            Assert.That(feat.Name, Is.EqualTo("additional feat"));
            Assert.That(feat.Power, Is.EqualTo(12345));
        }

        [Test]
        public void GetSpecialQualities()
        {
            var feat1 = new SpecialQualitySelection();
            feat1.Feat = "special quality 1";

            var feat2 = new SpecialQualitySelection();
            feat2.Feat = "special quality 2";
            feat2.Power = 9266;
            feat2.Frequency.Quantity = 42;
            feat2.Frequency.TimePeriod = "fortnight";

            specialQualitySelections.Add(feat1);
            specialQualitySelections.Add(feat2);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            var first = specialQualities.First();
            var last = specialQualities.Last();

            Assert.That(first.Name, Is.EqualTo("special quality 1"));
            Assert.That(first.Power, Is.Zero);
            Assert.That(first.Frequency.Quantity, Is.Zero);
            Assert.That(first.Frequency.TimePeriod, Is.Empty);

            Assert.That(last.Name, Is.EqualTo("special quality 2"));
            Assert.That(last.Power, Is.EqualTo(9266));
            Assert.That(last.Frequency.Quantity, Is.EqualTo(42));
            Assert.That(last.Frequency.TimePeriod, Is.EqualTo("fortnight"));
        }

        [Test]
        public void CanGetSpecialQualitiesWhenNoIntelligence()
        {
            abilities[AbilityConstants.Intelligence].BaseScore = 0;

            var feat1 = new SpecialQualitySelection();
            feat1.Feat = "special quality 1";

            specialQualitySelections.Add(feat1);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(specialQualities, Is.Not.Empty);

            var specialQuality = specialQualities.Single();

            Assert.That(specialQuality.Name, Is.EqualTo("special quality 1"));
            Assert.That(specialQuality.Power, Is.Zero);
            Assert.That(specialQuality.Frequency.Quantity, Is.Zero);
            Assert.That(specialQuality.Frequency.TimePeriod, Is.Empty);
        }

        [Test]
        public void DoNotGetSpecialQualityThatDoNotMeetRequirements()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "base race feat";
            specialQualitySelection.MinimumAbilities[AbilityConstants.Intelligence] = 11;
            specialQualitySelections.Add(specialQualitySelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(specialQualities, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityThatMeetRequirements()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "base race feat";
            specialQualitySelection.MinimumAbilities[AbilityConstants.Intelligence] = 11;
            specialQualitySelections.Add(specialQualitySelection);

            abilities[AbilityConstants.Intelligence].BaseScore = 11;

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));
        }

        [Test]
        public void BUG_GetSpecialQualityThatMeetRequirements_OutOfOrder()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.RequiredFeats = new[]
            {
                new RequiredFeatSelection { Feat = "required feat" }
            };
            var requirementSelection = new SpecialQualitySelection();
            requirementSelection.Feat = "required feat";
            specialQualitySelections.Add(specialQualitySelection);
            specialQualitySelections.Add(requirementSelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(specialQualities.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetFociForSpecialQualities()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.FocusType = "base focus type";
            specialQualitySelections.Add(specialQualitySelection);

            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("special quality", "base focus type", skills, abilities)).Returns("base focus");

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            var baseFeat = feats.First(f => f.Name == specialQualitySelection.Feat);

            Assert.That(baseFeat.Foci.Single(), Is.EqualTo("base focus"));
        }

        [Test]
        public void BUG_GetFociForSpecialQualities_WithZeroRandomFoci_ButPreset()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.FocusType = "my specific focus";
            specialQualitySelection.RandomFociQuantity = "0";

            specialQualitySelections.Add(specialQualitySelection);

            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("special quality", "my specific focus", skills, abilities)).Returns("my focus");
            mockDice.Setup(d => d.Roll("0").AsSum<int>()).Returns(0);

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            var baseFeat = feats.First(f => f.Name == specialQualitySelection.Feat);

            Assert.That(baseFeat.Foci.Single(), Is.EqualTo("my focus"));
        }

        [Test]
        public void DoNotGetEmptyFociStrings()
        {
            var specialQualitySelectionselection = new SpecialQualitySelection();
            specialQualitySelectionselection.Feat = "special quality";
            specialQualitySelectionselection.FocusType = string.Empty;
            specialQualitySelections.Add(specialQualitySelectionselection);

            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("special quality", string.Empty, skills, abilities)).Returns(string.Empty);

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            var baseFeat = feats.First(f => f.Name == specialQualitySelectionselection.Feat);

            Assert.That(baseFeat.Foci, Is.Empty);
        }

        [Test]
        public void DoNotAddHitDiceToPower()
        {
            var specialQualitySelectionselection = new SpecialQualitySelection();
            specialQualitySelectionselection.Feat = "different feat";
            specialQualitySelectionselection.Power = 10;
            specialQualitySelections.Add(specialQualitySelectionselection);

            hitPoints.HitDice[0].Quantity = 2;

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo("different feat"));
            Assert.That(feat.Power, Is.EqualTo(10));
        }

        [Test]
        public void GetOneRandomFociForSpecialQualityIfNoRandomFociQuantity()
        {
            var featSelection = new SpecialQualitySelection();
            featSelection.Feat = "special quality";
            featSelection.FocusType = "focus type";

            var count = 1;
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("special quality", "focus type", skills, abilities)).Returns(() => $"focus {count++}");
            mockDice.Setup(d => d.Roll("dice roll").AsSum<int>()).Returns(3);

            specialQualitySelections.Add(featSelection);

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo("special quality"));
            Assert.That(feat.Foci.Single(), Is.EqualTo("focus 1"));
        }

        [Test]
        public void GetRandomFociForSpecialQuality()
        {
            var featSelection = new SpecialQualitySelection();
            featSelection.Feat = "special quality";
            featSelection.FocusType = "focus type";
            featSelection.RandomFociQuantity = "dice roll";

            var count = 1;
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("special quality", "focus type", skills, abilities)).Returns(() => $"focus {count++}");
            mockDice.Setup(d => d.Roll("dice roll").AsSum<int>()).Returns(3);

            specialQualitySelections.Add(featSelection);

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo("special quality"));
            Assert.That(feat.Foci, Is.EquivalentTo(new[] { "focus 1", "focus 2", "focus 3" }));
        }

        [Test]
        public void GetNoDuplicateRandomFociForSpecialQuality()
        {
            var featSelection = new SpecialQualitySelection();
            featSelection.Feat = "special quality";
            featSelection.FocusType = "focus type";
            featSelection.RandomFociQuantity = "dice roll";

            var count = 1;
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("special quality", "focus type", skills, abilities)).Returns(() => $"focus {count++ / 2}");
            mockDice.Setup(d => d.Roll("dice roll").AsSum<int>()).Returns(3);

            specialQualitySelections.Add(featSelection);

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo("special quality"));
            Assert.That(feat.Foci, Is.Unique);
            Assert.That(feat.Foci, Is.EquivalentTo(new[] { "focus 0", "focus 1", "focus 2" }));
        }

        [Test]
        public void GetNoRandomFociForSpecialQualityIfNoFocusType()
        {
            var featSelection = new SpecialQualitySelection();
            featSelection.Feat = "special quality";
            featSelection.RandomFociQuantity = "dice roll";

            var count = 1;
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("special quality", "focus type", skills, abilities)).Returns(() => $"focus {count++}");
            mockDice.Setup(d => d.Roll("dice roll").AsSum<int>()).Returns(3);

            specialQualitySelections.Add(featSelection);

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo("special quality"));
            Assert.That(feat.Foci, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityRequiringCanUseEquipment()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.RequiresEquipment = true;

            specialQualitySelections.Add(specialQualitySelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, true, "size", alignment);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DoNotGetSpecialQualityRequiringCanUseEquipment()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.RequiresEquipment = true;

            specialQualitySelections.Add(specialQualitySelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(specialQualities, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityNotRequiringCanUseEquipment()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.RequiresEquipment = false;

            specialQualitySelections.Add(specialQualitySelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetSpecialQualityRequiringNoSize()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.RequiredSizes = Enumerable.Empty<string>();

            specialQualitySelections.Add(specialQualitySelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetSpecialQualityRequiringSize()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.RequiredSizes = new[] { "size" };

            specialQualitySelections.Add(specialQualitySelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DoNotGetSpecialQualityRequiringSize()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.RequiredSizes = new[] { "size" };

            specialQualitySelections.Add(specialQualitySelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "wrong size", alignment);
            Assert.That(specialQualities, Is.Empty);
        }

        //INFO: Example is Titans of different alignments have different spell-like abilities
        [Test]
        public void GetSpecialQualityRequiringAlignment()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.RequiredAlignments = new[] { "lawfulness goodness" };

            specialQualitySelections.Add(specialQualitySelection);

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));
        }

        //INFO: Example is Titans of different alignments have different spell-like abilities
        [Test]
        public void DoNotGetSpecialQualityRequiringAlignment()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.RequiredAlignments = new[] { "lawfulness goodness" };

            specialQualitySelections.Add(specialQualitySelection);

            alignment.Goodness = "wrong goodness";
            alignment.Lawfulness = "wrong lawfulness";

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "wrong size", alignment);
            Assert.That(specialQualities, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityWithSave()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.SaveAbility = "save ability";
            specialQualitySelection.Save = "save";
            specialQualitySelection.SaveBaseValue = 9266;

            abilities["save ability"] = new Ability("save ability");

            specialQualitySelections.Add(specialQualitySelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();
            Assert.That(specialQuality.Save, Is.Not.Null);
            Assert.That(specialQuality.Save.BaseAbility, Is.EqualTo(abilities["save ability"]));
            Assert.That(specialQuality.Save.Save, Is.EqualTo("save"));
            Assert.That(specialQuality.Save.BaseValue, Is.EqualTo(9266));
        }

        [Test]
        public void GetSpecialQualityWithoutSave()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.SaveAbility = string.Empty;
            specialQualitySelection.Save = "save";
            specialQualitySelection.SaveBaseValue = 9266;

            abilities["save ability"] = new Ability("save ability");

            specialQualitySelections.Add(specialQualitySelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));

            var specialQuality = specialQualities.Single();
            Assert.That(specialQuality.Save, Is.Null);
        }

        [Test]
        public void BUG_HalfOrcIsNotSensitiveToLight()
        {
            var feat1 = new SpecialQualitySelection();
            feat1.Feat = "special quality 1";

            var lightSensitivity = new SpecialQualitySelection();
            lightSensitivity.Feat = FeatConstants.SpecialQualities.LightSensitivity;

            var feat2 = new SpecialQualitySelection();
            feat2.Feat = "special quality 2";
            feat2.Power = 9266;
            feat2.Frequency.Quantity = 42;
            feat2.Frequency.TimePeriod = "fortnight";

            specialQualitySelections.Add(feat1);
            specialQualitySelections.Add(lightSensitivity);
            specialQualitySelections.Add(feat2);

            mockFeatsSelector.Setup(s => s.SelectSpecialQualities(CreatureConstants.Orc_Half, creatureType)).Returns(specialQualitySelections);

            var specialQualities = featsGenerator.GenerateSpecialQualities(CreatureConstants.Orc_Half, creatureType, hitPoints, abilities, skills, false, "size", alignment);
            Assert.That(specialQualities.Count(), Is.EqualTo(2));
            Assert.That(specialQualities.First().Name, Is.EqualTo("special quality 1"));
            Assert.That(specialQualities.Last().Name, Is.EqualTo("special quality 2"));
        }

        [TestCase(FeatConstants.SpecialQualities.Blind)]
        public void BUG_IfBlind_RemoveSightFeats(string blindFeatName)
        {
            var feat1 = new SpecialQualitySelection();
            feat1.Feat = "special quality 1";

            var blindFeat = new SpecialQualitySelection();
            blindFeat.Feat = blindFeatName;

            var feat2 = new SpecialQualitySelection();
            feat2.Feat = "special quality 2";
            feat2.Power = 9266;
            feat2.Frequency.Quantity = 42;
            feat2.Frequency.TimePeriod = "fortnight";

            var sightedFeats = new[]
            {
                new SpecialQualitySelection { Feat = FeatConstants.SpecialQualities.AllAroundVision },
                new SpecialQualitySelection { Feat = FeatConstants.SpecialQualities.Darkvision },
                new SpecialQualitySelection { Feat = FeatConstants.SpecialQualities.LowLightVision },
                new SpecialQualitySelection { Feat = FeatConstants.SpecialQualities.LowLightVision_Superior },
            };

            specialQualitySelections.Add(feat1);
            specialQualitySelections.AddRange(sightedFeats);
            specialQualitySelections.Add(blindFeat);
            specialQualitySelections.Add(feat2);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false, "size", alignment);
            var array = specialQualities.ToArray();
            Assert.That(array, Has.Length.EqualTo(3));
            Assert.That(array[0].Name, Is.EqualTo("special quality 1"));
            Assert.That(array[1].Name, Is.EqualTo(blindFeatName));
            Assert.That(array[2].Name, Is.EqualTo("special quality 2"));
        }
    }
}