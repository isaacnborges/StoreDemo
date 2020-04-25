using MediatR;
using NerdStore.Vendas.Application.Events;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.EventHandlers
{
    public class PedidoRascunhoIniciadoEventHandler : INotificationHandler<PedidoRascunhoIniciadoEvent>
    {
        public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
