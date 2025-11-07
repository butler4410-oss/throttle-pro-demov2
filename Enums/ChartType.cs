namespace ThrottlePro.Shared.Enums;

/// <summary>
/// Defines the type of chart visualization
/// </summary>
public enum ChartType
{
    /// <summary>
    /// Vertical or horizontal bar chart
    /// </summary>
    Bar = 0,

    /// <summary>
    /// Line chart for trends
    /// </summary>
    Line = 1,

    /// <summary>
    /// Pie chart for proportions
    /// </summary>
    Pie = 2,

    /// <summary>
    /// Doughnut chart (pie with center hole)
    /// </summary>
    Doughnut = 3,

    /// <summary>
    /// Area chart with filled region
    /// </summary>
    Area = 4,

    /// <summary>
    /// Scatter plot for correlation
    /// </summary>
    Scatter = 5,

    /// <summary>
    /// Funnel chart for conversion flows
    /// </summary>
    Funnel = 6,

    /// <summary>
    /// Gauge chart for single KPI
    /// </summary>
    Gauge = 7
}
