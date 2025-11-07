namespace ThrottlePro.Shared.Enums;

/// <summary>
/// Defines the execution status of a report run
/// </summary>
public enum ReportExecutionStatus
{
    /// <summary>
    /// Report is queued for execution
    /// </summary>
    Queued = 0,

    /// <summary>
    /// Report is currently executing
    /// </summary>
    Running = 1,

    /// <summary>
    /// Report execution completed successfully
    /// </summary>
    Completed = 2,

    /// <summary>
    /// Report execution failed with errors
    /// </summary>
    Failed = 3,

    /// <summary>
    /// Report execution was cancelled by user
    /// </summary>
    Cancelled = 4
}
