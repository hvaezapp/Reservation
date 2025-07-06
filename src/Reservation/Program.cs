using Reservation.Bootstraper;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices();

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
