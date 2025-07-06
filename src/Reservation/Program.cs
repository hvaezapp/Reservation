using Microsoft.EntityFrameworkCore;
using Reservation.Features.Room.Services;
using Reservation.Infrastructure.Persistence.Context;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReservationDbContext>(configure =>
{
    configure.UseSqlServer(builder.Configuration.GetConnectionString(ReservationDbContext.DefaultConnectionStringName));
});


builder.Services.AddScoped<RoomService>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

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
