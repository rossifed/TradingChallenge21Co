using QuantLab.Modules.StateTracking.Application.Dtos;

namespace QuantLab.Modules.StateTracking.Application.Services
{
    internal interface ITradeCaptureService
    {
        Task OnNewTradeAsync(TradeDto tradeDto);//not good but lack of time so direct usafe of dto for simple capture

        Task ExportTradesAsync(string path);
    }
}
