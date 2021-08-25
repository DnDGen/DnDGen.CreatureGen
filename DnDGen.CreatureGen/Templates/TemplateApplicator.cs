using DnDGen.CreatureGen.Creatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal interface TemplateApplicator
    {
        bool IsCompatible(string creature, string type = null, string challengeRating = null);
        IEnumerable<string> GetPotentialTypes(string creature);
        (string Lower, string Upper) GetChallengeRatingRange(string challengeRating);
        //(string Lower, string Upper) GetChallengeRatingRange();
        (double? Lower, double? Upper) GetHitDiceRange(string challengeRating);
        string GetPotentialChallengeRating(string creature);

        Creature ApplyTo(Creature creature);
        Task<Creature> ApplyToAsync(Creature creature);
    }
}
