using DnDGen.CreatureGen.Creatures;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Templates
{
    internal interface TemplateApplicator
    {
        bool IsCompatible(string creature);
        Creature ApplyTo(Creature creature);
        Task<Creature> ApplyToAsync(Creature creature);
    }
}
