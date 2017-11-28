using CreatureGen.Creatures;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;

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
                CreatureConstants.Baboon,
                CreatureConstants.Badger,
                CreatureConstants.Bat,
                CreatureConstants.Groups.Bear,
                CreatureConstants.Bison,
                CreatureConstants.Boar,
                CreatureConstants.Camel,
                CreatureConstants.Cat,
                CreatureConstants.Cheetah,
                CreatureConstants.Crocodile,
                CreatureConstants.Groups.Dinosaur,
                CreatureConstants.Dog,
                CreatureConstants.Donkey,
                CreatureConstants.Eagle,
                CreatureConstants.Elephant,
                CreatureConstants.Hawk,
                CreatureConstants.Groups.Horse,
                CreatureConstants.Hyena,
                CreatureConstants.Leopard,
                CreatureConstants.Lion,
                CreatureConstants.Lizard,
                CreatureConstants.Lizard_Monitor,
                CreatureConstants.MantaRay,
                CreatureConstants.Monkey,
                CreatureConstants.Mule,
                CreatureConstants.Octopus,
                CreatureConstants.Owl,
                CreatureConstants.Pony,
                CreatureConstants.Porpoise,
                CreatureConstants.Rat,
                CreatureConstants.Raven,
                CreatureConstants.Rhinoceras,
                CreatureConstants.Roc,
                CreatureConstants.Snake_Constrictor,
                CreatureConstants.Groups.Snake_Viper,
                CreatureConstants.Groups.Shark,
                CreatureConstants.Squid,
                CreatureConstants.Tiger,
                CreatureConstants.Toad,
                CreatureConstants.Weasel,
                CreatureConstants.Groups.Whale,
                CreatureConstants.Wolf,
                CreatureConstants.Wolverine,
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
                CreatureConstants.Androsphinx,
                CreatureConstants.Criosphinx,
                CreatureConstants.Gynosphinx,
                CreatureConstants.Hieracosphinx,
                CreatureConstants.SpiderEater,
                CreatureConstants.Stirge,
                CreatureConstants.Tarrasque,
                CreatureConstants.Unicorn,
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
                CreatureConstants.Groups.Lycanthrope,
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
                CreatureConstants.NessianWarhound,
                CreatureConstants.NightHag,
                CreatureConstants.Nightmare,
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
                CreatureConstants.Ghoul_Lacedon,
                CreatureConstants.Mohrg,
                CreatureConstants.Mummy,
                CreatureConstants.Groups.Nightshade,
                CreatureConstants.Shadow,
                CreatureConstants.Spectre,
                CreatureConstants.Wight,
                CreatureConstants.Wraith,
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

            foreach (var creature in allCreatures)
            {
                var type = CollectionSelector.FindCollectionOf(tableName, creature, allTypes);
                Assert.That(type, Is.Not.Null, creature);
                Assert.That(type, Is.Not.Empty, creature);
                Assert.That(new[] { type }, Is.SubsetOf(allTypes), creature);
            }
        }
    }
}
