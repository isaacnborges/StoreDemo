using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Events.Handlers
{
    public class PedidoRascunhoIniciadoEventHandler : INotificationHandler<PedidoRascunhoIniciadoEvent>
    {
        public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
