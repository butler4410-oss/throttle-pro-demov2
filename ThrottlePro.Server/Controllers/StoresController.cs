using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ThrottlePro.Server.Data;
using ThrottlePro.Shared.Entities;

namespace ThrottlePro.Server.Controllers;

/// <summary>
/// OData controller for Store entities
/// </summary>
[Route("odata/[controller]")]
public class StoresController : ODataController
{
    private readonly ThrottleProDbContext _context;
    private readonly ILogger<StoresController> _logger;

    public StoresController(ThrottleProDbContext context, ILogger<StoresController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Get all stores with OData query support
    /// </summary>
    /// <returns>List of stores</returns>
    [HttpGet]
    [EnableQuery(MaxExpansionDepth = 2, PageSize = 100)]
    public IActionResult Get()
    {
        try
        {
            return Ok(_context.Stores);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving stores");
            return StatusCode(500, "An error occurred while retrieving stores");
        }
    }

    /// <summary>
    /// Get a single store by ID with related data
    /// </summary>
    /// <param name="key">Store ID</param>
    /// <returns>Store details</returns>
    [HttpGet("{key}")]
    [EnableQuery(MaxExpansionDepth = 2)]
    public async Task<IActionResult> Get([FromRoute] Guid key)
    {
        try
        {
            var store = await _context.Stores
                .Include(s => s.Parent)
                .FirstOrDefaultAsync(s => s.Id == key);

            if (store == null)
            {
                return NotFound();
            }

            return Ok(store);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving store {StoreId}", key);
            return StatusCode(500, "An error occurred while retrieving the store");
        }
    }

    /// <summary>
    /// Create a new store
    /// </summary>
    /// <param name="store">Store data</param>
    /// <returns>Created store</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Store store)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            store.Id = Guid.NewGuid();
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Store created: {StoreId} - {StoreName}", store.Id, store.Name);

            return Created(store);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating store");
            return StatusCode(500, "An error occurred while creating the store");
        }
    }
}
