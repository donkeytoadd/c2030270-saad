using c2030270_saad.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Scan(scan => scan
    .FromAssemblies(typeof(Program).Assembly)
    .AddClasses(c => c.Where(t =>
        t.Name.EndsWith("Getter") ||
        t.Name.EndsWith("Creator") ||
        t.Name.EndsWith("Updater") ||
        t.Name.EndsWith("Query")
    ))
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SAAD Project API",
        Version = "v1",
        Description = "This will be an API"
    });
});

var myAllowSpecificOrigins = "myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

var app = builder.Build();

app.UseCors(myAllowSpecificOrigins);

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SAAD Project API v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/api/health");

app.Run();