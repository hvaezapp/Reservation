using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation.Features.Room.Dtos;
using Reservation.Features.Room.Services;

namespace Reservation.Features.Room.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomApiController : ControllerBase
    {
        public readonly RoomService _roomService;

        public RoomApiController(RoomService roomService)
        {
            _roomService = roomService;
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetlAll(CancellationToken cancellationToken)
        {
            return Ok(await _roomService.GetAll(cancellationToken));
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateRoomRequestDto dto , CancellationToken cancellationToken)
        {
            await _roomService.Create(dto, cancellationToken);
            return Ok();
        }
    }
}
