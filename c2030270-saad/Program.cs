using c2030270_saad.Business.Creators.Complaint;
using c2030270_saad.Business.Creators.Complaint.Interfaces;
using c2030270_saad.Business.Creators.Tenant;
using c2030270_saad.Business.Creators.Tenant.Interfaces;
using c2030270_saad.Business.Getters.Complaint;
using c2030270_saad.Business.Getters.Complaint.Interfaces;
using c2030270_saad.Business.Getters.Tenant;
using c2030270_saad.Business.Getters.Tenant.Interfaces;
using c2030270_saad.Business.Updaters.Complaint;
using c2030270_saad.Business.Updaters.Complaint.Interfaces;
using c2030270_saad.Data;
using c2030270_saad.Data.Queries;
using c2030270_saad.Data.Queries.Complaint;
using c2030270_saad.Data.Queries.Complaint.Interfaces;
using c2030270_saad.Data.Queries.Consumer;
using c2030270_saad.Data.Queries.Consumer.Interfaces;
using c2030270_saad.Data.Queries.Tenant;
using c2030270_saad.Data.Queries.Tenant.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IComplaintGetter, ComplaintGetter>();
builder.Services.AddScoped<IComplaintCreator, ComplaintCreator>();
builder.Services.AddScoped<IComplaintStatusUpdater, ComplaintStatusUpdater>();
builder.Services.AddScoped<ITenantCreator, TenantCreator>();
builder.Services.AddScoped<ITenantGetter, TenantGetter>();
builder.Services.AddScoped<IGetComplaintByIdQuery, GetComplaintByIdQuery>();
builder.Services.AddScoped<IGetAllComplaintsByConsumerIdQuery, GetAllComplaintsByConsumerIdQuery>();
builder.Services.AddScoped<IGetAllComplaintsByTenantIdQuery, GetAllComplaintsByTenantIdQuery>();
builder.Services.AddScoped<ICreateComplaintQuery, CreateComplaintQuery>();
builder.Services.AddScoped<IGetConsumerByConsumerIdQuery, GetConsumerByConsumerIdQuery>();
builder.Services.AddScoped<IUpdateComplaintStatusQuery, UpdateComplaintStatusQuery>();
builder.Services.AddScoped<ICreateTenantQuery, CreateTenantQuery>();
builder.Services.AddScoped<IGetAllTenantsQuery, GetAllTenantsQuery>();
builder.Services.AddScoped<IGetTenantByTenantIdQuery, GetTenantByTenantIdQuery>();

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