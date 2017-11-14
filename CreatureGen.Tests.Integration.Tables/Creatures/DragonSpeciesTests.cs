using CreatureGen.Alignments;
using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.Metaraces
{
    [TestFixture]
    public class DragonSpeciesTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.DragonSpecies; }
        }

        [Test]
        public void CollectionNames()
        {
            var names = new[]
            {
                AlignmentConstants.LawfulGood,
                AlignmentConstants.NeutralGood,
                AlignmentConstants.ChaoticGood,
                AlignmentConstants.LawfulEvil,
                AlignmentConstants.NeutralEvil,
                AlignmentConstants.ChaoticEvil
            };

            AssertCollectionNames(names);
        }

        [TestCase(AlignmentConstants.LawfulGood,
            CreatureConstants.Templates.Species.Bronze,
            CreatureConstants.Templates.Species.Gold,
            CreatureConstants.Templates.Species.Silver)]
        [TestCase(AlignmentConstants.NeutralGood,
            CreatureConstants.Templates.Species.Bronze,
            CreatureConstants.Templates.Species.Gold,
            CreatureConstants.Templates.Species.Silver,
            CreatureConstants.Templates.Species.Brass,
            CreatureConstants.Templates.Species.Copper)]
        [TestCase(AlignmentConstants.ChaoticGood,
            CreatureConstants.Templates.Species.Brass,
            CreatureConstants.Templates.Species.Copper)]
        [TestCase(AlignmentConstants.LawfulEvil,
            CreatureConstants.Templates.Species.Blue,
            CreatureConstants.Templates.Species.Green)]
        [TestCase(AlignmentConstants.NeutralEvil,
            CreatureConstants.Templates.Species.Blue,
            CreatureConstants.Templates.Species.Green,
            CreatureConstants.Templates.Species.Black,
            CreatureConstants.Templates.Species.Red,
            CreatureConstants.Templates.Species.White)]
        [TestCase(AlignmentConstants.ChaoticEvil,
            CreatureConstants.Templates.Species.Black,
            CreatureConstants.Templates.Species.Red,
            CreatureConstants.Templates.Species.White)]
        public void DragonSpecies(string name, params string[] collection)
        {
            DistinctCollection(name, collection);
        }
    }
}