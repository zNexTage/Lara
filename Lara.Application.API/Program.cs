using System.Reflection;
using dotenv.net;
using Lara.Application.API.Extesions;
using Lara.Data.Repository;
using Lara.Domain.Contracts;
using Lara.Service.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo() {Title = "LARA - Biblioteca virtual", Description = "API para gerenciamento de biblioteca"});

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllers();

builder.Services.AddDbContext<PgSqlContext>(opts =>
{
    DotEnv.Load();

    var envs = DotEnv.Read();

    var user = envs["LARA_DB_USER_ID"];
    var password = envs["LARA_DB_PASSWORD"];
    var host = envs["LARA_DB_HOST"];
    var port = envs["LARA_DB_PORT"];
    var dbName = envs["LARA_DB_NAME"];
    
    opts.UseNpgsql($"User ID={user};Password={password};Host={host};Port={port};Database={dbName};");
});

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<PgSqlContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

// Configuração do JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        DotEnv.Load();
        
        var envVars = DotEnv.Read();

        var jwtKey = envVars["LARA_JWT_KEY"];
        
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

app.Run();