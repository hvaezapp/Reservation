using Microsoft.AspNetCore.Mvc;
using Reservation.Common;
using Reservation.Features.Room.Services;

namespace Reservation.Features.Room.GetAllRoom.Endpoint
{
    public class GetAllRoom : BaseEndpoint
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(RoomService _roomService, CancellationToken cancellationToken)
        {
            return Ok(await _roomService.GetAll(cancellationToken));
        }
    }
}
