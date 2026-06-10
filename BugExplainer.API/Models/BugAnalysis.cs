namespace BugExplainer.API.Models;

public class BugAnalysis
{
    public int Id { get; set; }

    public string Language { get; set; } = string.Empty;

    public string Framework { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public string StackTrace { get; set; } = string.Empty;

    public string CodeSnippet { get; set; } = string.Empty;

    public string Severity { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public string RootCause { get; set; } = string.Empty;

    public string FixSteps { get; set; } = string.Empty;

    public string CorrectedCode { get; set; } = string.Empty;

    public string PreventionTips { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}