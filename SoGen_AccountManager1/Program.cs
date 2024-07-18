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

// Récupération de la chaîne de connexion de JAWSDB_JADE_URL pour Heroku, sinon utilisation de DefaultConnection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    // ?? builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};"
                     + $"port={Environment.GetEnvironmentVariable("DB_PORT")};"
                     + $"Database={Environment.GetEnvironmentVariable("DB_NAME")};"
                     + $"user={Environment.GetEnvironmentVariable("DB_USER")};"
                     + $"password={Environment.GetEnvironmentVariable("DB_PASSWORD")};"
                     + "Persist security Info=true;";
}

builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 31))));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

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
    policy.WithOrigins("http://localhost:4200", "https://sogen-front1-1.onrender.com")
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
