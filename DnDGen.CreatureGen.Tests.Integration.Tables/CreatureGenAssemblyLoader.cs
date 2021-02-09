using DnDGen.CreatureGen.Tables;
using DnDGen.Infrastructure.Tables;
using System.Reflection;

namespace DnDGen.CreatureGen.Tests.Integration.Tables
{
    public class CreatureGenAssemblyLoader : AssemblyLoader
    {
        public Assembly GetRunningAssembly()
        {
            var type = typeof(TableNameConstants);
            return type.Assembly;
        }
    }
}
