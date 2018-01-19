﻿using CreatureGen.Creatures;
using CreatureGen.Selectors.Collections;
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
    public class AerialManeuverabilityTests : CollectionTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        internal ITypeAndAmountSelector TypesAndAmountsSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.AerialManeuverability;
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

        [TestCase(CreatureConstants.Aasimar)]
        [TestCase(CreatureConstants.Aboleth)]
        [TestCase(CreatureConstants.Aboleth_Mage)]
        [TestCase(CreatureConstants.Achaierai)]
        [TestCase(CreatureConstants.Allip, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Androsphinx, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Angel_AstralDeva, "Good Maneuverability")]
        [TestCase(CreatureConstants.Angel_Planetar, "Good Maneuverability")]
        [TestCase(CreatureConstants.Angel_Solar, "Good Maneuverability")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Large)]
        [TestCase(CreatureConstants.AnimatedObject_Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Small)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny)]
        [TestCase(CreatureConstants.Ankheg)]
        [TestCase(CreatureConstants.Annis)]
        [TestCase(CreatureConstants.Ant_Giant_Queen)]
        [TestCase(CreatureConstants.Ant_Giant_Soldier)]
        [TestCase(CreatureConstants.Ant_Giant_Worker)]
        [TestCase(CreatureConstants.Ape)]
        [TestCase(CreatureConstants.Ape_Dire)]
        [TestCase(CreatureConstants.Aranea)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Arrowhawk_Elder, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.AssassinVine)]
        [TestCase(CreatureConstants.Athach)]
        [TestCase(CreatureConstants.Avoral, "Good Maneuverability")]
        [TestCase(CreatureConstants.Azer)]
        [TestCase(CreatureConstants.Babau)]
        [TestCase(CreatureConstants.Baboon)]
        [TestCase(CreatureConstants.Badger)]
        [TestCase(CreatureConstants.Badger_Dire)]
        [TestCase(CreatureConstants.Balor, "Good Maneuverability")]
        [TestCase(CreatureConstants.BarbedDevil_Hamatula)]
        [TestCase(CreatureConstants.Barghest)]
        [TestCase(CreatureConstants.Barghest_Greater)]
        [TestCase(CreatureConstants.Basilisk)]
        [TestCase(CreatureConstants.Basilisk_AbyssalGreater)]
        [TestCase(CreatureConstants.Bat, "Good Maneuverability")]
        [TestCase(CreatureConstants.Bat_Dire, "Good Maneuverability")]
        [TestCase(CreatureConstants.Bat_Swarm, "Good Maneuverability")]
        [TestCase(CreatureConstants.Bear_Black)]
        [TestCase(CreatureConstants.Bear_Brown)]
        [TestCase(CreatureConstants.Bear_Dire)]
        [TestCase(CreatureConstants.Bear_Polar)]
        [TestCase(CreatureConstants.BeardedDevil_Barbazu)]
        [TestCase(CreatureConstants.Bebilith)]
        [TestCase(CreatureConstants.Bee_Giant, "Good Maneuverability")]
        [TestCase(CreatureConstants.Behir)]
        [TestCase(CreatureConstants.Beholder, "Good Maneuverability")]
        [TestCase(CreatureConstants.Beholder_Gauth, "Good Maneuverability")]
        [TestCase(CreatureConstants.Belker, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Bison)]
        [TestCase(CreatureConstants.BlackPudding)]
        [TestCase(CreatureConstants.BlackPudding_Elder)]
        [TestCase(CreatureConstants.BlinkDog)]
        [TestCase(CreatureConstants.Boar)]
        [TestCase(CreatureConstants.Boar_Dire)]
        [TestCase(CreatureConstants.Bodak)]
        [TestCase(CreatureConstants.BombardierBeetle_Giant)]
        [TestCase(CreatureConstants.BoneDevil_Osyluth)]
        [TestCase(CreatureConstants.Bralani, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Bugbear)]
        [TestCase(CreatureConstants.Bulette)]
        [TestCase(CreatureConstants.Camel)]
        [TestCase(CreatureConstants.CarrionCrawler)]
        [TestCase(CreatureConstants.Cat)]
        [TestCase(CreatureConstants.Centaur)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Colossal)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Gargantuan)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Huge)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Large)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Medium)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Small)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Tiny)]
        [TestCase(CreatureConstants.Centipede_Swarm)]
        [TestCase(CreatureConstants.ChainDevil_Kyton)]
        [TestCase(CreatureConstants.ChaosBeast)]
        [TestCase(CreatureConstants.Cheetah)]
        [TestCase(CreatureConstants.Chimera, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Choker)]
        [TestCase(CreatureConstants.Chuul)]
        [TestCase(CreatureConstants.Cloaker, "Average Maneuverability")]
        [TestCase(CreatureConstants.Cockatrice, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Couatl, "Good Maneuverability")]
        [TestCase(CreatureConstants.Criosphinx, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Crocodile)]
        [TestCase(CreatureConstants.Crocodile_Giant)]
        [TestCase(CreatureConstants.Cryohydra_10Heads)]
        [TestCase(CreatureConstants.Cryohydra_11Heads)]
        [TestCase(CreatureConstants.Cryohydra_12Heads)]
        [TestCase(CreatureConstants.Cryohydra_5Heads)]
        [TestCase(CreatureConstants.Cryohydra_6Heads)]
        [TestCase(CreatureConstants.Cryohydra_7Heads)]
        [TestCase(CreatureConstants.Cryohydra_8Heads)]
        [TestCase(CreatureConstants.Cryohydra_9Heads)]
        [TestCase(CreatureConstants.Darkmantle, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Deinonychus)]
        [TestCase(CreatureConstants.Delver)]
        [TestCase(CreatureConstants.Derro)]
        [TestCase(CreatureConstants.Destrachan)]
        [TestCase(CreatureConstants.Devourer)]
        [TestCase(CreatureConstants.Digester)]
        [TestCase(CreatureConstants.DisplacerBeast)]
        [TestCase(CreatureConstants.DisplacerBeast_PackLord)]
        [TestCase(CreatureConstants.Djinni, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Djinni_Noble, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Dog)]
        [TestCase(CreatureConstants.Dog_Riding)]
        [TestCase(CreatureConstants.Donkey)]
        [TestCase(CreatureConstants.Doppelganger)]
        [TestCase(CreatureConstants.Dragon_Black_Adult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Black_Ancient, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Black_GreatWyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Black_Juvenile, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Black_MatureAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Black_Old, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Black_VeryOld, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Black_VeryYoung, "Average Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Black_Wyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Black_Wyrmling, "Average Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Black_Young, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Black_YoungAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Blue_Adult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Blue_Ancient, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Blue_GreatWyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Blue_Juvenile, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Blue_MatureAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Blue_Old, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Blue_VeryOld, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Blue_VeryYoung, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrmling, "Average Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Blue_Young, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Blue_YoungAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Brass_Adult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Brass_Ancient, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Brass_GreatWyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Brass_Juvenile, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Brass_MatureAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Brass_Old, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Brass_VeryOld, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Brass_VeryYoung, "Average Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrmling, "Average Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Brass_Young, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Brass_YoungAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Bronze_Adult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Bronze_Ancient, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Bronze_GreatWyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Bronze_Juvenile, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Bronze_MatureAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Bronze_Old, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryOld, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryYoung, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrmling, "Average Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Bronze_Young, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Bronze_YoungAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Copper_Adult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Copper_Ancient, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Copper_GreatWyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Copper_Juvenile, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Copper_MatureAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Copper_Old, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Copper_VeryOld, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Copper_VeryYoung, "Average Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrmling, "Average Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Copper_Young, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Copper_YoungAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Gold_Adult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Gold_Ancient, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Gold_GreatWyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Gold_Juvenile, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Gold_MatureAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Gold_Old, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Gold_VeryOld, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Gold_VeryYoung, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrmling, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Gold_Young, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Gold_YoungAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Green_Adult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Green_Ancient, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Green_GreatWyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Green_Juvenile, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Green_MatureAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Green_Old, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Green_VeryOld, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Green_VeryYoung, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Green_Wyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Green_Wyrmling, "Average Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Green_Young, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Green_YoungAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Red_Adult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Red_Ancient, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Red_GreatWyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Red_Juvenile, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Red_MatureAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Red_Old, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Red_VeryOld, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Red_VeryYoung, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Red_Wyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Red_Wyrmling, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Red_Young, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Red_YoungAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Silver_Adult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Silver_Ancient, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Silver_GreatWyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Silver_Juvenile, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Silver_MatureAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Silver_Old, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Silver_VeryOld, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Silver_VeryYoung, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrmling, "Average Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Silver_Young, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_Silver_YoungAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_White_Adult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_White_Ancient, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_White_GreatWyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_White_Juvenile, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_White_MatureAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_White_Old, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_White_VeryOld, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_White_VeryYoung, "Average Maneuverability")]
        [TestCase(CreatureConstants.Dragon_White_Wyrm, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Dragon_White_Wyrmling, "Average Maneuverability")]
        [TestCase(CreatureConstants.Dragon_White_Young, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dragon_White_YoungAdult, "Poor Maneuverability")]
        [TestCase(CreatureConstants.DragonTurtle)]
        [TestCase(CreatureConstants.Dragonne, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Dretch)]
        [TestCase(CreatureConstants.Drider)]
        [TestCase(CreatureConstants.Dryad)]
        [TestCase(CreatureConstants.Dwarf_Deep)]
        [TestCase(CreatureConstants.Dwarf_Duergar)]
        [TestCase(CreatureConstants.Dwarf_Hill)]
        [TestCase(CreatureConstants.Dwarf_Mountain)]
        [TestCase(CreatureConstants.Eagle, "Average Maneuverability")]
        [TestCase(CreatureConstants.Eagle_Giant, "Average Maneuverability")]
        [TestCase(CreatureConstants.Efreeti, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Elasmosaurus)]
        [TestCase(CreatureConstants.Elemental_Air_Elder, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Elemental_Air_Greater, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Elemental_Air_Huge, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Elemental_Air_Large, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Elemental_Air_Medium, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Elemental_Air_Small, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Elemental_Earth_Elder)]
        [TestCase(CreatureConstants.Elemental_Earth_Greater)]
        [TestCase(CreatureConstants.Elemental_Earth_Huge)]
        [TestCase(CreatureConstants.Elemental_Earth_Large)]
        [TestCase(CreatureConstants.Elemental_Earth_Medium)]
        [TestCase(CreatureConstants.Elemental_Earth_Small)]
        [TestCase(CreatureConstants.Elemental_Fire_Elder)]
        [TestCase(CreatureConstants.Elemental_Fire_Greater)]
        [TestCase(CreatureConstants.Elemental_Fire_Huge)]
        [TestCase(CreatureConstants.Elemental_Fire_Large)]
        [TestCase(CreatureConstants.Elemental_Fire_Medium)]
        [TestCase(CreatureConstants.Elemental_Fire_Small)]
        [TestCase(CreatureConstants.Elemental_Water_Elder)]
        [TestCase(CreatureConstants.Elemental_Water_Greater)]
        [TestCase(CreatureConstants.Elemental_Water_Huge)]
        [TestCase(CreatureConstants.Elemental_Water_Large)]
        [TestCase(CreatureConstants.Elemental_Water_Medium)]
        [TestCase(CreatureConstants.Elemental_Water_Small)]
        [TestCase(CreatureConstants.Elephant)]
        [TestCase(CreatureConstants.Elf_Aquatic)]
        [TestCase(CreatureConstants.Elf_Drow)]
        [TestCase(CreatureConstants.Elf_Gray)]
        [TestCase(CreatureConstants.Elf_Half)]
        [TestCase(CreatureConstants.Elf_High)]
        [TestCase(CreatureConstants.Elf_Wild)]
        [TestCase(CreatureConstants.Elf_Wood)]
        [TestCase(CreatureConstants.Erinyes, "Good Maneuverability")]
        [TestCase(CreatureConstants.EtherealFilcher)]
        [TestCase(CreatureConstants.EtherealMarauder)]
        [TestCase(CreatureConstants.Ettercap)]
        [TestCase(CreatureConstants.Ettin)]
        [TestCase(CreatureConstants.FireBeetle_Giant)]
        [TestCase(CreatureConstants.FormianMyrmarch)]
        [TestCase(CreatureConstants.FormianQueen)]
        [TestCase(CreatureConstants.FormianTaskmaster)]
        [TestCase(CreatureConstants.FormianWarrior)]
        [TestCase(CreatureConstants.FormianWorker)]
        [TestCase(CreatureConstants.FrostWorm)]
        [TestCase(CreatureConstants.Gargoyle, "Average Maneuverability")]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth)]
        [TestCase(CreatureConstants.GelatinousCube)]
        [TestCase(CreatureConstants.Ghaele, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Ghoul)]
        [TestCase(CreatureConstants.Ghoul_Ghast)]
        [TestCase(CreatureConstants.Ghoul_Lacedon)]
        [TestCase(CreatureConstants.Giant_Cloud)]
        [TestCase(CreatureConstants.Giant_Fire)]
        [TestCase(CreatureConstants.Giant_Frost)]
        [TestCase(CreatureConstants.Giant_Hill)]
        [TestCase(CreatureConstants.Giant_Stone)]
        [TestCase(CreatureConstants.Giant_Stone_Elder)]
        [TestCase(CreatureConstants.Giant_Storm)]
        [TestCase(CreatureConstants.GibberingMouther)]
        [TestCase(CreatureConstants.Girallon)]
        [TestCase(CreatureConstants.Githyanki)]
        [TestCase(CreatureConstants.Githzerai)]
        [TestCase(CreatureConstants.Glabrezu)]
        [TestCase(CreatureConstants.Gnoll)]
        [TestCase(CreatureConstants.Gnome_Forest)]
        [TestCase(CreatureConstants.Gnome_Rock)]
        [TestCase(CreatureConstants.Gnome_Svirfneblin)]
        [TestCase(CreatureConstants.Goblin)]
        [TestCase(CreatureConstants.Golem_Clay)]
        [TestCase(CreatureConstants.Golem_Flesh)]
        [TestCase(CreatureConstants.Golem_Iron)]
        [TestCase(CreatureConstants.Golem_Stone)]
        [TestCase(CreatureConstants.Golem_Stone_Greater)]
        [TestCase(CreatureConstants.Gorgon)]
        [TestCase(CreatureConstants.GrayOoze)]
        [TestCase(CreatureConstants.GrayRender)]
        [TestCase(CreatureConstants.GreenHag)]
        [TestCase(CreatureConstants.Grick)]
        [TestCase(CreatureConstants.Griffon, "Average Maneuverability")]
        [TestCase(CreatureConstants.Grig, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Grimlock)]
        [TestCase(CreatureConstants.Gynosphinx, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Halfling_Deep)]
        [TestCase(CreatureConstants.Halfling_Lightfoot)]
        [TestCase(CreatureConstants.Halfling_Tallfellow)]
        [TestCase(CreatureConstants.Harpy, "Average Maneuverability")]
        [TestCase(CreatureConstants.Hawk, "Average Maneuverability")]
        [TestCase(CreatureConstants.HellHound)]
        [TestCase(CreatureConstants.HellHound_NessianWarhound)]
        [TestCase(CreatureConstants.Hellcat_Bezekira)]
        [TestCase(CreatureConstants.Hellwasp_Swarm, "Good Maneuverability")]
        [TestCase(CreatureConstants.Hezrou)]
        [TestCase(CreatureConstants.Hieracosphinx, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Hippogriff, "Average Maneuverability")]
        [TestCase(CreatureConstants.Hobgoblin)]
        [TestCase(CreatureConstants.Homunculus, "Good Maneuverability")]
        [TestCase(CreatureConstants.HornedDevil_Cornugon, "Average Maneuverability")]
        [TestCase(CreatureConstants.Horse_Heavy)]
        [TestCase(CreatureConstants.Horse_Heavy_War)]
        [TestCase(CreatureConstants.Horse_Light)]
        [TestCase(CreatureConstants.Horse_Light_War)]
        [TestCase(CreatureConstants.HoundArchon)]
        [TestCase(CreatureConstants.Howler)]
        [TestCase(CreatureConstants.Human)]
        [TestCase(CreatureConstants.Hydra_10Heads)]
        [TestCase(CreatureConstants.Hydra_11Heads)]
        [TestCase(CreatureConstants.Hydra_12Heads)]
        [TestCase(CreatureConstants.Hydra_5Heads)]
        [TestCase(CreatureConstants.Hydra_6Heads)]
        [TestCase(CreatureConstants.Hydra_7Heads)]
        [TestCase(CreatureConstants.Hydra_8Heads)]
        [TestCase(CreatureConstants.Hydra_9Heads)]
        [TestCase(CreatureConstants.Hyena)]
        [TestCase(CreatureConstants.IceDevil_Gelugon)]
        [TestCase(CreatureConstants.Imp, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.InvisibleStalker, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Janni, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Kobold)]
        [TestCase(CreatureConstants.Kolyarut)]
        [TestCase(CreatureConstants.Kraken)]
        [TestCase(CreatureConstants.Krenshar)]
        [TestCase(CreatureConstants.KuoToa)]
        [TestCase(CreatureConstants.Lamia)]
        [TestCase(CreatureConstants.Lammasu, "Average Maneuverability")]
        [TestCase(CreatureConstants.Lammasu_GoldenProtector, "Average Maneuverability")]
        [TestCase(CreatureConstants.LanternArchon, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Lemure)]
        [TestCase(CreatureConstants.Leonal)]
        [TestCase(CreatureConstants.Leopard)]
        [TestCase(CreatureConstants.Lillend, "Average Maneuverability")]
        [TestCase(CreatureConstants.Lion)]
        [TestCase(CreatureConstants.Lion_Dire)]
        [TestCase(CreatureConstants.Lizard)]
        [TestCase(CreatureConstants.Lizardfolk)]
        [TestCase(CreatureConstants.Lizard_Monitor)]
        [TestCase(CreatureConstants.Locathah)]
        [TestCase(CreatureConstants.Locust_Swarm, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Magmin)]
        [TestCase(CreatureConstants.MantaRay)]
        [TestCase(CreatureConstants.Manticore, "Clumsy Maneuverability")]
        [TestCase(CreatureConstants.Marilith)]
        [TestCase(CreatureConstants.Marut)]
        [TestCase(CreatureConstants.Medusa)]
        [TestCase(CreatureConstants.Megaraptor)]
        [TestCase(CreatureConstants.Merfolk)]
        [TestCase(CreatureConstants.Mephit_Air, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Dust, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Earth, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Fire, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Ice, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Magma, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Ooze, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Salt, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Steam, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mephit_Water, "Average Maneuverability")]
        [TestCase(CreatureConstants.Mimic)]
        [TestCase(CreatureConstants.MindFlayer)]
        [TestCase(CreatureConstants.Minotaur)]
        [TestCase(CreatureConstants.Mohrg)]
        [TestCase(CreatureConstants.Monkey)]
        [TestCase(CreatureConstants.Mule)]
        [TestCase(CreatureConstants.Mummy)]
        [TestCase(CreatureConstants.Naga_Dark)]
        [TestCase(CreatureConstants.Naga_Guardian)]
        [TestCase(CreatureConstants.Naga_Spirit)]
        [TestCase(CreatureConstants.Naga_Water)]
        [TestCase(CreatureConstants.Nalfeshnee, "Poor Maneuverability")]
        [TestCase(CreatureConstants.NightHag)]
        [TestCase(CreatureConstants.Nightcrawler)]
        [TestCase(CreatureConstants.Nightmare, "Good Maneuverability")]
        [TestCase(CreatureConstants.Nightmare_Cauchemar, "Good Maneuverability")]
        [TestCase(CreatureConstants.Nightwalker, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Nightwing, "Good Maneuverability")]
        [TestCase(CreatureConstants.Nixie)]
        [TestCase(CreatureConstants.Nymph)]
        [TestCase(CreatureConstants.OchreJelly)]
        [TestCase(CreatureConstants.Octopus)]
        [TestCase(CreatureConstants.Octopus_Giant)]
        [TestCase(CreatureConstants.Ogre)]
        [TestCase(CreatureConstants.Ogre_Merrow)]
        [TestCase(CreatureConstants.OgreMage, "Good Maneuverability")]
        [TestCase(CreatureConstants.Orc)]
        [TestCase(CreatureConstants.Orc_Half)]
        [TestCase(CreatureConstants.Otyugh)]
        [TestCase(CreatureConstants.Owl, "Average Maneuverability")]
        [TestCase(CreatureConstants.Owl_Giant, "Average Maneuverability")]
        [TestCase(CreatureConstants.Owlbear)]
        [TestCase(CreatureConstants.Pegasus, "Average Maneuverability")]
        [TestCase(CreatureConstants.PhantomFungus)]
        [TestCase(CreatureConstants.PhaseSpider)]
        [TestCase(CreatureConstants.Phasm)]
        [TestCase(CreatureConstants.PitFiend, "Average Maneuverability")]
        [TestCase(CreatureConstants.Pixie, "Good Maneuverability")]
        [TestCase(CreatureConstants.Pixie_WithIrresistableDance, "Good Maneuverability")]
        [TestCase(CreatureConstants.Pony)]
        [TestCase(CreatureConstants.Pony_War)]
        [TestCase(CreatureConstants.Porpoise)]
        [TestCase(CreatureConstants.PrayingMantis_Giant, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Pseudodragon, "Good Maneuverability")]
        [TestCase(CreatureConstants.PurpleWorm)]
        [TestCase(CreatureConstants.Pyrohydra_10Heads)]
        [TestCase(CreatureConstants.Pyrohydra_11Heads)]
        [TestCase(CreatureConstants.Pyrohydra_12Heads)]
        [TestCase(CreatureConstants.Pyrohydra_5Heads)]
        [TestCase(CreatureConstants.Pyrohydra_6Heads)]
        [TestCase(CreatureConstants.Pyrohydra_7Heads)]
        [TestCase(CreatureConstants.Pyrohydra_8Heads)]
        [TestCase(CreatureConstants.Pyrohydra_9Heads)]
        [TestCase(CreatureConstants.Quasit, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Rakshasa)]
        [TestCase(CreatureConstants.Rast, "Good Maneuverability")]
        [TestCase(CreatureConstants.Rat)]
        [TestCase(CreatureConstants.Rat_Dire)]
        [TestCase(CreatureConstants.Rat_Swarm)]
        [TestCase(CreatureConstants.Raven, "Average Maneuverability")]
        [TestCase(CreatureConstants.Ravid, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.RazorBoar)]
        [TestCase(CreatureConstants.Remorhaz)]
        [TestCase(CreatureConstants.Retriever)]
        [TestCase(CreatureConstants.Rhinoceras)]
        [TestCase(CreatureConstants.Roc, "Average Maneuverability")]
        [TestCase(CreatureConstants.Roper)]
        [TestCase(CreatureConstants.RustMonster)]
        [TestCase(CreatureConstants.Sahuagin)]
        [TestCase(CreatureConstants.Salamander_Average)]
        [TestCase(CreatureConstants.Salamander_Flamebrother)]
        [TestCase(CreatureConstants.Salamander_Noble)]
        [TestCase(CreatureConstants.Satyr)]
        [TestCase(CreatureConstants.Satyr_WithPipes)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Colossal)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Gargantuan)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Huge)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Large)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Medium)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Small)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Tiny)]
        [TestCase(CreatureConstants.Scorpionfolk)]
        [TestCase(CreatureConstants.SeaCat)]
        [TestCase(CreatureConstants.SeaHag)]
        [TestCase(CreatureConstants.Shadow, "Good Maneuverability")]
        [TestCase(CreatureConstants.Shadow_Greater, "Good Maneuverability")]
        [TestCase(CreatureConstants.ShadowMastiff)]
        [TestCase(CreatureConstants.ShamblingMound)]
        [TestCase(CreatureConstants.Shark_Dire)]
        [TestCase(CreatureConstants.Shark_Huge)]
        [TestCase(CreatureConstants.Shark_Large)]
        [TestCase(CreatureConstants.Shark_Medium)]
        [TestCase(CreatureConstants.ShieldGuardian)]
        [TestCase(CreatureConstants.ShockerLizard)]
        [TestCase(CreatureConstants.Shrieker)]
        [TestCase(CreatureConstants.Skum)]
        [TestCase(CreatureConstants.Slaad_Blue)]
        [TestCase(CreatureConstants.Slaad_Death)]
        [TestCase(CreatureConstants.Slaad_Gray)]
        [TestCase(CreatureConstants.Slaad_Green)]
        [TestCase(CreatureConstants.Slaad_Red)]
        [TestCase(CreatureConstants.Snake_Constrictor)]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant)]
        [TestCase(CreatureConstants.Snake_Viper_Huge)]
        [TestCase(CreatureConstants.Snake_Viper_Large)]
        [TestCase(CreatureConstants.Snake_Viper_Medium)]
        [TestCase(CreatureConstants.Snake_Viper_Small)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny)]
        [TestCase(CreatureConstants.Spectre, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.Spider_Monstrous_Colossal)]
        [TestCase(CreatureConstants.Spider_Monstrous_Gargantuan)]
        [TestCase(CreatureConstants.Spider_Monstrous_Huge)]
        [TestCase(CreatureConstants.Spider_Monstrous_Large)]
        [TestCase(CreatureConstants.Spider_Monstrous_Medium)]
        [TestCase(CreatureConstants.Spider_Monstrous_Small)]
        [TestCase(CreatureConstants.Spider_Monstrous_Tiny)]
        [TestCase(CreatureConstants.Spider_Swarm)]
        [TestCase(CreatureConstants.SpiderEater, "Good Maneuverability")]
        [TestCase(CreatureConstants.Squid)]
        [TestCase(CreatureConstants.Squid_Giant)]
        [TestCase(CreatureConstants.StagBeetle_Giant)]
        [TestCase(CreatureConstants.Stirge, "Average Maneuverability")]
        [TestCase(CreatureConstants.Succubus, "Average Maneuverability")]
        [TestCase(CreatureConstants.Tarrasque)]
        [TestCase(CreatureConstants.Tendriculos)]
        [TestCase(CreatureConstants.Thoqqua)]
        [TestCase(CreatureConstants.Tiefling)]
        [TestCase(CreatureConstants.Tiger)]
        [TestCase(CreatureConstants.Tiger_Dire)]
        [TestCase(CreatureConstants.Titan)]
        [TestCase(CreatureConstants.Toad)]
        [TestCase(CreatureConstants.Tojanida_Adult)]
        [TestCase(CreatureConstants.Tojanida_Elder)]
        [TestCase(CreatureConstants.Tojanida_Juvenile)]
        [TestCase(CreatureConstants.Treant)]
        [TestCase(CreatureConstants.Triceratops)]
        [TestCase(CreatureConstants.Triton)]
        [TestCase(CreatureConstants.Troglodyte)]
        [TestCase(CreatureConstants.Troll)]
        [TestCase(CreatureConstants.Troll_Scrag)]
        [TestCase(CreatureConstants.TrumpetArchon, "Good Maneuverability")]
        [TestCase(CreatureConstants.Tyrannosaurus)]
        [TestCase(CreatureConstants.UmberHulk)]
        [TestCase(CreatureConstants.UmberHulk_TrulyHorrid)]
        [TestCase(CreatureConstants.Unicorn)]
        [TestCase(CreatureConstants.Unicorn_CelestialCharger)]
        [TestCase(CreatureConstants.VampireSpawn)]
        [TestCase(CreatureConstants.Vargouille, "Good Maneuverability")]
        [TestCase(CreatureConstants.VioletFungus)]
        [TestCase(CreatureConstants.Vrock, "Average Maneuverability")]
        [TestCase(CreatureConstants.Wasp_Giant, "Good Maneuverability")]
        [TestCase(CreatureConstants.Weasel)]
        [TestCase(CreatureConstants.Weasel_Dire)]
        [TestCase(CreatureConstants.Whale_Baleen)]
        [TestCase(CreatureConstants.Whale_Cachalot)]
        [TestCase(CreatureConstants.Whale_Orca)]
        [TestCase(CreatureConstants.Wight)]
        [TestCase(CreatureConstants.WillOWisp, "Perfect Maneuverability")]
        [TestCase(CreatureConstants.WinterWolf)]
        [TestCase(CreatureConstants.Wolf)]
        [TestCase(CreatureConstants.Wolf_Dire)]
        [TestCase(CreatureConstants.Wolverine)]
        [TestCase(CreatureConstants.Wolverine_Dire)]
        [TestCase(CreatureConstants.Worg)]
        [TestCase(CreatureConstants.Wraith, "Good Maneuverability")]
        [TestCase(CreatureConstants.Wraith_Dread, "Good Maneuverability")]
        [TestCase(CreatureConstants.Wyvern, "Poor Maneuverability")]
        [TestCase(CreatureConstants.Xill)]
        [TestCase(CreatureConstants.Xorn_Average)]
        [TestCase(CreatureConstants.Xorn_Elder)]
        [TestCase(CreatureConstants.Xorn_Minor)]
        [TestCase(CreatureConstants.YethHound, "Good Maneuverability")]
        [TestCase(CreatureConstants.Yrthak, "Average Maneuverability")]
        [TestCase(CreatureConstants.YuanTi_Abomination)]
        [TestCase(CreatureConstants.YuanTi_Halfblood)]
        [TestCase(CreatureConstants.YuanTi_Pureblood)]
        [TestCase(CreatureConstants.Zelekhut, "Average Maneuverability")]
        public void AerialManeuverability(string creature, params string[] maneuverability)
        {
            Assert.That(maneuverability.Length, Is.Zero.Or.EqualTo(1), creature);
            DistinctCollection(creature, maneuverability);
        }

        [Test]
        public void AllCreaturesWithAerialSpeedHaveManeuverability()
        {
            var speeds = TypesAndAmountsSelector.SelectAll(TableNameConstants.Set.Collection.Speeds);
            var aerialSpeeds = speeds.Where(kvp => kvp.Value.Any(s => s.Type == SpeedConstants.Fly));
            var aerialCreatures = aerialSpeeds.Select(kvp => kvp.Key);

            AssertCollection(aerialCreatures.Intersect(table.Keys), aerialCreatures);

            foreach (var creature in aerialCreatures)
            {
                var maneuverability = GetCollection(creature);
                Assert.That(maneuverability, Is.Not.Empty, creature);
                Assert.That(maneuverability.Count, Is.EqualTo(1), creature);
                Assert.That(maneuverability.Single(), Is.Not.Empty, creature);
                Assert.That(maneuverability.Single(), Does.EndWith(" Maneuverability"), creature);
            }
        }

        [Test]
        public void AllCreaturesWithoutAerialSpeedHaveNoManeuverability()
        {
            var speeds = TypesAndAmountsSelector.SelectAll(TableNameConstants.Set.Collection.Speeds);
            var aerialSpeeds = speeds.Where(kvp => kvp.Value.Any(s => s.Type == SpeedConstants.Fly));
            var nonAerialSpeeds = speeds.Except(aerialSpeeds);
            var nonAerialCreatures = nonAerialSpeeds.Select(kvp => kvp.Key);

            AssertCollection(nonAerialCreatures.Intersect(table.Keys), nonAerialCreatures);

            foreach (var creature in nonAerialCreatures)
            {
                var maneuverability = GetCollection(creature);
                Assert.That(maneuverability, Is.Empty, creature);
            }
        }
    }
}
