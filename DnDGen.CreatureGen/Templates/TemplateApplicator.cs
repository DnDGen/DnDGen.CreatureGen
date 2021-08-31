using DnDGen.CreatureGen.Creatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal interface TemplateApplicator
    {
        bool IsCompatible(string creature, bool asCharacter, string type = null, string challengeRating = null);
        IEnumerable<string> GetPotentialTypes(string creature);
        IEnumerable<string> GetChallengeRatings(string challengeRating);
        IEnumerable<string> GetChallengeRatings();
        IEnumerable<string> GetCompatibleCreatures(IEnumerable<string> sourceCreatures, bool asCharacter, string type = null, string challengeRating = null);
        (double? Lower, double? Upper) GetHitDiceRange(string challengeRating);
        string GetPotentialChallengeRating(string creature, bool asCharacter);

        Creature ApplyTo(Creature creature);
        Task<Creature> ApplyToAsync(Creature creature);
    }
}
