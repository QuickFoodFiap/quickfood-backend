using Core.Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Produto : Entity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public Categoria Categoria { get; private set; }
        public bool Ativo { get; private set; }

        public Produto(Guid id, string nome, string descricao, decimal preco, Categoria categoria, bool ativo)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Categoria = categoria;
            Ativo = ativo;
        }
    }
}
