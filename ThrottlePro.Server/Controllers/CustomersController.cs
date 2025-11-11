using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ThrottlePro.Server.Data;
using ThrottlePro.Shared.Entities;

namespace ThrottlePro.Server.Controllers;

/// <summary>
/// OData controller for Customer entities
/// </summary>
[Route("odata/[controller]")]
public class CustomersController : ODataController
{
    private readonly ThrottleProDbContext _context;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(ThrottleProDbContext context, ILogger<CustomersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Get all customers with OData query support
    /// </summary>
    /// <returns>List of customers</returns>
    [HttpGet]
    [EnableQuery(MaxExpansionDepth = 3, PageSize = 50)]
    public IActionResult Get()
    {
        try
        {
            return Ok(_context.Customers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving customers");
            return StatusCode(500, "An error occurred while retrieving customers");
        }
    }

    /// <summary>
    /// Get a single customer by ID with related data
    /// </summary>
    /// <param name="key">Customer ID</param>
    /// <returns>Customer details</returns>
    [HttpGet("{key}")]
    [EnableQuery(MaxExpansionDepth = 3)]
    public async Task<IActionResult> Get([FromRoute] Guid key)
    {
        try
        {
            var customer = await _context.Customers
                .Include(c => c.Vehicles)
                .Include(c => c.Visits.OrderByDescending(v => v.VisitDate).Take(10))
                .Include(c => c.CustomerSegments)
                    .ThenInclude(cs => cs.Segment)
                .FirstOrDefaultAsync(c => c.Id == key);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving customer {CustomerId}", key);
            return StatusCode(500, "An error occurred while retrieving the customer");
        }
    }

    /// <summary>
    /// Create a new customer
    /// </summary>
    /// <param name="customer">Customer data</param>
    /// <returns>Created customer</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Customer customer)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            customer.Id = Guid.NewGuid();
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Customer created: {CustomerId} - {CustomerName}",
                customer.Id, $"{customer.FirstName} {customer.LastName}");

            return Created(customer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating customer");
            return StatusCode(500, "An error occurred while creating the customer");
        }
    }

    /// <summary>
    /// Update an existing customer (partial update)
    /// </summary>
    /// <param name="key">Customer ID</param>
    /// <param name="delta">Changes to apply</param>
    /// <returns>Updated customer</returns>
    [HttpPatch("{key}")]
    public async Task<IActionResult> Patch([FromRoute] Guid key, [FromBody] Customer customer)
    {
        try
        {
            var existingCustomer = await _context.Customers.FindAsync(key);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            // Update properties
            _context.Entry(existingCustomer).CurrentValues.SetValues(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Customer updated: {CustomerId}", key);

            return Ok(existingCustomer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating customer {CustomerId}", key);
            return StatusCode(500, "An error occurred while updating the customer");
        }
    }

    /// <summary>
    /// Delete a customer (soft delete)
    /// </summary>
    /// <param name="key">Customer ID</param>
    /// <returns>No content</returns>
    [HttpDelete("{key}")]
    public async Task<IActionResult> Delete([FromRoute] Guid key)
    {
        try
        {
            var customer = await _context.Customers.FindAsync(key);
            if (customer == null)
            {
                return NotFound();
            }

            // Soft delete is handled automatically by DbContext
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Customer soft deleted: {CustomerId}", key);

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting customer {CustomerId}", key);
            return StatusCode(500, "An error occurred while deleting the customer");
        }
    }
}
