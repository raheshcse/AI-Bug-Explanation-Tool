# 🐞 AI Bug Explanation Tool

An AI-powered debugging assistant that helps developers understand, troubleshoot, and resolve software errors faster using a locally hosted Large Language Model (LLM).

Built with **React**, **ASP.NET Core Web API**, and **Ollama**, the platform analyzes error messages, stack traces, and code snippets to provide intelligent bug explanations, root cause analysis, suggested fixes, corrected code, and prevention recommendations.

---

## 🚀 Overview

Debugging software issues can be one of the most time-consuming parts of software development.

The AI Bug Explanation Tool simplifies this process by allowing developers to submit:

* Error Messages
* Stack Traces
* Code Snippets
* Programming Language Information
* Framework Details

The AI engine then analyzes the issue and returns structured troubleshooting guidance.

---

## ✨ Features

### Intelligent Bug Analysis

Analyze:

* Runtime Exceptions
* Null Reference Errors
* API Failures
* SQL Errors
* Frontend Errors
* Backend Exceptions
* Validation Issues
* Configuration Problems

### AI-Generated Insights

Receive:

* Severity Assessment
* Summary
* Root Cause Analysis
* Suggested Fixes
* Corrected Code Examples
* Prevention Tips

### Multi-Language Support

Supports debugging scenarios for:

* C#
* JavaScript
* TypeScript
* Python
* SQL
* Java
* React
* ASP.NET Core
* REST APIs

### Local AI Processing

Powered by:

* Ollama
* Qwen2.5-Coder

No external AI API subscriptions required.

---

## 🏗️ Architecture

```text
React Frontend
       │
       ▼
ASP.NET Core Web API
       │
       ▼
Service Layer
       │
       ▼
Ollama Local LLM
       │
       ▼
AI Bug Analysis
       │
       ▼
Structured Response
```

---

## 🛠️ Technology Stack

### Frontend

* React
* Vite
* JavaScript
* HTML5
* CSS3

### Backend

* ASP.NET Core Web API
* C#
* Dependency Injection
* REST APIs

### AI

* Ollama
* Qwen2.5-Coder

### Development Tools

* Visual Studio Code
* Swagger / OpenAPI
* Postman
* Git
* GitHub

---

## 📂 Project Structure

```text
AI-Bug-Explanation-Tool
│
├── BugExplainer.API
│   ├── Controllers
│   ├── DTOs
│   ├── Interfaces
│   ├── Models
│   ├── Services
│   └── Data
│
├── bug-explainer-client
│   ├── src
│   ├── public
│   └── assets
│
└── README.md
```

---

## 🔄 Workflow

### Step 1

Developer enters:

* Programming Language
* Framework
* Error Message
* Stack Trace
* Code Snippet

### Step 2

React frontend sends the request to the ASP.NET Core Web API.

### Step 3

The backend service forwards the debugging context to Ollama.

### Step 4

The local AI model analyzes the issue.

### Step 5

Structured debugging insights are returned.

### Step 6

Results are displayed in a user-friendly format.

---

## 📸 Example Analysis

### Input

```javascript
const user = undefined;
console.log(user.name);
```

Error:

```text
Cannot read properties of undefined (reading 'name')
```

### AI Output

**Severity:** High

**Root Cause:**
The variable `user` is undefined and is being accessed without validation.

**Suggested Fix:**

```javascript
if (user) {
  console.log(user.name);
}
```

**Prevention Tip:**
Use null checks or optional chaining before accessing object properties.

---

## 🎯 Learning Objectives

This project demonstrates practical experience in:

* Full-Stack Development
* React Development
* ASP.NET Core Web API Development
* REST API Integration
* Dependency Injection
* Service-Oriented Architecture
* AI Integration
* Prompt Engineering
* Local LLM Deployment
* Software Troubleshooting Workflows

---

## 📈 Current Status

### Version 1.0

Completed:

* React Frontend
* ASP.NET Core Backend
* DTO Architecture
* Service Layer
* Dependency Injection
* Swagger Documentation
* Ollama Integration
* AI Bug Analysis
* Responsive User Interface

---

## 🚀 Planned Enhancements

### Analysis History

Store previous bug analyses.

### SQLite Database

Persist analysis results and metadata.

### Dashboard Analytics

Visualize:

* Total Analyses
* Severity Distribution
* Common Bug Categories
* Technology Trends

### Log File Upload

Analyze:

* Application Logs
* Error Logs
* System Logs

### AI Follow-Up Chat

Allow developers to ask:

* Why did this happen?
* Show another solution.
* Explain in simple terms.
* What is the best practice?

### PDF Export

Generate shareable debugging reports.

### Authentication

* User Accounts
* Personal Analysis History
* Saved Reports

---

## 💼 Portfolio Value

This project showcases:

* Full-Stack Engineering
* AI-Assisted Development
* Software Troubleshooting
* API Design
* Enterprise Application Architecture
* Modern Development Workflows

It demonstrates how AI can be integrated into software engineering workflows to improve developer productivity and accelerate issue resolution.

---

## 👨‍💻 Author

### Rahesh Saravanan

AI & Full Stack Engineer

GitHub: https://github.com/raheshcse

LinkedIn: https://www.linkedin.com/in/raheshsaravanan/

Building intelligent software solutions across AI, full-stack development, automation, cybersecurity, and enterprise application workflows.

---

## ⭐ Version

**Current Release:** v1.0.0

**Status:** Active Development

**Next Milestone:** Analysis History + SQLite Database + Dashboard Analytics
