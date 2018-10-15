using CreatureGen.Alignments;
using CreatureGen.Creatures;
using CreatureGen.Tables;
using CreatureGen.Tests.Integration.Tables.TestData;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Linq;

namespace CreatureGen.Tests.Integration.Tables.Alignments
{
    [TestFixture]
    public class AlignmentGroupsTests : CollectionTests
    {
        [Inject]
        public ICollectionSelector CollectionSelector { get; set; }
        [Inject]
        public ClientIDManager ClientIdManager { get; set; }

        [SetUp]
        public void Setup()
        {
            var clientId = Guid.NewGuid();
            ClientIdManager.SetClientID(clientId);
        }

        protected override string tableName
        {
            get
            {
                return TableNameConstants.Collection.AlignmentGroups;
            }
        }

        [Test]
        public void CollectionNames()
        {
            var creatures = CreatureConstants.All();

            var names = new[]
            {
                AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralGood,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticNeutral,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulGood,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralGood,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulNeutral,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulGood,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.NeutralGood,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticGood,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulNeutral,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.TrueNeutral,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticNeutral,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulEvil,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.NeutralEvil,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticEvil,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Good,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.Good,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.Good,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Neutral,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.Neutral,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.Neutral,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Evil,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.Evil,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.Evil,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Lawful,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.Lawful,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.Lawful,
                AlignmentConstants.Modifiers.Always + AlignmentConstants.Chaotic,
                AlignmentConstants.Modifiers.Usually + AlignmentConstants.Chaotic,
                AlignmentConstants.Modifiers.Often + AlignmentConstants.Chaotic,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Good,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Neutral,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Evil,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Lawful,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Chaotic,
                AlignmentConstants.Modifiers.Any,
                GroupConstants.All,
            };

            names = names.Union(creatures).ToArray();

            AssertCollectionNames(names);
        }

