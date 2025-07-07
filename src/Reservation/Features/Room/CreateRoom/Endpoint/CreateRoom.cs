using Microsoft.AspNetCore.Mvc;
using Reservation.Common;
using Reservation.Features.Room.CreateRoom.Dtos;
using Reservation.Features.Room.Services;

namespace Reservation.Features.Room.CreateRoom.Endpoint
{
    public class CreateRoom : BaseEndpoint
    {
        [HttpPost]
        public async Task<IActionResult> Create(RoomService _roomService, CreateRoomRequestDto dto, CancellationToken cancellationToken)
        {
            await _roomService.Create(dto, cancellationToken);
            return Ok();
        }
    }
}
