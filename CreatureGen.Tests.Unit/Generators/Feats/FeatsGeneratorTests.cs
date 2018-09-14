using CreatureGen.Abilities;
using CreatureGen.Attacks;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Generators.Feats;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Feats
{
    [TestFixture]
    public class FeatsGeneratorTests
    {
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IFeatsSelector> mockFeatsSelector;
        private Mock<IFeatFocusGenerator> mockFeatFocusGenerator;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private IFeatsGenerator featsGenerator;
        private Dictionary<string, Ability> abilities;
        private Dictionary<string, Measurement> speeds;
        private List<Skill> skills;
        private List<FeatSelection> featSelections;
        private List<FeatSelection> skillSynergyFeatSelections;
        private List<SpecialQualitySelection> specialQualitySelections;
        private Mock<Dice> mockDice;
        private HitPoints hitPoints;
        private List<Attack> attacks;
        private List<Feat> specialQualities;
        private CreatureType creatureType;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockFeatsSelector = new Mock<IFeatsSelector>();
            mockFeatFocusGenerator = new Mock<IFeatFocusGenerator>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockDice = new Mock<Dice>();
            featsGenerator = new FeatsGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object, mockFeatsSelector.Object, mockFeatFocusGenerator.Object, mockDice.Object);

            abilities = new Dictionary<string, Ability>();
            skills = new List<Skill>();
            featSelections = new List<FeatSelection>();
            specialQualitySelections = new List<SpecialQualitySelection>();
            hitPoints = new HitPoints();
            attacks = new List<Attack>();
            specialQualities = new List<Feat>();
            speeds = new Dictionary<string, Measurement>();
            creatureType = new CreatureType();
            skillSynergyFeatSelections = new List<FeatSelection>();

            hitPoints.HitDiceQuantity = 1;
            creatureType.Name = "creature type";

            abilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);

            mockFeatsSelector.Setup(s => s.SelectFeats()).Returns(featSelections);
            mockFeatsSelector.Setup(s => s.SelectSkillSynergies()).Returns(skillSynergyFeatSelections);
            mockFeatsSelector.Setup(s => s.SelectSpecialQualities("creature", creatureType)).Returns(specialQualitySelections);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<FeatSelection>>())).Returns((IEnumerable<FeatSelection> fs) => fs.First());
        }

        [Test]
        public void CreaturesWithoutIntelligenceReceiveNoFeats()
        {
            abilities[AbilityConstants.Intelligence].BaseScore = 0;
            hitPoints.HitDiceQuantity = 20;
            AddFeatSelections(8);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            Assert.That(feats, Is.Empty);
        }

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
            hitPoints.HitDiceQuantity = hitDiceQuantity;
            AddFeatSelections(numberOfFeats + 2);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            var featNames = feats.Select(f => f.Name);

            for (var i = 0; i < numberOfFeats; i++)
                Assert.That(featNames, Contains.Item(featSelections[i].Feat));

            Assert.That(featNames.Count(), Is.EqualTo(numberOfFeats));
        }

        private void AddFeatSelections(int quantity)
        {
            for (var i = 0; i < quantity; i++)
            {
                var selection = new FeatSelection();
                selection.Feat = $"feat{i + 1}";

                featSelections.Add(selection);
            }
        }

        [Test]
        public void DoNotGetFeatIfNoneAvailable()
        {
            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetMoreFeatsIfNoneAvailable()
        {
            AddFeatSelections(1);
            featSelections[0].RequiredBaseAttack = 90210;

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void FeatsPickedAtRandom()
        {
            hitPoints.HitDiceQuantity = 3;
            AddFeatSelections(3);

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<FeatSelection>>(fs => fs.Count() == 3)))
                .Returns((IEnumerable<FeatSelection> fs) => fs.Last());
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<FeatSelection>>(fs => fs.Count() == 2)))
                .Returns((IEnumerable<FeatSelection> fs) => fs.First());

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
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

            var feats = featsGenerator.GenerateFeats(hitPoints, 9265, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[1].Feat));
            Assert.That(featNames.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetFeatsWithMetPrerequisite()
        {
            AddFeatSelections(2);
            featSelections[0].RequiredBaseAttack = 9266;

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames, Contains.Item(featSelections[0].Feat));
            Assert.That(featNames.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ReassessPrerequisitesEveryFeat()
        {
            hitPoints.HitDiceQuantity = 3;
            AddFeatSelections(3);
            featSelections[1].RequiredFeats = new[] { new RequiredFeatSelection { Feat = featSelections[0].Feat } };

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<FeatSelection>>())).Returns((IEnumerable<FeatSelection> fs) => fs.ElementAt(index++ % fs.Count()));

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
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

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
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

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo(featSelections[0].Feat));
            Assert.That(onlyFeat.Foci.Single(), Is.EqualTo("focus"));
        }

        [Test]
        public void FeatFociAreFilledIndividually()
        {
            hitPoints.HitDiceQuantity = 3;
            AddFeatSelections(2);
            featSelections[0].FocusType = "focus type 1";
            featSelections[1].FocusType = "focus type 2";

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom("feat1", "focus type 1", skills, featSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns("focus 1");
            mockFeatFocusGenerator.Setup(g => g.GenerateFrom("feat2", "focus type 2", skills, featSelections[1].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns("focus 2");

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<FeatSelection>>()))
                .Returns(() => featSelections[index])
                .Callback(() => index++);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
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
            hitPoints.HitDiceQuantity = 3;
            AddFeatSelections(1);
            featSelections[0].FocusType = "focus type";

            mockFeatFocusGenerator.SetupSequence(g => g.GenerateFrom("feat1", "focus type", skills, featSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns("focus 1").Returns("focus 2");

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
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
            hitPoints.HitDiceQuantity = 3;
            featSelections[0].CanBeTakenMultipleTimes = true;

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
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

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void IfNoValidFocusGenerated_DoNotTryToSelectFeatAgain()
        {
            AddFeatSelections(2);
            featSelections[0].FocusType = "focus type";

            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(featSelections[0].Feat, "focus type", skills, featSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns(FeatConstants.Foci.NoValidFociAvailable);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            var onlyFeat = feats.Single();
            Assert.That(onlyFeat.Name, Is.EqualTo(featSelections[1].Feat));
        }

        [Test]
        public void CanHaveFeatWithoutFocus()
        {
            AddFeatSelections(1);
            mockFeatFocusGenerator.Setup(g => g.GenerateFrom(featSelections[0].Feat, string.Empty, skills, featSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns(string.Empty);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo(featSelections[0].Feat));
            Assert.That(feat.Foci, Is.Empty);
        }

        [Test]
        public void AllDataFromFeatSelectionIsCopiedToFeat()
        {
            var selection = new FeatSelection();
            selection.Feat = "additional feat";
            selection.FocusType = "focus type";
            selection.Frequency.Quantity = 9266;
            selection.Frequency.TimePeriod = "frequency time period";
            selection.Power = 12345;

            featSelections.Add(selection);

            mockFeatFocusGenerator.SetupSequence(g => g.GenerateFrom("additional feat", "focus type", skills, featSelections[0].RequiredFeats, It.IsAny<IEnumerable<Feat>>(), 90210, abilities, attacks))
                .Returns("focus").Returns("wrong focus");

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            var feat = feats.Single();
            Assert.That(feat.Foci.Single(), Is.EqualTo("focus"));
            Assert.That(feat.Frequency.Quantity, Is.EqualTo(9266));
            Assert.That(feat.Frequency.TimePeriod, Is.EqualTo("frequency time period"));
            Assert.That(feat.Name, Is.EqualTo("additional feat"));
            Assert.That(feat.Power, Is.EqualTo(12345));
        }

        [Test]
        public void GetNoSkillSynergyBonusWithFeats()
        {
            var selection = new FeatSelection();
            selection.Feat = "synergy feat";
            selection.FocusType = "focus type";
            selection.Power = 12345;
            selection.RequiredSkills = new[]
            {
                new RequiredSkillSelection { Skill = "source skill", Ranks = 5 },
                new RequiredSkillSelection { Skill = "target skill", Ranks = 0 }
            };

            skillSynergyFeatSelections.Add(selection);

            mockFeatFocusGenerator.Setup(g => g.FocusTypeIsPreset("focus type")).Returns(true);

            skills.Add(new Skill("source skill", abilities[AbilityConstants.Intelligence], 10));
            skills[0].Ranks = 5;
            skills[0].ClassSkill = true;

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetSkillSynergyBonusWithFeats()
        {
            var selection = new FeatSelection();
            selection.Feat = "synergy feat";
            selection.FocusType = "focus type";
            selection.Power = 12345;
            selection.RequiredSkills = new[]
            {
                new RequiredSkillSelection { Skill = "source skill", Ranks = 5 },
                new RequiredSkillSelection { Skill = "target skill", Ranks = 0 }
            };

            skillSynergyFeatSelections.Add(selection);

            mockFeatFocusGenerator.Setup(g => g.FocusTypeIsPreset("focus type")).Returns(true);

            skills.Add(new Skill("source skill", abilities[AbilityConstants.Intelligence], 10));
            skills[0].Ranks = 5;
            skills[0].ClassSkill = true;
            skills.Add(new Skill("target skill", abilities[AbilityConstants.Intelligence], 10));

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames.Count(), Is.EqualTo(1));
            Assert.That(featNames.Single(), Is.EqualTo("synergy feat"));
        }

        [TestCase(5, true)]
        [TestCase(6, true)]
        [TestCase(42, true)]
        [TestCase(10, false)]
        [TestCase(11, false)]
        [TestCase(42, false)]
        public void GetSkillSynergyBonusWithSufficientRanksWithFeats(int ranks, bool classSkill)
        {
            var selection = new FeatSelection();
            selection.Feat = "synergy feat";
            selection.FocusType = "focus type";
            selection.Power = 12345;
            selection.RequiredSkills = new[]
            {
                new RequiredSkillSelection { Skill = "source skill", Ranks = 5 },
                new RequiredSkillSelection { Skill = "target skill", Ranks = 0 }
            };

            skills.Add(new Skill("source skill", abilities[AbilityConstants.Intelligence], 96));
            skills[0].Ranks = ranks;
            skills[0].ClassSkill = classSkill;
            skills.Add(new Skill("target skill", abilities[AbilityConstants.Intelligence], 96));

            skillSynergyFeatSelections.Add(selection);

            mockFeatFocusGenerator.Setup(g => g.FocusTypeIsPreset("focus type")).Returns(true);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames.Count(), Is.EqualTo(1));
            Assert.That(featNames.Single(), Is.EqualTo("synergy feat"));
        }

        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, true)]
        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, false)]
        [TestCase(3, false)]
        [TestCase(4, false)]
        [TestCase(5, false)]
        [TestCase(6, false)]
        [TestCase(7, false)]
        [TestCase(8, false)]
        [TestCase(9, false)]
        public void DoNotGetSkillSynergyBonusWithInsufficientRanksWithFeats(int ranks, bool classSkill)
        {
            var selection = new FeatSelection();
            selection.Feat = "synergy feat";
            selection.FocusType = "focus type";
            selection.Power = 12345;
            selection.RequiredSkills = new[]
            {
                new RequiredSkillSelection { Skill = "source skill", Ranks = 5 },
                new RequiredSkillSelection { Skill = "target skill", Ranks = 0 }
            };

            skills.Add(new Skill("source skill", abilities[AbilityConstants.Intelligence], 10));
            skills[0].Ranks = ranks;
            skills[0].ClassSkill = classSkill;
            skills.Add(new Skill("target skill", abilities[AbilityConstants.Intelligence], 10));

            skillSynergyFeatSelections.Add(selection);

            mockFeatFocusGenerator.Setup(g => g.FocusTypeIsPreset("focus type")).Returns(true);

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetSkillSynergyBonusWithoutSourceSkillWithFeats()
        {
            var selection = new FeatSelection();
            selection.Feat = "synergy feat";
            selection.FocusType = "focus type";
            selection.Power = 12345;
            selection.RequiredSkills = new[]
            {
                new RequiredSkillSelection { Skill = "source skill", Ranks = 5 },
                new RequiredSkillSelection { Skill = "target skill", Ranks = 0 }
            };

            skillSynergyFeatSelections.Add(selection);

            mockFeatFocusGenerator.Setup(g => g.FocusTypeIsPreset("focus type")).Returns(true);

            skills.Add(new Skill("target skill", abilities[AbilityConstants.Intelligence], 10));

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void DoNotGetSkillSynergyBonusWithoutTargetSkillWithFeats()
        {
            var selection = new FeatSelection();
            selection.Feat = "synergy feat";
            selection.FocusType = "focus type";
            selection.Power = 12345;
            selection.RequiredSkills = new[]
            {
                new RequiredSkillSelection { Skill = "source skill", Ranks = 5 },
                new RequiredSkillSelection { Skill = "target skill", Ranks = 0 }
            };

            skillSynergyFeatSelections.Add(selection);

            mockFeatFocusGenerator.Setup(g => g.FocusTypeIsPreset("focus type")).Returns(true);

            skills.Add(new Skill("source skill", abilities[AbilityConstants.Intelligence], 10));
            skills[0].Ranks = 5;
            skills[0].ClassSkill = true;

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            Assert.That(feats, Is.Empty);
        }

        [Test]
        public void GetSkillSynergyBonusesWithFeats()
        {
            var selection = new FeatSelection();
            selection.Feat = "synergy feat";
            selection.FocusType = "focus type";
            selection.Power = 12345;
            selection.RequiredSkills = new[]
            {
                new RequiredSkillSelection { Skill = "source skill", Ranks = 5 },
                new RequiredSkillSelection { Skill = "target skill", Ranks = 0 }
            };

            var otherSelection = new FeatSelection();
            otherSelection.Feat = "other synergy feat";
            otherSelection.FocusType = "focus type";
            otherSelection.Power = 12345;
            otherSelection.RequiredSkills = new[]
            {
                new RequiredSkillSelection { Skill = "other source skill", Ranks = 5 },
                new RequiredSkillSelection { Skill = "other target skill", Ranks = 0 }
            };

            skillSynergyFeatSelections.Add(selection);
            skillSynergyFeatSelections.Add(otherSelection);

            mockFeatFocusGenerator.Setup(g => g.FocusTypeIsPreset("focus type")).Returns(true);

            skills.Add(new Skill("source skill", abilities[AbilityConstants.Intelligence], 10));
            skills[0].Ranks = 5;
            skills[0].ClassSkill = true;
            skills.Add(new Skill("target skill", abilities[AbilityConstants.Intelligence], 10));
            skills.Add(new Skill("other source skill", abilities[AbilityConstants.Intelligence], 10));
            skills[2].Ranks = 5;
            skills[2].ClassSkill = true;
            skills.Add(new Skill("other target skill", abilities[AbilityConstants.Intelligence], 10));

            var feats = featsGenerator.GenerateFeats(hitPoints, 9266, abilities, skills, attacks, specialQualities, 90210, speeds, 600, 1337, "size");
            var featNames = feats.Select(f => f.Name);

            Assert.That(featNames.Count(), Is.EqualTo(2));
            Assert.That(featNames.First(), Is.EqualTo("synergy feat"));
            Assert.That(featNames.Last(), Is.EqualTo("other synergy feat"));
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

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
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

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
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

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
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

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetFociForSpecialQualities()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "racial feat";
            specialQualitySelection.FocusType = "base focus type";
            specialQualitySelections.Add(specialQualitySelection);

            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("racial feat", "base focus type", skills, abilities)).Returns("base focus");

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
            var baseFeat = feats.First(f => f.Name == specialQualitySelection.Feat);

            Assert.That(baseFeat.Foci.Single(), Is.EqualTo("base focus"));
        }

        [Test]
        public void DoNotGetEmptyFociStrings()
        {
            var specialQualitySelectionselection = new SpecialQualitySelection();
            specialQualitySelectionselection.Feat = "racial feat";
            specialQualitySelectionselection.FocusType = string.Empty;
            specialQualitySelections.Add(specialQualitySelectionselection);

            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("racial feat", string.Empty, skills, abilities)).Returns(string.Empty);

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
            var baseFeat = feats.First(f => f.Name == specialQualitySelectionselection.Feat);

            Assert.That(baseFeat.Foci, Is.Empty);
        }

        [Test]
        public void AddHitDiceToPower()
        {
            var metaraceFeat = new SpecialQualitySelection();
            metaraceFeat.Feat = "metarace feat";
            metaraceFeat.Power = 10;
            specialQualitySelections.Add(metaraceFeat);

            hitPoints.HitDiceQuantity = 2;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.AddHitDiceToPower)).Returns(new[] { metaraceFeat.Feat });

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
            var onlyFeat = feats.Single();

            Assert.That(onlyFeat.Name, Is.EqualTo("metarace feat"));
            Assert.That(onlyFeat.Power, Is.EqualTo(12));
        }

        [Test]
        public void DoNotAddHitDiceToPower()
        {
            var specialQualitySelectionselection = new SpecialQualitySelection();
            specialQualitySelectionselection.Feat = "different feat";
            specialQualitySelectionselection.Power = 10;
            specialQualitySelections.Add(specialQualitySelectionselection);

            hitPoints.HitDiceQuantity = 2;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.FeatGroups, GroupConstants.AddHitDiceToPower)).Returns(new[] { "other feat" });

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo("different feat"));
            Assert.That(feat.Power, Is.EqualTo(10));
        }

        [Test]
        public void GetOneRandomFociForSpecialQualityIfNoRandomFociQuantity()
        {
            var featSelection = new SpecialQualitySelection();
            featSelection.Feat = "racial feat";
            featSelection.FocusType = "focus type";

            var count = 1;
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("racial feat", "focus type", skills, abilities)).Returns(() => $"focus {count++}");
            mockDice.Setup(d => d.Roll("dice roll").AsSum()).Returns(3);

            specialQualitySelections.Add(featSelection);

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo("racial feat"));
            Assert.That(feat.Foci.Single(), Is.EqualTo("focus 1"));
        }

        [Test]
        public void GetRandomFociForSpecialQuality()
        {
            var featSelection = new SpecialQualitySelection();
            featSelection.Feat = "racial feat";
            featSelection.FocusType = "focus type";
            featSelection.RandomFociQuantity = "dice roll";

            var count = 1;
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("racial feat", "focus type", skills, abilities)).Returns(() => $"focus {count++}");
            mockDice.Setup(d => d.Roll("dice roll").AsSum()).Returns(3);

            specialQualitySelections.Add(featSelection);

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo("racial feat"));
            Assert.That(feat.Foci, Is.EquivalentTo(new[] { "focus 1", "focus 2", "focus 3" }));
        }

        [Test]
        public void GetNoDuplicateRandomFociForSpecialQuality()
        {
            var featSelection = new SpecialQualitySelection();
            featSelection.Feat = "racial feat";
            featSelection.FocusType = "focus type";
            featSelection.RandomFociQuantity = "dice roll";

            var count = 1;
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("racial feat", "focus type", skills, abilities)).Returns(() => $"focus {count++ / 2}");
            mockDice.Setup(d => d.Roll("dice roll").AsSum()).Returns(3);

            specialQualitySelections.Add(featSelection);

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo("racial feat"));
            Assert.That(feat.Foci, Is.Unique);
            Assert.That(feat.Foci, Is.EquivalentTo(new[] { "focus 0", "focus 1", "focus 2" }));
        }

        [Test]
        public void GetNoRandomFociForSpecialQualityIfNoFocusType()
        {
            var featSelection = new SpecialQualitySelection();
            featSelection.Feat = "racial feat";
            featSelection.RandomFociQuantity = "dice roll";

            var count = 1;
            mockFeatFocusGenerator.Setup(g => g.GenerateAllowingFocusOfAllFrom("racial feat", "focus type", skills, abilities)).Returns(() => $"focus {count++}");
            mockDice.Setup(d => d.Roll("dice roll").AsSum()).Returns(3);

            specialQualitySelections.Add(featSelection);

            var feats = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
            var feat = feats.Single();

            Assert.That(feat.Name, Is.EqualTo("racial feat"));
            Assert.That(feat.Foci, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityRequiringCanUseEquipment()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.RequiresEquipment = true;

            specialQualitySelections.Add(specialQualitySelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, true);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DoNotGetSpecialQualityRequiringCanUseEquipment()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.RequiresEquipment = true;

            specialQualitySelections.Add(specialQualitySelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
            Assert.That(specialQualities, Is.Empty);
        }

        [Test]
        public void GetSpecialQualityNotRequiringCanUseEquipment()
        {
            var specialQualitySelection = new SpecialQualitySelection();
            specialQualitySelection.Feat = "special quality";
            specialQualitySelection.RequiresEquipment = false;

            specialQualitySelections.Add(specialQualitySelection);

            var specialQualities = featsGenerator.GenerateSpecialQualities("creature", creatureType, hitPoints, abilities, skills, false);
            Assert.That(specialQualities.Count(), Is.EqualTo(1));
        }
    }
}