namespace CreatureGen
{
    public class Bonus
    {
        public int Value { get; set; }
        public string Condition { get; set; }

        public bool IsConditional
        {
            get { return !string.IsNullOrEmpty(Condition); }
        }

        public Bonus()
        {
            Condition = string.Empty;
        }
    }
}
