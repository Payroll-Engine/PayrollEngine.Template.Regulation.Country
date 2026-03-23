# {CC}.{RegulationName}

> **Payroll Engine Country Regulation Template**
> Replace all `{CC}`, `{RegulationName}`, `{cc}`, `{Provider}` placeholders before use.
> See [TEMPLATE.md](TEMPLATE.md) for step-by-step setup instructions.

Country payroll regulation for **{CC}** — `{RegulationName}`.

Implemented as a [Payroll Engine](https://github.com/Payroll-Engine) regulation.

---

## Scope

| Component | Included |
|---|---|
| Base salary | Yes |
| Tax withholding | Placeholder |
| Social insurance | Placeholder |

---

## Regulation Structure

```
{CC}.{RegulationName}                  Core — cases, collectors, scripts, wage types
{CC}.{RegulationName}.Data.*           Data regulations (annual rates and parameters)
```

---

## Repository Layout

```
Regulation.{CC}.{RegulationName}/
  Regulation/
    {CC}.{RegulationName}.{YYYY}.json
    {CC}.{RegulationName}.Scripts.{YYYY}.json
    {CC}.{RegulationName}.Collectors.{YYYY}.json
    {CC}.{RegulationName}.Cases.{YYYY}.json
    {CC}.{RegulationName}.WageTypes.{YYYY}.json
    {CC}.{RegulationName}.Data.{Source}.{YYYY}.json
  Scripts/
    WageTypeValueFunction.Action.cs
    CaseValidateFunction.Action.cs
  Tests/
    README.md
    {CC}.Test.Setup.json
    {CC}.Test.CompanyCases.json
    TC01/
  Schemas/
    PayrollEngine.Exchange.schema.json
  Directory.Build.props
  regulation-package.json
  nuget.config
  Setup.pecmd
  Setup.Regulation.pecmd
  Setup.Data.pecmd
  Setup.Tests.pecmd
  Test.All.pecmd
  Test.Preview.pecmd
  Delete.pecmd
  Delete.Tests.pecmd
  README.md
```

---

## Setup

```
Setup.pecmd
```

## Testing

```
Setup.Tests.pecmd
Test.All.pecmd
```

## Data Sources

| Regulation | Source | Update Cycle |
|---|---|---|
| `{CC}.{RegulationName}.Data.*` | Official statutory source | Annual |

---

## See Also
- [Payroll Engine](https://github.com/Payroll-Engine/PayrollEngine)
- [Country Bootstrap Guide](https://github.com/Payroll-Engine/Regulation.COM.Base/blob/main/Docs/Country-Bootstrap.md)
- [Regulation Deployment](https://payrollengine.org/concepts/regulation-deployment)
