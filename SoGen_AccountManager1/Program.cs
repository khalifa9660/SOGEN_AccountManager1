using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SoGen_AccountManager1.Data;
using SoGen_AccountManager1.Repositories.Implementation;
using SoGen_AccountManager1.Services;
using SoGen_AccountManager1.Models.Domain;
using SoGen_AccountManager1.Repositories.Interface.IRepository;
using SoGen_AccountManager1.Repositories.Interface.IService;
using SoGen_AccountManager1.Services.PlayerService;
using SoGen_AccountManager1.Interface.IExternalApi;
using SoGen_AccountManager1.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Récupération de la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString("use_env_variable")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};"
                     + $"port={Environment.GetEnvironmentVariable("DB_PORT")};"
                     + $"Database={Environment.GetEnvironmentVariable("DB_NAME")};"
                     + $"user={Environment.GetEnvironmentVariable("DB_USER")};"
                     + $"password={Environment.GetEnvironmentVariable("DB_PASSWORD")};"
                     + "Persist security Info=true;";
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration JWT
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
    var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
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

// Enregistrement des services
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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
