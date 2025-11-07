using System.ComponentModel.DataAnnotations;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Campaign;

/// <summary>
/// Request DTO for creating a new Campaign
/// </summary>
public class CreateCampaignRequest
{
    public Guid? SegmentId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    public CampaignChannel Channel { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value")]
    public decimal Budget { get; set; } = 0;

    [Range(0, int.MaxValue, ErrorMessage = "Target audience must be a positive value")]
    public int TargetAudience { get; set; } = 0;
}
