using System;

#pragma warning disable IDE0130
namespace PayrollEngine.Client.Scripting.Function;
#pragma warning restore IDE0130

/// <summary>
/// WageType custom actions for {CC}.{RegulationName} — {Scope} domain.
///
/// One file per domain (e.g. Gross, Deductions, Employer, Benefits, …).
/// Only actions (methods with [WageTypeValueAction]) belong here.
/// Shared helper methods go in WageTypeValueFunction.Shared.Action.cs.
/// Algorithm classes with pure logic go in {CC}{Domain}Algorithm.cs.
///
/// Naming conventions:
///   - Method name = action name used in valueActions JSON
///   - Parameters must be fully qualified CaseField names (e.g. '{CC}.MonthlySalary')
///     because string literals in action calls are NOT namespace-resolved by PE.
///   - Token references (^^, ^$, ^&, ^#) ARE namespace-resolved automatically.
///
/// Add [ActionParameter] attributes before [WageTypeValueAction].
/// Return type must be ActionValue. Round monetary results with Math.Round(x, 2).
/// </summary>
public partial class WageTypeValueFunction
{
    // -----------------------------------------------------------------------
    // {CC}Calculate{WageTypeName} — replace with actual statutory calculation
    // -----------------------------------------------------------------------
    [ActionParameter("baseSalary", "Fully qualified CaseField name for monthly base salary ({CC}.BaseSalary)")]
    [WageTypeValueAction("{CC}Calculate{WageTypeName}", "{Description of what this action calculates}")]
    public ActionValue {CC}Calculate{WageTypeName}(string baseSalary)
    {
        var salary = GetCaseValue<decimal>(baseSalary);

        // TODO: implement statutory calculation
        // Use {CC}{Domain}Algorithm.Compute(...) for complex logic (keeps it unit-testable).

        return Math.Round(salary, 2);
    }

    // -----------------------------------------------------------------------
    // {CC}CalculateProrata — calendar-day prorata factor
    // -----------------------------------------------------------------------
    [ActionParameter("startDate", "Fully qualified CaseField name for employment start date ({CC}.StartDate)")]
    [ActionParameter("endDate",   "Fully qualified CaseField name for employment end date ({CC}.EndDate). Optional.")]
    [WageTypeValueAction("{CC}CalculateProrata", "Calendar-day prorata factor from start/end dates")]
    public ActionValue {CC}CalculateProrata(string startDate, string endDate = "")
    {
        var periodStart  = PeriodStart.Date;
        var periodEnd    = PeriodEnd.Date;
        var daysInPeriod = (periodEnd - periodStart).Days + 1;

        var ingress  = GetCaseValue<DateTime?>(startDate);
        var egress   = string.IsNullOrEmpty(endDate) ? (DateTime?)null : GetCaseValue<DateTime?>(endDate);

        var from = ingress.HasValue && ingress.Value.Date > periodStart ? ingress.Value.Date : periodStart;
        var to   = egress.HasValue  && egress.Value.Date  < periodEnd   ? egress.Value.Date  : periodEnd;

        if (to < from)
        {
            return 0m;
        }

        var activeDays = (to - from).Days + 1;
        return Math.Round((decimal)activeDays / daysInPeriod, 10);
    }
}
