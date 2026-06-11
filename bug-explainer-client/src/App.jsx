import { useState } from "react";
import "./App.css";

const severityClasses = {
  low: "severity-low",
  medium: "severity-medium",
  high: "severity-high",
  critical: "severity-critical",
};

function getSeverityClass(severity = "") {
  return severityClasses[severity.toLowerCase()] ?? "severity-medium";
}

function formatValue(value) {
  if (value === null || value === undefined || value === "") {
    return "Not provided.";
  }

  if (Array.isArray(value)) {
    return value.join("\n");
  }

  if (typeof value === "object") {
    return JSON.stringify(value, null, 2);
  }

  return String(value);
}

function TextBlock({ children }) {
  return <p className="result-text">{formatValue(children)}</p>;
}

async function readApiError(response) {
  const contentType = response.headers.get("content-type") ?? "";
  const fallbackMessage = `Request failed with HTTP ${response.status}`;

  try {
    if (contentType.includes("application/json")) {
      const body = await response.json();

      return {
        status: response.status,
        statusText: response.statusText,
        message: body?.title || body?.message || body?.error || fallbackMessage,
        details: body,
      };
    }

    const text = await response.text();

    return {
      status: response.status,
      statusText: response.statusText,
      message: text || fallbackMessage,
      details: text,
    };
  } catch (err) {
    console.error("Failed to read API error response", err);

    return {
      status: response.status,
      statusText: response.statusText,
      message: fallbackMessage,
      details: null,
    };
  }
}

function App() {
  const [analysis, setAnalysis] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError("");
    setAnalysis(null);

    const formData = new FormData(e.target);

    const requestBody = {
      language: formData.get("language"),
      framework: formData.get("framework"),
      errorMessage: formData.get("errorMessage"),
      stackTrace: formData.get("stackTrace"),
      codeSnippet: formData.get("codeSnippet"),
    };

    try {
      const response = await fetch("http://localhost:5020/api/bug-analysis/explain", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(requestBody),
      });

      if (!response.ok) {
        const apiError = await readApiError(response);
        console.error("Bug analysis request failed", {
          requestBody,
          response: apiError,
        });
        setError(apiError);
        return;
      }

      const data = await response.json();
      setAnalysis(data);
    } catch (err) {
      const networkError = {
        status: null,
        statusText: "Network error",
        message:
          err instanceof Error
            ? err.message
            : "Unable to reach the backend API. Check that it is running and reachable.",
        details: err instanceof Error ? err.stack : err,
      };

      console.error("Bug analysis request could not be completed", {
        requestBody,
        error: err,
      });
      setError(networkError);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="app">
      <div className="container">
        <div className="header">
          <div>
            <p className="eyebrow">AI Debugging Workspace</p>
            <h1>AI Bug Explanation Tool</h1>
          </div>
          <p>Paste an error, stack trace, or code snippet and get a clear fix explanation.</p>
        </div>

        <div className="main-grid">
          <section className="panel input-panel" aria-labelledby="input-title">
            <div className="panel-header">
              <div>
                <p className="section-label">Bug Details</p>
                <h2 id="input-title">Describe the issue</h2>
              </div>
            </div>

            <form className="bug-form" onSubmit={handleSubmit}>
              <div className="form-row">
                <label>
                  <span>Language</span>
                  <input name="language" type="text" placeholder="C#, JavaScript, Python" />
                </label>

                <label>
                  <span>Framework / Tool</span>
                  <input name="framework" type="text" placeholder="ASP.NET Core, React, SQL" />
                </label>
              </div>

              <label>
                <span>Error Message</span>
                <textarea
                  name="errorMessage"
                  className="textarea-medium"
                  placeholder="Paste the exact error message here"
                ></textarea>
              </label>

              <label>
                <span>Stack Trace</span>
                <textarea
                  name="stackTrace"
                  className="textarea-large"
                  placeholder="Paste stack trace here, if available"
                ></textarea>
              </label>

              <label>
                <span>Code Snippet</span>
                <textarea
                  name="codeSnippet"
                  className="textarea-large code-input"
                  placeholder="Paste the relevant code snippet"
                ></textarea>
              </label>

              <button className="submit-button" type="submit" disabled={loading}>
                {loading && <span className="button-spinner" aria-hidden="true"></span>}
                {loading ? "Analysing..." : "Explain Bug"}
              </button>
            </form>
          </section>

          <section className="panel result-panel" aria-labelledby="result-title">
            <div className="panel-header result-heading">
              <div>
                <p className="section-label">Analysis</p>
                <h2 id="result-title">Bug Analysis Result</h2>
              </div>

              {analysis?.severity && (
                <span className={`severity-badge ${getSeverityClass(analysis.severity)}`}>
                  {analysis.severity}
                </span>
              )}
            </div>

            {loading && (
              <div className="loading-state" role="status">
                <div className="spinner"></div>
                <div>
                  <strong>Analysing bug details</strong>
                  <p>Ollama is reviewing the error, stack trace, and code.</p>
                </div>
              </div>
            )}

            {error && (
              <div className="error-card" role="alert">
                <div className="error-card-header">
                  <span className="error-icon">!</span>
                  <div>
                    <strong>Analysis request failed</strong>
                    <p>
                      {error.status
                        ? `HTTP ${error.status}${error.statusText ? ` ${error.statusText}` : ""}`
                        : error.statusText}
                    </p>
                  </div>
                </div>

                <p className="error-summary">{error.message}</p>

                {import.meta.env.DEV && error.details && (
                  <details className="error-details">
                    <summary>Backend error details</summary>
                    <pre>{formatValue(error.details)}</pre>
                  </details>
                )}
              </div>
            )}

            {!analysis && !error && !loading && (
              <p className="empty-state">
                Your AI bug explanation will appear here after you submit the form.
              </p>
            )}

            {analysis && !loading && (
              <div className="result-content">
                <div className="result-section">
                  <h3>Summary</h3>
                  <TextBlock>{analysis.summary}</TextBlock>
                </div>

                <div className="result-section">
                  <h3>Root Cause</h3>
                  <TextBlock>{analysis.rootCause}</TextBlock>
                </div>

                <div className="result-section">
                  <h3>Suggested Fix</h3>
                  <TextBlock>{analysis.fixSteps}</TextBlock>
                </div>

                <div className="result-section">
                  <h3>Corrected Code</h3>
                  <pre className="code-block">
                    <code>{formatValue(analysis.correctedCode)}</code>
                  </pre>
                </div>

                <div className="result-section">
                  <h3>Prevention Tips</h3>
                  <TextBlock>{analysis.preventionTips}</TextBlock>
                </div>
              </div>
            )}
          </section>
        </div>
      </div>
    </div>
  );
}

export default App;
