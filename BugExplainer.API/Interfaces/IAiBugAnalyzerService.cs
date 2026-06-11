using BugExplainer.API.DTOs;

namespace BugExplainer.API.Interfaces;

public interface IAiBugAnalyzerService
{
    Task<BugAnalysisResponseDto> AnalyzeBugAsync(BugAnalysisRequestDto request);
}