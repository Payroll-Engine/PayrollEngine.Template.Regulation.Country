# {CC}.{RegulationName} — Annual Maintenance

> Version 1.0 · {Month} {YYYY}
> Repo: `Regulation.{CC}.{RegulationName}`

Year-over-year update workflow for `{CC}.{RegulationName}`.
Covers the annual data update cycle, functional regulation updates,
and validation steps before each release.

---

## 1. Annual Release Cycle

| Step | Timing | Source | Deliverable |
|:---|:---|:---|:---|
| {Source1} rates published | {Month} | {Authority} — *{Publication}* | `Data.{Source1}.{YYYY+1}.json` |
| {Source2} rates published | {Month} | {Authority} — *{Publication}* | `Data.{Source2}.{YYYY+1}.json` |
| Functional logic change (if any) | {Month} | {Authority} | `{CC}.{RegulationName}.{YYYY+1}.json` |

A **new functional regulation file** is only required when the calculation logic changes
(new WageType, changed formula, new case field). Rate-only updates never require a new
functional file — only new data regulation files.

---

## 2. Data Regulation Update — Step by Step

### 2.1 {Source1} Update

**Trigger:** {Authority} publishes new {description} for {YYYY+1}.

**Steps:**

1. Create new file `{CC}.{RegulationName}.Data.{Source1}.{YYYY+1}.json` by copying the
   previous year's file.

2. Update `"validFrom"` to `"{YYYY+1}-01-01T00:00:00Z"`.

3. Update lookup values from official source:

   | Lookup | Field | Old ({YYYY}) | New ({YYYY+1}) |
   |:---|:---|---:|---:|
   | `{CC}.{LookupName}` | `{RateField1}` | {value} | **{new value}** |
   | `{CC}.{LookupName}` | `{RateField2}` | {value} | **{new value}** |
   | `{CC}.{LookupName}` | `{CeilingField}` | {value} | **{new value}** |

4. Verify **lookup completeness**: all keys contain the same fields
   (→ BestPractices-CountryRegulation.md Sec. 21).

5. Import and run test cases:
   ```pecmd
   {YYYY+1}/Setup.pecmd
   {YYYY+1}/Test.All.pecmd
   ```

6. Verify expected values match official {authority} {YYYY+1} tables.

### 2.2 {Source2} Update

**Trigger:** {Authority} publishes new {description}.

**Steps:** Same as 2.1 for `Data.{Source2}.{YYYY+1}.json`.

---

## 3. Functional Regulation Update

A functional update is required when:

- New statutory rule introduced (new WageType or Case)
- Existing formula logic changed (not just rate values)
- New mandatory CaseField added

**Steps:**

1. Create `{YYYY+1}/` sub-project by copying `{YYYY}/`.

2. Update in `{YYYY+1}/Directory.Build.props`:
   ```xml
   <Version>{YYYY+1}.1-beta.dev</Version>
   ```

3. Update `"validFrom"` in all regulation JSON files.

4. Implement changes (WageType logic, new cases, scripts).

5. Add new test cases for changed WageTypes.

6. Run full test suite:
   ```pecmd
   {YYYY+1}/Test.All.pecmd
   ```

7. Update `CHANGELOG.md` with new release entry.

---

## 4. Intra-Year Update

If the {authority} publishes a mid-year change to rates or parameters:

1. Create a new data file with the mid-year `validFrom` date:
   ```
   {CC}.{RegulationName}.Data.{Source1}.{YYYY}b.json   validFrom: {YYYY}-07-01
   ```

2. Import via `Setup.Data.pecmd`.

3. Verify retro payruns use the correct version for each period.

---

## 5. Release Checklist

Before each release (functional or data):

- [ ] All lookup values updated from official source — document source and publication date
- [ ] Lookup completeness verified — all keys have the same fields
- [ ] Test cases updated with {YYYY+1} parameters and expected values
- [ ] All TCs pass: `{YYYY+1}/Test.All.pecmd`
- [ ] Working titles removed from all production files (→ BestPractices Sec. 22)
- [ ] `CHANGELOG.md` entry added
- [ ] Version in `Directory.Build.props` updated (`.dev` suffix removed for release)

---

## 6. Rate History

| Parameter | {YYYY-1} | {YYYY} | {YYYY+1} |
|:---|---:|---:|---:|
| {RateField1} | {value} | {value} | {value} |
| {RateField2} | {value} | {value} | {value} |
| {CeilingField} | {value} | {value} | {value} |

Source: {Authority — publication name}

---

*Version 1.0 · {Month} {YYYY}*
