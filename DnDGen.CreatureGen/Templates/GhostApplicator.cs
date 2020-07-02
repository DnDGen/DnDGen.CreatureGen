using DnDGen.CreatureGen.Creatures;
using System;
using System.Linq;

namespace DnDGen.CreatureGen.Templates
{
    internal class GhostApplicator : TemplateApplicator
    {
        public Creature ApplyTo(Creature creature)
        {
            //Type
            creature.Type.Name = CreatureConstants.Types.Undead;

            if (!creature.Type.SubTypes.Contains(CreatureConstants.Types.Subtypes.Incorporeal))
            {
                creature.Type.SubTypes = creature.Type.SubTypes.Union(new[]
                {
                    CreatureConstants.Types.Subtypes.Incorporeal
                });
            }

            //Speed

            //Armor Class

            //Challenge Rating

            //Level Adjustment

            //Abilities

            //Hit Points

            //Attacks

            //Special Qualities

            //Skills

            return creature;
        }
    }
}
