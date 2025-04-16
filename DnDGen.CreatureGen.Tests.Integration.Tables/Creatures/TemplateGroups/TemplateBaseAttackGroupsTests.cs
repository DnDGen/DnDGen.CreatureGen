using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.TemplateGroups
{
    [TestFixture]
    public class TemplateBaseAttackGroupsTests : TemplateGroupsTableTests
    {
        [Test]
        public void TemplateGroupNames()
        {
            AssertTemplateGroupNamesAreComplete();
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
        public void TemplateBaseAttackGroup(string baseAttack, params string[] group)
        {
            AssertTemplateGroup(baseAttack, group);
        }
    }
}
