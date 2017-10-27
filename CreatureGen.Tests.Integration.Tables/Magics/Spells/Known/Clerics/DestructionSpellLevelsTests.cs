using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class DestructionSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Destruction);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.CureInflictLightWounds,
                SpellConstants.Shatter,
                SpellConstants.Contagion,
                SpellConstants.CureInflictCriticalWounds,
                SpellConstants.CureInflictLightWounds_Mass,
                SpellConstants.HealHarm,
                SpellConstants.Disintegrate,
                SpellConstants.Earthquake,
                SpellConstants.Implosion
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllDestructionSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Destruction]);
        }

        [TestCase(SpellConstants.CureInflictLightWounds, 1)]
        [TestCase(SpellConstants.Shatter, 2)]
        [TestCase(SpellConstants.Contagion, 3)]
        [TestCase(SpellConstants.CureInflictCriticalWounds, 4)]
        [TestCase(SpellConstants.CureInflictLightWounds_Mass, 5)]
        [TestCase(SpellConstants.HealHarm, 6)]
        [TestCase(SpellConstants.Disintegrate, 7)]
        [TestCase(SpellConstants.Earthquake, 8)]
        [TestCase(SpellConstants.Implosion, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
