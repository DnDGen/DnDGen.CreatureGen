namespace CreatureGen.Selectors.Selections
{
    internal class SkillBonusSelection
    {
        public string Skill { get; set; }
        public int Bonus { get; set; }
        public string Condition { get; set; }

        public SkillBonusSelection()
        {
            Skill = string.Empty;
            Condition = string.Empty;
        }
    }
}
