using DnDGen.CreatureGen.Feats;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Selectors.Selections
{
    internal class RequiredFeatSelection
    {
        public string Feat { get; set; }
        public IEnumerable<string> Foci { get; set; }

        public RequiredFeatSelection()
        {
            Feat = string.Empty;
            Foci = Enumerable.Empty<string>();
        }

        public bool RequirementMet(IEnumerable<Feat> otherFeats)
        {
            var requiredFeats = otherFeats.Where(f => f.Name == Feat);

            if (!requiredFeats.Any())
                return false;

            if (!Foci.Any())
                return true;

            var requiredFoci = requiredFeats.SelectMany(f => f.Foci);
            return Foci.Intersect(requiredFoci).Any();
        }
    }
}