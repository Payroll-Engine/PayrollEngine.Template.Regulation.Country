# Template Setup Guide

This repository is a **GitHub Template** for creating Payroll Engine country regulations.
Click **"Use this template"** on GitHub to create your regulation repo.

---

## Placeholders

Replace every occurrence of these placeholders throughout all files:

| Placeholder | Example | Description |
|---|---|---|
| `{CC}` | `DE` | ISO-3166 alpha-2 country code, uppercase |
| `{cc}` | `de` | ISO-3166 alpha-2 country code, lowercase (for GitHub topics) |
| `{RegulationName}` | `Lohnsteuer` | Regulation name, PascalCase, no spaces |
| `{Provider}` | `Acme` | Your organisation / GitHub account name |
| `{YYYY}` | `2026` | Tax year |

---

## Step-by-Step

### 1. Create repo from template
On GitHub: **Use this template → Create a new repository**

Repo name convention: `Regulation.{CC}.{RegulationName}`
Example: `Regulation.DE.Lohnsteuer`

Add GitHub Topics: `payrollengine`, `regulation`, `country-regulation`, `country-{cc}`

### 2. Replace placeholders
Find & replace in all files:
```
{CC}             → DE
{RegulationName} → Lohnsteuer
{cc}             → de
{Provider}       → Acme
{YYYY}           → 2026
```

Rename files accordingly:
```
Regulation/{CC}.{RegulationName}.{YYYY}.json
  → Regulation/DE.Lohnsteuer.2026.json
```

### 3. Configure Directory.Build.props
```xml
<Version>2026.1-beta.dev</Version>
<Product>Payroll Engine Regulation DE.Lohnsteuer</Product>
<RepositoryUrl>https://github.com/Acme/Regulation.DE.Lohnsteuer.git</RepositoryUrl>
```

### 4. Configure regulation-package.json
Update `packageId`, `regulationName`, and `installFiles` order.
Scripts must come before Cases and WageTypes.

### 5. Add PAT_DISPATCH secret
In your GitHub repo: **Settings → Secrets → Actions → New repository secret**
Name: `PAT_DISPATCH`
Value: Classic PAT with scopes: `repo`, `workflow`, `write:packages`, `read:packages`

### 6. Add schemas
Copy `PayrollEngine.Exchange.schema.json` from the PE main repo into `Schemas/`.

### 7. Implement regulation objects
Follow the [Country Bootstrap Guide](https://github.com/Payroll-Engine/Regulation.COM.Base/blob/main/Docs/Country-Bootstrap.md).

### 8. First release
Set version in `Directory.Build.props` (remove `.dev` suffix):
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
