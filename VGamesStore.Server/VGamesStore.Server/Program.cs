using Microsoft.EntityFrameworkCore;
using VGameStore.Application.Interfaces;
using VGameStore.Application.Mappings;
using VGameStore.Application.Repositories;
using VGameStore.Application.Services;
using VGameStore.Infrastructure.Persistence;
using VGameStore.Persistence;

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

builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(GameProfile));
builder.Services.AddAutoMapper(typeof(CategoryProfile));

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
