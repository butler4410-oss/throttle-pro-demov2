namespace ThrottlePro.Shared.Enums;

/// <summary>
/// Defines how often a scheduled report should execute
/// </summary>
public enum ReportFrequency
{
    /// <summary>
    /// Execute only on demand
    /// </summary>
    Manual = 0,

    /// <summary>
    /// Execute daily
    /// </summary>
    Daily = 1,

    /// <summary>
    /// Execute weekly
    /// </summary>
    Weekly = 2,

    /// <summary>
    /// Execute monthly
    /// </summary>
    Monthly = 3,

    /// <summary>
    /// Execute quarterly
    /// </summary>
    Quarterly = 4,

    /// <summary>
    /// Custom schedule using cron expression
    /// </summary>
    Custom = 99
}
