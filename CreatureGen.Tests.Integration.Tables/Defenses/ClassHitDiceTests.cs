using CreatureGen.CharacterClasses;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class ClassHitDiceTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.ClassHitDice; }
        }

        [Test]
        public override void CollectionNames()
        {
            var classNameGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);
            AssertCollectionNames(classNameGroups[GroupConstants.All]);
        }

        [TestCase(CharacterClassConstants.Barbarian, 12)]
        [TestCase(CharacterClassConstants.Bard, 6)]
        [TestCase(CharacterClassConstants.Cleric, 8)]
        [TestCase(CharacterClassConstants.Druid, 8)]
        [TestCase(CharacterClassConstants.Fighter, 10)]
        [TestCase(CharacterClassConstants.Monk, 8)]
        [TestCase(CharacterClassConstants.Paladin, 10)]
        [TestCase(CharacterClassConstants.Ranger, 8)]
        [TestCase(CharacterClassConstants.Rogue, 6)]
        [TestCase(CharacterClassConstants.Sorcerer, 4)]
        [TestCase(CharacterClassConstants.Wizard, 4)]
        [TestCase(CharacterClassConstants.Adept, 6)]
        [TestCase(CharacterClassConstants.Aristocrat, 8)]
        [TestCase(CharacterClassConstants.Commoner, 4)]
        [TestCase(CharacterClassConstants.Expert, 6)]
        [TestCase(CharacterClassConstants.Warrior, 8)]
        public void ClassHitDice(string className, int hitDice)
        {
            Adjustment(className, hitDice);
        }
    }
}