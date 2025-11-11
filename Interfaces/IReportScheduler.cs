using ThrottlePro.Shared.DTOs.Reporting;
using ThrottlePro.Shared.Entities;

namespace ThrottlePro.Shared.Interfaces;

/// <summary>
/// Interface for report scheduling and automated execution
/// Manages scheduled report runs and background processing
/// </summary>
public interface IReportScheduler
{
    /// <summary>
    /// Creates a new report schedule
    /// </summary>
    /// <param name="request">Schedule configuration with frequency, recipients, and parameters</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>Created report schedule entity</returns>
    Task<ReportSchedule> CreateScheduleAsync(
        CreateReportScheduleRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all active schedules for the current tenant
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>List of active report schedules</returns>
    Task<List<ReportScheduleDto>> GetActiveSchedulesAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all schedules for a specific report template
    /// </summary>
    /// <param name="templateId">ID of the report template</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>List of schedules for the template</returns>
    Task<List<ReportScheduleDto>> GetSchedulesByTemplateAsync(
        Guid templateId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Enables a schedule to allow automatic execution
    /// </summary>
    /// <param name="scheduleId">ID of the schedule to enable</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    Task EnableScheduleAsync(
        Guid scheduleId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Disables a schedule to prevent automatic execution
    /// </summary>
    /// <param name="scheduleId">ID of the schedule to disable</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    Task DisableScheduleAsync(
        Guid scheduleId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes all schedules that are due to run
    /// Called by a background worker process on a timer
    /// </summary>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    Task ExecuteDueSchedulesAsync(
        CancellationToken cancellationToken = default);
}
