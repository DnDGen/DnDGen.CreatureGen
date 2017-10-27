using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Races
{
    [TestFixture]
    public class AerialSpeedsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.AerialSpeeds; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);

            var names = baseRaceGroups[GroupConstants.All].Union(metaraceGroups[GroupConstants.All]);
            AssertCollectionNames(names);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, 0)]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, 0)]
        [TestCase(SizeConstants.BaseRaces.Azer, 0)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, 0)]
        [TestCase(SizeConstants.BaseRaces.Bugbear, 0)]
        [TestCase(SizeConstants.BaseRaces.Centaur, 0)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, 0)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, 0)]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Derro, 0)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, 0)]
        [TestCase(SizeConstants.BaseRaces.Drow, 0)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.FireGiant, 0)]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, 0)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, 60)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, 0)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, 0)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, 0)]
        [TestCase(SizeConstants.BaseRaces.Goblin, 0)]
        [TestCase(SizeConstants.BaseRaces.GrayElf, 0)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, 0)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, 0)]
        [TestCase(SizeConstants.BaseRaces.Grimlock, 0)]
        [TestCase(SizeConstants.BaseRaces.HalfElf, 0)]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(SizeConstants.BaseRaces.Harpy, 80)]
        [TestCase(SizeConstants.BaseRaces.HighElf, 0)]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, 0)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, 0)]
        [TestCase(SizeConstants.BaseRaces.Human, 0)]
        [TestCase(SizeConstants.BaseRaces.Janni, 20)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, 0)]
        [TestCase(SizeConstants.BaseRaces.Kobold, 0)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, 0)]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, 0)]
        [TestCase(SizeConstants.BaseRaces.Locathah, 0)]
        [TestCase(SizeConstants.BaseRaces.Merfolk, 0)]
        [TestCase(SizeConstants.BaseRaces.Merrow, 0)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, 0)]
        [TestCase(SizeConstants.BaseRaces.Minotaur, 0)]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.Ogre, 0)]
        [TestCase(SizeConstants.BaseRaces.OgreMage, 40)]
        [TestCase(SizeConstants.BaseRaces.Orc, 0)]
        [TestCase(SizeConstants.BaseRaces.Pixie, 60)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, 0)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, 0)]
        [TestCase(SizeConstants.BaseRaces.RockGnome, 0)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, 0)]
        [TestCase(SizeConstants.BaseRaces.Satyr, 0)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, 0)]
        [TestCase(SizeConstants.BaseRaces.Scrag, 0)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, 0)]
        [TestCase(SizeConstants.BaseRaces.StormGiant, 0)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, 0)]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Tiefling, 0)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, 0)]
        [TestCase(SizeConstants.BaseRaces.Troll, 0)]
        [TestCase(SizeConstants.BaseRaces.WildElf, 0)]
        [TestCase(SizeConstants.BaseRaces.WoodElf, 0)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, 0)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, 0)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, 0)]
        [TestCase(SizeConstants.Metaraces.Ghost, 30)]
        [TestCase(SizeConstants.Metaraces.HalfCelestial, 2)]
        [TestCase(SizeConstants.Metaraces.HalfDragon, 2)]
        [TestCase(SizeConstants.Metaraces.HalfFiend, 1)]
        [TestCase(SizeConstants.Metaraces.Lich, 0)]
        [TestCase(SizeConstants.Metaraces.Mummy, 0)]
        [TestCase(SizeConstants.Metaraces.None, 0)]
        [TestCase(SizeConstants.Metaraces.Vampire, 0)]
        [TestCase(SizeConstants.Metaraces.Werebear, 0)]
        [TestCase(SizeConstants.Metaraces.Wereboar, 0)]
        [TestCase(SizeConstants.Metaraces.Wererat, 0)]
        [TestCase(SizeConstants.Metaraces.Weretiger, 0)]
        [TestCase(SizeConstants.Metaraces.Werewolf, 0)]
        public void AerialSpeed(string name, int speed)
        {
            Adjustment(name, speed);
        }
    }
}