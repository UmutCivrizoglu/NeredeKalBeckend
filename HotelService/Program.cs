using System.Reflection;
using Application.HotelService.Commands.CreateHotel;
using Application.HotelService.Queries.GetAllHotelManagers;
using Application.Report;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MediatR;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HotelDbContext>();
builder.Services.AddMediatR(typeof(CreateHotelCommand).GetTypeInfo().Assembly);
builder.Services.AddTransient(typeof(IHotelRepository), typeof(HotelRepository));
builder.Services.AddTransient(typeof(IReportService), typeof(ReportService));
builder.Services.AddTransient<ReportDetailProducer>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ReportConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("ReportRequest", e =>
        {
            e.ConfigureConsumer<ReportConsumer>(context);
           
        });
    });
});
builder.Services.AddHttpClient<ReportService>();
builder.Services.AddMassTransitHostedService();
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