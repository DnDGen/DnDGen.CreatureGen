using CreatureGen.IoC.Modules;
using Ninject;

namespace CreatureGen.IoC
{
    public class CharacterGenModuleLoader
    {
        public void LoadModules(IKernel kernel)
        {
            kernel.Load<GeneratorsModule>();
            kernel.Load<SelectorsModule>();
        }
    }
}