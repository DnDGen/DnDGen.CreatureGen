using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Magics;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Magics.Spells.Known.Clerics
{
    [TestFixture]
    public class AnimalSpellLevelsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get
            {
                return string.Format(TableNameConstants.Formattable.Adjustments.CLASSSpellLevels, CharacterClassConstants.Domains.Animal);
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SpellConstants.CalmAnimals,
                SpellConstants.HoldAnimal,
                SpellConstants.DominateAnimal,
                SpellConstants.SummonNaturesAllyIV,
                SpellConstants.CommuneWithNature,
                SpellConstants.AntilifeShell,
                SpellConstants.AnimalShapes,
                SpellConstants.SummonNaturesAllyVIII,
                SpellConstants.Shapechange
            };

            AssertCollectionNames(names);
        }

        [Test]
        public void AllAnimalSpellsInAdjustmentsTable()
        {
            var spellGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.SpellGroups);
            AssertCollectionNames(spellGroups[CharacterClassConstants.Domains.Animal]);
        }

        [TestCase(SpellConstants.CalmAnimals, 1)]
        [TestCase(SpellConstants.HoldAnimal, 2)]
        [TestCase(SpellConstants.DominateAnimal, 3)]
        [TestCase(SpellConstants.SummonNaturesAllyIV, 4)]
        [TestCase(SpellConstants.CommuneWithNature, 5)]
        [TestCase(SpellConstants.AntilifeShell, 6)]
        [TestCase(SpellConstants.AnimalShapes, 7)]
        [TestCase(SpellConstants.SummonNaturesAllyVIII, 8)]
        [TestCase(SpellConstants.Shapechange, 9)]
        public override void Adjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}
