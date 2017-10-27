using CreatureGen.Domain.Tables;
using CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Races
{
    [TestFixture]
    public class LevelAdjustmentsTests : AdjustmentsTests
    {
        protected override string tableName
        {
            get { return TableNameConstants.Set.Adjustments.LevelAdjustments; }
        }

        [Test]
        public override void CollectionNames()
        {
            var names = new[]
            {
                SizeConstants.BaseRaces.Animals.Ape,
                SizeConstants.BaseRaces.Animals.Badger,
                SizeConstants.BaseRaces.Animals.Bat,
                SizeConstants.BaseRaces.Animals.Bison,
                SizeConstants.BaseRaces.Animals.BlackBear,
                SizeConstants.BaseRaces.Animals.Boar,
                SizeConstants.BaseRaces.Animals.BrownBear,
                SizeConstants.BaseRaces.Animals.Camel,
                SizeConstants.BaseRaces.Animals.Cat,
                SizeConstants.BaseRaces.Animals.CelestialBat,
                SizeConstants.BaseRaces.Animals.CelestialCat,
                SizeConstants.BaseRaces.Animals.CelestialHawk,
                SizeConstants.BaseRaces.Animals.CelestialLizard,
                SizeConstants.BaseRaces.Animals.CelestialOwl,
                SizeConstants.BaseRaces.Animals.CelestialRat,
                SizeConstants.BaseRaces.Animals.CelestialRaven,
                SizeConstants.BaseRaces.Animals.CelestialTinyViperSnake,
                SizeConstants.BaseRaces.Animals.CelestialToad,
                SizeConstants.BaseRaces.Animals.CelestialWeasel,
                SizeConstants.BaseRaces.Animals.Cheetah,
                SizeConstants.BaseRaces.Animals.ConstrictorSnake,
                SizeConstants.BaseRaces.Animals.Deinonychus,
                SizeConstants.BaseRaces.Animals.DireApe,
                SizeConstants.BaseRaces.Animals.DireBadger,
                SizeConstants.BaseRaces.Animals.DireBat,
                SizeConstants.BaseRaces.Animals.DireBear,
                SizeConstants.BaseRaces.Animals.DireBoar,
                SizeConstants.BaseRaces.Animals.DireLion,
                SizeConstants.BaseRaces.Animals.DireRat,
                SizeConstants.BaseRaces.Animals.DireTiger,
                SizeConstants.BaseRaces.Animals.DireWeasel,
                SizeConstants.BaseRaces.Animals.DireWolf,
                SizeConstants.BaseRaces.Animals.DireWolverine,
                SizeConstants.BaseRaces.Animals.Dog,
                SizeConstants.BaseRaces.Animals.Eagle,
                SizeConstants.BaseRaces.Animals.Elephant,
                SizeConstants.BaseRaces.Animals.FiendishBat,
                SizeConstants.BaseRaces.Animals.FiendishCat,
                SizeConstants.BaseRaces.Animals.FiendishHawk,
                SizeConstants.BaseRaces.Animals.FiendishLizard,
                SizeConstants.BaseRaces.Animals.FiendishOwl,
                SizeConstants.BaseRaces.Animals.FiendishRat,
                SizeConstants.BaseRaces.Animals.FiendishRaven,
                SizeConstants.BaseRaces.Animals.FiendishTinyViperSnake,
                SizeConstants.BaseRaces.Animals.FiendishToad,
                SizeConstants.BaseRaces.Animals.FiendishWeasel,
                SizeConstants.BaseRaces.Animals.FormianWorker,
                SizeConstants.BaseRaces.Animals.GiantConstrictorSnake,
                SizeConstants.BaseRaces.Animals.Hawk,
                SizeConstants.BaseRaces.Animals.HeavyHorse,
                SizeConstants.BaseRaces.Animals.HeavyWarhorse,
                SizeConstants.BaseRaces.Animals.Homonculus,
                SizeConstants.BaseRaces.Animals.HugeViperSnake,
                SizeConstants.BaseRaces.Animals.AirMephit,
                SizeConstants.BaseRaces.Animals.DustMephit,
                SizeConstants.BaseRaces.Animals.EarthMephit,
                SizeConstants.BaseRaces.Animals.FireMephit,
                SizeConstants.BaseRaces.Animals.IceMephit,
                SizeConstants.BaseRaces.Animals.MagmaMephit,
                SizeConstants.BaseRaces.Animals.OozeMephit,
                SizeConstants.BaseRaces.Animals.SaltMephit,
                SizeConstants.BaseRaces.Animals.SteamMephit,
                SizeConstants.BaseRaces.Animals.WaterMephit,
                SizeConstants.BaseRaces.Animals.Imp,
                SizeConstants.BaseRaces.Animals.LargeViperSnake,
                SizeConstants.BaseRaces.Animals.Leopard,
                SizeConstants.BaseRaces.Animals.LightHorse,
                SizeConstants.BaseRaces.Animals.Lion,
                SizeConstants.BaseRaces.Animals.Lizard,
                SizeConstants.BaseRaces.Animals.MediumViperSnake,
                SizeConstants.BaseRaces.Animals.Megaraptor,
                SizeConstants.BaseRaces.Animals.MonitorLizard,
                SizeConstants.BaseRaces.Animals.Owl,
                SizeConstants.BaseRaces.Animals.PolarBear,
                SizeConstants.BaseRaces.Animals.Pony,
                SizeConstants.BaseRaces.Animals.Pseudodragon,
                SizeConstants.BaseRaces.Animals.Quasit,
                SizeConstants.BaseRaces.Animals.Rat,
                SizeConstants.BaseRaces.Animals.Raven,
                SizeConstants.BaseRaces.Animals.Rhinoceras,
                SizeConstants.BaseRaces.Animals.RidingDog,
                SizeConstants.BaseRaces.Animals.ShockerLizard,
                SizeConstants.BaseRaces.Animals.SmallAirElemental,
                SizeConstants.BaseRaces.Animals.SmallEarthElemental,
                SizeConstants.BaseRaces.Animals.SmallFireElemental,
                SizeConstants.BaseRaces.Animals.SmallViperSnake,
                SizeConstants.BaseRaces.Animals.SmallWaterElemental,
                SizeConstants.BaseRaces.Animals.Stirge,
                SizeConstants.BaseRaces.Animals.Tiger,
                SizeConstants.BaseRaces.Animals.TinyViperSnake,
                SizeConstants.BaseRaces.Animals.Toad,
                SizeConstants.BaseRaces.Animals.Triceratops,
                SizeConstants.BaseRaces.Animals.Tyrannosaurus,
                SizeConstants.BaseRaces.Animals.Warpony,
                SizeConstants.BaseRaces.Animals.Weasel,
                SizeConstants.BaseRaces.Animals.Wolf,
                SizeConstants.BaseRaces.Animals.Wolverine
            };

            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);

            names = names.Union(baseRaceGroups[GroupConstants.All]).Union(metaraceGroups[GroupConstants.All]).ToArray();

            AssertCollectionNames(names);
        }

        [TestCase(SizeConstants.BaseRaces.Aasimar, 1)]
        [TestCase(SizeConstants.BaseRaces.AquaticElf, 0)]
        [TestCase(SizeConstants.BaseRaces.Azer, 4)]
        [TestCase(SizeConstants.BaseRaces.BlueSlaad, 6)]
        [TestCase(SizeConstants.BaseRaces.Bugbear, 1)]
        [TestCase(SizeConstants.BaseRaces.Centaur, 2)]
        [TestCase(SizeConstants.BaseRaces.CloudGiant, 4)]
        [TestCase(SizeConstants.BaseRaces.DeathSlaad, 6)]
        [TestCase(SizeConstants.BaseRaces.DeepDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.DeepHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Derro, 0)]
        [TestCase(SizeConstants.BaseRaces.Doppelganger, 4)]
        [TestCase(SizeConstants.BaseRaces.Drow, 2)]
        [TestCase(SizeConstants.BaseRaces.DuergarDwarf, 1)]
        [TestCase(SizeConstants.BaseRaces.FireGiant, 4)]
        [TestCase(SizeConstants.BaseRaces.ForestGnome, 0)]
        [TestCase(SizeConstants.BaseRaces.FrostGiant, 4)]
        [TestCase(SizeConstants.BaseRaces.Gargoyle, 5)]
        [TestCase(SizeConstants.BaseRaces.Githyanki, 2)]
        [TestCase(SizeConstants.BaseRaces.Githzerai, 2)]
        [TestCase(SizeConstants.BaseRaces.Gnoll, 1)]
        [TestCase(SizeConstants.BaseRaces.Goblin, 0)]
        [TestCase(SizeConstants.BaseRaces.GrayElf, 0)]
        [TestCase(SizeConstants.BaseRaces.GraySlaad, 6)]
        [TestCase(SizeConstants.BaseRaces.GreenSlaad, 6)]
        [TestCase(SizeConstants.BaseRaces.Grimlock, 2)]
        [TestCase(SizeConstants.BaseRaces.HalfElf, 0)]
        [TestCase(SizeConstants.BaseRaces.HalfOrc, 0)]
        [TestCase(SizeConstants.BaseRaces.Harpy, 3)]
        [TestCase(SizeConstants.BaseRaces.HighElf, 0)]
        [TestCase(SizeConstants.BaseRaces.HillDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.HillGiant, 4)]
        [TestCase(SizeConstants.BaseRaces.Hobgoblin, 1)]
        [TestCase(SizeConstants.BaseRaces.HoundArchon, 5)]
        [TestCase(SizeConstants.BaseRaces.Human, 0)]
        [TestCase(SizeConstants.BaseRaces.Janni, 5)]
        [TestCase(SizeConstants.BaseRaces.Kapoacinth, 5)]
        [TestCase(SizeConstants.BaseRaces.Kobold, 0)]
        [TestCase(SizeConstants.BaseRaces.KuoToa, 3)]
        [TestCase(SizeConstants.BaseRaces.LightfootHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Lizardfolk, 1)]
        [TestCase(SizeConstants.BaseRaces.Locathah, 1)]
        [TestCase(SizeConstants.BaseRaces.Merfolk, 1)]
        [TestCase(SizeConstants.BaseRaces.Merrow, 2)]
        [TestCase(SizeConstants.BaseRaces.MindFlayer, 7)]
        [TestCase(SizeConstants.BaseRaces.Minotaur, 2)]
        [TestCase(SizeConstants.BaseRaces.MountainDwarf, 0)]
        [TestCase(SizeConstants.BaseRaces.Ogre, 2)]
        [TestCase(SizeConstants.BaseRaces.OgreMage, 7)]
        [TestCase(SizeConstants.BaseRaces.Orc, 0)]
        [TestCase(SizeConstants.BaseRaces.Pixie, 4)]
        [TestCase(SizeConstants.BaseRaces.Rakshasa, 7)]
        [TestCase(SizeConstants.BaseRaces.RedSlaad, 6)]
        [TestCase(SizeConstants.BaseRaces.RockGnome, 0)]
        [TestCase(SizeConstants.BaseRaces.Sahuagin, 2)]
        [TestCase(SizeConstants.BaseRaces.Satyr, 2)]
        [TestCase(SizeConstants.BaseRaces.Scorpionfolk, 4)]
        [TestCase(SizeConstants.BaseRaces.Scrag, 5)]
        [TestCase(SizeConstants.BaseRaces.StoneGiant, 4)]
        [TestCase(SizeConstants.BaseRaces.StormGiant, 4)]
        [TestCase(SizeConstants.BaseRaces.Svirfneblin, 3)]
        [TestCase(SizeConstants.BaseRaces.TallfellowHalfling, 0)]
        [TestCase(SizeConstants.BaseRaces.Tiefling, 1)]
        [TestCase(SizeConstants.BaseRaces.Troglodyte, 2)]
        [TestCase(SizeConstants.BaseRaces.Troll, 5)]
        [TestCase(SizeConstants.BaseRaces.WildElf, 0)]
        [TestCase(SizeConstants.BaseRaces.WoodElf, 0)]
        [TestCase(SizeConstants.BaseRaces.YuanTiAbomination, 7)]
        [TestCase(SizeConstants.BaseRaces.YuanTiHalfblood, 5)]
        [TestCase(SizeConstants.BaseRaces.YuanTiPureblood, 2)]
        [TestCase(SizeConstants.Metaraces.Ghost, 5)]
        [TestCase(SizeConstants.Metaraces.HalfCelestial, 4)]
        [TestCase(SizeConstants.Metaraces.HalfDragon, 3)]
        [TestCase(SizeConstants.Metaraces.HalfFiend, 4)]
        [TestCase(SizeConstants.Metaraces.Lich, 4)]
        [TestCase(SizeConstants.Metaraces.Mummy, 0)]
        [TestCase(SizeConstants.Metaraces.None, 0)]
        [TestCase(SizeConstants.Metaraces.Vampire, 8)]
        [TestCase(SizeConstants.Metaraces.Werebear, 3)]
        [TestCase(SizeConstants.Metaraces.Wereboar, 3)]
        [TestCase(SizeConstants.Metaraces.Wererat, 3)]
        [TestCase(SizeConstants.Metaraces.Weretiger, 3)]
        [TestCase(SizeConstants.Metaraces.Werewolf, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.Badger, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Camel, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.DireRat, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Dog, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.RidingDog, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Eagle, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Hawk, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.LightHorse, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.HeavyHorse, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Owl, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Pony, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.SmallViperSnake, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.MediumViperSnake, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Wolf, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Ape, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.BlackBear, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.Bison, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.Boar, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.Cheetah, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.DireBadger, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.DireBat, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.DireWeasel, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.Leopard, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.MonitorLizard, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.ConstrictorSnake, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.LargeViperSnake, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.Wolverine, 3)]
        [TestCase(SizeConstants.BaseRaces.Animals.BrownBear, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.DireWolverine, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.Deinonychus, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.DireApe, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.DireBoar, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.DireWolf, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.Lion, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.Rhinoceras, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.HugeViperSnake, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.Tiger, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.PolarBear, 9)]
        [TestCase(SizeConstants.BaseRaces.Animals.DireLion, 9)]
        [TestCase(SizeConstants.BaseRaces.Animals.Megaraptor, 9)]
        [TestCase(SizeConstants.BaseRaces.Animals.GiantConstrictorSnake, 9)]
        [TestCase(SizeConstants.BaseRaces.Animals.DireBear, 12)]
        [TestCase(SizeConstants.BaseRaces.Animals.Elephant, 12)]
        [TestCase(SizeConstants.BaseRaces.Animals.DireTiger, 15)]
        [TestCase(SizeConstants.BaseRaces.Animals.Triceratops, 15)]
        [TestCase(SizeConstants.BaseRaces.Animals.Tyrannosaurus, 15)]
        [TestCase(SizeConstants.BaseRaces.Animals.Bat, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Cat, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Lizard, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Rat, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Raven, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.TinyViperSnake, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Toad, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.Weasel, 0)]
        [TestCase(SizeConstants.BaseRaces.Animals.ShockerLizard, 4)]
        [TestCase(SizeConstants.BaseRaces.Animals.Stirge, 4)]
        [TestCase(SizeConstants.BaseRaces.Animals.FormianWorker, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.Imp, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.Pseudodragon, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.Quasit, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.CelestialBat, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.CelestialCat, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.CelestialHawk, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.CelestialLizard, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.CelestialOwl, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.CelestialRat, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.CelestialRaven, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.CelestialTinyViperSnake, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.CelestialToad, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.CelestialWeasel, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.FiendishBat, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.FiendishCat, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.FiendishHawk, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.FiendishLizard, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.FiendishOwl, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.FiendishRat, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.FiendishRaven, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.FiendishTinyViperSnake, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.FiendishToad, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.FiendishWeasel, 2)]
        [TestCase(SizeConstants.BaseRaces.Animals.SmallAirElemental, 4)]
        [TestCase(SizeConstants.BaseRaces.Animals.SmallEarthElemental, 4)]
        [TestCase(SizeConstants.BaseRaces.Animals.SmallFireElemental, 4)]
        [TestCase(SizeConstants.BaseRaces.Animals.SmallWaterElemental, 4)]
        [TestCase(SizeConstants.BaseRaces.Animals.Homonculus, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.AirMephit, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.DustMephit, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.EarthMephit, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.FireMephit, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.IceMephit, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.MagmaMephit, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.OozeMephit, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.SaltMephit, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.SteamMephit, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.WaterMephit, 6)]
        [TestCase(SizeConstants.BaseRaces.Animals.HeavyWarhorse, 4)]
        [TestCase(SizeConstants.BaseRaces.Animals.Warpony, 4)]
        public void LevelAdjustment(string name, int adjustment)
        {
            base.Adjustment(name, adjustment);
        }
    }
}