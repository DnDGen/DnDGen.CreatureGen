using CreatureGen.Alignments;
using CreatureGen.Domain.Generators.Randomizers.CharacterClasses.ClassNames;
using CreatureGen.Domain.Tables;
using CreatureGen.Randomizers.CharacterClasses;
using DnDGen.Core.Selectors.Collections;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class AnyNPCClassNameRandomizerTests
    {
        private IClassNameRandomizer npcRandomizer;
        private Mock<ICollectionSelector> mockCollectionsSelector;
        private List<string> npcs;
        private Alignment alignment;

        [SetUp]
        public void Setup()
        {
            mockCollectionsSelector = new Mock<ICollectionSelector>();
            npcRandomizer = new AnyNPCClassNameRandomizer(mockCollectionsSelector.Object);
            npcs = new List<string>();
            alignment = new Alignment();

            npcs.Add("npc 1");
            npcs.Add("npc 2");

            mockCollectionsSelector.Setup(s => s.SelectFrom(TableNameConstants.Set.Collection.ClassNameGroups, GroupConstants.NPCs)).Returns(npcs);
            mockCollectionsSelector.Setup(s => s.SelectRandomFrom(It.IsAny<IEnumerable<string>>())).Returns((IEnumerable<string> c) => c.Last());
        }

        [Test]
        public void ReturnARandomNPCClass()
        {
            var className = npcRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo("npc 2"));
        }

        [Test]
        public void ReturnAllNPCClasses()
        {
            var classNames = npcRandomizer.GetAllPossibleResults(alignment);
            Assert.That(classNames, Is.EqualTo(npcs));
        }
    }
}
