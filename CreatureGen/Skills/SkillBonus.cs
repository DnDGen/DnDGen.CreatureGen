namespace CreatureGen.Skills
{
    public class SkillBonus
    {
        public int Value { get; set; }
        public string Condition { get; set; }

        public bool IsConditional
        {
            get { return !string.IsNullOrEmpty(Condition); }
        }

        public SkillBonus()
        {
            Condition = string.Empty;
        }
    }
}
