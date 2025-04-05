using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Selectors.Selections
{
    [TestFixture]
    public class CreatureDataSelectionTests
    {
        private CreatureDataSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new CreatureDataSelection();
        }

        [Test]
        public void CreatureDataSelectionInitialized()
        {
            Assert.That(selection.ChallengeRating, Is.Empty);
            Assert.That(selection.Size, Is.Empty);
            Assert.That(selection.Reach, Is.Zero);
            Assert.That(selection.Space, Is.Zero);
            Assert.That(selection.NumberOfHands, Is.Zero);
            Assert.That(selection.NaturalArmor, Is.Zero);
            Assert.That(selection.CasterLevel, Is.Zero);
            Assert.That(selection.LevelAdjustment, Is.Null);
            Assert.That(selection.CanUseEquipment, Is.False);
        }

        [Test]
        public void SectionCountIs9()
        {
            Assert.That(selection.SectionCount, Is.EqualTo(9));
        }

        [Test]
        public void Map_FromString_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.CreatureData.ChallengeRating] = "my cr";
            data[DataIndexConstants.CreatureData.Size] = "my size";
            data[DataIndexConstants.CreatureData.Reach] = "926.6";
            data[DataIndexConstants.CreatureData.Space] = "902.10";
            data[DataIndexConstants.CreatureData.NumberOfHands] = "42";
            data[DataIndexConstants.CreatureData.NaturalArmor] = "600";
            data[DataIndexConstants.CreatureData.CasterLevel] = "1337";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "1336";
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;

            var newSelection = CreatureDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.ChallengeRating, Is.EqualTo("my cr"));
            Assert.That(newSelection.Size, Is.EqualTo("my size"));
            Assert.That(newSelection.Reach, Is.EqualTo(926.6));
            Assert.That(newSelection.Space, Is.EqualTo(902.10));
            Assert.That(newSelection.NumberOfHands, Is.EqualTo(42));
            Assert.That(newSelection.NaturalArmor, Is.EqualTo(600));
            Assert.That(newSelection.CasterLevel, Is.EqualTo(1337));
            Assert.That(newSelection.LevelAdjustment, Is.EqualTo(1336));
            Assert.That(newSelection.CanUseEquipment, Is.EqualTo(true));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_NullLevelAdjustment()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.CreatureData.ChallengeRating] = "my cr";
            data[DataIndexConstants.CreatureData.Size] = "my size";
            data[DataIndexConstants.CreatureData.Reach] = "926.6";
            data[DataIndexConstants.CreatureData.Space] = "902.10";
            data[DataIndexConstants.CreatureData.NumberOfHands] = "42";
            data[DataIndexConstants.CreatureData.NaturalArmor] = "600";
            data[DataIndexConstants.CreatureData.CasterLevel] = "1337";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = string.Empty;
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;

            var newSelection = CreatureDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.ChallengeRating, Is.EqualTo("my cr"));
            Assert.That(newSelection.Size, Is.EqualTo("my size"));
            Assert.That(newSelection.Reach, Is.EqualTo(926.6));
            Assert.That(newSelection.Space, Is.EqualTo(902.10));
            Assert.That(newSelection.NumberOfHands, Is.EqualTo(42));
            Assert.That(newSelection.NaturalArmor, Is.EqualTo(600));
            Assert.That(newSelection.CasterLevel, Is.EqualTo(1337));
            Assert.That(newSelection.LevelAdjustment, Is.Null);
        }

        [Test]
        public void Map_FromString_ReturnsSelection_ZeroLevelAdjustment()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.CreatureData.ChallengeRating] = "my cr";
            data[DataIndexConstants.CreatureData.Size] = "my size";
            data[DataIndexConstants.CreatureData.Reach] = "926.6";
            data[DataIndexConstants.CreatureData.Space] = "902.10";
            data[DataIndexConstants.CreatureData.NumberOfHands] = "42";
            data[DataIndexConstants.CreatureData.NaturalArmor] = "600";
            data[DataIndexConstants.CreatureData.CasterLevel] = "1337";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "0";
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;

            var newSelection = CreatureDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.ChallengeRating, Is.EqualTo("my cr"));
            Assert.That(newSelection.Size, Is.EqualTo("my size"));
            Assert.That(newSelection.Reach, Is.EqualTo(926.6));
            Assert.That(newSelection.Space, Is.EqualTo(902.10));
            Assert.That(newSelection.NumberOfHands, Is.EqualTo(42));
            Assert.That(newSelection.NaturalArmor, Is.EqualTo(600));
            Assert.That(newSelection.CasterLevel, Is.EqualTo(1337));
            Assert.That(newSelection.LevelAdjustment, Is.Not.Null.And.Zero);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromString_ReturnsSelection_CanUseEquipment(bool equipment)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.CreatureData.ChallengeRating] = "my cr";
            data[DataIndexConstants.CreatureData.Size] = "my size";
            data[DataIndexConstants.CreatureData.Reach] = "926.6";
            data[DataIndexConstants.CreatureData.Space] = "902.10";
            data[DataIndexConstants.CreatureData.NumberOfHands] = "42";
            data[DataIndexConstants.CreatureData.NaturalArmor] = "600";
            data[DataIndexConstants.CreatureData.CasterLevel] = "1337";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "1336";
            data[DataIndexConstants.CreatureData.CanUseEquipment] = equipment.ToString();

            var newSelection = CreatureDataSelection.Map(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.ChallengeRating, Is.EqualTo("my cr"));
            Assert.That(newSelection.Size, Is.EqualTo("my size"));
            Assert.That(newSelection.Reach, Is.EqualTo(926.6));
            Assert.That(newSelection.Space, Is.EqualTo(902.10));
            Assert.That(newSelection.NumberOfHands, Is.EqualTo(42));
            Assert.That(newSelection.NaturalArmor, Is.EqualTo(600));
            Assert.That(newSelection.CasterLevel, Is.EqualTo(1337));
            Assert.That(newSelection.LevelAdjustment, Is.EqualTo(1336));
            Assert.That(newSelection.CanUseEquipment, Is.EqualTo(equipment));
        }

        [Test]
        public void Map_FromSelection_ReturnsString()
        {
            var selection = new CreatureDataSelection
            {
                ChallengeRating = "my cr",
                Size = "my size",
                Reach = 926.6,
                Space = 902.10,
                NumberOfHands = 42,
                NaturalArmor = 600,
                CasterLevel = 1337,
                LevelAdjustment = 1336,
                CanUseEquipment = true,
            };

            var rawData = CreatureDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.CreatureData.ChallengeRating], Is.EqualTo("my cr"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Size], Is.EqualTo("my size"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Reach], Is.EqualTo("926.6"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Space], Is.EqualTo("902.1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NumberOfHands], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NaturalArmor], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CasterLevel], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.CreatureData.LevelAdjustment], Is.EqualTo("1336"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(bool.TrueString));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_NullLevelAdjustment()
        {
            var selection = new CreatureDataSelection
            {
                ChallengeRating = "my cr",
                Size = "my size",
                Reach = 926.6,
                Space = 902.10,
                NumberOfHands = 42,
                NaturalArmor = 600,
                CasterLevel = 1337,
                LevelAdjustment = null,
                CanUseEquipment = true,
            };

            var rawData = CreatureDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.CreatureData.ChallengeRating], Is.EqualTo("my cr"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Size], Is.EqualTo("my size"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Reach], Is.EqualTo("926.6"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Space], Is.EqualTo("902.1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NumberOfHands], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NaturalArmor], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CasterLevel], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.CreatureData.LevelAdjustment], Is.Empty);
            Assert.That(rawData[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(bool.TrueString));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_ZeroLevelAdjustment()
        {
            var selection = new CreatureDataSelection
            {
                ChallengeRating = "my cr",
                Size = "my size",
                Reach = 926.6,
                Space = 902.10,
                NumberOfHands = 42,
                NaturalArmor = 600,
                CasterLevel = 1337,
                LevelAdjustment = 0,
                CanUseEquipment = true,
            };

            var rawData = CreatureDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.CreatureData.ChallengeRating], Is.EqualTo("my cr"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Size], Is.EqualTo("my size"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Reach], Is.EqualTo("926.6"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Space], Is.EqualTo("902.1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NumberOfHands], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NaturalArmor], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CasterLevel], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.CreatureData.LevelAdjustment], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(bool.TrueString));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromSelection_ReturnsString_CanUseEquipment(bool equipment)
        {
            var selection = new CreatureDataSelection
            {
                ChallengeRating = "my cr",
                Size = "my size",
                Reach = 926.6,
                Space = 902.10,
                NumberOfHands = 42,
                NaturalArmor = 600,
                CasterLevel = 1337,
                LevelAdjustment = 1336,
                CanUseEquipment = equipment,
            };

            var rawData = CreatureDataSelection.Map(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.CreatureData.ChallengeRating], Is.EqualTo("my cr"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Size], Is.EqualTo("my size"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Reach], Is.EqualTo("926.6"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Space], Is.EqualTo("902.1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NumberOfHands], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NaturalArmor], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CasterLevel], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.CreatureData.LevelAdjustment], Is.EqualTo("1336"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(equipment.ToString()));
        }

        [Test]
        public void MapTo_ReturnsSelection()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.CreatureData.ChallengeRating] = "my cr";
            data[DataIndexConstants.CreatureData.Size] = "my size";
            data[DataIndexConstants.CreatureData.Reach] = "926.6";
            data[DataIndexConstants.CreatureData.Space] = "902.10";
            data[DataIndexConstants.CreatureData.NumberOfHands] = "42";
            data[DataIndexConstants.CreatureData.NaturalArmor] = "600";
            data[DataIndexConstants.CreatureData.CasterLevel] = "1337";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "1336";
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.ChallengeRating, Is.EqualTo("my cr"));
            Assert.That(newSelection.Size, Is.EqualTo("my size"));
            Assert.That(newSelection.Reach, Is.EqualTo(926.6));
            Assert.That(newSelection.Space, Is.EqualTo(902.10));
            Assert.That(newSelection.NumberOfHands, Is.EqualTo(42));
            Assert.That(newSelection.NaturalArmor, Is.EqualTo(600));
            Assert.That(newSelection.CasterLevel, Is.EqualTo(1337));
            Assert.That(newSelection.LevelAdjustment, Is.EqualTo(1336));
            Assert.That(newSelection.CanUseEquipment, Is.EqualTo(true));
        }

        [Test]
        public void MapTo_FromString_ReturnsSelection_NullLevelAdjustment()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.CreatureData.ChallengeRating] = "my cr";
            data[DataIndexConstants.CreatureData.Size] = "my size";
            data[DataIndexConstants.CreatureData.Reach] = "926.6";
            data[DataIndexConstants.CreatureData.Space] = "902.10";
            data[DataIndexConstants.CreatureData.NumberOfHands] = "42";
            data[DataIndexConstants.CreatureData.NaturalArmor] = "600";
            data[DataIndexConstants.CreatureData.CasterLevel] = "1337";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = string.Empty;
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.ChallengeRating, Is.EqualTo("my cr"));
            Assert.That(newSelection.Size, Is.EqualTo("my size"));
            Assert.That(newSelection.Reach, Is.EqualTo(926.6));
            Assert.That(newSelection.Space, Is.EqualTo(902.10));
            Assert.That(newSelection.NumberOfHands, Is.EqualTo(42));
            Assert.That(newSelection.NaturalArmor, Is.EqualTo(600));
            Assert.That(newSelection.CasterLevel, Is.EqualTo(1337));
            Assert.That(newSelection.LevelAdjustment, Is.Null);
        }

        [Test]
        public void MapTo_FromString_ReturnsSelection_ZeroLevelAdjustment()
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.CreatureData.ChallengeRating] = "my cr";
            data[DataIndexConstants.CreatureData.Size] = "my size";
            data[DataIndexConstants.CreatureData.Reach] = "926.6";
            data[DataIndexConstants.CreatureData.Space] = "902.10";
            data[DataIndexConstants.CreatureData.NumberOfHands] = "42";
            data[DataIndexConstants.CreatureData.NaturalArmor] = "600";
            data[DataIndexConstants.CreatureData.CasterLevel] = "1337";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "0";
            data[DataIndexConstants.CreatureData.CanUseEquipment] = bool.TrueString;

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.ChallengeRating, Is.EqualTo("my cr"));
            Assert.That(newSelection.Size, Is.EqualTo("my size"));
            Assert.That(newSelection.Reach, Is.EqualTo(926.6));
            Assert.That(newSelection.Space, Is.EqualTo(902.10));
            Assert.That(newSelection.NumberOfHands, Is.EqualTo(42));
            Assert.That(newSelection.NaturalArmor, Is.EqualTo(600));
            Assert.That(newSelection.CasterLevel, Is.EqualTo(1337));
            Assert.That(newSelection.LevelAdjustment, Is.Not.Null.And.Zero);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapTo_FromString_ReturnsSelection_CanUseEquipment(bool equipment)
        {
            var data = new string[selection.SectionCount];
            data[DataIndexConstants.CreatureData.ChallengeRating] = "my cr";
            data[DataIndexConstants.CreatureData.Size] = "my size";
            data[DataIndexConstants.CreatureData.Reach] = "926.6";
            data[DataIndexConstants.CreatureData.Space] = "902.10";
            data[DataIndexConstants.CreatureData.NumberOfHands] = "42";
            data[DataIndexConstants.CreatureData.NaturalArmor] = "600";
            data[DataIndexConstants.CreatureData.CasterLevel] = "1337";
            data[DataIndexConstants.CreatureData.LevelAdjustment] = "1336";
            data[DataIndexConstants.CreatureData.CanUseEquipment] = equipment.ToString();

            var newSelection = selection.MapTo(data);
            Assert.That(newSelection, Is.Not.Null);
            Assert.That(newSelection.ChallengeRating, Is.EqualTo("my cr"));
            Assert.That(newSelection.Size, Is.EqualTo("my size"));
            Assert.That(newSelection.Reach, Is.EqualTo(926.6));
            Assert.That(newSelection.Space, Is.EqualTo(902.10));
            Assert.That(newSelection.NumberOfHands, Is.EqualTo(42));
            Assert.That(newSelection.NaturalArmor, Is.EqualTo(600));
            Assert.That(newSelection.CasterLevel, Is.EqualTo(1337));
            Assert.That(newSelection.LevelAdjustment, Is.EqualTo(1336));
            Assert.That(newSelection.CanUseEquipment, Is.EqualTo(equipment));
        }

        [Test]
        public void MapFrom_ReturnsString()
        {
            var selection = new CreatureDataSelection
            {
                ChallengeRating = "my cr",
                Size = "my size",
                Reach = 926.6,
                Space = 902.10,
                NumberOfHands = 42,
                NaturalArmor = 600,
                CasterLevel = 1337,
                LevelAdjustment = 1336,
                CanUseEquipment = true,
            };

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.CreatureData.ChallengeRating], Is.EqualTo("my cr"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Size], Is.EqualTo("my size"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Reach], Is.EqualTo("926.6"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Space], Is.EqualTo("902.1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NumberOfHands], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NaturalArmor], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CasterLevel], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.CreatureData.LevelAdjustment], Is.EqualTo("1336"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(bool.TrueString));
        }

        [Test]
        public void MapFrom_FromSelection_ReturnsString_NullLevelAdjustment()
        {
            var selection = new CreatureDataSelection
            {
                ChallengeRating = "my cr",
                Size = "my size",
                Reach = 926.6,
                Space = 902.10,
                NumberOfHands = 42,
                NaturalArmor = 600,
                CasterLevel = 1337,
                LevelAdjustment = null,
                CanUseEquipment = true,
            };

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.CreatureData.ChallengeRating], Is.EqualTo("my cr"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Size], Is.EqualTo("my size"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Reach], Is.EqualTo("926.6"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Space], Is.EqualTo("902.1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NumberOfHands], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NaturalArmor], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CasterLevel], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.CreatureData.LevelAdjustment], Is.Empty);
            Assert.That(rawData[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(bool.TrueString));
        }

        [Test]
        public void MapFrom_FromSelection_ReturnsString_ZeroLevelAdjustment()
        {
            var selection = new CreatureDataSelection
            {
                ChallengeRating = "my cr",
                Size = "my size",
                Reach = 926.6,
                Space = 902.10,
                NumberOfHands = 42,
                NaturalArmor = 600,
                CasterLevel = 1337,
                LevelAdjustment = 0,
                CanUseEquipment = true,
            };

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.CreatureData.ChallengeRating], Is.EqualTo("my cr"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Size], Is.EqualTo("my size"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Reach], Is.EqualTo("926.6"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Space], Is.EqualTo("902.1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NumberOfHands], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NaturalArmor], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CasterLevel], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.CreatureData.LevelAdjustment], Is.EqualTo("0"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(bool.TrueString));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapFrom_FromSelection_ReturnsString_CanUseEquipment(bool equipment)
        {
            var selection = new CreatureDataSelection
            {
                ChallengeRating = "my cr",
                Size = "my size",
                Reach = 926.6,
                Space = 902.10,
                NumberOfHands = 42,
                NaturalArmor = 600,
                CasterLevel = 1337,
                LevelAdjustment = 1336,
                CanUseEquipment = equipment,
            };

            var rawData = selection.MapFrom(selection);
            Assert.That(rawData.Length, Is.EqualTo(selection.SectionCount));
            Assert.That(rawData[DataIndexConstants.CreatureData.ChallengeRating], Is.EqualTo("my cr"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Size], Is.EqualTo("my size"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Reach], Is.EqualTo("926.6"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Space], Is.EqualTo("902.1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NumberOfHands], Is.EqualTo("42"));
            Assert.That(rawData[DataIndexConstants.CreatureData.NaturalArmor], Is.EqualTo("600"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CasterLevel], Is.EqualTo("1337"));
            Assert.That(rawData[DataIndexConstants.CreatureData.LevelAdjustment], Is.EqualTo("1336"));
            Assert.That(rawData[DataIndexConstants.CreatureData.CanUseEquipment], Is.EqualTo(equipment.ToString()));
        }
    }
}
