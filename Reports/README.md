# Reports

Place one subfolder per report here. Each report requires:

```
Reports/{ReportName}/
  Report.json               Report definition (name, parameters, queries, template)
  ReportEndFunction.cs      ReportEndScript — data transformation before FastReport
  {ReportName}.frx          FastReport template
  parameters.json           Default parameters for ReportBuild
  Import.pecmd              PayrollImport Report.json
  Script.pecmd              ScriptPublish ReportEndFunction.cs
  Report.Build.pecmd        ReportBuild — generates FRX schema skeleton from live data
  Report.Pdf.pecmd          Report ... /pdf /shellopen
  README.md                 Report documentation (English)
```

## Workflow for a new report

```
1. Create Report.json (templates: [] empty)
   Run Import.pecmd
2. Create ReportEndFunction.cs
   Run Script.pecmd
3. Run Report.Build.pecmd → generates {ReportName}.frx with DataSet schema
4. Design {ReportName}.frx in FastReport Designer
5. Add template block to Report.json → Run Import.pecmd again
6. Run Report.Pdf.pecmd — verify output
7. Write README.md
```

## Key rules

- `Report.json` → `name`: short name without namespace prefix
- `ReportBuild` / `Report` pecmd → `report:`: fully qualified (`{CC}.ReportName`)
- `[ReportEndScript]` → `reportName`: short name — required for import
- Namespace prefix (`{CC}.`) must be stripped in `ReportEndFunction.cs`
  before passing the DataSet to FastReport
