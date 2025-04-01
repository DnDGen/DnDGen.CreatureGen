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

        public static Dictionary<string, List<string>> Creatures
        {
            get
            {
                var testCases = new Dictionary<string, List<string>>();

                testCases[CreatureConstants.Aasimar] = [GetData(SkillConstants.Listen, 2), GetData(SkillConstants.Spot, 2)];

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

                testCases[CreatureConstants.Ant_Giant_Queen] =
                    [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Survival, 4, condition: "tracking by scent")];

                testCases[CreatureConstants.Ant_Giant_Soldier] =
                    [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Survival, 4, condition: "tracking by scent")];

                testCases[CreatureConstants.Ant_Giant_Worker] =
                    [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Survival, 4, condition: "tracking by scent")];

                testCases[CreatureConstants.Ape] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Ape_Dire] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Aranea] =
                    [GetData(SkillConstants.Jump, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Arrowhawk_Adult] = [None];

                testCases[CreatureConstants.Arrowhawk_Elder] = [None];

                testCases[CreatureConstants.Arrowhawk_Juvenile] = [None];

                testCases[CreatureConstants.AssassinVine] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Athach] = [None];

                testCases[CreatureConstants.Avoral] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Azer] = [None];

                testCases[CreatureConstants.Babau] =
                    [GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.MoveSilently, 8),
                    GetData(SkillConstants.Search, 8)];

                testCases[CreatureConstants.Baboon] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Badger] = [GetData(SkillConstants.EscapeArtist, 4)];

                testCases[CreatureConstants.Badger_Dire] = [None];

                testCases[CreatureConstants.Balor] = [GetData(SkillConstants.Listen, 8), GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.BarbedDevil_Hamatula] = [None];

                testCases[CreatureConstants.Barghest] = [GetData(SkillConstants.Hide, 4, condition: "in wolf form")];

                testCases[CreatureConstants.Barghest_Greater] = [GetData(SkillConstants.Hide, 4, condition: "in wolf form")];

                testCases[CreatureConstants.Basilisk] = [GetData(SkillConstants.Hide, 4, condition: "in natural settings")];

                testCases[CreatureConstants.Basilisk_Greater] = [GetData(SkillConstants.Hide, 4, condition: "in natural settings")];

                testCases[CreatureConstants.Bat] =
                    [GetData(SkillConstants.Listen, 4, condition: "while able to use blindsense"),
                    GetData(SkillConstants.Spot, 4, condition: "while able to use blindsense")];

                testCases[CreatureConstants.Bat_Dire] =
                    [GetData(SkillConstants.Listen, 4, condition: "while able to use blindsense"),
                    GetData(SkillConstants.Spot, 4, condition: "while able to use blindsense")];

                testCases[CreatureConstants.Bat_Swarm] =
                    [GetData(SkillConstants.Listen, 4, condition: "while able to use blindsense"),
                    GetData(SkillConstants.Spot, 4, condition: "while able to use blindsense")];

                testCases[CreatureConstants.Bear_Black] = [GetData(SkillConstants.Swim, 4)];

                testCases[CreatureConstants.Bear_Brown] = [GetData(SkillConstants.Swim, 4)];

                testCases[CreatureConstants.Bear_Dire] = [None];

                testCases[CreatureConstants.Bear_Polar] =
                    [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 12, condition: "in snowy settings")];

                testCases[CreatureConstants.BeardedDevil_Barbazu] = [None];

                testCases[CreatureConstants.Bebilith] = [GetData(SkillConstants.Hide, 8)];

                testCases[CreatureConstants.Bee_Giant] = [GetData(SkillConstants.Spot, 4), GetData(SkillConstants.Survival, 4, condition: "To orient itself")];

                testCases[CreatureConstants.Behir] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Beholder] = [None];

                testCases[CreatureConstants.Beholder_Gauth] = [None];

                testCases[CreatureConstants.Belker] = [GetData(SkillConstants.MoveSilently, 4)];

                testCases[CreatureConstants.Bison] = [None];

                testCases[CreatureConstants.BlackPudding] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.BlackPudding_Elder] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.BlinkDog] = [None];

                testCases[CreatureConstants.Boar] = [None];

                testCases[CreatureConstants.Boar_Dire] = [None];

                testCases[CreatureConstants.Bodak] = [None];

                testCases[CreatureConstants.BombardierBeetle_Giant] = [None];

                testCases[CreatureConstants.BoneDevil_Osyluth] = [None];

                testCases[CreatureConstants.Bralani] = [None];

                testCases[CreatureConstants.Bugbear] = [GetData(SkillConstants.MoveSilently, 4)];

                testCases[CreatureConstants.Bulette] = [None];

                testCases[CreatureConstants.Camel_Bactrian] = [None];

                testCases[CreatureConstants.Camel_Dromedary] = [None];

                testCases[CreatureConstants.CarrionCrawler] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Cat] =
                    [GetData(SkillConstants.Climb, 4),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Jump, 8),
                    GetData(SkillConstants.Balance, 8),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of tall grass or heavy undergrowth")];

                testCases[CreatureConstants.Centaur] = [None];

                testCases[CreatureConstants.Centipede_Monstrous_Colossal] =
                    [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan] =
                    [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Centipede_Monstrous_Huge] =
                    [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Centipede_Monstrous_Large] =
                    [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Centipede_Monstrous_Medium] =
                    [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Centipede_Monstrous_Small] =
                    [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Centipede_Monstrous_Tiny] =
                    [GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Centipede_Swarm] =
                    [GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.ChainDevil_Kyton] = [GetData(SkillConstants.Craft, 8, condition: "involving metalwork")];

                testCases[CreatureConstants.ChaosBeast] = [None];

                testCases[CreatureConstants.Cheetah] = [None];

                testCases[CreatureConstants.Chimera_Black] =
                    [GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of scrubland or brush")];

                testCases[CreatureConstants.Chimera_Blue] =
                    [GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of scrubland or brush")];

                testCases[CreatureConstants.Chimera_Green] =
                    [GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of scrubland or brush")];

                testCases[CreatureConstants.Chimera_Red] =
                    [GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of scrubland or brush")];

                testCases[CreatureConstants.Chimera_White] =
                    [GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Hide, 4, condition: "in areas of scrubland or brush")];

                testCases[CreatureConstants.Choker] = [GetData(SkillConstants.Climb, 8), GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Chuul] = [None];

                testCases[CreatureConstants.Cloaker] = [None];

                testCases[CreatureConstants.Cockatrice] = [None];

                testCases[CreatureConstants.Couatl] = [None];

                testCases[CreatureConstants.Criosphinx] = [None];

                testCases[CreatureConstants.Crocodile] =
                    [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4, condition: "in water"),
                    GetData(SkillConstants.Hide, 10, condition: "laying in water with only its eyes and nostrils showing")];

                testCases[CreatureConstants.Crocodile_Giant] =
                    [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10"),
                    GetData(SkillConstants.Hide, 4, condition: "in water"),
                    GetData(SkillConstants.Hide, 10, condition: "laying in water with only its eyes and nostrils showing")];

                testCases[CreatureConstants.Cryohydra_5Heads] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Cryohydra_6Heads] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Cryohydra_7Heads] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Cryohydra_8Heads] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Cryohydra_9Heads] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Cryohydra_10Heads] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Cryohydra_11Heads] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Cryohydra_12Heads] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Spot, 2),
                    GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard"),
                    GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Darkmantle] =
                    [GetData(SkillConstants.Spot, 4, condition: "while able to use blindsight"),
                    GetData(SkillConstants.Listen, 4, condition: "while able to use blindsight"),
                    GetData(SkillConstants.Hide, 4)];

                testCases[CreatureConstants.Deinonychus] =
                    [GetData(SkillConstants.Hide, 8),
                    GetData(SkillConstants.Jump, 8),
                    GetData(SkillConstants.Listen, 8),
                    GetData(SkillConstants.Spot, 8),
                    GetData(SkillConstants.Survival, 8)];

                testCases[CreatureConstants.Delver] = [None];

                testCases[CreatureConstants.Derro] = [None];

                testCases[CreatureConstants.Derro_Sane] = [None];

                testCases[CreatureConstants.Destrachan] = [GetData(SkillConstants.Listen, 10)];

                testCases[CreatureConstants.Devourer] = [None];

                testCases[CreatureConstants.Digester] = [GetData(SkillConstants.Hide, 4), GetData(SkillConstants.Jump, 4)];

                testCases[CreatureConstants.DisplacerBeast] = [GetData(SkillConstants.Hide, 8)];

                testCases[CreatureConstants.DisplacerBeast_PackLord] = [GetData(SkillConstants.Hide, 8)];

                testCases[CreatureConstants.Djinni] = [None];

                testCases[CreatureConstants.Djinni_Noble] = [None];

                testCases[CreatureConstants.Dog] = [GetData(SkillConstants.Jump, 4), GetData(SkillConstants.Survival, 4, condition: "tracking by scent")];

                testCases[CreatureConstants.Dog_Riding] = [GetData(SkillConstants.Jump, 4), GetData(SkillConstants.Survival, 4, condition: "tracking by scent")];

                testCases[CreatureConstants.Donkey] = [GetData(SkillConstants.Balance, 2)];

                testCases[CreatureConstants.Doppelganger] =
                    [GetData(SkillConstants.Bluff, 4),
                    GetData(SkillConstants.Disguise, 4),
                    GetData(SkillConstants.Bluff, 4, condition: "when reading an opponent's mind"),
                    GetData(SkillConstants.Disguise, 4, condition: "when reading an opponent's mind"),
                    GetData(SkillConstants.Disguise, 10, condition: "when using Change Shape")];

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

                testCases[CreatureConstants.DragonTurtle] = [GetData(SkillConstants.Hide, 8, condition: "when submerged")];

                testCases[CreatureConstants.Dragonne] = [GetData(SkillConstants.Listen, 4), GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Dretch] = [None];

                testCases[CreatureConstants.Drider] =
                    [GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Dryad] = [GetData(SkillConstants.Diplomacy, 6, condition: "when using Wild Empathy")];

                testCases[CreatureConstants.Dwarf_Deep] = [None];

                testCases[CreatureConstants.Dwarf_Duergar] =
                    [GetData(SkillConstants.MoveSilently, 4),
                    GetData(SkillConstants.Listen, 1),
                    GetData(SkillConstants.Spot, 1)];

                testCases[CreatureConstants.Dwarf_Hill] = [None];

                testCases[CreatureConstants.Dwarf_Mountain] = [None];

                testCases[CreatureConstants.Eagle] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Eagle_Giant] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Efreeti] = [None];

                testCases[CreatureConstants.Elasmosaurus] = [GetData(SkillConstants.Hide, 8, condition: "in water")];

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

                testCases[CreatureConstants.Elf_Aquatic] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Search, 0,
                        condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for"),
                    GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.Elf_Drow] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Search, 0,
                        condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for"),
                    GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.Elf_Gray] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Search, 0,
                        condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for"),
                    GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.Elf_Half] =
                    [GetData(SkillConstants.Diplomacy, 2),
                    GetData(SkillConstants.GatherInformation, 2),
                    GetData(SkillConstants.Listen, 1),
                    GetData(SkillConstants.Search, 1),
                    GetData(SkillConstants.Spot, 1)];

                testCases[CreatureConstants.Elf_High] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Search, 0,
                        condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for"),
                    GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.Elf_Wild] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Search, 0,
                        condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for"),
                    GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.Elf_Wood] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.Search, 2),
                    GetData(SkillConstants.Search, 0,
                        condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for"),
                    GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.Erinyes] = [None];

                testCases[CreatureConstants.EtherealFilcher] =
                    [GetData(SkillConstants.Listen, 4),
                    GetData(SkillConstants.SleightOfHand, 8),
                    GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.EtherealMarauder] =
                    [GetData(SkillConstants.Listen, 2),
                    GetData(SkillConstants.MoveSilently, 2),
                    GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.Ettercap] =
                    [GetData(SkillConstants.Craft, 4, focus: SkillConstants.Foci.Craft.Trapmaking),
                    GetData(SkillConstants.Hide, 4),
                    GetData(SkillConstants.Spot, 4),
                    GetData(SkillConstants.Climb, 8),
                    GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Ettin] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Ettin] = [GetData(SkillConstants.Search, 2)];
                testCases[CreatureConstants.Ettin] = [GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.FireBeetle_Giant] = [None];

                testCases[CreatureConstants.FormianMyrmarch] = [None];

                testCases[CreatureConstants.FormianQueen] = [None];

                testCases[CreatureConstants.FormianTaskmaster] = [None];

                testCases[CreatureConstants.FormianWarrior] = [None];

                testCases[CreatureConstants.FormianWorker] = [None];

                testCases[CreatureConstants.FrostWorm] = [GetData(SkillConstants.Hide, condition: "on Cold Plains")] = 10;

                testCases[CreatureConstants.Gargoyle] = [GetData(SkillConstants.Hide)] = 2;
                testCases[CreatureConstants.Gargoyle] = [GetData(SkillConstants.Hide, condition: "concealed against a background of stone")] = 8;
                testCases[CreatureConstants.Gargoyle] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Gargoyle] = [GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.Gargoyle_Kapoacinth] = [GetData(SkillConstants.Hide)] = 2;
                testCases[CreatureConstants.Gargoyle_Kapoacinth] = [GetData(SkillConstants.Hide, condition: "concealed against a background of stone")] = 8;
                testCases[CreatureConstants.Gargoyle_Kapoacinth] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Gargoyle_Kapoacinth] = [GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.GelatinousCube] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.GelatinousCube] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Ghaele] = [None];

                testCases[CreatureConstants.Ghoul] = [None];

                testCases[CreatureConstants.Ghoul_Ghast] = [None];

                testCases[CreatureConstants.Ghoul_Lacedon] = [None];

                testCases[CreatureConstants.Giant_Cloud] = [None];

                testCases[CreatureConstants.Giant_Fire] = [None];

                testCases[CreatureConstants.Giant_Frost] = [None];

                testCases[CreatureConstants.Giant_Hill] = [None];

                testCases[CreatureConstants.Giant_Stone] = [GetData(SkillConstants.Hide, condition: "in rocky terrain")] = 8;

                testCases[CreatureConstants.Giant_Stone_Elder] = [GetData(SkillConstants.Hide, condition: "in rocky terrain")] = 8;

                testCases[CreatureConstants.Giant_Storm] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Giant_Storm] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.GibberingMouther] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.GibberingMouther] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.GibberingMouther] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Girallon] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Girallon] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Githyanki] = [None];

                testCases[CreatureConstants.Githzerai] = [None];

                testCases[CreatureConstants.Glabrezu] = [GetData(SkillConstants.Listen, 8)];
                testCases[CreatureConstants.Glabrezu] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Gnoll] = [None];

                testCases[CreatureConstants.Gnome_Forest] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Gnome_Forest] = [GetData(SkillConstants.Hide, condition: "in a wooded area")] = 8;

                testCases[CreatureConstants.Gnome_Rock] = [None];

                testCases[CreatureConstants.Gnome_Svirfneblin] = [GetData(SkillConstants.Hide)] = 2;
                testCases[CreatureConstants.Gnome_Svirfneblin] = [GetData(SkillConstants.Hide, condition: "underground")] = 2;

                testCases[CreatureConstants.Goblin] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.Goblin] = [GetData(SkillConstants.Ride)] = 4;

                testCases[CreatureConstants.Golem_Clay] = [None];

                testCases[CreatureConstants.Golem_Flesh] = [None];

                testCases[CreatureConstants.Golem_Iron] = [None];

                testCases[CreatureConstants.Golem_Stone] = [None];

                testCases[CreatureConstants.Golem_Stone_Greater] = [None];

                testCases[CreatureConstants.Gorgon] = [None];

                testCases[CreatureConstants.GrayOoze] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.GrayOoze] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.GrayRender] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.GreenHag] = [None];

                testCases[CreatureConstants.Grick] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Grick] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Grick] = [GetData(SkillConstants.Hide, condition: "in natural, rocky areas")] = 8;

                testCases[CreatureConstants.Griffon] = [GetData(SkillConstants.Jump, 4)];
                testCases[CreatureConstants.Griffon] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Grig] = [GetData(SkillConstants.Jump, 8)];
                testCases[CreatureConstants.Grig] = [GetData(SkillConstants.MoveSilently, condition: "in a forest setting")] = 5;

                testCases[CreatureConstants.Grig_WithFiddle] = [GetData(SkillConstants.Jump, 8)];
                testCases[CreatureConstants.Grig_WithFiddle] = [GetData(SkillConstants.MoveSilently, condition: "in a forest setting")] = 5;

                testCases[CreatureConstants.Grimlock] = [GetData(SkillConstants.Hide, condition: "in mountains or underground")] = 10;

                testCases[CreatureConstants.Gynosphinx] = [None];

                testCases[CreatureConstants.Halfling_Deep] = [None];

                testCases[CreatureConstants.Halfling_Lightfoot] = [GetData(SkillConstants.Climb)] = 2;
                testCases[CreatureConstants.Halfling_Lightfoot] = [GetData(SkillConstants.Jump)] = 2;
                testCases[CreatureConstants.Halfling_Lightfoot] = [GetData(SkillConstants.MoveSilently, 2)];

                testCases[CreatureConstants.Halfling_Tallfellow] = [None];

                testCases[CreatureConstants.Harpy] = [GetData(SkillConstants.Bluff, 4)];
                testCases[CreatureConstants.Harpy] = [GetData(SkillConstants.Listen, 4)];

                testCases[CreatureConstants.Hawk] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Hellcat_Bezekira] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.Hellcat_Bezekira] = [GetData(SkillConstants.MoveSilently, 4)];

                testCases[CreatureConstants.Hellwasp_Swarm] = [None];

                testCases[CreatureConstants.HellHound] = [GetData(SkillConstants.Hide)] = 5;
                testCases[CreatureConstants.HellHound] = [GetData(SkillConstants.MoveSilently)] = 5;

                testCases[CreatureConstants.HellHound_NessianWarhound] = [GetData(SkillConstants.Hide)] = 5;
                testCases[CreatureConstants.HellHound_NessianWarhound] = [GetData(SkillConstants.MoveSilently)] = 5;

                testCases[CreatureConstants.Hezrou] = [GetData(SkillConstants.Listen, 8)];
                testCases[CreatureConstants.Hezrou] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Hieracosphinx] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Hippogriff] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Hobgoblin] = [GetData(SkillConstants.MoveSilently, 4)];

                testCases[CreatureConstants.Horse_Heavy] = [None];

                testCases[CreatureConstants.Horse_Heavy_War] = [None];

                testCases[CreatureConstants.Horse_Light] = [None];

                testCases[CreatureConstants.Horse_Light_War] = [None];

                testCases[CreatureConstants.Howler] = [None];

                testCases[CreatureConstants.Hydra_5Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Hydra_5Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Hydra_5Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Hydra_5Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Hydra_6Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Hydra_6Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Hydra_6Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Hydra_6Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Hydra_7Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Hydra_7Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Hydra_7Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Hydra_7Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Hydra_8Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Hydra_8Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Hydra_8Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Hydra_8Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Hydra_9Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Hydra_9Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Hydra_9Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Hydra_9Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Hydra_10Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Hydra_10Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Hydra_10Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Hydra_10Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Hydra_11Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Hydra_11Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Hydra_11Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Hydra_11Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Hydra_12Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Hydra_12Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Hydra_12Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Hydra_12Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Homunculus] = [None];

                testCases[CreatureConstants.HornedDevil_Cornugon] = [None];

                testCases[CreatureConstants.HoundArchon] = [GetData(SkillConstants.Hide, condition: "in canine form")] = 4;
                testCases[CreatureConstants.HoundArchon] = [GetData(SkillConstants.Survival, condition: "in canine form")] = 4;

                testCases[CreatureConstants.Human] = [None];

                testCases[CreatureConstants.Hyena] = [GetData(SkillConstants.Hide, condition: "in tall grass or heavy undergrowth")] = 4;

                testCases[CreatureConstants.IceDevil_Gelugon] = [None];

                testCases[CreatureConstants.Imp] = [None];

                testCases[CreatureConstants.InvisibleStalker] = [None];

                testCases[CreatureConstants.Janni] = [None];

                testCases[CreatureConstants.Kobold] = [GetData(SkillConstants.Craft, focus: SkillConstants.Foci.Craft.Trapmaking)] = 2;
                testCases[CreatureConstants.Kobold] = [GetData(SkillConstants.Profession, focus: SkillConstants.Foci.Profession.Miner)] = 2;
                testCases[CreatureConstants.Kobold] = [GetData(SkillConstants.Search, 2)];

                testCases[CreatureConstants.Kolyarut] = [GetData(SkillConstants.Disguise, 4)];
                testCases[CreatureConstants.Kolyarut] = [GetData(SkillConstants.GatherInformation)] = 4;
                testCases[CreatureConstants.Kolyarut] = [GetData(SkillConstants.SenseMotive)] = 4;

                testCases[CreatureConstants.Kraken] = [None];

                testCases[CreatureConstants.Krenshar] = [GetData(SkillConstants.Jump, 4)];
                testCases[CreatureConstants.Krenshar] = [GetData(SkillConstants.MoveSilently, 4)];

                testCases[CreatureConstants.KuoToa] = [GetData(SkillConstants.EscapeArtist)] = 8;
                testCases[CreatureConstants.KuoToa] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.KuoToa] = [GetData(SkillConstants.Search)] = 4;

                testCases[CreatureConstants.Lamia] = [GetData(SkillConstants.Bluff, 4)];
                testCases[CreatureConstants.Lamia] = [GetData(SkillConstants.Hide, 4)];

                testCases[CreatureConstants.Lammasu] = [GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.LanternArchon] = [None];

                testCases[CreatureConstants.Lemure] = [None];

                testCases[CreatureConstants.Leonal] = [GetData(SkillConstants.Balance)] = 4;
                testCases[CreatureConstants.Leonal] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Leonal] = [GetData(SkillConstants.MoveSilently, 4)];

                testCases[CreatureConstants.Leopard] = [GetData(SkillConstants.Jump, 8)];
                testCases[CreatureConstants.Leopard] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Leopard] = [GetData(SkillConstants.Hide, condition: "in areas of tall grass or heavy undergrowth")] = 4;
                testCases[CreatureConstants.Leopard] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.Leopard] = [GetData(SkillConstants.Balance, 8)];
                testCases[CreatureConstants.Leopard] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Leopard] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Lillend] = [GetData(SkillConstants.Survival)] = 4;

                testCases[CreatureConstants.Lion] = [GetData(SkillConstants.Balance)] = 4;
                testCases[CreatureConstants.Lion] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Lion] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.Lion] = [GetData(SkillConstants.Hide, condition: "in tall grass or heavy undergrowth")] = 8;

                testCases[CreatureConstants.Lion_Dire] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Lion_Dire] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.Lion_Dire] = [GetData(SkillConstants.Hide, condition: "in tall grass or heavy undergrowth")] = 4;

                testCases[CreatureConstants.Lizard] = [GetData(SkillConstants.Balance, 8)];
                testCases[CreatureConstants.Lizard] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Lizard] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Lizard_Monitor] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Lizard_Monitor] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Lizard_Monitor] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Lizard_Monitor] = [GetData(SkillConstants.Hide, condition: "in forested or overgrown areas")] = 4;
                testCases[CreatureConstants.Lizard_Monitor] = [GetData(SkillConstants.MoveSilently, 4)];

                testCases[CreatureConstants.Lizardfolk] = [GetData(SkillConstants.Jump, 4)];
                testCases[CreatureConstants.Lizardfolk] = [GetData(SkillConstants.Swim)] = 4;
                testCases[CreatureConstants.Lizardfolk] = [GetData(SkillConstants.Balance)] = 4;

                testCases[CreatureConstants.Locathah] = [None];

                testCases[CreatureConstants.Locust_Swarm] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.Locust_Swarm] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Magmin] = [None];

                testCases[CreatureConstants.MantaRay] = [None];

                testCases[CreatureConstants.Manticore] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Marilith] = [GetData(SkillConstants.Listen, 8)];
                testCases[CreatureConstants.Marilith] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Marut] = [GetData(SkillConstants.Concentration)] = 4;
                testCases[CreatureConstants.Marut] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.Marut] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Medusa] = [None];

                testCases[CreatureConstants.Megaraptor] = [GetData(SkillConstants.Hide, 8)];
                testCases[CreatureConstants.Megaraptor] = [GetData(SkillConstants.Jump, 8)];
                testCases[CreatureConstants.Megaraptor] = [GetData(SkillConstants.Listen, 8)];
                testCases[CreatureConstants.Megaraptor] = [GetData(SkillConstants.Spot, 8)];
                testCases[CreatureConstants.Megaraptor] = [GetData(SkillConstants.Survival, 8)];

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

                testCases[CreatureConstants.Mimic] = [GetData(SkillConstants.Disguise)] = 8;

                testCases[CreatureConstants.MindFlayer] = [None];

                testCases[CreatureConstants.Minotaur] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.Minotaur] = [GetData(SkillConstants.Search)] = 4;
                testCases[CreatureConstants.Minotaur] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Mohrg] = [None];

                testCases[CreatureConstants.Monkey] = [GetData(SkillConstants.Balance, 8)];
                testCases[CreatureConstants.Monkey] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Monkey] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Mule] = [GetData(SkillConstants.Balance, condition: "To avoid slipping or falling")] = 2;

                testCases[CreatureConstants.Mummy] = [None];

                testCases[CreatureConstants.Naga_Dark] = [None];

                testCases[CreatureConstants.Naga_Guardian] = [None];

                testCases[CreatureConstants.Naga_Spirit] = [None];

                testCases[CreatureConstants.Naga_Water] = [None];

                testCases[CreatureConstants.Nalfeshnee] = [GetData(SkillConstants.Listen, 8)];
                testCases[CreatureConstants.Nalfeshnee] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.NightHag] = [None];

                testCases[CreatureConstants.Nightcrawler] = [None];

                testCases[CreatureConstants.Nightmare] = [None];

                testCases[CreatureConstants.Nightmare_Cauchemar] = [None];

                testCases[CreatureConstants.Nightwalker] = [GetData(SkillConstants.Hide, condition: "in a dark area")] = 8;

                testCases[CreatureConstants.Nightwing] = [GetData(SkillConstants.Hide, condition: "in a dark area or flying in a dark sky")] = 8;

                testCases[CreatureConstants.Nixie] = [GetData(SkillConstants.Hide, condition: "in water")] = 5;

                testCases[CreatureConstants.Nymph] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Nymph] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.OchreJelly] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.OchreJelly] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Octopus] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Octopus] = [GetData(SkillConstants.EscapeArtist)] = 10;

                testCases[CreatureConstants.Octopus_Giant] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Octopus_Giant] = [GetData(SkillConstants.EscapeArtist)] = 10;

                testCases[CreatureConstants.Ogre] = [None];

                testCases[CreatureConstants.Ogre_Merrow] = [None];

                testCases[CreatureConstants.OgreMage] = [None];

                testCases[CreatureConstants.Orc] = [None];

                testCases[CreatureConstants.Orc_Half] = [None];

                testCases[CreatureConstants.Otyugh] = [GetData(SkillConstants.Hide, condition: "in its lair")] = 8;

                testCases[CreatureConstants.Owl] = [GetData(SkillConstants.Listen, 8)];
                testCases[CreatureConstants.Owl] = [GetData(SkillConstants.MoveSilently)] = 14;
                testCases[CreatureConstants.Owl] = [GetData(SkillConstants.Spot, condition: "in areas of shadowy illumination")] = 8;

                testCases[CreatureConstants.Owl_Giant] = [GetData(SkillConstants.Listen, 8)];
                testCases[CreatureConstants.Owl_Giant] = [GetData(SkillConstants.MoveSilently, condition: "in flight")] = 8;
                testCases[CreatureConstants.Owl_Giant] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Owlbear] = [None];

                testCases[CreatureConstants.Pegasus] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.Pegasus] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.PhantomFungus] = [GetData(SkillConstants.MoveSilently)] = 5;

                testCases[CreatureConstants.PhaseSpider] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.PhaseSpider] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Phasm] = [GetData(SkillConstants.Disguise, condition: "when using Shapechange")] = 10;

                testCases[CreatureConstants.PitFiend] = [None];

                testCases[CreatureConstants.Pixie] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Pixie] = [GetData(SkillConstants.Search, 2)];
                testCases[CreatureConstants.Pixie] = [GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.Pixie_WithIrresistibleDance] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Pixie_WithIrresistibleDance] = [GetData(SkillConstants.Search, 2)];
                testCases[CreatureConstants.Pixie_WithIrresistibleDance] = [GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.Pony] = [None];

                testCases[CreatureConstants.Pony_War] = [None];

                testCases[CreatureConstants.Porpoise] = [GetData(SkillConstants.Listen, condition: "while able to use blindsight")] = 4;
                testCases[CreatureConstants.Porpoise] = [GetData(SkillConstants.Spot, condition: "while able to use blindsight")] = 4;

                testCases[CreatureConstants.PrayingMantis_Giant] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.PrayingMantis_Giant] = [GetData(SkillConstants.Hide, condition: "surrounded by foliage")] = 8;
                testCases[CreatureConstants.PrayingMantis_Giant] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Pseudodragon] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Pseudodragon] = [GetData(SkillConstants.Hide, condition: "in forests or overgrown areas")] = 4;

                testCases[CreatureConstants.PurpleWorm] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.PurpleWorm] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Pyrohydra_5Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Pyrohydra_5Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Pyrohydra_5Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Pyrohydra_5Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Pyrohydra_6Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Pyrohydra_6Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Pyrohydra_6Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Pyrohydra_6Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Pyrohydra_7Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Pyrohydra_7Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Pyrohydra_7Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Pyrohydra_7Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Pyrohydra_8Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Pyrohydra_8Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Pyrohydra_8Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Pyrohydra_8Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Pyrohydra_9Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Pyrohydra_9Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Pyrohydra_9Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Pyrohydra_9Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Pyrohydra_10Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Pyrohydra_10Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Pyrohydra_10Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Pyrohydra_10Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Pyrohydra_11Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Pyrohydra_11Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Pyrohydra_11Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Pyrohydra_11Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Pyrohydra_12Heads] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Pyrohydra_12Heads] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Pyrohydra_12Heads] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Pyrohydra_12Heads] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Quasit] = [None];

                testCases[CreatureConstants.Rakshasa] = [GetData(SkillConstants.Bluff, 4)];
                testCases[CreatureConstants.Rakshasa] = [GetData(SkillConstants.Bluff, condition: "when using Change Shape")] = 10;
                testCases[CreatureConstants.Rakshasa] = [GetData(SkillConstants.Bluff, condition: "when reading an opponent's mind")] = 4;
                testCases[CreatureConstants.Rakshasa] = [GetData(SkillConstants.Disguise, 4)];
                testCases[CreatureConstants.Rakshasa] = [GetData(SkillConstants.Disguise, condition: "when reading an opponent's mind")] = 4;

                testCases[CreatureConstants.Rast] = [None];

                testCases[CreatureConstants.Rat] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Rat] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.Rat] = [GetData(SkillConstants.Balance, 8)];
                testCases[CreatureConstants.Rat] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Rat] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Rat] = [GetData(SkillConstants.Swim)] = 8;
                testCases[CreatureConstants.Rat] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Rat] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Rat_Dire] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Rat_Dire] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Rat_Dire] = [GetData(SkillConstants.Swim)] = 8;
                testCases[CreatureConstants.Rat_Dire] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Rat_Swarm] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Rat_Swarm] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.Rat_Swarm] = [GetData(SkillConstants.Balance, 8)];
                testCases[CreatureConstants.Rat_Swarm] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Rat_Swarm] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Rat_Swarm] = [GetData(SkillConstants.Swim)] = 8;
                testCases[CreatureConstants.Rat_Swarm] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Rat_Swarm] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Raven] = [None];

                testCases[CreatureConstants.Ravid] = [None];

                testCases[CreatureConstants.RazorBoar] = [None];

                testCases[CreatureConstants.Remorhaz] = [GetData(SkillConstants.Listen, 4)];

                testCases[CreatureConstants.Retriever] = [None];

                testCases[CreatureConstants.Rhinoceras] = [None];

                testCases[CreatureConstants.Roc] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Roper] = [GetData(SkillConstants.Hide, condition: "in stony or icy areas")] = 8;

                testCases[CreatureConstants.RustMonster] = [None];

                testCases[CreatureConstants.Sahuagin] = [GetData(SkillConstants.Hide, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin] = [GetData(SkillConstants.Listen, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin] = [GetData(SkillConstants.Spot, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin] = [GetData(SkillConstants.Survival, condition: "within 50 miles of its home")] = 4;
                testCases[CreatureConstants.Sahuagin] = [GetData(SkillConstants.Profession, focus: SkillConstants.Foci.Profession.Hunter, condition: "within 50 miles of its home")] = 4;
                testCases[CreatureConstants.Sahuagin] = [GetData(SkillConstants.HandleAnimal, condition: "when working with sharks")] = 4;

                testCases[CreatureConstants.Sahuagin_Malenti] = [GetData(SkillConstants.Hide, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin_Malenti] = [GetData(SkillConstants.Listen, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin_Malenti] = [GetData(SkillConstants.Spot, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin_Malenti] = [GetData(SkillConstants.Survival, condition: "within 50 miles of its home")] = 4;
                testCases[CreatureConstants.Sahuagin_Malenti] = [GetData(SkillConstants.Profession, focus: SkillConstants.Foci.Profession.Hunter, condition: "within 50 miles of its home")] = 4;
                testCases[CreatureConstants.Sahuagin_Malenti] = [GetData(SkillConstants.HandleAnimal, condition: "when working with sharks")] = 4;

                testCases[CreatureConstants.Sahuagin_Mutant] = [GetData(SkillConstants.Hide, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin_Mutant] = [GetData(SkillConstants.Listen, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin_Mutant] = [GetData(SkillConstants.Spot, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin_Mutant] = [GetData(SkillConstants.Survival, condition: "within 50 miles of its home")] = 4;
                testCases[CreatureConstants.Sahuagin_Mutant] = [GetData(SkillConstants.Profession, focus: SkillConstants.Foci.Profession.Hunter, condition: "within 50 miles of its home")] = 4;
                testCases[CreatureConstants.Sahuagin_Mutant] = [GetData(SkillConstants.HandleAnimal, condition: "when working with sharks")] = 4;

                testCases[CreatureConstants.Salamander_Flamebrother] = [GetData(SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing))] = 4;

                testCases[CreatureConstants.Salamander_Average] = [GetData(SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing))] = 4;

                testCases[CreatureConstants.Salamander_Noble] = [GetData(SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing))] = 4;

                testCases[CreatureConstants.Satyr] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Satyr] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.Satyr] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.Satyr] = [GetData(SkillConstants.Perform)] = 4;
                testCases[CreatureConstants.Satyr] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Satyr_WithPipes] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Satyr_WithPipes] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.Satyr_WithPipes] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.Satyr_WithPipes] = [GetData(SkillConstants.Perform)] = 4;
                testCases[CreatureConstants.Satyr_WithPipes] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Scorpion_Monstrous_Colossal] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal] = [GetData(SkillConstants.Climb, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal] = [GetData(SkillConstants.Hide, 4)];

                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan] = [GetData(SkillConstants.Climb, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan] = [GetData(SkillConstants.Hide, 4)];

                testCases[CreatureConstants.Scorpion_Monstrous_Huge] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Huge] = [GetData(SkillConstants.Climb, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Huge] = [GetData(SkillConstants.Hide, 4)];

                testCases[CreatureConstants.Scorpion_Monstrous_Large] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Large] = [GetData(SkillConstants.Climb, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Large] = [GetData(SkillConstants.Hide, 4)];

                testCases[CreatureConstants.Scorpion_Monstrous_Medium] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Medium] = [GetData(SkillConstants.Climb, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Medium] = [GetData(SkillConstants.Hide, 4)];

                testCases[CreatureConstants.Scorpion_Monstrous_Small] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Small] = [GetData(SkillConstants.Climb, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Small] = [GetData(SkillConstants.Hide, 4)];

                testCases[CreatureConstants.Scorpion_Monstrous_Tiny] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny] = [GetData(SkillConstants.Climb, 4)];
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny] = [GetData(SkillConstants.Hide, 4)];

                testCases[CreatureConstants.Scorpionfolk] = [None];

                testCases[CreatureConstants.SeaCat] = [None];

                testCases[CreatureConstants.SeaHag] = [None];

                testCases[CreatureConstants.Shadow] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Shadow] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Shadow] = [GetData(SkillConstants.Search)] = 4;
                testCases[CreatureConstants.Shadow] = [GetData(SkillConstants.Hide, condition: "in areas of shadowy illumination")] = 4;
                testCases[CreatureConstants.Shadow] = [GetData(SkillConstants.Hide, condition: "in brightly lit areas")] = -4;

                testCases[CreatureConstants.Shadow_Greater] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Shadow_Greater] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Shadow_Greater] = [GetData(SkillConstants.Search)] = 4;
                testCases[CreatureConstants.Shadow_Greater] = [GetData(SkillConstants.Hide, condition: "in areas of shadowy illumination")] = 4;
                testCases[CreatureConstants.Shadow_Greater] = [GetData(SkillConstants.Hide, condition: "in brightly lit areas")] = -4;

                testCases[CreatureConstants.ShadowMastiff] = [GetData(SkillConstants.Survival, condition: "when tracking by scent")] = 4;

                testCases[CreatureConstants.ShamblingMound] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.ShamblingMound] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.ShamblingMound] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.ShamblingMound] = [GetData(SkillConstants.Hide, condition: "in a swampy or forested area")] = 8;

                testCases[CreatureConstants.Shark_Dire] = [None];

                testCases[CreatureConstants.Shark_Huge] = [None];

                testCases[CreatureConstants.Shark_Large] = [None];

                testCases[CreatureConstants.Shark_Medium] = [None];

                testCases[CreatureConstants.ShieldGuardian] = [None];

                testCases[CreatureConstants.ShockerLizard] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.ShockerLizard] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.ShockerLizard] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.ShockerLizard] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.ShockerLizard] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.ShockerLizard] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.ShockerLizard] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Shrieker] = [None];

                testCases[CreatureConstants.Skum] = [GetData(SkillConstants.Hide, condition: "underwater")] = 4;
                testCases[CreatureConstants.Skum] = [GetData(SkillConstants.Listen, condition: "underwater")] = 4;
                testCases[CreatureConstants.Skum] = [GetData(SkillConstants.Spot, condition: "underwater")] = 4;

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

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal] = [GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = [GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge] = [GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large] = [GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium] = [GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small] = [GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny] = [GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = [GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = [GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = [GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = [GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = [GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = [GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large] = [GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large] = [GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = [GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = [GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small] = [GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small] = [GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = [GetData(SkillConstants.Spot, 4)];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = [GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = [GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Swarm] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Spider_Swarm] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.Spider_Swarm] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Spider_Swarm] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.SpiderEater] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.SpiderEater] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Squid] = [None];

                testCases[CreatureConstants.Squid_Giant] = [None];

                testCases[CreatureConstants.StagBeetle_Giant] = [None];

                testCases[CreatureConstants.Stirge] = [None];

                testCases[CreatureConstants.Succubus] = [GetData(SkillConstants.Listen, 8)];
                testCases[CreatureConstants.Succubus] = [GetData(SkillConstants.Spot, 8)];
                testCases[CreatureConstants.Succubus] = [GetData(SkillConstants.Disguise, condition: "when using Change Shape")] = 10;

                testCases[CreatureConstants.Tarrasque] = [GetData(SkillConstants.Listen, 8)];
                testCases[CreatureConstants.Tarrasque] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Tendriculos] = [None];

                testCases[CreatureConstants.Thoqqua] = [None];

                testCases[CreatureConstants.Tiefling] = [GetData(SkillConstants.Bluff, 2)];
                testCases[CreatureConstants.Tiefling] = [GetData(SkillConstants.Hide)] = 2;

                testCases[CreatureConstants.Tiger] = [GetData(SkillConstants.Balance)] = 4;
                testCases[CreatureConstants.Tiger] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Tiger] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.Tiger] = [GetData(SkillConstants.Hide, condition: "in tall grass or heavy undergrowth")] = 4;

                testCases[CreatureConstants.Tiger_Dire] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Tiger_Dire] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.Tiger_Dire] = [GetData(SkillConstants.Hide, condition: "in tall grass or heavy undergrowth")] = 4;

                testCases[CreatureConstants.Titan] = [None];

                testCases[CreatureConstants.Toad] = [GetData(SkillConstants.Hide, 4)];

                testCases[CreatureConstants.Tojanida_Juvenile] = [None];

                testCases[CreatureConstants.Tojanida_Adult] = [None];

                testCases[CreatureConstants.Tojanida_Elder] = [None];

                testCases[CreatureConstants.Treant] = [None];

                testCases[CreatureConstants.Triceratops] = [None];

                testCases[CreatureConstants.Triton] = [None];

                testCases[CreatureConstants.Troglodyte] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.Troglodyte] = [GetData(SkillConstants.Hide, condition: "in rocky or underground settings")] = 4;

                testCases[CreatureConstants.Troll] = [None];

                testCases[CreatureConstants.Troll_Scrag] = [None];

                testCases[CreatureConstants.TrumpetArchon] = [None];

                testCases[CreatureConstants.Tyrannosaurus] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Tyrannosaurus] = [GetData(SkillConstants.Spot, 2)];

                testCases[CreatureConstants.UmberHulk] = [None];

                testCases[CreatureConstants.UmberHulk_TrulyHorrid] = [None];

                testCases[CreatureConstants.Unicorn] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.Unicorn] = [GetData(SkillConstants.Survival, condition: "within the boundaries of their forest")] = 3;

                testCases[CreatureConstants.VampireSpawn] = [GetData(SkillConstants.Bluff, 4)];
                testCases[CreatureConstants.VampireSpawn] = [GetData(SkillConstants.Hide, 4)];
                testCases[CreatureConstants.VampireSpawn] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.VampireSpawn] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.VampireSpawn] = [GetData(SkillConstants.Search)] = 4;
                testCases[CreatureConstants.VampireSpawn] = [GetData(SkillConstants.SenseMotive)] = 4;
                testCases[CreatureConstants.VampireSpawn] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Vargouille] = [None];

                testCases[CreatureConstants.VioletFungus] = [None];

                testCases[CreatureConstants.Vrock] = [GetData(SkillConstants.Listen, 8)];
                testCases[CreatureConstants.Vrock] = [GetData(SkillConstants.Spot, 8)];

                testCases[CreatureConstants.Wasp_Giant] = [GetData(SkillConstants.Spot, 8)];
                testCases[CreatureConstants.Wasp_Giant] = [GetData(SkillConstants.Survival, condition: "to orient itself")] = 4;

                testCases[CreatureConstants.Weasel] = [GetData(SkillConstants.MoveSilently, 4)];
                testCases[CreatureConstants.Weasel] = [GetData(SkillConstants.Balance, 8)];
                testCases[CreatureConstants.Weasel] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Weasel] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Weasel_Dire] = [None];

                testCases[CreatureConstants.Whale_Baleen] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.Whale_Baleen] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Whale_Cachalot] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.Whale_Cachalot] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Whale_Orca] = [GetData(SkillConstants.Listen, 4)];
                testCases[CreatureConstants.Whale_Orca] = [GetData(SkillConstants.Spot, 4)];

                testCases[CreatureConstants.Wight] = [GetData(SkillConstants.MoveSilently, 8)];

                testCases[CreatureConstants.WillOWisp] = [None];

                testCases[CreatureConstants.WinterWolf] = [GetData(SkillConstants.Listen, 1)];
                testCases[CreatureConstants.WinterWolf] = [GetData(SkillConstants.MoveSilently)] = 1;
                testCases[CreatureConstants.WinterWolf] = [GetData(SkillConstants.Spot, 1)];
                testCases[CreatureConstants.WinterWolf] = [GetData(SkillConstants.Hide)] = 2;
                testCases[CreatureConstants.WinterWolf] = [GetData(SkillConstants.Hide, condition: "in areas of snow and ice")] = 5;
                testCases[CreatureConstants.WinterWolf] = [GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Wolf] = [GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Wolf_Dire] = [GetData(SkillConstants.Hide)] = 2;
                testCases[CreatureConstants.Wolf_Dire] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Wolf_Dire] = [GetData(SkillConstants.MoveSilently, 2)];
                testCases[CreatureConstants.Wolf_Dire] = [GetData(SkillConstants.Spot, 2)];
                testCases[CreatureConstants.Wolf_Dire] = [GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Wolverine] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Wolverine] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Wolverine_Dire] = [GetData(SkillConstants.Climb, 8)];
                testCases[CreatureConstants.Wolverine_Dire] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Worg] = [GetData(SkillConstants.Listen, 1)];
                testCases[CreatureConstants.Worg] = [GetData(SkillConstants.MoveSilently)] = 1;
                testCases[CreatureConstants.Worg] = [GetData(SkillConstants.Spot, 1)];
                testCases[CreatureConstants.Worg] = [GetData(SkillConstants.Hide)] = 2;
                testCases[CreatureConstants.Worg] = [GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Wraith] = [None];

                testCases[CreatureConstants.Wraith_Dread] = [None];

                testCases[CreatureConstants.Wyvern] = [GetData(SkillConstants.Spot)] = 3;

                testCases[CreatureConstants.Xill] = [None];

                testCases[CreatureConstants.Xorn_Minor] = [None];

                testCases[CreatureConstants.Xorn_Average] = [None];

                testCases[CreatureConstants.Xorn_Elder] = [None];

                testCases[CreatureConstants.YethHound] = [GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Yrthak] = [GetData(SkillConstants.Listen, 4)];

                testCases[CreatureConstants.YuanTi_Pureblood] = [GetData(SkillConstants.Disguise, condition: "when impersonating a human")] = 5;

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms] = [GetData(SkillConstants.Hide, condition: "when using Chameleon Power")] = 10;

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead] = [GetData(SkillConstants.Hide, condition: "when using Chameleon Power")] = 10;

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail] = [GetData(SkillConstants.Hide, condition: "when using Chameleon Power")] = 10;

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = [GetData(SkillConstants.Hide, condition: "when using Chameleon Power")] = 10;

                testCases[CreatureConstants.YuanTi_Abomination] = [GetData(SkillConstants.Climb, 10, condition: "can always take 10")];
                testCases[CreatureConstants.YuanTi_Abomination] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.YuanTi_Abomination] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];
                testCases[CreatureConstants.YuanTi_Abomination] = [GetData(SkillConstants.Hide, condition: "when using Chameleon Power")] = 10;

                testCases[CreatureConstants.Zelekhut] = [GetData(SkillConstants.Search)] = 4;
                testCases[CreatureConstants.Zelekhut] = [GetData(SkillConstants.SenseMotive)] = 4;

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        public static Dictionary<string, List<string>> GetSkillBonusesData()
        {
            return GetCreatureSkillBonuses()
                .Union(GetTemplateSkillBonuses())
                .Union(GetTypeSkillBonuses())
                .Union(GetSubtypeSkillBonuses())
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static Dictionary<string, List<string>> Types
        {
            get
            {
                var testCases = new Dictionary<string, Dictionary<string, int>>();
                var types = CreatureConstants.Types.GetAll();

                foreach (var type in types)
                {
                    testCases[type] = new Dictionary<string, int>();
                }

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

        public static Dictionary<string, List<string>> Subtypes
        {
            get
            {
                var testCases = new Dictionary<string, Dictionary<string, int>>();
                var subtypes = CreatureConstants.Types.Subtypes.GetAll()
                    .Except(new[]
                    {
                        CreatureConstants.Types.Subtypes.Gnoll,
                        CreatureConstants.Types.Subtypes.Human,
                        CreatureConstants.Types.Subtypes.Orc,
                    }); //INFO: This is duplicated from the creature entry

                foreach (var subtype in subtypes)
                {
                    testCases[subtype] = new Dictionary<string, int>();
                }

                testCases[CreatureConstants.Types.Subtypes.Air] = [None];

                testCases[CreatureConstants.Types.Subtypes.Angel] = [None];

                testCases[CreatureConstants.Types.Subtypes.Aquatic] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Types.Subtypes.Aquatic] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                testCases[CreatureConstants.Types.Subtypes.Archon] = [None];

                testCases[CreatureConstants.Types.Subtypes.Augmented] = [None];

                testCases[CreatureConstants.Types.Subtypes.Chaotic] = [None];

                testCases[CreatureConstants.Types.Subtypes.Cold] = [None];

                testCases[CreatureConstants.Types.Subtypes.Dwarf] = [GetData(SkillConstants.Appraise, condition: "for items related to stone or metal")] = 2;
                testCases[CreatureConstants.Types.Subtypes.Dwarf] = [GetData(SkillConstants.Craft, condition: "for items related to stone or metal")] = 2;

                testCases[CreatureConstants.Types.Subtypes.Earth] = [None];

                testCases[CreatureConstants.Types.Subtypes.Elf] = [None];

                testCases[CreatureConstants.Types.Subtypes.Evil] = [None];

                testCases[CreatureConstants.Types.Subtypes.Extraplanar] = [None];

                testCases[CreatureConstants.Types.Subtypes.Fire] = [None];

                testCases[CreatureConstants.Types.Subtypes.Gnome] = [GetData(SkillConstants.Listen, 2)];
                testCases[CreatureConstants.Types.Subtypes.Gnome] = [GetData(SkillConstants.Craft, focus: SkillConstants.Foci.Craft.Alchemy)] = 2;

                testCases[CreatureConstants.Types.Subtypes.Goblinoid] = [None];

                testCases[CreatureConstants.Types.Subtypes.Good] = [None];

                testCases[CreatureConstants.Types.Subtypes.Halfling] = [GetData(SkillConstants.Listen, 2)];

                testCases[CreatureConstants.Types.Subtypes.Incorporeal] = [GetData(SkillConstants.MoveSilently, condition: "always succeeds, and cannot be heard by Listen checks unless it wants to be")] = 9000;

                testCases[CreatureConstants.Types.Subtypes.Lawful] = [None];

                testCases[CreatureConstants.Types.Subtypes.Native] = [None];

                testCases[CreatureConstants.Types.Subtypes.Reptilian] = [None];

                testCases[CreatureConstants.Types.Subtypes.Shapechanger] = [None];

                testCases[CreatureConstants.Types.Subtypes.Swarm] = [None];

                testCases[CreatureConstants.Types.Subtypes.Water] = [GetData(SkillConstants.Swim, 8, condition: "special action or avoid a hazard")];
                testCases[CreatureConstants.Types.Subtypes.Water] = [GetData(SkillConstants.Swim, 10, condition: "can always take 10")];

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        public static Dictionary<string, List<string>> SkillSynergies
        {
            get
            {
                var testCases = new Dictionary<string, Dictionary<string, int>>();
                var skills = new[]
                {
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
                    };

                foreach (var skill in skills)
                {
                    testCases[GetData(] = new Dictionary<string, int>();
                }

                testCases[GetData(SkillConstants.Appraise] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser)] = 2;
                testCases[GetData(SkillConstants.Appraise] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)] = 2;

                testCases[GetData(SkillConstants.Balance] = [None];

                testCases[GetData(SkillConstants.Bluff] = [GetData(SkillConstants.Diplomacy, 2)];
                testCases[GetData(SkillConstants.Bluff] = [GetData(SkillConstants.Disguise, condition: "acting")] = 2;
                testCases[GetData(SkillConstants.Bluff] = [GetData(SkillConstants.Intimidate)] = 2;
                testCases[GetData(SkillConstants.Bluff] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)] = 2;
                testCases[GetData(SkillConstants.Bluff] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer)] = 2;
                testCases[GetData(SkillConstants.Bluff] = [GetData(SkillConstants.SleightOfHand, 2)];

                testCases[GetData(SkillConstants.Climb] = [None];

                testCases[GetData(SkillConstants.Concentration] = [None];

                testCases[GetData(SkillConstants.Craft] = [GetData(SkillConstants.Appraise, condition: "related to items made with your Craft skill")] = 2;
                testCases[GetData(SkillConstants.Craft] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Cloth)] = [None];

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Glass)] = [None];

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Leather)] = [None];

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Stonemasonry)] = [None];

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] = [GetData(SkillConstants.Search, condition: "finding traps")] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker)] = 2;

                testCases[GetData(SkillConstants.DecipherScript] = [GetData(SkillConstants.UseMagicDevice, condition: "with scrolls")] = 2;

                testCases[GetData(SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister)] = 2;
                testCases[GetData(SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)] = 2;
                testCases[GetData(SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)] = 2;
                testCases[GetData(SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)] = 2;
                testCases[GetData(SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)] = 2;
                testCases[GetData(SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)] = 2;
                testCases[GetData(SkillConstants.Diplomacy] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet)] = 2;

                testCases[GetData(SkillConstants.DisableDevice] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Disguise] = [GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Act, "in costume")] = 2;

                testCases[GetData(SkillConstants.EscapeArtist] = [GetData(SkillConstants.UseRope, condition: "binding someone")] = 2;

                testCases[GetData(SkillConstants.Forgery] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.GatherInformation] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.HandleAnimal] = [GetData(SkillConstants.Diplomacy, condition: "wild empathy")] = 2;
                testCases[GetData(SkillConstants.HandleAnimal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer)] = 2;
                testCases[GetData(SkillConstants.HandleAnimal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer)] = 2;
                testCases[GetData(SkillConstants.HandleAnimal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer)] = 2;
                testCases[GetData(SkillConstants.HandleAnimal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd)] = 2;
                testCases[GetData(SkillConstants.HandleAnimal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)] = 2;
                testCases[GetData(SkillConstants.HandleAnimal] = [GetData(SkillConstants.Ride)] = 2;

                testCases[GetData(SkillConstants.Heal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer)] = 2;
                testCases[GetData(SkillConstants.Heal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse)] = 2;
                testCases[GetData(SkillConstants.Heal] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife)] = 2;

                testCases[GetData(SkillConstants.Hide] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Intimidate] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)] = 2;

                testCases[GetData(SkillConstants.Jump] = [GetData(SkillConstants.Tumble)] = 2;

                testCases[GetData(SkillConstants.Knowledge] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)] = 2;
                testCases[GetData(SkillConstants.Knowledge] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage)] = 2;
                testCases[GetData(SkillConstants.Knowledge] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana)] = [GetData(SkillConstants.Spellcraft)] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] = [GetData(SkillConstants.Search, condition: "find secret doors or compartments")] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)] = [GetData(SkillConstants.Survival, condition: "underground")] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)] = [GetData(SkillConstants.Survival, condition: "keep from getting lost or avoid natural hazards")] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History)] = [GetData(SkillConstants.Knowledge, "bardic")] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = [GetData(SkillConstants.GatherInformation, 2)];
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = [GetData(SkillConstants.Survival, condition: "in aboveground natural environments")] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = [GetData(SkillConstants.Diplomacy, 2)];
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid)] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Religion)] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes)] = [GetData(SkillConstants.Survival, condition: "on other planes")] = 2;

                testCases[GetData(SkillConstants.Listen] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.MoveSilently] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.OpenLock] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)] = 2;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Act)] = [GetData(SkillConstants.Disguise, condition: "impersonating someone else")] = 2;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Comedy)] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Dance)] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.KeyboardInstruments)] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Oratory)] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.PercussionInstruments)] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Sing)] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.StringInstruments)] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.WindInstruments)] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Profession] = [GetData(None)] = 0;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)] = [GetData(SkillConstants.Diplomacy, 2)];
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)] = [GetData(SkillConstants.Knowledge)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer)] = [GetData(SkillConstants.HandleAnimal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer)] = [GetData(SkillConstants.HandleAnimal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser)] = [GetData(SkillConstants.Appraise)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister)] = [GetData(SkillConstants.Diplomacy, 2)];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)] = [GetData(SkillConstants.Diplomacy, 2)];
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)] = [GetData(SkillConstants.Diplomacy, 2)];
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Clerk)] = [None];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coiffeur)] = [None];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cook)] = [None];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)] = [GetData(SkillConstants.Craft)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)] = [GetData(SkillConstants.Bluff, 2)];
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)] = [GetData(SkillConstants.Survival, 2)];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)] = [GetData(SkillConstants.Perform)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer)] = [GetData(SkillConstants.HandleAnimal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)] = [GetData(SkillConstants.Diplomacy, 2)];
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)] = [GetData(SkillConstants.Diplomacy, 2)];
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer)] = [GetData(SkillConstants.Heal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = [GetData(SkillConstants.Survival, 2)];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter)] = [None];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Laborer)] = [None];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Launderer)] = [None];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse)] = [GetData(SkillConstants.Heal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)] = [GetData(SkillConstants.Diplomacy, 2)];
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)] = [GetData(SkillConstants.SenseMotive)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife)] = [GetData(SkillConstants.Heal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator)] = [GetData(SkillConstants.Survival, 2)];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Nursemaid)] = [None];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)] = [GetData(SkillConstants.Ride)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Polisher)] = [None];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Porter)] = [None];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage)] = [GetData(SkillConstants.Knowledge)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember)] = [GetData(SkillConstants.Swim)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)] = [GetData(SkillConstants.Intimidate)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)] = [GetData(SkillConstants.Swim)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Scribe)] = [None];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd)] = [GetData(SkillConstants.HandleAnimal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer)] = [GetData(SkillConstants.Bluff, 2)];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)] = [GetData(SkillConstants.Knowledge)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)] = [GetData(SkillConstants.HandleAnimal)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)] = [GetData(SkillConstants.Ride)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)] = [GetData(SkillConstants.Appraise)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)] = [GetData(SkillConstants.SenseMotive)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = [GetData(SkillConstants.Survival, 2)];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet)] = [GetData(SkillConstants.Diplomacy, 2)];

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright)] = [GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;

                testCases[GetData(SkillConstants.Ride] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)] = 2;
                testCases[GetData(SkillConstants.Ride] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)] = 2;

                testCases[GetData(SkillConstants.Search] = [GetData(SkillConstants.Survival, condition: "following tracks")] = 2;

                testCases[GetData(SkillConstants.SenseMotive] = [GetData(SkillConstants.Diplomacy, 2)];
                testCases[GetData(SkillConstants.SenseMotive] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)] = 2;
                testCases[GetData(SkillConstants.SenseMotive] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)] = 2;

                testCases[GetData(SkillConstants.SleightOfHand] = [None];

                testCases[GetData(SkillConstants.Spellcraft] = [GetData(SkillConstants.UseMagicDevice, condition: "with scrolls")] = 2;

                testCases[GetData(SkillConstants.Spot] = [None];

                testCases[GetData(SkillConstants.Survival] = [GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;
                testCases[GetData(SkillConstants.Survival] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)] = 2;
                testCases[GetData(SkillConstants.Survival] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = 2;
                testCases[GetData(SkillConstants.Survival] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator)] = 2;
                testCases[GetData(SkillConstants.Survival] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = 2;

                testCases[GetData(SkillConstants.Swim] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember)] = 2;
                testCases[GetData(SkillConstants.Swim] = [GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)] = 2;

                testCases[GetData(SkillConstants.Tumble] = [GetData(SkillConstants.Balance, 2)];
                testCases[GetData(SkillConstants.Tumble] = [GetData(SkillConstants.Jump)] = 2;

                testCases[GetData(SkillConstants.UseMagicDevice] = [GetData(SkillConstants.Spellcraft, condition: "decipher scrolls")] = 2;

                testCases[GetData(SkillConstants.UseRope] = [GetData(SkillConstants.Climb, condition: "with rope")] = 2;
                testCases[GetData(SkillConstants.UseRope] = [GetData(SkillConstants.EscapeArtist, condition: "escaping rope bonds")] = 2;

                return TestDataHelper.EnumerateTestCases(testCases);
            }
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
