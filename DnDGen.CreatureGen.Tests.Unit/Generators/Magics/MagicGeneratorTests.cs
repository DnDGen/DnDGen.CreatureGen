using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Items;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.TreasureGen.Items;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class MagicGeneratorTests
    {
        private Mock<ISpellsGenerator> mockSpellsGenerator;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private Mock<ITypeAndAmountSelector> mockTypeAndAmountSelector;
        private IMagicGenerator magicGenerator;
        private Alignment alignment;
        private Dictionary<string, Ability> abilities;
        private Equipment equipment;
        private Dictionary<string, int> arcaneSpellFailures;
        private List<string> classesThatPrepareSpells;
        private TypeAndAmountSelection caster;

        [SetUp]
        public void Setup()
        {
            mockSpellsGenerator = new Mock<ISpellsGenerator>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockTypeAndAmountSelector = new Mock<ITypeAndAmountSelector>();
            magicGenerator = new MagicGenerator(
                mockSpellsGenerator.Object,
                mockCollectionsSelector.Object,
                mockAdjustmentsSelector.Object,
                mockTypeAndAmountSelector.Object);
            alignment = new Alignment();
            abilities = new Dictionary<string, Ability>();
            equipment = new Equipment();
            arcaneSpellFailures = new Dictionary<string, int>();
            classesThatPrepareSpells = new List<string>();
            caster = new TypeAndAmountSelection();

            caster.Type = "class name";
            caster.Amount = 42;

            classesThatPrepareSpells.Add(caster.Type);
            classesThatPrepareSpells.Add("other class");

            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.Casters, "creature"))
                .Returns(new[] { caster });

            mockAdjustmentsSelector
                .Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.ArcaneSpellFailures, It.IsAny<string>()))
                .Returns((string table, string name) => arcaneSpellFailures[name]);
            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, GroupConstants.PreparesSpells))
                .Returns(classesThatPrepareSpells);
        }

        [Test]
        public void GenerateMagic()
        {
            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic, Is.Not.Null);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
        }

        [Test]
        public void GenerateSpells()
        {
            var spellsPerDay = new List<SpellQuantity> { new SpellQuantity() };
            var knownSpells = new List<Spell> { new Spell() };
            var preparedSpells = new List<Spell> { new Spell() };

            mockSpellsGenerator
                .Setup(g => g.GeneratePerDay(caster.Type, caster.Amount, abilities))
                .Returns(spellsPerDay);
            mockSpellsGenerator
                .Setup(g => g.GenerateKnown("creature", caster.Type, caster.Amount, alignment, abilities))
                .Returns(knownSpells);
            mockSpellsGenerator
                .Setup(g => g.GeneratePrepared(knownSpells, spellsPerDay))
                .Returns(preparedSpells);

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.SpellsPerDay, Is.EqualTo(spellsPerDay));
            Assert.That(magic.KnownSpells, Is.EqualTo(knownSpells));
            Assert.That(magic.PreparedSpells, Is.EqualTo(preparedSpells));
        }

        [Test]
        public void GenerateDomains_None()
        {
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellDomains, "creature"))
                .Returns(Enumerable.Empty<TypeAndAmountSelection>());

            var spellsPerDay = new List<SpellQuantity> { new SpellQuantity() };
            var knownSpells = new List<Spell> { new Spell() };
            var preparedSpells = new List<Spell> { new Spell() };

            mockSpellsGenerator
                .Setup(g => g.GeneratePerDay(caster.Type, caster.Amount, abilities))
                .Returns(spellsPerDay);
            mockSpellsGenerator
                .Setup(g => g.GenerateKnown("creature", caster.Type, caster.Amount, alignment, abilities))
                .Returns(knownSpells);
            mockSpellsGenerator
                .Setup(g => g.GeneratePrepared(knownSpells, spellsPerDay))
                .Returns(preparedSpells);

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.Domains, Is.Empty);
            Assert.That(magic.SpellsPerDay, Is.EqualTo(spellsPerDay));
            Assert.That(magic.KnownSpells, Is.EqualTo(knownSpells));
            Assert.That(magic.PreparedSpells, Is.EqualTo(preparedSpells));
        }

        //INFO: Example is Blue Dragons that can cast from cleric, air, evil, and law domains
        //all as arcane spells (Sorcerer)
        [Test]
        public void GenerateDomains_All()
        {
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellDomains, "creature"))
                .Returns(new[]
                {
                    new TypeAndAmountSelection { Type = "domain 1", Amount = 4 },
                    new TypeAndAmountSelection { Type = "domain 2", Amount = 4 },
                    new TypeAndAmountSelection { Type = "domain 3", Amount = 4 },
                    new TypeAndAmountSelection { Type = "domain 4", Amount = 4 },
                });

            var spellsPerDay = new List<SpellQuantity> { new SpellQuantity() };
            var knownSpells = new List<Spell> { new Spell() };
            var preparedSpells = new List<Spell> { new Spell() };

            mockSpellsGenerator
                .Setup(g => g.GeneratePerDay(caster.Type, caster.Amount, abilities, "domain 1", "domain 2", "domain 3", "domain 4"))
                .Returns(spellsPerDay);
            mockSpellsGenerator
                .Setup(g => g.GenerateKnown("creature", caster.Type, caster.Amount, alignment, abilities, "domain 1", "domain 2", "domain 3", "domain 4"))
                .Returns(knownSpells);
            mockSpellsGenerator
                .Setup(g => g.GeneratePrepared(knownSpells, spellsPerDay, "domain 1", "domain 2", "domain 3", "domain 4"))
                .Returns(preparedSpells);

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.Domains.Count(), Is.EqualTo(4));
            Assert.That(magic.Domains, Contains.Item("domain 1")
                .And.Contains("domain 2")
                .And.Contains("domain 3")
                .And.Contains("domain 4"));
            Assert.That(magic.SpellsPerDay, Is.EqualTo(spellsPerDay));
            Assert.That(magic.KnownSpells, Is.EqualTo(knownSpells));
            Assert.That(magic.PreparedSpells, Is.EqualTo(preparedSpells));
        }

        [Test]
        public void GenerateDomains_RandomSubset()
        {
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellDomains, "creature"))
                .Returns(new[]
                {
                    new TypeAndAmountSelection { Type = "domain 1", Amount = 2 },
                    new TypeAndAmountSelection { Type = "domain 2", Amount = 2 },
                    new TypeAndAmountSelection { Type = "domain 3", Amount = 2 },
                    new TypeAndAmountSelection { Type = "domain 4", Amount = 2 },
                });

            var count = 0;
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> c) => c.ElementAt(count++ % c.Count()));

            var spellsPerDay = new List<SpellQuantity> { new SpellQuantity() };
            var knownSpells = new List<Spell> { new Spell() };
            var preparedSpells = new List<Spell> { new Spell() };

            mockSpellsGenerator
                .Setup(g => g.GeneratePerDay(caster.Type, caster.Amount, abilities, "domain 1", "domain 3"))
                .Returns(spellsPerDay);
            mockSpellsGenerator
                .Setup(g => g.GenerateKnown("creature", caster.Type, caster.Amount, alignment, abilities, "domain 1", "domain 3"))
                .Returns(knownSpells);
            mockSpellsGenerator
                .Setup(g => g.GeneratePrepared(knownSpells, spellsPerDay, "domain 1", "domain 3"))
                .Returns(preparedSpells);

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.Domains.Count(), Is.EqualTo(2));
            Assert.That(magic.Domains, Contains.Item("domain 1").And.Contains("domain 3"));
            Assert.That(magic.SpellsPerDay, Is.EqualTo(spellsPerDay));
            Assert.That(magic.KnownSpells, Is.EqualTo(knownSpells));
            Assert.That(magic.PreparedSpells, Is.EqualTo(preparedSpells));
        }

        [Test]
        public void GenerateDomains_Only1Available()
        {
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellDomains, "creature"))
                .Returns(new[]
                {
                    new TypeAndAmountSelection { Type = "domain 3", Amount = 1 },
                });

            var count = 0;
            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> c) => c.ElementAt(count++ % c.Count()));

            var spellsPerDay = new List<SpellQuantity> { new SpellQuantity() };
            var knownSpells = new List<Spell> { new Spell() };
            var preparedSpells = new List<Spell> { new Spell() };

            mockSpellsGenerator
                .Setup(g => g.GeneratePerDay(caster.Type, caster.Amount, abilities, "domain 3"))
                .Returns(spellsPerDay);
            mockSpellsGenerator
                .Setup(g => g.GenerateKnown("creature", caster.Type, caster.Amount, alignment, abilities, "domain 3"))
                .Returns(knownSpells);
            mockSpellsGenerator
                .Setup(g => g.GeneratePrepared(knownSpells, spellsPerDay, "domain 3"))
                .Returns(preparedSpells);

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.Domains.Count(), Is.EqualTo(1));
            Assert.That(magic.Domains, Contains.Item("domain 3"));
            Assert.That(magic.SpellsPerDay, Is.EqualTo(spellsPerDay));
            Assert.That(magic.KnownSpells, Is.EqualTo(knownSpells));
            Assert.That(magic.PreparedSpells, Is.EqualTo(preparedSpells));
        }

        [Test]
        public void GenerateDomains_DoNotRepeatDomains()
        {
            mockTypeAndAmountSelector
                .Setup(s => s.Select(TableNameConstants.TypeAndAmount.SpellDomains, "creature"))
                .Returns(new[]
                {
                    new TypeAndAmountSelection { Type = "domain 1", Amount = 2 },
                    new TypeAndAmountSelection { Type = "domain 2", Amount = 2 },
                    new TypeAndAmountSelection { Type = "domain 3", Amount = 2 },
                    new TypeAndAmountSelection { Type = "domain 4", Amount = 2 },
                });

            mockCollectionsSelector
                .Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>()))
                .Returns((IEnumerable<string> c) => c.First());

            var spellsPerDay = new List<SpellQuantity> { new SpellQuantity() };
            var knownSpells = new List<Spell> { new Spell() };
            var preparedSpells = new List<Spell> { new Spell() };

            mockSpellsGenerator
                .Setup(g => g.GeneratePerDay(caster.Type, caster.Amount, abilities, "domain 1", "domain 2"))
                .Returns(spellsPerDay);
            mockSpellsGenerator
                .Setup(g => g.GenerateKnown("creature", caster.Type, caster.Amount, alignment, abilities, "domain 1", "domain 2"))
                .Returns(knownSpells);
            mockSpellsGenerator
                .Setup(g => g.GeneratePrepared(knownSpells, spellsPerDay, "domain 1", "domain 2"))
                .Returns(preparedSpells);

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.Domains.Count(), Is.EqualTo(2));
            Assert.That(magic.Domains, Contains.Item("domain 1").And.Contains("domain 2"));
            Assert.That(magic.SpellsPerDay, Is.EqualTo(spellsPerDay));
            Assert.That(magic.KnownSpells, Is.EqualTo(knownSpells));
            Assert.That(magic.PreparedSpells, Is.EqualTo(preparedSpells));
        }

        [Test]
        public void DoNotGeneratePreparedSpellsIfClassDoesNotPrepareSpells()
        {
            classesThatPrepareSpells.Remove(caster.Type);

            var spellsPerDay = new List<SpellQuantity> { new SpellQuantity() };
            var knownSpells = new List<Spell> { new Spell() };
            var preparedSpells = new List<Spell> { new Spell() };

            mockSpellsGenerator
                .Setup(g => g.GeneratePerDay(caster.Type, caster.Amount, abilities))
                .Returns(spellsPerDay);
            mockSpellsGenerator
                .Setup(g => g.GenerateKnown("creature", caster.Type, caster.Amount, alignment, abilities))
                .Returns(knownSpells);
            mockSpellsGenerator
                .Setup(g => g.GeneratePrepared(knownSpells, spellsPerDay))
                .Returns(preparedSpells);

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.SpellsPerDay, Is.EqualTo(spellsPerDay));
            Assert.That(magic.KnownSpells, Is.EqualTo(knownSpells));
            Assert.That(magic.PreparedSpells, Is.Not.EqualTo(preparedSpells));
            Assert.That(magic.PreparedSpells, Is.Empty);
        }

        [Test]
        public void GenerateNoArcaneSpellFailureIfNotArcaneSpellcaster()
        {
            equipment.Armor = new Armor();
            equipment.Armor.Name = "armor";
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane))
                .Returns(new[] { "other class", "wrong class" });

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.ArcaneSpellFailure, Is.Zero);
        }

        [Test]
        public void GenerateArcaneSpellFailureIfArcaneSpellcasterWithArmor()
        {
            equipment.Armor = new Armor();
            equipment.Armor.Name = "armor";
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane))
                .Returns(new[] { "other class", caster.Type, "wrong class" });

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateNoArcaneSpellFailureIfNoArmorOrShield()
        {
            arcaneSpellFailures[string.Empty] = 9266;

            mockCollectionsSelector
                .Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane))
                .Returns(new[] { caster.Type });

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.ArcaneSpellFailure, Is.Zero);
        }

        [Test]
        public void GenerateArcaneSpellFailureIfArcaneSpellcasterAndHasArmor()
        {
            equipment.Armor = new Armor();
            equipment.Armor.Name = "armor";
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane)).Returns(new[] { caster.Type });

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfArcaneSpellcasterAndHasShield()
        {
            equipment.Shield = new Armor();
            equipment.Shield.Name = "shield";
            equipment.Shield.ItemType = ItemTypeConstants.Armor;
            equipment.Shield.Attributes = new[] { AttributeConstants.Shield };
            arcaneSpellFailures[equipment.Shield.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane)).Returns(new[] { caster.Type });

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266));
        }

        [Test]
        public void GenerateArcaneSpellFailureIfArcaneSpellcasterAndHasArmorAndShield()
        {
            equipment.Armor = new Armor();
            equipment.Armor.Name = "armor";

            equipment.Shield = new Armor();
            equipment.Shield.Name = "shield";
            equipment.Shield.ItemType = ItemTypeConstants.Armor;
            equipment.Shield.Attributes = new[] { AttributeConstants.Shield };

            arcaneSpellFailures[equipment.Armor.Name] = 9266;
            arcaneSpellFailures[equipment.Shield.Name] = 90210;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane)).Returns(new[] { caster.Type });

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9266 + 90210));
        }

        [Test]
        public void MithralArmorDecreasesArcaneSpellFailureBy10()
        {
            equipment.Armor = new Armor();
            equipment.Armor.Name = "armor";
            equipment.Armor.Traits.Add(TraitConstants.SpecialMaterials.Mithral);
            arcaneSpellFailures[equipment.Armor.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane)).Returns(new[] { caster.Type });

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9256));
        }

        [Test]
        public void MithralArmorCannotDecreasesArcaneSpellFailureBelow0()
        {
            equipment.Armor = new Armor();
            equipment.Armor.Name = "armor";
            equipment.Armor.Traits.Add(TraitConstants.SpecialMaterials.Mithral);
            arcaneSpellFailures[equipment.Armor.Name] = 5;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane)).Returns(new[] { caster.Type });

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.ArcaneSpellFailure, Is.Zero);
        }

        [Test]
        public void MithralArmorCannotDecreasesShieldArcaneSpellFailure()
        {
            equipment.Armor = new Armor();
            equipment.Armor.Name = "armor";
            equipment.Armor.Traits.Add(TraitConstants.SpecialMaterials.Mithral);
            arcaneSpellFailures[equipment.Armor.Name] = 5;

            equipment.Shield = new Armor();
            equipment.Shield.Name = "shield";
            equipment.Shield.ItemType = ItemTypeConstants.Armor;
            equipment.Shield.Attributes = new[] { AttributeConstants.Shield };
            arcaneSpellFailures[equipment.Shield.Name] = 5;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane)).Returns(new[] { caster.Type });

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(5));
        }

        [Test]
        public void MithralShieldDecreasesArcaneSpellFailureBy10()
        {
            equipment.Shield = new Armor();
            equipment.Shield.Name = "shield";
            equipment.Shield.ItemType = ItemTypeConstants.Armor;
            equipment.Shield.Attributes = new[] { AttributeConstants.Shield };
            equipment.Shield.Traits.Add(TraitConstants.SpecialMaterials.Mithral);
            arcaneSpellFailures[equipment.Shield.Name] = 9266;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane)).Returns(new[] { caster.Type });

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(9256));
        }

        [Test]
        public void MithralShieldCannotDecreasesArcaneSpellFailureBelow0()
        {
            equipment.Shield = new Armor();
            equipment.Shield.Name = "shield";
            equipment.Shield.ItemType = ItemTypeConstants.Armor;
            equipment.Shield.Attributes = new[] { AttributeConstants.Shield };
            equipment.Shield.Traits.Add(TraitConstants.SpecialMaterials.Mithral);
            arcaneSpellFailures[equipment.Shield.Name] = 5;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane)).Returns(new[] { caster.Type });

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.ArcaneSpellFailure, Is.Zero);
        }

        [Test]
        public void MithralShieldCannotDecreasesArmorArcaneSpellFailure()
        {
            equipment.Armor = new Armor();
            equipment.Armor.Name = "armor";
            arcaneSpellFailures[equipment.Armor.Name] = 5;

            equipment.Shield = new Armor();
            equipment.Shield.Name = "shield";
            equipment.Shield.ItemType = ItemTypeConstants.Armor;
            equipment.Shield.Attributes = new[] { AttributeConstants.Shield };
            equipment.Shield.Traits.Add(TraitConstants.SpecialMaterials.Mithral);
            arcaneSpellFailures[equipment.Shield.Name] = 5;

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.CasterGroups, SpellConstants.Sources.Arcane)).Returns(new[] { caster.Type });

            var magic = magicGenerator.GenerateWith("creature", alignment, abilities, equipment);
            Assert.That(magic.Caster, Is.EqualTo("class name"));
            Assert.That(magic.CasterLevel, Is.EqualTo(42));
            Assert.That(magic.ArcaneSpellFailure, Is.EqualTo(5));
        }
    }
}
