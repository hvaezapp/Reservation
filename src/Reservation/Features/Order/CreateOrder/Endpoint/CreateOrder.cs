using Microsoft.AspNetCore.Mvc;
using Reservation.Common;
using Reservation.Features.Order.CreateOrder.Dtos;
using Reservation.Features.Order.Services;

namespace Reservation.Features.Order.CreateOrder.Endpoint
{
    public class CreateOrder : BaseEndpoint
    {
        [HttpPost]
        public async Task<IActionResult> Create(OrderService _orderService, CreateOrderRequestDto dto, CancellationToken cancellationToken)
        {
            await _orderService.Create(dto, cancellationToken);
            return Ok();
        }
    }
}
