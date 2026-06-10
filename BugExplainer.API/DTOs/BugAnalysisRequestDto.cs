namespace BugExplainer.API.DTOs;

public class BugAnalysisRequestDto
{
    public string Language { get; set; } = string.Empty;

    public string Framework { get; set; } = string.Empty;

    public string ErrorMessage { get; set; } = string.Empty;

    public string StackTrace { get; set; } = string.Empty;

    public string CodeSnippet { get; set; } = string.Empty;
}