namespace DnDGen.CreatureGen.Attacks
{
    public class Damage
    {
        public string Roll { get; set; }
        public string Type { get; set; }

        public Damage()
        {
            Roll = string.Empty;
            Type = string.Empty;
        }

        public override string ToString()
        {
            return $"{Roll} {Type}".Trim();
        }
    }
}
