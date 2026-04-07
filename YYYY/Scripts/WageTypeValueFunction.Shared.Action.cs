using System;
using System.Collections.Generic;

#pragma warning disable IDE0130
namespace PayrollEngine.Client.Scripting.Function;
#pragma warning restore IDE0130

/// <summary>
/// Shared helper methods for {CC}.{RegulationName} WageType actions.
///
/// This file contains ONLY private helpers — no [WageTypeValueAction] methods.
/// It is compiled alongside all WageTypeValueFunction.*.Action.cs files via
/// the partial class pattern, making every helper available to all action files.
///
/// Naming: {CC}{HelperName} to avoid collisions with other partial classes.
/// </summary>
public partial class WageTypeValueFunction
{
    // -----------------------------------------------------------------------
    // {CC}GetLookupParam&lt;T&gt; — year-keyed lookup with LogWarning on miss
    // -----------------------------------------------------------------------
    private T {CC}GetLookupParam<T>(string lookupName) where T : class
    {
        var param = GetLookup<T>(lookupName, PeriodStartYear.ToString());
        if (param == null)
        {
            LogWarning($"{{{CC}}}: {lookupName}[{PeriodStartYear}] not found — check Data regulation import");
        }

        return param;
    }

    // -----------------------------------------------------------------------
    // {CC}ComputeCalendarProrata — active days / calendar days
    // -----------------------------------------------------------------------
    private decimal {CC}ComputeCalendarProrata(DateTime? startDate, DateTime? endDate)
    {
        var periodStart  = PeriodStart.Date;
        var periodEnd    = PeriodEnd.Date;
        var daysInPeriod = (periodEnd - periodStart).Days + 1;

        var from = startDate.HasValue && startDate.Value.Date > periodStart ? startDate.Value.Date : periodStart;
        var to   = endDate.HasValue   && endDate.Value.Date   < periodEnd   ? endDate.Value.Date   : periodEnd;

        if (to < from || daysInPeriod <= 0)
        {
            return 0m;
        }

        return Math.Round((decimal)((to - from).Days + 1) / daysInPeriod, 10);
    }
}
