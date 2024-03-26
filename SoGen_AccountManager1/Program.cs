using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SoGen_AccountManager1.Data;
using SoGen_AccountManager1.Repositories.Interface;
using SoGen_AccountManager1.Repositories.Implementation;
using SoGen_AccountManager1.Interface;
using SoGen_AccountManager1.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using SoGen_AccountManager1.Models.Domain;

var builder = WebApplication.CreateBuilder(args);

// Récupération de la chaîne de connexion de JAWSDB_JADE_URL pour Heroku, sinon utilisation de DefaultConnection
var connectionString = Environment.GetEnvironmentVariable("JAWSDB_JADE_URL")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(key: "JwtConfig"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = true,
        ValidateLifetime = true
    };
});

builder.Services.AddCors(options => options.AddPolicy("FrontEnd", policy =>
{
    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    policy.WithOrigins("https://sogen-front1-1.onrender.com").AllowAnyHeader().AllowAnyMethod();
}));

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IApiService, ApiService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("FrontEnd");

app.MapControllers();

app.Run();
