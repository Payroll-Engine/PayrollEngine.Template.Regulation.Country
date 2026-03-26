# Tests — {CC}.{RegulationName} {YYYY}

## Setup

```
{YYYY}/Setup.pecmd
```

Full setup: deletes existing data, imports regulation + data regulation + test tenant.

## Run All

```
{YYYY}/Test.All.pecmd
```

For CI/CD (no persistence):

```
{YYYY}/Test.Preview.pecmd
```

## Teardown

Removes the test payroll and payrun without deleting the regulation:

```
{YYYY}/Delete.Tests.pecmd
```

---

## Test Cases

| TC | Folder | Focus |
|---|---|---|
| TC01 | [TC01](TC01/) | Base salary, standard tax — baseline verification |

---

## Test Data

| File | Description |
|---|---|
| `{CC}.Test.Setup.json` | Tenant, user, division, employee, payroll, payrun |
| `{CC}.Test.CompanyCases.json` | Company cases: employer tax registration |

---

## Tax Parameters ({YYYY})

| Parameter | Value | Source |
|---|---|---|
| Tax rate (placeholder) | — | Replace with official {CC} statutory source |
| Threshold (placeholder) | — | Replace with official {CC} statutory source |

> Replace placeholder values with official statutory parameters.
> Cite the source publication and date.
