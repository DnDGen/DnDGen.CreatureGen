using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class EvilSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Evil);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.ProtectionFromAlignment,
                SpellConstants.Desecrate,
                SpellConstants.MagicCircleAgainstAlignment,
                SpellConstants.UnholyBlight,
                SpellConstants.DispelAlignment,
                SpellConstants.CreateUndead,
                SpellConstants.Blasphemy,
                SpellConstants.UnholyAura,
                SpellConstants.SummonMonsterIX
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllEvilSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Evil]);
        }

        [TestCase(SpellConstants.ProtectionFromAlignment, 1)]
        [TestCase(SpellConstants.Desecrate, 2)]
        [TestCase(SpellConstants.MagicCircleAgainstAlignment, 3)]
        [TestCase(SpellConstants.UnholyBlight, 4)]
        [TestCase(SpellConstants.DispelAlignment, 5)]
        [TestCase(SpellConstants.CreateUndead, 6)]
        [TestCase(SpellConstants.Blasphemy, 7)]
        [TestCase(SpellConstants.UnholyAura, 8)]
        [TestCase(SpellConstants.SummonMonsterIX, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
