using API.Extensions;
using API.Middlewares;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

//applying migration if database doesn't exist
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ShopContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
if (await context.Database.EnsureCreatedAsync() == false)
{
    try
    {
        await context.Database.MigrateAsync();
        await ShopContextSeed.SeedAsync(context);

    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Migration failed");
    }
}

app.Run();
