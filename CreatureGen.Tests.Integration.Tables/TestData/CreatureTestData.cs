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
    }
}
