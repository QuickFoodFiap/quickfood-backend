using Cora.Infra.Repository;
using Domain.Entities;
using Domain.Repositories;
using Infra.Context;

namespace Infra.Repositories
{
    public class PedidoRepository(ApplicationDbContext context) : RepositoryGeneric<Pedido>(context), IPedidoRepository
    {
    }
}
