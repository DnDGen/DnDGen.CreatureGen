using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Ages.Rolls
{
    [TestFixture]
    public class MaximumAgeRollsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.MaximumAgeRolls; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, "2d20")]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, "4d100")]
        [TestCase(SizeConstants.BaseRaces.Azer, "2d100")]
        [TestCase(SizeConstants.BaseRaces.Bugbear, "2d10")]
        [TestCase(SizeConstants.BaseRaces.Centaur, "2d12")]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, "10d10")]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, "2d100")]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, "5d20")]
        [TestCase(SizeConstants.BaseRaces.Derro, "2d100")]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, "2d20")]
        [TestCase(SizeConstants.BaseRaces.Drow, "4d100")]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, "2d100")]
        [TestCase(SizeConstants.BaseRaces.FireGiant, "9d10")]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, "3d100")]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, "6d10")]
        [TestCase(SizeConstants.BaseRaces.Githyanki, "50")]
        [TestCase(SizeConstants.BaseRaces.Githzerai, "50")]
        [TestCase(SizeConstants.BaseRaces.Gnoll, "2d10")]
        [TestCase(SizeConstants.BaseRaces.Goblin, "1d20")]
        [TestCase(SizeConstants.BaseRaces.GrayElf, "4d100")]
        [TestCase(SizeConstants.BaseRaces.Grimlock, "2d20")]
        [TestCase(SizeConstants.BaseRaces.HalfElf, "3d20")]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, "2d10")]
        [TestCase(SizeConstants.BaseRaces.Harpy, "3d6")]
        [TestCase(SizeConstants.BaseRaces.HighElf, "4d100")]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, "2d100")]
        [TestCase(SizeConstants.BaseRaces.HillGiant, "5d10")]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, "2d10")]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, "7d6")]
        [TestCase(SizeConstants.BaseRaces.Human, "2d20")]
        [TestCase(SizeConstants.BaseRaces.Janni, "2d20")]
        [TestCase(SizeConstants.BaseRaces.Kobold, "1d20")]
        [TestCase(SizeConstants.BaseRaces.KuoToa, "2d10")]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, "5d20")]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, "2d20")]
        [TestCase(SizeConstants.BaseRaces.Locathah, "2d20")]
        [TestCase(SizeConstants.BaseRaces.Merfolk, "3d10")]
        [TestCase(SizeConstants.BaseRaces.Merrow, "3d20")]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, "3d10")]
        [TestCase(SizeConstants.BaseRaces.Minotaur, "2d10")]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, "2d100")]
        [TestCase(SizeConstants.BaseRaces.Ogre, "3d20")]
        [TestCase(SizeConstants.BaseRaces.OgreMage, "3d20")]
        [TestCase(SizeConstants.BaseRaces.Orc, "1d20")]
        [TestCase(SizeConstants.BaseRaces.Pixie, "10d100")]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, "2d20")]
        [TestCase(SizeConstants.BaseRaces.RockGnome, "3d100")]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, "3d20")]
        [TestCase(SizeConstants.BaseRaces.Satyr, "4d12")]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, "2d10")]
        [TestCase(SizeConstants.BaseRaces.Scrag, "2d20")]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, "2d100")]
        [TestCase(SizeConstants.BaseRaces.StormGiant, "15d10")]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, "3d100")]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, "5d20")]
        [TestCase(SizeConstants.BaseRaces.Tiefling, "2d20")]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, "1d20")]
        [TestCase(SizeConstants.BaseRaces.Troll, "2d20")]
        [TestCase(SizeConstants.BaseRaces.WildElf, "4d100")]
        [TestCase(SizeConstants.BaseRaces.WoodElf, "4d100")]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, "3d20")]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, "3d20")]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, "3d20")]
        public void MaximumAgeRoll(string name, string ageRoll)
        {
            base.DistinctCollection(name, ageRoll);
        }

        [TestCase(SizeConstants.BaseRaces.BlueSlaad)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad)]
        public void ImmortalCreatures(string creature)
        {
            base.DistinctCollection(creature, SizeConstants.Ages.Ageless.ToString());
        }
    }
}
