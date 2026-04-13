# {CC}.{RegulationName} — Model Reference

> Version 1.0 · {Month} {YYYY}
> Source: {official source name and URL}
> Repo: `Regulation.{CC}.{RegulationName}`

Model reference for `{CC}.{RegulationName}` — collectors, WageType matrix,
cases, lookups, and reports. For calculation logic see `Actions.md`;
for design decisions see `Design.md`.

---

## 1. Regulation Layout

### Sub-Projects

| Folder | Content | Version |
|:---|:---|:---|
| `{YYYY}/` | Functional regulation {YYYY} — logic, cases, WageTypes, scripts, reports | {YYYY}.1 |
| `Data.{Source1}.{YYYY}/` | {Source1} data — {description} | {YYYY}.1 |
| `Data.{Source2}.{YYYY}/` | {Source2} data — {description} | {YYYY}.1 |

### Payroll Layer Configuration

```json
"layers": [
  { "level": 1, "priority": 1, "regulationName": "{CC}.{RegulationName}" },
  { "level": 1, "priority": 2, "regulationName": "{CC}.{RegulationName}.Data.{Source1}" },
  { "level": 1, "priority": 3, "regulationName": "{CC}.{RegulationName}.Data.{Source2}" }
]
```

---

## 2. Collectors

| Collector | Role | Sign in NetPay |
|:---|:---|:---:|
| `{CC}.GrossIncome` | Gross income — all gross salary components | + |
| `{CC}.Deductions` | Employee deductions — tax and social contributions | − |
| `{CC}.EmployerCost` | Employer social costs — not deducted from employee wage | info |

`{CC}.NetPay (WT {nr})` = `{CC}.GrossIncome` − `{CC}.Deductions`

---

## 3. WageType Matrix

| Nr | Name | Gross | Ded | Empl | Action / Expression |
|---:|:---|:---:|:---:|:---:|:---|
| 1 | Guard | | | | `{CC}GuardMandatoryFields` — aborts on missing {Field1} / {Field2} |
| 10 | {TechnicalWT} | | | | `{CC}{TechnicalAction}` — sets runtime value `{CC}.{RuntimeField}` |
| 1000 | {BaseSalary} | ✓ | | | `^^{SalaryField}` |
| 1100 | {SupplementWT} | ✓ | | | `^^{SupplementField}` |
| 5000 | {GrossMirror} | | | | `^&{CC}.GrossIncome` — collector mirror, informational |
| 5100 | {TaxWT} | | | | `{CC}Calculate{Tax}` — progressive calculation |
| 5110 | {CreditWT} | | | | `{CC}Calculate{Credit}` — negative; reduces tax |
| 5130 | {TaxNet} | | ✓ | | `^$5100 + ^$5110` |
| 6500 | {NetPay} | | | | `^&{CC}.GrossIncome - ^&{CC}.Deductions` |
| 6700 | {EmployerWT1} | | | ✓ | `{CC}Calculate{EmployerCost1}` |
| 6710 | {EmployerWT2} | | | ✓ | `{CC}Calculate{EmployerCost2}` |

> **Notes:**
> - WT 1–{n} (Guards): no collector — abort / warn on invalid data.
> - WTs 5100–5120: no collector — intermediate values summed in WT 5130.
> - Collector mirrors (WT 5000, 6600-series): suppressed on payslip; informational only.

---

## 4. Cases

### 4.1 Employee Cases

| Case | CaseFields | Description | Availability |
|:---|:---|:---|:---|
| `{CC}.{Employment}` | `{CC}.{ContractType}`, `{CC}.{StartDate}` | Employment contract data | always |
| `{CC}.{Salary}` | `{CC}.{SalaryField}` | Monthly gross salary | `{CC}.{ContractType}` is set |
| `{CC}.{TaxConfig}` | `{CC}.{TaxCreditRequested}` | Tax credit configuration | `{CC}.{ContractType}` is set |

### 4.2 Company Cases

| Case | CaseFields | Description |
|:---|:---|:---|
| `{CC}.{CompanyConfig}` | `{CC}.{CompanyField1}` | {Description} |

### 4.3 Minimum Required Set

The following fields are mandatory — missing values cause Guard WT abort:

| CaseField | Case | Scope | Guard |
|:---|:---|:---|:---|
| `{CC}.{SalaryField}` | `{CC}.{Salary}` | Employee | WT 1 (> 0) |
| `{CC}.{ContractType}` | `{CC}.{Employment}` | Employee | WT 1 (not null) |
| `{CC}.{CompanyField1}` | `{CC}.{CompanyConfig}` | Company | WT 1 (not null) |

---

## 5. Lookups

| Lookup | Type | Source Regulation | Source Authority | Update Cycle |
|:---|:---|:---|:---|:---|
| `{CC}.{TaxBrackets}` | Progressive range | `Data.{Source1}` | {Authority} | Annual |
| `{CC}.{TaxParameter}` | Single-record (`valueObject`) | `Data.{Source1}` | {Authority} | Annual |
| `{CC}.{SocialParameter}` | Single-record (`valueObject`) | `Data.{Source2}` | {Authority} | Annual |

### Key Schema — `{CC}.{TaxParameter}`

```json
{
  "key": "{YYYY}",
  "valueObject": {
    "{RateField1}": 0.0000,
    "{RateField2}": 0.0000,
    "{CeilingField}": 0
  }
}
```

---

## 6. Reports

| ID | Name | DB Name | Description |
|:---|:---|:---|:---|
| R01 | {ReportName1} | `{CC}.{ReportName1}` | {Period payslip / YTD summary / annual statement} |
| R02 | {ReportName2} | `{CC}.{ReportName2}` | {Description} |

Reports are located in `{YYYY}/Reports/`. Each report folder contains:
`Report.json`, `{Name}.frx`, `ReportEndFunction.cs`, `Import.pecmd`.

---

*Version 1.0 · {Month} {YYYY}*
