# Template Setup Guide

This repository is a **GitHub Template** for creating Payroll Engine country regulations.
Click **"Use this template"** on GitHub to create your regulation repo.

---

## Placeholders

Replace every occurrence of these placeholders throughout all files:

| Placeholder | Example | Description |
|---|---|---|
| `{CC}` | `DE` | ISO-3166 alpha-2 country code, uppercase |
| `{cc}` | `de` | ISO-3166 alpha-2 country code, lowercase (GitHub topics, identifiers) |
| `{RegulationName}` | `Lohnsteuer` | Regulation name, PascalCase, no spaces |
| `{Provider}` | `Acme` | Your organisation / GitHub account name |
| `{YYYY}` | `2026` | Tax year |
| `{PrimaryLanguage}` | `de` | ISO-639-1 language code of the regulation's primary language |

> **`{cc}` vs `{PrimaryLanguage}`:** Different placeholders.
> `{cc}` is the ISO-3166 country code (lowercase) — used in identifiers and GitHub topics.
> `{PrimaryLanguage}` is the ISO-639-1 language code — used in `descriptionLocalizations` and `culture`.
> For most countries they differ: BE → `{cc}` = `be`, `{PrimaryLanguage}` = `nl` (or `fr`).
> For NL they happen to be equal: `{cc}` = `nl`, `{PrimaryLanguage}` = `nl`.

> **PascalCase names are required.** WageTypes, Collectors, Cases, CaseFields, Lookups
> must be PascalCase with no spaces. No-Code action references (`^$`, `^&`) use names
> as identifiers — spaces break the parser.

---

## Step-by-Step

### 1. Create repo from template
On GitHub: **Use this template → Create a new repository**

Repo name convention: `Regulation.{CC}.{RegulationName}`
Example: `Regulation.DE.Lohnsteuer`

Add GitHub Topics: `payrollengine`, `regulation`, `country-regulation`, `country-{cc}`, `{cc}-payroll`

### 2. Replace placeholders
Find & replace in all files:
```
{CC}               → DE
{RegulationName}   → Lohnsteuer
{cc}               → de
{Provider}         → Acme
{YYYY}             → 2026
{PrimaryLanguage}  → de
```

Rename files:
```
YYYY/                                        → 2026/
Data.Tax.YYYY/                               → Data.Tax.2026/
YYYY/Regulation/{CC}.{RegulationName}.*      → 2026/Regulation/DE.Lohnsteuer.*
YYYY/Regulation.CC.RegulationName.YYYY.csproj → 2026/Regulation.DE.Lohnsteuer.2026.csproj
Data.Tax.YYYY/Regulation.CC.*.csproj        → Data.Tax.2026/Regulation.DE.Lohnsteuer.Data.Tax.2026.csproj
YYYY/Tests/CC.Test.Setup.json               → 2026/Tests/DE.Test.Setup.json
YYYY/Tests/CC.Test.CompanyCases.json        → 2026/Tests/DE.Test.CompanyCases.json
Test.YYYY.pecmd                              → Test.2026.pecmd
```

Rename test folders (replace `{CC}`, `{nn}`, `{Scope}` with actual values):
```
YYYY/Tests/WT-TC{nn}-{CC}-{Scope}/         → 2026/Tests/WT-TC1000-DE-BaseSalary/
YYYY/Tests/GUARD-TC{n}-{CC}-{Scope}/       → 2026/Tests/GUARD-TC1-DE-MandatoryFields/
```

Rename script files (replace `{Scope}` and `{Domain}` with actual domain names):
```
YYYY/Scripts/WageTypeValueFunction.{Scope}.Action.cs  → 2026/Scripts/WageTypeValueFunction.Gross.Action.cs
YYYY/Scripts/{CC}{Domain}Algorithm.cs                 → 2026/Scripts/DE{Domain}Algorithm.cs
```

