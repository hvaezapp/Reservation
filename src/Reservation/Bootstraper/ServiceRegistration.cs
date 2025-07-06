using Microsoft.EntityFrameworkCore;
using Reservation.Features.Room.Services;
using Reservation.Infrastructure.Persistence.Context;

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
            #endregion

            #region commons
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            #endregion
        }

    }
}
