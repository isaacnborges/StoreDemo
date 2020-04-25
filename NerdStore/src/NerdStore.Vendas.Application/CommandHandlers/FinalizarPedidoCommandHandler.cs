using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Handlers;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.CommandHandlers
{
    public class FinalizarPedidoCommandHandler : CommandHandlerBase, IRequestHandler<FinalizarPedidoCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPedidoRepository _pedidoRepository;

        public FinalizarPedidoCommandHandler(IMediatorHandler mediatorHandler, IPedidoRepository pedidoRepository) 
            : base(mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> Handle(FinalizarPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorId(request.PedidoId);

            if (pedido == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
                return false;
            }

            pedido.FinalizarPedido();

            pedido.AdicionarEvento(new PedidoFinalizadoEvent(request.PedidoId));
            return await _pedidoRepository.UnitOfWork.Commit();
        }
    }
}
