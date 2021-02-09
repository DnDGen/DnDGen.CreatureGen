using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Magics
{
    public class Spell
    {
        public string Source { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Metamagic { get; set; }

        public Spell()
        {
            Name = string.Empty;
            Source = string.Empty;
            Metamagic = Enumerable.Empty<string>();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Spell))
                return false;

            var otherSpell = obj as Spell;

            return Level == otherSpell.Level && Name == otherSpell.Name && Source == otherSpell.Source;
        }

        public override int GetHashCode()
        {
            return Level.GetHashCode() + Name.GetHashCode() + Source.GetHashCode();
        }
    }
}
