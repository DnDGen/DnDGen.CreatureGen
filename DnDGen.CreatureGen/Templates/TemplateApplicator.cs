using DnDGen.CreatureGen.Creatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal interface TemplateApplicator
    {
        IEnumerable<string> GetCompatibleCreatures(
            IEnumerable<string> sourceCreatures,
            bool asCharacter,
            string type = null,
            string challengeRating = null,
            string alignment = null);

        Creature ApplyTo(Creature creature, string presetAlignment);
        Task<Creature> ApplyToAsync(Creature creature, string presetAlignment);
    }
}
