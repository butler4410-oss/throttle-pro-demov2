using System.ComponentModel.DataAnnotations;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Campaign;

/// <summary>
/// Request DTO for updating an existing Campaign (PATCH support - all nullable)
/// </summary>
public class UpdateCampaignRequest
{
    public Guid? SegmentId { get; set; }

    [MaxLength(200)]
    public string? Name { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public CampaignStatus? Status { get; set; }

    public CampaignChannel? Channel { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Budget must be a positive value")]
    public decimal? Budget { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Target audience must be a positive value")]
    public int? TargetAudience { get; set; }
}
