namespace ThrottlePro.Shared.Enums;

/// <summary>
/// Defines the visualization type for a report
/// </summary>
public enum ReportType
{
    /// <summary>
    /// Tabular data display
    /// </summary>
    Table = 0,

    /// <summary>
    /// Chart/graph visualization
    /// </summary>
    Chart = 1,

    /// <summary>
    /// Multi-widget dashboard
    /// </summary>
    Dashboard = 2,

    /// <summary>
    /// Single key performance indicator
    /// </summary>
    KPI = 3,

    /// <summary>
    /// Side-by-side comparison report
    /// </summary>
    Comparison = 4,

    /// <summary>
    /// Time-series trend analysis
    /// </summary>
    Trend = 5
}
