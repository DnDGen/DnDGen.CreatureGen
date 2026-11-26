---

description: "Task list for the GitHub Actions and icon move"
---

# Tasks: Build and Release on GitHub Actions, with the DnDGen Icon

**Input**: [plan.md](plan.md), [spec.md](spec.md)

- **(maintainer)**: Only the maintainer can do this task: it is a repository or nuget.org setting.

## Phase 1: The change

- [X] T001 Add `.github/workflows/build.yml` and `release.yml` from DnDGen/.github's templates, with
  `DnDGen.CreatureGen`
- [X] T002 Add `CodeCoverage.runsettings` from the template, with `DnDGen\.CreatureGen\.dll`
- [X] T003 Remove `cicd`
- [X] T004 Replace the package icon with `avatar-128.png`; version 2.0.3 and its release notes
- [X] T005 Build the package and confirm the icon is in it; run a unit test with the coverage
  settings and confirm it reports `DnDGen.CreatureGen.dll`. (The first run reported none, because
  the project name in the runsettings hadn't been replaced; fixed.)
- [X] T006a Point the README's build badge at `release.yml`; update the constitution to 1.0.1

## Phase 2: The maintainer's settings

- [ ] T006 (maintainer) Create a `Prod` environment in the repository's settings
- [ ] T007 (maintainer) Require the `build / check` status check on `main`
- [ ] T008 (maintainer) On nuget.org, add a Trusted Publishing policy for this repository, with
  `release.yml` as the workflow file and `Prod` as the environment
- [ ] T009 (maintainer) Retire the Azure DevOps build and release pipelines, so they don't run
  beside the new ones

## Phase 3: Polish

- [ ] T010 Once the pull request's `build / check` has run, confirm the stress tests' time on a
  GitHub runner is acceptable, and that the first release attaches `coverage.cobertura.xml`
