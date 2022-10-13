using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Attacks;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Generators.Defenses;
using DnDGen.CreatureGen.Generators.Feats;
using DnDGen.CreatureGen.Generators.Skills;
using DnDGen.CreatureGen.Selectors.Collections;
using DnDGen.Infrastructure.Selectors.Collections;
using DnDGen.RollGen;

namespace DnDGen.CreatureGen.Templates.Lycanthropes
{
    internal class LycanthropeDireTigerAfflictedApplicator : LycanthropeApplicator
    {
        protected override string LycanthropeSpecies => CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted;
        protected override string AnimalSpecies => CreatureConstants.Tiger_Dire;

        public LycanthropeDireTigerAfflictedApplicator(
            ICollectionSelector collectionSelector,
            ICreatureDataSelector creatureDataSelector,
            IHitPointsGenerator hitPointsGenerator,
            Dice dice,
            ITypeAndAmountSelector typeAndAmountSelector,
            IFeatsGenerator featsGenerator,
            IAttacksGenerator attacksGenerator,
            ISavesGenerator savesGenerator,
            ISkillsGenerator skillsGenerator,
            ISpeedsGenerator speedsGenerator,
            IAdjustmentsSelector adjustmentsSelector,
            ICreaturePrototypeFactory creaturePrototypeFactory)
            : base(
                  collectionSelector,
                  creatureDataSelector,
                  hitPointsGenerator,
                  dice,
                  typeAndAmountSelector,
                  featsGenerator,
                  attacksGenerator,
                  savesGenerator,
                  skillsGenerator,
                  speedsGenerator,
                  adjustmentsSelector,
                  creaturePrototypeFactory)
        {
        }
    }
}
