using System;

namespace DnDGen.CreatureGen.Magics
{
    public class SpellQuantity
    {
        public string Source { get; set; }
        public int Level { get; set; }
        public int Quantity { get; set; }
        public int BonusSpells { get; set; }
        public bool HasDomainSpell { get; set; }

        public int TotalQuantity => Quantity + BonusSpells + Convert.ToInt32(HasDomainSpell);

        public SpellQuantity()
        {
            Source = string.Empty;
        }
    }
}
