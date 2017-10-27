using CreatureGen.CharacterClasses;
using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;

namespace CreatureGen.Tests.Integration.Tables.Races.BaseRaces.Ages.Rolls
{
    [TestFixture]
    public class SelfTaughtAgeRollsTests : CollectionTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Collection.CLASSTYPEAgeRolls, CharacterClassConstants.TrainingTypes.SelfTaught); }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            AssertCollectionNames(allBaseRaces);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, "1d8")]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, "6d6")]
        [TestCase(SizeConstants.BaseRaces.Azer, "5d6")]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, "5d6")]
        [TestCase(SizeConstants.BaseRaces.Bugbear, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Centaur, "2d6")]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, "5d6")]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, "5d6")]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, "5d6")]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, "3d6")]
        [TestCase(SizeConstants.BaseRaces.Derro, "5d6")]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Drow, "6d6")]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, "5d6")]
        [TestCase(SizeConstants.BaseRaces.FireGiant, "5d6")]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, "6d6")]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, "5d6")]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Githyanki, "3d6")]
        [TestCase(SizeConstants.BaseRaces.Githzerai, "3d6")]
        [TestCase(SizeConstants.BaseRaces.Gnoll, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Goblin, "1d6")]
        [TestCase(SizeConstants.BaseRaces.GrayElf, "6d6")]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, "5d6")]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, "5d6")]
        [TestCase(SizeConstants.BaseRaces.Grimlock, "1d6")]
        [TestCase(SizeConstants.BaseRaces.HalfElf, "2d6")]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Harpy, "1d8")]
        [TestCase(SizeConstants.BaseRaces.HighElf, "6d6")]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, "5d6")]
        [TestCase(SizeConstants.BaseRaces.HillGiant, "5d6")]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, "1d6")]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Human, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Janni, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Kobold, "1d6")]
        [TestCase(SizeConstants.BaseRaces.KuoToa, "1d6")]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, "3d6")]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Locathah, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Merfolk, "2d6")]
        [TestCase(SizeConstants.BaseRaces.Merrow, "3d6")]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, "4d6")]
        [TestCase(SizeConstants.BaseRaces.Minotaur, "1d6")]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, "5d6")]
        [TestCase(SizeConstants.BaseRaces.Ogre, "3d6")]
        [TestCase(SizeConstants.BaseRaces.OgreMage, "3d6")]
        [TestCase(SizeConstants.BaseRaces.Orc, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Pixie, "1d4")]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, "1d6")]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, "5d6")]
        [TestCase(SizeConstants.BaseRaces.RockGnome, "6d6")]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Satyr, "2d8")]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Scrag, "1d6")]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, "5d6")]
        [TestCase(SizeConstants.BaseRaces.StormGiant, "5d6")]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, "6d6")]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, "3d6")]
        [TestCase(SizeConstants.BaseRaces.Tiefling, "1d8")]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, "1d6")]
        [TestCase(SizeConstants.BaseRaces.Troll, "1d6")]
        [TestCase(SizeConstants.BaseRaces.WildElf, "6d6")]
        [TestCase(SizeConstants.BaseRaces.WoodElf, "6d6")]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, "1d6")]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, "1d6")]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, "1d6")]
        public void SelfTaughtAgeRoll(string name, string ageRoll)
        {
            base.DistinctCollection(name, ageRoll);
        }
    }
}
