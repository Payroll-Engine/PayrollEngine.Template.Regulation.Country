# {CC}.{RegulationName} — Compliance

> Version 1.0 · {Month} {YYYY}
> Repo: `Regulation.{CC}.{RegulationName}`

Compliance scope, certification status, and statutory filing obligations
for `{CC}.{RegulationName}`.

---

## 1. Certification

| Authority | Certification | Status | Notes |
|:---|:---|:---:|:---|
| {Authority 1} | {Certification name or "None required"} | {✅ / ❌ / N/A} | {Description} |
| {Authority 2} | {Certification name} | {✅ / N/A} | {Description} |

> **{CC} principle:** {One sentence on whether software certification is required in this country.
> Example: "No software certification is required — {format} is a technical format specification,
> not an accreditation process."}

---

## 2. Filing Obligations

| Report | Recipient | Frequency | Format | PE Coverage |
|:---|:---|:---:|:---:|:---:|
| {FilingName1} | {Authority} | {Monthly / Quarterly / Annual} | {Format} | {✅ R01 / ⏳ planned / ❌ out of scope} |
| {FilingName2} | {Authority} | {Frequency} | {Format} | {Status} |

### {FilingName1} — Details

**What it is:** {Description — purpose and content of the filing.}

**Deadline:** {When it must be submitted.}

**PE status:** {What the regulation delivers (calculated amounts) and what is the operator's
responsibility (actual submission to authority).}

---

## 3. Statutory Scope

### Included in `{CC}.{RegulationName}`

| Area | Statutory basis | Coverage |
|:---|:---|:---:|
| {Area 1} | {Law, article} | ✅ Full |
| {Area 2} | {Law, article} | ✅ Full |
| {Area 3} | {Law, article} | ⚠️ Partial — {reason} |

### Explicitly Out of Scope

| Area | Reason |
|:---|:---|
| {Area A} | {Why — employer-specific / handled externally / separate product} |
| {Area B} | {Reason} |

For the full uncovered-cases analysis see `{CC}.{RegulationName}-UncoveredCases.md`.

---

## 4. Data Retention

| Data type | Retention period | Basis |
|:---|:---:|:---|
| Payroll records | {X years} | {Law, article} |
| {Other data type} | {Period} | {Basis} |

---

## 5. Compliance Checklist (Annual)

- [ ] All statutory rates updated for {YYYY} — verified against official {authority} publications
- [ ] Filing format validated against current {format} schema version
- [ ] New statutory requirements from {authority} assessed — see `Maintenance.md`
- [ ] UncoveredCases.md reviewed for items reaching legal effective date

---

*Version 1.0 · {Month} {YYYY}*
