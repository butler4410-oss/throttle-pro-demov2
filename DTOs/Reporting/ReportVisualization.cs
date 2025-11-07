using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Reporting;

/// <summary>
/// Configuration for chart/graph visualization of report data
/// </summary>
public class ReportVisualization
{
    /// <summary>
    /// Type of chart to render
    /// </summary>
    public ChartType ChartType { get; set; }

    /// <summary>
    /// Field to use for X-axis or categories
    /// </summary>
    public string? XAxisField { get; set; }

    /// <summary>
    /// Fields to use for Y-axis or values (supports multiple series)
    /// </summary>
    public List<string> YAxisFields { get; set; } = new();

    /// <summary>
    /// Custom color palette for chart series
    /// Uses Matrix brand colors by default if not specified
    /// </summary>
    public List<string>? Colors { get; set; }

    /// <summary>
    /// Whether to show the chart legend
    /// </summary>
    public bool ShowLegend { get; set; } = true;

    /// <summary>
    /// Whether to show data labels on chart elements
    /// </summary>
    public bool ShowDataLabels { get; set; } = false;

    /// <summary>
    /// Chart title (displayed above the chart)
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Chart height in pixels (null for responsive)
    /// </summary>
    public int? Height { get; set; }

    /// <summary>
    /// Chart width in pixels (null for responsive)
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// X-axis label
    /// </summary>
    public string? XAxisLabel { get; set; }

    /// <summary>
    /// Y-axis label
    /// </summary>
    public string? YAxisLabel { get; set; }
}
