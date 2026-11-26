# Implementation Plan: Build and Release on GitHub Actions, with the DnDGen Icon

**Branch**: `github-actions-and-icon` | **Date**: 2026-11-27 | **Spec**: [spec.md](spec.md)

## Summary

Copy DnDGen/.github's `templates` into CreatureGen with its name, delete `cicd`, replace the
package icon with the DnDGen avatar, point the README's build badge at the new workflow, and bring
the constitution's Versioning line up to date. No package code changes.

## Technical Context

**Language/Version**: C# on `net10.0`. **CI**: GitHub Actions, calling DnDGen/.github `@v1`.
**Testing**: Verified here by building the package and running a unit test with the coverage
settings. The workflows themselves can only be verified by running on GitHub.

## Constitution Check

| Label | Check | Result |
|---|---|---|
| I.1 | Every file is the template's, as TreasureGen, Stress, RollGen, and Infrastructure copied it. | Pass |
| I.2 | No new pattern. | Pass |
| I.3 | No comments added. | Pass |
| II.2.c (CreatureGen) | No package code changes; coverage is now checked by the pipeline, against the latest release. | Pass |
| III.1.a | Moving to the shared pipeline and avatar is maintenance, in its own pull request (III.3). | Pass |
| Versioning | PATCH: pipeline, icon, and documentation, with no code change. Release notes say so. | Pass |

## Decisions

- **D1: Stacked on spec 001 and 002.** This branch starts from `speckit-readme-theme-song`, since
  the constitution it amends is there. It targets that branch until it merges, then `main`.
- **D2: Runsettings regex.** The template's `DnDGen\.YourGen` is replaced with
  `DnDGen\.CreatureGen`; a first `sed` left it unchanged and the local run reported no coverage,
  which is how it was caught (T005).
- **D3: Version 2.0.3.** Spec 001 and 002's pull request is 2.0.2; this follows it.

## Project Structure

```text
.github/workflows/build.yml, release.yml                       # new, from the template
CodeCoverage.runsettings                                       # new, from the template
cicd/                                                          # removed
DnDGen.CreatureGen/avatar-128.png                              # new package icon
DnDGen.CreatureGen/DnDGen.CreatureGen.csproj                   # icon, version 2.0.3, release notes
README.md                                                      # build badge
.specify/memory/constitution.md                                # Versioning, 1.0.1
```
