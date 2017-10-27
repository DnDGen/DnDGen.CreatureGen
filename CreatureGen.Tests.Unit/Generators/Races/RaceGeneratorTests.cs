using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Races;
using CreatureGen.Domain.Selectors.Collections;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using CreatureGen.Randomizers.Races;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using RollGen;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Races
{
    [TestFixture]
    public class RaceGeneratorTests
    {
        private const string BaseRace = "base race";
        private const string Metarace = "metarace";

        private Mock<IPercentileSelector> mockPercentileSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<Dice> mockDice;
        private IRaceGenerator raceGenerator;
        private Alignment alignment;
        private CharacterClass characterClass;
        private RacePrototype racePrototype;
        private Dictionary<string, int> landSpeeds;
        private Dictionary<string, int> aerialSpeeds;
        private Dictionary<string, int> swimSpeeds;
        private Dictionary<string, int> ages;
        private Dictionary<string, int> challengeRatings;
        private Dictionary<string, int> maleHeights;
        private Dictionary<string, int> femaleHeights;
        private Dictionary<string, int> maleWeights;
        private Dictionary<string, int> femaleWeights;
        private Dictionary<string, List<string>> aerialManeuverability;
        private string classType;
        private string baseRaceSize;
        private List<string> baseRacesWithWings;
        private List<string> metaracesWithWings;
        private Mock<RaceRandomizer> mockBaseRaceRandomizer;
        private Mock<RaceRandomizer> mockMetaraceRandomizer;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            mockDice = new Mock<Dice>();
            var generator = new ConfigurableIterationGenerator(5);
            raceGenerator = new RaceGenerator(mockPercentileSelector.Object,
                mockCollectionsSelector.Object,
                mockAdjustmentsSelector.Object,
                mockDice.Object,
                generator);

            alignment = new Alignment();
            characterClass = new CharacterClass();
            racePrototype = new RacePrototype();
            landSpeeds = new Dictionary<string, int>();
            aerialSpeeds = new Dictionary<string, int>();
            swimSpeeds = new Dictionary<string, int>();
            classType = CharacterClassConstants.TrainingTypes.Intuitive;
            baseRaceSize = SizeConstants.Sizes.Medium;
            baseRacesWithWings = new List<string>();
            metaracesWithWings = new List<string>();
            challengeRatings = new Dictionary<string, int>();
            maleHeights = new Dictionary<string, int>();
            femaleHeights = new Dictionary<string, int>();
            maleWeights = new Dictionary<string, int>();
            femaleWeights = new Dictionary<string, int>();
            aerialManeuverability = new Dictionary<string, List<string>>();
            mockBaseRaceRandomizer = new Mock<RaceRandomizer>();
            mockMetaraceRandomizer = new Mock<RaceRandomizer>();

            characterClass.Name = "class name";
            characterClass.Level = 15;
            alignment.Goodness = "goodness";
            racePrototype.BaseRace = BaseRace;
            racePrototype.Metarace = Metarace;

            SetUpTablesForBaseRace(BaseRace);
            SetUpTablesForMetarace(Metarace);

            var endRoll = new Mock<PartialRoll>();
            endRoll.Setup(r => r.AsSum()).Returns(5);

            var mockPartial = new Mock<PartialRoll>();
            mockPartial.Setup(r => r.d(It.IsAny<int>())).Returns(endRoll.Object);
            mockDice.Setup(d => d.Roll(characterClass.Level)).Returns(mockPartial.Object);

            mockCollectionsSelector.Setup(s => s.FindCollectionOf(TableNameConstants.Set.Collection.ClassNameGroups, characterClass.Name, CharacterClassConstants.TrainingTypes.Intuitive, CharacterClassConstants.TrainingTypes.SelfTaught, CharacterClassConstants.TrainingTypes.Trained))
                .Returns(() => classType);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.BaseRaceGroups, GroupConstants.HasWings)).Returns(baseRacesWithWings);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.HasWings)).Returns(metaracesWithWings);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AerialManeuverability, It.IsAny<string>())).Returns((string table, string name) => aerialManeuverability[name]);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.ChallengeRatings, It.IsAny<string>())).Returns((string table, string name) => challengeRatings[name]);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.AerialSpeeds, It.IsAny<string>())).Returns((string table, string name) => aerialSpeeds[name]);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.LandSpeeds, It.IsAny<string>())).Returns((string table, string name) => landSpeeds[name]);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Adjustments.SwimSpeeds, It.IsAny<string>())).Returns((string table, string name) => swimSpeeds[name]);

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, "Male");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName, It.IsAny<string>())).Returns((string table, string name) => maleHeights[name]);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERHeights, "Female");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName, It.IsAny<string>())).Returns((string table, string name) => femaleHeights[name]);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, "Male");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName, It.IsAny<string>())).Returns((string table, string name) => maleWeights[name]);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.GENDERWeights, "Female");
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName, It.IsAny<string>())).Returns((string table, string name) => femaleWeights[name]);
        }

        private void SetUpRoll(string roll, int result, int min = 0, int max = int.MaxValue)
        {
            var mockPartial = new Mock<PartialRoll>();

            var count = 0;
            mockPartial.Setup(r => r.AsSum()).Returns(() => result + count++);
            mockPartial.Setup(r => r.AsPotentialMaximum()).Returns(max);
            mockPartial.Setup(r => r.AsPotentialMinimum()).Returns(min);

            mockDice.Setup(d => d.Roll(roll)).Returns(mockPartial.Object);
        }

        private void SetUpTablesForBaseRace(string baseRace)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, CharacterClassConstants.TrainingTypes.Trained);
            mockCollectionsSelector.Setup(s => s.SelectFrom(tableName, baseRace)).Returns(new[] { "4200d60000" });
            SetUpRoll("4200d60000", 34);

            tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, CharacterClassConstants.TrainingTypes.SelfTaught);
            mockCollectionsSelector.Setup(s => s.SelectFrom(tableName, baseRace)).Returns(new[] { "420d6000" });
            SetUpRoll("420d6000", 24);

            tableName = string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, CharacterClassConstants.TrainingTypes.Intuitive);
            mockCollectionsSelector.Setup(s => s.SelectFrom(tableName, baseRace)).Returns(new[] { "42d600" });
            SetUpRoll("42d600", 14);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MaximumAgeRolls, baseRace)).Returns(new[] { "4d26" });
            SetUpRoll("4d26", 1000);

            ages = new Dictionary<string, int>();
            ages[SizeConstants.Ages.Adulthood] = 90210;
            ages[SizeConstants.Ages.MiddleAge] = 90300;
            ages[SizeConstants.Ages.Old] = 90450;
            ages[SizeConstants.Ages.Venerable] = 90600;

            maleHeights[baseRace] = 209;
            femaleHeights[baseRace] = 902;
            maleWeights[baseRace] = 22;
            femaleWeights[baseRace] = 2;

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, baseRace);
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(ages);
            mockAdjustmentsSelector.Setup(s => s.SelectFrom(tableName, It.IsAny<string>())).Returns((string table, string name) => ages[name]);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.HeightRolls, baseRace)).Returns(new[] { "10d4" });
            SetUpRoll("10d4", 104);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.WeightRolls, baseRace)).Returns(new[] { "92d66" });
            SetUpRoll("92d66", 424);

            mockCollectionsSelector.Setup(s => s.FindCollectionOf(TableNameConstants.Set.Collection.BaseRaceGroups, baseRace, It.IsAny<string[]>())).Returns(() => baseRaceSize);

            if (landSpeeds.Any())
                landSpeeds[baseRace] = landSpeeds.Last().Value + 1;
            else
                landSpeeds[baseRace] = 9266;

            aerialSpeeds[baseRace] = 0;
            swimSpeeds[baseRace] = 0;
            aerialManeuverability[baseRace] = new List<string> { string.Empty };
            challengeRatings[baseRace] = 0;
        }

        private void SetUpTablesForMetarace(string metarace)
        {
            aerialSpeeds[metarace] = 0;
            aerialManeuverability[metarace] = new List<string> { string.Empty };
            challengeRatings[metarace] = 0;
        }

        [Test]
        public void GeneratePrototype()
        {
            var classPrototype = new CharacterClassPrototype();
            classPrototype.Level = 1029;
            classPrototype.Name = "class prototype name";

            mockBaseRaceRandomizer.Setup(r => r.Randomize(alignment, classPrototype)).Returns("random base race");
            mockMetaraceRandomizer.Setup(r => r.Randomize(alignment, classPrototype)).Returns("random metarace");

            var prototype = raceGenerator.GeneratePrototype(alignment, classPrototype, mockBaseRaceRandomizer.Object, mockMetaraceRandomizer.Object);
            Assert.That(prototype.BaseRace, Is.EqualTo("random base race"));
            Assert.That(prototype.Metarace, Is.EqualTo("random metarace"));
        }

        [Test]
        public void GenerateRaceFromPrototype()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.BaseRace, Is.EqualTo(BaseRace));
            Assert.That(race.Metarace, Is.EqualTo(Metarace));
        }

        [Test]
        public void ReturnMale()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.Male)).Returns(true);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.IsMale, Is.True);
        }

        [Test]
        public void ReturnFemale()
        {
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.Male)).Returns(false);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.IsMale, Is.False);
        }

        [Test]
        public void RaceGeneratorReturnsMaleForDrowWizard()
        {
            characterClass.Name = CharacterClassConstants.Wizard;
            racePrototype.BaseRace = SizeConstants.BaseRaces.Drow;

            mockCollectionsSelector.Setup(s => s.FindCollectionOf(TableNameConstants.Set.Collection.ClassNameGroups, CharacterClassConstants.Wizard, CharacterClassConstants.TrainingTypes.Intuitive, CharacterClassConstants.TrainingTypes.SelfTaught, CharacterClassConstants.TrainingTypes.Trained))
                .Returns(() => classType);

            SetUpTablesForBaseRace(SizeConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.IsMale, Is.True);
        }

        [Test]
        public void RaceGeneratorReturnsFemaleForDrowCleric()
        {
            characterClass.Name = CharacterClassConstants.Cleric;
            racePrototype.BaseRace = SizeConstants.BaseRaces.Drow;

            mockCollectionsSelector.Setup(s => s.FindCollectionOf(TableNameConstants.Set.Collection.ClassNameGroups, CharacterClassConstants.Cleric, CharacterClassConstants.TrainingTypes.Intuitive, CharacterClassConstants.TrainingTypes.SelfTaught, CharacterClassConstants.TrainingTypes.Trained))
                .Returns(() => classType);

            SetUpTablesForBaseRace(SizeConstants.BaseRaces.Drow);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.IsMale, Is.False);
        }

        [Test]
        public void GetRaceSize()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Size, Is.EqualTo(SizeConstants.Sizes.Medium));
        }

        [Test]
        public void MetaraceHasWings()
        {
            metaracesWithWings.Add(Metarace);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);

            Assert.That(race.Metarace, Is.EqualTo(Metarace));
            Assert.That(race.HasWings, Is.True);
        }

        [Test]
        public void BaseRaceHasWings()
        {
            baseRacesWithWings.Add(BaseRace);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.BaseRace, Is.EqualTo(BaseRace));
            Assert.That(race.HasWings, Is.True);
        }

        [Test]
        public void BaseRaceAndMetaraceHaveWings()
        {
            baseRacesWithWings.Add(BaseRace);
            metaracesWithWings.Add(Metarace);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.BaseRace, Is.EqualTo(BaseRace));
            Assert.That(race.Metarace, Is.EqualTo(Metarace));
            Assert.That(race.HasWings, Is.True);
        }

        [TestCase(SizeConstants.Sizes.Colossal)]
        [TestCase(SizeConstants.Sizes.Gargantuan)]
        [TestCase(SizeConstants.Sizes.Huge)]
        [TestCase(SizeConstants.Sizes.Large)]
        [TestCase(SizeConstants.Sizes.Medium)]
        [TestCase(SizeConstants.Sizes.Small)]
        [TestCase(SizeConstants.Sizes.Tiny)]
        public void BaseRaceAndHalfDragonHaveWings(string size)
        {
            SetUpTablesForMetarace(SizeConstants.Metaraces.HalfDragon);
            aerialSpeeds[SizeConstants.Metaraces.HalfDragon] = 2;
            racePrototype.Metarace = SizeConstants.Metaraces.HalfDragon;

            baseRacesWithWings.Add(BaseRace);

            baseRaceSize = size;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.BaseRace, Is.EqualTo(BaseRace));
            Assert.That(race.Metarace, Is.EqualTo(SizeConstants.Metaraces.HalfDragon));
            Assert.That(race.HasWings, Is.True);
        }

        [Test]
        public void NoWings()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.HasWings, Is.False);
        }

        [TestCase(SizeConstants.Sizes.Colossal)]
        [TestCase(SizeConstants.Sizes.Gargantuan)]
        [TestCase(SizeConstants.Sizes.Huge)]
        [TestCase(SizeConstants.Sizes.Large)]
        public void HalfDragonsHaveWings(string size)
        {
            SetUpTablesForMetarace(SizeConstants.Metaraces.HalfDragon);
            aerialSpeeds[SizeConstants.Metaraces.HalfDragon] = 2;
            racePrototype.Metarace = SizeConstants.Metaraces.HalfDragon;

            baseRaceSize = size;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.HasWings, Is.True);
        }

        [TestCase(SizeConstants.Sizes.Medium)]
        [TestCase(SizeConstants.Sizes.Small)]
        [TestCase(SizeConstants.Sizes.Tiny)]
        public void HalfDragonsDoNotHaveWings(string size)
        {
            SetUpTablesForMetarace(SizeConstants.Metaraces.HalfDragon);
            aerialSpeeds[SizeConstants.Metaraces.HalfDragon] = 2;
            racePrototype.Metarace = SizeConstants.Metaraces.HalfDragon;

            baseRaceSize = size;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.HasWings, Is.False);
        }

        [Test]
        public void DetermineSpeciesOfHalfDragonByRandomWithinAlignment()
        {
            SetUpTablesForMetarace(SizeConstants.Metaraces.HalfDragon);
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";
            aerialSpeeds[SizeConstants.Metaraces.HalfDragon] = 2;
            racePrototype.Metarace = SizeConstants.Metaraces.HalfDragon;

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(TableNameConstants.Set.Collection.DragonSpecies, "lawfulness goodness")).Returns("dragon species");

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.MetaraceSpecies, Is.EqualTo("dragon species"));
        }

        [Test]
        public void NonHalfDragonsDoNotDetermineSpecies()
        {
            alignment.Goodness = "goodness";
            alignment.Lawfulness = "lawfulness";

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            mockCollectionsSelector.Verify(s => s.SelectFrom(TableNameConstants.Set.Collection.DragonSpecies, It.IsAny<string>()), Times.Never);
            Assert.That(race.MetaraceSpecies, Is.Empty);
        }

        [Test]
        public void DetermineLandSpeed()
        {
            landSpeeds["other base race"] = 42;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.LandSpeed.Value, Is.EqualTo(9266));
            Assert.That(race.LandSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(race.LandSpeed.Description, Is.Empty);
        }

        [Test]
        public void MetaraceAerialSpeedIsSet()
        {
            aerialSpeeds[Metarace] = 30;
            aerialSpeeds[BaseRace] = 20;
            aerialManeuverability[Metarace][0] = "awkward maneuverability";

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.AerialSpeed.Value, Is.EqualTo(30));
            Assert.That(race.AerialSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(race.AerialSpeed.Description, Is.EqualTo("awkward maneuverability"));
        }

        [Test]
        public void MetaraceAerialSpeedIsSet_ButBaseRaceAerialSpeedIsHigher()
        {
            aerialSpeeds[Metarace] = 30;
            aerialSpeeds[BaseRace] = 40;
            aerialManeuverability[Metarace][0] = "awkward maneuverability";
            aerialManeuverability[BaseRace][0] = "awesome maneuverability";

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.AerialSpeed.Value, Is.EqualTo(40));
            Assert.That(race.AerialSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(race.AerialSpeed.Description, Is.EqualTo("awesome maneuverability"));
        }

        [Test]
        public void BaseRaceAerialSpeedIsSet()
        {
            SetUpTablesForMetarace(SizeConstants.Metaraces.HalfDragon);

            aerialSpeeds[SizeConstants.Metaraces.HalfDragon] = 2;
            aerialSpeeds[BaseRace] = 20;
            aerialManeuverability[BaseRace][0] = "awkward maneuverability";

            racePrototype.Metarace = SizeConstants.Metaraces.HalfDragon;
            baseRaceSize = SizeConstants.Sizes.Large;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.AerialSpeed.Value, Is.EqualTo(20));
            Assert.That(race.AerialSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(race.AerialSpeed.Description, Is.EqualTo("awkward maneuverability"));
        }

        [Test]
        public void MetaraceAerialSpeedIsMultiplierWithWings()
        {
            SetUpTablesForMetarace(SizeConstants.Metaraces.HalfDragon);
            aerialSpeeds[SizeConstants.Metaraces.HalfDragon] = 2;
            aerialSpeeds[BaseRace] = 0;
            aerialManeuverability[SizeConstants.Metaraces.HalfDragon][0] = "awkward maneuverability";

            racePrototype.Metarace = SizeConstants.Metaraces.HalfDragon;
            baseRaceSize = SizeConstants.Sizes.Large;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.AerialSpeed.Value, Is.EqualTo(9266 * 2));
            Assert.That(race.AerialSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(race.AerialSpeed.Description, Is.EqualTo("awkward maneuverability"));
        }

        [Test]
        public void MetaraceAerialSpeedIsMultiplierWithoutWings()
        {
            SetUpTablesForMetarace(SizeConstants.Metaraces.HalfDragon);
            aerialSpeeds[SizeConstants.Metaraces.HalfDragon] = 2;
            aerialSpeeds[BaseRace] = 0;
            aerialManeuverability[SizeConstants.Metaraces.HalfDragon][0] = "awkward maneuverability";
            racePrototype.Metarace = SizeConstants.Metaraces.HalfDragon;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.AerialSpeed.Value, Is.EqualTo(0));
            Assert.That(race.AerialSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(race.AerialSpeed.Description, Is.Empty);
        }

        [Test]
        public void NoAerialSpeed()
        {
            aerialSpeeds[Metarace] = 0;
            aerialSpeeds[BaseRace] = 0;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.AerialSpeed.Value, Is.EqualTo(0));
            Assert.That(race.AerialSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(race.AerialSpeed.Description, Is.Empty);
        }

        [Test]
        public void GetSwimSpeed()
        {
            swimSpeeds[BaseRace] = 42;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.SwimSpeed.Value, Is.EqualTo(42));
            Assert.That(race.SwimSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(race.SwimSpeed.Description, Is.Empty);
        }

        [Test]
        public void NoSwimSpeed()
        {
            swimSpeeds[BaseRace] = 0;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.SwimSpeed.Value, Is.EqualTo(0));
            Assert.That(race.SwimSpeed.Unit, Is.EqualTo("feet per round"));
            Assert.That(race.SwimSpeed.Description, Is.Empty);
        }

        [TestCase(1, "Short")]
        [TestCase(2, "Average")]
        [TestCase(3, "Tall")]
        public void GetMaleHeight(int roll, string description)
        {
            SetUpRoll("10d4", roll, 1, 3);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.Male)).Returns(true);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Height.Value, Is.EqualTo(209 + roll));
            Assert.That(race.Height.Unit, Is.EqualTo("Inches"));
            Assert.That(race.Height.Description, Is.EqualTo(description));
        }

        [Test]
        public void GetNonRandomMaleHeight()
        {
            SetUpRoll("10d4", 1, 1, 1);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.Male)).Returns(true);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Height.Value, Is.EqualTo(210));
            Assert.That(race.Height.Unit, Is.EqualTo("Inches"));
            Assert.That(race.Height.Description, Is.EqualTo("Average"));
        }

        [TestCase(1, "Short")]
        [TestCase(2, "Average")]
        [TestCase(3, "Tall")]
        public void GetFemaleHeight(int roll, string description)
        {
            SetUpRoll("10d4", roll, 1, 3);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.Male)).Returns(false);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Height.Value, Is.EqualTo(902 + roll));
            Assert.That(race.Height.Unit, Is.EqualTo("Inches"));
            Assert.That(race.Height.Description, Is.EqualTo(description));
        }

        [Test]
        public void GetNonRandomFemaleHeight()
        {
            SetUpRoll("10d4", 1, 1, 1);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.Male)).Returns(false);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Height.Value, Is.EqualTo(903));
            Assert.That(race.Height.Unit, Is.EqualTo("Inches"));
            Assert.That(race.Height.Description, Is.EqualTo("Average"));
        }

        [TestCase(1, "Light")]
        [TestCase(2, "Average")]
        [TestCase(3, "Heavy")]
        public void GetMaleWeight(int roll, string description)
        {
            SetUpRoll("92d66", roll, 1, 3);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.Male)).Returns(true);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Weight.Value, Is.EqualTo(22 + 104 * roll));
            Assert.That(race.Weight.Unit, Is.EqualTo("Pounds"));
            Assert.That(race.Weight.Description, Is.EqualTo(description));
        }

        [Test]
        public void GetNonRandomMaleWeight()
        {
            SetUpRoll("92d66", 1, 1, 1);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.Male)).Returns(true);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Weight.Value, Is.EqualTo(126));
            Assert.That(race.Weight.Unit, Is.EqualTo("Pounds"));
            Assert.That(race.Weight.Description, Is.EqualTo("Average"));
        }

        [TestCase(1, "Light")]
        [TestCase(2, "Average")]
        [TestCase(3, "Heavy")]
        public void GetFemaleWeight(int roll, string description)
        {
            SetUpRoll("92d66", roll, 1, 3);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.Male)).Returns(false);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Weight.Value, Is.EqualTo(2 + 104 * roll));
            Assert.That(race.Weight.Unit, Is.EqualTo("Pounds"));
            Assert.That(race.Weight.Description, Is.EqualTo(description));
        }

        [Test]
        public void GetNonRandomFemaleWeight()
        {
            SetUpRoll("92d66", 1, 1, 1);
            mockPercentileSelector.Setup(s => s.SelectFrom<bool>(TableNameConstants.Set.TrueOrFalse.Male)).Returns(false);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Weight.Value, Is.EqualTo(106));
            Assert.That(race.Weight.Unit, Is.EqualTo("Pounds"));
            Assert.That(race.Weight.Description, Is.EqualTo("Average"));
        }

        [TestCase(CharacterClassConstants.TrainingTypes.Intuitive, 90525)]
        [TestCase(CharacterClassConstants.TrainingTypes.SelfTaught, 90675)]
        [TestCase(CharacterClassConstants.TrainingTypes.Trained, 90825)]
        public void GetAgeByClassType(string classTypeForAge, int age)
        {
            classType = classTypeForAge;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Age.Value, Is.EqualTo(age));
            Assert.That(race.Age.Unit, Is.EqualTo("Years"));
            Assert.That(race.Age.Description, Is.Not.Empty);
        }

        [TestCase(1, 90224, SizeConstants.Ages.Adulthood)]
        [TestCase(2, 90239, SizeConstants.Ages.Adulthood)]
        [TestCase(3, 90255, SizeConstants.Ages.Adulthood)]
        [TestCase(4, 90272, SizeConstants.Ages.Adulthood)]
        [TestCase(5, 90290, SizeConstants.Ages.Adulthood)]
        [TestCase(6, 90309, SizeConstants.Ages.MiddleAge)]
        [TestCase(7, 90329, SizeConstants.Ages.MiddleAge)]
        [TestCase(8, 90350, SizeConstants.Ages.MiddleAge)]
        [TestCase(9, 90372, SizeConstants.Ages.MiddleAge)]
        [TestCase(10, 90395, SizeConstants.Ages.MiddleAge)]
        [TestCase(11, 90419, SizeConstants.Ages.MiddleAge)]
        [TestCase(12, 90444, SizeConstants.Ages.MiddleAge)]
        [TestCase(13, 90470, SizeConstants.Ages.Old)]
        [TestCase(14, 90497, SizeConstants.Ages.Old)]
        [TestCase(15, 90525, SizeConstants.Ages.Old)]
        [TestCase(16, 90554, SizeConstants.Ages.Old)]
        [TestCase(17, 90584, SizeConstants.Ages.Old)]
        [TestCase(18, 90615, SizeConstants.Ages.Venerable)]
        [TestCase(19, 90647, SizeConstants.Ages.Venerable)]
        [TestCase(20, 90680, SizeConstants.Ages.Venerable)]
        public void AgeIncreasesAsCharacterLevelsUp(int level, int age, string description)
        {
            characterClass.Level = level;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Age.Value, Is.EqualTo(age));
            Assert.That(race.Age.Unit, Is.EqualTo("Years"));
            Assert.That(race.Age.Description, Is.EqualTo(description));
        }

        [TestCase(SizeConstants.Ages.Adulthood, SizeConstants.Ages.Ageless, SizeConstants.Ages.Ageless, SizeConstants.Ages.Ageless)]
        [TestCase(SizeConstants.Ages.MiddleAge, 90300, SizeConstants.Ages.Ageless, SizeConstants.Ages.Ageless)]
        [TestCase(SizeConstants.Ages.Old, 90300, 90450, SizeConstants.Ages.Ageless)]
        public void GetAgeless(string description, int middleAge, int old, int venerable)
        {
            characterClass.Level = 20;

            ages[SizeConstants.Ages.MiddleAge] = middleAge;
            ages[SizeConstants.Ages.Old] = old;
            ages[SizeConstants.Ages.Venerable] = venerable;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Age.Value, Is.EqualTo(90680));
            Assert.That(race.Age.Unit, Is.EqualTo("Years"));
            Assert.That(race.Age.Description, Is.EqualTo(description));
        }

        [Test]
        public void GetMaximumAge()
        {
            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.MaximumAge.Value, Is.EqualTo(91600));
            Assert.That(race.MaximumAge.Unit, Is.EqualTo("Years"));
            Assert.That(race.MaximumAge.Description, Is.EqualTo("Will die of natural causes"));
        }

        [Test]
        public void GetImmortalAge()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MaximumAgeRolls, BaseRace)).Returns(new[] { SizeConstants.Ages.Ageless.ToString() });
            SetUpRoll(SizeConstants.Ages.Ageless.ToString(), -1);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.MaximumAge.Value, Is.EqualTo(SizeConstants.Ages.Ageless));
            Assert.That(race.MaximumAge.Unit, Is.EqualTo("Years"));
            Assert.That(race.MaximumAge.Description, Is.EqualTo("Immortal"));
        }

        [Test]
        public void GetPixieMaximumAgeDescription()
        {
            racePrototype.BaseRace = SizeConstants.BaseRaces.Pixie;
            SetUpTablesForBaseRace(SizeConstants.BaseRaces.Pixie);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.MaximumAge.Value, Is.EqualTo(91600));
            Assert.That(race.MaximumAge.Unit, Is.EqualTo("Years"));
            Assert.That(race.MaximumAge.Description, Is.EqualTo("Will return to their plane of origin"));
        }

        [Test]
        public void UndeadAreImmortal()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead)).Returns(new[] { Metarace });

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.MaximumAge.Value, Is.EqualTo(SizeConstants.Ages.Ageless));
            Assert.That(race.MaximumAge.Unit, Is.EqualTo("Years"));
            Assert.That(race.MaximumAge.Description, Is.EqualTo("Immortal"));
        }

        [Test]
        public void GetMetaraceMortalAge()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MetaraceGroups, GroupConstants.Undead)).Returns(new[] { "other metarace" });

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.MaximumAge.Value, Is.EqualTo(91600));
            Assert.That(race.MaximumAge.Unit, Is.EqualTo("Years"));
            Assert.That(race.MaximumAge.Description, Is.EqualTo("Will die of natural causes"));
        }

        [Test]
        public void IfTooOld_RerollAge()
        {
            var mockPartial = new Mock<PartialRoll>();
            mockPartial.SetupSequence(r => r.AsSum()).Returns(1391).Returns(1389);
            mockDice.Setup(d => d.Roll("42d600")).Returns(mockPartial.Object);

            characterClass.Level = 1;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Age.Value, Is.LessThanOrEqualTo(race.MaximumAge.Value));
            Assert.That(race.MaximumAge.Value, Is.EqualTo(91600));
            Assert.That(race.Age.Value, Is.EqualTo(91599));
        }

        [Test]
        public void DoNotRerollImmortalAge()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.MaximumAgeRolls, BaseRace)).Returns(new[] { SizeConstants.Ages.Ageless.ToString() });
            SetUpRoll(SizeConstants.Ages.Ageless.ToString(), -1);

            characterClass.Level = 1;
            SetUpRoll("42d600", 2000);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Age.Value, Is.EqualTo(92210));
            Assert.That(race.Age.Unit, Is.EqualTo("Years"));
            Assert.That(race.Age.Description, Is.EqualTo(SizeConstants.Ages.Venerable));
            Assert.That(race.MaximumAge.Value, Is.EqualTo(SizeConstants.Ages.Ageless));
            Assert.That(race.MaximumAge.Unit, Is.EqualTo("Years"));
            Assert.That(race.MaximumAge.Description, Is.EqualTo("Immortal"));
        }

        [Test]
        public void IfTooOld_RerollAgeAsMaximum()
        {
            var mockPartial = new Mock<PartialRoll>();
            mockPartial.SetupSequence(r => r.AsSum()).Returns(1391).Returns(1390);
            mockDice.Setup(d => d.Roll("42d600")).Returns(mockPartial.Object);

            characterClass.Level = 1;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Age.Value, Is.LessThanOrEqualTo(race.MaximumAge.Value));
            Assert.That(race.MaximumAge.Value, Is.EqualTo(91600));
            Assert.That(race.Age.Value, Is.EqualTo(91600));
        }

        [Test]
        public void UseDefaultAgeOfMaximum()
        {
            characterClass.Level = 21;

            ages[SizeConstants.Ages.MiddleAge] = 90215;
            ages[SizeConstants.Ages.Old] = 90220;
            ages[SizeConstants.Ages.Venerable] = 90225;
            SetUpRoll("4d26", 5);
            SetUpRoll("42d600", 1);

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.Age.Value, Is.LessThanOrEqualTo(race.MaximumAge.Value));
            Assert.That(race.MaximumAge.Value, Is.EqualTo(90230));
            Assert.That(race.Age.Value, Is.EqualTo(90230));
        }

        [Test]
        public void SetBaseRaceChallengeRating()
        {
            challengeRatings[BaseRace] = 9266;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.ChallengeRating, Is.EqualTo(9266));
        }

        [Test]
        public void SetMetaraceChallengeRating()
        {
            challengeRatings[Metarace] = 90210;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.ChallengeRating, Is.EqualTo(90210));
        }

        [Test]
        public void SetBaseRaceAndMetaraceChallengeRating()
        {
            challengeRatings[BaseRace] = 9266;
            challengeRatings[Metarace] = 90210;

            var race = raceGenerator.GenerateWith(alignment, characterClass, racePrototype);
            Assert.That(race.ChallengeRating, Is.EqualTo(9266 + 90210));
        }
    }
}