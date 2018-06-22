using CreatureGen.Creatures;
using CreatureGen.Tables;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class AdvancementsTests : TypesAndAmountsTests
    {
        protected override string tableName => TableNameConstants.TypeAndAmount.Advancements;

        [Test]
        public void CollectionNames()
        {
            var creatures = CreatureConstants.All();
            var creatureTypes = CreatureConstants.Types.All();

            var names = creatures.Union(creatureTypes);

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(AdvancementsTestData), "TestCases")]
        public void Advancement(string creature, Dictionary<string, string[]> rollAndData)
        {
            var typesAndAmounts = new Dictionary<string, string>();

            foreach (var kvp in rollAndData)
            {
                var roll = kvp.Key;
                var data = kvp.Value;

                var type = string.Join(",", data);

                typesAndAmounts[type] = roll;
            }

            AssertTypesAndAmounts(creature, typesAndAmounts);
        }

        public class AdvancementsTestData
        {
            private const string None = "NONE";

            public static IEnumerable TestCases
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, string[]>>();
                    var creatures = CreatureConstants.All();

                    foreach (var creature in creatures)
                    {
                        testCases[creature] = new Dictionary<string, string[]>();
                    }

                    testCases[CreatureConstants.Aasimar][None] = new string[0];
                    testCases[CreatureConstants.Aboleth][GetRoll(8, 9, 16)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Aboleth][GetRoll(8, 17, 24)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Achaierai][GetRoll(6, 7, 12)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Achaierai][GetRoll(6, 13, 18)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Allip][GetRoll(4, 5, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Androsphinx][GetRoll(12, 13, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Androsphinx][GetRoll(12, 19, 36)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Angel_AstralDeva][GetRoll(12, 13, 18)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Angel_AstralDeva][GetRoll(12, 19, 36)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Angel_Planetar][GetRoll(14, 15, 21)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Angel_Planetar][GetRoll(14, 22, 42)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Angel_Solar][GetRoll(22, 23, 33)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Angel_Solar][GetRoll(22, 34, 66)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.AnimatedObject_Colossal][None] = new string[0];
                    testCases[CreatureConstants.AnimatedObject_Gargantuan][None] = new string[0];
                    testCases[CreatureConstants.AnimatedObject_Huge][None] = new string[0];
                    testCases[CreatureConstants.AnimatedObject_Large][None] = new string[0];
                    testCases[CreatureConstants.AnimatedObject_Medium][None] = new string[0];
                    testCases[CreatureConstants.AnimatedObject_Small][None] = new string[0];
                    testCases[CreatureConstants.AnimatedObject_Tiny][None] = new string[0];
                    testCases[CreatureConstants.Ankheg][GetRoll(3, 4, 4)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Ankheg][GetRoll(3, 5, 9)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Annis][None] = new string[0];
                    testCases[CreatureConstants.Ant_Giant_Queen][GetRoll(4, 5, 6)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Ant_Giant_Queen][GetRoll(4, 7, 8)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Ant_Giant_Soldier][GetRoll(2, 3, 4)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Ant_Giant_Soldier][GetRoll(2, 5, 6)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Ant_Giant_Worker][GetRoll(2, 3, 4)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Ant_Giant_Worker][GetRoll(2, 5, 6)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Ape][GetRoll(4, 5, 8)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Ape_Dire][GetRoll(5, 6, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Aranea][None] = new string[0];
                    testCases[CreatureConstants.Arrowhawk_Adult][GetRoll(7, 8, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Arrowhawk_Elder][GetRoll(15, 16, 24)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Arrowhawk_Juvenile][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.AssassinVine][GetRoll(4, 5, 16)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.AssassinVine][GetRoll(4, 17, 32)] = GetData(SizeConstants.Gargantuan, 20, 20);
                    testCases[CreatureConstants.AssassinVine][GetRoll(4, 33, 100)] = GetData(SizeConstants.Colossal, 30, 30);
                    testCases[CreatureConstants.Athach][GetRoll(14, 15, 28)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Avoral][GetRoll(7, 8, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Avoral][GetRoll(7, 15, 21)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Azer][None] = new string[0];
                    testCases[CreatureConstants.Babau][GetRoll(7, 8, 14)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Babau][GetRoll(7, 15, 21)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Baboon][GetRoll(1, 2, 3)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Badger][GetRoll(1, 2, 2)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Balor][GetRoll(20, 21, 30)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Balor][GetRoll(20, 31, 60)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Barghest][GetRoll(6, 7, 8)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Barghest_Greater][GetRoll(9, 10, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Basilisk][GetRoll(6, 7, 10)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Basilisk][GetRoll(6, 11, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Basilisk_AbyssalGreater][None] = new string[0];
                    testCases[CreatureConstants.Bat][None] = new string[0];
                    testCases[CreatureConstants.Bat_Swarm][None] = new string[0];
                    testCases[CreatureConstants.Bear_Black][GetRoll(3, 4, 5)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Bebilith][GetRoll(12, 13, 18)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Bebilith][GetRoll(12, 19, 36)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Behir][GetRoll(9, 10, 13)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Behir][GetRoll(9, 14, 27)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Beholder][GetRoll(11, 12, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Beholder][GetRoll(11, 17, 33)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Beholder_Gauth][GetRoll(6, 7, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Beholder_Gauth][GetRoll(6, 13, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Belker][GetRoll(7, 8, 10)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Belker][GetRoll(7, 11, 21)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Bison][GetRoll(5, 6, 7)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.BlackPudding][GetRoll(10, 11, 15)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.BlackPudding_Elder][None] = new string[0];
                    testCases[CreatureConstants.BlinkDog][GetRoll(4, 5, 7)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.BlinkDog][GetRoll(4, 8, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Centipede_Swarm][None] = new string[0];
                    testCases[CreatureConstants.Criosphinx][GetRoll(10, 11, 15)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Criosphinx][GetRoll(10, 16, 30)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Black_Wyrmling][GetRoll(4, 5, 6)] = GetData(SizeConstants.Tiny, 2.5, 0);
                    testCases[CreatureConstants.Dragon_Black_VeryYoung][GetRoll(7, 8, 9)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_Black_Young][GetRoll(10, 11, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Black_Juvenile][GetRoll(13, 14, 15)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Black_YoungAdult][GetRoll(16, 17, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Black_Adult][GetRoll(19, 20, 21)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Black_MatureAdult][GetRoll(22, 23, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Black_Old][GetRoll(25, 26, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Black_VeryOld][GetRoll(28, 29, 30)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Black_Ancient][GetRoll(31, 32, 33)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Black_Wyrm][GetRoll(34, 35, 36)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Black_GreatWyrm][GetRoll(37, 38, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Blue_Wyrmling][GetRoll(6, 7, 8)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_Blue_VeryYoung][GetRoll(9, 10, 11)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Blue_Young][GetRoll(12, 13, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Blue_Juvenile][GetRoll(15, 16, 17)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Blue_YoungAdult][GetRoll(18, 19, 20)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Blue_Adult][GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Blue_MatureAdult][GetRoll(24, 25, 26)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Blue_Old][GetRoll(27, 28, 29)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Blue_VeryOld][GetRoll(30, 31, 32)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Blue_Ancient][GetRoll(33, 34, 35)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Blue_Wyrm][GetRoll(36, 37, 38)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Blue_GreatWyrm][GetRoll(39, 40, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Green_Wyrmling][GetRoll(5, 6, 7)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_Green_VeryYoung][GetRoll(8, 9, 10)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Green_Young][GetRoll(11, 12, 13)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Green_Juvenile][GetRoll(14, 15, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Green_YoungAdult][GetRoll(17, 18, 19)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Green_Adult][GetRoll(20, 21, 22)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Green_MatureAdult][GetRoll(23, 24, 25)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Green_Old][GetRoll(26, 27, 28)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Green_VeryOld][GetRoll(29, 30, 31)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Green_Ancient][GetRoll(32, 33, 34)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Green_Wyrm][GetRoll(35, 36, 37)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Green_GreatWyrm][GetRoll(38, 39, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Red_Wyrmling][GetRoll(7, 8, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Red_VeryYoung][GetRoll(10, 11, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Red_Young][GetRoll(13, 14, 15)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Red_Juvenile][GetRoll(16, 17, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Red_YoungAdult][GetRoll(19, 20, 21)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Red_Adult][GetRoll(22, 23, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Red_MatureAdult][GetRoll(25, 26, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Red_Old][GetRoll(28, 29, 30)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Red_VeryOld][GetRoll(31, 32, 33)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Red_Ancient][GetRoll(34, 35, 36)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Red_Wyrm][GetRoll(37, 38, 39)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Red_GreatWyrm][GetRoll(40, 41, 100)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Dragon_White_Wyrmling][GetRoll(3, 4, 5)] = GetData(SizeConstants.Tiny, 2.5, 0);
                    testCases[CreatureConstants.Dragon_White_VeryYoung][GetRoll(6, 7, 8)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_White_Young][GetRoll(9, 10, 11)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_White_Juvenile][GetRoll(12, 13, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_White_YoungAdult][GetRoll(15, 16, 17)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_White_Adult][GetRoll(18, 19, 20)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_White_MatureAdult][GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_White_Old][GetRoll(24, 25, 26)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_White_VeryOld][GetRoll(27, 28, 29)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_White_Ancient][GetRoll(30, 31, 32)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_White_Wyrm][GetRoll(33, 34, 35)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_White_GreatWyrm][GetRoll(36, 37, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Brass_Wyrmling][GetRoll(4, 5, 6)] = GetData(SizeConstants.Tiny, 2.5, 0);
                    testCases[CreatureConstants.Dragon_Brass_VeryYoung][GetRoll(7, 8, 9)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_Brass_Young][GetRoll(10, 11, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Brass_Juvenile][GetRoll(13, 14, 15)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Brass_YoungAdult][GetRoll(16, 17, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Brass_Adult][GetRoll(19, 20, 21)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Brass_MatureAdult][GetRoll(22, 23, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Brass_Old][GetRoll(25, 26, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Brass_VeryOld][GetRoll(28, 29, 30)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Brass_Ancient][GetRoll(31, 32, 33)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Brass_Wyrm][GetRoll(34, 35, 36)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Brass_GreatWyrm][GetRoll(37, 38, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Bronze_Wyrmling][GetRoll(6, 7, 8)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_Bronze_VeryYoung][GetRoll(9, 10, 11)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Bronze_Young][GetRoll(12, 13, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Bronze_Juvenile][GetRoll(15, 16, 17)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Bronze_YoungAdult][GetRoll(18, 19, 20)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Bronze_Adult][GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Bronze_MatureAdult][GetRoll(24, 25, 26)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Bronze_Old][GetRoll(27, 28, 29)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Bronze_VeryOld][GetRoll(30, 31, 32)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Bronze_Ancient][GetRoll(33, 34, 35)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Bronze_Wyrm][GetRoll(36, 37, 38)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][GetRoll(39, 40, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Copper_Wyrmling][GetRoll(5, 6, 7)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dragon_Copper_VeryYoung][GetRoll(8, 9, 10)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Copper_Young][GetRoll(11, 12, 13)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Copper_Juvenile][GetRoll(14, 15, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Copper_YoungAdult][GetRoll(17, 18, 19)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Copper_Adult][GetRoll(20, 21, 22)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Copper_MatureAdult][GetRoll(23, 24, 25)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Copper_Old][GetRoll(26, 27, 28)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Copper_VeryOld][GetRoll(29, 30, 31)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Copper_Ancient][GetRoll(32, 33, 34)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Copper_Wyrm][GetRoll(35, 36, 37)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Copper_GreatWyrm][GetRoll(38, 39, 100)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Gold_Wyrmling][GetRoll(8, 9, 10)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Gold_VeryYoung][GetRoll(11, 12, 13)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Gold_Young][GetRoll(14, 15, 16)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Gold_Juvenile][GetRoll(17, 18, 19)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Gold_YoungAdult][GetRoll(20, 21, 22)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Gold_Adult][GetRoll(23, 24, 25)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Gold_MatureAdult][GetRoll(26, 27, 28)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Gold_Old][GetRoll(29, 30, 31)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Gold_VeryOld][GetRoll(32, 33, 34)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Gold_Ancient][GetRoll(35, 36, 37)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Gold_Wyrm][GetRoll(38, 39, 40)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Dragon_Gold_GreatWyrm][GetRoll(41, 42, 100)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Dragon_Silver_Wyrmling][GetRoll(7, 8, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Dragon_Silver_VeryYoung][GetRoll(10, 11, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Silver_Young][GetRoll(13, 14, 15)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Silver_Juvenile][GetRoll(16, 17, 18)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Dragon_Silver_YoungAdult][GetRoll(19, 20, 21)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Silver_Adult][GetRoll(22, 23, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Silver_MatureAdult][GetRoll(25, 26, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Dragon_Silver_Old][GetRoll(28, 29, 30)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Silver_VeryOld][GetRoll(31, 32, 33)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Silver_Ancient][GetRoll(34, 35, 36)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Silver_Wyrm][GetRoll(37, 38, 39)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Dragon_Silver_GreatWyrm][GetRoll(40, 41, 100)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Dretch][GetRoll(2, 3, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Dwarf_Deep][None] = new string[0];
                    testCases[CreatureConstants.Dwarf_Duergar][None] = new string[0];
                    testCases[CreatureConstants.Dwarf_Hill][None] = new string[0];
                    testCases[CreatureConstants.Dwarf_Mountain][None] = new string[0];
                    testCases[CreatureConstants.Elemental_Air_Elder][GetRoll(24, 25, 48)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Air_Greater][GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Air_Huge][GetRoll(16, 17, 20)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Air_Large][GetRoll(8, 9, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Elemental_Air_Medium][GetRoll(4, 5, 7)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Elemental_Air_Small][GetRoll(2, 3, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Elemental_Earth_Elder][GetRoll(24, 25, 48)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Earth_Greater][GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Earth_Huge][GetRoll(16, 17, 20)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Earth_Large][GetRoll(8, 9, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Elemental_Earth_Medium][GetRoll(4, 5, 7)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Elemental_Earth_Small][GetRoll(2, 3, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Elemental_Fire_Elder][GetRoll(24, 25, 48)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Fire_Greater][GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Fire_Huge][GetRoll(16, 17, 20)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Fire_Large][GetRoll(8, 9, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Elemental_Fire_Medium][GetRoll(4, 5, 7)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Elemental_Fire_Small][GetRoll(2, 3, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Elemental_Water_Elder][GetRoll(24, 25, 48)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Water_Greater][GetRoll(21, 22, 23)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Water_Huge][GetRoll(16, 17, 20)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Elemental_Water_Large][GetRoll(8, 9, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Elemental_Water_Medium][GetRoll(4, 5, 7)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Elemental_Water_Small][GetRoll(2, 3, 3)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Elf_Aquatic][None] = new string[0];
                    testCases[CreatureConstants.Elf_Drow][None] = new string[0];
                    testCases[CreatureConstants.Elf_Gray][None] = new string[0];
                    testCases[CreatureConstants.Elf_Half][None] = new string[0];
                    testCases[CreatureConstants.Elf_High][None] = new string[0];
                    testCases[CreatureConstants.Elf_Wild][None] = new string[0];
                    testCases[CreatureConstants.Elf_Wood][None] = new string[0];
                    testCases[CreatureConstants.GelatinousCube][GetRoll(4, 5, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.GelatinousCube][GetRoll(4, 13, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Glabrezu][GetRoll(12, 13, 18)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Glabrezu][GetRoll(12, 19, 36)] = GetData(SizeConstants.Gargantuan, 20, 20);
                    testCases[CreatureConstants.Gnome_Forest][None] = new string[0];
                    testCases[CreatureConstants.Gnome_Rock][None] = new string[0];
                    testCases[CreatureConstants.Gnome_Svirfneblin][None] = new string[0];
                    testCases[CreatureConstants.GrayOoze][GetRoll(3, 4, 6)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.GrayOoze][GetRoll(3, 7, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.GreenHag][None] = new string[0];
                    testCases[CreatureConstants.Gynosphinx][GetRoll(8, 9, 12)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Gynosphinx][GetRoll(8, 13, 24)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Halfling_Deep][None] = new string[0];
                    testCases[CreatureConstants.Halfling_Lightfoot][None] = new string[0];
                    testCases[CreatureConstants.Halfling_Tallfellow][None] = new string[0];
                    testCases[CreatureConstants.Hellwasp_Swarm][None] = new string[0];
                    testCases[CreatureConstants.Hezrou][GetRoll(10, 11, 15)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Hezrou][GetRoll(10, 16, 30)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Hieracosphinx][GetRoll(9, 10, 14)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Hieracosphinx][GetRoll(9, 15, 27)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Human][None] = new string[0];
                    testCases[CreatureConstants.Locust_Swarm][None] = new string[0];
                    testCases[CreatureConstants.Marilith][GetRoll(16, 17, 20)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Marilith][GetRoll(16, 21, 48)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Mephit_Air][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Air][GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Dust][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Dust][GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Earth][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Earth][GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Fire][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Fire][GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Ice][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Ice][GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Magma][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Magma][GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Ooze][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Ooze][GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Salt][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Salt][GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Steam][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Steam][GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Mephit_Water][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Mephit_Water][GetRoll(3, 7, 9)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Nalfeshnee][GetRoll(14, 15, 20)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Nalfeshnee][GetRoll(14, 21, 42)] = GetData(SizeConstants.Gargantuan, 20, 20);
                    testCases[CreatureConstants.OchreJelly][GetRoll(6, 7, 9)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.OchreJelly][GetRoll(6, 10, 18)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Orc][None] = new string[0];
                    testCases[CreatureConstants.Orc_Half][None] = new string[0];
                    testCases[CreatureConstants.Quasit][GetRoll(3, 4, 6)] = GetData(SizeConstants.Tiny, 2.5, 0);
                    testCases[CreatureConstants.Rat_Swarm][None] = new string[0];
                    testCases[CreatureConstants.Retriever][GetRoll(10, 11, 15)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Retriever][GetRoll(10, 16, 30)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Salamander_Average][GetRoll(9, 10, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Salamander_Flamebrother][GetRoll(4, 5, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Salamander_Noble][GetRoll(15, 16, 21)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Salamander_Noble][GetRoll(15, 22, 45)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.SeaHag][None] = new string[0];
                    testCases[CreatureConstants.Spider_Swarm][None] = new string[0];
                    testCases[CreatureConstants.Succubus][GetRoll(6, 7, 12)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Tiefling][None] = new string[0];
                    testCases[CreatureConstants.Tojanida_Adult][GetRoll(7, 8, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Tojanida_Elder][GetRoll(15, 16, 24)] = GetData(SizeConstants.Large, 10, 5);
                    testCases[CreatureConstants.Tojanida_Juvenile][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);
                    testCases[CreatureConstants.Vrock][GetRoll(10, 11, 14)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Vrock][GetRoll(10, 15, 30)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Whale_Baleen][GetRoll(12, 13, 18)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Whale_Baleen][GetRoll(12, 19, 36)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Whale_Cachalot][GetRoll(12, 13, 18)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Whale_Cachalot][GetRoll(12, 19, 36)] = GetData(SizeConstants.Colossal, 30, 20);
                    testCases[CreatureConstants.Whale_Orca][GetRoll(9, 10, 13)] = GetData(SizeConstants.Huge, 15, 10);
                    testCases[CreatureConstants.Whale_Orca][GetRoll(9, 14, 27)] = GetData(SizeConstants.Gargantuan, 20, 15);
                    testCases[CreatureConstants.Xorn_Average][GetRoll(7, 8, 14)] = GetData(SizeConstants.Medium, 5, 5);
                    testCases[CreatureConstants.Xorn_Elder][GetRoll(15, 16, 21)] = GetData(SizeConstants.Large, 10, 10);
                    testCases[CreatureConstants.Xorn_Elder][GetRoll(15, 22, 45)] = GetData(SizeConstants.Huge, 15, 15);
                    testCases[CreatureConstants.Xorn_Minor][GetRoll(3, 4, 6)] = GetData(SizeConstants.Small, 5, 5);

                    foreach (var testCase in testCases)
                    {
                        if (testCase.Value.Any() && testCase.Value.First().Key == None)
                        {
                            testCase.Value.Clear();

                            yield return new TestCaseData(testCase.Key, testCase.Value)
                                .SetName($"Advancement({testCase.Key}, [{None}])");
                        }
                        else
                        {
                            var advancements = testCase.Value.Select(kvp => $"{string.Join(",", kvp.Value)}:{kvp.Key}");

                            yield return new TestCaseData(testCase.Key, testCase.Value)
                                .SetName($"Advancement({testCase.Key}, [{string.Join("], [", advancements)}])");
                        }
                    }
                }
            }

            private static string GetRoll(int baseHitDiceQuantity, int advancedLowerRange, int dvancedUpperRange)
            {
                var lower = advancedLowerRange - baseHitDiceQuantity;
                var upper = dvancedUpperRange - baseHitDiceQuantity;

                if (lower == upper)
                    return lower.ToString();

                var standardDie = new[] { 2, 3, 4, 6, 8, 10, 12, 20, 100 };
                var range = upper - lower;

                var possibleDieRolls = Enumerable.Range(1, range)
                    .Where(f => range % f == 0) //Get factors
                    .ToDictionary(f => f, f => range / f + 1) //pair quantities with die
                    .Where(r => standardDie.Contains(r.Value)) //filter out non-standard die
                    .ToDictionary(r => r.Key, r => r.Value);

                var quantity = possibleDieRolls.Min(r => r.Key);
                var die = possibleDieRolls[quantity];
                var adjustment = lower - quantity;

                Assert.That(quantity + adjustment, Is.EqualTo(lower));
                Assert.That(quantity * die + adjustment, Is.EqualTo(upper));

                if (adjustment == 0)
                    return $"{quantity}d{die}";

                if (adjustment > 0)
                    return $"{quantity}d{die}+{adjustment}";

                return $"{quantity}d{die}{adjustment}";
            }

            private static string[] GetData(
                string advancedSize,
                double space,
                double reach)
            {

                var data = new string[3];
                data[DataIndexConstants.AdvancementSelectionData.Reach] = reach.ToString();
                data[DataIndexConstants.AdvancementSelectionData.Size] = advancedSize;
                data[DataIndexConstants.AdvancementSelectionData.Space] = space.ToString();

                return data;
            }
        }

        [TestCase(CreatureConstants.Types.Aberration, 4)]
        [TestCase(CreatureConstants.Types.Animal, 3)]
        [TestCase(CreatureConstants.Types.Construct, 4)]
        [TestCase(CreatureConstants.Types.Dragon, 2)]
        [TestCase(CreatureConstants.Types.Elemental, 4)]
        [TestCase(CreatureConstants.Types.Fey, 4)]
        [TestCase(CreatureConstants.Types.Giant, 4)]
        [TestCase(CreatureConstants.Types.Humanoid, 4)]
        [TestCase(CreatureConstants.Types.MagicalBeast, 3)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, 3)]
        [TestCase(CreatureConstants.Types.Ooze, 4)]
        [TestCase(CreatureConstants.Types.Outsider, 2)]
        [TestCase(CreatureConstants.Types.Plant, 4)]
        [TestCase(CreatureConstants.Types.Undead, 4)]
        [TestCase(CreatureConstants.Types.Vermin, 4)]
        public void AdvancementChallengeRatingDivisor(string creatureType, int divisor)
        {
            var typesAndAmounts = new Dictionary<string, int>();
            typesAndAmounts[creatureType] = divisor;

            AssertTypesAndAmounts(creatureType, typesAndAmounts);
        }
    }
}
