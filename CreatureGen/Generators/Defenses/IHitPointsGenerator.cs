using CreatureGen.Creatures;
using CreatureGen.Defenses;

namespace CreatureGen.Generators.Defenses
{
    internal interface IHitPointsGenerator
    {
        HitPoints GenerateFor(Creature creature);
    }
}