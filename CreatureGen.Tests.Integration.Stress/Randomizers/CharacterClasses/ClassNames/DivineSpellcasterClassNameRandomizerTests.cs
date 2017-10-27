using CreatureGen.CharacterClasses;
using CreatureGen.Randomizers.CharacterClasses;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class DivineSpellcasterClassNameRandomizerTests : ClassNameRandomizerTests
    {
        protected override IEnumerable<string> allowedClassNames
        {
            get
            {
                return new[]
                {
                    CharacterClassConstants.Cleric,
                    CharacterClassConstants.Druid,
                    CharacterClassConstants.Paladin,
                    CharacterClassConstants.Ranger,
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.DivineSpellcaster);
        }

        [Test]
        public void StressDivineSpellcasterClassName()
        {
            stressor.Stress(AssertClassName);
        }
    }
}