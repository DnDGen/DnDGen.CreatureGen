using CreatureGen.Selectors.Helpers;
using NUnit.Framework;

namespace CreatureGen.Tests.Unit.Selectors.Helpers
{
    [TestFixture]
    public class TypeAndAmountHelperTests
    {
        [Test]
        public void BuildTypeAndAmountData()
        {
            var data = TypeAndAmountHelper.BuildData("type", "amount");
            Assert.That(data, Is.EqualTo("type/amount"));
        }

        [Test]
        public void ParseTypeAndAmountData()
        {
            var data = TypeAndAmountHelper.ParseData("type/amount");
            Assert.That(data[0], Is.EqualTo("type"));
            Assert.That(data[1], Is.EqualTo("amount"));
            Assert.That(data.Length, Is.EqualTo(2));
        }

        [Test]
        public void ParseTypeAndAmountDataWithSlash()
        {
            var data = TypeAndAmountHelper.ParseData("my/type/amount");
            Assert.That(data[0], Is.EqualTo("my/type"));
            Assert.That(data[1], Is.EqualTo("amount"));
            Assert.That(data.Length, Is.EqualTo(2));
        }
    }
}
