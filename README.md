# CreatureGen

Generates a random, fleshed-out creature for Dungeons and Dragons 3.X

[![Build Status](https://dev.azure.com/dndgen/DnDGen/_apis/build/status/DnDGen.CreatureGen?branchName=master)](https://dev.azure.com/dndgen/DnDGen/_build/latest?definitionId=5&branchName=master)

### Use

To use CreatureGen, simply use the CreatureGenerator.

```C#
var creature = creatureGenerator.Generate(CreatureConstants.Ogre, CreatureConstants.Templates.Zombie, false);
var asyncCreature = await creatureGenerator.GenerateAsync(CreatureConstants.Human, CreatureConstants.Templates.None, false);
var character = creatureGenerator.Generate(CreatureConstants.Human, CreatureConstants.Templates.None, true);

//INFO: When the ability randomizer is not passed in, it defaults to no modifications and the default roll (1d2+9, or 10-11)
var abilityRandomizer = new AbilityRandomizer();
abilityRandomizer.Roll = AbilityConstants.RandomizerRolls.Best;
abilityRandomizer.PriorityAbility = AbilityConstants.Intelligence;

var creatureWithAbilities = creatureGenerator.Generate(CreatureConstants.Elf_High, CreatureConstants.Templates.Lich, true, abilityRandomizer);

var randomName = creatureGenerator.GenerateRandomName(false);
var randomCharacter = creatureGenerator.GenerateRandomName(true);

//INFO: When the filters are not passed, they default to allowing anything.
//To not have a particular filter set, simply leave it as null
var filters = new Filters();
filters.Template = CreatureConstants.Templates.HalfFiend;
filters.Type = CreatureConstants.Types.Humanoid;
filters.ChallengeRating = ChallengeRatingConstants.CR3;
filters.Alignment = AlignmentConstants.NeutralEvil;

var randomNameWithFilters = creatureGenerator.GenerateRandomName(false, filters);
var randomWithFilters = creatureGenerator.GenerateRandom(false, null, filters);
var randomAsyncWithFilters = await creatureGenerator.GenerateRandomAsync(false, abilityRandomizer, filters);
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
