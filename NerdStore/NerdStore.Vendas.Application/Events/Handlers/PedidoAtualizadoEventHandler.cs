using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Events.Handlers
{
    public class PedidoAtualizadoEventHandler : INotificationHandler<PedidoAtualizadoEvent>
    {
        public Task Handle(PedidoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
