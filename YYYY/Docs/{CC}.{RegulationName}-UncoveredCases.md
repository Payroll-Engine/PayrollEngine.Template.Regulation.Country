# {CC}.{RegulationName} — Uncovered Cases

> Version 1.0 · {Month} {YYYY}
> Source: {official source name and URL}
> Repo: `Regulation.{CC}.{RegulationName}`

Payroll scenarios not covered by `{CC}.{RegulationName}`, classified by tier.

---

## Classification

| Tier | Meaning |
|:---|:---|
| **A — Generic regulation** | Can and should be implemented in `{CC}.{RegulationName}` core |
| **B — Employer-specific layer** | Belongs in a provider or employer regulation; structural blocker for generic implementation |
| **C — Out of scope** | Not a payroll calculation responsibility; handled externally |

---

## Tier A — Implementable in Generic Regulation

### A1 — {Feature Name}

| | |
|:---|:---|
| **Legal basis** | {Law, article} |
| **Status** | {Planned vX.X / In progress / Deferred} |
| **Priority** | {High / Medium / Low} |
| **Estimated effort** | {X days} |

**What it is:** {Description.}

**Why not yet implemented:** {Reason — complexity, lower priority, pending clarification.}

**Recommended approach:**
- {Implementation step 1}
- {Implementation step 2}

---

### A2 — {Feature Name}

| | |
|:---|:---|
| **Legal basis** | {Law, article} |
| **Status** | {Planned / Deferred} |
| **Priority** | {Priority} |

**What it is:** {Description.}

---

## Tier B — Employer-Specific Layer Only

### B1 — {Feature Name}

| | |
|:---|:---|
| **Legal basis** | {Law, article, or CAO} |
| **Blocker** | {Why generic implementation is not possible} |

**What it is:** {Description.}

**Why not generic:** {Structural reason — varies per employer, sector, or contract.}

**Recommended approach:** Provider regulation layer with {lookup / Case / WT pattern}.

---

### B2 — {Feature Name}

| | |
|:---|:---|
| **Legal basis** | {Reference} |
| **Blocker** | {Reason} |

**What it is:** {Description.}

---

## Tier C — Out of Scope

### C1 — {Feature Name}

| | |
|:---|:---|
| **What it is** | {Description} |
| **Why out of scope** | {Reason — not a payroll calculation responsibility} |

---

## Known Approximations

Approximations already in the regulation where a more exact calculation is possible but not
implemented generically:

| Item | Location | Impact | Note |
|:---|:---|:---:|:---|
| {Approximation 1} | WT {nr} | {Low / Minor / Minimal} | {Description — what is approximated and why} |

---

## Summary Table

| # | Item | Tier | Priority | Status |
|:--|:---|:---:|:---:|:---:|
| A1 | {Feature Name} | A | {High} | {Planned / Deferred} |
| A2 | {Feature Name} | A | {Medium} | {Planned} |
| B1 | {Feature Name} | B | — | — |
| C1 | {Feature Name} | C | — | — |

---

*Version 1.0 · {Month} {YYYY}*
