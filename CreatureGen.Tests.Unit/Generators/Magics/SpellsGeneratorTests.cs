using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Generators.Magics;
using CreatureGen.Domain.Selectors.Collections;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class SpellsGeneratorTests
    {
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IPercentileSelector> mockPercentileSelector;
        private ISpellsGenerator spellsGenerator;
        private CharacterClass characterClass;
        private Dictionary<string, Ability> abilities;
        private List<string> spellcasters;
        private Dictionary<string, int> spellsPerDayForClass;
        private Dictionary<string, int> spellsKnownForClass;
        private List<string> classSpells;
        private Dictionary<string, int> spellLevels;
        private List<string> divineCasters;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockPercentileSelector = new Mock<IPercentileSelector>();
            spellsGenerator = new SpellsGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object, mockPercentileSelector.Object);
            characterClass = new CharacterClass();
            spellcasters = new List<string>();
            abilities = new Dictionary<string, Ability>();
            spellsPerDayForClass = new Dictionary<string, int>();
            spellsKnownForClass = new Dictionary<string, int>();
            classSpells = new List<string>();
            spellLevels = new Dictionary<string, int>();
            divineCasters = new List<string>();

            characterClass.Name = "class name";
            characterClass.Level = 9;
            spellcasters.Add(characterClass.Name);
            spellcasters.Add("other class");
            spellsPerDayForClass["0"] = 90210;
            spellsPerDayForClass["1"] = 42;
            spellsKnownForClass["0"] = 2;
            spellsKnownForClass["1"] = 1;
            abilities["stat"] = new Ability("stat");
            abilities["stat"].BaseValue = 11;
            abilities["other stat"] = new Ability("other stat");
            abilities["other stat"].BaseValue = 11;
            classSpells.Add("spell 1");
            classSpells.Add("spell 2");
            classSpells.Add("spell 3");
            classSpells.Add("spell 4");
            spellLevels[classSpells[0]] = 0;
            spellLevels[classSpells[1]] = 0;
            spellLevels[classSpells[2]] = 1;
            spellLevels[classSpells[3]] = 1;
            divineCasters.Add("other divine class");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.Spellcasters)).Returns(spellcasters);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, SpellConstants.Sources.Divine)).Returns(divineCasters);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.AbilityGroups, characterClass.Name + GroupConstants.Spellcasters)).Returns(new[] { "stat" });

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, characterClass.Level, characterClass.Name);
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(spellsPerDayForClass);

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSKnownSpells, characterClass.Level, characterClass.Name);
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(spellsKnownForClass);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, characterClass.Name)).Returns(classSpells);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, "domain 1")).Returns(new[] { classSpells[0], classSpells[2] });
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, "domain 2")).Returns(new[] { classSpells[1], classSpells[3] });

            tableName = string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, characterClass.Name);
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(spellLevels);

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(index++ % c.Count()));
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<Spell>>())).Returns((IEnumerable<Spell> c) => c.ElementAt(index++ % c.Count()));

        }

        [Test]
        public void DoNotGenerateSpellsPerDayIfNotASpellcaster()
        {
            spellcasters.Remove(characterClass.Name);
            var spellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities);
            Assert.That(spellsPerDay, Is.Empty);
        }

        [Test]
        public void GenerateSpellsPerDay()
        {
            var spellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities);

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(42));

            Assert.That(spellsPerDay.Select(s => s.Source), Is.All.EqualTo(characterClass.Name));
            Assert.That(spellsPerDay.Count, Is.EqualTo(2));
        }

        [Test]
        public void IfLevelAbove20_UseLevel20SpellsPerDay()
        {
            characterClass.Level = 9266;
            abilities["stat"].BaseValue = 16;

            var level20SpellsPerDay = new Dictionary<string, int>();
            level20SpellsPerDay["0"] = 90210;
            level20SpellsPerDay["1"] = 42;
            level20SpellsPerDay["2"] = 600;
            level20SpellsPerDay["3"] = 1337;

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, 20, characterClass.Name);
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(level20SpellsPerDay);

            var spellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities).ToArray();
            Assert.That(spellsPerDay[0].Level, Is.EqualTo(0));
            Assert.That(spellsPerDay[0].Quantity, Is.EqualTo(90210));
            Assert.That(spellsPerDay[1].Level, Is.EqualTo(1));
            Assert.That(spellsPerDay[1].Quantity, Is.EqualTo(42 + 1));
            Assert.That(spellsPerDay[2].Level, Is.EqualTo(2));
            Assert.That(spellsPerDay[2].Quantity, Is.EqualTo(600 + 1));
            Assert.That(spellsPerDay[3].Level, Is.EqualTo(3));
            Assert.That(spellsPerDay[3].Quantity, Is.EqualTo(1337 + 1));
            Assert.That(spellsPerDay.Length, Is.EqualTo(4));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10, 0)]
        [TestCase(11, 0, 0)]
        [TestCase(12, 0, 1, 0)]
        [TestCase(13, 0, 1, 0, 0)]
        [TestCase(14, 0, 1, 1, 0, 0)]
        [TestCase(15, 0, 1, 1, 0, 0, 0)]
        [TestCase(16, 0, 1, 1, 1, 0, 0, 0)]
        [TestCase(17, 0, 1, 1, 1, 0, 0, 0, 0)]
        [TestCase(18, 0, 1, 1, 1, 1, 0, 0, 0, 0)]
        [TestCase(19, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0)]
        [TestCase(20, 0, 2, 1, 1, 1, 1, 0, 0, 0, 0)]
        [TestCase(21, 0, 2, 1, 1, 1, 1, 0, 0, 0, 0)]
        [TestCase(22, 0, 2, 2, 1, 1, 1, 1, 0, 0, 0)]
        [TestCase(23, 0, 2, 2, 1, 1, 1, 1, 0, 0, 0)]
        [TestCase(24, 0, 2, 2, 2, 1, 1, 1, 1, 0, 0)]
        [TestCase(25, 0, 2, 2, 2, 1, 1, 1, 1, 0, 0)]
        [TestCase(26, 0, 2, 2, 2, 2, 1, 1, 1, 1, 0)]
        [TestCase(27, 0, 2, 2, 2, 2, 1, 1, 1, 1, 0)]
        [TestCase(28, 0, 3, 2, 2, 2, 2, 1, 1, 1, 1)]
        [TestCase(29, 0, 3, 2, 2, 2, 2, 1, 1, 1, 1)]
        [TestCase(30, 0, 3, 3, 2, 2, 2, 2, 1, 1, 1)]
        [TestCase(31, 0, 3, 3, 2, 2, 2, 2, 1, 1, 1)]
        [TestCase(32, 0, 3, 3, 3, 2, 2, 2, 2, 1, 1)]
        [TestCase(33, 0, 3, 3, 3, 2, 2, 2, 2, 1, 1)]
        [TestCase(34, 0, 3, 3, 3, 3, 2, 2, 2, 2, 1)]
        [TestCase(35, 0, 3, 3, 3, 3, 2, 2, 2, 2, 1)]
        [TestCase(36, 0, 4, 3, 3, 3, 3, 2, 2, 2, 2)]
        [TestCase(37, 0, 4, 3, 3, 3, 3, 2, 2, 2, 2)]
        [TestCase(38, 0, 4, 4, 3, 3, 3, 3, 2, 2, 2)]
        [TestCase(39, 0, 4, 4, 3, 3, 3, 3, 2, 2, 2)]
        [TestCase(40, 0, 4, 4, 4, 3, 3, 3, 3, 2, 2)]
        [TestCase(41, 0, 4, 4, 4, 3, 3, 3, 3, 2, 2)]
        [TestCase(42, 0, 4, 4, 4, 4, 3, 3, 3, 3, 2)]
        [TestCase(43, 0, 4, 4, 4, 4, 3, 3, 3, 3, 2)]
        [TestCase(44, 0, 5, 4, 4, 4, 4, 3, 3, 3, 3)]
        [TestCase(45, 0, 5, 4, 4, 4, 4, 3, 3, 3, 3)]
        public void AddBonusSpellsPerDayForStat(int statValue, params int[] levelBonuses)
        {
            abilities["stat"].BaseValue = statValue;
            spellsPerDayForClass["0"] = 10;
            spellsPerDayForClass["1"] = 9;
            spellsPerDayForClass["2"] = 8;
            spellsPerDayForClass["3"] = 7;
            spellsPerDayForClass["4"] = 6;
            spellsPerDayForClass["5"] = 5;
            spellsPerDayForClass["6"] = 4;
            spellsPerDayForClass["7"] = 3;
            spellsPerDayForClass["8"] = 2;
            spellsPerDayForClass["9"] = 1;

            var generatedSpellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities);

            for (var spellLevel = 0; spellLevel < levelBonuses.Length; spellLevel++)
            {
                var expectedQuantity = (10 - spellLevel) + levelBonuses[spellLevel];
                var spells = generatedSpellsPerDay.First(s => s.Level == spellLevel);
                Assert.That(spells.Quantity, Is.EqualTo(expectedQuantity), spellLevel.ToString());
            }

            Assert.That(generatedSpellsPerDay.Count, Is.EqualTo(levelBonuses.Length));
        }

        [Test]
        public void CannotGetSpellsPerDayInLevelThatAbilitiesDoNotAllow()
        {
            abilities["stat"].BaseValue = 10;

            var spellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities);
            Assert.That(spellsPerDay.Count, Is.EqualTo(1));

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));
        }

        [Test]
        public void CannotGetBonusSpellsPerDayInLevelThatCharacterCannotCast()
        {
            abilities["stat"].BaseValue = 45;

            var spellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities);
            Assert.That(spellsPerDay.Count, Is.EqualTo(2));

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(47));
        }

        [Test]
        public void CanGetBonusSpellsPerDayInLevelWithQuantityOf0()
        {
            abilities["stat"].BaseValue = 45;
            spellsPerDayForClass["2"] = 0;

            var spellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities);
            Assert.That(spellsPerDay.Count, Is.EqualTo(3));

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(47));

            var secondLevelSpells = spellsPerDay.First(s => s.Level == 2);
            Assert.That(secondLevelSpells.Quantity, Is.EqualTo(4));
        }

        [Test]
        public void IfTotalSpellsPerDayIs0AndDoesNotHaveDomainSpell_RemoveSpellLevel()
        {
            spellsPerDayForClass["1"] = 0;

            var spellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities);
            var cantrips = spellsPerDay.First(s => s.Level == 0);

            Assert.That(cantrips.Quantity, Is.EqualTo(90210));
            Assert.That(spellsPerDay.Count, Is.EqualTo(1));
        }

        [Test]
        public void IfTotalSpellsPerDayIs0AndHasDomainSpell_DoNotRemoveSpellLevel()
        {
            spellsPerDayForClass["1"] = 0;
            characterClass.SpecialistFields = new[] { "specialist" };

            var spellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities);
            Assert.That(spellsPerDay.Count, Is.EqualTo(2));

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));
            Assert.That(cantrips.HasDomainSpell, Is.False);

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(0));
            Assert.That(firstLevelSpells.HasDomainSpell, Is.True);
        }

        [Test]
        public void IfTotalSpellsPerDayIsGreaterThan0AndDoesNotHaveDomainSpell_DoNotRemoveSpellLevel()
        {
            spellsPerDayForClass["1"] = 1;

            var spellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities);
            Assert.That(spellsPerDay.Count, Is.EqualTo(2));

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));
            Assert.That(cantrips.HasDomainSpell, Is.False);

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(1));
            Assert.That(firstLevelSpells.HasDomainSpell, Is.False);
        }

        [Test]
        public void AllSpellLevelsPerDayExcept0GetDomainSpellIfClassHasSpecialistFields()
        {
            characterClass.SpecialistFields = new[] { "specialist" };
            spellsPerDayForClass["0"] = 10;
            spellsPerDayForClass["1"] = 9;
            spellsPerDayForClass["2"] = 8;
            spellsPerDayForClass["3"] = 7;
            spellsPerDayForClass["4"] = 6;
            spellsPerDayForClass["5"] = 5;
            spellsPerDayForClass["6"] = 4;
            spellsPerDayForClass["7"] = 3;
            spellsPerDayForClass["8"] = 2;
            spellsPerDayForClass["9"] = 1;

            var spellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities);

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.HasDomainSpell, Is.False);

            var nonCantrips = spellsPerDay.Except(new[] { cantrips });
            foreach (var nonCantrip in nonCantrips)
                Assert.That(nonCantrip.HasDomainSpell, Is.True);
        }

        [Test]
        public void AllSpellLevelsPerDayExcept0GetDomainSpellIfClassHasMultipleSpecialistFields()
        {
            characterClass.SpecialistFields = new[] { "specialist", "also specialist" };
            spellsPerDayForClass["0"] = 10;
            spellsPerDayForClass["1"] = 9;
            spellsPerDayForClass["2"] = 8;
            spellsPerDayForClass["3"] = 7;
            spellsPerDayForClass["4"] = 6;
            spellsPerDayForClass["5"] = 5;
            spellsPerDayForClass["6"] = 4;
            spellsPerDayForClass["7"] = 3;
            spellsPerDayForClass["8"] = 2;
            spellsPerDayForClass["9"] = 1;

            var spellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities);

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.HasDomainSpell, Is.False);

            var nonCantrips = spellsPerDay.Except(new[] { cantrips });
            foreach (var nonCantrip in nonCantrips)
                Assert.That(nonCantrip.HasDomainSpell, Is.True, nonCantrip.Level.ToString());
        }

        [Test]
        public void NoSpellLevelsPerDayGetDomainSpellIfClassDoesNotHaveSpecialistFields()
        {
            spellsPerDayForClass["0"] = 10;
            spellsPerDayForClass["1"] = 9;
            spellsPerDayForClass["2"] = 8;
            spellsPerDayForClass["3"] = 7;
            spellsPerDayForClass["4"] = 6;
            spellsPerDayForClass["5"] = 5;
            spellsPerDayForClass["6"] = 4;
            spellsPerDayForClass["7"] = 3;
            spellsPerDayForClass["8"] = 2;
            spellsPerDayForClass["9"] = 1;

            var spellsPerDay = spellsGenerator.GeneratePerDay(characterClass, abilities);

            foreach (var spells in spellsPerDay)
                Assert.That(spells.HasDomainSpell, Is.False, spells.Level.ToString());
        }

        [Test]
        public void DoNotGenerateKnownSpellsIfNotASpellcaster()
        {
            spellcasters.Remove(characterClass.Name);
            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown, Is.Empty);
        }

        [Test]
        public void GenerateKnownSpells()
        {
            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(3));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));

            Assert.That(spellsKnown.Select(s => s.Source), Is.All.EqualTo(characterClass.Name));
        }

        [Test]
        public void IfLevelAbove20_UseLevel20KnownSpells()
        {
            characterClass.Level = 9266;
            abilities["stat"].BaseValue = 16;

            var level20KnownSpells = new Dictionary<string, int>();
            level20KnownSpells["0"] = 90;
            level20KnownSpells["1"] = 210;
            level20KnownSpells["2"] = 42;
            level20KnownSpells["3"] = 600;
            level20KnownSpells["4"] = 13;
            level20KnownSpells["5"] = 37;

            foreach (var kvp in level20KnownSpells)
            {
                var spellLevel = Convert.ToInt32(kvp.Key);
                var quantity = kvp.Value;

                while (quantity-- > 0)
                {
                    var spell = Guid.NewGuid().ToString();
                    classSpells.Add(spell);
                    spellLevels[spell] = spellLevel;
                }
            }

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSKnownSpells, 20, characterClass.Name);
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(level20KnownSpells);

            var knownSpells = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(knownSpells.Count(s => s.Level == 0), Is.EqualTo(90));
            Assert.That(knownSpells.Count(s => s.Level == 1), Is.EqualTo(210));
            Assert.That(knownSpells.Count(s => s.Level == 2), Is.EqualTo(42));
            Assert.That(knownSpells.Count(s => s.Level == 3), Is.EqualTo(600));
            Assert.That(knownSpells.Count(s => s.Level == 4), Is.EqualTo(13));
            Assert.That(knownSpells.Count(s => s.Level == 5), Is.EqualTo(37));
            Assert.That(knownSpells.Count, Is.EqualTo(90 + 210 + 42 + 600 + 13 + 37));
        }

        [Test]
        public void KnowAllSpellsBySheerQuantity()
        {
            spellsKnownForClass["0"] = 1;
            spellsKnownForClass["1"] = 3;

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(3));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(1));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersKnowAllSpells()
        {
            divineCasters.Add(characterClass.Name);

            spellsKnownForClass["0"] = 1;
            spellsKnownForClass["1"] = 1;

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersKnowAllSpecialistSpells()
        {
            divineCasters.Add(characterClass.Name);

            characterClass.SpecialistFields = new[] { "special domain" };

            spellsKnownForClass["0"] = 1;
            spellsKnownForClass["1"] = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, "special domain")).Returns(new[] { "spell 5", "other special domain spell" });

            var specialDomainSpellLevels = new Dictionary<string, int>();
            specialDomainSpellLevels["spell 5"] = 1;
            specialDomainSpellLevels["other special domain spell"] = 2;

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, "special domain");
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(5));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(3));
            Assert.That(firstLevelSpells.Select(s => s.Name), Contains.Item("spell 5"));
        }

        [Test]
        public void DivineCastersDoNotKnowProhibitedSpells()
        {
            divineCasters.Add(characterClass.Name);

            characterClass.ProhibitedFields = new[] { "prohibited field" };

            classSpells.Add("spell 5");
            spellLevels[classSpells[4]] = 0;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, "prohibited field")).Returns(new[] { "other forbidden spell", classSpells[4] });

            spellsKnownForClass["0"] = 1;
            spellsKnownForClass["1"] = 1;

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));
            Assert.That(spellsKnown.Select(s => s.Name), Has.None.EqualTo("spell 5"));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersKnowSpellsEvenIfQuantityForLevelIs0()
        {
            divineCasters.Add(characterClass.Name);

            spellsKnownForClass["0"] = 1;
            spellsKnownForClass["1"] = 0;

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersDoNotKnowSpellsBeyondLevel()
        {
            divineCasters.Add(characterClass.Name);

            spellsKnownForClass["0"] = 1;
            spellsKnownForClass["1"] = 1;

            classSpells.Add("spell 5");
            spellLevels[classSpells[4]] = 2;

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersDoNotKnowSpecialistSpellsBeyondLevel()
        {
            divineCasters.Add(characterClass.Name);

            characterClass.ProhibitedFields = new[] { "special domain" };

            spellsKnownForClass["0"] = 1;
            spellsKnownForClass["1"] = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, "special domain")).Returns(new[] { "spell 5", "other special domain spell" });

            var specialDomainSpellLevels = new Dictionary<string, int>();
            specialDomainSpellLevels["spell 5"] = 2;
            specialDomainSpellLevels["other special domain spell"] = 2;

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, "special domain");
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersKnowSpecialistSpellsEvenIfQuantityForLevelIs0()
        {
            divineCasters.Add(characterClass.Name);

            characterClass.SpecialistFields = new[] { "special domain" };

            spellsKnownForClass["0"] = 1;
            spellsKnownForClass["1"] = 0;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, "special domain")).Returns(new[] { "spell 5", "other special domain spell" });

            var specialDomainSpellLevels = new Dictionary<string, int>();
            specialDomainSpellLevels["spell 5"] = 1;
            specialDomainSpellLevels["other special domain spell"] = 2;

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, "special domain");
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(5));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DivineCastersDoNotKnowSpellsBeyondWhatAbilitiesAllow()
        {
            abilities["stat"].BaseValue = 10;
            divineCasters.Add(characterClass.Name);

            spellsKnownForClass["0"] = 1;
            spellsKnownForClass["1"] = 1;

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(2));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersDoNotKnowSpecialistSpellsBeyondWhatAbilitiesAllow()
        {
            abilities["stat"].BaseValue = 10;
            divineCasters.Add(characterClass.Name);

            characterClass.SpecialistFields = new[] { "special domain" };

            spellsKnownForClass["0"] = 1;
            spellsKnownForClass["1"] = 0;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, "special domain")).Returns(new[] { "spell 5", "other special domain spell" });

            var specialDomainSpellLevels = new Dictionary<string, int>();
            specialDomainSpellLevels["spell 5"] = 1;
            specialDomainSpellLevels["other special domain spell"] = 2;

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, "special domain");
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(2));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CanHaveMoreThanNormalKnownSpellQuantity()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSKnowsAdditionalSpells, characterClass.Name);
            mockPercentileSelector.SetupSequence(s => s.SelectFrom<bool>(tableName))
                .Returns(true).Returns(false).Returns(true).Returns(false);

            spellsKnownForClass["0"] = 1;
            spellsKnownForClass["1"] = 1;

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CanHaveManyMoreThanNormalKnownSpellQuantity()
        {
            var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSKnowsAdditionalSpells, characterClass.Name);
            mockPercentileSelector.SetupSequence(s => s.SelectFrom<bool>(tableName))
                .Returns(true).Returns(true).Returns(false)
                .Returns(true).Returns(true).Returns(false);

            spellsKnownForClass["0"] = 0;
            spellsKnownForClass["1"] = 0;

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetRandomKnownSpecialistSpells()
        {
            characterClass.SpecialistFields = new[] { "special domain" };

            classSpells.Add("spell 5");
            spellLevels[classSpells[4]] = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, "special domain")).Returns(new[] { "other special domain spell", classSpells[4] });

            var specialDomainSpellLevels = new Dictionary<string, int>();
            specialDomainSpellLevels["spell 5"] = 1;
            specialDomainSpellLevels["other special domain spell"] = 2;

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, "special domain");
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetRandomKnownNonSpecialistSpellIfAllSpecialistSpellsAreKnown()
        {
            characterClass.SpecialistFields = new[] { "special domain" };

            classSpells.Add("spell 5");
            spellLevels[classSpells[4]] = 1;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, "special domain")).Returns(new[] { classSpells[3] });

            var specialDomainSpellLevels = new Dictionary<string, int>();
            specialDomainSpellLevels[classSpells[3]] = 1;

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, "special domain");
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void RandomKnownSpellsAreNotProhibitedSpells()
        {
            characterClass.ProhibitedFields = new[] { "domain 1" };

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(2));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(1));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));
        }

        [Test]
        public void RandomKnownSpellsAreSpecialistSpellsEvenIfQuantityForLevelIs0()
        {
            characterClass.SpecialistFields = new[] { "special domain" };

            spellsKnownForClass["1"] = 0;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, "special domain")).Returns(new[] { "spell 5", "other special domain spell" });

            var specialDomainSpellLevels = new Dictionary<string, int>();
            specialDomainSpellLevels["spell 5"] = 1;
            specialDomainSpellLevels["other special domain spell"] = 2;

            var tableName = string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, "special domain");
            mockAdjustmentsSelector.Setup(s => s.SelectAllFrom(tableName)).Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(3));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));
        }

        [Test]
        public void RandomKnownSpellsAreNotBeyondWhatAbilitiesAllow()
        {
            abilities["stat"].BaseValue = 10;

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(2));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));
        }

        [Test]
        public void RandomKnownSpellsAreNotSpecialistSpellsBeyondWhatAbilitiesAllow()
        {
            abilities["stat"].BaseValue = 10;

            characterClass.SpecialistFields = new[] { "domain 2" };

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(2));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CannotHaveDuplicateKnownSpells()
        {
            mockCollectionsSelector.SetupSequence(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns(classSpells[0]).Returns(classSpells[0])
                .Returns(classSpells[1])
                .Returns(classSpells[2])
                .Returns(classSpells[3]);

            var spellsKnown = spellsGenerator.GenerateKnown(characterClass, abilities);
            Assert.That(spellsKnown.Count(), Is.EqualTo(3));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));
            Assert.That(cantrips.Select(c => c.Name), Is.Unique);

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));
        }

        [Test]
        public void DoNotGeneratePreparedSpellsIfNotASpellcaster()
        {
            spellcasters.Remove(characterClass.Name);

            var knownSpells = new List<Spell>();
            knownSpells.Add(new Spell { Name = classSpells[0], Level = 0 });
            knownSpells.Add(new Spell { Name = classSpells[1], Level = 0 });
            knownSpells.Add(new Spell { Name = "other cantrip", Level = 0 });
            knownSpells.Add(new Spell { Name = classSpells[2], Level = 1 });
            knownSpells.Add(new Spell { Name = "other spell", Level = 1 });

            var spellsPerDay = new List<SpellQuantity>();
            spellsPerDay.Add(new SpellQuantity { Level = 0, Quantity = 2 });
            spellsPerDay.Add(new SpellQuantity { Level = 1, Quantity = 1 });

            var preparedSpells = spellsGenerator.GeneratePrepared(characterClass, knownSpells, spellsPerDay);
            Assert.That(preparedSpells, Is.Empty);
        }

        [Test]
        public void GetRandomPreparedSpells()
        {
            var knownSpells = new List<Spell>();
            knownSpells.Add(new Spell { Name = classSpells[0], Level = 0, Source = "source" });
            knownSpells.Add(new Spell { Name = classSpells[1], Level = 0, Source = "source" });
            knownSpells.Add(new Spell { Name = "other cantrip", Level = 0, Source = "source" });
            knownSpells.Add(new Spell { Name = classSpells[2], Level = 1, Source = "source" });
            knownSpells.Add(new Spell { Name = "other spell", Level = 1, Source = "source" });

            var spellsPerDay = new List<SpellQuantity>();
            spellsPerDay.Add(new SpellQuantity { Level = 0, Quantity = 2, Source = "source" });
            spellsPerDay.Add(new SpellQuantity { Level = 1, Quantity = 1, Source = "source" });

            var spellsPrepared = spellsGenerator.GeneratePrepared(characterClass, knownSpells, spellsPerDay);
            Assert.That(spellsPrepared.Count(), Is.EqualTo(3));

            var cantrips = spellsPrepared.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsPrepared.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));

            Assert.That(spellsPrepared.Select(s => s.Source), Is.All.EqualTo("source"));
        }

        [Test]
        public void GetRandomPreparedSpecialistSpells()
        {
            characterClass.SpecialistFields = new[] { "domain 2" };

            var knownSpells = new List<Spell>();
            knownSpells.Add(new Spell { Name = classSpells[0], Level = 0 });
            knownSpells.Add(new Spell { Name = classSpells[1], Level = 0 });
            knownSpells.Add(new Spell { Name = "other cantrip", Level = 0 });
            knownSpells.Add(new Spell { Name = classSpells[2], Level = 1 });
            knownSpells.Add(new Spell { Name = "other spell", Level = 1 });

            var spellsPerDay = new List<SpellQuantity>();
            spellsPerDay.Add(new SpellQuantity { Level = 0, Quantity = 2 });
            spellsPerDay.Add(new SpellQuantity { Level = 1, Quantity = 1, HasDomainSpell = true });

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.SpellGroups, "domain 2")).Returns(new[] { classSpells[1], classSpells[3], "other spell" });

            var spellsPrepared = spellsGenerator.GeneratePrepared(characterClass, knownSpells, spellsPerDay);
            Assert.That(spellsPrepared.Count(), Is.EqualTo(4));

            var cantrips = spellsPrepared.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsPrepared.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void CanHaveDuplicatePreparedSpells()
        {
            var knownSpells = new List<Spell>();
            knownSpells.Add(new Spell { Name = classSpells[0], Level = 0 });
            knownSpells.Add(new Spell { Name = classSpells[1], Level = 0 });
            knownSpells.Add(new Spell { Name = "other cantrip", Level = 0 });
            knownSpells.Add(new Spell { Name = classSpells[2], Level = 1 });
            knownSpells.Add(new Spell { Name = "other spell", Level = 1 });

            var spellsPerDay = new List<SpellQuantity>();
            spellsPerDay.Add(new SpellQuantity { Level = 0, Quantity = 2 });
            spellsPerDay.Add(new SpellQuantity { Level = 1, Quantity = 1 });

            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<Spell>>())).Returns((IEnumerable<Spell> c) => c.First());

            var spellsPrepared = spellsGenerator.GeneratePrepared(characterClass, knownSpells, spellsPerDay);
            Assert.That(spellsPrepared.Count(), Is.EqualTo(3));

            var cantrips = spellsPrepared.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));
            Assert.That(cantrips.Select(c => c.Name), Is.All.EqualTo(classSpells[0]));

            var firstLevelSpells = spellsPrepared.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));
        }
    }
}
