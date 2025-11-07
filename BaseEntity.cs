using System.ComponentModel.DataAnnotations;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Base entity class containing common properties for all entities
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Unique identifier for the entity
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Timestamp when the entity was created
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Timestamp when the entity was last updated
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Username or identifier of who created the entity
    /// </summary>
    [MaxLength(100)]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Username or identifier of who last updated the entity
    /// </summary>
    [MaxLength(100)]
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Soft delete flag - if true, entity is considered deleted
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Timestamp when the entity was soft deleted
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Username or identifier of who deleted the entity
    /// </summary>
    [MaxLength(100)]
    public string? DeletedBy { get; set; }

    /// <summary>
    /// Row version for optimistic concurrency control
    /// </summary>
    [Timestamp]
    public byte[]? RowVersion { get; set; }
}
