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
        [TestCase(CreatureConstants.Aboleth_Mage, CreatureConstants.Types.Aberration,
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
        [TestCase(CreatureConstants.Camel, CreatureConstants.Types.Animal)]
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
        [TestCase(CreatureConstants.Efreeti, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Fire,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Elasmosaurus, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Elephant, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Erinyes, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.FireBeetle_Giant, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.GelatinousCube, CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Glabrezu, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.GrayOoze, CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.GreenHag, CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Gynosphinx, CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Hawk, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Hellcat_Bezekira, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
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
        [TestCase(CreatureConstants.HornedDevil_Cornugon, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Horse_Heavy, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Horse_Heavy_War, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Horse_Light, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Horse_Light_War, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Human, CreatureConstants.Types.Humanoid)]
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
        [TestCase(CreatureConstants.Janni, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Native)]
        [TestCase(CreatureConstants.Lemure, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Leopard, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Lion, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Lion_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Lizard, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Lizard_Monitor, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Locust_Swarm, CreatureConstants.Types.Vermin,
            CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.MantaRay, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Marilith, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Megaraptor, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Mule, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Nalfeshnee, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.OchreJelly, CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Octopus, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Octopus_Giant, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Owl, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.PitFiend, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Lawful)]
        [TestCase(CreatureConstants.Pony, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Pony_War, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Porpoise, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.PrayingMantis_Giant, CreatureConstants.Types.Vermin)]
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
        [TestCase(CreatureConstants.Rat, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Rat_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Rat_Swarm, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Raven, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Retriever, CreatureConstants.Types.Construct,
            CreatureConstants.Types.Subtypes.Extraplanar)]
        [TestCase(CreatureConstants.Rhinoceras, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Colossal, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Gargantuan, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Huge, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Large, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Medium, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Small, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Tiny, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.SeaHag, CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Shark_Dire, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Shark_Huge, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Shark_Large, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Shark_Medium, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Snake_Constrictor, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Snake_Viper_Huge, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Snake_Viper_Large, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Snake_Viper_Medium, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Snake_Viper_Small, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Spider_Monstrous_Colossal, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Monstrous_Gargantuan, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Monstrous_Huge, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Monstrous_Large, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Monstrous_Medium, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Monstrous_Small, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Monstrous_Tiny, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Spider_Swarm, CreatureConstants.Types.Vermin,
            CreatureConstants.Types.Subtypes.Swarm)]
        [TestCase(CreatureConstants.Squid, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.Squid_Giant, CreatureConstants.Types.Animal,
            CreatureConstants.Types.Subtypes.Aquatic)]
        [TestCase(CreatureConstants.StagBeetle_Giant, CreatureConstants.Types.Vermin)]
        [TestCase(CreatureConstants.Succubus, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Evil,
            CreatureConstants.Types.Subtypes.Extraplanar,
            CreatureConstants.Types.Subtypes.Chaotic)]
        [TestCase(CreatureConstants.Tiefling, CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Subtypes.Native)]
        [TestCase(CreatureConstants.Tiger, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Tiger_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Toad, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Triceratops, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Tyrannosaurus, CreatureConstants.Types.Animal)]
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
        [TestCase(CreatureConstants.Wolf, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Wolf_Dire, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Wolverine, CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Wolverine_Dire, CreatureConstants.Types.Animal)]
        public void CreatureTypes(string creature, params string[] types)
        {
            OrderedCollection(creature, types);
        }

        [Test]
        public void AllCreaturesHaveAtLeastACreatureType()
        {
            var creatures = CreatureConstants.All();
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

            var allSubTypes = new[]
            {
                CreatureConstants.Types.Subtypes.Air,
                CreatureConstants.Types.Subtypes.Angel,
                CreatureConstants.Types.Subtypes.Aquatic,
                CreatureConstants.Types.Subtypes.Archon,
                CreatureConstants.Types.Subtypes.Augmented,
                CreatureConstants.Types.Subtypes.Chaotic,
                CreatureConstants.Types.Subtypes.Cold,
                CreatureConstants.Types.Subtypes.Earth,
                CreatureConstants.Types.Subtypes.Evil,
                CreatureConstants.Types.Subtypes.Extraplanar,
                CreatureConstants.Types.Subtypes.Fire,
                CreatureConstants.Types.Subtypes.Goblinoid,
                CreatureConstants.Types.Subtypes.Good,
                CreatureConstants.Types.Subtypes.Incorporeal,
                CreatureConstants.Types.Subtypes.Lawful,
                CreatureConstants.Types.Subtypes.Native,
                CreatureConstants.Types.Subtypes.Reptilian,
                CreatureConstants.Types.Subtypes.Shapechanger,
                CreatureConstants.Types.Subtypes.Swarm,
                CreatureConstants.Types.Subtypes.Water,
            };

            foreach (var creature in creatures)
            {
                Assert.That(table.Keys, Contains.Item(creature), "Table keys");

                var types = table[creature];
                Assert.That(types, Is.Not.Empty, creature);
                Assert.That(types.Take(1), Is.SubsetOf(allTypes), creature);

                var subtypes = types.Skip(1);
                Assert.That(subtypes.Intersect(allSubTypes), Is.EquivalentTo(subtypes), creature);
            }
        }

        [Test]
        public void CreatureTypeMatchesCreatureGroupType()
        {
            var creatures = CreatureConstants.All();
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
    }
}
