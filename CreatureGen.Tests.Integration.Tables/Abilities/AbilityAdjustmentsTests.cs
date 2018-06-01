using CreatureGen.Abilities;
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

namespace CreatureGen.Tests.Integration.Tables.Abilities
{
    [TestFixture]
    public class AbilityAdjustmentsTests : TypesAndAmountsTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        internal ITypeAndAmountSelector TypesAndAmountsSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.TypeAndAmount.AbilityAdjustments;
            }
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
            var creatures = CreatureConstants.All();
            var names = creatures.Union(new[] { GroupConstants.All });

            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(AbilityAdjustmentsTestData), "TestCases")]
        public void AbilityAdjustment(string name, Dictionary<string, int> typesAndAmounts)
        {
            Assert.That(typesAndAmounts, Is.Not.Empty, name);
            AssertTypesAndAmounts(name, typesAndAmounts);
        }

        public class AbilityAdjustmentsTestData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    var testCases = new Dictionary<string, Dictionary<string, int>>();
                    testCases[GroupConstants.All] = new Dictionary<string, int>();
                    testCases[GroupConstants.All][AbilityConstants.Charisma] = 0;
                    testCases[GroupConstants.All][AbilityConstants.Constitution] = 0;
                    testCases[GroupConstants.All][AbilityConstants.Dexterity] = 0;
                    testCases[GroupConstants.All][AbilityConstants.Intelligence] = 0;
                    testCases[GroupConstants.All][AbilityConstants.Strength] = 0;
                    testCases[GroupConstants.All][AbilityConstants.Wisdom] = 0;

                    var creatures = CreatureConstants.All();

                    foreach (var creature in creatures)
                    {
                        testCases[creature] = new Dictionary<string, int>();
                    }

                    testCases[CreatureConstants.Aasimar][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Aasimar][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Aasimar][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Aasimar][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Aasimar][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Aasimar][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Aboleth][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Aboleth][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Aboleth][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Aboleth][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Aboleth][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Aboleth][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Achaierai][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Achaierai][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Achaierai][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Achaierai][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Achaierai][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Achaierai][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Allip][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Allip][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Allip][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Allip][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Androsphinx][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Androsphinx][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Androsphinx][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Androsphinx][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Androsphinx][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.Androsphinx][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Angel_AstralDeva][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Angel_AstralDeva][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Angel_AstralDeva][AbilityConstants.Dexterity] = 8;
                    testCases[CreatureConstants.Angel_AstralDeva][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Angel_AstralDeva][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Angel_AstralDeva][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Angel_Planetar][AbilityConstants.Charisma] = 12;
                    testCases[CreatureConstants.Angel_Planetar][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Angel_Planetar][AbilityConstants.Dexterity] = 8;
                    testCases[CreatureConstants.Angel_Planetar][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Angel_Planetar][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.Angel_Planetar][AbilityConstants.Wisdom] = 12;
                    testCases[CreatureConstants.Angel_Solar][AbilityConstants.Charisma] = 14;
                    testCases[CreatureConstants.Angel_Solar][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Angel_Solar][AbilityConstants.Dexterity] = 10;
                    testCases[CreatureConstants.Angel_Solar][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Angel_Solar][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Angel_Solar][AbilityConstants.Wisdom] = 14;
                    testCases[CreatureConstants.AnimatedObject_Colossal][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Colossal][AbilityConstants.Dexterity] = -6;
                    testCases[CreatureConstants.AnimatedObject_Colossal][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.AnimatedObject_Colossal][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.AnimatedObject_Gargantuan][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Gargantuan][AbilityConstants.Dexterity] = -4;
                    testCases[CreatureConstants.AnimatedObject_Gargantuan][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.AnimatedObject_Gargantuan][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.AnimatedObject_Huge][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Huge][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.AnimatedObject_Huge][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.AnimatedObject_Huge][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.AnimatedObject_Large][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Large][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.AnimatedObject_Large][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.AnimatedObject_Large][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.AnimatedObject_Medium][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Medium][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.AnimatedObject_Medium][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.AnimatedObject_Medium][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.AnimatedObject_Small][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Small][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.AnimatedObject_Small][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.AnimatedObject_Small][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.AnimatedObject_Tiny][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.AnimatedObject_Tiny][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.AnimatedObject_Tiny][AbilityConstants.Strength] = -2;
                    testCases[CreatureConstants.AnimatedObject_Tiny][AbilityConstants.Wisdom] = -10;
                    testCases[CreatureConstants.Ankheg][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Ankheg][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Ankheg][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Ankheg][AbilityConstants.Intelligence] = -10;
                    testCases[CreatureConstants.Ankheg][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.Ankheg][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Annis][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Annis][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Annis][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Annis][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Annis][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.Annis][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Ant_Giant_Queen][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Ant_Giant_Queen][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Ant_Giant_Queen][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.Ant_Giant_Queen][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Ant_Giant_Queen][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Ant_Giant_Soldier][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Ant_Giant_Soldier][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Ant_Giant_Soldier][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Ant_Giant_Soldier][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Ant_Giant_Soldier][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Ant_Giant_Worker][AbilityConstants.Charisma] = -2;
                    testCases[CreatureConstants.Ant_Giant_Worker][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Ant_Giant_Worker][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Ant_Giant_Worker][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Ant_Giant_Worker][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Ape][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Ape][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Ape][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Ape][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Ape][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.Ape][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Ape_Dire][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Ape_Dire][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Ape_Dire][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Ape_Dire][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Ape_Dire][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Ape_Dire][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Aranea][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Aranea][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Aranea][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Aranea][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Aranea][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Aranea][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Arrowhawk_Adult][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Arrowhawk_Adult][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Arrowhawk_Adult][AbilityConstants.Dexterity] = 10;
                    testCases[CreatureConstants.Arrowhawk_Adult][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Arrowhawk_Adult][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Arrowhawk_Adult][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Arrowhawk_Elder][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Arrowhawk_Elder][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Arrowhawk_Elder][AbilityConstants.Dexterity] = 10;
                    testCases[CreatureConstants.Arrowhawk_Elder][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Arrowhawk_Elder][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Arrowhawk_Elder][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Arrowhawk_Juvenile][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Arrowhawk_Juvenile][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Arrowhawk_Juvenile][AbilityConstants.Dexterity] = 10;
                    testCases[CreatureConstants.Arrowhawk_Juvenile][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Arrowhawk_Juvenile][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Arrowhawk_Juvenile][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.AssassinVine][AbilityConstants.Charisma] = -2;
                    testCases[CreatureConstants.AssassinVine][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.AssassinVine][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.AssassinVine][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.AssassinVine][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Athach][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Athach][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Athach][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Athach][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Athach][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Athach][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Avoral][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Avoral][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Avoral][AbilityConstants.Dexterity] = 12;
                    testCases[CreatureConstants.Avoral][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Avoral][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Avoral][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Azer][AbilityConstants.Charisma] = -2;
                    testCases[CreatureConstants.Azer][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Azer][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Azer][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Azer][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Azer][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Babau][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Babau][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Babau][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Babau][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Babau][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.Babau][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Baboon][AbilityConstants.Charisma] = -6;
                    testCases[CreatureConstants.Baboon][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Baboon][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Baboon][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Baboon][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Baboon][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Badger][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Badger][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Badger][AbilityConstants.Dexterity] = 6;
                    testCases[CreatureConstants.Badger][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Badger][AbilityConstants.Strength] = -2;
                    testCases[CreatureConstants.Badger][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Balor][AbilityConstants.Charisma] = 16;
                    testCases[CreatureConstants.Balor][AbilityConstants.Constitution] = 20;
                    testCases[CreatureConstants.Balor][AbilityConstants.Dexterity] = 14;
                    testCases[CreatureConstants.Balor][AbilityConstants.Intelligence] = 14;
                    testCases[CreatureConstants.Balor][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Balor][AbilityConstants.Wisdom] = 14;
                    testCases[CreatureConstants.Barghest][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Barghest][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Barghest][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Barghest][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Barghest][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Barghest][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Barghest_Greater][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Barghest_Greater][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Barghest_Greater][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Barghest_Greater][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Barghest_Greater][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.Barghest_Greater][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Basilisk][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Basilisk][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Basilisk][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.Basilisk][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Basilisk][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Basilisk][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Basilisk_AbyssalGreater][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Basilisk_AbyssalGreater][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Basilisk_AbyssalGreater][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.Basilisk_AbyssalGreater][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Basilisk_AbyssalGreater][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.Basilisk_AbyssalGreater][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Bat][AbilityConstants.Charisma] = -6;
                    testCases[CreatureConstants.Bat][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Bat][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Bat][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Bat][AbilityConstants.Strength] = -10;
                    testCases[CreatureConstants.Bat][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Bat_Swarm][AbilityConstants.Charisma] = -6;
                    testCases[CreatureConstants.Bat_Swarm][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Bat_Swarm][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Bat_Swarm][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Bat_Swarm][AbilityConstants.Strength] = -8;
                    testCases[CreatureConstants.Bat_Swarm][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Bear_Black][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Bear_Black][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Bear_Black][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Bear_Black][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Bear_Black][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Bear_Black][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Bebilith][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Bebilith][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Bebilith][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Bebilith][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Bebilith][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Bebilith][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Behir][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Behir][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Behir][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Behir][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Behir][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Behir][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Beholder][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Beholder][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Beholder][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Beholder][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Beholder][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Beholder][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Beholder_Gauth][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Beholder_Gauth][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Beholder_Gauth][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Beholder_Gauth][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Beholder_Gauth][AbilityConstants.Strength] = -2;
                    testCases[CreatureConstants.Beholder_Gauth][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Belker][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Belker][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Belker][AbilityConstants.Dexterity] = 10;
                    testCases[CreatureConstants.Belker][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Belker][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Belker][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Bison][AbilityConstants.Charisma] = -6;
                    testCases[CreatureConstants.Bison][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Bison][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Bison][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Bison][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Bison][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.CarrionCrawler][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.CarrionCrawler][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.CarrionCrawler][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.CarrionCrawler][AbilityConstants.Intelligence] = -10;
                    testCases[CreatureConstants.CarrionCrawler][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.CarrionCrawler][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Centipede_Swarm][AbilityConstants.Charisma] = -8;
                    testCases[CreatureConstants.Centipede_Swarm][AbilityConstants.Constitution] = -2;
                    testCases[CreatureConstants.Centipede_Swarm][AbilityConstants.Dexterity] = 8;
                    testCases[CreatureConstants.Centipede_Swarm][AbilityConstants.Strength] = -10;
                    testCases[CreatureConstants.Centipede_Swarm][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Criosphinx][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Criosphinx][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Criosphinx][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Criosphinx][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Criosphinx][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Criosphinx][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dretch][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Dretch][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dretch][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dretch][AbilityConstants.Intelligence] = -6;
                    testCases[CreatureConstants.Dretch][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Dretch][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_Black_Wyrmling][AbilityConstants.Charisma] = -2;
                    testCases[CreatureConstants.Dragon_Black_Wyrmling][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Dragon_Black_Wyrmling][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Black_Wyrmling][AbilityConstants.Intelligence] = -2;
                    testCases[CreatureConstants.Dragon_Black_Wyrmling][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Dragon_Black_Wyrmling][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_Black_VeryYoung][AbilityConstants.Charisma] = -2;
                    testCases[CreatureConstants.Dragon_Black_VeryYoung][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Dragon_Black_VeryYoung][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Black_VeryYoung][AbilityConstants.Intelligence] = -2;
                    testCases[CreatureConstants.Dragon_Black_VeryYoung][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Dragon_Black_VeryYoung][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_Black_Young][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Dragon_Black_Young][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Black_Young][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Black_Young][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Dragon_Black_Young][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Dragon_Black_Young][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_Black_Juvenile][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Dragon_Black_Juvenile][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Black_Juvenile][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Black_Juvenile][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Dragon_Black_Juvenile][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Dragon_Black_Juvenile][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_Black_YoungAdult][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_Black_YoungAdult][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Dragon_Black_YoungAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Black_YoungAdult][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Dragon_Black_YoungAdult][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Dragon_Black_YoungAdult][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Dragon_Black_Adult][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_Black_Adult][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Dragon_Black_Adult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Black_Adult][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Dragon_Black_Adult][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Dragon_Black_Adult][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Dragon_Black_MatureAdult][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Black_MatureAdult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Black_MatureAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Black_MatureAdult][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Black_MatureAdult][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Dragon_Black_MatureAdult][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Black_Old][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Black_Old][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Black_Old][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Black_Old][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Black_Old][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Dragon_Black_Old][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Black_VeryOld][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Black_VeryOld][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Black_VeryOld][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Black_VeryOld][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Black_VeryOld][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Dragon_Black_VeryOld][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Black_Ancient][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Black_Ancient][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Black_Ancient][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Black_Ancient][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Black_Ancient][AbilityConstants.Strength] = 22;
                    testCases[CreatureConstants.Dragon_Black_Ancient][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Black_Wyrm][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Black_Wyrm][AbilityConstants.Constitution] = 14;
                    testCases[CreatureConstants.Dragon_Black_Wyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Black_Wyrm][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Black_Wyrm][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Dragon_Black_Wyrm][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Black_GreatWyrm][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Black_GreatWyrm][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Dragon_Black_GreatWyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Black_GreatWyrm][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Black_GreatWyrm][AbilityConstants.Strength] = 26;
                    testCases[CreatureConstants.Dragon_Black_GreatWyrm][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Blue_Wyrmling][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Dragon_Blue_Wyrmling][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Dragon_Blue_Wyrmling][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Blue_Wyrmling][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Dragon_Blue_Wyrmling][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Dragon_Blue_Wyrmling][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_Blue_VeryYoung][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Dragon_Blue_VeryYoung][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Blue_VeryYoung][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Blue_VeryYoung][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Dragon_Blue_VeryYoung][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Dragon_Blue_VeryYoung][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_Blue_Young][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_Blue_Young][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Blue_Young][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Blue_Young][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Dragon_Blue_Young][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Dragon_Blue_Young][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Dragon_Blue_Juvenile][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Blue_Juvenile][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Dragon_Blue_Juvenile][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Blue_Juvenile][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Blue_Juvenile][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Dragon_Blue_Juvenile][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Blue_YoungAdult][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Blue_YoungAdult][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Dragon_Blue_YoungAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Blue_YoungAdult][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Blue_YoungAdult][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Dragon_Blue_YoungAdult][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Blue_Adult][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Blue_Adult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Blue_Adult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Blue_Adult][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Blue_Adult][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Dragon_Blue_Adult][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Blue_MatureAdult][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Blue_MatureAdult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Blue_MatureAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Blue_MatureAdult][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Blue_MatureAdult][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Dragon_Blue_MatureAdult][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Blue_Old][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Blue_Old][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Blue_Old][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Blue_Old][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Blue_Old][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Dragon_Blue_Old][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Blue_VeryOld][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Blue_VeryOld][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Blue_VeryOld][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Blue_VeryOld][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Blue_VeryOld][AbilityConstants.Strength] = 22;
                    testCases[CreatureConstants.Dragon_Blue_VeryOld][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Blue_Ancient][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Blue_Ancient][AbilityConstants.Constitution] = 14;
                    testCases[CreatureConstants.Dragon_Blue_Ancient][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Blue_Ancient][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Blue_Ancient][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Dragon_Blue_Ancient][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Blue_Wyrm][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Blue_Wyrm][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Dragon_Blue_Wyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Blue_Wyrm][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Blue_Wyrm][AbilityConstants.Strength] = 26;
                    testCases[CreatureConstants.Dragon_Blue_Wyrm][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Blue_GreatWyrm][AbilityConstants.Charisma] = 12;
                    testCases[CreatureConstants.Dragon_Blue_GreatWyrm][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Dragon_Blue_GreatWyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Blue_GreatWyrm][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Dragon_Blue_GreatWyrm][AbilityConstants.Strength] = 28;
                    testCases[CreatureConstants.Dragon_Blue_GreatWyrm][AbilityConstants.Wisdom] = 12;
                    testCases[CreatureConstants.Dragon_Green_Wyrmling][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Dragon_Green_Wyrmling][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Dragon_Green_Wyrmling][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Green_Wyrmling][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Dragon_Green_Wyrmling][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Dragon_Green_Wyrmling][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_Green_VeryYoung][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Dragon_Green_VeryYoung][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Green_VeryYoung][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Green_VeryYoung][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Dragon_Green_VeryYoung][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Dragon_Green_VeryYoung][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_Green_Young][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_Green_Young][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Green_Young][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Green_Young][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Dragon_Green_Young][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Dragon_Green_Young][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Dragon_Green_Juvenile][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Green_Juvenile][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Dragon_Green_Juvenile][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Green_Juvenile][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Green_Juvenile][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Dragon_Green_Juvenile][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Green_YoungAdult][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Green_YoungAdult][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Dragon_Green_YoungAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Green_YoungAdult][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Green_YoungAdult][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Dragon_Green_YoungAdult][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Green_Adult][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Green_Adult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Green_Adult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Green_Adult][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Green_Adult][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Dragon_Green_Adult][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Green_MatureAdult][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Green_MatureAdult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Green_MatureAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Green_MatureAdult][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Green_MatureAdult][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Dragon_Green_MatureAdult][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Green_Old][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Green_Old][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Green_Old][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Green_Old][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Green_Old][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Dragon_Green_Old][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Green_VeryOld][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Green_VeryOld][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Green_VeryOld][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Green_VeryOld][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Green_VeryOld][AbilityConstants.Strength] = 22;
                    testCases[CreatureConstants.Dragon_Green_VeryOld][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Green_Ancient][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Green_Ancient][AbilityConstants.Constitution] = 14;
                    testCases[CreatureConstants.Dragon_Green_Ancient][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Green_Ancient][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Green_Ancient][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Dragon_Green_Ancient][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Green_Wyrm][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Green_Wyrm][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Dragon_Green_Wyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Green_Wyrm][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Green_Wyrm][AbilityConstants.Strength] = 26;
                    testCases[CreatureConstants.Dragon_Green_Wyrm][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Green_GreatWyrm][AbilityConstants.Charisma] = 12;
                    testCases[CreatureConstants.Dragon_Green_GreatWyrm][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Dragon_Green_GreatWyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Green_GreatWyrm][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Dragon_Green_GreatWyrm][AbilityConstants.Strength] = 28;
                    testCases[CreatureConstants.Dragon_Green_GreatWyrm][AbilityConstants.Wisdom] = 12;
                    testCases[CreatureConstants.Dragon_Red_Wyrmling][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Dragon_Red_Wyrmling][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Red_Wyrmling][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Red_Wyrmling][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Dragon_Red_Wyrmling][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Dragon_Red_Wyrmling][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_Red_VeryYoung][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_Red_VeryYoung][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Dragon_Red_VeryYoung][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Red_VeryYoung][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Dragon_Red_VeryYoung][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.Dragon_Red_VeryYoung][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Dragon_Red_Young][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_Red_Young][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Dragon_Red_Young][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Red_Young][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Dragon_Red_Young][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.Dragon_Red_Young][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Dragon_Red_Juvenile][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Red_Juvenile][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Dragon_Red_Juvenile][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Red_Juvenile][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Red_Juvenile][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Dragon_Red_Juvenile][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Red_YoungAdult][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Red_YoungAdult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Red_YoungAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Red_YoungAdult][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Red_YoungAdult][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Dragon_Red_YoungAdult][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Red_Adult][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Red_Adult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Red_Adult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Red_Adult][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Red_Adult][AbilityConstants.Strength] = 22;
                    testCases[CreatureConstants.Dragon_Red_Adult][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Red_MatureAdult][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Red_MatureAdult][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Red_MatureAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Red_MatureAdult][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Red_MatureAdult][AbilityConstants.Strength] = 22;
                    testCases[CreatureConstants.Dragon_Red_MatureAdult][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Red_Old][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Red_Old][AbilityConstants.Constitution] = 14;
                    testCases[CreatureConstants.Dragon_Red_Old][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Red_Old][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Red_Old][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Dragon_Red_Old][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Red_VeryOld][AbilityConstants.Charisma] = 12;
                    testCases[CreatureConstants.Dragon_Red_VeryOld][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Dragon_Red_VeryOld][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Red_VeryOld][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Dragon_Red_VeryOld][AbilityConstants.Strength] = 26;
                    testCases[CreatureConstants.Dragon_Red_VeryOld][AbilityConstants.Wisdom] = 12;
                    testCases[CreatureConstants.Dragon_Red_Ancient][AbilityConstants.Charisma] = 14;
                    testCases[CreatureConstants.Dragon_Red_Ancient][AbilityConstants.Constitution] = 18;
                    testCases[CreatureConstants.Dragon_Red_Ancient][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Red_Ancient][AbilityConstants.Intelligence] = 14;
                    testCases[CreatureConstants.Dragon_Red_Ancient][AbilityConstants.Strength] = 28;
                    testCases[CreatureConstants.Dragon_Red_Ancient][AbilityConstants.Wisdom] = 14;
                    testCases[CreatureConstants.Dragon_Red_Wyrm][AbilityConstants.Charisma] = 14;
                    testCases[CreatureConstants.Dragon_Red_Wyrm][AbilityConstants.Constitution] = 20;
                    testCases[CreatureConstants.Dragon_Red_Wyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Red_Wyrm][AbilityConstants.Intelligence] = 14;
                    testCases[CreatureConstants.Dragon_Red_Wyrm][AbilityConstants.Strength] = 30;
                    testCases[CreatureConstants.Dragon_Red_Wyrm][AbilityConstants.Wisdom] = 14;
                    testCases[CreatureConstants.Dragon_Red_GreatWyrm][AbilityConstants.Charisma] = 16;
                    testCases[CreatureConstants.Dragon_Red_GreatWyrm][AbilityConstants.Constitution] = 20;
                    testCases[CreatureConstants.Dragon_Red_GreatWyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Red_GreatWyrm][AbilityConstants.Intelligence] = 16;
                    testCases[CreatureConstants.Dragon_Red_GreatWyrm][AbilityConstants.Strength] = 34;
                    testCases[CreatureConstants.Dragon_Red_GreatWyrm][AbilityConstants.Wisdom] = 16;
                    testCases[CreatureConstants.Dragon_White_Wyrmling][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Dragon_White_Wyrmling][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Dragon_White_Wyrmling][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_White_Wyrmling][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Dragon_White_Wyrmling][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Dragon_White_Wyrmling][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_White_VeryYoung][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Dragon_White_VeryYoung][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Dragon_White_VeryYoung][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_White_VeryYoung][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Dragon_White_VeryYoung][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Dragon_White_VeryYoung][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_White_Young][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Dragon_White_Young][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_White_Young][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_White_Young][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Dragon_White_Young][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Dragon_White_Young][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_White_Juvenile][AbilityConstants.Charisma] = -2;
                    testCases[CreatureConstants.Dragon_White_Juvenile][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_White_Juvenile][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_White_Juvenile][AbilityConstants.Intelligence] = -2;
                    testCases[CreatureConstants.Dragon_White_Juvenile][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Dragon_White_Juvenile][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_White_YoungAdult][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Dragon_White_YoungAdult][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Dragon_White_YoungAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_White_YoungAdult][AbilityConstants.Intelligence] = -2;
                    testCases[CreatureConstants.Dragon_White_YoungAdult][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Dragon_White_YoungAdult][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_White_Adult][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_White_Adult][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Dragon_White_Adult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_White_Adult][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Dragon_White_Adult][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Dragon_White_Adult][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_White_MatureAdult][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_White_MatureAdult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_White_MatureAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_White_MatureAdult][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Dragon_White_MatureAdult][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Dragon_White_MatureAdult][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Dragon_White_Old][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_White_Old][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_White_Old][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_White_Old][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Dragon_White_Old][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Dragon_White_Old][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Dragon_White_VeryOld][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_White_VeryOld][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_White_VeryOld][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_White_VeryOld][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_White_VeryOld][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Dragon_White_VeryOld][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_White_Ancient][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_White_Ancient][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_White_Ancient][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_White_Ancient][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_White_Ancient][AbilityConstants.Strength] = 22;
                    testCases[CreatureConstants.Dragon_White_Ancient][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_White_Wyrm][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_White_Wyrm][AbilityConstants.Constitution] = 14;
                    testCases[CreatureConstants.Dragon_White_Wyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_White_Wyrm][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_White_Wyrm][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Dragon_White_Wyrm][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_White_GreatWyrm][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_White_GreatWyrm][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Dragon_White_GreatWyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_White_GreatWyrm][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_White_GreatWyrm][AbilityConstants.Strength] = 26;
                    testCases[CreatureConstants.Dragon_White_GreatWyrm][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Brass_Wyrmling][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Dragon_Brass_Wyrmling][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Dragon_Brass_Wyrmling][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Brass_Wyrmling][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Dragon_Brass_Wyrmling][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Dragon_Brass_Wyrmling][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_Brass_VeryYoung][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Dragon_Brass_VeryYoung][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Dragon_Brass_VeryYoung][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Brass_VeryYoung][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Dragon_Brass_VeryYoung][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Dragon_Brass_VeryYoung][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Dragon_Brass_Young][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_Brass_Young][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Brass_Young][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Brass_Young][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Dragon_Brass_Young][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Dragon_Brass_Young][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Dragon_Brass_Juvenile][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_Brass_Juvenile][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Brass_Juvenile][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Brass_Juvenile][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Dragon_Brass_Juvenile][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Dragon_Brass_Juvenile][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Dragon_Brass_YoungAdult][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Brass_YoungAdult][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Dragon_Brass_YoungAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Brass_YoungAdult][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Brass_YoungAdult][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Dragon_Brass_YoungAdult][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Brass_Adult][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Brass_Adult][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Dragon_Brass_Adult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Brass_Adult][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Brass_Adult][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Dragon_Brass_Adult][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Brass_MatureAdult][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Brass_MatureAdult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Brass_MatureAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Brass_MatureAdult][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Brass_MatureAdult][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Dragon_Brass_MatureAdult][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Brass_Old][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Brass_Old][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Brass_Old][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Brass_Old][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Brass_Old][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Dragon_Brass_Old][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Brass_VeryOld][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Brass_VeryOld][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Brass_VeryOld][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Brass_VeryOld][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Brass_VeryOld][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Dragon_Brass_VeryOld][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Brass_Ancient][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Brass_Ancient][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Brass_Ancient][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Brass_Ancient][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Brass_Ancient][AbilityConstants.Strength] = 22;
                    testCases[CreatureConstants.Dragon_Brass_Ancient][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Brass_Wyrm][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Brass_Wyrm][AbilityConstants.Constitution] = 14;
                    testCases[CreatureConstants.Dragon_Brass_Wyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Brass_Wyrm][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Brass_Wyrm][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Dragon_Brass_Wyrm][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Brass_GreatWyrm][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Brass_GreatWyrm][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Dragon_Brass_GreatWyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Brass_GreatWyrm][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Brass_GreatWyrm][AbilityConstants.Strength] = 26;
                    testCases[CreatureConstants.Dragon_Brass_GreatWyrm][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrmling][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrmling][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrmling][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrmling][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrmling][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrmling][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Bronze_VeryYoung][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Bronze_VeryYoung][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Bronze_VeryYoung][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Bronze_VeryYoung][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Bronze_VeryYoung][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Dragon_Bronze_VeryYoung][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Bronze_Young][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Bronze_Young][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Bronze_Young][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Bronze_Young][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Bronze_Young][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Dragon_Bronze_Young][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Bronze_Juvenile][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Bronze_Juvenile][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Dragon_Bronze_Juvenile][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Bronze_Juvenile][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Bronze_Juvenile][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Dragon_Bronze_Juvenile][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Bronze_YoungAdult][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Bronze_YoungAdult][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Dragon_Bronze_YoungAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Bronze_YoungAdult][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Bronze_YoungAdult][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Dragon_Bronze_YoungAdult][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Bronze_Adult][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Bronze_Adult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Bronze_Adult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Bronze_Adult][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Bronze_Adult][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Dragon_Bronze_Adult][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Bronze_MatureAdult][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Bronze_MatureAdult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Bronze_MatureAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Bronze_MatureAdult][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Bronze_MatureAdult][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Dragon_Bronze_MatureAdult][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Bronze_Old][AbilityConstants.Charisma] = 12;
                    testCases[CreatureConstants.Dragon_Bronze_Old][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Bronze_Old][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Bronze_Old][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Dragon_Bronze_Old][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Dragon_Bronze_Old][AbilityConstants.Wisdom] = 12;
                    testCases[CreatureConstants.Dragon_Bronze_VeryOld][AbilityConstants.Charisma] = 12;
                    testCases[CreatureConstants.Dragon_Bronze_VeryOld][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Bronze_VeryOld][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Bronze_VeryOld][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Dragon_Bronze_VeryOld][AbilityConstants.Strength] = 22;
                    testCases[CreatureConstants.Dragon_Bronze_VeryOld][AbilityConstants.Wisdom] = 12;
                    testCases[CreatureConstants.Dragon_Bronze_Ancient][AbilityConstants.Charisma] = 14;
                    testCases[CreatureConstants.Dragon_Bronze_Ancient][AbilityConstants.Constitution] = 14;
                    testCases[CreatureConstants.Dragon_Bronze_Ancient][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Bronze_Ancient][AbilityConstants.Intelligence] = 14;
                    testCases[CreatureConstants.Dragon_Bronze_Ancient][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Dragon_Bronze_Ancient][AbilityConstants.Wisdom] = 14;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrm][AbilityConstants.Charisma] = 16;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrm][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrm][AbilityConstants.Intelligence] = 16;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrm][AbilityConstants.Strength] = 26;
                    testCases[CreatureConstants.Dragon_Bronze_Wyrm][AbilityConstants.Wisdom] = 16;
                    testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][AbilityConstants.Charisma] = 16;
                    testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][AbilityConstants.Intelligence] = 16;
                    testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][AbilityConstants.Strength] = 28;
                    testCases[CreatureConstants.Dragon_Bronze_GreatWyrm][AbilityConstants.Wisdom] = 16;
                    testCases[CreatureConstants.Dragon_Copper_Wyrmling][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_Copper_Wyrmling][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Dragon_Copper_Wyrmling][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Copper_Wyrmling][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Dragon_Copper_Wyrmling][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Dragon_Copper_Wyrmling][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Dragon_Copper_VeryYoung][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Dragon_Copper_VeryYoung][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Dragon_Copper_VeryYoung][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Copper_VeryYoung][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Dragon_Copper_VeryYoung][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Dragon_Copper_VeryYoung][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Dragon_Copper_Young][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Copper_Young][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Copper_Young][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Copper_Young][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Copper_Young][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Dragon_Copper_Young][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Copper_Juvenile][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Copper_Juvenile][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Copper_Juvenile][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Copper_Juvenile][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Copper_Juvenile][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Dragon_Copper_Juvenile][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Copper_YoungAdult][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Copper_YoungAdult][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Dragon_Copper_YoungAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Copper_YoungAdult][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Copper_YoungAdult][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Dragon_Copper_YoungAdult][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Copper_Adult][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Copper_Adult][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Dragon_Copper_Adult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Copper_Adult][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Copper_Adult][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Dragon_Copper_Adult][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Copper_MatureAdult][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Copper_MatureAdult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Copper_MatureAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Copper_MatureAdult][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Copper_MatureAdult][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Dragon_Copper_MatureAdult][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Copper_Old][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Copper_Old][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Copper_Old][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Copper_Old][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Copper_Old][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Dragon_Copper_Old][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Copper_VeryOld][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Copper_VeryOld][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Copper_VeryOld][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Copper_VeryOld][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Copper_VeryOld][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Dragon_Copper_VeryOld][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Copper_Ancient][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Copper_Ancient][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Copper_Ancient][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Copper_Ancient][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Copper_Ancient][AbilityConstants.Strength] = 22;
                    testCases[CreatureConstants.Dragon_Copper_Ancient][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Copper_Wyrm][AbilityConstants.Charisma] = 12;
                    testCases[CreatureConstants.Dragon_Copper_Wyrm][AbilityConstants.Constitution] = 14;
                    testCases[CreatureConstants.Dragon_Copper_Wyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Copper_Wyrm][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Dragon_Copper_Wyrm][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Dragon_Copper_Wyrm][AbilityConstants.Wisdom] = 12;
                    testCases[CreatureConstants.Dragon_Copper_GreatWyrm][AbilityConstants.Charisma] = 12;
                    testCases[CreatureConstants.Dragon_Copper_GreatWyrm][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Dragon_Copper_GreatWyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Copper_GreatWyrm][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Dragon_Copper_GreatWyrm][AbilityConstants.Strength] = 26;
                    testCases[CreatureConstants.Dragon_Copper_GreatWyrm][AbilityConstants.Wisdom] = 12;
                    testCases[CreatureConstants.Dragon_Gold_Wyrmling][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Gold_Wyrmling][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Gold_Wyrmling][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Gold_Wyrmling][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Gold_Wyrmling][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Dragon_Gold_Wyrmling][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Gold_VeryYoung][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Gold_VeryYoung][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Dragon_Gold_VeryYoung][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Gold_VeryYoung][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Gold_VeryYoung][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.Dragon_Gold_VeryYoung][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Gold_Young][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Gold_Young][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Dragon_Gold_Young][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Gold_Young][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Gold_Young][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.Dragon_Gold_Young][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Gold_Juvenile][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Gold_Juvenile][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Dragon_Gold_Juvenile][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Gold_Juvenile][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Gold_Juvenile][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Dragon_Gold_Juvenile][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Gold_YoungAdult][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Gold_YoungAdult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Gold_YoungAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Gold_YoungAdult][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Gold_YoungAdult][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Dragon_Gold_YoungAdult][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Gold_Adult][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Gold_Adult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Gold_Adult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Gold_Adult][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Gold_Adult][AbilityConstants.Strength] = 22;
                    testCases[CreatureConstants.Dragon_Gold_Adult][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Gold_MatureAdult][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Gold_MatureAdult][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Gold_MatureAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Gold_MatureAdult][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Gold_MatureAdult][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Dragon_Gold_MatureAdult][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Gold_Old][AbilityConstants.Charisma] = 14;
                    testCases[CreatureConstants.Dragon_Gold_Old][AbilityConstants.Constitution] = 14;
                    testCases[CreatureConstants.Dragon_Gold_Old][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Gold_Old][AbilityConstants.Intelligence] = 14;
                    testCases[CreatureConstants.Dragon_Gold_Old][AbilityConstants.Strength] = 28;
                    testCases[CreatureConstants.Dragon_Gold_Old][AbilityConstants.Wisdom] = 14;
                    testCases[CreatureConstants.Dragon_Gold_VeryOld][AbilityConstants.Charisma] = 16;
                    testCases[CreatureConstants.Dragon_Gold_VeryOld][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Dragon_Gold_VeryOld][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Gold_VeryOld][AbilityConstants.Intelligence] = 16;
                    testCases[CreatureConstants.Dragon_Gold_VeryOld][AbilityConstants.Strength] = 30;
                    testCases[CreatureConstants.Dragon_Gold_VeryOld][AbilityConstants.Wisdom] = 16;
                    testCases[CreatureConstants.Dragon_Gold_Ancient][AbilityConstants.Charisma] = 18;
                    testCases[CreatureConstants.Dragon_Gold_Ancient][AbilityConstants.Constitution] = 18;
                    testCases[CreatureConstants.Dragon_Gold_Ancient][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Gold_Ancient][AbilityConstants.Intelligence] = 18;
                    testCases[CreatureConstants.Dragon_Gold_Ancient][AbilityConstants.Strength] = 32;
                    testCases[CreatureConstants.Dragon_Gold_Ancient][AbilityConstants.Wisdom] = 18;
                    testCases[CreatureConstants.Dragon_Gold_Wyrm][AbilityConstants.Charisma] = 20;
                    testCases[CreatureConstants.Dragon_Gold_Wyrm][AbilityConstants.Constitution] = 20;
                    testCases[CreatureConstants.Dragon_Gold_Wyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Gold_Wyrm][AbilityConstants.Intelligence] = 20;
                    testCases[CreatureConstants.Dragon_Gold_Wyrm][AbilityConstants.Strength] = 34;
                    testCases[CreatureConstants.Dragon_Gold_Wyrm][AbilityConstants.Wisdom] = 20;
                    testCases[CreatureConstants.Dragon_Gold_GreatWyrm][AbilityConstants.Charisma] = 22;
                    testCases[CreatureConstants.Dragon_Gold_GreatWyrm][AbilityConstants.Constitution] = 22;
                    testCases[CreatureConstants.Dragon_Gold_GreatWyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Gold_GreatWyrm][AbilityConstants.Intelligence] = 22;
                    testCases[CreatureConstants.Dragon_Gold_GreatWyrm][AbilityConstants.Strength] = 36;
                    testCases[CreatureConstants.Dragon_Gold_GreatWyrm][AbilityConstants.Wisdom] = 22;
                    testCases[CreatureConstants.Dragon_Silver_Wyrmling][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Silver_Wyrmling][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Dragon_Silver_Wyrmling][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Silver_Wyrmling][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Silver_Wyrmling][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Dragon_Silver_Wyrmling][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Silver_VeryYoung][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Dragon_Silver_VeryYoung][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Silver_VeryYoung][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Silver_VeryYoung][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Dragon_Silver_VeryYoung][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Dragon_Silver_VeryYoung][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Dragon_Silver_Young][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Dragon_Silver_Young][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Dragon_Silver_Young][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Silver_Young][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Dragon_Silver_Young][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Dragon_Silver_Young][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Dragon_Silver_Juvenile][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Silver_Juvenile][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Dragon_Silver_Juvenile][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Silver_Juvenile][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Silver_Juvenile][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Dragon_Silver_Juvenile][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Silver_YoungAdult][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Dragon_Silver_YoungAdult][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Dragon_Silver_YoungAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Silver_YoungAdult][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Dragon_Silver_YoungAdult][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Dragon_Silver_YoungAdult][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Dragon_Silver_Adult][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Silver_Adult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Silver_Adult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Silver_Adult][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Silver_Adult][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Dragon_Silver_Adult][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Silver_MatureAdult][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Dragon_Silver_MatureAdult][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Dragon_Silver_MatureAdult][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Silver_MatureAdult][AbilityConstants.Intelligence] = 10;
                    testCases[CreatureConstants.Dragon_Silver_MatureAdult][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Dragon_Silver_MatureAdult][AbilityConstants.Wisdom] = 10;
                    testCases[CreatureConstants.Dragon_Silver_Old][AbilityConstants.Charisma] = 12;
                    testCases[CreatureConstants.Dragon_Silver_Old][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Silver_Old][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Silver_Old][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Dragon_Silver_Old][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Dragon_Silver_Old][AbilityConstants.Wisdom] = 12;
                    testCases[CreatureConstants.Dragon_Silver_VeryOld][AbilityConstants.Charisma] = 14;
                    testCases[CreatureConstants.Dragon_Silver_VeryOld][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Dragon_Silver_VeryOld][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Silver_VeryOld][AbilityConstants.Intelligence] = 14;
                    testCases[CreatureConstants.Dragon_Silver_VeryOld][AbilityConstants.Strength] = 22;
                    testCases[CreatureConstants.Dragon_Silver_VeryOld][AbilityConstants.Wisdom] = 14;
                    testCases[CreatureConstants.Dragon_Silver_Ancient][AbilityConstants.Charisma] = 16;
                    testCases[CreatureConstants.Dragon_Silver_Ancient][AbilityConstants.Constitution] = 14;
                    testCases[CreatureConstants.Dragon_Silver_Ancient][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Silver_Ancient][AbilityConstants.Intelligence] = 16;
                    testCases[CreatureConstants.Dragon_Silver_Ancient][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Dragon_Silver_Ancient][AbilityConstants.Wisdom] = 16;
                    testCases[CreatureConstants.Dragon_Silver_Wyrm][AbilityConstants.Charisma] = 18;
                    testCases[CreatureConstants.Dragon_Silver_Wyrm][AbilityConstants.Constitution] = 18;
                    testCases[CreatureConstants.Dragon_Silver_Wyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Silver_Wyrm][AbilityConstants.Intelligence] = 18;
                    testCases[CreatureConstants.Dragon_Silver_Wyrm][AbilityConstants.Strength] = 28;
                    testCases[CreatureConstants.Dragon_Silver_Wyrm][AbilityConstants.Wisdom] = 18;
                    testCases[CreatureConstants.Dragon_Silver_GreatWyrm][AbilityConstants.Charisma] = 20;
                    testCases[CreatureConstants.Dragon_Silver_GreatWyrm][AbilityConstants.Constitution] = 20;
                    testCases[CreatureConstants.Dragon_Silver_GreatWyrm][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Dragon_Silver_GreatWyrm][AbilityConstants.Intelligence] = 20;
                    testCases[CreatureConstants.Dragon_Silver_GreatWyrm][AbilityConstants.Strength] = 32;
                    testCases[CreatureConstants.Dragon_Silver_GreatWyrm][AbilityConstants.Wisdom] = 20;
                    testCases[CreatureConstants.Elemental_Air_Elder][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Air_Elder][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Elemental_Air_Elder][AbilityConstants.Dexterity] = 22;
                    testCases[CreatureConstants.Elemental_Air_Elder][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Elemental_Air_Elder][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Elemental_Air_Elder][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Air_Greater][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Air_Greater][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Elemental_Air_Greater][AbilityConstants.Dexterity] = 20;
                    testCases[CreatureConstants.Elemental_Air_Greater][AbilityConstants.Intelligence] = -2;
                    testCases[CreatureConstants.Elemental_Air_Greater][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.Elemental_Air_Greater][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Air_Huge][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Air_Huge][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Elemental_Air_Huge][AbilityConstants.Dexterity] = 18;
                    testCases[CreatureConstants.Elemental_Air_Huge][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Elemental_Air_Huge][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Elemental_Air_Huge][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Air_Large][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Air_Large][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Elemental_Air_Large][AbilityConstants.Dexterity] = 14;
                    testCases[CreatureConstants.Elemental_Air_Large][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Elemental_Air_Large][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Elemental_Air_Large][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Air_Medium][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Air_Medium][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Elemental_Air_Medium][AbilityConstants.Dexterity] = 10;
                    testCases[CreatureConstants.Elemental_Air_Medium][AbilityConstants.Intelligence] = -6;
                    testCases[CreatureConstants.Elemental_Air_Medium][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Elemental_Air_Medium][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Air_Small][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Air_Small][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Elemental_Air_Small][AbilityConstants.Dexterity] = 6;
                    testCases[CreatureConstants.Elemental_Air_Small][AbilityConstants.Intelligence] = -6;
                    testCases[CreatureConstants.Elemental_Air_Small][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Elemental_Air_Small][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Elder][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Elder][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Elemental_Earth_Elder][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.Elemental_Earth_Elder][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Elder][AbilityConstants.Strength] = 22;
                    testCases[CreatureConstants.Elemental_Earth_Elder][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Greater][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Greater][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Elemental_Earth_Greater][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.Elemental_Earth_Greater][AbilityConstants.Intelligence] = -2;
                    testCases[CreatureConstants.Elemental_Earth_Greater][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Elemental_Earth_Greater][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Huge][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Huge][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Elemental_Earth_Huge][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.Elemental_Earth_Huge][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Elemental_Earth_Huge][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Elemental_Earth_Huge][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Large][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Large][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Elemental_Earth_Large][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.Elemental_Earth_Large][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Elemental_Earth_Large][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.Elemental_Earth_Large][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Medium][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Medium][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Elemental_Earth_Medium][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.Elemental_Earth_Medium][AbilityConstants.Intelligence] = -6;
                    testCases[CreatureConstants.Elemental_Earth_Medium][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.Elemental_Earth_Medium][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Small][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Earth_Small][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Elemental_Earth_Small][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.Elemental_Earth_Small][AbilityConstants.Intelligence] = -6;
                    testCases[CreatureConstants.Elemental_Earth_Small][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Elemental_Earth_Small][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Elder][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Elder][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Elemental_Fire_Elder][AbilityConstants.Dexterity] = 18;
                    testCases[CreatureConstants.Elemental_Fire_Elder][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Elemental_Fire_Elder][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Elemental_Fire_Elder][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Greater][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Greater][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Elemental_Fire_Greater][AbilityConstants.Dexterity] = 16;
                    testCases[CreatureConstants.Elemental_Fire_Greater][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Elemental_Fire_Greater][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.Elemental_Fire_Greater][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Huge][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Huge][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Elemental_Fire_Huge][AbilityConstants.Dexterity] = 14;
                    testCases[CreatureConstants.Elemental_Fire_Huge][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Elemental_Fire_Huge][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Elemental_Fire_Huge][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Large][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Large][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Elemental_Fire_Large][AbilityConstants.Dexterity] = 10;
                    testCases[CreatureConstants.Elemental_Fire_Large][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Elemental_Fire_Large][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Elemental_Fire_Large][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Medium][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Medium][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Elemental_Fire_Medium][AbilityConstants.Dexterity] = 6;
                    testCases[CreatureConstants.Elemental_Fire_Medium][AbilityConstants.Intelligence] = -6;
                    testCases[CreatureConstants.Elemental_Fire_Medium][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Elemental_Fire_Medium][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Small][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Small][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Small][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Elemental_Fire_Small][AbilityConstants.Intelligence] = -6;
                    testCases[CreatureConstants.Elemental_Fire_Small][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Elemental_Fire_Small][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Water_Elder][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Water_Elder][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Elemental_Water_Elder][AbilityConstants.Dexterity] = 12;
                    testCases[CreatureConstants.Elemental_Water_Elder][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Elemental_Water_Elder][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Elemental_Water_Elder][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Water_Greater][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Water_Greater][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Elemental_Water_Greater][AbilityConstants.Dexterity] = 10;
                    testCases[CreatureConstants.Elemental_Water_Greater][AbilityConstants.Intelligence] = -2;
                    testCases[CreatureConstants.Elemental_Water_Greater][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Elemental_Water_Greater][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Water_Huge][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Water_Huge][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Elemental_Water_Huge][AbilityConstants.Dexterity] = 8;
                    testCases[CreatureConstants.Elemental_Water_Huge][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Elemental_Water_Huge][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.Elemental_Water_Huge][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Water_Large][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Water_Large][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Elemental_Water_Large][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Elemental_Water_Large][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Elemental_Water_Large][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.Elemental_Water_Large][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Water_Medium][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Water_Medium][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Elemental_Water_Medium][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Elemental_Water_Medium][AbilityConstants.Intelligence] = -6;
                    testCases[CreatureConstants.Elemental_Water_Medium][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Elemental_Water_Medium][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elemental_Water_Small][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elemental_Water_Small][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Elemental_Water_Small][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Elemental_Water_Small][AbilityConstants.Intelligence] = -6;
                    testCases[CreatureConstants.Elemental_Water_Small][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Elemental_Water_Small][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elf_Aquatic][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elf_Aquatic][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Elf_Aquatic][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Elf_Aquatic][AbilityConstants.Intelligence] = -2;
                    testCases[CreatureConstants.Elf_Aquatic][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Elf_Aquatic][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elf_Drow][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Elf_Drow][AbilityConstants.Constitution] = -2;
                    testCases[CreatureConstants.Elf_Drow][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Elf_Drow][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Elf_Drow][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Elf_Drow][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elf_Gray][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elf_Gray][AbilityConstants.Constitution] = -2;
                    testCases[CreatureConstants.Elf_Gray][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Elf_Gray][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Elf_Gray][AbilityConstants.Strength] = -2;
                    testCases[CreatureConstants.Elf_Gray][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elf_Half][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elf_Half][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Elf_Half][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Elf_Half][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Elf_Half][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Elf_Half][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elf_High][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elf_High][AbilityConstants.Constitution] = -2;
                    testCases[CreatureConstants.Elf_High][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Elf_High][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Elf_High][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Elf_High][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elf_Wild][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elf_Wild][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Elf_Wild][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Elf_Wild][AbilityConstants.Intelligence] = -2;
                    testCases[CreatureConstants.Elf_Wild][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Elf_Wild][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Elf_Wood][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Elf_Wood][AbilityConstants.Constitution] = -2;
                    testCases[CreatureConstants.Elf_Wood][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Elf_Wood][AbilityConstants.Intelligence] = -2;
                    testCases[CreatureConstants.Elf_Wood][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Elf_Wood][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Glabrezu][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Glabrezu][AbilityConstants.Constitution] = 20;
                    testCases[CreatureConstants.Glabrezu][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Glabrezu][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Glabrezu][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Glabrezu][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.GreenHag][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.GreenHag][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.GreenHag][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.GreenHag][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.GreenHag][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.GreenHag][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Gynosphinx][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Gynosphinx][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Gynosphinx][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Gynosphinx][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Gynosphinx][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.Gynosphinx][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Hellwasp_Swarm][AbilityConstants.Charisma] = -2;
                    testCases[CreatureConstants.Hellwasp_Swarm][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Hellwasp_Swarm][AbilityConstants.Dexterity] = 12;
                    testCases[CreatureConstants.Hellwasp_Swarm][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Hellwasp_Swarm][AbilityConstants.Strength] = -10;
                    testCases[CreatureConstants.Hellwasp_Swarm][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Hezrou][AbilityConstants.Charisma] = 8;
                    testCases[CreatureConstants.Hezrou][AbilityConstants.Constitution] = 18;
                    testCases[CreatureConstants.Hezrou][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Hezrou][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Hezrou][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.Hezrou][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Hieracosphinx][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Hieracosphinx][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Hieracosphinx][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Hieracosphinx][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Hieracosphinx][AbilityConstants.Strength] = 10;
                    testCases[CreatureConstants.Hieracosphinx][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Human][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Human][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Human][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Human][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Human][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Human][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Locust_Swarm][AbilityConstants.Charisma] = -8;
                    testCases[CreatureConstants.Locust_Swarm][AbilityConstants.Constitution] = -2;
                    testCases[CreatureConstants.Locust_Swarm][AbilityConstants.Dexterity] = 8;
                    testCases[CreatureConstants.Locust_Swarm][AbilityConstants.Strength] = -10;
                    testCases[CreatureConstants.Locust_Swarm][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Marilith][AbilityConstants.Charisma] = 14;
                    testCases[CreatureConstants.Marilith][AbilityConstants.Constitution] = 18;
                    testCases[CreatureConstants.Marilith][AbilityConstants.Dexterity] = 8;
                    testCases[CreatureConstants.Marilith][AbilityConstants.Intelligence] = 8;
                    testCases[CreatureConstants.Marilith][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Marilith][AbilityConstants.Wisdom] = 8;
                    testCases[CreatureConstants.Mephit_Air][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Mephit_Air][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Mephit_Air][AbilityConstants.Dexterity] = 6;
                    testCases[CreatureConstants.Mephit_Air][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Mephit_Air][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Mephit_Air][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Mephit_Dust][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Mephit_Dust][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Mephit_Dust][AbilityConstants.Dexterity] = 6;
                    testCases[CreatureConstants.Mephit_Dust][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Mephit_Dust][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Mephit_Dust][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Mephit_Earth][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Mephit_Earth][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Mephit_Earth][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.Mephit_Earth][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Mephit_Earth][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Mephit_Earth][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Mephit_Fire][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Mephit_Fire][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Mephit_Fire][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Mephit_Fire][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Mephit_Fire][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Mephit_Fire][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Mephit_Ice][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Mephit_Ice][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Mephit_Ice][AbilityConstants.Dexterity] = 6;
                    testCases[CreatureConstants.Mephit_Ice][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Mephit_Ice][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Mephit_Ice][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Mephit_Magma][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Mephit_Magma][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Mephit_Magma][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Mephit_Magma][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Mephit_Magma][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Mephit_Magma][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Mephit_Ooze][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Mephit_Ooze][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Mephit_Ooze][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Mephit_Ooze][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Mephit_Ooze][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Mephit_Ooze][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Mephit_Salt][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Mephit_Salt][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Mephit_Salt][AbilityConstants.Dexterity] = -2;
                    testCases[CreatureConstants.Mephit_Salt][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Mephit_Salt][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Mephit_Salt][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Mephit_Steam][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Mephit_Steam][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Mephit_Steam][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Mephit_Steam][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Mephit_Steam][AbilityConstants.Strength] = 0;
                    testCases[CreatureConstants.Mephit_Steam][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Mephit_Water][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Mephit_Water][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Mephit_Water][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Mephit_Water][AbilityConstants.Intelligence] = -4;
                    testCases[CreatureConstants.Mephit_Water][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Mephit_Water][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Nalfeshnee][AbilityConstants.Charisma] = 10;
                    testCases[CreatureConstants.Nalfeshnee][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Nalfeshnee][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Nalfeshnee][AbilityConstants.Intelligence] = 12;
                    testCases[CreatureConstants.Nalfeshnee][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.Nalfeshnee][AbilityConstants.Wisdom] = 12;
                    testCases[CreatureConstants.Quasit][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Quasit][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Quasit][AbilityConstants.Dexterity] = 6;
                    testCases[CreatureConstants.Quasit][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Quasit][AbilityConstants.Strength] = -2;
                    testCases[CreatureConstants.Quasit][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Rat_Swarm][AbilityConstants.Charisma] = -8;
                    testCases[CreatureConstants.Rat_Swarm][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Rat_Swarm][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Rat_Swarm][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Rat_Swarm][AbilityConstants.Strength] = -8;
                    testCases[CreatureConstants.Rat_Swarm][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Retriever][AbilityConstants.Charisma] = -10;
                    testCases[CreatureConstants.Retriever][AbilityConstants.Dexterity] = 6;
                    testCases[CreatureConstants.Retriever][AbilityConstants.Strength] = 20;
                    testCases[CreatureConstants.Retriever][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Salamander_Average][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Salamander_Average][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Salamander_Average][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Salamander_Average][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Salamander_Average][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Salamander_Average][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Salamander_Flamebrother][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Salamander_Flamebrother][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Salamander_Flamebrother][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Salamander_Flamebrother][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Salamander_Flamebrother][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Salamander_Flamebrother][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Salamander_Noble][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.Salamander_Noble][AbilityConstants.Constitution] = 6;
                    testCases[CreatureConstants.Salamander_Noble][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Salamander_Noble][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Salamander_Noble][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Salamander_Noble][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.SeaHag][AbilityConstants.Charisma] = 4;
                    testCases[CreatureConstants.SeaHag][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.SeaHag][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.SeaHag][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.SeaHag][AbilityConstants.Strength] = 8;
                    testCases[CreatureConstants.SeaHag][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Spider_Swarm][AbilityConstants.Charisma] = -8;
                    testCases[CreatureConstants.Spider_Swarm][AbilityConstants.Constitution] = 0;
                    testCases[CreatureConstants.Spider_Swarm][AbilityConstants.Dexterity] = 6;
                    testCases[CreatureConstants.Spider_Swarm][AbilityConstants.Strength] = -10;
                    testCases[CreatureConstants.Spider_Swarm][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Succubus][AbilityConstants.Charisma] = 16;
                    testCases[CreatureConstants.Succubus][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Succubus][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Succubus][AbilityConstants.Intelligence] = 6;
                    testCases[CreatureConstants.Succubus][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Succubus][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Tiefling][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Tiefling][AbilityConstants.Constitution] = 2;
                    testCases[CreatureConstants.Tiefling][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Tiefling][AbilityConstants.Intelligence] = 2;
                    testCases[CreatureConstants.Tiefling][AbilityConstants.Strength] = 2;
                    testCases[CreatureConstants.Tiefling][AbilityConstants.Wisdom] = -2;
                    testCases[CreatureConstants.Tojanida_Adult][AbilityConstants.Charisma] = -2;
                    testCases[CreatureConstants.Tojanida_Adult][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Tojanida_Adult][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Tojanida_Adult][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Tojanida_Adult][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Tojanida_Adult][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Tojanida_Elder][AbilityConstants.Charisma] = -2;
                    testCases[CreatureConstants.Tojanida_Elder][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Tojanida_Elder][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Tojanida_Elder][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Tojanida_Elder][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Tojanida_Elder][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Tojanida_Juvenile][AbilityConstants.Charisma] = -2;
                    testCases[CreatureConstants.Tojanida_Juvenile][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Tojanida_Juvenile][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Tojanida_Juvenile][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Tojanida_Juvenile][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Tojanida_Juvenile][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Vrock][AbilityConstants.Charisma] = 6;
                    testCases[CreatureConstants.Vrock][AbilityConstants.Constitution] = 14;
                    testCases[CreatureConstants.Vrock][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Vrock][AbilityConstants.Intelligence] = 4;
                    testCases[CreatureConstants.Vrock][AbilityConstants.Strength] = 12;
                    testCases[CreatureConstants.Vrock][AbilityConstants.Wisdom] = 6;
                    testCases[CreatureConstants.Whale_Baleen][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Whale_Baleen][AbilityConstants.Constitution] = 12;
                    testCases[CreatureConstants.Whale_Baleen][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Whale_Baleen][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Whale_Baleen][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Whale_Baleen][AbilityConstants.Wisdom] = 2;
                    testCases[CreatureConstants.Whale_Cachalot][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Whale_Cachalot][AbilityConstants.Constitution] = 14;
                    testCases[CreatureConstants.Whale_Cachalot][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Whale_Cachalot][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Whale_Cachalot][AbilityConstants.Strength] = 24;
                    testCases[CreatureConstants.Whale_Cachalot][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Whale_Orca][AbilityConstants.Charisma] = -4;
                    testCases[CreatureConstants.Whale_Orca][AbilityConstants.Constitution] = 10;
                    testCases[CreatureConstants.Whale_Orca][AbilityConstants.Dexterity] = 4;
                    testCases[CreatureConstants.Whale_Orca][AbilityConstants.Intelligence] = -8;
                    testCases[CreatureConstants.Whale_Orca][AbilityConstants.Strength] = 16;
                    testCases[CreatureConstants.Whale_Orca][AbilityConstants.Wisdom] = 4;
                    testCases[CreatureConstants.Xorn_Average][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Xorn_Average][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Xorn_Average][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Xorn_Average][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Xorn_Average][AbilityConstants.Strength] = 6;
                    testCases[CreatureConstants.Xorn_Average][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Xorn_Elder][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Xorn_Elder][AbilityConstants.Constitution] = 8;
                    testCases[CreatureConstants.Xorn_Elder][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Xorn_Elder][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Xorn_Elder][AbilityConstants.Strength] = 14;
                    testCases[CreatureConstants.Xorn_Elder][AbilityConstants.Wisdom] = 0;
                    testCases[CreatureConstants.Xorn_Minor][AbilityConstants.Charisma] = 0;
                    testCases[CreatureConstants.Xorn_Minor][AbilityConstants.Constitution] = 4;
                    testCases[CreatureConstants.Xorn_Minor][AbilityConstants.Dexterity] = 0;
                    testCases[CreatureConstants.Xorn_Minor][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Xorn_Minor][AbilityConstants.Strength] = 4;
                    testCases[CreatureConstants.Xorn_Minor][AbilityConstants.Wisdom] = 0;

                    foreach (var testCase in testCases)
                    {
                        var speeds = testCase.Value.Select(kvp => $"{kvp.Key}:{kvp.Value}");
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"AbilityAdjustments({testCase.Key}, [{string.Join("], [", speeds)}])");
                    }
                }
            }
        }

        [Test]
        public void AllCreaturesHaveAtLeast1Ability()
        {
            var creatures = CreatureConstants.All();

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                Assert.That(table.Keys, Contains.Item(creature));

                var abilities = GetCollection(creature);
                Assert.That(abilities, Is.Not.Empty, creature);
            }
        }

        [Test]
        public void AllAbilityAdjustmentsAreMultiplesOf2()
        {
            var creatures = CreatureConstants.All();

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                Assert.That(table.Keys, Contains.Item(creature));

                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                Assert.That(abilities, Is.Not.Empty, creature);

                foreach (var ability in abilities)
                {
                    Assert.That(ability.Amount % 2, Is.EqualTo(0), $"{creature} {ability.Type}");
                }
            }
        }

        [Test]
        public void AllAbilityAdjustmentsAreRealAbilities()
        {
            var allAbilities = new[]
            {
                AbilityConstants.Charisma,
                AbilityConstants.Constitution,
                AbilityConstants.Dexterity,
                AbilityConstants.Intelligence,
                AbilityConstants.Strength,
                AbilityConstants.Wisdom,
            };

            var creatures = CreatureConstants.All();

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                Assert.That(table.Keys, Contains.Item(creature));

                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                Assert.That(abilities, Is.Not.Empty, creature);

                var abilityNames = abilities.Select(a => a.Type);
                Assert.That(abilityNames, Is.SubsetOf(allAbilities), creature);
            }
        }

        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Undead)]
        public void DoNotHaveConstitution(string creatureType)
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                var abilityNames = abilities.Select(a => a.Type);
                Assert.That(abilityNames, Does.Not.Contains(AbilityConstants.Constitution), creature);
            }
        }

        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal)]
        public void DoNotHaveStrength(string creatureType)
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                var abilityNames = abilities.Select(a => a.Type);
                Assert.That(abilityNames, Does.Not.Contains(AbilityConstants.Strength), creature);
            }
        }

        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void DoNotHaveIntelligence(string creatureType)
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                var abilityNames = abilities.Select(a => a.Type);
                Assert.That(abilityNames, Does.Not.Contains(AbilityConstants.Intelligence), creature);
            }
        }

        [Test]
        public void AnimalsHaveLowIntelligence()
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, CreatureConstants.Types.Animal);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                var intelligence = abilities.First(a => a.Type == AbilityConstants.Intelligence);
                Assert.That(intelligence.Amount, Is.EqualTo(-8).Or.EqualTo(-10), creature);
            }
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.Plant)]
        public void HaveAllAbilities(string creatureType)
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            var allAbilities = TypesAndAmountsSelector.Select(tableName, GroupConstants.All);
            var allAbilitiesNames = allAbilities.Select(a => a.Type);

            foreach (var creature in creatures)
            {
                var abilities = TypesAndAmountsSelector.Select(tableName, creature);
                var abilityNames = abilities.Select(a => a.Type);
                Assert.That(allAbilitiesNames, Is.EquivalentTo(abilityNames), creature);
            }
        }
    }
}
