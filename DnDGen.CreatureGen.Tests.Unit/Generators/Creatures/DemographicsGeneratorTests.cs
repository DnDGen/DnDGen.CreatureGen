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

            mockDice
                .Setup(d => d.Describe(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()))
                .Returns((string r, int v, string[] descriptions) => descriptions[descriptions.Length / 2]);
        }

        [Test]
        public void Generate_ReturnsDemographics()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" });
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

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo(AgeConstants.Categories.Adulthood));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.EqualTo(922 + 227));
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo("Average"));
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

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

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
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.EqualTo(922 + 227));
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo("Average"));
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

        //INFO: This emulates how Unicorns age
        [TestCase(0)]
        [TestCase(1)]
        public void Generate_ReturnsDemographics_WithScatteredAgeCategories(int index)
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Venerable, Amount = 42, RawAmount = "raw 42" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my creature"))
                .Returns(ageRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[0]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => !c.Any()),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => !c.Any()),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[1])))
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

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

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
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.EqualTo(922 + 227));
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo("Average"));
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
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" });
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

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

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
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.EqualTo(922 + 227));
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo("Average"));
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
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" });
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

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo(AgeConstants.Categories.Adulthood));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.EqualTo(922 + 227));
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo("Average"));
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

        [TestCase(773, "Very Short", 0)]
        [TestCase(776, "Very Short", 0)]
        [TestCase(777, "Short", 1)]
        [TestCase(780, "Short", 1)]
        [TestCase(781, "Average", 2)]
        [TestCase(783, "Average", 2)]
        [TestCase(784, "Average", 2)]
        [TestCase(785, "Tall", 3)]
        [TestCase(788, "Tall", 3)]
        [TestCase(789, "Very Tall", 4)]
        [TestCase(793, "Very Tall", 4)]
        public void Generate_ReturnsDemographics_WithHeightDescription(int roll, string description, int index)
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" });
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

            mockDice
                .Setup(d => d.Describe("raw 42+raw 783", 42 + roll, It.IsAny<string[]>()))
                .Returns((string r, int v, string[] descriptions) => descriptions[index]);

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo(AgeConstants.Categories.Adulthood));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.EqualTo(42 + roll));
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo(description));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.EqualTo(922 + 227));
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo("Average"));
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

        [TestCase(217, "Very Short", 0)]
        [TestCase(220, "Very Short", 0)]
        [TestCase(221, "Short", 1)]
        [TestCase(224, "Short", 1)]
        [TestCase(225, "Average", 2)]
        [TestCase(227, "Average", 2)]
        [TestCase(228, "Average", 2)]
        [TestCase(229, "Long", 3)]
        [TestCase(232, "Long", 3)]
        [TestCase(233, "Very Long", 4)]
        [TestCase(237, "Very Long", 4)]
        public void Generate_ReturnsDemographics_WithLengthDescription(int roll, string description, int index)
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" });
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

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = roll, RawAmount = "raw 227" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            mockDice
                .Setup(d => d.Describe("raw 922+raw 227", 922 + roll, It.IsAny<string[]>()))
                .Returns((string r, int v, string[] descriptions) => descriptions[index]);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo(AgeConstants.Categories.Adulthood));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.EqualTo(922 + roll));
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo(description));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo("Average"));
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

        [TestCase(410, "Very Light", 0)]
        [TestCase(413, "Very Light", 0)]
        [TestCase(414, "Light", 1)]
        [TestCase(417, "Light", 1)]
        [TestCase(418, "Average", 2)]
        [TestCase(420, "Average", 2)]
        [TestCase(421, "Average", 2)]
        [TestCase(422, "Heavy", 3)]
        [TestCase(425, "Heavy", 3)]
        [TestCase(426, "Very Heavy", 4)]
        [TestCase(430, "Very Heavy", 4)]
        public void Generate_ReturnsDemographics_WithWeightDescription(int roll, string description, int index)
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" });
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

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = roll, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            mockDice
                .Setup(d => d.Describe("raw 8245+783*raw 420", 8245 + 783 * roll, It.IsAny<string[]>()))
                .Returns((string r, int v, string[] descriptions) => descriptions[index]);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo(AgeConstants.Categories.Adulthood));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.EqualTo(922 + 227));
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo("Average"));
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

        [Test]
        public void Generate_ReturnsDemographics_WithNoLength()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" });
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

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 0, RawAmount = "raw 0" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 0, RawAmount = "raw 0" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 0, RawAmount = "raw 0" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo(AgeConstants.Categories.Adulthood));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.Zero);
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo("Average"));
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
        public void Generate_ReturnsDemographics_WithNoHeight()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" });
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
            heightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 0, RawAmount = "raw 0" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 0, RawAmount = "raw 0" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 0, RawAmount = "raw 0" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo(AgeConstants.Categories.Adulthood));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.Zero);
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.EqualTo(922 + 227));
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo("Average"));
            Assert.That(demographics.MaximumAge, Is.Not.Null);
            Assert.That(demographics.MaximumAge.Unit, Is.EqualTo("years"));
            Assert.That(demographics.MaximumAge.Value, Is.EqualTo(90210));
            Assert.That(demographics.MaximumAge.Description, Is.EqualTo("This is how I die"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Weight.Value, Is.EqualTo(8245 + 227 * 420));
            Assert.That(demographics.Weight.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Appearance, Is.EqualTo("my random appearance"));
        }

        [Test]
        public void Generate_ReturnsDemographics_UseLengthForWeightMultiplier()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" });
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

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 2277, RawAmount = "raw 2277" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo(AgeConstants.Categories.Adulthood));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.EqualTo(922 + 2277));
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo("Average"));
            Assert.That(demographics.MaximumAge, Is.Not.Null);
            Assert.That(demographics.MaximumAge.Unit, Is.EqualTo("years"));
            Assert.That(demographics.MaximumAge.Value, Is.EqualTo(90210));
            Assert.That(demographics.MaximumAge.Description, Is.EqualTo("This is how I die"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Weight.Value, Is.EqualTo(8245 + 2277 * 420));
            Assert.That(demographics.Weight.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Appearance, Is.EqualTo("my random appearance"));
        }

        [TestCase(335, "Very Narrow", 0)]
        [TestCase(338, "Very Narrow", 0)]
        [TestCase(339, "Narrow", 1)]
        [TestCase(342, "Narrow", 1)]
        [TestCase(343, "Average", 2)]
        [TestCase(345, "Average", 2)]
        [TestCase(346, "Average", 2)]
        [TestCase(347, "Broad", 3)]
        [TestCase(350, "Broad", 3)]
        [TestCase(351, "Very Broad", 4)]
        [TestCase(355, "Very Broad", 4)]
        public void Generate_ReturnsDemographics_WithWingspanDescription(int roll, string description, int index)
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" });
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

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = roll, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            mockDice
                .Setup(d => d.Describe("raw 123+raw 345", 123 + roll, It.IsAny<string[]>()))
                .Returns((string r, int v, string[] descriptions) => descriptions[index]);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo(AgeConstants.Categories.Adulthood));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.EqualTo(922 + 227));
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.EqualTo(123 + roll));
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo(description));
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
        public void Generate_ReturnsDemographics_WithNoWingspan()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" });
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

            var lengthRolls = new List<TypeAndAmountSelection>();
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" });
            lengthRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 0, RawAmount = "raw 0" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 0, RawAmount = "raw 0" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 0, RawAmount = "raw 0" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Appearances, "my creature"))
                .Returns("my random appearance");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo(AgeConstants.Categories.Adulthood));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.Height, Is.Not.Null);
            Assert.That(demographics.Height.Value, Is.EqualTo(42 + 783));
            Assert.That(demographics.Height.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Height.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Length, Is.Not.Null);
            Assert.That(demographics.Length.Value, Is.EqualTo(922 + 227));
            Assert.That(demographics.Length.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Length.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Wingspan, Is.Not.Null);
            Assert.That(demographics.Wingspan.Value, Is.Zero);
            Assert.That(demographics.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.Wingspan.Description, Is.EqualTo("Average"));
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
        public void GenerateWingspan_ReturnsWingspan()
        {
            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my base key", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other base key", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            var wingspan = generator.GenerateWingspan("my creature", "my base key");
            Assert.That(wingspan, Is.Not.Null);
            Assert.That(wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(wingspan.Description, Is.EqualTo("Average"));
        }

        [TestCase(335, "Very Narrow", 0)]
        [TestCase(338, "Very Narrow", 0)]
        [TestCase(339, "Narrow", 1)]
        [TestCase(342, "Narrow", 1)]
        [TestCase(343, "Average", 2)]
        [TestCase(345, "Average", 2)]
        [TestCase(346, "Average", 2)]
        [TestCase(347, "Broad", 3)]
        [TestCase(350, "Broad", 3)]
        [TestCase(351, "Very Broad", 4)]
        [TestCase(355, "Very Broad", 4)]
        public void GenerateWingspan_ReturnsWingspan_WithDescription(int roll, string description, int index)
        {
            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my base key", Amount = 123, RawAmount = "raw 123" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other base key", Amount = 234, RawAmount = "raw 234" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = roll, RawAmount = "raw 345" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            mockDice
                .Setup(d => d.Describe("raw 123+raw 345", 123 + roll, It.IsAny<string[]>()))
                .Returns((string r, int v, string[] descriptions) => descriptions[index]);

            var wingspan = generator.GenerateWingspan("my creature", "my base key");
            Assert.That(wingspan, Is.Not.Null);
            Assert.That(wingspan.Value, Is.EqualTo(123 + roll));
            Assert.That(wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(wingspan.Description, Is.EqualTo(description));
        }

        [Test]
        public void GenerateWingspan_ReturnsWingspan_WithNoWingspan()
        {
            var wingspanRolls = new List<TypeAndAmountSelection>();
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my base key", Amount = 0, RawAmount = "raw 0" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my other base key", Amount = 0, RawAmount = "raw 0" });
            wingspanRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 0, RawAmount = "raw 0" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            var wingspan = generator.GenerateWingspan("my creature", "my base key");
            Assert.That(wingspan, Is.Not.Null);
            Assert.That(wingspan.Value, Is.Zero);
            Assert.That(wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(wingspan.Description, Is.EqualTo("Average"));
        }
    }
}
