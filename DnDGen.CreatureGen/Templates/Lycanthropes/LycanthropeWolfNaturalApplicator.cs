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
    internal class LycanthropeWolfNaturalApplicator : LycanthropeApplicator
    {
        protected override string LycanthropeSpecies => CreatureConstants.Templates.Lycanthrope_Wolf_Natural;
        protected override string AnimalSpecies => CreatureConstants.Wolf;

        public LycanthropeWolfNaturalApplicator(
            ICollectionSelector collectionSelector,
            ICreatureDataSelector creatureDataSelector,
            IHitPointsGenerator hitPointsGenerator,
            Dice dice,
            ITypeAndAmountSelector typeAndAmountSelector,
            IFeatsGenerator featsGenerator,
            IAttacksGenerator attacksGenerator,
            ISavesGenerator savesGenerator,
            ISkillsGenerator skillsGenerator,
            ISpeedsGenerator speedsGenerator)
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
                  speedsGenerator)
        {
        }
    }
}
