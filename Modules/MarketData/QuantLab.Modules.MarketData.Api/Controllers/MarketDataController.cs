using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuantLab.Modules.MarketData.Application.Commands;
using QuantLab.Modules.MarketData.Application.Commands.In;
using QuantLab.Shared.Abstractions.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;
namespace QuantLab.Modules.MarketData.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class MarketDataController : Controller
    {
        private IDispatcher Dispatcher { get; }

        public MarketDataController(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;

        }

        [HttpPost()]
        [Route("BestBidOffer/Subscribe")]
        [SwaggerOperation("Subscribe To  BestBidOffer Feed")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SubscribeToBestBidOffer(string symbol = "BTCUSDT")
        {
            await Dispatcher.SendAsync(new SubscribeToBestBidOffer(symbol));
            return NoContent();
        }


        [HttpPost()]
        [Route("BestBidOffer/Unsubscribe")]
        [SwaggerOperation("Unsubscribe To  BestBidOffer Feed")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UnSubscribeToBestBidOffer()
        {
            await Dispatcher.SendAsync(new UnsubscribeToBestBidOffer());
            return NoContent();
        }
    }
}
