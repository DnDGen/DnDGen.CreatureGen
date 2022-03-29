using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Generators.Abilities;
using System.Threading.Tasks;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public interface ICreatureGenerator
    {
        Creature Generate(bool asCharacter, string creatureName, AbilityRandomizer abilityRandomizer = null, params string[] templates);
        Task<Creature> GenerateAsync(bool asCharacter, string creatureName, AbilityRandomizer abilityRandomizer = null, params string[] templates);

        (string Creature, string Template) GenerateRandomName(bool asCharacter, Filters filters = null);
        Creature GenerateRandom(bool asCharacter, AbilityRandomizer abilityRandomizer = null, Filters filters = null);
        Task<Creature> GenerateRandomAsync(bool asCharacter, AbilityRandomizer abilityRandomizer = null, Filters filters = null);
    }
}