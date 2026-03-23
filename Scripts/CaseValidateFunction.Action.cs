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
    // Example: ValidateId — replace with actual statutory ID validation
    // -----------------------------------------------------------------------
    [ActionIssue("InvalidId", "(0) contains an invalid ID: (1)", 2)]
    [ActionParameter("fieldName", "CaseField name of the ID field")]
    [ActionParameter("id", "ID value to validate")]
    [CaseValidateAction("ValidateId", "Validate statutory ID — replace with {CC}-specific algorithm")]
    public bool ValidateId(string fieldName, string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            AddFieldAttributeIssue(fieldName, "InvalidId", fieldName, id ?? string.Empty);
            return false;
        }

        // TODO: implement statutory ID validation (e.g. modulo check)
        // Example: return ValidateModulo11(id);
        return true;
    }
}
