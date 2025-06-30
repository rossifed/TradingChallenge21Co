using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuantLab.Modules.Orders.Application.Commands.In;
using QuantLab.Shared.Abstractions.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;
namespace QuantLab.Modules.Orders.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class OrdersController : Controller
    {
        private IDispatcher Dispatcher { get; }

        public OrdersController(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;

        }

        [HttpPost()]
        [Route("PlaceOrder")]
        [SwaggerOperation("PlaceOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PlaceOrder(PlaceOrder commmand)
        {
            await Dispatcher.SendAsync(commmand);
            return NoContent();
        }
    }
}
