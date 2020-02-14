using DnDGen.TreasureGen.Items;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Items
{
    public class Equipment
    {
        public IEnumerable<Item> Weapons { get; set; }
        public Item Armor { get; set; }
        public Item Shield { get; set; }
        public IEnumerable<Item> Items { get; set; }

        public Equipment()
        {
            Weapons = Enumerable.Empty<Item>();
            Items = Enumerable.Empty<Item>();
        }
    }
}
