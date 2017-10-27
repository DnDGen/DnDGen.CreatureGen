using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Classes;
using CreatureGen.Domain.Selectors.Collections;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.CharacterClasses;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Classes
{
    [TestFixture]
    public class CharacterClassGeneratorTests
    {
        private const string ClassName = "class name";

        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private ICharacterClassGenerator characterClassGenerator;
        private Alignment alignment;
        private CharacterClassPrototype classPrototype;
        private Dictionary<string, int> specialistFieldQuantities;
        private Dictionary<string, int> prohibitedFieldQuantities;
        private List<string> specialistFields;
        private List<string> prohibitedFields;
        private Mock<IClassNameRandomizer> mockClassNameRandomizer;
        private Mock<ILevelRandomizer> mockLevelRandomizer;

        [SetUp]
        public void Setup()
        {
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            characterClassGenerator = new CharacterClassGenerator(mockAdjustmentsSelector.Object, mockCollectionsSelector.Object, mockPercentileSelector.Object);

            alignment = new Alignment();
            classPrototype = new CharacterClassPrototype();
            specialistFieldQuantities = new Dictionary<string, int>();
            prohibitedFieldQuantities = new Dictionary<string, int>();
            specialistFields = new List<string>();
            prohibitedFields = new List<string>();
            mockClassNameRandomizer = new Mock<IClassNameRandomizer>();
            mockLevelRandomizer = new Mock<ILevelRandomizer>();

            classPrototype.Level = 9266;
            classPrototype.Name = ClassName;

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SpecialistFieldQuantities, ClassName)).Returns(() => specialistFieldQuantities[ClassName]);
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Adjustments.ProhibitedFieldQuantities)).Returns(prohibitedFieldQuantities);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, ClassName)).Returns(specialistFields);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, ClassName)).Returns(prohibitedFields);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.First());
        }

        [Test]
        public void GeneratePrototype()
        {
            var npcs = new[] { "class name", "other class name" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs)).Returns(npcs);

            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns("random class name");
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(90210);

            var prototype = characterClassGenerator.GeneratePrototype(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object);
            Assert.That(prototype.Level, Is.EqualTo(90210));
            Assert.That(prototype.Name, Is.EqualTo("random class name"));
            Assert.That(prototype.IsNPC, Is.False);
        }

        [Test]
        public void GenerateNPCPrototype()
        {
            var npcs = new[] { "class name", "random class name" };
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs)).Returns(npcs);

            mockClassNameRandomizer.Setup(r => r.Randomize(alignment)).Returns("random class name");
            mockLevelRandomizer.Setup(r => r.Randomize()).Returns(90210);

            var prototype = characterClassGenerator.GeneratePrototype(alignment, mockClassNameRandomizer.Object, mockLevelRandomizer.Object);
            Assert.That(prototype.Level, Is.EqualTo(90210));
            Assert.That(prototype.Name, Is.EqualTo("random class name"));
            Assert.That(prototype.IsNPC, Is.True);
        }

        [Test]
        public void GeneratorReturnsClassFromPrototype()
        {
            classPrototype.IsNPC = false;

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.Name, Is.EqualTo(ClassName));
            Assert.That(characterClass.Level, Is.EqualTo(9266));
            Assert.That(characterClass.IsNPC, Is.False);
            Assert.That(characterClass.EffectiveLevel, Is.EqualTo(9266));
        }

        [Test]
        public void GeneratorReturnsNPCClassFromPrototype()
        {
            classPrototype.IsNPC = true;

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.Name, Is.EqualTo(ClassName));
            Assert.That(characterClass.Level, Is.EqualTo(9266));
            Assert.That(characterClass.IsNPC, Is.True);
            Assert.That(characterClass.EffectiveLevel, Is.EqualTo(9266 / 2));
        }

        [Test]
        public void DoNotGetSpecialistFieldsIfShouldNotHaveAny()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(tableName)).Returns(false);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("field 1");

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.SpecialistFields, Is.Empty);
        }

        [Test]
        public void DoNotGetSpecialistFieldsIfNone()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.SpecialistFields, Is.Empty);
        }

        [Test]
        public void GetSpecialistField()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("field 1");
            specialistFields.Add("field 2");

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 2")))).Returns("field 2");

            prohibitedFieldQuantities["field 1"] = 0;
            prohibitedFieldQuantities["field 2"] = 0;

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.SpecialistFields, Contains.Item("field 2"));
            Assert.That(characterClass.SpecialistFields.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetSpecialistFields()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 2;
            specialistFields.Add("field 1");
            specialistFields.Add("field 2");
            specialistFields.Add("field 3");

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 3") && fs.Contains("field 1"))))
                .Returns("field 3").Returns("field 1");

            prohibitedFieldQuantities["field 1"] = 0;
            prohibitedFieldQuantities["field 2"] = 0;
            prohibitedFieldQuantities["field 3"] = 0;

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.SpecialistFields, Contains.Item("field 3"));
            Assert.That(characterClass.SpecialistFields, Contains.Item("field 1"));
            Assert.That(characterClass.SpecialistFields.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CannotSpecializeInAlignmentFieldThatDoesNotMatchAlignment()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("alignment field");
            specialistFields.Add("non-alignment field");

            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, alignment.ToString()))
                .Returns(new[] { "non-alignment field" });

            prohibitedFieldQuantities["alignment field"] = 0;

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("alignment field")))).Returns("alignment field");

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.SpecialistFields, Contains.Item("alignment field"));
            Assert.That(characterClass.SpecialistFields.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DoNotGetProhibitedFieldsIfThereAreNoSpecialistFields()
        {
            prohibitedFields.Add("field 1");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 1")))).Returns("field 1");

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.SpecialistFields, Is.Empty);
            Assert.That(characterClass.ProhibitedFields, Is.Empty);
        }

        [Test]
        public void DoNotGetProhibitedFieldsIfNone()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("field 1");

            prohibitedFieldQuantities["field 1"] = 1;

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.ProhibitedFields, Is.Empty);
        }

        [Test]
        public void GetProhibitedField()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("field 1");

            prohibitedFieldQuantities["field 1"] = 1;
            prohibitedFields.Add("field 1");
            prohibitedFields.Add("field 2");

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.ProhibitedFields, Contains.Item("field 2"));
            Assert.That(characterClass.ProhibitedFields.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ProhibitedFieldCannotAlreadyBeASpecialistField()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("field 1");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 1")))).Returns("field 1");

            prohibitedFieldQuantities["field 1"] = 1;
            prohibitedFields.Add("field 1");
            prohibitedFields.Add("field 2");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 2")))).Returns("field 2");

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.ProhibitedFields, Contains.Item("field 2"));
            Assert.That(characterClass.ProhibitedFields.Count(), Is.EqualTo(1));
        }

        [Test]
        public void GetProhibitedFields()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 1;
            specialistFields.Add("field 2");
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 2")))).Returns("field 2");

            prohibitedFieldQuantities["field 2"] = 2;
            prohibitedFields.Add("field 1");
            prohibitedFields.Add("field 2");
            prohibitedFields.Add("field 3");
            prohibitedFields.Add("field 4");
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 1") && fs.Contains("field 4"))))
                .Returns("field 4").Returns("field 1");

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.ProhibitedFields, Contains.Item("field 4"));
            Assert.That(characterClass.ProhibitedFields, Contains.Item("field 1"));
            Assert.That(characterClass.ProhibitedFields.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetProhibitedFieldsFromMultipleSpecialistFields()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, ClassName);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(tableName)).Returns(true);
            specialistFieldQuantities[ClassName] = 2;
            specialistFields.Add("field 1");
            specialistFields.Add("field 2");
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 1") && fs.Contains("field 2"))))
                .Returns("field 1").Returns("field 2");

            prohibitedFieldQuantities["field 1"] = 1;
            prohibitedFieldQuantities["field 2"] = 1;
            prohibitedFields.Add("field 1");
            prohibitedFields.Add("field 2");
            prohibitedFields.Add("field 3");
            prohibitedFields.Add("field 4");
            prohibitedFields.Add("field 5");
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.Is<IEnumerable<string>>(fs => fs.Contains("field 3") && fs.Contains("field 5"))))
                .Returns("field 5").Returns("field 3");

            var characterClass = characterClassGenerator.GenerateWith(alignment, classPrototype);
            Assert.That(characterClass.ProhibitedFields, Contains.Item("field 5"));
            Assert.That(characterClass.ProhibitedFields, Contains.Item("field 3"));
            Assert.That(characterClass.ProhibitedFields.Count(), Is.EqualTo(2));
        }

        [Test]
        public void IfNotASpecialist_ReturnNothingForRegeneratedSpecialistField()
        {
            var characterClass = new CharacterClass();
            characterClass.Name = ClassName;
            var race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";

            specialistFields.Add("specialist field");
            specialistFields.Add("other specialist field");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.Metarace))
                .Returns(new[] { "metarace specialist field", "other specialist field" });

            var regeneratedSpecialistFields = characterClassGenerator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(regeneratedSpecialistFields, Is.Empty);
        }

        [Test]
        public void ReturnIntersectionOfAllRestrictionsOfSpecialistFields()
        {
            var characterClass = new CharacterClass();
            characterClass.Name = ClassName;
            characterClass.SpecialistFields = new[] { "specialist field" };
            var race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";

            specialistFields.Add("specialist field");
            specialistFields.Add("alignment specialist field");
            specialistFields.Add("other specialist field");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.Metarace))
                .Returns(new[] { "metarace specialist field", "specialist field" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.BaseRace))
                .Returns(new[] { "base race specialist field", "specialist field" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, alignment.ToString()))
                .Returns(new[] { "alignment specialist field" });

            var regeneratedSpecialistFields = characterClassGenerator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(regeneratedSpecialistFields.Single(), Is.EqualTo("specialist field"));
        }

        [Test]
        public void ReturnEqualNumberOfIntersectionOfAllRestrictionsOfSpecialistFields()
        {
            var characterClass = new CharacterClass();
            characterClass.Name = ClassName;
            characterClass.SpecialistFields = new[] { "specialist field", "other specialist field" };
            var race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";

            specialistFields.Add("specialist field");
            specialistFields.Add("other specialist field");
            specialistFields.Add("alignment specialist field");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.Metarace))
                .Returns(new[] { "metarace specialist field", "specialist field", "other specialist field" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.BaseRace))
                .Returns(new[] { "base race specialist field", "specialist field", "other specialist field" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, alignment.ToString()))
                .Returns(new[] { "other alignment specialist field" });

            var count = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(count++ % c.Count()));

            var regeneratedSpecialistFields = characterClassGenerator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(regeneratedSpecialistFields.First, Is.EqualTo("specialist field"));
            Assert.That(regeneratedSpecialistFields.Last, Is.EqualTo("other specialist field"));
            Assert.That(regeneratedSpecialistFields.Count, Is.EqualTo(2));
        }

        [Test]
        public void ReturnFewerIntersectionOfAllRestrictionsOfSpecialistFields()
        {
            var characterClass = new CharacterClass();
            characterClass.Name = ClassName;
            characterClass.SpecialistFields = new[] { "specialist field", "other specialist field" };
            var race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";

            specialistFields.Add("specialist field");
            specialistFields.Add("other specialist field");
            specialistFields.Add("alignment specialist field");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.Metarace))
                .Returns(new[] { "metarace specialist field", "specialist field", "other specialist field" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.BaseRace))
                .Returns(new[] { "base race specialist field", "specialist field" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, alignment.ToString()))
                .Returns(new[] { "alignment specialist field" });

            var regeneratedSpecialistFields = characterClassGenerator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(regeneratedSpecialistFields.Single, Is.EqualTo("specialist field"));
        }

        [Test]
        public void ReturnNoSpecialistFieldsWhenTooRestricted()
        {
            var characterClass = new CharacterClass();
            characterClass.Name = ClassName;
            characterClass.SpecialistFields = new[] { "specialist field", "other specialist field" };
            var race = new Race();
            race.BaseRace = "base race";
            race.Metarace = "metarace";

            specialistFields.Add("specialist field");
            specialistFields.Add("other specialist field");
            specialistFields.Add("alignment specialist field");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.Metarace))
                .Returns(new[] { "metarace specialist field", "other specialist field" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpecialistFields, race.BaseRace))
                .Returns(new[] { "base race specialist field", "specialist field" });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ProhibitedFields, alignment.ToString()))
                .Returns(new[] { "alignment specialist field" });

            var regeneratedSpecialistFields = characterClassGenerator.RegenerateSpecialistFields(alignment, characterClass, race);
            Assert.That(regeneratedSpecialistFields, Is.Empty);
        }
    }
}