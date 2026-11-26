# Implementation Plan: DnDGen.CreatureGen Theme Song

**Branch**: `speckit-readme-theme-song` | **Date**: 2026-11-23 | **Spec**: [spec.md](spec.md)

## Summary

Record a theme song in `docs/theme-song`: dark Nordic folk, drawn from the README as it is after
spec 001, with Sources that quote it, album art recoloured from the main theme's cover, and tests
that keep the record and the README in step. Add the Theme Song badge and section to the README
once the song has a Suno link, and a patch for the DnDGen/.github pull request.

## Technical Context

**Language/Version**: C# on `net10.0`. **Dependencies**: NUnit 5 (tests only).
**Testing**: `DnDGen.CreatureGen.Tests.Integration`, which already copies `README.md`, and now
also `docs/**/*.md` and `docs/**/*.png`, to its output.
**Constraints**: Title, Style, and Lyrics within Suno's 80, 1,000, and 5,000 characters.

## Constitution Check

| Label | Check | Result |
|---|---|---|
| I.1 | The record's tests and README tests are TreasureGen's, with CreatureGen's names. | Pass |
| I.2 | No new pattern. | Pass |
| I.3 | No comments added. | Pass |
| II | The record and README tests exist before the song's link does (T003 to T005). | Pass |
| II.1.c | Each rule of the record, and each README requirement, has its own case. | Pass |
| II.2.c (CreatureGen) | No package code changes. | Pass |
| II.3.a | Each test compares a whole list, so a failure shows every missing phrase or section. | Pass |
| II.3.c | The tests read only the build output. | Pass |
| III.1.c | The README gains a Theme Song section; documents derived from it say how they are kept in step (README as contract). | Pass |
| Versioning | PATCH: documentation and tests only. | Pass |

## Decisions

- **D1: Title.** *Wake the Bestiary*, the refrain: the skald wakes each creature, as the library
  generates one.
- **D2: Voices.** The Centaur King (the skald, in the first person) sings every English line, and the verses and bridge are his alone. A circle of low voices sings the leitmotif as the refrain that ends the intro, each chorus, and the outro, as an ordinary line with an em dash, never in parentheses (Suno dropped parenthesized answers in Infrastructure's drafts). This is the Nordic leader-and-refrain form; the echoes after every pair of lines came from other DnDGen songs and were dropped, because each song's structure stands on its own.
- **D6: Verse form.** Unrhymed alliterative verse, after the old Nordic poems: two or three stressed words in a line start alike, and the genre does not end-rhyme (the maintainer's decision, as with Infrastructure's chant). Choruses follow verses 1 to 4, and a final one closes the song.
- **D3: Sources.** Every sung line quotes the README's prose or table text, not its code samples,
  so lines are sung as words. The Centaur King's introduction and sign-off, and the refrain, are the lines not from the README.
- **D4: Suno link.** Until the maintainer has one, the record says `TODO_SUNO_LINK` and the README
  has no badge, so the link tests fail. The patch's `SUNO_LINK` is replaced at the same time.
- **D5: Album art.** A scene, like the other songs' covers: the large, centred d20 is the Centaur King's head and wears his crown level on its brow, in a night forest of tall dark trunks (two sharp near ones of different widths, thinner blurred ones behind) with pairs of glowing circles for eyes. Five earlier covers did not work (see the record's Album Art).

## Project Structure

```text
README.md                                                      # Theme Song badge and section
docs/theme-song/
├── README.md                                                  # the record
├── cover.html, cover.png                                      # album art
└── Cinzel.ttf, Cinzel-OFL.txt                                 # the title font
DnDGen.CreatureGen.Tests.Integration/
├── DnDGen.CreatureGen.Tests.Integration.csproj                # copies docs/** to the output
├── ReadmeTests.cs                                             # the README's theme song tests
└── Docs/ThemeSong/ReadmeTests.cs                              # the record's tests
specs/002-creaturegen-theme-song/dndgen-github.patch           # for DnDGen/.github
```
