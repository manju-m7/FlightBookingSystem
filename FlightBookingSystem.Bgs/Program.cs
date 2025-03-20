using FlightBookingSystem.Bgs;
using FlightBookingSystem.Infrastructure.MessageBroker;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddHostedService<RabbitMqConsumerService>();

var app  = builder.Build();
app.Run();