using System.Text;
using API.Configurations;
using API.Extensions;
using Application.Services;
using Core.Interfaces;
using Core.Interfaces.Services;
using Core.Models;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Repositories;
using Persistence.Converter;
using FluentValidation.AspNetCore;
using API.Validators;
using Persistence.MinioRepositories;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSeqLogging();

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
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<CreateLongreadDtoValidator>());
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000","http://localhost:3001" ,"http://frontend:3000","http://frontend:3001", "https://levelupapp.hopto.org/", "https://levelupapp.hopto.org");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<ITestRepository, TestRepository>();

builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStatisticService, StatisticService>();
builder.Services.AddScoped<ITestResultRepository, TestResultRepository>();

builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();

builder.Services.AddScoped<ITelegramAuthService, TelegramAuthService>();
builder.Services.AddScoped<ITelegramUserRepository, TelegramUserRepository>();

builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IModuleService, ModuleService>();

builder.Services.AddScoped<IModuleAccessRepository, ModuleAccessRepository>();
builder.Services.AddScoped<IModuleAccessService, ModuleAccessService>();

// builder.Services.AddScoped<IModuleActivityService, ModuleActivityService>();
// builder.Services.AddHostedService<ModuleActivityBackgroundService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CoursesService>();

builder.Services.AddScoped<ITestingRepository, TestingRepository>();
builder.Services.AddScoped<ITestingService, TestingService>();

builder.Services.AddScoped<ILongreadRepository, LongreadRepository>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<ILongreadService, LongreadService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddSingleton<DocxConverter>();
builder.Services.AddScoped<ILongreadConverter, LongreadConverter>();

builder.Services.AddPostgresDb(builder.Configuration);
builder.Services.AddMinio(builder.Configuration);
builder.Services.AddRedis(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile));

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

    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    try
    {
        await uow.Roles.AddAsync(new Role()
        {
            Name = "User",
            RoleLevel = 0
        });
        await uow.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}


app.UseCors();

app.UseAuthentication();  // происходит проверка JWT
app.UseAuthorization();   // применяется [Authorize]

app.MapControllers();

app.Run();