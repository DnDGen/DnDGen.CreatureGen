# Feature Specification: Build and Release on GitHub Actions, with the DnDGen Icon

**Feature Branch**: `github-actions-and-icon`

**Created**: 2026-11-27

**Status**: Draft

**Input**: User description: "Move CreatureGen to GitHub Actions, as Stress, RollGen, Infrastructure, and
TreasureGen were, and update the package icon, as in those repositories."

CreatureGen still builds and releases on Azure Pipelines (`cicd/build.yml`, `cicd/release.yml`),
which check neither that `<Version>` increased nor that coverage held, and its package icon is a
stock image. Every other DnDGen NuGet library calls the shared workflows in DnDGen/.github and uses
the DnDGen avatar (DnDGen constitution, Technology & Release Constraints).

## Clarifications

### Session 2026-11-27

- Q: Which workflows? → A: The ones in DnDGen/.github's `templates` (`build.yml`, `release.yml`),
  with `DnDGen.YourGen` replaced by `DnDGen.CreatureGen`, calling `nuget-build.yml`,
  `nuget-publish`, and `nuget-release.yml` at `@v1`, as TreasureGen does.
- Q: Which icon? → A: `avatar-128.png` from DnDGen/.github's `avatar` folder, packed as the package
  icon, as in the other libraries. The old `18531-twenty-sided-dice-vector-thumb.png` is removed.
- Q: What does coverage measure? → A: `CodeCoverage.runsettings`, from the template, includes only
  `DnDGen.CreatureGen.dll` and excludes `*.g.cs`, which matches CreatureGen's constitution.
- Q: Does it need a MAJOR? → A: No. The package's code and public API don't change: PATCH (2.0.3).
- Q: What can't this change do? → A: The repository and nuget.org settings (see tasks T006 to T009)
  are the maintainer's, so the first runs of the workflows aren't verified here.

## User Scenarios & Testing *(mandatory)*

### User Story 1 - A pull request is checked as the other libraries' are (Priority: P1)

As the maintainer, I expect every pull request to run the shared build check: the version must be
greater than the latest release's, the tests of every tier run, and coverage may not fall below the
latest release's.

**Acceptance Scenarios**:

1. **Given** a pull request, **When** it is opened, **Then** `build / check` runs the unit, IoC,
   tables, integration, and stress tests (stress in the `Stress` configuration) with coverage.
2. **Given** a `<Version>` not greater than the latest release's, **Then** the check fails.

### User Story 2 - A merge to `main` is a release (Priority: P1)

As the maintainer, I expect a merge to `main` to publish the package to NuGet.org with Trusted
Publishing, and to create a GitHub release tagged with `<Version>`, with the package and merged
coverage report attached.

### User Story 3 - The package and README show the DnDGen identity (Priority: P2)

As a consumer, I expect the package icon to be the DnDGen avatar, and the README's build badge to
report the GitHub Actions workflow.

### Edge Cases

- **Coverage baseline**: The latest release (2.0.1) has no `coverage.cobertura.xml`. The shared
  check then runs without a baseline and logs that it has none; the release this pull request
  makes attaches the first one.
- **Old pipelines**: The Azure build and release pipelines still exist on Azure DevOps until the
  maintainer retires them, so they could run beside the new ones.
- **Stress run time**: The stress tests are CreatureGen's longest tier, and now run on a GitHub
  runner. Their time on it is unmeasured.

## Requirements *(mandatory)*

- **FR-001**: `.github/workflows/build.yml` and `release.yml` MUST call the shared workflows with
  `project: DnDGen.CreatureGen`, as the template does.
- **FR-002**: `CodeCoverage.runsettings` MUST include only `DnDGen.CreatureGen.dll`, so that
  coverage measures the library.
- **FR-003**: `cicd` MUST be removed.
- **FR-004**: The package icon MUST be `avatar-128.png`, packed into the package.
- **FR-005**: The README's build badge MUST report the GitHub Actions `release.yml` workflow, and
  the constitution MUST describe the new pipeline.
- **FR-006**: `<Version>` MUST be 2.0.3 with release notes for this release only.

## Success Criteria *(mandatory)*

- **SC-001**: The package built from this branch contains `avatar-128.png` as its icon.
- **SC-002**: A unit test run with `--settings CodeCoverage.runsettings` reports coverage of
  `DnDGen.CreatureGen.dll`.
- **SC-003**: No package code changes, so the existing tests and coverage are unaffected (II.2.c
  (CreatureGen)).

## Assumptions

- The shared workflows' layout expectations hold for CreatureGen: `DnDGen.CreatureGen/DnDGen.CreatureGen.csproj`
  and the five test tiers, each in a folder of the same name. They do.
