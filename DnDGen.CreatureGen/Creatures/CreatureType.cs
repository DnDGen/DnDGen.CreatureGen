﻿using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Creatures
{
    public class CreatureType
    {
        public string Name { get; set; }
        public IEnumerable<string> SubTypes { get; set; }

        public CreatureType()
        {
            Name = string.Empty;
            SubTypes = [];
        }

        public CreatureType(IEnumerable<string> types)
        {
            Name = types.First();
            SubTypes = types.Skip(1);
        }

        public bool Is(string creatureType)
        {
            return creatureType == Name || SubTypes.Contains(creatureType);
        }

        public IEnumerable<string> AllTypes => new[] { Name }.Union(SubTypes);
    }
}
