# CHANGELOG — {CC}.{RegulationName}

All notable changes to this regulation are documented in this file.

Each entry corresponds to a regulation release (functional or data sub-project).
A new release is only created when the content has changed — unchanged sub-projects
carry no new release.

---

## [{CC}.{RegulationName}.{YYYY}] — {YYYY}-01-01

### Initial release

Full implementation of the {CC} {RegulationName} regulation for payroll year {YYYY}.

**Scope:**

| Component | Description |
|---|---|
| BaseSalary with prorata | Calendar-day prorata via Indienst/Uitdienst (WT 10 + WT 1000) |
| TaxWithheld | Annual projection via TaxParameter lookup |
| NetPay | GrossIncome − Deductions |
| NationalId validation | Checksum validation |

**Cases (Employee):** `{CC}.Salary`, `{CC}.Personal`, `{CC}.Employment`

**Cases (Company):** `{CC}.Configuration`

**Collectors:** `{CC}.GrossIncome`, `{CC}.Deductions`, `{CC}.EmployerCost`

### Data — Initial statutory parameters {YYYY}

| Sub-project | Content | Source | Update Cycle |
|---|---|---|---|
| `{CC}.{RegulationName}.Data.Tax` | Tax brackets and rates | Official statutory source | Annual |
