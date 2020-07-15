using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Templates;
using DnDGen.CreatureGen.Templates.HalfDragons;
using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit.Templates.HalfDragons
{
    [TestFixture]
    public class HalfDragonBlackApplicatorTests
    {
        private TemplateApplicator applicator;
        private Creature baseCreature;

        [SetUp]
        public void Setup()
        {
            applicator = new HalfDragonBlackApplicator();

            baseCreature = new CreatureBuilder().WithTestValues().Build();
        }

        [Test]
        public void Tests()
        {
            Assert.Fail("not yet written");
        }
    }
}
