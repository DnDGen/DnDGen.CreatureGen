using CreatureGen.Selectors.Selections;
using CreatureGen.Feats;
using CreatureGen.Skills;
using System.Collections.Generic;

namespace CreatureGen.Generators.Feats
{
    internal interface IFeatFocusGenerator
    {
        string GenerateFrom(string feat, string focusType, IEnumerable<Skill> skills, IEnumerable<RequiredFeatSelection> requiredFeats, IEnumerable<Feat> otherFeat);
        string GenerateFrom(string feat, string focusType, IEnumerable<Skill> skills);
        string GenerateAllowingFocusOfAllFrom(string feat, string focusType, IEnumerable<Skill> skills, IEnumerable<RequiredFeatSelection> requiredFeats, IEnumerable<Feat> otherFeat);
        string GenerateAllowingFocusOfAllFrom(string feat, string focusType, IEnumerable<Skill> skills);
    }
}