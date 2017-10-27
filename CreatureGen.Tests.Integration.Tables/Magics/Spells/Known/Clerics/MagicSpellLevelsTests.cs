using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class MagicSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Magic);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.MagicAura,
                SpellConstants.Identify,
                SpellConstants.DispelMagic,
                SpellConstants.ImbueWithSpellAbility,
                SpellConstants.SpellResistance,
                SpellConstants.AntimagicField,
                SpellConstants.SpellTurning,
                SpellConstants.ProtectionFromSpells,
                SpellConstants.MagesDisjunction
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllMagicSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Magic]);
        }

        [TestCase(SpellConstants.MagicAura, 1)]
        [TestCase(SpellConstants.Identify, 2)]
        [TestCase(SpellConstants.DispelMagic, 3)]
        [TestCase(SpellConstants.ImbueWithSpellAbility, 4)]
        [TestCase(SpellConstants.SpellResistance, 5)]
        [TestCase(SpellConstants.AntimagicField, 6)]
        [TestCase(SpellConstants.SpellTurning, 7)]
        [TestCase(SpellConstants.ProtectionFromSpells, 8)]
        [TestCase(SpellConstants.MagesDisjunction, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
