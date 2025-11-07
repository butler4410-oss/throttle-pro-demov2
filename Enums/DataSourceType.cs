namespace ThrottlePro.Shared.Enums;

/// <summary>
/// Defines the type of data source for a report
/// </summary>
public enum DataSourceType
{
    /// <summary>
    /// Query entity directly using LINQ/EF Core
    /// </summary>
    Entity = 0,

    /// <summary>
    /// Execute custom SQL query
    /// </summary>
    CustomSQL = 1,

    /// <summary>
    /// Call a stored procedure
    /// </summary>
    StoredProcedure = 2,

    /// <summary>
    /// Fetch data from external API
    /// </summary>
    API = 3
}
