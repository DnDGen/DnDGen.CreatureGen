using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Attacks;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Skills;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Feats
{
    internal interface IFeatFocusGenerator
    {
        string GenerateFrom(
            string feat,
            string focusType,
            IEnumerable<Skill> skills,
            IEnumerable<FeatDataSelection.RequiredFeatDataSelection> requiredFeats,
            IEnumerable<Feat> otherFeat,
            int casterLevel,
            Dictionary<string, Ability> abilities,
            IEnumerable<Attack> attacks);
        string GenerateFrom(string feat, string focusType, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities);
        string GenerateAllowingFocusOfAllFrom(
            string feat,
            string focusType,
            IEnumerable<Skill> skills,
            IEnumerable<FeatDataSelection.RequiredFeatDataSelection> requiredFeats,
            IEnumerable<Feat> otherFeat,
            int casterLevel,
            Dictionary<string, Ability> abilities,
            IEnumerable<Attack> attacks);
        string GenerateAllowingFocusOfAllFrom(string feat, string focusType, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities);
        bool FocusTypeIsPreset(string focusType);
    }
}