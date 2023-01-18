using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    internal class DemographicsGeneratorTests
    {
        private IDemographicsGenerator generator;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<Dice> mockDice;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockDice = new Mock<Dice>();
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();

            generator = new DemographicsGenerator(mockCollectionsSelector.Object, mockDice.Object, mockTypeAndAmountSelector.Object);
        }

        [Test]
        public void Generate_ReturnsDemographics()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = "my age description", Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "my other age description", Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "another age description", Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "Boomer", Amount = 1336, RawAmount = "raw 1336" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my creature"))
                .Returns(ageRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[0]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[1]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[2]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[3])))
                .Returns(ageRolls[0]);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
                .Returns(new[] { "This is how I die" });

            var heightRolls = new List<TypeAndAmountSelection>();
            heightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 783, RawAmount = "raw 783" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            SetUpMinMaxRolls("raw 783", 773, 793);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            SetUpMinMaxRolls("raw 420", 410, 430);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo("my age description"));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.HeightOrLength, Is.Not.Null);
            Assert.That(demographics.HeightOrLength.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.HeightOrLength.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.HeightOrLength.Description, Is.EqualTo("Average"));
            Assert.That(demographics.MaximumAge, Is.Not.Null);
            Assert.That(demographics.MaximumAge.Unit, Is.EqualTo("years"));
            Assert.That(demographics.MaximumAge.Value, Is.EqualTo(90210));
            Assert.That(demographics.MaximumAge.Description, Is.EqualTo("This is how I die"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Weight.Value, Is.EqualTo(8245 + 783 * 420));
            Assert.That(demographics.Weight.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Appearance, Is.EqualTo("my random appearance"));
        }

        private void SetUpMinMaxRolls(string roll, int min, int max)
        {
            var mockRoll = new Mock<PartialRoll>();
            mockDice.Setup(d => d.Roll(roll)).Returns(mockRoll.Object);

            mockRoll.Setup(d => d.AsPotentialMinimum<int>()).Returns(min);
            mockRoll.Setup(d => d.AsPotentialMaximum<int>(true)).Returns(max);
        }

        [Test]
        public void Generate_ReturnsDemographics_WithSingleAgeCategory()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = "my only age description", Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my creature"))
                .Returns(ageRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[0]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => !c.Any()),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => !c.Any()),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => !c.Any())))
                .Returns(ageRolls[0]);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
                .Returns(new[] { "This is how I die" });

            var heightRolls = new List<TypeAndAmountSelection>();
            heightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 783, RawAmount = "raw 783" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            SetUpMinMaxRolls("raw 783", 773, 793);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            SetUpMinMaxRolls("raw 420", 410, 430);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo("my only age description"));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.HeightOrLength, Is.Not.Null);
            Assert.That(demographics.HeightOrLength.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.HeightOrLength.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.HeightOrLength.Description, Is.EqualTo("Average"));
            Assert.That(demographics.MaximumAge, Is.Not.Null);
            Assert.That(demographics.MaximumAge.Unit, Is.EqualTo("years"));
            Assert.That(demographics.MaximumAge.Value, Is.EqualTo(90210));
            Assert.That(demographics.MaximumAge.Description, Is.EqualTo("This is how I die"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Weight.Value, Is.EqualTo(8245 + 783 * 420));
            Assert.That(demographics.Weight.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Appearance, Is.EqualTo("my random appearance"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Generate_ReturnsDemographics_WithAgeCategory(int index)
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = "my age description", Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "my other age description", Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "another age description", Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "Boomer", Amount = 1336, RawAmount = "raw 1336" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my creature"))
                .Returns(ageRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[0]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[1]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[2]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[3])))
                .Returns(ageRolls[index]);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
                .Returns(new[] { "This is how I die" });

            var heightRolls = new List<TypeAndAmountSelection>();
            heightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 783, RawAmount = "raw 783" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            SetUpMinMaxRolls("raw 783", 773, 793);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            SetUpMinMaxRolls("raw 420", 410, 430);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(ageRolls[index].Amount));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo(ageRolls[index].Type));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.HeightOrLength, Is.Not.Null);
            Assert.That(demographics.HeightOrLength.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.HeightOrLength.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.HeightOrLength.Description, Is.EqualTo("Average"));
            Assert.That(demographics.MaximumAge, Is.Not.Null);
            Assert.That(demographics.MaximumAge.Unit, Is.EqualTo("years"));
            Assert.That(demographics.MaximumAge.Value, Is.EqualTo(90210));
            Assert.That(demographics.MaximumAge.Description, Is.EqualTo("This is how I die"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Weight.Value, Is.EqualTo(8245 + 783 * 420));
            Assert.That(demographics.Weight.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Appearance, Is.EqualTo("my random appearance"));
        }

        [Test]
        public void Generate_ReturnsDemographics_WhenAgeIsGreaterThanMaximumAge()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = "my age description", Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "my other age description", Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "another age description", Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "Boomer", Amount = 1336, RawAmount = "raw 1336" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Maximum, Amount = 1209, RawAmount = "raw 1209" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my creature"))
                .Returns(ageRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[0]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[1]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[2]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[3])))
                .Returns(ageRolls[0]);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
                .Returns(new[] { "This is how I die" });

            var heightRolls = new List<TypeAndAmountSelection>();
            heightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 783, RawAmount = "raw 783" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            SetUpMinMaxRolls("raw 783", 773, 793);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            SetUpMinMaxRolls("raw 420", 410, 430);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo("my age description"));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.HeightOrLength, Is.Not.Null);
            Assert.That(demographics.HeightOrLength.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.HeightOrLength.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.HeightOrLength.Description, Is.EqualTo("Average"));
            Assert.That(demographics.MaximumAge, Is.Not.Null);
            Assert.That(demographics.MaximumAge.Unit, Is.EqualTo("years"));
            Assert.That(demographics.MaximumAge.Value, Is.EqualTo(9266));
            Assert.That(demographics.MaximumAge.Description, Is.EqualTo("This is how I die"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Weight.Value, Is.EqualTo(8245 + 783 * 420));
            Assert.That(demographics.Weight.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Appearance, Is.EqualTo("my random appearance"));
        }

        [TestCase(773, "Short")]
        [TestCase(779, "Short")]
        [TestCase(780, "Average")]
        [TestCase(783, "Average")]
        [TestCase(786, "Average")]
        [TestCase(787, "Tall")]
        [TestCase(793, "Tall")]
        public void Generate_ReturnsDemographics_WithHeightDescription(int roll, string description)
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = "my age description", Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "my other age description", Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "another age description", Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "Boomer", Amount = 1336, RawAmount = "raw 1336" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my creature"))
                .Returns(ageRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[0]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[1]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[2]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[3])))
                .Returns(ageRolls[0]);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
                .Returns(new[] { "This is how I die" });

            var heightRolls = new List<TypeAndAmountSelection>();
            heightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = roll, RawAmount = "raw 783" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            SetUpMinMaxRolls("raw 783", 773, 793);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            SetUpMinMaxRolls("raw 420", 410, 430);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo("my age description"));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.HeightOrLength, Is.Not.Null);
            Assert.That(demographics.HeightOrLength.Value, Is.EqualTo(42 + roll));
            Assert.That(demographics.HeightOrLength.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.HeightOrLength.Description, Is.EqualTo(description));
            Assert.That(demographics.MaximumAge, Is.Not.Null);
            Assert.That(demographics.MaximumAge.Unit, Is.EqualTo("years"));
            Assert.That(demographics.MaximumAge.Value, Is.EqualTo(90210));
            Assert.That(demographics.MaximumAge.Description, Is.EqualTo("This is how I die"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Weight.Value, Is.EqualTo(8245 + roll * 420));
            Assert.That(demographics.Weight.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Appearance, Is.EqualTo("my random appearance"));
        }

        [TestCase(410, "Light")]
        [TestCase(416, "Light")]
        [TestCase(417, "Average")]
        [TestCase(420, "Average")]
        [TestCase(423, "Average")]
        [TestCase(424, "Heavy")]
        [TestCase(430, "Heavy")]
        public void Generate_ReturnsDemographics_WithWeightDescription(int roll, string description)
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = "my age description", Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "my other age description", Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "another age description", Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "Boomer", Amount = 1336, RawAmount = "raw 1336" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my creature"))
                .Returns(ageRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[0]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[1]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[2]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[3])))
                .Returns(ageRolls[0]);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
                .Returns(new[] { "This is how I die" });

            var heightRolls = new List<TypeAndAmountSelection>();
            heightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 783, RawAmount = "raw 783" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            SetUpMinMaxRolls("raw 783", 773, 793);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = roll, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            SetUpMinMaxRolls("raw 420", 410, 430);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo("my age description"));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.HeightOrLength, Is.Not.Null);
            Assert.That(demographics.HeightOrLength.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.HeightOrLength.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.HeightOrLength.Description, Is.EqualTo("Average"));
            Assert.That(demographics.MaximumAge, Is.Not.Null);
            Assert.That(demographics.MaximumAge.Unit, Is.EqualTo("years"));
            Assert.That(demographics.MaximumAge.Value, Is.EqualTo(90210));
            Assert.That(demographics.MaximumAge.Description, Is.EqualTo("This is how I die"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Weight.Value, Is.EqualTo(8245 + 783 * roll));
            Assert.That(demographics.Weight.Description, Is.EqualTo(description));
            Assert.That(demographics.Appearance, Is.EqualTo("my random appearance"));
        }
    }
}
