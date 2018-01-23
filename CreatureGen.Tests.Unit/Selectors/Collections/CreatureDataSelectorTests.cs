using CreatureGen.Selectors.Collections;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Collections
{
    [TestFixture]
    public class CreatureDataSelectorTests
    {
        private ICreatureDataSelector creatureDataSelector;

        [SetUp]
        public void Setup()
        {
            creatureDataSelector = new CreatureDataSelector();
        }

        [Test]
        public void SelectCreatureData()
        {
            Assert.Fail("Not yet written");
        }
    }
}
