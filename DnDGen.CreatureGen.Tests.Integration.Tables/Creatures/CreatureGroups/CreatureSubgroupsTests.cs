﻿using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures.CreatureGroups
{
    [TestFixture]
    public class CreatureSubgroupsTests : CreatureGroupsTableTests
    {
        [Test]
        public void CreatureGroupNames()
        {
            AssertCreatureGroupNamesAreComplete();
        }

        [TestCase(CreatureConstants.Groups.Angel,
            CreatureConstants.Angel_AstralDeva,
            CreatureConstants.Angel_Planetar,
            CreatureConstants.Angel_Solar)]
        [TestCase(CreatureConstants.Groups.Ant_Giant,
            CreatureConstants.Ant_Giant_Queen,
            CreatureConstants.Ant_Giant_Soldier,
            CreatureConstants.Ant_Giant_Worker)]
        [TestCase(CreatureConstants.Groups.Archon,
            CreatureConstants.HoundArchon,
            CreatureConstants.LanternArchon,
            CreatureConstants.TrumpetArchon)]
        [TestCase(CreatureConstants.Groups.Arrowhawk,
            CreatureConstants.Arrowhawk_Adult,
            CreatureConstants.Arrowhawk_Elder,
            CreatureConstants.Arrowhawk_Juvenile)]
        [TestCase(CreatureConstants.Groups.Bear,
            CreatureConstants.Bear_Black,
            CreatureConstants.Bear_Brown,
            CreatureConstants.Bear_Dire,
            CreatureConstants.Bear_Polar)]
        [TestCase(CreatureConstants.Groups.Centipede_Monstrous,
            CreatureConstants.Centipede_Monstrous_Colossal,
            CreatureConstants.Centipede_Monstrous_Gargantuan,
            CreatureConstants.Centipede_Monstrous_Huge,
            CreatureConstants.Centipede_Monstrous_Large,
            CreatureConstants.Centipede_Monstrous_Medium,
            CreatureConstants.Centipede_Monstrous_Small,
            CreatureConstants.Centipede_Monstrous_Tiny)]
        [TestCase(CreatureConstants.Groups.Cryohydra,
            CreatureConstants.Cryohydra_10Heads,
            CreatureConstants.Cryohydra_11Heads,
            CreatureConstants.Cryohydra_12Heads,
            CreatureConstants.Cryohydra_5Heads,
            CreatureConstants.Cryohydra_6Heads,
            CreatureConstants.Cryohydra_7Heads,
            CreatureConstants.Cryohydra_8Heads,
            CreatureConstants.Cryohydra_9Heads)]
        [TestCase(CreatureConstants.Groups.HalfDragon,
            CreatureConstants.Templates.HalfDragon_Black,
            CreatureConstants.Templates.HalfDragon_Blue,
            CreatureConstants.Templates.HalfDragon_Brass,
            CreatureConstants.Templates.HalfDragon_Bronze,
            CreatureConstants.Templates.HalfDragon_Copper,
            CreatureConstants.Templates.HalfDragon_Gold,
            CreatureConstants.Templates.HalfDragon_Green,
            CreatureConstants.Templates.HalfDragon_Red,
            CreatureConstants.Templates.HalfDragon_Silver,
            CreatureConstants.Templates.HalfDragon_White)]
        [TestCase(CreatureConstants.Groups.Demon,
            CreatureConstants.Babau,
            CreatureConstants.Balor,
            CreatureConstants.Bebilith,
            CreatureConstants.Dretch,
            CreatureConstants.Glabrezu,
            CreatureConstants.Hezrou,
            CreatureConstants.Marilith,
            CreatureConstants.Nalfeshnee,
            CreatureConstants.Quasit,
            CreatureConstants.Retriever,
            CreatureConstants.Succubus,
            CreatureConstants.Vrock)]
        [TestCase(CreatureConstants.Groups.Devil,
            CreatureConstants.BarbedDevil_Hamatula,
            CreatureConstants.BeardedDevil_Barbazu,
            CreatureConstants.BoneDevil_Osyluth,
            CreatureConstants.ChainDevil_Kyton,
            CreatureConstants.Erinyes,
            CreatureConstants.Hellcat_Bezekira,
            CreatureConstants.HornedDevil_Cornugon,
            CreatureConstants.IceDevil_Gelugon,
            CreatureConstants.Imp,
            CreatureConstants.Lemure,
            CreatureConstants.PitFiend)]
        [TestCase(CreatureConstants.Groups.Dinosaur,
            CreatureConstants.Deinonychus,
            CreatureConstants.Elasmosaurus,
            CreatureConstants.Megaraptor,
            CreatureConstants.Triceratops,
            CreatureConstants.Tyrannosaurus)]
        [TestCase(CreatureConstants.Groups.Dragon_Black,
            CreatureConstants.Dragon_Black_Adult,
            CreatureConstants.Dragon_Black_Ancient,
            CreatureConstants.Dragon_Black_GreatWyrm,
            CreatureConstants.Dragon_Black_Juvenile,
            CreatureConstants.Dragon_Black_MatureAdult,
            CreatureConstants.Dragon_Black_Old,
            CreatureConstants.Dragon_Black_VeryOld,
            CreatureConstants.Dragon_Black_VeryYoung,
            CreatureConstants.Dragon_Black_Wyrm,
            CreatureConstants.Dragon_Black_Wyrmling,
            CreatureConstants.Dragon_Black_Young,
            CreatureConstants.Dragon_Black_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Blue,
            CreatureConstants.Dragon_Blue_Adult,
            CreatureConstants.Dragon_Blue_Ancient,
            CreatureConstants.Dragon_Blue_GreatWyrm,
            CreatureConstants.Dragon_Blue_Juvenile,
            CreatureConstants.Dragon_Blue_MatureAdult,
            CreatureConstants.Dragon_Blue_Old,
            CreatureConstants.Dragon_Blue_VeryOld,
            CreatureConstants.Dragon_Blue_VeryYoung,
            CreatureConstants.Dragon_Blue_Wyrm,
            CreatureConstants.Dragon_Blue_Wyrmling,
            CreatureConstants.Dragon_Blue_Young,
            CreatureConstants.Dragon_Blue_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Brass,
            CreatureConstants.Dragon_Brass_Adult,
            CreatureConstants.Dragon_Brass_Ancient,
            CreatureConstants.Dragon_Brass_GreatWyrm,
            CreatureConstants.Dragon_Brass_Juvenile,
            CreatureConstants.Dragon_Brass_MatureAdult,
            CreatureConstants.Dragon_Brass_Old,
            CreatureConstants.Dragon_Brass_VeryOld,
            CreatureConstants.Dragon_Brass_VeryYoung,
            CreatureConstants.Dragon_Brass_Wyrm,
            CreatureConstants.Dragon_Brass_Wyrmling,
            CreatureConstants.Dragon_Brass_Young,
            CreatureConstants.Dragon_Brass_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Bronze,
            CreatureConstants.Dragon_Bronze_Adult,
            CreatureConstants.Dragon_Bronze_Ancient,
            CreatureConstants.Dragon_Bronze_GreatWyrm,
            CreatureConstants.Dragon_Bronze_Juvenile,
            CreatureConstants.Dragon_Bronze_MatureAdult,
            CreatureConstants.Dragon_Bronze_Old,
            CreatureConstants.Dragon_Bronze_VeryOld,
            CreatureConstants.Dragon_Bronze_VeryYoung,
            CreatureConstants.Dragon_Bronze_Wyrm,
            CreatureConstants.Dragon_Bronze_Wyrmling,
            CreatureConstants.Dragon_Bronze_Young,
            CreatureConstants.Dragon_Bronze_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Copper,
            CreatureConstants.Dragon_Copper_Adult,
            CreatureConstants.Dragon_Copper_Ancient,
            CreatureConstants.Dragon_Copper_GreatWyrm,
            CreatureConstants.Dragon_Copper_Juvenile,
            CreatureConstants.Dragon_Copper_MatureAdult,
            CreatureConstants.Dragon_Copper_Old,
            CreatureConstants.Dragon_Copper_VeryOld,
            CreatureConstants.Dragon_Copper_VeryYoung,
            CreatureConstants.Dragon_Copper_Wyrm,
            CreatureConstants.Dragon_Copper_Wyrmling,
            CreatureConstants.Dragon_Copper_Young,
            CreatureConstants.Dragon_Copper_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Gold,
            CreatureConstants.Dragon_Gold_Adult,
            CreatureConstants.Dragon_Gold_Ancient,
            CreatureConstants.Dragon_Gold_GreatWyrm,
            CreatureConstants.Dragon_Gold_Juvenile,
            CreatureConstants.Dragon_Gold_MatureAdult,
            CreatureConstants.Dragon_Gold_Old,
            CreatureConstants.Dragon_Gold_VeryOld,
            CreatureConstants.Dragon_Gold_VeryYoung,
            CreatureConstants.Dragon_Gold_Wyrm,
            CreatureConstants.Dragon_Gold_Wyrmling,
            CreatureConstants.Dragon_Gold_Young,
            CreatureConstants.Dragon_Gold_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Green,
            CreatureConstants.Dragon_Green_Adult,
            CreatureConstants.Dragon_Green_Ancient,
            CreatureConstants.Dragon_Green_GreatWyrm,
            CreatureConstants.Dragon_Green_Juvenile,
            CreatureConstants.Dragon_Green_MatureAdult,
            CreatureConstants.Dragon_Green_Old,
            CreatureConstants.Dragon_Green_VeryOld,
            CreatureConstants.Dragon_Green_VeryYoung,
            CreatureConstants.Dragon_Green_Wyrm,
            CreatureConstants.Dragon_Green_Wyrmling,
            CreatureConstants.Dragon_Green_Young,
            CreatureConstants.Dragon_Green_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Red,
            CreatureConstants.Dragon_Red_Adult,
            CreatureConstants.Dragon_Red_Ancient,
            CreatureConstants.Dragon_Red_GreatWyrm,
            CreatureConstants.Dragon_Red_Juvenile,
            CreatureConstants.Dragon_Red_MatureAdult,
            CreatureConstants.Dragon_Red_Old,
            CreatureConstants.Dragon_Red_VeryOld,
            CreatureConstants.Dragon_Red_VeryYoung,
            CreatureConstants.Dragon_Red_Wyrm,
            CreatureConstants.Dragon_Red_Wyrmling,
            CreatureConstants.Dragon_Red_Young,
            CreatureConstants.Dragon_Red_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_Silver,
            CreatureConstants.Dragon_Silver_Adult,
            CreatureConstants.Dragon_Silver_Ancient,
            CreatureConstants.Dragon_Silver_GreatWyrm,
            CreatureConstants.Dragon_Silver_Juvenile,
            CreatureConstants.Dragon_Silver_MatureAdult,
            CreatureConstants.Dragon_Silver_Old,
            CreatureConstants.Dragon_Silver_VeryOld,
            CreatureConstants.Dragon_Silver_VeryYoung,
            CreatureConstants.Dragon_Silver_Wyrm,
            CreatureConstants.Dragon_Silver_Wyrmling,
            CreatureConstants.Dragon_Silver_Young,
            CreatureConstants.Dragon_Silver_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dragon_White,
            CreatureConstants.Dragon_White_Adult,
            CreatureConstants.Dragon_White_Ancient,
            CreatureConstants.Dragon_White_GreatWyrm,
            CreatureConstants.Dragon_White_Juvenile,
            CreatureConstants.Dragon_White_MatureAdult,
            CreatureConstants.Dragon_White_Old,
            CreatureConstants.Dragon_White_VeryOld,
            CreatureConstants.Dragon_White_VeryYoung,
            CreatureConstants.Dragon_White_Wyrm,
            CreatureConstants.Dragon_White_Wyrmling,
            CreatureConstants.Dragon_White_Young,
            CreatureConstants.Dragon_White_YoungAdult)]
        [TestCase(CreatureConstants.Groups.Dwarf,
            CreatureConstants.Dwarf_Deep,
            CreatureConstants.Dwarf_Duergar,
            CreatureConstants.Dwarf_Hill,
            CreatureConstants.Dwarf_Mountain)]
        [TestCase(CreatureConstants.Groups.Elemental_Air,
            CreatureConstants.Elemental_Air_Elder,
            CreatureConstants.Elemental_Air_Greater,
            CreatureConstants.Elemental_Air_Huge,
            CreatureConstants.Elemental_Air_Large,
            CreatureConstants.Elemental_Air_Medium,
            CreatureConstants.Elemental_Air_Small)]
        [TestCase(CreatureConstants.Groups.Elemental_Earth,
            CreatureConstants.Elemental_Earth_Elder,
            CreatureConstants.Elemental_Earth_Greater,
            CreatureConstants.Elemental_Earth_Huge,
            CreatureConstants.Elemental_Earth_Large,
            CreatureConstants.Elemental_Earth_Medium,
            CreatureConstants.Elemental_Earth_Small)]
        [TestCase(CreatureConstants.Groups.Elemental_Fire,
            CreatureConstants.Elemental_Fire_Elder,
            CreatureConstants.Elemental_Fire_Greater,
            CreatureConstants.Elemental_Fire_Huge,
            CreatureConstants.Elemental_Fire_Large,
            CreatureConstants.Elemental_Fire_Medium,
            CreatureConstants.Elemental_Fire_Small)]
        [TestCase(CreatureConstants.Groups.Elemental_Water,
            CreatureConstants.Elemental_Water_Elder,
            CreatureConstants.Elemental_Water_Greater,
            CreatureConstants.Elemental_Water_Huge,
            CreatureConstants.Elemental_Water_Large,
            CreatureConstants.Elemental_Water_Medium,
            CreatureConstants.Elemental_Water_Small)]
        [TestCase(CreatureConstants.Groups.Elf,
            CreatureConstants.Elf_Aquatic,
            CreatureConstants.Elf_Drow,
            CreatureConstants.Elf_Gray,
            CreatureConstants.Elf_Half,
            CreatureConstants.Elf_High,
            CreatureConstants.Elf_Wild,
            CreatureConstants.Elf_Wood)]
        [TestCase(CreatureConstants.Groups.Formian,
            CreatureConstants.FormianMyrmarch,
            CreatureConstants.FormianQueen,
            CreatureConstants.FormianTaskmaster,
            CreatureConstants.FormianWarrior,
            CreatureConstants.FormianWorker)]
        [TestCase(CreatureConstants.Groups.Fungus,
            CreatureConstants.Shrieker,
            CreatureConstants.VioletFungus)]
        [TestCase(CreatureConstants.Groups.Genie,
            CreatureConstants.Djinni,
            CreatureConstants.Djinni_Noble,
            CreatureConstants.Efreeti,
            CreatureConstants.Janni)]
        [TestCase(CreatureConstants.Groups.Gnome,
            CreatureConstants.Gnome_Forest,
            CreatureConstants.Gnome_Rock,
            CreatureConstants.Gnome_Svirfneblin)]
        [TestCase(CreatureConstants.Groups.Golem,
            CreatureConstants.Golem_Clay,
            CreatureConstants.Golem_Flesh,
            CreatureConstants.Golem_Iron,
            CreatureConstants.Golem_Stone,
            CreatureConstants.Golem_Stone_Greater)]
        [TestCase(CreatureConstants.Groups.Hag,
            CreatureConstants.Annis,
            CreatureConstants.GreenHag,
            CreatureConstants.SeaHag)]
        [TestCase(CreatureConstants.Groups.Halfling,
            CreatureConstants.Halfling_Deep,
            CreatureConstants.Halfling_Lightfoot,
            CreatureConstants.Halfling_Tallfellow)]
        [TestCase(CreatureConstants.Groups.Horse,
            CreatureConstants.Horse_Heavy,
            CreatureConstants.Horse_Heavy_War,
            CreatureConstants.Horse_Light,
            CreatureConstants.Horse_Light_War)]
        [TestCase(CreatureConstants.Groups.Hydra,
            CreatureConstants.Groups.Cryohydra,
            CreatureConstants.Groups.Pyrohydra,
            CreatureConstants.Hydra_10Heads,
            CreatureConstants.Hydra_11Heads,
            CreatureConstants.Hydra_12Heads,
            CreatureConstants.Hydra_5Heads,
            CreatureConstants.Hydra_6Heads,
            CreatureConstants.Hydra_7Heads,
            CreatureConstants.Hydra_8Heads,
            CreatureConstants.Hydra_9Heads)]
        [TestCase(CreatureConstants.Groups.Inevitable,
            CreatureConstants.Kolyarut,
            CreatureConstants.Marut,
            CreatureConstants.Zelekhut)]
        [TestCase(CreatureConstants.Groups.Lycanthrope,
            CreatureConstants.Templates.Lycanthrope_Bear,
            CreatureConstants.Templates.Lycanthrope_Boar,
            CreatureConstants.Templates.Lycanthrope_Rat,
            CreatureConstants.Templates.Lycanthrope_Tiger,
            CreatureConstants.Templates.Lycanthrope_Wolf)]
        [TestCase(CreatureConstants.Groups.Mephit,
            CreatureConstants.Mephit_Air,
            CreatureConstants.Mephit_Dust,
            CreatureConstants.Mephit_Earth,
            CreatureConstants.Mephit_Fire,
            CreatureConstants.Mephit_Ice,
            CreatureConstants.Mephit_Magma,
            CreatureConstants.Mephit_Ooze,
            CreatureConstants.Mephit_Salt,
            CreatureConstants.Mephit_Steam,
            CreatureConstants.Mephit_Water)]
        [TestCase(CreatureConstants.Groups.Naga,
            CreatureConstants.Naga_Dark,
            CreatureConstants.Naga_Guardian,
            CreatureConstants.Naga_Spirit,
            CreatureConstants.Naga_Water)]
        [TestCase(CreatureConstants.Groups.Nightshade,
            CreatureConstants.Nightcrawler,
            CreatureConstants.Nightwalker,
            CreatureConstants.Nightwing)]
        [TestCase(CreatureConstants.Groups.Orc,
            CreatureConstants.Orc,
            CreatureConstants.Orc_Half)]
        [TestCase(CreatureConstants.Groups.Planetouched,
            CreatureConstants.Aasimar,
            CreatureConstants.Tiefling)]
        [TestCase(CreatureConstants.Groups.Pyrohydra,
            CreatureConstants.Pyrohydra_10Heads,
            CreatureConstants.Pyrohydra_11Heads,
            CreatureConstants.Pyrohydra_12Heads,
            CreatureConstants.Pyrohydra_5Heads,
            CreatureConstants.Pyrohydra_6Heads,
            CreatureConstants.Pyrohydra_7Heads,
            CreatureConstants.Pyrohydra_8Heads,
            CreatureConstants.Pyrohydra_9Heads)]
        [TestCase(CreatureConstants.Groups.Salamander,
            CreatureConstants.Salamander_Average,
            CreatureConstants.Salamander_Flamebrother,
            CreatureConstants.Salamander_Noble)]
        [TestCase(CreatureConstants.Groups.Scorpion_Monstrous,
            CreatureConstants.Scorpion_Monstrous_Colossal,
            CreatureConstants.Scorpion_Monstrous_Gargantuan,
            CreatureConstants.Scorpion_Monstrous_Huge,
            CreatureConstants.Scorpion_Monstrous_Large,
            CreatureConstants.Scorpion_Monstrous_Medium,
            CreatureConstants.Scorpion_Monstrous_Small,
            CreatureConstants.Scorpion_Monstrous_Tiny)]
        [TestCase(CreatureConstants.Groups.Shark,
            CreatureConstants.Shark_Dire,
            CreatureConstants.Shark_Medium,
            CreatureConstants.Shark_Large,
            CreatureConstants.Shark_Huge)]
        [TestCase(CreatureConstants.Groups.Slaad,
            CreatureConstants.Slaad_Blue,
            CreatureConstants.Slaad_Death,
            CreatureConstants.Slaad_Gray,
            CreatureConstants.Slaad_Green,
            CreatureConstants.Slaad_Red)]
        [TestCase(CreatureConstants.Groups.Snake_Viper,
            CreatureConstants.Snake_Viper_Huge,
            CreatureConstants.Snake_Viper_Large,
            CreatureConstants.Snake_Viper_Medium,
            CreatureConstants.Snake_Viper_Small,
            CreatureConstants.Snake_Viper_Tiny)]
        [TestCase(CreatureConstants.Groups.Sphinx,
            CreatureConstants.Androsphinx,
            CreatureConstants.Criosphinx,
            CreatureConstants.Gynosphinx,
            CreatureConstants.Hieracosphinx)]
        [TestCase(CreatureConstants.Groups.Spider_Monstrous,
            CreatureConstants.Spider_Monstrous_Hunter_Colossal,
            CreatureConstants.Spider_Monstrous_Hunter_Gargantuan,
            CreatureConstants.Spider_Monstrous_Hunter_Huge,
            CreatureConstants.Spider_Monstrous_Hunter_Large,
            CreatureConstants.Spider_Monstrous_Hunter_Medium,
            CreatureConstants.Spider_Monstrous_Hunter_Small,
            CreatureConstants.Spider_Monstrous_Hunter_Tiny,
            CreatureConstants.Spider_Monstrous_WebSpinner_Colossal,
            CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan,
            CreatureConstants.Spider_Monstrous_WebSpinner_Huge,
            CreatureConstants.Spider_Monstrous_WebSpinner_Large,
            CreatureConstants.Spider_Monstrous_WebSpinner_Medium,
            CreatureConstants.Spider_Monstrous_WebSpinner_Small,
            CreatureConstants.Spider_Monstrous_WebSpinner_Tiny)]
        [TestCase(CreatureConstants.Groups.Sprite,
            CreatureConstants.Grig,
            CreatureConstants.Grig_WithFiddle,
            CreatureConstants.Pixie,
            CreatureConstants.Pixie_WithIrresistibleDance,
            CreatureConstants.Nixie)]
        [TestCase(CreatureConstants.Groups.Tojanida,
            CreatureConstants.Tojanida_Juvenile,
            CreatureConstants.Tojanida_Adult,
            CreatureConstants.Tojanida_Elder)]
        [TestCase(CreatureConstants.Groups.Whale,
            CreatureConstants.Whale_Baleen,
            CreatureConstants.Whale_Cachalot,
            CreatureConstants.Whale_Orca)]
        [TestCase(CreatureConstants.Groups.Xorn,
            CreatureConstants.Xorn_Average,
            CreatureConstants.Xorn_Elder,
            CreatureConstants.Xorn_Minor)]
        [TestCase(CreatureConstants.Groups.YuanTi,
            CreatureConstants.YuanTi_Abomination,
            CreatureConstants.YuanTi_Halfblood_SnakeArms,
            CreatureConstants.YuanTi_Halfblood_SnakeHead,
            CreatureConstants.YuanTi_Halfblood_SnakeTail,
            CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs,
            CreatureConstants.YuanTi_Pureblood)]
        public void CreatureSubgroup(string creature, params string[] subgroup)
        {
            AssertDistinctCollection(creature, subgroup);
        }

        [Test]
        public void AnimatedObjectGroup()
        {
            var entries = new[]
            {
                CreatureConstants.AnimatedObject_Anvil_Colossal,
                CreatureConstants.AnimatedObject_Anvil_Gargantuan,
                CreatureConstants.AnimatedObject_Anvil_Huge,
                CreatureConstants.AnimatedObject_Anvil_Large,
                CreatureConstants.AnimatedObject_Anvil_Medium,
                CreatureConstants.AnimatedObject_Anvil_Small,
                CreatureConstants.AnimatedObject_Anvil_Tiny,
                CreatureConstants.AnimatedObject_Block_Stone_Colossal,
                CreatureConstants.AnimatedObject_Block_Stone_Gargantuan,
                CreatureConstants.AnimatedObject_Block_Stone_Huge,
                CreatureConstants.AnimatedObject_Block_Stone_Large,
                CreatureConstants.AnimatedObject_Block_Stone_Medium,
                CreatureConstants.AnimatedObject_Block_Stone_Small,
                CreatureConstants.AnimatedObject_Block_Stone_Tiny,
                CreatureConstants.AnimatedObject_Block_Wood_Colossal,
                CreatureConstants.AnimatedObject_Block_Wood_Gargantuan,
                CreatureConstants.AnimatedObject_Block_Wood_Huge,
                CreatureConstants.AnimatedObject_Block_Wood_Large,
                CreatureConstants.AnimatedObject_Block_Wood_Medium,
                CreatureConstants.AnimatedObject_Block_Wood_Small,
                CreatureConstants.AnimatedObject_Block_Wood_Tiny,
                CreatureConstants.AnimatedObject_Box_Colossal,
                CreatureConstants.AnimatedObject_Box_Gargantuan,
                CreatureConstants.AnimatedObject_Box_Huge,
                CreatureConstants.AnimatedObject_Box_Large,
                CreatureConstants.AnimatedObject_Box_Medium,
                CreatureConstants.AnimatedObject_Box_Small,
                CreatureConstants.AnimatedObject_Box_Tiny,
                CreatureConstants.AnimatedObject_Carpet_Colossal,
                CreatureConstants.AnimatedObject_Carpet_Gargantuan,
                CreatureConstants.AnimatedObject_Carpet_Huge,
                CreatureConstants.AnimatedObject_Carpet_Large,
                CreatureConstants.AnimatedObject_Carpet_Medium,
                CreatureConstants.AnimatedObject_Carpet_Small,
                CreatureConstants.AnimatedObject_Carpet_Tiny,
                CreatureConstants.AnimatedObject_Carriage_Colossal,
                CreatureConstants.AnimatedObject_Carriage_Gargantuan,
                CreatureConstants.AnimatedObject_Carriage_Huge,
                CreatureConstants.AnimatedObject_Carriage_Large,
                CreatureConstants.AnimatedObject_Carriage_Medium,
                CreatureConstants.AnimatedObject_Carriage_Small,
                CreatureConstants.AnimatedObject_Carriage_Tiny,
                CreatureConstants.AnimatedObject_Chain_Colossal,
                CreatureConstants.AnimatedObject_Chain_Gargantuan,
                CreatureConstants.AnimatedObject_Chain_Huge,
                CreatureConstants.AnimatedObject_Chain_Large,
                CreatureConstants.AnimatedObject_Chain_Medium,
                CreatureConstants.AnimatedObject_Chain_Small,
                CreatureConstants.AnimatedObject_Chain_Tiny,
                CreatureConstants.AnimatedObject_Chair_Colossal,
                CreatureConstants.AnimatedObject_Chair_Gargantuan,
                CreatureConstants.AnimatedObject_Chair_Huge,
                CreatureConstants.AnimatedObject_Chair_Large,
                CreatureConstants.AnimatedObject_Chair_Medium,
                CreatureConstants.AnimatedObject_Chair_Small,
                CreatureConstants.AnimatedObject_Chair_Tiny,
                CreatureConstants.AnimatedObject_Clothes_Colossal,
                CreatureConstants.AnimatedObject_Clothes_Gargantuan,
                CreatureConstants.AnimatedObject_Clothes_Huge,
                CreatureConstants.AnimatedObject_Clothes_Large,
                CreatureConstants.AnimatedObject_Clothes_Medium,
                CreatureConstants.AnimatedObject_Clothes_Small,
                CreatureConstants.AnimatedObject_Clothes_Tiny,
                CreatureConstants.AnimatedObject_Ladder_Colossal,
                CreatureConstants.AnimatedObject_Ladder_Gargantuan,
                CreatureConstants.AnimatedObject_Ladder_Huge,
                CreatureConstants.AnimatedObject_Ladder_Large,
                CreatureConstants.AnimatedObject_Ladder_Medium,
                CreatureConstants.AnimatedObject_Ladder_Small,
                CreatureConstants.AnimatedObject_Ladder_Tiny,
                CreatureConstants.AnimatedObject_Rope_Colossal,
                CreatureConstants.AnimatedObject_Rope_Gargantuan,
                CreatureConstants.AnimatedObject_Rope_Huge,
                CreatureConstants.AnimatedObject_Rope_Large,
                CreatureConstants.AnimatedObject_Rope_Medium,
                CreatureConstants.AnimatedObject_Rope_Small,
                CreatureConstants.AnimatedObject_Rope_Tiny,
                CreatureConstants.AnimatedObject_Rug_Colossal,
                CreatureConstants.AnimatedObject_Rug_Gargantuan,
                CreatureConstants.AnimatedObject_Rug_Huge,
                CreatureConstants.AnimatedObject_Rug_Large,
                CreatureConstants.AnimatedObject_Rug_Medium,
                CreatureConstants.AnimatedObject_Rug_Small,
                CreatureConstants.AnimatedObject_Rug_Tiny,
                CreatureConstants.AnimatedObject_Sled_Colossal,
                CreatureConstants.AnimatedObject_Sled_Gargantuan,
                CreatureConstants.AnimatedObject_Sled_Huge,
                CreatureConstants.AnimatedObject_Sled_Large,
                CreatureConstants.AnimatedObject_Sled_Medium,
                CreatureConstants.AnimatedObject_Sled_Small,
                CreatureConstants.AnimatedObject_Sled_Tiny,
                CreatureConstants.AnimatedObject_Statue_Animal_Colossal,
                CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan,
                CreatureConstants.AnimatedObject_Statue_Animal_Huge,
                CreatureConstants.AnimatedObject_Statue_Animal_Large,
                CreatureConstants.AnimatedObject_Statue_Animal_Medium,
                CreatureConstants.AnimatedObject_Statue_Animal_Small,
                CreatureConstants.AnimatedObject_Statue_Animal_Tiny,
                CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal,
                CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan,
                CreatureConstants.AnimatedObject_Statue_Humanoid_Huge,
                CreatureConstants.AnimatedObject_Statue_Humanoid_Large,
                CreatureConstants.AnimatedObject_Statue_Humanoid_Medium,
                CreatureConstants.AnimatedObject_Statue_Humanoid_Small,
                CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny,
                CreatureConstants.AnimatedObject_Stool_Colossal,
                CreatureConstants.AnimatedObject_Stool_Gargantuan,
                CreatureConstants.AnimatedObject_Stool_Huge,
                CreatureConstants.AnimatedObject_Stool_Large,
                CreatureConstants.AnimatedObject_Stool_Medium,
                CreatureConstants.AnimatedObject_Stool_Small,
                CreatureConstants.AnimatedObject_Stool_Tiny,
                CreatureConstants.AnimatedObject_Table_Colossal,
                CreatureConstants.AnimatedObject_Table_Gargantuan,
                CreatureConstants.AnimatedObject_Table_Huge,
                CreatureConstants.AnimatedObject_Table_Large,
                CreatureConstants.AnimatedObject_Table_Medium,
                CreatureConstants.AnimatedObject_Table_Small,
                CreatureConstants.AnimatedObject_Table_Tiny,
                CreatureConstants.AnimatedObject_Tapestry_Colossal,
                CreatureConstants.AnimatedObject_Tapestry_Gargantuan,
                CreatureConstants.AnimatedObject_Tapestry_Huge,
                CreatureConstants.AnimatedObject_Tapestry_Large,
                CreatureConstants.AnimatedObject_Tapestry_Medium,
                CreatureConstants.AnimatedObject_Tapestry_Small,
                CreatureConstants.AnimatedObject_Tapestry_Tiny,
                CreatureConstants.AnimatedObject_Wagon_Colossal,
                CreatureConstants.AnimatedObject_Wagon_Gargantuan,
                CreatureConstants.AnimatedObject_Wagon_Huge,
                CreatureConstants.AnimatedObject_Wagon_Large,
                CreatureConstants.AnimatedObject_Wagon_Medium,
                CreatureConstants.AnimatedObject_Wagon_Small,
                CreatureConstants.AnimatedObject_Wagon_Tiny
            };

            AssertDistinctCollection(CreatureConstants.Groups.AnimatedObject, entries);
        }

        [Test]
        public void HasSkeletonGroup()
        {
            var entries = new[]
            {
                //Aberration
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
                CreatureConstants.MindFlayer,
                CreatureConstants.Groups.Naga,
                CreatureConstants.Otyugh,
                CreatureConstants.RustMonster,
                CreatureConstants.Skum,
                CreatureConstants.UmberHulk,
                CreatureConstants.UmberHulk_TrulyHorrid,
                
                //Animal
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
                CreatureConstants.Owl,
                CreatureConstants.Pony,
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

                //Magical Beasts
                CreatureConstants.Ankheg,
                CreatureConstants.Aranea,
                CreatureConstants.Basilisk,
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
                CreatureConstants.Girallon,
                CreatureConstants.Gorgon,
                CreatureConstants.GrayRender,
                CreatureConstants.Griffon,
                CreatureConstants.Hippogriff,
                CreatureConstants.Groups.Hydra,
                CreatureConstants.Krenshar,
                CreatureConstants.Lamia,
                CreatureConstants.Lammasu,
                CreatureConstants.Manticore,
                CreatureConstants.Owl_Giant,
                CreatureConstants.Owlbear,
                CreatureConstants.Pegasus,
                CreatureConstants.RazorBoar,
                CreatureConstants.SeaCat,
                CreatureConstants.ShockerLizard,
                CreatureConstants.Androsphinx,
                CreatureConstants.Criosphinx,
                CreatureConstants.Gynosphinx,
                CreatureConstants.Hieracosphinx,
                CreatureConstants.Tarrasque,
                CreatureConstants.Unicorn,
                CreatureConstants.WinterWolf,
                CreatureConstants.Worg,
                CreatureConstants.Yrthak,

                CreatureConstants.Types.Dragon,
                CreatureConstants.Types.Fey,
                CreatureConstants.Types.Giant,
                CreatureConstants.Types.Humanoid,
                CreatureConstants.Types.MonstrousHumanoid,
            };

            AssertDistinctCollection(CreatureConstants.Groups.HasSkeleton, entries);
        }

        [Test]
        public void NoCircularSubgroups()
        {
            foreach (var kvp in table)
            {
                AssertGroupDoesNotContain(kvp.Key, kvp.Key);
            }
        }

        private void AssertGroupDoesNotContain(string name, string forbiddenEntry)
        {
            var group = table[name];

            //INFO: A group is allowed to contain itself as an immediate child
            //Example is the Orc subtype group containing the Orc creature
            if (name != forbiddenEntry)
                Assert.That(group, Does.Not.Contain(forbiddenEntry), name);

            //INFO: Remove the name from the group, or we get infinite recursion traversing itself as a subgroup
            var subgroupNames = group.Intersect(table.Keys).Except(new[] { name });

            foreach (var subgroupName in subgroupNames)
            {
                AssertGroupDoesNotContain(subgroupName, forbiddenEntry);
                AssertGroupDoesNotContain(subgroupName, subgroupName);
            }
        }
    }
}
