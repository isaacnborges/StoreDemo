using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Vendas.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.EventHandlers
{
    public class PedidoEstoqueRejeitadoEventHandler : INotificationHandler<PedidoEstoqueRejeitadoEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoEstoqueRejeitadoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(PedidoEstoqueRejeitadoEvent notification, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoCommand(notification.PedidoId, notification.ClienteId));
        }
    }
}
