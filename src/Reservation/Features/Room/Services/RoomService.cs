using Microsoft.EntityFrameworkCore;
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

        public async Task Create(CreateRoomRequestDto dto , CancellationToken cancellationToken)
        {
            var newRoom = Domain.Entities.Room.Create(dto.name);

            _reservationDbContext.Rooms.Add(newRoom);
            await _reservationDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<GetRoomResponseDto>> GetAll(CancellationToken cancellationToken) 
        {
            return (await _reservationDbContext.Rooms.ToListAsync(cancellationToken)).Select(a => new GetRoomResponseDto
            (
                a.Id,
                a.Name
            ));
        
        }
    }
}
