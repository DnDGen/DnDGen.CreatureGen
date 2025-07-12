using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.Infrastructure.Helpers;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Skills
{
    public class SkillBonusesTestData
    {
        public static Dictionary<string, List<string>> GetCreatureSkillBonuses()
        {
            var testCases = new Dictionary<string, List<string>>
            {
                [CreatureConstants.Aasimar] = [GetData(SkillConstants.Listen, 2), GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Aboleth] = [],

                [CreatureConstants.Achaierai] = [],

                [CreatureConstants.Allip] = [],

                [CreatureConstants.Androsphinx] = [],

                [CreatureConstants.Angel_AstralDeva] = [],

                [CreatureConstants.Angel_Planetar] = [],

                [CreatureConstants.Angel_Solar] = [],

                [CreatureConstants.AnimatedObject_Colossal] = [],
                [CreatureConstants.AnimatedObject_Colossal_Flexible] = [],
                [CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long] = [],
                [CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long_Wooden] = [],
                [CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall] = [],
                [CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall_Wooden] = [],
                [CreatureConstants.AnimatedObject_Colossal_Sheetlike] = [],
                [CreatureConstants.AnimatedObject_Colossal_TwoLegs] = [],
                [CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden] = [],
                [CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden] = [],
                [CreatureConstants.AnimatedObject_Colossal_Wooden] = [],
                [CreatureConstants.AnimatedObject_Gargantuan] = [],
                [CreatureConstants.AnimatedObject_Gargantuan_Flexible] = [],
                [CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Long] = [],
                [CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Long_Wooden] = [],
                [CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Tall] = [],
                [CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Tall_Wooden] = [],
                [CreatureConstants.AnimatedObject_Gargantuan_Sheetlike] = [],
                [CreatureConstants.AnimatedObject_Gargantuan_TwoLegs] = [],
                [CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden] = [],
                [CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden] = [],
                [CreatureConstants.AnimatedObject_Gargantuan_Wooden] = [],
                [CreatureConstants.AnimatedObject_Huge] = [],
                [CreatureConstants.AnimatedObject_Huge_Flexible] = [],
                [CreatureConstants.AnimatedObject_Huge_MultipleLegs_Long] = [],
                [CreatureConstants.AnimatedObject_Huge_MultipleLegs_Long_Wooden] = [],
                [CreatureConstants.AnimatedObject_Huge_MultipleLegs_Tall] = [],
                [CreatureConstants.AnimatedObject_Huge_MultipleLegs_Tall_Wooden] = [],
                [CreatureConstants.AnimatedObject_Huge_Sheetlike] = [],
                [CreatureConstants.AnimatedObject_Huge_TwoLegs] = [],
                [CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden] = [],
                [CreatureConstants.AnimatedObject_Huge_Wheels_Wooden] = [],
                [CreatureConstants.AnimatedObject_Huge_Wooden] = [],
                [CreatureConstants.AnimatedObject_Large] = [],
                [CreatureConstants.AnimatedObject_Large_Flexible] = [],
                [CreatureConstants.AnimatedObject_Large_MultipleLegs_Long] = [],
                [CreatureConstants.AnimatedObject_Large_MultipleLegs_Long_Wooden] = [],
                [CreatureConstants.AnimatedObject_Large_MultipleLegs_Tall] = [],
                [CreatureConstants.AnimatedObject_Large_MultipleLegs_Tall_Wooden] = [],
                [CreatureConstants.AnimatedObject_Large_Sheetlike] = [],
                [CreatureConstants.AnimatedObject_Large_TwoLegs] = [],
                [CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden] = [],
                [CreatureConstants.AnimatedObject_Large_Wheels_Wooden] = [],
                [CreatureConstants.AnimatedObject_Large_Wooden] = [],
                [CreatureConstants.AnimatedObject_Medium] = [],
                [CreatureConstants.AnimatedObject_Medium_Flexible] = [],
                [CreatureConstants.AnimatedObject_Medium_MultipleLegs] = [],
                [CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden] = [],
                [CreatureConstants.AnimatedObject_Medium_Sheetlike] = [],
                [CreatureConstants.AnimatedObject_Medium_TwoLegs] = [],
                [CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden] = [],
                [CreatureConstants.AnimatedObject_Medium_Wheels_Wooden] = [],
                [CreatureConstants.AnimatedObject_Medium_Wooden] = [],
                [CreatureConstants.AnimatedObject_Small] = [],
                [CreatureConstants.AnimatedObject_Small_Flexible] = [],
                [CreatureConstants.AnimatedObject_Small_MultipleLegs] = [],
                [CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden] = [],
                [CreatureConstants.AnimatedObject_Small_Sheetlike] = [],
                [CreatureConstants.AnimatedObject_Small_TwoLegs] = [],
                [CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden] = [],
                [CreatureConstants.AnimatedObject_Small_Wheels_Wooden] = [],
                [CreatureConstants.AnimatedObject_Small_Wooden] = [],
                [CreatureConstants.AnimatedObject_Tiny] = [],
                [CreatureConstants.AnimatedObject_Tiny_Flexible] = [],
                [CreatureConstants.AnimatedObject_Tiny_MultipleLegs] = [],
                [CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden] = [],
                [CreatureConstants.AnimatedObject_Tiny_Sheetlike] = [],
                [CreatureConstants.AnimatedObject_Tiny_TwoLegs] = [],
                [CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden] = [],
                [CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden] = [],
                [CreatureConstants.AnimatedObject_Tiny_Wooden] = [],

                [CreatureConstants.Ankheg] = [],

                [CreatureConstants.Annis] = [],

                [CreatureConstants.Ant_Giant_Queen] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Survival, 4, condition: "tracking by scent")],

                [CreatureConstants.Ant_Giant_Soldier] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Survival, 4, condition: "tracking by scent")],

                [CreatureConstants.Ant_Giant_Worker] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Survival, 4, condition: "tracking by scent")],

                [CreatureConstants.Ape] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Ape_Dire] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Aranea] =
                [GetData(SkillConstants.Jump, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Arrowhawk_Adult] = [],

                [CreatureConstants.Arrowhawk_Elder] = [],

                [CreatureConstants.Arrowhawk_Juvenile] = [],

                [CreatureConstants.AssassinVine] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Athach] = [],

                [CreatureConstants.Avoral] = [GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Azer] = [],

                [CreatureConstants.Babau] =
                [GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.MoveSilently, 8),
                    GetData(SkillConstants.Search, 8)],

                [CreatureConstants.Baboon] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Badger] = [GetData(SkillConstants.EscapeArtist, 4)],

                [CreatureConstants.Badger_Dire] = [],

                [CreatureConstants.Balor] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.BarbedDevil_Hamatula] = [],

                [CreatureConstants.Barghest] = [GetData(SkillConstants.Hide, 4, condition: "in wolf form")],

                [CreatureConstants.Barghest_Greater] = [GetData(SkillConstants.Hide, 4, condition: "in wolf form")],

                [CreatureConstants.Basilisk] = [GetData(SkillConstants.Hide, 4, condition: "in natural settings")],

                [CreatureConstants.Basilisk_Greater] = [GetData(SkillConstants.Hide, 4, condition: "in natural settings")],

                [CreatureConstants.Bat] =
                [GetData(SkillConstants.Listen, 4, condition: "while able to use blindsense"),
                    GetData(SkillConstants.Spot, 4, condition: "while able to use blindsense")],

                [CreatureConstants.Bat_Dire] =
                [GetData(SkillConstants.Listen, 4, condition: "while able to use blindsense"),
                    GetData(SkillConstants.Spot, 4, condition: "while able to use blindsense")],

                [CreatureConstants.Bat_Swarm] =
                [GetData(SkillConstants.Listen, 4, condition: "while able to use blindsense"),
                    GetData(SkillConstants.Spot, 4, condition: "while able to use blindsense")],

                [CreatureConstants.Bear_Black] = [GetData(SkillConstants.Swim, 4)],

                [CreatureConstants.Bear_Brown] = [GetData(SkillConstants.Swim, 4)],

                [CreatureConstants.Bear_Dire] = [],

                [CreatureConstants.Bear_Polar] =
                [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 12, condition: "in snowy settings")],

                [CreatureConstants.BeardedDevil_Barbazu] = [],

                [CreatureConstants.Bebilith] = [GetData(SkillConstants.Hide, 8)],

                [CreatureConstants.Bee_Giant] = [GetData(SkillConstants.Spot, 4), GetData(SkillConstants.Survival, 4, condition: "To orient itself")],

                [CreatureConstants.Behir] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Beholder] = [],

                [CreatureConstants.Beholder_Gauth] = [],

                [CreatureConstants.Belker] = [GetData(SkillConstants.MoveSilently, 4)],

                [CreatureConstants.Bison] = [],

                [CreatureConstants.BlackPudding] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.BlackPudding_Elder] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.BlinkDog] = [],

                [CreatureConstants.Boar] = [],

                [CreatureConstants.Boar_Dire] = [],

                [CreatureConstants.Bodak] = [],

                [CreatureConstants.BombardierBeetle_Giant] = [],

                [CreatureConstants.BoneDevil_Osyluth] = [],

                [CreatureConstants.Bralani] = [],

                [CreatureConstants.Bugbear] = [GetData(SkillConstants.MoveSilently, 4)],

                [CreatureConstants.Bulette] = [],

                [CreatureConstants.Camel_Bactrian] = [],

                [CreatureConstants.Camel_Dromedary] = [],

                [CreatureConstants.CarrionCrawler] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Cat] =
                [GetData(SkillConstants.Climb, 4),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Jump, 8),
                    GetData(SkillConstants.Balance, 8),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of tall grass or heavy undergrowth")],

                [CreatureConstants.Centaur] = [],

                [CreatureConstants.Centipede_Monstrous_Colossal] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Centipede_Monstrous_Gargantuan] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Centipede_Monstrous_Huge] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Centipede_Monstrous_Large] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Centipede_Monstrous_Medium] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Centipede_Monstrous_Small] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Centipede_Monstrous_Tiny] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Centipede_Swarm] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.ChainDevil_Kyton] = [GetData(SkillConstants.Craft, 8, condition: "involving metalwork")],

                [CreatureConstants.ChaosBeast] = [],

                [CreatureConstants.Cheetah] = [],

                [CreatureConstants.Chimera_Black] =
                [GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of scrubland or brush")],

                [CreatureConstants.Chimera_Blue] =
                [GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of scrubland or brush")],

                [CreatureConstants.Chimera_Green] =
                [GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of scrubland or brush")],

                [CreatureConstants.Chimera_Red] =
                [GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of scrubland or brush")],

                [CreatureConstants.Chimera_White] =
                [GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of scrubland or brush")],

                [CreatureConstants.Choker] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Chuul] = [],

                [CreatureConstants.Cloaker] = [],

                [CreatureConstants.Cockatrice] = [],

                [CreatureConstants.Couatl] = [],

                [CreatureConstants.Criosphinx] = [],

                [CreatureConstants.Crocodile] =
                [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4, condition: "in water"),
                    GetData(SkillConstants.Hide, 10, condition: "laying in water with only its eyes and nostrils showing")],

                [CreatureConstants.Crocodile_Giant] =
                [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4, condition: "in water"),
                    GetData(SkillConstants.Hide, 10, condition: "laying in water with only its eyes and nostrils showing")],

                [CreatureConstants.Cryohydra_5Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Cryohydra_6Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Cryohydra_7Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Cryohydra_8Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Cryohydra_9Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Cryohydra_10Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Cryohydra_11Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Cryohydra_12Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Darkmantle] =
                [GetData(SkillConstants.Spot, 4, condition: "while able to use blindsight"),
                    GetData(SkillConstants.Listen, 4, condition: "while able to use blindsight"),
                    GetData(SkillConstants.Hide, 4)],

                [CreatureConstants.Deinonychus] =
                [GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Jump, 8),
                    GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.Spot, 8),
                    GetData(SkillConstants.Survival, 8)],

                [CreatureConstants.Delver] = [],

                [CreatureConstants.Derro] = [],

                [CreatureConstants.Derro_Sane] = [],

                [CreatureConstants.Destrachan] = [GetData(SkillConstants.Listen, 10)],

                [CreatureConstants.Devourer] = [],

                [CreatureConstants.Digester] = [GetData(SkillConstants.Hide, 4), GetData(SkillConstants.Jump, 4)],

                [CreatureConstants.DisplacerBeast] = [GetData(SkillConstants.Hide, 8)],

                [CreatureConstants.DisplacerBeast_PackLord] = [GetData(SkillConstants.Hide, 8)],

                [CreatureConstants.Djinni] = [],

                [CreatureConstants.Djinni_Noble] = [],

                [CreatureConstants.Dog] = [GetData(SkillConstants.Jump, 4), GetData(SkillConstants.Survival, 4, condition: "tracking by scent")],

                [CreatureConstants.Dog_Riding] = [GetData(SkillConstants.Jump, 4), GetData(SkillConstants.Survival, 4, condition: "tracking by scent")],

                [CreatureConstants.Donkey] = [GetData(SkillConstants.Balance, 2)],

                [CreatureConstants.Doppelganger] =
                [GetData(SkillConstants.Bluff, 4),
                    GetData(SkillConstants.Disguise, 4),
                    GetData(SkillConstants.Bluff, 4, condition: "when reading an opponent's mind"),
                    GetData(SkillConstants.Disguise, 4, condition: "when reading an opponent's mind"),
                    GetData(SkillConstants.Disguise, 10, condition: "when using Change Shape")],

                [CreatureConstants.Dragon_Black_Wyrmling] = [],

                [CreatureConstants.Dragon_Black_VeryYoung] = [],

                [CreatureConstants.Dragon_Black_Young] = [],

                [CreatureConstants.Dragon_Black_Juvenile] = [],

                [CreatureConstants.Dragon_Black_YoungAdult] = [],

                [CreatureConstants.Dragon_Black_Adult] = [],

                [CreatureConstants.Dragon_Black_MatureAdult] = [],

                [CreatureConstants.Dragon_Black_Old] = [],

                [CreatureConstants.Dragon_Black_VeryOld] = [],

                [CreatureConstants.Dragon_Black_Ancient] = [],

                [CreatureConstants.Dragon_Black_Wyrm] = [],

                [CreatureConstants.Dragon_Black_GreatWyrm] = [],

                [CreatureConstants.Dragon_Blue_Wyrmling] = [],

                [CreatureConstants.Dragon_Blue_VeryYoung] = [],

                [CreatureConstants.Dragon_Blue_Young] = [],

                [CreatureConstants.Dragon_Blue_Juvenile] = [],

                [CreatureConstants.Dragon_Blue_YoungAdult] = [],

                [CreatureConstants.Dragon_Blue_Adult] = [],

                [CreatureConstants.Dragon_Blue_MatureAdult] = [],

                [CreatureConstants.Dragon_Blue_Old] = [],

                [CreatureConstants.Dragon_Blue_VeryOld] = [],

                [CreatureConstants.Dragon_Blue_Ancient] = [],

                [CreatureConstants.Dragon_Blue_Wyrm] = [],

                [CreatureConstants.Dragon_Blue_GreatWyrm] = [],

                [CreatureConstants.Dragon_Green_Wyrmling] = [],

                [CreatureConstants.Dragon_Green_VeryYoung] = [],

                [CreatureConstants.Dragon_Green_Young] = [],

                [CreatureConstants.Dragon_Green_Juvenile] = [],

                [CreatureConstants.Dragon_Green_YoungAdult] = [],

                [CreatureConstants.Dragon_Green_Adult] = [],

                [CreatureConstants.Dragon_Green_MatureAdult] = [],

                [CreatureConstants.Dragon_Green_Old] = [],

                [CreatureConstants.Dragon_Green_VeryOld] = [],

                [CreatureConstants.Dragon_Green_Ancient] = [],

                [CreatureConstants.Dragon_Green_Wyrm] = [],

                [CreatureConstants.Dragon_Green_GreatWyrm] = [],

                [CreatureConstants.Dragon_Red_Wyrmling] = [],

                [CreatureConstants.Dragon_Red_VeryYoung] = [],

                [CreatureConstants.Dragon_Red_Young] = [],

                [CreatureConstants.Dragon_Red_Juvenile] = [],

                [CreatureConstants.Dragon_Red_YoungAdult] = [],

                [CreatureConstants.Dragon_Red_Adult] = [],

                [CreatureConstants.Dragon_Red_MatureAdult] = [],

                [CreatureConstants.Dragon_Red_Old] = [],

                [CreatureConstants.Dragon_Red_VeryOld] = [],

                [CreatureConstants.Dragon_Red_Ancient] = [],

                [CreatureConstants.Dragon_Red_Wyrm] = [],

                [CreatureConstants.Dragon_Red_GreatWyrm] = [],

                [CreatureConstants.Dragon_White_Wyrmling] = [],

                [CreatureConstants.Dragon_White_VeryYoung] = [],

                [CreatureConstants.Dragon_White_Young] = [],

                [CreatureConstants.Dragon_White_Juvenile] = [],

                [CreatureConstants.Dragon_White_YoungAdult] = [],

                [CreatureConstants.Dragon_White_Adult] = [],

                [CreatureConstants.Dragon_White_MatureAdult] = [],

                [CreatureConstants.Dragon_White_Old] = [],

                [CreatureConstants.Dragon_White_VeryOld] = [],

                [CreatureConstants.Dragon_White_Ancient] = [],

                [CreatureConstants.Dragon_White_Wyrm] = [],

                [CreatureConstants.Dragon_White_GreatWyrm] = [],

                [CreatureConstants.Dragon_Brass_Wyrmling] = [],

                [CreatureConstants.Dragon_Brass_VeryYoung] = [],

                [CreatureConstants.Dragon_Brass_Young] = [],

                [CreatureConstants.Dragon_Brass_Juvenile] = [],

                [CreatureConstants.Dragon_Brass_YoungAdult] = [],

                [CreatureConstants.Dragon_Brass_Adult] = [],

                [CreatureConstants.Dragon_Brass_MatureAdult] = [],

                [CreatureConstants.Dragon_Brass_Old] = [],

                [CreatureConstants.Dragon_Brass_VeryOld] = [],

                [CreatureConstants.Dragon_Brass_Ancient] = [],

                [CreatureConstants.Dragon_Brass_Wyrm] = [],

                [CreatureConstants.Dragon_Brass_GreatWyrm] = [],

                [CreatureConstants.Dragon_Bronze_Wyrmling] = [],

                [CreatureConstants.Dragon_Bronze_VeryYoung] = [],

                [CreatureConstants.Dragon_Bronze_Young] = [],

                [CreatureConstants.Dragon_Bronze_Juvenile] = [],

                [CreatureConstants.Dragon_Bronze_YoungAdult] = [],

                [CreatureConstants.Dragon_Bronze_Adult] = [],

                [CreatureConstants.Dragon_Bronze_MatureAdult] = [],

                [CreatureConstants.Dragon_Bronze_Old] = [],

                [CreatureConstants.Dragon_Bronze_VeryOld] = [],

                [CreatureConstants.Dragon_Bronze_Ancient] = [],

                [CreatureConstants.Dragon_Bronze_Wyrm] = [],

                [CreatureConstants.Dragon_Bronze_GreatWyrm] = [],

                [CreatureConstants.Dragon_Copper_Wyrmling] = [],

                [CreatureConstants.Dragon_Copper_VeryYoung] = [],

                [CreatureConstants.Dragon_Copper_Young] = [],

                [CreatureConstants.Dragon_Copper_Juvenile] = [],

                [CreatureConstants.Dragon_Copper_YoungAdult] = [],

                [CreatureConstants.Dragon_Copper_Adult] = [],

                [CreatureConstants.Dragon_Copper_MatureAdult] = [],

                [CreatureConstants.Dragon_Copper_Old] = [],

                [CreatureConstants.Dragon_Copper_VeryOld] = [],

                [CreatureConstants.Dragon_Copper_Ancient] = [],

                [CreatureConstants.Dragon_Copper_Wyrm] = [],

                [CreatureConstants.Dragon_Copper_GreatWyrm] = [],

                [CreatureConstants.Dragon_Gold_Wyrmling] = [],

                [CreatureConstants.Dragon_Gold_VeryYoung] = [],

                [CreatureConstants.Dragon_Gold_Young] = [],

                [CreatureConstants.Dragon_Gold_Juvenile] = [],

                [CreatureConstants.Dragon_Gold_YoungAdult] = [],

                [CreatureConstants.Dragon_Gold_Adult] = [],

                [CreatureConstants.Dragon_Gold_MatureAdult] = [],

                [CreatureConstants.Dragon_Gold_Old] = [],

                [CreatureConstants.Dragon_Gold_VeryOld] = [],

                [CreatureConstants.Dragon_Gold_Ancient] = [],

                [CreatureConstants.Dragon_Gold_Wyrm] = [],

                [CreatureConstants.Dragon_Gold_GreatWyrm] = [],

                [CreatureConstants.Dragon_Silver_Wyrmling] = [],

                [CreatureConstants.Dragon_Silver_VeryYoung] = [],

                [CreatureConstants.Dragon_Silver_Young] = [],

                [CreatureConstants.Dragon_Silver_Juvenile] = [],

                [CreatureConstants.Dragon_Silver_YoungAdult] = [],

                [CreatureConstants.Dragon_Silver_Adult] = [],

                [CreatureConstants.Dragon_Silver_MatureAdult] = [],

                [CreatureConstants.Dragon_Silver_Old] = [],

                [CreatureConstants.Dragon_Silver_VeryOld] = [],

                [CreatureConstants.Dragon_Silver_Ancient] = [],

                [CreatureConstants.Dragon_Silver_Wyrm] = [],

                [CreatureConstants.Dragon_Silver_GreatWyrm] = [],

                [CreatureConstants.DragonTurtle] = [GetData(SkillConstants.Hide, 8, condition: "when submerged")],

                [CreatureConstants.Dragonne] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Dretch] = [],

                [CreatureConstants.Drider] =
                [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Dryad] = [GetData(SkillConstants.Diplomacy, 6, condition: "when using Wild Empathy")],

                [CreatureConstants.Dwarf_Deep] = [],

                [CreatureConstants.Dwarf_Duergar] =
                [GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Listen, 1),
                    GetData(SkillConstants.Spot, 1)],

                [CreatureConstants.Dwarf_Hill] = [],

                [CreatureConstants.Dwarf_Mountain] = [],

                [CreatureConstants.Eagle] = [GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Eagle_Giant] = [GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Efreeti] = [],

                [CreatureConstants.Elasmosaurus] = [GetData(SkillConstants.Hide, 8, condition: "in water")],

                [CreatureConstants.Elemental_Air_Small] = [],

                [CreatureConstants.Elemental_Air_Medium] = [],

                [CreatureConstants.Elemental_Air_Large] = [],

                [CreatureConstants.Elemental_Air_Huge] = [],

                [CreatureConstants.Elemental_Air_Greater] = [],

                [CreatureConstants.Elemental_Air_Elder] = [],

                [CreatureConstants.Elemental_Earth_Small] = [],

                [CreatureConstants.Elemental_Earth_Medium] = [],

                [CreatureConstants.Elemental_Earth_Large] = [],

                [CreatureConstants.Elemental_Earth_Huge] = [],

                [CreatureConstants.Elemental_Earth_Greater] = [],

                [CreatureConstants.Elemental_Earth_Elder] = [],

                [CreatureConstants.Elemental_Fire_Small] = [],

                [CreatureConstants.Elemental_Fire_Medium] = [],

                [CreatureConstants.Elemental_Fire_Large] = [],

                [CreatureConstants.Elemental_Fire_Huge] = [],

                [CreatureConstants.Elemental_Fire_Greater] = [],

                [CreatureConstants.Elemental_Fire_Elder] = [],

                [CreatureConstants.Elemental_Water_Small] = [],

                [CreatureConstants.Elemental_Water_Medium] = [],

                [CreatureConstants.Elemental_Water_Large] = [],

                [CreatureConstants.Elemental_Water_Huge] = [],

                [CreatureConstants.Elemental_Water_Greater] = [],

                [CreatureConstants.Elemental_Water_Elder] = [],

                [CreatureConstants.Elephant] = [],

                [CreatureConstants.Elf_Aquatic] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Search, 0,
                        condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for"),
                    GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Elf_Drow] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Search, 0,
                        condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for"),
                    GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Elf_Gray] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Search, 0,
                        condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for"),
                    GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Elf_Half] =
                [GetData(SkillConstants.Diplomacy, 2),
                    GetData(SkillConstants.GatherInformation, 2),
                    GetData(SkillConstants.Listen, 1),
                    GetData(SkillConstants.Search, 1),
                    GetData(SkillConstants.Spot, 1)],

                [CreatureConstants.Elf_High] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Search, 0,
                        condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for"),
                    GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Elf_Wild] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Search, 0,
                        condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for"),
                    GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Elf_Wood] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Search, 0,
                        condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for"),
                    GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Erinyes] = [],

                [CreatureConstants.EtherealFilcher] =
                [GetData(SkillConstants.Listen, 4),
                    GetData(SkillConstants.SleightOfHand, 8),
                    GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.EtherealMarauder] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.MoveSilently, 2),
                    GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Ettercap] =
                [GetData(SkillConstants.Craft, 4, focus: SkillConstants.Foci.Craft.Trapmaking),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Ettin] = [GetData(SkillConstants.Listen, 2), GetData(SkillConstants.Search, 2), GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.FireBeetle_Giant] = [],

                [CreatureConstants.FormianMyrmarch] = [],

                [CreatureConstants.FormianQueen] = [],

                [CreatureConstants.FormianTaskmaster] = [],

                [CreatureConstants.FormianWarrior] = [],

                [CreatureConstants.FormianWorker] = [],

                [CreatureConstants.FrostWorm] = [GetData(SkillConstants.Hide, 10, condition: "on Cold Plains")],

                [CreatureConstants.Gargoyle] =
                [GetData(SkillConstants.Hide, 2),
                    GetData(SkillConstants.Hide, 8, condition: "concealed against a background of stone"),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Gargoyle_Kapoacinth] =
                [GetData(SkillConstants.Hide, 2),
                    GetData(SkillConstants.Hide, 8, condition: "concealed against a background of stone"),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.GelatinousCube] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Ghaele] = [],

                [CreatureConstants.Ghoul] = [],

                [CreatureConstants.Ghoul_Ghast] = [],

                [CreatureConstants.Ghoul_Lacedon] = [],

                [CreatureConstants.Giant_Cloud] = [],

                [CreatureConstants.Giant_Fire] = [],

                [CreatureConstants.Giant_Frost] = [],

                [CreatureConstants.Giant_Hill] = [],

                [CreatureConstants.Giant_Stone] = [GetData(SkillConstants.Hide, 8, condition: "in rocky terrain")],

                [CreatureConstants.Giant_Stone_Elder] = [GetData(SkillConstants.Hide, 8, condition: "in rocky terrain")],

                [CreatureConstants.Giant_Storm] =
                [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.GibberingMouther] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Girallon] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Githyanki] = [],

                [CreatureConstants.Githzerai] = [],

                [CreatureConstants.Glabrezu] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Gnoll] = [],

                [CreatureConstants.Gnome_Forest] = [GetData(SkillConstants.Hide, 4), GetData(SkillConstants.Hide, 8, condition: "in a wooded area")],

                [CreatureConstants.Gnome_Rock] = [],

                [CreatureConstants.Gnome_Svirfneblin] = [GetData(SkillConstants.Hide, 2), GetData(SkillConstants.Hide, 2, condition: "underground")],

                [CreatureConstants.Goblin] = [GetData(SkillConstants.MoveSilently, 4), GetData(SkillConstants.Ride, 4)],

                [CreatureConstants.Golem_Clay] = [],

                [CreatureConstants.Golem_Flesh] = [],

                [CreatureConstants.Golem_Iron] = [],

                [CreatureConstants.Golem_Stone] = [],

                [CreatureConstants.Golem_Stone_Greater] = [],

                [CreatureConstants.Gorgon] = [],

                [CreatureConstants.GrayOoze] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.GrayRender] = [GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.GreenHag] = [],

                [CreatureConstants.Grick] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 8, condition: "in natural, rocky areas")],

                [CreatureConstants.Griffon] = [GetData(SkillConstants.Jump, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Grig] = [GetData(SkillConstants.Jump, 8), GetData(SkillConstants.MoveSilently, 5, condition: "in a forest setting")],

                [CreatureConstants.Grig_WithFiddle] = [GetData(SkillConstants.Jump, 8), GetData(SkillConstants.MoveSilently, 5, condition: "in a forest setting")],

                [CreatureConstants.Grimlock] = [GetData(SkillConstants.Hide, 10, condition: "in mountains or underground")],

                [CreatureConstants.Gynosphinx] = [],

                [CreatureConstants.Halfling_Deep] = [],

                [CreatureConstants.Halfling_Lightfoot] =
                [GetData(SkillConstants.Climb, 2),
                    GetData(SkillConstants.Jump, 2),
                    GetData(SkillConstants.MoveSilently, 2)],

                [CreatureConstants.Halfling_Tallfellow] = [],

                [CreatureConstants.Harpy] = [GetData(SkillConstants.Bluff, 4), GetData(SkillConstants.Listen, 4)],

                [CreatureConstants.Hawk] = [GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Hellcat_Bezekira] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.MoveSilently, 4)],

                [CreatureConstants.Hellwasp_Swarm] = [],

                [CreatureConstants.HellHound] = [GetData(SkillConstants.Hide, 5), GetData(SkillConstants.MoveSilently, 5)],

                [CreatureConstants.HellHound_NessianWarhound] = [GetData(SkillConstants.Hide, 5), GetData(SkillConstants.MoveSilently, 5)],

                [CreatureConstants.Hezrou] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Hieracosphinx] = [GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Hippogriff] = [GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Hobgoblin] = [GetData(SkillConstants.MoveSilently, 4)],

                [CreatureConstants.Horse_Heavy] = [],

                [CreatureConstants.Horse_Heavy_War] = [],

                [CreatureConstants.Horse_Light] = [],

                [CreatureConstants.Horse_Light_War] = [],

                [CreatureConstants.Howler] = [],

                [CreatureConstants.Hydra_5Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Hydra_6Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Hydra_7Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Hydra_8Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Hydra_9Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Hydra_10Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Hydra_11Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Hydra_12Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Homunculus] = [],

                [CreatureConstants.HornedDevil_Cornugon] = [],

                [CreatureConstants.HoundArchon] =
                [GetData(SkillConstants.Hide, 4, condition: "in canine form"),
                    GetData(SkillConstants.Survival, 4, condition: "in canine form")],

                [CreatureConstants.Human] = [],

                [CreatureConstants.Hyena] = [GetData(SkillConstants.Hide, 4, condition: "in tall grass or heavy undergrowth")],

                [CreatureConstants.IceDevil_Gelugon] = [],

                [CreatureConstants.Imp] = [],

                [CreatureConstants.InvisibleStalker] = [],

                [CreatureConstants.Janni] = [],

                [CreatureConstants.Kobold] =
                [GetData(SkillConstants.Craft, 2, focus: SkillConstants.Foci.Craft.Trapmaking),
                    GetData(SkillConstants.Profession, 2, focus: SkillConstants.Foci.Profession.Miner),
                    GetData(SkillConstants.Search, 2)],

                [CreatureConstants.Kolyarut] =
                [GetData(SkillConstants.Disguise, 4),
                    GetData(SkillConstants.GatherInformation, 4),
                    GetData(SkillConstants.SenseMotive, 4)],

                [CreatureConstants.Kraken] = [],

                [CreatureConstants.Krenshar] = [GetData(SkillConstants.Jump, 4), GetData(SkillConstants.MoveSilently, 4)],

                [CreatureConstants.KuoToa] = [GetData(SkillConstants.EscapeArtist, 8), GetData(SkillConstants.Spot, 4), GetData(SkillConstants.Search, 4)],

                [CreatureConstants.Lamia] = [GetData(SkillConstants.Bluff, 4), GetData(SkillConstants.Hide, 4)],

                [CreatureConstants.Lammasu] = [GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.LanternArchon] = [],

                [CreatureConstants.Lemure] = [],

                [CreatureConstants.Leonal] = [GetData(SkillConstants.Balance, 4), GetData(SkillConstants.Hide, 4), GetData(SkillConstants.MoveSilently, 4)],

                [CreatureConstants.Leopard] =
                [GetData(SkillConstants.Jump, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of tall grass or heavy undergrowth"),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Balance, 8),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Lillend] = [GetData(SkillConstants.Survival, 4)],

                [CreatureConstants.Lion] =
                [GetData(SkillConstants.Balance, 4),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Hide, 8, condition: "in tall grass or heavy undergrowth")],

                [CreatureConstants.Lion_Dire] =
                [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Hide, 4, condition: "in tall grass or heavy undergrowth")],

                [CreatureConstants.Lizard] =
                [GetData(SkillConstants.Balance, 8),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Lizard_Monitor] =
                [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Hide, 4, condition: "in forested or overgrown areas"),
                    GetData(SkillConstants.MoveSilently, 4)],

                [CreatureConstants.Lizardfolk] = [GetData(SkillConstants.Jump, 4), GetData(SkillConstants.Swim, 4), GetData(SkillConstants.Balance, 4)],

                [CreatureConstants.Locathah] = [],

                [CreatureConstants.Locust_Swarm] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Magmin] = [],

                [CreatureConstants.MantaRay] = [],

                [CreatureConstants.Manticore] = [GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Marilith] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Marut] = [GetData(SkillConstants.Concentration, 4), GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Medusa] = [],

                [CreatureConstants.Megaraptor] =
                [GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Jump, 8),
                    GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.Spot, 8),
                    GetData(SkillConstants.Survival, 8)],

                [CreatureConstants.Mephit_Air] = [],

                [CreatureConstants.Mephit_Dust] = [],

                [CreatureConstants.Mephit_Earth] = [],

                [CreatureConstants.Mephit_Fire] = [],

                [CreatureConstants.Mephit_Ice] = [],

                [CreatureConstants.Mephit_Magma] = [],

                [CreatureConstants.Mephit_Ooze] = [],

                [CreatureConstants.Mephit_Salt] = [],

                [CreatureConstants.Mephit_Steam] = [],

                [CreatureConstants.Mephit_Water] = [],

                [CreatureConstants.Merfolk] = [],

                [CreatureConstants.Mimic] = [GetData(SkillConstants.Disguise, 8)],

                [CreatureConstants.MindFlayer] = [],

                [CreatureConstants.Minotaur] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Search, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Mohrg] = [],

                [CreatureConstants.Monkey] =
                [GetData(SkillConstants.Balance, 8),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Mule] = [GetData(SkillConstants.Balance, 2, condition: "To avoid slipping or falling")],

                [CreatureConstants.Mummy] = [],

                [CreatureConstants.Naga_Dark] = [],

                [CreatureConstants.Naga_Guardian] = [],

                [CreatureConstants.Naga_Spirit] = [],

                [CreatureConstants.Naga_Water] = [],

                [CreatureConstants.Nalfeshnee] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.NightHag] = [],

                [CreatureConstants.Nightcrawler] = [],

                [CreatureConstants.Nightmare] = [],

                [CreatureConstants.Nightmare_Cauchemar] = [],

                [CreatureConstants.Nightwalker] = [GetData(SkillConstants.Hide, 8, condition: "in a dark area")],

                [CreatureConstants.Nightwing] = [GetData(SkillConstants.Hide, 8, condition: "in a dark area or flying in a dark sky")],

                [CreatureConstants.Nixie] = [GetData(SkillConstants.Hide, 5, condition: "in water")],

                [CreatureConstants.Nymph] =
                [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.OchreJelly] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Octopus] = [GetData(SkillConstants.Hide, 4), GetData(SkillConstants.EscapeArtist, 10)],

                [CreatureConstants.Octopus_Giant] = [GetData(SkillConstants.Hide, 4), GetData(SkillConstants.EscapeArtist, 10)],

                [CreatureConstants.Ogre] = [],

                [CreatureConstants.Ogre_Merrow] = [],

                [CreatureConstants.OgreMage] = [],

                [CreatureConstants.Orc] = [],

                [CreatureConstants.Orc_Half] = [],

                [CreatureConstants.Otyugh] = [GetData(SkillConstants.Hide, 8, condition: "in its lair")],

                [CreatureConstants.Owl] =
                [GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.MoveSilently, 14),
                    GetData(SkillConstants.Spot, 8, condition: "in areas of shadowy illumination")],

                [CreatureConstants.Owl_Giant] =
                [GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.MoveSilently, 8, condition: "in flight"),
                    GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Owlbear] = [],

                [CreatureConstants.Pegasus] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.PhantomFungus] = [GetData(SkillConstants.MoveSilently, 5)],

                [CreatureConstants.PhaseSpider] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Phasm] = [GetData(SkillConstants.Disguise, 10, condition: "when using Shapechange")],

                [CreatureConstants.PitFiend] = [],

                [CreatureConstants.Pixie] = [GetData(SkillConstants.Listen, 2), GetData(SkillConstants.Search, 2), GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Pixie_WithIrresistibleDance] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Pony] = [],

                [CreatureConstants.Pony_War] = [],

                [CreatureConstants.Porpoise] =
                [GetData(SkillConstants.Listen, 4, condition: "while able to use blindsight"),
                    GetData(SkillConstants.Spot, 4, condition: "while able to use blindsight")],

                [CreatureConstants.PrayingMantis_Giant] =
                [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Hide, 8, condition: "surrounded by foliage"),
                    GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Pseudodragon] = [GetData(SkillConstants.Hide, 4), GetData(SkillConstants.Hide, 4, condition: "in forests or overgrown areas")],

                [CreatureConstants.PurpleWorm] =
                [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Pyrohydra_5Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Pyrohydra_6Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Pyrohydra_7Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Pyrohydra_8Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Pyrohydra_9Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Pyrohydra_10Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Pyrohydra_11Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Pyrohydra_12Heads] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Quasit] = [],

                [CreatureConstants.Rakshasa] =
                [GetData(SkillConstants.Bluff, 4),
                    GetData(SkillConstants.Bluff, 10, condition: "when using Change Shape"),
                    GetData(SkillConstants.Bluff, 4, condition: "when reading an opponent's mind"),
                    GetData(SkillConstants.Disguise, 4),
                    GetData(SkillConstants.Disguise, 4, condition: "when reading an opponent's mind")],

                [CreatureConstants.Rast] = [],

                [CreatureConstants.Rat] =
                [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Balance, 8),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Swim, 8),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Rat_Dire] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Swim, 8),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Rat_Swarm] =
                [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Balance, 8),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Swim, 8),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Raven] = [],

                [CreatureConstants.Ravid] = [],

                [CreatureConstants.RazorBoar] = [],

                [CreatureConstants.Remorhaz] = [GetData(SkillConstants.Listen, 4)],

                [CreatureConstants.Retriever] = [],

                [CreatureConstants.Rhinoceras] = [],

                [CreatureConstants.Roc] = [GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Roper] = [GetData(SkillConstants.Hide, 8, condition: "in stony or icy areas")],

                [CreatureConstants.RustMonster] = [],

                [CreatureConstants.Sahuagin] =
                [GetData(SkillConstants.Hide, 4, condition: "underwater"),
                    GetData(SkillConstants.Listen, 4, condition: "underwater"),
                    GetData(SkillConstants.Spot, 4, condition: "underwater"),
                    GetData(SkillConstants.Survival, 4, condition: "within 50 miles of its home"),
                    GetData(SkillConstants.Profession, 4, focus: SkillConstants.Foci.Profession.Hunter, condition: "within 50 miles of its home"),
                    GetData(SkillConstants.HandleAnimal, 4, condition: "when working with sharks")],

                [CreatureConstants.Sahuagin_Malenti] =
                [GetData(SkillConstants.Hide, 4, condition: "underwater"),
                    GetData(SkillConstants.Listen, 4, condition: "underwater"),
                    GetData(SkillConstants.Spot, 4, condition: "underwater"),
                    GetData(SkillConstants.Survival, 4, condition: "within 50 miles of its home"),
                    GetData(SkillConstants.Profession, 4, focus: SkillConstants.Foci.Profession.Hunter, condition: "within 50 miles of its home"),
                    GetData(SkillConstants.HandleAnimal, 4, condition: "when working with sharks")],

                [CreatureConstants.Sahuagin_Mutant] =
                [GetData(SkillConstants.Hide, 4, condition: "underwater"),
                    GetData(SkillConstants.Listen, 4, condition: "underwater"),
                    GetData(SkillConstants.Spot, 4, condition: "underwater"),
                    GetData(SkillConstants.Survival, 4, condition: "within 50 miles of its home"),
                    GetData(SkillConstants.Profession, 4, focus: SkillConstants.Foci.Profession.Hunter, condition: "within 50 miles of its home"),
                    GetData(SkillConstants.HandleAnimal, 4, condition: "when working with sharks")],

                [CreatureConstants.Salamander_Flamebrother] = [GetData(SkillConstants.Craft, 4, focus: SkillConstants.Foci.Craft.Blacksmithing)],

                [CreatureConstants.Salamander_Average] = [GetData(SkillConstants.Craft, 4, focus: SkillConstants.Foci.Craft.Blacksmithing)],

                [CreatureConstants.Salamander_Noble] = [GetData(SkillConstants.Craft, 4, focus: SkillConstants.Foci.Craft.Blacksmithing)],

                [CreatureConstants.Satyr] =
                [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Listen, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Perform, 4),
                    GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Satyr_WithPipes] =
                [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Listen, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Perform, 4),
                    GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Scorpion_Monstrous_Colossal] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 4),
                    GetData(SkillConstants.Hide, 4)],

                [CreatureConstants.Scorpion_Monstrous_Gargantuan] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 4),
                    GetData(SkillConstants.Hide, 4)],

                [CreatureConstants.Scorpion_Monstrous_Huge] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 4),
                    GetData(SkillConstants.Hide, 4)],

                [CreatureConstants.Scorpion_Monstrous_Large] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 4),
                    GetData(SkillConstants.Hide, 4)],

                [CreatureConstants.Scorpion_Monstrous_Medium] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 4),
                    GetData(SkillConstants.Hide, 4)],

                [CreatureConstants.Scorpion_Monstrous_Small] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 4),
                    GetData(SkillConstants.Hide, 4)],

                [CreatureConstants.Scorpion_Monstrous_Tiny] =
                [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 4),
                    GetData(SkillConstants.Hide, 4)],

                [CreatureConstants.Scorpionfolk] = [],

                [CreatureConstants.SeaCat] = [],

                [CreatureConstants.SeaHag] = [],

                [CreatureConstants.Shadow] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Search, 4),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of shadowy illumination"),
                    GetData(SkillConstants.Hide, -4, condition: "in brightly lit areas")],

                [CreatureConstants.Shadow_Greater] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Search, 4),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of shadowy illumination"),
                    GetData(SkillConstants.Hide, -4, condition: "in brightly lit areas")],

                [CreatureConstants.ShadowMastiff] = [GetData(SkillConstants.Survival, 4, condition: "when tracking by scent")],

                [CreatureConstants.ShamblingMound] =
                [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Listen, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Hide, 8, condition: "in a swampy or forested area")],

                [CreatureConstants.Shark_Dire] = [],

                [CreatureConstants.Shark_Huge] = [],

                [CreatureConstants.Shark_Large] = [],

                [CreatureConstants.Shark_Medium] = [],

                [CreatureConstants.ShieldGuardian] = [],

                [CreatureConstants.ShockerLizard] =
                [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Shrieker] = [],

                [CreatureConstants.Skum] =
                [GetData(SkillConstants.Hide, 4, condition: "underwater"),
                    GetData(SkillConstants.Listen, 4, condition: "underwater"),
                    GetData(SkillConstants.Spot, 4, condition: "underwater")],

                [CreatureConstants.Slaad_Red] = [],

                [CreatureConstants.Slaad_Blue] = [],

                [CreatureConstants.Slaad_Green] = [],

                [CreatureConstants.Slaad_Gray] = [],

                [CreatureConstants.Slaad_Death] = [],

                [CreatureConstants.Snake_Constrictor] = [],

                [CreatureConstants.Snake_Constrictor_Giant] = [],

                [CreatureConstants.Snake_Viper_Tiny] = [],

                [CreatureConstants.Snake_Viper_Small] = [],

                [CreatureConstants.Snake_Viper_Medium] = [],

                [CreatureConstants.Snake_Viper_Large] = [],

                [CreatureConstants.Snake_Viper_Huge] = [],

                [CreatureConstants.Spectre] = [],

                [CreatureConstants.Spider_Monstrous_Hunter_Colossal] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_Hunter_Huge] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_Hunter_Large] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_Hunter_Medium] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_Hunter_Small] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_Hunter_Tiny] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Huge] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Large] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Medium] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Small] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Swarm] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.SpiderEater] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Squid] = [],

                [CreatureConstants.Squid_Giant] = [],

                [CreatureConstants.StagBeetle_Giant] = [],

                [CreatureConstants.Stirge] = [],

                [CreatureConstants.Succubus] =
                [GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.Spot, 8),
                    GetData(SkillConstants.Disguise, 10, condition: "when using Change Shape")],

                [CreatureConstants.Tarrasque] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Tendriculos] = [],

                [CreatureConstants.Thoqqua] = [],

                [CreatureConstants.Tiefling] = [GetData(SkillConstants.Bluff, 2), GetData(SkillConstants.Hide, 2)],

                [CreatureConstants.Tiger] =
                [GetData(SkillConstants.Balance, 4),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Hide, 4, condition: "in tall grass or heavy undergrowth")],

                [CreatureConstants.Tiger_Dire] =
                [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Hide, 4, condition: "in tall grass or heavy undergrowth")],

                [CreatureConstants.Titan] = [],

                [CreatureConstants.Toad] = [GetData(SkillConstants.Hide, 4)],

                [CreatureConstants.Tojanida_Juvenile] = [],

                [CreatureConstants.Tojanida_Adult] = [],

                [CreatureConstants.Tojanida_Elder] = [],

                [CreatureConstants.Treant] = [],

                [CreatureConstants.Triceratops] = [],

                [CreatureConstants.Triton] = [],

                [CreatureConstants.Troglodyte] = [GetData(SkillConstants.Hide, 4), GetData(SkillConstants.Hide, 4, condition: "in rocky or underground settings")],

                [CreatureConstants.Troll] = [],

                [CreatureConstants.Troll_Scrag] = [],

                [CreatureConstants.TrumpetArchon] = [],

                [CreatureConstants.Tyrannosaurus] = [GetData(SkillConstants.Listen, 2), GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.UmberHulk] = [],

                [CreatureConstants.UmberHulk_TrulyHorrid] = [],

                [CreatureConstants.Unicorn] =
                [GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Survival, 3, condition: "within the boundaries of their forest")],

                [CreatureConstants.VampireSpawn] =
                [GetData(SkillConstants.Bluff, 4),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Listen, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Search, 4),
                    GetData(SkillConstants.SenseMotive, 4),
                    GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Vargouille] = [],

                [CreatureConstants.VioletFungus] = [],

                [CreatureConstants.Vrock] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Wasp_Giant] = [GetData(SkillConstants.Spot, 8), GetData(SkillConstants.Survival, 4, condition: "to orient itself")],

                [CreatureConstants.Weasel] =
                [GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Balance, 8),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Weasel_Dire] = [],

                [CreatureConstants.Whale_Baleen] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Whale_Cachalot] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Whale_Orca] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Wight] = [GetData(SkillConstants.MoveSilently, 8)],

                [CreatureConstants.WillOWisp] = [],

                [CreatureConstants.WinterWolf] =
                [GetData(SkillConstants.Listen, 1),
                    GetData(SkillConstants.MoveSilently, 1),
                    GetData(SkillConstants.Spot, 1),
                    GetData(SkillConstants.Hide, 2),
                    GetData(SkillConstants.Hide, 5, condition: "in areas of snow and ice"),
                    GetData(SkillConstants.Survival, 4, condition: "tracking by scent")],

                [CreatureConstants.Wolf] = [GetData(SkillConstants.Survival, 4, condition: "tracking by scent")],

                [CreatureConstants.Wolf_Dire] =
                [GetData(SkillConstants.Hide, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.MoveSilently, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Survival, 4, condition: "tracking by scent")],

                [CreatureConstants.Wolverine] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Wolverine_Dire] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Worg] =
                [GetData(SkillConstants.Listen, 1),
                    GetData(SkillConstants.MoveSilently, 1),
                    GetData(SkillConstants.Spot, 1),
                    GetData(SkillConstants.Hide, 2),
                    GetData(SkillConstants.Survival, 4, condition: "tracking by scent")],

                [CreatureConstants.Wraith] = [],

                [CreatureConstants.Wraith_Dread] = [],

                [CreatureConstants.Wyvern] = [GetData(SkillConstants.Spot, 3)],

                [CreatureConstants.Xill] = [],

                [CreatureConstants.Xorn_Minor] = [],

                [CreatureConstants.Xorn_Average] = [],

                [CreatureConstants.Xorn_Elder] = [],

                [CreatureConstants.YethHound] = [GetData(SkillConstants.Survival, 4, condition: "tracking by scent")],

                [CreatureConstants.Yrthak] = [GetData(SkillConstants.Listen, 4)],

                [CreatureConstants.YuanTi_Pureblood] = [GetData(SkillConstants.Disguise, 5, condition: "when impersonating a human")],

                [CreatureConstants.YuanTi_Halfblood_SnakeArms] = [GetData(SkillConstants.Hide, 10, condition: "when using Chameleon Power")],

                [CreatureConstants.YuanTi_Halfblood_SnakeHead] = [GetData(SkillConstants.Hide, 10, condition: "when using Chameleon Power")],

                [CreatureConstants.YuanTi_Halfblood_SnakeTail] = [GetData(SkillConstants.Hide, 10, condition: "when using Chameleon Power")],

                [CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = [GetData(SkillConstants.Hide, 10, condition: "when using Chameleon Power")],

                [CreatureConstants.YuanTi_Abomination] =
                [GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 10, condition: "when using Chameleon Power")],

                [CreatureConstants.Zelekhut] = [GetData(SkillConstants.Search, 4), GetData(SkillConstants.SenseMotive, 4)]
            };

            return testCases;
        }

        public static Dictionary<string, List<string>> GetSkillBonusesData()
        {
            return GetCreatureSkillBonuses()
                .Union(GetSkillSynergiesSkillBonuses())
                .Union(GetTypeSkillBonuses())
                .Union(GetSubtypeSkillBonuses())
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static Dictionary<string, List<string>> GetTypeSkillBonuses()
        {
            var testCases = new Dictionary<string, List<string>>
            {
                [CreatureConstants.Types.Aberration] = [],

                [CreatureConstants.Types.Animal] = [],

                [CreatureConstants.Types.Construct] = [],

                [CreatureConstants.Types.Dragon] = [],

                [CreatureConstants.Types.Elemental] = [],

                [CreatureConstants.Types.Fey] = [],

                [CreatureConstants.Types.Giant] = [],

                [CreatureConstants.Types.Humanoid] = [],

                [CreatureConstants.Types.MagicalBeast] = [],

                [CreatureConstants.Types.MonstrousHumanoid] = [],

                [CreatureConstants.Types.Ooze] = [],

                [CreatureConstants.Types.Outsider] = [],

                [CreatureConstants.Types.Plant] = [],

                [CreatureConstants.Types.Undead] = [],

                [CreatureConstants.Types.Vermin] = []
            };

            return testCases;
        }

        public static Dictionary<string, List<string>> GetSubtypeSkillBonuses()
        {
            var testCases = new Dictionary<string, List<string>>
            {
                [CreatureConstants.Types.Subtypes.Air] = [],

                [CreatureConstants.Types.Subtypes.Angel] = [],

                [CreatureConstants.Types.Subtypes.Aquatic] =
                    [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Types.Subtypes.Archon] = [],

                [CreatureConstants.Types.Subtypes.Augmented] = [],

                [CreatureConstants.Types.Subtypes.Chaotic] = [],

                [CreatureConstants.Types.Subtypes.Cold] = [],

                [CreatureConstants.Types.Subtypes.Dwarf] =
                    [GetData(SkillConstants.Appraise, 2, condition: "for items related to stone or metal"),
                    GetData(SkillConstants.Craft, 2, condition: "for items related to stone or metal")],

                [CreatureConstants.Types.Subtypes.Earth] = [],

                [CreatureConstants.Types.Subtypes.Elf] = [],

                [CreatureConstants.Types.Subtypes.Evil] = [],

                [CreatureConstants.Types.Subtypes.Extraplanar] = [],

                [CreatureConstants.Types.Subtypes.Fire] = [],

                [CreatureConstants.Types.Subtypes.Gnome] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Craft, 2, focus: SkillConstants.Foci.Craft.Alchemy)],

                [CreatureConstants.Types.Subtypes.Goblinoid] = [],

                [CreatureConstants.Types.Subtypes.Good] = [],

                [CreatureConstants.Types.Subtypes.Halfling] = [GetData(SkillConstants.Listen, 2)],

                [CreatureConstants.Types.Subtypes.Incorporeal] =
                    [GetData(SkillConstants.MoveSilently, 9000, condition: "always succeeds, and cannot be heard by Listen checks unless it wants to be")],

                [CreatureConstants.Types.Subtypes.Lawful] = [],

                [CreatureConstants.Types.Subtypes.Native] = [],

                [CreatureConstants.Types.Subtypes.Reptilian] = [],

                [CreatureConstants.Types.Subtypes.Shapechanger] = [],

                [CreatureConstants.Types.Subtypes.Swarm] = [],

                [CreatureConstants.Types.Subtypes.Water] =
                    [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")]
            };

            return testCases;
        }

        public static IEnumerable<string> SkillSynergyNames =>
            [
                SkillConstants.Appraise,
                SkillConstants.Balance,
                SkillConstants.Bluff,
                SkillConstants.Craft,
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Cloth),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Glass),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Leather),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Stonemasonry),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking),
                SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking),
                SkillConstants.Climb,
                SkillConstants.Concentration,
                SkillConstants.DecipherScript,
                SkillConstants.Diplomacy,
                SkillConstants.DisableDevice,
                SkillConstants.Disguise,
                SkillConstants.EscapeArtist,
                SkillConstants.Forgery,
                SkillConstants.GatherInformation,
                SkillConstants.HandleAnimal,
                SkillConstants.Heal,
                SkillConstants.Hide,
                SkillConstants.Intimidate,
                SkillConstants.Jump,
                SkillConstants.Knowledge,
                SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana),
                SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering),
                SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering),
                SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography),
                SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History),
                SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local),
                SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature),
                SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty),
                SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Religion),
                SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes),
                SkillConstants.Listen,
                SkillConstants.MoveSilently,
                SkillConstants.OpenLock,
                SkillConstants.Perform,
                SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Act),
                SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Comedy),
                SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Dance),
                SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.KeyboardInstruments),
                SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Oratory),
                SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.PercussionInstruments),
                SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Sing),
                SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.StringInstruments),
                SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.WindInstruments),
                SkillConstants.Profession,
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Clerk),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coiffeur),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cook),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Laborer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Launderer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Nursemaid),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Polisher),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Porter),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Scribe),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright),
                SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide),
                SkillConstants.Ride,
                SkillConstants.Search,
                SkillConstants.SenseMotive,
                SkillConstants.SleightOfHand,
                SkillConstants.Spellcraft,
                SkillConstants.Spot,
                SkillConstants.Survival,
                SkillConstants.Swim,
                SkillConstants.Tumble,
                SkillConstants.UseMagicDevice,
                SkillConstants.UseRope
            ];

        public static IEnumerable SkillSynergies => SkillSynergyNames.Select(n => new TestCaseData(n));

        public static Dictionary<string, List<string>> GetSkillSynergiesSkillBonuses()
        {
            var testCases = new Dictionary<string, List<string>>
            {
                [SkillConstants.Appraise] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Appraiser),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Trader)],

                [SkillConstants.Balance] = [],

                [SkillConstants.Bluff] =
                [GetData(SkillConstants.Diplomacy, 2),
                GetData(SkillConstants.Disguise, 2, condition: "acting"),
                GetData(SkillConstants.Intimidate, 2),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Dowser),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Soothsayer),
                GetData(SkillConstants.SleightOfHand, 2)],

                [SkillConstants.Climb] = [],

                [SkillConstants.Concentration] = [],

                [SkillConstants.Craft] =
                [GetData(SkillConstants.Appraise, 2, condition: "related to items made with your Craft skill"),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Craftsman)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Alchemist),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Embalmer)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Armorer)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Blacksmith)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Bookbinder)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Bowyer),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Fletcher)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Brazier)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Brewer)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Chandler)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Cloth)] = [],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Coppersmith)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Dyer)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Gemcutter)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Glass)] = [],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Goldsmith)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Haberdasher)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Horner)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Jeweler)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Leather)] = [],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Locksmith)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Cartographer)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Miller)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Limner),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Painter)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Parchmentmaker)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Pewterer)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Potter)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Sculptor)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Shipwright)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Cobbler)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Silversmith)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Skinner)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Soapmaker)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Stonemasonry)] = [],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Tanner)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Hunter),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Trapper),
                GetData(SkillConstants.Search, 2, condition: "finding traps")],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Weaponsmith)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Weaver)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Wheelwright)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Vintner)],

                [SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Carpenter),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Cartwright),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Coffinmaker)],

                [SkillConstants.DecipherScript] = [GetData(SkillConstants.UseMagicDevice, 2, condition: "with scrolls")],

                [SkillConstants.Diplomacy] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Barrister),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Butler),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.CityGuide),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Footman),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Governess),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Matchmaker),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Valet)],

                [SkillConstants.DisableDevice] = [],

                [SkillConstants.Disguise] = [GetData(SkillConstants.Perform, 2, SkillConstants.Foci.Perform.Act, "in costume")],

                [SkillConstants.EscapeArtist] = [GetData(SkillConstants.UseRope, 2, condition: "binding someone")],

                [SkillConstants.Forgery] = [],

                [SkillConstants.GatherInformation] = [],

                [SkillConstants.HandleAnimal] =
                [GetData(SkillConstants.Diplomacy, 2, condition: "wild empathy"),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.AnimalGroomer),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.AnimalTrainer),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.ExoticAnimalTrainer),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Shepherd),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Teamster),
                GetData(SkillConstants.Ride, 2)],

                [SkillConstants.Heal] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Healer),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Masseuse),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Midwife)],

                [SkillConstants.Hide] = [],

                [SkillConstants.Intimidate] = [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.SailorMate)],

                [SkillConstants.Jump] = [GetData(SkillConstants.Tumble, 2)],

                [SkillConstants.Knowledge] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Adviser),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Sage),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Teacher)],

                [SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana)] = [GetData(SkillConstants.Spellcraft, 2)],

                [SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Architect),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Engineer),
                GetData(SkillConstants.Search, 2, condition: "find secret doors or compartments")],

                [SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Miner),
                GetData(SkillConstants.Survival, 2, condition: "underground")],

                [SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Miner),
                GetData(SkillConstants.Survival, 2, condition: "keep from getting lost or avoid natural hazards")],

                [SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History)] = [GetData(SkillConstants.Knowledge, 2, "bardic")],

                [SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] =
                [GetData(SkillConstants.GatherInformation, 2),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.CityGuide),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.LocalCourier),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.OutOfTownCourier),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.WildernessGuide)],

                [SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Apothecary),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Farmer),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Hunter),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Miner),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Trapper),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.WildernessGuide),
                GetData(SkillConstants.Survival, 2, condition: "in aboveground natural environments")],

                [SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] =
                [GetData(SkillConstants.Diplomacy, 2),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Butler),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Footman),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Governess),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Maid)],

                [SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Religion)] = [],

                [SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes)] =
                [GetData(SkillConstants.Survival, 2, condition: "on other planes")],

                [SkillConstants.Listen] = [],

                [SkillConstants.MoveSilently] = [],

                [SkillConstants.OpenLock] = [],

                [SkillConstants.Perform] = [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Entertainer)],

                [SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Act)] =
                [GetData(SkillConstants.Disguise, 2, condition: "impersonating someone else")],

                [SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Comedy)] = [],

                [SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Dance)] = [],

                [SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.KeyboardInstruments)] = [],

                [SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Oratory)] = [],

                [SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.PercussionInstruments)] = [],

                [SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Sing)] = [],

                [SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.StringInstruments)] = [],

                [SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.WindInstruments)] = [],

                [SkillConstants.Profession] = [],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)] =
                [GetData(SkillConstants.Diplomacy, 2),
                GetData(SkillConstants.Knowledge, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Alchemy)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer)] = [GetData(SkillConstants.HandleAnimal, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer)] = [GetData(SkillConstants.HandleAnimal, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary)] =
                [GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.Nature)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser)] = [GetData(SkillConstants.Appraise, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect)] =
                [GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Armorsmithing)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister)] = [GetData(SkillConstants.Diplomacy, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Blacksmithing)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Bookbinding)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Bowmaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Brassmaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Brewing)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)] =
                [GetData(SkillConstants.Diplomacy, 2),
                GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Woodworking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Mapmaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Woodworking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Candlemaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)] =
                [GetData(SkillConstants.Diplomacy, 2),
                GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.Local)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Clerk)] = [],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Shoemaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Woodworking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coiffeur)] = [],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cook)] = [],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Coppersmithing)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)] = [GetData(SkillConstants.Craft, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)] =
                [GetData(SkillConstants.Bluff, 2),
                GetData(SkillConstants.Survival, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Dyemaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Alchemy)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer)] =
                [GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)] = [GetData(SkillConstants.Perform, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer)] = [GetData(SkillConstants.HandleAnimal, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer)] =
                [GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.Nature)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Bowmaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)] =
                [GetData(SkillConstants.Diplomacy, 2),
                GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Gemcutting)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Goldsmithing)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)] =
                [GetData(SkillConstants.Diplomacy, 2),
                GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Hatmaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer)] = [GetData(SkillConstants.Heal, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Hornworking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Trapmaking),
                GetData(SkillConstants.Survival, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter)] = [],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Jewelmaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Laborer)] = [],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Launderer)] = [],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Painting)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier)] =
                [GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.Local)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Locksmithing)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid)] =
                [GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse)] = [GetData(SkillConstants.Heal, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)] =
                [GetData(SkillConstants.Diplomacy, 2),
                GetData(SkillConstants.SenseMotive, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife)] = [GetData(SkillConstants.Heal, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Milling)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] =
                [GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.Geography),
                GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.Nature),
                GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.Dungeoneering)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator)] = [GetData(SkillConstants.Survival, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Nursemaid)] = [],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)] =
                [GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.Local),
                GetData(SkillConstants.Ride, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Painting)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Parchmentmaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Pewtermaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Polisher)] = [],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Porter)] = [],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Potterymaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage)] = [GetData(SkillConstants.Knowledge, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember)] = [GetData(SkillConstants.Swim, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)] =
                [GetData(SkillConstants.Intimidate, 2),
                GetData(SkillConstants.Swim, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Scribe)] = [],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Sculpting)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd)] = [GetData(SkillConstants.HandleAnimal, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Shipmaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Silversmithing)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Skinning)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Soapmaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer)] = [GetData(SkillConstants.Bluff, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Tanning)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)] = [GetData(SkillConstants.Knowledge, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)] =
                [GetData(SkillConstants.HandleAnimal, 2),
                GetData(SkillConstants.Ride, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)] =
                [GetData(SkillConstants.Appraise, 2),
                GetData(SkillConstants.SenseMotive, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Trapmaking),
                GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.Nature),
                GetData(SkillConstants.Survival, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet)] = [GetData(SkillConstants.Diplomacy, 2)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Winemaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Weaponsmithing)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Weaving)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright)] =
                [GetData(SkillConstants.Craft, 2, SkillConstants.Foci.Craft.Wheelmaking)],

                [SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)] =
                [GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.Local),
                GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.Nature)],

                [SkillConstants.Ride] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.OutOfTownCourier),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Teamster)],

                [SkillConstants.Search] = [GetData(SkillConstants.Survival, 2, condition: "following tracks")],

                [SkillConstants.SenseMotive] =
                [GetData(SkillConstants.Diplomacy, 2),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Matchmaker),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Trader)],

                [SkillConstants.SleightOfHand] = [],

                [SkillConstants.Spellcraft] = [GetData(SkillConstants.UseMagicDevice, 2, condition: "with scrolls")],

                [SkillConstants.Spot] = [],

                [SkillConstants.Survival] =
                [GetData(SkillConstants.Knowledge, 2, SkillConstants.Foci.Knowledge.Nature),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Dowser),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Hunter),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Navigator),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Trapper)],

                [SkillConstants.Swim] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.SailorCrewmember),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.SailorMate)],

                [SkillConstants.Tumble] = [GetData(SkillConstants.Balance, 2), GetData(SkillConstants.Jump, 2)],

                [SkillConstants.UseMagicDevice] = [GetData(SkillConstants.Spellcraft, 2, condition: "decipher scrolls")],

                [SkillConstants.UseRope] =
                [GetData(SkillConstants.Climb, 2, condition: "with rope"),
                GetData(SkillConstants.EscapeArtist, 2, condition: "escaping rope bonds")]
            };

            return testCases;
        }

        private static string GetData(string skillName, int bonus, string focus = "", string condition = "")
        {
            return DataHelper.Parse(new BonusDataSelection
            {
                Target = SkillConstants.Build(skillName, focus),
                Bonus = bonus,
                Condition = condition
            });
        }
    }
}
