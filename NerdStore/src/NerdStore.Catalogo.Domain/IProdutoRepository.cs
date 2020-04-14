using NerdStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> ObterPorId(Guid id);
        Task<IEnumerable<Produto>> ObterTodos();
        Task<IEnumerable<Produto>> ObterPorCategoria(int codigo);
        Task<IEnumerable<Categoria>> ObterCategorias();
        void Adicionar(Produto produto);
        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Atualizar(Produto produto);
    }
}
