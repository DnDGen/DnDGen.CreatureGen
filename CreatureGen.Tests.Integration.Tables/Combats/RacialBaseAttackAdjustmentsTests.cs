using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Combats
{
    [TestFixture]
    public class RacialBaseAttackAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.RacialBaseAttackAdjustments; }
        }

        [Test]
        public override void CollectionNames()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);

            var names = metaraceGroups[GroupConstants.All].Union(baseRaceGroups[GroupConstants.All]);
            AssertCollectionNames(names);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, 0)]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, 0)]
        [TestCase(SizeConstants.BaseRaces.Azer, 2)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, 8)]
        [TestCase(SizeConstants.BaseRaces.Bugbear, 2)]
        [TestCase(SizeConstants.BaseRaces.Centaur, 4)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, 12)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, 15)]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, 4)]
        [TestCase(SizeConstants.BaseRaces.Derro, 3)]
        [TestCase(SizeConstants.BaseRaces.Drow, 0)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.FireGiant, 11)]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, 10)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, 4)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, 0)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, 0)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, 1)]
        [TestCase(SizeConstants.BaseRaces.Goblin, 0)]
        [TestCase(SizeConstants.BaseRaces.GrayElf, 0)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, 10)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, 9)]
        [TestCase(SizeConstants.BaseRaces.Grimlock, 2)]
        [TestCase(SizeConstants.BaseRaces.HalfElf, 0)]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(SizeConstants.BaseRaces.Harpy, 7)]
        [TestCase(SizeConstants.BaseRaces.HighElf, 0)]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, 9)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, 0)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, 6)]
        [TestCase(SizeConstants.BaseRaces.Human, 0)]
        [TestCase(SizeConstants.BaseRaces.Janni, 6)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, 4)]
        [TestCase(SizeConstants.BaseRaces.Kobold, 0)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, 0)]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, 1)]
        [TestCase(SizeConstants.BaseRaces.Locathah, 0)]
        [TestCase(SizeConstants.BaseRaces.Merfolk, 0)]
        [TestCase(SizeConstants.BaseRaces.Merrow, 3)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, 6)]
        [TestCase(SizeConstants.BaseRaces.Minotaur, 6)]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.Ogre, 3)]
        [TestCase(SizeConstants.BaseRaces.OgreMage, 3)]
        [TestCase(SizeConstants.BaseRaces.Orc, 0)]
        [TestCase(SizeConstants.BaseRaces.Pixie, 0)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, 7)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, 7)]
        [TestCase(SizeConstants.BaseRaces.RockGnome, 0)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, 2)]
        [TestCase(SizeConstants.BaseRaces.Satyr, 2)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, 12)]
        [TestCase(SizeConstants.BaseRaces.Scrag, 4)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, 10)]
        [TestCase(SizeConstants.BaseRaces.StormGiant, 14)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, 0)]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Tiefling, 0)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, 1)]
        [TestCase(SizeConstants.BaseRaces.Troll, 4)]
        [TestCase(SizeConstants.BaseRaces.WildElf, 0)]
        [TestCase(SizeConstants.BaseRaces.WoodElf, 0)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, 9)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, 7)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, 4)]
        [TestCase(SizeConstants.Metaraces.Ghost, 0)]
        [TestCase(SizeConstants.Metaraces.HalfCelestial, 0)]
        [TestCase(SizeConstants.Metaraces.HalfDragon, 0)]
        [TestCase(SizeConstants.Metaraces.HalfFiend, 0)]
        [TestCase(SizeConstants.Metaraces.Lich, 0)]
        [TestCase(SizeConstants.Metaraces.Mummy, 4)]
        [TestCase(SizeConstants.Metaraces.None, 0)]
        [TestCase(SizeConstants.Metaraces.Vampire, 0)]
        [TestCase(SizeConstants.Metaraces.Werebear, 5)]
        [TestCase(SizeConstants.Metaraces.Wereboar, 3)]
        [TestCase(SizeConstants.Metaraces.Wererat, 1)]
        [TestCase(SizeConstants.Metaraces.Weretiger, 5)]
        [TestCase(SizeConstants.Metaraces.Werewolf, 2)]
        public void RacialBaseAttackAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}