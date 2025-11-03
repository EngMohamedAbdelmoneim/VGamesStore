using Microsoft.EntityFrameworkCore;
using VGameStore.Application.Interfaces;
using VGameStore.Application.Mappings;
using VGameStore.Application.Repositories;
using VGameStore.Application.Services;

using VGameStore.Infrastructure.Services;
using VGameStore.Infrastructure.RedisServices;
using VGameStore.Infrastructure.Persistence.GameStoreDb;
using VGameStore.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using VGameStore.Infrastructure.Persistence.Identitydb;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using VGameStore.Application.Interfaces.AuthInterfaces;
using VGameStore.Application.Services.AuthServices;
using StackExchange.Redis;
using Microsoft.OpenApi.Models; // Add this namespace for AddStackExchangeRedisCache

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Enter: Bearer <your_token>"
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
			new string[] {}
		}
	});
});

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
	options.AddPolicy(MyAllowSpecificOrigins, policy =>
	{
		policy.AllowAnyOrigin()
		.AllowAnyMethod()
		.AllowAnyHeader();  // السماح بكل الـ Headers
	});
});

builder.Services.AddControllers(
	options =>
	{
		options.Filters.Add( new Microsoft.AspNetCore.Mvc.Authorization.AuthorizeFilter());
	});

// Add Persistence Services
builder.Services.AddPersistenceServices(builder.Configuration);

// Add Identity Persistence Services
builder.Services.AddIdentityPersistenceServices(builder.Configuration);

// Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
	.AddEntityFrameworkStores<VGameStoreIdentityDbContext>()
	.AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = true;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
});
// JWT Authentication
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ClockSkew = TimeSpan.Zero,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidAudience = builder.Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(
		Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
	)
	};
});
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the generic repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register the specific repository and service
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();

// Register the services
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IUserService, UserService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(GameProfile));
builder.Services.AddAutoMapper(typeof(GenreProfile));


// Add Redis services
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddScoped<ICartService, RedisCartService>();
builder.Services.AddScoped<IWishlistService, RedisWishlistService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);  // ✅ MUST COME BEFORE AUTH

app.UseAuthentication();              // ✅ Must be before UseAuthorization
app.UseAuthorization();

app.MapControllers();                 // ✅ After Auth

app.Run();
