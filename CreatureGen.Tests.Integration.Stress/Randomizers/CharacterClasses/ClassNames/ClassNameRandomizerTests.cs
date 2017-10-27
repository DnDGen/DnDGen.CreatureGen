using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public abstract class ClassNameRandomizerTests : StressTests
    {
        protected abstract IEnumerable<string> allowedClassNames { get; }

        protected void AssertClassName()
        {
            var prototype = GetCharacterPrototype();
            var className = ClassNameRandomizer.Randomize(prototype.Alignment);
            Assert.That(allowedClassNames, Contains.Item(className));
        }
    }
}