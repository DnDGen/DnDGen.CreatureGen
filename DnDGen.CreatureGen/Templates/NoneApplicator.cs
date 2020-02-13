using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Templates
{
    internal class NoneApplicator : TemplateApplicator
    {
        public Creature ApplyTo(Creature creature)
        {
            return creature;
        }
    }
}
