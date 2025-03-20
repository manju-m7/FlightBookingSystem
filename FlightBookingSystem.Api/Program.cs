using FlightBookingSystem.Application.Mappings;
using FlightBookingSystem.Application.Queries;
using FlightBookingSystem.Domain.Repositories;
using FlightBookingSystem.Domain.SeedWork;
using FlightBookingSystem.Infrastructure.MessageBroker;
using FlightBookingSystem.Infrastructure.Persistence;
using FlightBookingSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FlightBookingSystem.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateFlightCommandValidator>()); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllFlightsQuery).Assembly));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<FlightBookingDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("FlightConnectionString")));
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<FlightBookingDbContext>());
builder.Services.AddSingleton<RabbitMqPublisher>();
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMQ"));

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
