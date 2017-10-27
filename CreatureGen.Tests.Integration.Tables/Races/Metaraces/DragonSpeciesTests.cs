using CreatureGen.Alignments;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
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
        public override void CollectionNames()
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
            SizeConstants.Metaraces.Species.Bronze,
            SizeConstants.Metaraces.Species.Gold,
            SizeConstants.Metaraces.Species.Silver)]
        [TestCase(AlignmentConstants.NeutralGood,
            SizeConstants.Metaraces.Species.Bronze,
            SizeConstants.Metaraces.Species.Gold,
            SizeConstants.Metaraces.Species.Silver,
            SizeConstants.Metaraces.Species.Brass,
            SizeConstants.Metaraces.Species.Copper)]
        [TestCase(AlignmentConstants.ChaoticGood,
            SizeConstants.Metaraces.Species.Brass,
            SizeConstants.Metaraces.Species.Copper)]
        [TestCase(AlignmentConstants.LawfulEvil,
            SizeConstants.Metaraces.Species.Blue,
            SizeConstants.Metaraces.Species.Green)]
        [TestCase(AlignmentConstants.NeutralEvil,
            SizeConstants.Metaraces.Species.Blue,
            SizeConstants.Metaraces.Species.Green,
            SizeConstants.Metaraces.Species.Black,
            SizeConstants.Metaraces.Species.Red,
            SizeConstants.Metaraces.Species.White)]
        [TestCase(AlignmentConstants.ChaoticEvil,
            SizeConstants.Metaraces.Species.Black,
            SizeConstants.Metaraces.Species.Red,
            SizeConstants.Metaraces.Species.White)]
        public override void DistinctCollection(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}