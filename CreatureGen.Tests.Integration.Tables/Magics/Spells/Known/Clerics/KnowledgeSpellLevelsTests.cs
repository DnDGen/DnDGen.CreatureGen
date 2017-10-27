using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class KnowledgeSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Knowledge);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.DetectSecretDoors,
                SpellConstants.DetectThoughts,
                SpellConstants.ClairaudienceClairvoyance,
                SpellConstants.Divination,
                SpellConstants.TrueSeeing,
                SpellConstants.FindThePath,
                SpellConstants.LegendLore,
                SpellConstants.DiscernLocation,
                SpellConstants.Foresight
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllKnowledgeSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Knowledge]);
        }

        [TestCase(SpellConstants.DetectSecretDoors, 1)]
        [TestCase(SpellConstants.DetectThoughts, 2)]
        [TestCase(SpellConstants.ClairaudienceClairvoyance, 3)]
        [TestCase(SpellConstants.Divination, 4)]
        [TestCase(SpellConstants.TrueSeeing, 5)]
        [TestCase(SpellConstants.FindThePath, 6)]
        [TestCase(SpellConstants.LegendLore, 7)]
        [TestCase(SpellConstants.DiscernLocation, 8)]
        [TestCase(SpellConstants.Foresight, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