Rename doc files (replace `{CC}`, `{RegulationName}`, `{Month}`, `{YYYY}` with actual values):
```
YYYY/Docs/{CC}.{RegulationName}-Model.md          → 2026/Docs/DE.Lohnsteuer-Model.md
YYYY/Docs/{CC}.{RegulationName}-Design.md         → 2026/Docs/DE.Lohnsteuer-Design.md
YYYY/Docs/{CC}.{RegulationName}-Actions.md        → 2026/Docs/DE.Lohnsteuer-Actions.md
YYYY/Docs/{CC}.{RegulationName}-UseCases.md       → 2026/Docs/DE.Lohnsteuer-UseCases.md
YYYY/Docs/{CC}.{RegulationName}-UncoveredCases.md → 2026/Docs/DE.Lohnsteuer-UncoveredCases.md
YYYY/Docs/{CC}.{RegulationName}-Maintenance.md    → 2026/Docs/DE.Lohnsteuer-Maintenance.md
YYYY/Docs/{CC}.{RegulationName}-ProviderStubs.md  → 2026/Docs/DE.Lohnsteuer-ProviderStubs.md
YYYY/Docs/{CC}.{RegulationName}-Compliance.md     → 2026/Docs/DE.Lohnsteuer-Compliance.md
```

Update `.sln`: replace `YYYY` folder references and `.csproj` filenames throughout.

### 3. Configure `YYYY/Directory.Build.props`
```xml
<Version>2026.1-beta.dev</Version>
<Product>Payroll Engine Regulation DE.Lohnsteuer 2026</Product>
<RepositoryUrl>https://github.com/Acme/Regulation.DE.Lohnsteuer.git</RepositoryUrl>
```

### 4. Configure `Data.Tax.YYYY/Directory.Build.props`
```xml
<Version>2026.1-beta.dev</Version>
<Product>Payroll Engine Data Regulation DE.Lohnsteuer.Data.Tax 2026</Product>
<RepositoryUrl>https://github.com/Acme/Regulation.DE.Lohnsteuer.git</RepositoryUrl>
```

### 5. Configure `YYYY/regulation-package.json`
Update `packageId`, `regulationName`, and `installFiles`.

Scripts must be listed **before** Cases and WageTypes (PE processes files in order).

Split Cases and WageTypes into separate files by domain:
```json
"installFiles": [
  "Regulation/{CC}.{RegulationName}.{YYYY}.json",
  "Regulation/{CC}.{RegulationName}.Scripts.{YYYY}.json",
  "Regulation/{CC}.{RegulationName}.Collectors.{YYYY}.json",
  "Regulation/{CC}.{RegulationName}.Cases.Company.{YYYY}.json",
  "Regulation/{CC}.{RegulationName}.Cases.Employee.Core.{YYYY}.json",
  "Regulation/{CC}.{RegulationName}.WageTypes.Guard.{YYYY}.json",
  "Regulation/{CC}.{RegulationName}.WageTypes.Gross.{YYYY}.json",
  "Regulation/{CC}.{RegulationName}.WageTypes.Deductions.{YYYY}.json",
  "Regulation/{CC}.{RegulationName}.WageTypes.Employer.{YYYY}.json"
]
```

Add further `Cases.Employee.{Scope}` or `WageTypes.{Scope}` splits as needed.

### 6. Configure `YYYY/Regulation.CC.RegulationName.YYYY.csproj`
Update `PackageId`, `Description`, and `PackageTags`.

### 7. Update `.github/workflows/ci.yml` and `release.yml`
Replace `YYYY` in `working-directory` and `paths` with the actual year:
```yaml
working-directory: '2026'    # was: 'YYYY'
paths:
  - '2026/Directory.Build.props'    # was: 'YYYY/Directory.Build.props'
```

### 8. Add PAT_DISPATCH secret
In your GitHub repo: **Settings → Secrets → Actions → New repository secret**
Name: `PAT_DISPATCH`
Value: Classic PAT with scopes: `repo`, `workflow`, `write:packages`, `read:packages`

