# Docs — {CC}.{RegulationName}

Regulation-wide, timeless reference documents. These files are **not year-specific**
and apply across all regulation versions.

## Distinction: root `Docs/` vs. `{YYYY}/Docs/`

| Location | Scope | Examples |
|:---|:---|:---|
| `Docs/` (this folder) | Regulation-wide, timeless | Provider stubs, payroll reference |
| `{YYYY}/Docs/` | Year-specific | Analysis, TestSpecification, NoCodeDesign |

## Required files

| File | Purpose |
|:---|:---|
| `{CC}.{RegulationName}-ProviderStubs.md` | Describes all stub and approximation WageTypes — which employer-specific overrides are expected and how to implement them. **Required for every regulation.** |

## Optional files

| File | Purpose |
|:---|:---|
| `{CC}-Payroll-Reference.md` | Country payroll reference: statutory rates, contribution ceilings, filing deadlines, official source links. Useful for providers unfamiliar with the country. |

---

## ProviderStubs — Minimal Structure

`{CC}.{RegulationName}-ProviderStubs.md` must cover:

1. **What Is a Stub** — definition and the two stub patterns (data stub / extension point).
2. **How to Override** — minimal regulation overlay JSON example.
3. **Stub Overview** — table: WT number, name, default value, override trigger.
4. **Stub Details** — one section per stub: when to override, implementation notes, legal source, collector impact.
5. **Approximations** — WTs with working defaults that providers should override for precision.
