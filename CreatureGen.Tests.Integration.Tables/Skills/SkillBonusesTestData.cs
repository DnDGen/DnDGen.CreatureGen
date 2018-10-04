using CreatureGen.Creatures;
using CreatureGen.Skills;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Skills
{
    public class SkillBonusesTestData
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

                testCases[CreatureConstants.AnimatedObject_Gargantuan][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Huge][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Large][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Medium][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Small][None] = 0;

                testCases[CreatureConstants.AnimatedObject_Tiny][None] = 0;

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

                testCases[CreatureConstants.Aranea][GetData(SkillConstants.Jump)] = 2;
                testCases[CreatureConstants.Aranea][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Aranea][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Aranea][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Aranea][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.AssassinVine][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.AssassinVine][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Avoral][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Babau][GetData(SkillConstants.Hide)] = 8;
                testCases[CreatureConstants.Babau][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Babau][GetData(SkillConstants.MoveSilently)] = 8;
                testCases[CreatureConstants.Babau][GetData(SkillConstants.Search)] = 8;

                testCases[CreatureConstants.Baboon][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Baboon][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Badger][GetData(SkillConstants.EscapeArtist)] = 4;

                testCases[CreatureConstants.Balor][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Balor][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Barghest][GetData(SkillConstants.Hide, condition: "in wolf form")] = 4;

                testCases[CreatureConstants.Barghest_Greater][GetData(SkillConstants.Hide, condition: "in wolf form")] = 4;

                testCases[CreatureConstants.Basilisk][GetData(SkillConstants.Hide, condition: "in natural settings")] = 4;

                testCases[CreatureConstants.Basilisk_AbyssalGreater][GetData(SkillConstants.Hide, condition: "in natural settings")] = 4;

                testCases[CreatureConstants.Bat][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Bat][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Bat_Swarm][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Bat_Swarm][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Bear_Black][GetData(SkillConstants.Swim)] = 4;

                testCases[CreatureConstants.Bebilith][GetData(SkillConstants.Hide)] = 8;

                testCases[CreatureConstants.Behir][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Behir][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Belker][GetData(SkillConstants.MoveSilently)] = 4;

                testCases[CreatureConstants.Bison][None] = 0;

                testCases[CreatureConstants.BlackPudding][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.BlackPudding][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.BlackPudding_Elder][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.BlackPudding_Elder][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.BlinkDog][None] = 0;

                testCases[CreatureConstants.Boar][None] = 0;

                testCases[CreatureConstants.Bodak][None] = 0;

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

                testCases[CreatureConstants.Centipede_Swarm][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Centipede_Swarm][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Centipede_Swarm][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.ChainDevil_Kyton][GetData(SkillConstants.Craft, condition: "involving metalwork")] = 8;

                testCases[CreatureConstants.ChaosBeast][None] = 0;

                testCases[CreatureConstants.Cheetah][None] = 0;

                testCases[CreatureConstants.Chimera][GetData(SkillConstants.Spot)] = 2;
                testCases[CreatureConstants.Chimera][GetData(SkillConstants.Listen)] = 2;
                testCases[CreatureConstants.Chimera][GetData(SkillConstants.Hide, condition: "in areas of scrubland or brush")] = 4;

                testCases[CreatureConstants.Choker][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Choker][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Chuul][None] = 0;

                testCases[CreatureConstants.Criosphinx][None] = 0;

                testCases[CreatureConstants.Derro][None] = 0;

                testCases[CreatureConstants.Derro_Sane][None] = 0;

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

                testCases[CreatureConstants.Dretch][None] = 0;

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

                testCases[CreatureConstants.GelatinousCube][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.GelatinousCube][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Glabrezu][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Glabrezu][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.GrayOoze][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.GrayOoze][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.GreenHag][None] = 0;

                testCases[CreatureConstants.Gynosphinx][None] = 0;

                testCases[CreatureConstants.Hellcat_Bezekira][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Hellcat_Bezekira][GetData(SkillConstants.MoveSilently)] = 4;

                testCases[CreatureConstants.Hellwasp_Swarm][None] = 0;

                testCases[CreatureConstants.Hezrou][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Hezrou][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Hieracosphinx][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Homunculus][None] = 0;

                testCases[CreatureConstants.HornedDevil_Cornugon][None] = 0;

                testCases[CreatureConstants.Human][None] = 0;

                testCases[CreatureConstants.IceDevil_Gelugon][None] = 0;

                testCases[CreatureConstants.Imp][None] = 0;

                testCases[CreatureConstants.Lemure][None] = 0;

                testCases[CreatureConstants.Locust_Swarm][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Locust_Swarm][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Marilith][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Marilith][GetData(SkillConstants.Spot)] = 8;

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

                testCases[CreatureConstants.Nalfeshnee][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Nalfeshnee][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.OchreJelly][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.OchreJelly][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.PitFiend][None] = 0;

                testCases[CreatureConstants.Quasit][None] = 0;

                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.MoveSilently)] = 4;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Balance)] = 8;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Swim)] = 8;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Rat_Swarm][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Retriever][None] = 0;

                testCases[CreatureConstants.Salamander_Flamebrother][GetData(SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing))] = 4;

                testCases[CreatureConstants.Salamander_Average][GetData(SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing))] = 4;

                testCases[CreatureConstants.Salamander_Noble][GetData(SkillConstants.Build(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing))] = 4;

                testCases[CreatureConstants.SeaHag][None] = 0;

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

                testCases[CreatureConstants.Spider_Swarm][GetData(SkillConstants.Climb)] = 8;
                testCases[CreatureConstants.Spider_Swarm][GetData(SkillConstants.Climb, condition: "can always take 10")] = 10;
                testCases[CreatureConstants.Spider_Swarm][GetData(SkillConstants.Hide)] = 4;
                testCases[CreatureConstants.Spider_Swarm][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Succubus][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Succubus][GetData(SkillConstants.Spot)] = 8;
                testCases[CreatureConstants.Succubus][GetData(SkillConstants.Disguise, condition: "when using Change Shape")] = 10;

                testCases[CreatureConstants.Tiefling][GetData(SkillConstants.Bluff)] = 2;
                testCases[CreatureConstants.Tiefling][GetData(SkillConstants.Hide)] = 2;

                testCases[CreatureConstants.Tojanida_Juvenile][None] = 0;

                testCases[CreatureConstants.Tojanida_Adult][None] = 0;

                testCases[CreatureConstants.Tojanida_Elder][None] = 0;

                testCases[CreatureConstants.Triton][None] = 0;

                testCases[CreatureConstants.Vrock][GetData(SkillConstants.Listen)] = 8;
                testCases[CreatureConstants.Vrock][GetData(SkillConstants.Spot)] = 8;

                testCases[CreatureConstants.Whale_Baleen][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Whale_Baleen][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Whale_Cachalot][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Whale_Cachalot][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Whale_Orca][GetData(SkillConstants.Listen)] = 4;
                testCases[CreatureConstants.Whale_Orca][GetData(SkillConstants.Spot)] = 4;

                testCases[CreatureConstants.Xorn_Minor][GetData(SkillConstants.Survival, condition: "underground")] = 2;

                testCases[CreatureConstants.Xorn_Average][GetData(SkillConstants.Survival, condition: "underground")] = 2;

                testCases[CreatureConstants.Xorn_Elder][GetData(SkillConstants.Survival, condition: "underground")] = 2;

                return TestDataHelper.EnumerateTestCases(testCases);
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

                foreach (var testCase in testCases)
                {
                    var description = None;

                    if (testCase.Value.Any() && testCase.Value.First().Key == None)
                    {
                        testCase.Value.Clear();
                    }
                    else
                    {
                        var bonuses = testCase.Value.Select(kvp => $"{string.Join(",", kvp.Value)}:{kvp.Key}");
                        description = string.Join("], [", bonuses);
                    }

                    yield return new TestCaseData(testCase.Key, testCase.Value)
                        .SetName($"SkillBonuses({testCase.Key}, [{description}])");
                }
            }
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

                testCases[CreatureConstants.Types.Subtypes.Angel][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Aquatic][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                testCases[CreatureConstants.Types.Subtypes.Aquatic][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                testCases[CreatureConstants.Types.Subtypes.Archon][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Augmented][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Chaotic][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Cold][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Earth][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Evil][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Extraplanar][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Fire][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Goblinoid][None] = 0;

                testCases[CreatureConstants.Types.Subtypes.Good][None] = 0;

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
                        SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Act),
                        SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Comedy),
                        SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Dance),
                        SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.KeyboardInstruments),
                        SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Oratory),
                        SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.PercussionInstruments),
                        SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.Sing),
                        SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.StringInstruments),
                        SkillConstants.Build(SkillConstants.Perform, SkillConstants.Foci.Perform.WindInstruments),
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
                testCases[SkillConstants.Bluff][GetData(SkillConstants.Diplomacy)] = 2;
                testCases[SkillConstants.Bluff][GetData(SkillConstants.Disguise, condition: "acting")] = 2;
                testCases[SkillConstants.Bluff][GetData(SkillConstants.Intimidate)] = 2;
                testCases[SkillConstants.Bluff][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)] = 2;
                testCases[SkillConstants.Bluff][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer)] = 2;
                testCases[SkillConstants.Bluff][GetData(SkillConstants.SleightOfHand)] = 2;
                testCases[SkillConstants.Craft][GetData(SkillConstants.Appraise, condition: "related to items made with your Craft skill")] = 2;
                testCases[SkillConstants.Craft][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking)][GetData(SkillConstants.Search, null, "finding traps")] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright, null)] = 2;
                testCases[GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker, null)] = 2;
                testCases[SkillConstants.DecipherScript][GetData(SkillConstants.UseMagicDevice, null, "with scrolls")] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister, null)] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler, null)] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide, null)] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman, null)] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess, null)] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker, null)] = 2;
                testCases[SkillConstants.Diplomacy][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet, null)] = 2;
                testCases[SkillConstants.EscapeArtist][GetData(SkillConstants.UseRope, null, "binding someone")] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Diplomacy, null, "wild empathy")] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer, null)] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer, null)] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer, null)] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd, null)] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster, null)] = 2;
                testCases[SkillConstants.HandleAnimal][GetData(SkillConstants.Ride, null, null)] = 2;
                testCases[SkillConstants.Heal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer, null)] = 2;
                testCases[SkillConstants.Heal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse, null)] = 2;
                testCases[SkillConstants.Heal][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife, null)] = 2;
                testCases[SkillConstants.Intimidate][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate, null)] = 2;
                testCases[SkillConstants.Jump][GetData(SkillConstants.Tumble, null, null)] = 2;
                testCases[SkillConstants.Knowledge][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser, null)] = 2;
                testCases[SkillConstants.Knowledge][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage, null)] = 2;
                testCases[SkillConstants.Knowledge][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Arcana)][GetData(SkillConstants.Spellcraft, null, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering)][GetData(SkillConstants.Search, null, "find secret doors or compartments")] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering)][GetData(SkillConstants.Survival, null, "underground")] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography)][GetData(SkillConstants.Survival, null, "keep from getting lost or avoid natural hazards")] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.History)][GetData(SkillConstants.Knowledge, "bardic", null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)][GetData(SkillConstants.GatherInformation, null, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature)][GetData(SkillConstants.Survival, null, "in aboveground natural environments")] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)][GetData(SkillConstants.Diplomacy, null, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty)][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid, null)] = 2;
                testCases[GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ThePlanes)][GetData(SkillConstants.Survival, null, "on other planes")] = 2;
                testCases[SkillConstants.Perform][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)][GetData(SkillConstants.Diplomacy, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Adviser)][GetData(SkillConstants.Knowledge, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Alchemist)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalGroomer)][GetData(SkillConstants.HandleAnimal, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.AnimalTrainer)][GetData(SkillConstants.HandleAnimal, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Apothecary)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Appraiser)][GetData(SkillConstants.Appraise, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Architect)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Armorer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Armorsmithing, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Barrister)][GetData(SkillConstants.Diplomacy, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Blacksmith)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Blacksmithing, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bookbinder)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bookbinding, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Bowyer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brazier)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brassmaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Brewer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Brewing, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)][GetData(SkillConstants.Diplomacy, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Butler)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Carpenter)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartographer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Mapmaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cartwright)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Chandler)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Candlemaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)][GetData(SkillConstants.Diplomacy, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.CityGuide)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Cobbler)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shoemaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coffinmaker)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Woodworking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Coppersmith)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Coppersmithing, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Craftsman)][GetData(SkillConstants.Craft, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)][GetData(SkillConstants.Bluff, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser)][GetData(SkillConstants.Survival, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dyer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Dyemaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Embalmer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Alchemy, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Engineer)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.ArchitectureAndEngineering, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Entertainer)][GetData(SkillConstants.Perform, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.ExoticAnimalTrainer)][GetData(SkillConstants.HandleAnimal, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Farmer)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Fletcher)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Bowmaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)][GetData(SkillConstants.Diplomacy, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Footman)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Gemcutter)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Gemcutting, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Goldsmith)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Goldsmithing, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)][GetData(SkillConstants.Diplomacy, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Governess)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Haberdasher)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hatmaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Healer)][GetData(SkillConstants.Heal, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Horner)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Hornworking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter)][GetData(SkillConstants.Survival, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Jeweler)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Jewelmaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Limner)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.LocalCourier)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Locksmith)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Locksmithing, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Maid)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.NobilityAndRoyalty, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Masseuse)][GetData(SkillConstants.Heal, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)][GetData(SkillConstants.Diplomacy, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker)][GetData(SkillConstants.SenseMotive, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Midwife)][GetData(SkillConstants.Heal, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miller)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Milling, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Geography, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Miner)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Dungeoneering, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator)][GetData(SkillConstants.Survival, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier)][GetData(SkillConstants.Ride, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Painter)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Painting, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Parchmentmaker)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Parchmentmaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Pewterer)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Pewtermaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Potter)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Potterymaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sage)][GetData(SkillConstants.Knowledge, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember)][GetData(SkillConstants.Swim, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)][GetData(SkillConstants.Intimidate, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate)][GetData(SkillConstants.Swim, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Sculptor)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Sculpting, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shepherd)][GetData(SkillConstants.HandleAnimal, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Shipwright)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Shipmaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Silversmith)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Silversmithing, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Skinner)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Skinning, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soapmaker)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Soapmaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Soothsayer)][GetData(SkillConstants.Bluff, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Tanner)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Tanning, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teacher)][GetData(SkillConstants.Knowledge, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)][GetData(SkillConstants.HandleAnimal, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster)][GetData(SkillConstants.Ride, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)][GetData(SkillConstants.Appraise, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader)][GetData(SkillConstants.SenseMotive, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Trapmaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper)][GetData(SkillConstants.Survival, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Valet)][GetData(SkillConstants.Diplomacy, null, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Vintner)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Winemaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaponsmith)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaponsmithing, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Weaver)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Weaving, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Wheelwright)][GetData(SkillConstants.Craft, SkillConstants.Foci.Craft.Wheelmaking, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Local, null)] = 2;
                testCases[GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.WildernessGuide)][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)] = 2;
                testCases[SkillConstants.Ride][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.OutOfTownCourier, null)] = 2;
                testCases[SkillConstants.Ride][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Teamster, null)] = 2;
                testCases[SkillConstants.Search][GetData(SkillConstants.Survival, null, "following tracks")] = 2;
                testCases[SkillConstants.SenseMotive][GetData(SkillConstants.Diplomacy, null, null)] = 2;
                testCases[SkillConstants.SenseMotive][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Matchmaker, null)] = 2;
                testCases[SkillConstants.SenseMotive][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trader, null)] = 2;
                testCases[SkillConstants.Spellcraft][GetData(SkillConstants.UseMagicDevice, null, "with scrolls")] = 2;
                testCases[SkillConstants.Survival][GetData(SkillConstants.Knowledge, SkillConstants.Foci.Knowledge.Nature, null)] = 2;
                testCases[SkillConstants.Survival][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Dowser, null)] = 2;
                testCases[SkillConstants.Survival][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Hunter, null)] = 2;
                testCases[SkillConstants.Survival][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Navigator, null)] = 2;
                testCases[SkillConstants.Survival][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.Trapper, null)] = 2;
                testCases[SkillConstants.Swim][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorCrewmember, null)] = 2;
                testCases[SkillConstants.Swim][GetData(SkillConstants.Profession, SkillConstants.Foci.Profession.SailorMate, null)] = 2;
                testCases[SkillConstants.Tumble][GetData(SkillConstants.Balance, null, null)] = 2;
                testCases[SkillConstants.Tumble][GetData(SkillConstants.Jump, null, null)] = 2;
                testCases[SkillConstants.UseMagicDevice][GetData(SkillConstants.Spellcraft, null, "decipher scrolls")] = 2;
                testCases[SkillConstants.UseRope][GetData(SkillConstants.Climb, null, "with rope")] = 2;
                testCases[SkillConstants.UseRope][GetData(SkillConstants.EscapeArtist, null, "escaping rope bonds")] = 2;

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
                        .SetName($"SkillBonuses({testCase.Key}, {description})");
                }
            }
        }

        private static string GetData(string skillName, string focus = "", string condition = "")
        {
            var data = SkillConstants.Build(skillName, focus);

            if (!string.IsNullOrEmpty(condition))
                data += "," + condition;

            return data;
        }
    }
}
