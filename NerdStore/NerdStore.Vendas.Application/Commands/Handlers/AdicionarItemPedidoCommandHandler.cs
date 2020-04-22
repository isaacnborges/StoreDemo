using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Handlers;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Domain.Entities;
using NerdStore.Vendas.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Commands.Handlers
{
    public class AdicionarItemPedidoCommandHandler : CommandHandlerBase, IRequestHandler<AdicionarItemPedidoCommand, bool>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public AdicionarItemPedidoCommandHandler(IMediatorHandler mediatorHandler, IPedidoRepository pedidoRepository) 
            : base(mediatorHandler)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> Handle(AdicionarItemPedidoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);
            var pedidoItem = new PedidoItem(request.ProdutoId, request.Nome, request.Quantidade, request.ValorUnitario);

            if (pedido == null)
            {
                pedido = CriarPedido(request, pedidoItem);
            }
            else
            {
                AtualizarPedido(pedido, pedidoItem);
            }

            pedido.AdicionarEvento(new PedidoItemAdicionadoEvent(pedido.ClienteId, pedido.Id, request.ProdutoId, request.Nome, request.ValorUnitario, request.Quantidade));
            return await _pedidoRepository.UnitOfWork.Commit();
        }

        private Pedido CriarPedido(AdicionarItemPedidoCommand request, PedidoItem pedidoItem)
        {
            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(request.ClienteId);
            pedido.AdicionarItem(pedidoItem);

            _pedidoRepository.Adicionar(pedido);
            pedido.AdicionarEvento(new PedidoRascunhoIniciadoEvent(request.ClienteId, request.ProdutoId));

            return pedido;
        }

        private void AtualizarPedido(Pedido pedido, PedidoItem pedidoItem)
        {
            var pedidoItemExistente = pedido.PedidoItemExistente(pedidoItem);
            pedido.AdicionarItem(pedidoItem);

            if (pedidoItemExistente)
                _pedidoRepository.AtualizarItem(pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == pedidoItem.ProdutoId));
            else
                _pedidoRepository.AdicionarItem(pedidoItem);

            pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
        }
    }
}
