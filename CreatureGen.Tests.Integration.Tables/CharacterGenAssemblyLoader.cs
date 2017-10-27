using CreatureGen.Domain.Tables;
using DnDGen.Core.Tables;
using System.Reflection;

namespace CreatureGen.Tests.Integration.Tables
{
    public class CharacterGenAssemblyLoader : AssemblyLoader
    {
        public Assembly GetRunningAssembly()
        {
            var type = typeof(TableNameConstants);
            return type.Assembly;
        }
    }
}
