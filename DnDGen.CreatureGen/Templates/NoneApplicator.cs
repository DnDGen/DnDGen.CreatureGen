using DnDGen.CreatureGen.Creatures;
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

        public bool IsCompatible(string creature)
        {
            return true;
        }
    }
}
