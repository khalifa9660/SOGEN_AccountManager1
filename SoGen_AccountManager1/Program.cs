using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SoGen_AccountManager1.Data;
using SoGen_AccountManager1.Repositories.Interface;
using SoGen_AccountManager1.Repositories.Implementation;
using SoGen_AccountManager1.Interface;
using SoGen_AccountManager1.Services;
using SoGen_AccountManager1.Models.Domain;

var builder = WebApplication.CreateBuilder(args);

// Récupération de la chaîne de connexion
string connectionString;
if (builder.Environment.IsDevelopment()) 
{
    // Utiliser LocalConnection pour le développement
    connectionString = builder.Configuration.GetConnectionString("LocalConnection");
}
else
{
    // Utiliser DATABASE_URL pour la production (Heroku)
    var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (!string.IsNullOrEmpty(databaseUrl))
    {
        // Parser DATABASE_URL
        var uri = new Uri(databaseUrl);
        var db = uri.AbsolutePath.Trim('/');
        var user = uri.UserInfo.Split(':')[0];
        var passwd = uri.UserInfo.Split(':')[1];
        var port = uri.Port > 0 ? uri.Port : 3306;
        connectionString = $"Server={uri.Host};Port={port};Database={db};User={user};Password={passwd};SSL Mode=Required;";
    }
    else
    {
        // Fallback sur DefaultConnection si DATABASE_URL n'est pas défini
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    }
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration JWT
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? 
    builder.Configuration.GetSection("JwtConfig:Secret").Value;
builder.Services.Configure<JwtConfig>(options => options.Secret = jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(jwtSecret);
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

// Configuration CORS
var allowedOrigins = (Environment.GetEnvironmentVariable("ALLOWED_ORIGINS") ?? "http://localhost:4200,https://sogen-front1-1.onrender.com").Split(',');
builder.Services.AddCors(options => options.AddPolicy("FrontEnd", policy =>
{
    policy.WithOrigins(allowedOrigins)
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials();
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