using System;

#pragma warning disable IDE0130
namespace PayrollEngine.Client.Scripting.Function;
#pragma warning restore IDE0130

/// <summary>
/// Guard actions for {CC}.{RegulationName} — WT 1 (Guard).
///
/// Guard WTs abort payrun execution when mandatory CaseFields or Lookups are
/// missing or inconsistent. This prevents all downstream WTs from silently
/// returning 0 due to unresolved dependencies.
///
/// Pattern:
///   1. Collect all errors into a List&lt;string&gt;.
///   2. If errors.Count > 0: call AbortExecution with a combined message.
///   3. Return 0m (Guard WTs never contribute to any collector).
/// </summary>
public partial class WageTypeValueFunction
{
    // -----------------------------------------------------------------------
    // {CC}GuardMandatoryFields — WT 1
    // Aborts execution when required CaseFields or Lookups are missing.
    // -----------------------------------------------------------------------
    [ActionParameter("baseSalary",   "Fully qualified CaseField: {CC}.BaseSalary")]
    [ActionParameter("contractType", "Fully qualified CaseField: {CC}.ContractType")]
    [ActionParameter("taxParameter", "Lookup name for tax parameters (year-keyed)")]
    [WageTypeValueAction("{CC}GuardMandatoryFields",
        "Guard WT 1: validates mandatory CaseFields and Lookups; AbortExecution on first failure.")]
    public ActionValue {CC}GuardMandatoryFields(
        string baseSalary, string contractType, string taxParameter)
    {
        var errors = new System.Collections.Generic.List<string>();

        // CaseField checks
        if (GetCaseValue<decimal?>(baseSalary) is not > 0m)
        {
            errors.Add($"{baseSalary} missing or zero");
        }

        if (string.IsNullOrEmpty(GetCaseValue<string>(contractType)))
        {
            errors.Add($"{contractType} not set");
        }

        // Lookup checks — keyed by year so a missing data regulation is caught early
        if (GetLookup<object>(taxParameter, PeriodStartYear.ToString()) == null)
        {
            errors.Add($"{taxParameter}[{PeriodStartYear}] missing");
        }

        if (errors.Count > 0)
        {
            AbortExecution($"{{{CC}}} Guard: mandatory fields/lookups missing or invalid for {EmployeeIdentifier}: {string.Join(", ", errors)}");
        }

        return 0m;
    }
}
