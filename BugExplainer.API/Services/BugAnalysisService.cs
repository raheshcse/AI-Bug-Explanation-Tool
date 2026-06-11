using BugExplainer.API.DTOs;
using BugExplainer.API.Interfaces;

namespace BugExplainer.API.Services;

public class BugAnalysisService : IBugAnalysisService
{
    private readonly IAiBugAnalyzerService _aiBugAnalyzerService;

    public BugAnalysisService(IAiBugAnalyzerService aiBugAnalyzerService)
    {
        _aiBugAnalyzerService = aiBugAnalyzerService;
    }

    public async Task<BugAnalysisResponseDto> ExplainBugAsync(BugAnalysisRequestDto request)
    {
        return await _aiBugAnalyzerService.AnalyzeBugAsync(request);
    }
}