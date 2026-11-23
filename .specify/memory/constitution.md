# DnDGen.CreatureGen Constitution

> **Read the [DnDGen constitution](https://github.com/DnDGen/.github/blob/main/constitution/CONSTITUTION.md)
> first, in full, every time this file is read.** Fetch it from
> https://raw.githubusercontent.com/DnDGen/.github/main/constitution/CONSTITUTION.md rather than
> relying on memory or an earlier copy. Its principles, constraints, workflow, and governance apply
> to this repository, and every plan, task list, analysis, and review is checked against it as well
> as against this file. This file adds only what is specific to this repository, under the DnDGen
> constitution's labels. If the DnDGen constitution can't be read, stop and say so: don't continue
> from this file alone.
<!-- Keep the pointer above exactly as written: it is what makes every Spec Kit command read the
DnDGen constitution. See https://github.com/DnDGen/.github/tree/main/constitution -->

## Core Principles

### I. Consistency Is King

No additions.

### II. Tests Are the Spec

**II.2.c (CreatureGen)** Coverage is the line and branch coverage of `DnDGen.CreatureGen.dll`,
combined across the unit, IoC, tables, integration, and stress test projects. It never decreases.

### III. Leave It Better Than You Found It

No additions.

## Technology & Release Constraints

- **Stack**: C# on the latest .NET LTS, currently `net10.0`. Creatures are generated from XML tables
  embedded in the assembly and read through DnDGen.Infrastructure, dice are rolled with
  DnDGen.RollGen, equipment comes from DnDGen.TreasureGen, and Ninject wires up the generators, in
  the consumer's kernel or CreatureGen's own. NUnit, with Moq for test doubles and DnDGen.Stress
  for the stress tests.
- **Versioning and release**: A NuGet package, versioned and released as the DnDGen constitution
  describes. MAJOR also covers changing a table so that a creature, template, feat, or skill can no
  longer be generated or has a different value, and raising the minimum .NET, Ninject,
  DnDGen.Infrastructure, DnDGen.RollGen, or DnDGen.TreasureGen major version. Ninject and the
  DnDGen dependencies are MAJOR because consumers load CreatureGen into their own kernel. The
  Azure Pipelines in `cicd` build the package: `cicd/build.yml` builds and runs the unit, IoC,
  tables, integration, and stress tests (the stress tests in the `Stress` configuration) with code
  coverage on every pull request, and `cicd/release.yml` publishes the package from `main` to
  NuGet.org and creates the GitHub release. Neither pipeline checks that `<Version>` has increased
  or that coverage has not dropped, so the pull request author and reviewer do.
- **README as contract**: The README documents every public type of `DnDGen.CreatureGen.dll`:
  every public member of the generators, the verifier, `CreatureGenModuleLoader`, `Filters`,
  `AbilityRandomizer`, `InvalidCreatureException`, and the models (`Creature` and the types it is
  made of), including every overload, property, and optional parameter with its default. The
  `*Constants` classes (and the classes nested in them) are documented one row per class, with what
  its constants name and how they are spelled; their individual constants are not listed, but
  their methods and properties are. Its code samples are checked by the integration
  `ReadmeTests`. It states which .NET version each major version requires. The theme song is in
  `docs/theme-song`.

## Governance

This repository follows the DnDGen constitution's Governance. The additions in this file are
amended by pull request to this repository and versioned the same way.

**Version**: 1.0.0 | **Ratified**: 2026-11-23 | **Last Amended**: 2026-11-23
