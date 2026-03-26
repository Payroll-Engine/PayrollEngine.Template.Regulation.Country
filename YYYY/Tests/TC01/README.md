# TC01 — Base Salary, Standard Tax

**Setup:** Standard employee, monthly salary 3,000, full period {YYYY}-01

## Expected Results

| WT | Name | Value | Notes |
|---:|---|---:|---|
| 1000 | BaseSalary | 3,000.00 | Replace with actual value |
| 5100 | TaxWithheld | — | Replace with statutory calculation |
| 6500 | NetPay | — | GrossIncome − Deductions |

## Expected Collectors

| Collector | Value |
|---|---:|
| {CC}.GrossIncome | 3,000.00 |
| {CC}.Deductions | — |

## Derivation

```
Gross salary:   3,000.00
Tax:            [formula from {CC} statutory source]
Net salary:     3,000.00 − tax
```

> Replace with official {CC} statutory formula and source reference.

## Run

```
TC01.pecmd
```
