using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Generators.Defenses;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Defenses
{
    [TestFixture]
    public class ArmorClassGeneratorTests
    {
        private IArmorClassGenerator armorClassGenerator;
        private List<Feat> feats;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private Mock<IAdjustmentsSelector> mockAdjustmentsSelector;
        private CreatureType creatureType;
        private Dictionary<string, Ability> abilities;

        [SetUp]
        public void Setup()
        {
            mockAdjustmentsSelector = new Mock<IAdjustmentsSelector>();
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            armorClassGenerator = new ArmorClassGenerator(mockCollectionsSelector.Object, mockAdjustmentsSelector.Object);

            feats = new List<Feat>();
            creatureType = new CreatureType();
            abilities = new Dictionary<string, Ability>();

            creatureType.Name = "creature type";
            abilities[AbilityConstants.Dexterity] = new Ability(AbilityConstants.Dexterity);
            abilities[AbilityConstants.Charisma] = new Ability(AbilityConstants.Charisma);

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(0);
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.ArmorClassModifiers, GroupConstants.NaturalArmor)).Returns(Enumerable.Empty<string>());
        }

        [Test]
        public void ArmorClassesStartsAtBase()
        {
            GenerateAndAssertArmorClass();
        }

        private ArmorClass GenerateAndAssertArmorClass(int full = ArmorClass.BaseArmorClass, int flatFooted = ArmorClass.BaseArmorClass, int touch = ArmorClass.BaseArmorClass, bool circumstantial = false, int naturalArmor = 0)
        {
            var armorClass = armorClassGenerator.GenerateWith(abilities, "size", "creature", creatureType, feats, naturalArmor);

            Assert.That(armorClass.TotalBonus, Is.EqualTo(full), "full");
            Assert.That(armorClass.FlatFootedBonus, Is.EqualTo(flatFooted), "flat-footed");
            Assert.That(armorClass.TouchBonus, Is.EqualTo(touch), "touch");
            Assert.That(armorClass.CircumstantialBonus, Is.EqualTo(circumstantial));
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(naturalArmor));

            return armorClass;
        }

        //INFO: Example here is Githzerai's Inertial Armor
        [Test]
        public void AddArmorBonusFromFeats()
        {
            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "bracers", "other item", "feat", "wrong feat" });

            feats.Add(new Feat());
            feats.Add(new Feat());
            feats[0].Name = "feat";
            feats[0].Power = 1;
            feats[1].Name = "other feat";
            feats[1].Power = -1;

            var armorClass = GenerateAndAssertArmorClass(11, 11);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(1));
        }

        [Test]
        public void DexterityBonusApplied()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 12;
            var armorClass = GenerateAndAssertArmorClass(11, touch: 11);
        }

        [Test]
        public void NegativeDexterityBonusApplied()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 9;
            var armorClass = GenerateAndAssertArmorClass(9, touch: 9);
        }

        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 1)]
        [TestCase(5, 1)]
        [TestCase(6, 1)]
        [TestCase(7, 1)]
        [TestCase(8, 1)]
        [TestCase(9, 1)]
        [TestCase(10, 1)]
        [TestCase(11, 1)]
        [TestCase(12, 1)]
        [TestCase(13, 1)]
        [TestCase(14, 2)]
        [TestCase(15, 2)]
        [TestCase(16, 3)]
        [TestCase(17, 3)]
        [TestCase(18, 4)]
        [TestCase(19, 4)]
        [TestCase(20, 5)]
        public void IncorporealCreaturesGetDeflectionBonusEqualToCharismaModifier(int charisma, int bonus)
        {
            abilities[AbilityConstants.Charisma].BaseScore = charisma;

            creatureType.SubTypes = new[] { "other subtype", CreatureConstants.Types.Subtypes.Incorporeal };

            var armorClass = GenerateAndAssertArmorClass(10 + bonus, 10 + bonus, 10 + bonus);
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(bonus));
        }

        [Test]
        public void IncorporealCreaturesGetDeflectionBonusEqualToCharismaModifier()
        {
            abilities[AbilityConstants.Charisma].BaseScore = 9266;

            creatureType.SubTypes = new[] { "other subtype", CreatureConstants.Types.Subtypes.Incorporeal };

            var bonus = abilities[AbilityConstants.Charisma].Modifier;

            var armorClass = GenerateAndAssertArmorClass(10 + bonus, 10 + bonus, 10 + bonus);
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(bonus));
        }

        [Test]
        public void CorporealCreaturesDoNotGetDeflectionBonusEqualToCharismaModifier()
        {
            abilities[AbilityConstants.Charisma].BaseScore = 9266;

            creatureType.SubTypes = new[] { "other subtype", "subtype" };

            var armorClass = GenerateAndAssertArmorClass(10, 10, 10);
            Assert.That(armorClass.DeflectionBonus, Is.Zero);
        }

        [Test]
        public void SizeModifiesArmorClass()
        {
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(9266);

            var armorClass = GenerateAndAssertArmorClass(9276, 9276, 9276);
            Assert.That(armorClass.SizeModifier, Is.EqualTo(9266));
        }

        [Test]
        public void SizeModifiesArmorClassNegatively()
        {
            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(-4);

            var armorClass = GenerateAndAssertArmorClass(6, 6, 6);
        }

        [Test]
        public void NaturalArmorApplied()
        {
            var armorClass = GenerateAndAssertArmorClass(9276, 9276, naturalArmor: 9266);
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(9266));
        }

        [Test]
        public void ArmorClassesAreSummed()
        {
            abilities[AbilityConstants.Dexterity].BaseScore = 12;

            var feat = new Feat();
            feat.Name = "feat 1";
            feat.Power = 1;
            feats.Add(feat);

            var otherFeat = new Feat();
            otherFeat.Name = "feat 2";
            otherFeat.Power = 1;
            feats.Add(otherFeat);

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.ArmorClassModifiers, GroupConstants.ArmorBonus))
                .Returns(new[] { "feat 1" });

            mockAdjustmentsSelector.Setup(s => s.SelectFrom<int>(TableNameConstants.Adjustments.SizeModifiers, "size")).Returns(1);

            creatureType.SubTypes = new[] { "other subtype", CreatureConstants.Types.Subtypes.Incorporeal };

            var armorClass = GenerateAndAssertArmorClass(15, 14, 13, naturalArmor: 1);
            Assert.That(armorClass.ArmorBonus, Is.EqualTo(1));
            Assert.That(armorClass.DeflectionBonus, Is.EqualTo(1));
            Assert.That(armorClass.NaturalArmorBonus, Is.EqualTo(1));
            Assert.That(armorClass.ShieldBonus, Is.Zero);
            Assert.That(armorClass.SizeModifier, Is.EqualTo(1));
            Assert.That(armorClass.Dexterity.Modifier, Is.EqualTo(1));
        }
    }
}