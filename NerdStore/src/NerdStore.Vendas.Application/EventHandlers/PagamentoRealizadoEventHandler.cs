using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Vendas.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.EventHandlers
{
    public class PagamentoRealizadoEventHandler : INotificationHandler<PagamentoRealizadoEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PagamentoRealizadoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(PagamentoRealizadoEvent notification, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new FinalizarPedidoCommand(notification.PedidoId, notification.ClienteId));
        }
    }
}
