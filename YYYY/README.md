# {CC}.{RegulationName} {YYYY}

Functional regulation sub-project for year {YYYY}.
For the full scope, WT/Collector Matrix, Cases, Legal Sources, and Known Limitations
see the [repo root README](../README.md).

---

## What's New in {YYYY}

> Replace with the concrete changes that took effect on 1 January {YYYY}.
> Remove this section entirely for the initial year (no predecessor).

| Area | Change |
|:---|:---|
| {Area 1} | {Change description — cite the legal source} |
| {Area 2} | {Change description} |

---

## Scope ({YYYY})

| Component | Status | Notes |
|:---|:---:|:---|
| {Component 1} | ✅ | |
| {Component 2} | ✅ | {Notes on specifics or limitations} |
| {Component 3 — planned} | 🔜 | {Planned for next release / phase} |
| {Component 4 — out of scope} | ❌ | {Reason} |

---

## Sub-project Layout

```
{YYYY}/
  Docs/
    {CC}.{RegulationName}-Analysis.md
    {CC}.{RegulationName}-UncoveredCases.md
    {CC}.{RegulationName}-UpdateWorkflow.md
  Regulation/
    {CC}.{RegulationName}.{YYYY}.json
    {CC}.{RegulationName}.Scripts.{YYYY}.json
    {CC}.{RegulationName}.Collectors.{YYYY}.json
    {CC}.{RegulationName}.Cases.Company.{YYYY}.json
    {CC}.{RegulationName}.Cases.Employee.Core.{YYYY}.json
    {CC}.{RegulationName}.WageTypes.Guard.{YYYY}.json
    {CC}.{RegulationName}.WageTypes.Gross.{YYYY}.json
    {CC}.{RegulationName}.WageTypes.Deductions.{YYYY}.json
    {CC}.{RegulationName}.WageTypes.Employer.{YYYY}.json
  Scripts/
    WageTypeValueFunction.Shared.Action.cs
    WageTypeValueFunction.Guard.Action.cs
    WageTypeValueFunction.{Scope}.Action.cs    (one file per domain)
    CaseValidateFunction.Action.cs
    {CC}{Domain}Algorithm.cs                   (unit-testable algorithm classes)
  Reports/
    {ReportName}/
  Tests/
    README.md
    {CC}.Test.Setup.json
    {CC}.Test.CompanyCases.json
    GUARD-TC{n}-{CC}-{Scope}/
    WT-TC{nn}-{CC}-{Scope}/
  Tests.Unit/
    {CC}{Domain}AlgorithmTests.cs
  Schemas/
    PayrollEngine.Exchange.schema.json
  Setup.pecmd
  Test.All.pecmd
  Delete.pecmd
```

---

## Setup

```
{YYYY}/Setup.pecmd
```

Full setup: deletes existing data, imports regulation + data regulations + test tenant.

## Testing

```
{YYYY}/Test.All.pecmd
```

See [Tests/README.md](Tests/README.md) for the full TC catalogue, execution order,
and statutory parameters used in assertions.

## Reports

```
{YYYY}/Reports/Setup.Reports.pecmd
```

See the report-specific README in each report subfolder.

---

## Design Notes

> Add architecture notes that are specific to this year's implementation.
> Use this section for non-obvious design decisions, known approximations, or
> edge cases that differ from the general description in the root README.
> Delete this section if there is nothing year-specific to document.

### {Design Topic 1}

{Explanation of the design decision, the statutory basis, and any known limitations.}

---

## Key Parameters ({YYYY})

> These values are the primary reference for TC assertions.
> Always cite the source publication and article.

| Parameter | Value | Source |
|:---|---:|:---|
| {Rate 1} | {value} | {Authority} — {Publication / Article} |
| {Ceiling 1} | {value} | {Authority} — {Publication / Article} |
| {Threshold 1} | {value} | {Authority} — {Publication / Article} |

---

## See Also

- [Repo root README](../README.md)
- [Tests/README.md](Tests/README.md)
- [Docs/{CC}.{RegulationName}-UncoveredCases.md](Docs/{CC}.{RegulationName}-UncoveredCases.md)
- [Docs/{CC}.{RegulationName}-Analysis.md](Docs/{CC}.{RegulationName}-Analysis.md)
