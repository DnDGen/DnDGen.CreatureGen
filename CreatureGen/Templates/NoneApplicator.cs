using CreatureGen.Creatures;

namespace CreatureGen.Templates
{
    internal class NoneApplicator : TemplateApplicator
    {
        public Creature ApplyTo(Creature creature)
        {
            return creature;
        }
    }
}
