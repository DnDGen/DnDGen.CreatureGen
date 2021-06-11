using DnDGen.CreatureGen.Creatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal class NoneApplicator : TemplateApplicator
    {
        public Creature ApplyTo(Creature creature)
        {
            return creature;
        }

        public async Task<Creature> ApplyToAsync(Creature creature)
        {
            return creature;
        }

        public string GetPotentialChallengeRating(string creature)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetPotentialTypes(string creature)
        {
            throw new System.NotImplementedException();
        }

        public bool IsCompatible(string creature)
        {
            return true;
        }
    }
}
