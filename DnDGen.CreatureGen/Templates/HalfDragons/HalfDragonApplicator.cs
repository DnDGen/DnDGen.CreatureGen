using DnDGen.CreatureGen.Creatures;
using System;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates.HalfDragons
{
    internal abstract class HalfDragonApplicator : TemplateApplicator
    {
        protected abstract string DragonSpecies { get; }

        public Creature ApplyTo(Creature creature)
        {
            throw new NotImplementedException();
        }

        public Task<Creature> ApplyToAsync(Creature creature)
        {
            throw new NotImplementedException();
        }

        public bool IsCompatible(string creature)
        {
            throw new NotImplementedException();
        }
    }
}
