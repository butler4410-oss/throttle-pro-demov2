using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Serilog;
using ThrottlePro.Server.Data;
using ThrottlePro.Server.Middleware;
using ThrottlePro.Shared.Entities;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/throttlepro-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers()
    .AddOData(options =>
    {
        options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(1000);
        options.AddRouteComponents("odata", GetEdmModel());
    });

// Configure SQL Server with retry on failure
builder.Services.AddDbContext<ThrottleProDbContext>((serviceProvider, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
        );
        sqlOptions.CommandTimeout(60);
    });

    // Enable sensitive data logging in development
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

// Add Tenant Context
builder.Services.AddTenantContext();

// Configure CORS
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? new[] { "https://localhost:5001" };

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Disposition");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ThrottlePro API",
        Version = "v1",
        Description = "Multi-tenant CRM and Marketing Automation API for automotive service centers"
    });

    // Add XML comments if available
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Add tenant headers to Swagger UI
    c.AddSecurityDefinition("Tenant", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Tenant context headers (X-Parent-Id, X-Store-Id)",
        Name = "X-Parent-Id",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ThrottlePro API v1");
        c.RoutePrefix = "swagger";
    });

    // Run database migrations automatically in development
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ThrottleProDbContext>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            Log.Information("Applying database migrations...");
            context.Database.Migrate();
            Log.Information("Database migrations applied successfully");

            // Seed the database with sample data
            await SeedData.SeedAsync(context, logger);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while migrating the database");
            // Don't throw - allow app to start even if migrations fail
        }
    }
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors();

// Add tenant context middleware before authorization
app.UseTenantContext();

app.UseAuthorization();

app.MapControllers();

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new
{
    status = "healthy",
    timestamp = DateTime.UtcNow,
    environment = app.Environment.EnvironmentName
}));

Log.Information("ThrottlePro Server starting on {Environment}", app.Environment.EnvironmentName);

app.Run();

/// <summary>
/// Build the EDM (Entity Data Model) for OData
/// </summary>
static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();

    // Configure entity sets
    builder.EntitySet<Parent>("Parents");
    builder.EntitySet<Store>("Stores");
    builder.EntitySet<Customer>("Customers");
    builder.EntitySet<Vehicle>("Vehicles");
    builder.EntitySet<Visit>("Visits");
    builder.EntitySet<Campaign>("Campaigns");
    builder.EntitySet<Coupon>("Coupons");
    builder.EntitySet<Segment>("Segments");
    builder.EntitySet<CustomerSegment>("CustomerSegments");
    builder.EntitySet<Journey>("Journeys");
    builder.EntitySet<JourneyStep>("JourneySteps");
    builder.EntitySet<ReportTemplate>("ReportTemplates");
    builder.EntitySet<ReportSchedule>("ReportSchedules");
    builder.EntitySet<ReportExecution>("ReportExecutions");
    builder.EntitySet<ReportDataSource>("ReportDataSources");

    return builder.GetEdmModel();
}
