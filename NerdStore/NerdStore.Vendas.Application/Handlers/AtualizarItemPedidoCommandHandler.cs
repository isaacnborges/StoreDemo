﻿using MediatR;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Handlers;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Vendas.Application.Commands;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Handlers
{
    public class AtualizarItemPedidoCommandHandler : CommandHandlerBase, IRequestHandler<AtualizarItemPedidoCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPedidoRepository _pedidoRepository;

        public AtualizarItemPedidoCommandHandler(IMediatorHandler mediatorHandler, IPedidoRepository pedidoRepository)
            : base(mediatorHandler)
        {
            _pedidoRepository = pedidoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AtualizarItemPedidoCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request)) 
                return false;

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(request.ClienteId);

            if (pedido == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));
                return false;
            }

            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.Id, request.ProdutoId);

            if (!pedido.PedidoItemExistente(pedidoItem))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Item do pedido não encontrado!"));
                return false;
            }

            pedido.AtualizarUnidades(pedidoItem, request.Quantidade);

            pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteId, pedido.Id, pedido.ValorTotal));
            pedido.AdicionarEvento(new PedidoProdutoAtualizadoEvent(request.ClienteId, pedido.Id, request.ProdutoId, request.Quantidade));

            _pedidoRepository.AtualizarItem(pedidoItem);
            _pedidoRepository.Atualizar(pedido);

            return await _pedidoRepository.UnitOfWork.Commit();
        }
    }
}
