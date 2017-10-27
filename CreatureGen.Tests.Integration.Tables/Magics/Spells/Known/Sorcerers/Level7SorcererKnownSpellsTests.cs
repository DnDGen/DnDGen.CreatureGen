﻿using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Sorcerers
{
    [TestFixture]
    public class Level7SorcererKnownSpellsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.LevelXCLASSKnownSpells, 7, CharacterClassConstants.Sorcerer);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = Enumerable.Range(0, 4).Select(i => i.ToString());
            AssertCollectionNames(names);
        }

        [TestCase(0, 7)]
        [TestCase(1, 5)]
        [TestCase(2, 3)]
        [TestCase(3, 2)]
        public void Adjustment(int spellLevel, int quantity)
        {
            base.Adjustment(spellLevel.ToString(), quantity);
        }
    }
}
