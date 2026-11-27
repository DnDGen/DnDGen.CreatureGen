# CreatureGen

Generates a random, fleshed-out creature for Dungeons and Dragons 3.5

[![Build Status](https://github.com/DnDGen/DnDGen.CreatureGen/actions/workflows/release.yml/badge.svg?branch=main)](https://github.com/DnDGen/DnDGen.CreatureGen/actions/workflows/release.yml?query=branch%3Amain) [![Theme Song](https://img.shields.io/badge/theme_song-Suno-ff69b4)](https://suno.com/s/idhKDWtBG8g26Ar3)

## Installing CreatureGen

The project is on [NuGet](https://www.nuget.org/packages/DnDGen.CreatureGen). It targets .NET 10.

    dotnet add package DnDGen.CreatureGen

## Getting the Generators

You can obtain generators from the IoC namespace within the domain project. Because the generators are very complex and are decorated in various ways, there is not a (recommended) way to build these generators manually. Please use the Module Loader in the IoC namespace. **Note**: This will also load dependencies of CreatureGen, including RollGen, Infrastructure, and TreasureGen.

```C#
var kernel = new StandardKernel();
CreatureGenModuleLoader.LoadModules(kernel);

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

You can also pass any other valid roll, such as `2d6+6`, to the constructor: `new AbilityRandomizer("2d6+6")`.

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

## Reference

Members are written as signatures, with types, parameter names, and defaults. An `Async` method returns a `Task` of the same result as the method it is named for.
Properties with no setter are calculated from the others.

### Generating

#### ICreatureGenerator

Generates creatures. Throws an `InvalidCreatureException` when the creature, templates, ability randomizer, and filters can't produce a valid creature.

| Method | Returns |
| --- | --- |
| `Generate(bool asCharacter, string creatureName, AbilityRandomizer abilityRandomizer = null, params string[] templates)` | The named `Creature`, with the templates applied in order |
| `GenerateAsync(bool asCharacter, string creatureName, AbilityRandomizer abilityRandomizer = null, params string[] templates)` | The same, without blocking |
| `GenerateRandomName(bool asCharacter, Filters filters = null, params string[] templates)` | The name and templates of a random creature that satisfies the filters, without generating it |
| `GenerateRandom(bool asCharacter, AbilityRandomizer abilityRandomizer = null, Filters filters = null, params string[] templates)` | A random `Creature` that satisfies the filters; random templates are chosen when none are given |
| `GenerateRandomAsync(bool asCharacter, AbilityRandomizer abilityRandomizer = null, Filters filters = null, params string[] templates)` | The same, without blocking |

#### ICreatureVerifier

| Method | Returns |
| --- | --- |
| `VerifyCompatibility(bool asCharacter, string creature = null, AbilityRandomizer abilityRandomizer = null, Filters filters = null, params string[] templates)` | Whether a valid creature can be generated with these arguments. A null creature means any creature, and no templates means any template or none |

#### CreatureGenModuleLoader

Loads CreatureGen, and its dependencies, into a Ninject kernel.

| Constructor |
| --- |
| `CreatureGenModuleLoader()` |

| Method | Returns |
| --- | --- |
| `LoadModules(IKernel kernel)` | Nothing; the generators can then be taken from the kernel |

#### AbilityRandomizer

Decides how ability scores are rolled. The default roll is `AbilityConstants.RandomizerRolls.Default`.

| Constructor |
| --- |
| `AbilityRandomizer(string roll)` |
| `AbilityRandomizer()` |

| Method | Returns |
| --- | --- |
| `Validate(Dice dice)` | Whether the roll and set rolls can produce a score of at least 1 |

| Property | Type | Description |
| --- | --- | --- |
| `Roll` | `string` | The roll used for every ability without a set roll (read only; set by the constructor) |
| `SetRolls` | `Dictionary<string, int>` | Abilities with a fixed score, used as-is instead of rolling. Empty by default |
| `PriorityAbility` | `string` | The ability that gets the best score, if any |
| `AbilityAdvancements` | `Dictionary<string, int>` | Extra points to add to abilities when the creature is advanced. Empty by default |

#### Filters

Narrows down which random creatures are valid. Each list is empty by default. A creature satisfies a list if it matches any of its values, and must satisfy every list that has values.

| Constructor |
| --- |
| `Filters()` |

| Method | Returns |
| --- | --- |
| `GetDescription()` | The alignments, challenge ratings, and types of the filters, as text |

| Property | Type | Description |
| --- | --- | --- |
| `Types` | `List<string>` | Creature types and subtypes, from `CreatureConstants.Types` |
| `ChallengeRatings` | `List<string>` | From `ChallengeRatingConstants` |
| `Alignments` | `List<string>` | Full alignments, from `AlignmentConstants` |

#### InvalidCreatureException

Thrown when a valid creature can't be generated. Its message describes the reason, and everything that was asked for.

| Constructor |
| --- |
| `InvalidCreatureException(string reason, bool asCharacter, string creature = null, Filters filters = null, AbilityRandomizer abilityRandomizer = null, params string[] templates)` |
| `InvalidCreatureException(string reason, bool asCharacter, string creature, string abilityRoll, Filters filters, params string[] templates)` |

| Property | Type | Description |
| --- | --- | --- |
| `Message` | `string` | The reason, as character or monster, creature, templates, filters, and ability randomizer |

### Creatures

#### Creature

The generated creature, and the sum of everything below. Collections are empty, not null, when the creature has none.

| Constructor |
| --- |
| `Creature()` |

| Property | Type | Description |
| --- | --- | --- |
| `Name` | `string` | The creature, from `CreatureConstants` |
| `Type` | `CreatureType` | Its type and subtypes |
| `Abilities` | `Dictionary<string, Ability>` | Keyed by `AbilityConstants` |
| `ChallengeRating` | `string` | From `ChallengeRatingConstants` |
| `Alignment` | `Alignment` | Its alignment |
| `LevelAdjustment` | `int?` | Null when the creature can't be a character |
| `CasterLevel` | `int` | The level at which it casts spells |
| `Templates` | `List<string>` | The templates applied, in order |
| `Size` | `string` | From `SizeConstants` |
| `HitPoints` | `HitPoints` | Its hit dice and hit points |
| `Languages` | `IEnumerable<string>` | From `LanguageConstants` |
| `IsAdvanced` | `bool` | Whether its hit dice were advanced |
| `Demographics` | `Demographics` | Its gender, age, and measurements |
| `HasSkeleton` | `bool` | Whether it has a skeleton |
| `InitiativeBonus` | `int` | The bonus before its Dexterity modifier |
| `TotalInitiativeBonus` | `int` | The bonus including its Dexterity modifier |
| `Speeds` | `Dictionary<string, Measurement>` | Keyed by `SpeedConstants` |
| `ArmorClass` | `ArmorClass` | Its armor class |
| `BaseAttackBonus` | `int` | Its base attack bonus |
| `GrappleBonus` | `int?` | Null when the creature can't grapple |
| `Space` | `Measurement` | The space it takes up |
| `Reach` | `Measurement` | How far it reaches |
| `SpecialQualities` | `IEnumerable<Feat>` | Its special qualities |
| `Saves` | `Dictionary<string, Save>` | Keyed by `SaveConstants` |
| `Skills` | `IEnumerable<Skill>` | Its skills |
| `Feats` | `IEnumerable<Feat>` | Its feats |
| `NumberOfHands` | `int` | How many hands it has |
| `CanUseEquipment` | `bool` | Whether it can use weapons, armor, and items |
| `Attacks` | `IEnumerable<Attack>` | All of its attacks |
| `Magic` | `Magic` | Its spells and casting |
| `FullMeleeAttack` | `IEnumerable<Attack>` | The melee attacks it makes in a full attack (read only) |
| `FullRangedAttack` | `IEnumerable<Attack>` | The ranged attacks it makes in a full attack (read only) |
| `SpecialAttacks` | `IEnumerable<Attack>` | Its special attacks (read only) |
| `MeleeAttack` | `Attack` | Its primary melee attack (read only) |
| `RangedAttack` | `Attack` | Its primary ranged attack (read only) |
| `Summary` | `string` | The creature's name and templates (read only) |
| `Equipment` | `Equipment` | What it carries |

#### CreatureType

| Constructor |
| --- |
| `CreatureType()` |
| `CreatureType(IEnumerable<string> types)` |

| Method | Returns |
| --- | --- |
| `Is(string creatureType)` | Whether the name or any subtype is the given type |

| Property | Type | Description |
| --- | --- | --- |
| `Name` | `string` | The creature type, such as Humanoid |
| `SubTypes` | `IEnumerable<string>` | Its subtypes, such as Reptilian |
| `AllTypes` | `IEnumerable<string>` | The name and the subtypes (read only) |

#### Demographics

| Constructor |
| --- |
| `Demographics()` |

| Property | Type | Description |
| --- | --- | --- |
| `Gender` | `string` | From `GenderConstants` |
| `Age` | `Measurement` | Its current age |
| `MaximumAge` | `Measurement` | The age it can reach |
| `Height` | `Measurement` | Standing height |
| `Length` | `Measurement` | Length, for creatures that aren't upright |
| `Weight` | `Measurement` | Its weight |
| `Wingspan` | `Measurement` | Its wingspan, if it has wings |
| `Skin` | `string` | Description of its skin |
| `Hair` | `string` | Description of its hair |
| `Eyes` | `string` | Description of its eyes |
| `Other` | `string` | Any other notable appearance |

#### Measurement

A value with a unit, such as 30 feet or 120 pounds.

| Constructor |
| --- |
| `Measurement(string unit)` |

| Method | Returns |
| --- | --- |
| `AddBonus(int value, string condition)` | Nothing; adds a bonus to `Bonuses` |

| Property | Type | Description |
| --- | --- | --- |
| `Unit` | `string` | The unit of the value (set by the constructor) |
| `Value` | `double` | The value |
| `Description` | `string` | The value in words, such as an age category. Empty by default |
| `Bonuses` | `List<Bonus>` | The bonuses to the value |

#### Alignment

| Constructor |
| --- |
| `Alignment()` |
| `Alignment(string alignment)` |

| Property | Type | Description |
| --- | --- | --- |
| `Lawfulness` | `string` | Lawful, Neutral, or Chaotic |
| `Goodness` | `string` | Good, Neutral, or Evil |
| `Full` | `string` | Both together, such as Lawful Good (read only) |

#### Bonus

A modifier, with the condition that it applies under, if any.

| Constructor |
| --- |
| `Bonus()` |

| Property | Type | Description |
| --- | --- | --- |
| `Value` | `int` | The size of the bonus |
| `Condition` | `string` | When it applies |
| `IsConditional` | `bool` | Whether it has a condition (read only) |

#### Equipment

| Constructor |
| --- |
| `Equipment()` |

| Property | Type | Description |
| --- | --- | --- |
| `Weapons` | `IEnumerable<Weapon>` | Its weapons, from TreasureGen |
| `Armor` | `Armor` | Its armor, from TreasureGen |
| `Shield` | `Armor` | Its shield, from TreasureGen |
| `Items` | `IEnumerable<Item>` | Everything else it carries, from TreasureGen |

#### SizeConstants

| Constructor |
| --- |
| `SizeConstants()` |

| Method | Returns |
| --- | --- |
| `GetOrdered()` | The sizes, from smallest to largest |

| Property | Type | Description |
| --- | --- | --- |
| `Colossal` | `string` | Size category |
| `Gargantuan` | `string` | Size category |
| `Huge` | `string` | Size category |
| `Large` | `string` | Size category |
| `Medium` | `string` | Size category |
| `Small` | `string` | Size category |
| `Tiny` | `string` | Size category |
| `Diminutive` | `string` | Size category |
| `Fine` | `string` | Size category |

#### SpeedConstants

| Constructor |
| --- |
| `SpeedConstants()` |

| Property | Type | Description |
| --- | --- | --- |
| `Burrow` | `string` | Kind of speed |
| `Climb` | `string` | Kind of speed |
| `Fly` | `string` | Kind of speed |
| `Land` | `string` | Kind of speed |
| `Swim` | `string` | Kind of speed |

### Abilities and defenses

#### Ability

| Constructor |
| --- |
| `Ability(string name)` |

| Property | Type | Description |
| --- | --- | --- |
| `DefaultScore` | `int` | 10, the score when nothing changes it |
| `Name` | `string` | From `AbilityConstants` (set by the constructor) |
| `BaseScore` | `int` | The rolled score. Starts at `DefaultScore` |
| `RacialAdjustment` | `int` | The adjustment from the creature |
| `AdvancementAdjustment` | `int` | The adjustment from advancing |
| `AgeAdjustment` | `int` | The adjustment from age |
| `TemplateAdjustment` | `int` | The adjustment from templates |
| `TemplateScore` | `int` | The score a template sets outright. -1 when none does |
| `MaxModifier` | `int` | The most the modifier can be, such as from armor. Unlimited by default |
| `Bonuses` | `List<Bonus>` | Bonuses and penalties to the score |
| `Bonus` | `int` | The sum of the bonuses that have no condition (read only) |
| `HasTemplateScore` | `bool` | Whether a template sets the score (read only) |
| `HasScore` | `bool` | Whether the creature has this ability at all (read only) |
| `FullScore` | `int` | The final score; 0 without the ability, and at least 1 with it (read only) |
| `RawScore` | `int` | Every adjustment and bonus added up, with no minimum (read only) |
| `Modifier` | `int` | The modifier of the full score (read only) |
| `Summary` | `string` | The name, score, and modifier (read only) |

#### ArmorClass

| Constructor |
| --- |
| `ArmorClass()` |

| Method | Returns |
| --- | --- |
| `AddBonus(string source, int value, string condition = "")` | Nothing; adds a bonus of the source's kind, from `ArmorClassConstants` |
| `RemoveAllBonuses(string source)` | Nothing; removes every bonus of the source's kind |

| Property | Type | Description |
| --- | --- | --- |
| `BaseArmorClass` | `int` | 10 |
| `Dexterity` | `Ability` | The ability that adds to armor class |
| `SizeModifier` | `int` | The modifier from its size |
| `MaxDexterityBonus` | `int` | The most Dexterity can add, such as from armor. Unlimited by default |
| `DexterityBonus` | `int` | The Dexterity modifier, up to the maximum (read only) |
| `ArmorBonuses` | `List<Bonus>` | Bonuses from armor |
| `ShieldBonuses` | `List<Bonus>` | Bonuses from shields |
| `DeflectionBonuses` | `List<Bonus>` | Deflection bonuses |
| `NaturalArmorBonuses` | `List<Bonus>` | Natural armor bonuses |
| `DodgeBonuses` | `List<Bonus>` | Dodge bonuses |
| `ArmorBonus` | `int` | The best of the armor bonuses with no condition (read only) |
| `ShieldBonus` | `int` | The best of the shield bonuses with no condition (read only) |
| `DeflectionBonus` | `int` | The best of the deflection bonuses with no condition (read only) |
| `NaturalArmorBonus` | `int` | The best of the natural armor bonuses with no condition (read only) |
| `DodgeBonus` | `int` | The sum of the dodge bonuses with no condition (read only) |
| `Bonuses` | `IEnumerable<Bonus>` | Every bonus of every kind (read only) |
| `IsConditional` | `bool` | Whether any bonus has a condition (read only) |
| `TotalBonus` | `int` | The armor class, at least 1 (read only) |
| `FlatFootedBonus` | `int` | The armor class without dodge and Dexterity (read only) |
| `TouchBonus` | `int` | The armor class without armor, shield, and natural armor (read only) |

#### HitPoints

| Constructor |
| --- |
| `HitPoints()` |

| Method | Returns |
| --- | --- |
| `RollTotal(Dice dice)` | Nothing; rolls each hit die and sets `Total` |
| `RollDefaultTotal(Dice dice)` | Nothing; takes the average of each hit die and sets `DefaultTotal` |

| Property | Type | Description |
| --- | --- | --- |
| `HitDice` | `List<HitDice>` | The dice that make up the hit points |
| `Constitution` | `Ability` | The ability whose modifier is added per die |
| `Total` | `int` | The rolled hit points |
| `DefaultTotal` | `int` | The average hit points |
| `Bonus` | `int` | A flat bonus, such as from a template |
| `HitDiceQuantity` | `double` | How many hit dice, with fractions (read only) |
| `RoundedHitDiceQuantity` | `int` | How many hit dice, rounded down, and at least 1 for a fraction (read only) |
| `DefaultRoll` | `string` | The roll for the hit points, such as 4d8+8 (read only) |
| `ConditionalBonuses` | `IEnumerable<(string, int)>` | The condition and hit point bonus of each conditional Constitution bonus (read only) |
| `Summary` | `string` | The default roll and the default total (read only) |

#### HitDice

| Constructor |
| --- |
| `HitDice()` |

| Method | Returns |
| --- | --- |
| `GetRoundedQuantity(double input)` | The quantity rounded down, and 1 for a fraction |

| Property | Type | Description |
| --- | --- | --- |
| `HitDie` | `int` | The sides of the die |
| `Quantity` | `double` | How many dice. A fraction such as 0.5 is a half die |
| `RoundedQuantity` | `int` | The quantity rounded (read only) |
| `DefaultRoll` | `string` | The roll for these dice, such as 2d8 or 1d8/2 (read only) |
| `Divisor` | `int` | What a fractional die's result is divided by (read only) |

#### Save

| Constructor |
| --- |
| `Save()` |

| Method | Returns |
| --- | --- |
| `AddBonus(int value, string condition = "")` | Nothing; adds a bonus |

| Property | Type | Description |
| --- | --- | --- |
| `BaseAbility` | `Ability` | The ability whose modifier is added |
| `BaseValue` | `int` | The save from hit dice |
| `Bonuses` | `List<Bonus>` | Bonuses and penalties |
| `IsConditional` | `bool` | Whether any bonus has a condition (read only) |
| `HasSave` | `bool` | Whether the creature has the ability to make this save (read only) |
| `Bonus` | `int` | The sum of the bonuses that have no condition (read only) |
| `TotalBonus` | `int` | The base value, bonuses, and ability modifier (read only) |

### Attacks

#### Attack

| Constructor |
| --- |
| `Attack()` |

| Property | Type | Description |
| --- | --- | --- |
| `Name` | `string` | What the attack is called |
| `BaseAttackBonus` | `int` | The creature's base attack bonus |
| `SizeModifier` | `int` | The modifier from its size |
| `BaseAbility` | `Ability` | The ability whose modifier is added to the attack |
| `IsMelee` | `bool` | Melee or ranged |
| `Damages` | `List<Damage>` | The damage dealt, from TreasureGen |
| `DamageEffect` | `string` | Any effect beyond damage |
| `DamageBonus` | `int` | The bonus added to the first damage |
| `IsPrimary` | `bool` | Whether it is a primary attack |
| `IsSpecial` | `bool` | Whether it is a special attack |
| `IsNatural` | `bool` | Whether it is a natural weapon |
| `Frequency` | `Frequency` | How often it can be used |
| `Save` | `SaveDieCheck` | The save it allows, if any |
| `AttackType` | `string` | The kind of attack, such as bite or claw |
| `AttackBonuses` | `List<int>` | Bonuses to the attack |
| `MaxNumberOfAttacks` | `int` | The most times it can attack in a full attack. 1 by default |
| `TotalAttackBonus` | `int` | The base attack bonus, ability modifier, size modifier, and bonuses (read only) |
| `FullAttackBonuses` | `int[]` | The bonus of each attack in a full attack, 5 lower each time (read only) |
| `DamageSummary` | `string` | The damage as text, such as 1d6+2 plus poison (read only) |

#### SaveDieCheck

The save a victim makes against an attack or a feat.

| Constructor |
| --- |
| `SaveDieCheck()` |

| Property | Type | Description |
| --- | --- | --- |
| `BaseAbility` | `Ability` | The ability whose modifier is added to the DC |
| `BaseValue` | `int` | The DC before the ability modifier |
| `Save` | `string` | The save made, from `SaveConstants` |
| `DC` | `int` | The difficulty class (read only) |

### Feats and skills

#### Feat

Feats and special qualities.

| Constructor |
| --- |
| `Feat()` |

| Method | Returns |
| --- | --- |
| `GetSummary()` | The feat's name and foci, as text |

| Property | Type | Description |
| --- | --- | --- |
| `Name` | `string` | From `FeatConstants` |
| `Foci` | `IEnumerable<string>` | What the feat applies to |
| `Power` | `int` | The strength of the feat, such as a number of damage dice |
| `Frequency` | `Frequency` | How often it can be used |
| `CanBeTakenMultipleTimes` | `bool` | Whether the feat can be taken more than once |
| `Save` | `SaveDieCheck` | The save it allows, if any |

#### Frequency

| Constructor |
| --- |
| `Frequency()` |

| Property | Type | Description |
| --- | --- | --- |
| `Quantity` | `int` | How many times |
| `TimePeriod` | `string` | Per what period, from `FeatConstants.Frequencies` |

#### Skill

| Constructor |
| --- |
| `Skill(string name, Ability baseStat, int rankCap, string focus = "")` |

| Method | Returns |
| --- | --- |
| `IsEqualTo(Skill skill)` | Whether it is the same skill; a skill with no focus matches any focus |
| `IsEqualTo(string skill)` | The same, for a skill written by `SkillConstants.Build` |
| `AddBonus(int value, string condition = "")` | Nothing; adds a bonus |

| Property | Type | Description |
| --- | --- | --- |
| `Name` | `string` | From `SkillConstants` (set by the constructor) |
| `BaseAbility` | `Ability` | The ability whose modifier is added |
| `Focus` | `string` | What the skill applies to, such as a craft (set by the constructor) |
| `ClassSkill` | `bool` | Whether ranks count in full, instead of half |
| `ArmorCheckPenalty` | `int` | The penalty from armor |
| `RankCap` | `int` | The most ranks it can have |
| `HasArmorCheckPenalty` | `bool` | Whether armor penalizes the skill |
| `Bonuses` | `List<Bonus>` | Bonuses and penalties |
| `CircumstantialBonus` | `bool` | Whether any bonus has a condition (read only) |
| `Key` | `string` | The name and focus together (read only) |
| `Bonus` | `int` | The sum of the bonuses that have no condition (read only) |
| `EffectiveRanks` | `double` | The ranks, halved when not a class skill (read only) |
| `RanksMaxedOut` | `bool` | Whether the ranks are at the cap (read only) |
| `Ranks` | `int` | The ranks. Setting more than the cap throws an `InvalidOperationException` |
| `QualifiesForSkillSynergy` | `bool` | Whether it has 5 or more effective ranks (read only) |
| `TotalBonus` | `int` | The ranks, bonuses, ability modifier, and armor check penalty, rounded down (read only) |

### Magic

#### Magic

| Constructor |
| --- |
| `Magic()` |

| Property | Type | Description |
| --- | --- | --- |
| `SpellsPerDay` | `IEnumerable<SpellQuantity>` | How many spells of each level it can cast |
| `KnownSpells` | `IEnumerable<Spell>` | The spells it knows |
| `PreparedSpells` | `IEnumerable<Spell>` | The spells it has prepared |
| `ArcaneSpellFailure` | `int` | The chance, as a percentage, that arcane spells fail |
| `Caster` | `string` | The kind of caster, from `SpellConstants.Casters` |
| `CasterLevel` | `int` | The level at which it casts |
| `Domains` | `IEnumerable<string>` | Its domains, from `SpellConstants.Domains` |
| `CastingAbility` | `Ability` | The ability that powers its spells |

#### Spell

| Constructor |
| --- |
| `Spell()` |

| Property | Type | Description |
| --- | --- | --- |
| `Source` | `string` | The class or domain it comes from |
| `Level` | `int` | The spell level |
| `Name` | `string` | The spell |
| `Metamagic` | `IEnumerable<string>` | The metamagic feats applied, from `FeatConstants.Metamagic` |

#### SpellQuantity

| Constructor |
| --- |
| `SpellQuantity()` |

| Property | Type | Description |
| --- | --- | --- |
| `Source` | `string` | The class or domain the spells come from |
| `Level` | `int` | The spell level |
| `Quantity` | `int` | The spells per day from the level |
| `BonusSpells` | `int` | The extra spells from a high ability score |
| `HasDomainSpell` | `bool` | Whether there is an extra spell from a domain |
| `TotalQuantity` | `int` | Every spell of the level per day (read only) |

### Constants

Each constants class names the values for one kind of thing. The values are the strings the rest of CreatureGen uses, so always pass the constant rather than writing out its text. A constant is named for its value, with an underscore before a qualifier: `CreatureConstants.Elf_High` is "High Elf".

| Constants | What it names |
| --- | --- |
| `AbilityConstants` | The six abilities |
| `AbilityConstants.RandomizerRolls` | The rolls for an `AbilityRandomizer`, in the table above |
| `AlignmentConstants` | The lawfulness and goodness that make up an alignment, and the full alignments |
| `AlignmentConstants.Modifiers` | How often creatures of a kind have an alignment: Always, Usually, Often, or Any |
| `AgeConstants` | `Ageless`, the age of a creature that never ages |
| `AgeConstants.Categories` | The age categories of most creatures, such as Adulthood and Middle Age, and the special cases of swarms, undead, and constructs |
| `AgeConstants.Categories.Arrowhawk` | The age categories of an arrowhawk |
| `AgeConstants.Categories.BlackPudding` | The age categories of a black pudding |
| `AgeConstants.Categories.Dragon` | The age categories of a dragon |
| `AgeConstants.Categories.Salamander` | The age categories of a salamander |
| `AgeConstants.Categories.Tojanida` | The age categories of a tojanida |
| `AgeConstants.Categories.Xorn` | The age categories of a xorn |
| `ChallengeRatingConstants` | Challenge ratings, from `CR0` through `CR30`, with fractions written `CR1_2nd` and the like |
| `CreatureConstants` | Every creature, with a qualifier after an underscore, such as `Dwarf_Hill` |
| `CreatureConstants.Templates` | Every template, such as `Ghost` and `HalfDragon_Black`, and `None` |
| `CreatureConstants.Types` | The creature types, such as `Humanoid` |
| `CreatureConstants.Types.Subtypes` | The creature subtypes, such as `Reptilian` |
| `GenderConstants` | The genders |
| `ArmorClassConstants` | The kinds of armor class bonus |
| `SaveConstants` | The three saves: Fortitude, Reflex, and Will |
| `FeatConstants` | The feats |
| `FeatConstants.Foci` | The kinds of thing a feat can apply to, such as Weapon, Element, and School |
| `FeatConstants.Foci.Elements` | The elements that a feat can apply to |
| `FeatConstants.Foci.Schools` | The schools of magic that a feat can apply to |
| `FeatConstants.Foci.Weapons` | The groups of weapons that a feat can apply to |
| `FeatConstants.Frequencies` | The time periods for how often a feat or attack can be used |
| `FeatConstants.MagicItemCreation` | The item creation feats |
| `FeatConstants.Metamagic` | The metamagic feats |
| `FeatConstants.Monster` | The monster feats |
| `FeatConstants.SpecialQualities` | The special qualities of creatures |
| `LanguageConstants` | The languages |
| `LanguageConstants.Groups` | Whether a language is automatic or a bonus language |
| `LanguageConstants.Special` | The languages of particular creatures, such as Aboleth and Druidic |
| `SpellConstants` | The spells |
| `SpellConstants.Casters` | The classes that cast spells, such as Bard and Cleric |
| `SpellConstants.Domains` | The cleric domains |
| `SpellConstants.Sources` | Whether spells are Arcane or Divine |
| `SkillConstants` | The skills |
| `SkillConstants.Foci` | What a skill can apply to, in the groups below |
| `SkillConstants.Foci.Craft` | The crafts |
| `SkillConstants.Foci.Knowledge` | The areas of knowledge |
| `SkillConstants.Foci.Perform` | The kinds of performance |
| `SkillConstants.Foci.Profession` | The professions |
| `SkillConstants.Special` | Skills that creatures have only in special cases, such as Control Shape |

Some constants classes also have methods and properties of their own, which are documented here.

#### ChallengeRatingConstants

| Method | Returns |
| --- | --- |
| `GetOrdered()` | The challenge ratings, from lowest to highest |
| `IncreaseChallengeRating(string challengeRating, int increaseAmount)` | The challenge rating that many steps higher |
| `IsGreaterThan(string source, string target)` | Whether the source challenge rating is higher than the target |

| Property | Type | Description |
| --- | --- | --- |
| `Fractional` | `IEnumerable<string>` | The challenge ratings below 1 (read only) |

#### CreatureConstants

| Method | Returns |
| --- | --- |
| `GetAll()` | Every creature |
| `GetAllNonCharacters()` | Every creature that can only be a monster |
| `GetAllCharacters()` | Every creature that can be a character |

#### CreatureConstants.Templates

| Method | Returns |
| --- | --- |
| `GetAll()` | Every template |

#### CreatureConstants.Types

| Method | Returns |
| --- | --- |
| `GetAll()` | Every creature type |

#### CreatureConstants.Types.Subtypes

| Method | Returns |
| --- | --- |
| `GetAll()` | Every creature subtype |

#### FeatConstants

| Method | Returns |
| --- | --- |
| `All()` | Every feat |

#### FeatConstants.MagicItemCreation

| Method | Returns |
| --- | --- |
| `All()` | Every item creation feat |

#### FeatConstants.Metamagic

| Method | Returns |
| --- | --- |
| `All()` | Every metamagic feat |

#### FeatConstants.Monster

| Method | Returns |
| --- | --- |
| `All()` | Every monster feat |

#### SkillConstants

| Method | Returns |
| --- | --- |
| `Build(string skill, string focus)` | The skill and focus written as one string |
| `Parse(string skillString)` | The skill, and its focus if it has one, from a string written by `Build` |

## Upgrading from 1.x

Version 2.0.0 has breaking changes to the public API:

- **Targets .NET 10.** Version 1.x targeted .NET 8, which reaches end of support on November 10, 2026.
- **Templates moved out of `Filters`.** Pass templates as the trailing `params string[] templates` argument of `GenerateRandomName`, `GenerateRandom`, and `GenerateRandomAsync` instead of `Filters.Templates`.
- **Filters are now lists.** `Filters.Type`, `Filters.ChallengeRating`, and `Filters.Alignment` are replaced by `Filters.Types`, `Filters.ChallengeRatings`, and `Filters.Alignments`. A creature matches if it matches any value in a list.
- **`Filters.GetDescription(bool asCharacter)`** is now `Filters.GetDescription()`.
- **`AbilityRandomizer.Roll` is set through the constructor**, for example `new AbilityRandomizer(AbilityConstants.RandomizerRolls.Good)`. The parameterless constructor still uses the default roll.
- **`ICreatureVerifier.VerifyCompatibility`** now takes the ability randomizer and the templates: `VerifyCompatibility(asCharacter, creature, abilityRandomizer, filters, templates)`.
- **`InvalidCreatureException`** constructors now take `Filters`, an `AbilityRandomizer`, and templates, and the message includes all of them.
- **`CreaturePrototype` is now internal.**
- **New challenge ratings** `ChallengeRatingConstants.CR28`, `CR29`, and `CR30`.

## Theme Song

DnDGen.CreatureGen has its own theme song: [Wake the Bestiary -DnDGen.CreatureGen Theme-](https://suno.com/s/idhKDWtBG8g26Ar3), dark Nordic folk for the bestiary.
It follows this README, so a change here may need the song updated too.
See the song's [lyrics, notes, and update procedure](https://github.com/DnDGen/DnDGen.CreatureGen/blob/main/docs/theme-song/README.md).
