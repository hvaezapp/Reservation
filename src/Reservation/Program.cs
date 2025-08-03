using Reservation.Bootstraper;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterCommon();
builder.RegisterMssql();
builder.RegisterIoc();
builder.RegisterRedis();
builder.RegisterRedLock();
builder.RegisterBroker();
builder.RegisterHostedService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();

public partial class Program { }