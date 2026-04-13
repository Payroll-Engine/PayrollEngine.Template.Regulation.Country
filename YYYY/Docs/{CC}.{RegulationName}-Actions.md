# {CC}.{RegulationName} — Actions

> Version 1.0 · {Month} {YYYY}
> Source: {official source name and URL}
> Repo: `Regulation.{CC}.{RegulationName}`

No-Code / Low-Code action specification for `{CC}.{RegulationName}`.
For the WageType matrix see `Model.md`; for design decisions see `Design.md`.

---

## 1. Design Principle

{One paragraph: No-Code wrappers call C# Custom Actions. All lookup access,
scaling, and threshold logic is encapsulated in the script.}

```
No-Code Action (Wrapper)             Low-Code Script (Logic)
────────────────────────             ────────────────────────────────────────
{CC}Calculate{Component1}(...)   →   {description: what the script does}
{CC}Calculate{Component2}(...)   →   {description}
{CC}Validate{Field}(...)         →   {description}
```

---

## 2. Custom Actions — Overview

### 2.1 WageType Actions

| Action | WT | Description |
|:---|---:|:---|
| `{CC}GuardMandatoryFields` | 1 | Aborts when {Field1} missing or {Field2} not set; logs warning when {Condition} |
| `{CC}Calculate{Component1}` | {nr} | {Short description} |
| `{CC}Calculate{Component2}` | {nr} | {Short description} — negative; reduces {collector} |
| `{CC}Calculate{EmployerCost}` | {nr} | {Short description}; capped at {ceiling field} |

### 2.2 Case Validate Actions

| Action | Case | Description |
|:---|:---|:---|
| `{CC}Validate{Field}` | `{CC}.{Case}` | {Short description — algorithm or business rule} |

---

## 3. Action Specifications

### 3.1 `{CC}GuardMandatoryFields`

**Purpose:** Abort the payrun when required fields are missing.
**WT:** 1

| Condition | Type | Field |
|:---|:---:|:---|
| `{CC}.{SalaryField}` missing or ≤ 0 | Abort | `{CC}.{SalaryField}` |
| `{CC}.{ContractType}` not set | Abort | `{CC}.{ContractType}` |
| `{CC}.{CompanyField}` not set | Abort | `{CC}.{CompanyField}` |
| `{Condition}` | Warning | — |

**Source:** {Statutory reference.}

---

### 3.2 `{CC}Calculate{Component1}`

**Purpose:** {Description.}
**WT:** {nr}
**Parameters:**

| Parameter | Type | Description |
|:---|:---:|:---|
| `{paramName}` | `string` | CaseField name for {description} |

**Formula:**
```
{annual or monthly formula description}
```

**Lookup:** `{CC}.{LookupName}` key `PeriodStartYear`

```csharp
[WageTypeValueAction("{CC}Calculate{Component1}", "{description}")]
[ActionParameter("{paramName}", "{description}")]
public ActionValue {CC}Calculate{Component1}(string {paramName})
{
    var param = GetLookup<{LookupType}>("{CC}.{LookupName}", PeriodStartYear.ToString());
    if (param == null) { LogWarning($"..."); return 0m; }
    var base = GetCaseValue<decimal>({paramName});
    return Math.Round(base * param.{RateField}, 2);
}
```

**No-Code wrapper:**
```json
"valueActions": [ "{CC}Calculate{Component1}('{CC}.{SalaryField}')" ]
```

**Feeds:** `{CC}.{Collector}` ✓

**Source:** {Statutory reference.}

---

### 3.3 `{CC}Calculate{Component2}`

**Purpose:** {Description.}
**WT:** {nr}

**Formula:**
```
{formula}
```

**Returns:** Negative value (reduces {tax / social contribution}).

**No-Code wrapper:**
```json
"valueActions": [ "{CC}Calculate{Component2}('{CC}.{SalaryField}', '{CC}.{ConfigField}')" ]
```

**Feeds:** `{CC}.{Collector}` ✓

**Source:** {Statutory reference.}

---

### 3.4 `{CC}Validate{Field}`

**Purpose:** {Description — business rule or algorithm.}
**Case:** `{CC}.{Case}`

```csharp
[CaseValidateAction("{CC}Validate{Field}", "{description}")]
[ActionIssue("{CC}Invalid{Field}", "(0) contains an invalid {field}: (1)")]
[ActionParameter("fieldName", "CaseField name of the {field}")]
[ActionParameter("value", "Value to validate")]
public ActionValue {CC}Validate{Field}(string fieldName, string value)
```

**No-Code wrapper:**
```json
"validateActions": [ "? {CC}Validate{Field}('{CC}.{FieldName}', ^:{CC}.{FieldName})" ]
```

---

## 4. WageType Action Configuration Summary

### No-Code: direct expressions

| WT | Name | valueActions |
|---:|:---|:---|
| 1000 | `{BaseSalary}` | `^^{CC}.{SalaryField}` |
| 5130 | `{TaxNet}` | `^$5100 + ^$5110` |
| 6500 | `{NetPay}` | `^&{CC}.GrossIncome - ^&{CC}.Deductions` |

### Custom Action wrappers

| WT | Name | valueActions |
|---:|:---|:---|
| 5100 | `{TaxWT}` | `{CC}Calculate{Tax}('{CC}.{SalaryField}')` |
| 5110 | `{CreditWT}` | `{CC}Calculate{Credit}('{CC}.{SalaryField}', '{CC}.{CreditFlag}')` |
| 6700 | `{EmployerWT1}` | `{CC}Calculate{EmployerCost1}('{CC}.{SalaryField}', '{CC}.{ConfigField}')` |

---

## 5. Lookup Types for `GetLookup<T>`

```csharp
// {CC}.{LookupName}
private sealed class {LookupType}
{
    public decimal {RateField1} { get; set; }
    public decimal {RateField2} { get; set; }
    public decimal {CeilingField} { get; set; }
}
```

---

*Version 1.0 · {Month} {YYYY}*
