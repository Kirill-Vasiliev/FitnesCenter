using System.Text.Json.Serialization;
using FitnesCenter.API.Middleware;
using FitnesCenter.Application.Services;
using FitnesCenter.Domain.Interfaces;
using FitnesCenter.Infrastructure.Data;
using FitnesCenter.Infrastructure.Repositories.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =============================================
// œŒƒ Àﬁ◊≈Õ»≈ ¡¿«€ ƒ¿ÕÕ€’
// =============================================

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// =============================================
// –≈√»—“–¿÷»ﬂ –≈œŒ«»“Œ–»≈¬
// =============================================

builder.Services.AddScoped<IClientRepository, EfClientRepository>();
builder.Services.AddScoped<ITrainerRepository, EfTrainerRepository>();
builder.Services.AddScoped<ILockerRepository, EfLockerRepository>();
builder.Services.AddScoped<IServiceRepository, EfServiceRepository>();

// =============================================
// –≈√»—“–¿÷»ﬂ —≈–¬»—Œ¬
// =============================================

builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<TrainerService>();

var app = builder.Build();

// =============================================
// œ–»Ã≈Õ≈Õ»≈ Ã»√–¿÷»… ¿¬“ŒÃ¿“»◊≈— »
// =============================================

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();