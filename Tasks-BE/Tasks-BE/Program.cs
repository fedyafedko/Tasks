using Microsoft.EntityFrameworkCore;
using Tasks.DAL.EF;
using Tasks.DAL.Repositories.Interfaces;
using Tasks.DAL.Repositories;
using Tasks_BE.Middlewares;
using Tasks.Common.Extensions;
using Tasks.Common.Configs;
using Tasks.BLL.Profiles;
using Microsoft.AspNetCore.Identity;
using Tasks.Entities;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Tasks_BE.Utility;
using Tasks.BLL.Services.Interfaces;
using Tasks.BLL.Services;
using Tasks_BE.Validators.Auth;
using FluentValidation.AspNetCore;
using Tasks_BE.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configs
builder.Services.ConfigsAssembly(builder.Configuration, opt => opt
       .AddConfig<JwtConfig>());

builder.Services.AddAutoMapper(typeof(AuthProfile));

builder.Services.AddControllers(cfg => cfg.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddControllers(options => options
    .Conventions
    .Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())));

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Identity
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtConfig:Secret")!)),
    ValidateIssuer = false,
    ValidateAudience = false,
    RequireExpirationTime = false,
    ValidateLifetime = true
};
builder.Services.AddAuthentication(configureOptions: x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.SaveToken = true;
        x.TokenValidationParameters = tokenValidationParameters;
    });

builder.Services.AddSingleton(tokenValidationParameters);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        }
    );
    c.MapType<TimeSpan>(() => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("00:00:00")
    });
});

// CORS
builder.Services.AddCors(options => options
    .AddDefaultPolicy(build => build
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MigrateDatabase();

app.UseHttpsRedirection();
app.UseCors(
    opt => opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
