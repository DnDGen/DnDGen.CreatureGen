using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureTypeGroupsTests : CreatureGroupsTableTests
    {
        private ICollectionSelector collectionSelector;

        [SetUp]
        public void Setup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
        }

        [Test]
        public void CreatureGroupNames()
        {
            AssertCreatureGroupNamesAreComplete();
        }

        [TestCase(CreatureConstants.Types.Construct,
            CreatureConstants.Groups.Inevitable,
            CreatureConstants.Groups.AnimatedObject,
            CreatureConstants.Groups.Golem,
            CreatureConstants.Homunculus,
            CreatureConstants.Retriever,
            CreatureConstants.ShieldGuardian)]
        [TestCase(CreatureConstants.Types.Dragon,
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
            CreatureConstants.Wyvern)]
        [TestCase(CreatureConstants.Types.Elemental,
            CreatureConstants.Belker,
            CreatureConstants.Groups.Elemental_Air,
            CreatureConstants.Groups.Elemental_Earth,
            CreatureConstants.Groups.Elemental_Fire,
            CreatureConstants.Groups.Elemental_Water,
            CreatureConstants.InvisibleStalker,
            CreatureConstants.Magmin,
            CreatureConstants.Thoqqua)]
        [TestCase(CreatureConstants.Types.Fey,
            CreatureConstants.Dryad,
            CreatureConstants.Nymph,
            CreatureConstants.Satyr,
            CreatureConstants.Satyr_WithPipes,
            CreatureConstants.Groups.Sprite)]
        [TestCase(CreatureConstants.Types.Giant,
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
            CreatureConstants.Troll_Scrag)]
        [TestCase(CreatureConstants.Types.Humanoid,
            CreatureConstants.Bugbear,
            CreatureConstants.Groups.Dwarf,
            CreatureConstants.Groups.Elf,
            CreatureConstants.Githyanki,
            CreatureConstants.Githzerai,
            CreatureConstants.Gnoll,
            CreatureConstants.Groups.Gnome,
            CreatureConstants.Goblin,
            CreatureConstants.Groups.Halfling,
            CreatureConstants.Hobgoblin,
            CreatureConstants.Human,
            CreatureConstants.Kobold,
            CreatureConstants.Lizardfolk,
            CreatureConstants.Locathah,
            CreatureConstants.Merfolk,
            CreatureConstants.Groups.Orc,
            CreatureConstants.Troglodyte)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Centaur,
            CreatureConstants.Derro,
            CreatureConstants.Derro_Sane,
            CreatureConstants.Doppelganger,
            CreatureConstants.Gargoyle,
            CreatureConstants.Gargoyle_Kapoacinth,
            CreatureConstants.Grimlock,
            CreatureConstants.Groups.Hag,
            CreatureConstants.Harpy,
            CreatureConstants.KuoToa,
            CreatureConstants.Medusa,
            CreatureConstants.Minotaur,
            CreatureConstants.Sahuagin,
            CreatureConstants.Sahuagin_Malenti,
            CreatureConstants.Sahuagin_Mutant,
            CreatureConstants.Scorpionfolk,
            CreatureConstants.Groups.YuanTi)]
        [TestCase(CreatureConstants.Types.Ooze,
            CreatureConstants.BlackPudding,
            CreatureConstants.BlackPudding_Elder,
            CreatureConstants.GelatinousCube,
            CreatureConstants.GrayOoze,
            CreatureConstants.OchreJelly)]
        [TestCase(CreatureConstants.Types.Plant,
            CreatureConstants.AssassinVine,
            CreatureConstants.Groups.Fungus,
            CreatureConstants.PhantomFungus,
            CreatureConstants.ShamblingMound,
            CreatureConstants.Tendriculos,
            CreatureConstants.Treant)]
        [TestCase(CreatureConstants.Types.Undead,
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
            CreatureConstants.Wraith_Dread)]
        [TestCase(CreatureConstants.Types.Vermin,
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
            CreatureConstants.Wasp_Giant)]
        [TestCase(CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Groups.Arrowhawk,
            CreatureConstants.Belker,
            CreatureConstants.Djinni,
            CreatureConstants.Djinni_Noble,
            CreatureConstants.Groups.Dragon_Green,
            CreatureConstants.Groups.Elemental_Air,
            CreatureConstants.Giant_Cloud,
            CreatureConstants.InvisibleStalker,
            CreatureConstants.Mephit_Air,
            CreatureConstants.Mephit_Dust,
            CreatureConstants.Mephit_Ice,
            CreatureConstants.WillOWisp)]
        [TestCase(CreatureConstants.Types.Subtypes.Angel,
            CreatureConstants.Angel_AstralDeva,
            CreatureConstants.Angel_Planetar,
            CreatureConstants.Angel_Solar)]
        [TestCase(CreatureConstants.Types.Subtypes.Aquatic,
            CreatureConstants.Aboleth,
            CreatureConstants.Chuul,
            CreatureConstants.DragonTurtle,
            CreatureConstants.Elf_Aquatic,
            CreatureConstants.Gargoyle_Kapoacinth,
            CreatureConstants.Ghoul_Lacedon,
            CreatureConstants.Kraken,
            CreatureConstants.KuoToa,
            CreatureConstants.Locathah,
            CreatureConstants.MantaRay,
            CreatureConstants.Merfolk,
            CreatureConstants.Naga_Water,
            CreatureConstants.Nixie,
            CreatureConstants.Octopus,
            CreatureConstants.Octopus_Giant,
            CreatureConstants.Ogre_Merrow,
            CreatureConstants.Porpoise,
            CreatureConstants.Sahuagin,
            CreatureConstants.Sahuagin_Malenti,
            CreatureConstants.Sahuagin_Mutant,
            CreatureConstants.SeaHag,
            CreatureConstants.Groups.Shark,
            CreatureConstants.Skum,
            CreatureConstants.Squid,
            CreatureConstants.Squid_Giant,
            CreatureConstants.Troll_Scrag,
            CreatureConstants.Groups.Whale)]
        [TestCase(CreatureConstants.Types.Subtypes.Archon,
            CreatureConstants.HoundArchon,
            CreatureConstants.LanternArchon,
            CreatureConstants.TrumpetArchon)]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented,
            CreatureConstants.Basilisk_Greater)]
        [TestCase(CreatureConstants.Types.Subtypes.Chaotic,
            CreatureConstants.ChaosBeast,
            CreatureConstants.Babau,
            CreatureConstants.Balor,
            CreatureConstants.Bebilith,
            CreatureConstants.Bralani,
            CreatureConstants.Dretch,
            CreatureConstants.Ghaele,
            CreatureConstants.Glabrezu,
            CreatureConstants.Hezrou,
            CreatureConstants.Howler,
            CreatureConstants.Lillend,
            CreatureConstants.Marilith,
            CreatureConstants.Nalfeshnee,
            CreatureConstants.Quasit,
            CreatureConstants.Groups.Slaad,
            CreatureConstants.Succubus,
            CreatureConstants.Titan,
            CreatureConstants.Vrock)]
        [TestCase(CreatureConstants.Types.Subtypes.Cold,
            CreatureConstants.Groups.Dragon_Silver,
            CreatureConstants.Groups.Dragon_White,
            CreatureConstants.FrostWorm,
            CreatureConstants.Giant_Frost,
            CreatureConstants.Groups.Cryohydra,
            CreatureConstants.Mephit_Ice,
            CreatureConstants.WinterWolf)]
        [TestCase(CreatureConstants.Types.Subtypes.Dwarf,
            CreatureConstants.Dwarf_Deep,
            CreatureConstants.Dwarf_Duergar,
            CreatureConstants.Dwarf_Hill,
            CreatureConstants.Dwarf_Mountain)]
        [TestCase(CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Groups.Dragon_Blue,
            CreatureConstants.Groups.Dragon_Copper,
            CreatureConstants.Groups.Elemental_Earth,
            CreatureConstants.Gargoyle,
            CreatureConstants.Gargoyle_Kapoacinth,
            CreatureConstants.Giant_Stone,
            CreatureConstants.Giant_Stone_Elder,
            CreatureConstants.Mephit_Earth,
            CreatureConstants.Mephit_Salt,
            CreatureConstants.Thoqqua,
            CreatureConstants.Groups.Xorn)]
        [TestCase(CreatureConstants.Types.Subtypes.Elf,
            CreatureConstants.Elf_Aquatic,
            CreatureConstants.Elf_Drow,
            CreatureConstants.Elf_Gray,
            CreatureConstants.Elf_Half,
            CreatureConstants.Elf_High,
            CreatureConstants.Elf_Wild,
            CreatureConstants.Elf_Wood)]
        [TestCase(CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Achaierai,
            CreatureConstants.Babau,
            CreatureConstants.Balor,
            CreatureConstants.Barghest,
            CreatureConstants.Barghest_Greater,
            CreatureConstants.Bebilith,
            CreatureConstants.Groups.Devil,
            CreatureConstants.Dretch,
            CreatureConstants.Glabrezu,
            CreatureConstants.HellHound,
            CreatureConstants.HellHound_NessianWarhound,
            CreatureConstants.Hellwasp_Swarm,
            CreatureConstants.Hezrou,
            CreatureConstants.Howler,
            CreatureConstants.Marilith,
            CreatureConstants.Nalfeshnee,
            CreatureConstants.NightHag,
            CreatureConstants.Nightmare,
            CreatureConstants.Nightmare_Cauchemar,
            CreatureConstants.Quasit,
            CreatureConstants.Succubus,
            CreatureConstants.Vargouille,
            CreatureConstants.Vrock,
            CreatureConstants.YethHound)]
        [TestCase(CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Azer,
            CreatureConstants.Groups.Dragon_Brass,
            CreatureConstants.Groups.Dragon_Gold,
            CreatureConstants.Groups.Dragon_Red,
            CreatureConstants.Groups.Elemental_Fire,
            CreatureConstants.Efreeti,
            CreatureConstants.Giant_Fire,
            CreatureConstants.HellHound,
            CreatureConstants.HellHound_NessianWarhound,
            CreatureConstants.Groups.Pyrohydra,
            CreatureConstants.Magmin,
            CreatureConstants.Mephit_Fire,
            CreatureConstants.Mephit_Magma,
            CreatureConstants.Mephit_Steam,
            CreatureConstants.Rast,
            CreatureConstants.Groups.Salamander,
            CreatureConstants.Thoqqua)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnoll,
            CreatureConstants.Gnoll)]
        [TestCase(CreatureConstants.Types.Subtypes.Gnome,
            CreatureConstants.Gnome_Forest,
            CreatureConstants.Gnome_Rock,
            CreatureConstants.Gnome_Svirfneblin)]
        [TestCase(CreatureConstants.Types.Subtypes.Goblinoid,
            CreatureConstants.Bugbear,
            CreatureConstants.Goblin,
            CreatureConstants.Hobgoblin)]
        [TestCase(CreatureConstants.Types.Subtypes.Good,
            CreatureConstants.Groups.Angel,
            CreatureConstants.Groups.Archon,
            CreatureConstants.Avoral,
            CreatureConstants.Bralani,
            CreatureConstants.Ghaele,
            CreatureConstants.Leonal,
            CreatureConstants.Lillend)]
        [TestCase(CreatureConstants.Types.Subtypes.Halfling,
            CreatureConstants.Halfling_Deep,
            CreatureConstants.Halfling_Lightfoot,
            CreatureConstants.Halfling_Tallfellow)]
        [TestCase(CreatureConstants.Types.Subtypes.Human,
            CreatureConstants.Elf_Half,
            CreatureConstants.Orc_Half,
            CreatureConstants.Human)]
        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal,
            CreatureConstants.Allip,
            CreatureConstants.Shadow,
            CreatureConstants.Shadow_Greater,
            CreatureConstants.Spectre,
            CreatureConstants.Wraith,
            CreatureConstants.Wraith_Dread)]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful,
            CreatureConstants.Achaierai,
            CreatureConstants.Groups.Archon,
            CreatureConstants.Barghest,
            CreatureConstants.Barghest_Greater,
            CreatureConstants.Groups.Devil,
            CreatureConstants.Groups.Formian,
            CreatureConstants.HellHound,
            CreatureConstants.HellHound_NessianWarhound,
            CreatureConstants.Groups.Inevitable)]
        [TestCase(CreatureConstants.Types.Subtypes.Native,
            CreatureConstants.Couatl,
            CreatureConstants.Janni,
            CreatureConstants.Groups.Planetouched,
            CreatureConstants.Rakshasa,
            CreatureConstants.Triton)]
        [TestCase(CreatureConstants.Types.Subtypes.Orc,
            CreatureConstants.Orc,
            CreatureConstants.Orc_Half)]
        [TestCase(CreatureConstants.Types.Subtypes.Reptilian,
            CreatureConstants.Kobold,
            CreatureConstants.Lizardfolk,
            CreatureConstants.Troglodyte)]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger,
            CreatureConstants.Aranea,
            CreatureConstants.Barghest,
            CreatureConstants.Barghest_Greater,
            CreatureConstants.Doppelganger,
            CreatureConstants.Mimic,
            CreatureConstants.Phasm)]
        [TestCase(CreatureConstants.Types.Subtypes.Swarm,
            CreatureConstants.Bat_Swarm,
            CreatureConstants.Centipede_Swarm,
            CreatureConstants.Hellwasp_Swarm,
            CreatureConstants.Locust_Swarm,
            CreatureConstants.Rat_Swarm,
            CreatureConstants.Spider_Swarm)]
        [TestCase(CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Groups.Dragon_Black,
            CreatureConstants.Groups.Dragon_Bronze,
            CreatureConstants.Groups.Elemental_Water,
            CreatureConstants.Mephit_Ooze,
            CreatureConstants.Mephit_Water,
            CreatureConstants.Triton,
            CreatureConstants.Groups.Tojanida)]
        public void CreatureTypeGroup(string name, params string[] entries)
        {
            AssertDistinctCollection(name, entries);
        }

        [Test]
        public void AberrationGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Aboleth,
                CreatureConstants.Athach,
                CreatureConstants.Beholder,
                CreatureConstants.Beholder_Gauth,
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

            AssertDistinctCollection(CreatureConstants.Types.Aberration, creatures);
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
                CreatureConstants.Camel_Bactrian,
                CreatureConstants.Camel_Dromedary,
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

            AssertDistinctCollection(CreatureConstants.Types.Animal, creatures);
        }

        [Test]
        public void ExtraplanarGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Achaierai,
                CreatureConstants.Groups.Archon,
                CreatureConstants.Groups.Angel,
                CreatureConstants.Groups.Arrowhawk,
                CreatureConstants.Avoral,
                CreatureConstants.Azer,
                CreatureConstants.Barghest,
                CreatureConstants.Barghest_Greater,
                CreatureConstants.Basilisk_Greater,
                CreatureConstants.Bodak,
                CreatureConstants.Bralani,
                CreatureConstants.ChaosBeast,
                CreatureConstants.Groups.Demon,
                CreatureConstants.Groups.Devil,
                CreatureConstants.Devourer,
                CreatureConstants.Djinni,
                CreatureConstants.Djinni_Noble,
                CreatureConstants.Efreeti,
                CreatureConstants.Groups.Elemental,
                CreatureConstants.EtherealMarauder,
                CreatureConstants.Groups.Formian,
                CreatureConstants.Ghaele,
                CreatureConstants.Githyanki,
                CreatureConstants.Githzerai,
                CreatureConstants.HellHound,
                CreatureConstants.HellHound_NessianWarhound,
                CreatureConstants.Hellwasp_Swarm,
                CreatureConstants.Howler,
                CreatureConstants.Groups.Inevitable,
                CreatureConstants.Leonal,
                CreatureConstants.Lillend,
                CreatureConstants.Groups.Mephit,
                CreatureConstants.NightHag,
                CreatureConstants.Nightmare,
                CreatureConstants.Nightmare_Cauchemar,
                CreatureConstants.Groups.Nightshade,
                CreatureConstants.Rast,
                CreatureConstants.Ravid,
                CreatureConstants.Groups.Salamander,
                CreatureConstants.ShadowMastiff,
                CreatureConstants.Groups.Slaad,
                CreatureConstants.Titan,
                CreatureConstants.Groups.Tojanida,
                CreatureConstants.Vargouille,
                CreatureConstants.Xill,
                CreatureConstants.Groups.Xorn,
                CreatureConstants.YethHound,
            };

            AssertDistinctCollection(CreatureConstants.Types.Subtypes.Extraplanar, creatures);
        }

        [Test]
        public void MagicalBeastsGroup()
        {
            var creatures = new[]
            {
                CreatureConstants.Ankheg,
                CreatureConstants.Aranea,
                CreatureConstants.Basilisk,
                //CreatureConstants.Basilisk_AbyssalGreater, //INFO: This is not included in this group, even thoguh it does contain it as a subtype, since the group signifies main type of Magical Beast
                CreatureConstants.Behir,
                CreatureConstants.BlinkDog,
                CreatureConstants.Bulette,
                CreatureConstants.Chimera_Black,
                CreatureConstants.Chimera_Blue,
                CreatureConstants.Chimera_Green,
                CreatureConstants.Chimera_Red,
                CreatureConstants.Chimera_White,
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
                CreatureConstants.Hellwasp_Swarm,
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
                CreatureConstants.Groups.Sphinx,
                CreatureConstants.SpiderEater,
                CreatureConstants.Stirge,
                CreatureConstants.Tarrasque,
                CreatureConstants.Unicorn,
                CreatureConstants.WinterWolf,
                CreatureConstants.Worg,
                CreatureConstants.Yrthak,
            };

            AssertDistinctCollection(CreatureConstants.Types.MagicalBeast, creatures);
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
                CreatureConstants.Basilisk_Greater,
                CreatureConstants.Bralani,
                CreatureConstants.ChaosBeast,
                CreatureConstants.Couatl,
                CreatureConstants.Babau,
                CreatureConstants.Balor,
                CreatureConstants.Bebilith,
                CreatureConstants.Dretch,
                CreatureConstants.Glabrezu,
                CreatureConstants.Hezrou,
                CreatureConstants.Marilith,
                CreatureConstants.Nalfeshnee,
                CreatureConstants.Quasit,
                CreatureConstants.Succubus,
                CreatureConstants.Vrock,
                CreatureConstants.Groups.Devil,
                CreatureConstants.Groups.Formian,
                CreatureConstants.Groups.Genie,
                CreatureConstants.Ghaele,
                CreatureConstants.HellHound,
                CreatureConstants.HellHound_NessianWarhound,
                CreatureConstants.Howler,
                CreatureConstants.Leonal,
                CreatureConstants.Lillend,
                CreatureConstants.Groups.Mephit,
                CreatureConstants.NightHag,
                CreatureConstants.Nightmare,
                CreatureConstants.Nightmare_Cauchemar,
                CreatureConstants.Groups.Planetouched,
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

            base.AssertDistinctCollection(CreatureConstants.Types.Outsider, creatures);
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreatureTypeMatchesCreatureGroupType(string creature)
        {
            var types = collectionMapper.Map(TableNameConstants.Collection.CreatureTypes);
            Assert.That(types.Keys, Contains.Item(creature));
            Assert.That(types[creature], Is.Not.Empty);

            var type = types[creature].First();

            Assert.That(table.Keys, Contains.Item(type), "Table keys");
            var creaturesOfType = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, type);

            Assert.That(creaturesOfType, Contains.Item(creature));
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreatureSubtypesMatchCreatureGroupSubtypes(string creature)
        {
            var types = collectionMapper.Map(TableNameConstants.Collection.CreatureTypes);
            Assert.That(types.Keys, Contains.Item(creature));
            Assert.That(types[creature], Is.Not.Empty);

            //INFO: Have to remove types, as augmented creatures have original creature type as a subtype
            var allTypes = CreatureConstants.Types.GetAll();
            var subtypes = types[creature].Skip(1).Except(allTypes);

            foreach (var subtype in subtypes)
            {
                Assert.That(table.Keys, Contains.Item(subtype), "Table keys");
                var creaturesOfSubtype = collectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, subtype);

                Assert.That(creaturesOfSubtype, Contains.Item(creature), subtype);
            }
        }
    }
}