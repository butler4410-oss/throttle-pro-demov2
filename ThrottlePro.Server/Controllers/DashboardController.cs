using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThrottlePro.Server.Data;
using ThrottlePro.Shared.DTOs;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Server.Controllers;

/// <summary>
/// API controller for dashboard summary data
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ThrottleProDbContext _context;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(ThrottleProDbContext context, ILogger<DashboardController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Get comprehensive dashboard summary with key metrics
    /// </summary>
    /// <returns>Dashboard summary data</returns>
    [HttpGet("summary")]
    public async Task<ActionResult<DashboardSummary>> GetSummary()
    {
        try
        {
            var now = DateTime.UtcNow;
            var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);

            // Calculate customer metrics
            var totalCustomers = await _context.Customers.CountAsync();
            var newCustomersThisMonth = await _context.Customers
                .Where(c => c.CreatedAt >= firstDayOfMonth)
                .CountAsync();

            var activeCustomers = await _context.Customers
                .Where(c => c.LifecycleStage == CustomerLifecycleStage.Active)
                .CountAsync();

            var atRiskCustomers = await _context.Customers
                .Where(c => c.LifecycleStage == CustomerLifecycleStage.AtRisk)
                .CountAsync();

            var lapsedCustomers = await _context.Customers
                .Where(c => c.LifecycleStage == CustomerLifecycleStage.Lapsed)
                .CountAsync();

            var lostCustomers = await _context.Customers
                .Where(c => c.LifecycleStage == CustomerLifecycleStage.Lost)
                .CountAsync();

            // Calculate revenue metrics
            var totalRevenue = await _context.Visits
                .SumAsync(v => v.NetAmount);

            var revenueThisMonth = await _context.Visits
                .Where(v => v.VisitDate >= firstDayOfMonth)
                .SumAsync(v => v.NetAmount);

            var averageOrderValue = totalCustomers > 0
                ? totalRevenue / totalCustomers
                : 0;

            // Calculate visit metrics
            var totalVisits = await _context.Visits.CountAsync();
            var visitsThisMonth = await _context.Visits
                .Where(v => v.VisitDate >= firstDayOfMonth)
                .CountAsync();

            // Calculate campaign metrics
            var activeCampaigns = await _context.Campaigns
                .Where(c => c.Status == CampaignStatus.Active)
                .CountAsync();

            var totalCampaigns = await _context.Campaigns.CountAsync();

            var averageROAS = await _context.Campaigns
                .Where(c => c.Spent > 0)
                .AverageAsync(c => (decimal?)c.ROAS) ?? 0;

            // Calculate segment metrics
            var activeSegments = await _context.Segments
                .Where(s => s.IsActive)
                .CountAsync();

            var totalSegments = await _context.Segments.CountAsync();

            // Get lifecycle breakdown
            var lifecycleBreakdown = await _context.Customers
                .GroupBy(c => c.LifecycleStage)
                .Select(g => new { Stage = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Stage, x => x.Count);

            // Get recent activities (last 10 visits)
            var recentActivities = await _context.Visits
                .OrderByDescending(v => v.VisitDate)
                .Take(10)
                .Include(v => v.Customer)
                .Select(v => new RecentActivity
                {
                    Id = v.Id,
                    Type = "Visit",
                    Description = $"{v.Customer.FirstName} {v.Customer.LastName} visited - ${v.NetAmount:N2}",
                    Timestamp = v.VisitDate,
                    CustomerName = $"{v.Customer.FirstName} {v.Customer.LastName}"
                })
                .ToListAsync();

            var summary = new DashboardSummary
            {
                TotalCustomers = totalCustomers,
                NewCustomersThisMonth = newCustomersThisMonth,
                ActiveCustomers = activeCustomers,
                AtRiskCustomers = atRiskCustomers,
                LapsedCustomers = lapsedCustomers,
                LostCustomers = lostCustomers,
                TotalRevenue = totalRevenue,
                RevenueThisMonth = revenueThisMonth,
                AverageOrderValue = averageOrderValue,
                TotalVisits = totalVisits,
                VisitsThisMonth = visitsThisMonth,
                ActiveCampaigns = activeCampaigns,
                TotalCampaigns = totalCampaigns,
                AverageROAS = averageROAS,
                ActiveSegments = activeSegments,
                TotalSegments = totalSegments,
                LifecycleBreakdown = lifecycleBreakdown,
                RecentActivities = recentActivities
            };

            _logger.LogInformation("Dashboard summary retrieved successfully");

            return Ok(summary);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving dashboard summary");
            return StatusCode(500, "An error occurred while retrieving dashboard summary");
        }
    }

    /// <summary>
    /// Get customer lifecycle distribution for charts
    /// </summary>
    /// <returns>Lifecycle stage counts</returns>
    [HttpGet("lifecycle-distribution")]
    public async Task<ActionResult<Dictionary<string, int>>> GetLifecycleDistribution()
    {
        try
        {
            var distribution = await _context.Customers
                .GroupBy(c => c.LifecycleStage)
                .Select(g => new { Stage = g.Key.ToString(), Count = g.Count() })
                .ToDictionaryAsync(x => x.Stage, x => x.Count);

            return Ok(distribution);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving lifecycle distribution");
            return StatusCode(500, "An error occurred while retrieving lifecycle distribution");
        }
    }

    /// <summary>
    /// Get revenue trends for the last 12 months
    /// </summary>
    /// <returns>Monthly revenue data</returns>
    [HttpGet("revenue-trends")]
    public async Task<ActionResult<List<object>>> GetRevenueTrends()
    {
        try
        {
            var twelveMonthsAgo = DateTime.UtcNow.AddMonths(-12);

            var trends = await _context.Visits
                .Where(v => v.VisitDate >= twelveMonthsAgo)
                .GroupBy(v => new { v.VisitDate.Year, v.VisitDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(v => v.NetAmount),
                    VisitCount = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            return Ok(trends);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving revenue trends");
            return StatusCode(500, "An error occurred while retrieving revenue trends");
        }
    }
}
