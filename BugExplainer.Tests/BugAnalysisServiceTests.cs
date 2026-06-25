using BugExplainer.API.DTOs;
using BugExplainer.API.Interfaces;
using BugExplainer.API.Services;
using Moq;

namespace BugExplainer.Tests;

public class BugAnalysisServiceTests
{
    [Fact]
    public async Task ExplainBugAsync_ShouldReturnAiAnalyzerResponse()
    {
        var request = new BugAnalysisRequestDto
        {
            Language = "C#",
            Framework = ".NET 10",
            ErrorMessage = "Object reference not set to an instance of an object.",
            StackTrace = "System.NullReferenceException",
            CodeSnippet = "string? name = null; Console.WriteLine(name.Length);"
        };

        var expectedResponse = new BugAnalysisResponseDto
        {
            Severity = "High",
            Summary = "Null reference issue",
            RootCause = "The code is accessing Length on a null string.",
            FixSteps = "Check for null before accessing the property.",
            CorrectedCode = "if (name != null) Console.WriteLine(name.Length);",
            PreventionTips = "Use null checks and nullable reference types."
        };

        var mockAiAnalyzer = new Mock<IAiBugAnalyzerService>();

        mockAiAnalyzer
            .Setup(service => service.AnalyzeBugAsync(request))
            .ReturnsAsync(expectedResponse);

        var service = new BugAnalysisService(mockAiAnalyzer.Object);

        var actualResponse = await service.ExplainBugAsync(request);

        Assert.Equal(expectedResponse.Severity, actualResponse.Severity);
        Assert.Equal(expectedResponse.Summary, actualResponse.Summary);
        Assert.Equal(expectedResponse.RootCause, actualResponse.RootCause);
        Assert.Equal(expectedResponse.FixSteps, actualResponse.FixSteps);
        Assert.Equal(expectedResponse.CorrectedCode, actualResponse.CorrectedCode);
        Assert.Equal(expectedResponse.PreventionTips, actualResponse.PreventionTips);

        mockAiAnalyzer.Verify(x => x.AnalyzeBugAsync(request), Times.Once);
    }
}