namespace BugExplainer.API.DTOs;

public class BugAnalysisResponseDto
{
    public string Severity { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public string RootCause { get; set; } = string.Empty;

    public string FixSteps { get; set; } = string.Empty;

    public string CorrectedCode { get; set; } = string.Empty;

    public string PreventionTips { get; set; } = string.Empty;
}