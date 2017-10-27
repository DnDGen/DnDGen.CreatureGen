using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class LuckSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Luck);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.EntropicShield,
                SpellConstants.Aid,
                SpellConstants.ProtectionFromEnergy,
                SpellConstants.FreedomOfMovement,
                SpellConstants.BreakEnchantment,
                SpellConstants.Mislead,
                SpellConstants.SpellTurning,
                SpellConstants.MomentOfPrescience,
                SpellConstants.Miracle
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllLuckSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Luck]);
        }

        [TestCase(SpellConstants.EntropicShield, 1)]
        [TestCase(SpellConstants.Aid, 2)]
        [TestCase(SpellConstants.ProtectionFromEnergy, 3)]
        [TestCase(SpellConstants.FreedomOfMovement, 4)]
        [TestCase(SpellConstants.BreakEnchantment, 5)]
        [TestCase(SpellConstants.Mislead, 6)]
        [TestCase(SpellConstants.SpellTurning, 7)]
        [TestCase(SpellConstants.MomentOfPrescience, 8)]
        [TestCase(SpellConstants.Miracle, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
