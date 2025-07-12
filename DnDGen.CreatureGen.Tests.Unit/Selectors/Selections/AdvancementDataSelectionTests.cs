using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class AdvancementDataSelectionTests
    {
        private AdvancementDataSelection selection;
        private Mock<Dice> mockDice;

        [SetUp]
        public void Setup()
        {
            selection = new AdvancementDataSelection();
            mockDice = new Mock<Dice>();
        }

        [Test]
        public void AdvancementDataSelectionIsInitialized()
        {
            Assert.That(selection.AdditionalHitDice, Is.Zero);
            Assert.That(selection.AdditionalHitDiceRoll, Is.Empty);
            Assert.That(selection.AdjustedChallengeRating, Is.Empty);
            Assert.That(selection.ChallengeRatingDivisor, Is.EqualTo(1));
            Assert.That(selection.CasterLevelAdjustment, Is.Zero);
            Assert.That(selection.ConstitutionAdjustment, Is.Zero);
            Assert.That(selection.DexterityAdjustment, Is.Zero);
            Assert.That(selection.NaturalArmorAdjustment, Is.Zero);
            Assert.That(selection.Reach, Is.Zero);
            Assert.That(selection.Size, Is.Empty);
            Assert.That(selection.Space, Is.Zero);
            Assert.That(selection.StrengthAdjustment, Is.Zero);
            Assert.That(selection.MaxHitDice, Is.EqualTo(int.MaxValue));
        }

        [Test]
        public void SectionCountIs10()
        {
            Assert.That(selection.SectionCount, Is.EqualTo(10));
        }

        [Test]
        public void Map_FromString_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AdvancementSelectionData.AdditionalHitDiceRoll] = "9266d90210";
            data[DataIndexConstants.AdvancementSelectionData.ChallengeRatingDivisor] = "42";
            data[DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment] = "600";
            data[DataIndexConstants.AdvancementSelectionData.DexterityAdjustment] = "1337";
            data[DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment] = "1336";
            data[DataIndexConstants.AdvancementSelectionData.Reach] = "9.6";
            data[DataIndexConstants.AdvancementSelectionData.Size] = "enormous";
            data[DataIndexConstants.AdvancementSelectionData.Space] = "78.3";
            data[DataIndexConstants.AdvancementSelectionData.StrengthAdjustment] = "8245";
            data[DataIndexConstants.AdvancementSelectionData.AdjustedChallengeRating] = "adjusted cr";

            var newSelection = AdvancementDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.AdditionalHitDiceRoll, Is.EqualTo("9266d90210"));
            Assert.That(newSelection.ChallengeRatingDivisor, Is.EqualTo(42));
            Assert.That(newSelection.ConstitutionAdjustment, Is.EqualTo(600));
            Assert.That(newSelection.DexterityAdjustment, Is.EqualTo(1337));
            Assert.That(newSelection.NaturalArmorAdjustment, Is.EqualTo(1336));
            Assert.That(newSelection.Reach, Is.EqualTo(9.6));
            Assert.That(newSelection.Size, Is.EqualTo("enormous"));
            Assert.That(newSelection.Space, Is.EqualTo(78.3));
            Assert.That(newSelection.StrengthAdjustment, Is.EqualTo(8245));
            Assert.That(newSelection.AdjustedChallengeRating, Is.EqualTo("adjusted cr"));
        }

        [Test]
        public void Map_FromSelection_ReturnsString()
        {
            var selection = new AdvancementDataSelection
            {
                AdditionalHitDiceRoll = "9266d90210",
                ChallengeRatingDivisor = 42,
                ConstitutionAdjustment = 600,
                DexterityAdjustment = 1337,
                NaturalArmorAdjustment = 1336,
                Reach = 9.6,
                Size = "enormous",
                Space = 78.3,
                StrengthAdjustment = 8245,
                AdjustedChallengeRating = "adjusted cr",
            };

            var rawData = AdvancementDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.AdditionalHitDiceRoll], Is.EqualTo("9266d90210"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.ChallengeRatingDivisor], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.DexterityAdjustment], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment], Is.EqualTo("1336"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.Reach], Is.EqualTo("9.6"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.Size], Is.EqualTo("enormous"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.Space], Is.EqualTo("78.3"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.StrengthAdjustment], Is.EqualTo("8245"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.AdjustedChallengeRating], Is.EqualTo("adjusted cr"));
        }

        [Test]
        public void MapTo_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.AdvancementSelectionData.AdditionalHitDiceRoll] = "9266d90210";
            data[DataIndexConstants.AdvancementSelectionData.ChallengeRatingDivisor] = "42";
            data[DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment] = "600";
            data[DataIndexConstants.AdvancementSelectionData.DexterityAdjustment] = "1337";
            data[DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment] = "1336";
            data[DataIndexConstants.AdvancementSelectionData.Reach] = "9.6";
            data[DataIndexConstants.AdvancementSelectionData.Size] = "enormous";
            data[DataIndexConstants.AdvancementSelectionData.Space] = "78.3";
            data[DataIndexConstants.AdvancementSelectionData.StrengthAdjustment] = "8245";
            data[DataIndexConstants.AdvancementSelectionData.AdjustedChallengeRating] = "adjusted cr";

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.AdditionalHitDiceRoll, Is.EqualTo("9266d90210"));
            Assert.That(newSelection.ChallengeRatingDivisor, Is.EqualTo(42));
            Assert.That(newSelection.ConstitutionAdjustment, Is.EqualTo(600));
            Assert.That(newSelection.DexterityAdjustment, Is.EqualTo(1337));
            Assert.That(newSelection.NaturalArmorAdjustment, Is.EqualTo(1336));
            Assert.That(newSelection.Reach, Is.EqualTo(9.6));
            Assert.That(newSelection.Size, Is.EqualTo("enormous"));
            Assert.That(newSelection.Space, Is.EqualTo(78.3));
            Assert.That(newSelection.StrengthAdjustment, Is.EqualTo(8245));
            Assert.That(newSelection.AdjustedChallengeRating, Is.EqualTo("adjusted cr"));
        }

        [Test]
        public void MapFrom_ReturnsString()
        {
            var selection = new AdvancementDataSelection
            {
                AdditionalHitDiceRoll = "9266d90210",
                ChallengeRatingDivisor = 42,
                ConstitutionAdjustment = 600,
                DexterityAdjustment = 1337,
                NaturalArmorAdjustment = 1336,
                Reach = 9.6,
                Size = "enormous",
                Space = 78.3,
                StrengthAdjustment = 8245,
                AdjustedChallengeRating = "adjusted cr",
            };

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.AdditionalHitDiceRoll], Is.EqualTo("9266d90210"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.ChallengeRatingDivisor], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.ConstitutionAdjustment], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.DexterityAdjustment], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.NaturalArmorAdjustment], Is.EqualTo("1336"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.Reach], Is.EqualTo("9.6"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.Size], Is.EqualTo("enormous"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.Space], Is.EqualTo("78.3"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.StrengthAdjustment], Is.EqualTo("8245"));
            Assert.That(rawData[DataIndexConstants.AdvancementSelectionData.AdjustedChallengeRating], Is.EqualTo("adjusted cr"));
        }

        [Test]
        public void AdvancementIsValid_SetsMaxHitDice()
        {
            selection.AdditionalHitDiceRoll = "roll 9266";
            mockDice.Setup(d => d.Roll("roll 9266").AsPotentialMinimum<int>()).Returns(9266);

            var isValid = selection.AdvancementIsValid(mockDice.Object, 90210);
            Assert.That(selection.MaxHitDice, Is.EqualTo(90210));
        }

        [TestCase(0, int.MaxValue, true)]
        [TestCase(1, int.MaxValue, true)]
        [TestCase(2, int.MaxValue, true)]
        [TestCase(10, int.MaxValue, true)]
        [TestCase(100, int.MaxValue, true)]
        [TestCase(0, 20, true)]
        [TestCase(1, 20, true)]
        [TestCase(2, 20, true)]
        [TestCase(10, 20, true)]
        [TestCase(19, 20, true)]
        [TestCase(20, 20, true)]
        [TestCase(21, 20, false)]
        [TestCase(100, 20, false)]
        [TestCase(0, 10, true)]
        [TestCase(1, 10, true)]
        [TestCase(2, 10, true)]
        [TestCase(9, 10, true)]
        [TestCase(10, 10, true)]
        [TestCase(11, 10, false)]
        [TestCase(100, 10, false)]
        public void AdvancementIsValid_ReturnsValidity(int minRoll, int max, bool expected)
        {
            selection.AdditionalHitDiceRoll = "roll 9266";
            mockDice.Setup(d => d.Roll("roll 9266").AsPotentialMinimum<int>()).Returns(minRoll);

            var isValid = selection.AdvancementIsValid(mockDice.Object, max);
            Assert.That(selection.MaxHitDice, Is.EqualTo(max));
            Assert.That(isValid, Is.EqualTo(expected));
        }

        [Test]
        public void SetAdditionalProperties_SetsAdditionalHitDice()
        {
            selection.AdditionalHitDiceRoll = "roll 9266";
            selection.AdjustedChallengeRating = ChallengeRatingConstants.CR1;
            selection.ChallengeRatingDivisor = 1;

            mockDice.Setup(d => d.Roll("roll 9266").AsSum<int>()).Returns(9266);

            selection.SetAdditionalProperties(mockDice.Object);
            Assert.That(selection.AdditionalHitDice, Is.EqualTo(9266));
        }

        [Test]
        public void SetAdditionalProperties_SetsAdditionalHitDice_LessThanMax()
        {
            selection.AdditionalHitDiceRoll = "roll 9266";
            selection.AdjustedChallengeRating = ChallengeRatingConstants.CR1;
            selection.ChallengeRatingDivisor = 1;

            mockDice.Setup(d => d.Roll("roll 9266").AsPotentialMinimum<int>()).Returns(42);
            mockDice.Setup(d => d.Roll("roll 9266").AsSum<int>()).Returns(9266);

            var isValid = selection.AdvancementIsValid(mockDice.Object, 90210);
            Assert.That(selection.MaxHitDice, Is.EqualTo(90210));
            Assert.That(isValid, Is.True);

            selection.SetAdditionalProperties(mockDice.Object);
            Assert.That(selection.AdditionalHitDice, Is.EqualTo(9266));
        }

        [Test]
        public void SetAdditionalProperties_SetsAdditionalHitDice_ToMax()
        {
            selection.AdditionalHitDiceRoll = "roll 9266";
            selection.AdjustedChallengeRating = ChallengeRatingConstants.CR1;
            selection.ChallengeRatingDivisor = 1;

            mockDice.Setup(d => d.Roll("roll 9266").AsPotentialMinimum<int>()).Returns(42);
            mockDice.Setup(d => d.Roll("roll 9266").AsSum<int>()).Returns(90210);

            var isValid = selection.AdvancementIsValid(mockDice.Object, 9266);
            Assert.That(selection.MaxHitDice, Is.EqualTo(9266));
            Assert.That(isValid, Is.True);

            selection.SetAdditionalProperties(mockDice.Object);
            Assert.That(selection.AdditionalHitDice, Is.EqualTo(9266));
        }

        [TestCase(ChallengeRatingConstants.CR1, 0, 1, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1, 0, 2, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1, 0, 3, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1, 0, 4, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1, 1, 1, ChallengeRatingConstants.CR2)]
        [TestCase(ChallengeRatingConstants.CR1, 1, 2, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1, 1, 3, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1, 1, 4, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1, 2, 1, ChallengeRatingConstants.CR3)]
        [TestCase(ChallengeRatingConstants.CR1, 2, 2, ChallengeRatingConstants.CR2)]
        [TestCase(ChallengeRatingConstants.CR1, 2, 3, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1, 2, 4, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1, 3, 1, ChallengeRatingConstants.CR4)]
        [TestCase(ChallengeRatingConstants.CR1, 3, 2, ChallengeRatingConstants.CR2)]
        [TestCase(ChallengeRatingConstants.CR1, 3, 3, ChallengeRatingConstants.CR2)]
        [TestCase(ChallengeRatingConstants.CR1, 3, 4, ChallengeRatingConstants.CR1)]
        [TestCase(ChallengeRatingConstants.CR1, 4, 1, ChallengeRatingConstants.CR5)]
        [TestCase(ChallengeRatingConstants.CR1, 4, 2, ChallengeRatingConstants.CR3)]
        [TestCase(ChallengeRatingConstants.CR1, 4, 3, ChallengeRatingConstants.CR2)]
        [TestCase(ChallengeRatingConstants.CR1, 4, 4, ChallengeRatingConstants.CR2)]
        [TestCase(ChallengeRatingConstants.CR1, 5, 1, ChallengeRatingConstants.CR6)]
        [TestCase(ChallengeRatingConstants.CR1, 5, 2, ChallengeRatingConstants.CR3)]
        [TestCase(ChallengeRatingConstants.CR1, 5, 3, ChallengeRatingConstants.CR2)]
        [TestCase(ChallengeRatingConstants.CR1, 5, 4, ChallengeRatingConstants.CR2)]
        [TestCase(ChallengeRatingConstants.CR1, 10, 1, ChallengeRatingConstants.CR11)]
        [TestCase(ChallengeRatingConstants.CR1, 10, 2, ChallengeRatingConstants.CR6)]
        [TestCase(ChallengeRatingConstants.CR1, 10, 3, ChallengeRatingConstants.CR4)]
        [TestCase(ChallengeRatingConstants.CR1, 10, 4, ChallengeRatingConstants.CR3)]
        [TestCase(ChallengeRatingConstants.CR1, 100, 1, "101")]
        [TestCase(ChallengeRatingConstants.CR1, 100, 2, "51")]
        [TestCase(ChallengeRatingConstants.CR1, 100, 3, "34")]
        [TestCase(ChallengeRatingConstants.CR1, 100, 4, ChallengeRatingConstants.CR26)]
        public void SetAdditionalProperties_SetsAdjustedChallengeRating(string cr, int roll, int divisor, string expected)
        {
            selection.AdditionalHitDiceRoll = "roll 9266";
            selection.AdjustedChallengeRating = cr;
            selection.ChallengeRatingDivisor = divisor;

            mockDice.Setup(d => d.Roll("roll 9266").AsSum<int>()).Returns(roll);

            selection.SetAdditionalProperties(mockDice.Object);
            Assert.That(selection.AdditionalHitDice, Is.EqualTo(roll));
            Assert.That(selection.AdjustedChallengeRating, Is.EqualTo(expected));
        }

        [Test]
        public void SetAdditionalProperties_SetsAdjustedChallengeRating_Max()
        {
            selection.AdditionalHitDiceRoll = "roll 9266";
            selection.AdjustedChallengeRating = ChallengeRatingConstants.CR1;
            selection.ChallengeRatingDivisor = 3;

            mockDice.Setup(d => d.Roll("roll 9266").AsPotentialMinimum<int>()).Returns(2);
            mockDice.Setup(d => d.Roll("roll 9266").AsSum<int>()).Returns(9266);

            var isValid = selection.AdvancementIsValid(mockDice.Object, 42);
            Assert.That(selection.MaxHitDice, Is.EqualTo(42));
            Assert.That(isValid, Is.True);

            selection.SetAdditionalProperties(mockDice.Object);
            Assert.That(selection.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(selection.AdjustedChallengeRating, Is.EqualTo(ChallengeRatingConstants.CR15));
        }
    }
}
