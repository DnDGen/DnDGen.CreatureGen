# Feature Specification: README Documents the Public API

**Feature Branch**: `speckit-readme-theme-song`

**Created**: 2026-11-23

**Status**: Implemented

**Input**: User description: "Introduce Spec Kit to CreatureGen, flesh out the README, and test that
the README documents the public API, as DnDGen.TreasureGen's spec 001 does."

DnDGen.Stress, DnDGen.RollGen, and DnDGen.TreasureGen test that their READMEs document every public
member, because the README is packed into the NuGet package and is the only documentation a consumer
sees (DnDGen constitution, README as contract). CreatureGen's README explains how to generate
creatures, but documents none of the model a consumer gets back (`Creature`, `Ability`, `Skill`,
`Feat`, `Attack`, and the rest), and nothing stops it falling behind.

## Clarifications

### Session 2026-11-23

These follow DnDGen.TreasureGen's answers to the same questions (its spec 001).

- Q: CreatureGen has 44 `*Constants` classes and thousands of public constants (`SpellConstants`
  alone has over 600). Must the README list each one? → A: No. Each `*Constants` class, and each
  class nested in one, is documented as one row of a Constants table, with what it names. Its
  methods and properties are still documented individually. CreatureGen's constitution says so
  under "README as contract".
- Q: Which other public members must the README document? → A: Every constructor, method,
  property, and field of every other public type in `DnDGen.CreatureGen.dll`.
- Q: `SizeConstants` and `SpeedConstants` are instantiable classes, not static ones. How are they
  documented? → A: As ordinary types, because the tests define a constants class as a static class.
  Changing them to static classes would be a breaking change, so it is out of scope (III.3).
- Q: Should the tests pin the README's overall structure? → A: No, only what the contract needs.
- Q: Where do defaults live? → A: In optional parameters (`condition = ""`), so each method is
  documented by its full signature, defaults included.

## User Scenarios & Testing *(mandatory)*

Two people rely on the README: the **consumer**, who reads it to learn what CreatureGen does, and
the **maintainer**, who changes CreatureGen and needs to know when the README has fallen behind.

### User Story 1 - A new or changed member can't go undocumented (Priority: P1)

As the maintainer, if I add, remove, or re-shape a public member of CreatureGen (a constructor,
method, property, or field, including a parameter's type, name, or default) or add or remove a
`*Constants` class, and don't update the README, I expect the build to fail, and the failure to
name exactly which type and member the README is missing or has wrong.

**Independent Test**: Change `ICreatureVerifier.VerifyCompatibility`'s `creature` default to `""`
without touching the README: one test fails, listing the `= ""` signature as undocumented and the
`= null` one as documented but missing.

**Acceptance Scenarios**:

1. **Given** every public member is documented, **When** the tests run, **Then** they pass.
2. **Given** a public member or overload the README doesn't document, **When** the tests run,
   **Then** a test fails and lists every undocumented one, qualified by its type, methods and
   constructors by their full signature.
3. **Given** the README documents a member or type that no longer exists, **When** the tests run,
   **Then** a test fails and lists every one.
4. **Given** a public type is added, **When** the tests run, **Then** it is checked too, without
   anyone adding it to a list.

### User Story 2 - The README's code samples work (Priority: P2)

As a consumer, if I copy a sample from the README, I expect it to compile and do what the README
says, and as the maintainer, I expect the build to fail when one stops working.

**Acceptance Scenarios**:

1. **Given** each sample in the README, **When** the tests run, **Then** an integration test does
   the same thing and checks the result.

### Edge Cases

- **Same-named members across types** (`Name`, `Save`, `Bonuses`): documented per type, under that
  type's heading, so a member can't be satisfied by another type's row.
- **Grouped rows**: every code span in a row's first cell counts as documented.
- **Overloads**: `Alignment()` and `Alignment(string alignment)`, and the two
  `InvalidCreatureException` constructors, are told apart by full signature.
- **Tuples**: `GenerateRandomName` returns `(string, string[])`; return types aren't part of a
  signature, so only parameters are compared. `HitPoints.ConditionalBonuses` is a property typed
  `IEnumerable<(string, int)>` and is documented by name.
- **Nested constants classes** are written `Outer.Inner`, such as `CreatureConstants.Templates`.
- **Inherited members** (`ToString`, `Equals`, `GetHashCode`) and overrides of them aren't required.
- **Code samples**: a name that appears only inside a code sample isn't documented.

## Requirements *(mandatory)*

- **FR-001**: The tests MUST fail when a public constructor, method, property, or field of a
  public type that is not a constants class isn't documented in a README table outside a code
  sample under that type's heading, and MUST list every one that isn't.
- **FR-002**: The tests MUST fail when the README documents a member or constants class that
  doesn't exist, and MUST list every one.
- **FR-003**: Methods and constructors MUST be documented by full signature: generic type
  parameters, and each parameter's type, name, `params`, and default.
- **FR-004**: A constants class is a static class named `*Constants`, or nested in one. It is
  documented by name in a Constants table; its fields are not listed, and its methods and
  properties are, like any other type's.
- **FR-005**: The README MUST be filled out in the same change, so that every new test passes, and
  every sample MUST be one that a test runs.
- **FR-006**: The new tests MUST sit in the integration `ReadmeTests`, and MUST NOT pin the
  README's overall structure.

## Success Criteria *(mandatory)*

- **SC-001**: Every new test passes, and the README documents the whole public API as FR-001 to
  FR-004 define it.
- **SC-002**: Removing any single documented item makes exactly the test for that kind fail,
  naming the item and its type.
- **SC-003**: Line and branch coverage of `DnDGen.CreatureGen.dll` doesn't drop (II.2.c
  (CreatureGen)); no package code changes.
- **SC-004**: A maintainer reading a failure knows what to change in the README without opening
  the test code (II.3).

## Assumptions

- The README is the NuGet package's readme, so its links are absolute.
- Per-type headings are CreatureGen's own need, from same-named members; it is not a new DnDGen
  pattern (I.2).
