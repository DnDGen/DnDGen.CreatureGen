using CreatureGen.Abilities;

namespace CreatureGen.Attacks
{
    public class SaveDieCheck
    {
        public Ability BaseAbility { get; set; }
        public int BaseValue { get; set; }
        public string Save { get; set; }

        public int Value => BaseValue + BaseAbility.Modifier;
    }
}
