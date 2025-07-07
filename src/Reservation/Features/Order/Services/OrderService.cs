using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.EntityFrameworkCore;
using RedLockNet.SERedis;
using Reservation.Features.Order.CreateOrder.Dtos;
using Reservation.Features.Order.GetAllOrder.Dtos;
using Reservation.Infrastructure.Persistence.Context;

namespace Reservation.Features.Order.Services
{
    public class OrderService(ReservationDbContext reservationDbContext , RedLockFactory redLockFactory)
    {
        private readonly ReservationDbContext _reservationDbContext = reservationDbContext;
        private readonly RedLockFactory _redLockFactory = redLockFactory;


        public const string CreateOrderDistributedLockPattern = "reservation:order:create:{0}";
        public static TimeSpan ExpireTime = TimeSpan.FromSeconds(20);
        public static TimeSpan WaitTime = TimeSpan.FromSeconds(5);
        public static TimeSpan RetryTime = TimeSpan.FromSeconds(3);


        public async Task Create(CreateOrderRequestDto dto, CancellationToken cancellationToken)
        {
            var resource = string.Format(CreateOrderDistributedLockPattern , dto.RequesterNationalCode);

            await using (var redLock = await _redLockFactory.CreateLockAsync(resource , ExpireTime , WaitTime , RetryTime, cancellationToken))
            {
                if (!redLock.IsAcquired)
                    throw new Exception("Unexpected Error");

                var room = await _reservationDbContext.Rooms.FindAsync(dto.RoomId, cancellationToken);

                if (room is null)
                    throw new Exception("Selected Room Not Found, RoomId Invalid");

                if (room.IsReserved)
                    throw new Exception("Requester Can't Reserve this Room, Becuase this Room Reserved Before");


                var newOrder = Domain.Entities.Order.Create(dto.RequesterName, dto.RequesterPhoneNom,
                                                            dto.RequesterNationalCode, dto.FromDate,
                                                            dto.ToDate, dto.RoomId);

                _reservationDbContext.Orders.Add(newOrder);

                room.Reserve();

                await _reservationDbContext.SaveChangesAsync(cancellationToken);

            }
        }

        public async Task<IEnumerable<GetOrderResponseDto>> GetAll(CancellationToken cancellationToken)
        {
            return (await _reservationDbContext.Orders.Include(a => a.Room).ToListAsync(cancellationToken)).Select(a => 
            new GetOrderResponseDto
            (
                a.RequesterName,
                a.RequesterPhoneNom ,
                a.RequesterNationalCode , 
                a.FromDate ,
                a.ToDate,
                a.RoomId ,
                a.Room.Name
            ));
        }
    }
}
