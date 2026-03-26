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
Update `packageId`, `regulationName`, and `installFiles` order.
Scripts must be listed before Cases and WageTypes.

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

### 10. Create `YYYY/Docs/`
Add analysis and design documentation:
```
YYYY/Docs/{CC}.{RegulationName}-Analysis.md      — system analysis, regulation overview
YYYY/Docs/{CC}.{RegulationName}-NoCodeDesign.md  — No-Code/Low-Code action specification
YYYY/Docs/{CC}.{RegulationName}-TestSpec.md      — test case calculations with source references
```

### 11. Implement regulation objects
Follow the [Country Bootstrap Guide](https://github.com/Payroll-Engine/Regulation.COM.Base/blob/main/Docs/Country-Bootstrap.md).

Implement JSON files in `YYYY/Regulation/`:
- `{CC}.{RegulationName}.{YYYY}.json` — regulation definition (name, attributes)
- `{CC}.{RegulationName}.Scripts.{YYYY}.json` — script references
- `{CC}.{RegulationName}.Collectors.{YYYY}.json` — collector definitions
- `{CC}.{RegulationName}.Cases.{YYYY}.json` — employee + company case definitions
- `{CC}.{RegulationName}.WageTypes.{YYYY}.json` — wage type definitions with actions

Implement data file in `Data.Tax.YYYY/Regulation/`:
- `{CC}.{RegulationName}.Data.Tax.{YYYY}.json` — tax brackets and rates as lookups

### 12. Update `YYYY/Tests/TC01/TC01-BaseSalary.et.json`
Replace case field names with actual field names from your regulation.
Calculate and fill in the expected `wageTypeResults` and `collectorResults`.
Document the derivation in `YYYY/Tests/TC01/README.md`.

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
