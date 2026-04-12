# {TC-Folder-Name} — {Short Title}

**Type:** ET &nbsp;|&nbsp; **WT:** {nr} `{WTName}`

## Purpose

{1–2 sentences. What this TC verifies, and why it matters.
Name any cross-references: e.g. "Pair with WT-TC{nn}-{CC}-{OtherScope} (same salary,
different contract type) for contrast."}

## Scenario

| Parameter | Value |
|:---|:---|
| {Input field 1} | {value} |
| {Input field 2} | {value} |
| Period | {Month YYYY} (full) |

## Expected Results

| WT | Name | Value |
|---:|:---|---:|
| {nn} | **{WTName}** | **{value}** |

> Include all WTs that appear in the payrollResults assertion, not just the primary WT.
> Bold the primary WT being tested.

| Collector | Value |
|:---|---:|
| `{CC}.{GrossCollector}` | {value} |
| `{CC}.{DeductionsCollector}` | {value} |

## Calculation

```
{step-by-step derivation using exact statutory formula}
{e.g. CuotaCC = BaseCotizacion × 4.70% = 3,500 × 4.70% = 164.50}
```

Source: {Authority} — {Article / Publication name / Date}

## Run

```
{TC-Folder-Name}.pecmd
```
