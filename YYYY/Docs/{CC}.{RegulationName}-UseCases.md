# {CC}.{RegulationName} — Use Cases

> Version 1.0 · {Month} {YYYY}
> Repo: `Regulation.{CC}.{RegulationName}`

Provider use cases for `{CC}.{RegulationName}` — concrete workflows for setting up
and running payroll with this regulation. For the model reference see `Model.md`.

---

## 1. Prerequisites

Before running any use case, ensure the following regulations are loaded:

```pecmd
# Load all required regulations
{YYYY}/Setup.pecmd
```

Required payroll layers:

```json
"layers": [
  { "level": 1, "priority": 1, "regulationName": "{CC}.{RegulationName}" },
  { "level": 1, "priority": 2, "regulationName": "{CC}.{RegulationName}.Data.{Source1}" }
]
```

---

## 2. UC-01 — Company Setup

### Context
First-time configuration of a new company using `{CC}.{RegulationName}`.

### Steps

1. **Create tenant and payroll** with regulation layers (see Prerequisites).

2. **Set mandatory company cases:**

   | Case | Field | Value | Description |
   |:---|:---|:---|:---|
   | `{CC}.{CompanyConfig}` | `{CC}.{CompanyField1}` | {example} | {description} |
   | `{CC}.{CompanyConfig}` | `{CC}.{CompanyField2}` | {example} | {description} |

3. **Verify guard passes** — run a preview payrun for an existing employee.
   Expected: no Guard abort, calculation result for all WTs.

### Validation
```pecmd
{YYYY}/Test.Preview.pecmd
```

---

## 3. UC-02 — New Employee Onboarding

### Context
Adding a new employee and setting the minimum required case data.

### Steps

1. **Set identification case** (always first):

   | Case | Field | Value |
   |:---|:---|:---|
   | `{CC}.{Employment}` | `{CC}.{ContractType}` | `{value}` |

2. **Set salary and employment data:**

   | Case | Field | Value |
   |:---|:---|:---|
   | `{CC}.{Salary}` | `{CC}.{SalaryField}` | `{example value}` |
   | `{CC}.{TaxConfig}` | `{CC}.{TaxCreditFlag}` | `{true / false}` |

3. **Run preview payrun** — verify calculated values match expected:

   | WT | Name | Expected |
   |---:|:---|:---|
   | 1000 | {BaseSalary} | = {SalaryField} |
   | 5100 | {TaxWT} | {formula reference} |
   | 6500 | {NetPay} | GrossIncome − Deductions |

---

## 4. UC-03 — Standard Monthly Payrun

### Context
Running a regular monthly legal payrun for all employees.

### Steps

1. Verify all mandatory case data is set for all active employees.
2. Run the payrun job:
   ```pecmd
   # Standard monthly payrun
   {YYYY}/Test.All.pecmd
   ```
3. Review results — check for warnings in Guard WTs.
4. Approve and export for submission to {authority}.

### Expected Results

| WT | Name | Result |
|---:|:---|:---|
| 1 | Guard | 0 (no abort) |
| 6500 | {NetPay} | GrossIncome − Deductions |

---

## 5. UC-04 — Mid-Year Salary Change

### Context
Employee receives a salary increase effective mid-period.
Retro correction recalculates prior months at the updated salary.

### Steps

1. **Update salary case value** with the new start date:
   ```json
   { "caseFieldName": "{CC}.{SalaryField}", "value": "{new value}", "start": "{YYYY}-{MM}-01" }
   ```

2. **Run retro payrun** — PE recalculates all affected prior periods automatically.

3. **Verify retro delta** — check WT results for the correction period:
   - Delta WT 1000: `new salary − old salary`
   - Delta WT 5100: recalculated tax on new salary

### Notes
- Retro payruns require `storeEmptyResults: true` on the payrun job.
- See BestPractices-Retro.md for the full retro correction pattern.

---

## 6. UC-05 — {Special Scenario}

### Context
{Describe a common provider-specific scenario, e.g. part-time, bonus payment,
expatriate supplement, or special contract type.}

### Steps

1. {Step 1}
2. {Step 2}

### Expected Results

| WT | Name | Result |
|---:|:---|:---|
| {nr} | {Name} | {expected value / formula} |

---

## 7. Provider Extensibility

Providers can extend `{CC}.{RegulationName}` by adding a regulation layer:

```json
{
  "name": "Employer.MyCompany.{CC}",
  "baseRegulations": ["{CC}.{RegulationName}"],
  "wageTypes": [
    {
      "wageTypeNumber": 9100,
      "name": "{ProviderWT}",
      "valueActions": [ "^^{CC}.{SalaryField} * 0.05" ],
      "collectors": [ "{CC}.GrossIncome" ]
    }
  ]
}
```

For stub WageTypes that are designed to be overridden, see `../Docs/{CC}.{RegulationName}-ProviderStubs.md`.

---

*Version 1.0 · {Month} {YYYY}*
