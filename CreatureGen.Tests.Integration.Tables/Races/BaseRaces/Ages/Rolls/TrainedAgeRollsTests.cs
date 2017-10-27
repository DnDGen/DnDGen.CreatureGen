using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Ages.Rolls
{
    [TestFixture]
    public class TrainedAgeRollsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, CharacterClassConstants.TrainingTypes.Trained); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            AssertCollectionNames(baseRaceGroups[GroupConstants.All]);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, "2d8")]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, "10d6")]
        [TestCase(SizeConstants.BaseRaces.Azer, "7d6")]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, "5d12")]
        [TestCase(SizeConstants.BaseRaces.Bugbear, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Centaur, "3d6")]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, "5d12")]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, "5d12")]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, "7d6")]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, "4d6")]
        [TestCase(SizeConstants.BaseRaces.Derro, "7d6")]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Drow, "10d6")]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, "7d6")]
        [TestCase(SizeConstants.BaseRaces.FireGiant, "5d12")]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, "9d6")]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, "5d12")]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Githyanki, "4d6")]
        [TestCase(SizeConstants.BaseRaces.Githzerai, "4d6")]
        [TestCase(SizeConstants.BaseRaces.Gnoll, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Goblin, "2d6")]
        [TestCase(SizeConstants.BaseRaces.GrayElf, "10d6")]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, "5d12")]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, "5d12")]
        [TestCase(SizeConstants.BaseRaces.Grimlock, "2d6")]
        [TestCase(SizeConstants.BaseRaces.HalfElf, "3d6")]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Harpy, "2d4")]
        [TestCase(SizeConstants.BaseRaces.HighElf, "10d6")]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, "7d6")]
        [TestCase(SizeConstants.BaseRaces.HillGiant, "5d12")]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, "2d6")]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Human, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Janni, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Kobold, "2d6")]
        [TestCase(SizeConstants.BaseRaces.KuoToa, "3d6")]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, "3d6")]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, "1d10")]
        [TestCase(SizeConstants.BaseRaces.Locathah, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Merfolk, "3d6")]
        [TestCase(SizeConstants.BaseRaces.Merrow, "4d6")]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, "5d8")]
        [TestCase(SizeConstants.BaseRaces.Minotaur, "2d6")]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, "7d6")]
        [TestCase(SizeConstants.BaseRaces.Ogre, "4d6")]
        [TestCase(SizeConstants.BaseRaces.OgreMage, "4d6")]
        [TestCase(SizeConstants.BaseRaces.Orc, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Pixie, "2d4")]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, "2d6")]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, "5d12")]
        [TestCase(SizeConstants.BaseRaces.RockGnome, "9d6")]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Satyr, "2d12")]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Scrag, "2d6")]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, "5d12")]
        [TestCase(SizeConstants.BaseRaces.StormGiant, "5d12")]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, "9d6")]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, "3d6")]
        [TestCase(SizeConstants.BaseRaces.Tiefling, "2d8")]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Troll, "2d6")]
        [TestCase(SizeConstants.BaseRaces.WildElf, "10d6")]
        [TestCase(SizeConstants.BaseRaces.WoodElf, "10d6")]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, "2d6")]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, "2d6")]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, "2d6")]
        public void TrainedAgeRoll(string name, string ageRoll)
        {
            DistinctCollection(name, ageRoll);
        }
    }
}
