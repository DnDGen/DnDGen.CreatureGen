using CreatureGen.Alignments;
using CreatureGen.Creatures;
using CreatureGen.Tables;
using DnDGen.Core.Selectors.Collections;
using EventGen;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
                return TableNameConstants.Set.Collection.AlignmentGroups;
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
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Good,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Neutral,
                AlignmentConstants.Modifiers.Any + AlignmentConstants.Evil,
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
            AlignmentConstants.Modifiers.Always + AlignmentConstants.Good,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.Good,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.Good)]
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
            AlignmentConstants.Modifiers.Always + AlignmentConstants.Neutral,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.Neutral,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.Neutral)]
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
            AlignmentConstants.Modifiers.Always + AlignmentConstants.Evil,
            AlignmentConstants.Modifiers.Usually + AlignmentConstants.Evil,
            AlignmentConstants.Modifiers.Often + AlignmentConstants.Evil)]
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
        [TestCase(CreatureConstants.Aboleth_Mage, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
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
        [TestCase(CreatureConstants.AstralDeva, AlignmentConstants.Modifiers.Always + AlignmentConstants.Good)]
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
        [TestCase(CreatureConstants.Bat_Swarm)]
        [TestCase(CreatureConstants.Bear_Black)]
        [TestCase(CreatureConstants.Bear_Brown)]
        [TestCase(CreatureConstants.Bear_Dire)]
        [TestCase(CreatureConstants.Bear_Polar)]
        [TestCase(CreatureConstants.BeardedDevil_Barbazu, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Bebilith, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Bee_Giant)]
        [TestCase(CreatureConstants.Behir, AlignmentConstants.Modifiers.Often + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Beholder, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
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
        [TestCase(CreatureConstants.Camel)]
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
        [TestCase(CreatureConstants.Centipede_Swarm)]
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
        [TestCase(CreatureConstants.Destrachan, AlignmentConstants.Modifiers.Usually + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Devourer, AlignmentConstants.Modifiers.Always + AlignmentConstants.NeutralEvil)]
        [TestCase(CreatureConstants.Digester, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.DisplacerBeast, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.DisplacerBeast_PackLord, AlignmentConstants.Modifiers.Usually + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Djinni, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Djinni_Noble, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticGood)]
        [TestCase(CreatureConstants.Dog)]
        [TestCase(CreatureConstants.Dog_Riding)]
        [TestCase(CreatureConstants.Dretch, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Efreeti, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Elasmosaurus, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Erinyes, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.FireBeetle_Giant)]
        [TestCase(CreatureConstants.GelatinousCube, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Glabrezu, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.GreenHag, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Gynosphinx, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hellcat_Bezekira, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Hezrou, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Hieracosphinx, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.HornedDevil_Cornugon, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Hydra_10Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_11Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_12Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_5Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_6Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_7Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_8Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Hydra_9Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.IceDevil_Gelugon, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Imp, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Janni, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Lemure, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.Locust_Swarm)]
        [TestCase(CreatureConstants.Marilith, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Megaraptor, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Nalfeshnee, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Ooze_Gray, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Ooze_OchreJelly, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Planetar, AlignmentConstants.Modifiers.Always + AlignmentConstants.Good)]
        [TestCase(CreatureConstants.PitFiend, AlignmentConstants.Modifiers.Always + AlignmentConstants.LawfulEvil)]
        [TestCase(CreatureConstants.PrayingMantis_Giant)]
        [TestCase(CreatureConstants.Pyrohydra_10Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_11Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_12Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_5Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_6Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_7Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_8Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Pyrohydra_9Heads, AlignmentConstants.Modifiers.Usually + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Quasit, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Retriever, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Colossal)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Gargantuan)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Huge)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Large)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Medium)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Small)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Tiny)]
        [TestCase(CreatureConstants.SeaHag, AlignmentConstants.Modifiers.Usually + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Solar, AlignmentConstants.Modifiers.Always + AlignmentConstants.Good)]
        [TestCase(CreatureConstants.Snake_Constrictor)]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant)]
        [TestCase(CreatureConstants.Snake_Viper_Huge)]
        [TestCase(CreatureConstants.Snake_Viper_Large)]
        [TestCase(CreatureConstants.Snake_Viper_Medium)]
        [TestCase(CreatureConstants.Snake_Viper_Small)]
        [TestCase(CreatureConstants.Snake_Viper_Tiny)]
        [TestCase(CreatureConstants.Spider_Monstrous_Colossal)]
        [TestCase(CreatureConstants.Spider_Monstrous_Gargantuan)]
        [TestCase(CreatureConstants.Spider_Monstrous_Huge)]
        [TestCase(CreatureConstants.Spider_Monstrous_Large)]
        [TestCase(CreatureConstants.Spider_Monstrous_Medium)]
        [TestCase(CreatureConstants.Spider_Monstrous_Small)]
        [TestCase(CreatureConstants.Spider_Monstrous_Tiny)]
        [TestCase(CreatureConstants.Spider_Swarm)]
        [TestCase(CreatureConstants.StagBeetle_Giant)]
        [TestCase(CreatureConstants.Succubus, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Tiefling, AlignmentConstants.Modifiers.Usually + AlignmentConstants.Evil)]
        [TestCase(CreatureConstants.Triceratops, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Tyrannosaurus, AlignmentConstants.Modifiers.Always + AlignmentConstants.TrueNeutral)]
        [TestCase(CreatureConstants.Vrock, AlignmentConstants.Modifiers.Always + AlignmentConstants.ChaoticEvil)]
        [TestCase(CreatureConstants.Wasp_Giant)]
        public void AlignmentGroup(string name, params string[] collection)
        {
            base.Collection(name, collection);
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
            DistinctCollection(group, alignment);
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
            var weightedAlignments = ExplodeAndPreserveDuplicates(group);

            var alignmentCount = weightedAlignments.Count(a => a == alignment);
            var halfCount = weightedAlignments.Count() / 2;
            Assert.That(alignmentCount, Is.AtLeast(halfCount));
        }

        private IEnumerable<string> ExplodeAndPreserveDuplicates(string group)
        {
            //INFO: Not using Explode as that does deduplication, and we want duplication/weighting with these alignments
            var alignmentGroups = table[group];
            var weightedAlignments = new List<string>(alignmentGroups);

            alignmentGroups = weightedAlignments.Where(a => table.ContainsKey(a)).ToArray();

            while (alignmentGroups.Any())
            {
                foreach (var alignmentGroup in alignmentGroups)
                {
                    var alignments = table[alignmentGroup];
                    weightedAlignments.AddRange(alignments);
                    weightedAlignments.Remove(alignmentGroup);
                }

                alignmentGroups = weightedAlignments.Where(a => table.ContainsKey(a)).ToArray();
            }

            return weightedAlignments;
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
            var weightedAlignments = ExplodeAndPreserveDuplicates(group);

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
        public void AllHaveNoAlignment(string creatureType)
        {
            var animals = CollectionSelector.Explode(TableNameConstants.Set.Collection.CreatureGroups, creatureType);

            foreach (var animal in animals)
            {
                Assert.That(table.Keys, Contains.Item(animal));
                var alignments = ExplodeAndPreserveDuplicates(animal);
                Assert.That(alignments, Is.Empty);
            }
        }
    }
}
