using QuantLab.Modules.Risk.Application.Dtos;
using QuantLab.Modules.Risk.Domain.Repositories;
using QuantLab.Shared.Abstractions.Events;

namespace QuantLab.Modules.Risk.Application.Events.Handlers
{
    internal class PositionUpdatedHandler : IEventHandler<PositionUpdated>
    {
        private readonly IPositionRepository _positionRepository;

        public PositionUpdatedHandler(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task HandleAsync(PositionUpdated @event, CancellationToken cancellationToken = default)
        {
            // await  _positionRepository.AddOrUpdate(@event.Position.Map(@event.PositionPnL));
            await Task.CompletedTask;
        }
    }
}
