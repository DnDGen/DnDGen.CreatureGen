using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Defenses
{
    public class SaveBonusesTestData
    {
        public const string None = "NONE";

        public static Dictionary<string, List<string>> GetCreatureSaveBonuses()
        {
            var testCases = new Dictionary<string, List<string>>();

            testCases[CreatureConstants.Aasimar] = [None];

            testCases[CreatureConstants.Aboleth] = [None];

            testCases[CreatureConstants.Achaierai] = [None];

            testCases[CreatureConstants.Allip] = [None];

            testCases[CreatureConstants.Androsphinx] = [None];

            testCases[CreatureConstants.Angel_AstralDeva] = [None];

            testCases[CreatureConstants.Angel_Planetar] = [None];

            testCases[CreatureConstants.Angel_Solar] = [None];

            testCases[CreatureConstants.AnimatedObject_Colossal] = [None];
            testCases[CreatureConstants.AnimatedObject_Colossal_Flexible] = [None];
            testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Colossal_Sheetlike] = [None];
            testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Colossal_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Gargantuan] = [None];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Flexible] = [None];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike] = [None];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Huge] = [None];
            testCases[CreatureConstants.AnimatedObject_Huge_Flexible] = [None];
            testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Huge_Sheetlike] = [None];
            testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Huge_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Large] = [None];
            testCases[CreatureConstants.AnimatedObject_Large_Flexible] = [None];
            testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Large_Sheetlike] = [None];
            testCases[CreatureConstants.AnimatedObject_Large_TwoLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Large_Wheels_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Large_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Medium] = [None];
            testCases[CreatureConstants.AnimatedObject_Medium_Flexible] = [None];
            testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Medium_Sheetlike] = [None];
            testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Medium_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Small] = [None];
            testCases[CreatureConstants.AnimatedObject_Small_Flexible] = [None];
            testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Small_Sheetlike] = [None];
            testCases[CreatureConstants.AnimatedObject_Small_TwoLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Small_Wheels_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Small_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Tiny] = [None];
            testCases[CreatureConstants.AnimatedObject_Tiny_Flexible] = [None];
            testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Tiny_Sheetlike] = [None];
            testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs] = [None];
            testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden] = [None];
            testCases[CreatureConstants.AnimatedObject_Tiny_Wooden] = [None];

            testCases[CreatureConstants.Ankheg] = [None];

            testCases[CreatureConstants.Annis] = [None];

            testCases[CreatureConstants.Ant_Giant_Queen] = [None];

            testCases[CreatureConstants.Ant_Giant_Soldier] = [None];

            testCases[CreatureConstants.Ant_Giant_Worker] = [None];

            testCases[CreatureConstants.Ape] = [None];

            testCases[CreatureConstants.Ape_Dire] = [None];

            testCases[CreatureConstants.Aranea] = [None];

            testCases[CreatureConstants.Arrowhawk_Juvenile] = [None];

            testCases[CreatureConstants.Arrowhawk_Adult] = [None];

            testCases[CreatureConstants.Arrowhawk_Elder] = [None];

            testCases[CreatureConstants.AssassinVine] = [None];

            testCases[CreatureConstants.Athach] = [None];

            testCases[CreatureConstants.Avoral] = [None];

            testCases[CreatureConstants.Azer] = [None];

            testCases[CreatureConstants.Babau] = [None];

            testCases[CreatureConstants.Baboon] = [None];

            testCases[CreatureConstants.Badger] = [None];

            testCases[CreatureConstants.Badger_Dire] = [None];

            testCases[CreatureConstants.Balor] = [None];

            testCases[CreatureConstants.BarbedDevil_Hamatula] = [None];

            testCases[CreatureConstants.Barghest] = [None];

            testCases[CreatureConstants.Barghest_Greater] = [None];

            testCases[CreatureConstants.Basilisk] = [None];

            testCases[CreatureConstants.Basilisk_Greater] = [None];

            testCases[CreatureConstants.Bat] = [None];

            testCases[CreatureConstants.Bat_Dire] = [None];

            testCases[CreatureConstants.Bat_Swarm] = [None];

            testCases[CreatureConstants.Bear_Black] = [None];

            testCases[CreatureConstants.Bear_Brown] = [None];

            testCases[CreatureConstants.Bear_Dire] = [None];

            testCases[CreatureConstants.Bear_Polar] = [None];

            testCases[CreatureConstants.BeardedDevil_Barbazu] = [None];

            testCases[CreatureConstants.Bebilith] = [None];

            testCases[CreatureConstants.Bee_Giant] = [None];

            testCases[CreatureConstants.Behir] = [None];

            testCases[CreatureConstants.Beholder] = [None];

            testCases[CreatureConstants.Beholder_Gauth] = [None];

            testCases[CreatureConstants.Belker] = [None];

            testCases[CreatureConstants.Bison] = [None];

            testCases[CreatureConstants.BlackPudding] = [None];

            testCases[CreatureConstants.BlackPudding_Elder] = [None];

            testCases[CreatureConstants.BlinkDog] = [None];

            testCases[CreatureConstants.Boar] = [None];

            testCases[CreatureConstants.Boar_Dire] = [None];

            testCases[CreatureConstants.Bodak] = [None];

            testCases[CreatureConstants.BombardierBeetle_Giant] = [None];

            testCases[CreatureConstants.BoneDevil_Osyluth] = [None];

            testCases[CreatureConstants.Bralani] = [None];

            testCases[CreatureConstants.Bugbear] = [None];

            testCases[CreatureConstants.Bulette] = [None];

            testCases[CreatureConstants.Camel_Bactrian] = [None];

            testCases[CreatureConstants.Camel_Dromedary] = [None];

            testCases[CreatureConstants.CarrionCrawler] = [None];

            testCases[CreatureConstants.Cat] = [None];

            testCases[CreatureConstants.Centaur] = [None];

            testCases[CreatureConstants.Centipede_Monstrous_Colossal] = [None];

            testCases[CreatureConstants.Centipede_Monstrous_Gargantuan] = [None];

            testCases[CreatureConstants.Centipede_Monstrous_Huge] = [None];

            testCases[CreatureConstants.Centipede_Monstrous_Large] = [None];

            testCases[CreatureConstants.Centipede_Monstrous_Medium] = [None];

            testCases[CreatureConstants.Centipede_Monstrous_Small] = [None];

            testCases[CreatureConstants.Centipede_Monstrous_Tiny] = [None];

            testCases[CreatureConstants.Centipede_Swarm] = [None];

            testCases[CreatureConstants.ChainDevil_Kyton] = [None];

            testCases[CreatureConstants.ChaosBeast] = [None];

            testCases[CreatureConstants.Cheetah] = [None];

            testCases[CreatureConstants.Chimera_Black] = [None];

            testCases[CreatureConstants.Chimera_Blue] = [None];

            testCases[CreatureConstants.Chimera_Green] = [None];

            testCases[CreatureConstants.Chimera_Red] = [None];

            testCases[CreatureConstants.Chimera_White] = [None];

            testCases[CreatureConstants.Choker] = [None];

            testCases[CreatureConstants.Chuul] = [None];

            testCases[CreatureConstants.Cloaker] = [None];

            testCases[CreatureConstants.Cockatrice] = [None];

            testCases[CreatureConstants.Couatl] = [None];

            testCases[CreatureConstants.Criosphinx] = [None];

            testCases[CreatureConstants.Crocodile] = [None];

            testCases[CreatureConstants.Crocodile_Giant] = [None];

            testCases[CreatureConstants.Cryohydra_5Heads] = [None];

            testCases[CreatureConstants.Cryohydra_6Heads] = [None];

            testCases[CreatureConstants.Cryohydra_7Heads] = [None];

            testCases[CreatureConstants.Cryohydra_8Heads] = [None];

            testCases[CreatureConstants.Cryohydra_9Heads] = [None];

            testCases[CreatureConstants.Cryohydra_10Heads] = [None];

            testCases[CreatureConstants.Cryohydra_11Heads] = [None];

            testCases[CreatureConstants.Cryohydra_12Heads] = [None];

            testCases[CreatureConstants.Darkmantle] = [None];

            testCases[CreatureConstants.Deinonychus] = [None];

            testCases[CreatureConstants.Delver] = [None];

            testCases[CreatureConstants.Derro] = [None];

            testCases[CreatureConstants.Derro_Sane] = [None];

            testCases[CreatureConstants.Destrachan] = [None];

            testCases[CreatureConstants.Devourer] = [None];

            testCases[CreatureConstants.Digester] = [None];

            testCases[CreatureConstants.DisplacerBeast] =
                [GetData(GroupConstants.All, 2, "ranged magical attacks that specifically target the displacer beast, except for ranged touch attacks")];

            testCases[CreatureConstants.DisplacerBeast_PackLord] =
                [GetData(GroupConstants.All, 2, "ranged magical attacks that specifically target the displacer beast, except for ranged touch attacks")];

            testCases[CreatureConstants.Djinni] = [None];

            testCases[CreatureConstants.Djinni_Noble] = [None];

            testCases[CreatureConstants.Dog] = [None];

            testCases[CreatureConstants.Dog_Riding] = [None];

            testCases[CreatureConstants.Donkey] = [None];

            testCases[CreatureConstants.Doppelganger] = [None];

            testCases[CreatureConstants.Dragon_Black_Wyrmling] = [None];

            testCases[CreatureConstants.Dragon_Black_VeryYoung] = [None];

            testCases[CreatureConstants.Dragon_Black_Young] = [None];

            testCases[CreatureConstants.Dragon_Black_Juvenile] = [None];

            testCases[CreatureConstants.Dragon_Black_YoungAdult] = [None];

            testCases[CreatureConstants.Dragon_Black_Adult] = [None];

            testCases[CreatureConstants.Dragon_Black_MatureAdult] = [None];

            testCases[CreatureConstants.Dragon_Black_Old] = [None];

            testCases[CreatureConstants.Dragon_Black_VeryOld] = [None];

            testCases[CreatureConstants.Dragon_Black_Ancient] = [None];

            testCases[CreatureConstants.Dragon_Black_Wyrm] = [None];

            testCases[CreatureConstants.Dragon_Black_GreatWyrm] = [None];

            testCases[CreatureConstants.Dragon_Blue_Wyrmling] = [None];

            testCases[CreatureConstants.Dragon_Blue_VeryYoung] = [None];

            testCases[CreatureConstants.Dragon_Blue_Young] = [None];

            testCases[CreatureConstants.Dragon_Blue_Juvenile] = [None];

            testCases[CreatureConstants.Dragon_Blue_YoungAdult] = [None];

            testCases[CreatureConstants.Dragon_Blue_Adult] = [None];

            testCases[CreatureConstants.Dragon_Blue_MatureAdult] = [None];

            testCases[CreatureConstants.Dragon_Blue_Old] = [None];

            testCases[CreatureConstants.Dragon_Blue_VeryOld] = [None];

            testCases[CreatureConstants.Dragon_Blue_Ancient] = [None];

            testCases[CreatureConstants.Dragon_Blue_Wyrm] = [None];

            testCases[CreatureConstants.Dragon_Blue_GreatWyrm] = [None];

            testCases[CreatureConstants.Dragon_Green_Wyrmling] = [None];

            testCases[CreatureConstants.Dragon_Green_VeryYoung] = [None];

            testCases[CreatureConstants.Dragon_Green_Young] = [None];

            testCases[CreatureConstants.Dragon_Green_Juvenile] = [None];

            testCases[CreatureConstants.Dragon_Green_YoungAdult] = [None];

            testCases[CreatureConstants.Dragon_Green_Adult] = [None];

            testCases[CreatureConstants.Dragon_Green_MatureAdult] = [None];

            testCases[CreatureConstants.Dragon_Green_Old] = [None];

            testCases[CreatureConstants.Dragon_Green_VeryOld] = [None];

            testCases[CreatureConstants.Dragon_Green_Ancient] = [None];

            testCases[CreatureConstants.Dragon_Green_Wyrm] = [None];

            testCases[CreatureConstants.Dragon_Green_GreatWyrm] = [None];

            testCases[CreatureConstants.Dragon_Red_Wyrmling] = [None];

            testCases[CreatureConstants.Dragon_Red_VeryYoung] = [None];

            testCases[CreatureConstants.Dragon_Red_Young] = [None];

            testCases[CreatureConstants.Dragon_Red_Juvenile] = [None];

            testCases[CreatureConstants.Dragon_Red_YoungAdult] = [None];

            testCases[CreatureConstants.Dragon_Red_Adult] = [None];

            testCases[CreatureConstants.Dragon_Red_MatureAdult] = [None];

            testCases[CreatureConstants.Dragon_Red_Old] = [None];

            testCases[CreatureConstants.Dragon_Red_VeryOld] = [None];

            testCases[CreatureConstants.Dragon_Red_Ancient] = [None];

            testCases[CreatureConstants.Dragon_Red_Wyrm] = [None];

            testCases[CreatureConstants.Dragon_Red_GreatWyrm] = [None];

            testCases[CreatureConstants.Dragon_White_Wyrmling] = [None];

            testCases[CreatureConstants.Dragon_White_VeryYoung] = [None];

            testCases[CreatureConstants.Dragon_White_Young] = [None];

            testCases[CreatureConstants.Dragon_White_Juvenile] = [None];

            testCases[CreatureConstants.Dragon_White_YoungAdult] = [None];

            testCases[CreatureConstants.Dragon_White_Adult] = [None];

            testCases[CreatureConstants.Dragon_White_MatureAdult] = [None];

            testCases[CreatureConstants.Dragon_White_Old] = [None];

            testCases[CreatureConstants.Dragon_White_VeryOld] = [None];

            testCases[CreatureConstants.Dragon_White_Ancient] = [None];

            testCases[CreatureConstants.Dragon_White_Wyrm] = [None];

            testCases[CreatureConstants.Dragon_White_GreatWyrm] = [None];

            testCases[CreatureConstants.Dragon_Brass_Wyrmling] = [None];

            testCases[CreatureConstants.Dragon_Brass_VeryYoung] = [None];

            testCases[CreatureConstants.Dragon_Brass_Young] = [None];

            testCases[CreatureConstants.Dragon_Brass_Juvenile] = [None];

            testCases[CreatureConstants.Dragon_Brass_YoungAdult] = [None];

            testCases[CreatureConstants.Dragon_Brass_Adult] = [None];

            testCases[CreatureConstants.Dragon_Brass_MatureAdult] = [None];

            testCases[CreatureConstants.Dragon_Brass_Old] = [None];

            testCases[CreatureConstants.Dragon_Brass_VeryOld] = [None];

            testCases[CreatureConstants.Dragon_Brass_Ancient] = [None];

            testCases[CreatureConstants.Dragon_Brass_Wyrm] = [None];

            testCases[CreatureConstants.Dragon_Brass_GreatWyrm] = [None];

            testCases[CreatureConstants.Dragon_Bronze_Wyrmling] = [None];

            testCases[CreatureConstants.Dragon_Bronze_VeryYoung] = [None];

            testCases[CreatureConstants.Dragon_Bronze_Young] = [None];

            testCases[CreatureConstants.Dragon_Bronze_Juvenile] = [None];

            testCases[CreatureConstants.Dragon_Bronze_YoungAdult] = [None];

            testCases[CreatureConstants.Dragon_Bronze_Adult] = [None];

            testCases[CreatureConstants.Dragon_Bronze_MatureAdult] = [None];

            testCases[CreatureConstants.Dragon_Bronze_Old] = [None];

            testCases[CreatureConstants.Dragon_Bronze_VeryOld] = [None];

            testCases[CreatureConstants.Dragon_Bronze_Ancient] = [None];

            testCases[CreatureConstants.Dragon_Bronze_Wyrm] = [None];

            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm] = [None];

            testCases[CreatureConstants.Dragon_Copper_Wyrmling] = [None];

            testCases[CreatureConstants.Dragon_Copper_VeryYoung] = [None];

            testCases[CreatureConstants.Dragon_Copper_Young] = [None];

            testCases[CreatureConstants.Dragon_Copper_Juvenile] = [None];

            testCases[CreatureConstants.Dragon_Copper_YoungAdult] = [None];

            testCases[CreatureConstants.Dragon_Copper_Adult] = [None];

            testCases[CreatureConstants.Dragon_Copper_MatureAdult] = [None];

            testCases[CreatureConstants.Dragon_Copper_Old] = [None];

            testCases[CreatureConstants.Dragon_Copper_VeryOld] = [None];

            testCases[CreatureConstants.Dragon_Copper_Ancient] = [None];

            testCases[CreatureConstants.Dragon_Copper_Wyrm] = [None];

            testCases[CreatureConstants.Dragon_Copper_GreatWyrm] = [None];

            testCases[CreatureConstants.Dragon_Gold_Wyrmling] = [None];

            testCases[CreatureConstants.Dragon_Gold_VeryYoung] = [None];

            testCases[CreatureConstants.Dragon_Gold_Young] = [None];

            testCases[CreatureConstants.Dragon_Gold_Juvenile] = [None];

            testCases[CreatureConstants.Dragon_Gold_YoungAdult] = [None];

            testCases[CreatureConstants.Dragon_Gold_Adult] = [None];

            testCases[CreatureConstants.Dragon_Gold_MatureAdult] = [None];

            testCases[CreatureConstants.Dragon_Gold_Old] = [None];

            testCases[CreatureConstants.Dragon_Gold_VeryOld] = [None];

            testCases[CreatureConstants.Dragon_Gold_Ancient] = [None];

            testCases[CreatureConstants.Dragon_Gold_Wyrm] = [None];

            testCases[CreatureConstants.Dragon_Gold_GreatWyrm] = [None];

            testCases[CreatureConstants.Dragon_Silver_Wyrmling] = [None];

            testCases[CreatureConstants.Dragon_Silver_VeryYoung] = [None];

            testCases[CreatureConstants.Dragon_Silver_Young] = [None];

            testCases[CreatureConstants.Dragon_Silver_Juvenile] = [None];

            testCases[CreatureConstants.Dragon_Silver_YoungAdult] = [None];

            testCases[CreatureConstants.Dragon_Silver_Adult] = [None];

            testCases[CreatureConstants.Dragon_Silver_MatureAdult] = [None];

            testCases[CreatureConstants.Dragon_Silver_Old] = [None];

            testCases[CreatureConstants.Dragon_Silver_VeryOld] = [None];

            testCases[CreatureConstants.Dragon_Silver_Ancient] = [None];

            testCases[CreatureConstants.Dragon_Silver_Wyrm] = [None];

            testCases[CreatureConstants.Dragon_Silver_GreatWyrm] = [None];

            testCases[CreatureConstants.DragonTurtle] = [None];

            testCases[CreatureConstants.Dragonne] = [None];

            testCases[CreatureConstants.Dretch] = [None];

            testCases[CreatureConstants.Drider] = [None];

            testCases[CreatureConstants.Dryad] = [None];

            testCases[CreatureConstants.Dwarf_Deep] = [GetData(SaveConstants.Fortitude, 3, "against poison")];
            testCases[CreatureConstants.Dwarf_Deep] = [GetData(GroupConstants.All, 3, "against spells and spell-like abilities")];

            testCases[CreatureConstants.Dwarf_Duergar] = [GetData(GroupConstants.All, 2, "against spells and spell-like abilities")];

            testCases[CreatureConstants.Dwarf_Hill] = [GetData(SaveConstants.Fortitude, 2, "against poison")];
            testCases[CreatureConstants.Dwarf_Hill] = [GetData(GroupConstants.All, 2, "against spells and spell-like abilities")];

            testCases[CreatureConstants.Dwarf_Mountain] = [GetData(SaveConstants.Fortitude, 2, "against poison")];
            testCases[CreatureConstants.Dwarf_Mountain] = [GetData(GroupConstants.All, 2, "against spells and spell-like abilities")];

            testCases[CreatureConstants.Eagle] = [None];

            testCases[CreatureConstants.Eagle_Giant] = [None];

            testCases[CreatureConstants.Efreeti] = [None];

            testCases[CreatureConstants.Elasmosaurus] = [None];

            testCases[CreatureConstants.Elemental_Air_Small] = [None];

            testCases[CreatureConstants.Elemental_Air_Medium] = [None];

            testCases[CreatureConstants.Elemental_Air_Large] = [None];

            testCases[CreatureConstants.Elemental_Air_Huge] = [None];

            testCases[CreatureConstants.Elemental_Air_Greater] = [None];

            testCases[CreatureConstants.Elemental_Air_Elder] = [None];

            testCases[CreatureConstants.Elemental_Earth_Small] = [None];

            testCases[CreatureConstants.Elemental_Earth_Medium] = [None];

            testCases[CreatureConstants.Elemental_Earth_Large] = [None];

            testCases[CreatureConstants.Elemental_Earth_Huge] = [None];

            testCases[CreatureConstants.Elemental_Earth_Greater] = [None];

            testCases[CreatureConstants.Elemental_Earth_Elder] = [None];

            testCases[CreatureConstants.Elemental_Fire_Small] = [None];

            testCases[CreatureConstants.Elemental_Fire_Medium] = [None];

            testCases[CreatureConstants.Elemental_Fire_Large] = [None];

            testCases[CreatureConstants.Elemental_Fire_Huge] = [None];

            testCases[CreatureConstants.Elemental_Fire_Greater] = [None];

            testCases[CreatureConstants.Elemental_Fire_Elder] = [None];

            testCases[CreatureConstants.Elemental_Water_Small] = [None];

            testCases[CreatureConstants.Elemental_Water_Medium] = [None];

            testCases[CreatureConstants.Elemental_Water_Large] = [None];

            testCases[CreatureConstants.Elemental_Water_Huge] = [None];

            testCases[CreatureConstants.Elemental_Water_Greater] = [None];

            testCases[CreatureConstants.Elemental_Water_Elder] = [None];

            testCases[CreatureConstants.Elephant] = [None];

            testCases[CreatureConstants.Elf_Aquatic] = [None];

            testCases[CreatureConstants.Elf_Drow] = [GetData(SaveConstants.Will, 2, "spells and spell-like abilities")];

            testCases[CreatureConstants.Elf_Gray] = [None];

            testCases[CreatureConstants.Elf_Half] = [None];

            testCases[CreatureConstants.Elf_High] = [None];

            testCases[CreatureConstants.Elf_Wild] = [None];

            testCases[CreatureConstants.Elf_Wood] = [None];

            testCases[CreatureConstants.Erinyes] = [None];

            testCases[CreatureConstants.EtherealFilcher] = [None];

            testCases[CreatureConstants.EtherealMarauder] = [None];

            testCases[CreatureConstants.Ettercap] = [None];

            testCases[CreatureConstants.Ettin] = [None];

            testCases[CreatureConstants.FireBeetle_Giant] = [None];

            testCases[CreatureConstants.FormianMyrmarch] = [None];

            testCases[CreatureConstants.FormianQueen] = [None];

            testCases[CreatureConstants.FormianTaskmaster] = [None];

            testCases[CreatureConstants.FormianWarrior] = [None];

            testCases[CreatureConstants.FormianWorker] = [None];

            testCases[CreatureConstants.FrostWorm] = [None];

            testCases[CreatureConstants.Gargoyle] = [None];

            testCases[CreatureConstants.Gargoyle_Kapoacinth] = [None];

            testCases[CreatureConstants.GelatinousCube] = [None];

            testCases[CreatureConstants.Ghaele] = [None];

            testCases[CreatureConstants.Ghoul] = [None];

            testCases[CreatureConstants.Ghoul_Ghast] = [None];

            testCases[CreatureConstants.Ghoul_Lacedon] = [None];

            testCases[CreatureConstants.Giant_Cloud] = [None];

            testCases[CreatureConstants.Giant_Fire] = [None];

            testCases[CreatureConstants.Giant_Frost] = [None];

            testCases[CreatureConstants.Giant_Hill] = [None];

            testCases[CreatureConstants.Giant_Stone] = [None];

            testCases[CreatureConstants.Giant_Stone_Elder] = [None];

            testCases[CreatureConstants.Giant_Storm] = [None];

            testCases[CreatureConstants.GibberingMouther] = [None];

            testCases[CreatureConstants.Girallon] = [None];

            testCases[CreatureConstants.Githyanki] = [None];

            testCases[CreatureConstants.Githzerai] = [None];

            testCases[CreatureConstants.Glabrezu] = [None];

            testCases[CreatureConstants.Gnoll] = [None];

            testCases[CreatureConstants.Gnome_Forest] = [GetData(GroupConstants.All, 2, "against illusions")];

            testCases[CreatureConstants.Gnome_Rock] = [GetData(GroupConstants.All, 2, "against illusions")];

            testCases[CreatureConstants.Gnome_Svirfneblin] = [GetData(GroupConstants.All, 2)];

            testCases[CreatureConstants.Goblin] = [None];

            testCases[CreatureConstants.Golem_Clay] = [None];

            testCases[CreatureConstants.Golem_Flesh] = [None];

            testCases[CreatureConstants.Golem_Iron] = [None];

            testCases[CreatureConstants.Golem_Stone] = [None];

            testCases[CreatureConstants.Golem_Stone_Greater] = [None];

            testCases[CreatureConstants.Gorgon] = [None];

            testCases[CreatureConstants.GrayOoze] = [None];

            testCases[CreatureConstants.GrayRender] = [None];

            testCases[CreatureConstants.GreenHag] = [None];

            testCases[CreatureConstants.Grick] = [None];

            testCases[CreatureConstants.Griffon] = [None];

            testCases[CreatureConstants.Grig] = [None];

            testCases[CreatureConstants.Grig_WithFiddle] = [None];

            testCases[CreatureConstants.Grimlock] = [None];

            testCases[CreatureConstants.Gynosphinx] = [None];

            testCases[CreatureConstants.Halfling_Deep] = [None];

            testCases[CreatureConstants.Halfling_Lightfoot] = [None];

            testCases[CreatureConstants.Halfling_Tallfellow] = [None];

            testCases[CreatureConstants.Harpy] = [None];

            testCases[CreatureConstants.Hawk] = [None];

            testCases[CreatureConstants.HellHound] = [None];

            testCases[CreatureConstants.HellHound_NessianWarhound] = [None];

            testCases[CreatureConstants.Hellcat_Bezekira] = [None];

            testCases[CreatureConstants.Hellwasp_Swarm] = [None];

            testCases[CreatureConstants.Hezrou] = [None];

            testCases[CreatureConstants.Hieracosphinx] = [None];

            testCases[CreatureConstants.Hippogriff] = [None];

            testCases[CreatureConstants.Hobgoblin] = [None];

            testCases[CreatureConstants.Homunculus] = [None];

            testCases[CreatureConstants.HornedDevil_Cornugon] = [None];

            testCases[CreatureConstants.Horse_Heavy] = [None];

            testCases[CreatureConstants.Horse_Heavy_War] = [None];

            testCases[CreatureConstants.Horse_Light] = [None];

            testCases[CreatureConstants.Horse_Light_War] = [None];

            testCases[CreatureConstants.HoundArchon] = [None];

            testCases[CreatureConstants.Howler] = [None];

            testCases[CreatureConstants.Human] = [None];

            testCases[CreatureConstants.Hydra_5Heads] = [None];

            testCases[CreatureConstants.Hydra_6Heads] = [None];

            testCases[CreatureConstants.Hydra_7Heads] = [None];

            testCases[CreatureConstants.Hydra_8Heads] = [None];

            testCases[CreatureConstants.Hydra_9Heads] = [None];

            testCases[CreatureConstants.Hydra_10Heads] = [None];

            testCases[CreatureConstants.Hydra_11Heads] = [None];

            testCases[CreatureConstants.Hydra_12Heads] = [None];

            testCases[CreatureConstants.Hyena] = [None];

            testCases[CreatureConstants.IceDevil_Gelugon] = [None];

            testCases[CreatureConstants.Imp] = [None];

            testCases[CreatureConstants.InvisibleStalker] = [None];

            testCases[CreatureConstants.Janni] = [None];

            testCases[CreatureConstants.Kobold] = [None];

            testCases[CreatureConstants.Kolyarut] = [None];

            testCases[CreatureConstants.Kraken] = [None];

            testCases[CreatureConstants.Krenshar] = [None];

            testCases[CreatureConstants.KuoToa] = [None];

            testCases[CreatureConstants.Lamia] = [None];

            testCases[CreatureConstants.Lammasu] = [None];

            testCases[CreatureConstants.LanternArchon] = [None];

            testCases[CreatureConstants.Lemure] = [None];

            testCases[CreatureConstants.Leonal] = [None];

            testCases[CreatureConstants.Leopard] = [None];

            testCases[CreatureConstants.Lillend] = [None];

            testCases[CreatureConstants.Lion] = [None];

            testCases[CreatureConstants.Lion_Dire] = [None];

            testCases[CreatureConstants.Lizard] = [None];

            testCases[CreatureConstants.Lizard_Monitor] = [None];

            testCases[CreatureConstants.Lizardfolk] = [None];

            testCases[CreatureConstants.Locathah] = [None];

            testCases[CreatureConstants.Locust_Swarm] = [None];

            testCases[CreatureConstants.Magmin] = [None];

            testCases[CreatureConstants.MantaRay] = [None];

            testCases[CreatureConstants.Manticore] = [None];

            testCases[CreatureConstants.Marilith] = [None];

            testCases[CreatureConstants.Marut] = [None];

            testCases[CreatureConstants.Medusa] = [None];

            testCases[CreatureConstants.Megaraptor] = [None];

            testCases[CreatureConstants.Mephit_Air] = [None];

            testCases[CreatureConstants.Mephit_Dust] = [None];

            testCases[CreatureConstants.Mephit_Earth] = [None];

            testCases[CreatureConstants.Mephit_Fire] = [None];

            testCases[CreatureConstants.Mephit_Ice] = [None];

            testCases[CreatureConstants.Mephit_Magma] = [None];

            testCases[CreatureConstants.Mephit_Ooze] = [None];

            testCases[CreatureConstants.Mephit_Salt] = [None];

            testCases[CreatureConstants.Mephit_Steam] = [None];

            testCases[CreatureConstants.Mephit_Water] = [None];

            testCases[CreatureConstants.Merfolk] = [None];

            testCases[CreatureConstants.Mimic] = [None];

            testCases[CreatureConstants.MindFlayer] = [None];

            testCases[CreatureConstants.Minotaur] = [None];

            testCases[CreatureConstants.Mohrg] = [None];

            testCases[CreatureConstants.Monkey] = [None];

            testCases[CreatureConstants.Mule] = [None];

            testCases[CreatureConstants.Mummy] = [None];

            testCases[CreatureConstants.Naga_Dark] = [GetData(GroupConstants.All, "against charm effects")] = 2;

            testCases[CreatureConstants.Naga_Guardian] = [None];

            testCases[CreatureConstants.Naga_Spirit] = [None];

            testCases[CreatureConstants.Naga_Water] = [None];

            testCases[CreatureConstants.Nalfeshnee] = [None];

            testCases[CreatureConstants.NightHag] = [GetData(GroupConstants.All)] = 2;

            testCases[CreatureConstants.Nightcrawler] = [None];

            testCases[CreatureConstants.Nightmare] = [None];

            testCases[CreatureConstants.Nightmare_Cauchemar] = [None];

            testCases[CreatureConstants.Nightwalker] = [None];

            testCases[CreatureConstants.Nightwing] = [None];

            testCases[CreatureConstants.Nixie] = [None];

            testCases[CreatureConstants.Nymph] = [None];

            testCases[CreatureConstants.OchreJelly] = [None];

            testCases[CreatureConstants.Octopus] = [None];

            testCases[CreatureConstants.Octopus_Giant] = [None];

            testCases[CreatureConstants.Ogre] = [None];

            testCases[CreatureConstants.Ogre_Merrow] = [None];

            testCases[CreatureConstants.OgreMage] = [None];

            testCases[CreatureConstants.Orc] = [None];

            testCases[CreatureConstants.Orc_Half] = [None];

            testCases[CreatureConstants.Otyugh] = [None];

            testCases[CreatureConstants.Owl] = [None];

            testCases[CreatureConstants.Owl_Giant] = [None];

            testCases[CreatureConstants.Owlbear] = [None];

            testCases[CreatureConstants.Pegasus] = [None];

            testCases[CreatureConstants.PhantomFungus] = [None];

            testCases[CreatureConstants.PhaseSpider] = [None];

            testCases[CreatureConstants.Phasm] = [None];

            testCases[CreatureConstants.PitFiend] = [None];

            testCases[CreatureConstants.Pixie] = [None];

            testCases[CreatureConstants.Pixie_WithIrresistibleDance] = [None];

            testCases[CreatureConstants.Pony] = [None];

            testCases[CreatureConstants.Pony_War] = [None];

            testCases[CreatureConstants.Porpoise] = [None];

            testCases[CreatureConstants.PrayingMantis_Giant] = [None];

            testCases[CreatureConstants.Pseudodragon] = [None];

            testCases[CreatureConstants.PurpleWorm] = [None];

            testCases[CreatureConstants.Pyrohydra_5Heads] = [None];

            testCases[CreatureConstants.Pyrohydra_6Heads] = [None];

            testCases[CreatureConstants.Pyrohydra_7Heads] = [None];

            testCases[CreatureConstants.Pyrohydra_8Heads] = [None];

            testCases[CreatureConstants.Pyrohydra_9Heads] = [None];

            testCases[CreatureConstants.Pyrohydra_10Heads] = [None];

            testCases[CreatureConstants.Pyrohydra_11Heads] = [None];

            testCases[CreatureConstants.Pyrohydra_12Heads] = [None];

            testCases[CreatureConstants.Quasit] = [None];

            testCases[CreatureConstants.Rakshasa] = [None];

            testCases[CreatureConstants.Rast] = [None];

            testCases[CreatureConstants.Rat] = [None];

            testCases[CreatureConstants.Rat_Dire] = [None];

            testCases[CreatureConstants.Rat_Swarm] = [None];

            testCases[CreatureConstants.Raven] = [None];

            testCases[CreatureConstants.Ravid] = [None];

            testCases[CreatureConstants.RazorBoar] = [None];

            testCases[CreatureConstants.Remorhaz] = [None];

            testCases[CreatureConstants.Retriever] = [None];

            testCases[CreatureConstants.Rhinoceras] = [None];

            testCases[CreatureConstants.Roc] = [None];

            testCases[CreatureConstants.Roper] = [None];

            testCases[CreatureConstants.RustMonster] = [None];

            testCases[CreatureConstants.Sahuagin] = [None];

            testCases[CreatureConstants.Sahuagin_Malenti] = [None];

            testCases[CreatureConstants.Sahuagin_Mutant] = [None];

            testCases[CreatureConstants.Salamander_Flamebrother] = [None];

            testCases[CreatureConstants.Salamander_Average] = [None];

            testCases[CreatureConstants.Salamander_Noble] = [None];

            testCases[CreatureConstants.Satyr] = [None];

            testCases[CreatureConstants.Satyr_WithPipes] = [None];

            testCases[CreatureConstants.Scorpion_Monstrous_Colossal] = [None];

            testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan] = [None];

            testCases[CreatureConstants.Scorpion_Monstrous_Huge] = [None];

            testCases[CreatureConstants.Scorpion_Monstrous_Large] = [None];

            testCases[CreatureConstants.Scorpion_Monstrous_Medium] = [None];

            testCases[CreatureConstants.Scorpion_Monstrous_Small] = [None];

            testCases[CreatureConstants.Scorpion_Monstrous_Tiny] = [None];

            testCases[CreatureConstants.Scorpionfolk] = [None];

            testCases[CreatureConstants.SeaCat] = [None];

            testCases[CreatureConstants.SeaHag] = [None];

            testCases[CreatureConstants.Shadow] = [None];

            testCases[CreatureConstants.Shadow_Greater] = [None];

            testCases[CreatureConstants.ShadowMastiff] = [None];

            testCases[CreatureConstants.ShamblingMound] = [None];

            testCases[CreatureConstants.Shark_Dire] = [None];

            testCases[CreatureConstants.Shark_Huge] = [None];

            testCases[CreatureConstants.Shark_Large] = [None];

            testCases[CreatureConstants.Shark_Medium] = [None];

            testCases[CreatureConstants.ShieldGuardian] = [None];

            testCases[CreatureConstants.ShockerLizard] = [None];

            testCases[CreatureConstants.Shrieker] = [None];

            testCases[CreatureConstants.Skum] = [None];

            testCases[CreatureConstants.Slaad_Red] = [None];

            testCases[CreatureConstants.Slaad_Blue] = [None];

            testCases[CreatureConstants.Slaad_Green] = [None];

            testCases[CreatureConstants.Slaad_Gray] = [None];

            testCases[CreatureConstants.Slaad_Death] = [None];

            testCases[CreatureConstants.Snake_Constrictor] = [None];

            testCases[CreatureConstants.Snake_Constrictor_Giant] = [None];

            testCases[CreatureConstants.Snake_Viper_Tiny] = [None];

            testCases[CreatureConstants.Snake_Viper_Small] = [None];

            testCases[CreatureConstants.Snake_Viper_Medium] = [None];

            testCases[CreatureConstants.Snake_Viper_Large] = [None];

            testCases[CreatureConstants.Snake_Viper_Huge] = [None];

            testCases[CreatureConstants.Spectre] = [None];

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal] = [None];

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = [None];

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge] = [None];

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Large] = [None];

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium] = [None];

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Small] = [None];

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny] = [None];

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = [None];

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = [None];

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = [None];

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large] = [None];

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = [None];

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small] = [None];

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = [None];

            testCases[CreatureConstants.SpiderEater] = [None];

            testCases[CreatureConstants.Spider_Swarm] = [None];

            testCases[CreatureConstants.Squid] = [None];

            testCases[CreatureConstants.Squid_Giant] = [None];

            testCases[CreatureConstants.StagBeetle_Giant] = [None];

            testCases[CreatureConstants.Stirge] = [None];

            testCases[CreatureConstants.Succubus] = [None];

            testCases[CreatureConstants.Tarrasque] = [None];

            testCases[CreatureConstants.Tendriculos] = [None];

            testCases[CreatureConstants.Thoqqua] = [None];

            testCases[CreatureConstants.Tiefling] = [None];

            testCases[CreatureConstants.Tiger] = [None];

            testCases[CreatureConstants.Tiger_Dire] = [None];

            testCases[CreatureConstants.Titan] = [None];

            testCases[CreatureConstants.Toad] = [None];

            testCases[CreatureConstants.Tojanida_Juvenile] = [None];

            testCases[CreatureConstants.Tojanida_Adult] = [None];

            testCases[CreatureConstants.Tojanida_Elder] = [None];

            testCases[CreatureConstants.Treant] = [None];

            testCases[CreatureConstants.Triceratops] = [None];

            testCases[CreatureConstants.Triton] = [None];

            testCases[CreatureConstants.Troglodyte] = [None];

            testCases[CreatureConstants.Troll] = [None];

            testCases[CreatureConstants.Troll_Scrag] = [None];

            testCases[CreatureConstants.TrumpetArchon] = [None];

            testCases[CreatureConstants.Tyrannosaurus] = [None];

            testCases[CreatureConstants.UmberHulk] = [None];

            testCases[CreatureConstants.UmberHulk_TrulyHorrid] = [None];

            testCases[CreatureConstants.Unicorn] = [None];

            testCases[CreatureConstants.VampireSpawn] = [None];

            testCases[CreatureConstants.Vargouille] = [None];

            testCases[CreatureConstants.VioletFungus] = [None];

            testCases[CreatureConstants.Vrock] = [None];

            testCases[CreatureConstants.Wasp_Giant] = [None];

            testCases[CreatureConstants.Weasel] = [None];

            testCases[CreatureConstants.Weasel_Dire] = [None];

            testCases[CreatureConstants.Whale_Baleen] = [None];

            testCases[CreatureConstants.Whale_Cachalot] = [None];

            testCases[CreatureConstants.Whale_Orca] = [None];

            testCases[CreatureConstants.Wight] = [None];

            testCases[CreatureConstants.WillOWisp] = [None];

            testCases[CreatureConstants.WinterWolf] = [None];

            testCases[CreatureConstants.Wolf] = [None];

            testCases[CreatureConstants.Wolf_Dire] = [None];

            testCases[CreatureConstants.Wolverine] = [None];

            testCases[CreatureConstants.Wolverine_Dire] = [None];

            testCases[CreatureConstants.Worg] = [None];

            testCases[CreatureConstants.Wraith] = [None];

            testCases[CreatureConstants.Wraith_Dread] = [None];

            testCases[CreatureConstants.Wyvern] = [None];

            testCases[CreatureConstants.Xill] = [None];

            testCases[CreatureConstants.Xorn_Minor] = [None];

            testCases[CreatureConstants.Xorn_Average] = [None];

            testCases[CreatureConstants.Xorn_Elder] = [None];

            testCases[CreatureConstants.YethHound] = [None];

            testCases[CreatureConstants.Yrthak] = [None];

            testCases[CreatureConstants.YuanTi_Abomination] = [None];

            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms] = [None];

            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead] = [None];

            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail] = [None];

            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = [None];

            testCases[CreatureConstants.YuanTi_Pureblood] = [None];

            testCases[CreatureConstants.Zelekhut] = [None];

            return testCases;
        }

        public static IEnumerable Types
        {
            get
            {
                var testCases = new Dictionary<string, List<string>>();

                testCases[CreatureConstants.Types.Aberration] = [None];

                testCases[CreatureConstants.Types.Animal] = [None];

                testCases[CreatureConstants.Types.Construct] = [None];

                testCases[CreatureConstants.Types.Dragon] = [None];

                testCases[CreatureConstants.Types.Elemental] = [None];

                testCases[CreatureConstants.Types.Fey] = [None];

                testCases[CreatureConstants.Types.Giant] = [None];

                testCases[CreatureConstants.Types.Humanoid] = [None];

                testCases[CreatureConstants.Types.MagicalBeast] = [None];

                testCases[CreatureConstants.Types.MonstrousHumanoid] = [None];

                testCases[CreatureConstants.Types.Ooze] = [None];

                testCases[CreatureConstants.Types.Outsider] = [None];

                testCases[CreatureConstants.Types.Plant] = [None];

                testCases[CreatureConstants.Types.Undead] = [None];

                testCases[CreatureConstants.Types.Vermin] = [None];

                return TestDataHelper.EnumerateTestCases(testCases);
            }
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

        public static IEnumerable Subtypes
        {
            get
            {
                var testCases = new Dictionary<string, List<string>>();
                var subtypes = CreatureConstants.Types.Subtypes.GetAll()
                    .Except(new[]
                    {
                        CreatureConstants.Types.Subtypes.Gnoll,
                        CreatureConstants.Types.Subtypes.Human,
                        CreatureConstants.Types.Subtypes.Orc,
                    }); //INFO: This is duplicated from the creature entry

                foreach (var subtype in subtypes)
                {
                    testCases[subtype] = new List<string>();
                }

                testCases[CreatureConstants.Types.Subtypes.Air] = [None];

                testCases[CreatureConstants.Types.Subtypes.Angel] = [GetData(SaveConstants.Fortitude, "against poison")] = 4;

                testCases[CreatureConstants.Types.Subtypes.Aquatic] = [None];

                testCases[CreatureConstants.Types.Subtypes.Archon] = [GetData(SaveConstants.Fortitude, "against poison")] = 4;

                testCases[CreatureConstants.Types.Subtypes.Augmented] = [None];

                testCases[CreatureConstants.Types.Subtypes.Chaotic] = [None];

                testCases[CreatureConstants.Types.Subtypes.Cold] = [None];

                testCases[CreatureConstants.Types.Subtypes.Dwarf] = [None];

                testCases[CreatureConstants.Types.Subtypes.Earth] = [None];

                testCases[CreatureConstants.Types.Subtypes.Elf] = [GetData(GroupConstants.All, "enchantment spells or effects")] = 2;

                testCases[CreatureConstants.Types.Subtypes.Evil] = [None];

                testCases[CreatureConstants.Types.Subtypes.Extraplanar] = [None];

                testCases[CreatureConstants.Types.Subtypes.Fire] = [None];

                testCases[CreatureConstants.Types.Subtypes.Gnome] = [None];

                testCases[CreatureConstants.Types.Subtypes.Goblinoid] = [None];

                testCases[CreatureConstants.Types.Subtypes.Good] = [None];

                testCases[CreatureConstants.Types.Subtypes.Halfling] = [GetData(GroupConstants.All)] = 1;
                testCases[CreatureConstants.Types.Subtypes.Halfling] = [GetData(GroupConstants.All, "morale against fear")] = 2;

                testCases[CreatureConstants.Types.Subtypes.Incorporeal] = [None];

                testCases[CreatureConstants.Types.Subtypes.Lawful] = [None];

                testCases[CreatureConstants.Types.Subtypes.Native] = [None];

                testCases[CreatureConstants.Types.Subtypes.Reptilian] = [None];

                testCases[CreatureConstants.Types.Subtypes.Shapechanger] = [None];

                testCases[CreatureConstants.Types.Subtypes.Swarm] = [None];

                testCases[CreatureConstants.Types.Subtypes.Water] = [None];

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        public static IEnumerable Templates
        {
            get
            {
                var testCases = new Dictionary<string, List<string>>();
                var templates = CreatureConstants.Templates.GetAll(); //INFO: This is duplicated from the creature entry

                foreach (var template in templates)
                {
                    testCases[template] = new List<string>();
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

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }
    }
}
