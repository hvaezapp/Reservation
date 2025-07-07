using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using Reservation.Features.Order.Services;
using Reservation.Features.Room.Services;
using Reservation.Infrastructure.Persistence.Context;
using StackExchange.Redis;
using System.Reflection;

namespace Reservation.Bootstraper
{
    public static class ServiceRegistration
    {
        public static void RegisterCommon(this WebApplicationBuilder builder)
        {

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();


            #region fluent validaion
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            #endregion

        }

        public static void RegisterMssql(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ReservationDbContext>(configure =>
            {
                configure.UseSqlServer(builder.Configuration.GetConnectionString(ReservationDbContext.DefaultConnectionStringName));
            });
        }

        public static void RegisterIoc(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<RoomService>();
            builder.Services.AddScoped<OrderService>();
        }

        public static void RegisterRedis(this WebApplicationBuilder builder)
        {
            var RedisConnectionString = builder.Configuration.GetConnectionString("Redis");
            if (string.IsNullOrEmpty(RedisConnectionString))
                throw new InvalidOperationException("This Redis connection string is missing or empty");
            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(RedisConnectionString));
        }

        public static void RegisterRedLock(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton(sp =>
            {
                var connectionMultiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
                var lockMultiplexer = new RedLockMultiplexer(connectionMultiplexer);
                return RedLockFactory.Create([lockMultiplexer]);
            });
        }
    }

}
