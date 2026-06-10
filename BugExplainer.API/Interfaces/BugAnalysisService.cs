using BugExplainer.API.DTOs;
using BugExplainer.API.Interfaces;

namespace BugExplainer.API.Services;

public class BugAnalysisService : IBugAnalysisService
{
    public BugAnalysisResponseDto ExplainBug(BugAnalysisRequestDto request)
    {
        return new BugAnalysisResponseDto
        {
            Severity = "Medium",
            Summary = $"The error in your {request.Language} code needs investigation.",
            RootCause = "The application may be trying to access invalid, missing, or incorrectly structured data.",
            FixSteps = "Check the error message, validate the input data, and inspect the line mentioned in the stack trace.",
            CorrectedCode = "// AI-generated corrected code will appear here",
            PreventionTips = "Use validation, null checks, error handling, and test edge cases before deployment."
        };
    }
}