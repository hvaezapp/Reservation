using Reservation.Features.Order.CreateOrder.Dtos;
using Reservation.Infrastructure.Persistence.Context;

namespace Reservation.Features.Order.Services
{
    public class OrderService(ReservationDbContext reservationDbContext)
    {
        private readonly ReservationDbContext _reservationDbContext = reservationDbContext;

        public async Task Create(CreateOrderRequestDto dto, CancellationToken cancellationToken)
        {

            var room = await _reservationDbContext.Rooms.FindAsync(dto.RoomId, cancellationToken);

            if (room is null)
                throw new Exception("Selected Room Not Found, RoomId Invalid ");

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
}
