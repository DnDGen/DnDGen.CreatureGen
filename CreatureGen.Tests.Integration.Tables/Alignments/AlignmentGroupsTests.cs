using CreatureGen.Alignments;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Alignments
{
    [TestFixture]
    public class AlignmentGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.AlignmentGroups;
            }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                AlignmentConstants.LawfulGood,
                AlignmentConstants.NeutralGood,
                AlignmentConstants.ChaoticGood,
                AlignmentConstants.LawfulNeutral,
                AlignmentConstants.TrueNeutral,
                AlignmentConstants.ChaoticNeutral,
                AlignmentConstants.LawfulEvil,
                AlignmentConstants.NeutralEvil,
                AlignmentConstants.ChaoticEvil,
                GroupConstants.All,
            };

            AssertCollectionNames(names);
        }

        [TestCase(AlignmentConstants.LawfulGood,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.TrueNeutral)]
        [TestCase(AlignmentConstants.NeutralGood,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.ChaoticGood,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.TrueNeutral)]
        [TestCase(AlignmentConstants.LawfulNeutral,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.TrueNeutral,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.LawfulEvil,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.TrueNeutral)]
        [TestCase(AlignmentConstants.NeutralEvil,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.ChaoticEvil,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.ChaoticEvil,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.ChaoticEvil,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.TrueNeutral)]
        [TestCase(GroupConstants.All,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.ChaoticEvil)]
        public void AlignmentGroup(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }
    }
}
