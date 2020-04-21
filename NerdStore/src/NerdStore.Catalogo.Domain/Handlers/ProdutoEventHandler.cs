﻿using MediatR;
using NerdStore.Catalogo.Domain.Events;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Handlers
{
    public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
    {
        public Task Handle(ProdutoAbaixoEstoqueEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
