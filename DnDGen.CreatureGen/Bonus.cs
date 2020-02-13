using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DnDGen.CreatureGen.Tests.Integration")]
[assembly: InternalsVisibleTo("DnDGen.CreatureGen.Tests.Integration.IoC")]
[assembly: InternalsVisibleTo("DnDGen.CreatureGen.Tests.Integration.Tables")]
[assembly: InternalsVisibleTo("DnDGen.CreatureGen.Tests.Unit")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace DnDGen.CreatureGen
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
