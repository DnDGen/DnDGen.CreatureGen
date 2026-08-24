using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Generators.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Verifiers;
using DnDGen.CreatureGen.Verifiers.Exceptions;
using DnDGen.Infrastructure.Selectors.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Generators.Alignments
{
    internal class AlignmentGenerator(ICollectionSelector collectionSelector, ICreatureVerifier creatureVerifier) : IAlignmentGenerator
    {
        public Alignment Generate(string creatureName, string[] templates, Filters filters)
        {
            templates ??= [];
            var weightedAlignments = GetWeightedAlignments(creatureName, templates, filters);

            if (filters?.Alignments?.Count > 0)
            {
                //INFO: Doing Where instead of Intersect in order to preserve weighting
                weightedAlignments = weightedAlignments.Where(filters.Alignments.Contains);
            }

            if (!weightedAlignments.Any())
                throw new InvalidCreatureException(
                    $"Creature {creatureName} has no valid alignments for templates [{string.Join(", ", templates)}]",
                    false,
                    creatureName,
                    filters);

            var randomAlignment = collectionSelector.SelectRandomFrom(weightedAlignments);
            return new Alignment(randomAlignment);
        }

        private IEnumerable<string> GetWeightedAlignments(string creatureName, string[] templates, Filters filters)
        {
            var weightedAlignments = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, creatureName);

            if (templates.Length == 0)
                return weightedAlignments;

            if (templates.Length == 1)
            {
                var templateAlignments = collectionSelector.SelectFrom(Config.Name, TableNameConstants.Collection.AlignmentGroups, templates[0] + GroupConstants.AllowedInput);

                //INFO: Doing this instead of intersect in order to preserve duplicates/weighting
                weightedAlignments = weightedAlignments.Where(templateAlignments.Contains);

                return weightedAlignments;
            }

            //INFO: When multiple templates are applied, the following is true for alignments:
            //1. Lycanthrope and Ghost allow all inputs, make no alterations
            //2. Celestial Creature and Half-Celestial only allow non-Evil inputs, outputs always Good
            //3. Fiendish Creature and Half-Fiend only allow non-Good inputs, outputs always Evil
            //4. Lich and Vampire allow all inputs, outsputs always Evil
            //5. Skeleton and Zombie allow all inputs, outputs always Neutral Evil
            //6. Half-Dragons allow all inputs, output is always the dragon's alignmnt (Lawful Good, Chaotic Good, Lawful Evil, or Chaotic Evil)

            var prototype = creatureVerifier.GetChainedTemplates([creatureName], templates, false, null, filters).Single();

            return weightedAlignments;
        }
    }
}