using Walk_Tracker.Data;
using Walk_Tracker.Repository;
using Walk_Tracker.Interfaces;
using Microsoft.EntityFrameworkCore;
using Walk_Tracker.Bot;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<WalkTrackerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITrackLocationRepository, TrackLocationRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();
builder.Services.AddSingleton(new WalkTrackerBotClient("50eaa95e35e7dcd4-a761534226845886-b5133bcb1c1e0d21"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
