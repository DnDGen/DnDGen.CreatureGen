using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class HealingSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Healing);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.CureInflictLightWounds,
                SpellConstants.CureInflictModerateWounds,
                SpellConstants.CureInflictSeriousWounds,
                SpellConstants.CureInflictCriticalWounds,
                SpellConstants.CureInflictLightWounds_Mass,
                SpellConstants.HealHarm,
                SpellConstants.Regenerate,
                SpellConstants.CureInflictCriticalWounds_Mass,
                SpellConstants.HealHarm_Mass
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllHealingSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Healing]);
        }

        [TestCase(SpellConstants.CureInflictLightWounds, 1)]
        [TestCase(SpellConstants.CureInflictModerateWounds, 2)]
        [TestCase(SpellConstants.CureInflictSeriousWounds, 3)]
        [TestCase(SpellConstants.CureInflictCriticalWounds, 4)]
        [TestCase(SpellConstants.CureInflictLightWounds_Mass, 5)]
        [TestCase(SpellConstants.HealHarm, 6)]
        [TestCase(SpellConstants.Regenerate, 7)]
        [TestCase(SpellConstants.CureInflictCriticalWounds_Mass, 8)]
        [TestCase(SpellConstants.HealHarm_Mass, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
