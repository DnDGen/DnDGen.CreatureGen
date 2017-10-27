using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Magics;
using CreatureGen.Domain.Selectors.Collections;
using CreatureGen.Domain.Tables;
using CreatureGen.Feats;
using CreatureGen.Magics;
using CreatureGen.Creatures;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class AnimalGeneratorTests
    {
        private const string Animal = "animal";

        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private IAnimalGenerator animalGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;
        private List<string> animals;
        private List<string> druidAnimals;
        private Race characterRace;
        private Alignment alignment;
        private Dictionary<string, int> levelAdjustments;
        private List<string> animalsForSize;
        private List<string> improvedFamiliars;
        private List<string> animalsForMetarace;
        private List<string> arcaneSpellcasters;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            animalGenerator = new AnimalGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);

            characterClass = new CharacterClass();
            characterRace = new Race();
            feats = new List<Feat>();
            animals = new List<string>();
            alignment = new Alignment();
            levelAdjustments = new Dictionary<string, int>();
            animalsForSize = new List<string>();
            improvedFamiliars = new List<string>();
            druidAnimals = new List<string>();
            animalsForMetarace = new List<string>();
            arcaneSpellcasters = new List<string>();

            characterRace.BaseRace = "character race";
            characterRace.Metarace = "character metarace";
            characterRace.Size = "character size";
            characterClass.Level = 9266;
            characterClass.Name = "class name";
            animals.Add(Animal);
            animalsForSize.Add(Animal);
            animalsForMetarace.Add(Animal);
            levelAdjustments[Animal] = 0;

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AnimalGroups, characterClass.Name)).Returns(animals);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AnimalGroups, CharacterClassConstants.Druid)).Returns(druidAnimals);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AnimalGroups, characterRace.Size)).Returns(animalsForSize);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AnimalGroups, characterRace.Metarace)).Returns(animalsForMetarace);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AnimalGroups, FeatConstants.ImprovedFamiliar)).Returns(improvedFamiliars);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Arcane)).Returns(arcaneSpellcasters);
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(TableNameConstants.Set.Adjustments.LevelAdjustments)).Returns(levelAdjustments);
        }

        [Test]
        public void GenerateNoAnimalIfNoneAvailable()
        {
            animals.Clear();
            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void GenerateNoAnimalIfNoneAvailableWithinSize()
        {
            animals.Clear();
            animals.Add("other animal");
            levelAdjustments["other animal"] = 0;

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void GenerateNoAnimalIfNoneMatchSize()
        {
            animalsForSize.Clear();
            animalsForSize.Add("other animal");

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void GenerateNoAnimalIfNoneFitWithinLevel()
        {
            levelAdjustments[Animal] = characterClass.Level;
            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void FilterByMetaraceIfClassIsArcaneSpellcaster()
        {
            animalsForMetarace.Clear();
            animalsForMetarace.Add("other animal");
            arcaneSpellcasters.Add(characterClass.Name);

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void DoNotFilterByMetaraceIfClassIsNotArcaneSpellcaster()
        {
            animalsForMetarace.Clear();
            animalsForMetarace.Add("other animal");
            arcaneSpellcasters.Add("other class");

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void GenerateAnimal()
        {
            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void GenerateImprovedFamiliar()
        {
            animals.Add("other animal");
            levelAdjustments["other animal"] = 0;
            improvedFamiliars.Add(Animal);

            feats.Add(new Feat { Name = FeatConstants.ImprovedFamiliar });

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void DoNotGenerateImprovedFamiliarBecauseFamiliarIsNotImproved()
        {
            animals.Add("other animal");
            levelAdjustments["other animal"] = 0;
            improvedFamiliars.Add("other animal");

            feats.Add(new Feat { Name = FeatConstants.ImprovedFamiliar });

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void DoNotGenerateImprovedFamiliarBecauseCharacterDoesNotHaveImprovedFamiliarFeat()
        {
            animals.Add("other animal");
            levelAdjustments["other animal"] = 0;
            improvedFamiliars.Add("other animal");

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void DoNotGenerateImprovedFamiliarBecauseMetaraceDoesNotAllowIt()
        {
            animals.Add("other animal");
            levelAdjustments["other animal"] = 0;
            improvedFamiliars.Add("other animal");
            improvedFamiliars.Add(Animal);
            arcaneSpellcasters.Add(characterClass.Name);

            feats.Add(new Feat { Name = FeatConstants.ImprovedFamiliar });

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void RangersUseHalfTheirLevelAsDruidLevel()
        {
            characterClass.Name = CharacterClassConstants.Ranger;
            druidAnimals.Add("other animal");
            druidAnimals.Add(Animal);
            levelAdjustments["other animal"] = characterClass.Level / 2 - 1;

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }

        [Test]
        public void OriginalClassNotModified()
        {
            characterClass.Name = CharacterClassConstants.Ranger;
            druidAnimals.Add("other animal");
            druidAnimals.Add(Animal);
            levelAdjustments["other animal"] = characterClass.Level / 2 - 1;

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(characterClass.Name, Is.EqualTo(CharacterClassConstants.Ranger));
            Assert.That(characterClass.Level, Is.EqualTo(9266));
        }

        [Test]
        public void NPCLevel1DoesNotHaveAnimal()
        {
            characterClass.IsNPC = true;
            characterClass.Level = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AnimalGroups, CharacterClassConstants.Adept)).Returns(animals);

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.Empty);
        }

        [Test]
        public void NPCGetsAnimalAtLevel2()
        {
            characterClass.IsNPC = true;
            characterClass.Level = 2;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AnimalGroups, CharacterClassConstants.Adept)).Returns(animals);

            var animal = animalGenerator.GenerateFrom(alignment, characterClass, characterRace, feats);
            Assert.That(animal, Is.EqualTo(Animal));
        }
    }
}
