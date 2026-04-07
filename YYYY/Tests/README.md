# Tests — {CC}.{RegulationName} {YYYY}

## Setup

```pecmd
{YYYY}/Setup.pecmd
```

Full setup: deletes existing data, imports regulation + data regulation + test tenant.

## Run All

```pecmd
{YYYY}/Test.All.pecmd
```

> **Prerequisite:** Run `Setup.pecmd` before each test run — `Test.All.pecmd` does not
> perform cleanup between individual TCs.

For CI/CD (no persistence):

```pecmd
{YYYY}/Test.Preview.pecmd
```

## Teardown

Removes the test payroll and payrun without deleting the regulation:

```pecmd
{YYYY}/Delete.Tests.pecmd
```

---

## Test Suite Structure — `Test.All.pecmd`

`Test.All.pecmd` runs all TCs in a fixed phase order. The order is NOT alphabetical
or numeric — it reflects **calculation dependencies** and **Company Case state management**.

### Phase Order

| Phase | Scope | WTs | Rationale |
|:---|:---|:---|:---|
| 1 — Guards | Guards | 1–{n} | No dependencies; must run first |
| 2 — Technical | Contribution base | {nn} | All contribution WTs depend on this; no own dependencies |
| 3 — Gross | Gross income | 1000–{nnnn} | No inter-WT dependencies |
| 4 — Deductions | Employee deductions | 5000–{nnnn} | All read WT {nn} (contribution base) |
| 5 — Net | Net pay | 6500 | Reads GrossCollector − DeductionsCollector |
| 6 — Employer | Employer costs | 6600–{nnnn} | All read WT {nn} (contribution base) |
| **Last — ConvenioConfig** | **Company Cases** | **{relevant WTs}** | **Sets non-zero Company Cases — must run last** |

### Company Case Contamination Rule

`CleanTest` removes the test employee after each TC but **Company Cases persist**
across the entire test run. Any TC that sets a Company Case to a non-default value
**must run in the last phase** — otherwise all subsequent TCs see the modified
Company Case values and produce unexpected results.

See `BestPractices-Testing.md` — *"Company Cases — Shared State in Test.All.pecmd"*.

---

## Business Test Cases

| TC | Folder | Key Parameter | Focus |
|:---|:---|:---|:---|
| WT-TC{nn} | [WT-TC{nn}-{CC}-{Scope}](WT-TC{nn}-{CC}-{Scope}/) | {e.g. BaseSalary €3.000} | {Description — e.g. Base scenario, full period} |

---

## Guard Test Cases

| TC | Folder | Trigger | Focus |
|:---|:---|:---|:---|
| GUARD-TC{n} | [GUARD-TC{n}-{CC}-{Scope}](GUARD-TC{n}-{CC}-{Scope}/) | {missing field/lookup} | AbortExecution on {condition} |

---

## Test Data

| File | Description |
|:---|:---|
| `{CC}.Test.Setup.json` | Tenant, user, division, employee, payroll, payrun |
| `{CC}.Test.CompanyCases.json` | Company cases: employer registration, rate overrides |

---

## Statutory Parameters ({YYYY})

| Parameter | Value | Source |
|:---|:---|:---|
| {Rate name} | {value} | {Authority} — *{Publication name / Article}* |
| {Ceiling name} | {value} | {Authority} — *{Publication name / Article}* |
| {Threshold name} | {value} | {Authority} — *{Publication name / Article}* |

> Replace placeholder values with official statutory parameters.
> Always cite the source publication, article, and effective date.
