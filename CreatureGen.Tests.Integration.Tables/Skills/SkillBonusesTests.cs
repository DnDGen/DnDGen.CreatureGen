using CreatureGen.Creatures;
using CreatureGen.Skills;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Skills
{
    [TestFixture]
    public class SkillBonusesTests : TypesAndAmountsTests
    {
        protected override string tableName => TableNameConstants.TypeAndAmount.SkillBonuses;

        [Test]
        public void CollectionNames()
        {
            var creatures = CreatureConstants.All();
            AssertCollectionNames(creatures);
        }

        [TestCaseSource(typeof(SkillBonusesTestData), "Creatures")]
        [TestCaseSource(typeof(SkillBonusesTestData), "Types")]
        [TestCaseSource(typeof(SkillBonusesTestData), "Subtypes")]
        public void SkillBonuses(string source, Dictionary<string, int> skillAndBonus)
        {
            if (!skillAndBonus.Any())
                Assert.Fail("Test case did not specify special qualities or NONE");

            if (skillAndBonus.ContainsKey(SkillBonusesTestData.None))
                skillAndBonus.Clear();

            AssertTypesAndAmounts(source, skillAndBonus);
        }

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

                    testCases[CreatureConstants.Aasimar][GetData(SkillConstants.Listen)] = 2;
                    testCases[CreatureConstants.Aasimar][GetData(SkillConstants.Spot)] = 2;

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

                    testCases[CreatureConstants.Ape][RollHelper.GetRoll(4, 5, 8)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Ape_Dire][RollHelper.GetRoll(5, 6, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Aranea][None] = 0;
                    testCases[CreatureConstants.Arrowhawk_Adult][RollHelper.GetRoll(7, 8, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Arrowhawk_Elder][RollHelper.GetRoll(15, 16, 24)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Arrowhawk_Juvenile][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.AssassinVine][RollHelper.GetRoll(4, 5, 16)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.AssassinVine][RollHelper.GetRoll(4, 17, 32)] = GetData(SizeConstants.Gargantuan, 20, 20);
                    testCases[CreatureConstants.AssassinVine][RollHelper.GetRoll(4, 33, 100)] = GetData(SizeConstants.Colossal, 30, 30);
                    testCases[CreatureConstants.Athach][RollHelper.GetRoll(14, 15, 28)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Avoral][RollHelper.GetRoll(7, 8, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Avoral][RollHelper.GetRoll(7, 15, 21)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Azer][None] = 0;
                    testCases[CreatureConstants.Babau][RollHelper.GetRoll(7, 8, 14)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Babau][RollHelper.GetRoll(7, 15, 21)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Baboon][RollHelper.GetRoll(1, 2, 3)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Badger][RollHelper.GetRoll(1, 2, 2)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Badger_Dire][RollHelper.GetRoll(3, 4, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Balor][RollHelper.GetRoll(20, 21, 30)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Balor][RollHelper.GetRoll(20, 31, 60)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.BarbedDevil_Hamatula][RollHelper.GetRoll(12, 13, 24)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.BarbedDevil_Hamatula][RollHelper.GetRoll(12, 25, 36)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Barghest][RollHelper.GetRoll(6, 7, 8)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Barghest_Greater][RollHelper.GetRoll(9, 10, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Basilisk][RollHelper.GetRoll(6, 7, 10)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Basilisk][RollHelper.GetRoll(6, 11, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Basilisk_AbyssalGreater][None] = 0;
                    testCases[CreatureConstants.Bat][None] = 0;
                    testCases[CreatureConstants.Bat_Dire][RollHelper.GetRoll(4, 5, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Bat_Swarm][None] = 0;
                    testCases[CreatureConstants.Bear_Black][RollHelper.GetRoll(3, 4, 5)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Bear_Brown][RollHelper.GetRoll(6, 7, 10)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Bear_Polar][RollHelper.GetRoll(8, 9, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Bear_Dire][RollHelper.GetRoll(12, 13, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Bear_Dire][RollHelper.GetRoll(12, 17, 36)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.BeardedDevil_Barbazu][RollHelper.GetRoll(6, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.BeardedDevil_Barbazu][RollHelper.GetRoll(6, 10, 18)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Bebilith][RollHelper.GetRoll(12, 13, 18)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Bebilith][RollHelper.GetRoll(12, 19, 36)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Bee_Giant][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Bee_Giant][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Behir][RollHelper.GetRoll(9, 10, 13)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Behir][RollHelper.GetRoll(9, 14, 27)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Beholder][RollHelper.GetRoll(11, 12, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Beholder][RollHelper.GetRoll(11, 17, 33)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Beholder_Gauth][RollHelper.GetRoll(6, 7, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Beholder_Gauth][RollHelper.GetRoll(6, 13, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Belker][RollHelper.GetRoll(7, 8, 10)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Belker][RollHelper.GetRoll(7, 11, 21)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Bison][RollHelper.GetRoll(5, 6, 7)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.BlackPudding][RollHelper.GetRoll(10, 11, 15)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.BlackPudding_Elder][None] = 0;
                    testCases[CreatureConstants.BlinkDog][RollHelper.GetRoll(4, 5, 7)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.BlinkDog][RollHelper.GetRoll(4, 8, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Boar][RollHelper.GetRoll(3, 4, 5)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Boar_Dire][RollHelper.GetRoll(7, 8, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Boar_Dire][RollHelper.GetRoll(7, 17, 21)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Bodak][RollHelper.GetRoll(9, 10, 13)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Bodak][RollHelper.GetRoll(9, 14, 27)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.BombardierBeetle_Giant][RollHelper.GetRoll(2, 3, 4)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.BombardierBeetle_Giant][RollHelper.GetRoll(2, 5, 6)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.BoneDevil_Osyluth][RollHelper.GetRoll(10, 11, 20)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.BoneDevil_Osyluth][RollHelper.GetRoll(10, 21, 30)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Bralani][RollHelper.GetRoll(6, 7, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Bralani][RollHelper.GetRoll(6, 13, 18)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Bugbear][None] = 0;
                    testCases[CreatureConstants.Bulette][RollHelper.GetRoll(9, 10, 16)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Bulette][RollHelper.GetRoll(9, 17, 27)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Camel_Bactrian][None] = 0;
                    testCases[CreatureConstants.Camel_Dromedary][None] = 0;
                    testCases[CreatureConstants.CarrionCrawler][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.CarrionCrawler][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Cat][None] = 0;
                    testCases[CreatureConstants.Centaur][None] = 0;
                    testCases[CreatureConstants.Centipede_Monstrous_Colossal][RollHelper.GetRoll(24, 25, 48)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Centipede_Monstrous_Gargantuan][RollHelper.GetRoll(12, 13, 23)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Centipede_Monstrous_Huge][RollHelper.GetRoll(6, 7, 11)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Centipede_Monstrous_Large][RollHelper.GetRoll(3, 4, 5)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Centipede_Monstrous_Medium][None] = 0;
                    testCases[CreatureConstants.Centipede_Monstrous_Small][None] = 0;
                    testCases[CreatureConstants.Centipede_Monstrous_Tiny][None] = 0;
                    testCases[CreatureConstants.Centipede_Swarm][None] = 0;
                    testCases[CreatureConstants.ChainDevil_Kyton][RollHelper.GetRoll(8, 9, 16)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.ChaosBeast][RollHelper.GetRoll(8, 9, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.ChaosBeast][RollHelper.GetRoll(8, 13, 24)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Cheetah][RollHelper.GetRoll(3, 4, 5)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Chimera][RollHelper.GetRoll(9, 10, 13)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Chimera][RollHelper.GetRoll(9, 14, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Choker][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 10);
                    testCases[CreatureConstants.Choker][RollHelper.GetRoll(3, 7, 12)] = GetData(SizeConstants.Medium, 5, 10);
                    testCases[CreatureConstants.Chuul][RollHelper.GetRoll(11, 12, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Chuul][RollHelper.GetRoll(11, 17, 33)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Cloaker][RollHelper.GetRoll(6, 7, 9)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Cloaker][RollHelper.GetRoll(6, 10, 18)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Cockatrice][RollHelper.GetRoll(5, 6, 8)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Cockatrice][RollHelper.GetRoll(5, 9, 15)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Couatl][RollHelper.GetRoll(9, 10, 13)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Couatl][RollHelper.GetRoll(9, 14, 27)] = GetData(SizeConstants.Huge, 15, 10);

                    testCases[CreatureConstants.Criosphinx][None] = 0;

                    testCases[CreatureConstants.Crocodile][RollHelper.GetRoll(3, 4, 5)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Crocodile_Giant][RollHelper.GetRoll(7, 8, 14)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Cryohydra_10Heads][None] = 0;
                    testCases[CreatureConstants.Cryohydra_11Heads][None] = 0;
                    testCases[CreatureConstants.Cryohydra_12Heads][None] = 0;
                    testCases[CreatureConstants.Cryohydra_5Heads][None] = 0;
                    testCases[CreatureConstants.Cryohydra_6Heads][None] = 0;
                    testCases[CreatureConstants.Cryohydra_7Heads][None] = 0;
                    testCases[CreatureConstants.Cryohydra_8Heads][None] = 0;
                    testCases[CreatureConstants.Cryohydra_9Heads][None] = 0;
                    testCases[CreatureConstants.Darkmantle][RollHelper.GetRoll(1, 2, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Deinonychus][RollHelper.GetRoll(4, 5, 8)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Delver][RollHelper.GetRoll(15, 16, 30)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Delver][RollHelper.GetRoll(15, 31, 45)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Derro][None] = 0;
                    testCases[CreatureConstants.Derro_Sane][None] = 0;
                    testCases[CreatureConstants.Destrachan][RollHelper.GetRoll(8, 9, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Destrachan][RollHelper.GetRoll(8, 17, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Devourer][RollHelper.GetRoll(12, 13, 24)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Devourer][RollHelper.GetRoll(12, 25, 36)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Digester][RollHelper.GetRoll(8, 9, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Digester][RollHelper.GetRoll(8, 13, 24)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.DisplacerBeast][RollHelper.GetRoll(6, 7, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.DisplacerBeast][RollHelper.GetRoll(6, 10, 18)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.DisplacerBeast_PackLord][None] = 0;
                    testCases[CreatureConstants.Djinni][RollHelper.GetRoll(7, 8, 10)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Djinni][RollHelper.GetRoll(7, 11, 21)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Djinni_Noble][RollHelper.GetRoll(10, 11, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Djinni_Noble][RollHelper.GetRoll(10, 16, 30)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Dog][None] = 0;
                    testCases[CreatureConstants.Dog_Riding][None] = 0;
                    testCases[CreatureConstants.Donkey][None] = 0;
                    testCases[CreatureConstants.Doppelganger][None] = 0;
                    testCases[CreatureConstants.Dragon_Black_Wyrmling][RollHelper.GetRoll(4, 5, 6)] = GetData(SizeConstants.Tiny, 2.5, 0);
                    testCases[CreatureConstants.Dragon_Black_VeryYoung][RollHelper.GetRoll(7, 8, 9)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_Black_Young][RollHelper.GetRoll(10, 11, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Black_Juvenile][RollHelper.GetRoll(13, 14, 15)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Black_YoungAdult][RollHelper.GetRoll(16, 17, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Black_Adult][RollHelper.GetRoll(19, 20, 21)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Black_MatureAdult][RollHelper.GetRoll(22, 23, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Black_Old][RollHelper.GetRoll(25, 26, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Black_VeryOld][RollHelper.GetRoll(28, 29, 30)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Black_Ancient][RollHelper.GetRoll(31, 32, 33)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Black_Wyrm][RollHelper.GetRoll(34, 35, 36)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Black_GreatWyrm][RollHelper.GetRoll(37, 38, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Blue_Wyrmling][RollHelper.GetRoll(6, 7, 8)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_Blue_VeryYoung][RollHelper.GetRoll(9, 10, 11)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Blue_Young][RollHelper.GetRoll(12, 13, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Blue_Juvenile][RollHelper.GetRoll(15, 16, 17)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Blue_YoungAdult][RollHelper.GetRoll(18, 19, 20)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Blue_Adult][RollHelper.GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Blue_MatureAdult][RollHelper.GetRoll(24, 25, 26)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Blue_Old][RollHelper.GetRoll(27, 28, 29)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Blue_VeryOld][RollHelper.GetRoll(30, 31, 32)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Blue_Ancient][RollHelper.GetRoll(33, 34, 35)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Blue_Wyrm][RollHelper.GetRoll(36, 37, 38)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Blue_GreatWyrm][RollHelper.GetRoll(39, 40, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Green_Wyrmling][RollHelper.GetRoll(5, 6, 7)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_Green_VeryYoung][RollHelper.GetRoll(8, 9, 10)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Green_Young][RollHelper.GetRoll(11, 12, 13)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Green_Juvenile][RollHelper.GetRoll(14, 15, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Green_YoungAdult][RollHelper.GetRoll(17, 18, 19)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Green_Adult][RollHelper.GetRoll(20, 21, 22)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Green_MatureAdult][RollHelper.GetRoll(23, 24, 25)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Green_Old][RollHelper.GetRoll(26, 27, 28)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Green_VeryOld][RollHelper.GetRoll(29, 30, 31)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Green_Ancient][RollHelper.GetRoll(32, 33, 34)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Green_Wyrm][RollHelper.GetRoll(35, 36, 37)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Green_GreatWyrm][RollHelper.GetRoll(38, 39, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Red_Wyrmling][RollHelper.GetRoll(7, 8, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Red_VeryYoung][RollHelper.GetRoll(10, 11, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Red_Young][RollHelper.GetRoll(13, 14, 15)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Red_Juvenile][RollHelper.GetRoll(16, 17, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Red_YoungAdult][RollHelper.GetRoll(19, 20, 21)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Red_Adult][RollHelper.GetRoll(22, 23, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Red_MatureAdult][RollHelper.GetRoll(25, 26, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Red_Old][RollHelper.GetRoll(28, 29, 30)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Red_VeryOld][RollHelper.GetRoll(31, 32, 33)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Red_Ancient][RollHelper.GetRoll(34, 35, 36)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Red_Wyrm][RollHelper.GetRoll(37, 38, 39)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Red_GreatWyrm][RollHelper.GetRoll(40, 41, 100)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Dragon_White_Wyrmling][RollHelper.GetRoll(3, 4, 5)] = GetData(SizeConstants.Tiny, 2.5, 0);
                    testCases[CreatureConstants.Dragon_White_VeryYoung][RollHelper.GetRoll(6, 7, 8)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_White_Young][RollHelper.GetRoll(9, 10, 11)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_White_Juvenile][RollHelper.GetRoll(12, 13, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_White_YoungAdult][RollHelper.GetRoll(15, 16, 17)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_White_Adult][RollHelper.GetRoll(18, 19, 20)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_White_MatureAdult][RollHelper.GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_White_Old][RollHelper.GetRoll(24, 25, 26)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_White_VeryOld][RollHelper.GetRoll(27, 28, 29)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_White_Ancient][RollHelper.GetRoll(30, 31, 32)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_White_Wyrm][RollHelper.GetRoll(33, 34, 35)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_White_GreatWyrm][RollHelper.GetRoll(36, 37, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Brass_Wyrmling][RollHelper.GetRoll(4, 5, 6)] = GetData(SizeConstants.Tiny, 2.5, 0);
                    testCases[CreatureConstants.Dragon_Brass_VeryYoung][RollHelper.GetRoll(7, 8, 9)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_Brass_Young][RollHelper.GetRoll(10, 11, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Brass_Juvenile][RollHelper.GetRoll(13, 14, 15)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Brass_YoungAdult][RollHelper.GetRoll(16, 17, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Brass_Adult][RollHelper.GetRoll(19, 20, 21)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Brass_MatureAdult][RollHelper.GetRoll(22, 23, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Brass_Old][RollHelper.GetRoll(25, 26, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Brass_VeryOld][RollHelper.GetRoll(28, 29, 30)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Brass_Ancient][RollHelper.GetRoll(31, 32, 33)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Brass_Wyrm][RollHelper.GetRoll(34, 35, 36)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Brass_GreatWyrm][RollHelper.GetRoll(37, 38, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Bronze_Wyrmling][RollHelper.GetRoll(6, 7, 8)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_Bronze_VeryYoung][RollHelper.GetRoll(9, 10, 11)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Bronze_Young][RollHelper.GetRoll(12, 13, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Bronze_Juvenile][RollHelper.GetRoll(15, 16, 17)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Bronze_YoungAdult][RollHelper.GetRoll(18, 19, 20)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Bronze_Adult][RollHelper.GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Bronze_MatureAdult][RollHelper.GetRoll(24, 25, 26)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Bronze_Old][RollHelper.GetRoll(27, 28, 29)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Bronze_VeryOld][RollHelper.GetRoll(30, 31, 32)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Bronze_Ancient][RollHelper.GetRoll(33, 34, 35)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Bronze_Wyrm][RollHelper.GetRoll(36, 37, 38)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][RollHelper.GetRoll(39, 40, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Copper_Wyrmling][RollHelper.GetRoll(5, 6, 7)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_Copper_VeryYoung][RollHelper.GetRoll(8, 9, 10)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Copper_Young][RollHelper.GetRoll(11, 12, 13)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Copper_Juvenile][RollHelper.GetRoll(14, 15, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Copper_YoungAdult][RollHelper.GetRoll(17, 18, 19)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Copper_Adult][RollHelper.GetRoll(20, 21, 22)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Copper_MatureAdult][RollHelper.GetRoll(23, 24, 25)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Copper_Old][RollHelper.GetRoll(26, 27, 28)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Copper_VeryOld][RollHelper.GetRoll(29, 30, 31)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Copper_Ancient][RollHelper.GetRoll(32, 33, 34)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Copper_Wyrm][RollHelper.GetRoll(35, 36, 37)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Copper_GreatWyrm][RollHelper.GetRoll(38, 39, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Gold_Wyrmling][RollHelper.GetRoll(8, 9, 10)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Gold_VeryYoung][RollHelper.GetRoll(11, 12, 13)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Gold_Young][RollHelper.GetRoll(14, 15, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Gold_Juvenile][RollHelper.GetRoll(17, 18, 19)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Gold_YoungAdult][RollHelper.GetRoll(20, 21, 22)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Gold_Adult][RollHelper.GetRoll(23, 24, 25)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Gold_MatureAdult][RollHelper.GetRoll(26, 27, 28)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Gold_Old][RollHelper.GetRoll(29, 30, 31)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Gold_VeryOld][RollHelper.GetRoll(32, 33, 34)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Gold_Ancient][RollHelper.GetRoll(35, 36, 37)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Gold_Wyrm][RollHelper.GetRoll(38, 39, 40)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Dragon_Gold_GreatWyrm][RollHelper.GetRoll(41, 42, 100)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Dragon_Silver_Wyrmling][RollHelper.GetRoll(7, 8, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Silver_VeryYoung][RollHelper.GetRoll(10, 11, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Silver_Young][RollHelper.GetRoll(13, 14, 15)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Silver_Juvenile][RollHelper.GetRoll(16, 17, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Silver_YoungAdult][RollHelper.GetRoll(19, 20, 21)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Silver_Adult][RollHelper.GetRoll(22, 23, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Silver_MatureAdult][RollHelper.GetRoll(25, 26, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Silver_Old][RollHelper.GetRoll(28, 29, 30)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Silver_VeryOld][RollHelper.GetRoll(31, 32, 33)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Silver_Ancient][RollHelper.GetRoll(34, 35, 36)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Silver_Wyrm][RollHelper.GetRoll(37, 38, 39)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Silver_GreatWyrm][RollHelper.GetRoll(40, 41, 100)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.DragonTurtle][RollHelper.GetRoll(12, 13, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.DragonTurtle][RollHelper.GetRoll(12, 25, 36)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragonne][RollHelper.GetRoll(9, 10, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragonne][RollHelper.GetRoll(9, 13, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dretch][RollHelper.GetRoll(2, 3, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Drider][None] = 0;
                    testCases[CreatureConstants.Dryad][None] = 0;
                    testCases[CreatureConstants.Dwarf_Deep][None] = 0;
                    testCases[CreatureConstants.Dwarf_Duergar][None] = 0;
                    testCases[CreatureConstants.Dwarf_Hill][None] = 0;
                    testCases[CreatureConstants.Dwarf_Mountain][None] = 0;
                    testCases[CreatureConstants.Eagle][RollHelper.GetRoll(1, 2, 3)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Eagle_Giant][RollHelper.GetRoll(4, 5, 8)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Eagle_Giant][RollHelper.GetRoll(4, 9, 12)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Efreeti][RollHelper.GetRoll(10, 11, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Efreeti][RollHelper.GetRoll(10, 16, 30)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elasmosaurus][RollHelper.GetRoll(10, 11, 20)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Elasmosaurus][RollHelper.GetRoll(10, 21, 30)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Elemental_Air_Elder][RollHelper.GetRoll(24, 25, 48)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Air_Greater][RollHelper.GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Air_Huge][RollHelper.GetRoll(16, 17, 20)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Air_Large][RollHelper.GetRoll(8, 9, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Elemental_Air_Medium][RollHelper.GetRoll(4, 5, 7)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Elemental_Air_Small][RollHelper.GetRoll(2, 3, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Elemental_Earth_Elder][RollHelper.GetRoll(24, 25, 48)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Earth_Greater][RollHelper.GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Earth_Huge][RollHelper.GetRoll(16, 17, 20)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Earth_Large][RollHelper.GetRoll(8, 9, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Elemental_Earth_Medium][RollHelper.GetRoll(4, 5, 7)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Elemental_Earth_Small][RollHelper.GetRoll(2, 3, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Elemental_Fire_Elder][RollHelper.GetRoll(24, 25, 48)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Fire_Greater][RollHelper.GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Fire_Huge][RollHelper.GetRoll(16, 17, 20)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Fire_Large][RollHelper.GetRoll(8, 9, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Elemental_Fire_Medium][RollHelper.GetRoll(4, 5, 7)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Elemental_Fire_Small][RollHelper.GetRoll(2, 3, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Elemental_Water_Elder][RollHelper.GetRoll(24, 25, 48)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Water_Greater][RollHelper.GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Water_Huge][RollHelper.GetRoll(16, 17, 20)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Water_Large][RollHelper.GetRoll(8, 9, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Elemental_Water_Medium][RollHelper.GetRoll(4, 5, 7)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Elemental_Water_Small][RollHelper.GetRoll(2, 3, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Elephant][RollHelper.GetRoll(11, 12, 22)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Elf_Aquatic][None] = 0;
                    testCases[CreatureConstants.Elf_Drow][None] = 0;
                    testCases[CreatureConstants.Elf_Gray][None] = 0;
                    testCases[CreatureConstants.Elf_Half][None] = 0;
                    testCases[CreatureConstants.Elf_High][None] = 0;
                    testCases[CreatureConstants.Elf_Wild][None] = 0;
                    testCases[CreatureConstants.Elf_Wood][None] = 0;
                    testCases[CreatureConstants.Erinyes][RollHelper.GetRoll(9, 10, 18)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.EtherealFilcher][RollHelper.GetRoll(5, 6, 7)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.EtherealFilcher][RollHelper.GetRoll(5, 8, 15)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.EtherealMarauder][RollHelper.GetRoll(2, 3, 4)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.EtherealMarauder][RollHelper.GetRoll(2, 5, 6)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Ettercap][RollHelper.GetRoll(5, 6, 7)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Ettercap][RollHelper.GetRoll(5, 8, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Ettin][None] = 0;
                    testCases[CreatureConstants.FireBeetle_Giant][RollHelper.GetRoll(1, 2, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.FormianMyrmarch][RollHelper.GetRoll(12, 13, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.FormianMyrmarch][RollHelper.GetRoll(12, 19, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.FormianQueen][RollHelper.GetRoll(20, 21, 30)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.FormianQueen][RollHelper.GetRoll(20, 31, 40)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.FormianTaskmaster][RollHelper.GetRoll(6, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.FormianTaskmaster][RollHelper.GetRoll(6, 10, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.FormianWarrior][RollHelper.GetRoll(4, 5, 8)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.FormianWarrior][RollHelper.GetRoll(4, 9, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.FormianWorker][RollHelper.GetRoll(1, 2, 3)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.FrostWorm][RollHelper.GetRoll(14, 15, 21)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.FrostWorm][RollHelper.GetRoll(14, 22, 42)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Gargoyle][RollHelper.GetRoll(4, 5, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Gargoyle][RollHelper.GetRoll(4, 7, 12)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Gargoyle_Kapoacinth][RollHelper.GetRoll(4, 5, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Gargoyle_Kapoacinth][RollHelper.GetRoll(4, 7, 12)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.GelatinousCube][RollHelper.GetRoll(4, 5, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.GelatinousCube][RollHelper.GetRoll(4, 13, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Ghaele][RollHelper.GetRoll(10, 11, 15)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Ghaele][RollHelper.GetRoll(10, 16, 30)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Ghoul][RollHelper.GetRoll(2, 3, 3)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Ghoul_Ghast][RollHelper.GetRoll(4, 5, 8)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Ghoul_Lacedon][RollHelper.GetRoll(2, 3, 3)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Giant_Cloud][None] = 0;
                    testCases[CreatureConstants.Giant_Fire][None] = 0;
                    testCases[CreatureConstants.Giant_Frost][None] = 0;
                    testCases[CreatureConstants.Giant_Hill][None] = 0;
                    testCases[CreatureConstants.Giant_Stone][None] = 0;
                    testCases[CreatureConstants.Giant_Stone_Elder][None] = 0;
                    testCases[CreatureConstants.Giant_Storm][None] = 0;
                    testCases[CreatureConstants.GibberingMouther][RollHelper.GetRoll(4, 5, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Girallon][RollHelper.GetRoll(7, 8, 10)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Girallon][RollHelper.GetRoll(7, 11, 21)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Githyanki][None] = 0;
                    testCases[CreatureConstants.Githzerai][None] = 0;
                    testCases[CreatureConstants.Glabrezu][RollHelper.GetRoll(12, 13, 18)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Glabrezu][RollHelper.GetRoll(12, 19, 36)] = GetData(SizeConstants.Gargantuan, 20, 20);
                    testCases[CreatureConstants.Gnoll][None] = 0;
                    testCases[CreatureConstants.Gnome_Forest][None] = 0;
                    testCases[CreatureConstants.Gnome_Rock][None] = 0;
                    testCases[CreatureConstants.Gnome_Svirfneblin][None] = 0;
                    testCases[CreatureConstants.Goblin][None] = 0;
                    testCases[CreatureConstants.Golem_Clay][RollHelper.GetRoll(11, 12, 18)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Golem_Clay][RollHelper.GetRoll(11, 19, 33)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Golem_Flesh][RollHelper.GetRoll(9, 10, 18)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Golem_Flesh][RollHelper.GetRoll(9, 19, 27)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Golem_Iron][RollHelper.GetRoll(18, 19, 24)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Golem_Iron][RollHelper.GetRoll(18, 25, 54)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Golem_Stone][RollHelper.GetRoll(14, 15, 21)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Golem_Stone][RollHelper.GetRoll(14, 22, 42)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Golem_Stone_Greater][None] = 0;
                    testCases[CreatureConstants.Gorgon][RollHelper.GetRoll(8, 9, 15)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Gorgon][RollHelper.GetRoll(8, 16, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.GrayOoze][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.GrayOoze][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.GrayRender][RollHelper.GetRoll(10, 11, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.GrayRender][RollHelper.GetRoll(10, 16, 30)] = GetData(SizeConstants.Huge, 15, 15);

                    testCases[CreatureConstants.GreenHag][GetData(SkillConstants.Swim, condition: "special action or avoid a hazard")] = 8;
                    testCases[CreatureConstants.GreenHag][GetData(SkillConstants.Swim, condition: "can always take 10")] = 10;

                    testCases[CreatureConstants.Grick][RollHelper.GetRoll(2, 3, 4)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Grick][RollHelper.GetRoll(2, 5, 6)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Griffon][RollHelper.GetRoll(7, 8, 10)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Griffon][RollHelper.GetRoll(7, 11, 21)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Grig][RollHelper.GetRoll(0, 1, 3)] = GetData(SizeConstants.Tiny, 2.5, 0);
                    testCases[CreatureConstants.Grimlock][None] = 0;

                    testCases[CreatureConstants.Gynosphinx][None] = 0;

                    testCases[CreatureConstants.Halfling_Deep][None] = 0;
                    testCases[CreatureConstants.Halfling_Lightfoot][None] = 0;
                    testCases[CreatureConstants.Halfling_Tallfellow][None] = 0;
                    testCases[CreatureConstants.Harpy][None] = 0;
                    testCases[CreatureConstants.Hawk][None] = 0;
                    testCases[CreatureConstants.HellHound][RollHelper.GetRoll(4, 5, 8)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.HellHound][RollHelper.GetRoll(4, 9, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.HellHound_NessianWarhound][RollHelper.GetRoll(12, 13, 17)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.HellHound_NessianWarhound][RollHelper.GetRoll(12, 18, 24)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Hellcat_Bezekira][RollHelper.GetRoll(8, 9, 10)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Hellcat_Bezekira][RollHelper.GetRoll(8, 11, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Hellwasp_Swarm][None] = 0;
                    testCases[CreatureConstants.Hezrou][RollHelper.GetRoll(10, 11, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Hezrou][RollHelper.GetRoll(10, 16, 30)] = GetData(SizeConstants.Huge, 15, 15);

                    testCases[CreatureConstants.Hieracosphinx][SkillConstants.Spot] = 4;

                    testCases[CreatureConstants.Hippogriff][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Hippogriff][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Hobgoblin][None] = 0;
                    testCases[CreatureConstants.Homunculus][RollHelper.GetRoll(2, 3, 6)] = GetData(SizeConstants.Tiny, 2.5, 0);
                    testCases[CreatureConstants.HornedDevil_Cornugon][RollHelper.GetRoll(15, 16, 20)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.HornedDevil_Cornugon][RollHelper.GetRoll(15, 21, 45)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Horse_Heavy][None] = 0;
                    testCases[CreatureConstants.Horse_Heavy_War][None] = 0;
                    testCases[CreatureConstants.Horse_Light][None] = 0;
                    testCases[CreatureConstants.Horse_Light_War][None] = 0;
                    testCases[CreatureConstants.HoundArchon][RollHelper.GetRoll(6, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.HoundArchon][RollHelper.GetRoll(6, 10, 18)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Howler][RollHelper.GetRoll(6, 7, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Howler][RollHelper.GetRoll(6, 10, 18)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Human][None] = 0;
                    testCases[CreatureConstants.Hydra_10Heads][None] = 0;
                    testCases[CreatureConstants.Hydra_11Heads][None] = 0;
                    testCases[CreatureConstants.Hydra_12Heads][None] = 0;
                    testCases[CreatureConstants.Hydra_5Heads][None] = 0;
                    testCases[CreatureConstants.Hydra_6Heads][None] = 0;
                    testCases[CreatureConstants.Hydra_7Heads][None] = 0;
                    testCases[CreatureConstants.Hydra_8Heads][None] = 0;
                    testCases[CreatureConstants.Hydra_9Heads][None] = 0;
                    testCases[CreatureConstants.Hyena][RollHelper.GetRoll(2, 3, 3)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Hyena][RollHelper.GetRoll(2, 4, 5)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.IceDevil_Gelugon][RollHelper.GetRoll(14, 15, 28)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.IceDevil_Gelugon][RollHelper.GetRoll(14, 29, 42)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Imp][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Tiny, 2.5, 0);
                    testCases[CreatureConstants.InvisibleStalker][RollHelper.GetRoll(8, 9, 12)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.InvisibleStalker][RollHelper.GetRoll(8, 13, 24)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Janni][RollHelper.GetRoll(6, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Janni][RollHelper.GetRoll(6, 10, 18)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Kobold][None] = 0;
                    testCases[CreatureConstants.Kolyarut][RollHelper.GetRoll(13, 14, 22)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Kolyarut][RollHelper.GetRoll(13, 23, 39)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Kraken][RollHelper.GetRoll(20, 21, 32)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Kraken][RollHelper.GetRoll(20, 33, 60)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Krenshar][RollHelper.GetRoll(2, 3, 4)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Krenshar][RollHelper.GetRoll(2, 5, 8)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.KuoToa][None] = 0;
                    testCases[CreatureConstants.Lamia][RollHelper.GetRoll(9, 10, 13)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Lamia][RollHelper.GetRoll(9, 14, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Lammasu][RollHelper.GetRoll(7, 8, 10)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Lammasu][RollHelper.GetRoll(7, 11, 21)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.LanternArchon][RollHelper.GetRoll(1, 2, 4)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Lemure][RollHelper.GetRoll(2, 3, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Leonal][RollHelper.GetRoll(12, 13, 18)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Leonal][RollHelper.GetRoll(12, 19, 36)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Leopard][RollHelper.GetRoll(3, 4, 5)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Lillend][RollHelper.GetRoll(7, 8, 10)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Lillend][RollHelper.GetRoll(7, 11, 21)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Lion][RollHelper.GetRoll(5, 6, 8)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Lion_Dire][RollHelper.GetRoll(8, 9, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Lion_Dire][RollHelper.GetRoll(8, 17, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Lizard][None] = 0;
                    testCases[CreatureConstants.Lizard_Monitor][RollHelper.GetRoll(3, 4, 5)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Lizardfolk][None] = 0;
                    testCases[CreatureConstants.Locathah][None] = 0;
                    testCases[CreatureConstants.Locust_Swarm][None] = 0;
                    testCases[CreatureConstants.Magmin][RollHelper.GetRoll(2, 3, 4)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Magmin][RollHelper.GetRoll(2, 5, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.MantaRay][RollHelper.GetRoll(4, 5, 6)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Manticore][RollHelper.GetRoll(6, 7, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Manticore][RollHelper.GetRoll(6, 17, 18)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Marilith][RollHelper.GetRoll(16, 17, 20)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Marilith][RollHelper.GetRoll(16, 21, 48)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Marut][RollHelper.GetRoll(15, 16, 28)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Marut][RollHelper.GetRoll(15, 29, 45)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Medusa][None] = 0;
                    testCases[CreatureConstants.Megaraptor][RollHelper.GetRoll(8, 9, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Megaraptor][RollHelper.GetRoll(8, 17, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Mephit_Air][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Air][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Dust][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Dust][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Earth][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Earth][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Fire][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Fire][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Ice][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Ice][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Magma][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Magma][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Ooze][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Ooze][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Salt][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Salt][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Steam][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Steam][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Water][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Water][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Merfolk][None] = 0;
                    testCases[CreatureConstants.Mimic][RollHelper.GetRoll(7, 8, 10)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Mimic][RollHelper.GetRoll(7, 11, 21)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.MindFlayer][None] = 0;
                    testCases[CreatureConstants.Minotaur][None] = 0;
                    testCases[CreatureConstants.Mohrg][RollHelper.GetRoll(14, 15, 21)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mohrg][RollHelper.GetRoll(14, 22, 28)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Monkey][RollHelper.GetRoll(1, 2, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mule][None] = 0;
                    testCases[CreatureConstants.Mummy][RollHelper.GetRoll(8, 9, 16)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mummy][RollHelper.GetRoll(8, 17, 24)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Naga_Dark][RollHelper.GetRoll(9, 10, 13)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Naga_Dark][RollHelper.GetRoll(9, 14, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Naga_Guardian][RollHelper.GetRoll(11, 12, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Naga_Guardian][RollHelper.GetRoll(11, 17, 33)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Naga_Spirit][RollHelper.GetRoll(9, 10, 13)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Naga_Spirit][RollHelper.GetRoll(9, 14, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Naga_Water][RollHelper.GetRoll(7, 8, 10)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Naga_Water][RollHelper.GetRoll(7, 11, 21)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Nalfeshnee][RollHelper.GetRoll(14, 15, 20)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Nalfeshnee][RollHelper.GetRoll(14, 21, 42)] = GetData(SizeConstants.Gargantuan, 20, 20);
                    testCases[CreatureConstants.NightHag][RollHelper.GetRoll(8, 9, 16)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Nightcrawler][RollHelper.GetRoll(25, 26, 50)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Nightmare][RollHelper.GetRoll(6, 7, 10)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Nightmare][RollHelper.GetRoll(6, 11, 18)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Nightmare_Cauchemar][None] = 0;
                    testCases[CreatureConstants.Nightwalker][RollHelper.GetRoll(21, 22, 31)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Nightwalker][RollHelper.GetRoll(21, 32, 42)] = GetData(SizeConstants.Gargantuan, 20, 20);
                    testCases[CreatureConstants.Nightwing][RollHelper.GetRoll(17, 18, 25)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Nightwing][RollHelper.GetRoll(17, 26, 34)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Nixie][RollHelper.GetRoll(1, 2, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Nymph][RollHelper.GetRoll(6, 7, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.OchreJelly][RollHelper.GetRoll(6, 7, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.OchreJelly][RollHelper.GetRoll(6, 10, 18)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Octopus][RollHelper.GetRoll(2, 3, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Octopus_Giant][RollHelper.GetRoll(8, 9, 12)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Octopus_Giant][RollHelper.GetRoll(8, 13, 24)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Ogre][None] = 0;
                    testCases[CreatureConstants.Ogre_Merrow][None] = 0;
                    testCases[CreatureConstants.OgreMage][None] = 0;
                    testCases[CreatureConstants.Orc][None] = 0;
                    testCases[CreatureConstants.Orc_Half][None] = 0;
                    testCases[CreatureConstants.Otyugh][RollHelper.GetRoll(6, 7, 8)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Otyugh][RollHelper.GetRoll(6, 9, 18)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Owl][RollHelper.GetRoll(1, 2, 2)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Owl_Giant][RollHelper.GetRoll(4, 5, 8)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Owl_Giant][RollHelper.GetRoll(4, 9, 12)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Owlbear][RollHelper.GetRoll(5, 6, 8)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Owlbear][RollHelper.GetRoll(5, 9, 15)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Pegasus][RollHelper.GetRoll(4, 5, 8)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.PhantomFungus][RollHelper.GetRoll(2, 3, 4)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.PhantomFungus][RollHelper.GetRoll(2, 5, 6)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.PhaseSpider][RollHelper.GetRoll(5, 6, 8)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.PhaseSpider][RollHelper.GetRoll(5, 9, 15)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Phasm][RollHelper.GetRoll(15, 16, 21)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Phasm][RollHelper.GetRoll(15, 22, 45)] = GetData(SizeConstants.Gargantuan, 20, 20);
                    testCases[CreatureConstants.PitFiend][RollHelper.GetRoll(18, 19, 36)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.PitFiend][RollHelper.GetRoll(18, 37, 54)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Pixie][RollHelper.GetRoll(1, 2, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Pixie_WithIrresistableDance][RollHelper.GetRoll(1, 2, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Pony][None] = 0;
                    testCases[CreatureConstants.Pony_War][None] = 0;
                    testCases[CreatureConstants.Porpoise][RollHelper.GetRoll(2, 3, 4)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Porpoise][RollHelper.GetRoll(2, 3, 4)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.PrayingMantis_Giant][RollHelper.GetRoll(4, 5, 8)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.PrayingMantis_Giant][RollHelper.GetRoll(4, 9, 12)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Pseudodragon][RollHelper.GetRoll(2, 3, 4)] = GetData(SizeConstants.Tiny, 2.5, 0);
                    testCases[CreatureConstants.PurpleWorm][RollHelper.GetRoll(16, 17, 32)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.PurpleWorm][RollHelper.GetRoll(16, 33, 48)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Pyrohydra_10Heads][None] = 0;
                    testCases[CreatureConstants.Pyrohydra_11Heads][None] = 0;
                    testCases[CreatureConstants.Pyrohydra_12Heads][None] = 0;
                    testCases[CreatureConstants.Pyrohydra_5Heads][None] = 0;
                    testCases[CreatureConstants.Pyrohydra_6Heads][None] = 0;
                    testCases[CreatureConstants.Pyrohydra_7Heads][None] = 0;
                    testCases[CreatureConstants.Pyrohydra_8Heads][None] = 0;
                    testCases[CreatureConstants.Pyrohydra_9Heads][None] = 0;
                    testCases[CreatureConstants.Quasit][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Tiny, 2.5, 0);
                    testCases[CreatureConstants.Rakshasa][None] = 0;
                    testCases[CreatureConstants.Rast][RollHelper.GetRoll(4, 5, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Rast][RollHelper.GetRoll(4, 7, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Rat][None] = 0;
                    testCases[CreatureConstants.Rat_Dire][RollHelper.GetRoll(1, 2, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Rat_Dire][RollHelper.GetRoll(1, 4, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Rat_Swarm][None] = 0;
                    testCases[CreatureConstants.Raven][None] = 0;
                    testCases[CreatureConstants.Ravid][RollHelper.GetRoll(3, 4, 4)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Ravid][RollHelper.GetRoll(3, 5, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.RazorBoar][RollHelper.GetRoll(15, 16, 30)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.RazorBoar][RollHelper.GetRoll(15, 31, 45)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Remorhaz][RollHelper.GetRoll(7, 8, 14)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Remorhaz][RollHelper.GetRoll(7, 15, 21)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Retriever][RollHelper.GetRoll(10, 11, 15)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Retriever][RollHelper.GetRoll(10, 16, 30)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Rhinoceras][RollHelper.GetRoll(8, 9, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Rhinoceras][RollHelper.GetRoll(8, 13, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Roc][RollHelper.GetRoll(18, 19, 32)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Roc][RollHelper.GetRoll(18, 33, 54)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Roper][RollHelper.GetRoll(10, 11, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Roper][RollHelper.GetRoll(10, 16, 30)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.RustMonster][RollHelper.GetRoll(5, 6, 8)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.RustMonster][RollHelper.GetRoll(5, 9, 15)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Sahuagin][RollHelper.GetRoll(2, 3, 5)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Sahuagin][RollHelper.GetRoll(2, 6, 10)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Sahuagin_Malenti][RollHelper.GetRoll(2, 3, 5)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Sahuagin_Malenti][RollHelper.GetRoll(2, 6, 10)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Sahuagin_Mutant][RollHelper.GetRoll(2, 3, 5)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Sahuagin_Mutant][RollHelper.GetRoll(2, 6, 10)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Salamander_Average][RollHelper.GetRoll(9, 10, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Salamander_Flamebrother][RollHelper.GetRoll(4, 5, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Salamander_Noble][RollHelper.GetRoll(15, 16, 21)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Salamander_Noble][RollHelper.GetRoll(15, 22, 45)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Satyr][RollHelper.GetRoll(5, 6, 10)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Satyr_WithPipes][RollHelper.GetRoll(5, 6, 10)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Scorpion_Monstrous_Colossal][RollHelper.GetRoll(40, 41, 60)] = GetData(SizeConstants.Colossal, 40, 30);
                    testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan][RollHelper.GetRoll(20, 21, 39)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Scorpion_Monstrous_Huge][RollHelper.GetRoll(10, 11, 19)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Scorpion_Monstrous_Large][RollHelper.GetRoll(5, 6, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Scorpion_Monstrous_Medium][RollHelper.GetRoll(2, 3, 4)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Scorpion_Monstrous_Small][None] = 0;
                    testCases[CreatureConstants.Scorpion_Monstrous_Tiny][None] = 0;
                    testCases[CreatureConstants.Scorpionfolk][None] = 0;
                    testCases[CreatureConstants.SeaCat][RollHelper.GetRoll(6, 7, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.SeaCat][RollHelper.GetRoll(6, 10, 18)] = GetData(SizeConstants.Huge, 15, 10);

                    testCases[CreatureConstants.SeaHag][None] = 0;

                    testCases[CreatureConstants.Shadow][RollHelper.GetRoll(3, 4, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Shadow_Greater][None] = 0;
                    testCases[CreatureConstants.ShadowMastiff][RollHelper.GetRoll(4, 5, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.ShadowMastiff][RollHelper.GetRoll(4, 7, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.ShamblingMound][RollHelper.GetRoll(8, 9, 12)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.ShamblingMound][RollHelper.GetRoll(8, 13, 24)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Shark_Dire][RollHelper.GetRoll(18, 19, 32)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Shark_Dire][RollHelper.GetRoll(18, 33, 54)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Shark_Huge][RollHelper.GetRoll(10, 11, 17)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Shark_Large][RollHelper.GetRoll(7, 8, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Shark_Medium][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.ShieldGuardian][RollHelper.GetRoll(15, 16, 24)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.ShieldGuardian][RollHelper.GetRoll(15, 25, 45)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.ShockerLizard][RollHelper.GetRoll(2, 3, 4)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.ShockerLizard][RollHelper.GetRoll(2, 5, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Shrieker][RollHelper.GetRoll(2, 3, 3)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Skum][RollHelper.GetRoll(2, 3, 4)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Skum][RollHelper.GetRoll(2, 5, 6)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Slaad_Blue][RollHelper.GetRoll(8, 9, 12)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Slaad_Blue][RollHelper.GetRoll(8, 13, 24)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Slaad_Death][RollHelper.GetRoll(15, 16, 22)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Slaad_Death][RollHelper.GetRoll(15, 23, 45)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Slaad_Gray][RollHelper.GetRoll(10, 11, 15)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Slaad_Gray][RollHelper.GetRoll(10, 16, 30)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Slaad_Green][RollHelper.GetRoll(9, 10, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Slaad_Green][RollHelper.GetRoll(9, 16, 27)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Slaad_Red][RollHelper.GetRoll(7, 8, 10)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Slaad_Red][RollHelper.GetRoll(7, 11, 21)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Snake_Constrictor][RollHelper.GetRoll(3, 4, 5)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Snake_Constrictor][RollHelper.GetRoll(3, 6, 10)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Snake_Constrictor_Giant][RollHelper.GetRoll(11, 12, 16)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Snake_Constrictor_Giant][RollHelper.GetRoll(11, 17, 33)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Snake_Viper_Huge][RollHelper.GetRoll(6, 7, 18)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Snake_Viper_Large][None] = 0;
                    testCases[CreatureConstants.Snake_Viper_Medium][None] = 0;
                    testCases[CreatureConstants.Snake_Viper_Small][None] = 0;
                    testCases[CreatureConstants.Snake_Viper_Tiny][None] = 0;
                    testCases[CreatureConstants.Spectre][RollHelper.GetRoll(7, 8, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Spider_Monstrous_Colossal][RollHelper.GetRoll(32, 33, 60)] = GetData(SizeConstants.Colossal, 40, 30);
                    testCases[CreatureConstants.Spider_Monstrous_Gargantuan][RollHelper.GetRoll(16, 17, 31)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Spider_Monstrous_Huge][RollHelper.GetRoll(8, 9, 15)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Spider_Monstrous_Large][RollHelper.GetRoll(4, 5, 7)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Spider_Monstrous_Medium][RollHelper.GetRoll(2, 3, 3)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Spider_Monstrous_Small][None] = 0;
                    testCases[CreatureConstants.Spider_Monstrous_Tiny][None] = 0;
                    testCases[CreatureConstants.Spider_Swarm][None] = 0;
                    testCases[CreatureConstants.SpiderEater][RollHelper.GetRoll(4, 5, 12)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Squid][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Squid][RollHelper.GetRoll(3, 7, 11)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Squid_Giant][RollHelper.GetRoll(12, 13, 18)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Squid_Giant][RollHelper.GetRoll(12, 19, 36)] = GetData(SizeConstants.Gargantuan, 20, 20);
                    testCases[CreatureConstants.StagBeetle_Giant][RollHelper.GetRoll(7, 8, 10)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.StagBeetle_Giant][RollHelper.GetRoll(7, 11, 21)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Stirge][None] = 0;
                    testCases[CreatureConstants.Succubus][RollHelper.GetRoll(6, 7, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Tarrasque][RollHelper.GetRoll(48, 49, 100)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Tendriculos][RollHelper.GetRoll(9, 10, 16)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Tendriculos][RollHelper.GetRoll(9, 17, 27)] = GetData(SizeConstants.Gargantuan, 20, 20);
                    testCases[CreatureConstants.Thoqqua][RollHelper.GetRoll(3, 4, 9)] = GetData(SizeConstants.Large, 10, 5);

                    testCases[CreatureConstants.Tiefling][GetData(SkillConstants.Bluff)] = 2;
                    testCases[CreatureConstants.Tiefling][GetData(SkillConstants.Hide)] = 2;

                    testCases[CreatureConstants.Tiger][RollHelper.GetRoll(6, 7, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Tiger][RollHelper.GetRoll(6, 13, 18)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Tiger_Dire][RollHelper.GetRoll(16, 17, 32)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Tiger_Dire][RollHelper.GetRoll(16, 33, 48)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Titan][RollHelper.GetRoll(20, 21, 30)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Titan][RollHelper.GetRoll(20, 31, 60)] = GetData(SizeConstants.Gargantuan, 20, 20);
                    testCases[CreatureConstants.Toad][None] = 0;
                    testCases[CreatureConstants.Tojanida_Adult][RollHelper.GetRoll(7, 8, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Tojanida_Elder][RollHelper.GetRoll(15, 16, 24)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Tojanida_Juvenile][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Treant][RollHelper.GetRoll(7, 8, 16)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Treant][RollHelper.GetRoll(7, 17, 21)] = GetData(SizeConstants.Gargantuan, 20, 20);
                    testCases[CreatureConstants.Triceratops][RollHelper.GetRoll(16, 17, 32)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Triceratops][RollHelper.GetRoll(16, 33, 48)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Triton][RollHelper.GetRoll(3, 4, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Troglodyte][None] = 0;
                    testCases[CreatureConstants.Troll][None] = 0;
                    testCases[CreatureConstants.Troll_Scrag][None] = 0;
                    testCases[CreatureConstants.TrumpetArchon][RollHelper.GetRoll(12, 13, 18)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.TrumpetArchon][RollHelper.GetRoll(12, 19, 36)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Tyrannosaurus][RollHelper.GetRoll(18, 19, 36)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Tyrannosaurus][RollHelper.GetRoll(18, 37, 54)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Unicorn][RollHelper.GetRoll(4, 5, 8)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.UmberHulk][RollHelper.GetRoll(8, 9, 12)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.UmberHulk][RollHelper.GetRoll(8, 13, 24)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.UmberHulk_TrulyHorrid][None] = 0;
                    testCases[CreatureConstants.VampireSpawn][None] = 0;
                    testCases[CreatureConstants.Vargouille][RollHelper.GetRoll(1, 2, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.VioletFungus][RollHelper.GetRoll(2, 3, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Vrock][RollHelper.GetRoll(10, 11, 14)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Vrock][RollHelper.GetRoll(10, 15, 30)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Wasp_Giant][RollHelper.GetRoll(5, 6, 8)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Wasp_Giant][RollHelper.GetRoll(5, 9, 15)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Weasel][None] = 0;
                    testCases[CreatureConstants.Weasel_Dire][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Weasel_Dire][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Whale_Baleen][RollHelper.GetRoll(12, 13, 18)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Whale_Baleen][RollHelper.GetRoll(12, 19, 36)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Whale_Cachalot][RollHelper.GetRoll(12, 13, 18)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Whale_Cachalot][RollHelper.GetRoll(12, 19, 36)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Whale_Orca][RollHelper.GetRoll(9, 10, 13)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Whale_Orca][RollHelper.GetRoll(9, 14, 27)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Wight][RollHelper.GetRoll(4, 5, 8)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.WillOWisp][RollHelper.GetRoll(9, 10, 18)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.WinterWolf][RollHelper.GetRoll(6, 7, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.WinterWolf][RollHelper.GetRoll(6, 10, 18)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Wolf][RollHelper.GetRoll(2, 3, 3)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Wolf][RollHelper.GetRoll(2, 4, 6)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Wolf_Dire][RollHelper.GetRoll(6, 7, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Wolverine][RollHelper.GetRoll(3, 4, 5)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Wolverine_Dire][RollHelper.GetRoll(5, 6, 15)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Worg][RollHelper.GetRoll(4, 5, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Worg][RollHelper.GetRoll(4, 7, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Wraith][RollHelper.GetRoll(5, 6, 10)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Wraith_Dread][RollHelper.GetRoll(16, 17, 32)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Wyvern][RollHelper.GetRoll(7, 8, 10)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Wyvern][RollHelper.GetRoll(7, 11, 21)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Xill][RollHelper.GetRoll(5, 6, 8)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Xill][RollHelper.GetRoll(5, 9, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Xorn_Average][RollHelper.GetRoll(7, 8, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Xorn_Elder][RollHelper.GetRoll(15, 16, 21)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Xorn_Elder][RollHelper.GetRoll(15, 22, 45)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Xorn_Minor][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.YethHound][RollHelper.GetRoll(3, 4, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.YethHound][RollHelper.GetRoll(3, 7, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Yrthak][RollHelper.GetRoll(12, 13, 16)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Yrthak][RollHelper.GetRoll(12, 17, 36)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.YuanTi_Abomination][None] = 0;
                    testCases[CreatureConstants.YuanTi_Halfblood][None] = 0;
                    testCases[CreatureConstants.YuanTi_Pureblood][None] = 0;
                    testCases[CreatureConstants.Zelekhut][RollHelper.GetRoll(8, 9, 16)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Zelekhut][RollHelper.GetRoll(8, 17, 24)] = GetData(SizeConstants.Huge, 15, 15);

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

            private static string GetData(string skillName, string focus = "", string condition = "")
            {
                var data = SkillConstants.Build(skillName, focus);

                if (!string.IsNullOrEmpty(condition))
                    data += "," + condition;

                return data;
            }
        }
    }
}
