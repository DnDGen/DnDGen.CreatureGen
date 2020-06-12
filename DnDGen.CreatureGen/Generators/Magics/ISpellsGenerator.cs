using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Alignments;
using DnDGen.CreatureGen.Magics;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Magics
{
    internal interface ISpellsGenerator
    {
        IEnumerable<SpellQuantity> GeneratePerDay(string caster, int casterLevel, Dictionary<string, Ability> abilities, params string[] domains);
        IEnumerable<Spell> GenerateKnown(string creature, string caster, int casterLevel, Alignment alignment, Dictionary<string, Ability> abilities, params string[] domains);
        IEnumerable<Spell> GeneratePrepared(IEnumerable<Spell> knownSpells, IEnumerable<SpellQuantity> spellsPerDay, params string[] domains);
    }
}
