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
        public void Speeds(string name, IEnumerable<Tuple<string, int>> typesAndAmounts)
        {
            AssertTypesAndAmounts(name, typesAndAmounts);
        }

        private class SpeedsTestData
        {
            public static IEnumerable TestCases
            {
                get
                {
                    var testCases = new Dictionary<string, IEnumerable<Tuple<string, int>>>();

                    testCases[CreatureConstants.Aasimar] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Aboleth] = new[] { Tuple.Create(SpeedConstants.Walk, 10), Tuple.Create(SpeedConstants.Swim, 60) };
                    testCases[CreatureConstants.Aboleth_Mage] = new[] { Tuple.Create(SpeedConstants.Walk, 10), Tuple.Create(SpeedConstants.Swim, 60) };
                    testCases[CreatureConstants.Ankheg] = new[] { Tuple.Create(SpeedConstants.Walk, 30), Tuple.Create(SpeedConstants.Burrow, 20) };
                    testCases[CreatureConstants.Annis] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Aranea] = new[] { Tuple.Create(SpeedConstants.Walk, 50), Tuple.Create(SpeedConstants.Climb, 25) };
                    testCases[CreatureConstants.Azer] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Babau] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Balor] = new[] { Tuple.Create(SpeedConstants.Walk, 40), Tuple.Create(SpeedConstants.Fly, 90) };
                    testCases[CreatureConstants.Bebilith] = new[] { Tuple.Create(SpeedConstants.Walk, 40), Tuple.Create(SpeedConstants.Climb, 20) };
                    testCases[CreatureConstants.Bugbear] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Centaur] = new[] { Tuple.Create(SpeedConstants.Walk, 50) };
                    testCases[CreatureConstants.Derro] = new[] { Tuple.Create(SpeedConstants.Walk, 20) };
                    testCases[CreatureConstants.Djinni] = new[] { Tuple.Create(SpeedConstants.Walk, 20), Tuple.Create(SpeedConstants.Fly, 60) };
                    testCases[CreatureConstants.Djinni_Noble] = new[] { Tuple.Create(SpeedConstants.Walk, 20), Tuple.Create(SpeedConstants.Fly, 60) };
                    testCases[CreatureConstants.Doppelganger] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Dretch] = new[] { Tuple.Create(SpeedConstants.Walk, 20) };
                    testCases[CreatureConstants.Dwarf_Deep] = new[] { Tuple.Create(SpeedConstants.Walk, 20) };
                    testCases[CreatureConstants.Dwarf_Duergar] = new[] { Tuple.Create(SpeedConstants.Walk, 20) };
                    testCases[CreatureConstants.Dwarf_Hill] = new[] { Tuple.Create(SpeedConstants.Walk, 20) };
                    testCases[CreatureConstants.Dwarf_Mountain] = new[] { Tuple.Create(SpeedConstants.Walk, 20) };
                    testCases[CreatureConstants.Efreeti] = new[] { Tuple.Create(SpeedConstants.Walk, 20), Tuple.Create(SpeedConstants.Fly, 40) };
                    testCases[CreatureConstants.Elf_Aquatic] = new[] { Tuple.Create(SpeedConstants.Walk, 30), Tuple.Create(SpeedConstants.Swim, 40) };
                    testCases[CreatureConstants.Elf_Drow] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Elf_Gray] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Elf_Half] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Elf_High] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Elf_Wild] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Elf_Wood] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Gargoyle] = new[] { Tuple.Create(SpeedConstants.Walk, 40), Tuple.Create(SpeedConstants.Fly, 60) };
                    testCases[CreatureConstants.Gargoyle_Kapoacinth] = new[] { Tuple.Create(SpeedConstants.Walk, 40), Tuple.Create(SpeedConstants.Swim, 60) };
                    testCases[CreatureConstants.Giant_Cloud] = new[] { Tuple.Create(SpeedConstants.Walk, 50) };
                    testCases[CreatureConstants.Giant_Fire] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Giant_Frost] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Giant_Hill] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Giant_Stone] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Giant_Stone_Elder] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Giant_Storm] = new[] { Tuple.Create(SpeedConstants.Walk, 50), Tuple.Create(SpeedConstants.Swim, 40) };
                    testCases[CreatureConstants.Githyanki] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Githzerai] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Glabrezu] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Gnoll] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Gnome_Forest] = new[] { Tuple.Create(SpeedConstants.Walk, 20) };
                    testCases[CreatureConstants.Gnome_Rock] = new[] { Tuple.Create(SpeedConstants.Walk, 20) };
                    testCases[CreatureConstants.Gnome_Svirfneblin] = new[] { Tuple.Create(SpeedConstants.Walk, 20) };
                    testCases[CreatureConstants.Goblin] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.GreenHag] = new[] { Tuple.Create(SpeedConstants.Walk, 30), Tuple.Create(SpeedConstants.Swim, 30) };
                    testCases[CreatureConstants.Grig] = new[] { Tuple.Create(SpeedConstants.Walk, 20), Tuple.Create(SpeedConstants.Fly, 40) };
                    testCases[CreatureConstants.Grimlock] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Halfling_Deep] = new[] { Tuple.Create(SpeedConstants.Walk, 20) };
                    testCases[CreatureConstants.Halfling_Lightfoot] = new[] { Tuple.Create(SpeedConstants.Walk, 20) };
                    testCases[CreatureConstants.Halfling_Tallfellow] = new[] { Tuple.Create(SpeedConstants.Walk, 20) };
                    testCases[CreatureConstants.Harpy] = new[] { Tuple.Create(SpeedConstants.Walk, 20), Tuple.Create(SpeedConstants.Fly, 80) };
                    testCases[CreatureConstants.Hezrou] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Hobgoblin] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.HoundArchon] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Human] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Janni] = new[] { Tuple.Create(SpeedConstants.Walk, 30), Tuple.Create(SpeedConstants.Fly, 20) };
                    testCases[CreatureConstants.Kobold] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.KuoToa] = new[] { Tuple.Create(SpeedConstants.Walk, 20), Tuple.Create(SpeedConstants.Swim, 50) };
                    testCases[CreatureConstants.Lizardfolk] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Locathah] = new[] { Tuple.Create(SpeedConstants.Walk, 10), Tuple.Create(SpeedConstants.Swim, 60) };
                    testCases[CreatureConstants.Marilith] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Merfolk] = new[] { Tuple.Create(SpeedConstants.Walk, 5), Tuple.Create(SpeedConstants.Swim, 50) };
                    testCases[CreatureConstants.MindFlayer] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Minotaur] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Nalfeshnee] = new[] { Tuple.Create(SpeedConstants.Walk, 30), Tuple.Create(SpeedConstants.Fly, 40) };
                    testCases[CreatureConstants.Nixie] = new[] { Tuple.Create(SpeedConstants.Walk, 20), Tuple.Create(SpeedConstants.Swim, 30) };
                    testCases[CreatureConstants.Orc] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Orc_Half] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Ogre] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Ogre_Merrow] = new[] { Tuple.Create(SpeedConstants.Walk, 30), Tuple.Create(SpeedConstants.Swim, 40) };
                    testCases[CreatureConstants.OgreMage] = new[] { Tuple.Create(SpeedConstants.Walk, 40), Tuple.Create(SpeedConstants.Fly, 40) };
                    testCases[CreatureConstants.Pixie] = new[] { Tuple.Create(SpeedConstants.Walk, 20), Tuple.Create(SpeedConstants.Fly, 60) };
                    testCases[CreatureConstants.Pixie_WithIrresistableDance] = new[] { Tuple.Create(SpeedConstants.Walk, 20), Tuple.Create(SpeedConstants.Fly, 60) };
                    testCases[CreatureConstants.Quasit] = new[] { Tuple.Create(SpeedConstants.Walk, 20), Tuple.Create(SpeedConstants.Fly, 50) };
                    testCases[CreatureConstants.Rakshasa] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Retriever] = new[] { Tuple.Create(SpeedConstants.Walk, 50) };
                    testCases[CreatureConstants.Sahuagin] = new[] { Tuple.Create(SpeedConstants.Walk, 30), Tuple.Create(SpeedConstants.Swim, 60) };
                    testCases[CreatureConstants.Satyr] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Satyr_WithPipes] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.Scorpionfolk] = new[] { Tuple.Create(SpeedConstants.Walk, 40) };
                    testCases[CreatureConstants.SeaHag] = new[] { Tuple.Create(SpeedConstants.Walk, 30), Tuple.Create(SpeedConstants.Swim, 40) };
                    testCases[CreatureConstants.Slaad_Blue] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Slaad_Death] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Slaad_Gray] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Slaad_Green] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Slaad_Red] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Succubus] = new[] { Tuple.Create(SpeedConstants.Walk, 30), Tuple.Create(SpeedConstants.Fly, 50) };
                    testCases[CreatureConstants.Tiefling] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Troglodyte] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Troll] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.Troll_Scrag] = new[] { Tuple.Create(SpeedConstants.Walk, 20), Tuple.Create(SpeedConstants.Swim, 40) };
                    testCases[CreatureConstants.Vrock] = new[] { Tuple.Create(SpeedConstants.Walk, 30), Tuple.Create(SpeedConstants.Fly, 50) };
                    testCases[CreatureConstants.YuanTi_Abomination] = new[] { Tuple.Create(SpeedConstants.Walk, 30), Tuple.Create(SpeedConstants.Swim, 20) };
                    testCases[CreatureConstants.YuanTi_Halfblood] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };
                    testCases[CreatureConstants.YuanTi_Pureblood] = new[] { Tuple.Create(SpeedConstants.Walk, 30) };

                    foreach (var testCase in testCases)
                    {
                        yield return new TestCaseData(testCase.Key, testCase.Value)
                            .SetName($"Speeds({testCase.Key}, [{string.Join("], [", testCase.Value.Select(v => $"{v.Item1}:{v.Item2}"))}])");
                    }
                }
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
    }
}