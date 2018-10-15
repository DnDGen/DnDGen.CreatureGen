using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Tables.Defenses
{
    public class SaveBonusesTestData
    {
        public const string None = "NONE";

        public static IEnumerable Creatures
        {
            get
            {
                var testCases = new Dictionary<string, Dictionary<string, int>>();
                var creatures = CreatureConstants.All();

                foreach (var creature in creatures)
                {
                    testCases[creature] = new Dictionary<string, int>();
                }

                testCases[CreatureConstants.Aasimar][None] = 0;

                testCases[CreatureConstants.Aboleth][None] = 0;

                testCases[CreatureConstants.Achaierai][None] = 0;

                testCases[CreatureConstants.Allip][None] = 0;

                testCases[CreatureConstants.Androsphinx][None] = 0;

                testCases[CreatureConstants.Angel_AstralDeva][None] = 0;

                testCases[CreatureConstants.Angel_Planetar][None] = 0;

                testCases[CreatureConstants.Angel_Solar][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Tiny][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Small][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Medium][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Large][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Huge][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Gargantuan][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Colossal][None] = 0;

                testCases[CreatureConstants.Ankheg][None] = 0;

                testCases[CreatureConstants.Annis][None] = 0;

                testCases[CreatureConstants.Ant_Giant_Queen][None] = 0;

                testCases[CreatureConstants.Ant_Giant_Soldier][None] = 0;

                testCases[CreatureConstants.Ant_Giant_Worker][None] = 0;

                testCases[CreatureConstants.Ape][None] = 0;

                testCases[CreatureConstants.Ape_Dire][None] = 0;

                testCases[CreatureConstants.Aranea][None] = 0;

                testCases[CreatureConstants.Arrowhawk_Juvenile][None] = 0;

                testCases[CreatureConstants.Arrowhawk_Adult][None] = 0;

                testCases[CreatureConstants.Arrowhawk_Elder][None] = 0;

                testCases[CreatureConstants.AssassinVine][None] = 0;

                testCases[CreatureConstants.Athach][None] = 0;

                testCases[CreatureConstants.Avoral][None] = 0;

                testCases[CreatureConstants.Azer][None] = 0;

                testCases[CreatureConstants.Babau][None] = 0;

                testCases[CreatureConstants.Baboon][None] = 0;

                testCases[CreatureConstants.Badger][None] = 0;

                testCases[CreatureConstants.Badger_Dire][None] = 0;

                testCases[CreatureConstants.Balor][None] = 0;

                testCases[CreatureConstants.BarbedDevil_Hamatula][None] = 0;

                testCases[CreatureConstants.Barghest][None] = 0;

                testCases[CreatureConstants.Barghest_Greater][None] = 0;

                testCases[CreatureConstants.Basilisk][None] = 0;

                testCases[CreatureConstants.Basilisk_AbyssalGreater][None] = 0;

                testCases[CreatureConstants.Bat][None] = 0;

                testCases[CreatureConstants.Bat_Dire][None] = 0;

                testCases[CreatureConstants.Bat_Swarm][None] = 0;

                testCases[CreatureConstants.Bear_Black][None] = 0;

                testCases[CreatureConstants.Bear_Brown][None] = 0;

                testCases[CreatureConstants.Bear_Dire][None] = 0;

                testCases[CreatureConstants.BeardedDevil_Barbazu][None] = 0;

                testCases[CreatureConstants.Bebilith][None] = 0;

                testCases[CreatureConstants.Bee_Giant][None] = 0;

                testCases[CreatureConstants.Behir][None] = 0;

                testCases[CreatureConstants.Beholder][None] = 0;

                testCases[CreatureConstants.Beholder_Gauth][None] = 0;

                testCases[CreatureConstants.Belker][None] = 0;

                testCases[CreatureConstants.Bison][None] = 0;

                testCases[CreatureConstants.BlackPudding][None] = 0;

                testCases[CreatureConstants.BlackPudding_Elder][None] = 0;

                testCases[CreatureConstants.BlinkDog][None] = 0;

                testCases[CreatureConstants.Boar][None] = 0;

                testCases[CreatureConstants.Boar_Dire][None] = 0;

                testCases[CreatureConstants.Bodak][None] = 0;

                testCases[CreatureConstants.BombardierBeetle_Giant][None] = 0;

                testCases[CreatureConstants.BoneDevil_Osyluth][None] = 0;

                testCases[CreatureConstants.Bralani][None] = 0;

                testCases[CreatureConstants.Bugbear][None] = 0;

                testCases[CreatureConstants.Bulette][None] = 0;

                testCases[CreatureConstants.Camel_Bactrian][None] = 0;

                testCases[CreatureConstants.Camel_Dromedary][None] = 0;

                testCases[CreatureConstants.CarrionCrawler][None] = 0;

                testCases[CreatureConstants.Cat][None] = 0;

                testCases[CreatureConstants.Centaur][None] = 0;

                testCases[CreatureConstants.Centipede_Swarm][None] = 0;

                testCases[CreatureConstants.ChainDevil_Kyton][None] = 0;

                testCases[CreatureConstants.ChaosBeast][None] = 0;

                testCases[CreatureConstants.Cheetah][None] = 0;

                testCases[CreatureConstants.Chimera][None] = 0;

                testCases[CreatureConstants.Choker][None] = 0;

                testCases[CreatureConstants.Chuul][None] = 0;

                testCases[CreatureConstants.Cloaker][None] = 0;

                testCases[CreatureConstants.Cockatrice][None] = 0;

                testCases[CreatureConstants.Couatl][None] = 0;

                testCases[CreatureConstants.Criosphinx][None] = 0;

                testCases[CreatureConstants.Crocodile][None] = 0;

                testCases[CreatureConstants.Crocodile_Giant][None] = 0;

                testCases[CreatureConstants.Cryohydra_5Heads][None] = 0;

                testCases[CreatureConstants.Cryohydra_6Heads][None] = 0;

                testCases[CreatureConstants.Cryohydra_7Heads][None] = 0;

                testCases[CreatureConstants.Cryohydra_8Heads][None] = 0;

                testCases[CreatureConstants.Cryohydra_9Heads][None] = 0;

                testCases[CreatureConstants.Cryohydra_10Heads][None] = 0;

                testCases[CreatureConstants.Cryohydra_11Heads][None] = 0;

                testCases[CreatureConstants.Cryohydra_12Heads][None] = 0;

                testCases[CreatureConstants.Darkmantle][None] = 0;

                testCases[CreatureConstants.Deinonychus][None] = 0;

                testCases[CreatureConstants.Delver][None] = 0;

                testCases[CreatureConstants.Derro][None] = 0;

                testCases[CreatureConstants.Derro_Sane][None] = 0;

                testCases[CreatureConstants.Destrachan][None] = 0;

                testCases[CreatureConstants.Devourer][None] = 0;

                testCases[CreatureConstants.Digester][None] = 0;

                testCases[CreatureConstants.DisplacerBeast][GetData(GroupConstants.All, "ranged magical attacks that specifically target the displacer beast, except for ranged touch attacks")] = 2;

                testCases[CreatureConstants.DisplacerBeast_PackLord][GetData(GroupConstants.All, "ranged magical attacks that specifically target the displacer beast, except for ranged touch attacks")] = 2;

                testCases[CreatureConstants.Djinni][None] = 0;

                testCases[CreatureConstants.Djinni_Noble][None] = 0;

                testCases[CreatureConstants.Dog][None] = 0;

                testCases[CreatureConstants.Dog_Riding][None] = 0;

                testCases[CreatureConstants.Donkey][None] = 0;

                testCases[CreatureConstants.Doppelganger][None] = 0;

                testCases[CreatureConstants.Dragon_Black_Wyrmling][None] = 0;

                testCases[CreatureConstants.Dragon_Black_VeryYoung][None] = 0;

                testCases[CreatureConstants.Dragon_Black_Young][None] = 0;

                testCases[CreatureConstants.Dragon_Black_Juvenile][None] = 0;

                testCases[CreatureConstants.Dragon_Black_YoungAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Black_Adult][None] = 0;

                testCases[CreatureConstants.Dragon_Black_MatureAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Black_Old][None] = 0;

                testCases[CreatureConstants.Dragon_Black_VeryOld][None] = 0;

                testCases[CreatureConstants.Dragon_Black_Ancient][None] = 0;

                testCases[CreatureConstants.Dragon_Black_Wyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Black_GreatWyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Blue_Wyrmling][None] = 0;

                testCases[CreatureConstants.Dragon_Blue_VeryYoung][None] = 0;

                testCases[CreatureConstants.Dragon_Blue_Young][None] = 0;

                testCases[CreatureConstants.Dragon_Blue_Juvenile][None] = 0;

                testCases[CreatureConstants.Dragon_Blue_YoungAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Blue_Adult][None] = 0;

                testCases[CreatureConstants.Dragon_Blue_MatureAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Blue_Old][None] = 0;

                testCases[CreatureConstants.Dragon_Blue_VeryOld][None] = 0;

                testCases[CreatureConstants.Dragon_Blue_Ancient][None] = 0;

                testCases[CreatureConstants.Dragon_Blue_Wyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Blue_GreatWyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Green_Wyrmling][None] = 0;

                testCases[CreatureConstants.Dragon_Green_VeryYoung][None] = 0;

                testCases[CreatureConstants.Dragon_Green_Young][None] = 0;

                testCases[CreatureConstants.Dragon_Green_Juvenile][None] = 0;

                testCases[CreatureConstants.Dragon_Green_YoungAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Green_Adult][None] = 0;

                testCases[CreatureConstants.Dragon_Green_MatureAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Green_Old][None] = 0;

                testCases[CreatureConstants.Dragon_Green_VeryOld][None] = 0;

                testCases[CreatureConstants.Dragon_Green_Ancient][None] = 0;

                testCases[CreatureConstants.Dragon_Green_Wyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Green_GreatWyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Red_Wyrmling][None] = 0;

                testCases[CreatureConstants.Dragon_Red_VeryYoung][None] = 0;

                testCases[CreatureConstants.Dragon_Red_Young][None] = 0;

                testCases[CreatureConstants.Dragon_Red_Juvenile][None] = 0;

                testCases[CreatureConstants.Dragon_Red_YoungAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Red_Adult][None] = 0;

                testCases[CreatureConstants.Dragon_Red_MatureAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Red_Old][None] = 0;

                testCases[CreatureConstants.Dragon_Red_VeryOld][None] = 0;

                testCases[CreatureConstants.Dragon_Red_Ancient][None] = 0;

                testCases[CreatureConstants.Dragon_Red_Wyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Red_GreatWyrm][None] = 0;

                testCases[CreatureConstants.Dragon_White_Wyrmling][None] = 0;

                testCases[CreatureConstants.Dragon_White_VeryYoung][None] = 0;

                testCases[CreatureConstants.Dragon_White_Young][None] = 0;

                testCases[CreatureConstants.Dragon_White_Juvenile][None] = 0;

                testCases[CreatureConstants.Dragon_White_YoungAdult][None] = 0;

                testCases[CreatureConstants.Dragon_White_Adult][None] = 0;

                testCases[CreatureConstants.Dragon_White_MatureAdult][None] = 0;

                testCases[CreatureConstants.Dragon_White_Old][None] = 0;

                testCases[CreatureConstants.Dragon_White_VeryOld][None] = 0;

                testCases[CreatureConstants.Dragon_White_Ancient][None] = 0;

                testCases[CreatureConstants.Dragon_White_Wyrm][None] = 0;

                testCases[CreatureConstants.Dragon_White_GreatWyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Brass_Wyrmling][None] = 0;

                testCases[CreatureConstants.Dragon_Brass_VeryYoung][None] = 0;

                testCases[CreatureConstants.Dragon_Brass_Young][None] = 0;

                testCases[CreatureConstants.Dragon_Brass_Juvenile][None] = 0;

                testCases[CreatureConstants.Dragon_Brass_YoungAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Brass_Adult][None] = 0;

                testCases[CreatureConstants.Dragon_Brass_MatureAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Brass_Old][None] = 0;

                testCases[CreatureConstants.Dragon_Brass_VeryOld][None] = 0;

                testCases[CreatureConstants.Dragon_Brass_Ancient][None] = 0;

                testCases[CreatureConstants.Dragon_Brass_Wyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Brass_GreatWyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Bronze_Wyrmling][None] = 0;

                testCases[CreatureConstants.Dragon_Bronze_VeryYoung][None] = 0;

                testCases[CreatureConstants.Dragon_Bronze_Young][None] = 0;

                testCases[CreatureConstants.Dragon_Bronze_Juvenile][None] = 0;

                testCases[CreatureConstants.Dragon_Bronze_YoungAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Bronze_Adult][None] = 0;

                testCases[CreatureConstants.Dragon_Bronze_MatureAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Bronze_Old][None] = 0;

                testCases[CreatureConstants.Dragon_Bronze_VeryOld][None] = 0;

                testCases[CreatureConstants.Dragon_Bronze_Ancient][None] = 0;

                testCases[CreatureConstants.Dragon_Bronze_Wyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Copper_Wyrmling][None] = 0;

                testCases[CreatureConstants.Dragon_Copper_VeryYoung][None] = 0;

                testCases[CreatureConstants.Dragon_Copper_Young][None] = 0;

                testCases[CreatureConstants.Dragon_Copper_Juvenile][None] = 0;

                testCases[CreatureConstants.Dragon_Copper_YoungAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Copper_Adult][None] = 0;

                testCases[CreatureConstants.Dragon_Copper_MatureAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Copper_Old][None] = 0;

                testCases[CreatureConstants.Dragon_Copper_VeryOld][None] = 0;

                testCases[CreatureConstants.Dragon_Copper_Ancient][None] = 0;

                testCases[CreatureConstants.Dragon_Copper_Wyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Copper_GreatWyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Gold_Wyrmling][None] = 0;

                testCases[CreatureConstants.Dragon_Gold_VeryYoung][None] = 0;

                testCases[CreatureConstants.Dragon_Gold_Young][None] = 0;

                testCases[CreatureConstants.Dragon_Gold_Juvenile][None] = 0;

                testCases[CreatureConstants.Dragon_Gold_YoungAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Gold_Adult][None] = 0;

                testCases[CreatureConstants.Dragon_Gold_MatureAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Gold_Old][None] = 0;

                testCases[CreatureConstants.Dragon_Gold_VeryOld][None] = 0;

                testCases[CreatureConstants.Dragon_Gold_Ancient][None] = 0;

                testCases[CreatureConstants.Dragon_Gold_Wyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Gold_GreatWyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Silver_Wyrmling][None] = 0;

                testCases[CreatureConstants.Dragon_Silver_VeryYoung][None] = 0;

                testCases[CreatureConstants.Dragon_Silver_Young][None] = 0;

                testCases[CreatureConstants.Dragon_Silver_Juvenile][None] = 0;

                testCases[CreatureConstants.Dragon_Silver_YoungAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Silver_Adult][None] = 0;

                testCases[CreatureConstants.Dragon_Silver_MatureAdult][None] = 0;

                testCases[CreatureConstants.Dragon_Silver_Old][None] = 0;

                testCases[CreatureConstants.Dragon_Silver_VeryOld][None] = 0;

                testCases[CreatureConstants.Dragon_Silver_Ancient][None] = 0;

                testCases[CreatureConstants.Dragon_Silver_Wyrm][None] = 0;

                testCases[CreatureConstants.Dragon_Silver_GreatWyrm][None] = 0;

                testCases[CreatureConstants.DragonTurtle][None] = 0;

                testCases[CreatureConstants.Dragonne][None] = 0;

                testCases[CreatureConstants.Dretch][None] = 0;

                testCases[CreatureConstants.Drider][None] = 0;

                testCases[CreatureConstants.Dryad][None] = 0;

                testCases[CreatureConstants.Dwarf_Deep][GetData(SaveConstants.Fortitude, "against poison")] = 3;
                testCases[CreatureConstants.Dwarf_Deep][GetData(GroupConstants.All, "against spells and spell-like abilities")] = 3;

                testCases[CreatureConstants.Dwarf_Duergar][GetData(GroupConstants.All, "against spells and spell-like abilities")] = 2;

                testCases[CreatureConstants.Dwarf_Hill][GetData(SaveConstants.Fortitude, "against poison")] = 2;
                testCases[CreatureConstants.Dwarf_Hill][GetData(GroupConstants.All, "against spells and spell-like abilities")] = 2;

                testCases[CreatureConstants.Dwarf_Mountain][GetData(SaveConstants.Fortitude, "against poison")] = 2;
                testCases[CreatureConstants.Dwarf_Mountain][GetData(GroupConstants.All, "against spells and spell-like abilities")] = 2;

                testCases[CreatureConstants.Eagle][None] = 0;

                testCases[CreatureConstants.Eagle_Giant][None] = 0;

                testCases[CreatureConstants.Efreeti][None] = 0;

                testCases[CreatureConstants.Elasmosaurus][None] = 0;

                testCases[CreatureConstants.Elemental_Air_Small][None] = 0;

                testCases[CreatureConstants.Elemental_Air_Medium][None] = 0;

                testCases[CreatureConstants.Elemental_Air_Large][None] = 0;

                testCases[CreatureConstants.Elemental_Air_Huge][None] = 0;

                testCases[CreatureConstants.Elemental_Air_Greater][None] = 0;

                testCases[CreatureConstants.Elemental_Air_Elder][None] = 0;

                testCases[CreatureConstants.Elemental_Earth_Small][None] = 0;

                testCases[CreatureConstants.Elemental_Earth_Medium][None] = 0;

                testCases[CreatureConstants.Elemental_Earth_Large][None] = 0;

                testCases[CreatureConstants.Elemental_Earth_Huge][None] = 0;

                testCases[CreatureConstants.Elemental_Earth_Greater][None] = 0;

                testCases[CreatureConstants.Elemental_Earth_Elder][None] = 0;

                testCases[CreatureConstants.Elemental_Fire_Small][None] = 0;

                testCases[CreatureConstants.Elemental_Fire_Medium][None] = 0;

                testCases[CreatureConstants.Elemental_Fire_Large][None] = 0;

                testCases[CreatureConstants.Elemental_Fire_Huge][None] = 0;

                testCases[CreatureConstants.Elemental_Fire_Greater][None] = 0;

                testCases[CreatureConstants.Elemental_Fire_Elder][None] = 0;

                testCases[CreatureConstants.Elemental_Water_Small][None] = 0;

                testCases[CreatureConstants.Elemental_Water_Medium][None] = 0;

                testCases[CreatureConstants.Elemental_Water_Large][None] = 0;

                testCases[CreatureConstants.Elemental_Water_Huge][None] = 0;

                testCases[CreatureConstants.Elemental_Water_Greater][None] = 0;

                testCases[CreatureConstants.Elemental_Water_Elder][None] = 0;

                testCases[CreatureConstants.Elephant][None] = 0;

                testCases[CreatureConstants.Elf_Aquatic][None] = 0;

                testCases[CreatureConstants.Elf_Drow][GetData(SaveConstants.Will, "spells and spell-like abilities")] = 2;

                testCases[CreatureConstants.Elf_Gray][None] = 0;

                testCases[CreatureConstants.Elf_Half][None] = 0;

                testCases[CreatureConstants.Elf_High][None] = 0;

                testCases[CreatureConstants.Elf_Wild][None] = 0;

                testCases[CreatureConstants.Elf_Wood][None] = 0;

                testCases[CreatureConstants.Erinyes][None] = 0;

                testCases[CreatureConstants.EtherealFilcher][None] = 0;

                testCases[CreatureConstants.EtherealMarauder][None] = 0;

                testCases[CreatureConstants.Ettercap][None] = 0;

                testCases[CreatureConstants.Ettin][None] = 0;

                testCases[CreatureConstants.FireBeetle_Giant][None] = 0;

                testCases[CreatureConstants.FormianMyrmarch][None] = 0;

                testCases[CreatureConstants.FormianQueen][None] = 0;

                testCases[CreatureConstants.FormianTaskmaster][None] = 0;

                testCases[CreatureConstants.FormianWarrior][None] = 0;

                testCases[CreatureConstants.FormianWorker][None] = 0;

                testCases[CreatureConstants.FrostWorm][None] = 0;

                testCases[CreatureConstants.Gargoyle][None] = 0;

                testCases[CreatureConstants.Gargoyle_Kapoacinth][None] = 0;

                testCases[CreatureConstants.GelatinousCube][None] = 0;

                testCases[CreatureConstants.Ghaele][None] = 0;

                testCases[CreatureConstants.Ghoul][None] = 0;

                testCases[CreatureConstants.Ghoul_Ghast][None] = 0;

                testCases[CreatureConstants.Ghoul_Lacedon][None] = 0;

                testCases[CreatureConstants.Giant_Cloud][None] = 0;

                testCases[CreatureConstants.Giant_Fire][None] = 0;

                testCases[CreatureConstants.Giant_Frost][None] = 0;

                testCases[CreatureConstants.Giant_Hill][None] = 0;

                testCases[CreatureConstants.Giant_Stone][None] = 0;

                testCases[CreatureConstants.Giant_Stone_Elder][None] = 0;

                testCases[CreatureConstants.Giant_Storm][None] = 0;

                testCases[CreatureConstants.GibberingMouther][None] = 0;

                testCases[CreatureConstants.Girallon][None] = 0;

                testCases[CreatureConstants.Githyanki][None] = 0;

                testCases[CreatureConstants.Githzerai][None] = 0;

                testCases[CreatureConstants.Glabrezu][None] = 0;

                testCases[CreatureConstants.Gnoll][None] = 0;

                testCases[CreatureConstants.Gnome_Forest][GetData(GroupConstants.All, "against illusions")] = 2;

                testCases[CreatureConstants.Gnome_Rock][GetData(GroupConstants.All, "against illusions")] = 2;

                testCases[CreatureConstants.Gnome_Svirfneblin][GetData(GroupConstants.All)] = 2;

                testCases[CreatureConstants.Goblin][None] = 0;

                testCases[CreatureConstants.Golem_Clay][None] = 0;

                testCases[CreatureConstants.Golem_Flesh][None] = 0;

                testCases[CreatureConstants.Golem_Iron][None] = 0;

                testCases[CreatureConstants.Golem_Stone][None] = 0;

                testCases[CreatureConstants.Golem_Stone_Greater][None] = 0;

                testCases[CreatureConstants.Gorgon][None] = 0;

                testCases[CreatureConstants.GrayOoze][None] = 0;

                testCases[CreatureConstants.GrayRender][None] = 0;

                testCases[CreatureConstants.GreenHag][None] = 0;

                testCases[CreatureConstants.Grick][None] = 0;

                testCases[CreatureConstants.Griffon][None] = 0;

                testCases[CreatureConstants.Grig][None] = 0;

                testCases[CreatureConstants.Grig_WithFiddle][None] = 0;

                testCases[CreatureConstants.Grimlock][None] = 0;

                testCases[CreatureConstants.Gynosphinx][None] = 0;

                testCases[CreatureConstants.Halfling_Deep][None] = 0;

                testCases[CreatureConstants.Halfling_Lightfoot][None] = 0;

                testCases[CreatureConstants.Halfling_Tallfellow][None] = 0;

                testCases[CreatureConstants.Harpy][None] = 0;

                testCases[CreatureConstants.Hawk][None] = 0;

                testCases[CreatureConstants.Hellcat_Bezekira][None] = 0;

                testCases[CreatureConstants.Hellwasp_Swarm][None] = 0;

                testCases[CreatureConstants.Hezrou][None] = 0;

                testCases[CreatureConstants.Hieracosphinx][None] = 0;

                testCases[CreatureConstants.Homunculus][None] = 0;

                testCases[CreatureConstants.HornedDevil_Cornugon][None] = 0;

                testCases[CreatureConstants.Horse_Heavy][None] = 0;

                testCases[CreatureConstants.Horse_Heavy_War][None] = 0;

                testCases[CreatureConstants.Horse_Light][None] = 0;

                testCases[CreatureConstants.Horse_Light_War][None] = 0;

                testCases[CreatureConstants.Human][None] = 0;

                testCases[CreatureConstants.Hydra_5Heads][None] = 0;

                testCases[CreatureConstants.Hydra_6Heads][None] = 0;

                testCases[CreatureConstants.Hydra_7Heads][None] = 0;

                testCases[CreatureConstants.Hydra_8Heads][None] = 0;

                testCases[CreatureConstants.Hydra_9Heads][None] = 0;

                testCases[CreatureConstants.Hydra_10Heads][None] = 0;

                testCases[CreatureConstants.Hydra_11Heads][None] = 0;

                testCases[CreatureConstants.Hydra_12Heads][None] = 0;

                testCases[CreatureConstants.IceDevil_Gelugon][None] = 0;

                testCases[CreatureConstants.Imp][None] = 0;

                testCases[CreatureConstants.Janni][None] = 0;

                testCases[CreatureConstants.Lemure][None] = 0;

                testCases[CreatureConstants.Lion_Dire][None] = 0;

                testCases[CreatureConstants.Locust_Swarm][None] = 0;

                testCases[CreatureConstants.Marilith][None] = 0;

                testCases[CreatureConstants.Megaraptor][None] = 0;

                testCases[CreatureConstants.Mephit_Air][None] = 0;

                testCases[CreatureConstants.Mephit_Dust][None] = 0;

                testCases[CreatureConstants.Mephit_Earth][None] = 0;

                testCases[CreatureConstants.Mephit_Fire][None] = 0;

                testCases[CreatureConstants.Mephit_Ice][None] = 0;

                testCases[CreatureConstants.Mephit_Magma][None] = 0;

                testCases[CreatureConstants.Mephit_Ooze][None] = 0;

                testCases[CreatureConstants.Mephit_Salt][None] = 0;

                testCases[CreatureConstants.Mephit_Steam][None] = 0;

                testCases[CreatureConstants.Mephit_Water][None] = 0;

                testCases[CreatureConstants.MindFlayer][None] = 0;

                testCases[CreatureConstants.Naga_Dark][None] = 0;

                testCases[CreatureConstants.Naga_Guardian][None] = 0;

                testCases[CreatureConstants.Naga_Spirit][None] = 0;

                testCases[CreatureConstants.Naga_Water][None] = 0;

                testCases[CreatureConstants.Nalfeshnee][None] = 0;

                testCases[CreatureConstants.Nixie][None] = 0;

                testCases[CreatureConstants.OchreJelly][None] = 0;

                testCases[CreatureConstants.Octopus][None] = 0;

                testCases[CreatureConstants.Octopus_Giant][None] = 0;

                testCases[CreatureConstants.Orc][None] = 0;

                testCases[CreatureConstants.Orc_Half][None] = 0;

                testCases[CreatureConstants.Owl_Giant][None] = 0;

                testCases[CreatureConstants.PitFiend][None] = 0;

                testCases[CreatureConstants.Pixie][None] = 0;

                testCases[CreatureConstants.Pixie_WithIrresistableDance][None] = 0;

                testCases[CreatureConstants.PrayingMantis_Giant][None] = 0;

                testCases[CreatureConstants.Pyrohydra_5Heads][None] = 0;

                testCases[CreatureConstants.Pyrohydra_6Heads][None] = 0;

                testCases[CreatureConstants.Pyrohydra_7Heads][None] = 0;

                testCases[CreatureConstants.Pyrohydra_8Heads][None] = 0;

                testCases[CreatureConstants.Pyrohydra_9Heads][None] = 0;

                testCases[CreatureConstants.Pyrohydra_10Heads][None] = 0;

                testCases[CreatureConstants.Pyrohydra_11Heads][None] = 0;

                testCases[CreatureConstants.Pyrohydra_12Heads][None] = 0;

                testCases[CreatureConstants.Quasit][None] = 0;

                testCases[CreatureConstants.Rat_Dire][None] = 0;

                testCases[CreatureConstants.Rat_Swarm][None] = 0;

                testCases[CreatureConstants.Retriever][None] = 0;

                testCases[CreatureConstants.Salamander_Flamebrother][None] = 0;

                testCases[CreatureConstants.Salamander_Average][None] = 0;

                testCases[CreatureConstants.Salamander_Noble][None] = 0;

                testCases[CreatureConstants.SeaHag][None] = 0;

                testCases[CreatureConstants.Shadow][None] = 0;

                testCases[CreatureConstants.Shadow_Greater][None] = 0;

                testCases[CreatureConstants.Shark_Dire][None] = 0;

                testCases[CreatureConstants.Slaad_Red][None] = 0;

                testCases[CreatureConstants.Slaad_Blue][None] = 0;

                testCases[CreatureConstants.Slaad_Green][None] = 0;

                testCases[CreatureConstants.Slaad_Gray][None] = 0;

                testCases[CreatureConstants.Slaad_Death][None] = 0;

                testCases[CreatureConstants.Snake_Constrictor][None] = 0;

                testCases[CreatureConstants.Snake_Constrictor_Giant][None] = 0;

                testCases[CreatureConstants.Snake_Viper_Tiny][None] = 0;

                testCases[CreatureConstants.Snake_Viper_Small][None] = 0;

                testCases[CreatureConstants.Snake_Viper_Medium][None] = 0;

                testCases[CreatureConstants.Snake_Viper_Large][None] = 0;

                testCases[CreatureConstants.Snake_Viper_Huge][None] = 0;

                testCases[CreatureConstants.Spider_Swarm][None] = 0;

                testCases[CreatureConstants.Squid][None] = 0;

                testCases[CreatureConstants.Squid_Giant][None] = 0;

                testCases[CreatureConstants.StagBeetle_Giant][None] = 0;

                testCases[CreatureConstants.Succubus][None] = 0;

                testCases[CreatureConstants.Tiefling][None] = 0;

                testCases[CreatureConstants.Tiger_Dire][None] = 0;

                testCases[CreatureConstants.Tojanida_Juvenile][None] = 0;

                testCases[CreatureConstants.Tojanida_Adult][None] = 0;

                testCases[CreatureConstants.Tojanida_Elder][None] = 0;

                testCases[CreatureConstants.Triceratops][None] = 0;

                testCases[CreatureConstants.Triton][None] = 0;

                testCases[CreatureConstants.Tyrannosaurus][None] = 0;

                testCases[CreatureConstants.Vrock][None] = 0;

                testCases[CreatureConstants.Wasp_Giant][None] = 0;

                testCases[CreatureConstants.Weasel_Dire][None] = 0;

                testCases[CreatureConstants.Whale_Baleen][None] = 0;

                testCases[CreatureConstants.Whale_Cachalot][None] = 0;

                testCases[CreatureConstants.Whale_Orca][None] = 0;

                testCases[CreatureConstants.Wolf_Dire][None] = 0;

                testCases[CreatureConstants.Wolverine_Dire][None] = 0;

                testCases[CreatureConstants.Wraith][None] = 0;

                testCases[CreatureConstants.Wraith_Dread][None] = 0;

                testCases[CreatureConstants.Xorn_Minor][None] = 0;

                testCases[CreatureConstants.Xorn_Average][None] = 0;

                testCases[CreatureConstants.Xorn_Elder][None] = 0;

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        private static class TestDataHelper
        {
            public static IEnumerable EnumerateTestCases(Dictionary<string, Dictionary<string, int>> testCases)
            {
                foreach (var testCase in testCases)
                {
                    var total = testCase.Value.Count;
                    var description = total.ToString();

                    if (testCase.Value.ContainsKey(None))
                        description = None;

                    yield return new TestCaseData(testCase.Key, testCase.Value)
                        .SetName($"SaveBonuses({testCase.Key}, {description})");
                }
            }
        }

        public static IEnumerable Types
        {
            get
            {
                var testCases = new Dictionary<string, Dictionary<string, int>>();
                var types = CreatureConstants.Types.All();

                foreach (var type in types)
                {
                    testCases[type] = new Dictionary<string, int>();
                }

                testCases[CreatureConstants.Types.Aberration][None] = 0;

                testCases[CreatureConstants.Types.Animal][None] = 0;

                testCases[CreatureConstants.Types.Construct][None] = 0;

                testCases[CreatureConstants.Types.Dragon][None] = 0;

                testCases[CreatureConstants.Types.Elemental][None] = 0;

                testCases[CreatureConstants.Types.Fey][None] = 0;

                testCases[CreatureConstants.Types.Giant][None] = 0;

                testCases[CreatureConstants.Types.Humanoid][None] = 0;

                testCases[CreatureConstants.Types.MagicalBeast][None] = 0;

                testCases[CreatureConstants.Types.MonstrousHumanoid][None] = 0;

                testCases[CreatureConstants.Types.Ooze][None] = 0;

                testCases[CreatureConstants.Types.Outsider][None] = 0;

                testCases[CreatureConstants.Types.Plant][None] = 0;

                testCases[CreatureConstants.Types.Undead][None] = 0;

                testCases[CreatureConstants.Types.Vermin][None] = 0;

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        private static string GetData(string saveName, string condition = "")
        {
            var data = saveName;

            if (!string.IsNullOrEmpty(condition))
                data += "," + condition;

            return data;
        }

        public static IEnumerable Subtypes
        {
            get
            {
                var testCases = new Dictionary<string, Dictionary<string, int>>();
                var subtypes = CreatureConstants.Types.Subtypes.All();

                foreach (var subtype in subtypes)
                {
                    testCases[subtype] = new Dictionary<string, int>();
                }

                testCases[CreatureConstants.Types.Subtypes.Air][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Angel][GetData(SaveConstants.Fortitude, "against poison")] = 4;

                testCases[CreatureConstants.Types.Subtypes.Aquatic][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Archon][GetData(SaveConstants.Fortitude, "against poison")] = 4;

                testCases[CreatureConstants.Types.Subtypes.Augmented][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Chaotic][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Cold][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Dwarf][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Earth][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Elf][GetData(GroupConstants.All, "enchantment spells or effects")] = 2;

                testCases[CreatureConstants.Types.Subtypes.Evil][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Extraplanar][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Fire][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Gnoll][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Gnome][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Goblinoid][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Good][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Halfling][GetData(GroupConstants.All)] = 1;
                testCases[CreatureConstants.Types.Subtypes.Halfling][GetData(GroupConstants.All, "morale against fear")] = 2;

                testCases[CreatureConstants.Types.Subtypes.Incorporeal][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Lawful][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Native][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Reptilian][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Shapechanger][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Swarm][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Water][None] = 0;

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }
    }
}
