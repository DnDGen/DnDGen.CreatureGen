using CreatureGen.Alignments;
using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Races.Metaraces
{
    [TestFixture]
    public class MetaraceGroupsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.MetaraceGroups; }
        }

        [Test]
        public override void CollectionNames()
        {
            var alignmentGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.AlignmentGroups);
            var classGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);

            var names = new[]
            {
                GroupConstants.Genetic,
                GroupConstants.Lycanthrope,
                GroupConstants.Undead,
                GroupConstants.All,
                GroupConstants.HasWings,
            };

            names = names.Union(alignmentGroups[GroupConstants.All]).Union(classGroups[GroupConstants.All]).ToArray();

            AssertCollectionNames(names);
        }

        [TestCase(AlignmentConstants.ChaoticEvil,
            SizeConstants.Metaraces.Ghost,
            SizeConstants.Metaraces.HalfDragon,
            SizeConstants.Metaraces.HalfFiend,
            SizeConstants.Metaraces.Lich,
            SizeConstants.Metaraces.None,
            SizeConstants.Metaraces.Vampire,
            SizeConstants.Metaraces.Werewolf)]
        [TestCase(AlignmentConstants.ChaoticGood,
            SizeConstants.Metaraces.Ghost,
            SizeConstants.Metaraces.HalfDragon,
            SizeConstants.Metaraces.HalfCelestial,
            SizeConstants.Metaraces.None)]
        [TestCase(AlignmentConstants.ChaoticNeutral,
            SizeConstants.Metaraces.Ghost,
            SizeConstants.Metaraces.None)]
        [TestCase(AlignmentConstants.LawfulGood,
            SizeConstants.Metaraces.Ghost,
            SizeConstants.Metaraces.HalfDragon,
            SizeConstants.Metaraces.HalfCelestial,
            SizeConstants.Metaraces.None,
            SizeConstants.Metaraces.Werebear)]
        [TestCase(AlignmentConstants.LawfulEvil,
            SizeConstants.Metaraces.Ghost,
            SizeConstants.Metaraces.HalfDragon,
            SizeConstants.Metaraces.HalfFiend,
            SizeConstants.Metaraces.Lich,
            SizeConstants.Metaraces.Mummy,
            SizeConstants.Metaraces.None,
            SizeConstants.Metaraces.Vampire,
            SizeConstants.Metaraces.Wererat)]
        [TestCase(AlignmentConstants.LawfulNeutral,
            SizeConstants.Metaraces.Ghost,
            SizeConstants.Metaraces.Mummy,
            SizeConstants.Metaraces.None)]
        [TestCase(AlignmentConstants.NeutralEvil,
            SizeConstants.Metaraces.Ghost,
            SizeConstants.Metaraces.HalfDragon,
            SizeConstants.Metaraces.HalfFiend,
            SizeConstants.Metaraces.Lich,
            SizeConstants.Metaraces.Mummy,
            SizeConstants.Metaraces.None,
            SizeConstants.Metaraces.Vampire)]
        [TestCase(AlignmentConstants.NeutralGood,
            SizeConstants.Metaraces.Ghost,
            SizeConstants.Metaraces.HalfDragon,
            SizeConstants.Metaraces.HalfCelestial,
            SizeConstants.Metaraces.None)]
        [TestCase(AlignmentConstants.TrueNeutral,
            SizeConstants.Metaraces.Ghost,
            SizeConstants.Metaraces.None,
            SizeConstants.Metaraces.Wereboar,
            SizeConstants.Metaraces.Weretiger)]
        [TestCase(GroupConstants.Genetic,
            SizeConstants.Metaraces.HalfDragon,
            SizeConstants.Metaraces.HalfFiend,
            SizeConstants.Metaraces.HalfCelestial)]
        [TestCase(GroupConstants.Lycanthrope,
            SizeConstants.Metaraces.Werebear,
            SizeConstants.Metaraces.Wereboar,
            SizeConstants.Metaraces.Weretiger,
            SizeConstants.Metaraces.Wererat,
            SizeConstants.Metaraces.Werewolf)]
        [TestCase(GroupConstants.Undead,
            SizeConstants.Metaraces.Ghost,
            SizeConstants.Metaraces.Lich,
            SizeConstants.Metaraces.Mummy,
            SizeConstants.Metaraces.Vampire)]
        [TestCase(GroupConstants.HasWings,
            SizeConstants.Metaraces.HalfFiend,
            SizeConstants.Metaraces.HalfCelestial)]
        public void MetaraceGroup(string name, params string[] collection)
        {
            base.DistinctCollection(name, collection);
        }

        [Test]
        public void PaladinMetaraces()
        {
            var metaraces = new[]
            {
                SizeConstants.Metaraces.HalfCelestial,
                SizeConstants.Metaraces.HalfDragon,
                SizeConstants.Metaraces.Ghost,
                SizeConstants.Metaraces.None,
                SizeConstants.Metaraces.Werebear,
            };

            base.DistinctCollection(CharacterClassConstants.Paladin, metaraces);
        }

        [Test]
        public void AllMetaraces()
        {
            var metaraces = new[]
            {
                SizeConstants.Metaraces.Ghost,
                SizeConstants.Metaraces.HalfCelestial,
                SizeConstants.Metaraces.HalfDragon,
                SizeConstants.Metaraces.HalfFiend,
                SizeConstants.Metaraces.Lich,
                SizeConstants.Metaraces.Mummy,
                SizeConstants.Metaraces.None,
                SizeConstants.Metaraces.Vampire,
                SizeConstants.Metaraces.Werebear,
                SizeConstants.Metaraces.Wereboar,
                SizeConstants.Metaraces.Weretiger,
                SizeConstants.Metaraces.Wererat,
                SizeConstants.Metaraces.Werewolf,
            };

            base.DistinctCollection(GroupConstants.All, metaraces);
        }

        [TestCase(CharacterClassConstants.Adept)]
        [TestCase(CharacterClassConstants.Bard)]
        [TestCase(CharacterClassConstants.Cleric)]
        [TestCase(CharacterClassConstants.Druid)]
        [TestCase(CharacterClassConstants.Ranger)]
        [TestCase(CharacterClassConstants.Sorcerer)]
        [TestCase(CharacterClassConstants.Wizard)]
        public void SpellcasterMetarace(string className)
        {
            var metaraces = new[]
            {
                SizeConstants.Metaraces.Ghost,
                SizeConstants.Metaraces.HalfDragon,
                SizeConstants.Metaraces.HalfFiend,
                SizeConstants.Metaraces.HalfCelestial,
                SizeConstants.Metaraces.Lich,
                SizeConstants.Metaraces.Mummy,
                SizeConstants.Metaraces.None,
                SizeConstants.Metaraces.Vampire,
                SizeConstants.Metaraces.Werebear,
                SizeConstants.Metaraces.Wereboar,
                SizeConstants.Metaraces.Weretiger,
                SizeConstants.Metaraces.Wererat,
                SizeConstants.Metaraces.Werewolf,
            };

            base.DistinctCollection(className, metaraces);
        }

        [TestCase(CharacterClassConstants.Aristocrat)]
        [TestCase(CharacterClassConstants.Barbarian)]
        [TestCase(CharacterClassConstants.Commoner)]
        [TestCase(CharacterClassConstants.Expert)]
        [TestCase(CharacterClassConstants.Fighter)]
        [TestCase(CharacterClassConstants.Monk)]
        [TestCase(CharacterClassConstants.Rogue)]
        [TestCase(CharacterClassConstants.Warrior)]
        public void NonSpellcasterMetarace(string className)
        {
            var metaraces = new[]
            {
                SizeConstants.Metaraces.Ghost,
                SizeConstants.Metaraces.HalfDragon,
                SizeConstants.Metaraces.HalfFiend,
                SizeConstants.Metaraces.HalfCelestial,
                SizeConstants.Metaraces.Mummy,
                SizeConstants.Metaraces.None,
                SizeConstants.Metaraces.Vampire,
                SizeConstants.Metaraces.Werebear,
                SizeConstants.Metaraces.Wereboar,
                SizeConstants.Metaraces.Weretiger,
                SizeConstants.Metaraces.Wererat,
                SizeConstants.Metaraces.Werewolf,
            };

            base.DistinctCollection(className, metaraces);
        }

        [Test]
        public void AllMetaracesHaveFullAlignmentGroup()
        {
            var alignmentGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.AlignmentGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);
            var alignmentMetaraces = metaraceGroups
                .Where(kvp => alignmentGroups[GroupConstants.All].Contains(kvp.Key)) //Get alignment-key metarace groups
                .SelectMany(kvp => kvp.Value) //get metaraces in those groups
                .Distinct();

            AssertCollection(alignmentMetaraces, metaraceGroups[GroupConstants.All]);
        }

        [Test]
        public void AllMetaracesHaveClassNameGroup()
        {
            var classGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);
            var classMetaraces = metaraceGroups
                .Where(kvp => classGroups[GroupConstants.All].Contains(kvp.Key)) //Get class-key metarace groups
                .SelectMany(kvp => kvp.Value) //get metaraces in those groups
                .Distinct();

            AssertCollection(classMetaraces, metaraceGroups[GroupConstants.All]);
        }
    }
}