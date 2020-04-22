using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Events.Handlers
{
    public class PedidoItemAdicionadoEventHandler : INotificationHandler<PedidoItemAdicionadoEvent>
    {
        public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
