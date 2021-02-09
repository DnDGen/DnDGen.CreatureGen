# CharacterGen

Generates a random, fleshed-out creature for Dungeons and Dragons 3.X

[![Build Status](https://dev.azure.com/dndgen/DnDGen/_apis/build/status/DnDGen.CreatureGen?branchName=master)](https://dev.azure.com/dndgen/DnDGen/_build/latest?definitionId=5&branchName=master)

### Use

To use CreatureGen, simply use the CreatureGenerator.

```C#
var creature = creatureGenerator.GenerateWith(CreatureConstants.Ogre, CreatureConstants.Templates.Zombie);
```

### Getting the Generators

You can obtain generators from the IoC namespace within the domain project.  Because the generators are very complex and are decorated in various ways, there is not a (recommended) way to build these generator manually.  Please use the Module Loader in the IoC domain.  **Note**: This will also load dependencies of CreatureGen, including RollGen, Infrastructure, and TreasureGen

```C#
var kernel = new StandardKernel();
var creatureGenModuleLoader = new CreatureGenModuleLoader();

creatureGenModuleLoader.LoadModules(kernel);
```

Your particular syntax for how the Ninject injection should work will depend on your project (class library, web site, etc.)

### Installing CreatureGen

The project is on [Nuget](https://www.nuget.org/packages/CreatureGen). Install via the NuGet Package Manager.

    PM > Install-Package CreatureGen
