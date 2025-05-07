using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.TemplateGroups
{
    [TestFixture]
    public class TemplateCreatureTypeGroupsTests : TemplateGroupsTableTests
    {
        [Test]
        public void TemplateGroupNames()
        {
            AssertTemplateGroupNamesAreComplete();
        }

        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Dragon,
            CreatureConstants.Groups.HalfDragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Undead,
            CreatureConstants.Templates.Ghost,
            CreatureConstants.Templates.Lich,
            CreatureConstants.Templates.Skeleton,
            CreatureConstants.Templates.Vampire,
            CreatureConstants.Templates.Zombie)]
        [TestCase(CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Types.Subtypes.Angel)]
        [TestCase(CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Types.Subtypes.Archon)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented,
            CreatureConstants.Templates.CelestialCreature,
            CreatureConstants.Templates.FiendishCreature,
            CreatureConstants.Templates.Ghost,
            CreatureConstants.Templates.HalfCelestial,
            CreatureConstants.Templates.HalfFiend,
            CreatureConstants.Groups.HalfDragon,
            CreatureConstants.Templates.Lich,
            CreatureConstants.Templates.Vampire)]
        [TestCase(CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Types.Subtypes.Dwarf)]
        [TestCase(CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Types.Subtypes.Evil)]
        [TestCase(CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnoll)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnome)]
        [TestCase(CreatureConstants.Types.Subtypes.Goblinoid)]
        [TestCase(CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Types.Subtypes.Halfling)]
        [TestCase(CreatureConstants.Types.Subtypes.Human)]
        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal)]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Types.Subtypes.Native)]
        [TestCase(CreatureConstants.Types.Subtypes.Orc)]
        [TestCase(CreatureConstants.Types.Subtypes.Reptilian)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger,
            CreatureConstants.Groups.Lycanthrope)]
        [TestCase(CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Types.Subtypes.Water)]
        public void TemplateCreatureTypeGroup(string name, params string[] entries)
        {
            AssertTemplateGroup(name, entries);
        }

        [Test]
        public void AberrationGroup()
        {
            AssertTemplateGroup(CreatureConstants.Types.Aberration, []);
        }

        [Test]
        public void AnimalGroup()
        {
            AssertTemplateGroup(CreatureConstants.Types.Animal, []);
        }

        [Test]
        public void ExtraplanarGroup()
        {
            AssertTemplateGroup(CreatureConstants.Types.Subtypes.Extraplanar, [CreatureConstants.Templates.CelestialCreature, CreatureConstants.Templates.FiendishCreature]);
        }

        [Test]
        public void MagicalBeastsGroup()
        {
            AssertTemplateGroup(CreatureConstants.Types.MagicalBeast, []);
        }

        [Test]
        public void OutsiderGroup()
        {
            AssertTemplateGroup(CreatureConstants.Types.Outsider, [CreatureConstants.Templates.HalfCelestial, CreatureConstants.Templates.HalfFiend]);
        }
    }
}