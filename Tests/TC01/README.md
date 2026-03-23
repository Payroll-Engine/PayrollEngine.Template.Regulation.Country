# TC01 - Base Salary, Standard Tax

## Purpose
Verifies that a standard monthly salary is correctly processed through the full
WageType chain: gross salary → tax withholding → net salary.

Update expected results to match your actual statutory calculation.

## Scenario

| Field | Value |
|---|---|
| Employee | test.employee@{cc}.test |
| MonthlySalary | 3,000 |
| ContractType | Permanent |
| Period | January {YYYY} |

## Expected Results

| WageType | Name | Value | Notes |
|---|---|---|---|
| 1000 | MonthlySalary | 3,000.00 | Replace with your values |
| 5100 | TaxWithheld | 175.00 | Replace with statutory calculation |
| 6500 | NetSalary | 2,825.00 | GrossIncome − Deductions |

## Calculation

```
Gross salary:   3,000.00
Tax (30% of (3,000 × 12 − 10,000) / 12):
  Annual: 36,000 − 10,000 = 26,000 × 30% = 7,800 / 12 = 650.00
  (This is a placeholder — replace with official {CC} tax formula)
Net salary:   3,000.00 − 650.00 = 2,350.00
```

> Update the calculation above and the expected values with the actual
> statutory formula from the official {CC} tax authority.

## Run
```
TC01.pecmd
```
