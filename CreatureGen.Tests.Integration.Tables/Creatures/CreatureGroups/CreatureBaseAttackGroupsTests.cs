using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureBaseAttackGroupsTests : CreatureGroupsTableTests
    {
        [Test]
        public void CreatureGroupNames()
        {
            AssertCreatureGroupNamesAreComplete();
        }

        [TestCase(GroupConstants.GoodBaseAttack,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Outsider)]
        [TestCase(GroupConstants.AverageBaseAttack,
            CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Construct,
            CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Giant,
            CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Ooze,
            CreatureConstants.Types.Plant,
            CreatureConstants.Types.Vermin)]
        [TestCase(GroupConstants.PoorBaseAttack,
            CreatureConstants.Types.Fey,
            CreatureConstants.Types.Undead)]
        public void CreatureBaseAttackGroup(string baseAttack, params string[] group)
        {
            AssertDistinctCollection(baseAttack, group);
        }
    }
}
