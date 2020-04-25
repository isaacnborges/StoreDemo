using MediatR;
using NerdStore.Core.Dtos;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Pagamentos.Business.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Pagamentos.Business.EventHandler
{
    public class PedidoEstoqueConfirmadoEventHandler : INotificationHandler<PedidoEstoqueConfirmadoEvent>
    {
        private readonly IPagamentoService _pagamentoService;

        public PedidoEstoqueConfirmadoEventHandler(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }
        
        public async Task Handle(PedidoEstoqueConfirmadoEvent notification, CancellationToken cancellationToken)
        {
            var pagamentoPedido = new PagamentoPedido
            {
                PedidoId = notification.PedidoId,
                ClienteId = notification.ClienteId,
                Total = notification.Total,
                NomeCartao = notification.NomeCartao,
                NumeroCartao = notification.NumeroCartao,
                ExpiracaoCartao = notification.ExpiracaoCartao,
                CvvCartao = notification.CvvCartao
            };

            await _pagamentoService.RealizarPagamentoPedido(pagamentoPedido);
        }
    }
}
