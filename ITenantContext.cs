namespace ThrottlePro.Shared.Interfaces;

/// <summary>
/// Provides access to the current tenant context (Parent and Store)
/// </summary>
public interface ITenantContext
{
    Guid? ParentId { get; }
    Guid? StoreId { get; }
    bool HasParentId { get; }
    bool HasStoreId { get; }
}
