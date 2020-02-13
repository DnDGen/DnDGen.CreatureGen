using DnDGen.CreatureGen.Creatures;

namespace DnDGen.CreatureGen.Templates
{
    internal interface TemplateApplicator
    {
        Creature ApplyTo(Creature creature);
    }
}
