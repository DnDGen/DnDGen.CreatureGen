# Wake the Bestiary -DnDGen.CreatureGen Theme-

[![Wake the Bestiary album art](cover.png)](TODO_SUNO_LINK)

The DnDGen.CreatureGen theme song: dark Nordic folk for the bestiary, where a skald tells how every creature is woken and a circle of low voices answers with its name.
Its lyrics come from the [DnDGen.CreatureGen README](https://github.com/DnDGen/DnDGen.CreatureGen/blob/main/README.md), sung in plain English,
and the circle's answer is *Centaur Rege*, CreatureGen's leitmotif from the [DnDGen main theme](https://github.com/DnDGen/.github/blob/main/theme-song/README.md).

**Listen on [Suno](TODO_SUNO_LINK).**

## Updating

This song follows the [DnDGen.CreatureGen README](https://github.com/DnDGen/DnDGen.CreatureGen/blob/main/README.md).
Every pull request that changes the README gets a song check:

1. If the change rewords or removes a phrase in the Sources table below, the song needs updating.
   The tests catch this one: they fail and list every quoted phrase that is no longer in the README.
2. Otherwise, if the change alters the README's high-level ideas or vibe, the song needs updating.
3. Otherwise, it doesn't. Changes to the README's Theme Song badge or section never need a song update.

When the song needs updating, it happens in the same pull request:

1. The contributor updates the Lyrics and Sources here to match the new README.
   Names are sung as words, never as code.
2. The maintainer makes the new song in Suno:
   - **Cover the current song**, so the theme stays recognizably the same song as it evolves.
   - Use the Title, Style, and Lyrics from this page. **Leave the Style as it is**: only the lyrics change.
   - **Turn Variety all the way down**, so Suno keeps to the Style instead of straying from it.
   - Pick the take. The new take replaces the old one.
   - **Publish it**, so anyone with the link can find and hear it.
3. The maintainer updates the Suno link here and in the README, and adds anything learned to the Suno Notes.

## Title

```
Wake the Bestiary -DnDGen.CreatureGen Theme-
```

## Style

```
Dark Nordic folk, a slow, hypnotic, ritual pulse in a minor mode, Frame drum and deep war drums, a bowed tagelharpa drone, nyckelharpa, kantele, bone flute and a low jaw harp, with wind and a crackling fire beneath, A calm, deep, unhurried male skald telling the saga in a steady, clear voice, every word plain, shaped like a story told by the fire, The verses are the skald alone, spare and storytelling, The choruses are call and response: the skald sings the English line, and a small circle of low male and female voices answers with the Latin name, A hushed bridge, whispered and conspiratorial, with the circle echoing under his breath, Slow, steady tempo throughout, Cold, wide, earthy, live-in-the-forest production, Ancient, patient and ominous
```

## Lyrics

```
[Intro - a low drone and a slow frame drum, wind in the pines, then the skald begins and the circle whispers back]
Centaur Rege
(Centaur Rege)

[Verse 1 - the skald alone, slow and spare, telling the tale]
Ask for an ogre and an ogre comes
With its hit dice and its hit points
Its skills and its feats
Its attacks, its spells, and all that it carries
Its age, its gender, and how far it reaches
What it lacks is empty, never null

[Chorus - call and response: the skald sings the English line, the circle of low voices answers with the Latin name]
Wake the bestiary
Centaur Rege
Every creature, fleshed out whole
Centaur Rege
Monster or hero, as you say
Centaur Rege

[Verse 2 - the skald alone, slow and spare]
A hero of a playable race
Takes levels in a class
Chain the templates, one by one
Applied in the order given
Each must suit the creature
As it is after the ones before

[Chorus - call and response: the skald sings the English line, the circle of low voices answers with the Latin name]
Wake the bestiary
Centaur Rege
Every creature, fleshed out whole
Centaur Rege
Monster or hero, as you say
Centaur Rege

[Verse 3 - the skald alone, slow and spare]
Roll the scores the default way
Or roll four dice and keep the best three
Some templates need a minimum score
A ghost needs a Charisma of six
A score that falls short is raised to meet it
If it can never reach the minimum
The creature is not compatible

[Bridge - hushed, whispered and conspiratorial, the skald almost speaking, the circle echoing under his breath]
Ask before you wake it
Is there a creature that can be made?
(Can it be made?)
If it cannot, the generator throws
(It throws)

[Verse 4 - the skald alone, slow and spare]
Or let the dice choose, and they will
Weighted heavily toward no template
Filters narrow the creatures down
A creature that matches any value in a list
And must satisfy every filter that has values
Ask for a challenge rating
And it will never be advanced

[Verse 5 - the skald alone, slow and spare]
Version two has breaking changes
It needs dot net ten
Load it with the module loader
Dice from RollGen, tables from Infrastructure, treasure from TreasureGen

[Final Chorus - call and response: the skald sings the English line, the circle of low voices answers with the Latin name]
Wake the bestiary
Centaur Rege
Every creature, fleshed out whole
Centaur Rege
Monster or hero, as you say
Centaur Rege

[Outro - the drums fade, the drone remains, the circle whispers back]
Centaur Rege
(Centaur Rege)
```

## Sources

The leitmotif, *Centaur Rege*, is Latin for a centaur, by the king. It is CreatureGen's anagram from the [main theme](https://github.com/DnDGen/.github/blob/main/theme-song/README.md#leitmotifs), and the circle answers every English line of the chorus with it.

| Lyric | From the README |
| --- | --- |
| Centaur Rege | Not from the README: CreatureGen's leitmotif from the main theme, sung by the circle |
| Ask for an ogre and an ogre comes | Use: "Generating a specific creature" |
| With its hit dice and its hit points | Reference, Creature: "Its hit dice and hit points" |
| Its skills and its feats | Reference, Creature: "Its skills" and "Its feats" |
| Its attacks, its spells, and all that it carries | Reference, Creature: "All of its attacks", "Its spells and casting", and "What it carries" |
| Its age, its gender, and how far it reaches | Reference, Creature: "Its gender, age, and measurements" and "How far it reaches" |
| What it lacks is empty, never null | Reference, Creature: "Collections are empty, not null, when the creature has none" |
| Wake the bestiary | Not from the README: the refrain and the song's title |
| Every creature, fleshed out whole | Introduction: "fleshed-out creature" |
| Monster or hero, as you say | Use: "generated as a character" and "or as a monster" |
| A hero of a playable race | Use: "a playable race that will take class levels" |
| Takes levels in a class | Use: "a playable race that will take class levels" |
| Chain the templates, one by one | Use: "Chaining templates" |
| Applied in the order given | Use: "applied in the order given" |
| Each must suit the creature | Use: "Each template must be compatible with the creature" |
| As it is after the ones before | Use: "as it is after the templates before it have been applied" |
| Roll the scores the default way | Use, Ability scores: "abilities are rolled with the default roll" |
| Or roll four dice and keep the best three | Use, Ability scores: "keep the highest 3" |
| Some templates need a minimum score | Use, Ability scores: "Some templates require a minimum ability score" |
| A ghost needs a Charisma of six | Use, Ability scores: "a Ghost needs Charisma 6" |
| A score that falls short is raised to meet it | Use, Ability scores: "any rolled score that would fall short is raised to meet the minimum" |
| If it can never reach the minimum | Use, Ability scores: "If the randomizer can never reach the minimum" |
| The creature is not compatible | Use, Ability scores: "the creature is not compatible" |
| Ask before you wake it | Use, Checking compatibility: "To check ahead of time" |
| Is there a creature that can be made? | Use, Checking compatibility: "gives a valid creature" |
| If it cannot, the generator throws | Use, Checking compatibility: "the generator throws" |
| Or let the dice choose, and they will | Use: "Random creatures" |
| Weighted heavily toward no template | Use, Random creatures: "weighted heavily in favor of creatures without a template" |
| Filters narrow the creatures down | Use, Filters: "Filters narrow down which random creatures are valid" |
| A creature that matches any value in a list | Use, Filters: "A creature satisfies a filter if it matches" |
| And must satisfy every filter that has values | Use, Filters: "A creature must satisfy" |
| Ask for a challenge rating | Use, Filters: "challenge rating filter" |
| And it will never be advanced | Use, Filters: "are never advanced" |
| Version two has breaking changes | Upgrading from 1.x: "Version 2.0.0 has breaking changes" |
| It needs dot net ten | Installing: "It targets .NET 10." |
| Load it with the module loader | Getting the Generators: "use the Module Loader" |
| Dice from RollGen, tables from Infrastructure, treasure from TreasureGen | Getting the Generators: "including RollGen, Infrastructure, and TreasureGen" |

## Suno Notes

Nothing has been generated yet, so everything here is a guess to test:

- **Drafts and the Sample.** Every draft before this song first merges is a fresh generation that
  Samples the main theme's "Rogante Marendor" snippet (3:00.0 to 3:17.1) with Variety all the way
  down, per the main theme's
  [New Theme Songs](https://github.com/DnDGen/.github/blob/main/theme-song/README.md#new-theme-songs).
  The take that merges is v1; every update from there Covers it.
- **The circle's answers are ordinary lines, not parentheses.** Infrastructure's drafts lost almost
  every parenthesized echo and got the second voice only once it was sung as an ordinary line, with
  the voices assigned in the section's tag and the Style, and no inline voice tags. The chorus does
  the same: the skald sings the English and the circle sings *Centaur Rege*. Only the intro, the
  bridge, and the outro use parentheses, for English echoes and the leitmotif's whisper.
- **The singer is a guess.** "A calm, deep, unhurried male skald" names the singer, as RollGen's
  lesson says, and avoids "raspy" and "gravelly", which came out growly there. A dark Nordic folk
  take may still pull toward a throat-sung or growled voice.
- **The Sample pulls toward the orchestra.** The Style names the Nordic instruments (tagelharpa,
  nyckelharpa, kantele, bone flute, jaw harp) to counter it, as Web's Kit Kat band and TreasureGen's
  squeezebox did. This song wants a band, so naming them is fine, unlike Infrastructure's a cappella
  chant.
- **Pace.** The Style says slow twice, and "Slow, steady tempo throughout" and the verse tags say it
  again, because the Sample's cinematic swell tends to build over a song.
- **Length.** About 2,500 characters of lyrics. Slow chant ran about 460 characters a minute and
  TreasureGen's shanty about 880, so this should land between four and five minutes. Record the real
  duration here after the first take.
- **Latin only for the leitmotif.** The song is in English, as the maintainer decides; the only
  other language is the two Latin words the circle answers with. Whether Suno sings *Centaur Rege*
  as Latin is untested.
- **"Dot net ten"** is spelled out so Suno doesn't read ".NET" as a symbol.

## Album Art

[cover.png](cover.png) is rendered from [cover.html](cover.html), which you can open in any browser.
To render it again at 1500 × 1500, from this folder:

```
msedge --headless --hide-scrollbars --allow-file-access-from-files --window-size=1500,1500 --screenshot=cover.png cover.html
```

If Edge reads `cover.html` as a web address, pass it as a full `file:///` URL instead.

- It is the main theme's [cover](https://github.com/DnDGen/.github/blob/main/theme-song/cover.html), recoloured for the bestiary.
  The d20 is bone-pale in the cold light of a northern forest, and the ghost copies behind it are the
  spruce-green shadows of every beast it has woken. The title is the song's, *Wake the Bestiary*.
- The d20 is the dndgen.com loading spinner, from [DnDGen.Web](https://github.com/DnDGen/DnDGen.Web/blob/main/dndgen-web/src/assets/loadingDice.svg), originally from [a gist by hperantunes](https://gist.github.com/hperantunes/f4c2fd407c2ea74c6c9d).
- The title is set in [Cinzel](https://github.com/NDISCOVER/Cinzel), included as [Cinzel.ttf](Cinzel.ttf) under the [SIL Open Font License](Cinzel-OFL.txt).
