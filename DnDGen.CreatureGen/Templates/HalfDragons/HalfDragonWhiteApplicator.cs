using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Alignments;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Magics;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;

namespace DnDGen.CreatureGen.Templates.HalfDragons
{
    internal class HalfDragonWhiteApplicator : HalfDragonApplicator
    {
        public HalfDragonWhiteApplicator(
                ICollectionSelector collectionSelector,
                ISpeedsGenerator speedsGenerator,
                IAttacksGenerator attacksGenerator,
                IFeatsGenerator featsGenerator,
                ISkillsGenerator skillsGenerator,
                IAlignmentGenerator alignmentGenerator,
                Dice dice,
                IMagicGenerator magicGenerator,
                ICreatureDataSelector creatureDataSelector,
                IAdjustmentsSelector adjustmentSelector)
            : base(
                  collectionSelector,
                  speedsGenerator,
                  attacksGenerator,
                  featsGenerator,
                  skillsGenerator,
                  alignmentGenerator,
                  dice,
                  magicGenerator,
                  creatureDataSelector,
                  adjustmentSelector)
        {
        }

        protected override string DragonSpecies => CreatureConstants.Templates.HalfDragon_White;
    }
}
