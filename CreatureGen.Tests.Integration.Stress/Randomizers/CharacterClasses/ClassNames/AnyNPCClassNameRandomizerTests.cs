using CreatureGen.CharacterClasses;
using CreatureGen.Randomizers.CharacterClasses;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class AnyNPCClassNameRandomizerTests : ClassNameRandomizerTests
    {
        protected override IEnumerable<string> allowedClassNames
        {
            get
            {
                return new[] {
                    CharacterClassConstants.Adept,
                    CharacterClassConstants.Aristocrat,
                    CharacterClassConstants.Commoner,
                    CharacterClassConstants.Expert,
                    CharacterClassConstants.Warrior
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyNPC);
        }

        [Test]
        public void StressAnyNPCClassName()
        {
            stressor.Stress(AssertClassName);
        }
    }
}
