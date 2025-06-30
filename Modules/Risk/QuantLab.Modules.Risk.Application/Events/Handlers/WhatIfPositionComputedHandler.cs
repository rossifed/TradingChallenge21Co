using QuantLab.Modules.Risk.Application.Dtos;
using QuantLab.Modules.Risk.Application.Mappers;
using QuantLab.Modules.Risk.Application.Services;
using QuantLab.Modules.Risk.Domain.Model;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.Risk.Application.Events.Handlers
{
    internal class WhatIfPositionComputedHandler : IEventHandler<WhatIfPositionComputed>
    {
        private readonly IRiskManagementService _riskManagementService;

        public WhatIfPositionComputedHandler(IRiskManagementService riskManagementService)
        {
            _riskManagementService = riskManagementService;
        }

        public async Task HandleAsync(WhatIfPositionComputed @event, CancellationToken cancellationToken = default)
        {
            Position pos = @event.Position.Map();
            PositionPnL pnl = @event.Pnl.Map();
            Order order = @event.Order.Map();
            WhatIfPosition whatifPos = new WhatIfPosition(order, pos, pnl);
            await _riskManagementService.PerformRiskConstraintCheckAsync(whatifPos);
        }
    }
}
