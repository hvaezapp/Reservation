using Reservation.Domain.Entities;
using Reservation.Features.Room.Dtos;
using Reservation.Infrastructure.Persistence.Context;


namespace Reservation.Features.Room.Services
{
    public class RoomService
    {
        private readonly ReservationDbContext _reservationDbContext;
        public RoomService(ReservationDbContext reservationDbContext)
        {
            _reservationDbContext = reservationDbContext;
        }

        public async Task CreateRoom(CreateRoomRequestDto dto , CancellationToken cancellationToken)
        {
            var newRoom = new Domain.Entities.Room(dto.Name);
            _reservationDbContext.Rooms.Add(newRoom);
            await _reservationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
