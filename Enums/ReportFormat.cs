namespace ThrottlePro.Shared.Enums;

/// <summary>
/// Defines the output format for exported reports
/// </summary>
public enum ReportFormat
{
    /// <summary>
    /// Portable Document Format
    /// </summary>
    PDF = 0,

    /// <summary>
    /// Microsoft Excel format (.xlsx)
    /// </summary>
    Excel = 1,

    /// <summary>
    /// Comma-separated values
    /// </summary>
    CSV = 2,

    /// <summary>
    /// JavaScript Object Notation
    /// </summary>
    JSON = 3,

    /// <summary>
    /// Hypertext Markup Language
    /// </summary>
    HTML = 4
}
