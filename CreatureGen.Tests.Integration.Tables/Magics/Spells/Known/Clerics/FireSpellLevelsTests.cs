using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class FireSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Fire);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.BurningHands,
                SpellConstants.ProduceFlame,
                SpellConstants.ResistEnergy,
                SpellConstants.WallOfFire,
                SpellConstants.FireShield,
                SpellConstants.FireSeeds,
                SpellConstants.FireStorm,
                SpellConstants.IncendiaryCloud,
                SpellConstants.ElementalSwarm
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllFireSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Fire]);
        }

        [TestCase(SpellConstants.BurningHands, 1)]
        [TestCase(SpellConstants.ProduceFlame, 2)]
        [TestCase(SpellConstants.ResistEnergy, 3)]
        [TestCase(SpellConstants.WallOfFire, 4)]
        [TestCase(SpellConstants.FireShield, 5)]
        [TestCase(SpellConstants.FireSeeds, 6)]
        [TestCase(SpellConstants.FireStorm, 7)]
        [TestCase(SpellConstants.IncendiaryCloud, 8)]
        [TestCase(SpellConstants.ElementalSwarm, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
