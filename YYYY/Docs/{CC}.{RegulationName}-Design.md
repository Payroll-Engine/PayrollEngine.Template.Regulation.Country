# {CC}.{RegulationName} — Design

> Version 1.0 · {Month} {YYYY}
> Source: {official source name and URL}
> Repo: `Regulation.{CC}.{RegulationName}`

Architecture and design decisions for `{CC}.{RegulationName}`.
For the WageType matrix and case definitions see `Model.md`;
for action specifications see `Actions.md`.

---

## 1. Overview: {Country} Payroll

{2–4 paragraph summary of the statutory payroll system.}

```
{RegulationName}
  ├── {Pillar 1} — {description}
  │     ├── {Component 1a}
  │     └── {Component 1b}
  └── {Pillar 2} — {description}
```

---

## 2. Regulation Split: Logic vs. Data

### 2.1 Core Principle

Each year the {authority} changes rates and parameters — the calculation logic remains stable.
PE supports this pattern via **regulation splitting**:

```
{CC}.{RegulationName}.{YYYY}.json          Core — logic, cases, WageTypes, scripts, reports
{CC}.{RegulationName}.Data.{Source1}.json  Data — {description}
{CC}.{RegulationName}.Data.{Source2}.json  Data — {description}
```

The regulation name (internal `name` field) is immutable.
Layer configurations always reference the regulation name, not the file name.

### 2.2 What Goes Where

| Parameter | Location | Reason |
|:---|:---|:---|
| {TaxRate}, {SocialRate} | Data lookup | Set annually by {authority}; independent of employer |
| {EmployerConfig} | Company Case | Per employer — varies between companies |
| {EmployeeField} | Employee Case | Per employee — individual values |

### 2.3 Data Sources and Update Cycles

| Data Regulation | Source | Content | Update Cycle |
|:---|:---|:---|:---|
| `{CC}.{RegulationName}.Data.{Source1}` | {Authority} — *{Publication}* | {description} | Annual ({Month}) |
| `{CC}.{RegulationName}.Data.{Source2}` | {Authority} — *{Publication}* | {description} | Annual ({Month}) |

---

## 3. No-Code vs. Low-Code

`{CC}.{RegulationName}` uses {No-Code / Low-Code / mixed} implementation.

| Criterion | No-Code | Low-Code |
|:---|:---:|:---:|
| Simple arithmetic, direct case value | ✓ | |
| Progressive range calculation | | ✓ |
| Multi-parameter lookup, phase-out curve | | ✓ |
| Algorithm-based validation | | ✓ |

For the full action specification see `Actions.md`.

---

## 4. Design Decisions

### 4.1 {Decision Title 1}

**Context:** {Describe the statutory rule or calculation challenge.}

**Decision:** {Describe the chosen implementation approach.}

**Rationale:** {Why this approach over alternatives.}

**Source:** {Statutory reference.}

---

### 4.2 {Decision Title 2}

**Context:** {Describe the issue.}

**Decision:** {Describe the approach.}

**Rationale:** {Why.}

---

## 5. Scope

### Included

| Component | Description |
|:---|:---|
| {Component 1} | {Description} |
| {Component 2} | {Description} |

### Out of Scope

| Component | Reason | Alternative |
|:---|:---|:---|
| {ExcludedComponent 1} | {Reason} | {Provider layer / external system / separate product} |
| {ExcludedComponent 2} | {Reason} | {Alternative} |

For the full uncovered-cases analysis see `UncoveredCases.md`.

---

## 6. Legal Sources

- {Law / Ordinance 1} — {short description}
- {Law / Ordinance 2} — {short description}
- {Official Website} — https://{url}

---

*Version 1.0 · {Month} {YYYY}*
