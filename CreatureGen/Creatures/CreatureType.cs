using System.Collections.Generic;
using System.Linq;

namespace CreatureGen.Creatures
{
    public class CreatureType
    {
        public string Name { get; set; }
        public IEnumerable<string> SubTypes { get; set; }

        public CreatureType()
        {
            Name = string.Empty;
            SubTypes = Enumerable.Empty<string>();
        }

        public bool Is(string creatureType)
        {
            return creatureType == Name || SubTypes.Contains(creatureType);
        }
    }
}
