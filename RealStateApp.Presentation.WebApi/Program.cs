using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.IOC;
using RealStateApp.Infrastructure.Identity.Entities;
using RealStateApp.Infrastructure.Identity.IOC;
using RealStateApp.Infrastructure.Identity.Seeds;
using RealStateApp.Infrastructure.Persistence.IOC;
using RealStateApp.Infrastructure.Shared.IOC;
using RealStateApp.WebApi.Extensions;
using StockApp.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressConsumesConstraintForFormFileParameters = true;
    options.SuppressMapClientErrors = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructureForApi(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddApiVersioningExtension();
builder.Services.AddApplicationLayer();
builder.Services.AddSwaggerExtension();


builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSession();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await AdminUser.SeedAsync(userManager, roleManager);
        await AgentUser.SeedAsync(userManager, roleManager);
        await ClientUser.SeedAsync(userManager, roleManager);
        await DeveloperUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {

    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseErrorHandlingMiddleware();
app.UseHealthChecks("/sedisHealth");
app.UseSession();
app.UseHealthChecks("/health");
app.MapControllers();

app.Run();
