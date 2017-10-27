using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Ages.Rolls
{
    [TestFixture]
    public class IntuitiveAgeRollsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, CharacterClassConstants.TrainingTypes.Intuitive); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, "1d6")]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, "4d6")]
        [TestCase(SizeConstants.BaseRaces.Azer, "3d6")]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Bugbear, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Centaur, "1d6")]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, "2d4")]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, "2d4")]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, "3d6")]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Derro, "3d6")]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Drow, "4d6")]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, "3d6")]
        [TestCase(SizeConstants.BaseRaces.FireGiant, "2d4")]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, "4d6")]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Githyanki, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Githzerai, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Gnoll, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Goblin, "1d4")]
        [TestCase(SizeConstants.BaseRaces.GrayElf, "4d6")]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, "2d4")]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Grimlock, "1d4")]
        [TestCase(SizeConstants.BaseRaces.HalfElf, "1d6")]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Harpy, "1d3")]
        [TestCase(SizeConstants.BaseRaces.HighElf, "4d6")]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, "3d6")]
        [TestCase(SizeConstants.BaseRaces.HillGiant, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, "1d4")]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Human, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Janni, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Kobold, "1d4")]
        [TestCase(SizeConstants.BaseRaces.KuoToa, "2d6")]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, "1d3")]
        [TestCase(SizeConstants.BaseRaces.Locathah, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Merfolk, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Merrow, "2d6")]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Minotaur, "1d4")]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, "3d6")]
        [TestCase(SizeConstants.BaseRaces.Ogre, "2d6")]
        [TestCase(SizeConstants.BaseRaces.OgreMage, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Orc, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Pixie, "1d2-1")]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, "1d4")]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, "2d4")]
        [TestCase(SizeConstants.BaseRaces.RockGnome, "4d6")]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Satyr, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Scrag, "1d4")]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, "2d4")]
        [TestCase(SizeConstants.BaseRaces.StormGiant, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, "4d6")]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Tiefling, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Troll, "1d4")]
        [TestCase(SizeConstants.BaseRaces.WildElf, "4d6")]
        [TestCase(SizeConstants.BaseRaces.WoodElf, "4d6")]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, "1d4")]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, "1d4")]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, "1d4")]
        public void IntuitiveAgeRoll(string name, string ageRoll)
        {
            DistinctCollection(name, ageRoll);
        }
    }
}
