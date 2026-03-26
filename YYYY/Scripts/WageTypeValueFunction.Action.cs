using System;

#pragma warning disable IDE0130
namespace PayrollEngine.Client.Scripting.Function;
#pragma warning restore IDE0130

/// <summary>
/// WageType custom actions for {CC}.{RegulationName}.
///
/// Naming convention:
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
    // Example: parameter struct for tax lookup
    // -----------------------------------------------------------------------
    private sealed class TaxParam
    {
        public decimal Rate { get; set; }
        public decimal Threshold { get; set; }
    }

    // -----------------------------------------------------------------------
    // {CC}CalculateTax — replace with actual statutory tax calculation
    // -----------------------------------------------------------------------
    [ActionParameter("monthlySalary", "Fully qualified CaseField name for monthly gross salary")]
    [WageTypeValueAction("{CC}CalculateTax", "Income tax — replace with {CC} statutory calculation")]
    public ActionValue {CC}CalculateTax(string monthlySalary)
    {
        var param = GetLookup<TaxParam>("TaxParameter", PeriodStartYear.ToString());
        if (param == null)
        {
            LogWarning($"{CC}CalculateTax: TaxParameter not found for year {PeriodStartYear}");
            return 0m;
        }

        var annualSalary = GetCaseValue<decimal>(monthlySalary) * PeriodsInCycle;
        var taxableIncome = Math.Max(0m, annualSalary - param.Threshold);
        var annualTax = taxableIncome * param.Rate;

        return Math.Round(annualTax / PeriodsInCycle, 2);
    }

    // -----------------------------------------------------------------------
    // {CC}CalculateProrata — calendar-day prorata factor
    // -----------------------------------------------------------------------
    [WageTypeValueAction("{CC}CalculateProrata", "Calendar-day prorata factor from Indienst/Uitdienst dates")]
    public ActionValue {CC}CalculateProrata()
    {
        var indienst = GetCaseValue<DateTime?>("{CC}.Indienst");
        var uitdienst = GetCaseValue<DateTime?>("{CC}.Uitdienst");

        var periodStart = PeriodStart.Date;
        var periodEnd = PeriodEnd.Date;
        var daysInPeriod = (periodEnd - periodStart).Days + 1;

        var from = indienst.HasValue && indienst.Value > periodStart ? indienst.Value.Date : periodStart;
        var to = uitdienst.HasValue && uitdienst.Value < periodEnd ? uitdienst.Value.Date : periodEnd;

        if (to < from)
        {
            return 0m;
        }

        var activeDays = (to - from).Days + 1;
        return Math.Round((decimal)activeDays / daysInPeriod, 10);
    }
}
