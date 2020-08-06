using DnDGen.CreatureGen.Creatures;
using System;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates.Lycanthropes
{
    internal abstract class LycanthropeApplicator : TemplateApplicator
    {
        protected abstract string LycanthropeSpecies { get; }
        protected abstract string AnimalSpecies { get; }

        public bool IsCompatible(string creature)
        {
            throw new NotImplementedException();
        }

        public Creature ApplyTo(Creature creature)
        {
            throw new NotImplementedException();
        }

        public async Task<Creature> ApplyToAsync(Creature creature)
        {
            throw new NotImplementedException();
        }
    }
}
