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

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AgeCategories, "my creature"))
                .Returns(new[] { "my age description", "my other age description", "wrong age description", "Boomer" });

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = "my age description", Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "my other age description", Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "wrong age description", Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "Boomer", Amount = 1336, RawAmount = "raw 1336" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my creature"))
                .Returns(ageRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<string>>(c => c.Single() == "my age description"),
                    It.Is<IEnumerable<string>>(c => c.Single() == "my other age description"),
                    It.Is<IEnumerable<string>>(c => c.Single() == "wrong age description"),
                    It.Is<IEnumerable<string>>(c => c.Single() == "Boomer")))
                .Returns("my age description");

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AgeCategories, "my creature" + AgeConstants.Categories.Maximum))
                .Returns(new[] { "This is how I die" });

            var heightRolls = new List<TypeAndAmountSelection>();
            heightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 783, RawAmount = "raw 783" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42").AsPotentialMinimum())
                .Returns(32);
            mockDice
                .Setup(d => d.Roll("raw 42").AsPotentialMaximum(true))
                .Returns(52);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            mockDice
                .Setup(d => d.Roll("raw 8245").AsPotentialMinimum())
                .Returns(8145);
            mockDice
                .Setup(d => d.Roll("raw 8245").AsPotentialMaximum(true))
                .Returns(8345);

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo("my age description"));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.HeightOrLength, Is.Not.Null);
            Assert.That(demographics.HeightOrLength.Value, Is.EqualTo(783 + 42));
            Assert.That(demographics.HeightOrLength.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.HeightOrLength.Description, Is.EqualTo("Average"));
            Assert.That(demographics.MaximumAge, Is.Not.Null);
            Assert.That(demographics.MaximumAge.Unit, Is.EqualTo("years"));
            Assert.That(demographics.MaximumAge.Value, Is.EqualTo(90210));
            Assert.That(demographics.MaximumAge.Description, Is.EqualTo("This is how I die"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Weight.Value, Is.EqualTo(420 + 42 * 8245));
            Assert.That(demographics.Weight.Description, Is.EqualTo("Average"));
        }

        [Test]
        public void Generate_ReturnsDemographics_WithSingleAgeCategory()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AgeCategories, "my creature"))
                .Returns(new[] { "my only age description" });

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = "my age description", Amount = 9266, RawAmount = "raw 9266" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "my other age description", Amount = 1337, RawAmount = "raw 1337" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "wrong age description", Amount = 600, RawAmount = "raw 600" });
            ageRolls.Add(new TypeAndAmountSelection { Type = "Boomer", Amount = 1336, RawAmount = "raw 1336" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my creature"))
                .Returns(ageRolls);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AgeCategories, "my creature" + AgeConstants.Categories.Maximum))
                .Returns(new[] { "This is how I die" });

            var heightRolls = new List<TypeAndAmountSelection>();
            heightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 783, RawAmount = "raw 783" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42").AsPotentialMinimum())
                .Returns(32);
            mockDice
                .Setup(d => d.Roll("raw 42").AsPotentialMaximum(true))
                .Returns(52);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            mockDice
                .Setup(d => d.Roll("raw 8245").AsPotentialMinimum())
                .Returns(8145);
            mockDice
                .Setup(d => d.Roll("raw 8245").AsPotentialMaximum(true))
                .Returns(8345);

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo("my only age description"));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.HeightOrLength, Is.Not.Null);
            Assert.That(demographics.HeightOrLength.Value, Is.EqualTo(783 + 42));
            Assert.That(demographics.HeightOrLength.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.HeightOrLength.Description, Is.EqualTo("Average"));
            Assert.That(demographics.MaximumAge, Is.Not.Null);
            Assert.That(demographics.MaximumAge.Unit, Is.EqualTo("years"));
            Assert.That(demographics.MaximumAge.Value, Is.EqualTo(90210));
            Assert.That(demographics.MaximumAge.Description, Is.EqualTo("This is how I die"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Weight.Value, Is.EqualTo(420 + 42 * 8245));
            Assert.That(demographics.Weight.Description, Is.EqualTo("Average"));
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

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AgeCategories, "my creature"))
                .Returns(new[] { "my age description", "my other age description", "another age description", "Boomer" });

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
                    It.Is<IEnumerable<string>>(c => c.Single() == "my age description"),
                    It.Is<IEnumerable<string>>(c => c.Single() == "my other age description"),
                    It.Is<IEnumerable<string>>(c => c.Single() == "wrong age description"),
                    It.Is<IEnumerable<string>>(c => c.Single() == "Boomer")))
                .Returns(ageRolls[index].Type);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.AgeCategories, "my creature" + AgeConstants.Categories.Maximum))
                .Returns(new[] { "This is how I die" });

            var heightRolls = new List<TypeAndAmountSelection>();
            heightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" });
            heightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 783, RawAmount = "raw 783" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42").AsPotentialMinimum())
                .Returns(32);
            mockDice
                .Setup(d => d.Roll("raw 42").AsPotentialMaximum(true))
                .Returns(52);

            var weightRolls = new List<TypeAndAmountSelection>();
            weightRolls.Add(new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" });
            weightRolls.Add(new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            mockDice
                .Setup(d => d.Roll("raw 8245").AsPotentialMinimum())
                .Returns(8145);
            mockDice
                .Setup(d => d.Roll("raw 8245").AsPotentialMaximum(true))
                .Returns(8345);

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266));
            Assert.That(demographics.Age.Unit, Is.EqualTo("years"));
            Assert.That(demographics.Age.Description, Is.EqualTo("my age description"));
            Assert.That(demographics.Gender, Is.EqualTo("my gender"));
            Assert.That(demographics.HeightOrLength, Is.Not.Null);
            Assert.That(demographics.HeightOrLength.Value, Is.EqualTo(783 + 42));
            Assert.That(demographics.HeightOrLength.Unit, Is.EqualTo("inches"));
            Assert.That(demographics.HeightOrLength.Description, Is.EqualTo("Average"));
            Assert.That(demographics.MaximumAge, Is.Not.Null);
            Assert.That(demographics.MaximumAge.Unit, Is.EqualTo("years"));
            Assert.That(demographics.MaximumAge.Value, Is.EqualTo(90210));
            Assert.That(demographics.MaximumAge.Description, Is.EqualTo("This is how I die"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Weight.Value, Is.EqualTo(420 + 42 * 8245));
            Assert.That(demographics.Weight.Description, Is.EqualTo("Average"));
        }

        [Test]
        public void Generate_ReturnsDemographics_WhenAgeIsGreaterThanMaximumAge()
        {
            Assert.Fail("not yet written");
        }

        [TestCase(5, "Short")]
        [TestCase(6, "Average")]
        [TestCase(7, "Tall")]
        public void Generate_ReturnsDemographics_WithHeightDescription(int roll, string description)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(5, "Light")]
        [TestCase(6, "Average")]
        [TestCase(7, "Heavy")]
        public void Generate_ReturnsDemographics_WithWeightDescription(int roll, string description)
        {
            Assert.Fail("not yet written");
        }
    }
}
