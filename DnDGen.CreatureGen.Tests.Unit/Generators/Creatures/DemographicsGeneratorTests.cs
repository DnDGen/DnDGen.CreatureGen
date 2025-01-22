using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Creatures
{
    [TestFixture]
    internal class DemographicsGeneratorTests
    {
        private const int BaseMaleHumanHeight_58 = 4 * 12 + 10;
        private const int BaseFemaleHumanHeight_53 = 4 * 12 + 5;
        private const int MinMaleHumanHeight_60 = BaseMaleHumanHeight_58 + 2;
        private const int MaxMaleHumanHeight_78 = BaseMaleHumanHeight_58 + 2 * 10;
        private const int AvgMaleHumanHeight_69 = (MaxMaleHumanHeight_78 + MinMaleHumanHeight_60) / 2;
        private const int MinFemaleHumanHeight_55 = BaseFemaleHumanHeight_53 + 2;
        private const int MaxFemaleHumanHeight_73 = BaseFemaleHumanHeight_53 + 2 * 10;
        private const int AvgFemaleHumanHeight_64 = (MaxFemaleHumanHeight_73 + MinFemaleHumanHeight_55) / 2;

        private const int BaseMaleHumanWeight_120 = 120;
        private const int BaseFemaleHumanWeight_85 = 85;
        private const int MinMaleHumanWeight_124 = BaseMaleHumanWeight_120 + 2 * 2;
        private const int MaxMaleHumanWeight_280 = BaseMaleHumanWeight_120 + 2 * 10 * 2 * 4;
        private const int AvgMaleHumanWeight_202 = (MaxMaleHumanWeight_280 + MinMaleHumanWeight_124) / 2;
        private const int MinFemaleHumanWeight_89 = BaseFemaleHumanWeight_85 + 2 * 2;
        private const int MaxFemaleHumanWeight_245 = BaseFemaleHumanWeight_85 + 2 * 10 * 2 * 4;
        private const int AvgFemaleHumanWeight_167 = (MaxFemaleHumanWeight_245 + MinFemaleHumanWeight_89) / 2;

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

            mockDice
                .Setup(d => d.Roll("(1d12-1)/12").AsSum<double>())
                .Returns(0.5);
        }

        [Test]
        public void Generate_ReturnsDemographics()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" },
                new() { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" },
                new() { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" },
                new() { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" },
                new() { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" }
            };

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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
                .Returns(["This is how I die"]);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            var lengthRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        [Test]
        public void Generate_ReturnsDemographics_WithSingleAgeCategory()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
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

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        //INFO: This emulates how Unicorns age
        [TestCase(0)]
        [TestCase(1)]
        public void Generate_ReturnsDemographics_WithScatteredAgeCategories(int index)
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
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

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(ageRolls[index].Amount + 0.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Generate_ReturnsDemographics_WithAgeCategory(int index)
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" },
                new() { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" },
                new() { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" },
                new() { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" },
                new() { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" }
            };

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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
                .Returns(["This is how I die"]);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            var lengthRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 922, RawAmount = "raw 922" },
                new() { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" },
                new() { Type = "my creature", Amount = 227, RawAmount = "raw 227" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" },
                new() { Type = "my other gender", Amount = 69, RawAmount = "raw 69" },
                new() { Type = "my creature", Amount = 420, RawAmount = "raw 420" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 123, RawAmount = "raw 123" },
                new() { Type = "my other gender", Amount = 234, RawAmount = "raw 234" },
                new() { Type = "my creature", Amount = 345, RawAmount = "raw 345" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(ageRolls[index].Amount + 0.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        private void SetupAppearance(string creature, string skin, string hair, string eyes, string other)
        {
            SetupAppearance(creature, TableNameConstants.Collection.AppearanceCategories.Skin, skin);
            SetupAppearance(creature, TableNameConstants.Collection.AppearanceCategories.Hair, hair);
            SetupAppearance(creature, TableNameConstants.Collection.AppearanceCategories.Eyes, eyes);
            SetupAppearance(creature, TableNameConstants.Collection.AppearanceCategories.Other, other);
        }

        private void SetupAppearance(string creature, string category, string appearance)
        {
            var common = new[] { $"common {category} appearance", $"other common {category} appearance" };
            var uncommon = new[] { $"uncommon {category} appearance", $"other uncommon {category} appearance" };
            var rare = new[] { $"rare {category} appearance", $"other rare {category} appearance" };
            var tableName = TableNameConstants.Collection.Appearances(category);

            mockCollectionsSelector
                .Setup(s => s.IsCollection(Config.Name, tableName, creature + Rarity.Common.ToString()))
                .Returns(true);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, tableName, creature + Rarity.Common.ToString()))
                .Returns(common);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, tableName, creature + Rarity.Uncommon.ToString()))
                .Returns(uncommon);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, tableName, creature + Rarity.Rare.ToString()))
                .Returns(rare);
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(common, uncommon, rare, null))
                .Returns(appearance);
        }

        [Test]
        public void Generate_ReturnsDemographics_WhenAgeIsGreaterThanMaximumAge()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
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

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        [Test]
        public void Generate_ReturnsDemographics_WhenAgeIsGreaterThanMaximumAge_MaximumAgeIsZero()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>();
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Adulthood, Amount = 0, RawAmount = "raw 0" });
            ageRolls.Add(new TypeAndAmountSelection { Type = AgeConstants.Categories.Maximum, Amount = 0, RawAmount = "raw 0" });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my creature"))
                .Returns(ageRolls);

            mockDice
                .Setup(d => d.Roll("(1d12-1)/12").AsSum<double>())
                .Returns(0.2022);

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => c.Single() == ageRolls[0]),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => !c.Any()),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => !c.Any()),
                    It.Is<IEnumerable<TypeAndAmountSelection>>(c => !c.Any())))
                .Returns(ageRolls[0]);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
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

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(0.2022));
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
            Assert.That(demographics.MaximumAge.Value, Is.EqualTo(0.2022));
            Assert.That(demographics.MaximumAge.Description, Is.EqualTo("This is how I die"));
            Assert.That(demographics.Weight, Is.Not.Null);
            Assert.That(demographics.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(demographics.Weight.Value, Is.EqualTo(8245 + 783 * 420));
            Assert.That(demographics.Weight.Description, Is.EqualTo("Average"));
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        [Test]
        public void Generate_ReturnsDemographics_WithShortAge()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = AgeConstants.Categories.Adulthood, Amount = 0, RawAmount = "raw 0" },
                new() { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" },
                new() { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" },
                new() { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" },
                new() { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" }
            };

            mockDice
                .Setup(d => d.Roll("(1d12-1)/12").AsSum<double>())
                .Returns(0.922);

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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
                .Returns(["This is how I die"]);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            var lengthRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(0.922));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        [Test]
        public void Generate_ReturnsDemographics_WithShortAge_NoMonthsRolled()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = AgeConstants.Categories.Adulthood, Amount = 0, RawAmount = "raw 0" },
                new() { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" },
                new() { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" },
                new() { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" },
                new() { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" }
            };

            mockDice
                .Setup(d => d.Roll("(1d12-1)/12").AsSum<double>())
                .Returns(0);

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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
                .Returns(["This is how I die"]);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            var lengthRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(1 / 12d));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        [Test]
        public void Generate_ReturnsDemographics_WithRegularAge_NoMonthsRolled()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" },
                new() { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" },
                new() { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" },
                new() { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" },
                new() { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" }
            };

            mockDice
                .Setup(d => d.Roll("(1d12-1)/12").AsSum<double>())
                .Returns(0);

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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
                .Returns(["This is how I die"]);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            var lengthRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 922, RawAmount = "raw 922" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 227, RawAmount = "raw 227" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 69, RawAmount = "raw 69" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 420, RawAmount = "raw 420" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>
            {
                new TypeAndAmountSelection { Type = "my gender", Amount = 123, RawAmount = "raw 123" },
                new TypeAndAmountSelection { Type = "my other gender", Amount = 234, RawAmount = "raw 234" },
                new TypeAndAmountSelection { Type = "my creature", Amount = 345, RawAmount = "raw 345" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
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
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
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

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
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
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
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

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
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
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
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

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        [Test]
        public void Generate_ReturnsDemographics_WithNoLength()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
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

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        [Test]
        public void Generate_ReturnsDemographics_WithNoHeight()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
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

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        [Test]
        public void Generate_ReturnsDemographics_UseLengthForWeightMultiplier()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
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

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
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
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
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

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        [Test]
        public void Generate_ReturnsDemographics_WithNoWingspan()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
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

            SetupAppearance("my creature", "my random skin", "my random hair", "my random eyes", "my random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my random other"));
        }

        [Test]
        public void Generate_ReturnsDemographics_WithGenderSpecificAppearance()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(Config.Name, TableNameConstants.Collection.Genders, "my creature"))
                .Returns("my gender");

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = AgeConstants.Categories.Adulthood, Amount = 9266, RawAmount = "raw 9266" },
                new() { Type = AgeConstants.Categories.MiddleAge, Amount = 1337, RawAmount = "raw 1337" },
                new() { Type = AgeConstants.Categories.Old, Amount = 600, RawAmount = "raw 600" },
                new() { Type = AgeConstants.Categories.Venerable, Amount = 1336, RawAmount = "raw 1336" },
                new() { Type = AgeConstants.Categories.Maximum, Amount = 90210, RawAmount = "raw 90210" }
            };

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
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.MaxAgeDescriptions, "my creature"))
                .Returns(["This is how I die"]);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            var lengthRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 922, RawAmount = "raw 922" },
                new() { Type = "my other gender", Amount = 2022, RawAmount = "raw 2022" },
                new() { Type = "my creature", Amount = 227, RawAmount = "raw 227" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 8245, RawAmount = "raw 8245" },
                new() { Type = "my other gender", Amount = 69, RawAmount = "raw 69" },
                new() { Type = "my creature", Amount = 420, RawAmount = "raw 420" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            var wingspanRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 123, RawAmount = "raw 123" },
                new() { Type = "my other gender", Amount = 234, RawAmount = "raw 234" },
                new() { Type = "my creature", Amount = 345, RawAmount = "raw 345" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my creature"))
                .Returns(wingspanRolls);

            SetupAppearance("my creaturemy gender", "my gendered random skin", "my gendered random hair", "my gendered random eyes", "my gendered random other");

            var demographics = generator.Generate("my creature");
            Assert.That(demographics, Is.Not.Null);
            Assert.That(demographics.Age, Is.Not.Null);
            Assert.That(demographics.Age.Value, Is.EqualTo(9266.5));
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
            Assert.That(demographics.Skin, Is.EqualTo("my gendered random skin"));
            Assert.That(demographics.Hair, Is.EqualTo("my gendered random hair"));
            Assert.That(demographics.Eyes, Is.EqualTo("my gendered random eyes"));
            Assert.That(demographics.Other, Is.EqualTo("my gendered random other"));
        }

        [TestCase("", "")]
        [TestCase(" ", "")]
        [TestCase("my appearance", ". ")]
        [TestCase("my appearance.", " ")]
        [TestCase("my appearance. but period in the middle", ". ")]
        [TestCase("my appearance. but period in the middle. and the end.", " ")]
        public void GetAppearanceSeparator_ReturnsExpected(string input, string expected)
        {
            var separator = DemographicsGenerator.GetAppearanceSeparator(input);
            Assert.That(separator, Is.EqualTo(expected));
        }

        private static IEnumerable AppearanceTestCases
        {
            get
            {
                yield return new TestCaseData("", false, "", "");
                yield return new TestCaseData("", false, "", "");
                yield return new TestCaseData("", false, "template appearance", "template appearance");
                yield return new TestCaseData("", true, "", "");
                yield return new TestCaseData("", true, "template appearance", "template appearance");
                yield return new TestCaseData("my appearance", false, "", "my appearance");
                yield return new TestCaseData("my appearance", false, "template appearance", "my appearance. template appearance");
                yield return new TestCaseData("my appearance", true, "", "");
                yield return new TestCaseData("my appearance", true, "template appearance", "template appearance");
                yield return new TestCaseData("my appearance.", false, "", "my appearance.");
                yield return new TestCaseData("my appearance.", false, "template appearance", "my appearance. template appearance");
                yield return new TestCaseData("my appearance.", true, "", "");
                yield return new TestCaseData("my appearance.", true, "template appearance", "template appearance");
            }
        }

        [TestCaseSource(nameof(AppearanceTestCases))]
        public void Update_UpdatesSkin(string appearance, bool overwrite, string templateAppearance, string expected)
        {
            SetupTemplateDefaults("my template");
            SetupAppearance("my template", TableNameConstants.Collection.AppearanceCategories.Skin, templateAppearance);

            var demographics = new Demographics
            {
                Skin = appearance
            };

            var updated = generator.Update(demographics, "my creature", "my template", overwriteAppearance: overwrite);
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Skin, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(AppearanceTestCases))]
        public void Update_UpdatesHair(string appearance, bool overwrite, string templateAppearance, string expected)
        {
            SetupTemplateDefaults("my template");
            SetupAppearance("my template", TableNameConstants.Collection.AppearanceCategories.Hair, templateAppearance);

            var demographics = new Demographics
            {
                Hair = appearance
            };

            var updated = generator.Update(demographics, "my creature", "my template", overwriteAppearance: overwrite);
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Hair, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(AppearanceTestCases))]
        public void Update_UpdatesEyes(string appearance, bool overwrite, string templateAppearance, string expected)
        {
            SetupTemplateDefaults("my template");
            SetupAppearance("my template", TableNameConstants.Collection.AppearanceCategories.Eyes, templateAppearance);

            var demographics = new Demographics
            {
                Eyes = appearance
            };

            var updated = generator.Update(demographics, "my creature", "my template", overwriteAppearance: overwrite);
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Eyes, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(AppearanceTestCases))]
        public void Update_UpdatesOther(string appearance, bool overwrite, string templateAppearance, string expected)
        {
            SetupTemplateDefaults("my template");
            SetupAppearance("my template", TableNameConstants.Collection.AppearanceCategories.Other, templateAppearance);

            var demographics = new Demographics
            {
                Other = appearance
            };

            var updated = generator.Update(demographics, "my creature", "my template", overwriteAppearance: overwrite);
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Other, Is.EqualTo(expected));
        }

        [Test]
        public void Update_UpdatesAppearances()
        {
            SetupTemplateDefaults("my template");
            SetupAppearance("my template", "template skin", "template hair", "template eyes", "template other");

            var demographics = new Demographics
            {
                Skin = "my skin",
                Hair = "my hair",
                Eyes = "my eyes",
                Other = "my other",
            };

            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Skin, Is.EqualTo("my skin. template skin"));
            Assert.That(updated.Hair, Is.EqualTo("my hair. template hair"));
            Assert.That(updated.Eyes, Is.EqualTo("my eyes. template eyes"));
            Assert.That(updated.Other, Is.EqualTo("my other. template other"));
        }

        private void SetupTemplateDefaults(string template)
        {
            SetupAppearance(template, string.Empty, string.Empty, string.Empty, string.Empty);

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = AgeConstants.Categories.Adulthood, Amount = 0, RawAmount = "raw 0" },
                new() { Type = AgeConstants.Categories.Maximum, Amount = 0, RawAmount = "raw 0" }
            };
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, template))
                .Returns(ageRolls);

            var templateHeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 0, RawAmount = "raw 0" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my template"))
                .Returns(templateHeights);

            var templateLengths = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 0, RawAmount = "raw 0" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my template"))
                .Returns(templateLengths);

            var templateWeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 0, RawAmount = "raw 0" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my template"))
                .Returns(templateWeights);
        }

        [Test]
        public void Update_UpdatesAge_Multiplier()
        {
            SetupTemplateDefaults("my template");

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = AgeConstants.Categories.Multiplier, Amount = 42, RawAmount = "raw 42" },
                new() { Type = AgeConstants.Categories.Maximum, Amount = 0, RawAmount = "raw 0" }
            };
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my template"))
                .Returns(ageRolls);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
            };

            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Age.Value, Is.EqualTo(9266.90210 * 42));
            Assert.That(updated.Age.Description, Is.EqualTo("my age category"));
        }

        [Test]
        public void Update_UpdatesAge_Additive()
        {
            SetupTemplateDefaults("my template");

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "template age category", Amount = 42, RawAmount = "raw 42" },
                new() { Type = AgeConstants.Categories.Maximum, Amount = 0, RawAmount = "raw 0" }
            };
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my template"))
                .Returns(ageRolls);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
            };

            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Age.Value, Is.EqualTo(9266.90210 + 42));
            Assert.That(updated.Age.Description, Is.EqualTo("template age category"));
        }

        [Test]
        public void Update_UpdatesMaxAge_Multiplier()
        {
            SetupTemplateDefaults("my template");

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = AgeConstants.Categories.Multiplier, Amount = 42, RawAmount = "raw 42" },
                new() { Type = AgeConstants.Categories.Maximum, Amount = 0, RawAmount = "raw 0" }
            };
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my template"))
                .Returns(ageRolls);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
            };

            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.MaximumAge.Value, Is.EqualTo(600 * 42));
            Assert.That(updated.MaximumAge.Description, Is.EqualTo("gonna die"));
        }

        [Test]
        public void Update_UpdatesMaxAge_Ageless()
        {
            SetupTemplateDefaults("my template");

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "template age category", Amount = 42, RawAmount = "raw 42" },
                new() { Type = AgeConstants.Categories.Maximum, Amount = AgeConstants.Ageless, RawAmount = "raw ageless" }
            };
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my template"))
                .Returns(ageRolls);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
            };

            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.MaximumAge.Value, Is.EqualTo(AgeConstants.Ageless));
            Assert.That(updated.MaximumAge.Description, Is.EqualTo("template age category"));
        }

        [Test]
        public void Update_AddsWingspan_ByHeight()
        {
            SetupTemplateDefaults("my template");

            var wingspanRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 123, RawAmount = "raw 123" },
                new() { Type = "my other gender", Amount = 234, RawAmount = "raw 234" },
                new() { Type = "my creature", Amount = 345, RawAmount = "raw 345" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(wingspanRolls);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 1337 },
                Length = new Measurement("inches") { Value = 42 },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template", true);
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Wingspan, Is.Not.Null);
            Assert.That(updated.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(updated.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Wingspan.Description, Is.EqualTo("Average"));
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
        public void Update_AddsWingspan_WithDescription(int roll, string description, int index)
        {
            SetupTemplateDefaults("my template");

            var wingspanRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 123, RawAmount = "raw 123" },
                new() { Type = "my other gender", Amount = 234, RawAmount = "raw 234" },
                new() { Type = "my creature", Amount = roll, RawAmount = "raw 345" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(wingspanRolls);

            mockDice
                .Setup(d => d.Describe("raw 123+raw 345", 123 + roll, It.IsAny<string[]>()))
                .Returns((string r, int v, string[] descriptions) => descriptions[index]);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 1337 },
                Length = new Measurement("inches") { Value = 42 },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template", true);
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Wingspan, Is.Not.Null);
            Assert.That(updated.Wingspan.Value, Is.EqualTo(123 + roll));
            Assert.That(updated.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Wingspan.Description, Is.EqualTo(description));
        }

        [Test]
        public void Update_AddsWingspan_ByLength()
        {
            SetupTemplateDefaults("my template");

            var wingspanRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 123, RawAmount = "raw 123" },
                new() { Type = "my other gender", Amount = 234, RawAmount = "raw 234" },
                new() { Type = "my creature", Amount = 345, RawAmount = "raw 345" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(wingspanRolls);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 42 },
                Length = new Measurement("inches") { Value = 1337 },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template", true);
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Wingspan, Is.Not.Null);
            Assert.That(updated.Wingspan.Value, Is.EqualTo(123 + 345));
            Assert.That(updated.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Wingspan.Description, Is.EqualTo("Average"));
        }

        [Test]
        public void Update_DoNotAddWingspan()
        {
            SetupTemplateDefaults("my template");

            var wingspanRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my size", Amount = 123, RawAmount = "raw 123" },
                new() { Type = "my other base key", Amount = 234, RawAmount = "raw 234" },
                new() { Type = "my template", Amount = 345, RawAmount = "raw 345" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my template"))
                .Returns(wingspanRolls);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
            };
            var updated = generator.Update(demographics, "my creature", "my template", false);
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Wingspan, Is.Not.Null);
            Assert.That(updated.Wingspan.Value, Is.Zero);
            Assert.That(updated.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Wingspan.Description, Is.EqualTo(""));
        }

        [Test]
        public void Update_DoNotAddWingspan_AlreadyHasWingspan()
        {
            SetupTemplateDefaults("my template");

            var wingspanRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my size", Amount = 123, RawAmount = "raw 123" },
                new() { Type = "my other base key", Amount = 234, RawAmount = "raw 234" },
                new() { Type = "my template", Amount = 345, RawAmount = "raw 345" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my template"))
                .Returns(wingspanRolls);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Wingspan = new Measurement("inches") { Value = 1336, Description = "impressive" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template", true);
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Wingspan, Is.Not.Null);
            Assert.That(updated.Wingspan.Value, Is.EqualTo(1336));
            Assert.That(updated.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Wingspan.Description, Is.EqualTo("impressive"));
        }

        [Test]
        public void Update_UpdatesHeight_NoChange()
        {
            SetupTemplateDefaults("my template");

            var templateHeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 0, RawAmount = "raw 0" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my template"))
                .Returns(templateHeights);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 96, Description = "eh" },
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Height, Is.Not.Null);
            Assert.That(updated.Height.Value, Is.EqualTo(96));
            Assert.That(updated.Height.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Height.Description, Is.EqualTo("eh"));
        }

        [Test]
        public void Update_UpdatesHeight_NoChange_NoHeight()
        {
            SetupTemplateDefaults("my template");

            var templateHeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 1, RawAmount = "raw 0" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my template"))
                .Returns(templateHeights);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 0, Description = "eh" },
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Height, Is.Not.Null);
            Assert.That(updated.Height.Value, Is.EqualTo(0));
            Assert.That(updated.Height.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Height.Description, Is.EqualTo("eh"));
        }

        [Test]
        public void Update_UpdatesHeight_Increase()
        {
            SetupTemplateDefaults("my template");

            var templateHeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my template"))
                .Returns(templateHeights);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42+raw 783").AsPotentialMaximum<int>(true))
                .Returns(42 + 783);
            mockDice
                .Setup(d => d.Describe("raw 42+raw 783", 96 + 364, It.IsAny<string[]>()))
                .Returns("eh+");

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 96, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Height, Is.Not.Null);
            Assert.That(updated.Height.Value, Is.EqualTo(96 + 364));
            Assert.That(updated.Height.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Height.Description, Is.EqualTo("eh+"));
        }

        [TestCase(MinMaleHumanHeight_60, MaxMaleHumanHeight_78, AvgMaleHumanHeight_69)]
        [TestCase(MinMaleHumanHeight_60 + 1, MaxMaleHumanHeight_78, 69)]
        [TestCase(65, MaxMaleHumanHeight_78, 71)]
        [TestCase(70, MaxMaleHumanHeight_78, 74)]
        [TestCase(75, MaxMaleHumanHeight_78, 77)]
        [TestCase(MaxMaleHumanHeight_78 - 1, MaxMaleHumanHeight_78, 77)]
        [TestCase(MaxMaleHumanHeight_78, MaxMaleHumanHeight_78, MaxMaleHumanHeight_78)]
        [TestCase(MinFemaleHumanHeight_55, MaxFemaleHumanHeight_73, 64)]
        [TestCase(MinFemaleHumanHeight_55 + 1, MaxFemaleHumanHeight_73, 64)]
        [TestCase(60, MaxFemaleHumanHeight_73, 66)]
        [TestCase(65, MaxFemaleHumanHeight_73, 69)]
        [TestCase(70, MaxFemaleHumanHeight_73, 72)]
        [TestCase(MaxFemaleHumanHeight_73 - 1, MaxFemaleHumanHeight_73, 72)]
        [TestCase(MaxFemaleHumanHeight_73, MaxFemaleHumanHeight_73, MaxFemaleHumanHeight_73)]
        public void Update_UpdatesHeight_Increase_Human(int height, int max, int expectedHeight)
        {
            SetupTemplateDefaults("my template");

            var templateHeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my template"))
                .Returns(templateHeights);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", RawAmount = "raw 42" },
                new() { Type = "my creature", RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42+raw 783").AsPotentialMaximum<int>(true))
                .Returns(max);
            mockDice
                .Setup(d => d.Describe("raw 42+raw 783", expectedHeight, It.IsAny<string[]>()))
                .Returns("eh+");

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = height, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Height, Is.Not.Null);
            Assert.That(updated.Height.Value, Is.EqualTo(expectedHeight));
            Assert.That(updated.Height.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Height.Description, Is.EqualTo("eh+"));
        }

        [Test]
        public void Update_UpdatesHeight_Decrease()
        {
            SetupTemplateDefaults("my template");

            var templateHeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = -1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my template"))
                .Returns(templateHeights);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42+raw 783").AsPotentialMinimum<int>())
                .Returns(42 + 7);
            mockDice
                .Setup(d => d.Describe("raw 42+raw 783", 96 - 24, It.IsAny<string[]>()))
                .Returns("eh-");

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 96, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Height, Is.Not.Null);
            Assert.That(updated.Height.Value, Is.EqualTo(96 - 24));
            Assert.That(updated.Height.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Height.Description, Is.EqualTo("eh-"));
        }

        [TestCase(MinMaleHumanHeight_60, MinMaleHumanHeight_60, MinMaleHumanHeight_60)]
        [TestCase(MinMaleHumanHeight_60 + 1, MinMaleHumanHeight_60, 61)]
        [TestCase(65, MinMaleHumanHeight_60, 63)]
        [TestCase(70, MinMaleHumanHeight_60, 65)]
        [TestCase(75, MinMaleHumanHeight_60, 67)]
        [TestCase(MaxMaleHumanHeight_78 - 1, MinMaleHumanHeight_60, 69)]
        [TestCase(MaxMaleHumanHeight_78, MinMaleHumanHeight_60, AvgMaleHumanHeight_69)]
        [TestCase(MinFemaleHumanHeight_55, MinFemaleHumanHeight_55, MinFemaleHumanHeight_55)]
        [TestCase(MinFemaleHumanHeight_55 + 1, MinFemaleHumanHeight_55, 56)]
        [TestCase(60, MinFemaleHumanHeight_55, 58)]
        [TestCase(65, MinFemaleHumanHeight_55, 60)]
        [TestCase(70, MinFemaleHumanHeight_55, 62)]
        [TestCase(MaxFemaleHumanHeight_73 - 1, MinFemaleHumanHeight_55, 64)]
        [TestCase(MaxFemaleHumanHeight_73, MinFemaleHumanHeight_55, AvgFemaleHumanHeight_64)]
        public void Update_UpdatesHeight_Decrease_Human(int height, int min, int expectedHeight)
        {
            SetupTemplateDefaults("my template");

            var templateHeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = -1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my template"))
                .Returns(templateHeights);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", RawAmount = "raw 42" },
                new() { Type = "my creature", RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42+raw 783").AsPotentialMinimum<int>())
                .Returns(min);
            mockDice
                .Setup(d => d.Describe("raw 42+raw 783", expectedHeight, It.IsAny<string[]>()))
                .Returns("eh-");

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = height, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Height, Is.Not.Null);
            Assert.That(updated.Height.Value, Is.EqualTo(expectedHeight));
            Assert.That(updated.Height.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Height.Description, Is.EqualTo("eh-"));
        }

        [Test]
        public void Update_UpdatesLength_NoChange()
        {
            SetupTemplateDefaults("my template");

            var templateLengths = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 0, RawAmount = "raw 0" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my template"))
                .Returns(templateLengths);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Length = new Measurement("inches") { Value = 96, Description = "eh" },
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Length, Is.Not.Null);
            Assert.That(updated.Length.Value, Is.EqualTo(96));
            Assert.That(updated.Length.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Length.Description, Is.EqualTo("eh"));
        }

        [Test]
        public void Update_UpdatesLength_NoChange_NoLength()
        {
            SetupTemplateDefaults("my template");

            var templateLengths = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 1, RawAmount = "raw 0" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my template"))
                .Returns(templateLengths);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Length = new Measurement("inches") { Value = 0, Description = "eh" },
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Length, Is.Not.Null);
            Assert.That(updated.Length.Value, Is.EqualTo(0));
            Assert.That(updated.Length.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Length.Description, Is.EqualTo("eh"));
        }

        [Test]
        public void Update_UpdatesLength_Increase()
        {
            SetupTemplateDefaults("my template");

            var templateLengths = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my template"))
                .Returns(templateLengths);

            var lengthRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            mockDice
                .Setup(d => d.Roll("raw 42+raw 783").AsPotentialMaximum<int>(true))
                .Returns(42 + 783);
            mockDice
                .Setup(d => d.Describe("raw 42+raw 783", 96 + 364, It.IsAny<string[]>()))
                .Returns("eh+");

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Length = new Measurement("inches") { Value = 96, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Length, Is.Not.Null);
            Assert.That(updated.Length.Value, Is.EqualTo(96 + 364));
            Assert.That(updated.Length.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Length.Description, Is.EqualTo("eh+"));
        }

        [Test]
        public void Update_UpdatesLength_Decrease()
        {
            SetupTemplateDefaults("my template");

            var templateLengths = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = -1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my template"))
                .Returns(templateLengths);

            var lengthRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            mockDice
                .Setup(d => d.Roll("raw 42+raw 783").AsPotentialMinimum<int>())
                .Returns(42 + 7);
            mockDice
                .Setup(d => d.Describe("raw 42+raw 783", 96 - 24, It.IsAny<string[]>()))
                .Returns("eh-");

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Length = new Measurement("inches") { Value = 96, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Length, Is.Not.Null);
            Assert.That(updated.Length.Value, Is.EqualTo(96 - 24));
            Assert.That(updated.Length.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Length.Description, Is.EqualTo("eh-"));
        }

        [Test]
        public void Update_UpdatesWeight_NoChange()
        {
            SetupTemplateDefaults("my template");

            var templateWeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 0, RawAmount = "raw 0" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my template"))
                .Returns(templateWeights);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Weight = new Measurement("pounds") { Value = 96, Description = "eh" },
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Weight, Is.Not.Null);
            Assert.That(updated.Weight.Value, Is.EqualTo(96));
            Assert.That(updated.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(updated.Weight.Description, Is.EqualTo("eh"));
        }

        [Test]
        public void Update_UpdatesWeight_NoChange_NoWeight()
        {
            SetupTemplateDefaults("my template");

            var templateWeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 1, RawAmount = "raw 0" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my template"))
                .Returns(templateWeights);

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Weight = new Measurement("pounds") { Value = 0, Description = "eh" },
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Weight, Is.Not.Null);
            Assert.That(updated.Weight.Value, Is.EqualTo(0));
            Assert.That(updated.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(updated.Weight.Description, Is.EqualTo("eh"));
        }

        [Test]
        public void Update_UpdatesWeight_Increase_ByHeight()
        {
            SetupTemplateDefaults("my template");

            var templateWeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my template"))
                .Returns(templateWeights);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 45, RawAmount = "raw 42" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42+37*raw 783").AsPotentialMaximum<int>(true))
                .Returns(42 + 37 * 783);
            mockDice
                .Setup(d => d.Describe("raw 42+37*raw 783", 96 + 14458, It.IsAny<string[]>()))
                .Returns("eh+");

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 82, Description = "eh" },
                Length = new Measurement("inches") { Value = 9, Description = "eh" },
                Weight = new Measurement("pounds") { Value = 96, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Weight, Is.Not.Null);
            Assert.That(updated.Weight.Value, Is.EqualTo(96 + 14458));
            Assert.That(updated.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(updated.Weight.Description, Is.EqualTo("eh+"));
        }

        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, MinMaleHumanWeight_124, MaxMaleHumanWeight_280, AvgMaleHumanWeight_202)]
        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, MinMaleHumanWeight_124 + 1, MaxMaleHumanWeight_280, AvgMaleHumanWeight_202 + 1)]
        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, AvgMaleHumanWeight_202 - 1, MaxMaleHumanWeight_280, 241)]
        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, AvgMaleHumanWeight_202, MaxMaleHumanWeight_280, 241)]
        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, AvgMaleHumanWeight_202 + 1, MaxMaleHumanWeight_280, 241)]
        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, MaxMaleHumanWeight_280 - 1, MaxMaleHumanWeight_280, MaxMaleHumanWeight_280 - 1)]
        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, MaxMaleHumanWeight_280, MaxMaleHumanWeight_280, MaxMaleHumanWeight_280)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, MinMaleHumanWeight_124, MaxMaleHumanWeight_280, AvgMaleHumanWeight_202)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, MinMaleHumanWeight_124 + 1, MaxMaleHumanWeight_280, AvgMaleHumanWeight_202 + 1)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, AvgMaleHumanWeight_202 - 1, MaxMaleHumanWeight_280, 241)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, AvgMaleHumanWeight_202, MaxMaleHumanWeight_280, 241)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, AvgMaleHumanWeight_202 + 1, MaxMaleHumanWeight_280, 241)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, MaxMaleHumanWeight_280 - 1, MaxMaleHumanWeight_280, MaxMaleHumanWeight_280 - 1)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, MaxMaleHumanWeight_280, MaxMaleHumanWeight_280, MaxMaleHumanWeight_280)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, MinMaleHumanWeight_124, MaxMaleHumanWeight_280, AvgMaleHumanWeight_202)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, MinMaleHumanWeight_124 + 1, MaxMaleHumanWeight_280, AvgMaleHumanWeight_202 + 1)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, AvgMaleHumanWeight_202 - 1, MaxMaleHumanWeight_280, 241)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, AvgMaleHumanWeight_202, MaxMaleHumanWeight_280, 241)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, AvgMaleHumanWeight_202 + 1, MaxMaleHumanWeight_280, 241)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, MaxMaleHumanWeight_280 - 1, MaxMaleHumanWeight_280, MaxMaleHumanWeight_280 - 1)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, MaxMaleHumanWeight_280, MaxMaleHumanWeight_280, MaxMaleHumanWeight_280)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, MinFemaleHumanWeight_89, MaxFemaleHumanWeight_245, AvgFemaleHumanWeight_167)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, MinFemaleHumanWeight_89 + 1, MaxFemaleHumanWeight_245, AvgFemaleHumanWeight_167 + 1)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, AvgFemaleHumanWeight_167 - 1, MaxFemaleHumanWeight_245, 206)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, AvgFemaleHumanWeight_167, MaxFemaleHumanWeight_245, 206)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, AvgFemaleHumanWeight_167 + 1, MaxFemaleHumanWeight_245, 206)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, MaxFemaleHumanWeight_245 - 1, MaxFemaleHumanWeight_245, MaxFemaleHumanWeight_245 - 1)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, MaxFemaleHumanWeight_245, MaxFemaleHumanWeight_245, MaxFemaleHumanWeight_245)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, MinFemaleHumanWeight_89, MaxFemaleHumanWeight_245, AvgFemaleHumanWeight_167)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, MinFemaleHumanWeight_89 + 1, MaxFemaleHumanWeight_245, AvgFemaleHumanWeight_167 + 1)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, AvgFemaleHumanWeight_167 - 1, MaxFemaleHumanWeight_245, 206)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, AvgFemaleHumanWeight_167, MaxFemaleHumanWeight_245, 206)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, AvgFemaleHumanWeight_167 + 1, MaxFemaleHumanWeight_245, 206)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, MaxFemaleHumanWeight_245 - 1, MaxFemaleHumanWeight_245, MaxFemaleHumanWeight_245 - 1)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, MaxFemaleHumanWeight_245, MaxFemaleHumanWeight_245, MaxFemaleHumanWeight_245)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, MinFemaleHumanWeight_89, MaxFemaleHumanWeight_245, AvgFemaleHumanWeight_167)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, MinFemaleHumanWeight_89 + 1, MaxFemaleHumanWeight_245, AvgFemaleHumanWeight_167 + 1)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, AvgFemaleHumanWeight_167 - 1, MaxFemaleHumanWeight_245, 206)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, AvgFemaleHumanWeight_167, MaxFemaleHumanWeight_245, 206)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, AvgFemaleHumanWeight_167 + 1, MaxFemaleHumanWeight_245, 206)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, MaxFemaleHumanWeight_245 - 1, MaxFemaleHumanWeight_245, MaxFemaleHumanWeight_245 - 1)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, MaxFemaleHumanWeight_245, MaxFemaleHumanWeight_245, MaxFemaleHumanWeight_245)]
        public void Update_UpdatesWeight_Increase_Human(int baseHeight, int height, int weight, int max, int expectedWeight)
        {
            SetupTemplateDefaults("my template");

            var templateWeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my template"))
                .Returns(templateWeights);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = baseHeight, RawAmount = "raw 42" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            mockDice
                .Setup(d => d.Roll($"raw 42+{height - baseHeight}*raw 783").AsPotentialMaximum<int>(true))
                .Returns(max);
            mockDice
                .Setup(d => d.Describe($"raw 42+{height - baseHeight}*raw 783", expectedWeight, It.IsAny<string[]>()))
                .Returns("eh+");

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = height, Description = "eh" },
                Weight = new Measurement("pounds") { Value = weight, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Weight, Is.Not.Null);
            Assert.That(updated.Weight.Value, Is.EqualTo(expectedWeight));
            Assert.That(updated.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(updated.Weight.Description, Is.EqualTo("eh+"));
        }

        [Test]
        public void Update_UpdatesWeight_Increase_ByLength()
        {
            SetupTemplateDefaults("my template");

            var templateWeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my template"))
                .Returns(templateWeights);

            var lengthRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 45, RawAmount = "raw 42" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42+37*raw 783").AsPotentialMaximum<int>(true))
                .Returns(42 + 37 * 783);
            mockDice
                .Setup(d => d.Describe("raw 42+37*raw 783", 96 + 14458, It.IsAny<string[]>()))
                .Returns("eh+");

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 9, Description = "eh" },
                Length = new Measurement("inches") { Value = 82, Description = "eh" },
                Weight = new Measurement("pounds") { Value = 96, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Weight, Is.Not.Null);
            Assert.That(updated.Weight.Value, Is.EqualTo(96 + 14458));
            Assert.That(updated.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(updated.Weight.Description, Is.EqualTo("eh+"));
        }

        [Test]
        public void Update_UpdatesWeight_Decrease_ByHeight()
        {
            SetupTemplateDefaults("my template");

            var templateWeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = -1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my template"))
                .Returns(templateWeights);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 45, RawAmount = "raw 42" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42+37*raw 783").AsPotentialMinimum<int>())
                .Returns(42 + 37);
            mockDice
                .Setup(d => d.Describe("raw 42+37*raw 783", 96 - 8, It.IsAny<string[]>()))
                .Returns("eh-");

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 82, Description = "eh" },
                Length = new Measurement("inches") { Value = 9, Description = "eh" },
                Weight = new Measurement("pounds") { Value = 96, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Weight, Is.Not.Null);
            Assert.That(updated.Weight.Value, Is.EqualTo(96 - 8));
            Assert.That(updated.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(updated.Weight.Description, Is.EqualTo("eh-"));
        }

        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, MinMaleHumanWeight_124, MinMaleHumanWeight_124, MinMaleHumanWeight_124)]
        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, MinMaleHumanWeight_124 + 1, MinMaleHumanWeight_124, MinMaleHumanWeight_124 + 1)]
        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, AvgMaleHumanWeight_202 - 1, MinMaleHumanWeight_124, 241)]
        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, AvgMaleHumanWeight_202, MinMaleHumanWeight_124, 241)]
        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, AvgMaleHumanWeight_202 + 1, MinMaleHumanWeight_124, 241)]
        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, MaxMaleHumanWeight_280 - 1, MinMaleHumanWeight_124, AvgMaleHumanWeight_202 - 1)]
        [TestCase(BaseMaleHumanHeight_58, MinMaleHumanHeight_60, MaxMaleHumanWeight_280, MinMaleHumanWeight_124, AvgMaleHumanWeight_202)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, MinMaleHumanWeight_124, MinMaleHumanWeight_124, MinMaleHumanWeight_124)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, MinMaleHumanWeight_124 + 1, MinMaleHumanWeight_124, MinMaleHumanWeight_124 + 1)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, AvgMaleHumanWeight_202 - 1, MinMaleHumanWeight_124, 241)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, AvgMaleHumanWeight_202, MinMaleHumanWeight_124, 241)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, AvgMaleHumanWeight_202 + 1, MinMaleHumanWeight_124, 241)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, MaxMaleHumanWeight_280 - 1, MinMaleHumanWeight_124, AvgMaleHumanWeight_202 - 1)]
        [TestCase(BaseMaleHumanHeight_58, AvgMaleHumanHeight_69, MaxMaleHumanWeight_280, MinMaleHumanWeight_124, AvgMaleHumanWeight_202)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, MinMaleHumanWeight_124, MinMaleHumanWeight_124, MinMaleHumanWeight_124)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, MinMaleHumanWeight_124 + 1, MinMaleHumanWeight_124, MinMaleHumanWeight_124 + 1)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, AvgMaleHumanWeight_202 - 1, MinMaleHumanWeight_124, 241)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, AvgMaleHumanWeight_202, MinMaleHumanWeight_124, 241)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, AvgMaleHumanWeight_202 + 1, MinMaleHumanWeight_124, 241)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, MaxMaleHumanWeight_280 - 1, MinMaleHumanWeight_124, AvgMaleHumanWeight_202 - 1)]
        [TestCase(BaseMaleHumanHeight_58, MaxMaleHumanHeight_78, MaxMaleHumanWeight_280, MinMaleHumanWeight_124, AvgMaleHumanWeight_202)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, MinFemaleHumanWeight_89, MinFemaleHumanWeight_89, MinFemaleHumanWeight_89)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, MinFemaleHumanWeight_89 + 1, MinFemaleHumanWeight_89, MinFemaleHumanWeight_89 + 1)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, AvgFemaleHumanWeight_167 - 1, MinFemaleHumanWeight_89, 206)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, AvgFemaleHumanWeight_167, MinFemaleHumanWeight_89, 206)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, AvgFemaleHumanWeight_167 + 1, MinFemaleHumanWeight_89, 206)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, MaxFemaleHumanWeight_245 - 1, MinFemaleHumanWeight_89, AvgFemaleHumanWeight_167 - 1)]
        [TestCase(BaseFemaleHumanHeight_53, MinFemaleHumanHeight_55, MaxFemaleHumanWeight_245, MinFemaleHumanWeight_89, AvgFemaleHumanWeight_167)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, MinFemaleHumanWeight_89, MinFemaleHumanWeight_89, MinFemaleHumanWeight_89)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, MinFemaleHumanWeight_89 + 1, MinFemaleHumanWeight_89, MinFemaleHumanWeight_89 + 1)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, AvgFemaleHumanWeight_167 - 1, MinFemaleHumanWeight_89, 206)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, AvgFemaleHumanWeight_167, MinFemaleHumanWeight_89, 206)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, AvgFemaleHumanWeight_167 + 1, MinFemaleHumanWeight_89, 206)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, MaxFemaleHumanWeight_245 - 1, MinFemaleHumanWeight_89, AvgFemaleHumanWeight_167 - 1)]
        [TestCase(BaseFemaleHumanHeight_53, AvgFemaleHumanHeight_64, MaxFemaleHumanWeight_245, MinFemaleHumanWeight_89, AvgFemaleHumanWeight_167)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, MinFemaleHumanWeight_89, MinFemaleHumanWeight_89, MinFemaleHumanWeight_89)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, MinFemaleHumanWeight_89 + 1, MinFemaleHumanWeight_89, MinFemaleHumanWeight_89 + 1)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, AvgFemaleHumanWeight_167 - 1, MinFemaleHumanWeight_89, 206)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, AvgFemaleHumanWeight_167, MinFemaleHumanWeight_89, 206)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, AvgFemaleHumanWeight_167 + 1, MinFemaleHumanWeight_89, 206)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, MaxFemaleHumanWeight_245 - 1, MinFemaleHumanWeight_89, AvgFemaleHumanWeight_167 + 1)]
        [TestCase(BaseFemaleHumanHeight_53, MaxFemaleHumanHeight_73, MaxFemaleHumanWeight_245, MinFemaleHumanWeight_89, AvgFemaleHumanWeight_167)]
        public void Update_UpdatesWeight_Decrease_Human(int baseHeight, int height, int weight, int min, int expectedWeight)
        {
            SetupTemplateDefaults("my template");

            var templateWeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = -1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my template"))
                .Returns(templateWeights);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 45, RawAmount = "raw 42" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42+37*raw 783").AsPotentialMinimum<int>())
                .Returns(42 + 37);
            mockDice
                .Setup(d => d.Describe("raw 42+37*raw 783", 96 - 8, It.IsAny<string[]>()))
                .Returns("eh-");

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 82, Description = "eh" },
                Length = new Measurement("inches") { Value = 9, Description = "eh" },
                Weight = new Measurement("pounds") { Value = 96, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Weight, Is.Not.Null);
            Assert.That(updated.Weight.Value, Is.EqualTo(96 - 8));
            Assert.That(updated.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(updated.Weight.Description, Is.EqualTo("eh-"));
        }

        [Test]
        public void Update_UpdatesWeight_Decrease_ByLength()
        {
            SetupTemplateDefaults("my template");

            var templateWeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = -1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my template"))
                .Returns(templateWeights);

            var lengthRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 45, RawAmount = "raw 42" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my other gender", Amount = 96, RawAmount = "raw 96" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42+37*raw 783").AsPotentialMinimum<int>())
                .Returns(42 + 37);
            mockDice
                .Setup(d => d.Describe("raw 42+37*raw 783", 96 - 8, It.IsAny<string[]>()))
                .Returns("eh-");

            var demographics = new Demographics
            {
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 9, Description = "eh" },
                Length = new Measurement("inches") { Value = 82, Description = "eh" },
                Weight = new Measurement("pounds") { Value = 96, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template");
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Weight, Is.Not.Null);
            Assert.That(updated.Weight.Value, Is.EqualTo(96 - 8));
            Assert.That(updated.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(updated.Weight.Description, Is.EqualTo("eh-"));
        }

        [Test]
        public void Update_AllProperties()
        {
            SetupTemplateDefaults("my template");
            SetupAppearance("my template", "template skin", "template hair", "template eyes", "template other");

            var wingspanRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my size", Amount = 2022, RawAmount = "raw 2022" },
                new() { Type = "my template", Amount = 227, RawAmount = "raw 227" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Wingspans, "my template"))
                .Returns(wingspanRolls);

            var ageRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "template age category", Amount = 42, RawAmount = "raw 42" },
                new() { Type = AgeConstants.Categories.Maximum, Amount = AgeConstants.Ageless, RawAmount = "raw ageless" }
            };
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.AgeRolls, "my template"))
                .Returns(ageRolls);

            var templateHeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my template"))
                .Returns(templateHeights);

            var heightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 42, RawAmount = "raw 42" },
                new() { Type = "my creature", Amount = 783, RawAmount = "raw 783" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Heights, "my creature"))
                .Returns(heightRolls);

            mockDice
                .Setup(d => d.Roll("raw 42+raw 783").AsPotentialMaximum<int>(true))
                .Returns(42 + 783);
            mockDice
                .Setup(d => d.Describe("raw 42+raw 783", 96 + 364, It.IsAny<string[]>()))
                .Returns("eh+");

            var templateLengths = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = -1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my template"))
                .Returns(templateLengths);

            var lengthRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 9, RawAmount = "raw 9" },
                new() { Type = "my creature", Amount = 22, RawAmount = "raw 22" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Lengths, "my creature"))
                .Returns(lengthRolls);

            mockDice
                .Setup(d => d.Roll("raw 9+raw 22").AsPotentialMinimum<int>())
                .Returns(9 + 2);
            mockDice
                .Setup(d => d.Describe("raw 9+raw 22", 82 - 36, It.IsAny<string[]>()))
                .Returns("eh-");

            var templateWeights = new List<TypeAndAmountSelection>
            {
                new() { Type = "my template", Amount = 1, RawAmount = "raw 1" },
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my template"))
                .Returns(templateWeights);

            var weightRolls = new List<TypeAndAmountSelection>
            {
                new() { Type = "my gender", Amount = 123, RawAmount = "raw 123" },
                new() { Type = "my creature", Amount = 234, RawAmount = "raw 234" }
            };

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Weights, "my creature"))
                .Returns(weightRolls);

            mockDice
                .Setup(d => d.Roll("raw 123+418*raw 234").AsPotentialMaximum<int>(true))
                .Returns(123 + 37 * 234);
            mockDice
                .Setup(d => d.Describe("raw 123+418*raw 234", 4413, It.IsAny<string[]>()))
                .Returns("eh++");

            var demographics = new Demographics
            {
                Skin = "my skin",
                Hair = "my hair",
                Eyes = "my eyes",
                Other = "my other",
                Age = new Measurement("years") { Value = 9266.90210, Description = "my age category" },
                MaximumAge = new Measurement("years") { Value = 600, Description = "gonna die" },
                Height = new Measurement("inches") { Value = 96, Description = "eh" },
                Length = new Measurement("inches") { Value = 82, Description = "eh" },
                Weight = new Measurement("pounds") { Value = 45, Description = "eh" },
                Gender = "my gender",
            };
            var updated = generator.Update(demographics, "my creature", "my template", true);
            Assert.That(updated, Is.EqualTo(demographics));
            Assert.That(updated.Skin, Is.EqualTo("my skin. template skin"));
            Assert.That(updated.Hair, Is.EqualTo("my hair. template hair"));
            Assert.That(updated.Eyes, Is.EqualTo("my eyes. template eyes"));
            Assert.That(updated.Other, Is.EqualTo("my other. template other"));
            Assert.That(updated.Age.Value, Is.EqualTo(9266.90210 + 42));
            Assert.That(updated.Age.Description, Is.EqualTo("template age category"));
            Assert.That(updated.MaximumAge.Value, Is.EqualTo(AgeConstants.Ageless));
            Assert.That(updated.MaximumAge.Description, Is.EqualTo("template age category"));
            Assert.That(updated.Wingspan, Is.Not.Null);
            Assert.That(updated.Wingspan.Value, Is.EqualTo(42 + 783));
            Assert.That(updated.Wingspan.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Wingspan.Description, Is.EqualTo("Average"));
            Assert.That(updated.Height, Is.Not.Null);
            Assert.That(updated.Height.Value, Is.EqualTo(96 + 364));
            Assert.That(updated.Height.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Height.Description, Is.EqualTo("eh+"));
            Assert.That(updated.Length, Is.Not.Null);
            Assert.That(updated.Length.Value, Is.EqualTo(82 - 36));
            Assert.That(updated.Length.Unit, Is.EqualTo("inches"));
            Assert.That(updated.Length.Description, Is.EqualTo("eh-"));
            Assert.That(updated.Weight, Is.Not.Null);
            Assert.That(updated.Weight.Value, Is.EqualTo(4413));
            Assert.That(updated.Weight.Unit, Is.EqualTo("pounds"));
            Assert.That(updated.Weight.Description, Is.EqualTo("eh++"));
        }
    }
}
