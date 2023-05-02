

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TrainBooking.DBOperations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TrainBookingDbContext>(options => options.UseInMemoryDatabase(databaseName: "TrainBookingDB", b => b.EnableNullChecks(false)));
builder.Services.AddScoped<ITrainBookingDbContext>(provider => provider.GetService<TrainBookingDbContext>());
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
  
}

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