        [TestCase(GroupConstants.All,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Modifiers.Any,
            AlignmentConstants.Modifiers.Any + AlignmentConstants.Good,
            AlignmentConstants.Modifiers.Any + AlignmentConstants.Neutral,
            AlignmentConstants.Modifiers.Any + AlignmentConstants.Evil)]
        [TestCase(AlignmentConstants.Modifiers.Any + AlignmentConstants.Good,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.Good)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.Good,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralGood,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.Good,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulGood,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralGood,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.Good,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulGood,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.NeutralGood,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.Modifiers.Any + AlignmentConstants.Neutral,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.Neutral)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.Neutral,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.Neutral,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulNeutral,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.Neutral,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulNeutral,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.TrueNeutral,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.Modifiers.Any + AlignmentConstants.Evil,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.Evil)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.Evil,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.Evil,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.Evil,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulEvil,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.NeutralEvil,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Modifiers.Any + AlignmentConstants.Lawful,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.Lawful)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.Lawful,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.Lawful,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulGood,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.Lawful,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulEvil,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulGood,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.Modifiers.Any + AlignmentConstants.Chaotic,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.Chaotic)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.Chaotic,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.Chaotic,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.Chaotic,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticGood,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil, AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood, AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral, AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil, AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood, AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticNeutral, AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil, AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralGood, AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral, AlignmentConstants.TrueNeutral)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulGood,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulNeutral,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.TrueNeutral)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil,
            AlignmentConstants.ChaoticEvil,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralGood,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralGood,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulEvil,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulGood,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulGood,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulNeutral,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulNeutral,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticEvil,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil,
            AlignmentConstants.ChaoticEvil,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticGood,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.ChaoticEvil,
            AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.ChaoticEvil,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.NeutralEvil,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.ChaoticEvil,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.NeutralGood,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralGood,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralGood,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.Modifiers.Often + AlignmentConstants.TrueNeutral,
            AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral,
            AlignmentConstants.TrueNeutral,
            AlignmentConstants.LawfulNeutral,
            AlignmentConstants.NeutralGood,
            AlignmentConstants.NeutralEvil,
            AlignmentConstants.ChaoticNeutral,
            AlignmentConstants.LawfulGood,
            AlignmentConstants.LawfulEvil,
            AlignmentConstants.ChaoticGood,
            AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Aasimar, AlignmentConstants.Modifiers.Usually + AlignmentConstants.Good)]
        [TestCase(CreatureConstants.Aboleth, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Achaierai, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Allip, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Androsphinx, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.AnimatedObject_Huge, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.AnimatedObject_Large, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.AnimatedObject_Medium, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.AnimatedObject_Small, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Ankheg, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Annis, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Ant_Giant_Queen)]
        [TestCase(CreatureConstants.Ant_Giant_Soldier)]
        [TestCase(CreatureConstants.Ant_Giant_Worker)]
        [TestCase(CreatureConstants.Ape)]
        [TestCase(CreatureConstants.Ape_Dire)]
        [TestCase(CreatureConstants.Aranea, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Arrowhawk_Adult, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Arrowhawk_Elder, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.AssassinVine, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Angel_AstralDeva, AlignmentConstants.Modifiers.Always + AlignmentConstants.Good)]
        [TestCase(CreatureConstants.Athach, AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Avoral, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Azer, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral)]
        [TestCase(CreatureConstants.Babau, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Baboon)]
        [TestCase(CreatureConstants.Badger)]
        [TestCase(CreatureConstants.Badger_Dire)]
        [TestCase(CreatureConstants.Balor, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.BarbedDevil_Hamatula, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Barghest, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Barghest_Greater, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Basilisk, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Basilisk_AbyssalGreater, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Bat)]
        [TestCase(CreatureConstants.Bat_Dire)]
        [TestCase(CreatureConstants.Bat_Swarm, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Bear_Black)]
        [TestCase(CreatureConstants.Bear_Brown)]
        [TestCase(CreatureConstants.Bear_Dire)]
        [TestCase(CreatureConstants.Bear_Polar)]
        [TestCase(CreatureConstants.BeardedDevil_Barbazu, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Bebilith, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Bee_Giant)]
        [TestCase(CreatureConstants.Behir, AlignmentConstants.Modifiers.Often + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Beholder, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Beholder_Gauth, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Belker, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Bison)]
        [TestCase(CreatureConstants.BlackPudding, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.BlackPudding_Elder, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.BlinkDog, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Boar)]
        [TestCase(CreatureConstants.Boar_Dire)]
        [TestCase(CreatureConstants.Bodak, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.BombardierBeetle_Giant)]
        [TestCase(CreatureConstants.BoneDevil_Osyluth, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Bralani, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Bugbear, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Bulette, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Camel_Bactrian)]
        [TestCase(CreatureConstants.Camel_Dromedary)]
        [TestCase(CreatureConstants.CarrionCrawler, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Cat)]
        [TestCase(CreatureConstants.Centaur, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Colossal)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Gargantuan)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Huge)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Large)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Medium)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Small)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Tiny)]
        [TestCase(CreatureConstants.Centipede_Swarm, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.ChainDevil_Kyton, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.ChaosBeast, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticNeutral)]
        [TestCase(CreatureConstants.Cheetah)]
        [TestCase(CreatureConstants.Chimera, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Choker, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Chuul, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Cloaker, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral)]
        [TestCase(CreatureConstants.Cockatrice, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Couatl, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Criosphinx, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Cryohydra_10Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Cryohydra_11Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Cryohydra_12Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Cryohydra_5Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Cryohydra_6Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Cryohydra_7Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Cryohydra_8Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Cryohydra_9Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Crocodile)]
        [TestCase(CreatureConstants.Crocodile_Giant)]
        [TestCase(CreatureConstants.Darkmantle, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Deinonychus, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Delver, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Derro, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Derro_Sane, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Destrachan, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Devourer, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Digester, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.DisplacerBeast, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.DisplacerBeast_PackLord, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Djinni, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Djinni_Noble, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dog)]
        [TestCase(CreatureConstants.Dog_Riding)]
        [TestCase(CreatureConstants.Donkey)]
        [TestCase(CreatureConstants.Doppelganger, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Dragon_Black_Adult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Black_Ancient, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Black_GreatWyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Black_Juvenile, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Black_MatureAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Black_Old, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Black_VeryOld, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Black_VeryYoung, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Black_Wyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Black_Wyrmling, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Black_Young, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Black_YoungAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Blue_Adult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Blue_Ancient, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Blue_GreatWyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Blue_Juvenile, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Blue_MatureAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Blue_Old, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Blue_VeryOld, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Blue_VeryYoung, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrmling, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Blue_Young, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Blue_YoungAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Brass_Adult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Brass_Ancient, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Brass_GreatWyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Brass_Juvenile, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Brass_MatureAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Brass_Old, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Brass_VeryOld, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Brass_VeryYoung, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrmling, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Brass_Young, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Brass_YoungAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Bronze_Adult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Bronze_Ancient, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Bronze_GreatWyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Bronze_Juvenile, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Bronze_MatureAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Bronze_Old, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryOld, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryYoung, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrmling, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Bronze_Young, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Bronze_YoungAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Copper_Adult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Copper_Ancient, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Copper_GreatWyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Copper_Juvenile, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Copper_MatureAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Copper_Old, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Copper_VeryOld, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Copper_VeryYoung, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrmling, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Copper_Young, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Copper_YoungAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dragon_Gold_Adult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Gold_Ancient, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Gold_GreatWyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Gold_Juvenile, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Gold_MatureAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Gold_Old, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Gold_VeryOld, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Gold_VeryYoung, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrmling, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Gold_Young, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Gold_YoungAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Green_Adult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Green_Ancient, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Green_GreatWyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Green_Juvenile, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Green_MatureAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Green_Old, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Green_VeryOld, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Green_VeryYoung, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Green_Wyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Green_Wyrmling, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Green_Young, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Green_YoungAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dragon_Red_Adult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Red_Ancient, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Red_GreatWyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Red_Juvenile, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Red_MatureAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Red_Old, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Red_VeryOld, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Red_VeryYoung, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Red_Wyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Red_Wyrmling, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Red_Young, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Red_YoungAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_Silver_Adult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Silver_Ancient, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Silver_GreatWyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Silver_Juvenile, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Silver_MatureAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Silver_Old, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Silver_VeryOld, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Silver_VeryYoung, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrmling, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Silver_Young, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_Silver_YoungAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dragon_White_Adult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_White_Ancient, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_White_GreatWyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_White_Juvenile, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_White_MatureAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_White_Old, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_White_VeryOld, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_White_VeryYoung, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_White_Wyrm, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_White_Wyrmling, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_White_Young, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dragon_White_YoungAdult, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.DragonTurtle, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Dragonne, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Dretch, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Drider, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Dryad, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dwarf_Duergar, AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Dwarf_Deep,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulNeutral,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Dwarf_Hill, AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Dwarf_Mountain, AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Eagle)]
        [TestCase(CreatureConstants.Eagle_Giant, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Efreeti, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Elasmosaurus, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Air_Elder, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Air_Greater, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Air_Huge, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Air_Large, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Air_Medium, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Air_Small, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Earth_Elder, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Earth_Greater, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Earth_Huge, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Earth_Large, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Earth_Medium, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Earth_Small, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Fire_Elder, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Fire_Greater, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Fire_Huge, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Fire_Large, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Fire_Medium, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Fire_Small, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Water_Elder, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Water_Greater, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Water_Huge, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Water_Large, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Water_Medium, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elemental_Water_Small, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Elephant)]
        [TestCase(CreatureConstants.Elf_Aquatic, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Elf_Drow, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Elf_Gray, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Elf_Half, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Elf_High, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Elf_Wild, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Elf_Wood, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Erinyes, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.EtherealFilcher, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.EtherealMarauder, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Ettercap, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Ettin, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.FireBeetle_Giant)]
        [TestCase(CreatureConstants.FormianMyrmarch, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral)]
        [TestCase(CreatureConstants.FormianQueen, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral)]
        [TestCase(CreatureConstants.FormianTaskmaster, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral)]
        [TestCase(CreatureConstants.FormianWarrior, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral)]
        [TestCase(CreatureConstants.FormianWorker, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral)]
        [TestCase(CreatureConstants.FrostWorm, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Gargoyle, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.GelatinousCube, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Ghaele, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Ghoul, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Ghoul_Ghast, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Ghoul_Lacedon, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Giant_Cloud,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralGood,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Giant_Fire, AlignmentConstants.Modifiers.Often + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Giant_Frost, AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Giant_Hill, AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Giant_Stone, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Giant_Stone_Elder, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Giant_Storm, AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.GibberingMouther, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Girallon, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Githyanki, AlignmentConstants.Modifiers.Usually + AlignmentConstants.Evil)]
        [TestCase(CreatureConstants.Githzerai, AlignmentConstants.Modifiers.Any + AlignmentConstants.Neutral)]
        [TestCase(CreatureConstants.Glabrezu, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Gnoll, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Gnome_Forest, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Gnome_Rock, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Gnome_Svirfneblin, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Goblin, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Golem_Clay, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Golem_Flesh, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Golem_Iron, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Golem_Stone, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Golem_Stone_Greater, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Gorgon, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.GrayRender, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.GreenHag, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Grick, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Griffon, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Grig, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Grig_WithFiddle, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Grimlock, AlignmentConstants.Modifiers.Often + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Gynosphinx, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Halfling_Deep, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Halfling_Lightfoot, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Halfling_Tallfellow, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Harpy, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Hawk)]
        [TestCase(CreatureConstants.Hellcat_Bezekira, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.HellHound, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.HellHound_NessianWarhound, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Hellwasp_Swarm, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Hezrou, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Hieracosphinx, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Hippogriff, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hobgoblin, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Homunculus, AlignmentConstants.Modifiers.Any)]
        [TestCase(CreatureConstants.HornedDevil_Cornugon, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Horse_Heavy)]
        [TestCase(CreatureConstants.Horse_Heavy_War)]
        [TestCase(CreatureConstants.Horse_Light)]
        [TestCase(CreatureConstants.Horse_Light_War)]
        [TestCase(CreatureConstants.HoundArchon, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Howler, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Human, AlignmentConstants.Modifiers.Any)]
        [TestCase(CreatureConstants.Hydra_10Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_11Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_12Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_5Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_6Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_7Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_8Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_9Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hyena)]
        [TestCase(CreatureConstants.IceDevil_Gelugon, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Imp, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.InvisibleStalker, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Janni, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Kobold, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Kolyarut, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral)]
        [TestCase(CreatureConstants.Kraken, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Krenshar, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.KuoToa, AlignmentConstants.Modifiers.Often + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Lamia, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Lammasu, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Lemure, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.LanternArchon, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Leonal, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Leopard)]
        [TestCase(CreatureConstants.Lillend, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Lion)]
        [TestCase(CreatureConstants.Lion_Dire)]
        [TestCase(CreatureConstants.Lizard)]
        [TestCase(CreatureConstants.Lizard_Monitor)]
        [TestCase(CreatureConstants.Lizardfolk, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Locathah, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Locust_Swarm, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Magmin, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticNeutral)]
        [TestCase(CreatureConstants.MantaRay)]
        [TestCase(CreatureConstants.Manticore, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Marilith, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Marut, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral)]
        [TestCase(CreatureConstants.Medusa, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Megaraptor, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Mephit_Air, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Mephit_Dust, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Mephit_Earth, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Mephit_Fire, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Mephit_Ice, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Mephit_Magma, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Mephit_Ooze, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Mephit_Salt, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Mephit_Steam, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Mephit_Water, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Merfolk, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Mimic, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.MindFlayer, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Minotaur, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Mohrg, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Monkey)]
        [TestCase(CreatureConstants.Mule)]
        [TestCase(CreatureConstants.Mummy, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Naga_Dark, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Naga_Guardian, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Naga_Spirit, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Naga_Water, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Nalfeshnee, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.NightHag, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Nightcrawler, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Nightmare, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Nightmare_Cauchemar, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Nightwalker, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Nightwing, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Nixie, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Nymph, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Octopus)]
        [TestCase(CreatureConstants.Octopus_Giant)]
        [TestCase(CreatureConstants.Ogre, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Ogre_Merrow, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.OgreMage, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Orc, AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Orc_Half, AlignmentConstants.Modifiers.Often + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Otyugh, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Owl)]
        [TestCase(CreatureConstants.Owl_Giant, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Owlbear, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.GrayOoze, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.OchreJelly, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pegasus, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Angel_Planetar, AlignmentConstants.Modifiers.Always + AlignmentConstants.Good)]
        [TestCase(CreatureConstants.PhantomFungus, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.PhaseSpider, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Phasm, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral)]
        [TestCase(CreatureConstants.PitFiend, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Pixie, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Pixie_WithIrresistableDance, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Pony)]
        [TestCase(CreatureConstants.Pony_War)]
        [TestCase(CreatureConstants.Porpoise)]
        [TestCase(CreatureConstants.PrayingMantis_Giant)]
        [TestCase(CreatureConstants.Pseudodragon, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.PurpleWorm, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_10Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_11Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_12Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_5Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_6Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_7Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_8Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_9Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Quasit, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Rakshasa, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Rast, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Rat)]
        [TestCase(CreatureConstants.Rat_Dire)]
        [TestCase(CreatureConstants.Rat_Swarm, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Raven)]
        [TestCase(CreatureConstants.Ravid, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.RazorBoar, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Remorhaz, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Retriever, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Rhinoceras)]
        [TestCase(CreatureConstants.Roc)]
        [TestCase(CreatureConstants.Roper, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.RustMonster, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Sahuagin, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Sahuagin_Malenti, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Sahuagin_Mutant, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Salamander_Average, AlignmentConstants.Modifiers.Usually + AlignmentConstants.Evil)]
        [TestCase(CreatureConstants.Salamander_Flamebrother, AlignmentConstants.Modifiers.Usually + AlignmentConstants.Evil)]
        [TestCase(CreatureConstants.Salamander_Noble, AlignmentConstants.Modifiers.Usually + AlignmentConstants.Evil)]
        [TestCase(CreatureConstants.Satyr, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral)]
        [TestCase(CreatureConstants.Satyr_WithPipes, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Colossal)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Gargantuan)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Huge)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Large)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Medium)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Small)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Tiny)]
        [TestCase(CreatureConstants.Scorpionfolk, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.SeaCat, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.SeaHag, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Shadow, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Shadow_Greater, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.ShadowMastiff, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.ShamblingMound, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Shark_Dire)]
        [TestCase(CreatureConstants.Shark_Huge)]
        [TestCase(CreatureConstants.Shark_Large)]
        [TestCase(CreatureConstants.Shark_Medium)]
        [TestCase(CreatureConstants.ShieldGuardian, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.ShockerLizard, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Shrieker, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Skum, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Slaad_Blue, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral)]
        [TestCase(CreatureConstants.Slaad_Death, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Slaad_Gray, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral)]
        [TestCase(CreatureConstants.Slaad_Green, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral)]
        [TestCase(CreatureConstants.Slaad_Red, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticNeutral)]
        [TestCase(CreatureConstants.Angel_Solar, AlignmentConstants.Modifiers.Always + AlignmentConstants.Good)]
        [TestCase(CreatureConstants.Snake_Constrictor)]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant)]
        [TestCase(CreatureConstants.Snake_Viper_Huge)]
        [TestCase(CreatureConstants.Snake_Viper_Large)]
        [TestCase(CreatureConstants.Snake_Viper_Medium)]
        [TestCase(CreatureConstants.Snake_Viper_Small)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny)]
        [TestCase(CreatureConstants.Spectre, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Spider_Monstrous_Colossal)]
        [TestCase(CreatureConstants.Spider_Monstrous_Gargantuan)]
        [TestCase(CreatureConstants.Spider_Monstrous_Huge)]
        [TestCase(CreatureConstants.Spider_Monstrous_Large)]
        [TestCase(CreatureConstants.Spider_Monstrous_Medium)]
        [TestCase(CreatureConstants.Spider_Monstrous_Small)]
        [TestCase(CreatureConstants.Spider_Monstrous_Tiny)]
        [TestCase(CreatureConstants.Spider_Swarm, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.SpiderEater, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Squid)]
        [TestCase(CreatureConstants.Squid_Giant)]
        [TestCase(CreatureConstants.StagBeetle_Giant)]
        [TestCase(CreatureConstants.Stirge, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Succubus, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Tarrasque, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Tendriculos, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Thoqqua, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Tiefling, AlignmentConstants.Modifiers.Usually + AlignmentConstants.Evil)]
        [TestCase(CreatureConstants.Tiger)]
        [TestCase(CreatureConstants.Tiger_Dire)]
        [TestCase(CreatureConstants.Titan, AlignmentConstants.Modifiers.Always + AlignmentConstants.Chaotic)]
        [TestCase(CreatureConstants.Toad)]
        [TestCase(CreatureConstants.Tojanida_Adult, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Tojanida_Elder, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Tojanida_Juvenile, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Treant, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Triceratops, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Triton, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralGood)]
        [TestCase(CreatureConstants.Troglodyte, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Troll, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Troll_Scrag, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.TrumpetArchon, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulGood)]
        [TestCase(CreatureConstants.Tyrannosaurus, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.UmberHulk, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.UmberHulk_TrulyHorrid, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Unicorn, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.VampireSpawn, AlignmentConstants.Modifiers.Always + AlignmentConstants.Evil)]
        [TestCase(CreatureConstants.Vargouille, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.VioletFungus, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Vrock, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Wasp_Giant)]
        [TestCase(CreatureConstants.Weasel)]
        [TestCase(CreatureConstants.Weasel_Dire)]
        [TestCase(CreatureConstants.Whale_Baleen)]
        [TestCase(CreatureConstants.Whale_Cachalot)]
        [TestCase(CreatureConstants.Whale_Orca)]
        [TestCase(CreatureConstants.Wight, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.WillOWisp, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.WinterWolf, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Wolf)]
        [TestCase(CreatureConstants.Wolf_Dire)]
        [TestCase(CreatureConstants.Wolverine)]
        [TestCase(CreatureConstants.Wolverine_Dire)]
        [TestCase(CreatureConstants.Worg, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Wraith, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Wraith_Dread, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Wyvern, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Xill, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Xorn_Average, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Xorn_Elder, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Xorn_Minor, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.YethHound, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Yrthak, AlignmentConstants.Modifiers.Often + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.YuanTi_Abomination, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.YuanTi_Halfblood, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.YuanTi_Pureblood, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Zelekhut, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulNeutral)]
        public void AlignmentGroup(string name, params string[] collection)
        {
            AssertCollection(name, collection);
        }

        [TestCase(AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.TrueNeutral)]
        public void AlwaysAlignmentIsOnlyAlignment(string alignment)
        {
            var group = AlignmentConstants.Modifiers.Always + alignment;
            AssertDistinctCollection(group, alignment);
        }

        [TestCase(AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.TrueNeutral)]
        public void UsuallyAlignmentHasMajority(string alignment)
        {
            var group = AlignmentConstants.Modifiers.Usually + alignment;
            var weightedAlignments = CollectionSelector.ExplodeAndPreserveDuplicates(tableName, group);

            var alignmentCount = weightedAlignments.Count(a => a == alignment);
            var halfCount = weightedAlignments.Count() / 2;
            Assert.That(alignmentCount, Is.AtLeast(halfCount));
        }

        [TestCase(AlignmentConstants.LawfulEvil)]
        [TestCase(AlignmentConstants.LawfulGood)]
        [TestCase(AlignmentConstants.LawfulNeutral)]
        [TestCase(AlignmentConstants.ChaoticEvil)]
        [TestCase(AlignmentConstants.ChaoticGood)]
        [TestCase(AlignmentConstants.ChaoticNeutral)]
        [TestCase(AlignmentConstants.NeutralEvil)]
        [TestCase(AlignmentConstants.NeutralGood)]
        [TestCase(AlignmentConstants.TrueNeutral)]
        public void OftenAlignmentIsMode(string alignment)
        {
            var group = AlignmentConstants.Modifiers.Often + alignment;
            var weightedAlignments = CollectionSelector.ExplodeAndPreserveDuplicates(tableName, group);

            var modeCount = weightedAlignments
                .GroupBy(a => a)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Count())
                .First();
            var mode = weightedAlignments
                .GroupBy(a => a)
                .Where(g => g.Count() == modeCount)
                .Select(g => g.Key)
                .Single();

            Assert.That(mode, Is.EqualTo(alignment));
        }

        [TestCase(CreatureConstants.Types.Animal)]
        [TestCase(CreatureConstants.Types.Vermin)]
        public void AllCreaturesOfTypeHaveNoAlignmentOrAreTrueNeutral(string creatureType)
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, creatureType);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                var alignments = CollectionSelector.ExplodeAndPreserveDuplicates(tableName, creature);
                Assert.That(alignments.Count, Is.EqualTo(0).Or.EqualTo(1), creature);
                Assert.That(alignments, Is.Empty.Or.Contains(AlignmentConstants.TrueNeutral), creature);
            }
        }

        [TestCase(CreatureConstants.Types.Aberration)]
        [TestCase(CreatureConstants.Types.Construct)]
        [TestCase(CreatureConstants.Types.Dragon)]
        [TestCase(CreatureConstants.Types.Elemental)]
        [TestCase(CreatureConstants.Types.Fey)]
        [TestCase(CreatureConstants.Types.Giant)]
        [TestCase(CreatureConstants.Types.Humanoid)]
        [TestCase(CreatureConstants.Types.MagicalBeast)]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid)]
        [TestCase(CreatureConstants.Types.Ooze)]
        [TestCase(CreatureConstants.Types.Outsider)]
        [TestCase(CreatureConstants.Types.Plant)]
        [TestCase(CreatureConstants.Types.Undead)]
        public void AllCreaturesOfTypeHaveAlignment(string creatureType)
        {
            var creatures = CollectionSelector.Explode(TableNameConstants.Collection.CreatureGroups, creatureType);

            AssertCollection(creatures.Intersect(table.Keys), creatures);

            foreach (var creature in creatures)
            {
                var alignments = CollectionSelector.ExplodeAndPreserveDuplicates(tableName, creature);
                Assert.That(alignments, Is.Not.Empty, creature);
            }
        }

        [TestCaseSource(typeof(CreatureTestData), "All")]
        public void CreatureAlignmentGroupsBoilDownToSetAlignments(string creature)
        {
            var allAlignments = table[GroupConstants.All];

            Assert.That(table.Keys, Contains.Item(creature));

            var creatureAlignments = CollectionSelector.Explode(tableName, creature);
            Assert.That(creatureAlignments, Is.Empty.Or.SubsetOf(allAlignments), creature);
        }
    }
}
