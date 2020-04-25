using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Dtos;
using NerdStore.Core.Extensions;
using NerdStore.Core.Handlers;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.CommandHandlers
{
    public class IniciarPedidoCommandHandler : CommandHandlerBase, IRequestHandler<IniciarPedidoCommand, bool>
    {
        private readonly IPedidoRepository _pedidoRepository;

        public IniciarPedidoCommandHandler(IMediatorHandler mediatorHandler, IPedidoRepository pedidoRepository)
            : base(mediatorHandler)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<bool> Handle(IniciarPedidoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) 
                return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);
            pedido.IniciarPedido();

            var itensList = new List<Item>();
            pedido.PedidoItems.ForEach(i => itensList.Add(new Item { Id = i.ProdutoId, Quantidade = i.Quantidade }));
            var listaProdutosPedido = new ListaProdutosPedido { PedidoId = pedido.Id, Itens = itensList };

            pedido.AdicionarEvento(new PedidoIniciadoEvent(pedido.Id, pedido.ClienteId, pedido.ValorTotal, request.NomeCartao, request.NumeroCartao, request.ExpiracaoCartao, request.CvvCartao, listaProdutosPedido));

            _pedidoRepository.Atualizar(pedido);
            return await _pedidoRepository.UnitOfWork.Commit();
        }
    }
}
