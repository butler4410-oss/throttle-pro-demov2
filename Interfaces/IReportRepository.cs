using ThrottlePro.Shared.Entities;

namespace ThrottlePro.Shared.Interfaces;

/// <summary>
/// Repository interface for report data access operations
/// Provides specialized queries for report templates and executions
/// </summary>
public interface IReportRepository
{
    /// <summary>
    /// Gets all system report templates
    /// System templates are built-in and cannot be modified/deleted by users
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>List of system report templates</returns>
    Task<List<ReportTemplate>> GetSystemTemplatesAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all user-created templates for a specific tenant
    /// Includes both public and private templates
    /// </summary>
    /// <param name="parentId">ID of the tenant (Parent)</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>List of user-created report templates</returns>
    Task<List<ReportTemplate>> GetUserTemplatesAsync(
        Guid parentId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the most recent report executions for a tenant
    /// Used for execution history and "recent reports" lists
    /// </summary>
    /// <param name="parentId">ID of the tenant (Parent)</param>
    /// <param name="count">Maximum number of executions to return</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>List of recent report executions ordered by creation date descending</returns>
    Task<List<ReportExecution>> GetRecentExecutionsAsync(
        Guid parentId,
        int count = 10,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a report execution by ID including cached result data
    /// Used for re-displaying previously executed reports
    /// </summary>
    /// <param name="executionId">ID of the report execution</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>Report execution with result data, or null if not found</returns>
    Task<ReportExecution?> GetExecutionWithResultAsync(
        Guid executionId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a report template by ID
    /// </summary>
    /// <param name="templateId">ID of the report template</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>Report template, or null if not found</returns>
    Task<ReportTemplate?> GetByIdAsync(
        Guid templateId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new report template
    /// </summary>
    /// <param name="template">Report template to add</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>Added report template with generated ID</returns>
    Task<ReportTemplate> AddAsync(
        ReportTemplate template,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing report template
    /// </summary>
    /// <param name="template">Report template with updated values</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    Task UpdateAsync(
        ReportTemplate template,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a report template (soft delete)
    /// </summary>
    /// <param name="templateId">ID of the template to delete</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    Task DeleteAsync(
        Guid templateId,
        CancellationToken cancellationToken = default);
}
