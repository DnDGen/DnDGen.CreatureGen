using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class StrengthSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Strength);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.EnlargePerson,
                SpellConstants.BullsStrength,
                SpellConstants.MagicVestment,
                SpellConstants.SpellImmunity,
                SpellConstants.RighteousMight,
                SpellConstants.Stoneskin,
                SpellConstants.GraspingHand,
                SpellConstants.ClenchedFist,
                SpellConstants.CrushingHand
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllStrengthSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Strength]);
        }

        [TestCase(SpellConstants.EnlargePerson, 1)]
        [TestCase(SpellConstants.BullsStrength, 2)]
        [TestCase(SpellConstants.MagicVestment, 3)]
        [TestCase(SpellConstants.SpellImmunity, 4)]
        [TestCase(SpellConstants.RighteousMight, 5)]
        [TestCase(SpellConstants.Stoneskin, 6)]
        [TestCase(SpellConstants.GraspingHand, 7)]
        [TestCase(SpellConstants.ClenchedFist, 8)]
        [TestCase(SpellConstants.CrushingHand, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
