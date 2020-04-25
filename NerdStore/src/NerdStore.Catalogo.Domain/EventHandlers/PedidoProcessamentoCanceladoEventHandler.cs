using MediatR;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.EventHandlers
{
    public class PedidoProcessamentoCanceladoEventHandler : INotificationHandler<PedidoProcessamentoCanceladoEvent>
    {
        private readonly IEstoqueService _estoqueService;

        public PedidoProcessamentoCanceladoEventHandler(IEstoqueService estoqueService)
        {
            _estoqueService = estoqueService;
        }

        public async Task Handle(PedidoProcessamentoCanceladoEvent notification, CancellationToken cancellationToken)
        {
            await _estoqueService.ReporListaProdutosPedido(notification.ProdutosPedido);
        }
    }
}
