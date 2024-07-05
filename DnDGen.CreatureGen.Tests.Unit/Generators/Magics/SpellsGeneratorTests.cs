using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class SpellsGeneratorTests
    {
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private ISpellsGenerator spellsGenerator;
        private Ability castingAbility;
        private List<TypeAndAmountSelection> spellsPerDayForClass;
        private List<TypeAndAmountSelection> spellsKnownForClass;
        private List<string> classSpells;
        private List<TypeAndAmountSelection> spellLevels;
        private List<string> divineCasters;
        private string caster;
        private int casterLevel;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            spellsGenerator = new SpellsGenerator(mockCollectionsSelector.Object, mockTypeAndAmountSelector.Object);
            castingAbility = new Ability("casting ability");
            spellsPerDayForClass = new List<TypeAndAmountSelection>();
            spellsKnownForClass = new List<TypeAndAmountSelection>();
            classSpells = new List<string>();
            spellLevels = new List<TypeAndAmountSelection>();
            divineCasters = new List<string>();
            alignment = new Alignment();

            caster = "class name";
            casterLevel = 9;
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 90210, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 42, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 2, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 1, });
            castingAbility.BaseScore = 11;
            classSpells.Add("spell 1");
            classSpells.Add("spell 2");
            classSpells.Add("spell 3");
            classSpells.Add("spell 4");
            spellLevels.Add(new TypeAndAmountSelection { Type = classSpells[0], Amount = 0, });
            spellLevels.Add(new TypeAndAmountSelection { Type = classSpells[1], Amount = 0, });
            spellLevels.Add(new TypeAndAmountSelection { Type = classSpells[2], Amount = 1, });
            spellLevels.Add(new TypeAndAmountSelection { Type = classSpells[3], Amount = 1, });
            divineCasters.Add("other divine class");
            alignment.Goodness = "goodly";
            alignment.Lawfulness = "lawly";

            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Divine)).Returns(divineCasters);

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellsPerDay, $"{caster}:{casterLevel}"))
                .Returns(spellsPerDayForClass);

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.KnownSpells, $"{caster}:{casterLevel}"))
                .Returns(spellsKnownForClass);

            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, caster)).Returns(classSpells);
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "domain 1")).Returns(new[] { classSpells[0], classSpells[2] });
            mockCollectionsSelector.Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "domain 2")).Returns(new[] { classSpells[1], classSpells[3] });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellLevels, caster))
                .Returns(spellLevels);

            var index = 0;
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.ElementAt(index++ % c.Count()));
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<Spell>>())).Returns((IEnumerable<Spell> c) => c.ElementAt(index++ % c.Count()));

        }

        [Test]
        public void GenerateSpellsPerDay()
        {
            var spellsPerDay = spellsGenerator.GeneratePerDay(caster, casterLevel, castingAbility);

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(42));

            Assert.That(spellsPerDay.Select(s => s.Source), Is.All.EqualTo(caster));
            Assert.That(spellsPerDay.Count, Is.EqualTo(2));
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
            castingAbility.BaseScore = statValue + 2;
            castingAbility.RacialAdjustment = -2;

            spellsPerDayForClass.Clear();
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 10, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 9, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "2", Amount = 8, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "3", Amount = 7, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "4", Amount = 6, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "5", Amount = 5, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "6", Amount = 4, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "7", Amount = 3, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "8", Amount = 2, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "9", Amount = 1, });

            var generatedSpellsPerDay = spellsGenerator.GeneratePerDay(caster, casterLevel, castingAbility);

            for (var spellLevel = 0; spellLevel < levelBonuses.Length; spellLevel++)
            {
                var expectedQuantity = 10 - spellLevel + levelBonuses[spellLevel];
                var spells = generatedSpellsPerDay.First(s => s.Level == spellLevel);
                Assert.That(spells.Quantity, Is.EqualTo(10 - spellLevel), spellLevel.ToString());
                Assert.That(spells.BonusSpells, Is.EqualTo(levelBonuses[spellLevel]), spellLevel.ToString());
                Assert.That(spells.TotalQuantity, Is.EqualTo(expectedQuantity), spellLevel.ToString());
            }

            Assert.That(generatedSpellsPerDay.Count, Is.EqualTo(levelBonuses.Length));
        }

        [Test]
        public void CannotGetSpellsPerDayInLevelThatAbilitiesDoNotAllow()
        {
            castingAbility.BaseScore = 10;

            var spellsPerDay = spellsGenerator.GeneratePerDay(caster, casterLevel, castingAbility);
            Assert.That(spellsPerDay.Count, Is.EqualTo(1));

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));
        }

        [Test]
        public void CannotGetBonusSpellsPerDayInLevelThatCharacterCannotCast()
        {
            castingAbility.BaseScore = 45;

            var spellsPerDay = spellsGenerator.GeneratePerDay(caster, casterLevel, castingAbility);
            Assert.That(spellsPerDay.Count, Is.EqualTo(2));

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(42));
            Assert.That(firstLevelSpells.BonusSpells, Is.EqualTo(5));
            Assert.That(firstLevelSpells.TotalQuantity, Is.EqualTo(47));
        }

        [Test]
        public void CanGetBonusSpellsPerDayInLevelWithQuantityOf0()
        {
            castingAbility.BaseScore = 45;
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "2", Amount = 0, });

            var spellsPerDay = spellsGenerator.GeneratePerDay(caster, casterLevel, castingAbility);
            Assert.That(spellsPerDay.Count, Is.EqualTo(3));

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.Quantity, Is.EqualTo(90210));

            var firstLevelSpells = spellsPerDay.First(s => s.Level == 1);
            Assert.That(firstLevelSpells.Quantity, Is.EqualTo(42));
            Assert.That(firstLevelSpells.BonusSpells, Is.EqualTo(5));
            Assert.That(firstLevelSpells.TotalQuantity, Is.EqualTo(47));

            var secondLevelSpells = spellsPerDay.First(s => s.Level == 2);
            Assert.That(secondLevelSpells.Quantity, Is.EqualTo(0));
            Assert.That(secondLevelSpells.BonusSpells, Is.EqualTo(4));
            Assert.That(secondLevelSpells.TotalQuantity, Is.EqualTo(4));
        }

        [Test]
        public void IfTotalSpellsPerDayIs0AndDoesNotHaveDomainSpell_RemoveSpellLevel()
        {
            spellsPerDayForClass.Clear();
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 90210, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 0, });

            var spellsPerDay = spellsGenerator.GeneratePerDay(caster, casterLevel, castingAbility);
            var cantrips = spellsPerDay.First(s => s.Level == 0);

            Assert.That(cantrips.Quantity, Is.EqualTo(90210));
            Assert.That(spellsPerDay.Count, Is.EqualTo(1));
        }

        [Test]
        public void IfTotalSpellsPerDayIs0AndHasDomainSpell_DoNotRemoveSpellLevel()
        {
            spellsPerDayForClass.Clear();
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 90210, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 0, });

            var spellsPerDay = spellsGenerator.GeneratePerDay(caster, casterLevel, castingAbility, "specialist");
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
            spellsPerDayForClass.Clear();
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 90210, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 1, });

            var spellsPerDay = spellsGenerator.GeneratePerDay(caster, casterLevel, castingAbility);
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
            spellsPerDayForClass.Clear();
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 10, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 9, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "2", Amount = 8, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "3", Amount = 7, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "4", Amount = 6, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "5", Amount = 5, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "6", Amount = 4, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "7", Amount = 3, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "8", Amount = 2, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "9", Amount = 1, });

            var spellsPerDay = spellsGenerator.GeneratePerDay(caster, casterLevel, castingAbility, "specialist");

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.HasDomainSpell, Is.False);

            var nonCantrips = spellsPerDay.Except(new[] { cantrips });
            foreach (var nonCantrip in nonCantrips)
                Assert.That(nonCantrip.HasDomainSpell, Is.True);
        }

        [Test]
        public void AllSpellLevelsPerDayExcept0GetDomainSpellIfClassHasMultipleSpecialistFields()
        {
            spellsPerDayForClass.Clear();
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 10, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 9, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "2", Amount = 8, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "3", Amount = 7, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "4", Amount = 6, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "5", Amount = 5, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "6", Amount = 4, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "7", Amount = 3, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "8", Amount = 2, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "9", Amount = 1, });

            var spellsPerDay = spellsGenerator.GeneratePerDay(caster, casterLevel, castingAbility, "specialist", "also specialist");

            var cantrips = spellsPerDay.First(s => s.Level == 0);
            Assert.That(cantrips.HasDomainSpell, Is.False);

            var nonCantrips = spellsPerDay.Except(new[] { cantrips });
            foreach (var nonCantrip in nonCantrips)
                Assert.That(nonCantrip.HasDomainSpell, Is.True, nonCantrip.Level.ToString());
        }

        [Test]
        public void NoSpellLevelsPerDayGetDomainSpellIfClassDoesNotHaveSpecialistFields()
        {
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 10, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 9, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "2", Amount = 8, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "3", Amount = 7, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "4", Amount = 6, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "5", Amount = 5, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "6", Amount = 4, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "7", Amount = 3, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "8", Amount = 2, });
            spellsPerDayForClass.Add(new TypeAndAmountSelection { Type = "9", Amount = 1, });

            var spellsPerDay = spellsGenerator.GeneratePerDay(caster, casterLevel, castingAbility);

            foreach (var spells in spellsPerDay)
                Assert.That(spells.HasDomainSpell, Is.False, spells.Level.ToString());
        }

        [Test]
        public void GenerateKnownSpells()
        {
            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility);
            Assert.That(spellsKnown.Count(), Is.EqualTo(3));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));

            Assert.That(spellsKnown.Select(s => s.Source), Is.All.EqualTo(caster));
        }

        [Test]
        public void KnowAllSpellsBySheerQuantity()
        {
            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 3, });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility);
            Assert.That(spellsKnown.Count(), Is.EqualTo(3));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(1));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersKnowAllSpells()
        {
            divineCasters.Add(caster);

            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 1, });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility);
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersKnowAllSpecialistSpells()
        {
            divineCasters.Add(caster);

            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 1, });

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "special domain"))
                .Returns(new[] { "spell 5", "other special domain spell" });

            var specialDomainSpellLevels = new List<TypeAndAmountSelection>();
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "spell 5", Amount = 1, });
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "other special domain spell", Amount = 2, });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellLevels, "special domain"))
                .Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "special domain");
            Assert.That(spellsKnown.Count(), Is.EqualTo(5));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(3));
            Assert.That(firstLevelSpells.Select(s => s.Name), Contains.Item("spell 5"));
        }

        [Test]
        public void DivineCastersDoNotKnowProhibitedSpells_FromCreature()
        {
            divineCasters.Add(caster);

            classSpells.Add("spell 5");
            spellLevels.Add(new TypeAndAmountSelection { Type = classSpells[4], Amount = 0, });

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "creature:Prohibited"))
                .Returns(new[] { "other forbidden spell", classSpells[4] });

            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 1, });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "specialist field");
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));
            Assert.That(spellsKnown.Select(s => s.Name), Has.None.EqualTo("spell 5"));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersDoNotKnowProhibitedSpells_FromDomain()
        {
            divineCasters.Add(caster);

            classSpells.Add("spell 5");
            spellLevels.Add(new TypeAndAmountSelection { Type = classSpells[4], Amount = 0, });

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "specialist field:Prohibited"))
                .Returns(new[] { "other forbidden spell", classSpells[4] });

            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 1, });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "specialist field");
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));
            Assert.That(spellsKnown.Select(s => s.Name), Has.None.EqualTo("spell 5"));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersDoNotKnowProhibitedSpells_FromAlignmentGoodness()
        {
            divineCasters.Add(caster);

            classSpells.Add("spell 5");
            spellLevels.Add(new TypeAndAmountSelection { Type = classSpells[4], Amount = 0, });

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "goodly:Prohibited"))
                .Returns(new[] { "other forbidden spell", classSpells[4] });

            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 1, });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "specialist field");
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));
            Assert.That(spellsKnown.Select(s => s.Name), Has.None.EqualTo("spell 5"));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersDoNotKnowProhibitedSpells_FromAlignmentLawfulness()
        {
            divineCasters.Add(caster);

            classSpells.Add("spell 5");
            spellLevels.Add(new TypeAndAmountSelection { Type = classSpells[4], Amount = 0, });

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "lawly:Prohibited"))
                .Returns(new[] { "other forbidden spell", classSpells[4] });

            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 1, });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "specialist field");
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
            divineCasters.Add(caster);

            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 0, });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility);
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersDoNotKnowSpellsBeyondLevel()
        {
            divineCasters.Add(caster);

            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 1, });

            classSpells.Add("spell 5");
            spellLevels.Add(new TypeAndAmountSelection { Type = classSpells[4], Amount = 2, });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility);
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersDoNotKnowSpecialistSpellsBeyondLevel()
        {
            divineCasters.Add(caster);

            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 1, });

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "special domain"))
                .Returns(new[] { "spell 5", "other special domain spell" });

            var specialDomainSpellLevels = new List<TypeAndAmountSelection>();
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "spell 5", Amount = 2, });
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "other special domain spell", Amount = 2, });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellLevels, "special domain"))
                .Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "special domain");
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersKnowSpecialistSpellsEvenIfQuantityForLevelIs0()
        {
            divineCasters.Add(caster);

            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 0, });

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "special domain"))
                .Returns(new[] { "spell 5", "other special domain spell" });

            var specialDomainSpellLevels = new List<TypeAndAmountSelection>();
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "spell 5", Amount = 1, });
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "other special domain spell", Amount = 2, });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellLevels, "special domain"))
                .Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "special domain");
            Assert.That(spellsKnown.Count(), Is.EqualTo(5));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(3));
        }

        [Test]
        public void DivineCastersDoNotKnowSpellsBeyondWhatAbilitiesAllow()
        {
            castingAbility.BaseScore = 10;
            divineCasters.Add(caster);

            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 1, });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility);
            Assert.That(spellsKnown.Count(), Is.EqualTo(2));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));
        }

        [Test]
        public void DivineCastersDoNotKnowSpecialistSpellsBeyondWhatAbilitiesAllow()
        {
            castingAbility.BaseScore = 10;
            divineCasters.Add(caster);

            spellsKnownForClass.Clear();
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "0", Amount = 1, });
            spellsKnownForClass.Add(new TypeAndAmountSelection { Type = "1", Amount = 0, });

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "special domain"))
                .Returns(new[] { "spell 5", "other special domain spell" });

            var specialDomainSpellLevels = new List<TypeAndAmountSelection>();
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "spell 5", Amount = 1, });
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "other special domain spell", Amount = 2, });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellLevels, "special domain"))
                .Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "special domain");
            Assert.That(spellsKnown.Count(), Is.EqualTo(2));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetRandomKnownSpecialistSpells()
        {
            classSpells.Add("spell 5");
            spellLevels.Add(new TypeAndAmountSelection { Type = classSpells[4], Amount = 1, });

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "special domain"))
                .Returns(new[] { "other special domain spell", classSpells[4] });

            var specialDomainSpellLevels = new List<TypeAndAmountSelection>();
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "spell 5", Amount = 1, });
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "other special domain spell", Amount = 2, });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellLevels, "special domain"))
                .Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "special domain");
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void GetRandomKnownNonSpecialistSpellIfAllSpecialistSpellsAreKnown()
        {
            classSpells.Add("spell 5");
            spellLevels.Add(new TypeAndAmountSelection { Type = classSpells[4], Amount = 1, });

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "special domain"))
                .Returns(new[] { classSpells[3] });

            var specialDomainSpellLevels = new List<TypeAndAmountSelection>();
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = classSpells[3], Amount = 1, });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellLevels, "special domain"))
                .Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "special domain");
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
        }

        [Test]
        public void RandomKnownSpellsAreNotProhibitedSpells_ProhibitedByDomain()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "domain 2:Prohibited"))
                .Returns(new[] { classSpells[0], classSpells[2] });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "domain 2");
            Assert.That(spellsKnown.Count(), Is.EqualTo(2));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(1));
            Assert.That(cantrips.First().Name, Is.EqualTo(classSpells[1]));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));
            Assert.That(firstLevelSpells.First().Name, Is.EqualTo(classSpells[3]));
        }

        [Test]
        public void RandomKnownSpellsAreNotProhibitedSpells_ProhibitedByCreature()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "creature:Prohibited"))
                .Returns(new[] { classSpells[0], classSpells[2] });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "domain 2");
            Assert.That(spellsKnown.Count(), Is.EqualTo(2));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(1));
            Assert.That(cantrips.First().Name, Is.EqualTo(classSpells[1]));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));
            Assert.That(firstLevelSpells.First().Name, Is.EqualTo(classSpells[3]));
        }

        [Test]
        public void RandomKnownSpellsAreNotProhibitedSpells_ProhibitedByAlignmentGoodness()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "goodly:Prohibited"))
                .Returns(new[] { classSpells[0], classSpells[2] });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "domain 2");
            Assert.That(spellsKnown.Count(), Is.EqualTo(2));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(1));
            Assert.That(cantrips.First().Name, Is.EqualTo(classSpells[1]));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));
            Assert.That(firstLevelSpells.First().Name, Is.EqualTo(classSpells[3]));
        }

        [Test]
        public void RandomKnownSpellsAreNotProhibitedSpells_ProhibitedByAlignmentLawfulness()
        {
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "lawly:Prohibited"))
                .Returns(new[] { classSpells[0], classSpells[2] });

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "domain 2");
            Assert.That(spellsKnown.Count(), Is.EqualTo(2));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(1));
            Assert.That(cantrips.First().Name, Is.EqualTo(classSpells[1]));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));
            Assert.That(firstLevelSpells.First().Name, Is.EqualTo(classSpells[3]));
        }

        [Test]
        public void RandomKnownSpellsAreSpecialistSpellsEvenIfQuantityForLevelIs0()
        {
            var selection = spellsKnownForClass.First(s => s.Type == "1");
            selection.Amount = 0;

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "special domain"))
                .Returns(new[] { "spell 5", "other special domain spell" });

            var specialDomainSpellLevels = new List<TypeAndAmountSelection>();
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "spell 5", Amount = 1, });
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "other special domain spell", Amount = 2, });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellLevels, "special domain"))
                .Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "special domain");
            Assert.That(spellsKnown.Count(), Is.EqualTo(3));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));
        }

        [Test]
        public void RandomKnownSpellsAreNotBeyondWhatAbilitiesAllow()
        {
            castingAbility.BaseScore = 10;

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility);
            Assert.That(spellsKnown.Count(), Is.EqualTo(2));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));
        }

        [Test]
        public void RandomKnownSpellsAreNotSpecialistSpellsBeyondWhatAbilitiesAllow()
        {
            castingAbility.BaseScore = 10;

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "domain 2");
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

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility);
            Assert.That(spellsKnown.Count(), Is.EqualTo(3));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));
            Assert.That(cantrips.Select(c => c.Name), Is.Unique);

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));
        }

        //INFO: Example is Blue Dragons, who can cast Cleric spells (as a domain) as a Sorcerer
        [Test]
        public void KnownSpellsFromDomainCanBeOtherCasterSpells()
        {
            var otherClassSpells = new List<string>();
            otherClassSpells.Add("other spell 1");
            otherClassSpells.Add("other spell 2");
            otherClassSpells.Add("other spell 3");
            otherClassSpells.Add("other spell 4");
            otherClassSpells.Add("other spell 5");

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "other class name"))
                .Returns(otherClassSpells);

            var otherSpellLevels = new List<TypeAndAmountSelection>();
            otherSpellLevels.Add(new TypeAndAmountSelection { Type = otherClassSpells[0], Amount = 0, });
            otherSpellLevels.Add(new TypeAndAmountSelection { Type = otherClassSpells[1], Amount = 0, });
            otherSpellLevels.Add(new TypeAndAmountSelection { Type = otherClassSpells[2], Amount = 1, });
            otherSpellLevels.Add(new TypeAndAmountSelection { Type = otherClassSpells[3], Amount = 1, });
            otherSpellLevels.Add(new TypeAndAmountSelection { Type = otherClassSpells[4], Amount = 1, });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellLevels, "other class name"))
                .Returns(otherSpellLevels);

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "special domain"))
                .Returns(new[] { "other special domain spell", otherClassSpells[4] });

            var specialDomainSpellLevels = new List<TypeAndAmountSelection>();
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = otherClassSpells[4], Amount = 1, });
            specialDomainSpellLevels.Add(new TypeAndAmountSelection { Type = "other special domain spell", Amount = 2, });

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellLevels, "special domain"))
                .Returns(specialDomainSpellLevels);

            var spellsKnown = spellsGenerator.GenerateKnown("creature", caster, casterLevel, alignment, castingAbility, "other class name", "special domain");
            Assert.That(spellsKnown.Count(), Is.EqualTo(4));

            var cantrips = spellsKnown.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));
            Assert.That(cantrips.First().Name, Is.EqualTo("spell 1"));
            Assert.That(cantrips.Last().Name, Is.EqualTo("spell 2"));

            var firstLevelSpells = spellsKnown.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(2));
            Assert.That(firstLevelSpells.First().Name, Is.EqualTo("spell 3"));
            Assert.That(firstLevelSpells.Last().Name, Is.EqualTo("other spell 4"));
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

            var spellsPrepared = spellsGenerator.GeneratePrepared(knownSpells, spellsPerDay);
            Assert.That(spellsPrepared.Count(), Is.EqualTo(3));

            var cantrips = spellsPrepared.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsPrepared.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));

            Assert.That(spellsPrepared.Select(s => s.Source), Is.All.EqualTo("source"));
        }

        [Test]
        public void BUG_GetRandomPreparedSpells_WithBonusSpells()
        {
            var knownSpells = new List<Spell>();
            knownSpells.Add(new Spell { Name = classSpells[0], Level = 0, Source = "source" });
            knownSpells.Add(new Spell { Name = classSpells[1], Level = 0, Source = "source" });
            knownSpells.Add(new Spell { Name = "other cantrip", Level = 0, Source = "source" });
            knownSpells.Add(new Spell { Name = classSpells[2], Level = 1, Source = "source" });
            knownSpells.Add(new Spell { Name = "other spell", Level = 1, Source = "source" });

            var spellsPerDay = new List<SpellQuantity>();
            spellsPerDay.Add(new SpellQuantity { Level = 0, Quantity = 2, Source = "source" });
            spellsPerDay.Add(new SpellQuantity { Level = 1, Quantity = 1, BonusSpells = 3, Source = "source" });

            var spellsPrepared = spellsGenerator.GeneratePrepared(knownSpells, spellsPerDay);
            Assert.That(spellsPrepared.Count(), Is.EqualTo(6));

            var cantrips = spellsPrepared.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));

            var firstLevelSpells = spellsPrepared.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(4));

            Assert.That(spellsPrepared.Select(s => s.Source), Is.All.EqualTo("source"));
        }

        [Test]
        public void GetRandomPreparedSpecialistSpells()
        {
            var knownSpells = new List<Spell>
            {
                new Spell { Name = classSpells[0], Level = 0 },
                new Spell { Name = classSpells[1], Level = 0 },
                new Spell { Name = "other cantrip", Level = 0 },
                new Spell { Name = classSpells[2], Level = 1 },
                new Spell { Name = "other spell", Level = 1 }
            };

            var spellsPerDay = new List<SpellQuantity>
            {
                new SpellQuantity { Level = 0, Quantity = 2 },
                new SpellQuantity { Level = 1, Quantity = 1, HasDomainSpell = true }
            };

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(Config.Name, TableNameConstants.Collection.SpellGroups, "domain 2"))
                .Returns([classSpells[1], classSpells[3], "other spell"]);

            var spellsPrepared = spellsGenerator.GeneratePrepared(knownSpells, spellsPerDay, "domain 2");
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

            var spellsPrepared = spellsGenerator.GeneratePrepared(knownSpells, spellsPerDay);
            Assert.That(spellsPrepared.Count(), Is.EqualTo(3));

            var cantrips = spellsPrepared.Where(s => s.Level == 0);
            Assert.That(cantrips.Count(), Is.EqualTo(2));
            Assert.That(cantrips.Select(c => c.Name), Is.All.EqualTo(classSpells[0]));

            var firstLevelSpells = spellsPrepared.Where(s => s.Level == 1);
            Assert.That(firstLevelSpells.Count(), Is.EqualTo(1));
        }
    }
}
