# Docs — {CC}.{RegulationName} {YYYY}

Year-specific regulation documents for `{YYYY}`.

---

## Standard Documents (8)

| File | Content | Required |
|:---|:---|:---:|
| `{CC}.{RegulationName}-Model.md` | Collectors, WageType matrix, cases, lookups, reports | ✓ |
| `{CC}.{RegulationName}-Design.md` | Architecture, regulation split, design decisions, scope | ✓ |
| `{CC}.{RegulationName}-Actions.md` | No-Code / Low-Code action specification with formulas | ✓ |
| `{CC}.{RegulationName}-UseCases.md` | Provider use cases: setup, payrun, case availability, special scenarios | ✓ |
| `{CC}.{RegulationName}-UncoveredCases.md` | Uncovered statutory cases: Tier A/B/C classification | ✓ |
| `{CC}.{RegulationName}-Maintenance.md` | Annual update workflow and release checklist | ✓ |
| `{CC}.{RegulationName}-ProviderStubs.md` | Stub WageTypes, override instructions, approximations | ✓ |
| `{CC}.{RegulationName}-Compliance.md` | Certification status, filing obligations, statutory scope | ✓ |

---

## Test Documentation

Test documentation lives outside `Docs/` — in the test suite itself:

| Location | Content |
|:---|:---|
| `Tests/README.md` | Test index grouped by topic, with links and short descriptions per TC |
| `Tests/<TC>/README.md` | Full test description: purpose, scenario, expected results, calculation derivation, run command |

---

## Document Relationships

```
Design.md           ←  architecture, regulation split, scope
  │
  ├── Model.md      ←  WageType matrix, collectors, cases, lookups, reports
  │
  ├── Actions.md    ←  No-Code wrappers + Low-Code script specifications
  │
  └── UseCases.md   ←  provider workflows using Model + Actions
                        (incl. case availability and onboarding)

UncoveredCases.md   ←  what is NOT in the regulation and why
Maintenance.md      ←  annual update workflow + release checklist
ProviderStubs.md    ←  extension points for provider/employer layers
Compliance.md       ←  certification, filing obligations, data retention
```

---

## Naming Convention

All files follow the pattern: `{CC}.{RegulationName}-{Topic}.md`

The prefix makes files identifiable when viewed outside the repo context
(search results, open editor tabs, file transfers).

---

## Root `Docs/`

The root [`Docs/`](../Docs/) folder contains only the `README.md`.
All regulation documentation is year-specific and lives here in `{YYYY}/Docs/`.
