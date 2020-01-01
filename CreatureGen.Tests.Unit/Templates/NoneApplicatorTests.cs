using CreatureGen.Templates;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Templates
{
    [TestFixture]
    public class NoneApplicatorTests
    {
        private TemplateApplicator template;

        [SetUp]
        public void Setup()
        {
            template = new NoneApplicator();
        }

        [Test]
        public void DoNotAlterCreature()
        {
            var creature = new CreatureBuilder()
                .WithTestValues()
                .Build();

            Assert.Fail("not yet written");
        }
    }
}
