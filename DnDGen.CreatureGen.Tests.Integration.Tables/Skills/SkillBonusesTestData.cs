using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using DnDGen.Infrastructure.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Skills
{
    public class SkillBonusesTestData
    {
        public const string None = "NONE";

        public static Dictionary<string, List<string>> GetCreatureSkillBonuses()
        {
            var testCases = new Dictionary<string, List<string>>
            {
                [CreatureConstants.Aasimar] = [GetData(SkillConstants.Listen, 2), GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Aboleth] = [None],

                [CreatureConstants.Achaierai] = [None],

                [CreatureConstants.Allip] = [None],

                [CreatureConstants.Androsphinx] = [None],

                [CreatureConstants.Angel_AstralDeva] = [None],

                [CreatureConstants.Angel_Planetar] = [None],

                [CreatureConstants.Angel_Solar] = [None],

                [CreatureConstants.AnimatedObject_Colossal] = [None],
                [CreatureConstants.AnimatedObject_Colossal_Flexible] = [None],
                [CreatureConstants.AnimatedObject_Colossal_MultipleLegs] = [None],
                [CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Colossal_Sheetlike] = [None],
                [CreatureConstants.AnimatedObject_Colossal_TwoLegs] = [None],
                [CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Colossal_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_Flexible] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_Sheetlike] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_TwoLegs] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Gargantuan_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Huge] = [None],
                [CreatureConstants.AnimatedObject_Huge_Flexible] = [None],
                [CreatureConstants.AnimatedObject_Huge_MultipleLegs] = [None],
                [CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Huge_Sheetlike] = [None],
                [CreatureConstants.AnimatedObject_Huge_TwoLegs] = [None],
                [CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Huge_Wheels_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Huge_Wooden] = [None],
                [CreatureConstants.AnimatedObject_Large] = [None],
                [CreatureConstants.AnimatedObject_Large_Flexible] = [None],
                [CreatureConstants.AnimatedObject_Large_MultipleLegs] = [None],
                [CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden] = [None],
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

                [CreatureConstants.Arrowhawk_Adult] = [None],

                [CreatureConstants.Arrowhawk_Elder] = [None],

                [CreatureConstants.Arrowhawk_Juvenile] = [None],

                [CreatureConstants.AssassinVine] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Athach] = [None],

                [CreatureConstants.Avoral] = [GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Azer] = [None],

                [CreatureConstants.Babau] =
                [GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.MoveSilently, 8),
                    GetData(SkillConstants.Search, 8)],

                [CreatureConstants.Baboon] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Badger] = [GetData(SkillConstants.EscapeArtist, 4)],

                [CreatureConstants.Badger_Dire] = [None],

                [CreatureConstants.Balor] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.BarbedDevil_Hamatula] = [None],

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

                [CreatureConstants.Bear_Dire] = [None],

                [CreatureConstants.Bear_Polar] =
                [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 12, condition: "in snowy settings")],

                [CreatureConstants.BeardedDevil_Barbazu] = [None],

                [CreatureConstants.Bebilith] = [GetData(SkillConstants.Hide, 8)],

                [CreatureConstants.Bee_Giant] = [GetData(SkillConstants.Spot, 4), GetData(SkillConstants.Survival, 4, condition: "To orient itself")],

                [CreatureConstants.Behir] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Beholder] = [None],

                [CreatureConstants.Beholder_Gauth] = [None],

                [CreatureConstants.Belker] = [GetData(SkillConstants.MoveSilently, 4)],

                [CreatureConstants.Bison] = [None],

                [CreatureConstants.BlackPudding] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.BlackPudding_Elder] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.BlinkDog] = [None],

                [CreatureConstants.Boar] = [None],

                [CreatureConstants.Boar_Dire] = [None],

                [CreatureConstants.Bodak] = [None],

                [CreatureConstants.BombardierBeetle_Giant] = [None],

                [CreatureConstants.BoneDevil_Osyluth] = [None],

                [CreatureConstants.Bralani] = [None],

                [CreatureConstants.Bugbear] = [GetData(SkillConstants.MoveSilently, 4)],

                [CreatureConstants.Bulette] = [None],

                [CreatureConstants.Camel_Bactrian] = [None],

                [CreatureConstants.Camel_Dromedary] = [None],

                [CreatureConstants.CarrionCrawler] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Cat] =
                [GetData(SkillConstants.Climb, 4),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Jump, 8),
                    GetData(SkillConstants.Balance, 8),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of tall grass or heavy undergrowth")],

                [CreatureConstants.Centaur] = [None],

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

                [CreatureConstants.ChaosBeast] = [None],

                [CreatureConstants.Cheetah] = [None],

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

                [CreatureConstants.Chuul] = [None],

                [CreatureConstants.Cloaker] = [None],

                [CreatureConstants.Cockatrice] = [None],

                [CreatureConstants.Couatl] = [None],

                [CreatureConstants.Criosphinx] = [None],

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

                [CreatureConstants.Delver] = [None],

                [CreatureConstants.Derro] = [None],

                [CreatureConstants.Derro_Sane] = [None],

                [CreatureConstants.Destrachan] = [GetData(SkillConstants.Listen, 10)],

                [CreatureConstants.Devourer] = [None],

                [CreatureConstants.Digester] = [GetData(SkillConstants.Hide, 4), GetData(SkillConstants.Jump, 4)],

                [CreatureConstants.DisplacerBeast] = [GetData(SkillConstants.Hide, 8)],

                [CreatureConstants.DisplacerBeast_PackLord] = [GetData(SkillConstants.Hide, 8)],

                [CreatureConstants.Djinni] = [None],

                [CreatureConstants.Djinni_Noble] = [None],

                [CreatureConstants.Dog] = [GetData(SkillConstants.Jump, 4), GetData(SkillConstants.Survival, 4, condition: "tracking by scent")],

                [CreatureConstants.Dog_Riding] = [GetData(SkillConstants.Jump, 4), GetData(SkillConstants.Survival, 4, condition: "tracking by scent")],

                [CreatureConstants.Donkey] = [GetData(SkillConstants.Balance, 2)],

                [CreatureConstants.Doppelganger] =
                [GetData(SkillConstants.Bluff, 4),
                    GetData(SkillConstants.Disguise, 4),
                    GetData(SkillConstants.Bluff, 4, condition: "when reading an opponent's mind"),
                    GetData(SkillConstants.Disguise, 4, condition: "when reading an opponent's mind"),
                    GetData(SkillConstants.Disguise, 10, condition: "when using Change Shape")],

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

                [CreatureConstants.DragonTurtle] = [GetData(SkillConstants.Hide, 8, condition: "when submerged")],

                [CreatureConstants.Dragonne] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Dretch] = [None],

                [CreatureConstants.Drider] =
                [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Dryad] = [GetData(SkillConstants.Diplomacy, 6, condition: "when using Wild Empathy")],

                [CreatureConstants.Dwarf_Deep] = [None],

                [CreatureConstants.Dwarf_Duergar] =
                [GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Listen, 1),
                    GetData(SkillConstants.Spot, 1)],

                [CreatureConstants.Dwarf_Hill] = [None],

                [CreatureConstants.Dwarf_Mountain] = [None],

                [CreatureConstants.Eagle] = [GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Eagle_Giant] = [GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Efreeti] = [None],

                [CreatureConstants.Elasmosaurus] = [GetData(SkillConstants.Hide, 8, condition: "in water")],

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

                [CreatureConstants.Erinyes] = [None],

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

                [CreatureConstants.FireBeetle_Giant] = [None],

                [CreatureConstants.FormianMyrmarch] = [None],

                [CreatureConstants.FormianQueen] = [None],

                [CreatureConstants.FormianTaskmaster] = [None],

                [CreatureConstants.FormianWarrior] = [None],

                [CreatureConstants.FormianWorker] = [None],

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

                [CreatureConstants.Ghaele] = [None],

                [CreatureConstants.Ghoul] = [None],

                [CreatureConstants.Ghoul_Ghast] = [None],

                [CreatureConstants.Ghoul_Lacedon] = [None],

                [CreatureConstants.Giant_Cloud] = [None],

                [CreatureConstants.Giant_Fire] = [None],

                [CreatureConstants.Giant_Frost] = [None],

                [CreatureConstants.Giant_Hill] = [None],

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

                [CreatureConstants.Githyanki] = [None],

                [CreatureConstants.Githzerai] = [None],

                [CreatureConstants.Glabrezu] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Gnoll] = [None],

                [CreatureConstants.Gnome_Forest] = [GetData(SkillConstants.Hide, 4), GetData(SkillConstants.Hide, 8, condition: "in a wooded area")],

                [CreatureConstants.Gnome_Rock] = [None],

                [CreatureConstants.Gnome_Svirfneblin] = [GetData(SkillConstants.Hide, 2), GetData(SkillConstants.Hide, 2, condition: "underground")],

                [CreatureConstants.Goblin] = [GetData(SkillConstants.MoveSilently, 4), GetData(SkillConstants.Ride, 4)],

                [CreatureConstants.Golem_Clay] = [None],

                [CreatureConstants.Golem_Flesh] = [None],

                [CreatureConstants.Golem_Iron] = [None],

                [CreatureConstants.Golem_Stone] = [None],

                [CreatureConstants.Golem_Stone_Greater] = [None],

                [CreatureConstants.Gorgon] = [None],

                [CreatureConstants.GrayOoze] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.GrayRender] = [GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.GreenHag] = [None],

                [CreatureConstants.Grick] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 8, condition: "in natural, rocky areas")],

                [CreatureConstants.Griffon] = [GetData(SkillConstants.Jump, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Grig] = [GetData(SkillConstants.Jump, 8), GetData(SkillConstants.MoveSilently, 5, condition: "in a forest setting")],

                [CreatureConstants.Grig_WithFiddle] = [GetData(SkillConstants.Jump, 8), GetData(SkillConstants.MoveSilently, 5, condition: "in a forest setting")],

                [CreatureConstants.Grimlock] = [GetData(SkillConstants.Hide, 10, condition: "in mountains or underground")],

                [CreatureConstants.Gynosphinx] = [None],

                [CreatureConstants.Halfling_Deep] = [None],

                [CreatureConstants.Halfling_Lightfoot] =
                [GetData(SkillConstants.Climb, 2),
                    GetData(SkillConstants.Jump, 2),
                    GetData(SkillConstants.MoveSilently, 2)],

                [CreatureConstants.Halfling_Tallfellow] = [None],

                [CreatureConstants.Harpy] = [GetData(SkillConstants.Bluff, 4), GetData(SkillConstants.Listen, 4)],

                [CreatureConstants.Hawk] = [GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Hellcat_Bezekira] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.MoveSilently, 4)],

                [CreatureConstants.Hellwasp_Swarm] = [None],

                [CreatureConstants.HellHound] = [GetData(SkillConstants.Hide, 5), GetData(SkillConstants.MoveSilently, 5)],

                [CreatureConstants.HellHound_NessianWarhound] = [GetData(SkillConstants.Hide, 5), GetData(SkillConstants.MoveSilently, 5)],

                [CreatureConstants.Hezrou] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Hieracosphinx] = [GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Hippogriff] = [GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Hobgoblin] = [GetData(SkillConstants.MoveSilently, 4)],

                [CreatureConstants.Horse_Heavy] = [None],

                [CreatureConstants.Horse_Heavy_War] = [None],

                [CreatureConstants.Horse_Light] = [None],

                [CreatureConstants.Horse_Light_War] = [None],

                [CreatureConstants.Howler] = [None],

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

                [CreatureConstants.Homunculus] = [None],

                [CreatureConstants.HornedDevil_Cornugon] = [None],

                [CreatureConstants.HoundArchon] =
                [GetData(SkillConstants.Hide, 4, condition: "in canine form"),
                    GetData(SkillConstants.Survival, 4, condition: "in canine form")],

                [CreatureConstants.Human] = [None],

                [CreatureConstants.Hyena] = [GetData(SkillConstants.Hide, 4, condition: "in tall grass or heavy undergrowth")],

                [CreatureConstants.IceDevil_Gelugon] = [None],

                [CreatureConstants.Imp] = [None],

                [CreatureConstants.InvisibleStalker] = [None],

                [CreatureConstants.Janni] = [None],

                [CreatureConstants.Kobold] =
                [GetData(SkillConstants.Craft, 2, focus: SkillConstants.Foci.Craft.Trapmaking),
                    GetData(SkillConstants.Profession, 2, focus: SkillConstants.Foci.Profession.Miner),
                    GetData(SkillConstants.Search, 2)],

                [CreatureConstants.Kolyarut] =
                [GetData(SkillConstants.Disguise, 4),
                    GetData(SkillConstants.GatherInformation, 4),
                    GetData(SkillConstants.SenseMotive, 4)],

                [CreatureConstants.Kraken] = [None],

                [CreatureConstants.Krenshar] = [GetData(SkillConstants.Jump, 4), GetData(SkillConstants.MoveSilently, 4)],

                [CreatureConstants.KuoToa] = [GetData(SkillConstants.EscapeArtist, 8), GetData(SkillConstants.Spot, 4), GetData(SkillConstants.Search, 4)],

                [CreatureConstants.Lamia] = [GetData(SkillConstants.Bluff, 4), GetData(SkillConstants.Hide, 4)],

                [CreatureConstants.Lammasu] = [GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.LanternArchon] = [None],

                [CreatureConstants.Lemure] = [None],

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

                [CreatureConstants.Locathah] = [None],

                [CreatureConstants.Locust_Swarm] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Magmin] = [None],

                [CreatureConstants.MantaRay] = [None],

                [CreatureConstants.Manticore] = [GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Marilith] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Marut] = [GetData(SkillConstants.Concentration, 4), GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Medusa] = [None],

                [CreatureConstants.Megaraptor] =
                [GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Jump, 8),
                    GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.Spot, 8),
                    GetData(SkillConstants.Survival, 8)],

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

                [CreatureConstants.Mimic] = [GetData(SkillConstants.Disguise, 8)],

                [CreatureConstants.MindFlayer] = [None],

                [CreatureConstants.Minotaur] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Search, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Mohrg] = [None],

                [CreatureConstants.Monkey] =
                [GetData(SkillConstants.Balance, 8),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Mule] = [GetData(SkillConstants.Balance, 2, condition: "To avoid slipping or falling")],

                [CreatureConstants.Mummy] = [None],

                [CreatureConstants.Naga_Dark] = [None],

                [CreatureConstants.Naga_Guardian] = [None],

                [CreatureConstants.Naga_Spirit] = [None],

                [CreatureConstants.Naga_Water] = [None],

                [CreatureConstants.Nalfeshnee] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.NightHag] = [None],

                [CreatureConstants.Nightcrawler] = [None],

                [CreatureConstants.Nightmare] = [None],

                [CreatureConstants.Nightmare_Cauchemar] = [None],

                [CreatureConstants.Nightwalker] = [GetData(SkillConstants.Hide, 8, condition: "in a dark area")],

                [CreatureConstants.Nightwing] = [GetData(SkillConstants.Hide, 8, condition: "in a dark area or flying in a dark sky")],

                [CreatureConstants.Nixie] = [GetData(SkillConstants.Hide, 5, condition: "in water")],

                [CreatureConstants.Nymph] =
                [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.OchreJelly] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Octopus] = [GetData(SkillConstants.Hide, 4), GetData(SkillConstants.EscapeArtist, 10)],

                [CreatureConstants.Octopus_Giant] = [GetData(SkillConstants.Hide, 4), GetData(SkillConstants.EscapeArtist, 10)],

                [CreatureConstants.Ogre] = [None],

                [CreatureConstants.Ogre_Merrow] = [None],

                [CreatureConstants.OgreMage] = [None],

                [CreatureConstants.Orc] = [None],

                [CreatureConstants.Orc_Half] = [None],

                [CreatureConstants.Otyugh] = [GetData(SkillConstants.Hide, 8, condition: "in its lair")],

                [CreatureConstants.Owl] =
                [GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.MoveSilently, 14),
                    GetData(SkillConstants.Spot, 8, condition: "in areas of shadowy illumination")],

                [CreatureConstants.Owl_Giant] =
                [GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.MoveSilently, 8, condition: "in flight"),
                    GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Owlbear] = [None],

                [CreatureConstants.Pegasus] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.PhantomFungus] = [GetData(SkillConstants.MoveSilently, 5)],

                [CreatureConstants.PhaseSpider] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Phasm] = [GetData(SkillConstants.Disguise, 10, condition: "when using Shapechange")],

                [CreatureConstants.PitFiend] = [None],

                [CreatureConstants.Pixie] = [GetData(SkillConstants.Listen, 2), GetData(SkillConstants.Search, 2), GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Pixie_WithIrresistibleDance] =
                [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.Pony] = [None],

                [CreatureConstants.Pony_War] = [None],

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

                [CreatureConstants.Quasit] = [None],

                [CreatureConstants.Rakshasa] =
                [GetData(SkillConstants.Bluff, 4),
                    GetData(SkillConstants.Bluff, 10, condition: "when using Change Shape"),
                    GetData(SkillConstants.Bluff, 4, condition: "when reading an opponent's mind"),
                    GetData(SkillConstants.Disguise, 4),
                    GetData(SkillConstants.Disguise, 4, condition: "when reading an opponent's mind")],

                [CreatureConstants.Rast] = [None],

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

                [CreatureConstants.Raven] = [None],

                [CreatureConstants.Ravid] = [None],

                [CreatureConstants.RazorBoar] = [None],

                [CreatureConstants.Remorhaz] = [GetData(SkillConstants.Listen, 4)],

                [CreatureConstants.Retriever] = [None],

                [CreatureConstants.Rhinoceras] = [None],

                [CreatureConstants.Roc] = [GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Roper] = [GetData(SkillConstants.Hide, 8, condition: "in stony or icy areas")],

                [CreatureConstants.RustMonster] = [None],

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

                [CreatureConstants.Scorpionfolk] = [None],

                [CreatureConstants.SeaCat] = [None],

                [CreatureConstants.SeaHag] = [None],

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

                [CreatureConstants.Shark_Dire] = [None],

                [CreatureConstants.Shark_Huge] = [None],

                [CreatureConstants.Shark_Large] = [None],

                [CreatureConstants.Shark_Medium] = [None],

                [CreatureConstants.ShieldGuardian] = [None],

                [CreatureConstants.ShockerLizard] =
                [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Shrieker] = [None],

                [CreatureConstants.Skum] =
                [GetData(SkillConstants.Hide, 4, condition: "underwater"),
                    GetData(SkillConstants.Listen, 4, condition: "underwater"),
                    GetData(SkillConstants.Spot, 4, condition: "underwater")],

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

                [CreatureConstants.Spider_Monstrous_Hunter_Colossal] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_Hunter_Huge] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_Hunter_Large] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_Hunter_Medium] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_Hunter_Small] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_Hunter_Tiny] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Jump, 10),
                    GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 8, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 8, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Huge] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 8, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Large] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 8, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Medium] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 8, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Small] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 8, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 8, condition: "when using their webs"),
                    GetData(SkillConstants.MoveSilently, 8, condition: "when using their webs")],

                [CreatureConstants.Spider_Swarm] =
                [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.SpiderEater] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Squid] = [None],

                [CreatureConstants.Squid_Giant] = [None],

                [CreatureConstants.StagBeetle_Giant] = [None],

                [CreatureConstants.Stirge] = [None],

                [CreatureConstants.Succubus] =
                [GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.Spot, 8),
                    GetData(SkillConstants.Disguise, 10, condition: "when using Change Shape")],

                [CreatureConstants.Tarrasque] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Tendriculos] = [None],

                [CreatureConstants.Thoqqua] = [None],

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

                [CreatureConstants.Titan] = [None],

                [CreatureConstants.Toad] = [GetData(SkillConstants.Hide, 4)],

                [CreatureConstants.Tojanida_Juvenile] = [None],

                [CreatureConstants.Tojanida_Adult] = [None],

                [CreatureConstants.Tojanida_Elder] = [None],

                [CreatureConstants.Treant] = [None],

                [CreatureConstants.Triceratops] = [None],

                [CreatureConstants.Triton] = [None],

                [CreatureConstants.Troglodyte] = [GetData(SkillConstants.Hide, 4), GetData(SkillConstants.Hide, 4, condition: "in rocky or underground settings")],

                [CreatureConstants.Troll] = [None],

                [CreatureConstants.Troll_Scrag] = [None],

                [CreatureConstants.TrumpetArchon] = [None],

                [CreatureConstants.Tyrannosaurus] = [GetData(SkillConstants.Listen, 2), GetData(SkillConstants.Spot, 2)],

                [CreatureConstants.UmberHulk] = [None],

                [CreatureConstants.UmberHulk_TrulyHorrid] = [None],

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

                [CreatureConstants.Vargouille] = [None],

                [CreatureConstants.VioletFungus] = [None],

                [CreatureConstants.Vrock] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)],

                [CreatureConstants.Wasp_Giant] = [GetData(SkillConstants.Spot, 8), GetData(SkillConstants.Survival, 4, condition: "to orient itself")],

                [CreatureConstants.Weasel] =
                [GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Balance, 8),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")],

                [CreatureConstants.Weasel_Dire] = [None],

                [CreatureConstants.Whale_Baleen] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Whale_Cachalot] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Whale_Orca] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)],

                [CreatureConstants.Wight] = [GetData(SkillConstants.MoveSilently, 8)],

                [CreatureConstants.WillOWisp] = [None],

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

                [CreatureConstants.Wraith] = [None],

                [CreatureConstants.Wraith_Dread] = [None],

                [CreatureConstants.Wyvern] = [GetData(SkillConstants.Spot, 3)],

                [CreatureConstants.Xill] = [None],

                [CreatureConstants.Xorn_Minor] = [None],

                [CreatureConstants.Xorn_Average] = [None],

                [CreatureConstants.Xorn_Elder] = [None],

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

        public static Dictionary<string, List<string>> GetSubtypeSkillBonuses()
        {
            var testCases = new Dictionary<string, List<string>>
            {
                [CreatureConstants.Types.Subtypes.Air] = [None],

                [CreatureConstants.Types.Subtypes.Angel] = [None],

                [CreatureConstants.Types.Subtypes.Aquatic] =
                    [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")],

                [CreatureConstants.Types.Subtypes.Archon] = [None],

                [CreatureConstants.Types.Subtypes.Augmented] = [None],

                [CreatureConstants.Types.Subtypes.Chaotic] = [None],

                [CreatureConstants.Types.Subtypes.Cold] = [None],

                [CreatureConstants.Types.Subtypes.Dwarf] =
                    [GetData(SkillConstants.Appraise, 2, condition: "for items related to stone or metal"),
                    GetData(SkillConstants.Craft, 2, condition: "for items related to stone or metal")],

                [CreatureConstants.Types.Subtypes.Earth] = [None],

                [CreatureConstants.Types.Subtypes.Elf] = [None],

                [CreatureConstants.Types.Subtypes.Evil] = [None],

                [CreatureConstants.Types.Subtypes.Extraplanar] = [None],

                [CreatureConstants.Types.Subtypes.Fire] = [None],

                [CreatureConstants.Types.Subtypes.Gnome] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Craft, 2, focus: SkillConstants.Foci.Craft.Alchemy)],

                [CreatureConstants.Types.Subtypes.Goblinoid] = [None],

                [CreatureConstants.Types.Subtypes.Good] = [None],

                [CreatureConstants.Types.Subtypes.Halfling] = [GetData(SkillConstants.Listen, 2)],

                [CreatureConstants.Types.Subtypes.Incorporeal] =
                    [GetData(SkillConstants.MoveSilently, 9000, condition: "always succeeds, and cannot be heard by Listen checks unless it wants to be")],

                [CreatureConstants.Types.Subtypes.Lawful] = [None],

                [CreatureConstants.Types.Subtypes.Native] = [None],

                [CreatureConstants.Types.Subtypes.Reptilian] = [None],

                [CreatureConstants.Types.Subtypes.Shapechanger] = [None],

                [CreatureConstants.Types.Subtypes.Swarm] = [None],

                [CreatureConstants.Types.Subtypes.Water] =
                    [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")]
            };

            return testCases;
        }

        public static Dictionary<string, List<string>> GetSkillSynergiesSkillBonuses()
        {
            var testCases = new Dictionary<string, List<string>>();

            testCases[SkillConstants.Appraise] =
                [GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Appraiser),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Trader)];

            testCases[SkillConstants.Balance] = [None];

            testCases[SkillConstants.Bluff] =
                [GetData(SkillConstants.Diplomacy, 2),
                GetData(SkillConstants.Disguise, 2, condition: "acting"),
                GetData(SkillConstants.Intimidate, 2),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Dowser),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Soothsayer),
                GetData(SkillConstants.SleightOfHand, 2)];

            testCases[SkillConstants.Climb] = [None];

            testCases[SkillConstants.Concentration] = [None];

            testCases[SkillConstants.Craft] =
                [GetData(SkillConstants.Appraise, 2, condition: "related to items made with your Craft skill"),
                GetData(SkillConstants.Profession, 2, SkillConstants.Foci.Profession.Craftsman)];

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Cloth)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Glass)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Leather)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Stonemasonry)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] = [GetData(SkillConstants.Search, condition: "finding traps")] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker)] = 2;

            testCases[SkillConstants.DecipherScript] = [GetData(SkillConstants.UseMagicDevice, condition: "with scrolls")] = 2;

            testCases[SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister)] = 2;
            testCases[SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)] = 2;
            testCases[SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)] = 2;
            testCases[SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)] = 2;
            testCases[SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)] = 2;
            testCases[SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)] = 2;
            testCases[SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet)] = 2;

            testCases[SkillConstants.DisableDevice] = [GetData(None)] = 0;

            testCases[SkillConstants.Disguise] = [GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Act, "in costume")] = 2;

            testCases[SkillConstants.EscapeArtist] = [GetData(SkillConstants.UseRope, condition: "binding someone")] = 2;

            testCases[SkillConstants.Forgery] = [GetData(None)] = 0;

            testCases[SkillConstants.GatherInformation] = [GetData(None)] = 0;

            testCases[SkillConstants.HandleAnimal] = [GetData(SkillConstants.Diplomacy, condition: "wild empathy")] = 2;
            testCases[SkillConstants.HandleAnimal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer)] = 2;
            testCases[SkillConstants.HandleAnimal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer)] = 2;
            testCases[SkillConstants.HandleAnimal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer)] = 2;
            testCases[SkillConstants.HandleAnimal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd)] = 2;
            testCases[SkillConstants.HandleAnimal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)] = 2;
            testCases[SkillConstants.HandleAnimal] = [GetData(SkillConstants.Ride)] = 2;

            testCases[SkillConstants.Heal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer)] = 2;
            testCases[SkillConstants.Heal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse)] = 2;
            testCases[SkillConstants.Heal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife)] = 2;

            testCases[SkillConstants.Hide] = [GetData(None)] = 0;

            testCases[SkillConstants.Intimidate] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)] = 2;

            testCases[SkillConstants.Jump] = [GetData(SkillConstants.Tumble)] = 2;

            testCases[SkillConstants.Knowledge] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)] = 2;
            testCases[SkillConstants.Knowledge] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage)] = 2;
            testCases[SkillConstants.Knowledge] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana)] = [GetData(SkillConstants.Spellcraft)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] = [GetData(SkillConstants.Search, condition: "find secret doors or compartments")] = 2;

            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)] = [GetData(SkillConstants.Survival, condition: "underground")] = 2;

            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)] = [GetData(SkillConstants.Survival, condition: "keep from getting lost or avoid natural hazards")] = 2;

            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History)] = [GetData(SkillConstants.Knowledge, "bardic")] = 2;

            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = [GetData(SkillConstants.GatherInformation, 2)];
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Survival, condition: "in aboveground natural environments")] = 2;

            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = [GetData(SkillConstants.Diplomacy, 2)];
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Religion)] = [GetData(None)] = 0;

            testCases[SkillConstants.Build(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes)] = [GetData(SkillConstants.Survival, condition: "on other planes")] = 2;

            testCases[SkillConstants.Listen] = [GetData(None)] = 0;

            testCases[SkillConstants.MoveSilently] = [GetData(None)] = 0;

            testCases[SkillConstants.OpenLock] = [GetData(None)] = 0;

            testCases[SkillConstants.Perform] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Act)] = [GetData(SkillConstants.Disguise, condition: "impersonating someone else")] = 2;

            testCases[SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Comedy)] = [GetData(None)] = 0;

            testCases[SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Dance)] = [GetData(None)] = 0;

            testCases[SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.KeyboardInstruments)] = [GetData(None)] = 0;

            testCases[SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Oratory)] = [GetData(None)] = 0;

            testCases[SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.PercussionInstruments)] = [GetData(None)] = 0;

            testCases[SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Sing)] = [GetData(None)] = 0;

            testCases[SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.StringInstruments)] = [GetData(None)] = 0;

            testCases[SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.WindInstruments)] = [GetData(None)] = 0;

            testCases[SkillConstants.Profession] = [GetData(None)] = 0;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)] = [GetData(SkillConstants.Diplomacy, 2)];
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)] = [GetData(SkillConstants.Knowledge)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer)] = [GetData(SkillConstants.HandleAnimal)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer)] = [GetData(SkillConstants.HandleAnimal)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser)] = [GetData(SkillConstants.Appraise)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister)] = [GetData(SkillConstants.Diplomacy, 2)];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)] = [GetData(SkillConstants.Diplomacy, 2)];
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)] = [GetData(SkillConstants.Diplomacy, 2)];
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Clerk)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coiffeur)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Cook)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)] = [GetData(SkillConstants.Craft)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)] = [GetData(SkillConstants.Bluff, 2)];
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)] = [GetData(SkillConstants.Survival, 2)];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)] = [GetData(SkillConstants.Perform)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer)] = [GetData(SkillConstants.HandleAnimal)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)] = [GetData(SkillConstants.Diplomacy, 2)];
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)] = [GetData(SkillConstants.Diplomacy, 2)];
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer)] = [GetData(SkillConstants.Heal)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = [GetData(SkillConstants.Survival, 2)];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Laborer)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Launderer)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse)] = [GetData(SkillConstants.Heal)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)] = [GetData(SkillConstants.Diplomacy, 2)];
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)] = [GetData(SkillConstants.SenseMotive)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife)] = [GetData(SkillConstants.Heal)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator)] = [GetData(SkillConstants.Survival, 2)];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Nursemaid)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)] = [GetData(SkillConstants.Ride)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Polisher)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Porter)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage)] = [GetData(SkillConstants.Knowledge)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember)] = [GetData(SkillConstants.Swim)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)] = [GetData(SkillConstants.Intimidate)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)] = [GetData(SkillConstants.Swim)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Scribe)] = [None];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd)] = [GetData(SkillConstants.HandleAnimal)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer)] = [GetData(SkillConstants.Bluff, 2)];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)] = [GetData(SkillConstants.Knowledge)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)] = [GetData(SkillConstants.HandleAnimal)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)] = [GetData(SkillConstants.Ride)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)] = [GetData(SkillConstants.Appraise)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)] = [GetData(SkillConstants.SenseMotive)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = [GetData(SkillConstants.Survival, 2)];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet)] = [GetData(SkillConstants.Diplomacy, 2)];

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking)] = 2;

            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = 2;
            testCases[SkillConstants.Build(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;

            testCases[SkillConstants.Ride] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)] = 2;
            testCases[SkillConstants.Ride] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)] = 2;

            testCases[SkillConstants.Search] = [GetData(SkillConstants.Survival, condition: "following tracks")] = 2;

            testCases[SkillConstants.SenseMotive] = [GetData(SkillConstants.Diplomacy, 2)];
            testCases[SkillConstants.SenseMotive] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)] = 2;
            testCases[SkillConstants.SenseMotive] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)] = 2;

            testCases[SkillConstants.SleightOfHand] = [None];

            testCases[SkillConstants.Spellcraft] = [GetData(SkillConstants.UseMagicDevice, condition: "with scrolls")] = 2;

            testCases[SkillConstants.Spot] = [None];

            testCases[SkillConstants.Survival] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;
            testCases[SkillConstants.Survival] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)] = 2;
            testCases[SkillConstants.Survival] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = 2;
            testCases[SkillConstants.Survival] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator)] = 2;
            testCases[SkillConstants.Survival] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = 2;

            testCases[SkillConstants.Swim] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember)] = 2;
            testCases[SkillConstants.Swim] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)] = 2;

            testCases[SkillConstants.Tumble] = [GetData(SkillConstants.Balance, 2)];
            testCases[SkillConstants.Tumble] = [GetData(SkillConstants.Jump, 2)];

            testCases[SkillConstants.UseMagicDevice] = [GetData(SkillConstants.Spellcraft, condition: "decipher scrolls")] = 2;

            testCases[SkillConstants.UseRope] = [GetData(SkillConstants.Climb, condition: "with rope")] = 2;
            testCases[SkillConstants.UseRope] = [GetData(SkillConstants.EscapeArtist, condition: "escaping rope bonds")] = 2;

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
