using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuantLab.Modules.StateTracking.Application.Services;
using Swashbuckle.AspNetCore.Annotations;
namespace QuantLab.Modules.StateTrackingC.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class StateTrackingController : Controller
    {
        private ITradeCaptureService TradeCaptureService { get; }//should use dispatcher, relly bad

        public StateTrackingController(ITradeCaptureService tradeCaptureService)
        {
            TradeCaptureService = tradeCaptureService;
        }

        [HttpPost()]
        [Route("Export Trades")]
        [SwaggerOperation("Export Trades To CSV")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ExportTrade(string path = "C:/Temp/TradeExport.csv")
        {
            await TradeCaptureService.ExportTradesAsync(path);
            return NoContent();
        }
    }
}
