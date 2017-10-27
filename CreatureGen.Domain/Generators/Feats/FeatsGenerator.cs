using CreatureGen.Abilities;
using CreatureGen.CharacterClasses;
using CreatureGen.Combats;
using CreatureGen.Feats;
using CreatureGen.Creatures;
using CreatureGen.Skills;
using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Domain.Generators.Feats
{
    internal class FeatsGenerator : IFeatsGenerator
    {
        private readonly IRacialFeatsGenerator racialFeatsGenerator;
        private readonly IClassFeatsGenerator classFeatsGenerator;
        private readonly IAdditionalFeatsGenerator additionalFeatsGenerator;

        public FeatsGenerator(IRacialFeatsGenerator racialFeatsGenerator, IClassFeatsGenerator classFeatsGenerator, IAdditionalFeatsGenerator additionalFeatsGenerator)
        {
            this.racialFeatsGenerator = racialFeatsGenerator;
            this.classFeatsGenerator = classFeatsGenerator;
            this.additionalFeatsGenerator = additionalFeatsGenerator;
        }

        public FeatCollections GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills, BaseAttack baseAttack)
        {
            var featCollections = new FeatCollections();
            featCollections.Racial = racialFeatsGenerator.GenerateWith(race, skills, abilities);
            featCollections.Class = classFeatsGenerator.GenerateWith(characterClass, race, abilities, featCollections.Racial, skills);

            var automaticFeats = featCollections.All.ToArray();
            featCollections.Additional = additionalFeatsGenerator.GenerateWith(characterClass, race, abilities, skills, baseAttack, automaticFeats);

            return featCollections;
        }
    }
}