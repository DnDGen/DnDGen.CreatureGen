using CreatureGen.Domain.Selectors.Selections;
using System.Collections.Generic;

namespace CreatureGen.Domain.Selectors.Collections
{
    internal interface IFeatsSelector
    {
        IEnumerable<RacialFeatSelection> SelectRacial(string race);
        IEnumerable<AdditionalFeatSelection> SelectAdditional();
        IEnumerable<CharacterClassFeatSelection> SelectClass(string characterClassName);
    }
}