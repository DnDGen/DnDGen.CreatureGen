using DnDGen.CreatureGen.Creatures;
using NUnit.Framework;
using System;
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

        public static IEnumerable SizeIncreases
        {
            get
            {
                var sizes = SizeConstants.GetOrdered();

                for (var o = 0; o < sizes.Length; o++)
                {
                    var heightMultiplier = (int)Math.Pow(2, o);
                    var weightMultiplier = (int)Math.Pow(8, o);
                    yield return new TestCaseData(sizes[0], sizes[o], heightMultiplier, weightMultiplier);
                    yield return new TestCaseData(sizes[o], sizes[o], 1, 1);
                }

                yield return new TestCaseData(SizeConstants.Small, SizeConstants.Large, (int)Math.Pow(2, 2), (int)Math.Pow(8, 2));
                yield return new TestCaseData(SizeConstants.Small, SizeConstants.Medium, (int)Math.Pow(2, 1), (int)Math.Pow(8, 1));
                yield return new TestCaseData(SizeConstants.Medium, SizeConstants.Large, (int)Math.Pow(2, 1), (int)Math.Pow(8, 1));
                yield return new TestCaseData(SizeConstants.Large, SizeConstants.Huge, (int)Math.Pow(2, 1), (int)Math.Pow(8, 1));
                yield return new TestCaseData(SizeConstants.Large, SizeConstants.Gargantuan, (int)Math.Pow(2, 2), (int)Math.Pow(8, 2));
                yield return new TestCaseData(SizeConstants.Large, SizeConstants.Colossal, (int)Math.Pow(2, 3), (int)Math.Pow(8, 3));
            }
        }
    }
}
