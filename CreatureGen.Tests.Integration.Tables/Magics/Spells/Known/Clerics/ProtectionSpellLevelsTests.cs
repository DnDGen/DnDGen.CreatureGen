using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class ProtectionSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Protection);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.Sanctuary,
                SpellConstants.ShieldOther,
                SpellConstants.ProtectionFromEnergy,
                SpellConstants.SpellImmunity,
                SpellConstants.SpellResistance,
                SpellConstants.AntimagicField,
                SpellConstants.Repulsion,
                SpellConstants.MindBlank,
                SpellConstants.PrismaticSphere
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllProtectionSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Protection]);
        }

        [TestCase(SpellConstants.Sanctuary, 1)]
        [TestCase(SpellConstants.ShieldOther, 2)]
        [TestCase(SpellConstants.ProtectionFromEnergy, 3)]
        [TestCase(SpellConstants.SpellImmunity, 4)]
        [TestCase(SpellConstants.SpellResistance, 5)]
        [TestCase(SpellConstants.AntimagicField, 6)]
        [TestCase(SpellConstants.Repulsion, 7)]
        [TestCase(SpellConstants.MindBlank, 8)]
        [TestCase(SpellConstants.PrismaticSphere, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
