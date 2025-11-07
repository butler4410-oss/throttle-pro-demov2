using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a custom data source configuration for advanced reporting
/// Allows users to define custom SQL queries, stored procedures, or API endpoints
/// </summary>
[Index(nameof(ParentId), nameof(IsActive))]
public class ReportDataSource : TenantEntity
{
    /// <summary>
    /// Display name of the data source
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of what data this source provides
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Type of data source (Entity, CustomSQL, StoredProcedure, API)
    /// </summary>
    public DataSourceType Type { get; set; }

    /// <summary>
    /// Entity name for Type = Entity (e.g., "Customer", "Campaign")
    /// </summary>
    [MaxLength(100)]
    public string? EntityName { get; set; }

    /// <summary>
    /// Custom SQL query or stored procedure name for advanced users
    /// Must include @ParentId parameter for tenant isolation
    /// </summary>
    public string? CustomQuery { get; set; }

    /// <summary>
    /// JSON-serialized field mappings and metadata
    /// Defines available fields, data types, and display names
    /// </summary>
    public string? FieldMappingsJson { get; set; }

    /// <summary>
    /// Indicates if this data source is active and available
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// User ID who created this data source
    /// </summary>
    public Guid? CreatedByUserId { get; set; }
}
