using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.PerDay.Paladins
{
    [TestFixture]
    public class Level8PaladinSpellsPerDayTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSSpellsPerDay, 8, CharacterClassConstants.Paladin);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(1, 2).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(1, 1)]
        [TestCase(2, 0)]
        public void Adjustment(int spellLevel, int quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}
