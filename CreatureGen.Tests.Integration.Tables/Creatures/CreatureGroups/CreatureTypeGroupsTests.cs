using CreatureGen.Creatures;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureTypeGroupsTests : CreatureGroupsTableTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        [SetUp]
        public void Setup()
        {
            var clientID = Guid.NewGuid();
            ClientIdManager.SetClientID(clientID);
        }

        [Test]
        public void EntriesAreComplete()
        {
            AssertCreatureGroupEntriesAreComplete();
        }

        [Test]
        public void AberrationGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Aboleth,
                CreatureConstants.Aboleth_Mage,
                CreatureConstants.Athach,
                CreatureConstants.Beholder,
                CreatureConstants.CarrionCrawler,
                CreatureConstants.Choker,
                CreatureConstants.Chuul,
                CreatureConstants.Cloaker,
                CreatureConstants.Delver,
                CreatureConstants.Destrachan,
                CreatureConstants.Drider,
                CreatureConstants.EtherealFilcher,
                CreatureConstants.Ettercap,
                CreatureConstants.GibberingMouther,
                CreatureConstants.Grick,
                CreatureConstants.Mimic,
                CreatureConstants.MindFlayer,
                CreatureConstants.Groups.Naga,
                CreatureConstants.Otyugh,
                CreatureConstants.Phasm,
                CreatureConstants.RustMonster,
                CreatureConstants.Skum,
                CreatureConstants.UmberHulk,
                CreatureConstants.UmberHulk_TrulyHorrid,
                CreatureConstants.WillOWisp,
            };

            base.DistinctCollection(CreatureConstants.Types.Aberration, creatures);
        }

        [Test]
        public void AnimalGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Ape,
                CreatureConstants.Ape_Dire,
                CreatureConstants.Baboon,
                CreatureConstants.Badger,
                CreatureConstants.Badger_Dire,
                CreatureConstants.Bat,
                CreatureConstants.Bat_Dire,
                CreatureConstants.Bat_Swarm,
                CreatureConstants.Groups.Bear,
                CreatureConstants.Bison,
                CreatureConstants.Boar,
                CreatureConstants.Boar_Dire,
                CreatureConstants.Camel,
                CreatureConstants.Cat,
                CreatureConstants.Cheetah,
                CreatureConstants.Crocodile,
                CreatureConstants.Crocodile_Giant,
                CreatureConstants.Groups.Dinosaur,
                CreatureConstants.Dog,
                CreatureConstants.Dog_Riding,
                CreatureConstants.Donkey,
                CreatureConstants.Eagle,
                CreatureConstants.Elephant,
                CreatureConstants.Hawk,
                CreatureConstants.Groups.Horse,
                CreatureConstants.Hyena,
                CreatureConstants.Leopard,
                CreatureConstants.Lion,
                CreatureConstants.Lion_Dire,
                CreatureConstants.Lizard,
                CreatureConstants.Lizard_Monitor,
                CreatureConstants.MantaRay,
                CreatureConstants.Monkey,
                CreatureConstants.Mule,
                CreatureConstants.Octopus,
                CreatureConstants.Octopus_Giant,
                CreatureConstants.Owl,
                CreatureConstants.Pony,
                CreatureConstants.Pony_War,
                CreatureConstants.Porpoise,
                CreatureConstants.Rat,
                CreatureConstants.Rat_Dire,
                CreatureConstants.Rat_Swarm,
                CreatureConstants.Raven,
                CreatureConstants.Rhinoceras,
                CreatureConstants.Roc,
                CreatureConstants.Snake_Constrictor,
                CreatureConstants.Snake_Constrictor_Giant,
                CreatureConstants.Groups.Snake_Viper,
                CreatureConstants.Groups.Shark,
                CreatureConstants.Squid,
                CreatureConstants.Squid_Giant,
                CreatureConstants.Tiger,
                CreatureConstants.Tiger_Dire,
                CreatureConstants.Toad,
                CreatureConstants.Weasel,
                CreatureConstants.Weasel_Dire,
                CreatureConstants.Groups.Whale,
                CreatureConstants.Wolf,
                CreatureConstants.Wolf_Dire,
                CreatureConstants.Wolverine,
                CreatureConstants.Wolverine_Dire,
            };

            base.DistinctCollection(CreatureConstants.Types.Animal, creatures);
        }

        [Test]
        public void ConstructGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Groups.Inevitable,
                CreatureConstants.Groups.AnimatedObject,
                CreatureConstants.Groups.Golem,
                CreatureConstants.Homunculus,
                CreatureConstants.Retriever,
                CreatureConstants.ShieldGuardian,
            };

            base.DistinctCollection(CreatureConstants.Types.Construct, creatures);
        }

        [Test]
        public void DragonGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Groups.Dragon_Black,
                CreatureConstants.Groups.Dragon_Blue,
                CreatureConstants.Groups.Dragon_Green,
                CreatureConstants.Groups.Dragon_Red,
                CreatureConstants.Groups.Dragon_White,
                CreatureConstants.Groups.Dragon_Brass,
                CreatureConstants.Groups.Dragon_Bronze,
                CreatureConstants.Groups.Dragon_Copper,
                CreatureConstants.Groups.Dragon_Gold,
                CreatureConstants.Groups.Dragon_Silver,
                CreatureConstants.DragonTurtle,
                CreatureConstants.Pseudodragon,
                CreatureConstants.Wyvern,
            };

            base.DistinctCollection(CreatureConstants.Types.Dragon, creatures);
        }

        [Test]
        public void ElementalGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Belker,
                CreatureConstants.Groups.Elemental_Air,
                CreatureConstants.Groups.Elemental_Earth,
                CreatureConstants.Groups.Elemental_Fire,
                CreatureConstants.Groups.Elemental_Water,
                CreatureConstants.InvisibleStalker,
                CreatureConstants.Magmin,
                CreatureConstants.Thoqqua
            };

            base.DistinctCollection(CreatureConstants.Types.Elemental, creatures);
        }

        [Test]
        public void FeyGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Dryad,
                CreatureConstants.Nymph,
                CreatureConstants.Satyr,
                CreatureConstants.Satyr_WithPipes,
                CreatureConstants.Groups.Sprite,
            };

            base.DistinctCollection(CreatureConstants.Types.Fey, creatures);
        }

        [Test]
        public void GiantGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Ettin,
                CreatureConstants.Giant_Cloud,
                CreatureConstants.Giant_Fire,
                CreatureConstants.Giant_Frost,
                CreatureConstants.Giant_Hill,
                CreatureConstants.Giant_Stone,
                CreatureConstants.Giant_Stone_Elder,
                CreatureConstants.Giant_Storm,
                CreatureConstants.Ogre,
                CreatureConstants.Ogre_Merrow,
                CreatureConstants.OgreMage,
                CreatureConstants.Troll,
                CreatureConstants.Troll_Scrag,
            };

            base.DistinctCollection(CreatureConstants.Types.Giant, creatures);
        }

        [Test]
        public void HumanoidGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Aasimar,
                CreatureConstants.Bugbear,
                CreatureConstants.Duergar,
                CreatureConstants.Groups.Dwarf,
                CreatureConstants.Groups.Elf,
                CreatureConstants.Gnoll,
                CreatureConstants.Groups.Gnome,
                CreatureConstants.Goblin,
                CreatureConstants.Groups.Halfling,
                CreatureConstants.Hobgoblin,
                CreatureConstants.Human,
                CreatureConstants.Kobold,
                CreatureConstants.KuoToa,
                CreatureConstants.Lizardfolk,
                CreatureConstants.Locathah,
                CreatureConstants.Merfolk,
                CreatureConstants.Orc,
                CreatureConstants.Orc_Half,
                CreatureConstants.Tiefling,
                CreatureConstants.Troglodyte,
            };

            base.DistinctCollection(CreatureConstants.Types.Humanoid, creatures);
        }

        [Test]
        public void MagicalBeastsGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Ankheg,
                CreatureConstants.Aranea,
                CreatureConstants.Basilisk,
                CreatureConstants.Behir,
                CreatureConstants.BlinkDog,
                CreatureConstants.Bulette,
                CreatureConstants.Chimera,
                CreatureConstants.Cockatrice,
                CreatureConstants.Darkmantle,
                CreatureConstants.Digester,
                CreatureConstants.DisplacerBeast,
                CreatureConstants.DisplacerBeast_PackLord,
                CreatureConstants.Dragonne,
                CreatureConstants.Eagle_Giant,
                CreatureConstants.EtherealMarauder,
                CreatureConstants.FrostWorm,
                CreatureConstants.Girallon,
                CreatureConstants.Gorgon,
                CreatureConstants.GrayRender,
                CreatureConstants.Griffon,
                CreatureConstants.Hippogriff,
                CreatureConstants.Groups.Hydra,
                CreatureConstants.Kraken,
                CreatureConstants.Krenshar,
                CreatureConstants.Lamia,
                CreatureConstants.Lammasu,
                CreatureConstants.Lammasu_GoldenProtector,
                CreatureConstants.Manticore,
                CreatureConstants.Owl_Giant,
                CreatureConstants.Owlbear,
                CreatureConstants.Pegasus,
                CreatureConstants.PhaseSpider,
                CreatureConstants.PurpleWorm,
                CreatureConstants.RazorBoar,
                CreatureConstants.Remorhaz,
                CreatureConstants.Roper,
                CreatureConstants.SeaCat,
                CreatureConstants.ShockerLizard,
                CreatureConstants.Groups.Sphinx,
                CreatureConstants.SpiderEater,
                CreatureConstants.Stirge,
                CreatureConstants.Tarrasque,
                CreatureConstants.Unicorn,
                CreatureConstants.Unicorn_CelestialCharger,
                CreatureConstants.WinterWolf,
                CreatureConstants.Worg,
                CreatureConstants.Yrthak,
            };

            base.DistinctCollection(CreatureConstants.Types.MagicalBeast, creatures);
        }

        [Test]
        public void MonstrousHumanoidGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Centaur,
                CreatureConstants.Derro,
                CreatureConstants.Doppelganger,
                CreatureConstants.Gargoyle,
                CreatureConstants.Gargoyle_Kapoacinth,
                CreatureConstants.Grimlock,
                CreatureConstants.Groups.Hag,
                CreatureConstants.Harpy,
                CreatureConstants.Medusa,
                CreatureConstants.Minotaur,
                CreatureConstants.Sahuagin,
                CreatureConstants.Scorpionfolk,
                CreatureConstants.Groups.YuanTi,
            };

            base.DistinctCollection(CreatureConstants.Types.MonstrousHumanoid, creatures);
        }

        [Test]
        public void OozeGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.BlackPudding,
                CreatureConstants.BlackPudding_Elder,
                CreatureConstants.GelatinousCube,
                CreatureConstants.Ooze_Gray,
                CreatureConstants.Ooze_OchreJelly,
            };

            base.DistinctCollection(CreatureConstants.Types.Ooze, creatures);
        }

        [Test]
        public void OutsiderGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Achaierai,
                CreatureConstants.Groups.Angel,
                CreatureConstants.Groups.Archon,
                CreatureConstants.Groups.Arrowhawk,
                CreatureConstants.Avoral,
                CreatureConstants.Azer,
                CreatureConstants.Barghest,
                CreatureConstants.Barghest_Greater,
                CreatureConstants.Basilisk_AbyssalGreater,
                CreatureConstants.Bralani,
                CreatureConstants.ChaosBeast,
                CreatureConstants.Couatl,
                CreatureConstants.Groups.Demon,
                CreatureConstants.Groups.Devil,
                CreatureConstants.EtherealMarauder,
                CreatureConstants.Groups.Formian,
                CreatureConstants.Groups.Genie,
                CreatureConstants.Ghaele,
                CreatureConstants.Githyanki,
                CreatureConstants.Githzerai,
                CreatureConstants.HellHound,
                CreatureConstants.Hellwasp_Swarm,
                CreatureConstants.Howler,
                CreatureConstants.Groups.Inevitable,
                CreatureConstants.Leonal,
                CreatureConstants.Lillend,
                CreatureConstants.Groups.Mephit,
                CreatureConstants.HellHound_NessianWarhound,
                CreatureConstants.NightHag,
                CreatureConstants.Nightmare,
                CreatureConstants.Nightmare_Cauchemar,
                CreatureConstants.Rakshasa,
                CreatureConstants.Rast,
                CreatureConstants.Ravid,
                CreatureConstants.Groups.Salamander,
                CreatureConstants.ShadowMastiff,
                CreatureConstants.Groups.Slaad,
                CreatureConstants.Titan,
                CreatureConstants.Groups.Tojanida,
                CreatureConstants.Triton,
                CreatureConstants.Vargouille,
                CreatureConstants.Xill,
                CreatureConstants.Groups.Xorn,
                CreatureConstants.YethHound,
            };

            base.DistinctCollection(CreatureConstants.Types.Outsider, creatures);
        }

        [Test]
        public void PlantGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.AssassinVine,
                CreatureConstants.Groups.Fungus,
                CreatureConstants.PhantomFungus,
                CreatureConstants.ShamblingMound,
                CreatureConstants.Tendriculos,
                CreatureConstants.Treant,
            };

            base.DistinctCollection(CreatureConstants.Types.Plant, creatures);
        }

        [Test]
        public void UndeadGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Allip,
                CreatureConstants.Bodak,
                CreatureConstants.Devourer,
                CreatureConstants.Ghoul,
                CreatureConstants.Ghoul_Ghast,
                CreatureConstants.Ghoul_Lacedon,
                CreatureConstants.Mohrg,
                CreatureConstants.Mummy,
                CreatureConstants.Groups.Nightshade,
                CreatureConstants.Shadow,
                CreatureConstants.Shadow_Greater,
                CreatureConstants.Spectre,
                CreatureConstants.VampireSpawn,
                CreatureConstants.Wight,
                CreatureConstants.Wraith,
                CreatureConstants.Wraith_Dread,
            };

            base.DistinctCollection(CreatureConstants.Types.Undead, creatures);
        }

        [Test]
        public void VerminGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Groups.Ant_Giant,
                CreatureConstants.Bee_Giant,
                CreatureConstants.BombardierBeetle_Giant,
                CreatureConstants.Groups.Centipede_Monstrous,
                CreatureConstants.Centipede_Swarm,
                CreatureConstants.FireBeetle_Giant,
                CreatureConstants.Locust_Swarm,
                CreatureConstants.PrayingMantis_Giant,
                CreatureConstants.Groups.Scorpion_Monstrous,
                CreatureConstants.Groups.Spider_Monstrous,
                CreatureConstants.Spider_Swarm,
                CreatureConstants.StagBeetle_Giant,
                CreatureConstants.Wasp_Giant,
            };

            base.DistinctCollection(CreatureConstants.Types.Vermin, creatures);
        }

        [Test]
        public void AllCreaturesHaveType()
        {
            var allCreatures = CreatureConstants.All();
            var allTypes = new[]
            {
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

            var allCreaturesOfType = new List<string>();

            foreach (var type in allTypes)
            {
                var creaturesOfType = CollectionSelector.Explode(tableName, type);
                allCreaturesOfType.AddRange(creaturesOfType);
            }

            AssertCollection(allCreaturesOfType.Distinct(), allCreatures);
        }
    }
}
