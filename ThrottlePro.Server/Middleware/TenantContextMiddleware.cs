using ThrottlePro.Shared.Interfaces;

namespace ThrottlePro.Server.Middleware;

/// <summary>
/// Implementation of ITenantContext that extracts tenant information from HTTP headers
/// </summary>
public class TenantContext : ITenantContext
{
    public Guid? ParentId { get; set; }
    public Guid? StoreId { get; set; }
    public bool HasParentId => ParentId.HasValue;
    public bool HasStoreId => StoreId.HasValue;
}

/// <summary>
/// Middleware that extracts tenant context from HTTP headers and sets it for the current request
/// </summary>
public class TenantContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TenantContextMiddleware> _logger;

    public TenantContextMiddleware(RequestDelegate next, ILogger<TenantContextMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ITenantContext tenantContext)
    {
        if (tenantContext is TenantContext mutableContext)
        {
            // Extract X-Parent-Id header
            if (context.Request.Headers.TryGetValue("X-Parent-Id", out var parentIdValue))
            {
                if (Guid.TryParse(parentIdValue, out var parentId))
                {
                    mutableContext.ParentId = parentId;
                    _logger.LogDebug("Tenant context set: ParentId = {ParentId}", parentId);
                }
                else
                {
                    _logger.LogWarning("Invalid X-Parent-Id header value: {Value}", parentIdValue);
                }
            }

            // Extract X-Store-Id header
            if (context.Request.Headers.TryGetValue("X-Store-Id", out var storeIdValue))
            {
                if (Guid.TryParse(storeIdValue, out var storeId))
                {
                    mutableContext.StoreId = storeId;
                    _logger.LogDebug("Tenant context set: StoreId = {StoreId}", storeId);
                }
                else
                {
                    _logger.LogWarning("Invalid X-Store-Id header value: {Value}", storeIdValue);
                }
            }

            // Log warning if no tenant context is provided (except for non-tenant endpoints)
            if (!mutableContext.HasParentId && !IsPublicEndpoint(context.Request.Path))
            {
                _logger.LogWarning(
                    "No tenant context provided for path: {Path}. Add X-Parent-Id header to requests.",
                    context.Request.Path
                );
            }
        }

        await _next(context);
    }

    /// <summary>
    /// Determines if an endpoint should be accessible without tenant context
    /// </summary>
    private static bool IsPublicEndpoint(PathString path)
    {
        var publicPaths = new[]
        {
            "/swagger",
            "/health",
            "/api/auth",
            "/$metadata"
        };

        return publicPaths.Any(p => path.StartsWithSegments(p, StringComparison.OrdinalIgnoreCase));
    }
}

/// <summary>
/// Extension methods for registering tenant context middleware
/// </summary>
public static class TenantContextMiddlewareExtensions
{
    /// <summary>
    /// Adds tenant context services to the service collection
    /// </summary>
    public static IServiceCollection AddTenantContext(this IServiceCollection services)
    {
        services.AddScoped<ITenantContext, TenantContext>();
        return services;
    }

    /// <summary>
    /// Adds the tenant context middleware to the application pipeline
    /// </summary>
    public static IApplicationBuilder UseTenantContext(this IApplicationBuilder app)
    {
        return app.UseMiddleware<TenantContextMiddleware>();
    }
}