### 9. Add schemas
Copy `PayrollEngine.Exchange.schema.json` into:
- `YYYY/Schemas/`
- `Data.Tax.YYYY/Schemas/`

### 10. Fill in `YYYY/Docs/`
The 8 standard shell documents are already present in `YYYY/Docs/`. Fill in all `{…}`
placeholders with actual regulation content. See `YYYY/Docs/README.md` for the
document index and relationships.

```
YYYY/Docs/{CC}.{RegulationName}-Model.md          — collectors, WageType matrix, cases, lookups, reports
YYYY/Docs/{CC}.{RegulationName}-Design.md         — architecture, regulation split, design decisions, scope
YYYY/Docs/{CC}.{RegulationName}-Actions.md        — No-Code / Low-Code action specification with formulas
YYYY/Docs/{CC}.{RegulationName}-UseCases.md       — provider use cases: setup, payrun, case availability
YYYY/Docs/{CC}.{RegulationName}-UncoveredCases.md — uncovered statutory cases: Tier A/B/C classification
YYYY/Docs/{CC}.{RegulationName}-Maintenance.md    — year-over-year update workflow and release checklist
YYYY/Docs/{CC}.{RegulationName}-ProviderStubs.md  — stub WageTypes, override instructions, approximations
YYYY/Docs/{CC}.{RegulationName}-Compliance.md     — certification status, filing obligations, statutory scope
```

Test documentation lives in the test suite, not in `Docs/`:
- `YYYY/Tests/README.md` — test index grouped by topic, with links and short descriptions per TC
- `YYYY/Tests/<TC>/README.md` — full test description: purpose, scenario, expected results, derivation

