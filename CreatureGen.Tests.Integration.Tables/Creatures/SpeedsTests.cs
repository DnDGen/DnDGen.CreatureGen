using CreatureGen.Creatures;
using CreatureGen.Selectors.Collections;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    public class SpeedsTests : TypesAndAmountsTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        internal ITypeAndAmountSelector TypesAndAmountsSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        protected override string tableName
        {
            get { return TableNameConstants.Set.Collection.Speeds; }
        }

        [SetUp]
        public void Setup()
        {
            var clientId = Guid.NewGuid();
            ClientIdManager.SetClientID(clientId);
        }

        [Test]
        public void CollectionNames()
        {
            var names = CreatureConstants.All();
            AssertCollectionNames(names);
        }

        [Test]
        [TestCaseSource(typeof(SpeedsTestData), "TestCases")]
        public void Speeds(string name, Dictionary<string, int> typesAndAmounts)
        {
            Assert.That(typesAndAmounts, Is.Not.Empty, name);
            AssertTypesAndAmounts(name, typesAndAmounts);
        }

        private class SpeedsTestData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    var creatures = CreatureConstants.All();

                    foreach (var creature in creatures)
                    {
                        testCases[creature] = new Dictionary<string, int>();
                    }

                    testCases[CreatureConstants.Aasimar][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Aboleth][SpeedConstants.Land] = 10;
                    testCases[CreatureConstants.Aboleth][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Aboleth_Mage][SpeedConstants.Land] = 10;
                    testCases[CreatureConstants.Aboleth_Mage][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Achaierai][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Allip][SpeedConstants.Fly] = 30;
                    testCases[CreatureConstants.Androsphinx][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Androsphinx][SpeedConstants.Fly] = 80;
                    testCases[CreatureConstants.Angel_AstralDeva][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Angel_AstralDeva][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Angel_Planetar][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Angel_Planetar][SpeedConstants.Fly] = 90;
                    testCases[CreatureConstants.Angel_Solar][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Angel_Solar][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.AnimatedObject_Colossal][SpeedConstants.Land] = 10;
                    testCases[CreatureConstants.AnimatedObject_Gargantuan][SpeedConstants.Land] = 10;
                    testCases[CreatureConstants.AnimatedObject_Huge][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.AnimatedObject_Large][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.AnimatedObject_Medium][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.AnimatedObject_Small][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.AnimatedObject_Tiny][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Ankheg][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Ankheg][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Annis][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Ant_Giant_Queen][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Ant_Giant_Soldier][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Ant_Giant_Soldier][SpeedConstants.Climb] = 20;
                    testCases[CreatureConstants.Ant_Giant_Worker][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Ant_Giant_Worker][SpeedConstants.Climb] = 20;
                    testCases[CreatureConstants.Ape][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Ape][SpeedConstants.Climb] = 30;
                    testCases[CreatureConstants.Ape_Dire][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Ape_Dire][SpeedConstants.Climb] = 15;
                    testCases[CreatureConstants.Aranea][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Aranea][SpeedConstants.Climb] = 25;
                    testCases[CreatureConstants.Arrowhawk_Adult][SpeedConstants.Fly] = 60;
                    testCases[CreatureConstants.Arrowhawk_Elder][SpeedConstants.Fly] = 60;
                    testCases[CreatureConstants.Arrowhawk_Juvenile][SpeedConstants.Fly] = 60;
                    testCases[CreatureConstants.AssassinVine][SpeedConstants.Land] = 5;
                    testCases[CreatureConstants.Athach][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Avoral][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Avoral][SpeedConstants.Fly] = 90;
                    testCases[CreatureConstants.Azer][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Babau][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Baboon][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Baboon][SpeedConstants.Climb] = 30;
                    testCases[CreatureConstants.Badger][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Badger][SpeedConstants.Burrow] = 10;
                    testCases[CreatureConstants.Badger_Dire][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Badger_Dire][SpeedConstants.Burrow] = 10;
                    testCases[CreatureConstants.Balor][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Balor][SpeedConstants.Fly] = 90;
                    testCases[CreatureConstants.BarbedDevil_Hamatula][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Barghest][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Barghest_Greater][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Basilisk][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Basilisk_AbyssalGreater][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Bat][SpeedConstants.Land] = 5;
                    testCases[CreatureConstants.Bat][SpeedConstants.Fly] = 40;
                    testCases[CreatureConstants.Bat_Swarm][SpeedConstants.Land] = 5;
                    testCases[CreatureConstants.Bat_Swarm][SpeedConstants.Fly] = 40;
                    testCases[CreatureConstants.Bear_Black][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Bear_Brown][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Bear_Dire][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Bear_Polar][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Bear_Polar][SpeedConstants.Swim] = 30;
                    testCases[CreatureConstants.BeardedDevil_Barbazu][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Bebilith][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Bebilith][SpeedConstants.Climb] = 20;
                    testCases[CreatureConstants.Behir][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Behir][SpeedConstants.Climb] = 15;
                    testCases[CreatureConstants.Beholder][SpeedConstants.Land] = 5;
                    testCases[CreatureConstants.Beholder][SpeedConstants.Fly] = 20;
                    testCases[CreatureConstants.Beholder_Gauth][SpeedConstants.Land] = 5;
                    testCases[CreatureConstants.Beholder_Gauth][SpeedConstants.Fly] = 20;
                    testCases[CreatureConstants.Belker][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Belker][SpeedConstants.Fly] = 50;
                    testCases[CreatureConstants.Bison][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.BlackPudding][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.BlackPudding][SpeedConstants.Climb] = 20;
                    testCases[CreatureConstants.BlackPudding_Elder][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.BlackPudding_Elder][SpeedConstants.Climb] = 20;
                    testCases[CreatureConstants.BlinkDog][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Boar][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Bodak][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.BoneDevil_Osyluth][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Bugbear][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Centaur][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Centipede_Swarm][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Centipede_Swarm][SpeedConstants.Climb] = 20;
                    testCases[CreatureConstants.ChainDevil_Kyton][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Chuul][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Chuul][SpeedConstants.Swim] = 20;
                    testCases[CreatureConstants.Criosphinx][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Criosphinx][SpeedConstants.Fly] = 60;
                    testCases[CreatureConstants.Derro][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Djinni][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Djinni][SpeedConstants.Fly] = 70;
                    testCases[CreatureConstants.Djinni_Noble][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Djinni_Noble][SpeedConstants.Fly] = 70;
                    testCases[CreatureConstants.Doppelganger][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Dragon_Black_Wyrmling][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Black_Wyrmling][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Dragon_Black_Wyrmling][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Black_VeryYoung][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Black_VeryYoung][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Dragon_Black_VeryYoung][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Black_Young][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Black_Young][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Black_Young][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Black_Juvenile][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Black_Juvenile][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Black_Juvenile][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Black_YoungAdult][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Black_YoungAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Black_YoungAdult][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Black_Adult][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Black_Adult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Black_Adult][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Black_MatureAdult][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Black_MatureAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Black_MatureAdult][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Black_Old][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Black_Old][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Black_Old][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Black_VeryOld][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Black_VeryOld][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Black_VeryOld][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Black_Ancient][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Black_Ancient][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Black_Ancient][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Black_Wyrm][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Black_Wyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Black_Wyrm][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Black_GreatWyrm][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Black_GreatWyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Black_GreatWyrm][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Blue_Wyrmling][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Blue_Wyrmling][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Dragon_Blue_Wyrmling][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Dragon_Blue_VeryYoung][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Blue_VeryYoung][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Dragon_Blue_VeryYoung][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Blue_Young][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Blue_Young][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Dragon_Blue_Young][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Blue_Juvenile][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Blue_Juvenile][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Dragon_Blue_Juvenile][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Blue_YoungAdult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Blue_YoungAdult][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Dragon_Blue_YoungAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Blue_Adult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Blue_Adult][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Dragon_Blue_Adult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Blue_MatureAdult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Blue_MatureAdult][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Dragon_Blue_MatureAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Blue_Old][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Blue_Old][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Dragon_Blue_Old][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Blue_VeryOld][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Blue_VeryOld][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Dragon_Blue_VeryOld][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Blue_Ancient][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Blue_Ancient][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Dragon_Blue_Ancient][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Blue_Wyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Blue_Wyrm][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Dragon_Blue_Wyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Blue_GreatWyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Blue_GreatWyrm][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Dragon_Blue_GreatWyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Green_Wyrmling][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Green_Wyrmling][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Dragon_Green_Wyrmling][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Dragon_Green_VeryYoung][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Green_VeryYoung][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Green_VeryYoung][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Dragon_Green_Young][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Green_Young][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Green_Young][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Dragon_Green_Juvenile][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Green_Juvenile][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Green_Juvenile][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Dragon_Green_YoungAdult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Green_YoungAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Green_YoungAdult][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Dragon_Green_Adult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Green_Adult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Green_Adult][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Dragon_Green_MatureAdult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Green_MatureAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Green_MatureAdult][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Dragon_Green_Old][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Green_Old][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Green_Old][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Dragon_Green_VeryOld][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Green_VeryOld][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Green_VeryOld][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Dragon_Green_Ancient][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Green_Ancient][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Green_Ancient][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Dragon_Green_Wyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Green_Wyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Green_Wyrm][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Dragon_Green_GreatWyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Green_GreatWyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Green_GreatWyrm][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Dragon_Red_Wyrmling][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Red_Wyrmling][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Red_VeryYoung][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Red_VeryYoung][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Red_Young][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Red_Young][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Red_Juvenile][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Red_Juvenile][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Red_YoungAdult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Red_YoungAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Red_Adult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Red_Adult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Red_MatureAdult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Red_MatureAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Red_Old][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Red_Old][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Red_VeryOld][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Red_VeryOld][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Red_Ancient][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Red_Ancient][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Red_Wyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Red_Wyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Red_GreatWyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Red_GreatWyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_White_Wyrmling][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_White_Wyrmling][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_White_Wyrmling][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_White_Wyrmling][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_White_VeryYoung][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_White_VeryYoung][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_White_VeryYoung][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_White_VeryYoung][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_White_Young][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_White_Young][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_White_Young][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_White_Young][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_White_Juvenile][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_White_Juvenile][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_White_Juvenile][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_White_Juvenile][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_White_YoungAdult][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_White_YoungAdult][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_White_YoungAdult][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_White_YoungAdult][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_White_Adult][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_White_Adult][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_White_Adult][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_White_Adult][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_White_MatureAdult][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_White_MatureAdult][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_White_MatureAdult][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_White_MatureAdult][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_White_Old][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_White_Old][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_White_Old][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_White_Old][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_White_VeryOld][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_White_VeryOld][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_White_VeryOld][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_White_VeryOld][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_White_Ancient][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_White_Ancient][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_White_Ancient][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_White_Ancient][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_White_Wyrm][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_White_Wyrm][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_White_Wyrm][SpeedConstants.Fly] = 250;
                    testCases[CreatureConstants.Dragon_White_Wyrm][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_White_GreatWyrm][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_White_GreatWyrm][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_White_GreatWyrm][SpeedConstants.Fly] = 250;
                    testCases[CreatureConstants.Dragon_White_GreatWyrm][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Brass_Wyrmling][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Brass_Wyrmling][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_Brass_Wyrmling][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Brass_VeryYoung][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Brass_VeryYoung][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_Brass_VeryYoung][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Brass_Young][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Brass_Young][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_Brass_Young][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Brass_Juvenile][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Brass_Juvenile][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_Brass_Juvenile][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Brass_YoungAdult][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Brass_YoungAdult][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_Brass_YoungAdult][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Brass_Adult][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Brass_Adult][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_Brass_Adult][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Brass_MatureAdult][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Brass_MatureAdult][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_Brass_MatureAdult][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Brass_Old][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Brass_Old][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_Brass_Old][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Brass_VeryOld][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Brass_VeryOld][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_Brass_VeryOld][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Brass_Ancient][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Brass_Ancient][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_Brass_Ancient][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Brass_Wyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Brass_Wyrm][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_Brass_Wyrm][SpeedConstants.Fly] = 250;
                    testCases[CreatureConstants.Dragon_Brass_GreatWyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Brass_GreatWyrm][SpeedConstants.Burrow] = 30;
                    testCases[CreatureConstants.Dragon_Brass_GreatWyrm][SpeedConstants.Fly] = 250;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrmling][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrmling][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrmling][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Bronze_VeryYoung][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Bronze_VeryYoung][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Bronze_VeryYoung][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Bronze_Young][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Bronze_Young][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Bronze_Young][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Bronze_Juvenile][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Bronze_Juvenile][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Bronze_Juvenile][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Bronze_YoungAdult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Bronze_YoungAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Bronze_YoungAdult][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Bronze_Adult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Bronze_Adult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Bronze_Adult][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Bronze_MatureAdult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Bronze_MatureAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Bronze_MatureAdult][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Bronze_Old][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Bronze_Old][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Bronze_Old][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Bronze_VeryOld][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Bronze_VeryOld][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Bronze_VeryOld][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Bronze_Ancient][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Bronze_Ancient][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Bronze_Ancient][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrm][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Copper_Wyrmling][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Copper_Wyrmling][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Dragon_Copper_VeryYoung][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Copper_VeryYoung][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Dragon_Copper_Young][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Copper_Young][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Copper_Juvenile][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Copper_Juvenile][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Copper_YoungAdult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Copper_YoungAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Copper_Adult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Copper_Adult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Copper_MatureAdult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Copper_MatureAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Copper_Old][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Copper_Old][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Copper_VeryOld][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Copper_VeryOld][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Copper_Ancient][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Copper_Ancient][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Copper_Wyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Copper_Wyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Copper_GreatWyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Copper_GreatWyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Gold_Wyrmling][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Wyrmling][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Gold_Wyrmling][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Gold_VeryYoung][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Gold_VeryYoung][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Gold_VeryYoung][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Young][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Young][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Gold_Young][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Juvenile][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Juvenile][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Gold_Juvenile][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Gold_YoungAdult][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Gold_YoungAdult][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Gold_YoungAdult][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Adult][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Adult][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Gold_Adult][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Gold_MatureAdult][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Gold_MatureAdult][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Gold_MatureAdult][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Old][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Old][SpeedConstants.Fly] = 250;
                    testCases[CreatureConstants.Dragon_Gold_Old][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Gold_VeryOld][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Gold_VeryOld][SpeedConstants.Fly] = 250;
                    testCases[CreatureConstants.Dragon_Gold_VeryOld][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Ancient][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Ancient][SpeedConstants.Fly] = 250;
                    testCases[CreatureConstants.Dragon_Gold_Ancient][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Wyrm][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Gold_Wyrm][SpeedConstants.Fly] = 250;
                    testCases[CreatureConstants.Dragon_Gold_Wyrm][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Gold_GreatWyrm][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Dragon_Gold_GreatWyrm][SpeedConstants.Fly] = 250;
                    testCases[CreatureConstants.Dragon_Gold_GreatWyrm][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Dragon_Silver_Wyrmling][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Silver_Wyrmling][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Dragon_Silver_VeryYoung][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Silver_VeryYoung][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Silver_Young][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Silver_Young][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Silver_Juvenile][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Silver_Juvenile][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Silver_YoungAdult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Silver_YoungAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Silver_Adult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Silver_Adult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Silver_MatureAdult][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Silver_MatureAdult][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Silver_Old][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Silver_Old][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Silver_VeryOld][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Silver_VeryOld][SpeedConstants.Fly] = 150;
                    testCases[CreatureConstants.Dragon_Silver_Ancient][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Silver_Ancient][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Silver_Wyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Silver_Wyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.Dragon_Silver_GreatWyrm][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Dragon_Silver_GreatWyrm][SpeedConstants.Fly] = 200;
                    testCases[CreatureConstants.DragonTurtle][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.DragonTurtle][SpeedConstants.Swim] = 30;
                    testCases[CreatureConstants.Dretch][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Dwarf_Deep][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Dwarf_Duergar][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Dwarf_Hill][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Dwarf_Mountain][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Efreeti][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Efreeti][SpeedConstants.Fly] = 40;
                    testCases[CreatureConstants.Elemental_Air_Elder][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Elemental_Air_Greater][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Elemental_Air_Huge][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Elemental_Air_Large][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Elemental_Air_Medium][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Elemental_Air_Small][SpeedConstants.Fly] = 100;
                    testCases[CreatureConstants.Elemental_Earth_Elder][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Elemental_Earth_Greater][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Elemental_Earth_Huge][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Elemental_Earth_Large][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Elemental_Earth_Medium][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Elemental_Earth_Small][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Elemental_Fire_Elder][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Elemental_Fire_Greater][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Elemental_Fire_Huge][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Elemental_Fire_Large][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Elemental_Fire_Medium][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Elemental_Fire_Small][SpeedConstants.Land] = 60;
                    testCases[CreatureConstants.Elemental_Water_Elder][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Elemental_Water_Elder][SpeedConstants.Swim] = 120;
                    testCases[CreatureConstants.Elemental_Water_Greater][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Elemental_Water_Greater][SpeedConstants.Swim] = 120;
                    testCases[CreatureConstants.Elemental_Water_Huge][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Elemental_Water_Huge][SpeedConstants.Swim] = 120;
                    testCases[CreatureConstants.Elemental_Water_Large][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Elemental_Water_Large][SpeedConstants.Swim] = 90;
                    testCases[CreatureConstants.Elemental_Water_Medium][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Elemental_Water_Medium][SpeedConstants.Swim] = 90;
                    testCases[CreatureConstants.Elemental_Water_Small][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Elemental_Water_Small][SpeedConstants.Swim] = 90;
                    testCases[CreatureConstants.Elf_Aquatic][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Elf_Aquatic][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Elf_Drow][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Elf_Gray][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Elf_Half][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Elf_High][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Elf_Wild][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Elf_Wood][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Erinyes][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Erinyes][SpeedConstants.Fly] = 50;
                    testCases[CreatureConstants.Gargoyle][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Gargoyle][SpeedConstants.Fly] = 60;
                    testCases[CreatureConstants.Gargoyle_Kapoacinth][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Gargoyle_Kapoacinth][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.GelatinousCube][SpeedConstants.Land] = 15;
                    testCases[CreatureConstants.Ghoul][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Ghoul_Ghast][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Ghoul_Lacedon][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Ghoul_Lacedon][SpeedConstants.Swim] = 30;
                    testCases[CreatureConstants.Giant_Cloud][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Giant_Fire][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Giant_Frost][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Giant_Hill][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Giant_Stone][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Giant_Stone_Elder][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Giant_Storm][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Giant_Storm][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Githyanki][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Githzerai][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Glabrezu][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Gnoll][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Gnome_Forest][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Gnome_Rock][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Gnome_Svirfneblin][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Goblin][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.GrayOoze][SpeedConstants.Land] = 10;
                    testCases[CreatureConstants.GreenHag][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.GreenHag][SpeedConstants.Swim] = 30;
                    testCases[CreatureConstants.Grig][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Grig][SpeedConstants.Fly] = 40;
                    testCases[CreatureConstants.Grimlock][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Gynosphinx][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Gynosphinx][SpeedConstants.Fly] = 60;
                    testCases[CreatureConstants.Halfling_Deep][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Halfling_Lightfoot][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Halfling_Tallfellow][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Harpy][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Harpy][SpeedConstants.Fly] = 80;
                    testCases[CreatureConstants.Hellcat_Bezekira][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Hellwasp_Swarm][SpeedConstants.Land] = 5;
                    testCases[CreatureConstants.Hellwasp_Swarm][SpeedConstants.Fly] = 40;
                    testCases[CreatureConstants.Hezrou][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Hieracosphinx][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Hieracosphinx][SpeedConstants.Fly] = 90;
                    testCases[CreatureConstants.Hobgoblin][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.HornedDevil_Cornugon][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.HornedDevil_Cornugon][SpeedConstants.Fly] = 50;
                    testCases[CreatureConstants.HoundArchon][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Human][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.IceDevil_Gelugon][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Imp][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Imp][SpeedConstants.Fly] = 50;
                    testCases[CreatureConstants.Janni][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Janni][SpeedConstants.Fly] = 20;
                    testCases[CreatureConstants.Kobold][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Kraken][SpeedConstants.Swim] = 20;
                    testCases[CreatureConstants.KuoToa][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.KuoToa][SpeedConstants.Swim] = 50;
                    testCases[CreatureConstants.Lemure][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Lizardfolk][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Locathah][SpeedConstants.Land] = 10;
                    testCases[CreatureConstants.Locathah][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Locust_Swarm][SpeedConstants.Land] = 10;
                    testCases[CreatureConstants.Locust_Swarm][SpeedConstants.Fly] = 30;
                    testCases[CreatureConstants.MantaRay][SpeedConstants.Swim] = 30;
                    testCases[CreatureConstants.Marilith][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Mephit_Air][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Mephit_Air][SpeedConstants.Fly] = 60;
                    testCases[CreatureConstants.Mephit_Dust][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Mephit_Dust][SpeedConstants.Fly] = 50;
                    testCases[CreatureConstants.Mephit_Earth][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Mephit_Earth][SpeedConstants.Fly] = 40;
                    testCases[CreatureConstants.Mephit_Fire][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Mephit_Fire][SpeedConstants.Fly] = 40;
                    testCases[CreatureConstants.Mephit_Ice][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Mephit_Ice][SpeedConstants.Fly] = 50;
                    testCases[CreatureConstants.Mephit_Magma][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Mephit_Magma][SpeedConstants.Fly] = 50;
                    testCases[CreatureConstants.Mephit_Ooze][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Mephit_Ooze][SpeedConstants.Fly] = 40;
                    testCases[CreatureConstants.Mephit_Ooze][SpeedConstants.Swim] = 30;
                    testCases[CreatureConstants.Mephit_Salt][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Mephit_Salt][SpeedConstants.Fly] = 40;
                    testCases[CreatureConstants.Mephit_Steam][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Mephit_Steam][SpeedConstants.Fly] = 50;
                    testCases[CreatureConstants.Mephit_Water][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Mephit_Water][SpeedConstants.Fly] = 40;
                    testCases[CreatureConstants.Mephit_Water][SpeedConstants.Swim] = 30;
                    testCases[CreatureConstants.Merfolk][SpeedConstants.Land] = 5;
                    testCases[CreatureConstants.Merfolk][SpeedConstants.Swim] = 50;
                    testCases[CreatureConstants.MindFlayer][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Minotaur][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Naga_Dark][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Naga_Guardian][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Naga_Spirit][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Naga_Water][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Naga_Water][SpeedConstants.Swim] = 50;
                    testCases[CreatureConstants.Nalfeshnee][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Nalfeshnee][SpeedConstants.Fly] = 40;
                    testCases[CreatureConstants.Nixie][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Nixie][SpeedConstants.Swim] = 30;
                    testCases[CreatureConstants.OchreJelly][SpeedConstants.Land] = 10;
                    testCases[CreatureConstants.OchreJelly][SpeedConstants.Climb] = 10;
                    testCases[CreatureConstants.Octopus][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Octopus][SpeedConstants.Swim] = 30;
                    testCases[CreatureConstants.Octopus_Giant][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Octopus_Giant][SpeedConstants.Swim] = 30;
                    testCases[CreatureConstants.Orc][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Orc_Half][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Ogre][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Ogre_Merrow][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Ogre_Merrow][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.OgreMage][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.OgreMage][SpeedConstants.Fly] = 40;
                    testCases[CreatureConstants.PitFiend][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.PitFiend][SpeedConstants.Fly] = 60;
                    testCases[CreatureConstants.Pixie][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Pixie][SpeedConstants.Fly] = 60;
                    testCases[CreatureConstants.Pixie_WithIrresistableDance][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Pixie_WithIrresistableDance][SpeedConstants.Fly] = 60;
                    testCases[CreatureConstants.Porpoise][SpeedConstants.Swim] = 80;
                    testCases[CreatureConstants.Quasit][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Quasit][SpeedConstants.Fly] = 50;
                    testCases[CreatureConstants.Rakshasa][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Rat_Swarm][SpeedConstants.Land] = 15;
                    testCases[CreatureConstants.Rat_Swarm][SpeedConstants.Climb] = 15;
                    testCases[CreatureConstants.Retriever][SpeedConstants.Land] = 50;
                    testCases[CreatureConstants.Sahuagin][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Sahuagin][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Salamander_Average][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Salamander_Flamebrother][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Salamander_Noble][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Satyr][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Satyr_WithPipes][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.Scorpionfolk][SpeedConstants.Land] = 40;
                    testCases[CreatureConstants.SeaHag][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.SeaHag][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Shark_Dire][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Shark_Huge][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Shark_Large][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Shark_Medium][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Skum][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Skum][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Slaad_Blue][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Slaad_Death][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Slaad_Gray][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Slaad_Green][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Slaad_Red][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Spider_Swarm][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Spider_Swarm][SpeedConstants.Climb] = 20;
                    testCases[CreatureConstants.Squid][SpeedConstants.Swim] = 60;
                    testCases[CreatureConstants.Squid_Giant][SpeedConstants.Swim] = 80;
                    testCases[CreatureConstants.Succubus][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Succubus][SpeedConstants.Fly] = 50;
                    testCases[CreatureConstants.Tiefling][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Tojanida_Adult][SpeedConstants.Land] = 10;
                    testCases[CreatureConstants.Tojanida_Adult][SpeedConstants.Swim] = 90;
                    testCases[CreatureConstants.Tojanida_Elder][SpeedConstants.Land] = 10;
                    testCases[CreatureConstants.Tojanida_Elder][SpeedConstants.Swim] = 90;
                    testCases[CreatureConstants.Tojanida_Juvenile][SpeedConstants.Land] = 10;
                    testCases[CreatureConstants.Tojanida_Juvenile][SpeedConstants.Swim] = 90;
                    testCases[CreatureConstants.Troglodyte][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Troll][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Troll_Scrag][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Troll_Scrag][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Vrock][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.Vrock][SpeedConstants.Fly] = 50;
                    testCases[CreatureConstants.Whale_Baleen][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Whale_Cachalot][SpeedConstants.Swim] = 40;
                    testCases[CreatureConstants.Whale_Orca][SpeedConstants.Swim] = 50;
                    testCases[CreatureConstants.Xorn_Average][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Xorn_Average][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Xorn_Elder][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Xorn_Elder][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.Xorn_Minor][SpeedConstants.Land] = 20;
                    testCases[CreatureConstants.Xorn_Minor][SpeedConstants.Burrow] = 20;
                    testCases[CreatureConstants.YuanTi_Abomination][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.YuanTi_Abomination][SpeedConstants.Swim] = 20;
                    testCases[CreatureConstants.YuanTi_Halfblood][SpeedConstants.Land] = 30;
                    testCases[CreatureConstants.YuanTi_Pureblood][SpeedConstants.Land] = 30;

                    foreach (var testCase in testCases)
                    {
                        var speeds = testCase.Value.Select(kvp => $"{kvp.Key}:{kvp.Value}");
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"Speeds({testCase.Key}, [{string.Join("], [", speeds)}])");
                    }
                }
            }
        }

        [Test]
        public void AllCreaturesHaveAtLeast1Speed()
        {
            var creatures = CreatureConstants.All();
            AssertCollection(table.Keys, creatures);

            foreach (var creature in creatures)
            {
                var speeds = GetCollection(creature);
                Assert.That(speeds, Is.Not.Empty, creature);
            }
        }

        [Test]
        public void AllAquaticCreaturesHaveSwimSpeeds()
        {
            var aquaticCreatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Aquatic);

            AssertCollection(aquaticCreatures.Intersect(table.Keys), aquaticCreatures);

            foreach (var creature in aquaticCreatures)
            {
                var speeds = TypesAndAmountsSelector.Select(tableName, creature);
                var aquaticSpeed = speeds.FirstOrDefault(s => s.Type == SpeedConstants.Swim);

                Assert.That(aquaticSpeed, Is.Not.Null, creature);
                Assert.That(aquaticSpeed.Type, Is.EqualTo(SpeedConstants.Swim), creature);
                Assert.That(aquaticSpeed.Amount, Is.Positive, creature);
            }
        }

        [Test]
        public void AllWaterCreaturesHaveSwimSpeeds()
        {
            var aquaticCreatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Water);

            AssertCollection(aquaticCreatures.Intersect(table.Keys), aquaticCreatures);

            foreach (var creature in aquaticCreatures)
            {
                var speeds = TypesAndAmountsSelector.Select(tableName, creature);
                var aquaticSpeed = speeds.FirstOrDefault(s => s.Type == SpeedConstants.Swim);

                Assert.That(aquaticSpeed, Is.Not.Null, creature);
                Assert.That(aquaticSpeed.Type, Is.EqualTo(SpeedConstants.Swim), creature);
                Assert.That(aquaticSpeed.Amount, Is.Positive, creature);
            }
        }

        [Test]
        public void AllAirCreaturesHaveFlySpeeds()
        {
            var aquaticCreatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Air);

            AssertCollection(aquaticCreatures.Intersect(table.Keys), aquaticCreatures);

            foreach (var creature in aquaticCreatures)
            {
                var speeds = TypesAndAmountsSelector.Select(tableName, creature);
                var aquaticSpeed = speeds.FirstOrDefault(s => s.Type == SpeedConstants.Fly);

                Assert.That(aquaticSpeed, Is.Not.Null, creature);
                Assert.That(aquaticSpeed.Type, Is.EqualTo(SpeedConstants.Fly), creature);
                Assert.That(aquaticSpeed.Amount, Is.Positive, creature);
            }
        }

        [Test]
        public void AllEarthCreaturesHaveBurrowSpeeds()
        {
            var aquaticCreatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, CreatureConstants.Types.Subtypes.Earth);

            AssertCollection(aquaticCreatures.Intersect(table.Keys), aquaticCreatures);

            foreach (var creature in aquaticCreatures)
            {
                var speeds = TypesAndAmountsSelector.Select(tableName, creature);
                var aquaticSpeed = speeds.FirstOrDefault(s => s.Type == SpeedConstants.Burrow);

                Assert.That(aquaticSpeed, Is.Not.Null, creature);
                Assert.That(aquaticSpeed.Type, Is.EqualTo(SpeedConstants.Burrow), creature);
                Assert.That(aquaticSpeed.Amount, Is.Positive, creature);
            }
        }
    }
}