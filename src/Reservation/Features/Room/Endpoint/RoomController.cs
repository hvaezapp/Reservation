using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.Features.Room.Dtos;
using Reservation.Features.Room.Services;

namespace Reservation.Features.Room.Endpoint
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        public readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomRequestDto dto , CancellationToken cancellationToken)
        {
            await _roomService.CreateRoom(dto, cancellationToken);
            return Ok();
        }
    }
}
