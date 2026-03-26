using System;

#pragma warning disable IDE0130
namespace PayrollEngine.Client.Scripting.Function;
#pragma warning restore IDE0130

/// <summary>
/// Case validate custom actions for {CC}.{RegulationName}.
///
/// [ActionIssue] defines the error reported when validation fails.
/// [ActionParameter] must precede [CaseValidateAction].
/// Use GetValue&lt;T&gt;(fieldName) — short name, PE resolves namespace automatically.
/// </summary>
public partial class CaseValidateFunction
{
    // -----------------------------------------------------------------------
    // {CC}ValidateNationalId — replace with actual statutory ID validation
    // -----------------------------------------------------------------------
    [ActionIssue("InvalidNationalId", "(0) contains an invalid national ID: (1)", 2)]
    [ActionParameter("fieldName", "CaseField name of the national ID field")]
    [ActionParameter("id", "ID value to validate")]
    [CaseValidateAction("{CC}ValidateNationalId", "Validate national ID — replace with {CC}-specific algorithm")]
    public bool {CC}ValidateNationalId(string fieldName, string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            AddFieldAttributeIssue(fieldName, "InvalidNationalId", fieldName, id ?? string.Empty);
            return false;
        }

        // TODO: implement statutory ID validation (e.g. modulo check)
        // Example NL Elfproef: ValidateModulo11(id)
        // Example BE Rijksregister: ValidateModulo97(id)
        return true;
    }
}
