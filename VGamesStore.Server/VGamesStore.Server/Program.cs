using Microsoft.EntityFrameworkCore;
using VGameStore.Application.Interfaces;
using VGameStore.Application.Mappings;
using VGameStore.Application.Repositories;
using VGameStore.Application.Services;
using VGameStore.Infrastructure.Persistence;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using VGameStore.Infrastructure.Services;
using VGameStore.Infrastructure.RedisServices; // Add this namespace for AddStackExchangeRedisCache

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers();
builder.Services.AddPersistenceServices(builder.Configuration);
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


// Add AutoMapper
builder.Services.AddAutoMapper(typeof(GameProfile));
builder.Services.AddAutoMapper(typeof(GenreProfile));


// Add Redis services
builder.Services.AddScoped<ICartService, RedisCartService>();
builder.Services.AddScoped<IWishlistService, RedisWishlistService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
	options.Configuration = builder.Configuration.GetConnectionString("Redis");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
