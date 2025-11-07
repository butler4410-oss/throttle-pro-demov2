namespace ThrottlePro.Shared.DTOs.Reporting;

/// <summary>
/// Defines a runtime parameter for a report
/// Allows users to provide dynamic values at execution time
/// </summary>
public class ReportParameter
{
    /// <summary>
    /// Internal parameter name used in filters/expressions
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Display name shown to users
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Data type (string, int, decimal, datetime, bool)
    /// </summary>
    public string DataType { get; set; } = "string";

    /// <summary>
    /// Current value of the parameter
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// Default value if not provided
    /// </summary>
    public object? DefaultValue { get; set; }

    /// <summary>
    /// Whether this parameter must be provided
    /// </summary>
    public bool IsRequired { get; set; } = false;

    /// <summary>
    /// List of predefined options (for dropdown parameters)
    /// </summary>
    public List<ParameterOption>? Options { get; set; }
}

/// <summary>
/// Represents a predefined option for a parameter
/// </summary>
public class ParameterOption
{
    /// <summary>
    /// Display label shown to users
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Actual value to use in the query
    /// </summary>
    public object Value { get; set; } = null!;
}
