﻿using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Domain.ObjectValues
{
    public class Dimensoes
    {
        public decimal Altura { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Profundidade { get; private set; }

        public Dimensoes(decimal altura, decimal largura, decimal profundidade)
        {
            Validacoes.ValidarSeMenorQue(altura, 1, "O campo Altura deve ser maior que 0");
            Validacoes.ValidarSeMenorQue(largura, 1, "O campo Largura deve ser maior que 0");
            Validacoes.ValidarSeMenorQue(profundidade, 1, "O campo Profundidade deve ser maior que 0");

            Altura = altura;
            Largura = largura;
            Profundidade = profundidade;
        }

        public string DesricaoFormatada()
        {
            return $"LxAxP: {Largura} x {Altura} x {Profundidade}";
        }

        public override string ToString()
        {
            return DesricaoFormatada();
        }
    }
}
