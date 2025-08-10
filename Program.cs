using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using RecordManagement.Api.Services;
using RecordManagement.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization();

// Add API Explorer services for OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Record Management API", Version = "v1" });
});

// Add CORS for development
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register services
builder.Services.AddScoped<IRecordService, InMemoryRecordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Record Management API v1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the apps root
    });
    app.UseCors("DevelopmentPolicy");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Map API endpoints
app.MapRecordEndpoints();

// Simple health check endpoint
app.MapGet("/health", () => Results.Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow }))
   .WithName("HealthCheck")
   .WithTags("Health");

// Development login endpoint (for easy testing)
if (app.Environment.IsDevelopment())
{
    app.MapGet("/dev/login", () => Results.Ok(new { 
        Message = "Development mode - authentication bypassed",
        Instructions = "In production, use proper Microsoft Identity authentication"
    }))
    .WithName("DevLogin")
    .WithTags("Development")
    .AllowAnonymous();
}

app.Run();
