using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Unit.TestCaseSources
{
    public static class LinqExtensions
    {
        public static bool IsEquivalentTo<T>(this IEnumerable<T> source, IEnumerable<T> target)
        {
            if (source == null || target == null)
            {
                return source == target;
            }

            return source.Count() == target.Count()
                && !source.Except(target).Any();
        }
    }
}
