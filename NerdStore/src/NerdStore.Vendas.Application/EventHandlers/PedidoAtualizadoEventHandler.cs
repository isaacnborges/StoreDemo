using MediatR;
using NerdStore.Vendas.Application.Events;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.EventHandlers
{
    public class PedidoAtualizadoEventHandler : INotificationHandler<PedidoAtualizadoEvent>
    {
        public Task Handle(PedidoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
