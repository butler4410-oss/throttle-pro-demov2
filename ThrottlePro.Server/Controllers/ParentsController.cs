using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ThrottlePro.Server.Data;
using ThrottlePro.Shared.Entities;

namespace ThrottlePro.Server.Controllers;

/// <summary>
/// OData controller for Parent (Tenant) entities
/// </summary>
[Route("odata/[controller]")]
public class ParentsController : ODataController
{
    private readonly ThrottleProDbContext _context;
    private readonly ILogger<ParentsController> _logger;

    public ParentsController(ThrottleProDbContext context, ILogger<ParentsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Get all parents with OData query support
    /// </summary>
    /// <returns>List of parents</returns>
    [HttpGet]
    [EnableQuery(MaxExpansionDepth = 2, PageSize = 100)]
    public IActionResult Get()
    {
        try
        {
            return Ok(_context.Parents);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving parents");
            return StatusCode(500, "An error occurred while retrieving parents");
        }
    }

    /// <summary>
    /// Get a single parent by ID with related data
    /// </summary>
    /// <param name="key">Parent ID</param>
    /// <returns>Parent details</returns>
    [HttpGet("{key}")]
    [EnableQuery(MaxExpansionDepth = 2)]
    public async Task<IActionResult> Get([FromRoute] Guid key)
    {
        try
        {
            var parent = await _context.Parents
                .Include(p => p.Stores)
                .FirstOrDefaultAsync(p => p.Id == key);

            if (parent == null)
            {
                return NotFound();
            }

            return Ok(parent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving parent {ParentId}", key);
            return StatusCode(500, "An error occurred while retrieving the parent");
        }
    }

    /// <summary>
    /// Create a new parent
    /// </summary>
    /// <param name="parent">Parent data</param>
    /// <returns>Created parent</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Parent parent)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            parent.Id = Guid.NewGuid();
            _context.Parents.Add(parent);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Parent created: {ParentId} - {ParentName}", parent.Id, parent.Name);

            return Created(parent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating parent");
            return StatusCode(500, "An error occurred while creating the parent");
        }
    }
}
