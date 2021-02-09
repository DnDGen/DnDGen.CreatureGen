using DnDGen.CreatureGen.Abilities;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Magics
{
    public class Magic
    {
        public IEnumerable<SpellQuantity> SpellsPerDay { get; set; }
        public IEnumerable<Spell> KnownSpells { get; set; }
        public IEnumerable<Spell> PreparedSpells { get; set; }
        public int ArcaneSpellFailure { get; set; }
        public string Caster { get; set; }
        public int CasterLevel { get; set; }
        public IEnumerable<string> Domains { get; set; }
        public Ability CastingAbility { get; set; }

        public Magic()
        {
            SpellsPerDay = Enumerable.Empty<SpellQuantity>();
            KnownSpells = Enumerable.Empty<Spell>();
            PreparedSpells = Enumerable.Empty<Spell>();
            Caster = string.Empty;
            Domains = Enumerable.Empty<string>();
        }
    }
}