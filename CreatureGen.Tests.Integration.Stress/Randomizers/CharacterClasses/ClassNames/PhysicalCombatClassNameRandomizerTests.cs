using CreatureGen.CharacterClasses;
using CreatureGen.Randomizers.CharacterClasses;
using NUnit.Framework;
using System.Collections.Generic;

namespace CreatureGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class PhysicalCombatClassNameRandomizerTests : ClassNameRandomizerTests
    {
        protected override IEnumerable<string> allowedClassNames
        {
            get
            {
                return new[]
                {
                    CharacterClassConstants.Barbarian,
                    CharacterClassConstants.Fighter,
                    CharacterClassConstants.Monk,
                    CharacterClassConstants.Paladin,
                    CharacterClassConstants.Ranger,
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.PhysicalCombat);
        }

        [Test]
        public void StressPhysicalCombatClassName()
        {
            stressor.Stress(AssertClassName);
        }
    }
}