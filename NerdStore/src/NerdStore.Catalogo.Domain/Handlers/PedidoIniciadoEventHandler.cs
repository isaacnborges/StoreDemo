using MediatR;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Handlers
{
    public class PedidoIniciadoEventHandler : INotificationHandler<PedidoIniciadoEvent>
    {
        private readonly IEstoqueService _estoqueService;
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoIniciadoEventHandler(IEstoqueService estoqueService, IMediatorHandler mediatorHandler)
        {
            _estoqueService = estoqueService;
            _mediatorHandler = mediatorHandler;
        }
        

        public async Task Handle(PedidoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            var result = await _estoqueService.DebitarListaProdutosPedido(notification.ProdutosPedido);

            if (result)
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueConfirmadoEvent(notification.PedidoId, notification.ClienteId, notification.Total, notification.NomeCartao, 
                    notification.NumeroCartao, notification.ExpiracaoCartao, notification.CvvCartao, notification.ProdutosPedido));
            }
            else
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueRejeitadoEvent(notification.PedidoId, notification.ClienteId));
            }
        }
    }
}
