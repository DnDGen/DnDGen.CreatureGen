using System.Collections.Generic;

namespace DnDGen.CreatureGen.Generators.Creatures
{
    public class Filters
    {
        public List<string> Templates { get; set; }
        public string Type { get; set; }
        public string ChallengeRating { get; set; }
        public string Alignment { get; set; }

        public Filters()
        {
            Templates = new List<string>();
        }
    }
}
