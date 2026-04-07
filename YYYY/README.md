# Regulation.{CC}.{RegulationName} {YYYY}

Functional regulation sub-project for year {YYYY}.

## Setup

```pecmd
{YYYY}/Setup.pecmd
```

Full setup: deletes existing data, imports regulation + data regulation + test tenant.

## Run All Tests

```pecmd
{YYYY}/Test.All.pecmd
```

Or from repo root:

```pecmd
Test.{YYYY}.pecmd
```

---

## WageType Test Cases

### Guards

| TC | Folder | Focus |
|:---|:---|:---|
| GUARD-TC1 | [GUARD-TC1-{CC}-{Scope}](Tests/GUARD-TC1-{CC}-{Scope}/) | Abort when mandatory CaseField missing |

### Technical

| TC | Folder | Focus |
|:---|:---|:---|
| WT-TC{nn} | [WT-TC{nn}-{CC}-{Scope}](Tests/WT-TC{nn}-{CC}-{Scope}/) | {Description — e.g. contribution base calculation} |

### Gross

| TC | Folder | Focus |
|:---|:---|:---|
| WT-TC{nn} | [WT-TC{nn}-{CC}-{Scope}](Tests/WT-TC{nn}-{CC}-{Scope}/) | {Description} |

### Deductions / Net

| TC | Folder | Focus |
|:---|:---|:---|
| WT-TC{nn} | [WT-TC{nn}-{CC}-{Scope}](Tests/WT-TC{nn}-{CC}-{Scope}/) | {Description} |

### Employer

| TC | Folder | Focus |
|:---|:---|:---|
| WT-TC{nn} | [WT-TC{nn}-{CC}-{Scope}](Tests/WT-TC{nn}-{CC}-{Scope}/) | {Description} |
