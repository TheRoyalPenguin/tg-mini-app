using System.Text;
using API.Configurations;
using API.Extensions;
using Application.Services;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Models;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio;
using Persistence;
using Persistence.MinioRepositories;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddSingleton<ICacheService, RedisCacheService>();

var jwtSection = builder.Configuration.GetSection("Jwt");

var keyString = jwtSection["Key"];
if (string.IsNullOrEmpty(keyString))
    throw new InvalidOperationException("JWT Key отсутствует в конфигурации!");

var key = Encoding.UTF8.GetBytes(keyString);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            // Если нужно будет проверять Issuer/Audience
            // ValidateIssuer           = true,
            // ValidIssuer             = issuer,
            // ValidateAudience         = true,
            // ValidAudience           = audience,

            ValidateIssuer = false,  // пока не используем
            ValidateAudience = false,  // пока не используем

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Добавляем JWT-авторизацию
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\""
    });

    // Требуем JWT для всех запросов (или можно настроить точечно)
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://frontend:3000", "https://levelupapp.hopto.org/", "https://levelupapp.hopto.org");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<ITestRepository, TestRepository>();

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

builder.Services.AddScoped<ITelegramAuthService, TelegramAuthService>();
builder.Services.AddScoped<ITelegramUserRepository, TelegramUserRepository>();

builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IModuleService, ModuleService>();

builder.Services.AddScoped<IModuleAccessRepository, ModuleAccessRepository>();
builder.Services.AddScoped<IModuleAccessService, ModuleAccessService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CoursesService>();

builder.Services.AddScoped<ITestingRepository, TestingRepository>();
builder.Services.AddScoped<ITestingService, TestingService>();

builder.Services.AddPostgresDb(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSingleton<IMinioClient>(sp =>
{
    var config = builder.Configuration.GetSection("Minio");
    var endpoint = config["Endpoint"];
    var accessKey = config["AccessKey"];
    var secretKey = config["SecretKey"];
    var useSsl = bool.Parse(config["UseSSL"] ?? "false");

    return new MinioClient()
        .WithEndpoint(endpoint)
        .WithCredentials(accessKey, secretKey)
        .WithSSL(useSsl)
        .Build();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Автоматическое применение миграций и добавление дефолтной роди
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
    
    var roleRepo = scope.ServiceProvider.GetRequiredService<IRoleRepository>();
    await roleRepo.AddAsync(new Role()
    {
        Name = "User",
        RoleLevel = 0
    });
}


app.UseCors();

app.UseAuthentication();  // происходит проверка JWT
app.UseAuthorization();   // применяется [Authorize]

app.MapControllers();

app.Run();