### 11. Implement regulation objects
Follow the [Country Bootstrap Guide](https://github.com/Payroll-Engine/Regulation.Consolidation/blob/main/Docs/Country-Bootstrap.md).

Implement JSON files in `YYYY/Regulation/`:
- `{CC}.{RegulationName}.{YYYY}.json` — regulation definition (name, namespace, validFrom)
- `{CC}.{RegulationName}.Scripts.{YYYY}.json` — script registrations (see Step 11a)
- `{CC}.{RegulationName}.Collectors.{YYYY}.json` — collector definitions
- `{CC}.{RegulationName}.Cases.Company.{YYYY}.json` — company case definitions
- `{CC}.{RegulationName}.Cases.Employee.Core.{YYYY}.json` — core employee cases
- `{CC}.{RegulationName}.Cases.Employee.{Scope}.{YYYY}.json` — additional employee case splits
- `{CC}.{RegulationName}.WageTypes.Guard.{YYYY}.json` — guard WTs (WT 1–9)
- `{CC}.{RegulationName}.WageTypes.Gross.{YYYY}.json` — gross income WTs
- `{CC}.{RegulationName}.WageTypes.Deductions.{YYYY}.json` — employee deduction WTs
- `{CC}.{RegulationName}.WageTypes.Employer.{YYYY}.json` — employer cost WTs

Implement data file in `Data.Tax.YYYY/Regulation/`:
- `{CC}.{RegulationName}.Data.Tax.{YYYY}.json` — tax brackets and rates as lookups

### 11a. Consolidation WageTypes (multi-country only)

If the regulation participates in multi-country consolidation, add WageTypes
7000–7030 with `clusters: ["Consolidation"]` to the WageTypes files.
These read-back WTs are validated at payrun time by Guard WTs 8000–8030 in
`Regulation.Consolidation`.

```json
{ "wageTypeNumber": 7000, "name": "TotalGross",               "clusters": ["Consolidation"], "valueActions": ["^${TotalGrossWT}"] },
{ "wageTypeNumber": 7005, "name": "TaxEmployee",              "clusters": ["Consolidation"], "valueActions": ["..."] },
{ "wageTypeNumber": 7010, "name": "TotalDeductionsEmployee",  "clusters": ["Consolidation"], "valueActions": ["..."] },
{ "wageTypeNumber": 7015, "name": "SocialSecEmployee",        "clusters": ["Consolidation"], "valueActions": ["..."] },
{ "wageTypeNumber": 7020, "name": "TotalEmployerBurdens",     "clusters": ["Consolidation"], "valueActions": ["..."] },
{ "wageTypeNumber": 7025, "name": "NetIncome",                "clusters": ["Consolidation"], "valueActions": ["..."] },
{ "wageTypeNumber": 7030, "name": "TotalEmployerCost",        "clusters": ["Consolidation"], "valueActions": ["..."] }
```

Store the original local name in `nameLocalizations` to avoid naming conflicts
with existing WTs in the same regulation.

### 11b. Register scripts in `Scripts.{YYYY}.json`

Every `.cs` file in `YYYY/Scripts/` must be registered in
`{CC}.{RegulationName}.Scripts.{YYYY}.json`. The order matters:
**Shared → Guard → domain Actions → CaseValidate → Algorithms**

```json
{ "name": "WageTypeValue.Shared",  "functionTypes": ["WageTypeValue"], "valueFile": "../Scripts/WageTypeValueFunction.Shared.Action.cs" },
{ "name": "WageTypeValue.Guard",   "functionTypes": ["WageTypeValue"], "valueFile": "../Scripts/WageTypeValueFunction.Guard.Action.cs" },
{ "name": "WageTypeValue.{Scope}", "functionTypes": ["WageTypeValue"], "valueFile": "../Scripts/WageTypeValueFunction.{Scope}.Action.cs" },
{ "name": "CaseValidate",          "functionTypes": ["CaseValidate"],  "valueFile": "../Scripts/CaseValidateFunction.Action.cs" },
{ "name": "Algorithm.{CC}{Domain}Algorithm", "functionTypes": ["WageTypeValue"], "valueFile": "../Scripts/{CC}{Domain}Algorithm.cs" }
```

Unregistered scripts are silently ignored by PE — a missing entry causes
all actions defined in that file to return `0` with no error.

### 12. Implement test cases

**TC naming conventions (non-negotiable):**
```
Wage type tests:   WT-TC{nn}-{CC}-{Scope}     e.g. WT-TC1000-DE-BaseSalary
Guard tests:       GUARD-TC{n}-{CC}-{Scope}   e.g. GUARD-TC1-DE-MandatoryFields
```

Each TC lives in its own folder:
```
YYYY/Tests/WT-TC{nn}-{CC}-{Scope}/
  WT-TC{nn}-{CC}-{Scope}-{YYYY}.et.json    — payrun employee test
  WT-TC{nn}-{CC}-{Scope}.pecmd             — single-TC runner
  README.md                                — purpose, scenario, expected results, derivation
```

For guard TCs the expected result is always empty:
```json
"wageTypeResults": [],
"collectorResults": []
```

Document the full derivation (formula + statutory source) in each TC's `README.md`.

`YYYY/Tests/README.md` lists all TCs grouped by topic, with links and a short description per TC.

Update `YYYY/Test.All.pecmd` with all TCs grouped by phase (Guards first,
Company Case setters last).

### 13. First release
Remove `.dev` suffix in `YYYY/Directory.Build.props`:
```xml
<Version>2026.1</Version>
```
Commit + push → CI/CD creates the GitHub Release and NuGet package automatically.

---

## CI/CD Behaviour

| Situation | Result |
|---|---|
| Version ends with `.dev` | Workflow runs CI only, no release |
| Version tag already exists | Silently skipped (idempotent) |
| New version on `main` | Package built, published to GitHub Packages, GitHub Release created |
| Private repo | Package automatically private |
| Public repo | Package automatically public |

The release workflow triggers on changes to `YYYY/Directory.Build.props`.
`Data.Tax.YYYY` has its own independent CI/CD cycle — add separate workflow files
if independent data regulation releases are required.
