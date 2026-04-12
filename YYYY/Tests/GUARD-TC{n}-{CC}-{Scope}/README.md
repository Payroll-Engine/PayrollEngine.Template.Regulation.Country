# {TC-Folder-Name} — {Short Title}

**Type:** ET/CT &nbsp;|&nbsp; **Guard WT:** {nr} `{ActionName}`

## Purpose

Verifies that `{ActionName}` fires `{AbortExecution | LogWarning | CaseInvalid}`
when {condition — e.g. the mandatory field X is not set / value Y is out of range}.
Without this guard, {consequence — e.g. the salary chain would silently return 0}.

## Setup

| Field | Value | Note |
|:---|:---|:---|
| `{CC}.{TriggerField}` | **(not set / invalid value)** | Provokes Guard failure |
| `{CC}.{OtherField}` | `{value}` | Set — isolates the trigger field |

## Expected Behaviour

1. WT {nr} `{ActionName}` runs
2. `{ConditionCheck}` → {condition met}
3. `{AbortExecution | LogWarning | CaseInvalid}` fires
4. No further WageTypes run (ET only)
5. `wageTypeResults: []`, `collectorResults: []` (ET only)

## Failure Indicator

`wageTypeResult[any].value > 0` → Guard did NOT abort (implementation error)

## Run

```
{TC-Folder-Name}.pecmd
```
