using CreatureGen.Tables;
using DnDGen.Core.Tables;
using System.Reflection;

namespace CreatureGen.Tests.Integration.Tables
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
