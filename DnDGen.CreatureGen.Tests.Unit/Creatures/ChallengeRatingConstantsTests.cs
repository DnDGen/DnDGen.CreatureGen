using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Creatures
{
    [TestFixture]
    public class ChallengeRatingConstantsTests
    {
        [TestCase(ChallengeRatingConstants.Zero, "0")]
        [TestCase(ChallengeRatingConstants.OneTenth, "1/10")]
        [TestCase(ChallengeRatingConstants.OneEighth, "1/8")]
        [TestCase(ChallengeRatingConstants.OneSixth, "1/6")]
        [TestCase(ChallengeRatingConstants.OneFourth, "1/4")]
        [TestCase(ChallengeRatingConstants.OneThird, "1/3")]
        [TestCase(ChallengeRatingConstants.OneHalf, "1/2")]
        [TestCase(ChallengeRatingConstants.One, "1")]
        [TestCase(ChallengeRatingConstants.Two, "2")]
        [TestCase(ChallengeRatingConstants.Three, "3")]
        [TestCase(ChallengeRatingConstants.Four, "4")]
        [TestCase(ChallengeRatingConstants.Five, "5")]
        [TestCase(ChallengeRatingConstants.Six, "6")]
        [TestCase(ChallengeRatingConstants.Seven, "7")]
        [TestCase(ChallengeRatingConstants.Eight, "8")]
        [TestCase(ChallengeRatingConstants.Nine, "9")]
        [TestCase(ChallengeRatingConstants.Ten, "10")]
        [TestCase(ChallengeRatingConstants.Eleven, "11")]
        [TestCase(ChallengeRatingConstants.Twelve, "12")]
        [TestCase(ChallengeRatingConstants.Thirteen, "13")]
        [TestCase(ChallengeRatingConstants.Fourteen, "14")]
        [TestCase(ChallengeRatingConstants.Fifteen, "15")]
        [TestCase(ChallengeRatingConstants.Sixteen, "16")]
        [TestCase(ChallengeRatingConstants.Seventeen, "17")]
        [TestCase(ChallengeRatingConstants.Eighteen, "18")]
        [TestCase(ChallengeRatingConstants.Nineteen, "19")]
        [TestCase(ChallengeRatingConstants.Twenty, "20")]
        [TestCase(ChallengeRatingConstants.TwentyOne, "21")]
        [TestCase(ChallengeRatingConstants.TwentyTwo, "22")]
        [TestCase(ChallengeRatingConstants.TwentyThree, "23")]
        [TestCase(ChallengeRatingConstants.TwentyFour, "24")]
        [TestCase(ChallengeRatingConstants.TwentyFive, "25")]
        [TestCase(ChallengeRatingConstants.TwentySix, "26")]
        [TestCase(ChallengeRatingConstants.TwentySeven, "27")]
        [TestCase(ChallengeRatingConstants.TwentyEight, "28")]
        [TestCase(ChallengeRatingConstants.TwentyNine, "29")]
        [TestCase(ChallengeRatingConstants.Thirty, "30")]
        public void ChallengeRatingConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void OrderedChallengeRatings()
        {
            var orderedChallengeRatings = ChallengeRatingConstants.GetOrdered();

            Assert.That(orderedChallengeRatings[0], Is.EqualTo(ChallengeRatingConstants.Zero));
            Assert.That(orderedChallengeRatings[1], Is.EqualTo(ChallengeRatingConstants.OneTenth));
            Assert.That(orderedChallengeRatings[2], Is.EqualTo(ChallengeRatingConstants.OneEighth));
            Assert.That(orderedChallengeRatings[3], Is.EqualTo(ChallengeRatingConstants.OneSixth));
            Assert.That(orderedChallengeRatings[4], Is.EqualTo(ChallengeRatingConstants.OneFourth));
            Assert.That(orderedChallengeRatings[5], Is.EqualTo(ChallengeRatingConstants.OneThird));
            Assert.That(orderedChallengeRatings[6], Is.EqualTo(ChallengeRatingConstants.OneHalf));
            Assert.That(orderedChallengeRatings[7], Is.EqualTo(ChallengeRatingConstants.One));
            Assert.That(orderedChallengeRatings[8], Is.EqualTo(ChallengeRatingConstants.Two));
            Assert.That(orderedChallengeRatings[9], Is.EqualTo(ChallengeRatingConstants.Three));
            Assert.That(orderedChallengeRatings[10], Is.EqualTo(ChallengeRatingConstants.Four));
            Assert.That(orderedChallengeRatings[11], Is.EqualTo(ChallengeRatingConstants.Five));
            Assert.That(orderedChallengeRatings[12], Is.EqualTo(ChallengeRatingConstants.Six));
            Assert.That(orderedChallengeRatings[13], Is.EqualTo(ChallengeRatingConstants.Seven));
            Assert.That(orderedChallengeRatings[14], Is.EqualTo(ChallengeRatingConstants.Eight));
            Assert.That(orderedChallengeRatings[15], Is.EqualTo(ChallengeRatingConstants.Nine));
            Assert.That(orderedChallengeRatings[16], Is.EqualTo(ChallengeRatingConstants.Ten));
            Assert.That(orderedChallengeRatings[17], Is.EqualTo(ChallengeRatingConstants.Eleven));
            Assert.That(orderedChallengeRatings[18], Is.EqualTo(ChallengeRatingConstants.Twelve));
            Assert.That(orderedChallengeRatings[19], Is.EqualTo(ChallengeRatingConstants.Thirteen));
            Assert.That(orderedChallengeRatings[20], Is.EqualTo(ChallengeRatingConstants.Fourteen));
            Assert.That(orderedChallengeRatings[21], Is.EqualTo(ChallengeRatingConstants.Fifteen));
            Assert.That(orderedChallengeRatings[22], Is.EqualTo(ChallengeRatingConstants.Sixteen));
            Assert.That(orderedChallengeRatings[23], Is.EqualTo(ChallengeRatingConstants.Seventeen));
            Assert.That(orderedChallengeRatings[24], Is.EqualTo(ChallengeRatingConstants.Eighteen));
            Assert.That(orderedChallengeRatings[25], Is.EqualTo(ChallengeRatingConstants.Nineteen));
            Assert.That(orderedChallengeRatings[26], Is.EqualTo(ChallengeRatingConstants.Twenty));
            Assert.That(orderedChallengeRatings[27], Is.EqualTo(ChallengeRatingConstants.TwentyOne));
            Assert.That(orderedChallengeRatings[28], Is.EqualTo(ChallengeRatingConstants.TwentyTwo));
            Assert.That(orderedChallengeRatings[29], Is.EqualTo(ChallengeRatingConstants.TwentyThree));
            Assert.That(orderedChallengeRatings[30], Is.EqualTo(ChallengeRatingConstants.TwentyFour));
            Assert.That(orderedChallengeRatings[31], Is.EqualTo(ChallengeRatingConstants.TwentyFive));
            Assert.That(orderedChallengeRatings[32], Is.EqualTo(ChallengeRatingConstants.TwentySix));
            Assert.That(orderedChallengeRatings[33], Is.EqualTo(ChallengeRatingConstants.TwentySeven));
            Assert.That(orderedChallengeRatings[34], Is.EqualTo(ChallengeRatingConstants.TwentyEight));
            Assert.That(orderedChallengeRatings[35], Is.EqualTo(ChallengeRatingConstants.TwentyNine));
            Assert.That(orderedChallengeRatings[36], Is.EqualTo(ChallengeRatingConstants.Thirty));
        }
    }
}
