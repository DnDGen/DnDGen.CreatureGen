using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;
using System.Collections;

namespace DnDGen.CreatureGen.Tests.Integration.TestData
{
    public class CreatureTestData
    {
        public static IEnumerable Creatures
        {
            get
            {
                var creatures = CreatureConstants.GetAll();

                foreach (var creature in creatures)
                {
                    yield return new TestCaseData(creature);
                }
            }
        }

        public static IEnumerable Characters
        {
            get
            {
                var creatures = CreatureConstants.GetAllCharacters();

                foreach (var creature in creatures)
                {
                    yield return new TestCaseData(creature);
                }
            }
        }

        public static IEnumerable Templates
        {
            get
            {
                var templates = CreatureConstants.Templates.GetAll();

                foreach (var template in templates)
                {
                    yield return new TestCaseData(template);
                }
            }
        }

        public static IEnumerable Types
        {
            get
            {
                var types = CreatureConstants.Types.GetAll();

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
                var subtypes = CreatureConstants.Types.Subtypes.GetAll();

                foreach (var subtype in subtypes)
                {
                    yield return new TestCaseData(subtype);
                }
            }
        }
    }
}
