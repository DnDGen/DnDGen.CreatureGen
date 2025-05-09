using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System;

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
            Assert.That(selection.BaseAttackQuality, Is.EqualTo(BaseAttackQuality.Poor));
            Assert.That(selection.Types, Is.Empty);
            Assert.That(selection.HitDiceQuantity, Is.Zero);
            Assert.That(selection.HitDie, Is.Zero);
            Assert.That(selection.HasSkeleton, Is.False);
        }

        [Test]
        public void SectionCountIs14()
        {
            Assert.That(selection.SectionCount, Is.EqualTo(14));
        }

        [Test]
        public void DelimiterIsDistinctFromSeparator()
        {
            Assert.That(CreatureDataSelection.Delimiter, Is.EqualTo('|').And.Not.EqualTo(selection.Separator));
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
            data[DataIndexConstants.CreatureData.BaseAttackQuality] = "1";
            data[DataIndexConstants.CreatureData.Types] = "my type";
            data[DataIndexConstants.CreatureData.HitDiceQuantity] = "9.6";
            data[DataIndexConstants.CreatureData.HitDie] = "783";
            data[DataIndexConstants.CreatureData.HasSkeleton] = bool.TrueString;

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
            Assert.That(newSelection.BaseAttackQuality, Is.EqualTo(BaseAttackQuality.Average));
            Assert.That(newSelection.Types, Is.EqualTo(["my type"]));
            Assert.That(newSelection.HitDiceQuantity, Is.EqualTo(9.6));
            Assert.That(newSelection.HitDie, Is.EqualTo(783));
            Assert.That(newSelection.HasSkeleton, Is.EqualTo(true));
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

        [TestCase(BaseAttackQuality.Poor)]
        [TestCase(BaseAttackQuality.Average)]
        [TestCase(BaseAttackQuality.Good)]
        internal void Map_FromString_ReturnsSelection_BaseAttackQuality(BaseAttackQuality quality)
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
            data[DataIndexConstants.CreatureData.BaseAttackQuality] = Convert.ToInt32(quality).ToString();

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
            Assert.That(newSelection.BaseAttackQuality, Is.EqualTo(quality));
        }

        [Test]
        public void Map_FromString_ReturnsSelection_WithSubtypes()
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
            data[DataIndexConstants.CreatureData.BaseAttackQuality] = "1";
            data[DataIndexConstants.CreatureData.Types] = "my type|my subtype|my other subtype";

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
            Assert.That(newSelection.BaseAttackQuality, Is.EqualTo(BaseAttackQuality.Average));
            Assert.That(newSelection.Types, Is.EqualTo(["my type", "my subtype", "my other subtype"]));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromString_ReturnsSelection_HasSkeleton(bool hasSkeleton)
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
            data[DataIndexConstants.CreatureData.BaseAttackQuality] = "1";
            data[DataIndexConstants.CreatureData.Types] = "my type";
            data[DataIndexConstants.CreatureData.HitDiceQuantity] = "9.6";
            data[DataIndexConstants.CreatureData.HitDie] = "783";
            data[DataIndexConstants.CreatureData.HasSkeleton] = hasSkeleton.ToString();

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
            Assert.That(newSelection.BaseAttackQuality, Is.EqualTo(BaseAttackQuality.Average));
            Assert.That(newSelection.Types, Is.EqualTo(["my type"]));
            Assert.That(newSelection.HitDiceQuantity, Is.EqualTo(9.6));
            Assert.That(newSelection.HitDie, Is.EqualTo(783));
            Assert.That(newSelection.HasSkeleton, Is.EqualTo(hasSkeleton));
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
                BaseAttackQuality = BaseAttackQuality.Average,
                Types = ["my type"],
                HitDiceQuantity = 9.6,
                HitDie = 783,
                HasSkeleton = true,
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
            Assert.That(rawData[DataIndexConstants.CreatureData.BaseAttackQuality], Is.EqualTo("1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Types], Is.EqualTo("my type"));
            Assert.That(rawData[DataIndexConstants.CreatureData.HitDiceQuantity], Is.EqualTo("9.6"));
            Assert.That(rawData[DataIndexConstants.CreatureData.HitDie], Is.EqualTo("783"));
            Assert.That(rawData[DataIndexConstants.CreatureData.HasSkeleton], Is.EqualTo(bool.TrueString));
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

        [TestCase(BaseAttackQuality.Poor)]
        [TestCase(BaseAttackQuality.Average)]
        [TestCase(BaseAttackQuality.Good)]
        internal void Map_FromSelection_ReturnsString_BaseAttackQuality(BaseAttackQuality quality)
        {
            Assert.Fail("if NUnit is running this test, I should see this failure.");

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
                BaseAttackQuality = quality,
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
            Assert.That(rawData[DataIndexConstants.CreatureData.BaseAttackQuality], Is.EqualTo(Convert.ToInt32(quality).ToString()));
        }

        [Test]
        public void Map_FromSelection_ReturnsString_WithSubtypes()
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
                BaseAttackQuality = BaseAttackQuality.Average,
                Types = ["my type", "my subtype", "my other subtype"],
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
            Assert.That(rawData[DataIndexConstants.CreatureData.BaseAttackQuality], Is.EqualTo("1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Types], Is.EqualTo("my type|my subtype|my other subtype")
                .And.EqualTo(string.Join(CreatureDataSelection.Delimiter, selection.Types)));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Map_FromSelection_ReturnsString_HasSkeleton(bool hasSkeleton)
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
                BaseAttackQuality = BaseAttackQuality.Average,
                Types = ["my type"],
                HitDiceQuantity = 9.6,
                HitDie = 783,
                HasSkeleton = hasSkeleton,
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
            Assert.That(rawData[DataIndexConstants.CreatureData.BaseAttackQuality], Is.EqualTo("1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Types], Is.EqualTo("my type"));
            Assert.That(rawData[DataIndexConstants.CreatureData.HitDiceQuantity], Is.EqualTo("9.6"));
            Assert.That(rawData[DataIndexConstants.CreatureData.HitDie], Is.EqualTo("783"));
            Assert.That(rawData[DataIndexConstants.CreatureData.HasSkeleton], Is.EqualTo(hasSkeleton.ToString()));
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
            data[DataIndexConstants.CreatureData.BaseAttackQuality] = "1";
            data[DataIndexConstants.CreatureData.Types] = "my type";
            data[DataIndexConstants.CreatureData.HitDiceQuantity] = "9.6";
            data[DataIndexConstants.CreatureData.HitDie] = "783";
            data[DataIndexConstants.CreatureData.HasSkeleton] = bool.TrueString;

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
            Assert.That(newSelection.BaseAttackQuality, Is.EqualTo(BaseAttackQuality.Average));
            Assert.That(newSelection.Types, Is.EqualTo(["my type"]));
            Assert.That(newSelection.HitDiceQuantity, Is.EqualTo(9.6));
            Assert.That(newSelection.HitDie, Is.EqualTo(783));
            Assert.That(newSelection.HasSkeleton, Is.EqualTo(true));
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

        [TestCase(BaseAttackQuality.Poor)]
        [TestCase(BaseAttackQuality.Average)]
        [TestCase(BaseAttackQuality.Good)]
        internal void MapTo_FromString_ReturnsSelection_BaseAttackQuality(BaseAttackQuality quality)
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
            data[DataIndexConstants.CreatureData.BaseAttackQuality] = Convert.ToInt32(quality).ToString();

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
            Assert.That(newSelection.BaseAttackQuality, Is.EqualTo(quality));
        }

        [Test]
        public void MapTo_FromString_ReturnsSelection_WithSubtypes()
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
            data[DataIndexConstants.CreatureData.BaseAttackQuality] = "1";
            data[DataIndexConstants.CreatureData.Types] = "my type|my subtype|my other subtype";

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
            Assert.That(newSelection.BaseAttackQuality, Is.EqualTo(BaseAttackQuality.Average));
            Assert.That(newSelection.Types, Is.EqualTo(["my type", "my subtype", "my other subtype"]));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapTo_FromString_ReturnsSelection_HasSkeleton(bool hasSkeleton)
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
            data[DataIndexConstants.CreatureData.BaseAttackQuality] = "1";
            data[DataIndexConstants.CreatureData.Types] = "my type";
            data[DataIndexConstants.CreatureData.HitDiceQuantity] = "96.";
            data[DataIndexConstants.CreatureData.HitDie] = "783";
            data[DataIndexConstants.CreatureData.HasSkeleton] = hasSkeleton.ToString();

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
            Assert.That(newSelection.BaseAttackQuality, Is.EqualTo(BaseAttackQuality.Average));
            Assert.That(newSelection.Types, Is.EqualTo(["my type"]));
            Assert.That(newSelection.HitDiceQuantity, Is.EqualTo(9.6));
            Assert.That(newSelection.HitDie, Is.EqualTo(783));
            Assert.That(newSelection.HasSkeleton, Is.EqualTo(hasSkeleton));
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
                BaseAttackQuality = BaseAttackQuality.Average,
                Types = ["my type"],
                HitDiceQuantity = 9.6,
                HitDie = 783,
                HasSkeleton = true,
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
            Assert.That(rawData[DataIndexConstants.CreatureData.BaseAttackQuality], Is.EqualTo("1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Types], Is.EqualTo("my type"));
            Assert.That(rawData[DataIndexConstants.CreatureData.HitDiceQuantity], Is.EqualTo("9.6"));
            Assert.That(rawData[DataIndexConstants.CreatureData.HitDie], Is.EqualTo("783"));
            Assert.That(rawData[DataIndexConstants.CreatureData.HasSkeleton], Is.EqualTo(bool.TrueString));
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

        [TestCase(BaseAttackQuality.Poor)]
        [TestCase(BaseAttackQuality.Average)]
        [TestCase(BaseAttackQuality.Good)]
        internal void MapFrom_FromSelection_ReturnsString_BaseAttackQuality(BaseAttackQuality quality)
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
                BaseAttackQuality = quality,
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
            Assert.That(rawData[DataIndexConstants.CreatureData.BaseAttackQuality], Is.EqualTo(Convert.ToInt32(quality).ToString()));
        }

        [Test]
        public void MapFrom_FromSelection_ReturnsString_WithSubtypes()
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
                BaseAttackQuality = BaseAttackQuality.Average,
                Types = ["my type", "my subtype", "my other subtype"],
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
            Assert.That(rawData[DataIndexConstants.CreatureData.BaseAttackQuality], Is.EqualTo("1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Types], Is.EqualTo("my type|my subtype|my other subtype")
                .And.EqualTo(string.Join(CreatureDataSelection.Delimiter, selection.Types)));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void MapFrom_FromSelection_ReturnsString_HasSkeleton(bool hasSkeleton)
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
                BaseAttackQuality = BaseAttackQuality.Average,
                Types = ["my type"],
                HitDiceQuantity = 9.6,
                HitDie = 783,
                HasSkeleton = hasSkeleton,
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
            Assert.That(rawData[DataIndexConstants.CreatureData.BaseAttackQuality], Is.EqualTo("1"));
            Assert.That(rawData[DataIndexConstants.CreatureData.Types], Is.EqualTo("my type"));
            Assert.That(rawData[DataIndexConstants.CreatureData.HitDiceQuantity], Is.EqualTo("9.6"));
            Assert.That(rawData[DataIndexConstants.CreatureData.HitDie], Is.EqualTo("783"));
            Assert.That(rawData[DataIndexConstants.CreatureData.HasSkeleton], Is.EqualTo(hasSkeleton.ToString()));
        }

        [TestCase(.96)]
        [TestCase(1)]
        [TestCase(1.1)]
        [TestCase(2)]
        [TestCase(9.6)]
        [TestCase(96)]
        public void GetEffectiveHitDiceQuantity_ReturnsHitDiceQuantity_Humanoid_NotAsCharacter(double hitDice)
        {
            selection.HitDiceQuantity = hitDice;
            selection.Types = [CreatureConstants.Types.Humanoid];

            var effectiveHitDiceQuantity = selection.GetEffectiveHitDiceQuantity(false);
            Assert.That(effectiveHitDiceQuantity, Is.EqualTo(hitDice));
        }

        [TestCase(.96, 0)]
        [TestCase(1, 0)]
        [TestCase(1.1, 1.1)]
        [TestCase(2, 2)]
        [TestCase(9.6, 9.6)]
        [TestCase(96, 96)]
        public void GetEffectiveHitDiceQuantity_ReturnsZero_Humanoid_AsCharacter(double hitDice, double expected)
        {
            selection.HitDiceQuantity = hitDice;
            selection.Types = [CreatureConstants.Types.Humanoid];

            var effectiveHitDiceQuantity = selection.GetEffectiveHitDiceQuantity(true);
            Assert.That(effectiveHitDiceQuantity, Is.EqualTo(expected));
        }

        [TestCase(.96)]
        [TestCase(1)]
        [TestCase(1.1)]
        [TestCase(2)]
        [TestCase(9.6)]
        [TestCase(96)]
        public void GetEffectiveHitDiceQuantity_ReturnsHitDiceQuantity_NonHumanoid_NotAsCharacter(double hitDice)
        {
            selection.HitDiceQuantity = hitDice;
            selection.Types = [CreatureConstants.Types.MonstrousHumanoid];

            var effectiveHitDiceQuantity = selection.GetEffectiveHitDiceQuantity(false);
            Assert.That(effectiveHitDiceQuantity, Is.EqualTo(hitDice));
        }

        [TestCase(.96)]
        [TestCase(1)]
        [TestCase(1.1)]
        [TestCase(2)]
        [TestCase(9.6)]
        [TestCase(96)]
        public void GetEffectiveHitDiceQuantity_ReturnsHitDiceQuantity_NonHumanoid_AsCharacter(double hitDice)
        {
            selection.HitDiceQuantity = hitDice;
            selection.Types = [CreatureConstants.Types.MonstrousHumanoid];

            var effectiveHitDiceQuantity = selection.GetEffectiveHitDiceQuantity(true);
            Assert.That(effectiveHitDiceQuantity, Is.EqualTo(hitDice));
        }

        [TestCase(.96)]
        [TestCase(1)]
        [TestCase(1.1)]
        [TestCase(2)]
        [TestCase(9.6)]
        [TestCase(96)]
        public void GetEffectiveChallengeRating_ReturnsChallengeRating_Humanoid_NotAsCharacter(double hitDice)
        {
            selection.HitDiceQuantity = hitDice;
            selection.Types = [CreatureConstants.Types.Humanoid];
            selection.ChallengeRating = "my CR";

            var effectiveChallengeRating = selection.GetEffectiveChallengeRating(false);
            Assert.That(effectiveChallengeRating, Is.EqualTo("my CR"));
        }

        [TestCase(.96, ChallengeRatingConstants.CR0)]
        [TestCase(1, ChallengeRatingConstants.CR0)]
        [TestCase(1.1, "my CR")]
        [TestCase(2, "my CR")]
        [TestCase(9.6, "my CR")]
        [TestCase(96, "my CR")]
        public void GetEffectiveChallengeRating_ReturnsZero_Humanoid_AsCharacter(double hitDice, string expected)
        {
            selection.HitDiceQuantity = hitDice;
            selection.Types = [CreatureConstants.Types.Humanoid];
            selection.ChallengeRating = "my CR";

            var effectiveChallengeRating = selection.GetEffectiveChallengeRating(true);
            Assert.That(effectiveChallengeRating, Is.EqualTo(expected));
        }

        [TestCase(.96)]
        [TestCase(1)]
        [TestCase(1.1)]
        [TestCase(2)]
        [TestCase(9.6)]
        [TestCase(96)]
        public void GetEffectiveChallengeRating_ReturnsChallengeRating_NonHumanoid_NotAsCharacter(double hitDice)
        {
            selection.HitDiceQuantity = hitDice;
            selection.Types = [CreatureConstants.Types.MonstrousHumanoid];
            selection.ChallengeRating = "my CR";

            var effectiveChallengeRating = selection.GetEffectiveChallengeRating(false);
            Assert.That(effectiveChallengeRating, Is.EqualTo("my CR"));
        }

        [TestCase(.96)]
        [TestCase(1)]
        [TestCase(1.1)]
        [TestCase(2)]
        [TestCase(9.6)]
        [TestCase(96)]
        public void GetEffectiveChallengeRating_ReturnsChallengeRating_NonHumanoid_AsCharacter(double hitDice)
        {
            selection.HitDiceQuantity = hitDice;
            selection.Types = [CreatureConstants.Types.MonstrousHumanoid];
            selection.ChallengeRating = "my CR";

            var effectiveChallengeRating = selection.GetEffectiveChallengeRating(true);
            Assert.That(effectiveChallengeRating, Is.EqualTo("my CR"));
        }
    }
}
