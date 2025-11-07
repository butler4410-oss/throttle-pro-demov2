using ThrottlePro.Shared.DTOs.Reporting;
using ThrottlePro.Shared.Entities;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Interfaces;

/// <summary>
/// Core interface for report execution and management
/// Handles report generation, export, and template operations
/// </summary>
public interface IReportEngine
{
    /// <summary>
    /// Executes a report by template ID with optional runtime parameters
    /// </summary>
    /// <param name="templateId">ID of the report template to execute</param>
    /// <param name="parameters">Runtime parameter values (parameter name -> value)</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>Report results with data, columns, and visualization config</returns>
    Task<ReportResult> ExecuteReportAsync(
        Guid templateId,
        Dictionary<string, object>? parameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a report from a configuration object (for ad-hoc reports)
    /// </summary>
    /// <param name="config">Report configuration with data source, fields, filters, etc.</param>
    /// <param name="parameters">Runtime parameter values</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>Report results with data, columns, and visualization config</returns>
    Task<ReportResult> ExecuteReportAsync(
        ReportConfiguration config,
        Dictionary<string, object>? parameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Exports a completed report execution to the specified format
    /// </summary>
    /// <param name="executionId">ID of the completed report execution</param>
    /// <param name="format">Output format (PDF, Excel, CSV, JSON, HTML)</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>Byte array containing the exported file</returns>
    Task<byte[]> ExportReportAsync(
        Guid executionId,
        ReportFormat format,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all available system report templates
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>List of system report templates</returns>
    Task<List<ReportTemplateDto>> GetSystemTemplatesAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new report template
    /// </summary>
    /// <param name="request">Template creation request with configuration and metadata</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>Created report template entity</returns>
    Task<ReportTemplate> CreateTemplateAsync(
        CreateReportTemplateRequest request,
        CancellationToken cancellationToken = default);
}
