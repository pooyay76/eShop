using API.Extensions;
using API.Middlewares;
using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerServices();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
}

app.UseStaticFiles();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//applying migration if database doesn't exist
using var scope = app.Services.CreateScope();
var shopContext = scope.ServiceProvider.GetRequiredService<ShopContext>();

var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
if (await shopContext.Database.EnsureCreatedAsync() == false)
{
    try
    {
        await shopContext.Database.MigrateAsync();
        await ShopContextSeed.SeedAsync(shopContext);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Migration failed");
    }
}
var identityContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
if (await identityContext.Database.EnsureCreatedAsync() == false)
{
    try
    {
        await identityContext.Database.MigrateAsync();

        await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Migration failed");
    }
}


app.Run();
