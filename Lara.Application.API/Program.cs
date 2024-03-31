using System.Reflection;
using System.Security.Claims;
using dotenv.net;
using Lara.Application.API.Extesions;
using Lara.Data.Repository;
using Lara.Domain.Contracts;
using Lara.Service.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using Lara.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//Carrega as variáveis de ambiente.
DotEnv.Load();
var envVars = DotEnv.Read();

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo() {Title = "LARA - Biblioteca virtual", Description = "API para gerenciamento de biblioteca"});

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
    
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "Autenticação via token jwt",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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

var user = envVars["LARA_DB_USER_ID"];
var password = envVars["LARA_DB_PASSWORD"];
var host = envVars["LARA_DB_HOST"];
var port = envVars["LARA_DB_PORT"];
var dbName = envVars["LARA_DB_NAME"];

var connectionString = $"User ID={user};Password={password};Host={host};Port={port};Database={dbName};";

builder.Services.AddDbContext<PgSqlContext>(opts =>
{
    opts.UseNpgsql(connectionString);
    
});

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<PgSqlContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped<BorrowedBookService>();
builder.Services.AddScoped<BorrowedBookRepository>();

builder.Services.AddScoped<UserService>();

var jwtKey = envVars["LARA_JWT_KEY"];
builder.Services.AddScoped<IBaseTokenService, JwtService>(serviceProvider =>
{
    var manager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    
    return new JwtService(jwtKey, manager);
});

// Configuração do JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        
        opts.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = symmetricKey,
            ClockSkew = TimeSpan.Zero
        };
    });

// Adiciona regras de autoriazação.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim(claimType: ClaimTypes.Role, "ADMIN"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.AddRoles();

var adminPass = envVars["LARA_ADMIN_USER_PASSWORD"];
app.AddAdminUser(adminPass);

app.UseAuthentication();

app.UseAuthorization();

app.Run();