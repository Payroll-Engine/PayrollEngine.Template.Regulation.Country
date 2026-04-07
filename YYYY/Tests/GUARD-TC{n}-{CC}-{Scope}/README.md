# GUARD-TC{n}-{CC}-{Scope}

## Purpose

Tests `{CC}GuardMandatoryFields` (WT 1): fires `AbortExecution` when
{description of the missing or inconsistent field that triggers the guard}.

Without the guard, a missing {field/lookup} causes all dependent WTs to silently
return `0`, producing a payrun with incorrect results and no visible error.

## Setup

| Field | Value | Note |
|:---|:---|:---|
| `{CC}.{Field1}` | {value} | correctly set |
| `{CC}.{Field2}` | {value} | correctly set |

**Trigger:** {Exact condition that fires the guard — e.g. `{CC}.{RequiredField}` not set,
or `periodStart = {YYYY}-01-01` with no lookup for that year.}

## Expected Behaviour

1. WT 1 `{CC}GuardMandatoryFields` runs
2. All other CaseField checks pass
3. {Trigger condition} detected
4. `AbortExecution` fires with message: `"{CC} Guard: {field/condition} missing for test.employee@{cc}.test: {detail}"`
5. No WTs run → `wageTypeResults: []`, `collectorResults: []`

## Run

```
GUARD-TC{n}-{CC}-{Scope}.pecmd
```
