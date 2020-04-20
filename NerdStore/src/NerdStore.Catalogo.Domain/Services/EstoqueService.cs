using NerdStore.Catalogo.Domain.Events;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.Communication.Mediator;
using System;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatorHandler _mediator;

        public EstoqueService(IProdutoRepository produtoRepository, IMediatorHandler mediatorHandler)
        {
            _produtoRepository = produtoRepository;
            _mediator = mediatorHandler;
        }

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null)
                return false;

            if (!produto.PossuiEstoque(quantidade))
                return false;

            produto.DebitarEstoque(quantidade);

            // TODO: Parametrizar quantidade estoque baixo
            if (produto.QuantidadeEstoque < 10)
            {
                await _mediator.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
            }

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null)
                return false;
            
            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);
            return await _produtoRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
