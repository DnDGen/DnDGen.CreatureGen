using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Tables;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public abstract class CreatureGroupsTableTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.CreatureGroups;

        protected void AssertCreatureGroupNamesAreComplete()
        {
            var types = CreatureConstants.Types.GetAll();
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var challengeRatings = ChallengeRatingConstants.GetOrdered();

            var entries = new[]
            {
                CreatureConstants.Groups.Angel,
                CreatureConstants.Groups.AnimatedObject,
                CreatureConstants.Groups.Ant_Giant,
                CreatureConstants.Groups.Archon,
                CreatureConstants.Groups.Arrowhawk,
                CreatureConstants.Groups.Bear,
                CreatureConstants.Groups.Centipede_Monstrous,
                CreatureConstants.Groups.Chimera,
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
                CreatureConstants.Groups.HalfDragon,
                CreatureConstants.Groups.Halfling,
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
                SaveConstants.Fortitude,
                SaveConstants.Reflex,
                SaveConstants.Will,
                SaveConstants.Fortitude + GroupConstants.TREE,
                SaveConstants.Reflex + GroupConstants.TREE,
                SaveConstants.Will + GroupConstants.TREE,
                GroupConstants.GoodBaseAttack,
                GroupConstants.AverageBaseAttack,
                GroupConstants.PoorBaseAttack,
                GroupConstants.GoodBaseAttack + GroupConstants.TREE,
                GroupConstants.AverageBaseAttack + GroupConstants.TREE,
                GroupConstants.PoorBaseAttack + GroupConstants.TREE,
                GroupConstants.All,
                GroupConstants.All + GroupConstants.TREE,
                GroupConstants.Characters,
                GroupConstants.Characters + GroupConstants.TREE,
                GroupConstants.Templates,
                GroupConstants.Templates + GroupConstants.TREE,
                GroupConstants.HasSkeleton,
                GroupConstants.HasSkeleton + GroupConstants.TREE,
            };

            var alignments = new[]
            {
                AlignmentConstants.LawfulGood,
                AlignmentConstants.LawfulNeutral,
                AlignmentConstants.LawfulEvil,
                AlignmentConstants.NeutralGood,
                AlignmentConstants.TrueNeutral,
                AlignmentConstants.NeutralEvil,
                AlignmentConstants.ChaoticGood,
                AlignmentConstants.ChaoticNeutral,
                AlignmentConstants.ChaoticEvil,
            };

            var names = entries
                .Union(types)
                .Union(types.Select(t => t + GroupConstants.TREE))
                .Union(subtypes)
                .Union(subtypes.Select(st => st + GroupConstants.TREE))
                .Union(challengeRatings)
                .Union(challengeRatings.Select(cr => cr + GroupConstants.TREE))
                .Union(alignments)
                .Union(alignments.Select(a => a + GroupConstants.TREE));

            AssertCollectionNames(names);
        }

        protected void AssertCreatureGroup(string name, IEnumerable<string> entries)
        {
            AssertDistinctCollection(name + GroupConstants.TREE, [.. entries]);

            var exploded = ExplodeCollection(tableName, name + GroupConstants.TREE);
            AssertDistinctCollection(name, [.. exploded]);
        }
    }
}
