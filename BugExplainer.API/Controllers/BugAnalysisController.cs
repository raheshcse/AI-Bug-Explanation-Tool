using BugExplainer.API.DTOs;
using BugExplainer.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BugExplainer.API.Controllers;

[ApiController]
[Route("api/bug-analysis")]
public class BugAnalysisController : ControllerBase
{
    private readonly IBugAnalysisService _bugAnalysisService;

    public BugAnalysisController(IBugAnalysisService bugAnalysisService)
    {
        _bugAnalysisService = bugAnalysisService;
    }

    [HttpPost("explain")]
    public IActionResult ExplainBug([FromBody] BugAnalysisRequestDto request)
    {
        var result = _bugAnalysisService.ExplainBug(request);
        return Ok(result);
    }
}