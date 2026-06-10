import { useState } from "react";
import "./App.css";

function App() {
  const [analysis, setAnalysis] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

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
        throw new Error("API request failed");
      }

      const data = await response.json();
      setAnalysis(data);
    } catch (err) {
      setError("Something went wrong. Check if the backend is running or if CORS is blocked.");
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="app">
      <div className="container">
        <div className="header">
          <h1>AI Bug Explanation Tool</h1>
          <p>Paste an error, stack trace, or code snippet and get a clear fix explanation.</p>
        </div>

        <div className="main-grid">
          <div className="card">
            <form className="bug-form" onSubmit={handleSubmit}>
              <input name="language" type="text" placeholder="Programming Language e.g. JavaScript" />

              <input name="framework" type="text" placeholder="Framework / Tool e.g. React, .NET, SQL" />

              <textarea name="errorMessage" placeholder="Paste error message here"></textarea>

              <textarea name="stackTrace" placeholder="Paste stack trace here optional"></textarea>

              <textarea name="codeSnippet" placeholder="Paste code snippet here"></textarea>

              <button type="submit" disabled={loading}>
                {loading ? "Analysing..." : "Explain Bug"}
              </button>
            </form>
          </div>

          <div className="card result-card">
            <h2>Bug Analysis Result</h2>

            {error && <p className="error-message">{error}</p>}

            {!analysis && !error && (
              <p className="empty-state">
                Your AI bug explanation will appear here after you submit the form.
              </p>
            )}

            {analysis && (
              <>
                <div className="badge">Severity: {analysis.severity}</div>

                <div className="result-section">
                  <h3>Summary</h3>
                  <p>{analysis.summary}</p>
                </div>

                <div className="result-section">
                  <h3>Root Cause</h3>
                  <p>{analysis.rootCause}</p>
                </div>

                <div className="result-section">
                  <h3>Suggested Fix</h3>
                  <p>{analysis.fixSteps}</p>
                </div>

                <div className="result-section">
                  <h3>Corrected Code</h3>
                  <pre>{analysis.correctedCode}</pre>
                </div>

                <div className="result-section">
                  <h3>Prevention Tips</h3>
                  <p>{analysis.preventionTips}</p>
                </div>
              </>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;