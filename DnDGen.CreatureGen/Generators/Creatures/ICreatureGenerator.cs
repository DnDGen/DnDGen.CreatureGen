using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public interface ICreatureGenerator
    {
        Creature Generate(string creatureName, string template, bool asCharacter, AbilityRandomizer abilityRandomizer = null);
        Task<Creature> GenerateAsync(string creatureName, string template, bool asCharacter, AbilityRandomizer abilityRandomizer = null);

        (string Creature, string Template) GenerateRandomName(bool asCharacter, RandomFilters filters = null);
        Creature GenerateRandom(bool asCharacter, AbilityRandomizer abilityRandomizer = null, RandomFilters filters = null);
        Task<Creature> GenerateRandomAsync(bool asCharacter, AbilityRandomizer abilityRandomizer = null, RandomFilters filters = null);
    }
}