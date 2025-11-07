using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Reporting;

/// <summary>
/// Complete configuration for a report's data query
/// Includes field selection, filtering, grouping, sorting, and calculations
/// </summary>
public class ReportConfiguration
{
    /// <summary>
    /// Data source name (entity name or custom data source)
    /// Examples: "Customer", "Campaign", "Visit"
    /// </summary>
    public string DataSource { get; set; } = string.Empty;

    /// <summary>
    /// List of fields to include in the report
    /// </summary>
    public List<ReportField> Fields { get; set; } = new();

    /// <summary>
    /// Filters to apply to the data query
    /// </summary>
    public List<ReportFilter> Filters { get; set; } = new();

    /// <summary>
    /// Fields to group by for aggregation
    /// </summary>
    public List<ReportGroupBy> GroupBy { get; set; } = new();

    /// <summary>
    /// Sort order for the results
    /// </summary>
    public List<ReportSort> Sorting { get; set; } = new();

    /// <summary>
    /// Custom calculated fields
    /// </summary>
    public List<ReportCalculation> Calculations { get; set; } = new();

    /// <summary>
    /// Pagination settings (null for no pagination)
    /// </summary>
    public ReportPagination? Pagination { get; set; }
}

/// <summary>
/// Defines a field in the report output
/// </summary>
public class ReportField
{
    /// <summary>
    /// Internal field name from the entity
    /// </summary>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>
    /// Display name shown to users
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Data type (string, int, decimal, datetime, bool)
    /// </summary>
    public string? DataType { get; set; }

    /// <summary>
    /// Format string (e.g., "$#,##0.00", "MMM dd, yyyy")
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// Aggregation function to apply
    /// </summary>
    public AggregationType? Aggregation { get; set; }

    /// <summary>
    /// Whether this field is visible in output
    /// </summary>
    public bool IsVisible { get; set; } = true;

    /// <summary>
    /// Display order (lower numbers appear first)
    /// </summary>
    public int Order { get; set; } = 0;
}

/// <summary>
/// Defines a filter condition for the report query
/// </summary>
public class ReportFilter
{
    /// <summary>
    /// Field name to filter on
    /// </summary>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>
    /// Filter operator (Equals, Contains, GreaterThan, Between, In, etc.)
    /// </summary>
    public string Operator { get; set; } = string.Empty;

    /// <summary>
    /// Primary filter value
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// Secondary value for Between operator
    /// </summary>
    public object? Value2 { get; set; }

    /// <summary>
    /// Logical operator to combine with next filter (AND, OR)
    /// </summary>
    public string LogicalOperator { get; set; } = "AND";
}

/// <summary>
/// Defines a grouping dimension for aggregation
/// </summary>
public class ReportGroupBy
{
    /// <summary>
    /// Field name to group by
    /// </summary>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>
    /// Date interval for date fields (day, week, month, quarter, year)
    /// </summary>
    public string? DateInterval { get; set; }
}

/// <summary>
/// Defines a sort order for report results
/// </summary>
public class ReportSort
{
    /// <summary>
    /// Field name to sort by
    /// </summary>
    public string FieldName { get; set; } = string.Empty;

    /// <summary>
    /// Sort descending (true) or ascending (false)
    /// </summary>
    public bool Descending { get; set; } = false;
}

/// <summary>
/// Defines a calculated field using an expression
/// </summary>
public class ReportCalculation
{
    /// <summary>
    /// Name of the calculated field
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Expression to calculate the value
    /// Example: "Revenue / Spent" for ROAS
    /// </summary>
    public string Expression { get; set; } = string.Empty;

    /// <summary>
    /// Format string for the result
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// Aggregation to apply after calculation
    /// </summary>
    public AggregationType? Aggregation { get; set; }
}

/// <summary>
/// Defines pagination settings for the report
/// </summary>
public class ReportPagination
{
    /// <summary>
    /// Number of rows per page
    /// </summary>
    public int PageSize { get; set; } = 25;

    /// <summary>
    /// Current page number (1-based)
    /// </summary>
    public int CurrentPage { get; set; } = 1;

    /// <summary>
    /// Total number of rows (populated after query)
    /// </summary>
    public int TotalCount { get; set; } = 0;
}
