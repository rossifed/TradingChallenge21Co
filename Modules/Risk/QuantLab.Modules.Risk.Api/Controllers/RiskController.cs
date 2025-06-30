using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuantLab.Shared.Abstractions.Dispatchers;
using Swashbuckle.AspNetCore.Annotations;
namespace QuantLab.Modules.Risk.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class RiskController : Controller
    {
        private IDispatcher Dispatcher { get; }

        public RiskController(IDispatcher dispatcher)
        {
            Dispatcher = dispatcher;

        }

        //[HttpPost()]
        //[Route("Check Risk")]
        //[SwaggerOperation("CheckRisk")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult> PlaceOrder()
        //{

        //    return NoContent();
        //}
    }
}
