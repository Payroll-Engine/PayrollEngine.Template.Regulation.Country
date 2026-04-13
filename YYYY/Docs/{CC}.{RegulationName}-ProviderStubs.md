# {CC}.{RegulationName} — Provider Stubs

> Version 1.0 · {Month} {YYYY}

---

## What Is a Stub?

A **stub** is a WageType in the base regulation that returns `0` (or a safe default)
by design. It signals to providers and employers: *"this business rule is
yours to implement — we give you the correct number, collector, and execution
position; you supply the logic."*

`{CC}.{RegulationName}` uses stubs for:

1. **Employer-specific rules** — calculations that depend on company agreements
   (pension scheme, CAO, HR policy) that cannot be standardized in a national base regulation.
2. **Approximations** — working implementations that cover the common case, but
   a more exact calculation requires employer data.

---

## How to Override a Stub

PE supports **regulation layering**: a provider or employer regulation can override
any WageType from the base regulation without modifying it.

**Minimal override structure:**

```json
{
  "regulations": [
    {
      "name": "Employer.MyCompany.{CC}",
      "baseRegulations": [
        "{CC}.{RegulationName}",
        "{CC}.{RegulationName}.Data.{Source1}.{YYYY}"
      ],
      "cases": [...],
      "wageTypes": [
        {
          "wageTypeNumber": {nr},
          "name": "{StubWageTypeName}",
          "valueExpression": "{expression or valueActions reference}"
        }
      ]
    }
  ]
}
```

---

## Stub Overview

| WT | Name | Default | Override required? | Notes |
|:---:|:---|:---:|:---:|:---|
| {nr} | {StubName1} | 0 | Optional | {Short description} |
| {nr} | {StubName2} | 0 | **Required for {scenario}** | {Short description} |

**Approximations:**

| WT | Name | Approximation | Override for |
|:---:|:---|:---|:---|
| {nr} | {ApproxName} | {What the base regulation does} | {When to override for precision} |

---

## Stub Details

### WT {nr} — {StubName1}

**Default:** `0` (no `valueActions`).

**Override when:** {Describe the business scenario that requires implementation.}

**Implementation notes:**
- {Step 1}
- {Step 2}

**Feeds:** `{CC}.{Collector}` ✓

**Source:** {Statutory reference — law, article, authority publication.}

---

### WT {nr} — {StubName2} ⚠️ {e.g. FiscaalLoon adjustment required}

**Default:** `0` (no `valueActions`).

**Override required when:** {Describe when this stub must be implemented.}

**Implementation steps:**
1. {Step 1}
2. {Step 2}

**Feeds:** `{CC}.{Collector}` ✓

**Source:** {Statutory reference.}

---

## Approximation Details

### WT {nr} — {ApproxName}

**Base approximation:** {What the regulation does by default.}

**Limitation:** {What the approximation misses or where it diverges from exact statutory calculation.}

**Override when:** {Describe when the employer needs an exact calculation.}

---

*Version 1.0 · {Month} {YYYY}*
