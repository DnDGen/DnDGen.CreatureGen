using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Defenses
{
    public class SaveBonusesTestData
    {
        public const string None = "NONE";

        public static Dictionary<string, List<string>> GetCreatureSaveBonuses()
        {
            var testCases = new Dictionary<string, List<string>>
            {
                [CreatureConstants.Aasimar] = [None],

                [CreatureConstants.Aboleth] = [None],

                [CreatureConstants.Achaierai] = [None],

                [CreatureConstants.Allip] = [None],

                [CreatureConstants.Androsphinx] = [None],

                [CreatureConstants.Angel_AstralDeva] = [None],

                [CreatureConstants.Angel_Planetar] = [None],

                [CreatureConstants.Angel_Solar] = [None],

                [CreatureConstants.AnimatedObject_Colossal] = [None],
                [CreatureConstants.AnimatedObject_Colossal_Flexible] = [None],
                [CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long] = [None],
                [CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall] = [None],
                [CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Colossal_Sheetlike] = [None],
                [CreatureConstants.AnimatedObject_Colossal_TwoLegs] = [None],
                [CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Colossal_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_Flexible] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Long] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Long_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Tall] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Tall_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_Sheetlike] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_TwoLegs] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Huge] = [None],
                [CreatureConstants.AnimatedObject_Huge_Flexible] = [None],
                [CreatureConstants.AnimatedObject_Huge_MultipleLegs_Long] = [None],
                [CreatureConstants.AnimatedObject_Huge_MultipleLegs_Long_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Huge_MultipleLegs_Tall] = [None],
                [CreatureConstants.AnimatedObject_Huge_MultipleLegs_Tall_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Huge_Sheetlike] = [None],
                [CreatureConstants.AnimatedObject_Huge_TwoLegs] = [None],
                [CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Huge_Wheels_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Huge_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Large] = [None],
                [CreatureConstants.AnimatedObject_Large_Flexible] = [None],
                [CreatureConstants.AnimatedObject_Large_MultipleLegs_Long] = [None],
                [CreatureConstants.AnimatedObject_Large_MultipleLegs_Long_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Large_MultipleLegs_Tall] = [None],
                [CreatureConstants.AnimatedObject_Large_MultipleLegs_Tall_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Large_Sheetlike] = [None],
                [CreatureConstants.AnimatedObject_Large_TwoLegs] = [None],
                [CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Large_Wheels_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Large_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Medium] = [None],
                [CreatureConstants.AnimatedObject_Medium_Flexible] = [None],
                [CreatureConstants.AnimatedObject_Medium_MultipleLegs] = [None],
                [CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Medium_Sheetlike] = [None],
                [CreatureConstants.AnimatedObject_Medium_TwoLegs] = [None],
                [CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Medium_Wheels_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Medium_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Small] = [None],
                [CreatureConstants.AnimatedObject_Small_Flexible] = [None],
                [CreatureConstants.AnimatedObject_Small_MultipleLegs] = [None],
                [CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Small_Sheetlike] = [None],
                [CreatureConstants.AnimatedObject_Small_TwoLegs] = [None],
                [CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Small_Wheels_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Small_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Tiny] = [None],
                [CreatureConstants.AnimatedObject_Tiny_Flexible] = [None],
                [CreatureConstants.AnimatedObject_Tiny_MultipleLegs] = [None],
                [CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Tiny_Sheetlike] = [None],
                [CreatureConstants.AnimatedObject_Tiny_TwoLegs] = [None],
                [CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Tiny_Wooden] = [None],

                [CreatureConstants.Ankheg] = [None],

                [CreatureConstants.Annis] = [None],

                [CreatureConstants.Ant_Giant_Queen] = [None],

                [CreatureConstants.Ant_Giant_Soldier] = [None],

                [CreatureConstants.Ant_Giant_Worker] = [None],

                [CreatureConstants.Ape] = [None],

                [CreatureConstants.Ape_Dire] = [None],

                [CreatureConstants.Aranea] = [None],

                [CreatureConstants.Arrowhawk_Juvenile] = [None],

                [CreatureConstants.Arrowhawk_Adult] = [None],

                [CreatureConstants.Arrowhawk_Elder] = [None],

                [CreatureConstants.AssassinVine] = [None],

                [CreatureConstants.Athach] = [None],

                [CreatureConstants.Avoral] = [None],

                [CreatureConstants.Azer] = [None],

                [CreatureConstants.Babau] = [None],

                [CreatureConstants.Baboon] = [None],

                [CreatureConstants.Badger] = [None],

                [CreatureConstants.Badger_Dire] = [None],

                [CreatureConstants.Balor] = [None],

                [CreatureConstants.BarbedDevil_Hamatula] = [None],

                [CreatureConstants.Barghest] = [None],

                [CreatureConstants.Barghest_Greater] = [None],

                [CreatureConstants.Basilisk] = [None],

                [CreatureConstants.Basilisk_Greater] = [None],

                [CreatureConstants.Bat] = [None],

                [CreatureConstants.Bat_Dire] = [None],

                [CreatureConstants.Bat_Swarm] = [None],

                [CreatureConstants.Bear_Black] = [None],

                [CreatureConstants.Bear_Brown] = [None],

                [CreatureConstants.Bear_Dire] = [None],

                [CreatureConstants.Bear_Polar] = [None],

                [CreatureConstants.BeardedDevil_Barbazu] = [None],

                [CreatureConstants.Bebilith] = [None],

                [CreatureConstants.Bee_Giant] = [None],

                [CreatureConstants.Behir] = [None],

                [CreatureConstants.Beholder] = [None],

                [CreatureConstants.Beholder_Gauth] = [None],

                [CreatureConstants.Belker] = [None],

                [CreatureConstants.Bison] = [None],

                [CreatureConstants.BlackPudding] = [None],

                [CreatureConstants.BlackPudding_Elder] = [None],

                [CreatureConstants.BlinkDog] = [None],

                [CreatureConstants.Boar] = [None],

                [CreatureConstants.Boar_Dire] = [None],

                [CreatureConstants.Bodak] = [None],

                [CreatureConstants.BombardierBeetle_Giant] = [None],

                [CreatureConstants.BoneDevil_Osyluth] = [None],

                [CreatureConstants.Bralani] = [None],

                [CreatureConstants.Bugbear] = [None],

                [CreatureConstants.Bulette] = [None],

                [CreatureConstants.Camel_Bactrian] = [None],

                [CreatureConstants.Camel_Dromedary] = [None],

                [CreatureConstants.CarrionCrawler] = [None],

                [CreatureConstants.Cat] = [None],

                [CreatureConstants.Centaur] = [None],

                [CreatureConstants.Centipede_Monstrous_Colossal] = [None],

                [CreatureConstants.Centipede_Monstrous_Gargantuan] = [None],

                [CreatureConstants.Centipede_Monstrous_Huge] = [None],

                [CreatureConstants.Centipede_Monstrous_Large] = [None],

                [CreatureConstants.Centipede_Monstrous_Medium] = [None],

                [CreatureConstants.Centipede_Monstrous_Small] = [None],

                [CreatureConstants.Centipede_Monstrous_Tiny] = [None],

                [CreatureConstants.Centipede_Swarm] = [None],

                [CreatureConstants.ChainDevil_Kyton] = [None],

                [CreatureConstants.ChaosBeast] = [None],

                [CreatureConstants.Cheetah] = [None],

                [CreatureConstants.Chimera_Black] = [None],

                [CreatureConstants.Chimera_Blue] = [None],

                [CreatureConstants.Chimera_Green] = [None],

                [CreatureConstants.Chimera_Red] = [None],

                [CreatureConstants.Chimera_White] = [None],

                [CreatureConstants.Choker] = [None],

                [CreatureConstants.Chuul] = [None],

                [CreatureConstants.Cloaker] = [None],

                [CreatureConstants.Cockatrice] = [None],

                [CreatureConstants.Couatl] = [None],

                [CreatureConstants.Criosphinx] = [None],

                [CreatureConstants.Crocodile] = [None],

                [CreatureConstants.Crocodile_Giant] = [None],

                [CreatureConstants.Cryohydra_5Heads] = [None],

                [CreatureConstants.Cryohydra_6Heads] = [None],

                [CreatureConstants.Cryohydra_7Heads] = [None],

                [CreatureConstants.Cryohydra_8Heads] = [None],

                [CreatureConstants.Cryohydra_9Heads] = [None],

                [CreatureConstants.Cryohydra_10Heads] = [None],

                [CreatureConstants.Cryohydra_11Heads] = [None],

                [CreatureConstants.Cryohydra_12Heads] = [None],

                [CreatureConstants.Darkmantle] = [None],

                [CreatureConstants.Deinonychus] = [None],

                [CreatureConstants.Delver] = [None],

                [CreatureConstants.Derro] = [None],

                [CreatureConstants.Derro_Sane] = [None],

                [CreatureConstants.Destrachan] = [None],

                [CreatureConstants.Devourer] = [None],

                [CreatureConstants.Digester] = [None],

                [CreatureConstants.DisplacerBeast] =
                    [GetData(GroupConstants.All, 2, "ranged magical attacks that specifically target the displacer beast, except for ranged touch attacks")],

                [CreatureConstants.DisplacerBeast_PackLord] =
                    [GetData(GroupConstants.All, 2, "ranged magical attacks that specifically target the displacer beast, except for ranged touch attacks")],

                [CreatureConstants.Djinni] = [None],

                [CreatureConstants.Djinni_Noble] = [None],

                [CreatureConstants.Dog] = [None],

                [CreatureConstants.Dog_Riding] = [None],

                [CreatureConstants.Donkey] = [None],

                [CreatureConstants.Doppelganger] = [None],

                [CreatureConstants.Dragon_Black_Wyrmling] = [None],

                [CreatureConstants.Dragon_Black_VeryYoung] = [None],

                [CreatureConstants.Dragon_Black_Young] = [None],

                [CreatureConstants.Dragon_Black_Juvenile] = [None],

                [CreatureConstants.Dragon_Black_YoungAdult] = [None],

                [CreatureConstants.Dragon_Black_Adult] = [None],

                [CreatureConstants.Dragon_Black_MatureAdult] = [None],

                [CreatureConstants.Dragon_Black_Old] = [None],

                [CreatureConstants.Dragon_Black_VeryOld] = [None],

                [CreatureConstants.Dragon_Black_Ancient] = [None],

                [CreatureConstants.Dragon_Black_Wyrm] = [None],

                [CreatureConstants.Dragon_Black_GreatWyrm] = [None],

                [CreatureConstants.Dragon_Blue_Wyrmling] = [None],

                [CreatureConstants.Dragon_Blue_VeryYoung] = [None],

                [CreatureConstants.Dragon_Blue_Young] = [None],

                [CreatureConstants.Dragon_Blue_Juvenile] = [None],

                [CreatureConstants.Dragon_Blue_YoungAdult] = [None],

                [CreatureConstants.Dragon_Blue_Adult] = [None],

                [CreatureConstants.Dragon_Blue_MatureAdult] = [None],

                [CreatureConstants.Dragon_Blue_Old] = [None],

                [CreatureConstants.Dragon_Blue_VeryOld] = [None],

                [CreatureConstants.Dragon_Blue_Ancient] = [None],

                [CreatureConstants.Dragon_Blue_Wyrm] = [None],

                [CreatureConstants.Dragon_Blue_GreatWyrm] = [None],

                [CreatureConstants.Dragon_Green_Wyrmling] = [None],

                [CreatureConstants.Dragon_Green_VeryYoung] = [None],

                [CreatureConstants.Dragon_Green_Young] = [None],

                [CreatureConstants.Dragon_Green_Juvenile] = [None],

                [CreatureConstants.Dragon_Green_YoungAdult] = [None],

                [CreatureConstants.Dragon_Green_Adult] = [None],

                [CreatureConstants.Dragon_Green_MatureAdult] = [None],

                [CreatureConstants.Dragon_Green_Old] = [None],

                [CreatureConstants.Dragon_Green_VeryOld] = [None],

                [CreatureConstants.Dragon_Green_Ancient] = [None],

                [CreatureConstants.Dragon_Green_Wyrm] = [None],

                [CreatureConstants.Dragon_Green_GreatWyrm] = [None],

                [CreatureConstants.Dragon_Red_Wyrmling] = [None],

                [CreatureConstants.Dragon_Red_VeryYoung] = [None],

                [CreatureConstants.Dragon_Red_Young] = [None],

                [CreatureConstants.Dragon_Red_Juvenile] = [None],

                [CreatureConstants.Dragon_Red_YoungAdult] = [None],

                [CreatureConstants.Dragon_Red_Adult] = [None],

                [CreatureConstants.Dragon_Red_MatureAdult] = [None],

                [CreatureConstants.Dragon_Red_Old] = [None],

                [CreatureConstants.Dragon_Red_VeryOld] = [None],

                [CreatureConstants.Dragon_Red_Ancient] = [None],

                [CreatureConstants.Dragon_Red_Wyrm] = [None],

                [CreatureConstants.Dragon_Red_GreatWyrm] = [None],

                [CreatureConstants.Dragon_White_Wyrmling] = [None],

                [CreatureConstants.Dragon_White_VeryYoung] = [None],

                [CreatureConstants.Dragon_White_Young] = [None],

                [CreatureConstants.Dragon_White_Juvenile] = [None],

                [CreatureConstants.Dragon_White_YoungAdult] = [None],

                [CreatureConstants.Dragon_White_Adult] = [None],

                [CreatureConstants.Dragon_White_MatureAdult] = [None],

                [CreatureConstants.Dragon_White_Old] = [None],

                [CreatureConstants.Dragon_White_VeryOld] = [None],

                [CreatureConstants.Dragon_White_Ancient] = [None],

                [CreatureConstants.Dragon_White_Wyrm] = [None],

                [CreatureConstants.Dragon_White_GreatWyrm] = [None],

                [CreatureConstants.Dragon_Brass_Wyrmling] = [None],

                [CreatureConstants.Dragon_Brass_VeryYoung] = [None],

                [CreatureConstants.Dragon_Brass_Young] = [None],

                [CreatureConstants.Dragon_Brass_Juvenile] = [None],

                [CreatureConstants.Dragon_Brass_YoungAdult] = [None],

                [CreatureConstants.Dragon_Brass_Adult] = [None],

                [CreatureConstants.Dragon_Brass_MatureAdult] = [None],

                [CreatureConstants.Dragon_Brass_Old] = [None],

                [CreatureConstants.Dragon_Brass_VeryOld] = [None],

                [CreatureConstants.Dragon_Brass_Ancient] = [None],

                [CreatureConstants.Dragon_Brass_Wyrm] = [None],

                [CreatureConstants.Dragon_Brass_GreatWyrm] = [None],

                [CreatureConstants.Dragon_Bronze_Wyrmling] = [None],

                [CreatureConstants.Dragon_Bronze_VeryYoung] = [None],

                [CreatureConstants.Dragon_Bronze_Young] = [None],

                [CreatureConstants.Dragon_Bronze_Juvenile] = [None],

                [CreatureConstants.Dragon_Bronze_YoungAdult] = [None],

                [CreatureConstants.Dragon_Bronze_Adult] = [None],

                [CreatureConstants.Dragon_Bronze_MatureAdult] = [None],

                [CreatureConstants.Dragon_Bronze_Old] = [None],

                [CreatureConstants.Dragon_Bronze_VeryOld] = [None],

                [CreatureConstants.Dragon_Bronze_Ancient] = [None],

                [CreatureConstants.Dragon_Bronze_Wyrm] = [None],

                [CreatureConstants.Dragon_Bronze_GreatWyrm] = [None],

                [CreatureConstants.Dragon_Copper_Wyrmling] = [None],

                [CreatureConstants.Dragon_Copper_VeryYoung] = [None],

                [CreatureConstants.Dragon_Copper_Young] = [None],

                [CreatureConstants.Dragon_Copper_Juvenile] = [None],

                [CreatureConstants.Dragon_Copper_YoungAdult] = [None],

                [CreatureConstants.Dragon_Copper_Adult] = [None],

                [CreatureConstants.Dragon_Copper_MatureAdult] = [None],

                [CreatureConstants.Dragon_Copper_Old] = [None],

                [CreatureConstants.Dragon_Copper_VeryOld] = [None],

                [CreatureConstants.Dragon_Copper_Ancient] = [None],

                [CreatureConstants.Dragon_Copper_Wyrm] = [None],

                [CreatureConstants.Dragon_Copper_GreatWyrm] = [None],

                [CreatureConstants.Dragon_Gold_Wyrmling] = [None],

                [CreatureConstants.Dragon_Gold_VeryYoung] = [None],

                [CreatureConstants.Dragon_Gold_Young] = [None],

                [CreatureConstants.Dragon_Gold_Juvenile] = [None],

                [CreatureConstants.Dragon_Gold_YoungAdult] = [None],

                [CreatureConstants.Dragon_Gold_Adult] = [None],

                [CreatureConstants.Dragon_Gold_MatureAdult] = [None],

                [CreatureConstants.Dragon_Gold_Old] = [None],

                [CreatureConstants.Dragon_Gold_VeryOld] = [None],

                [CreatureConstants.Dragon_Gold_Ancient] = [None],

                [CreatureConstants.Dragon_Gold_Wyrm] = [None],

                [CreatureConstants.Dragon_Gold_GreatWyrm] = [None],

                [CreatureConstants.Dragon_Silver_Wyrmling] = [None],

                [CreatureConstants.Dragon_Silver_VeryYoung] = [None],

                [CreatureConstants.Dragon_Silver_Young] = [None],

                [CreatureConstants.Dragon_Silver_Juvenile] = [None],

                [CreatureConstants.Dragon_Silver_YoungAdult] = [None],

                [CreatureConstants.Dragon_Silver_Adult] = [None],

                [CreatureConstants.Dragon_Silver_MatureAdult] = [None],

                [CreatureConstants.Dragon_Silver_Old] = [None],

                [CreatureConstants.Dragon_Silver_VeryOld] = [None],

                [CreatureConstants.Dragon_Silver_Ancient] = [None],

                [CreatureConstants.Dragon_Silver_Wyrm] = [None],

                [CreatureConstants.Dragon_Silver_GreatWyrm] = [None],

                [CreatureConstants.DragonTurtle] = [None],

                [CreatureConstants.Dragonne] = [None],

                [CreatureConstants.Dretch] = [None],

                [CreatureConstants.Drider] = [None],

                [CreatureConstants.Dryad] = [None],

                [CreatureConstants.Dwarf_Deep] =
                    [GetData(SaveConstants.Fortitude, 3, "against poison"), GetData(GroupConstants.All, 3, "against spells and spell-like abilities")],

                [CreatureConstants.Dwarf_Duergar] = [GetData(GroupConstants.All, 2, "against spells and spell-like abilities")],

                [CreatureConstants.Dwarf_Hill] =
                    [GetData(SaveConstants.Fortitude, 2, "against poison"), GetData(GroupConstants.All, 2, "against spells and spell-like abilities")],

                [CreatureConstants.Dwarf_Mountain] =
                    [GetData(SaveConstants.Fortitude, 2, "against poison"), GetData(GroupConstants.All, 2, "against spells and spell-like abilities")],

                [CreatureConstants.Eagle] = [None],

                [CreatureConstants.Eagle_Giant] = [None],

                [CreatureConstants.Efreeti] = [None],

                [CreatureConstants.Elasmosaurus] = [None],

                [CreatureConstants.Elemental_Air_Small] = [None],

                [CreatureConstants.Elemental_Air_Medium] = [None],

                [CreatureConstants.Elemental_Air_Large] = [None],

                [CreatureConstants.Elemental_Air_Huge] = [None],

                [CreatureConstants.Elemental_Air_Greater] = [None],

                [CreatureConstants.Elemental_Air_Elder] = [None],

                [CreatureConstants.Elemental_Earth_Small] = [None],

                [CreatureConstants.Elemental_Earth_Medium] = [None],

                [CreatureConstants.Elemental_Earth_Large] = [None],

                [CreatureConstants.Elemental_Earth_Huge] = [None],

                [CreatureConstants.Elemental_Earth_Greater] = [None],

                [CreatureConstants.Elemental_Earth_Elder] = [None],

                [CreatureConstants.Elemental_Fire_Small] = [None],

                [CreatureConstants.Elemental_Fire_Medium] = [None],

                [CreatureConstants.Elemental_Fire_Large] = [None],

                [CreatureConstants.Elemental_Fire_Huge] = [None],

                [CreatureConstants.Elemental_Fire_Greater] = [None],

                [CreatureConstants.Elemental_Fire_Elder] = [None],

                [CreatureConstants.Elemental_Water_Small] = [None],

                [CreatureConstants.Elemental_Water_Medium] = [None],

                [CreatureConstants.Elemental_Water_Large] = [None],

                [CreatureConstants.Elemental_Water_Huge] = [None],

                [CreatureConstants.Elemental_Water_Greater] = [None],

                [CreatureConstants.Elemental_Water_Elder] = [None],

                [CreatureConstants.Elephant] = [None],

                [CreatureConstants.Elf_Aquatic] = [None],

                [CreatureConstants.Elf_Drow] = [GetData(SaveConstants.Will, 2, "spells and spell-like abilities")],

                [CreatureConstants.Elf_Gray] = [None],

                [CreatureConstants.Elf_Half] = [None],

                [CreatureConstants.Elf_High] = [None],

                [CreatureConstants.Elf_Wild] = [None],

                [CreatureConstants.Elf_Wood] = [None],

                [CreatureConstants.Erinyes] = [None],

                [CreatureConstants.EtherealFilcher] = [None],

                [CreatureConstants.EtherealMarauder] = [None],

                [CreatureConstants.Ettercap] = [None],

                [CreatureConstants.Ettin] = [None],

                [CreatureConstants.FireBeetle_Giant] = [None],

                [CreatureConstants.FormianMyrmarch] = [None],

                [CreatureConstants.FormianQueen] = [None],

                [CreatureConstants.FormianTaskmaster] = [None],

                [CreatureConstants.FormianWarrior] = [None],

                [CreatureConstants.FormianWorker] = [None],

                [CreatureConstants.FrostWorm] = [None],

                [CreatureConstants.Gargoyle] = [None],

                [CreatureConstants.Gargoyle_Kapoacinth] = [None],

                [CreatureConstants.GelatinousCube] = [None],

                [CreatureConstants.Ghaele] = [None],

                [CreatureConstants.Ghoul] = [None],

                [CreatureConstants.Ghoul_Ghast] = [None],

                [CreatureConstants.Ghoul_Lacedon] = [None],

                [CreatureConstants.Giant_Cloud] = [None],

                [CreatureConstants.Giant_Fire] = [None],

                [CreatureConstants.Giant_Frost] = [None],

                [CreatureConstants.Giant_Hill] = [None],

                [CreatureConstants.Giant_Stone] = [None],

                [CreatureConstants.Giant_Stone_Elder] = [None],

                [CreatureConstants.Giant_Storm] = [None],

                [CreatureConstants.GibberingMouther] = [None],

                [CreatureConstants.Girallon] = [None],

                [CreatureConstants.Githyanki] = [None],

                [CreatureConstants.Githzerai] = [None],

                [CreatureConstants.Glabrezu] = [None],

                [CreatureConstants.Gnoll] = [None],

                [CreatureConstants.Gnome_Forest] = [GetData(GroupConstants.All, 2, "against illusions")],

                [CreatureConstants.Gnome_Rock] = [GetData(GroupConstants.All, 2, "against illusions")],

                [CreatureConstants.Gnome_Svirfneblin] = [GetData(GroupConstants.All, 2)],

                [CreatureConstants.Goblin] = [None],

                [CreatureConstants.Golem_Clay] = [None],

                [CreatureConstants.Golem_Flesh] = [None],

                [CreatureConstants.Golem_Iron] = [None],

                [CreatureConstants.Golem_Stone] = [None],

                [CreatureConstants.Golem_Stone_Greater] = [None],

                [CreatureConstants.Gorgon] = [None],

                [CreatureConstants.GrayOoze] = [None],

                [CreatureConstants.GrayRender] = [None],

                [CreatureConstants.GreenHag] = [None],

                [CreatureConstants.Grick] = [None],

                [CreatureConstants.Griffon] = [None],

                [CreatureConstants.Grig] = [None],

                [CreatureConstants.Grig_WithFiddle] = [None],

                [CreatureConstants.Grimlock] = [None],

                [CreatureConstants.Gynosphinx] = [None],

                [CreatureConstants.Halfling_Deep] = [None],

                [CreatureConstants.Halfling_Lightfoot] = [None],

                [CreatureConstants.Halfling_Tallfellow] = [None],

                [CreatureConstants.Harpy] = [None],

                [CreatureConstants.Hawk] = [None],

                [CreatureConstants.HellHound] = [None],

                [CreatureConstants.HellHound_NessianWarhound] = [None],

                [CreatureConstants.Hellcat_Bezekira] = [None],

                [CreatureConstants.Hellwasp_Swarm] = [None],

                [CreatureConstants.Hezrou] = [None],

                [CreatureConstants.Hieracosphinx] = [None],

                [CreatureConstants.Hippogriff] = [None],

                [CreatureConstants.Hobgoblin] = [None],

                [CreatureConstants.Homunculus] = [None],

                [CreatureConstants.HornedDevil_Cornugon] = [None],

                [CreatureConstants.Horse_Heavy] = [None],

                [CreatureConstants.Horse_Heavy_War] = [None],

                [CreatureConstants.Horse_Light] = [None],

                [CreatureConstants.Horse_Light_War] = [None],

                [CreatureConstants.HoundArchon] = [None],

                [CreatureConstants.Howler] = [None],

                [CreatureConstants.Human] = [None],

                [CreatureConstants.Hydra_5Heads] = [None],

                [CreatureConstants.Hydra_6Heads] = [None],

                [CreatureConstants.Hydra_7Heads] = [None],

                [CreatureConstants.Hydra_8Heads] = [None],

                [CreatureConstants.Hydra_9Heads] = [None],

                [CreatureConstants.Hydra_10Heads] = [None],

                [CreatureConstants.Hydra_11Heads] = [None],

                [CreatureConstants.Hydra_12Heads] = [None],

                [CreatureConstants.Hyena] = [None],

                [CreatureConstants.IceDevil_Gelugon] = [None],

                [CreatureConstants.Imp] = [None],

                [CreatureConstants.InvisibleStalker] = [None],

                [CreatureConstants.Janni] = [None],

                [CreatureConstants.Kobold] = [None],

                [CreatureConstants.Kolyarut] = [None],

                [CreatureConstants.Kraken] = [None],

                [CreatureConstants.Krenshar] = [None],

                [CreatureConstants.KuoToa] = [None],

                [CreatureConstants.Lamia] = [None],

                [CreatureConstants.Lammasu] = [None],

                [CreatureConstants.LanternArchon] = [None],

                [CreatureConstants.Lemure] = [None],

                [CreatureConstants.Leonal] = [None],

                [CreatureConstants.Leopard] = [None],

                [CreatureConstants.Lillend] = [None],

                [CreatureConstants.Lion] = [None],

                [CreatureConstants.Lion_Dire] = [None],

                [CreatureConstants.Lizard] = [None],

                [CreatureConstants.Lizard_Monitor] = [None],

                [CreatureConstants.Lizardfolk] = [None],

                [CreatureConstants.Locathah] = [None],

                [CreatureConstants.Locust_Swarm] = [None],

                [CreatureConstants.Magmin] = [None],

                [CreatureConstants.MantaRay] = [None],

                [CreatureConstants.Manticore] = [None],

                [CreatureConstants.Marilith] = [None],

                [CreatureConstants.Marut] = [None],

                [CreatureConstants.Medusa] = [None],

                [CreatureConstants.Megaraptor] = [None],

                [CreatureConstants.Mephit_Air] = [None],

                [CreatureConstants.Mephit_Dust] = [None],

                [CreatureConstants.Mephit_Earth] = [None],

                [CreatureConstants.Mephit_Fire] = [None],

                [CreatureConstants.Mephit_Ice] = [None],

                [CreatureConstants.Mephit_Magma] = [None],

                [CreatureConstants.Mephit_Ooze] = [None],

                [CreatureConstants.Mephit_Salt] = [None],

                [CreatureConstants.Mephit_Steam] = [None],

                [CreatureConstants.Mephit_Water] = [None],

                [CreatureConstants.Merfolk] = [None],

                [CreatureConstants.Mimic] = [None],

                [CreatureConstants.MindFlayer] = [None],

                [CreatureConstants.Minotaur] = [None],

                [CreatureConstants.Mohrg] = [None],

                [CreatureConstants.Monkey] = [None],

                [CreatureConstants.Mule] = [None],

                [CreatureConstants.Mummy] = [None],

                [CreatureConstants.Naga_Dark] = [GetData(GroupConstants.All, 2, "against charm effects")],

                [CreatureConstants.Naga_Guardian] = [None],

                [CreatureConstants.Naga_Spirit] = [None],

                [CreatureConstants.Naga_Water] = [None],

                [CreatureConstants.Nalfeshnee] = [None],

                [CreatureConstants.NightHag] = [GetData(GroupConstants.All, 2)],

                [CreatureConstants.Nightcrawler] = [None],

                [CreatureConstants.Nightmare] = [None],

                [CreatureConstants.Nightmare_Cauchemar] = [None],

                [CreatureConstants.Nightwalker] = [None],

                [CreatureConstants.Nightwing] = [None],

                [CreatureConstants.Nixie] = [None],

                [CreatureConstants.Nymph] = [None],

                [CreatureConstants.OchreJelly] = [None],

                [CreatureConstants.Octopus] = [None],

                [CreatureConstants.Octopus_Giant] = [None],

                [CreatureConstants.Ogre] = [None],

                [CreatureConstants.Ogre_Merrow] = [None],

                [CreatureConstants.OgreMage] = [None],

                [CreatureConstants.Orc] = [None],

                [CreatureConstants.Orc_Half] = [None],

                [CreatureConstants.Otyugh] = [None],

                [CreatureConstants.Owl] = [None],

                [CreatureConstants.Owl_Giant] = [None],

                [CreatureConstants.Owlbear] = [None],

                [CreatureConstants.Pegasus] = [None],

                [CreatureConstants.PhantomFungus] = [None],

                [CreatureConstants.PhaseSpider] = [None],

                [CreatureConstants.Phasm] = [None],

                [CreatureConstants.PitFiend] = [None],

                [CreatureConstants.Pixie] = [None],

                [CreatureConstants.Pixie_WithIrresistibleDance] = [None],

                [CreatureConstants.Pony] = [None],

                [CreatureConstants.Pony_War] = [None],

                [CreatureConstants.Porpoise] = [None],

                [CreatureConstants.PrayingMantis_Giant] = [None],

                [CreatureConstants.Pseudodragon] = [None],

                [CreatureConstants.PurpleWorm] = [None],

                [CreatureConstants.Pyrohydra_5Heads] = [None],

                [CreatureConstants.Pyrohydra_6Heads] = [None],

                [CreatureConstants.Pyrohydra_7Heads] = [None],

                [CreatureConstants.Pyrohydra_8Heads] = [None],

                [CreatureConstants.Pyrohydra_9Heads] = [None],

                [CreatureConstants.Pyrohydra_10Heads] = [None],

                [CreatureConstants.Pyrohydra_11Heads] = [None],

                [CreatureConstants.Pyrohydra_12Heads] = [None],

                [CreatureConstants.Quasit] = [None],

                [CreatureConstants.Rakshasa] = [None],

                [CreatureConstants.Rast] = [None],

                [CreatureConstants.Rat] = [None],

                [CreatureConstants.Rat_Dire] = [None],

                [CreatureConstants.Rat_Swarm] = [None],

                [CreatureConstants.Raven] = [None],

                [CreatureConstants.Ravid] = [None],

                [CreatureConstants.RazorBoar] = [None],

                [CreatureConstants.Remorhaz] = [None],

                [CreatureConstants.Retriever] = [None],

                [CreatureConstants.Rhinoceras] = [None],

                [CreatureConstants.Roc] = [None],

                [CreatureConstants.Roper] = [None],

                [CreatureConstants.RustMonster] = [None],

                [CreatureConstants.Sahuagin] = [None],

                [CreatureConstants.Sahuagin_Malenti] = [None],

                [CreatureConstants.Sahuagin_Mutant] = [None],

                [CreatureConstants.Salamander_Flamebrother] = [None],

                [CreatureConstants.Salamander_Average] = [None],

                [CreatureConstants.Salamander_Noble] = [None],

                [CreatureConstants.Satyr] = [None],

                [CreatureConstants.Satyr_WithPipes] = [None],

                [CreatureConstants.Scorpion_Monstrous_Colossal] = [None],

                [CreatureConstants.Scorpion_Monstrous_Gargantuan] = [None],

                [CreatureConstants.Scorpion_Monstrous_Huge] = [None],

                [CreatureConstants.Scorpion_Monstrous_Large] = [None],

                [CreatureConstants.Scorpion_Monstrous_Medium] = [None],

                [CreatureConstants.Scorpion_Monstrous_Small] = [None],

                [CreatureConstants.Scorpion_Monstrous_Tiny] = [None],

                [CreatureConstants.Scorpionfolk] = [None],

                [CreatureConstants.SeaCat] = [None],

                [CreatureConstants.SeaHag] = [None],

                [CreatureConstants.Shadow] = [None],

                [CreatureConstants.Shadow_Greater] = [None],

                [CreatureConstants.ShadowMastiff] = [None],

                [CreatureConstants.ShamblingMound] = [None],

                [CreatureConstants.Shark_Dire] = [None],

                [CreatureConstants.Shark_Huge] = [None],

                [CreatureConstants.Shark_Large] = [None],

                [CreatureConstants.Shark_Medium] = [None],

                [CreatureConstants.ShieldGuardian] = [None],

                [CreatureConstants.ShockerLizard] = [None],

                [CreatureConstants.Shrieker] = [None],

                [CreatureConstants.Skum] = [None],

                [CreatureConstants.Slaad_Red] = [None],

                [CreatureConstants.Slaad_Blue] = [None],

                [CreatureConstants.Slaad_Green] = [None],

                [CreatureConstants.Slaad_Gray] = [None],

                [CreatureConstants.Slaad_Death] = [None],

                [CreatureConstants.Snake_Constrictor] = [None],

                [CreatureConstants.Snake_Constrictor_Giant] = [None],

                [CreatureConstants.Snake_Viper_Tiny] = [None],

                [CreatureConstants.Snake_Viper_Small] = [None],

                [CreatureConstants.Snake_Viper_Medium] = [None],

                [CreatureConstants.Snake_Viper_Large] = [None],

                [CreatureConstants.Snake_Viper_Huge] = [None],

                [CreatureConstants.Spectre] = [None],

                [CreatureConstants.Spider_Monstrous_Hunter_Colossal] = [None],

                [CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = [None],

                [CreatureConstants.Spider_Monstrous_Hunter_Huge] = [None],

                [CreatureConstants.Spider_Monstrous_Hunter_Large] = [None],

                [CreatureConstants.Spider_Monstrous_Hunter_Medium] = [None],

                [CreatureConstants.Spider_Monstrous_Hunter_Small] = [None],

                [CreatureConstants.Spider_Monstrous_Hunter_Tiny] = [None],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = [None],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = [None],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = [None],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Large] = [None],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = [None],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Small] = [None],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = [None],

                [CreatureConstants.SpiderEater] = [None],

                [CreatureConstants.Spider_Swarm] = [None],

                [CreatureConstants.Squid] = [None],

                [CreatureConstants.Squid_Giant] = [None],

                [CreatureConstants.StagBeetle_Giant] = [None],

                [CreatureConstants.Stirge] = [None],

                [CreatureConstants.Succubus] = [None],

                [CreatureConstants.Tarrasque] = [None],

                [CreatureConstants.Tendriculos] = [None],

                [CreatureConstants.Thoqqua] = [None],

                [CreatureConstants.Tiefling] = [None],

                [CreatureConstants.Tiger] = [None],

                [CreatureConstants.Tiger_Dire] = [None],

                [CreatureConstants.Titan] = [None],

                [CreatureConstants.Toad] = [None],

                [CreatureConstants.Tojanida_Juvenile] = [None],

                [CreatureConstants.Tojanida_Adult] = [None],

                [CreatureConstants.Tojanida_Elder] = [None],

                [CreatureConstants.Treant] = [None],

                [CreatureConstants.Triceratops] = [None],

                [CreatureConstants.Triton] = [None],

                [CreatureConstants.Troglodyte] = [None],

                [CreatureConstants.Troll] = [None],

                [CreatureConstants.Troll_Scrag] = [None],

                [CreatureConstants.TrumpetArchon] = [None],

                [CreatureConstants.Tyrannosaurus] = [None],

                [CreatureConstants.UmberHulk] = [None],

                [CreatureConstants.UmberHulk_TrulyHorrid] = [None],

                [CreatureConstants.Unicorn] = [None],

                [CreatureConstants.VampireSpawn] = [None],

                [CreatureConstants.Vargouille] = [None],

                [CreatureConstants.VioletFungus] = [None],

                [CreatureConstants.Vrock] = [None],

                [CreatureConstants.Wasp_Giant] = [None],

                [CreatureConstants.Weasel] = [None],

                [CreatureConstants.Weasel_Dire] = [None],

                [CreatureConstants.Whale_Baleen] = [None],

                [CreatureConstants.Whale_Cachalot] = [None],

                [CreatureConstants.Whale_Orca] = [None],

                [CreatureConstants.Wight] = [None],

                [CreatureConstants.WillOWisp] = [None],

                [CreatureConstants.WinterWolf] = [None],

                [CreatureConstants.Wolf] = [None],

                [CreatureConstants.Wolf_Dire] = [None],

                [CreatureConstants.Wolverine] = [None],

                [CreatureConstants.Wolverine_Dire] = [None],

                [CreatureConstants.Worg] = [None],

                [CreatureConstants.Wraith] = [None],

                [CreatureConstants.Wraith_Dread] = [None],

                [CreatureConstants.Wyvern] = [None],

                [CreatureConstants.Xill] = [None],

                [CreatureConstants.Xorn_Minor] = [None],

                [CreatureConstants.Xorn_Average] = [None],

                [CreatureConstants.Xorn_Elder] = [None],

                [CreatureConstants.YethHound] = [None],

                [CreatureConstants.Yrthak] = [None],

                [CreatureConstants.YuanTi_Abomination] = [None],

                [CreatureConstants.YuanTi_Halfblood_SnakeArms] = [None],

                [CreatureConstants.YuanTi_Halfblood_SnakeHead] = [None],

                [CreatureConstants.YuanTi_Halfblood_SnakeTail] = [None],

                [CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = [None],

                [CreatureConstants.YuanTi_Pureblood] = [None],

                [CreatureConstants.Zelekhut] = [None]
            };

            return testCases;
        }

        public static Dictionary<string, List<string>> GetTypeSaveBonuses()
        {
            var testCases = new Dictionary<string, List<string>>
            {
                [CreatureConstants.Types.Aberration] = [None],
                [CreatureConstants.Types.Animal] = [None],
                [CreatureConstants.Types.Construct] = [None],
                [CreatureConstants.Types.Dragon] = [None],
                [CreatureConstants.Types.Elemental] = [None],
                [CreatureConstants.Types.Fey] = [None],
                [CreatureConstants.Types.Giant] = [None],
                [CreatureConstants.Types.Humanoid] = [None],
                [CreatureConstants.Types.MagicalBeast] = [None],
                [CreatureConstants.Types.MonstrousHumanoid] = [None],
                [CreatureConstants.Types.Ooze] = [None],
                [CreatureConstants.Types.Outsider] = [None],
                [CreatureConstants.Types.Plant] = [None],
                [CreatureConstants.Types.Undead] = [None],
                [CreatureConstants.Types.Vermin] = [None]
            };

            return testCases;
        }

        private static string GetData(string saveName, int bonus, string condition = "")
        {
            return DataHelper.Parse(new BonusDataSelection
            {
                Target = saveName,
                Bonus = bonus,
                Condition = condition
            });
        }

        public static Dictionary<string, List<string>> GetSaveBonusesData()
        {
            return GetCreatureSaveBonuses()
                .Union(GetTemplateSaveBonuses())
                .Union(GetTypeSaveBonuses())
                .Union(GetSubtypeSaveBonuses())
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static Dictionary<string, List<string>> GetSubtypeSaveBonuses()
        {
            var testCases = new Dictionary<string, List<string>>();
            var creatures = CreatureConstants.GetAll();

            //INFO: Some subtypes, such as Gnoll, are duplicate of the creature entries
            var subtypes = CreatureConstants.Types.Subtypes.GetAll().Except(creatures);

            foreach (var subtype in subtypes)
            {
                testCases[subtype] = [];
            }

            testCases[CreatureConstants.Types.Subtypes.Air] = [None];

            testCases[CreatureConstants.Types.Subtypes.Angel] = [GetData(SaveConstants.Fortitude, 4, "against poison")];

            testCases[CreatureConstants.Types.Subtypes.Aquatic] = [None];

            testCases[CreatureConstants.Types.Subtypes.Archon] = [GetData(SaveConstants.Fortitude, 4, "against poison")];

            testCases[CreatureConstants.Types.Subtypes.Augmented] = [None];

            testCases[CreatureConstants.Types.Subtypes.Chaotic] = [None];

            testCases[CreatureConstants.Types.Subtypes.Cold] = [None];

            testCases[CreatureConstants.Types.Subtypes.Dwarf] = [None];

            testCases[CreatureConstants.Types.Subtypes.Earth] = [None];

            testCases[CreatureConstants.Types.Subtypes.Elf] = [GetData(GroupConstants.All, 2, "enchantment spells or effects")];

            testCases[CreatureConstants.Types.Subtypes.Evil] = [None];

            testCases[CreatureConstants.Types.Subtypes.Extraplanar] = [None];

            testCases[CreatureConstants.Types.Subtypes.Fire] = [None];

            testCases[CreatureConstants.Types.Subtypes.Gnome] = [None];

            testCases[CreatureConstants.Types.Subtypes.Goblinoid] = [None];

            testCases[CreatureConstants.Types.Subtypes.Good] = [None];

            testCases[CreatureConstants.Types.Subtypes.Halfling] = [GetData(GroupConstants.All, 1), GetData(GroupConstants.All, 2, "morale against fear")];

            testCases[CreatureConstants.Types.Subtypes.Incorporeal] = [None];

            testCases[CreatureConstants.Types.Subtypes.Lawful] = [None];

            testCases[CreatureConstants.Types.Subtypes.Native] = [None];

            testCases[CreatureConstants.Types.Subtypes.Reptilian] = [None];

            testCases[CreatureConstants.Types.Subtypes.Shapechanger] = [None];

            testCases[CreatureConstants.Types.Subtypes.Swarm] = [None];

            testCases[CreatureConstants.Types.Subtypes.Water] = [None];

            return testCases;
        }

        public static Dictionary<string, List<string>> GetTemplateSaveBonuses()
        {
            var testCases = new Dictionary<string, List<string>>();
            var templates = CreatureConstants.Templates.GetAll();

            foreach (var template in templates)
            {
                testCases[template] = [];
            }

            testCases[CreatureConstants.Templates.None] = [None];
            testCases[CreatureConstants.Templates.CelestialCreature] = [None];
            testCases[CreatureConstants.Templates.FiendishCreature] = [None];
            testCases[CreatureConstants.Templates.HalfCelestial] = [None];
            testCases[CreatureConstants.Templates.HalfDragon_Black] = [None];
            testCases[CreatureConstants.Templates.HalfDragon_Blue] = [None];
            testCases[CreatureConstants.Templates.HalfDragon_Brass] = [None];
            testCases[CreatureConstants.Templates.HalfDragon_Bronze] = [None];
            testCases[CreatureConstants.Templates.HalfDragon_Copper] = [None];
            testCases[CreatureConstants.Templates.HalfDragon_Gold] = [None];
            testCases[CreatureConstants.Templates.HalfDragon_Green] = [None];
            testCases[CreatureConstants.Templates.HalfDragon_Red] = [None];
            testCases[CreatureConstants.Templates.HalfDragon_Silver] = [None];
            testCases[CreatureConstants.Templates.HalfDragon_White] = [None];
            testCases[CreatureConstants.Templates.HalfFiend] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Afflicted] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted] = [None];
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural] = [None];
            testCases[CreatureConstants.Templates.Skeleton] = [None];
            testCases[CreatureConstants.Templates.Zombie] = [None];
            testCases[CreatureConstants.Templates.Vampire] = [None];
            testCases[CreatureConstants.Templates.Ghost] = [None];
            testCases[CreatureConstants.Templates.Lich] = [None];

            return testCases;
        }
    }
}
