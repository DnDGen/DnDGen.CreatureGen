using DnDGen.CreatureGen.Abilities;

namespace DnDGen.CreatureGen.Attacks
{
    public class SaveDieCheck
    {
        public Ability BaseAbility { get; set; }
        public int BaseValue { get; set; }
        public string Save { get; set; }

        public int DC => BaseValue + BaseAbility.Modifier;
    }
}
