using CreatureGen.Creatures;
using CreatureGen.Selectors.Collections;
using CreatureGen.Selectors.Selections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class AttackSelectorTests
    {
        private IAttackSelector attackSelector;
        private Mock<ICollectionSelector> mockCollectionSelector;

        [SetUp]
        public void Setup()
        {
            mockCollectionSelector = new Mock<ICollectionSelector>();
            attackSelector = new AttackSelector(mockCollectionSelector.Object);
        }

        [Test]
        public void SelectNoAttacks()
        {
            var attacks = attackSelector.Select("creature", "original size", "advanced size");
            Assert.That(attacks, Is.Empty);
        }

        [TestCase(false, false, false, false)]
        [TestCase(false, false, false, true)]
        [TestCase(false, false, true, false)]
        [TestCase(false, false, true, true)]
        [TestCase(false, true, false, false)]
        [TestCase(false, true, false, true)]
        [TestCase(false, true, true, false)]
        [TestCase(false, true, true, true)]
        [TestCase(true, false, false, false)]
        [TestCase(true, false, false, true)]
        [TestCase(true, false, true, false)]
        [TestCase(true, false, true, true)]
        [TestCase(true, true, false, false)]
        [TestCase(true, true, false, true)]
        [TestCase(true, true, true, false)]
        [TestCase(true, true, true, true)]
        public void SelectAttack(bool isNatural, bool isMelee, bool isPrimary, bool isSpecial)
        {
            var attackData = new[]
            {
                GetData("name", "damage", isNatural, isMelee, isPrimary, isSpecial)
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", "original size", "advanced size");
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damage, Is.EqualTo("damage"));
            Assert.That(attack.IsMelee, Is.EqualTo(isMelee));
            Assert.That(attack.IsNatural, Is.EqualTo(isNatural));
            Assert.That(attack.IsPrimary, Is.EqualTo(isPrimary));
            Assert.That(attack.Name, Is.EqualTo("name"));
        }

        private string GetData(string name, string damage, bool isNatural = false, bool isMelee = false, bool isPrimary = false, bool isSpecial = false)
        {
            var data = DataIndexConstants.AttackData.InitializeData();
            data[DataIndexConstants.AttackData.DamageIndex] = damage;
            data[DataIndexConstants.AttackData.IsMeleeIndex] = isMelee.ToString();
            data[DataIndexConstants.AttackData.IsNaturalIndex] = isNatural.ToString();
            data[DataIndexConstants.AttackData.IsPrimaryIndex] = isPrimary.ToString();
            data[DataIndexConstants.AttackData.IsSpecialIndex] = isSpecial.ToString();
            data[DataIndexConstants.AttackData.NameIndex] = name;

            return string.Join(AttackSelection.Divider.ToString(), data);
        }

        [Test]
        public void SelectAttacks()
        {
            var attackData = new[]
            {
                GetData("name", "damage"),
                GetData("other name", "other damage"),
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", "original size", "advanced size");
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(2));

            var attack = attacks.First();
            Assert.That(attack.Damage, Is.EqualTo("damage"));
            Assert.That(attack.Name, Is.EqualTo("name"));

            attack = attacks.Last();
            Assert.That(attack.Damage, Is.EqualTo("other damage"));
            Assert.That(attack.Name, Is.EqualTo("other name"));
        }

        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d2", "1d2")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d3", "1d3")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d4", "1d4")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d6", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d8", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "1d10", "1d10")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "2d6", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Fine, "2d8", "2d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d2", "1d3")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d3", "1d4")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d4", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d6", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d8", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "1d10", "2d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Diminutive, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d2", "1d4")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d3", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d4", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d6", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Tiny, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d2", "1d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d3", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d4", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Small, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d2", "1d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d3", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d2", "2d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d2", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d2", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d2", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Fine, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d2", "1d2")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d3", "1d3")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d4", "1d4")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d6", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d8", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "1d10", "1d10")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "2d6", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Diminutive, "2d8", "2d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d2", "1d3")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d3", "1d4")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d4", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d6", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d8", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "1d10", "2d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Tiny, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d2", "1d4")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d3", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d4", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d6", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Small, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d2", "1d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d3", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d4", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d2", "1d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d3", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d2", "2d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d3", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d2", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d3", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d2", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Diminutive, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d2", "1d2")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d3", "1d3")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d4", "1d4")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d6", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d8", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "1d10", "1d10")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "2d6", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Tiny, "2d8", "2d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d2", "1d3")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d3", "1d4")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d4", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d6", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d8", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "1d10", "2d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Small, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d2", "1d4")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d3", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d4", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d6", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d2", "1d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d3", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d4", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d2", "1d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d3", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d4", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d2", "2d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d3", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d2", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Tiny, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d2", "1d2")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d3", "1d3")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d4", "1d4")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d6", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d8", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "1d10", "1d10")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "2d6", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Small, "2d8", "2d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d2", "1d3")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d3", "1d4")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d4", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d6", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d8", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "1d10", "2d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Medium, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d2", "1d4")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d3", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d4", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d6", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d2", "1d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d3", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d4", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d2", "1d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d3", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d4", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d2", "2d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d3", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Small, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d2", "1d2")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d3", "1d3")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d4", "1d4")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d6", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d8", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "1d10", "1d10")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "2d6", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Medium, "2d8", "2d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d2", "1d3")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d3", "1d4")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d4", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d6", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d8", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "1d10", "2d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Large, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d2", "1d4")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d3", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d4", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d6", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d8", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "1d10", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d2", "1d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d3", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d4", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d2", "1d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d3", "2d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d4", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Medium, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d2", "1d2")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d3", "1d3")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d4", "1d4")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d6", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d8", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "1d10", "1d10")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "2d6", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Large, "2d8", "2d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d2", "1d3")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d3", "1d4")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d4", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d6", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d8", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "1d10", "2d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "2d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Huge, "2d8", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d2", "1d4")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d3", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d4", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d6", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d8", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "1d10", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d2", "1d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d3", "1d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d4", "2d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Large, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d2", "1d2")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d3", "1d3")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d4", "1d4")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d6", "1d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d8", "1d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "1d10", "1d10")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "2d6", "2d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Huge, "2d8", "2d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d2", "1d3")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d3", "1d4")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d4", "1d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d6", "1d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d8", "2d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "1d10", "2d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "2d6", "3d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Gargantuan, "2d8", "3d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d2", "1d4")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d3", "1d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d4", "1d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d6", "2d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d8", "3d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "1d10", "3d8")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Huge, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d2", "1d2")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d3", "1d3")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d4", "1d4")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d6", "1d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d8", "1d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "1d10", "1d10")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "2d6", "2d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Gargantuan, "2d8", "2d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d2", "1d3")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d3", "1d4")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d4", "1d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d6", "1d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d8", "2d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "1d10", "2d8")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "2d6", "3d6")]
        [TestCase(SizeConstants.Gargantuan, SizeConstants.Colossal, "2d8", "3d8")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d2", "1d2")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d3", "1d3")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d4", "1d4")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d6", "1d6")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d8", "1d8")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "1d10", "1d10")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "2d6", "2d6")]
        [TestCase(SizeConstants.Colossal, SizeConstants.Colossal, "2d8", "2d8")]
        public void AdjustDamageForAdvancedSizeForNaturalAttack(string originalSize, string advancedSize, string originalDamage, string advancedDamage)
        {
            var attackData = new[]
            {
                GetData("name", originalDamage, isNatural: true)
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", originalSize, advancedSize);
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damage, Is.EqualTo(advancedDamage));
            Assert.That(attack.IsNatural, Is.True);
            Assert.That(attack.Name, Is.EqualTo("name"));
        }

        [TestCase("1d6+Slime", "1d8+Slime")]
        [TestCase("1d6 + Slime", "1d8 + Slime")]
        [TestCase("1d6+STRENGTH", "1d8+STRENGTH")]
        [TestCase("1d6 + STRENGTH", "1d8 + STRENGTH")]
        [TestCase("1d6+STRENGTH+Slime", "1d8+STRENGTH+Slime")]
        [TestCase("1d6 + STRENGTH + Slime", "1d8 + STRENGTH + Slime")]
        [TestCase("2d6+2 + STRENGTH + 1d4 acid", "3d6+2 + STRENGTH + 1d6 acid")]
        [TestCase("1d4 Wisdom drain", "1d6 Wisdom drain")]
        public void AdjustDamageForAdvancedSizeForNaturalAttackWithNonRollDamage(string originalDamage, string advancedDamage)
        {
            var attackData = new[]
            {
                GetData("name", originalDamage, isNatural: true)
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", SizeConstants.Medium, SizeConstants.Large);
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damage, Is.EqualTo(advancedDamage));
            Assert.That(attack.IsNatural, Is.True);
            Assert.That(attack.Name, Is.EqualTo("name"));
        }

        [TestCase("4d6")]
        [TestCase("4d4")]
        public void AdjustDamageForAdvancedSizeForNaturalAttackWithNonAdjustableRollDamage(string originalDamage)
        {
            var attackData = new[]
            {
                GetData("name", originalDamage, isNatural: true)
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", SizeConstants.Fine, SizeConstants.Colossal);
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damage, Is.EqualTo(originalDamage));
            Assert.That(attack.IsNatural, Is.True);
            Assert.That(attack.Name, Is.EqualTo("name"));
        }

        [Test]
        public void DoNotAdjustDamageForAdvancedSizeForUnnaturalAttack()
        {
            var attackData = new[]
            {
                GetData("name", "1d2", isNatural: false)
            };

            mockCollectionSelector.Setup(s => s.SelectFrom(TableNameConstants.Collection.AttackData, "creature")).Returns(attackData);

            var attacks = attackSelector.Select("creature", SizeConstants.Medium, SizeConstants.Large);
            Assert.That(attacks, Is.Not.Empty);
            Assert.That(attacks.Count, Is.EqualTo(1));

            var attack = attacks.Single();
            Assert.That(attack.Damage, Is.EqualTo("1d2"));
            Assert.That(attack.IsNatural, Is.False);
            Assert.That(attack.Name, Is.EqualTo("name"));
        }
    }
}
