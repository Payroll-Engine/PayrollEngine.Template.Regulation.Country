# Regulation.{CC}.{Name}

{Country} payroll regulation for **{Full Legal Name}** — {Year}.

Implemented as a [Payroll Engine](https://github.com/Payroll-Engine) regulation.
Targets payroll periods from {Year} onwards. Rates and parameters are sourced
directly from official {AuthorityName} publications.

---

## Status

> **Status:** {e.g. stable / beta / in progress — short note on current phase}
> **Scope:** {e.g. Régimen General — key components covered in one line}
> **Certification:** {e.g. SILTRA/Sistema RED — out of scope | not applicable}

---

## Scope

| Component | Included |
|:---|:---:|
| {Component 1} | ✅ |
| {Component 2} | ✅ |
| {Component 3 — out of scope} | ❌ |
| {Component 4 — planned} | 🔜 |

---

## Regulation Layers

The regulation is split into a **core layer** (calculation logic) and
**data layers** (annual rates and parameters). Each data source can be
updated independently without touching the calculation scripts.

```
{CC}.{Name}                        Core — logic, cases, wage types, scripts
{CC}.{Name}.Data.{Source1}         {Description} ({Authority})
{CC}.{Name}.Data.{Source2}         {Description} ({Authority})
```

Annual updates require only new data files (e.g. `{CC}.{Name}.Data.{Source1}.{Year+1}.json`)
with a matching `validFrom`. The core regulation and scripts remain unchanged unless
the calculation logic itself changes.

| Regulation | Type | Content |
|:---|:---|:---|
| `{CC}.{Name}` | Functional | Cases, WageTypes, Collectors, Scripts |
| `{CC}.{Name}.Data.{Source1}` | Data | {Description} |
| `{CC}.{Name}.Data.{Source2}` | Data | {Description} |

---

## Repository Layout

```
Regulation.{CC}.{Name}/
  {Year}/
    Regulation/
      {CC}.{Name}.Cases.{Year}.json
      {CC}.{Name}.Collectors.{Year}.json
      {CC}.{Name}.Scripts.{Year}.json
      {CC}.{Name}.WageTypes.{Year}.json
    Scripts/
      WageTypeValueFunction.Action.cs   Custom actions (WageTypeValueAction)
      CaseValidateFunction.Action.cs    Custom actions (CaseValidateAction)
      {CC}{Helper1}.cs                  {Description} (also unit-tested)
      {CC}{Helper2}.cs                  {Description}
    Tests/
      {CC}.Test.Setup.json
      {CC}.Test.CompanyCases.json
      Tests/                            Individual test cases (WT-TC*, GUARD-TC*)
    Tests.Unit/                         xUnit unit tests for algorithm classes
    Reports/
      {ReportFolder1}/
      {ReportFolder2}/
    Setup.pecmd
    Test.All.pecmd
  Data.{Source1}.{Year}/
  Data.{Source2}.{Year}/
  Docs/
  Schemas/
```

---

## WageType / Collector Matrix

**Collectors:**

| Collector | Role | NettoLoon |
|:---|:---|:---:|
| `{CC}.{GrossCollector}` | Gross income — all gross salary components | + |
| `{CC}.{DeductionCollector}` | Employee deductions | − |
| `{CC}.{EmployerCollector}` | Employer social costs | info |

`NettoLoon (WT {nr})` = `{CC}.{GrossCollector}` − `{CC}.{DeductionCollector}`

**Matrix:**

| Nr | Name | Gross | Ded | Empl | Action / Expression |
|---:|:---|:---:|:---:|:---:|:---|
| 1 | Guard | | | | `{CC}GuardMandatoryFields` — aborts on missing {Field1}/{Field2} |
| 2 | Guard{X} | | | | `{CC}Guard{X}` — aborts when {Condition} |
| 10 | {TechnicalWT} | | | | `{ActionName}` — sets `{CC}.{RuntimeValue}` |
| 1000 | {GrossWT1} | ✓ | | | `{ActionName}` — {short description} |
| 1100 | {GrossWT2} | ✓ | | | `{expression}` |
| 5000 | {GrossMirror} | | | | `^&{GrossCollector}` — collector mirror, informational |
| 5100 | {TaxWT} | | | | `{ActionName}` — {short description} |
| 5110 | {CreditWT} | | | | `{ActionName}` — negative; {short description} |
| 5130 | {TaxNet} | | ✓ | | `^${5100} + ^${5110}` |
| 6500 | NettoLoon | | | | `^&{GrossCollector} − ^&{DeductionCollector}` |
| 6700 | {EmployerWT1} | | | ✓ | `{ActionName}` — {short description} |
| 6710 | {EmployerWT2} | | | ✓ | `{ActionName}` — {short description} |

> **Notes:**
> - WTs 1–{n} (Guards), {tech WTs}: no collector — runtime setters / control flow.
> - WTs 5100–5120: no collector — intermediate values summed in WT 5130.

---

## Custom Actions

### WageType Actions (`Scripts/WageTypeValueFunction.Action.cs`)

| Action | WT | Description |
|:---|---:|:---|
| `{CC}GuardMandatoryFields` | 1 | Validates {Field1} > 0, {Field2} set; aborts on failure |
| `{ActionName}` | {nr} | {short description} |

### Case Validate Actions (`Scripts/CaseValidateFunction.Action.cs`)

| Action | Case | Description |
|:---|:---|:---|
| `{ValidateAction1}` | `{CC}.{Case1}` | {short description} |
| `{ValidateAction2}` | `{CC}.{Case2}` | {short description} |

---

## Cases

### Employee Cases

| Case | Fields | Description | Availability |
|:---|:---|:---|:---|
| `{CC}.{Case1}` | `{CC}.{Field1}`, `{CC}.{Field2}` | {Description} | always |
| `{CC}.{Case2}` | `{CC}.{Field3}` | {Description} | {Condition} is set |

### Company Cases

| Case | Fields | Description |
|:---|:---|:---|
| `{CC}.{CompanyCase1}` | `{CC}.{Field1}` | {Description} |

---

## Payroll Layer Configuration

```json
"layers": [
  { "level": 1, "priority": 1, "regulationName": "{CC}.{Name}" },
  { "level": 1, "priority": 2, "regulationName": "{CC}.{Name}.Data.{Source1}" },
  { "level": 1, "priority": 3, "regulationName": "{CC}.{Name}.Data.{Source2}" }
]
```

PE selects the correct regulation version automatically based on the
payrun `EvaluationDate` — retro-payruns use historical versions without manual config.

---

## Sub-Projects

| Folder | Content | Version |
|:---|:---|:---|
| `{Year}/` | Functional regulation {Year} | {Year}.1-alpha.dev |
| `Data.{Source1}.{Year}/` | {Source1} data regulation {Year} | {Year}.1-alpha.dev |
| `Data.{Source2}.{Year}/` | {Source2} data regulation {Year} | {Year}.1-alpha.dev |

---

## Quick Start

```pecmd
# Standard payroll
Test.{Year}.pecmd

# {Special scenario, if applicable}
Test.{Scenario}.{Year}.pecmd
```

### Setup and Testing

```
# 1 — Import regulation and data
{Year}/Setup.pecmd

# 2 — Run all tests
{Year}/Test.All.pecmd
```

---

## Data Sources

| Regulation | Source | Update Cycle |
|:---|:---|:---|
| `Data.{Source1}` | {Authority} — *{Publication name}* | Annual ({Month}) |
| `Data.{Source2}` | {Authority} — *{Publication name}* | Annual ({Month}) |
| `Data.{Source3}` | {Authority} — *{Publication name}* | Semi-annual (1 Jan + 1 Jul) |

---

## Reports

Reports are independent of the test suite and located in `{Year}/Reports/`.
Each report lives in its own subfolder with all required files co-located.

| ID | Name | DB Name | Description |
|:---|:---|:---|:---|
| R01 | {ReportName1} | `{CC}.{ReportName1}` | {Description — period/YTD/annual} |
| R02 | {ReportName2} | `{CC}.{ReportName2}` | {Description} |

---

## Annual Update Workflow

See [`{Year}/Docs/{CC}.{Name}-UpdateWorkflow.md`]({Year}/Docs/{CC}.{Name}-UpdateWorkflow.md)
for the complete year-over-year update process ({key items: rates, brackets, ceilings, etc.}).

---

## Known Limitations

### {Limitation Title 1}
{Description — what is missing, why, and how the gap can be closed if needed.}

### {Limitation Title 2}
{Description.}

---

## Legal Sources

- {Law/Ordinance 1} — {short description of what it governs}
- {Law/Ordinance 2} — {short description}
- {Official Website} — https://{url}

---

## See Also

- [Payroll Engine](https://github.com/Payroll-Engine/PayrollEngine)
- [Country Bootstrap Guide](https://github.com/Payroll-Engine/Regulation.COM.Base/blob/main/Docs/Country-Bootstrap.md)
- [Country Regulation Template](https://github.com/Payroll-Engine/PayrollEngine.Template.Regulation.Country)
- [Regulation Deployment](https://payrollengine.org/concepts/regulation-deployment)
