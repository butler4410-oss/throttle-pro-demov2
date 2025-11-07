namespace ThrottlePro.Shared.DTOs;

/// <summary>
/// Standard OData response wrapper
/// </summary>
public class ODataResponse<T>
{
    public List<T> Value { get; set; } = new();
    public int? Count { get; set; }
    public string? NextLink { get; set; }
}
