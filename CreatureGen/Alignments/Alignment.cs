namespace CreatureGen.Alignments
{
    public class Alignment
    {
        public string Lawfulness { get; set; }
        public string Goodness { get; set; }

        public string Full
        {
            get
            {
                if (Lawfulness == AlignmentConstants.Neutral && Goodness == AlignmentConstants.Neutral)
                    return "True Neutral";

                return $"{Lawfulness} {Goodness}".Trim();
            }
        }

        public Alignment()
        {
            Lawfulness = string.Empty;
            Goodness = string.Empty;
        }

        public Alignment(string alignment)
        {
            var parts = alignment.Split(' ');

            if (parts.Length != 2)
            {
                Lawfulness = string.Empty;
                Goodness = string.Empty;
            }
            else
            {
                Lawfulness = parts[0];
                Goodness = parts[1];
            }

            if (Lawfulness == "True")
                Lawfulness = AlignmentConstants.Neutral;
        }

        public override string ToString()
        {
            return Full;
        }

        public override bool Equals(object toCompare)
        {
            if (!(toCompare is Alignment))
                return false;

            var alignment = toCompare as Alignment;
            return Full == alignment.Full;
        }

        public override int GetHashCode()
        {
            return Full.GetHashCode();
        }
    }
}