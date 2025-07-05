using Microsoft.EntityFrameworkCore;
using Reservation.Features.Room.Services;
using Reservation.Infrastructure.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReservationDbContext>(configure =>
{
    configure.UseSqlServer(builder.Configuration.GetConnectionString(ReservationDbContext.DefaultConnectionStringName));
});


builder.Services.AddScoped<RoomService>();

var app = builder.Build();

app.Run();
