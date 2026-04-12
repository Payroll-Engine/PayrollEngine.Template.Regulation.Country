# Tests — {CC}.{RegulationName} {YYYY}

## Setup

```pecmd
{YYYY}/Setup.pecmd
```

Full setup: deletes existing data, imports regulation + data regulations + test tenant.

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

```pecmd
{YYYY}/Delete.Tests.pecmd
```

---

## TC Naming Convention

**Guard TCs** use `GUARD-TC{N}` where `N` mirrors the Guard WageType number:

| Guard WT | TC Prefix | Covers |
|:---|:---|:---|
| WT 1 `Guard` | `GUARD-TC1` | Mandatory employee fields |
| WT {n} `Guard{X}` | `GUARD-TC{n}` | {Description} |

Multiple scenarios for the same Guard WT share the same TC number and are distinguished
by their directory suffix. Example: `GUARD-TC1-{CC}-{Field1}Missing` and
`GUARD-TC1-{CC}-{Field2}Missing` both test WT 1.

**WT TCs** use `WT-TC{WageTypeNumber}-{CC}-{ShortDescription}`.

---

## Test Suite Structure — `Test.All.pecmd`

`Test.All.pecmd` runs all TCs in a fixed phase order. The order is NOT alphabetical
or numeric — it reflects **calculation dependencies** and **Company Case state management**.

### Phase Order

| Phase | Scope | WTs | Rationale |
|:---|:---|:---|:---|
| 1 — Guards | Guard WTs | 1–{n} | No dependencies; must run first |
| 2 — Technical | Contribution base / runtime setters | {nn} | All contribution WTs depend on this; no own dependencies |
| 3 — Gross | Gross income | 1000–{nnnn} | No inter-WT dependencies |
| 4 — Deductions | Employee deductions | 5000–{nnnn} | Read contribution base WT; WT {nnn} reads prior-period results |
| 5 — Net | Net pay | 6500 | Reads GrossCollector − DeductionsCollector |
| 6 — Employer | Employer costs | 6600–{nnnn} | Read contribution base WT |
| **Last — Company Cases** | **TCs with non-default Company Cases** | **{relevant WTs}** | **Sets non-zero Company Cases — must run last** |

### Company Case Contamination Rule

`CleanTest` removes the test employee after each TC but **Company Cases persist**
across the entire test run. Any TC that sets a Company Case to a non-default value
**must run in the last phase** — otherwise all subsequent TCs see the modified Company
Case values and produce unexpected results.

See `BestPractices-Testing.md` — *"Company Cases — Shared State in Test.All.pecmd"*.

### {WT nn} Dependency Chain

> Add a dependency chain diagram when a central WT is read by many others.
> Example: BaseCotizacion / ProrataFactor / FiscaalLoon patterns.
> Delete this section if not applicable.

```
WT {nn} ({TechnicalWTName})
  └─ WT {n1} {Name1}    (× {rate1}%)
  └─ WT {n2} {Name2}    (× {rate2}%)
```

---

## Guard Test Cases

| TC | Folder | Type | Trigger | Focus |
|:---|:---|:---:|:---|:---|
| GUARD-TC1 | [GUARD-TC1-{CC}-{Scope}](GUARD-TC1-{CC}-{Scope}/) | ET | `{Field}` not set | AbortExecution: mandatory field missing |
| GUARD-TC{n} | [GUARD-TC{n}-{CC}-{Scope}](GUARD-TC{n}-{CC}-{Scope}/) | CT | {Condition} | CaseInvalid: {validation rule} |

> **ET** = PayrunEmployeeTest &nbsp;|&nbsp; **CT** = CaseTest

---

## WT Test Cases

### {Category 1 — e.g. Technical / Contribution Base}

| TC | Folder | Focus |
|:---|:---|:---|
| WT-TC{nn} | [WT-TC{nn}-{CC}-{Scope}](WT-TC{nn}-{CC}-{Scope}/) | {Description} |

### {Category 2 — e.g. Gross Income}

| TC | Folder | Focus |
|:---|:---|:---|
| WT-TC{nn} | [WT-TC{nn}-{CC}-{Scope}](WT-TC{nn}-{CC}-{Scope}/) | {Description} |

### {Category 3 — e.g. Employee Deductions}

| TC | Folder | Focus |
|:---|:---|:---|
| WT-TC{nn} | [WT-TC{nn}-{CC}-{Scope}](WT-TC{nn}-{CC}-{Scope}/) | {Description} |

### {Category 4 — e.g. Employer Costs}

| TC | Folder | Focus |
|:---|:---|:---|
| WT-TC{nn} | [WT-TC{nn}-{CC}-{Scope}](WT-TC{nn}-{CC}-{Scope}/) | {Description} |

### {Last Phase — Company Case TCs}

| TC | Folder | Focus |
|:---|:---|:---|
| WT-TC{nn} | [WT-TC{nn}-{CC}-{Scope}](WT-TC{nn}-{CC}-{Scope}/) | {Description — sets Company Case} |

---

## Business Test Cases

> Optional section for integration / end-to-end TCs that cover the full payroll
> calculation chain (all WTs in a single payrun). Delete if not used.

| TC | Folder | Key Input | Focus |
|:---|:---|:---|:---|
| WT-TC{nn} | [WT-TC{nn}-{CC}-{Scope}](WT-TC{nn}-{CC}-{Scope}/) | {e.g. BaseSalary EUR 3,000} | {Description} |

---

## Test Data Files

| File | Description |
|:---|:---|
| `{CC}.Test.Setup.json` | Tenant, user, division, employee, payroll, payrun + all regulation layers |
| `{CC}.Test.CompanyCases.json` | Company cases: employer registration, rate overrides, timeline data |

---

## Statutory Parameters ({YYYY})

> These values are the primary reference for TC assertions.
> Always cite the source publication and article.

| Parameter | Value | Source |
|:---|:---|:---|
| {Rate 1} | {value} | {Authority} — *{Publication / Article}* |
| {Ceiling 1} | {value} | {Authority} — *{Publication / Article}* |
| {Threshold 1} | {value} | {Authority} — *{Publication / Article}* |
