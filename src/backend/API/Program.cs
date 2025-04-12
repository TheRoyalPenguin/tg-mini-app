using API.Extensions;
using Application.Services;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<ITestRepository, TestRepository>();

builder.Services.AddPostgresDb(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Автоматическое применение миграций
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseCors();
app.MapControllers();

app.Run();