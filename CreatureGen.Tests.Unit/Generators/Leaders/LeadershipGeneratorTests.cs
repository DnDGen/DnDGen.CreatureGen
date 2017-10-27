using CreatureGen.Alignments;
using CreatureGen.Creatures;
using CreatureGen.Domain.Generators;
using CreatureGen.Domain.Selectors.Collections;
using CreatureGen.Domain.Tables;
using CreatureGen.Leaders;
using CreatureGen.Randomizers.Abilities;
using CreatureGen.Randomizers.Alignments;
using CreatureGen.Randomizers.CharacterClasses;
using CreatureGen.Randomizers.Races;
using DnDGen.Core.Generators;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Unit.Generators.Leaders
{
    [TestFixture]
    public class LeadershipGeneratorTests
    {
        private ILeadershipGenerator leadershipGenerator;
        private Mock<ICharacterGenerator> mockCharacterGenerator;
        private Mock<ILeadershipSelector> mockLeadershipSelector;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ISetLevelRandomizer> mockSetLevelRandomizer;
        private Mock<ISetAlignmentRandomizer> mockSetAlignmentRandomizer;
        private Mock<IClassNameRandomizer> mockAnyPlayerClassNameRandomizer;
        private Mock<RaceRandomizer> mockAnyBaseRaceRandomizer;
        private Mock<RaceRandomizer> mockAnyMetaraceRandomizer;
        private Mock<IAbilitiesRandomizer> mockRawAbilityRandomizer;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IClassNameRandomizer> mockAnyNPCClassNameRandomizer;
        private Mock<JustInTimeFactory> mockJustInTimeFactory;
        private List<string> allowedAlignments;
        private string leaderAlignment;
        private FollowerQuantities followerQuantities;
        private List<string> npcClasses;

        [SetUp]
        public void Setup()
        {
            mockCharacterGenerator = new Mock<ICharacterGenerator>();
            mockLeadershipSelector = new Mock<ILeadershipSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockSetLevelRandomizer = new Mock<ISetLevelRandomizer>();
            mockSetAlignmentRandomizer = new Mock<ISetAlignmentRandomizer>();
            mockAnyPlayerClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockAnyBaseRaceRandomizer = new Mock<RaceRandomizer>();
            mockAnyMetaraceRandomizer = new Mock<RaceRandomizer>();
            mockRawAbilityRandomizer = new Mock<IAbilitiesRandomizer>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            var generator = new ConfigurableIterationGenerator(2);
            mockAnyNPCClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockJustInTimeFactory = new Mock<JustInTimeFactory>();
            leadershipGenerator = new LeadershipGenerator(mockCharacterGenerator.Object,
                mockLeadershipSelector.Object,
                mockPercentileSelector.Object,
                mockAdjustmentsSelector.Object,
                mockCollectionsSelector.Object,
                generator,
                mockJustInTimeFactory.Object);

            allowedAlignments = new List<string>();
            followerQuantities = new FollowerQuantities();
            npcClasses = new List<string>();

            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(It.IsAny<int>())).Returns(new FollowerQuantities());
            mockSetLevelRandomizer.SetupAllProperties();
            mockSetAlignmentRandomizer.SetupAllProperties();
            leaderAlignment = "leader alignment";
            allowedAlignments.Add(leaderAlignment);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AlignmentGroups, leaderAlignment))
                .Returns(allowedAlignments);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs)).Returns(npcClasses);

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(TableNameConstants.Set.Collection.AlignmentGroups, leaderAlignment)).Returns(() => allowedAlignments[index++ % allowedAlignments.Count]);

            mockJustInTimeFactory.Setup(f => f.Build<ISetAlignmentRandomizer>()).Returns(mockSetAlignmentRandomizer.Object);
            mockJustInTimeFactory.Setup(f => f.Build<ISetLevelRandomizer>()).Returns(mockSetLevelRandomizer.Object);
            mockJustInTimeFactory.Setup(f => f.Build<RaceRandomizer>(RaceRandomizerTypeConstants.BaseRace.AnyBase)).Returns(mockAnyBaseRaceRandomizer.Object);
            mockJustInTimeFactory.Setup(f => f.Build<RaceRandomizer>(RaceRandomizerTypeConstants.Metarace.AnyMeta)).Returns(mockAnyMetaraceRandomizer.Object);
            mockJustInTimeFactory.Setup(f => f.Build<IAbilitiesRandomizer>(AbilitiesRandomizerTypeConstants.Raw)).Returns(mockRawAbilityRandomizer.Object);
            mockJustInTimeFactory.Setup(f => f.Build<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyNPC)).Returns(mockAnyNPCClassNameRandomizer.Object);
            mockJustInTimeFactory.Setup(f => f.Build<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer)).Returns(mockAnyPlayerClassNameRandomizer.Object);
        }

        [Test]
        public void LeadershipScoreIsLevelPlusCharismaModifier()
        {
            var leadership = leadershipGenerator.GenerateLeadership(9266, 90210, string.Empty);
            Assert.That(leadership.Score, Is.EqualTo(99476));
            Assert.That(leadership.CohortScore, Is.EqualTo(99476));
        }

        [Test]
        public void CharacterReputationGenerated()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.Reputation)).Returns("reputable");

            var reputationAjustments = new Dictionary<string, int>();
            reputationAjustments["reputable"] = 0;
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Adjustments.LeadershipModifiers)).Returns(reputationAjustments);

            var leadership = leadershipGenerator.GenerateLeadership(9266, 90210, string.Empty);
            Assert.That(leadership.LeadershipModifiers, Contains.Item("reputable"));
        }

        [Test]
        public void CharacterReputationAdjustmentIsApplied()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.Reputation)).Returns("reputable");

            var reputationAjustments = new Dictionary<string, int>();
            reputationAjustments["reputable"] = 42;
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Adjustments.LeadershipModifiers)).Returns(reputationAjustments);

            var leadership = leadershipGenerator.GenerateLeadership(9266, 90210, string.Empty);
            Assert.That(leadership.LeadershipModifiers, Contains.Item("reputable"));
            Assert.That(leadership.Score, Is.EqualTo(99518));
            Assert.That(leadership.CohortScore, Is.EqualTo(99518));
        }

        [Test]
        public void NegativeCharacterReputationAdjustmentIsApplied()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.Reputation)).Returns("reputable");

            var reputationAjustments = new Dictionary<string, int>();
            reputationAjustments["reputable"] = -42;
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Adjustments.LeadershipModifiers)).Returns(reputationAjustments);

            var leadership = leadershipGenerator.GenerateLeadership(9266, 90210, string.Empty);
            Assert.That(leadership.LeadershipModifiers, Contains.Item("reputable"));
            Assert.That(leadership.Score, Is.EqualTo(99434));
            Assert.That(leadership.CohortScore, Is.EqualTo(99434));
        }

        [Test]
        public void AnimalsDecreaseCohortScoreBy2()
        {
            var leadership = leadershipGenerator.GenerateLeadership(9266, 90210, "animal");
            Assert.That(leadership.Score, Is.EqualTo(99476));
            Assert.That(leadership.CohortScore, Is.EqualTo(99474));
        }

        [Test]
        public void KillingCohortsDecreasesScoreOfAttractingCohorts()
        {
            mockPercentileSelector.SetupSequence(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.KilledCohort)).Returns(true).Returns(false);

            var leadership = leadershipGenerator.GenerateLeadership(9266, 90210, string.Empty);
            Assert.That(leadership.Score, Is.EqualTo(99476));
            Assert.That(leadership.CohortScore, Is.EqualTo(99474));
            Assert.That(leadership.LeadershipModifiers, Contains.Item("Caused the death of 1 cohort(s)"));
        }

        [Test]
        public void KillingMultipleCohortsDecreasesScoreOfAttractingCohorts()
        {
            mockPercentileSelector.SetupSequence(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.KilledCohort)).Returns(true).Returns(true).Returns(false);

            var leadership = leadershipGenerator.GenerateLeadership(9266, 90210, string.Empty);
            Assert.That(leadership.Score, Is.EqualTo(99476));
            Assert.That(leadership.CohortScore, Is.EqualTo(99472));
            Assert.That(leadership.LeadershipModifiers, Contains.Item("Caused the death of 2 cohort(s)"));
        }

        [Test]
        public void GetFollowerQuantities()
        {
            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(99476)).Returns(followerQuantities);
            var leadership = leadershipGenerator.GenerateLeadership(9266, 90210, string.Empty);
            Assert.That(leadership.FollowerQuantities, Is.EqualTo(followerQuantities));
        }

        [Test]
        public void GenerateLeadershipMovementFactorsAndApplyThem()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.LeadershipMovement)).Returns("moves");

            var leadershipAdjustments = new Dictionary<string, int>();
            leadershipAdjustments["moves"] = 42;
            leadershipAdjustments["murders"] = -5;

            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Adjustments.LeadershipModifiers)).Returns(leadershipAdjustments);
            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(99518)).Returns(followerQuantities);

            var leadership = leadershipGenerator.GenerateLeadership(9266, 90210, string.Empty);
            Assert.That(leadership.Score, Is.EqualTo(99476));
            Assert.That(leadership.CohortScore, Is.EqualTo(99476));
            Assert.That(leadership.FollowerQuantities, Is.EqualTo(followerQuantities));
        }

        [Test]
        public void CharacterDoesNotHaveEmptyStringLeadershipModifiers()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Percentile.LeadershipMovement)).Returns(string.Empty);
            var leadership = leadershipGenerator.GenerateLeadership(9266, 90210, string.Empty);
            Assert.That(leadership.LeadershipModifiers, Is.Empty);
        }

        [Test]
        public void GenerateWhetherCharacterHasCausedFollowerDeathsAndApply()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.KilledFollowers)).Returns(true);
            mockLeadershipSelector.Setup(s => s.SelectFollowerQuantitiesFor(99475)).Returns(followerQuantities);

            var leadership = leadershipGenerator.GenerateLeadership(9266, 90210, string.Empty);
            Assert.That(leadership.Score, Is.EqualTo(99476));
            Assert.That(leadership.CohortScore, Is.EqualTo(99476));
            Assert.That(leadership.FollowerQuantities, Is.EqualTo(followerQuantities));
        }

        [Test]
        public void GenerateCohort()
        {
            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(9266)).Returns(42);

            var cohort = new Character();
            mockCharacterGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object, mockAnyPlayerClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object, mockRawAbilityRandomizer.Object))
                .Returns(cohort);

            var generatedCohort = leadershipGenerator.GenerateCohort(9266, 90210, leaderAlignment, "class name");
            Assert.That(generatedCohort, Is.EqualTo(cohort));
            mockSetLevelRandomizer.VerifySet(r => r.SetLevel = 42);
        }

        [Test]
        public void GenerateNPCCohort()
        {
            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(9266)).Returns(42);
            npcClasses.Add("class name");

            var cohort = new Character();
            mockCharacterGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object, mockAnyNPCClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object, mockRawAbilityRandomizer.Object))
                .Returns(cohort);

            var generatedCohort = leadershipGenerator.GenerateCohort(9266, 90210, leaderAlignment, "class name");
            Assert.That(generatedCohort, Is.EqualTo(cohort));
            mockSetLevelRandomizer.VerifySet(r => r.SetLevel = 42);
        }

        [Test]
        public void CohortLevelIs2LessThanLeaderLevel()
        {
            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(9266)).Returns(90210);

            var cohort = new Character();
            mockCharacterGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object, mockAnyPlayerClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object, mockRawAbilityRandomizer.Object))
                .Returns(cohort);

            var generatedCohort = leadershipGenerator.GenerateCohort(9266, 42, leaderAlignment, "class name");
            Assert.That(generatedCohort, Is.EqualTo(cohort));
            mockSetLevelRandomizer.VerifySet(r => r.SetLevel = 40);
        }

        [Test]
        public void AttractCohortOfSameAlignment()
        {
            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(9266)).Returns(42);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AttractCohortOfDifferentAlignment)).Returns(false);

            var cohortAlignment = new Alignment("cohort alignment");
            var leadersAlignment = new Alignment(leaderAlignment);
            allowedAlignments.Add(cohortAlignment.ToString());

            var cohort = new Character();
            mockCharacterGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object, mockAnyPlayerClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object, mockRawAbilityRandomizer.Object))
                .Returns(cohort);

            var generatedCohort = leadershipGenerator.GenerateCohort(9266, 90210, leaderAlignment, "class name");
            Assert.That(generatedCohort, Is.EqualTo(cohort));
            mockSetAlignmentRandomizer.VerifySet(r => r.SetAlignment = leadersAlignment);
        }

        [Test]
        public void AttractCohortOfDifferingAlignment()
        {
            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(9265)).Returns(42);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.AttractCohortOfDifferentAlignment)).Returns(true);

            var cohortAlignment = new Alignment("cohort alignment");
            allowedAlignments.Add(cohortAlignment.ToString());

            var cohort = new Character();
            mockCharacterGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object, mockAnyPlayerClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object, mockRawAbilityRandomizer.Object))
                .Returns(cohort);

            var generatedCohort = leadershipGenerator.GenerateCohort(9266, 90210, leaderAlignment, "class name");
            Assert.That(generatedCohort, Is.EqualTo(cohort));
            mockSetAlignmentRandomizer.VerifySet(r => r.SetAlignment = cohortAlignment);
        }

        [Test]
        public void IfSelectedCohortLevelIs0_DoNotGenerateCohort()
        {
            mockLeadershipSelector.Setup(s => s.SelectCohortLevelFor(9266)).Returns(0);

            var cohort = new Character();
            mockCharacterGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object, mockAnyPlayerClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object, mockRawAbilityRandomizer.Object))
                .Returns(cohort);

            var generatedCohort = leadershipGenerator.GenerateCohort(9266, 90210, leaderAlignment, "class name");
            Assert.That(generatedCohort, Is.Null);
        }

        [Test]
        public void FollowerGenerated()
        {
            var follower = new Character();
            mockCharacterGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object, mockAnyPlayerClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object, mockRawAbilityRandomizer.Object))
                .Returns(follower);

            var generatedFollower = leadershipGenerator.GenerateFollower(9266, leaderAlignment, "class name");
            Assert.That(generatedFollower, Is.EqualTo(follower));
            mockSetLevelRandomizer.VerifySet(r => r.SetLevel = 9266);
        }

        [Test]
        public void NPCFollowerGenerated()
        {
            var follower = new Character();
            mockCharacterGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object, mockAnyNPCClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object, mockRawAbilityRandomizer.Object))
                .Returns(follower);
            npcClasses.Add("class name");

            var generatedFollower = leadershipGenerator.GenerateFollower(9266, leaderAlignment, "class name");
            Assert.That(generatedFollower, Is.EqualTo(follower));
            mockSetLevelRandomizer.VerifySet(r => r.SetLevel = 9266);
        }

        [Test]
        public void FollowerCannotOpposeAlignment()
        {
            var followerAlignment = new Alignment("cohort alignment");

            allowedAlignments.Clear();
            allowedAlignments.Add(followerAlignment.ToString());

            var follower = new Character();
            mockCharacterGenerator.Setup(g => g.GenerateWith(mockSetAlignmentRandomizer.Object, mockAnyPlayerClassNameRandomizer.Object, mockSetLevelRandomizer.Object, mockAnyBaseRaceRandomizer.Object, mockAnyMetaraceRandomizer.Object, mockRawAbilityRandomizer.Object))
                .Returns(follower);

            var generatedFollower = leadershipGenerator.GenerateFollower(9266, leaderAlignment, "class name");
            Assert.That(generatedFollower, Is.EqualTo(follower));
            mockSetLevelRandomizer.VerifySet(r => r.SetLevel = 9266);
            mockSetAlignmentRandomizer.VerifySet(r => r.SetAlignment = followerAlignment);
        }
    }
}
