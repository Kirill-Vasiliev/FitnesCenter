using FitnesCenter.API.Middleware;
using FitnesCenter.Application.Services;
using FitnesCenter.Domain.Interfaces;
using FitnesCenter.Infrastructure.Repositories.InMemory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ? ИСПРАВЛЕНО: Используем Singleton вместо Scoped
builder.Services.AddSingleton<IClientRepository, InMemoryClientRepository>();
builder.Services.AddSingleton<ITrainerRepository, InMemoryTrainerRepository>();

builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<TrainerService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();