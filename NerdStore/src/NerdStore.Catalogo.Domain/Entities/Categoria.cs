using NerdStore.Core.DomainObjects;
using System.Collections.Generic;

namespace NerdStore.Catalogo.Domain.Entities
{
    public class Categoria : Entity
    {
        public string Nome { get; private set; }
        public int Codigo { get; private set; }
        public ICollection<Produto> Produtos { get; set; }

        protected Categoria() { }

        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;

            Validar();
        }

        public void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome não pode ser vazio");
            Validacoes.ValidarSeIgual(Codigo, 0, "O campo Código não pode ser 0");
        }

        public override string ToString()
        {
            return $"{Nome} - {Codigo}";
        }
    }
}
