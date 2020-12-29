# CharacterGen

Generates a random, fleshed-out creature for Dungeons and Dragons 3.X

[![Build Status](https://dev.azure.com/dndgen/DnDGen/_apis/build/status/DnDGen.CreatureGen?branchName=master)](https://dev.azure.com/dndgen/DnDGen/_build/latest?definitionId=5&branchName=master)

### Use

To use CreatureGen, simply use the CreatureGenerator.

```C#
var creature = creatureGenerator.GenerateWith(CreatureConstants.Ogre, CreatureConstants.Templates.Zombie);
```

### Getting the Generators

You can obtain generators from the IoC namespace within the domain project.  Because the generators are very complex and are decorated in various ways, there is not a (recommended) way to build these generator manually.  Please use the IoC container in the Domain package.  **Note:** if using the CreatureGen IoC container, be sure to also load modules for RollGen, DnDGen.Core, EventGen, and TreasureGen, as it is dependent on those modules

```C#
var kernel = new StandardKernel();
var rollGenModuleLoader = new RollGenModuleLoader();
var infrastructureModuleLoader = new InfrastructureModuleLoader();
var treasureGenModuleLoader = new TreasureGenModuleLoader();
var creatureGenModuleLoader = new CreatureGenModuleLoader();

rollGenModuleLoader.LoadModules(kernel);
infrastructureModuleLoader.LoadModules(kernel);
treasureGenModuleLoader.LoadModules(kernel);
creatureGenModuleLoader.LoadModules(kernel);
```

Your particular syntax for how the Ninject injection should work will depend on your project (class library, web site, etc.)

### Installing CreatureGen

The project is on [Nuget](https://www.nuget.org/packages/CreatureGen). Install via the NuGet Package Manager.

    PM > Install-Package CreatureGen
