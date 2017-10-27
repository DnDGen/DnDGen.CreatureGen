using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Abilities;
using CreatureGen.Domain.Selectors.Collections;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.Abilities;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class AbilitiesGeneratorTests
    {
        private Mock<IAbilityAdjustmentsSelector> mockAbilityAdjustmentsSelector;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private IAbilitiesGenerator abilitiesGenerator;

        private Mock<IAbilitiesRandomizer> mockAbilitiesRandomizer;
        private Mock<ISetAbilitiesRandomizer> mockSetAbilitiesRandomizer;
        private Dictionary<string, Ability> randomizedAbilities;
        private Dictionary<string, int> racialAdjustments;
        private Race race;
        private CharacterClass characterClass;
        private List<string> undead;
        private List<string> abilityPriorities;

        [SetUp]
        public void Setup()
        {
            mockAbilityAdjustmentsSelector = new Mock<IAbilityAdjustmentsSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            abilitiesGenerator = new AbilitiesGenerator(mockPercentileSelector.Object, mockAbilityAdjustmentsSelector.Object, mockAdjustmentsSelector.Object, mockCollectionsSelector.Object);

            mockAbilitiesRandomizer = new Mock<IAbilitiesRandomizer>();
            mockSetAbilitiesRandomizer = new Mock<ISetAbilitiesRandomizer>();

            randomizedAbilities = new Dictionary<string, Ability>();
            race = new Race();
            racialAdjustments = new Dictionary<string, int>();
            characterClass = new CharacterClass();
            undead = new List<string>();
            abilityPriorities = new List<string>();

            characterClass.Name = "class name";
            characterClass.Level = 1;
            race.BaseRace = "base race";
            race.Metarace = "metarace";

            randomizedAbilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);
            randomizedAbilities[AbilityConstants.Constitution] = new Ability(AbilityConstants.Constitution);
            randomizedAbilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            randomizedAbilities[AbilityConstants.Intelligence] = new Ability(AbilityConstants.Intelligence);
            randomizedAbilities[AbilityConstants.Strength] = new Ability(AbilityConstants.Strength);
            randomizedAbilities[AbilityConstants.Wisdom] = new Ability(AbilityConstants.Wisdom);

            abilityPriorities.Add(AbilityConstants.Strength);
            abilityPriorities.Add(AbilityConstants.Wisdom);

            racialAdjustments[AbilityConstants.Charisma] = 0;
            racialAdjustments[AbilityConstants.Constitution] = 0;
            racialAdjustments[AbilityConstants.Dexterity] = 0;
            racialAdjustments[AbilityConstants.Intelligence] = 0;
            racialAdjustments[AbilityConstants.Strength] = 0;
            racialAdjustments[AbilityConstants.Wisdom] = 0;

            mockSetAbilitiesRandomizer.SetupAllProperties();

            mockAbilityAdjustmentsSelector.Setup(p => p.SelectFor(race)).Returns(racialAdjustments);
            mockAbilitiesRandomizer.Setup(r => r.Randomize()).Returns(randomizedAbilities);
            mockSetAbilitiesRandomizer.Setup(r => r.Randomize()).Returns(randomizedAbilities);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead)).Returns(undead);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AbilityPriorities, characterClass.Name)).Returns(abilityPriorities);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());
        }

        [Test]
        public void RandomizesAbilityValues()
        {
            abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            mockAbilitiesRandomizer.Verify(r => r.Randomize(), Times.Once);
        }

        [Test]
        public void PrioritizesAbilitiesByClass()
        {
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = 18;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 16;

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(18));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(16));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
        }

        private void AssertAbilities(Dictionary<string, Ability> abilities)
        {
            foreach (var abilityKVP in abilities)
            {
                var ability = abilityKVP.Value;
                Assert.That(ability.Name, Is.EqualTo(abilityKVP.Key));
                Assert.That(ability.BaseValue, Is.AtLeast(3));
            }
        }

        [Test]
        public void IfMultipleSecondPriorityAbilities_DoNotCompeteAmongstThemselves()
        {
            abilityPriorities.Add(AbilityConstants.Charisma);
            randomizedAbilities[AbilityConstants.Charisma].BaseValue = 17;
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = 18;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 16;

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(18));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(16));
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(17));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
        }

        [Test]
        public void IfMultipleSecondPriorityAbilities_StillHigherThanNonPriorityAbilities()
        {
            abilityPriorities.Add(AbilityConstants.Charisma);
            randomizedAbilities[AbilityConstants.Charisma].BaseValue = 9;
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = 18;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 16;

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(18));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(16));
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Constitution].BaseValue, Is.EqualTo(9));
        }

        [Test]
        public void DoNotPrioritizeSecondPriorityAbilityMoreThanOnce()
        {
            abilityPriorities.Add(AbilityConstants.Charisma);
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 19;
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = 18;
            randomizedAbilities[AbilityConstants.Constitution].BaseValue = 17;
            randomizedAbilities[AbilityConstants.Intelligence].BaseValue = 16;

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(19));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(18));
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(17));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(16));
        }

        [Test]
        public void DoNotPrioritizeIfAllValuesTheSame()
        {
            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Constitution].BaseValue, Is.EqualTo(10));
        }

        [Test]
        public void DoNotPrioritizeSecondaryPrioritiesIfTheyAreAllAlreadyGreaterThanNonPriorityAbilities()
        {
            abilityPriorities.Add(AbilityConstants.Charisma);
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 17;
            randomizedAbilities[AbilityConstants.Charisma].BaseValue = 18;
            randomizedAbilities[AbilityConstants.Wisdom].BaseValue = 19;

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(19));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(17));
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(18));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Constitution].BaseValue, Is.EqualTo(10));
        }

        [Test]
        public void IfNoSecondPriorityAbilities_OnlyPrioritiesTheFirst()
        {
            abilityPriorities.Remove(AbilityConstants.Strength);
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 16;
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = 12;

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(16));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(12));
        }

        [Test]
        public void DoNotPrioritizeAbilitiesIfNoPriorities()
        {
            abilityPriorities.Clear();
            randomizedAbilities[AbilityConstants.Charisma].BaseValue = 17;
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = 18;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 16;

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(16));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(17));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(18));
        }

        [Test]
        public void AdjustsAbilitiesByRace()
        {
            racialAdjustments[AbilityConstants.Dexterity] = 1;
            racialAdjustments[AbilityConstants.Strength] = -1;

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(9));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(11));
        }

        [Test]
        public void AdjustAbilitiesAfterPrioritizing()
        {
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = 18;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 16;
            racialAdjustments[AbilityConstants.Dexterity] = 9266;
            racialAdjustments[AbilityConstants.Strength] = -10;
            racialAdjustments[AbilityConstants.Wisdom] = -7;

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(8));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(9));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(9276));
        }

        [Test]
        public void IncreaseFirstPriorityAbility()
        {
            characterClass.Level = 4;
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility)).Returns(true);

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(11));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
        }

        [Test]
        public void IncreaseSecondPriorityAbility()
        {
            characterClass.Level = 4;
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility)).Returns(false);

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(11));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
        }

        [Test]
        public void IncreaseRandomPriorityAbilityThatIsNotFirstPriority()
        {
            abilityPriorities.Add(AbilityConstants.Charisma);
            characterClass.Level = 4;

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility)).Returns(false);

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(11));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
        }

        [Test]
        public void DoNotIncreaseSecondPriorityAbilityIfNone()
        {
            abilityPriorities.Remove(AbilityConstants.Strength);
            characterClass.Level = 4;

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility)).Returns(false);

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(11));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
        }

        [Test]
        public void IncreaseRandomAbilityIfNoPriorities()
        {
            abilityPriorities.Clear();
            characterClass.Level = 4;

            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility)).Returns(false);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(2));

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(11));
        }

        [Test]
        public void IncreasingIsAfterPrioritizationAndAdjustments()
        {
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = 18;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 16;
            racialAdjustments[AbilityConstants.Dexterity] = 9266;
            racialAdjustments[AbilityConstants.Strength] = -10;
            racialAdjustments[AbilityConstants.Wisdom] = -7;
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility)).Returns(true);
            characterClass.Level = 4;

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(9));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(9));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(9276));
        }

        [TestCase(1, 0)]
        [TestCase(2, 0)]
        [TestCase(3, 0)]
        [TestCase(4, 1)]
        [TestCase(5, 1)]
        [TestCase(6, 1)]
        [TestCase(7, 1)]
        [TestCase(8, 2)]
        [TestCase(9, 2)]
        [TestCase(10, 2)]
        [TestCase(11, 2)]
        [TestCase(12, 3)]
        [TestCase(13, 3)]
        [TestCase(14, 3)]
        [TestCase(15, 3)]
        [TestCase(16, 4)]
        [TestCase(17, 4)]
        [TestCase(18, 4)]
        [TestCase(19, 4)]
        [TestCase(20, 5)]
        public void IncreaseAbility(int level, int increase)
        {
            characterClass.Level = level;
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility)).Returns(true);

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(10 + increase));
        }

        [Test]
        public void DetermineWhichAbilityToIncreasePerLevel()
        {
            characterClass.Level = 12;
            mockPercentileSelector.SetupSequence(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility))
                .Returns(true).Returns(false).Returns(true);

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(12));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(11));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
        }

        [Test]
        public void IncreasesIgnorePrioritization()
        {
            characterClass.Level = 12;
            mockPercentileSelector.SetupSequence(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility))
                .Returns(true).Returns(false).Returns(false);

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(11));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(12));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
        }

        [Test]
        public void CannotHaveAbilityLessThan3()
        {
            racialAdjustments[AbilityConstants.Strength] = -9266;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 3;

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(3));
        }

        [Test]
        public void SetMinimumAbilityBeforeIncreasingAbilities()
        {
            racialAdjustments[AbilityConstants.Strength] = -9266;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 3;

            characterClass.Level = 4;
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility)).Returns(true);

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(4));
        }

        [Test]
        public void UndeadHaveNoConstitution()
        {
            undead.Add(race.Metarace);

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(10));
            Assert.That(abilities.Keys, Is.All.Not.EqualTo(AbilityConstants.Constitution));
            Assert.That(abilities.Count, Is.EqualTo(5));
        }

        [Test]
        public void UndeadDoNotTryToIncreaseConstitution()
        {
            abilityPriorities.Clear();
            abilityPriorities.Add(AbilityConstants.Constitution);
            abilityPriorities.Add(AbilityConstants.Strength);

            undead.Add(race.Metarace);
            characterClass.Level = 4;
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility)).Returns(true);

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(11));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(10));
            Assert.That(abilities.Keys, Is.All.Not.EqualTo(AbilityConstants.Constitution));
            Assert.That(abilities.Count, Is.EqualTo(5));
        }

        [Test]
        public void UndeadDoNotTryToIncreaseConstitutionWhenNoPriorities()
        {
            abilityPriorities.Clear();

            undead.Add(race.Metarace);
            characterClass.Level = 4;

            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns(AbilityConstants.Constitution)
                .Returns(AbilityConstants.Intelligence);

            var abilities = abilitiesGenerator.GenerateWith(mockAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(11));
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(10));
            Assert.That(abilities.Keys, Is.All.Not.EqualTo(AbilityConstants.Constitution));
            Assert.That(abilities.Count, Is.EqualTo(5));
        }

        [Test]
        public void AdjustSetAbilities()
        {
            randomizedAbilities[AbilityConstants.Charisma].BaseValue = 10;
            randomizedAbilities[AbilityConstants.Constitution].BaseValue = 11;
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = 9;
            randomizedAbilities[AbilityConstants.Intelligence].BaseValue = 12;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 8;
            randomizedAbilities[AbilityConstants.Wisdom].BaseValue = 13;

            racialAdjustments[AbilityConstants.Charisma] = 1;
            racialAdjustments[AbilityConstants.Constitution] = -2;
            racialAdjustments[AbilityConstants.Dexterity] = 3;
            racialAdjustments[AbilityConstants.Intelligence] = -4;
            racialAdjustments[AbilityConstants.Strength] = 5;
            racialAdjustments[AbilityConstants.Wisdom] = -6;

            characterClass.Level = 20;
            mockPercentileSelector.SetupSequence(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility))
                .Returns(true).Returns(true).Returns(false).Returns(true).Returns(false);

            mockSetAbilitiesRandomizer.Object.AllowAdjustments = true;

            var abilities = abilitiesGenerator.GenerateWith(mockSetAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(11));
            Assert.That(abilities[AbilityConstants.Constitution].BaseValue, Is.EqualTo(9));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(12));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(4));
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(21));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(8));
        }

        [Test]
        public void DoNotAdjustSetAbilities()
        {
            randomizedAbilities[AbilityConstants.Charisma].BaseValue = 10;
            randomizedAbilities[AbilityConstants.Constitution].BaseValue = 11;
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = 9;
            randomizedAbilities[AbilityConstants.Intelligence].BaseValue = 12;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 8;
            randomizedAbilities[AbilityConstants.Wisdom].BaseValue = 13;

            racialAdjustments[AbilityConstants.Charisma] = 1;
            racialAdjustments[AbilityConstants.Constitution] = -2;
            racialAdjustments[AbilityConstants.Dexterity] = 3;
            racialAdjustments[AbilityConstants.Intelligence] = -4;
            racialAdjustments[AbilityConstants.Strength] = 5;
            racialAdjustments[AbilityConstants.Wisdom] = -6;

            characterClass.Level = 20;
            mockPercentileSelector.SetupSequence(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility))
                .Returns(true).Returns(true).Returns(false).Returns(true).Returns(false);

            mockSetAbilitiesRandomizer.Object.AllowAdjustments = false;

            var abilities = abilitiesGenerator.GenerateWith(mockSetAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Constitution].BaseValue, Is.EqualTo(11));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(9));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(12));
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(8));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(13));
        }

        [Test]
        public void CannotHaveSetAbilityBelow3IfAdjustingAbilities()
        {
            randomizedAbilities[AbilityConstants.Charisma].BaseValue = -10;
            randomizedAbilities[AbilityConstants.Constitution].BaseValue = -11;
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = -9;
            randomizedAbilities[AbilityConstants.Intelligence].BaseValue = -12;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = -8;
            randomizedAbilities[AbilityConstants.Wisdom].BaseValue = -13;

            racialAdjustments[AbilityConstants.Charisma] = -1;
            racialAdjustments[AbilityConstants.Constitution] = -2;
            racialAdjustments[AbilityConstants.Dexterity] = -3;
            racialAdjustments[AbilityConstants.Intelligence] = -4;
            racialAdjustments[AbilityConstants.Strength] = -5;
            racialAdjustments[AbilityConstants.Wisdom] = -6;

            mockSetAbilitiesRandomizer.Object.AllowAdjustments = true;

            var abilities = abilitiesGenerator.GenerateWith(mockSetAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Constitution].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(3));
        }

        [Test]
        public void CannotHaveSetAbilityBelow3IfNotAdjustingAbilities()
        {
            randomizedAbilities[AbilityConstants.Charisma].BaseValue = -10;
            randomizedAbilities[AbilityConstants.Constitution].BaseValue = -11;
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = -9;
            randomizedAbilities[AbilityConstants.Intelligence].BaseValue = -12;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = -8;
            randomizedAbilities[AbilityConstants.Wisdom].BaseValue = -13;

            racialAdjustments[AbilityConstants.Charisma] = -1;
            racialAdjustments[AbilityConstants.Constitution] = -2;
            racialAdjustments[AbilityConstants.Dexterity] = -3;
            racialAdjustments[AbilityConstants.Intelligence] = -4;
            racialAdjustments[AbilityConstants.Strength] = -5;
            racialAdjustments[AbilityConstants.Wisdom] = -6;

            mockSetAbilitiesRandomizer.Object.AllowAdjustments = false;

            var abilities = abilitiesGenerator.GenerateWith(mockSetAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Constitution].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(3));
        }

        [Test]
        public void UndeadStillHaveNoConstitutionWhenNotAdjustSetAbilities()
        {
            randomizedAbilities[AbilityConstants.Charisma].BaseValue = 10;
            randomizedAbilities[AbilityConstants.Constitution].BaseValue = 11;
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = 9;
            randomizedAbilities[AbilityConstants.Intelligence].BaseValue = 12;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = 8;
            randomizedAbilities[AbilityConstants.Wisdom].BaseValue = 13;

            racialAdjustments[AbilityConstants.Charisma] = 1;
            racialAdjustments[AbilityConstants.Constitution] = -2;
            racialAdjustments[AbilityConstants.Dexterity] = 3;
            racialAdjustments[AbilityConstants.Intelligence] = -4;
            racialAdjustments[AbilityConstants.Strength] = 5;
            racialAdjustments[AbilityConstants.Wisdom] = -6;

            characterClass.Level = 20;
            mockPercentileSelector.SetupSequence(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.IncreaseFirstPriorityAbility))
                .Returns(true).Returns(true).Returns(false).Returns(true).Returns(false);

            mockSetAbilitiesRandomizer.Object.AllowAdjustments = false;
            undead.Add(race.Metarace);

            var abilities = abilitiesGenerator.GenerateWith(mockSetAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(10));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(9));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(12));
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(8));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(13));
            Assert.That(abilities.Keys, Is.All.Not.EqualTo(AbilityConstants.Constitution));
            Assert.That(abilities.Count, Is.EqualTo(5));
        }

        [Test]
        public void UndeadStillHaveNoConstitutionWhenAdjustingSetAbilitiesBelow3()
        {
            randomizedAbilities[AbilityConstants.Charisma].BaseValue = -10;
            randomizedAbilities[AbilityConstants.Constitution].BaseValue = -11;
            randomizedAbilities[AbilityConstants.Dexterity].BaseValue = -9;
            randomizedAbilities[AbilityConstants.Intelligence].BaseValue = -12;
            randomizedAbilities[AbilityConstants.Strength].BaseValue = -8;
            randomizedAbilities[AbilityConstants.Wisdom].BaseValue = -13;

            racialAdjustments[AbilityConstants.Charisma] = -1;
            racialAdjustments[AbilityConstants.Constitution] = -2;
            racialAdjustments[AbilityConstants.Dexterity] = -3;
            racialAdjustments[AbilityConstants.Intelligence] = -4;
            racialAdjustments[AbilityConstants.Strength] = -5;
            racialAdjustments[AbilityConstants.Wisdom] = -6;

            mockSetAbilitiesRandomizer.Object.AllowAdjustments = false;

            mockSetAbilitiesRandomizer.Object.AllowAdjustments = false;
            undead.Add(race.Metarace);

            var abilities = abilitiesGenerator.GenerateWith(mockSetAbilitiesRandomizer.Object, characterClass, race);
            AssertAbilities(abilities);
            Assert.That(abilities[AbilityConstants.Charisma].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Dexterity].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Intelligence].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Strength].BaseValue, Is.EqualTo(3));
            Assert.That(abilities[AbilityConstants.Wisdom].BaseValue, Is.EqualTo(3));
            Assert.That(abilities.Keys, Is.All.Not.EqualTo(AbilityConstants.Constitution));
            Assert.That(abilities.Count, Is.EqualTo(5));
        }
    }
}