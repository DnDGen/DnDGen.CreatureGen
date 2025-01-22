using NUnit.Framework;

namespace DnDGen.CreatureGen.Tests.Unit
{
    internal class ConfigTests
    {
        [Test]
        public void ConfigNameIsCorrect()
        {
            var configType = typeof(Config);
            Assert.That(Config.Name, Is.EqualTo("DnDGen.CreatureGen").And.EqualTo(configType.Namespace));
        }
    }
}
