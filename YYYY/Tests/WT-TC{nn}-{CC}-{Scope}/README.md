# WT-TC{nn}-{CC}-{Scope}

## Purpose

{One sentence: what calculation path or statutory rule this TC verifies.}

Without this test, a regression in {WT name} would silently produce wrong results
for {description of the affected scenario}.

## Scenario

| Parameter | Value |
|:---|:---|
| `{CC}.{BaseSalaryField}` | {Value} |
| `{CC}.ContractType` | {ContractType} |
| `{CC}.TaxClass` | {TaxClass} |
| Period | {Month} {YYYY} |

## Expected Results

| WageType | Name | Expected |
|---:|:---|---:|
| 1000 | BaseSalary | {value} |
| 5100 | TaxWithheld | {value} |
| 6500 | NetPay | {value} |

| Collector | Expected |
|:---|---:|
| `{CC}.GrossIncome` | {value} |
| `{CC}.Deductions` | {value} |

## Calculation

```
{FieldName}:   {value}
{Formula}:     {step-by-step calculation}
{Result}:      {final value}
```

> Source: {Statutory reference — law / ordinance / publication}

## Run

```
WT-TC{nn}-{CC}-{Scope}.pecmd
```
