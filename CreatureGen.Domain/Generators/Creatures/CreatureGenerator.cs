using CreatureGen.Creatures;
using CreatureGen.Domain.Generators.Abilities;
using CreatureGen.Domain.Generators.Alignments;
using CreatureGen.Domain.Generators.Feats;
using CreatureGen.Domain.Generators.Skills;
using CreatureGen.Domain.Selectors.Collections;
using CreatureGen.Verifiers;
using DnDGen.Core.Selectors.Collections;
using DnDGen.Core.Selectors.Percentiles;
using System.Collections.Generic;

namespace CreatureGen.Domain.Generators.Creatures
{
    internal class CreatureGenerator : ICreatureGenerator
    {
        private readonly IAlignmentGenerator alignmentGenerator;
        private readonly IAdjustmentsSelector adjustmentsSelector;
        private readonly IRandomizerVerifier randomizerVerifier;
        private readonly IPercentileSelector percentileSelector;
        private readonly ICollectionSelector collectionsSelector;
        private readonly IAbilitiesGenerator abilitiesGenerator;
        private readonly ISkillsGenerator skillsGenerator;
        private readonly IFeatsGenerator featsGenerator;

        public CreatureGenerator(IAlignmentGenerator alignmentGenerator,
            IAdjustmentsSelector adjustmentsSelector,
            IRandomizerVerifier randomizerVerifier,
            IPercentileSelector percentileSelector,
            ICollectionSelector collectionsSelector,
            IAbilitiesGenerator abilitiesGenerator,
            ISkillsGenerator skillsGenerator,
            IFeatsGenerator featsGenerator)
        {
            this.alignmentGenerator = alignmentGenerator;
            this.abilitiesGenerator = abilitiesGenerator;
            this.skillsGenerator = skillsGenerator;
            this.featsGenerator = featsGenerator;

            this.adjustmentsSelector = adjustmentsSelector;
            this.randomizerVerifier = randomizerVerifier;
            this.percentileSelector = percentileSelector;
            this.collectionsSelector = collectionsSelector;
        }

        public IEnumerable<Creature> Generate(string creatureName, string template, int quantity)
        {
            var creatures = new List<Creature>();

            while (quantity-- > 0)
            {
                var creature = Generate(creatureName, template);
                creatures.Add(creature);
            }

            return creatures;
        }

        public Creature Generate(string creatureName, string template)
        {
            var creature = new Creature();
            creature.Name = creatureName;
            creature.Template = template;

            return creature;
        }
    }
}