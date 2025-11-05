using c2030270_saad.Business.Helpers;
using c2030270_saad.Business.Helpers.Interfaces;
using c2030270_saad.Data;
using c2030270_saad.Data.Queries.Complaint;
using c2030270_saad.Data.Queries.Complaint.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IComplaintGetter, ComplaintGetter>();
builder.Services.AddScoped<IGetComplaintById, GetComplaintById>();

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


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SAAD Project API v1");
    c.RoutePrefix = "swagger";
});

app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/api/health");

app.Run();