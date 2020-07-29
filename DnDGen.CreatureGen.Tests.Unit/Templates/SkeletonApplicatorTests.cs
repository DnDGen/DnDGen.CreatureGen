using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Templates;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class SkeletonApplicatorTests
    {
        private TemplateApplicator applicator;

        [SetUp]
        public void Setup()
        {
            applicator = new SkeletonApplicator();
        }

        [TestCase(CreatureConstants.Types.Aberration, true)]
        [TestCase(CreatureConstants.Types.Animal, true)]
        [TestCase(CreatureConstants.Types.Construct, true)]
        [TestCase(CreatureConstants.Types.Dragon, true)]
        [TestCase(CreatureConstants.Types.Elemental, true)]
        [TestCase(CreatureConstants.Types.Fey, true)]
        [TestCase(CreatureConstants.Types.Giant, true)]
        [TestCase(CreatureConstants.Types.Humanoid, true)]
        [TestCase(CreatureConstants.Types.MagicalBeast, true)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, true)]
        [TestCase(CreatureConstants.Types.Ooze, true)]
        [TestCase(CreatureConstants.Types.Outsider, false)]
        [TestCase(CreatureConstants.Types.Plant, true)]
        [TestCase(CreatureConstants.Types.Undead, false)]
        [TestCase(CreatureConstants.Types.Vermin, true)]
        public void IsCompatible_ByCreatureType(string creatureType)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_CannotBeIncorporeal()
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public void IsCompatible_MustHaveSkeleton()
        {
            Assert.Fail("not yet written");
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void ApplyTo_ChangeCreatureType(string original)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented)]
        [TestCase(CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Types.Subtypes.Dwarf)]
        [TestCase(CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnoll)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnome)]
        [TestCase(CreatureConstants.Types.Subtypes.Goblinoid)]
        [TestCase(CreatureConstants.Types.Subtypes.Halfling)]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Types.Subtypes.Orc)]
        [TestCase(CreatureConstants.Types.Subtypes.Reptilian)]
        [TestCase(CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void ApplyTo_KeepSubtype(string subtype)
        {
            Assert.Fail("not yet written");
        }

        [TestCase(CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Types.Subtypes.Evil)]
        [TestCase(CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger)]
        public void ApplyTo_LoseSubtype(string subtype)
        {
            Assert.Fail("not yet written");
        }

        [Test]
        public async Task ApplyToAsync_()
        {
            Assert.Fail("need to copy");
        }
    }
}
