namespace ThrottlePro.Shared.Enums;

/// <summary>
/// Defines the type of aggregation to apply to a field
/// </summary>
public enum AggregationType
{
    /// <summary>
    /// No aggregation, display raw values
    /// </summary>
    None = 0,

    /// <summary>
    /// Count the number of records
    /// </summary>
    Count = 1,

    /// <summary>
    /// Sum numeric values
    /// </summary>
    Sum = 2,

    /// <summary>
    /// Calculate average of numeric values
    /// </summary>
    Average = 3,

    /// <summary>
    /// Find minimum value
    /// </summary>
    Min = 4,

    /// <summary>
    /// Find maximum value
    /// </summary>
    Max = 5,

    /// <summary>
    /// Calculate percentage of total
    /// </summary>
    Percentage = 6,

    /// <summary>
    /// Calculate growth rate over time
    /// </summary>
    Growth = 7
}
