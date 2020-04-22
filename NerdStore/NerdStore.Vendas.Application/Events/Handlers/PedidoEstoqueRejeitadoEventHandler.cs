using MediatR;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Events.Handlers
{
    public class PedidoEstoqueRejeitadoEventHandler : INotificationHandler<PedidoEstoqueRejeitadoEvent>
    {
        public Task Handle(PedidoEstoqueRejeitadoEvent notification, CancellationToken cancellationToken)
        {
            //Cancelar o processamento do pedido
            return Task.CompletedTask;
        }
    }
}
