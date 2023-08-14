using Microsoft.EntityFrameworkCore;
using MovieApi.Models;
using MovieApi.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<MoviesRepo>();

string connStr = builder.Configuration.GetConnectionString("MovieContext");


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MovieContext>(options => options.UseSqlServer(connStr));



// builder.Services.AddCors(options =>
// {
//     options.AddDefaultPolicy(
//         policy =>
//         {
//             policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
//         });
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
