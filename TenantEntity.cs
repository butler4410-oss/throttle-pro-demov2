using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Base entity class for all tenant-scoped entities.
/// Adds ParentId for multi-tenant data isolation.
/// Designed to support thousands of tenants with efficient querying.
/// </summary>
[Index(nameof(ParentId))]
[Index(nameof(ParentId), nameof(IsDeleted))]
public abstract class TenantEntity : BaseEntity
{
    /// <summary>
    /// Foreign key to the Parent (tenant) that owns this entity.
    /// All queries should be filtered by ParentId for data isolation.
    /// Indexed for performance with thousands of tenants.
    /// </summary>
    [Required]
    public Guid ParentId { get; set; }

    /// <summary>
    /// Navigation property to the Parent tenant
    /// </summary>
    [ForeignKey(nameof(ParentId))]
    public virtual Parent Parent { get; set; } = null!;
}
