using CreatureGen.Creatures;
using CreatureGen.Tables;
using CreatureGen.Tests.Integration.Tables.TestData;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Defenses
{
    [TestFixture]
    public class HitDiceTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.HitDice; }
        }

        [Test]
        public void CollectionNames()
        {
            var creatures = CreatureConstants.All();
            var types = CreatureConstants.Types.All();

            var names = creatures.Union(types);

            AssertCollectionNames(names);
        }

        [TestCase(CreatureConstants.Aasimar, 1)]
        [TestCase(CreatureConstants.Aboleth, 8)]
        [TestCase(CreatureConstants.Achaierai, 6)]
        [TestCase(CreatureConstants.Allip, 4)]
        [TestCase(CreatureConstants.Angel_AstralDeva, 12)]
        [TestCase(CreatureConstants.Angel_Planetar, 14)]
        [TestCase(CreatureConstants.Angel_Solar, 22)]
        [TestCase(CreatureConstants.Ankheg, 3)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal, 32)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, 16)]
        [TestCase(CreatureConstants.AnimatedObject_Huge, 8)]
        [TestCase(CreatureConstants.AnimatedObject_Large, 4)]
        [TestCase(CreatureConstants.AnimatedObject_Medium, 2)]
        [TestCase(CreatureConstants.AnimatedObject_Small, 1)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny, .5)]
        [TestCase(CreatureConstants.Annis, 7)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, 7)]
        [TestCase(CreatureConstants.Arrowhawk_Elder, 15)]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, 3)]
        [TestCase(CreatureConstants.Androsphinx, 12)]
        [TestCase(CreatureConstants.Basilisk, 6)]
        [TestCase(CreatureConstants.Basilisk_AbyssalGreater, 18)]
        [TestCase(CreatureConstants.Criosphinx, 10)]
        [TestCase(CreatureConstants.Dwarf_Deep, 1)]
        [TestCase(CreatureConstants.Dwarf_Duergar, 1)]
        [TestCase(CreatureConstants.Dwarf_Hill, 1)]
        [TestCase(CreatureConstants.Dwarf_Mountain, 1)]
        [TestCase(CreatureConstants.Elemental_Air_Elder, 24)]
        [TestCase(CreatureConstants.Elemental_Air_Greater, 21)]
        [TestCase(CreatureConstants.Elemental_Air_Huge, 16)]
        [TestCase(CreatureConstants.Elemental_Air_Large, 8)]
        [TestCase(CreatureConstants.Elemental_Air_Medium, 4)]
        [TestCase(CreatureConstants.Elemental_Air_Small, 2)]
        [TestCase(CreatureConstants.Elemental_Earth_Elder, 24)]
        [TestCase(CreatureConstants.Elemental_Earth_Greater, 21)]
        [TestCase(CreatureConstants.Elemental_Earth_Huge, 16)]
        [TestCase(CreatureConstants.Elemental_Earth_Large, 8)]
        [TestCase(CreatureConstants.Elemental_Earth_Medium, 4)]
        [TestCase(CreatureConstants.Elemental_Earth_Small, 2)]
        [TestCase(CreatureConstants.Elemental_Fire_Elder, 24)]
        [TestCase(CreatureConstants.Elemental_Fire_Greater, 21)]
        [TestCase(CreatureConstants.Elemental_Fire_Huge, 16)]
        [TestCase(CreatureConstants.Elemental_Fire_Large, 8)]
        [TestCase(CreatureConstants.Elemental_Fire_Medium, 4)]
        [TestCase(CreatureConstants.Elemental_Fire_Small, 2)]
        [TestCase(CreatureConstants.Elemental_Water_Elder, 24)]
        [TestCase(CreatureConstants.Elemental_Water_Greater, 21)]
        [TestCase(CreatureConstants.Elemental_Water_Huge, 16)]
        [TestCase(CreatureConstants.Elemental_Water_Large, 8)]
        [TestCase(CreatureConstants.Elemental_Water_Medium, 4)]
        [TestCase(CreatureConstants.Elemental_Water_Small, 2)]
        [TestCase(CreatureConstants.Elf_Aquatic, 1)]
        [TestCase(CreatureConstants.Elf_Drow, 1)]
        [TestCase(CreatureConstants.Elf_Gray, 1)]
        [TestCase(CreatureConstants.Elf_Half, 1)]
        [TestCase(CreatureConstants.Elf_High, 1)]
        [TestCase(CreatureConstants.Elf_Wild, 1)]
        [TestCase(CreatureConstants.Elf_Wood, 1)]
        [TestCase(CreatureConstants.GreenHag, 9)]
        [TestCase(CreatureConstants.Gynosphinx, 8)]
        [TestCase(CreatureConstants.Halfling_Deep, 1)]
        [TestCase(CreatureConstants.Halfling_Lightfoot, 1)]
        [TestCase(CreatureConstants.Halfling_Tallfellow, 1)]
        [TestCase(CreatureConstants.Hieracosphinx, 9)]
        [TestCase(CreatureConstants.Human, 1)]
        [TestCase(CreatureConstants.Goblin, 1)]
        [TestCase(CreatureConstants.Gnome_Forest, 1)]
        [TestCase(CreatureConstants.Gnome_Rock, 1)]
        [TestCase(CreatureConstants.Gnome_Svirfneblin, 1)]
        [TestCase(CreatureConstants.Mephit_Air, 3)]
        [TestCase(CreatureConstants.Mephit_Dust, 3)]
        [TestCase(CreatureConstants.Mephit_Earth, 3)]
        [TestCase(CreatureConstants.Mephit_Fire, 3)]
        [TestCase(CreatureConstants.Mephit_Ice, 3)]
        [TestCase(CreatureConstants.Mephit_Magma, 3)]
        [TestCase(CreatureConstants.Mephit_Ooze, 3)]
        [TestCase(CreatureConstants.Mephit_Salt, 3)]
        [TestCase(CreatureConstants.Mephit_Steam, 3)]
        [TestCase(CreatureConstants.Mephit_Water, 3)]
        [TestCase(CreatureConstants.Orc, 1)]
        [TestCase(CreatureConstants.Orc_Half, 1)]
        [TestCase(CreatureConstants.SeaHag, 3)]
        [TestCase(CreatureConstants.Tiefling, 1)]
        public void HitDiceQuantity(string creature, double quantity)
        {
            AssertAdjustment(creature, quantity);
        }

        [TestCase(CreatureConstants.Types.Aberration, 8)]
        [TestCase(CreatureConstants.Types.Animal, 8)]
        [TestCase(CreatureConstants.Types.Construct, 10)]
        [TestCase(CreatureConstants.Types.Dragon, 12)]
        [TestCase(CreatureConstants.Types.Elemental, 8)]
        [TestCase(CreatureConstants.Types.Fey, 6)]
        [TestCase(CreatureConstants.Types.Giant, 8)]
        [TestCase(CreatureConstants.Types.Humanoid, 8)]
        [TestCase(CreatureConstants.Types.MagicalBeast, 10)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, 8)]
        [TestCase(CreatureConstants.Types.Ooze, 10)]
        [TestCase(CreatureConstants.Types.Outsider, 8)]
        [TestCase(CreatureConstants.Types.Plant, 8)]
        [TestCase(CreatureConstants.Types.Undead, 12)]
        [TestCase(CreatureConstants.Types.Vermin, 8)]
        public void HitDie(string creatureType, int quantity)
        {
            AssertAdjustment(creatureType, quantity);
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void PositiveHitDiceQuantity(string creature)
        {
            var hitDiceQuantity = GetAdjustment(creature);
            Assert.That(hitDiceQuantity, Is.Positive);
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
        [TestCase(CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void CreatureTypeHasValidHitDie(string creatureType)
        {
            var validDie = new[] { 2, 3, 4, 6, 8, 10, 12, 20, 100 };

            var hitDie = GetAdjustment(creatureType);
            Assert.That(hitDie, Is.Positive);
            Assert.That(validDie, Contains.Item(hitDie));
        }
    }
}
