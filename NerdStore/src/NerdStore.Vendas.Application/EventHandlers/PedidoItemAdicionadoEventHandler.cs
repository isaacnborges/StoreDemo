using MediatR;
using NerdStore.Vendas.Application.Events;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.EventHandlers
{
    public class PedidoItemAdicionadoEventHandler : INotificationHandler<PedidoItemAdicionadoEvent>
    {
        public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
