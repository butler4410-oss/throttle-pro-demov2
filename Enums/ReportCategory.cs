namespace ThrottlePro.Shared.Enums;

/// <summary>
/// Defines the category of a report template
/// </summary>
public enum ReportCategory
{
    /// <summary>
    /// Customer-related reports (demographics, lifecycle, etc.)
    /// </summary>
    Customer = 0,

    /// <summary>
    /// Campaign performance and ROAS reports
    /// </summary>
    Campaign = 1,

    /// <summary>
    /// Revenue and financial reports
    /// </summary>
    Revenue = 2,

    /// <summary>
    /// Customer lifecycle stage reports
    /// </summary>
    Lifecycle = 3,

    /// <summary>
    /// Store-level performance reports
    /// </summary>
    Store = 4,

    /// <summary>
    /// Executive summary and KPI reports
    /// </summary>
    Executive = 5,

    /// <summary>
    /// User-defined custom reports
    /// </summary>
    Custom = 99
}
