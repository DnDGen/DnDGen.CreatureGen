# CreatureGen

Generates a random, fleshed-out creature for Dungeons and Dragons 3.5

[![Build Status](https://dev.azure.com/dndgen/DnDGen/_apis/build/status/DnDGen.CreatureGen?branchName=main)](https://dev.azure.com/dndgen/DnDGen/_build/latest?definitionId=5&branchName=main)

## Installing CreatureGen

The project is on [NuGet](https://www.nuget.org/packages/DnDGen.CreatureGen).

    dotnet add package DnDGen.CreatureGen

## Getting the Generators

You can obtain generators from the IoC namespace within the domain project. Because the generators are very complex and are decorated in various ways, there is not a (recommended) way to build these generators manually. Please use the Module Loader in the IoC namespace. **Note**: This will also load dependencies of CreatureGen, including RollGen, Infrastructure, and TreasureGen.

```C#
var kernel = new StandardKernel();
var creatureGenModuleLoader = new CreatureGenModuleLoader();

creatureGenModuleLoader.LoadModules(kernel);

var creatureGenerator = kernel.Get<ICreatureGenerator>();
var creatureVerifier = kernel.Get<ICreatureVerifier>();
```

Your particular syntax for how the Ninject injection should work will depend on your project (class library, web site, etc.)

## Use

### Generating a specific creature

```C#
var ogre = creatureGenerator.Generate(false, CreatureConstants.Ogre);
var zombieOgre = creatureGenerator.Generate(false, CreatureConstants.Ogre, null, CreatureConstants.Templates.Zombie);
var humanCharacter = await creatureGenerator.GenerateAsync(true, CreatureConstants.Human);
```

The first argument, `asCharacter`, says whether the creature is generated as a character (a playable race that will take class levels) or as a monster.
Only creatures that can be characters may be generated as characters.
For example, a 1-Hit-Die humanoid such as a Human has its racial Hit Die replaced by class levels when generated as a character.

### Chaining templates

Templates are passed as the last arguments, and are applied in the order given.
Each template must be compatible with the creature as it is after the templates before it have been applied.

```C#
var halfDragonHalfFiend = creatureGenerator.Generate(
    false,
    CreatureConstants.Human,
    null,
    CreatureConstants.Templates.HalfDragon_Black,
    CreatureConstants.Templates.HalfFiend);
```

### Ability scores

When no `AbilityRandomizer` is passed in, abilities are rolled with the default roll (1d2+9, or 10-11) with no other modifications.

```C#
var abilityRandomizer = new AbilityRandomizer(AbilityConstants.RandomizerRolls.BestOfFour);
abilityRandomizer.PriorityAbility = AbilityConstants.Intelligence;

var elfLich = creatureGenerator.Generate(true, CreatureConstants.Elf_High, abilityRandomizer, CreatureConstants.Templates.Lich);

//INFO: Set rolls are used as-is instead of rolling
var setRandomizer = new AbilityRandomizer();
setRandomizer.SetRolls[AbilityConstants.Strength] = 18;
setRandomizer.SetRolls[AbilityConstants.Charisma] = 8;

var strongDwarf = creatureGenerator.Generate(true, CreatureConstants.Dwarf_Hill, setRandomizer);
```

The available rolls are in `AbilityConstants.RandomizerRolls`:

| Roll | Dice | Range |
| --- | --- | --- |
| `Poor` | 3d3 | 3-9 |
| `Default` | 1d2+9 | 10-11 |
| `Average` | 3d2+7 | 10-13 |
| `Good` | 3d2+10 | 13-16 |
| `Heroic` | 3d2+12 | 15-18 |
| `BestOfFour` | 4d6, keep the highest 3 | 3-18 |
| `OnesAsSixes` | 3d6, with 1s counted as 6s | 6-18 |
| `Raw` | 3d6 | 3-18 |
| `Wild` | 2d10 | 2-20 |

Some templates require a minimum ability score: a Ghost needs Charisma 6, and a Half-Celestial or Half-Fiend needs Intelligence 4.
When you generate one of these, any rolled score that would fall short is raised to meet the minimum.
If the randomizer can never reach the minimum (for example, a set roll that is too low), the creature is not compatible and an `InvalidCreatureException` is thrown.

### Random creatures

```C#
var (creatureName, templates) = creatureGenerator.GenerateRandomName(false);
var randomCreature = creatureGenerator.GenerateRandom(false);
var randomCharacter = await creatureGenerator.GenerateRandomAsync(true);

//INFO: A random creature with the given template(s)
var randomGhost = creatureGenerator.GenerateRandom(false, null, null, CreatureConstants.Templates.Ghost);

//INFO: Pass the None template to only get creatures without a template
var randomNonTemplated = creatureGenerator.GenerateRandom(false, null, null, CreatureConstants.Templates.None);
```

When no templates are passed in, the random creature may or may not have a template, weighted heavily in favor of creatures without a template.

### Filters

Filters narrow down which random creatures are valid. They are checked against the creature after all of its templates are applied.

- A creature satisfies a filter if it matches **any** of the values in that filter's list.
- A creature must satisfy **every** filter that has values.
- Null and empty filters are ignored, so `new Filters()` allows anything.

```C#
var filters = new Filters
{
    Types = [CreatureConstants.Types.Humanoid],
    ChallengeRatings = [ChallengeRatingConstants.CR3, ChallengeRatingConstants.CR4],
    Alignments = [AlignmentConstants.NeutralEvil, AlignmentConstants.ChaoticEvil],
};

var randomNameWithFilters = creatureGenerator.GenerateRandomName(false, filters);
var randomWithFilters = creatureGenerator.GenerateRandom(false, null, filters);
var randomHalfFiend = await creatureGenerator.GenerateRandomAsync(false, abilityRandomizer, filters, CreatureConstants.Templates.HalfFiend);
```

`Types` matches both creature types and subtypes, such as `CreatureConstants.Types.Subtypes.Reptilian`.
Creatures generated with a challenge rating filter are never advanced, so their challenge rating stays predictable.

### Checking compatibility

If the creature, templates, ability randomizer, and filters can't produce a valid creature, the generator throws an `InvalidCreatureException`.
To check ahead of time, use the `ICreatureVerifier`:

```C#
var canBeGhost = creatureVerifier.VerifyCompatibility(false, CreatureConstants.Ogre, null, null, CreatureConstants.Templates.Ghost);
var anyValidCreature = creatureVerifier.VerifyCompatibility(false, null, abilityRandomizer, filters);
```

When no templates are given, `VerifyCompatibility` returns true if any template (or no template) gives a valid creature.
Include `CreatureConstants.Templates.None` to only check creatures without a template.

## Upgrading from 1.x

Version 2.0.0 has breaking changes to the public API:

- **Templates moved out of `Filters`.** Pass templates as the trailing `params string[] templates` argument of `GenerateRandomName`, `GenerateRandom`, and `GenerateRandomAsync` instead of `Filters.Templates`.
- **Filters are now lists.** `Filters.Type`, `Filters.ChallengeRating`, and `Filters.Alignment` are replaced by `Filters.Types`, `Filters.ChallengeRatings`, and `Filters.Alignments`. A creature matches if it matches any value in a list.
- **`Filters.GetDescription(bool asCharacter)`** is now `Filters.GetDescription()`.
- **`AbilityRandomizer.Roll` is set through the constructor**, for example `new AbilityRandomizer(AbilityConstants.RandomizerRolls.Good)`. The parameterless constructor still uses the default roll.
- **`ICreatureVerifier.VerifyCompatibility`** now takes the ability randomizer and the templates: `VerifyCompatibility(asCharacter, creature, abilityRandomizer, filters, templates)`.
- **`InvalidCreatureException`** constructors now take `Filters`, an `AbilityRandomizer`, and templates, and the message includes all of them.
- **`CreaturePrototype` is now internal.**
- **New challenge ratings** `ChallengeRatingConstants.CR28`, `CR29`, and `CR30`.
