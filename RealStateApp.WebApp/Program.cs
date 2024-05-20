using Microsoft.AspNetCore.Identity;
using RealStateApp.Infrastructure.Identity.Entities;
using RealStateApp.Infrastructure.Identity.IOC;
using RealStateApp.Infrastructure.Identity.Seeds;
using RealStateApp.Infrastructure.Persistence.IOC;
using RealStateApp.Infrastructure.Shared.IOC;
using RealStateApp.WebApp.Middelwares;
using RealStateApp.Core.Application.IOC;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSession();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructureForWeb(builder.Configuration);
builder.Services.AddSharedInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddScoped<LoginAuthorize>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ValidateUserSession, ValidateUserSession>();

var app = builder.Build();

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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
