using CreatureGen.Abilities;
using CreatureGen.Attacks;
using CreatureGen.Feats;
using CreatureGen.Selectors.Selections;
using CreatureGen.Skills;
using System.Collections.Generic;

namespace CreatureGen.Generators.Feats
{
    internal interface IFeatFocusGenerator
    {
        string GenerateFrom(
            string feat,
            string focusType,
            IEnumerable<Skill> skills,
            IEnumerable<RequiredFeatSelection> requiredFeats,
            IEnumerable<Feat> otherFeat,
            int casterLevel,
            Dictionary<string, Ability> abilities,
            IEnumerable<Attack> attacks);
        string GenerateFrom(string feat, string focusType, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities);
        string GenerateAllowingFocusOfAllFrom(
            string feat,
            string focusType,
            IEnumerable<Skill> skills,
            IEnumerable<RequiredFeatSelection> requiredFeats,
            IEnumerable<Feat> otherFeat,
            int casterLevel,
            Dictionary<string, Ability> abilities,
            IEnumerable<Attack> attacks);
        string GenerateAllowingFocusOfAllFrom(string feat, string focusType, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities);
        bool FocusTypeIsPreset(string focusType);
    }
}