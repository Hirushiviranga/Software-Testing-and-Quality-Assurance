using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add services
builder.Services.AddControllers(options =>
{
    // Register API Key filter globally
    options.Filters.Add<ApiKeyAuthAttribute>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// ✅ Enforce HTTPS + HSTS
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();  // Adds Strict-Transport-Security header
}
app.UseHttpsRedirection();  // Redirect HTTP → HTTPS

// ✅ Middleware
app.UseCors("AllowReactApp");
app.UseAuthorization();

app.MapControllers();

app.Run();


// ------------------- API Key Attribute -------------------
public class ApiKeyAuthAttribute : Attribute, IAuthorizationFilter
{
    private const string ApiKeyHeader = "X-API-KEY";
    private const string ApiKeyValue = "my-secret-key"; // 🔒 Replace with strong key

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out var extractedKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!ApiKeyValue.Equals(extractedKey))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}
