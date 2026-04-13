# {CC}.{RegulationName} — Tests

> Version 1.0 · {Month} {YYYY}
> Source: {official tax / contribution tables — title, publisher, publication date}
> Repo: `Regulation.{CC}.{RegulationName}`

Test case documentation for `{CC}.{RegulationName}` {YYYY}.
All derivations are based on official statutory source parameters.

---

## Setup

```pecmd
# Load regulation and test data
{YYYY}/Setup.pecmd

# Run all test cases
{YYYY}/Test.All.pecmd
```

Test data files:
- `Tests/{CC}.Test.Setup.json` — tenant, payroll, regulation layers
- `Tests/{CC}.Test.CompanyCases.json` — company case values

---

## Test Overview

### Guard Tests

| TC | Directory | Description |
|:---|:---|:---|
| GUARD-TC1 | `GUARD-TC1-{CC}-MandatoryFields` | Verifies Guard WT aborts when mandatory fields are missing |

### WageType Tests

| TC | Directory | Scenario | Salary | Key Parameter |
|:---|:---|:---|---:|:---|
| WT-TC1000 | `WT-TC1000-{CC}-{Scope}` | {Description} | {value} | {parameter} |
| WT-TC1001 | `WT-TC1001-{CC}-{Scope}` | {Description} | {value} | {parameter} |

---

## Statutory Parameter Reference {YYYY}

| Parameter | Value | Source |
|:---|---:|:---|
| {Parameter 1} | {value} | {Authority — publication, date} |
| {Parameter 2} | {value} | {Authority — publication, date} |
| {Ceiling / Cap} | {value} | {Authority — publication, date} |

---

## GUARD-TC1 — Mandatory Fields

**Purpose:** Verifies that the Guard WT aborts execution when a mandatory field is missing.

| File | Missing Field | Expected |
|:---|:---|:---|
| `GUARD-TC1-{CC}-MandatoryFields-{YYYY}.et.json` | `{CC}.{SalaryField}` | `AbortExecution` |

**Expected results:**
```json
"wageTypeResults": [],
"collectorResults": []
```

---

## WT-TC1000 — {Base Scenario Description}

**Purpose:** Verifies correct calculation for {scenario description}.

**Scenario:**

| Field | Value |
|:---|:---|
| `{CC}.{SalaryField}` | {value} |
| `{CC}.{ContractType}` | `{value}` |
| `{CC}.{TaxCreditFlag}` | `{true / false}` |

**Expected Results {YYYY}:**

| WT | Name | Value | Derivation |
|---:|:---|---:|:---|
| 1000 | {BaseSalary} | {value} | = {SalaryField} |
| 5100 | {TaxWT} | {value} | {formula: salary × rate / 12} |
| 5110 | {CreditWT} | {value} | {formula: max(0, MaxCredit − phase-out)} |
| 5130 | {TaxNet} | {value} | 5100 + 5110 |
| 6500 | {NetPay} | {value} | GrossIncome − Deductions |

**Calculation:**

```
{SalaryField}           = {value}
Annual base             = {value} × 12 = {value}

{TaxWT}:
  Annual tax            = {formula}
  Monthly               = {annual} / 12 = {value}

{CreditWT}:
  Credit                = max(0, {MaxCredit} − ({annual} − {threshold}) × {phase-out pct})
  Monthly               = {value} / 12 = {value}

{NetPay}               = GrossIncome ({value}) − Deductions ({value}) = {value}
```

**Source:** {Official table / authority / publication confirming the expected values.}

---

## WT-TC1001 — {Second Scenario Description}

**Purpose:** Verifies {scenario — different salary bracket / credit fully phased out / employer cost cap}.

**Scenario:**

| Field | Value |
|:---|:---|
| `{CC}.{SalaryField}` | {value} |

**Expected Results {YYYY}:**

| WT | Name | Value | Derivation |
|---:|:---|---:|:---|
| 1000 | {BaseSalary} | {value} | |
| 5100 | {TaxWT} | {value} | |
| 6500 | {NetPay} | {value} | |

**Calculation:** {formula derivation}

---

*Version 1.0 · {Month} {YYYY}*
