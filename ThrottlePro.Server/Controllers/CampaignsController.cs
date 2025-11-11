using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ThrottlePro.Server.Data;
using ThrottlePro.Shared.Entities;

namespace ThrottlePro.Server.Controllers;

/// <summary>
/// OData controller for Campaign entities
/// </summary>
[Route("odata/[controller]")]
public class CampaignsController : ODataController
{
    private readonly ThrottleProDbContext _context;
    private readonly ILogger<CampaignsController> _logger;

    public CampaignsController(ThrottleProDbContext context, ILogger<CampaignsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Get all campaigns with OData query support
    /// </summary>
    /// <returns>List of campaigns</returns>
    [HttpGet]
    [EnableQuery(MaxExpansionDepth = 3, PageSize = 50)]
    public IActionResult Get()
    {
        try
        {
            return Ok(_context.Campaigns);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving campaigns");
            return StatusCode(500, "An error occurred while retrieving campaigns");
        }
    }

    /// <summary>
    /// Get a single campaign by ID with related data
    /// </summary>
    /// <param name="key">Campaign ID</param>
    /// <returns>Campaign details</returns>
    [HttpGet("{key}")]
    [EnableQuery(MaxExpansionDepth = 3)]
    public async Task<IActionResult> Get([FromRoute] Guid key)
    {
        try
        {
            var campaign = await _context.Campaigns
                .Include(c => c.Segment)
                .Include(c => c.Coupons.OrderByDescending(co => co.CreatedAt).Take(100))
                .FirstOrDefaultAsync(c => c.Id == key);

            if (campaign == null)
            {
                return NotFound();
            }

            return Ok(campaign);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving campaign {CampaignId}", key);
            return StatusCode(500, "An error occurred while retrieving the campaign");
        }
    }

    /// <summary>
    /// Create a new campaign
    /// </summary>
    /// <param name="campaign">Campaign data</param>
    /// <returns>Created campaign</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Campaign campaign)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            campaign.Id = Guid.NewGuid();
            _context.Campaigns.Add(campaign);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Campaign created: {CampaignId} - {CampaignName}",
                campaign.Id, campaign.Name);

            return Created(campaign);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating campaign");
            return StatusCode(500, "An error occurred while creating the campaign");
        }
    }

    /// <summary>
    /// Update an existing campaign (partial update)
    /// </summary>
    /// <param name="key">Campaign ID</param>
    /// <param name="campaign">Changes to apply</param>
    /// <returns>Updated campaign</returns>
    [HttpPatch("{key}")]
    public async Task<IActionResult> Patch([FromRoute] Guid key, [FromBody] Campaign campaign)
    {
        try
        {
            var existingCampaign = await _context.Campaigns.FindAsync(key);
            if (existingCampaign == null)
            {
                return NotFound();
            }

            // Update properties
            _context.Entry(existingCampaign).CurrentValues.SetValues(campaign);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Campaign updated: {CampaignId}", key);

            return Ok(existingCampaign);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating campaign {CampaignId}", key);
            return StatusCode(500, "An error occurred while updating the campaign");
        }
    }

    /// <summary>
    /// Delete a campaign (soft delete)
    /// </summary>
    /// <param name="key">Campaign ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromRoute] Guid key)
    {
        try
        {
            var campaign = await _context.Campaigns.FindAsync(key);
            if (campaign == null)
            {
                return NotFound();
            }

            // Soft delete is handled automatically by DbContext
            _context.Campaigns.Remove(campaign);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Campaign soft deleted: {CampaignId}", key);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting campaign {CampaignId}", key);
            return StatusCode(500, "An error occurred while deleting the campaign");
        }
    }
}
