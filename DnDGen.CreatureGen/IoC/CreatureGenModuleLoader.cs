using DnDGen.CreatureGen.IoC.Modules;
using Ninject;

namespace DnDGen.CreatureGen.IoC
{
    public class CreatureGenModuleLoader
    {
        public void LoadModules(IKernel kernel)
        {
            kernel.Load<GeneratorsModule>();
            kernel.Load<SelectorsModule>();
        }
    }
}