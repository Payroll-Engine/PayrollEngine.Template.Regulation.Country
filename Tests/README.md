# Tests — {CC}.{RegulationName}

## Setup

```
Setup.Tests.pecmd
```

## Run All

```
Test.All.pecmd
```

For CI/CD (no persistence):

```
Test.Preview.pecmd
```

## Test Cases

| TC | Folder | Focus |
|---|---|---|
| TC01 | [TC01](TC01/) | Base salary, standard tax — update with statutory values |

## Test Data

| File | Description |
|---|---|
| `{CC}.Test.Setup.json` | Tenant, user, division, employee, payroll, payrun |
| `{CC}.Test.CompanyCases.json` | Company cases: employer tax registration |

## Tax Parameters ({YYYY})

| Parameter | Value | Source |
|---|---|---|
| Tax rate (placeholder) | 30% | Replace with official {CC} source |
| Threshold (placeholder) | 10,000 | Replace with official {CC} source |

> Replace placeholder values with official statutory parameters from the
> relevant {CC} tax authority. Cite the source and publication date.
