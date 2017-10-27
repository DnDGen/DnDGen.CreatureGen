using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class SpecialistFieldQuantitiesTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.SpecialistFieldQuantities; }
        }

        [Test]
        public override void CollectionNames()
        {
            var classGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);
            AssertCollectionNames(classGroups[GroupConstants.All]);
        }

        [TestCase(CharacterClassConstants.Adept, 0)]
        [TestCase(CharacterClassConstants.Aristocrat, 0)]
        [TestCase(CharacterClassConstants.Barbarian, 0)]
        [TestCase(CharacterClassConstants.Bard, 0)]
        [TestCase(CharacterClassConstants.Cleric, 2)]
        [TestCase(CharacterClassConstants.Commoner, 0)]
        [TestCase(CharacterClassConstants.Druid, 0)]
        [TestCase(CharacterClassConstants.Expert, 0)]
        [TestCase(CharacterClassConstants.Fighter, 0)]
        [TestCase(CharacterClassConstants.Monk, 0)]
        [TestCase(CharacterClassConstants.Paladin, 0)]
        [TestCase(CharacterClassConstants.Ranger, 0)]
        [TestCase(CharacterClassConstants.Rogue, 0)]
        [TestCase(CharacterClassConstants.Sorcerer, 0)]
        [TestCase(CharacterClassConstants.Warrior, 0)]
        [TestCase(CharacterClassConstants.Wizard, 1)]
        public void SpecialistFieldQuantity(string name, int quantity)
        {
            base.Adjustment(name, quantity);
        }
    }
}