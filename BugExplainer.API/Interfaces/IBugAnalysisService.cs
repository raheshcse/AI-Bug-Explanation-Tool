using BugExplainer.API.DTOs;

namespace BugExplainer.API.Interfaces;

public interface IBugAnalysisService
{
    BugAnalysisResponseDto ExplainBug(BugAnalysisRequestDto request);
}