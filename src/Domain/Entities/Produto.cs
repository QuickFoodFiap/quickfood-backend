using Core.Domain.Entities;
using Domain.ValuesObjects;

namespace Domain.Entities
{
    public class Produto : Entity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; set; }
        public Categoria Categoria { get; set; }

        public Produto(Guid id, string nome, string descricao, decimal preco, Categoria categoria)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Categoria = categoria;
        }
    }
}
