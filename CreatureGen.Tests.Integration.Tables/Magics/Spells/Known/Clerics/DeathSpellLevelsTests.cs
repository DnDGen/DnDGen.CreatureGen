using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class DeathSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Death);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.CauseFear,
                SpellConstants.DeathKnell,
                SpellConstants.AnimateDead,
                SpellConstants.DeathWard,
                SpellConstants.SlayLiving,
                SpellConstants.CreateUndead,
                SpellConstants.Destruction,
                SpellConstants.CreateGreaterUndead,
                SpellConstants.WailOfTheBanshee
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllDeathSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Death]);
        }

        [TestCase(SpellConstants.CauseFear, 1)]
        [TestCase(SpellConstants.DeathKnell, 2)]
        [TestCase(SpellConstants.AnimateDead, 3)]
        [TestCase(SpellConstants.DeathWard, 4)]
        [TestCase(SpellConstants.SlayLiving, 5)]
        [TestCase(SpellConstants.CreateUndead, 6)]
        [TestCase(SpellConstants.Destruction, 7)]
        [TestCase(SpellConstants.CreateGreaterUndead, 8)]
        [TestCase(SpellConstants.WailOfTheBanshee, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
