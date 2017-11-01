using CreatureGen.Creatures;

namespace CreatureGen.Templates
{
    internal interface TemplateApplicator
    {
        Creature ApplyTo(Creature creature);
    }
}
