# {CC}.{RegulationName}

> **Payroll Engine Country Regulation Template**
> Replace all `{CC}`, `{RegulationName}`, `{cc}`, `{Provider}` placeholders before use.
> See [TEMPLATE.md](TEMPLATE.md) for step-by-step setup instructions.

Country payroll regulation for **{CC}** — `{RegulationName}`.

Implemented as a [Payroll Engine](https://github.com/Payroll-Engine) regulation.

---

## Scope

| Component | Status | Notes |
|---|---|---|
| Base salary (prorata) | ✅ | Calendar-day prorata via Indienst/Uitdienst |
| Tax withholding | 🔲 | To be implemented |
| Social insurance employee | 🔲 | To be implemented |
| Social insurance employer | 🔲 | To be implemented |
| ID validation | 🔲 | National identifier checksum |

---

## Regulation Structure

The regulation is split into a **core layer** (calculation logic) and
**data layers** (annual rates and parameters). Each data source can be
updated independently without touching the calculation scripts.

```
{CC}.{RegulationName}                  Core — cases, collectors, scripts, wage types
{CC}.{RegulationName}.Data.Tax         Tax brackets and rates (official statutory source)
```

Annual updates require only new data files with a matching `validFrom`.
The core regulation and scripts remain unchanged unless the calculation logic itself changes.

---

## Repository Layout

```
Regulation.{CC}.{RegulationName}/
  .github/
    workflows/
      ci.yml               CI build — working-directory: YYYY
      release.yml          Release trigger on YYYY/Directory.Build.props
  YYYY/
    Regulation/
      {CC}.{RegulationName}.{YYYY}.json
      {CC}.{RegulationName}.Scripts.{YYYY}.json
      {CC}.{RegulationName}.Collectors.{YYYY}.json
      {CC}.{RegulationName}.Cases.{YYYY}.json
      {CC}.{RegulationName}.WageTypes.{YYYY}.json
    Scripts/
      WageTypeValueFunction.Action.cs    No-Code custom actions (WageTypeValueAction)
      CaseValidateFunction.Action.cs     No-Code custom actions (CaseValidateAction)
    Reports/
      README.md
      {ReportName}/
        Report.json / ReportEndFunction.cs / {ReportName}.frx
        parameters.json / Import.pecmd / Script.pecmd / Report.Build.pecmd / Report.Pdf.pecmd / README.md
    Docs/
      {CC}.{RegulationName}-Analysis.md
      {CC}.{RegulationName}-NoCodeDesign.md
      {CC}.{RegulationName}-TestSpec.md
    Tests/
      README.md
      {CC}.Test.Setup.json
      {CC}.Test.CompanyCases.json
      TC01/
    Schemas/
      PayrollEngine.Exchange.schema.json
    Directory.Build.props
    regulation-package.json
    Regulation.{CC}.{RegulationName}.{YYYY}.csproj
    nuget.config
    Setup.pecmd
    Setup.Regulation.pecmd
    Setup.Data.pecmd
    Setup.Tests.pecmd
    Test.All.pecmd
    Test.Preview.pecmd
    Delete.pecmd
    Delete.Tests.pecmd
  Data.Tax.YYYY/
    Regulation/
      {CC}.{RegulationName}.Data.Tax.{YYYY}.json
    Schemas/
      PayrollEngine.Exchange.schema.json
    Directory.Build.props
    regulation-package.json
    Regulation.{CC}.{RegulationName}.Data.Tax.{YYYY}.csproj
    nuget.config
    Setup.pecmd
    Delete.pecmd
  Regulation.{CC}.{RegulationName}.sln
  Test.{YYYY}.pecmd
  README.md
  TEMPLATE.md
  .gitignore
```

---

## WageType / Collector Matrix

**Collectors:**

| Collector | Description | NetPay formula |
|---|---|---|
| `{CC}.GrossIncome` | Gross income — all gross salary components | + |
| `{CC}.Deductions` | Employee deductions — tax + social insurance | − |
| `{CC}.EmployerCost` | Employer social costs | informational |

`NetPay` (WT 6500) = `{CC}.GrossIncome` − `{CC}.Deductions`

**Matrix:**

| Nr | Name | GrossIncome | Deductions | EmployerCost | Action / Expression |
|---:|---|:---:|:---:|:---:|---|
| 10 | ProrataFactor | | | | `{CC}CalculateProrata` — calendar-day factor |
| 1000 | BaseSalary | ✓ | | | `^^BaseSalary × ^$10` |
| 5000 | GrossTotal | | | | `^&GrossIncome` — collector snapshot |
| 5100 | TaxWithheld | | ✓ | | `{CC}CalculateTax` — annual projection |
| 6500 | NetPay | | | | `^&GrossIncome − ^&Deductions` |

**Notes:**
- WT 6500 (`NetPay`) must always have a higher number than all component WageTypes
  to ensure collectors are fully populated at net salary calculation time.

---

## Custom Actions

### WageType Actions (`Scripts/WageTypeValueFunction.Action.cs`)

| Action | WT | Description |
|---|---|---|
| `{CC}CalculateProrata` | 10 | Calendar-day prorata factor from Indienst/Uitdienst dates |
| `{CC}CalculateTax` | 5100 | Tax withholding via annual projection |

### Case Validate Actions (`Scripts/CaseValidateFunction.Action.cs`)

| Action | Case | Description |
|---|---|---|
| `{CC}ValidateNationalId` | {CC}.Personal | National ID checksum validation |

---

## Cases

### Employee Cases

| Case | Fields | Description |
|---|---|---|
| `{CC}.Salary` | `{CC}.BaseSalary` | Monthly gross salary |
| `{CC}.Personal` | `{CC}.NationalId` | National identifier |
| `{CC}.Employment` | `{CC}.ContractType`, `{CC}.Indienst`, `{CC}.Uitdienst` | Contract type and employment period |

### Company Cases

| Case | Fields | Description |
|---|---|---|
| `{CC}.Configuration` | `{CC}.EmployerId` | Employer registration number |

---

## Payroll Layer Configuration

```json
"layers": [
  { "level": 1, "priority": 1, "regulationName": "{CC}.{RegulationName}" },
  { "level": 1, "priority": 2, "regulationName": "{CC}.{RegulationName}.Data.Tax" }
]
```

PE selects the correct regulation version automatically based on the payrun `EvaluationDate`.
Retro-payruns use historical versions without any manual configuration.

---

## Setup and Testing

```
# Full setup (regulation + data + test tenant)
YYYY/Setup.pecmd

# Run all tests
YYYY/Test.All.pecmd

# Or use the root shortcut
Test.YYYY.pecmd

# Cleanup
YYYY/Delete.pecmd
```

See [`YYYY/Tests/README.md`](YYYY/Tests/README.md) for the full test index.

---

## Reports

Reports are located in `YYYY/Reports/`. Each report lives in its own subfolder
with all required files co-located. See [`YYYY/Reports/README.md`](YYYY/Reports/README.md)
for the full workflow.

---

## Data Sources

| Regulation | Content | Source | Update Cycle |
|---|---|---|---|
| `{CC}.{RegulationName}.Data.Tax` | Tax brackets and rates | Official statutory source | Annual |

---

## Known Limitations

### Electronic Filing
Electronic declaration / submission to tax authorities is not included.
The regulation provides all calculation inputs required for manual filing.

---

## Documentation

| File | Description |
|---|---|
| [`YYYY/Docs/{CC}.{RegulationName}-Analysis.md`](YYYY/Docs/{CC}.{RegulationName}-Analysis.md) | System analysis, regulation design, versioning strategy |
| [`YYYY/Docs/{CC}.{RegulationName}-NoCodeDesign.md`](YYYY/Docs/{CC}.{RegulationName}-NoCodeDesign.md) | No-Code / Low-Code action specification |
| [`YYYY/Docs/{CC}.{RegulationName}-TestSpec.md`](YYYY/Docs/{CC}.{RegulationName}-TestSpec.md) | Test case calculations with source references |
| [`YYYY/Tests/README.md`](YYYY/Tests/README.md) | Test index |

---

## See Also
- [Payroll Engine](https://github.com/Payroll-Engine/PayrollEngine)
- [Country Bootstrap Guide](https://github.com/Payroll-Engine/Regulation.COM.Base/blob/main/Docs/Country-Bootstrap.md)
- [Consolidation Template](https://github.com/Payroll-Engine/PayrollEngine.Template.Regulation.Country.Consolidation) — template for COM.Base mapping regulation
- [Regulation Deployment](https://payrollengine.org/concepts/regulation-deployment)
