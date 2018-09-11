using CreatureGen.Creatures;
using CreatureGen.Selectors.Selections;
using System.Collections.Generic;

namespace CreatureGen.Selectors.Collections
{
    internal interface IFeatsSelector
    {
        IEnumerable<SpecialQualitySelection> SelectSpecialQualities(string creature, CreatureType creatureType);
        IEnumerable<FeatSelection> SelectFeats();
        IEnumerable<FeatSelection> SelectSkillSynergies();
    }
}