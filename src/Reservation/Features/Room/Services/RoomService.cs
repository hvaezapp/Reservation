using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Reservation.Features.Room.CreateRoom.Dtos;
using Reservation.Features.Room.GetAllRoom.Dtos;
using Reservation.Infrastructure.Persistence.Context;

namespace Reservation.Features.Room.Services
{
    public class RoomService(ReservationDbContext reservationDbContext, IValidator<CreateRoomRequestDto> validator)
    {
        private readonly ReservationDbContext _reservationDbContext = reservationDbContext;
        private readonly IValidator<CreateRoomRequestDto> _validator = validator;


        public async Task Create(CreateRoomRequestDto dto, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ArgumentException($"Validation Failed , Message => {validationResult.Errors.Select(s => s.ErrorMessage).First()} ", nameof(dto));

            var newRoom = Domain.Entities.Room.Create(dto.Name);

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
