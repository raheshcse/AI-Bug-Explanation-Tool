using System.Text;
using System.Text.Json;
using BugExplainer.API.DTOs;
using BugExplainer.API.Interfaces;

namespace BugExplainer.API.Services;

public class OllamaBugAnalyzerService : IAiBugAnalyzerService
{
    private readonly HttpClient _httpClient;

    public OllamaBugAnalyzerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BugAnalysisResponseDto> AnalyzeBugAsync(BugAnalysisRequestDto request)
    {
        var prompt =
$@"You are an expert software debugging assistant.

Analyse the following bug and return ONLY valid JSON.
Do not include markdown.
Do not include explanations outside JSON.

Return JSON using exactly these property names:
{{
  ""severity"": ""Low | Medium | High | Critical"",
  ""summary"": ""short explanation of the bug"",
  ""rootCause"": ""main reason why this bug happened"",
  ""fixSteps"": ""clear step-by-step fix guidance"",
  ""correctedCode"": ""corrected version of the code if possible"",
  ""preventionTips"": ""how to avoid this issue in future""
}}

Language: {request.Language}
Framework: {request.Framework}
Error Message: {request.ErrorMessage}
Stack Trace: {request.StackTrace}
Code Snippet:
{request.CodeSnippet}";

        var requestBody = new
        {
            model = "qwen2.5-coder:1.5b",
            prompt,
            format = "json",
            stream = false
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(
            "http://localhost:11434/api/generate",
            content
        );

        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Ollama API error: {responseContent}");
        }

        using var document = JsonDocument.Parse(responseContent);

        var outputText = document
            .RootElement
            .GetProperty("response")
            .GetString();

        var result = ParseAnalysisResponse(outputText ?? "");

        return result ?? new BugAnalysisResponseDto
        {
            Severity = "Medium",
            Summary = "AI response could not be parsed.",
            RootCause = "The AI returned an unexpected response format.",
            FixSteps = "Try submitting the bug again with more details.",
            CorrectedCode = "",
            PreventionTips = "Provide clear error messages, stack traces, and code snippets."
        };
    }

    private static string ExtractJson(string value)
    {
        var start = value.IndexOf('{');
        var end = value.LastIndexOf('}');

        if (start < 0 || end < start)
        {
            return value;
        }

        return value[start..(end + 1)];
    }

    private static BugAnalysisResponseDto? ParseAnalysisResponse(string value)
    {
        try
        {
            using var document = JsonDocument.Parse(ExtractJson(value));
            var root = document.RootElement;

            return new BugAnalysisResponseDto
            {
                Severity = GetStringValue(root, "severity"),
                Summary = GetStringValue(root, "summary"),
                RootCause = GetStringValue(root, "rootCause"),
                FixSteps = GetStringValue(root, "fixSteps"),
                CorrectedCode = GetStringValue(root, "correctedCode"),
                PreventionTips = GetStringValue(root, "preventionTips")
            };
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static string GetStringValue(JsonElement root, string propertyName)
    {
        if (!root.TryGetProperty(propertyName, out var property))
        {
            return string.Empty;
        }

        if (property.ValueKind == JsonValueKind.String)
        {
            return property.GetString() ?? string.Empty;
        }

        if (property.ValueKind == JsonValueKind.Array)
        {
            return string.Join(
                Environment.NewLine,
                property.EnumerateArray().Select(item => item.ToString()));
        }

        return property.ToString();
    }
}
