using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureChallengeRatingGroupsTests : CreatureGroupsTableTests
    {
        private ICreatureDataSelector creatureDataSelector;

        [SetUp]
        public void Setup()
        {
            creatureDataSelector = GetNewInstanceOf<ICreatureDataSelector>();
        }

        [Test]
        public void CreatureGroupNames()
        {
            AssertCreatureGroupNamesAreComplete();
        }

        [TestCase(ChallengeRatingConstants.Zero)]
        [TestCase(ChallengeRatingConstants.OneTenth)]
        [TestCase(ChallengeRatingConstants.OneEighth)]
        [TestCase(ChallengeRatingConstants.OneSixth)]
        [TestCase(ChallengeRatingConstants.OneFourth)]
        [TestCase(ChallengeRatingConstants.OneThird)]
        [TestCase(ChallengeRatingConstants.OneHalf)]
        [TestCase(ChallengeRatingConstants.One)]
        [TestCase(ChallengeRatingConstants.Two)]
        [TestCase(ChallengeRatingConstants.Three)]
        [TestCase(ChallengeRatingConstants.Four)]
        [TestCase(ChallengeRatingConstants.Five)]
        [TestCase(ChallengeRatingConstants.Six)]
        [TestCase(ChallengeRatingConstants.Seven)]
        [TestCase(ChallengeRatingConstants.Eight)]
        [TestCase(ChallengeRatingConstants.Nine)]
        [TestCase(ChallengeRatingConstants.Ten)]
        [TestCase(ChallengeRatingConstants.Eleven)]
        [TestCase(ChallengeRatingConstants.Twelve)]
        [TestCase(ChallengeRatingConstants.Thirteen)]
        [TestCase(ChallengeRatingConstants.Fourteen)]
        [TestCase(ChallengeRatingConstants.Fifteen)]
        [TestCase(ChallengeRatingConstants.Sixteen)]
        [TestCase(ChallengeRatingConstants.Seventeen)]
        [TestCase(ChallengeRatingConstants.Eighteen)]
        [TestCase(ChallengeRatingConstants.Nineteen)]
        [TestCase(ChallengeRatingConstants.Twenty)]
        [TestCase(ChallengeRatingConstants.TwentyOne)]
        [TestCase(ChallengeRatingConstants.TwentyTwo)]
        [TestCase(ChallengeRatingConstants.TwentyThree)]
        [TestCase(ChallengeRatingConstants.TwentyFour)]
        [TestCase(ChallengeRatingConstants.TwentyFive)]
        [TestCase(ChallengeRatingConstants.TwentySix)]
        [TestCase(ChallengeRatingConstants.TwentySeven)]
        public void CreatureChallengeRatingGroup(string name, params string[] entries)
        {
            AssertDistinctCollection(name, entries);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureChallengeRatingGroupMatchesCreatureData(string creature)
        {
            var data = creatureDataSelector.SelectFor(creature);
            Assert.That(table[data.ChallengeRating], Contains.Item(creature));
        }
    }
}