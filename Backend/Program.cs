var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseCors("AllowReactApp"); // enable CORS for React
app.UseAuthorization();
app.MapControllers();
app.Run();
