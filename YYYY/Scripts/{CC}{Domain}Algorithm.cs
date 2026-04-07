using System;

/// <summary>
/// {CC}{Domain}Algorithm — pure statutory calculation logic for {description}.
///
/// Design rules:
///   - No PE scripting API dependencies (no WageTypeValueFunction base class).
///   - All inputs passed as plain C# parameters.
///   - Static methods only — class is stateless.
///   - Covered by xUnit tests in Tests.Unit/{CC}{Domain}AlgorithmTests.cs.
///
/// Referenced from WageTypeValueFunction.{Scope}.Action.cs:
///   return {CC}{Domain}Algorithm.Compute(...);
///
/// Registered in Scripts.json as:
///   { "name": "Algorithm.{CC}{Domain}Algorithm", "functionTypes": ["WageTypeValue"],
///     "valueFile": "../Scripts/{CC}{Domain}Algorithm.cs" }
/// </summary>
internal static class {CC}{Domain}Algorithm
{
    // -----------------------------------------------------------------------
    // Compute — main entry point
    // -----------------------------------------------------------------------
    /// <summary>
    /// Computes {description of what is calculated}.
    /// </summary>
    /// <param name="baseSalary">Monthly base salary.</param>
    /// <param name="rate">Statutory rate (e.g. 0.047 for 4.70%).</param>
    /// <returns>Calculated amount, rounded to 2 decimal places.</returns>
    public static decimal Compute(decimal baseSalary, decimal rate)
    {
        if (baseSalary <= 0m || rate <= 0m)
        {
            return 0m;
        }

        // TODO: implement statutory formula
        // Reference: {Law / Ordinance / Publication}
        return Math.Round(baseSalary * rate, 2);
    }
}
