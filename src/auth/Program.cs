using AuthApi.Domains.Interfaces;
using AuthApi.Infra.Repositorys.Base;
using AuthApi.Infra.Repositorys.SqlServer;
using AuthApi.Services;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using AuthApi.Infra.Repositorys.SqlServer.DapperConfig;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var stringConexao = configuration.GetValue<string>("ConnectionStringSQL");
builder.Services.AddScoped<IDbConnection>((conexao) => new SqlConnection(stringConexao));
builder.Services.AddScoped<IUow, Uow>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDapperMapper();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Markplace", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Produto API V1");
    });
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
