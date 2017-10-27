using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Paladins
{
    [TestFixture]
    public class Level1PaladinKnownSpellsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSKnownSpells, 1, CharacterClassConstants.Paladin);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Empty<String>();
            AssertCollectionNames(names);
        }
    }
}
