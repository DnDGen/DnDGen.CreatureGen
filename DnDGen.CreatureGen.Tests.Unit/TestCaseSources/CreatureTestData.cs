using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;
using System.Collections;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.TestCaseSources
{
    public class CreatureTestData
    {
        public static IEnumerable Creatures => CreatureConstants.GetAll().Select(c => new TestCaseData(c));
        public static IEnumerable CharacterCreatures => CreatureConstants.GetAllCharacters().Select(c => new TestCaseData(c));
        public static IEnumerable NonCharacterCreatures => CreatureConstants.GetAllNonCharacters().Select(c => new TestCaseData(c));
        public static IEnumerable Templates => CreatureConstants.Templates.GetAll().Select(c => new TestCaseData(c));

        public static IEnumerable CreatureTemplatePairs
        {
            get
            {
                var templates = CreatureConstants.Templates.GetAll();
                var creatures = CreatureConstants.GetAll();

                foreach (var template in templates)
                {
                    foreach (var creature in creatures)
                    {
                        yield return new TestCaseData(creature, template);
                    }
                }
            }
        }

        public static IEnumerable Types => CreatureConstants.Types.GetAll().Select(c => new TestCaseData(c));
        public static IEnumerable Subtypes => CreatureConstants.Types.Subtypes.GetAll().Select(c => new TestCaseData(c));
    }
}
