using CreatureGen.Creatures;
using NUnit.Framework;
using System.Collections;

namespace CreatureGen.Tests.Integration.Tables.TestData
{
    public class CreatureTestData
    {
        public static IEnumerable All
        {
            get
            {
                var creatures = CreatureConstants.All();

                foreach (var creature in creatures)
                {
                    yield return new TestCaseData(creature);
                }
            }
        }

        public static IEnumerable Types
        {
            get
            {
                var types = CreatureConstants.Types.All();

                foreach (var creatureType in types)
                {
                    yield return new TestCaseData(creatureType);
                }
            }
        }

        public static IEnumerable Subtypes
        {
            get
            {
                var subtypes = CreatureConstants.Types.Subtypes.All();

                foreach (var subtype in subtypes)
                {
                    yield return new TestCaseData(subtype);
                }
            }
        }
    }
}
