﻿using NerdStore.Core.DomainObjects;
using NerdStore.Pagamentos.Business.Enums;
using System;

namespace NerdStore.Pagamentos.Business.Entities
{
    public class Transacao : Entity
    {
        public Guid PedidoId { get; set; }
        public Guid PagamentoId { get; set; }
        public decimal Total { get; set; }
        public StatusTransacao StatusTransacao { get; set; }
        public Pagamento Pagamento { get; set; }
    }
}