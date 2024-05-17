using Core.Domain.Entities;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class PedidoItem : Entity
    {
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        public Pedido Pedido { get; private set; }

        // Relation
        protected PedidoItem() { }

        public PedidoItem(Guid produtoId, int quantidade, decimal valorUnitario)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantidade), "A quantidade deve ser maior que zero.");
            }

            ProdutoId = produtoId;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public void AssociarPedido(Guid pedidoId) =>
            PedidoId = pedidoId;

        public void AdicionarUnidades(int unidades)
        {
            if (unidades <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(unidades), "O número de unidades a serem adicionadas deve ser maior que zero.");
            }

            Quantidade += unidades;
        }

        public decimal CalcularValor() =>
            Quantidade * ValorUnitario;
    }
}
