using System;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class ProhibitedFieldQuantitiesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.ProhibitedFieldQuantities; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[] 
            {
                CharacterClassConstants.Schools.Abjuration, 
                CharacterClassConstants.Schools.Conjuration, 
                CharacterClassConstants.Schools.Divination, 
                CharacterClassConstants.Schools.Enchantment, 
                CharacterClassConstants.Schools.Evocation, 
                CharacterClassConstants.Schools.Illusion,
                CharacterClassConstants.Schools.Necromancy, 
                CharacterClassConstants.Schools.Transmutation, 
                CharacterClassConstants.Domains.Air, 
                CharacterClassConstants.Domains.Animal, 
                CharacterClassConstants.Domains.Chaos, 
                CharacterClassConstants.Domains.Death, 
                CharacterClassConstants.Domains.Destruction, 
                CharacterClassConstants.Domains.Earth, 
                CharacterClassConstants.Domains.Evil, 
                CharacterClassConstants.Domains.Fire, 
                CharacterClassConstants.Domains.Good, 
                CharacterClassConstants.Domains.Healing, 
                CharacterClassConstants.Domains.Knowledge, 
                CharacterClassConstants.Domains.Law, 
                CharacterClassConstants.Domains.Luck, 
                CharacterClassConstants.Domains.Magic, 
                CharacterClassConstants.Domains.Plant, 
                CharacterClassConstants.Domains.Protection, 
                CharacterClassConstants.Domains.Strength, 
                CharacterClassConstants.Domains.Sun, 
                CharacterClassConstants.Domains.Travel, 
                CharacterClassConstants.Domains.Trickery, 
                CharacterClassConstants.Domains.War, 
                CharacterClassConstants.Domains.Water
            };

            AssertCollectionNames(names);
        }

        [TestCase(CharacterClassConstants.Schools.Abjuration, 2)]
        [TestCase(CharacterClassConstants.Schools.Conjuration, 2)]
        [TestCase(CharacterClassConstants.Schools.Divination, 1)]
        [TestCase(CharacterClassConstants.Schools.Enchantment, 2)]
        [TestCase(CharacterClassConstants.Schools.Evocation, 2)]
        [TestCase(CharacterClassConstants.Schools.Illusion, 2)]
        [TestCase(CharacterClassConstants.Schools.Necromancy, 2)]
        [TestCase(CharacterClassConstants.Schools.Transmutation, 2)]
        [TestCase(CharacterClassConstants.Domains.Air, 0)]
        [TestCase(CharacterClassConstants.Domains.Animal, 0)]
        [TestCase(CharacterClassConstants.Domains.Chaos, 0)]
        [TestCase(CharacterClassConstants.Domains.Death, 0)]
        [TestCase(CharacterClassConstants.Domains.Destruction, 0)]
        [TestCase(CharacterClassConstants.Domains.Earth, 0)]
        [TestCase(CharacterClassConstants.Domains.Evil, 0)]
        [TestCase(CharacterClassConstants.Domains.Fire, 0)]
        [TestCase(CharacterClassConstants.Domains.Good, 0)]
        [TestCase(CharacterClassConstants.Domains.Healing, 0)]
        [TestCase(CharacterClassConstants.Domains.Knowledge, 0)]
        [TestCase(CharacterClassConstants.Domains.Law, 0)]
        [TestCase(CharacterClassConstants.Domains.Luck, 0)]
        [TestCase(CharacterClassConstants.Domains.Magic, 0)]
        [TestCase(CharacterClassConstants.Domains.Plant, 0)]
        [TestCase(CharacterClassConstants.Domains.Protection, 0)]
        [TestCase(CharacterClassConstants.Domains.Strength, 0)]
        [TestCase(CharacterClassConstants.Domains.Sun, 0)]
        [TestCase(CharacterClassConstants.Domains.Travel, 0)]
        [TestCase(CharacterClassConstants.Domains.Trickery, 0)]
        [TestCase(CharacterClassConstants.Domains.War, 0)]
        [TestCase(CharacterClassConstants.Domains.Water, 0)]
        public void ProhibitedFieldQuantity(string name, int quantity)
        {
            base.Adjustment(name, quantity);
        }
    }
}