using Microsoft.EntityFrameworkCore;
using Reservation.Infrastructure.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ReservationDbContext>(configure =>
{
    configure.UseSqlServer(builder.Configuration.GetConnectionString(ReservationDbContext.DefaultConnectionStringName));
});

var app = builder.Build();

app.Run();
