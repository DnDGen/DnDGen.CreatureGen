using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Skills
{
    public class SkillBonusesTestData
    {
        public const string None = "NONE";

        public static IEnumerable Creatures
        {
            get
            {
                var testCases = new Dictionary<string, Dictionary<string, int>>();
                var creatures = CreatureConstants.GetAll();

                foreach (var creature in creatures)
                {
                    testCases[creature] = new Dictionary<string, int>();
                }

                testCases[CreatureConstants.Aasimar][SkillConstants.Listen] = 2;
                testCases[CreatureConstants.Aasimar][SkillConstants.Spot] = 2;

                testCases[CreatureConstants.Aboleth][None] = 0;

                testCases[CreatureConstants.Achaierai][None] = 0;

                testCases[CreatureConstants.Allip][None] = 0;

                testCases[CreatureConstants.Androsphinx][None] = 0;

                testCases[CreatureConstants.Angel_AstralDeva][None] = 0;

                testCases[CreatureConstants.Angel_Planetar][None] = 0;

                testCases[CreatureConstants.Angel_Solar][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Colossal][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Colossal_Flexible][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Colossal_Sheetlike][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Colossal_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Gargantuan][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Flexible][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Gargantuan_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Huge][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Huge_Flexible][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Huge_Sheetlike][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Huge_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Large][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Large_Flexible][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Large_Sheetlike][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Large_TwoLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Large_Wheels_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Large_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Medium][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Medium_Flexible][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Medium_Sheetlike][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Medium_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Small][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Small_Flexible][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Small_Sheetlike][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Small_TwoLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Small_Wheels_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Small_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Tiny][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Tiny_Flexible][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Tiny_Sheetlike][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden][None] = 0;
                testCases[CreatureConstants.AnimatedObject_Tiny_Wooden][None] = 0;

                testCases[CreatureConstants.Ankheg][None] = 0;

                testCases[CreatureConstants.Annis][None] = 0;

                testCases[CreatureConstants.Ant_Giant_Queen][SkillConstants.Climb] = 8;
                testCases[CreatureConstants.Ant_Giant_Queen][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Ant_Giant_Queen][GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Ant_Giant_Soldier][SkillConstants.Climb] = 8;
                testCases[CreatureConstants.Ant_Giant_Soldier][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Ant_Giant_Soldier][GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Ant_Giant_Worker][SkillConstants.Climb] = 8;
                testCases[CreatureConstants.Ant_Giant_Worker][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Ant_Giant_Worker][GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Ape][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Ape][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Ape_Dire][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Ape_Dire][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Aranea][GetData(SkillConstants.Jump)] = 2;
                testCases[CreatureConstants.Aranea][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Aranea][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Aranea][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Aranea][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Arrowhawk_Adult][None] = 0;

                testCases[CreatureConstants.Arrowhawk_Elder][None] = 0;

                testCases[CreatureConstants.Arrowhawk_Juvenile][None] = 0;

                testCases[CreatureConstants.AssassinVine][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.AssassinVine][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Athach][None] = 0;

                testCases[CreatureConstants.Avoral][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Azer][None] = 0;

                testCases[CreatureConstants.Babau][GetData(SkillConstants.Hide)] = 8;
                testCases[CreatureConstants.Babau][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Babau][GetData(SkillConstants.MoveSilently)] = 8;
                testCases[CreatureConstants.Babau][GetData(SkillConstants.Search)] = 8;

                testCases[CreatureConstants.Baboon][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Baboon][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Badger][GetData(SkillConstants.EscapeArtist)] = 4;

                testCases[CreatureConstants.Badger_Dire][None] = 0;

                testCases[CreatureConstants.Balor][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Balor][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.BarbedDevil_Hamatula][None] = 0;

                testCases[CreatureConstants.Barghest][GetData(SkillConstants.Hide, condition: "in wolf form")] = 4;

                testCases[CreatureConstants.Barghest_Greater][GetData(SkillConstants.Hide, condition: "in wolf form")] = 4;

                testCases[CreatureConstants.Basilisk][GetData(SkillConstants.Hide, condition: "in natural settings")] = 4;

                testCases[CreatureConstants.Basilisk_Greater][GetData(SkillConstants.Hide, condition: "in natural settings")] = 4;

                testCases[CreatureConstants.Bat][GetData(SkillConstants.Listen, condition: "while able to use blindsense")] = 4;
                testCases[CreatureConstants.Bat][GetData(SkillConstants.Spot, condition: "while able to use blindsense")] = 4;

                testCases[CreatureConstants.Bat_Dire][GetData(SkillConstants.Listen, condition: "while able to use blindsense")] = 4;
                testCases[CreatureConstants.Bat_Dire][GetData(SkillConstants.Spot, condition: "while able to use blindsense")] = 4;

                testCases[CreatureConstants.Bat_Swarm][GetData(SkillConstants.Listen, condition: "while able to use blindsense")] = 4;
                testCases[CreatureConstants.Bat_Swarm][GetData(SkillConstants.Spot, condition: "while able to use blindsense")] = 4;

                testCases[CreatureConstants.Bear_Black][GetData(SkillConstants.Swim)] = 4;

                testCases[CreatureConstants.Bear_Brown][GetData(SkillConstants.Swim)] = 4;

                testCases[CreatureConstants.Bear_Dire][None] = 0;

                testCases[CreatureConstants.Bear_Polar][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Bear_Polar][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Bear_Polar][GetData(SkillConstants.Hide, condition: "in snowy settings")] = 12;

                testCases[CreatureConstants.BeardedDevil_Barbazu][None] = 0;

                testCases[CreatureConstants.Bebilith][GetData(SkillConstants.Hide)] = 8;

                testCases[CreatureConstants.Bee_Giant][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Bee_Giant][GetData(SkillConstants.Survival, condition: "To orient itself")] = 4;

                testCases[CreatureConstants.Behir][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Behir][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Beholder][None] = 0;

                testCases[CreatureConstants.Beholder_Gauth][None] = 0;

                testCases[CreatureConstants.Belker][GetData(SkillConstants.MoveSilently)] = 4;

                testCases[CreatureConstants.Bison][None] = 0;

                testCases[CreatureConstants.BlackPudding][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.BlackPudding][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.BlackPudding_Elder][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.BlackPudding_Elder][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.BlinkDog][None] = 0;

                testCases[CreatureConstants.Boar][None] = 0;

                testCases[CreatureConstants.Boar_Dire][None] = 0;

                testCases[CreatureConstants.Bodak][None] = 0;

                testCases[CreatureConstants.BombardierBeetle_Giant][None] = 0;

                testCases[CreatureConstants.BoneDevil_Osyluth][None] = 0;

                testCases[CreatureConstants.Bralani][None] = 0;

                testCases[CreatureConstants.Bugbear][GetData(SkillConstants.MoveSilently)] = 4;

                testCases[CreatureConstants.Bulette][None] = 0;

                testCases[CreatureConstants.Camel_Bactrian][None] = 0;

                testCases[CreatureConstants.Camel_Dromedary][None] = 0;

                testCases[CreatureConstants.CarrionCrawler][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.CarrionCrawler][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Cat][GetData(SkillConstants.Climb)] = 4;
                testCases[CreatureConstants.Cat][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Cat][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Cat][GetData(SkillConstants.Jump)] = 8;
                testCases[CreatureConstants.Cat][GetData(SkillConstants.Balance)] = 8;
                testCases[CreatureConstants.Cat][GetData(SkillConstants.Hide, condition: "in areas of tall grass or heavy undergrowth")] = 4;

                testCases[CreatureConstants.Centaur][None] = 0;

                testCases[CreatureConstants.Centipede_Monstrous_Colossal][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][GetData(SkillConstants.Hide)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Colossal][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][GetData(SkillConstants.Hide)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Centipede_Monstrous_Huge][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Centipede_Monstrous_Huge][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Huge][GetData(SkillConstants.Hide)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Huge][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Centipede_Monstrous_Large][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Centipede_Monstrous_Large][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Large][GetData(SkillConstants.Hide)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Large][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Centipede_Monstrous_Medium][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Centipede_Monstrous_Medium][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Medium][GetData(SkillConstants.Hide)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Medium][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Centipede_Monstrous_Small][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Centipede_Monstrous_Small][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Small][GetData(SkillConstants.Hide)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Small][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Centipede_Monstrous_Tiny][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][GetData(SkillConstants.Hide)] = 8;
                testCases[CreatureConstants.Centipede_Monstrous_Tiny][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Centipede_Swarm][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Centipede_Swarm][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Centipede_Swarm][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.ChainDevil_Kyton][GetData(SkillConstants.Craft, condition: "involving metalwork")] = 8;

                testCases[CreatureConstants.ChaosBeast][None] = 0;

                testCases[CreatureConstants.Cheetah][None] = 0;

                testCases[CreatureConstants.Chimera_Black][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Chimera_Black][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Chimera_Black][GetData(SkillConstants.Hide, condition: "in areas of scrubland or brush")] = 4;

                testCases[CreatureConstants.Chimera_Blue][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Chimera_Blue][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Chimera_Blue][GetData(SkillConstants.Hide, condition: "in areas of scrubland or brush")] = 4;

                testCases[CreatureConstants.Chimera_Green][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Chimera_Green][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Chimera_Green][GetData(SkillConstants.Hide, condition: "in areas of scrubland or brush")] = 4;

                testCases[CreatureConstants.Chimera_Red][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Chimera_Red][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Chimera_Red][GetData(SkillConstants.Hide, condition: "in areas of scrubland or brush")] = 4;

                testCases[CreatureConstants.Chimera_White][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Chimera_White][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Chimera_White][GetData(SkillConstants.Hide, condition: "in areas of scrubland or brush")] = 4;

                testCases[CreatureConstants.Choker][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Choker][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Chuul][None] = 0;

                testCases[CreatureConstants.Cloaker][None] = 0;

                testCases[CreatureConstants.Cockatrice][None] = 0;

                testCases[CreatureConstants.Couatl][None] = 0;

                testCases[CreatureConstants.Criosphinx][None] = 0;

                testCases[CreatureConstants.Crocodile][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Crocodile][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Crocodile][GetData(SkillConstants.Hide, condition: "in water")] = 4;
                testCases[CreatureConstants.Crocodile][GetData(SkillConstants.Hide, condition: "laying in water with only its eyes and nostrils showing")] = 10;

                testCases[CreatureConstants.Crocodile_Giant][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Crocodile_Giant][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Crocodile_Giant][GetData(SkillConstants.Hide, condition: "in water")] = 4;
                testCases[CreatureConstants.Crocodile_Giant][GetData(SkillConstants.Hide, condition: "laying in water with only its eyes and nostrils showing")] = 10;

                testCases[CreatureConstants.Cryohydra_5Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Cryohydra_5Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Cryohydra_5Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Cryohydra_5Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Cryohydra_6Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Cryohydra_6Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Cryohydra_6Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Cryohydra_6Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Cryohydra_7Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Cryohydra_7Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Cryohydra_7Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Cryohydra_7Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Cryohydra_8Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Cryohydra_8Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Cryohydra_8Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Cryohydra_8Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Cryohydra_9Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Cryohydra_9Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Cryohydra_9Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Cryohydra_9Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Cryohydra_10Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Cryohydra_10Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Cryohydra_10Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Cryohydra_10Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Cryohydra_11Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Cryohydra_11Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Cryohydra_11Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Cryohydra_11Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Cryohydra_12Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Cryohydra_12Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Cryohydra_12Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Cryohydra_12Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Darkmantle][GetData(SkillConstants.Spot, condition: "while able to use blindsight")] = 4;
                testCases[CreatureConstants.Darkmantle][GetData(SkillConstants.Listen, condition: "while able to use blindsight")] = 4;
                testCases[CreatureConstants.Darkmantle][GetData(SkillConstants.Hide)] = 4;

                testCases[CreatureConstants.Deinonychus][GetData(SkillConstants.Hide)] = 8;
                testCases[CreatureConstants.Deinonychus][GetData(SkillConstants.Jump)] = 8;
                testCases[CreatureConstants.Deinonychus][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Deinonychus][GetData(SkillConstants.Spot)] = 8;
                testCases[CreatureConstants.Deinonychus][GetData(SkillConstants.Survival)] = 8;

                testCases[CreatureConstants.Delver][None] = 0;

                testCases[CreatureConstants.Derro][None] = 0;

                testCases[CreatureConstants.Derro_Sane][None] = 0;

                testCases[CreatureConstants.Destrachan][GetData(SkillConstants.Listen)] = 10;

                testCases[CreatureConstants.Devourer][None] = 0;

                testCases[CreatureConstants.Digester][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Digester][GetData(SkillConstants.Jump)] = 4;

                testCases[CreatureConstants.DisplacerBeast][GetData(SkillConstants.Hide)] = 8;

                testCases[CreatureConstants.DisplacerBeast_PackLord][GetData(SkillConstants.Hide)] = 8;

                testCases[CreatureConstants.Djinni][None] = 0;

                testCases[CreatureConstants.Djinni_Noble][None] = 0;

                testCases[CreatureConstants.Dog][GetData(SkillConstants.Jump)] = 4;
                testCases[CreatureConstants.Dog][GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Dog_Riding][GetData(SkillConstants.Jump)] = 4;
                testCases[CreatureConstants.Dog_Riding][GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Donkey][GetData(SkillConstants.Balance)] = 2;

                testCases[CreatureConstants.Doppelganger][GetData(SkillConstants.Bluff)] = 4;
                testCases[CreatureConstants.Doppelganger][GetData(SkillConstants.Disguise)] = 4;
                testCases[CreatureConstants.Doppelganger][GetData(SkillConstants.Bluff, condition: "when reading an opponent's mind")] = 4;
                testCases[CreatureConstants.Doppelganger][GetData(SkillConstants.Disguise, condition: "when reading an opponent's mind")] = 4;
                testCases[CreatureConstants.Doppelganger][GetData(SkillConstants.Disguise, condition: "when using Change Shape")] = 10;

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

                testCases[CreatureConstants.DragonTurtle][GetData(SkillConstants.Hide, condition: "when submerged")] = 8;

                testCases[CreatureConstants.Dragonne][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Dragonne][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Dretch][None] = 0;

                testCases[CreatureConstants.Drider][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Drider][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Drider][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Drider][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Dryad][GetData(SkillConstants.Diplomacy, condition: "when using Wild Empathy")] = 6;

                testCases[CreatureConstants.Dwarf_Deep][None] = 0;

                testCases[CreatureConstants.Dwarf_Duergar][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Dwarf_Duergar][GetData(SkillConstants.Listen)] = 1;
                testCases[CreatureConstants.Dwarf_Duergar][GetData(SkillConstants.Spot)] = 1;

                testCases[CreatureConstants.Dwarf_Hill][None] = 0;

                testCases[CreatureConstants.Dwarf_Mountain][None] = 0;

                testCases[CreatureConstants.Eagle][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Eagle_Giant][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Efreeti][None] = 0;

                testCases[CreatureConstants.Elasmosaurus][GetData(SkillConstants.Hide, condition: "in water")] = 8;

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

                testCases[CreatureConstants.Elf_Aquatic][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Elf_Aquatic][GetData(SkillConstants.Search)] = 2;
                testCases[CreatureConstants.Elf_Aquatic][GetData(SkillConstants.Search, condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for")] = 0;
                testCases[CreatureConstants.Elf_Aquatic][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.Elf_Drow][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Elf_Drow][GetData(SkillConstants.Search)] = 2;
                testCases[CreatureConstants.Elf_Drow][GetData(SkillConstants.Search, condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for")] = 0;
                testCases[CreatureConstants.Elf_Drow][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.Elf_Gray][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Elf_Gray][GetData(SkillConstants.Search)] = 2;
                testCases[CreatureConstants.Elf_Gray][GetData(SkillConstants.Search, condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for")] = 0;
                testCases[CreatureConstants.Elf_Gray][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.Elf_Half][GetData(SkillConstants.Diplomacy)] = 2;
                testCases[CreatureConstants.Elf_Half][GetData(SkillConstants.GatherInformation)] = 2;
                testCases[CreatureConstants.Elf_Half][GetData(SkillConstants.Listen)] = 1;
                testCases[CreatureConstants.Elf_Half][GetData(SkillConstants.Search)] = 1;
                testCases[CreatureConstants.Elf_Half][GetData(SkillConstants.Spot)] = 1;

                testCases[CreatureConstants.Elf_High][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Elf_High][GetData(SkillConstants.Search)] = 2;
                testCases[CreatureConstants.Elf_High][GetData(SkillConstants.Search, condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for")] = 0;
                testCases[CreatureConstants.Elf_High][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.Elf_Wild][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Elf_Wild][GetData(SkillConstants.Search)] = 2;
                testCases[CreatureConstants.Elf_Wild][GetData(SkillConstants.Search, condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for")] = 0;
                testCases[CreatureConstants.Elf_Wild][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.Elf_Wood][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Elf_Wood][GetData(SkillConstants.Search)] = 2;
                testCases[CreatureConstants.Elf_Wood][GetData(SkillConstants.Search, condition: "passing within 5 feet of a secret or concealed door allows a Search check to notice it as if the door was being actively looked for")] = 0;
                testCases[CreatureConstants.Elf_Wood][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.Erinyes][None] = 0;

                testCases[CreatureConstants.EtherealFilcher][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.EtherealFilcher][GetData(SkillConstants.SleightOfHand)] = 8;
                testCases[CreatureConstants.EtherealFilcher][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.EtherealMarauder][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.EtherealMarauder][GetData(SkillConstants.MoveSilently)] = 2;
                testCases[CreatureConstants.EtherealMarauder][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.Ettercap][GetData(SkillConstants.Craft, focus: SkillConstants.Foci.Craft.Trapmaking)] = 4;
                testCases[CreatureConstants.Ettercap][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Ettercap][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Ettercap][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Ettercap][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Ettin][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Ettin][GetData(SkillConstants.Search)] = 2;
                testCases[CreatureConstants.Ettin][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.FireBeetle_Giant][None] = 0;

                testCases[CreatureConstants.FormianMyrmarch][None] = 0;

                testCases[CreatureConstants.FormianQueen][None] = 0;

                testCases[CreatureConstants.FormianTaskmaster][None] = 0;

                testCases[CreatureConstants.FormianWarrior][None] = 0;

                testCases[CreatureConstants.FormianWorker][None] = 0;

                testCases[CreatureConstants.FrostWorm][GetData(SkillConstants.Hide, condition: "on Cold Plains")] = 10;

                testCases[CreatureConstants.Gargoyle][GetData(SkillConstants.Hide)] = 2;
                testCases[CreatureConstants.Gargoyle][GetData(SkillConstants.Hide, condition: "concealed against a background of stone")] = 8;
                testCases[CreatureConstants.Gargoyle][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Gargoyle][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.Gargoyle_Kapoacinth][GetData(SkillConstants.Hide)] = 2;
                testCases[CreatureConstants.Gargoyle_Kapoacinth][GetData(SkillConstants.Hide, condition: "concealed against a background of stone")] = 8;
                testCases[CreatureConstants.Gargoyle_Kapoacinth][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Gargoyle_Kapoacinth][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.GelatinousCube][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.GelatinousCube][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Ghaele][None] = 0;

                testCases[CreatureConstants.Ghoul][None] = 0;

                testCases[CreatureConstants.Ghoul_Ghast][None] = 0;

                testCases[CreatureConstants.Ghoul_Lacedon][None] = 0;

                testCases[CreatureConstants.Giant_Cloud][None] = 0;

                testCases[CreatureConstants.Giant_Fire][None] = 0;

                testCases[CreatureConstants.Giant_Frost][None] = 0;

                testCases[CreatureConstants.Giant_Hill][None] = 0;

                testCases[CreatureConstants.Giant_Stone][GetData(SkillConstants.Hide, condition: "in rocky terrain")] = 8;

                testCases[CreatureConstants.Giant_Stone_Elder][GetData(SkillConstants.Hide, condition: "in rocky terrain")] = 8;

                testCases[CreatureConstants.Giant_Storm][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Giant_Storm][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.GibberingMouther][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.GibberingMouther][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.GibberingMouther][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Girallon][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Girallon][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Githyanki][None] = 0;

                testCases[CreatureConstants.Githzerai][None] = 0;

                testCases[CreatureConstants.Glabrezu][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Glabrezu][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Gnoll][None] = 0;

                testCases[CreatureConstants.Gnome_Forest][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Gnome_Forest][GetData(SkillConstants.Hide, condition: "in a wooded area")] = 8;

                testCases[CreatureConstants.Gnome_Rock][None] = 0;

                testCases[CreatureConstants.Gnome_Svirfneblin][GetData(SkillConstants.Hide)] = 2;
                testCases[CreatureConstants.Gnome_Svirfneblin][GetData(SkillConstants.Hide, condition: "underground")] = 2;

                testCases[CreatureConstants.Goblin][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Goblin][GetData(SkillConstants.Ride)] = 4;

                testCases[CreatureConstants.Golem_Clay][None] = 0;

                testCases[CreatureConstants.Golem_Flesh][None] = 0;

                testCases[CreatureConstants.Golem_Iron][None] = 0;

                testCases[CreatureConstants.Golem_Stone][None] = 0;

                testCases[CreatureConstants.Golem_Stone_Greater][None] = 0;

                testCases[CreatureConstants.Gorgon][None] = 0;

                testCases[CreatureConstants.GrayOoze][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.GrayOoze][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.GrayRender][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.GreenHag][None] = 0;

                testCases[CreatureConstants.Grick][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Grick][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Grick][GetData(SkillConstants.Hide, condition: "in natural, rocky areas")] = 8;

                testCases[CreatureConstants.Griffon][GetData(SkillConstants.Jump)] = 4;
                testCases[CreatureConstants.Griffon][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Grig][GetData(SkillConstants.Jump)] = 8;
                testCases[CreatureConstants.Grig][GetData(SkillConstants.MoveSilently, condition: "in a forest setting")] = 5;

                testCases[CreatureConstants.Grig_WithFiddle][GetData(SkillConstants.Jump)] = 8;
                testCases[CreatureConstants.Grig_WithFiddle][GetData(SkillConstants.MoveSilently, condition: "in a forest setting")] = 5;

                testCases[CreatureConstants.Grimlock][GetData(SkillConstants.Hide, condition: "in mountains or underground")] = 10;

                testCases[CreatureConstants.Gynosphinx][None] = 0;

                testCases[CreatureConstants.Halfling_Deep][None] = 0;

                testCases[CreatureConstants.Halfling_Lightfoot][GetData(SkillConstants.Climb)] = 2;
                testCases[CreatureConstants.Halfling_Lightfoot][GetData(SkillConstants.Jump)] = 2;
                testCases[CreatureConstants.Halfling_Lightfoot][GetData(SkillConstants.MoveSilently)] = 2;

                testCases[CreatureConstants.Halfling_Tallfellow][None] = 0;

                testCases[CreatureConstants.Harpy][GetData(SkillConstants.Bluff)] = 4;
                testCases[CreatureConstants.Harpy][GetData(SkillConstants.Listen)] = 4;

                testCases[CreatureConstants.Hawk][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Hellcat_Bezekira][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Hellcat_Bezekira][GetData(SkillConstants.MoveSilently)] = 4;

                testCases[CreatureConstants.Hellwasp_Swarm][None] = 0;

                testCases[CreatureConstants.HellHound][GetData(SkillConstants.Hide)] = 5;
                testCases[CreatureConstants.HellHound][GetData(SkillConstants.MoveSilently)] = 5;

                testCases[CreatureConstants.HellHound_NessianWarhound][GetData(SkillConstants.Hide)] = 5;
                testCases[CreatureConstants.HellHound_NessianWarhound][GetData(SkillConstants.MoveSilently)] = 5;

                testCases[CreatureConstants.Hezrou][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Hezrou][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Hieracosphinx][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Hippogriff][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Hobgoblin][GetData(SkillConstants.MoveSilently)] = 4;

                testCases[CreatureConstants.Horse_Heavy][None] = 0;

                testCases[CreatureConstants.Horse_Heavy_War][None] = 0;

                testCases[CreatureConstants.Horse_Light][None] = 0;

                testCases[CreatureConstants.Horse_Light_War][None] = 0;

                testCases[CreatureConstants.Howler][None] = 0;

                testCases[CreatureConstants.Hydra_5Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Hydra_5Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Hydra_5Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Hydra_5Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Hydra_6Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Hydra_6Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Hydra_6Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Hydra_6Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Hydra_7Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Hydra_7Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Hydra_7Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Hydra_7Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Hydra_8Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Hydra_8Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Hydra_8Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Hydra_8Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Hydra_9Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Hydra_9Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Hydra_9Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Hydra_9Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Hydra_10Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Hydra_10Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Hydra_10Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Hydra_10Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Hydra_11Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Hydra_11Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Hydra_11Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Hydra_11Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Hydra_12Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Hydra_12Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Hydra_12Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Hydra_12Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Homunculus][None] = 0;

                testCases[CreatureConstants.HornedDevil_Cornugon][None] = 0;

                testCases[CreatureConstants.HoundArchon][GetData(SkillConstants.Hide, condition: "in canine form")] = 4;
                testCases[CreatureConstants.HoundArchon][GetData(SkillConstants.Survival, condition: "in canine form")] = 4;

                testCases[CreatureConstants.Human][None] = 0;

                testCases[CreatureConstants.Hyena][GetData(SkillConstants.Hide, condition: "in tall grass or heavy undergrowth")] = 4;

                testCases[CreatureConstants.IceDevil_Gelugon][None] = 0;

                testCases[CreatureConstants.Imp][None] = 0;

                testCases[CreatureConstants.InvisibleStalker][None] = 0;

                testCases[CreatureConstants.Janni][None] = 0;

                testCases[CreatureConstants.Kobold][GetData(SkillConstants.Craft, focus: SkillConstants.Foci.Craft.Trapmaking)] = 2;
                testCases[CreatureConstants.Kobold][GetData(SkillConstants.Profession, focus: SkillConstants.Foci.Profession.Miner)] = 2;
                testCases[CreatureConstants.Kobold][GetData(SkillConstants.Search)] = 2;

                testCases[CreatureConstants.Kolyarut][GetData(SkillConstants.Disguise)] = 4;
                testCases[CreatureConstants.Kolyarut][GetData(SkillConstants.GatherInformation)] = 4;
                testCases[CreatureConstants.Kolyarut][GetData(SkillConstants.SenseMotive)] = 4;

                testCases[CreatureConstants.Kraken][None] = 0;

                testCases[CreatureConstants.Krenshar][GetData(SkillConstants.Jump)] = 4;
                testCases[CreatureConstants.Krenshar][GetData(SkillConstants.MoveSilently)] = 4;

                testCases[CreatureConstants.KuoToa][GetData(SkillConstants.EscapeArtist)] = 8;
                testCases[CreatureConstants.KuoToa][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.KuoToa][GetData(SkillConstants.Search)] = 4;

                testCases[CreatureConstants.Lamia][GetData(SkillConstants.Bluff)] = 4;
                testCases[CreatureConstants.Lamia][GetData(SkillConstants.Hide)] = 4;

                testCases[CreatureConstants.Lammasu][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.LanternArchon][None] = 0;

                testCases[CreatureConstants.Lemure][None] = 0;

                testCases[CreatureConstants.Leonal][GetData(SkillConstants.Balance)] = 4;
                testCases[CreatureConstants.Leonal][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Leonal][GetData(SkillConstants.MoveSilently)] = 4;

                testCases[CreatureConstants.Leopard][GetData(SkillConstants.Jump)] = 8;
                testCases[CreatureConstants.Leopard][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Leopard][GetData(SkillConstants.Hide, condition: "in areas of tall grass or heavy undergrowth")] = 4;
                testCases[CreatureConstants.Leopard][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Leopard][GetData(SkillConstants.Balance)] = 8;
                testCases[CreatureConstants.Leopard][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Leopard][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Lillend][GetData(SkillConstants.Survival)] = 4;

                testCases[CreatureConstants.Lion][GetData(SkillConstants.Balance)] = 4;
                testCases[CreatureConstants.Lion][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Lion][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Lion][GetData(SkillConstants.Hide, condition: "in tall grass or heavy undergrowth")] = 8;

                testCases[CreatureConstants.Lion_Dire][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Lion_Dire][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Lion_Dire][GetData(SkillConstants.Hide, condition: "in tall grass or heavy undergrowth")] = 4;

                testCases[CreatureConstants.Lizard][GetData(SkillConstants.Balance)] = 8;
                testCases[CreatureConstants.Lizard][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Lizard][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Lizard_Monitor][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Lizard_Monitor][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Lizard_Monitor][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Lizard_Monitor][GetData(SkillConstants.Hide, condition: "in forested or overgrown areas")] = 4;
                testCases[CreatureConstants.Lizard_Monitor][GetData(SkillConstants.MoveSilently)] = 4;

                testCases[CreatureConstants.Lizardfolk][GetData(SkillConstants.Jump)] = 4;
                testCases[CreatureConstants.Lizardfolk][GetData(SkillConstants.Swim)] = 4;
                testCases[CreatureConstants.Lizardfolk][GetData(SkillConstants.Balance)] = 4;

                testCases[CreatureConstants.Locathah][None] = 0;

                testCases[CreatureConstants.Locust_Swarm][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Locust_Swarm][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Magmin][None] = 0;

                testCases[CreatureConstants.MantaRay][None] = 0;

                testCases[CreatureConstants.Manticore][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Marilith][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Marilith][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Marut][GetData(SkillConstants.Concentration)] = 4;
                testCases[CreatureConstants.Marut][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Marut][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Medusa][None] = 0;

                testCases[CreatureConstants.Megaraptor][GetData(SkillConstants.Hide)] = 8;
                testCases[CreatureConstants.Megaraptor][GetData(SkillConstants.Jump)] = 8;
                testCases[CreatureConstants.Megaraptor][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Megaraptor][GetData(SkillConstants.Spot)] = 8;
                testCases[CreatureConstants.Megaraptor][GetData(SkillConstants.Survival)] = 8;

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

                testCases[CreatureConstants.Merfolk][None] = 0;

                testCases[CreatureConstants.Mimic][GetData(SkillConstants.Disguise)] = 8;

                testCases[CreatureConstants.MindFlayer][None] = 0;

                testCases[CreatureConstants.Minotaur][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Minotaur][GetData(SkillConstants.Search)] = 4;
                testCases[CreatureConstants.Minotaur][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Mohrg][None] = 0;

                testCases[CreatureConstants.Monkey][GetData(SkillConstants.Balance)] = 8;
                testCases[CreatureConstants.Monkey][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Monkey][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Mule][GetData(SkillConstants.Balance, condition: "To avoid slipping or falling")] = 2;

                testCases[CreatureConstants.Mummy][None] = 0;

                testCases[CreatureConstants.Naga_Dark][None] = 0;

                testCases[CreatureConstants.Naga_Guardian][None] = 0;

                testCases[CreatureConstants.Naga_Spirit][None] = 0;

                testCases[CreatureConstants.Naga_Water][None] = 0;

                testCases[CreatureConstants.Nalfeshnee][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Nalfeshnee][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.NightHag][None] = 0;

                testCases[CreatureConstants.Nightcrawler][None] = 0;

                testCases[CreatureConstants.Nightmare][None] = 0;

                testCases[CreatureConstants.Nightmare_Cauchemar][None] = 0;

                testCases[CreatureConstants.Nightwalker][GetData(SkillConstants.Hide, condition: "in a dark area")] = 8;

                testCases[CreatureConstants.Nightwing][GetData(SkillConstants.Hide, condition: "in a dark area or flying in a dark sky")] = 8;

                testCases[CreatureConstants.Nixie][GetData(SkillConstants.Hide, condition: "in water")] = 5;

                testCases[CreatureConstants.Nymph][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Nymph][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.OchreJelly][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.OchreJelly][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Octopus][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Octopus][GetData(SkillConstants.EscapeArtist)] = 10;

                testCases[CreatureConstants.Octopus_Giant][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Octopus_Giant][GetData(SkillConstants.EscapeArtist)] = 10;

                testCases[CreatureConstants.Ogre][None] = 0;

                testCases[CreatureConstants.Ogre_Merrow][None] = 0;

                testCases[CreatureConstants.OgreMage][None] = 0;

                testCases[CreatureConstants.Orc][None] = 0;

                testCases[CreatureConstants.Orc_Half][None] = 0;

                testCases[CreatureConstants.Otyugh][GetData(SkillConstants.Hide, condition: "in its lair")] = 8;

                testCases[CreatureConstants.Owl][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Owl][GetData(SkillConstants.MoveSilently)] = 14;
                testCases[CreatureConstants.Owl][GetData(SkillConstants.Spot, condition: "in areas of shadowy illumination")] = 8;

                testCases[CreatureConstants.Owl_Giant][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Owl_Giant][GetData(SkillConstants.MoveSilently, condition: "in flight")] = 8;
                testCases[CreatureConstants.Owl_Giant][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Owlbear][None] = 0;

                testCases[CreatureConstants.Pegasus][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Pegasus][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.PhantomFungus][GetData(SkillConstants.MoveSilently)] = 5;

                testCases[CreatureConstants.PhaseSpider][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.PhaseSpider][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Phasm][GetData(SkillConstants.Disguise, condition: "when using Shapechange")] = 10;

                testCases[CreatureConstants.PitFiend][None] = 0;

                testCases[CreatureConstants.Pixie][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Pixie][GetData(SkillConstants.Search)] = 2;
                testCases[CreatureConstants.Pixie][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.Pixie_WithIrresistibleDance][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Pixie_WithIrresistibleDance][GetData(SkillConstants.Search)] = 2;
                testCases[CreatureConstants.Pixie_WithIrresistibleDance][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.Pony][None] = 0;

                testCases[CreatureConstants.Pony_War][None] = 0;

                testCases[CreatureConstants.Porpoise][GetData(SkillConstants.Listen, condition: "while able to use blindsight")] = 4;
                testCases[CreatureConstants.Porpoise][GetData(SkillConstants.Spot, condition: "while able to use blindsight")] = 4;

                testCases[CreatureConstants.PrayingMantis_Giant][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.PrayingMantis_Giant][GetData(SkillConstants.Hide, condition: "surrounded by foliage")] = 8;
                testCases[CreatureConstants.PrayingMantis_Giant][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Pseudodragon][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Pseudodragon][GetData(SkillConstants.Hide, condition: "in forests or overgrown areas")] = 4;

                testCases[CreatureConstants.PurpleWorm][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.PurpleWorm][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Pyrohydra_5Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Pyrohydra_5Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Pyrohydra_5Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Pyrohydra_5Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Pyrohydra_6Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Pyrohydra_6Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Pyrohydra_6Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Pyrohydra_6Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Pyrohydra_7Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Pyrohydra_7Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Pyrohydra_7Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Pyrohydra_7Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Pyrohydra_8Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Pyrohydra_8Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Pyrohydra_8Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Pyrohydra_8Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Pyrohydra_9Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Pyrohydra_9Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Pyrohydra_9Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Pyrohydra_9Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Pyrohydra_10Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Pyrohydra_10Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Pyrohydra_10Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Pyrohydra_10Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Pyrohydra_11Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Pyrohydra_11Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Pyrohydra_11Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Pyrohydra_11Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Pyrohydra_12Heads][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Pyrohydra_12Heads][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Pyrohydra_12Heads][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Pyrohydra_12Heads][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Quasit][None] = 0;

                testCases[CreatureConstants.Rakshasa][GetData(SkillConstants.Bluff)] = 4;
                testCases[CreatureConstants.Rakshasa][GetData(SkillConstants.Bluff, condition: "when using Change Shape")] = 10;
                testCases[CreatureConstants.Rakshasa][GetData(SkillConstants.Bluff, condition: "when reading an opponent's mind")] = 4;
                testCases[CreatureConstants.Rakshasa][GetData(SkillConstants.Disguise)] = 4;
                testCases[CreatureConstants.Rakshasa][GetData(SkillConstants.Disguise, condition: "when reading an opponent's mind")] = 4;

                testCases[CreatureConstants.Rast][None] = 0;

                testCases[CreatureConstants.Rat][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Rat][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Rat][GetData(SkillConstants.Balance)] = 8;
                testCases[CreatureConstants.Rat][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Rat][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Rat][GetData(SkillConstants.Swim)] = 8;
                testCases[CreatureConstants.Rat][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Rat][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Rat_Dire][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Rat_Dire][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Rat_Dire][GetData(SkillConstants.Swim)] = 8;
                testCases[CreatureConstants.Rat_Dire][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Balance)] = 8;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Swim)] = 8;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Raven][None] = 0;

                testCases[CreatureConstants.Ravid][None] = 0;

                testCases[CreatureConstants.RazorBoar][None] = 0;

                testCases[CreatureConstants.Remorhaz][GetData(SkillConstants.Listen)] = 4;

                testCases[CreatureConstants.Retriever][None] = 0;

                testCases[CreatureConstants.Rhinoceras][None] = 0;

                testCases[CreatureConstants.Roc][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Roper][GetData(SkillConstants.Hide, condition: "in stony or icy areas")] = 8;

                testCases[CreatureConstants.RustMonster][None] = 0;

                testCases[CreatureConstants.Sahuagin][GetData(SkillConstants.Hide, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin][GetData(SkillConstants.Listen, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin][GetData(SkillConstants.Spot, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin][GetData(SkillConstants.Survival, condition: "within 50 miles of its home")] = 4;
                testCases[CreatureConstants.Sahuagin][GetData(SkillConstants.Profession, focus: SkillConstants.Foci.Profession.Hunter, condition: "within 50 miles of its home")] = 4;
                testCases[CreatureConstants.Sahuagin][GetData(SkillConstants.HandleAnimal, condition: "when working with sharks")] = 4;

                testCases[CreatureConstants.Sahuagin_Malenti][GetData(SkillConstants.Hide, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin_Malenti][GetData(SkillConstants.Listen, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin_Malenti][GetData(SkillConstants.Spot, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin_Malenti][GetData(SkillConstants.Survival, condition: "within 50 miles of its home")] = 4;
                testCases[CreatureConstants.Sahuagin_Malenti][GetData(SkillConstants.Profession, focus: SkillConstants.Foci.Profession.Hunter, condition: "within 50 miles of its home")] = 4;
                testCases[CreatureConstants.Sahuagin_Malenti][GetData(SkillConstants.HandleAnimal, condition: "when working with sharks")] = 4;

                testCases[CreatureConstants.Sahuagin_Mutant][GetData(SkillConstants.Hide, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin_Mutant][GetData(SkillConstants.Listen, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin_Mutant][GetData(SkillConstants.Spot, condition: "underwater")] = 4;
                testCases[CreatureConstants.Sahuagin_Mutant][GetData(SkillConstants.Survival, condition: "within 50 miles of its home")] = 4;
                testCases[CreatureConstants.Sahuagin_Mutant][GetData(SkillConstants.Profession, focus: SkillConstants.Foci.Profession.Hunter, condition: "within 50 miles of its home")] = 4;
                testCases[CreatureConstants.Sahuagin_Mutant][GetData(SkillConstants.HandleAnimal, condition: "when working with sharks")] = 4;

                testCases[CreatureConstants.Salamander_Flamebrother][GetData(SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing))] = 4;

                testCases[CreatureConstants.Salamander_Average][GetData(SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing))] = 4;

                testCases[CreatureConstants.Salamander_Noble][GetData(SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing))] = 4;

                testCases[CreatureConstants.Satyr][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Satyr][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Satyr][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Satyr][GetData(SkillConstants.Perform)] = 4;
                testCases[CreatureConstants.Satyr][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Satyr_WithPipes][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Satyr_WithPipes][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Satyr_WithPipes][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Satyr_WithPipes][GetData(SkillConstants.Perform)] = 4;
                testCases[CreatureConstants.Satyr_WithPipes][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Scorpion_Monstrous_Colossal][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal][GetData(SkillConstants.Climb)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal][GetData(SkillConstants.Hide)] = 4;

                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan][GetData(SkillConstants.Climb)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan][GetData(SkillConstants.Hide)] = 4;

                testCases[CreatureConstants.Scorpion_Monstrous_Huge][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Huge][GetData(SkillConstants.Climb)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Huge][GetData(SkillConstants.Hide)] = 4;

                testCases[CreatureConstants.Scorpion_Monstrous_Large][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Large][GetData(SkillConstants.Climb)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Large][GetData(SkillConstants.Hide)] = 4;

                testCases[CreatureConstants.Scorpion_Monstrous_Medium][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Medium][GetData(SkillConstants.Climb)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Medium][GetData(SkillConstants.Hide)] = 4;

                testCases[CreatureConstants.Scorpion_Monstrous_Small][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Small][GetData(SkillConstants.Climb)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Small][GetData(SkillConstants.Hide)] = 4;

                testCases[CreatureConstants.Scorpion_Monstrous_Tiny][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny][GetData(SkillConstants.Climb)] = 4;
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny][GetData(SkillConstants.Hide)] = 4;

                testCases[CreatureConstants.Scorpionfolk][None] = 0;

                testCases[CreatureConstants.SeaCat][None] = 0;

                testCases[CreatureConstants.SeaHag][None] = 0;

                testCases[CreatureConstants.Shadow][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Shadow][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Shadow][GetData(SkillConstants.Search)] = 4;
                testCases[CreatureConstants.Shadow][GetData(SkillConstants.Hide, condition: "in areas of shadowy illumination")] = 4;
                testCases[CreatureConstants.Shadow][GetData(SkillConstants.Hide, condition: "in brightly lit areas")] = -4;

                testCases[CreatureConstants.Shadow_Greater][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Shadow_Greater][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Shadow_Greater][GetData(SkillConstants.Search)] = 4;
                testCases[CreatureConstants.Shadow_Greater][GetData(SkillConstants.Hide, condition: "in areas of shadowy illumination")] = 4;
                testCases[CreatureConstants.Shadow_Greater][GetData(SkillConstants.Hide, condition: "in brightly lit areas")] = -4;

                testCases[CreatureConstants.ShadowMastiff][GetData(SkillConstants.Survival, condition: "when tracking by scent")] = 4;

                testCases[CreatureConstants.ShamblingMound][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.ShamblingMound][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.ShamblingMound][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.ShamblingMound][GetData(SkillConstants.Hide, condition: "in a swampy or forested area")] = 8;

                testCases[CreatureConstants.Shark_Dire][None] = 0;

                testCases[CreatureConstants.Shark_Huge][None] = 0;

                testCases[CreatureConstants.Shark_Large][None] = 0;

                testCases[CreatureConstants.Shark_Medium][None] = 0;

                testCases[CreatureConstants.ShieldGuardian][None] = 0;

                testCases[CreatureConstants.ShockerLizard][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.ShockerLizard][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.ShockerLizard][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.ShockerLizard][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.ShockerLizard][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.ShockerLizard][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.ShockerLizard][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Shrieker][None] = 0;

                testCases[CreatureConstants.Skum][GetData(SkillConstants.Hide, condition: "underwater")] = 4;
                testCases[CreatureConstants.Skum][GetData(SkillConstants.Listen, condition: "underwater")] = 4;
                testCases[CreatureConstants.Skum][GetData(SkillConstants.Spot, condition: "underwater")] = 4;

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

                testCases[CreatureConstants.Spectre][None] = 0;

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small][GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny][GetData(SkillConstants.Jump)] = 10;
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large][GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small][GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small][GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][GetData(SkillConstants.Spot)] = 4;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][GetData(SkillConstants.Hide, condition: "when using their webs")] = 8;
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][GetData(SkillConstants.MoveSilently, condition: "when using their webs")] = 8;

                testCases[CreatureConstants.Spider_Swarm][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Swarm][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Swarm][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Swarm][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.SpiderEater][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.SpiderEater][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Squid][None] = 0;

                testCases[CreatureConstants.Squid_Giant][None] = 0;

                testCases[CreatureConstants.StagBeetle_Giant][None] = 0;

                testCases[CreatureConstants.Stirge][None] = 0;

                testCases[CreatureConstants.Succubus][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Succubus][GetData(SkillConstants.Spot)] = 8;
                testCases[CreatureConstants.Succubus][GetData(SkillConstants.Disguise, condition: "when using Change Shape")] = 10;

                testCases[CreatureConstants.Tarrasque][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Tarrasque][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Tendriculos][None] = 0;

                testCases[CreatureConstants.Thoqqua][None] = 0;

                testCases[CreatureConstants.Tiefling][GetData(SkillConstants.Bluff)] = 2;
                testCases[CreatureConstants.Tiefling][GetData(SkillConstants.Hide)] = 2;

                testCases[CreatureConstants.Tiger][GetData(SkillConstants.Balance)] = 4;
                testCases[CreatureConstants.Tiger][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Tiger][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Tiger][GetData(SkillConstants.Hide, condition: "in tall grass or heavy undergrowth")] = 4;

                testCases[CreatureConstants.Tiger_Dire][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Tiger_Dire][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Tiger_Dire][GetData(SkillConstants.Hide, condition: "in tall grass or heavy undergrowth")] = 4;

                testCases[CreatureConstants.Titan][None] = 0;

                testCases[CreatureConstants.Toad][GetData(SkillConstants.Hide)] = 4;

                testCases[CreatureConstants.Tojanida_Juvenile][None] = 0;

                testCases[CreatureConstants.Tojanida_Adult][None] = 0;

                testCases[CreatureConstants.Tojanida_Elder][None] = 0;

                testCases[CreatureConstants.Treant][None] = 0;

                testCases[CreatureConstants.Triceratops][None] = 0;

                testCases[CreatureConstants.Triton][None] = 0;

                testCases[CreatureConstants.Troglodyte][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Troglodyte][GetData(SkillConstants.Hide, condition: "in rocky or underground settings")] = 4;

                testCases[CreatureConstants.Troll][None] = 0;

                testCases[CreatureConstants.Troll_Scrag][None] = 0;

                testCases[CreatureConstants.TrumpetArchon][None] = 0;

                testCases[CreatureConstants.Tyrannosaurus][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Tyrannosaurus][GetData(SkillConstants.Spot)] = 2;

                testCases[CreatureConstants.UmberHulk][None] = 0;

                testCases[CreatureConstants.UmberHulk_TrulyHorrid][None] = 0;

                testCases[CreatureConstants.Unicorn][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Unicorn][GetData(SkillConstants.Survival, condition: "within the boundaries of their forest")] = 3;

                testCases[CreatureConstants.VampireSpawn][GetData(SkillConstants.Bluff)] = 4;
                testCases[CreatureConstants.VampireSpawn][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.VampireSpawn][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.VampireSpawn][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.VampireSpawn][GetData(SkillConstants.Search)] = 4;
                testCases[CreatureConstants.VampireSpawn][GetData(SkillConstants.SenseMotive)] = 4;
                testCases[CreatureConstants.VampireSpawn][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Vargouille][None] = 0;

                testCases[CreatureConstants.VioletFungus][None] = 0;

                testCases[CreatureConstants.Vrock][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Vrock][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Wasp_Giant][GetData(SkillConstants.Spot)] = 8;
                testCases[CreatureConstants.Wasp_Giant][GetData(SkillConstants.Survival, condition: "to orient itself")] = 4;

                testCases[CreatureConstants.Weasel][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Weasel][GetData(SkillConstants.Balance)] = 8;
                testCases[CreatureConstants.Weasel][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Weasel][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Weasel_Dire][None] = 0;

                testCases[CreatureConstants.Whale_Baleen][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Whale_Baleen][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Whale_Cachalot][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Whale_Cachalot][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Whale_Orca][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Whale_Orca][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Wight][GetData(SkillConstants.MoveSilently)] = 8;

                testCases[CreatureConstants.WillOWisp][None] = 0;

                testCases[CreatureConstants.WinterWolf][GetData(SkillConstants.Listen)] = 1;
                testCases[CreatureConstants.WinterWolf][GetData(SkillConstants.MoveSilently)] = 1;
                testCases[CreatureConstants.WinterWolf][GetData(SkillConstants.Spot)] = 1;
                testCases[CreatureConstants.WinterWolf][GetData(SkillConstants.Hide)] = 2;
                testCases[CreatureConstants.WinterWolf][GetData(SkillConstants.Hide, condition: "in areas of snow and ice")] = 5;
                testCases[CreatureConstants.WinterWolf][GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Wolf][GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Wolf_Dire][GetData(SkillConstants.Hide)] = 2;
                testCases[CreatureConstants.Wolf_Dire][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Wolf_Dire][GetData(SkillConstants.MoveSilently)] = 2;
                testCases[CreatureConstants.Wolf_Dire][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Wolf_Dire][GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Wolverine][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Wolverine][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Wolverine_Dire][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Wolverine_Dire][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Worg][GetData(SkillConstants.Listen)] = 1;
                testCases[CreatureConstants.Worg][GetData(SkillConstants.MoveSilently)] = 1;
                testCases[CreatureConstants.Worg][GetData(SkillConstants.Spot)] = 1;
                testCases[CreatureConstants.Worg][GetData(SkillConstants.Hide)] = 2;
                testCases[CreatureConstants.Worg][GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Wraith][None] = 0;

                testCases[CreatureConstants.Wraith_Dread][None] = 0;

                testCases[CreatureConstants.Wyvern][GetData(SkillConstants.Spot)] = 3;

                testCases[CreatureConstants.Xill][None] = 0;

                testCases[CreatureConstants.Xorn_Minor][None] = 0;

                testCases[CreatureConstants.Xorn_Average][None] = 0;

                testCases[CreatureConstants.Xorn_Elder][None] = 0;

                testCases[CreatureConstants.YethHound][GetData(SkillConstants.Survival, condition: "tracking by scent")] = 4;

                testCases[CreatureConstants.Yrthak][GetData(SkillConstants.Listen)] = 4;

                testCases[CreatureConstants.YuanTi_Pureblood][GetData(SkillConstants.Disguise, condition: "when impersonating a human")] = 5;

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms][GetData(SkillConstants.Hide, condition: "when using Chameleon Power")] = 10;

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead][GetData(SkillConstants.Hide, condition: "when using Chameleon Power")] = 10;

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail][GetData(SkillConstants.Hide, condition: "when using Chameleon Power")] = 10;

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][GetData(SkillConstants.Hide, condition: "when using Chameleon Power")] = 10;

                testCases[CreatureConstants.YuanTi_Abomination][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.YuanTi_Abomination][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.YuanTi_Abomination][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.YuanTi_Abomination][GetData(SkillConstants.Hide, condition: "when using Chameleon Power")] = 10;

                testCases[CreatureConstants.Zelekhut][GetData(SkillConstants.Search)] = 4;
                testCases[CreatureConstants.Zelekhut][GetData(SkillConstants.SenseMotive)] = 4;

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        public static IEnumerable Types
        {
            get
            {
                var testCases = new Dictionary<string, Dictionary<string, int>>();
                var types = CreatureConstants.Types.GetAll();

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

        public static IEnumerable Subtypes
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

                testCases[CreatureConstants.Types.Subtypes.Air][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Angel][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Aquatic][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Types.Subtypes.Aquatic][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Types.Subtypes.Archon][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Augmented][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Chaotic][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Cold][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Dwarf][GetData(SkillConstants.Appraise, condition: "for items related to stone or metal")] = 2;
                testCases[CreatureConstants.Types.Subtypes.Dwarf][GetData(SkillConstants.Craft, condition: "for items related to stone or metal")] = 2;

                testCases[CreatureConstants.Types.Subtypes.Earth][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Elf][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Evil][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Extraplanar][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Fire][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Gnome][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Types.Subtypes.Gnome][GetData(SkillConstants.Craft, focus: SkillConstants.Foci.Craft.Alchemy)] = 2;

                testCases[CreatureConstants.Types.Subtypes.Goblinoid][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Good][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Halfling][GetData(SkillConstants.Listen)] = 2;

                testCases[CreatureConstants.Types.Subtypes.Incorporeal][GetData(SkillConstants.MoveSilently, condition: "always succeeds, and cannot be heard by Listen checks unless it wants to be")] = 9000;

                testCases[CreatureConstants.Types.Subtypes.Lawful][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Native][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Reptilian][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Shapechanger][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Swarm][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Water][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Types.Subtypes.Water][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        public static IEnumerable SkillSynergies
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
                    testCases[skill] = new Dictionary<string, int>();
                }

                testCases[SkillConstants.Appraise][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser)] = 2;
                testCases[SkillConstants.Appraise][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)] = 2;

                testCases[SkillConstants.Balance][None] = 0;

                testCases[SkillConstants.Bluff][GetData(SkillConstants.Diplomacy)] = 2;
                testCases[SkillConstants.Bluff][GetData(SkillConstants.Disguise, condition: "acting")] = 2;
                testCases[SkillConstants.Bluff][GetData(SkillConstants.Intimidate)] = 2;
                testCases[SkillConstants.Bluff][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)] = 2;
                testCases[SkillConstants.Bluff][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer)] = 2;
                testCases[SkillConstants.Bluff][GetData(SkillConstants.SleightOfHand)] = 2;

                testCases[SkillConstants.Climb][None] = 0;

                testCases[SkillConstants.Concentration][None] = 0;

                testCases[SkillConstants.Craft][GetData(SkillConstants.Appraise, condition: "related to items made with your Craft skill")] = 2;
                testCases[SkillConstants.Craft][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Cloth)][None] = 0;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Glass)][None] = 0;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Leather)][None] = 0;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Stonemasonry)][None] = 0;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)][GetData(SkillConstants.Search, condition: "finding traps")] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner)] = 2;

                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker)] = 2;

                testCases[SkillConstants.DecipherScript][GetData(SkillConstants.UseMagicDevice, condition: "with scrolls")] = 2;

                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister)] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet)] = 2;

                testCases[SkillConstants.DisableDevice][GetData(None)] = 0;

                testCases[SkillConstants.Disguise][GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Act, "in costume")] = 2;

                testCases[SkillConstants.EscapeArtist][GetData(SkillConstants.UseRope, condition: "binding someone")] = 2;

                testCases[SkillConstants.Forgery][GetData(None)] = 0;

                testCases[SkillConstants.GatherInformation][GetData(None)] = 0;

                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Diplomacy, condition: "wild empathy")] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer)] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer)] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer)] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd)] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Ride)] = 2;

                testCases[SkillConstants.Heal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer)] = 2;
                testCases[SkillConstants.Heal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse)] = 2;
                testCases[SkillConstants.Heal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife)] = 2;

                testCases[SkillConstants.Hide][GetData(None)] = 0;

                testCases[SkillConstants.Intimidate][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)] = 2;

                testCases[SkillConstants.Jump][GetData(SkillConstants.Tumble)] = 2;

                testCases[SkillConstants.Knowledge][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)] = 2;
                testCases[SkillConstants.Knowledge][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage)] = 2;
                testCases[SkillConstants.Knowledge][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana)][GetData(SkillConstants.Spellcraft)] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)][GetData(SkillConstants.Search, condition: "find secret doors or compartments")] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)][GetData(SkillConstants.Survival, condition: "underground")] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)][GetData(SkillConstants.Survival, condition: "keep from getting lost or avoid natural hazards")] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History)][GetData(SkillConstants.Knowledge, "bardic")] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)][GetData(SkillConstants.GatherInformation)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Survival, condition: "in aboveground natural environments")] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)][GetData(SkillConstants.Diplomacy)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid)] = 2;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Religion)][GetData(None)] = 0;

                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes)][GetData(SkillConstants.Survival, condition: "on other planes")] = 2;

                testCases[SkillConstants.Listen][GetData(None)] = 0;

                testCases[SkillConstants.MoveSilently][GetData(None)] = 0;

                testCases[SkillConstants.OpenLock][GetData(None)] = 0;

                testCases[SkillConstants.Perform][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)] = 2;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Act)][GetData(SkillConstants.Disguise, condition: "impersonating someone else")] = 2;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Comedy)][GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Dance)][GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.KeyboardInstruments)][GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Oratory)][GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.PercussionInstruments)][GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.Sing)][GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.StringInstruments)][GetData(None)] = 0;

                testCases[GetData(SkillConstants.Perform, SkillConstants.Foci.Perform.WindInstruments)][GetData(None)] = 0;

                testCases[SkillConstants.Profession][GetData(None)] = 0;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)][GetData(SkillConstants.Diplomacy)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)][GetData(SkillConstants.Knowledge)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer)][GetData(SkillConstants.HandleAnimal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer)][GetData(SkillConstants.HandleAnimal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser)][GetData(SkillConstants.Appraise)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister)][GetData(SkillConstants.Diplomacy)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)][GetData(SkillConstants.Diplomacy)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)][GetData(SkillConstants.Diplomacy)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Clerk)][None] = 0;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coiffeur)][None] = 0;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cook)][None] = 0;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)][GetData(SkillConstants.Craft)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)][GetData(SkillConstants.Bluff)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)][GetData(SkillConstants.Survival)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)][GetData(SkillConstants.Perform)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer)][GetData(SkillConstants.HandleAnimal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)][GetData(SkillConstants.Diplomacy)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)][GetData(SkillConstants.Diplomacy)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer)][GetData(SkillConstants.Heal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)][GetData(SkillConstants.Survival)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Interpreter)][None] = 0;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Laborer)][None] = 0;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Launderer)][None] = 0;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse)][GetData(SkillConstants.Heal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)][GetData(SkillConstants.Diplomacy)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)][GetData(SkillConstants.SenseMotive)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife)][GetData(SkillConstants.Heal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator)][GetData(SkillConstants.Survival)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Nursemaid)][None] = 0;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)][GetData(SkillConstants.Ride)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Polisher)][None] = 0;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Porter)][None] = 0;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage)][GetData(SkillConstants.Knowledge)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember)][GetData(SkillConstants.Swim)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)][GetData(SkillConstants.Intimidate)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)][GetData(SkillConstants.Swim)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Scribe)][None] = 0;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd)][GetData(SkillConstants.HandleAnimal)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer)][GetData(SkillConstants.Bluff)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)][GetData(SkillConstants.Knowledge)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)][GetData(SkillConstants.HandleAnimal)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)][GetData(SkillConstants.Ride)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)][GetData(SkillConstants.Appraise)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)][GetData(SkillConstants.SenseMotive)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)][GetData(SkillConstants.Survival)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet)][GetData(SkillConstants.Diplomacy)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking)] = 2;

                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;

                testCases[SkillConstants.Ride][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)] = 2;
                testCases[SkillConstants.Ride][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)] = 2;

                testCases[SkillConstants.Search][GetData(SkillConstants.Survival, condition: "following tracks")] = 2;

                testCases[SkillConstants.SenseMotive][GetData(SkillConstants.Diplomacy)] = 2;
                testCases[SkillConstants.SenseMotive][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)] = 2;
                testCases[SkillConstants.SenseMotive][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)] = 2;

                testCases[SkillConstants.SleightOfHand][None] = 0;

                testCases[SkillConstants.Spellcraft][GetData(SkillConstants.UseMagicDevice, condition: "with scrolls")] = 2;

                testCases[SkillConstants.Spot][None] = 0;

                testCases[SkillConstants.Survival][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)] = 2;
                testCases[SkillConstants.Survival][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)] = 2;
                testCases[SkillConstants.Survival][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)] = 2;
                testCases[SkillConstants.Survival][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator)] = 2;
                testCases[SkillConstants.Survival][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)] = 2;

                testCases[SkillConstants.Swim][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember)] = 2;
                testCases[SkillConstants.Swim][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)] = 2;

                testCases[SkillConstants.Tumble][GetData(SkillConstants.Balance)] = 2;
                testCases[SkillConstants.Tumble][GetData(SkillConstants.Jump)] = 2;

                testCases[SkillConstants.UseMagicDevice][GetData(SkillConstants.Spellcraft, condition: "decipher scrolls")] = 2;

                testCases[SkillConstants.UseRope][GetData(SkillConstants.Climb, condition: "with rope")] = 2;
                testCases[SkillConstants.UseRope][GetData(SkillConstants.EscapeArtist, condition: "escaping rope bonds")] = 2;

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        private static class TestDataHelper
        {
            public static IEnumerable EnumerateTestCases(Dictionary<string, Dictionary<string, int>> testCases)
            {
                foreach (var testCase in testCases)
                {
                    yield return new TestCaseData(testCase.Key, testCase.Value);
                }
            }
        }

        private static string GetData(string skillName, string focus = "", string condition = "")
        {
            var data = SkillConstants.Build(skillName, focus);

            if (!string.IsNullOrEmpty(condition))
                data += BonusDataSelection.Divider + condition;

            return data;
        }
    }
}
