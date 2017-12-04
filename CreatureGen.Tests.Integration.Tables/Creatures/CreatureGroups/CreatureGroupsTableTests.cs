using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public abstract class CreatureGroupsTableTests : CollectionTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.CreatureGroups;
            }
        }

        protected void AssertCreatureGroupEntriesAreComplete()
        {
            var templates = CreatureConstants.Templates.All();

            var entries = new[]
            {
                CreatureConstants.Groups.Angel,
                CreatureConstants.Groups.AnimatedObject,
                CreatureConstants.Groups.Ant_Giant,
                CreatureConstants.Groups.Archon,
                CreatureConstants.Groups.Arrowhawk,
                CreatureConstants.Groups.Bear,
                CreatureConstants.Groups.Centipede_Monstrous,
                CreatureConstants.Groups.Cryohydra,
                CreatureConstants.Groups.Demon,
                CreatureConstants.Groups.Devil,
                CreatureConstants.Groups.Dinosaur,
                CreatureConstants.Groups.Dragon_Black,
                CreatureConstants.Groups.Dragon_Blue,
                CreatureConstants.Groups.Dragon_Brass,
                CreatureConstants.Groups.Dragon_Bronze,
                CreatureConstants.Groups.Dragon_Copper,
                CreatureConstants.Groups.Dragon_Gold,
                CreatureConstants.Groups.Dragon_Green,
                CreatureConstants.Groups.Dragon_Red,
                CreatureConstants.Groups.Dragon_Silver,
                CreatureConstants.Groups.Dragon_White,
                CreatureConstants.Groups.Dwarf,
                CreatureConstants.Groups.Elemental_Air,
                CreatureConstants.Groups.Elemental_Earth,
                CreatureConstants.Groups.Elemental_Fire,
                CreatureConstants.Groups.Elemental_Water,
                CreatureConstants.Groups.Elf,
                CreatureConstants.Groups.Formian,
                CreatureConstants.Groups.Fungus,
                CreatureConstants.Groups.Genie,
                CreatureConstants.Groups.Gnome,
                CreatureConstants.Groups.Golem,
                CreatureConstants.Groups.Hag,
                CreatureConstants.Groups.Halfling,
                CreatureConstants.Groups.HasSkeleton,
                CreatureConstants.Groups.Horse,
                CreatureConstants.Groups.Hydra,
                CreatureConstants.Groups.Inevitable,
                CreatureConstants.Groups.Lycanthrope,
                CreatureConstants.Groups.Mephit,
                CreatureConstants.Groups.Naga,
                CreatureConstants.Groups.Nightshade,
                CreatureConstants.Groups.Planetouched,
                CreatureConstants.Groups.Pyrohydra,
                CreatureConstants.Groups.Salamander,
                CreatureConstants.Groups.Scorpion_Monstrous,
                CreatureConstants.Groups.Shark,
                CreatureConstants.Groups.Slaad,
                CreatureConstants.Groups.Snake_Viper,
                CreatureConstants.Groups.Sphinx,
                CreatureConstants.Groups.Spider_Monstrous,
                CreatureConstants.Groups.Sprite,
                CreatureConstants.Groups.Tojanida,
                CreatureConstants.Groups.Whale,
                CreatureConstants.Groups.Xorn,
                CreatureConstants.Groups.YuanTi,
                CreatureConstants.Types.Aberration,
                CreatureConstants.Types.Animal,
                CreatureConstants.Types.Construct,
                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Elemental,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MagicalBeast,
                CreatureConstants.Types.MonstrousHumanoid,
                CreatureConstants.Types.Ooze,
                CreatureConstants.Types.Outsider,
                CreatureConstants.Types.Plant,
                CreatureConstants.Types.Undead,
                CreatureConstants.Types.Vermin,
            };

            var names = entries.Union(templates);

            AssertCollectionNames(names);
        }
    }
}
