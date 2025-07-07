using Microsoft.AspNetCore.Mvc;
using Reservation.Common;
using Reservation.Features.Order.Services;

namespace Reservation.Features.Order.GetAllOrder.Endpoint
{
    public class GetAllOrder : BaseEndpoint
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(OrderService _orderService, CancellationToken cancellationToken)
        {
            return Ok(await _orderService.GetAll(cancellationToken));
        }
    }
}
