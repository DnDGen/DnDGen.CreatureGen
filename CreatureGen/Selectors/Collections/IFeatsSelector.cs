using CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace CreatureGen.Selectors.Collections
{
    internal interface IFeatsSelector
    {
        IEnumerable<SpecialQualitySelection> SelectSpecialQualities(string creature);
        IEnumerable<FeatSelection> SelectFeats();
    }
}