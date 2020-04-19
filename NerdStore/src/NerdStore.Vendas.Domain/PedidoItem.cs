﻿using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Vendas.Domain
{
    public class PedidoItem : Entity
    {
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public Pedido Pedido { get; set; }

        public decimal CalcularValor() => Quantidade * ValorUnitario;

        internal void AssociarPedido(Guid pedidoId) => PedidoId = pedidoId;

        internal void AdicionarUnidades(int unidades) => Quantidade += unidades;

        internal void AtualizarUnidades(int unidades) => Quantidade = unidades;
    }
}
