# Implementation Plan: README Documents the Public API

**Branch**: `speckit-readme-theme-song` | **Date**: 2026-11-23 | **Spec**: [spec.md](spec.md)

## Summary

Add tests to a new integration `ReadmeTests` that compare, by reflection, every public member of
every public type in `DnDGen.CreatureGen.dll` with the members listed in the README's Constructor,
Method, and Property tables under that type's heading, and the `*Constants` classes with its
Constants table. Then add a Reference section to the README that makes them pass, and turn each of
its samples into a test. The helpers are DnDGen.TreasureGen's `ReadmeTests`, with tuple and
`long`/`void` support added for CreatureGen's signatures.

## Technical Context

**Language/Version**: C# on `net10.0`. **Dependencies**: NUnit 5 (tests only).
**Testing**: `DnDGen.CreatureGen.Tests.Integration`, which gains the README in its output.
**Constraints**: The README is packed into the package, so it reads well on NuGet.

## Constitution Check

| Label | Check | Result |
|---|---|---|
| I.1 | `GetLinesOutsideCodeBlocks`, `GetTableRows`, `WriteMember`, and the table kinds are TreasureGen's, which are RollGen's. | Pass |
| I.2 | Per-type headings and the Constants table answer CreatureGen's same-named members and thousands of constants; they don't replace the established pattern. | Pass |
| I.3 | No comments added. | Pass |
| II | Tests are written first and seen to fail against today's README (T004) before the README changes (T005). | Pass |
| II.1.c | Constructors, methods, properties, constants classes, and each sample have their own case. | Pass |
| II.2.a | The whole public API is checked, not a sample. | Pass |
| II.2.c (CreatureGen) | No package code changes. | Pass |
| II.3.a | Each test compares whole lists, so a failure shows every missing and extra item. | Pass |
| II.3.c | The tests read only the build output. | Pass |
| III.1.c | The README gains what it was missing. | Pass |
| README as contract | This feature is the contract's test. | Pass |
| Versioning | PATCH: README, tests, Spec Kit. | Pass |

## Decisions

- **D1: Signatures.** As TreasureGen's D1: `Name<T>(type name = default, ...)` with C# keywords.
- **D2: Found types.** `typeof(Creature).Assembly.GetExportedTypes()`, nested types included.
- **D3: Per-type headings.** Rows count only under the heading equal to the type's name
  (`Outer.Inner` when nested), written `Type.member` in failures.
- **D4: Constants classes** are listed in a Constants table, by name. Their methods and properties
  are in the type's own Method and Property tables.
- **D5: Overrides of `object`'s members** (`ToString`, `Equals`, `GetHashCode`) are excluded.
- **D6: Samples** that use random generation assert only what is deterministic: the creature,
  templates, and set ability, not the random values.

## Project Structure

```text
README.md                                                      # Reference added
DnDGen.CreatureGen.Tests.Integration/
├── DnDGen.CreatureGen.Tests.Integration.csproj                # copies README.md to the output
└── ReadmeTests.cs                                             # document and sample tests
```
