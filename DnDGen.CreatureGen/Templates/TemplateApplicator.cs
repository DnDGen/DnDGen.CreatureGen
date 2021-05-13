using DnDGen.CreatureGen.Creatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal interface TemplateApplicator
    {
        bool IsCompatible(string creature);
        IEnumerable<string> GetPotentialTypes(string creature);
        string GetPotentialChallengeRating(string creature);

        Creature ApplyTo(Creature creature);
        Task<Creature> ApplyToAsync(Creature creature);
    }
}
