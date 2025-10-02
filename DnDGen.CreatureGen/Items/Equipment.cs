using DnDGen.TreasureGen.Items;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Items
{
    public class Equipment
    {
        public IEnumerable<Weapon> Weapons { get; set; }
        public Armor Armor { get; set; }
        public Armor Shield { get; set; }
        public IEnumerable<Item> Items { get; set; }

        public Equipment()
        {
            Weapons = [];
            Items = [];
        }
    }
}
