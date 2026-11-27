# Feature Specification: DnDGen.CreatureGen Theme Song

**Feature Branch**: `speckit-readme-theme-song`

**Created**: 2026-11-23

**Status**: Draft

**Input**: User description: "Create the theme song" for CreatureGen, following the rules in
DnDGen/.github, with DnDGen.TreasureGen's spec 002 as the example.

## Clarifications

### Session 2026-11-23

Answered from DnDGen/.github's theme-song README and DnDGen.TreasureGen's spec 002, which settled
the same questions. The maintainer hasn't reviewed them yet.

- Q: Which genre? → A: Dark Nordic folk, the genre and area ("the bestiary") the Soundtrack Plan
  proposes for CreatureGen. **The Plan calls it a proposal, so the maintainer confirms it.**
- Q: Which language? → A: English, plain words, like Stress, RollGen, TreasureGen, and Web, with
  the leitmotif the only Latin. **Also the maintainer's to confirm.** A Norse-flavoured alternative
  is Old Norse echoes in the chorus; it is not drafted.
- Q: Does it carry a leitmotif? → A: Yes, as the narrator. CreatureGen is a generator, so its song brings back its leitmotif from the main theme, *Centaur Rege*. A bare Latin answer meant nothing in the song, so the Centaur King is the skald, speaking in the first person, and the circle answers with his name.
  leitmotif from the main theme, *Centaur Rege*: Latin for a centaur, by the king. The circle of
  voices answers every English line of the chorus with it.
- Q: Where do the lyrics come from? → A: CreatureGen's README as it is after spec 001, sung as
  words, never as code. The leitmotif and the refrain (which is also the song's title) are the
  only lines not from the README.
- Q: Where does the song live? → A: `docs/theme-song/README.md`, with its album art, sources, and
  update procedure, following TreasureGen's record and tests.
- Q: How are the first drafts made? → A: Fresh generations that Sample the main theme's "Rogante
  Marendor" snippet, 3:00.0 to 3:17.1, with Variety all the way down. The take that merges is v1.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - A listener finds and hears the song (Priority: P1)

As someone reading CreatureGen's README, on GitHub or NuGet, I expect a Theme Song badge beside the
build badge and a Theme Song section at the end, both linking to the song on Suno, and the section
linking to the song's record.

### User Story 2 - The song stays in step with the README (Priority: P1)

As the maintainer, if a README change rewords or removes a phrase the song quotes, I expect the
build to fail and list each phrase, and the record to tell me how to update the song.

### User Story 3 - The record is complete and fits Suno (Priority: P2)

As the maintainer making the song in Suno, I expect the record's Title, Style, and Lyrics to each
be one code block within Suno's limits (80, 1,000, and 5,000 characters), every lyric line to have
a source, and the album art to exist and link to the same song.

### Edge Cases

- **Before the song is generated**: There is no Suno link yet, so the link tests fail until the
  maintainer generates the song and adds it. That is the signal that the feature isn't done.
- **Repeated lines and responses**: A line sung more than once needs one Sources row, where it is
  first sung. A line wholly in parentheses is an echo, and needs none.
- **Not from the README**: A Sources row starting "Not from the README:" quotes nothing.

## Requirements *(mandatory)*

- **FR-001**: The record MUST have, in order, the sections Updating, Title, Style, Lyrics, Sources,
  Suno Notes, and Album Art, none empty.
- **FR-002**: Title, Style, and Lyrics MUST each be one code block within Suno's limits.
- **FR-003**: The Sources table MUST have one row per distinct lyric line, in the order first
  sung, and every phrase it quotes MUST be in the README.
- **FR-004**: The record and the README MUST link to the same, single song on Suno, and the album
  art MUST exist and link to it.
- **FR-005**: The README MUST have a Theme Song badge beside the build badge and a Theme Song
  section at the end that links to the record; every README link MUST be absolute.
- **FR-006**: The organization's Soundtrack Plan and soundtrack table MUST link to the song, by
  the maintainer's own pull request to DnDGen/.github.

## Success Criteria *(mandatory)*

- **SC-001**: Every new test passes once the maintainer adds the Suno link.
- **SC-002**: Line and branch coverage of `DnDGen.CreatureGen.dll` doesn't drop (II.2.c
  (CreatureGen)).

## Assumptions

- The tests are TreasureGen's record and README tests, unchanged but for names (I.1).
- No package code changes: the README change ships in the package, so it is a PATCH.
