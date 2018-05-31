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
                    testCases[CreatureConstants.Bebilith][AbilityConstants.Charisma] = 2;
                    testCases[CreatureConstants.Bebilith][AbilityConstants.Constitution] = 16;
                    testCases[CreatureConstants.Bebilith][AbilityConstants.Dexterity] = 2;
                    testCases[CreatureConstants.Bebilith][AbilityConstants.Intelligence] = 0;
                    testCases[CreatureConstants.Bebilith][AbilityConstants.Strength] = 18;
                    testCases[CreatureConstants.Bebilith][AbilityConstants.Wisdom] = 2;
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
