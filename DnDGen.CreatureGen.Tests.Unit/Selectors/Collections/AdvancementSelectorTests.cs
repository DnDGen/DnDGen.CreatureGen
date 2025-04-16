using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Models;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Percentiles;
using DnDGen.RollGen;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class AdvancementSelectorTests
    {
        private IAdvancementSelector advancementSelector;
        private Mock<ICollectionTypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<ICollectionSelector> mockCollectionSelector;
        private Mock<ICollectionDataSelector<AdvancementDataSelection>> mockAdvancementSelector;
        private Mock<Dice> mockDice;
        private List<AdvancementDataSelection> advancements;
        private CreatureType creatureType;
        private TypeAndAmountDataSelection creatureTypeDivisor;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ICollectionTypeAndAmountSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockCollectionSelector = new Mock<ICollectionSelector>();
            mockDice = new Mock<Dice>();
            mockAdvancementSelector = new Mock<ICollectionDataSelector<AdvancementDataSelection>>();
            advancementSelector = new AdvancementSelector(
                mockPercentileSelector.Object,
                mockCollectionSelector.Object,
                mockAdvancementSelector.Object,
                mockTypeAndAmountSelector.Object,
                mockDice.Object);

            advancements = [];
            creatureType = new CreatureType
            {
                Name = "creature type"
            };

            mockAdvancementSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.Advancements, "creature")).Returns(advancements);
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 1 });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = int.MaxValue });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "other template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = int.MaxValue });

            creatureTypeDivisor = new TypeAndAmountDataSelection
            {
                Type = "creature type",
                AmountAsDouble = 999999999
            };

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountDataSelection>>()))
                .Returns((IEnumerable<TypeAndAmountDataSelection> c) => c.Last());
        }

        [Test]
        public void SelectRandomFor_Advancement()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor("creature", ["template", "other template"]);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.Space, Is.EqualTo(1.9));
            Assert.That(advancement.Reach, Is.EqualTo(3.2));
            Assert.That(advancement.StrengthAdjustment, Is.EqualTo(1338));
            Assert.That(advancement.ConstitutionAdjustment, Is.EqualTo(1337));
            Assert.That(advancement.DexterityAdjustment, Is.EqualTo(97));
            Assert.That(advancement.NaturalArmorAdjustment, Is.EqualTo(784));
            Assert.That(advancement.CasterLevelAdjustment, Is.EqualTo(8246));
            Assert.That(advancement.AdjustedChallengeRating, Is.EqualTo("my +42 challenge rating"));
        }

        [Test]
        public void SelectRandomFor_ValidAdvancement_NoTemplate()
        {
            SetUpAdvancement(SizeConstants.Large, 42);
            SetUpAdvancement(SizeConstants.Huge, 600, min: 43);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 550 });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 610 });

            var advancement = advancementSelector.SelectRandomFor("creature", null);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(600));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Huge));
        }

        [Test]
        public void SelectRandomFor_ValidAdvancement_Template()
        {
            SetUpAdvancement(SizeConstants.Large, 42);
            SetUpAdvancement(SizeConstants.Huge, 600, min: 43);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 550 });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 610 });

            var advancement = advancementSelector.SelectRandomFor("creature", ["template"]);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
        }

        [Test]
        public void SelectRandomFor_ValidAdvancement_MultipleTemplates()
        {
            SetUpAdvancement(SizeConstants.Large, 42);
            SetUpAdvancement(SizeConstants.Huge, 600, min: 43);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 550 });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 610 });

            var advancement = advancementSelector.SelectRandomFor("creature", ["template", "other template"]);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
        }

        private void SetUpAdvancement(
            string size,
            int additionalHitDice,
            int min = 1)
        {
            var selection = new AdvancementDataSelection
            {
                Size = size,
                AdditionalHitDiceRoll = Guid.NewGuid().ToString(),
                Space = 0.9 + min,
                Reach = 2.2 + min,
                StrengthAdjustment = 1337 + min,
                ConstitutionAdjustment = 1336 + min,
                DexterityAdjustment = 96 + min,
                NaturalArmorAdjustment = 783 + min,
                CasterLevelAdjustment = 8245 + min,
                AdjustedChallengeRating = $"my +{additionalHitDice} challenge rating",
            };

            mockDice.Setup(d => d.Roll(selection.AdditionalHitDiceRoll).AsSum<int>()).Returns(additionalHitDice);
            mockDice.Setup(d => d.Roll(selection.AdditionalHitDiceRoll).AsPotentialMinimum<int>()).Returns(min);

            advancements.Add(selection);
        }

        [Test]
        public void SelectRandomFor_MultipleAdvancements()
        {
            SetUpAdvancement(SizeConstants.Large, 42);
            SetUpAdvancement(SizeConstants.Huge, 9266, min: 43);
            SetUpAdvancement("wrong advanced size", 90210, min: 9267);

            mockCollectionSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountDataSelection>>()))
                .Returns((IEnumerable<TypeAndAmountDataSelection> c) => c.ElementAt(1));

            var advancement = advancementSelector.SelectRandomFor("creature", ["template", "other template"]);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(9266));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Huge));
        }

        [Test]
        public void SelectRandomFor_MultipleValidAdvancements()
        {
            SetUpAdvancement(SizeConstants.Medium, 42);
            SetUpAdvancement(SizeConstants.Large, 96, min: 43);
            SetUpAdvancement(SizeConstants.Huge, 9266, min: 97);
            SetUpAdvancement("wrong advanced size", 90210, min: 9267);

            mockCollectionSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<TypeAndAmountDataSelection>>()))
                .Returns((IEnumerable<TypeAndAmountDataSelection> c) => c.ElementAt(1));

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 550 });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 700 });

            var advancement = advancementSelector.SelectRandomFor("creature", ["template", "other template"]);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(96));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
        }

        [TestCase(0.1, 19)]
        [TestCase(0.5, 19)]
        [TestCase(1, 19)]
        [TestCase(2, 18)]
        [TestCase(10, 10)]
        [TestCase(17, 3)]
        [TestCase(19, 1)]
        public void SelectRandomFor_SelectMinimumFromValidAdvancement(double creatureHitDice, int additionalHitDice)
        {
            SetUpAdvancement(SizeConstants.Medium, 42);
            SetUpAdvancement(SizeConstants.Large, 96, min: 43);
            SetUpAdvancement(SizeConstants.Huge, 9266, min: 97);
            SetUpAdvancement("wrong advanced size", 90210, min: 9267);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = creatureHitDice });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 20 });

            var advancement = advancementSelector.SelectRandomFor("creature", ["template"]);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(additionalHitDice));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Medium));
        }

        [TestCase(0.1)]
        [TestCase(0.5)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(17)]
        public void SelectRandomFor_SelectRollFromValidAdvancement(double creatureHitDice)
        {
            SetUpAdvancement(SizeConstants.Medium, 2);
            SetUpAdvancement(SizeConstants.Large, 96, min: 43);
            SetUpAdvancement(SizeConstants.Huge, 9266, min: 97);
            SetUpAdvancement("wrong advanced size", 90210, min: 9267);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = creatureHitDice });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 20 });

            var advancement = advancementSelector.SelectRandomFor("creature", ["template"]);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(2));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Medium));
        }

        [TestCase(0.1, 19)]
        [TestCase(0.5, 19)]
        [TestCase(1, 19)]
        [TestCase(2, 18)]
        [TestCase(10, 10)]
        [TestCase(12, 8)]
        [TestCase(19, 1)]
        public void SelectRandomFor_SelectMinimumFromValidAdvancements(double creatureHitDice, int additionalHitDice)
        {
            SetUpAdvancement(SizeConstants.Medium, 6);
            SetUpAdvancement(SizeConstants.Large, 42, min: 7);
            SetUpAdvancement(SizeConstants.Huge, 9266, min: 97);
            SetUpAdvancement("wrong advanced size", 90210, min: 9267);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = creatureHitDice });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 20 });

            var advancement = advancementSelector.SelectRandomFor("creature", ["template"]);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(additionalHitDice));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
        }

        [TestCase(0.1)]
        [TestCase(0.5)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        [TestCase(12)]
        public void SelectRandomFor_SelectRollFromValidAdvancements(double creatureHitDice)
        {
            SetUpAdvancement(SizeConstants.Medium, 6);
            SetUpAdvancement(SizeConstants.Large, 8, min: 7);
            SetUpAdvancement(SizeConstants.Huge, 9266, min: 9);
            SetUpAdvancement("wrong advanced size", 90210, min: 9267);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = creatureHitDice });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 20 });

            var advancement = advancementSelector.SelectRandomFor("creature", ["template"]);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(8));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
        }

        [TestCase(0.1, 19)]
        [TestCase(0.5, 19)]
        [TestCase(1, 19)]
        [TestCase(2, 18)]
        [TestCase(10, 10)]
        [TestCase(19, 1)]
        [TestCase(20, 0)]
        public void SelectRandomFor_NoValidAdvancements_ThrowsError(double creatureHitDice, int additionalHitDice)
        {
            SetUpAdvancement(SizeConstants.Medium, 42, min: 21);
            SetUpAdvancement(SizeConstants.Large, 96, min: 43);
            SetUpAdvancement(SizeConstants.Huge, 9266, min: 97);
            SetUpAdvancement("wrong advanced size", 90210, min: 9267);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = creatureHitDice });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 200 });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "other template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 20 });

            Assert.That(() => advancementSelector.SelectRandomFor("creature", ["template", "other template"]), Throws.Exception);
        }

        [Test]
        public void SelectNoAdvancements()
        {
            Assert.That(() => advancementSelector.SelectRandomFor("creature", ["template", "other template"]), Throws.Exception);
        }

        [Test]
        public void IsAdvanced_IsRandomlyAdvanced()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            mockPercentileSelector.Setup(s => s.SelectFrom(.9)).Returns(true);

            var isAdvanced = advancementSelector.IsAdvanced("creature", [], null);
            Assert.That(isAdvanced, Is.True);
        }

        [Test]
        public void IsAdvanced_IsRandomlyNotAdvanced()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            mockPercentileSelector.Setup(s => s.SelectFrom(.9)).Returns(false);

            var isAdvanced = advancementSelector.IsAdvanced("creature", [], null);
            Assert.That(isAdvanced, Is.False);
        }

        [Test]
        public void IsAdvanced_IsNotAdvanced_IfNoAdvancements()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom(.9)).Returns(true);

            var isAdvanced = advancementSelector.IsAdvanced("creature", [], null);
            Assert.That(isAdvanced, Is.False);
        }

        [Test]
        public void IsAdvanced_IsNotAdvanced_WhenChallengeRatingIsFiltered()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            mockPercentileSelector.Setup(s => s.SelectFrom(.9)).Returns(true);

            var isAdvanced = advancementSelector.IsAdvanced("creature", [], "my challenge rating");
            Assert.That(isAdvanced, Is.False);
        }

        [Test]
        public void IsAdvanced_IsRandomlyAdvanced_WithTemplates()
        {
            SetUpAdvancement(SizeConstants.Large, 42);

            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "creature"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 19 });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 200 });
            mockTypeAndAmountSelector
                .Setup(s => s.SelectOneFrom(Config.Name, TableNameConstants.TypeAndAmount.HitDice, "other template"))
                .Returns(new TypeAndAmountDataSelection { AmountAsDouble = 20 });

            mockPercentileSelector.Setup(s => s.SelectFrom(.9)).Returns(true);

            var isAdvanced = advancementSelector.IsAdvanced("creature", ["template", "other template"], null);
            Assert.That(isAdvanced, Is.True);
        }

        [Test]
        public void SelectRandomFor_AdvancedBarghest()
        {
            mockAdvancementSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.Advancements, CreatureConstants.Barghest)).Returns(advancements);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest, ["template", "other template"]);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.StrengthAdjustment, Is.EqualTo(42));
            Assert.That(advancement.ConstitutionAdjustment, Is.EqualTo(42));
            Assert.That(advancement.NaturalArmorAdjustment, Is.EqualTo(42));
            Assert.That(advancement.CasterLevelAdjustment, Is.EqualTo(42));
        }

        [Test]
        public void SelectRandomFor_AdvancedGreaterBarghest()
        {
            mockAdvancementSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.Advancements, CreatureConstants.Barghest_Greater))
                .Returns(advancements);
            SetUpAdvancement(SizeConstants.Large, 42);

            var advancement = advancementSelector.SelectRandomFor(CreatureConstants.Barghest_Greater, ["template", "other template"]);
            Assert.That(advancement.AdditionalHitDice, Is.EqualTo(42));
            Assert.That(advancement.Size, Is.EqualTo(SizeConstants.Large));
            Assert.That(advancement.StrengthAdjustment, Is.EqualTo(42));
            Assert.That(advancement.ConstitutionAdjustment, Is.EqualTo(42));
            Assert.That(advancement.NaturalArmorAdjustment, Is.EqualTo(42));
            Assert.That(advancement.CasterLevelAdjustment, Is.EqualTo(42));
        }
    }
}
