using System.Reflection;
using dotenv.net;
using Lara.Data.Repository;
using Lara.Domain.Contracts;
using Lara.Domain.Entities;
using Lara.Service.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();