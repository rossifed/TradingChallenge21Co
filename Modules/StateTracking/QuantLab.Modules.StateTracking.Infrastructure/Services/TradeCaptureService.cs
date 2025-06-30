using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuantLab.Modules.StateTracking.Application.Dtos;
using QuantLab.Modules.StateTracking.Application.Services;
using QuantLab.Modules.StateTracking.Infrastructure.Entities;
using QuantLab.Modules.StateTracking.Infrastructure.Mappers;
using System.Text;

namespace QuantLab.Modules.StateTracking.Infrastructure.Services
{

    //I use a simple dummy caching approach to avoid EF complexity and perf impact
    internal class TradeCaptureService : ITradeCaptureService
    {
        private readonly Entities.StateTrackingDbContext _context;
        private readonly ILogger<TradeCaptureService> _logger;

        public TradeCaptureService(StateTrackingDbContext context, ILogger<TradeCaptureService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task ExportTradesAsync(string path)
        {
            var allTrades = await _context.Trades.ToListAsync();
            var sb = new StringBuilder();

            // Header
            sb.AppendLine("TradeId,OrderId,Symbol,Quantity,FilledQuantity,ExecPrice,PlacedPrice,PlacedOn,FilledOn");

            // Rows
            foreach (var trade in allTrades)
            {
                sb.AppendLine($"{trade.TradeId},{trade.OrderId},{trade.Symbol},{trade.Quantity},{trade.FilledQuantity},{trade.ExecPrice},{trade.PlacedPrice},{trade.PlacedOn:yyyy-MM-dd HH:mm:ss},{trade.FilledOn:yyyy-MM-dd HH:mm:ss}");
            }

            var directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await File.WriteAllTextAsync(path, sb.ToString());

        }
        public async Task OnNewTradeAsync(TradeDto tradeDto)
        {
            Entities.Trade trade = tradeDto.Map();
            await _context.AddAsync(trade);
            await _context.SaveChangesAsync();
            var message = $"""
        🧾 Trade Executed:
        - Trade ID       : {trade.TradeId}
        - Order ID       : {trade.OrderId}
        - Symbol         : {trade.Symbol}
        - Quantity       : {trade.Quantity}
        - Filled Quantity: {trade.FilledQuantity}
        - Exec Price     : {trade.ExecPrice}
        - Placed Price   : {trade.PlacedPrice}
        - Placed On      : {trade.PlacedOn:yyyy-MM-dd HH:mm:ss}
        - Filled On      : {trade.FilledOn:yyyy-MM-dd HH:mm:ss}
        """;
            _logger.LogInformation(message);
        }
    }
}
