# CharacterGen

Generates a random, fleshed-out creature for Dungeons and Dragons 3.X

[![Build Status](https://travis-ci.org/DnDGen/CreatureGen.svg?branch=master)](https://travis-ci.org/DnDGen/CreatureGen)

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
var coreModuleLoader = new CoreModuleLoader();
var eventGenModuleLoader = new EventGenModuleLoader();
var treasureGenModuleLoader = new TreasureGenModuleLoader();
var creatureGenModuleLoader = new CreatureGenModuleLoader();

rollGenModuleLoader.LoadModules(kernel);
coreModuleLoader.LoadModules(kernel);
coreModuleLoader.LoadModules(kernel);
treasureGenModuleLoader.LoadModules(kernel);
creatureGenModuleLoader.LoadModules(kernel);
```

Your particular syntax for how the Ninject injection should work will depend on your project (class library, web site, etc.)

### Installing CreatureGen

The project is on [Nuget](https://www.nuget.org/packages/CreatureGen). Install via the NuGet Package Manager.

    PM > Install-Package CreatureGen

#### There's CreatureGen and CreatureGen.Domain - which do I install?

That depends on your project.  If you are making a library that will only **reference** CreatureGen, but does not expressly implement it (such as the EncounterGen project), then you only need the CreatureGen package.  If you actually want to run and implement the dice (such as in DnDGen.Web or in the tests for EncounterGen), then you need CreatureGen.Domain, which will install CreatureGen as a dependency.
