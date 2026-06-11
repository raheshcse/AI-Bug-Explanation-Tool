using BugExplainer.API.DTOs;

namespace BugExplainer.API.Interfaces;

public interface IBugAnalysisService
{
    Task<BugAnalysisResponseDto> ExplainBugAsync(BugAnalysisRequestDto request);
}