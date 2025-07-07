using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Reservation.Features.Order.Services;
using Reservation.Features.Room.Services;
using Reservation.Infrastructure.Persistence.Context;
using System.Reflection;

namespace Reservation.Bootstraper
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            #region database
            builder.Services.AddDbContext<ReservationDbContext>(configure =>
            {
                configure.UseSqlServer(builder.Configuration.GetConnectionString(ReservationDbContext.DefaultConnectionStringName));
            });
            #endregion

            #region ioc
            builder.Services.AddScoped<RoomService>();
            builder.Services.AddScoped<OrderService>();
            #endregion

            #region commons
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            #endregion

            #region fluent validaion
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            #endregion
        }

    }
}
