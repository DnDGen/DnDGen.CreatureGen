using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class ChaosSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Chaos);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.ProtectionFromAlignment,
                SpellConstants.Shatter,
                SpellConstants.MagicCircleAgainstAlignment,
                SpellConstants.ChaosHammer,
                SpellConstants.DispelAlignment,
                SpellConstants.AnimateObjects,
                SpellConstants.WordOfChaos,
                SpellConstants.CloakOfChaos,
                SpellConstants.SummonMonsterIX
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllChaosSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Chaos]);
        }

        [TestCase(SpellConstants.ProtectionFromAlignment, 1)]
        [TestCase(SpellConstants.Shatter, 2)]
        [TestCase(SpellConstants.MagicCircleAgainstAlignment, 3)]
        [TestCase(SpellConstants.ChaosHammer, 4)]
        [TestCase(SpellConstants.DispelAlignment, 5)]
        [TestCase(SpellConstants.AnimateObjects, 6)]
        [TestCase(SpellConstants.WordOfChaos, 7)]
        [TestCase(SpellConstants.CloakOfChaos, 8)]
        [TestCase(SpellConstants.SummonMonsterIX, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
