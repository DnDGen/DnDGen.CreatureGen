using CreatureGen.Creatures;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class CreatureTypesTests : CollectionTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.CreatureTypes;
            }
        }

        [SetUp]
        public void Setup()
        {
            var clientId = Guid.NewGuid();
            ClientIdManager.SetClientID(clientId);
        }

        [Test]
        public void CollectionNames()
        {
            var creatures = CreatureConstants.All();
            AssertCollectionNames(creatures);
        }

        [TestCase(CreatureConstants.Aasimar, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Native)]
        [TestCase(CreatureConstants.Aboleth, CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Achaierai, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Allip, CreatureConstants.Types.Undead,
            CreatureConstants.Types.Subtypes.Incorporeal)]
        [TestCase(CreatureConstants.Androsphinx, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.AnimatedObject_Huge, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.AnimatedObject_Large, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.AnimatedObject_Medium, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.AnimatedObject_Small, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Ankheg, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Annis, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Ant_Giant_Queen, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Ant_Giant_Soldier, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Ant_Giant_Worker, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Ape, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Ape_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Aranea, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Shapechanger)]
        [TestCase(CreatureConstants.HoundArchon, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Archon,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Good,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.LanternArchon, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Archon,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Good,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.TrumpetArchon, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Archon,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Good,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Arrowhawk_Elder, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Angel_AstralDeva, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Angel,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Angel_Planetar, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Angel,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Angel_Solar, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Angel,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.AssassinVine, CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Athach, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Avoral, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Good,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Azer, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Babau, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Baboon, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Badger, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Badger_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Balor, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.BarbedDevil_Hamatula, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Barghest, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful,
            CreatureConstants.Types.Subtypes.Shapechanger)]
        [TestCase(CreatureConstants.Barghest_Greater, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful,
            CreatureConstants.Types.Subtypes.Shapechanger)]
        [TestCase(CreatureConstants.Basilisk, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Basilisk_AbyssalGreater, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Augmented,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Bat, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Bat_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Bat_Swarm, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Bear_Black, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Bear_Brown, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Bear_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Bear_Polar, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.BeardedDevil_Barbazu, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Bebilith, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Bee_Giant, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Behir, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Beholder, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Beholder_Gauth, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Belker, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Bison, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.BlackPudding, CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.BlackPudding_Elder, CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.BlinkDog, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Boar, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Boar_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Bodak, CreatureConstants.Types.Undead,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.BombardierBeetle_Giant, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.BoneDevil_Osyluth, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Bralani, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Good,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Bugbear, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Goblinoid)]
        [TestCase(CreatureConstants.Bulette, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Camel_Bactrian, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Camel_Dromedary, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.CarrionCrawler, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Cat, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Centaur, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Colossal, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Gargantuan, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Huge, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Large, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Medium, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Small, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Tiny, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Centipede_Swarm, CreatureConstants.Types.Vermin,
            CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.ChainDevil_Kyton, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.ChaosBeast, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Chaotic,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Cheetah, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Chimera, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Choker, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Chuul, CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Cloaker, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Cockatrice, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Couatl, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Native)]
        [TestCase(CreatureConstants.Criosphinx, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Cryohydra_10Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Cryohydra_11Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Cryohydra_12Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Cryohydra_5Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Cryohydra_6Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Cryohydra_7Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Cryohydra_8Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Cryohydra_9Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Crocodile, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Crocodile_Giant, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Darkmantle, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Deinonychus, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Delver, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Derro, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Derro_Sane, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Destrachan, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Devourer, CreatureConstants.Types.Undead,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Digester, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.DisplacerBeast, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.DisplacerBeast_PackLord, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Djinni, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Djinni_Noble, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Dog, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Dog_Riding, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Donkey, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Doppelganger, CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Subtypes.Shapechanger)]
        [TestCase(CreatureConstants.Dragon_Black_Adult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Black_Ancient, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Black_GreatWyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Black_Juvenile, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Black_MatureAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Black_Old, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Black_VeryOld, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Black_VeryYoung, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Black_Wyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Black_Wyrmling, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Black_Young, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Black_YoungAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Blue_Adult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Blue_Ancient, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Blue_GreatWyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Blue_Juvenile, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Blue_MatureAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Blue_Old, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Blue_VeryOld, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Blue_VeryYoung, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrmling, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Blue_Young, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Blue_YoungAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Green_Adult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Dragon_Green_Ancient, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Dragon_Green_GreatWyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Dragon_Green_Juvenile, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Dragon_Green_MatureAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Dragon_Green_Old, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Dragon_Green_VeryOld, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Dragon_Green_VeryYoung, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Dragon_Green_Wyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Dragon_Green_Wyrmling, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Dragon_Green_Young, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Dragon_Green_YoungAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Dragon_Red_Adult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Red_Ancient, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Red_GreatWyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Red_Juvenile, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Red_MatureAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Red_Old, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Red_VeryOld, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Red_VeryYoung, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Red_Wyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Red_Wyrmling, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Red_Young, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Red_YoungAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_White_Adult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_White_Ancient, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_White_GreatWyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_White_Juvenile, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_White_MatureAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_White_Old, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_White_VeryOld, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_White_VeryYoung, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_White_Wyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_White_Wyrmling, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_White_Young, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_White_YoungAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_Brass_Adult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Brass_Ancient, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Brass_GreatWyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Brass_Juvenile, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Brass_MatureAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Brass_Old, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Brass_VeryOld, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Brass_VeryYoung, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrmling, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Brass_Young, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Brass_YoungAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Bronze_Adult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Bronze_Ancient, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Bronze_GreatWyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Bronze_Juvenile, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Bronze_MatureAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Bronze_Old, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryOld, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryYoung, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrmling, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Bronze_Young, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Bronze_YoungAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Water)]
        [TestCase(CreatureConstants.Dragon_Copper_Adult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Copper_Ancient, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Copper_GreatWyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Copper_Juvenile, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Copper_MatureAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Copper_Old, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Copper_VeryOld, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Copper_VeryYoung, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrmling, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Copper_Young, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Copper_YoungAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Dragon_Gold_Adult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Gold_Ancient, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Gold_GreatWyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Gold_Juvenile, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Gold_MatureAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Gold_Old, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Gold_VeryOld, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Gold_VeryYoung, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrmling, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Gold_Young, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Gold_YoungAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Dragon_Silver_Adult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_Silver_Ancient, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_Silver_GreatWyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_Silver_Juvenile, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_Silver_MatureAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_Silver_Old, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_Silver_VeryOld, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_Silver_VeryYoung, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrm, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrmling, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_Silver_Young, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Dragon_Silver_YoungAdult, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.DragonTurtle, CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Dragonne, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Dretch, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Drider, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Dryad, CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Dwarf_Duergar, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Dwarf)]
        [TestCase(CreatureConstants.Dwarf_Deep, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Dwarf)]
        [TestCase(CreatureConstants.Dwarf_Hill, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Dwarf)]
        [TestCase(CreatureConstants.Dwarf_Mountain, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Dwarf)]
        [TestCase(CreatureConstants.Eagle, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Eagle_Giant, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Efreeti, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elasmosaurus, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Elemental_Air_Elder, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Air_Greater, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Air_Huge, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Air_Large, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Air_Medium, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Air_Small, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Earth_Elder, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Earth_Greater, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Earth_Huge, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Earth_Large, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Earth_Medium, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Earth_Small, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Fire_Elder, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Fire_Greater, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Fire_Huge, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Fire_Large, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Fire_Medium, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Fire_Small, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Water_Elder, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Water_Greater, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Water_Huge, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Water_Large, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Water_Medium, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elemental_Water_Small, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elephant, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Elf_Aquatic, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Elf,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Elf_Drow, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Elf_Gray, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Elf_Half, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Elf_High, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Elf_Wild, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Elf_Wood, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Elf)]
        [TestCase(CreatureConstants.Erinyes, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.EtherealFilcher, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.EtherealMarauder, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Ettercap, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Ettin, CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.FireBeetle_Giant, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.FormianMyrmarch, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Lawful,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.FormianQueen, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Lawful,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.FormianTaskmaster, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Lawful,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.FormianWarrior, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Lawful,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.FormianWorker, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Lawful,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.FrostWorm, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Gargoyle, CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.GelatinousCube, CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Ghaele, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Chaotic,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Ghoul, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Ghoul_Ghast, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Ghoul_Lacedon, CreatureConstants.Types.Undead,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Giant_Cloud, CreatureConstants.Types.Giant,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.Giant_Fire, CreatureConstants.Types.Giant,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Giant_Frost, CreatureConstants.Types.Giant,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Giant_Hill, CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Giant_Stone, CreatureConstants.Types.Giant,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Giant_Stone_Elder, CreatureConstants.Types.Giant,
            CreatureConstants.Types.Subtypes.Earth)]
        [TestCase(CreatureConstants.Giant_Storm, CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.GibberingMouther, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Girallon, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Githyanki, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Githzerai, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Glabrezu, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Gnoll, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Gnoll)]
        [TestCase(CreatureConstants.Gnome_Forest, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Gnome)]
        [TestCase(CreatureConstants.Gnome_Rock, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Gnome)]
        [TestCase(CreatureConstants.Gnome_Svirfneblin, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Gnome)]
        [TestCase(CreatureConstants.Goblin, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Goblinoid)]
        [TestCase(CreatureConstants.Golem_Clay, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Golem_Flesh, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Golem_Iron, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Golem_Stone, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Golem_Stone_Greater, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Gorgon, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.GrayOoze, CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.GrayRender, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.GreenHag, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Grick, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Griffon, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Grig, CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Nixie, CreatureConstants.Types.Fey,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Pixie, CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Pixie_WithIrresistableDance, CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Grimlock, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Gynosphinx, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Halfling_Deep, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Halfling)]
        [TestCase(CreatureConstants.Halfling_Lightfoot, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Halfling)]
        [TestCase(CreatureConstants.Halfling_Tallfellow, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Halfling)]
        [TestCase(CreatureConstants.Harpy, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Hawk, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Hellcat_Bezekira, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.HellHound, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.HellHound_NessianWarhound, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Hellwasp_Swarm, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Hezrou, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Hieracosphinx, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Hippogriff, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Hobgoblin, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Goblinoid)]
        [TestCase(CreatureConstants.Homunculus, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.HornedDevil_Cornugon, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Horse_Heavy, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Horse_Heavy_War, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Horse_Light, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Horse_Light_War, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Howler, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Chaotic,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Human)]
        [TestCase(CreatureConstants.Hydra_10Heads, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Hydra_11Heads, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Hydra_12Heads, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Hydra_5Heads, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Hydra_6Heads, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Hydra_7Heads, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Hydra_8Heads, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Hydra_9Heads, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Hyena, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.IceDevil_Gelugon, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Imp, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.InvisibleStalker, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Janni, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Native)]
        [TestCase(CreatureConstants.Kobold, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Reptilian)]
        [TestCase(CreatureConstants.Kolyarut, CreatureConstants.Types.Construct,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Marut, CreatureConstants.Types.Construct,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Zelekhut, CreatureConstants.Types.Construct,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Kraken, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Krenshar, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.KuoToa, CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Lamia, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Lammasu, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Lemure, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Leonal, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Leopard, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Lillend, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Chaotic,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Good)]
        [TestCase(CreatureConstants.Lion, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Lion_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Lizard, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Lizard_Monitor, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Lizardfolk, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Reptilian)]
        [TestCase(CreatureConstants.Locathah, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Locust_Swarm, CreatureConstants.Types.Vermin,
            CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Magmin, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.MantaRay, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Manticore, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Marilith, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Medusa, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Megaraptor, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Mephit_Air, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Mephit_Dust, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Mephit_Earth, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Mephit_Fire, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Mephit_Ice, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Air,
            CreatureConstants.Types.Subtypes.Cold,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Mephit_Magma, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Mephit_Ooze, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Mephit_Salt, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Mephit_Steam, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Mephit_Water, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Merfolk, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Mimic, CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Subtypes.Shapechanger)]
        [TestCase(CreatureConstants.MindFlayer, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Minotaur, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Mohrg, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Monkey, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Mule, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Mummy, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Naga_Dark, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Naga_Guardian, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Naga_Spirit, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Naga_Water, CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Nalfeshnee, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.NightHag, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Nightmare, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Nightmare_Cauchemar, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Nightcrawler, CreatureConstants.Types.Undead,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Nightwalker, CreatureConstants.Types.Undead,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Nightwing, CreatureConstants.Types.Undead,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Nymph, CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.OchreJelly, CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Octopus, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Octopus_Giant, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Ogre, CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Ogre_Merrow, CreatureConstants.Types.Giant,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.OgreMage, CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Orc, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Orc)]
        [TestCase(CreatureConstants.Orc_Half, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Orc)]
        [TestCase(CreatureConstants.Otyugh, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Owl, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Owl_Giant, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Owlbear, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Pegasus, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.PhantomFungus, CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.PhaseSpider, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Phasm, CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Subtypes.Shapechanger)]
        [TestCase(CreatureConstants.PitFiend, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Pony, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Pony_War, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Porpoise, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.PrayingMantis_Giant, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Pseudodragon, CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.PurpleWorm, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Pyrohydra_10Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Pyrohydra_11Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Pyrohydra_12Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Pyrohydra_5Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Pyrohydra_6Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Pyrohydra_7Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Pyrohydra_8Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Pyrohydra_9Heads, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Quasit, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Rakshasa, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Native)]
        [TestCase(CreatureConstants.Rast, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Rat, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Rat_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Rat_Swarm, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Raven, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Ravid, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.RazorBoar, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Remorhaz, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Retriever, CreatureConstants.Types.Construct,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Rhinoceras, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Roc, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Roper, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.RustMonster, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Sahuagin, CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Salamander_Average, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Salamander_Flamebrother, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Salamander_Noble, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Satyr, CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Satyr_WithPipes, CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Colossal, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Gargantuan, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Huge, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Large, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Medium, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Small, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Tiny, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpionfolk, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.SeaCat, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.SeaHag, CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Shadow, CreatureConstants.Types.Undead,
            CreatureConstants.Types.Subtypes.Incorporeal)]
        [TestCase(CreatureConstants.Shadow_Greater, CreatureConstants.Types.Undead,
            CreatureConstants.Types.Subtypes.Incorporeal)]
        [TestCase(CreatureConstants.ShadowMastiff, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.ShamblingMound, CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Shark_Dire, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Shark_Huge, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Shark_Large, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Shark_Medium, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.ShieldGuardian, CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.ShockerLizard, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Shrieker, CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.VioletFungus, CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Skum, CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Slaad_Blue, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Chaotic,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Slaad_Death, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Chaotic,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Slaad_Gray, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Chaotic,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Slaad_Green, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Chaotic,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Slaad_Red, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Chaotic,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Snake_Constrictor, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Snake_Viper_Huge, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Snake_Viper_Large, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Snake_Viper_Medium, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Snake_Viper_Small, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Spectre, CreatureConstants.Types.Undead,
            CreatureConstants.Types.Subtypes.Incorporeal)]
        [TestCase(CreatureConstants.Spider_Monstrous_Colossal, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Monstrous_Gargantuan, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Monstrous_Huge, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Monstrous_Large, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Monstrous_Medium, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Monstrous_Small, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Monstrous_Tiny, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Swarm, CreatureConstants.Types.Vermin,
            CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.SpiderEater, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Squid, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Squid_Giant, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.StagBeetle_Giant, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Stirge, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Succubus, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Tarrasque, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Tendriculos, CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Thoqqua, CreatureConstants.Types.Elemental,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Fire)]
        [TestCase(CreatureConstants.Tiefling, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Native)]
        [TestCase(CreatureConstants.Tiger, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Tiger_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Titan, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Chaotic,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Toad, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Tojanida_Adult, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Tojanida_Elder, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Tojanida_Juvenile, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Treant, CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Triceratops, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Triton, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Water,
            CreatureConstants.Types.Subtypes.Native)]
        [TestCase(CreatureConstants.Troglodyte, CreatureConstants.Types.Humanoid,
            CreatureConstants.Types.Subtypes.Reptilian)]
        [TestCase(CreatureConstants.Troll, CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Troll_Scrag, CreatureConstants.Types.Giant,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Tyrannosaurus, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.UmberHulk, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.UmberHulk_TrulyHorrid, CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Unicorn, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Unicorn_CelestialCharger, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.VampireSpawn, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.Vargouille, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Vrock, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Wasp_Giant, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Weasel, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Weasel_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Whale_Baleen, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Whale_Cachalot, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Whale_Orca, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Wight, CreatureConstants.Types.Undead)]
        [TestCase(CreatureConstants.WillOWisp, CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Subtypes.Air)]
        [TestCase(CreatureConstants.WinterWolf, CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Subtypes.Cold)]
        [TestCase(CreatureConstants.Wolf, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Wolf_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Wolverine, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Wolverine_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Worg, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Wraith, CreatureConstants.Types.Undead,
            CreatureConstants.Types.Subtypes.Incorporeal)]
        [TestCase(CreatureConstants.Wraith_Dread, CreatureConstants.Types.Undead,
            CreatureConstants.Types.Subtypes.Incorporeal)]
        [TestCase(CreatureConstants.Wyvern, CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Xill, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Xorn_Average, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Xorn_Elder, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Xorn_Minor, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Earth,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.YethHound, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Yrthak, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.YuanTi_Abomination, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.YuanTi_Halfblood, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.YuanTi_Pureblood, CreatureConstants.Types.MonstrousHumanoid)]
        public void CreatureTypes(string creature, params string[] types)
        {
            OrderedCollection(creature, types);
            DistinctCollection(creature, types);
        }

        [Test]
        public void AllCreaturesHaveAtLeastACreatureType()
        {
            var creatures = CreatureConstants.All();
            var allTypes = CreatureConstants.Types.All();
            var allSubTypes = CreatureConstants.Types.Subtypes.All();

            foreach (var creature in creatures)
            {
                Assert.That(table.Keys, Contains.Item(creature), "Table keys");

                var types = table[creature];
                Assert.That(types, Is.Not.Empty, creature);
                Assert.That(types.Take(1), Is.SubsetOf(allTypes), creature);

                var subtypes = types.Skip(1);
                Assert.That(subtypes, Is.SubsetOf(allSubTypes), creature);
            }
        }

        [Test]
        public void CreatureTypeMatchesCreatureGroupType()
        {
            var creatures = CreatureConstants.All();
            var allTypes = CreatureConstants.Types.All();

            foreach (var creature in creatures)
            {
                Assert.That(table.Keys, Contains.Item(creature), "Table keys");

                var types = table[creature];
                Assert.That(types, Is.Not.Empty, creature);
                Assert.That(types.Take(1), Is.SubsetOf(allTypes), creature);

                var type = types.First();
                var creaturesOfType = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, type);
                Assert.That(creaturesOfType, Contains.Item(creature), type);
            }
        }

        [Test]
        public void CreatureSubtypesMatchCreatureGroupSubtypes()
        {
            var creatures = CreatureConstants.All();
            var allSubTypes = CreatureConstants.Types.Subtypes.All();

            foreach (var creature in creatures)
            {
                Assert.That(table.Keys, Contains.Item(creature), "Table keys");

                var types = table[creature];
                Assert.That(types, Is.Not.Empty, creature);

                var subtypes = types.Skip(1);
                Assert.That(subtypes, Is.SubsetOf(allSubTypes), creature);

                foreach (var subtype in subtypes)
                {
                    var creaturesOfType = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, subtype);
                    Assert.That(creaturesOfType, Contains.Item(creature), subtype);
                }
            }
        }
    }
}
